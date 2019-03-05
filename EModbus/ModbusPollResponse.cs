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
			DataLength,
			Data,
			CRC,
			Finished
		}

		private byte mFunCode;
		private byte mExpCode;
		private byte mByteCount;
		private byte mDevID;
		private byte[] mRxBuff;
		private int mRxIndex = 0;
		private ErrorCode mErrCode = ErrorCode.BusDataInvalid;
		private ResponseType mResType = ResponseType.Exception;
		private ResponseStatus mStatus = ResponseStatus.DeviceID;

		private int mDataIndex = 0;
		private int mCRCIndex = 0;

		public ResponseType RespType { get { return mResType; } }
		public ErrorCode Error { get { return mErrCode; } }
		public ResponseStatus Status { get { return mStatus; } }

		public byte[] GetData()
		{
			byte[] data = new byte[mByteCount];

			for(int i = 0; i < mByteCount; i++)
			{
				data[i] = mRxBuff[3 + i];
			}

			return data;
		}

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
						mRxBuff[mRxIndex++] = data;
						mStatus = ResponseStatus.FunctionCode;
					}
					break;
				case ResponseStatus.FunctionCode:
					if (data == mFunCode)
					{
						mRxBuff[mRxIndex++] = data;
						mStatus = ResponseStatus.DataLength;
						mDataIndex = 0;
					}
					else if (data == mExpCode)
					{
						mRxBuff[mRxIndex++] = data;
						mByteCount = 0;
						mStatus = ResponseStatus.ErrorCode;
					}
					else
					{
						mErrCode = ErrorCode.BusDataInvalid;
					}
					break;
				case ResponseStatus.ErrorCode:
					mRxBuff[mRxIndex++] = data;
					mStatus = ResponseStatus.CRC;
					mCRCIndex = 0;
					break;
				case ResponseStatus.DataLength:
					mRxBuff[mRxIndex++] = data;
					mStatus = ResponseStatus.Data;
					mDataIndex = 0;
					break;
				case ResponseStatus.Data:
					
					mRxBuff[mRxIndex++] = data;
					mDataIndex++;

					if(mDataIndex == mByteCount)
					{
						mStatus = ResponseStatus.CRC;
						mCRCIndex = 0;
					}

					break;
				case ResponseStatus.CRC:
					mRxBuff[mRxIndex++] = data;
					mCRCIndex++;
					if (mCRCIndex == 2)
					{
						UInt16 crc = Utilities.Utils.CRC16(mRxBuff, (uint)(mRxIndex-2));
						if(mRxBuff[mRxIndex - 2] == (byte)(crc&0xFF) && mRxBuff[mRxIndex - 1] == (byte)(crc >> 8))
						{
							mErrCode = ErrorCode.None;
						}
						else
						{
							mErrCode = ErrorCode.CRCError;
						}
						mStatus = ResponseStatus.Finished;
					}
					break;
				case ResponseStatus.Finished: break;
			}
		}
	}
}
