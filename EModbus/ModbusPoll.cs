using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public class ModbusPoll : ICloneable
	{
		private byte mDevID;
		private UInt16 mDataAddress;
		private UInt16 mDataCount;
		private volatile bool mBusy = false;
		private ObjectType mObjType = ObjectType.HoldingRegister;
		private Utilities.ProducerConsumer mCmdQueue = new Utilities.ProducerConsumer();
		public byte DeviceID { get { return mDevID; } }
		public UInt16 DataAddress { get { return mDataAddress; } }
		public UInt16 DataCount { get { return mDataCount; } }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public bool IsBusy { get { return mBusy; } }
		public UInt32 TimeoutMilisec { get; set; }

		public ObjectType ObjType { get { return mObjType; } }

		public ModbusPoll(byte devID, UInt16 address, UInt16 count, ObjectType oType)
		{
			mDevID = devID;
			mDataAddress = address;
			mDataCount = count;
			mObjType = oType;
			Name = "";
			Enabled = false;
			TimeoutMilisec = 1000;
		}


		// (incomplete) expand the function to comprae full details not just the address
		public bool IsClone(ModbusPoll dev)
		{
			bool result = true;
			if (dev == this) result = true;
			else
			{
				if (dev.DeviceID == this.DeviceID) result = true;
				else result = false;
			}
			return result;
		}

		public byte[] GetPollCommand()
		{
			ModbusCommandRead cmd = new ModbusCommandRead(mObjType, mDataAddress, mDataCount);
			byte[] readCmd = cmd.ToBytes();
			byte[] pollCmd = new byte[readCmd.Length + 3];
			pollCmd[0] = DeviceID;
			readCmd.CopyTo(pollCmd, 1);
			UInt16 crc = Utilities.Utils.CRC16(pollCmd, (UInt32)pollCmd.Length - 2);
			pollCmd[pollCmd.Length - 2] = (byte)(crc & 0xFF);
			pollCmd[pollCmd.Length - 1] = (byte)(crc >> 8);
			return pollCmd;
		}

		public UInt16 PollResponseLength
		{
			get { return (UInt16)(PollResponseDataLength + 5); }
		}

		public byte PollResponseDataLength
		{
			get
			{
				if (mObjType == ObjectType.HoldingRegister || mObjType == ObjectType.InputRegister)
				{
					return (byte)(DataCount * 2);
				}
				else if (mObjType == ObjectType.Coil || mObjType == ObjectType.DiscreteInput)
				{
					if (mDataCount % 8 == 0) return (byte)(DataCount / 8);
					else return (byte)(DataCount / 8 + 1);
				}
				else
				{
					return 0;
				}
			}
		}



		public static ModbusPoll PollWizard()
		{
			ModbusPoll poll = null;

			ModbusPollDefinitionForm form = new ModbusPollDefinitionForm();

			if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				poll = form.GetPoll();
			}

			return poll;
		}

		public object Clone()
		{
			ModbusPoll poll = new ModbusPoll(mDevID, mDataAddress, mDataCount, mObjType);
			poll.Name = Name;
			poll.TimeoutMilisec = TimeoutMilisec;
			poll.Enabled = Enabled;
			poll.mBusy = mBusy;
			return poll;
		}
	}
}
