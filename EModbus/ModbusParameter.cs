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

		public string Name { get; set; } = "Unnamed";
		public UInt32 DataCount { get; set; } = 1;
		public UInt16 DataAddress { get; set; }
		public DataType Type { get; set; } = DataType.UInt16;
		
		public UInt32 RegistersCount { get { return (DataCount * GetTypeLengthInBytes(Type)) / 2; } }

		public static UInt32 GetTypeLengthInBytes(DataType tn)
		{
			UInt32 length = 0;
			switch (tn)
			{
				case DataType.Bit:
				//	length = mDataCount % 8 == 0 ? mDataCount / 8 : mDataCount / 8 + 1;
					break;
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

		public ModbusParameter(DataType type, UInt16 address, UInt32 count = 1, UInt32 offset = 0)
		{
			Type = type;
			DataCount = count;
			DataAddress = address;
			mData = new byte[RegistersCount * 2];
		}
	}
}
