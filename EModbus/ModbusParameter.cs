﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
//using System.Xml.Serialization;
//using System.Xml;

namespace EModbus
{
//	[Serializable]
//	[XmlRoot("Modbus Parameter")]
	public class ModbusPollParameter : ICloneable
	{
		private byte[] mData;

		[Browsable(true)]
		public string Name { get; set; } = "Unnamed";

		[Browsable(true)]
		public int ByteIndex { get; set; } = 0;

		[Browsable(true)]
		public UInt32 ByteCount { get; set; } = 2;

		[Browsable(true)]
		public int BitIndex { get; set; } = -1;

		[Browsable(true)]
		public DataType Type { get; set; } = DataType.UInt16;

		//public static UInt32 GetTypeLengthInBytes(DataType tn)
		//{
		//	UInt32 length = 0;
		//	switch (tn)
		//	{
		//		case DataType.BOOLEAN:
		//			length = 1;
		//			break;
		//		case DataType.Double:
		//		case DataType.UInt64:
		//		case DataType.Int64:
		//			length = 8; break;
		//		case DataType.Float:
		//		case DataType.UInt32:
		//		case DataType.Int32:
		//			length = 4; break;
		//		case DataType.UInt16:
		//		case DataType.Int16:
		//			length = 2; break;
		//		case DataType.UInt8:
		//		case DataType.Int8:
		//			length = 1; break;
		//	}
		//	return length;
		//}

		public ModbusPollParameter()
		{
		}

		public ModbusPollParameter(DataType type, int byteIndex, UInt32 byteCount = 1, int bitIndex=-1)
		{
			Type = type;
			ByteCount = byteCount;
			ByteIndex = byteIndex;
			BitIndex = bitIndex;
			mData = new byte[ByteCount];
		}

		public ModbusPollParameter(ModbusPollParameter mp)
		{
			Name = mp.Name;
			Type = mp.Type;
			ByteCount = mp.ByteCount;
			ByteIndex = mp.ByteIndex;
			BitIndex = mp.BitIndex;
			mData = mp.mData == null ? null : mp.mData.Clone() as byte[];
		}

		public object Clone()
		{
			return new ModbusPollParameter(this);
		}
	}
}
