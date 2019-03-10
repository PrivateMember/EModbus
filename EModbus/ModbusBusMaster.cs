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

	public class ModbusBusMaster
	{
		private ResponseType mResType;
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
		private ProducerConsumer mActionsQueueHP = new ProducerConsumer(); // High Priority
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


		public delegate void PollFinishedEventHandler(string data);

		public delegate void StatusChangeHandler(MasterStatus status);

		public event StatusChangeHandler OnStatusChanged = null;
		public event PollFinishedEventHandler OnPollFinished = null;



		public ModbusBusMaster()
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
			mActionsQueueHP.Produce(new EMasterActionAddPoll(poll, handler));
		}

		public void RemovePoll(UInt32 index, OnRemovePollHandler handler)
		{
			mActionsQueueHP.Produce(new EMasterActionRemovePoll(index, handler));
		}

		public void RemovePoll(ModbusPoll poll, bool all, OnRemovePollHandler handler)
		{
			mActionsQueueHP.Produce(new EMasterActionRemovePoll(poll , all, handler));
		}

		public void ReplacePoll(UInt32 index, ModbusPoll newPoll, OnReplaceEventHandler handler)
		{
			mActionsQueueHP.Produce(new EMasterActionReplacePoll(index, newPoll, handler));
		}

		public void ReplacePoll(ModbusPoll oldPoll, ModbusPoll newPoll, OnReplaceEventHandler handler)
		{
			mActionsQueueHP.Produce(new EMasterActionReplacePoll(oldPoll, newPoll, handler));
		}

		public void GetPoll(UInt32 index, Delegate OnGetPollHandler, OnGetPollHandler handler)
		{
			mActionsQueueHP.Produce(new EMasterActionGetPoll(index, handler));
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
			byte[] cmd = poll.GetPollCommand();

			TimeOut to = new TimeOut(poll.TimeoutMilisec);

			ModbusPollResponse response = new ModbusPollResponse(poll);

			mResType = ResponseType.OK;

			mErrorCode = ErrorCode.Stopped;

			mPort.Write(cmd, 0, cmd.Length);

			to.Start();

			while (true)
			{
				if (mPort.BytesToRead > 0)
				{
					response.AddRxData((byte)mPort.ReadByte());
				}


				if (response.Status == ModbusPollResponse.ResponseStatus.Finished ||
					response.Status == ModbusPollResponse.ResponseStatus.ErrorCode)
				{
					mErrorCode = response.Error;

					byte[] data = response.GetData();
					string result = poll.Name + " : " + BitConverter.ToString(data);

					if (mErrorCode == ErrorCode.None) { if (OnPollFinished != null) OnPollFinished(result); }
					//
					// interpret the data if poll was successful
					//

					break;
				}

				if (mStopSignal)
				{
					mErrorCode = ErrorCode.Stopped;
					break;
				}

				if (to.IsTimedOut())
				{
					mErrorCode = ErrorCode.Timeout;
					break;
				}

				Thread.Sleep(10);
			}

			if (mErrorCode != ErrorCode.None) { if (OnPollFinished != null) OnPollFinished(mErrorCode.ToString()); }

			to.Dispose();
		}

		private void CommandDispatching()
		{
			int devIndex = 0;
			bool doPoll = true;
			mStopSignal = false;
			TimeOut pollTimeout = new TimeOut(mPollTimeout);
			pollTimeout.Start();
			mPort.Open();

			EMasterActionAddPoll actionAdd;
			EMasterActionRemovePoll actionRem;
			EMasterActionReplacePoll actionRep;
			EMasterActionGetPoll actionGet;

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

					//if (mActionsQueueHP.Count == 0 && mActionsQueueLP.Count == 0)
					//{
					//	continue;
					//}

					if(mActionsQueueHP.Count > 0)
					{
						EMasterActionBase actionHP = mActionsQueueHP.Consume() as EMasterActionBase;

						switch(actionHP.Type)
						{
							//-------------------------------------------------------------
							case EMasterActionType.AddPoll:
								actionAdd = actionHP as EMasterActionAddPoll;

								if (actionAdd.Poll == null)
								{
									actionAdd.FireEvent(false, "Adding Poll faild : null polll inserted");
								}
								else
								{
									mPolls.Add(actionAdd.Poll.Clone() as ModbusPoll);
									actionAdd.FireEvent(true, "Adding Poll succeeded");
								} // check for clones in the list !!
								
								break;
							//-------------------------------------------------------------
							case EMasterActionType.RemovePoll:
								actionRem = actionHP as EMasterActionRemovePoll;

								if (mPolls == null)
								{
									actionRem.FireEvent(false, "removing Poll faild : null list");
								}
								else
								{
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
								break;
							//-------------------------------------------------------------
							case EMasterActionType.ReplacePoll:
								actionRep = actionHP as EMasterActionReplacePoll;
								if (mPolls == null) { } // "null list"
								else
								{
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
								break;
							//-------------------------------------------------------------
							case EMasterActionType.GetPoll:
								actionGet = actionHP as EMasterActionGetPoll;
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
								break;
						}
						continue;
					}

					if(mActionsQueueLP.Count > 0)
					{
						EMasterActionBase actionLP = mActionsQueueLP.Consume() as EMasterActionBase;
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
							pollTimeout.Reset();
							pollTimeout.Start();
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

			mPort.Close();
			mpStatus = MasterStatus.Stopped;
			mStopSignal = false;
		}

	}
}