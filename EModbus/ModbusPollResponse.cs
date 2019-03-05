using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	class ModbusPollResponse
	{
		public enum ResponseStatus
		{
			DeviceID,
			FunctionCode,
			ErrorCode,
			Data,
			CRC,
			Finished
		}

		private byte mFunCode;
		private byte mExpCode;
		private byte mByteCount;
		private byte mDevID;
		private byte[] mRxBuff;
		private ErrorCode mErrCode = ErrorCode.BusDataInvalid;
		private ResponseType mResType = ResponseType.Exception;
		private ResponseStatus mStatus = ResponseStatus.DeviceID;

		private int mDataIndex = 0;
		private int mCRCIndex = 0;

		public ResponseType RespType { get { return mResType; } }
		public ErrorCode Error { get { return mErrCode; } }
		public ResponseStatus Status { get { return mStatus; } }

		public ModbusPollResponse(ModbusPoll poll)
		{
			byte[] pollCmd = poll.GetPollCommand();

			mDevID = pollCmd[0];
			mFunCode = pollCmd[1];
			mExpCode = (byte)(mFunCode & 0x80);
			mByteCount = poll.PollResponseDataLength;
			mRxBuff = new byte[poll.PollResponseLength];
		}

		public void Reset()
		{

		}

		public void AddRxData(byte[] data, int offset, int len)
		{
			for(int i = offset; i < offset+len; i++)
			{
				AddRxData(data[i]);
			}
		}

		public void AddRxData(byte data)
		{
			switch (mStatus)
			{
				case ResponseStatus.DeviceID:
					if (data == mDevID)
					{
						mRxBuff[0] = data;
						mStatus = ResponseStatus.FunctionCode;
					}
					break;
				case ResponseStatus.FunctionCode:
					if (data == mFunCode)
					{
						mRxBuff[1] = data;
						mStatus = ResponseStatus.Data;
						mDataIndex = 0;
					}
					else if (data == mExpCode)
					{
						mRxBuff[1] = data;
						mStatus = ResponseStatus.ErrorCode;
					}
					else
					{
						mErrCode = ErrorCode.BusDataInvalid;
					}
					break;
				case ResponseStatus.ErrorCode:
					mRxBuff[2] = data;
					mStatus = ResponseStatus.CRC;
					mCRCIndex = 0;
					break;
				case ResponseStatus.Data:
					if(mDataIndex < mByteCount)
					{
						mRxBuff[3 + mDataIndex] = data;
						mDataIndex++;
					}
					else
					{
						mStatus = ResponseStatus.CRC;
						mCRCIndex = 0;
					}

					break;
				case ResponseStatus.CRC:
					if(mCRCIndex < 2)
					{
						mRxBuff[3 + mDataIndex + mCRCIndex] = data;
						mCRCIndex++;
					}
					else
					{
						mStatus = ResponseStatus.Finished;
					}
					break;
				case ResponseStatus.Finished: break;
			}
		}
	}
}
