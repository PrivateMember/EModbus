using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public enum DisplayMode
	{
		Decimal,
		Hex
	}

	public class PollInterpreterMap
	{
		private uint mByteCount;
		private byte[] mData;
		public List<ModbusPollParameter> mParams = new List<ModbusPollParameter>();
		private DisplayMode mDefaultDisplay = DisplayMode.Hex;

		public string Name { set; get; } = "Unnamed Map";

		public PollInterpreterMap(uint bytesCount)
		{
			mByteCount = bytesCount;

			for(int i = 0; i < mByteCount / 2; i++)
			{
				mParams.Add(new ModbusPollParameter(DataType.UInt16, 1));
			}
		}

		private void DataReorder(byte[] data, DataType type, RegisterOrder rOrder = RegisterOrder.Inverse, ByteOrder bOrder = ByteOrder.MSBFirst)
		{
			byte[] temp = new byte[2];
			int regCount = 0;
			switch(type)
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
			}

			if (rOrder == RegisterOrder.Inverse && bOrder == ByteOrder.MSBFirst)
			{
				int byteCount = regCount * 2;
				byte swap;
				for (int i = 0; i < byteCount / 2; i++)
				{
					swap = data[i];
					data[i] = data[byteCount - 1 - i];
					data[byteCount - 1 - i] = swap;
				}
			}
		}

		public string ValueToString(byte[] data, int paramIndex, DisplayMode display = DisplayMode.Decimal)
		{
			string result = "";
			if (paramIndex < 0) return "";

			ModbusPollParameter p = mParams[paramIndex];

			byte[] myData = new byte[p.ByteCount];

			for(int i = 0; i < p.ByteCount; i++)
			{
				myData[i] = data[i + p.ByteIndex];
			}

			DataReorder(myData, p.Type);
			

			//switch (mParams[(int)paramIndex].ByteOrder)
			//{
			//}

			switch (p.Type)
			{
				case DataType.BOOLEAN:
					byte value = (byte)(myData[0] & (0x01 << p.BitIndex));
					if (value > 0) value = 1;
					result = value.ToString();
					break;
				case DataType.Double:
					result = BitConverter.ToDouble(myData, 0).ToString(); break;
				case DataType.Float:
					result = BitConverter.ToSingle(myData, 0).ToString(); break;
				case DataType.Int64:
					result = BitConverter.ToInt64(myData, 0).ToString(); break;
				case DataType.UInt64:
					result = BitConverter.ToUInt64(myData, 0).ToString(); break;
				case DataType.Int32:
					result = BitConverter.ToInt32(myData, 0).ToString(); break;
				case DataType.UInt32:
					result = BitConverter.ToUInt32(myData, 0).ToString(); break;
				case DataType.Int16:
					result = BitConverter.ToInt16(myData, 0).ToString(); break;
				case DataType.UInt16:
					result = BitConverter.ToUInt16(myData, 0).ToString(); break;
				case DataType.Int8:
					result = ((int)myData[0]).ToString(); break;
				case DataType.UInt8:
					result = myData[0].ToString(); break;
				default:break;
			}


			return result;
		}
	}
}
