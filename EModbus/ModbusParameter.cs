using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Xml.Serialization;
//using System.Xml;

namespace EModbus
{
//	[Serializable]
//	[XmlRoot("Modbus Parameter")]
	public class ModbusParameter
	{
		private byte[] mData;
		private UInt32 mCount = 1;
		private DataType mType = DataType.UInt16;
		
		public UInt32 RegistersCount { get { return (mCount * GetTypeLengthInBytes(mType)) / 2; } }
		public DataType Type { get { return mType; } }
		public UInt32 Count { get { return mCount; } }
		public UInt16 Address { get; set; }
		public string Name { get; set; }

		public static UInt32 GetTypeLengthInBytes(DataType tn)
		{
			UInt32 length = 0;
			switch (tn)
			{
				case DataType.Double:
				case DataType.UInt64:
				case DataType.Int64:
					length = 8; break;
				case DataType.Float:
				case DataType.UInt32:
				case DataType.Int32:
					length = 4; break;
				case DataType.UInt16:
				case DataType.Int16:
					length = 2; break;
				case DataType.UInt8:
				case DataType.Int8:
					length = 1; break;
			}
			return length;
		}

		public ModbusParameter(DataType type, UInt16 address, UInt32 count)
		{
			this.mType = type;
			Address = address;
			this.mCount = count;
			mData = new byte[RegistersCount * 2]; ;
			Name = "";
		}
	}
}
