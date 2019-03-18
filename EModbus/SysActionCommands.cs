using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	enum EMasterSysActionType
	{
		NoAction,
		ChangeScanRate
	}

	class SysCommand
	{
		private EMasterSysActionType mType = EMasterSysActionType.NoAction;

		public EMasterSysActionType Type { get { return mType; } }

		public SysCommand(EMasterSysActionType type)
		{
			mType = type;
		}
	}

	class SysCommandScanRate : SysCommand
	{
		private UInt32 mMilisecond;

		public UInt32 Milisecond { get { return mMilisecond; } }

		public SysCommandScanRate(UInt32 milisecond) : base(EMasterSysActionType.ChangeScanRate)
		{
			mMilisecond = milisecond;
		}
	}
}
