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
		public List<ModbusParameter> mParams = new List<ModbusParameter>();
		private DisplayMode mDefaultDisplay = DisplayMode.Hex;

		public string Name { set; get; } = "Unnamed Map";

		public PollInterpreterMap(uint bytesCount)
		{
			mByteCount = bytesCount;

			for(int i = 0; i < mByteCount / 2; i++)
			{
				mParams.Add(new ModbusParameter(DataType.UInt16, 1));
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
			uint dataByteIndex = 0;

			if (paramIndex < 0) return "";

			for(int i = 0; i < paramIndex; i++)
			{
				dataByteIndex += mParams[i].RegistersCount * 2;
			}

			byte[] myData = new byte[mParams[paramIndex].RegistersCount * 2];

			for(int i = 0; i < mParams[paramIndex].RegistersCount * 2; i++)
			{
				myData[i] = data[i + dataByteIndex];
			}

			DataReorder(myData, mParams[(int)paramIndex].Type);
			

			//switch (mParams[(int)paramIndex].ByteOrder)
			//{
			//}

			switch (mParams[(int)paramIndex].Type)
			{
				case DataType.Bit: break;
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
