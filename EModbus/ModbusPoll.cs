using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public partial class ModbusMaster
	{
		public class ModbusPoll : ICloneable
		{
			protected byte[] mRawData = null;
			private bool mDataValid = false;
			private UInt32 mPollCounter = 0;
			private List<PollInterpreterMap> mDataMaps = new List<PollInterpreterMap>();

			#region internal methdos
			// these methods are only accessible from ModbusMaster Class which is inside this assembly
			internal void SetResponseData(byte[] data) { mRawData = data; } // mRawData = data.Clone() as byte[]
			internal void SetDataValidity(bool state) { mDataValid = state; }
			internal void IncrementCounter() { mPollCounter++; }
			#endregion internal methods

			public List<PollInterpreterMap> DataMaps { get { return mDataMaps; } }
			public UInt32 PollCounter { get { return mPollCounter; } }
			public RegisterOrder RegOrder { get; set; } = RegisterOrder.MSRFirst;
			public ByteOrder ByteOrder { get; set; } = ByteOrder.MSBFirst;
			public byte[] ResponseData { get { return mRawData == null ? null : mRawData.Clone() as byte[]; } }
			public bool DataValid { get { return mDataValid; } }
			public byte DeviceID { get; set; }
			public UInt16 DataAddress { get; set; }
			public UInt16 DataCount { get; set; }
			public string Name { get; set; } = "";
			public bool Enabled { get; set; } = false;
			public UInt32 TimeoutMilisec { get; set; } = 1000;
			public ModbusObjectType ObjectType { get; set; } = ModbusObjectType.HoldingRegister;
			public UInt16 ResponseLength
			{
				get { return (UInt16)(ResponseDataLengthBytes + 5); }
			}
			public byte ResponseDataLengthBytes
			{
				get
				{
					if (ObjectType == ModbusObjectType.HoldingRegister || ObjectType == ModbusObjectType.InputRegister)
					{
						return (byte)(DataCount * 2);
					}
					else if (ObjectType == ModbusObjectType.Coil || ObjectType == ModbusObjectType.DiscreteInput)
					{
						if (DataCount % 8 == 0) return (byte)(DataCount / 8);
						else return (byte)(DataCount / 8 + 1);
					}
					else
					{
						return 0;
					}
				}
			}
			
			public ModbusPoll(byte devID, UInt16 address, UInt16 count, ModbusObjectType oType)
			{
				DeviceID = devID;
				DataAddress = address;
				DataCount = count;
				ObjectType = oType;

				DataMaps.Add(new PollInterpreterMap(ResponseDataLengthBytes)); // default map
			}

			public ModbusPoll(ModbusPoll poll)
			{
				DeviceID = poll.DeviceID;
				DataAddress = poll.DataAddress;
				DataCount = poll.DataCount;
				ObjectType = poll.ObjectType;
				Name = poll.Name;
				Enabled = poll.Enabled;
				TimeoutMilisec = poll.TimeoutMilisec;
				mDataValid = poll.mDataValid;
				if (poll.mRawData != null)
					mRawData = poll.mRawData.Clone() as byte[];
				mPollCounter = poll.mPollCounter;
				this.ByteOrder = poll.ByteOrder;
				this.RegOrder = poll.RegOrder;

				mDataMaps = new List<PollInterpreterMap>(poll.DataMaps.Count);

				foreach (PollInterpreterMap map in poll.DataMaps)
				{
					mDataMaps.Add((PollInterpreterMap)map.Clone());
				}
			}

			public bool IsClone(ModbusPoll poll)
			{
				if (poll == this) return true;

				if (poll.DeviceID == this.DeviceID &&
					poll.DataAddress == DataAddress &&
					poll.DataCount == DataCount &&
					poll.ObjectType == ObjectType &&
					poll.RegOrder == this.RegOrder &&
					poll.ByteOrder == this.ByteOrder)
				{
					return true;
				}

				return false;
			}

			public byte[] GetPollCommand()
			{
				ModbusCommandRead cmd = new ModbusCommandRead(ObjectType, DataAddress, DataCount);
				byte[] readCmd = cmd.ToBytes();
				byte[] pollCmd = new byte[readCmd.Length + 3];
				pollCmd[0] = DeviceID;
				readCmd.CopyTo(pollCmd, 1);
				UInt16 crc = Utilities.Utils.CRC16(pollCmd, (UInt32)pollCmd.Length - 2);
				pollCmd[pollCmd.Length - 2] = (byte)(crc & 0xFF);
				pollCmd[pollCmd.Length - 1] = (byte)(crc >> 8);
				return pollCmd;
			}

			public static ModbusPoll PollWizard(ModbusPoll poll = null)
			{
				ModbusPollDefinitionForm form = new ModbusPollDefinitionForm();
				form.SetPoll(poll);

				if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					return form.GetPoll();
				}
				return null;
			}

			public object Clone()
			{
				return new ModbusPoll(this);
			}

			public string MapToString(int mapIndex)
			{
				ModbusPollParameter p;
				string str = "";
				byte[] data = mRawData.Clone() as byte[];
				for (int i = 0; i < DataMaps[mapIndex].Parameters.Count; i++)
				{
					p = DataMaps[mapIndex].Parameters[i];
					str += "( " + p.ByteIndex.ToString("D2") + "," + p.ByteCount + " , " + p.BitIndex + " )\t";
					str += p.Name + "\t";
					str += p.Type + "\t";
					//DataReorder(ResponseData, )
					DataReorder(data, p.ByteIndex, DataMaps[mapIndex].Parameters[i].Type, this.RegOrder, this.ByteOrder);
					object obj = DataMaps[mapIndex].GetParamValue(data, i);
					str += obj.ToString();
					str += "\r\n";
				}

				return str;
			}
			public string MapToString()
			{
				ModbusPollParameter p;

				string str = Name + "[" + PollCounter + "]\r\n";

				for (int mapIndex = 0; mapIndex < DataMaps.Count; mapIndex++)
				{
					str += MapToString(mapIndex);
					str += "\r\n";
				}

				return str;
			}

			private void SwapRegisterBytes(byte[] data, int regIndex, int startIndex)
			{
				byte swap = data[startIndex + regIndex * 2];
				data[startIndex + regIndex * 2] = data[startIndex + regIndex * 2 + 1];
				data[startIndex + regIndex * 2 + 1] = swap;
			}

			private void SwapRegisters(byte[] data, int regIndex1, int regIndex2, int startIndex)
			{
				byte swap;

				swap = data[startIndex + regIndex1 * 2];
				data[startIndex + regIndex1 * 2] = data[startIndex + regIndex2 * 2];
				data[startIndex + regIndex2 * 2] = swap;

				swap = data[startIndex + regIndex1 * 2 + 1];
				data[startIndex + regIndex1 * 2 + 1] = data[startIndex + regIndex2 * 2 + 1];
				data[startIndex + regIndex2 * 2 + 1] = swap;
			}

			private void DataReorder(byte[] data, int startIndex, DataType type, RegisterOrder rOrder = RegisterOrder.MSRFirst, ByteOrder bOrder = ByteOrder.MSBFirst)
			{
				int regCount = 0;
				switch (type)
				{
					case DataType.UInt64:
					case DataType.Int64:
					case DataType.Double:
						regCount = 4; break;
					case DataType.UInt32:
					case DataType.Int32:
					case DataType.Float:
						regCount = 2; break;
					case DataType.UInt16:
					case DataType.Int16:
						regCount = 1; break;
					default: return;
				}

				if (regCount == 1)
				{
					if (bOrder == ByteOrder.MSBFirst)
					{
						SwapRegisterBytes(data, 0, startIndex);
					}
				}
				else if (regCount == 2)
				{
					if (RegOrder == RegisterOrder.MSRFirst)
					{
						SwapRegisters(data, 0, 1, startIndex);
					}
					if (bOrder == ByteOrder.MSBFirst)
					{
						SwapRegisterBytes(data, 0, startIndex);
						SwapRegisterBytes(data, 1, startIndex);
					}
				}
				else if (regCount == 4)
				{
					if (RegOrder == RegisterOrder.MSRFirst)
					{
						SwapRegisters(data, 0, 3, startIndex);
						SwapRegisters(data, 1, 2, startIndex);
					}
					if (bOrder == ByteOrder.MSBFirst)
					{
						SwapRegisterBytes(data, 0, startIndex);
						SwapRegisterBytes(data, 1, startIndex);
						SwapRegisterBytes(data, 2, startIndex);
						SwapRegisterBytes(data, 3, startIndex);
					}
				}
			}
		}
	}
}