using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	enum EMasterActionType
	{
		NoAction,
		AddPoll,
		RemovePoll,
		ReplacePoll,
		GetPoll,
	}

	class UserPollCommand
	{
		private EMasterActionType mType = EMasterActionType.NoAction;

		public EMasterActionType Type { get { return mType; } }

		public UserPollCommand(EMasterActionType type)
		{
			mType = type;
		}
	}

	public delegate void OnAddPollHandler(bool state, string message);

	class UserPollCommandAdd : UserPollCommand
	{
		private ModbusPoll mPoll = null;
		public OnAddPollHandler EventHandler = null;
		public ModbusPoll Poll
		{
			get { return mPoll.Clone() as ModbusPoll; }
			set { mPoll = value.Clone() as ModbusPoll; }
		}

		public UserPollCommandAdd(ModbusPoll poll, OnAddPollHandler handler) : base(EMasterActionType.AddPoll)
		{
			mPoll = poll.Clone() as ModbusPoll;
			this.EventHandler = handler;
		}

		public void FireEvent(bool state, string message)
		{
			EventHandler?.Invoke(state, message);
		}
	}

	enum PollRemoveMode { ByObject, ByIndex }
	public delegate void OnRemovePollHandler(bool state, string message);
	class UserPollCommandRemove : UserPollCommand
	{
		public event OnRemovePollHandler OnRemovePoll = null;
		private ModbusPoll mPoll = null;
		private PollRemoveMode mMode = PollRemoveMode.ByObject;
		private UInt32 mIndex = 0;
		public bool RemoveAll { get; set; }
		public ModbusPoll Poll
		{
			get
			{
				if (mPoll != null) return mPoll.Clone() as ModbusPoll;
				else return null;
			}
			set
			{
				if (value != null)
				{
					mPoll = value.Clone() as ModbusPoll;
					mIndex = 0;
					mMode = PollRemoveMode.ByObject;
				}
			}
		}

		public UInt32 PollIndex
		{
			get { return mIndex; }
			set
			{
				if (value >= 0)
				{
					mIndex = value;
					mPoll = null;
					mMode = PollRemoveMode.ByIndex;
				}
			}
		}

		public PollRemoveMode Mode
		{
			get { return mMode; }
		}

		public UserPollCommandRemove(ModbusPoll poll, bool removeAll, OnRemovePollHandler handelr) : base(EMasterActionType.RemovePoll)
		{
			Poll = poll;
			RemoveAll = removeAll;
			OnRemovePoll += handelr;
		}

		public UserPollCommandRemove(UInt32 index, OnRemovePollHandler handelr) : base(EMasterActionType.RemovePoll)
		{
			PollIndex = index;
			RemoveAll = false;
			OnRemovePoll += handelr;
		}

		public void FireEvent(bool state, string message)
		{
			OnRemovePoll?.Invoke(state, message);
		}
	}

	public delegate void OnGetPollHandler(ModbusPoll poll);

	class UserPollCommandGet : UserPollCommand
	{
		private UInt32 mIndex = 0;
		public event OnGetPollHandler OnGetPoll = null;

		public UInt32 PollIndex { get { return mIndex; } }

		public UserPollCommandGet(UInt32 index, OnGetPollHandler handler) : base(EMasterActionType.GetPoll)
		{
			mIndex = index >= 0 ? index : 0;
			OnGetPoll += handler;
		}

		public void FireEvent(ModbusPoll poll)
		{
			OnGetPoll?.Invoke(poll);
		}
	}

	public delegate void OnReplaceEventHandler(UInt32 index, ModbusPoll poll, bool state, string message);

	class UserPollCommandReplace : UserPollCommand
	{
		private ModbusPoll mOldPoll = null;
		private ModbusPoll mNewPoll = null;
		private uint mOldPollIndex = 0;
		private bool mReplaceAll = false;
		private PollRemoveMode mMode = PollRemoveMode.ByObject;

		public event OnReplaceEventHandler OnReplacePoll = null;

		public PollRemoveMode Mode
		{
			get { return mMode; }
		}

		public ModbusPoll OldPoll
		{
			get
			{
				if (mOldPoll == null) return null;
				else return mOldPoll.Clone() as ModbusPoll;
			}
		}

		public UInt32 OldPollIndex { get { return mOldPollIndex; } }

		public ModbusPoll NewPoll
		{
			get
			{
				if (mNewPoll == null) return null;
				else return mNewPoll.Clone() as ModbusPoll;
			}
		}

		public UserPollCommandReplace(ModbusPoll oldPoll, ModbusPoll newPoll, OnReplaceEventHandler handler) : base(EMasterActionType.ReplacePoll)
		{
			mNewPoll = newPoll.Clone() as ModbusPoll;
			mOldPoll = oldPoll.Clone() as ModbusPoll;
			mMode = PollRemoveMode.ByObject;
			this.OnReplacePoll += handler;
		}

		public UserPollCommandReplace(UInt32 index, ModbusPoll newPoll, OnReplaceEventHandler handler) : base(EMasterActionType.ReplacePoll)
		{
			mNewPoll = newPoll.Clone() as ModbusPoll;
			mOldPollIndex = index;
			mMode = PollRemoveMode.ByIndex;
			this.OnReplacePoll += handler;
		}

		public void FireEvent(UInt32 index, ModbusPoll poll, bool state, string message)
		{
			OnReplacePoll?.Invoke(index, poll, state, message);
		}
	}
}
