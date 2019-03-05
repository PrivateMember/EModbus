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
	public class ModbusBusMaster
	{
		public enum Status
		{
			Stopping,
			Stopped,
			Running,
			Paused
		}

		private ResponseType mResType;
		private ErrorCode mErrorCode = 0;
		private volatile Status mStatus = Status.Stopped;
		private volatile bool mStopSignal = false;
		private volatile bool mPauseSignal = false;
		private volatile bool mWriteExist = false;
		private volatile bool mStoped = true;
		private volatile UInt32 mPollTimeout = 5000;
		private List<ModbusPoll> mPolls = new List<ModbusPoll>();
		private MBCommInterface mComm = null;
		private SerialPort mPort = null;
		private Thread mDispatchingThread = null;

		public List<ModbusPoll> Polls { get { return new List<ModbusPoll>(mPolls); } }

		public string Description { set; get; }

		public ModbusBusMaster()
		{
			Description = "";
		}

		public void SetComm(SerialPort port)
		{
			if (mStatus == Status.Stopped)
			{
				// check port status and availability
				mPort = port;
			}
			else
			{
				throw new Exception("Stop the thread first !!");
			}
		}

		public void AddPoll(ModbusPoll poll)
		{
			// try to lock on devices for thread safety
			if(poll != null) mPolls.Add(poll);
		}


		public void RemovePoll(ModbusPoll poll, bool all = false)
		{
			if (poll != null)
			{
				foreach(ModbusPoll d in mPolls)
				{
					if(d.IsClone(poll))
					{
						mPolls.Remove(poll);
						if(!all)break;
					}
				}
			}
		}

		public void Start()
		{
			if (mStatus == Status.Stopped)
			{
				mDispatchingThread = new Thread(CommandDispatching);
				mDispatchingThread.Start();
			}
			else
			{
				throw new Exception(Description + " is already running!!");
			}
		}

		public void Stop()
		{
			if (mStatus == Status.Running || mStatus == Status.Paused)
			{
				mStopSignal = true;
				while (mStatus != Status.Stopped) Thread.Sleep(10);
			}
			else
			{
				throw new Exception(Description + " is " + mStatus.ToString() + " !!");
			}
		}

		public void Pause()
		{
			mPauseSignal = true;
		}

		public void Resume()
		{
			mPauseSignal = false;
		}

		private void CommandDispatching()
		{
			int devIndex = 0;
			bool doPoll = true;
			mStopSignal = false;
			TimeOut pollTimeout = new TimeOut(mPollTimeout);
			pollTimeout.Start();
			mPort.Open();

			while (!mStopSignal)
			{
				if (mPauseSignal)
				{
					mStatus = Status.Paused;
					mPauseSignal = false;
				}
				else
				{
					mStatus = Status.Running;
					if (doPoll)
					{
						ModbusPoll poll = mPolls[devIndex];

						if (poll.Enabled)
						{
							ExecutePoll(poll);
							//
							// interpret the data if poll was successful
							//
						}

						devIndex++;
						if (devIndex >= mPolls.Count)
						{
							devIndex = 0;
							doPoll = false;
						}
					}
					else
					{
						if (pollTimeout.IsTimedOut() && doPoll == false)
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

				Thread.Sleep(20);
			}

			mPort.Close();
			mStatus = Status.Stopped;
			mStopSignal = false;
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
				if(mPort.BytesToRead > 0)
				{
					response.AddRxData((byte)mPort.ReadByte());
				}

				if (response.Status == ModbusPollResponse.ResponseStatus.Finished || response.Status == ModbusPollResponse.ResponseStatus.ErrorCode)
				{
					mErrorCode = response.Error;
					break;
				}

				if(mStopSignal)
				{
					mErrorCode = ErrorCode.Stopped;
					break;
				}

				if (to.IsTimedOut())
				{
					mErrorCode = ErrorCode.Timeout;
					break;
				}

				Thread.Sleep(20);
			}

			to.Dispose();
		}
	}
}