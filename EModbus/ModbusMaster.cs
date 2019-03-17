using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using Utilities;

namespace EModbus
{
	public enum MasterStatus : UInt32
	{
		Stopping,
		Stopped,
		Running,
		Paused
	}

	public class ModbusMaster
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

				foreach(PollInterpreterMap map in poll.DataMaps)
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

			public static ModbusPoll PollWizard()
			{
				ModbusPoll poll = null;

				ModbusPollDefinitionForm form = new ModbusPollDefinitionForm();

				if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					poll = form.GetPoll();
				}

				return poll;
			}

			public object Clone()
			{
				return new ModbusPoll(this);
			}

			public string MapToString(int mapIndex)
			{
				ModbusPollParameter p;
				string str = "";
				for (int i = 0; i < DataMaps[mapIndex].Parameters.Count; i++)
				{
					p = DataMaps[mapIndex].Parameters[i];
					str += "( " + p.ByteIndex.ToString("D2") + "," + p.ByteCount + " , " + p.BitIndex + " )\t";
					str += p.Name + "\t";
					str += p.Type + "\t";
					//DataReorder(ResponseData, )
					DataReorder(mRawData, p.ByteIndex, DataMaps[mapIndex].Parameters[i].Type, this.RegOrder, this.ByteOrder);
					object obj = DataMaps[mapIndex].GetParamValue(mRawData, i);
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

				if(regCount == 1)
				{
					if(bOrder == ByteOrder.MSBFirst)
					{
						SwapRegisterBytes(data, 0, startIndex);
					}
				}
				else if(regCount == 2)
				{
					if(RegOrder == RegisterOrder.MSRFirst)
					{
						SwapRegisters(data, 0, 1, startIndex);
					}
					if (bOrder == ByteOrder.MSBFirst)
					{
						SwapRegisterBytes(data, 0, startIndex);
						SwapRegisterBytes(data, 1, startIndex);
					}
				}
				else if(regCount == 4)
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

		private ErrorCode mErrorCode = 0;
		private volatile MasterStatus mStatus = MasterStatus.Stopped;
		private volatile bool mStopSignal = false;
		private volatile bool mPauseSignal = false;
		private volatile bool mResumeSignal = false;
		private volatile bool mWriteExist = false;
		private volatile UInt32 mPollTimeout = 2000;
		private List<ModbusPoll> mPolls = new List<ModbusPoll>();
		private MBCommInterface mComm = null;
		private SerialPort mPort = null;
		private Thread mDispatchingThread = null;
		private ProducerConsumer mActionsQueueCR = new ProducerConsumer(); // critical commands (stop, start, pause, resume)
		private ProducerConsumer mUserPollActionQueue = new ProducerConsumer(); // High Priority
		private ProducerConsumer mActionsQueueLP = new ProducerConsumer(); // Low Priorirty

		private MasterStatus mpStatus
		{
			get { return mStatus; }
			set
			{
				if (mStatus != value)
				{
					mStatus = value;
					OnStatusChanged?.Invoke(mStatus);
				}
			}
		}
		public MasterStatus Status { get { return mStatus; } }
		public List<ModbusPoll> Polls { get { return new List<ModbusPoll>(mPolls); } }
		public string Description { set; get; }


		public delegate void PollFinishedEventHandler(string data, ModbusPoll poll);
		public delegate void StatusChangeHandler(MasterStatus status);
		public event StatusChangeHandler OnStatusChanged = null;
		public event PollFinishedEventHandler OnPollFinished = null;

		public ModbusMaster()
		{
			Description = "";
		}

		public void SetComm(SerialPort port)
		{
			if (mStatus == MasterStatus.Stopped)
			{
				// check port status and availability
				mPort = port;
			}
			else
			{
				throw new Exception("Stop the thread first !!");
			}
		}

		public void AddPoll(ModbusPoll poll, OnAddPollHandler handler)
		{
			mUserPollActionQueue.Produce(new UserPollCommandAdd(poll, handler));
		}

		public void RemovePoll(UInt32 index, OnRemovePollHandler handler)
		{
			mUserPollActionQueue.Produce(new UserPollCommandRemove(index, handler));
		}

		public void RemovePoll(ModbusPoll poll, bool all, OnRemovePollHandler handler)
		{
			mUserPollActionQueue.Produce(new UserPollCommandRemove(poll , all, handler));
		}

		public void ReplacePoll(UInt32 index, ModbusPoll newPoll, OnReplaceEventHandler handler)
		{
			mUserPollActionQueue.Produce(new UserPollCommandReplace(index, newPoll, handler));
		}

		public void ReplacePoll(ModbusPoll oldPoll, ModbusPoll newPoll, OnReplaceEventHandler handler)
		{
			mUserPollActionQueue.Produce(new UserPollCommandReplace(oldPoll, newPoll, handler));
		}

		public void GetPoll(UInt32 index, Delegate OnGetPollHandler, OnGetPollHandler handler)
		{
			mUserPollActionQueue.Produce(new UserPollCommandGet(index, handler));
		}

		public ModbusPoll GetPoll(UInt32 index)
		{
			if (mPolls == null) return null;
			if (index >= mPolls.Count) return null;
			return mPolls[(int)index].Clone() as ModbusPoll;
		}

		public void Start()
		{
			if (mStatus == MasterStatus.Stopped)
			{
				mDispatchingThread = new Thread(CommandDispatching);
				mDispatchingThread.Name = "Worker thread";
				mDispatchingThread.Start();
			}
			else
			{
				throw new Exception(Description + " is already running!!");
			}
		}

		public void Stop()
		{
			if (mStatus == MasterStatus.Running || mStatus == MasterStatus.Paused)
			{
				mStopSignal = true;
				while (mStatus != MasterStatus.Stopped) Thread.Sleep(10);
			}
			else
			{

			}
		}

		/// <summary>
		/// this function puts the calling thread to sleep untill the status is changed to pause
		/// </summary>
		public void Pause()
		{
			if (mStatus == MasterStatus.Running)
			{
				mPauseSignal = true;
				Thread th = Thread.CurrentThread;
				while (mStatus != MasterStatus.Paused) Thread.Sleep(10);
			}
		}

		public void Resume()
		{
			if (mStatus == MasterStatus.Paused)
			{
				mResumeSignal = true;
				while (mStatus != MasterStatus.Running) Thread.Sleep(10);
			}
		}

		private void ExecutePoll(ModbusPoll poll)
		{
			string result = poll.Name + " : ";
			byte[] cmd = poll.GetPollCommand();
			TimeOut to = new TimeOut(poll.TimeoutMilisec);
			ModbusPollResponse response = new ModbusPollResponse(poll);
			mErrorCode = ErrorCode.None;

			poll.SetDataValidity(false);
			poll.SetResponseData(null);

			mPort.Write(cmd, 0, cmd.Length);
			to.Start();

			while (true)
			{
				Thread.Sleep(10);
				if (mStopSignal) { mErrorCode = ErrorCode.Stopped; break; }
				if (to.IsTimedOut()) { mErrorCode = ErrorCode.Timeout; break; }

				if (mPort.BytesToRead == 0) continue;

				response.AddRxData((byte)mPort.ReadByte());

				if (response.Status != ModbusPollResponse.ResponseStatus.Finished) continue;

				mErrorCode = response.Error;

				break;
			}

			to.Dispose();

			if (mErrorCode != ErrorCode.None)
			{
				result += mErrorCode.ToString();
			}
			else if (response.RespType == ResponseType.Data)
			{
				poll.SetDataValidity(true);
				poll.SetResponseData(response.GetData());
				result += BitConverter.ToString(poll.ResponseData);
				// interpret the data (or it is up to user ?!)
			}
			else
			{
				result += "Exception ( " + response.ModbusException.ToString() + " )";
			}

			poll.IncrementCounter();

			OnPollFinished?.Invoke(result, poll);
		}

		private void ExecuteActionAddPoll(UserPollCommandAdd actionAdd)
		{
			if (actionAdd.Poll == null)
			{
				actionAdd.FireEvent(false, "Adding Poll faild : null poll inserted");
			}
			else
			{
				mPolls.Add(actionAdd.Poll.Clone() as ModbusPoll);
				actionAdd.FireEvent(true, "Adding Poll succeeded");
			} // check for clones in the list !!
		}

		private void ExecuteActionRemPoll(UserPollCommandRemove actionRem)
		{
			if (mPolls == null)
			{
				actionRem.FireEvent(false, "removing Poll faild : null list");
				return;
			}
			

			if (actionRem.Mode == PollRemoveMode.ByIndex)
			{
				if (actionRem.PollIndex >= mPolls.Count)
				{
					actionRem.FireEvent(false, "removing Poll faild : index out of bounds");
				}
				else
				{
					mPolls.RemoveAt((int)actionRem.PollIndex);
					actionRem.FireEvent(false, "removing Poll succeeded");
				}
			}
			else
			{
				if (actionRem.Poll == null)
				{
					actionRem.FireEvent(false, "removing Poll faild : null poll inserted");
				}
				else
				{
					mPolls.Remove(actionRem.Poll);
					actionRem.FireEvent(false, "removing Poll succeeded");
				}

				//// if all
				//foreach (ModbusPoll d in mPolls)
				//{
				//	if (d.IsClone(poll))
				//	{
				//		mPolls.Remove(poll);
				//		if (!all) break;
				//	}
				//}
			}
		}

		private void ExecuteActionRepPoll(UserPollCommandReplace actionRep)
		{
			if (mPolls == null) { return; } // "null list"
			
			if (actionRep.Mode == PollRemoveMode.ByIndex)
			{
				if (actionRep.OldPollIndex >= mPolls.Count)
				{
					actionRep.FireEvent(0, null, false, "index out of bounds");
				}
				else if (actionRep.NewPoll == null)
				{
					actionRep.FireEvent(0, null, false, "null poll");
				}
				else
				{
					mPolls.RemoveAt((int)actionRep.OldPollIndex);
					mPolls.Insert((int)actionRep.OldPollIndex, actionRep.NewPoll.Clone() as ModbusPoll);
					actionRep.FireEvent(actionRep.OldPollIndex, actionRep.NewPoll, true, "succeeded");
				}
			}
			else
			{
				if (actionRep.OldPoll == null || actionRep.NewPoll == null)
				{
					actionRep.FireEvent(0, null, false, "null poll");
				}
				else
				{
					int index = mPolls.IndexOf(actionRep.OldPoll);
					mPolls.RemoveAt(index);
					mPolls.Insert(index, actionRep.NewPoll.Clone() as ModbusPoll);
					actionRep.FireEvent((UInt32)index, actionRep.NewPoll, true, "succeeded");
				}
			}
		}

		private void ExecuteActionGetPoll(UserPollCommandGet actionGet)
		{
			if (mPolls == null) { } // "null list"
			else
			{
				if (actionGet.PollIndex >= mPolls.Count)
				{
					actionGet.FireEvent(null); // "index out of bounds"
				}
				else
				{
					ModbusPoll poll = mPolls[(int)actionGet.PollIndex].Clone() as ModbusPoll;
					actionGet.FireEvent(poll);
				}
			}
		}

		private void ExecuteUserPollCommand(UserPollCommand actionHP)
		{
			switch (actionHP.Type)
			{
				//-------------------------------------------------------------
				case EMasterActionType.AddPoll:
					ExecuteActionAddPoll(actionHP as UserPollCommandAdd);
					break;
				//-------------------------------------------------------------
				case EMasterActionType.RemovePoll:
					ExecuteActionRemPoll(actionHP as UserPollCommandRemove);
					break;
				//-------------------------------------------------------------
				case EMasterActionType.ReplacePoll:
					ExecuteActionRepPoll(actionHP as UserPollCommandReplace);
					break;
				//-------------------------------------------------------------
				case EMasterActionType.GetPoll:
					ExecuteActionGetPoll(actionHP as UserPollCommandGet);
					break;
			}
		}

		private void CommandDispatching()
		{
			int devIndex = 0;
			bool doPoll = true;
			mStopSignal = false;
			TimeOut pollTimeout = new TimeOut(mPollTimeout);
			pollTimeout.Start();
			mPort.Open();

			mpStatus = MasterStatus.Running;
			try
			{
				while (!mStopSignal)
				{
					Thread.Sleep(10);

					if (mPauseSignal)
					{
						mPauseSignal = false;
						mpStatus = MasterStatus.Paused;
					}

					if (mResumeSignal)
					{
						mResumeSignal = false;
						mpStatus = MasterStatus.Running;
					}

					if (mStatus == MasterStatus.Paused) continue;

					if (mUserPollActionQueue.Count > 0)
					{
						UserPollCommand actionHP = mUserPollActionQueue.Consume() as UserPollCommand;
						ExecuteUserPollCommand(actionHP);
						continue;
					}

					if (mActionsQueueLP.Count > 0)
					{
						UserPollCommand actionLP = mActionsQueueLP.Consume() as UserPollCommand;
						continue;
					}

					if (doPoll)
					{
						if (devIndex >= mPolls.Count)
						{
							devIndex = 0;
							doPoll = false;
						}
						else
						{
							ModbusPoll poll = mPolls[devIndex];

							if (poll.Enabled)
							{
								ExecutePoll(poll);
							}

							devIndex++;
						}
					}
					else
					{
						if (pollTimeout.IsTimedOut())
						{
							doPoll = true;
							pollTimeout.ReStart();
						}
					}

					if (mWriteExist)
					{
						mWriteExist = false;
						// write parameters
					}
				}
			}
			catch (Exception wxp)
			{
				System.Windows.Forms.MessageBox.Show(wxp.Message);
			}
			finally
			{
				mPort.Close();
				mpStatus = MasterStatus.Stopped;
				mStopSignal = false;
			}
		}
	}
}