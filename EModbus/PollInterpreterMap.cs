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

	public class PollInterpreterMap : ICloneable
	{
		private uint mByteCount;
		private List<ModbusPollParameter> mParams = new List<ModbusPollParameter>();

		public List<ModbusPollParameter> Parameters { get { return mParams; } }
		public UInt32 ByteCount { get { return mByteCount; } set { mByteCount = value < 1 ? 1 : value; } }
		public string Name { set; get; } = "Unnamed Map";
		public DisplayMode DefaultDisplayMode { get; set; } = DisplayMode.Hex;

		public PollInterpreterMap(uint bytesCount)
		{
			mByteCount = bytesCount;

			for (int i = 0; i < mByteCount; i += 2)
			{
				mParams.Add(new ModbusPollParameter(DataType.UInt16, i, 2));
			}
		}

		public PollInterpreterMap(PollInterpreterMap map)
		{
			mByteCount = map.mByteCount;
			DefaultDisplayMode = map.DefaultDisplayMode;
			Name = map.Name;
			mParams = new List<ModbusPollParameter>(map.mParams.Count);
			foreach (ModbusPollParameter mpp in map.mParams)
			{
				mParams.Add(mpp.Clone() as ModbusPollParameter);
			}
		}

		public object GetParamValue(byte[] data, int paramIndex)
		{
			object obj = null;
			if (paramIndex < 0) return null;
			ModbusPollParameter p = mParams[paramIndex];

			byte[] myData = new byte[p.ByteCount];

			for (int i = 0; i < p.ByteCount; i++)
			{
				myData[i] = data[i + p.ByteIndex];
			}

			byte bitByte = 0;

			if (p.BitIndex >= 0) // coils and disceret inputs
			{
				bitByte = (byte)(myData[0] & (0x01 << p.BitIndex));
				if (bitByte > 0) bitByte = 1;

				switch (p.Type)
				{
					case DataType.BOOLEAN:
						obj = (bitByte == 1 ? true : false); break;
					case DataType.Door:
						obj = (bitByte == 1 ? Door.Open : Door.Close).ToString(); break;
					case DataType.OnOffSwitch:
						obj = (bitByte == 1 ? OnOffSwitch.On : OnOffSwitch.Off).ToString(); break;
					case DataType.DigitalIO:
						obj = (bitByte == 1 ? DigitalIO.High : DigitalIO.Low).ToString(); break;
					default: obj = null; break;
				}
			}
			else
			{
				if (p.ByteCount == 1)
				{
					switch (p.Type)
					{
						case DataType.Int8:
							obj = (char)myData[0]; break;
						case DataType.UInt8:
							obj = myData[0]; break;
						case DataType.BOOLEAN:
							obj = myData[0] > 0 ? true : false; break;
						case DataType.DigitalIO:
							obj = myData[0] > 0 ? DigitalIO.High : DigitalIO.Low; break;
						case DataType.Door:
							obj = myData[0] > 0 ? Door.Open : Door.Close; break;
						case DataType.OnOffSwitch:
							obj = myData[0] > 0 ? OnOffSwitch.On : OnOffSwitch.Off; break;
						default: obj = null; break;
					}
				}
				else if (p.ByteCount == 2)
				{
					UInt16 valueU16 = BitConverter.ToUInt16(myData, 0);
					Int16 valueI16 = BitConverter.ToInt16(myData, 0);
					switch (p.Type)
					{
						case DataType.Int16:
							obj = valueU16;	break;
						case DataType.UInt16:
							obj = valueI16; break;
						case DataType.BOOLEAN:
							obj = valueU16 > 0 ? true : false; break;
						case DataType.DigitalIO:
							obj = valueU16 > 0 ? DigitalIO.High : DigitalIO.Low; break;
						case DataType.Door:
							obj = valueU16 > 0 ? Door.Open : Door.Close; break;
						case DataType.OnOffSwitch:
							obj = valueU16 > 0 ? OnOffSwitch.On : OnOffSwitch.Off; break;
						default: obj = null; break;
					}
				}
				else if (p.ByteCount == 4)
				{
					UInt32 valueU32 = BitConverter.ToUInt32(myData, 0);
					Int32 valueI32 = BitConverter.ToInt32(myData, 0);
					float valueF = BitConverter.ToSingle(myData, 0);
					switch (p.Type)
					{
						case DataType.Int32:
							obj = valueU32; break;
						case DataType.UInt32:
							obj = valueU32; break;
						case DataType.Float:
							obj = valueF; break;
						case DataType.BOOLEAN:
							obj = valueU32 > 0 ? true : false; break;
						case DataType.DigitalIO:
							obj = valueU32 > 0 ? DigitalIO.High : DigitalIO.Low; break;
						case DataType.Door:
							obj = valueU32 > 0 ? Door.Open : Door.Close; break;
						case DataType.OnOffSwitch:
							obj = valueU32 > 0 ? OnOffSwitch.On : OnOffSwitch.Off; break;
						default: obj = null; break;
					}
				}
				else if (p.ByteCount == 8)
				{
					UInt64 valueU64 = BitConverter.ToUInt64(myData, 0);
					Int64 valueI64 = BitConverter.ToInt64(myData, 0);
					double valueF = BitConverter.ToDouble(myData, 0);
					switch (p.Type)
					{
						case DataType.Int64:
							obj = valueU64; break;
						case DataType.UInt64:
							obj = valueU64; break;
						case DataType.Double:
							obj = valueF; break;
						case DataType.BOOLEAN:
							obj = valueU64 > 0 ? true : false; break;
						case DataType.DigitalIO:
							obj = valueU64 > 0 ? DigitalIO.High : DigitalIO.Low; break;
						case DataType.Door:
							obj = valueU64 > 0 ? Door.Open : Door.Close; break;
						case DataType.OnOffSwitch:
							obj = valueU64 > 0 ? OnOffSwitch.On : OnOffSwitch.Off; break;
						default: obj = null; break;
					}
				}
				else
				{

				}
			}

			return obj;
		}

		public object Clone()
		{
			return new PollInterpreterMap(this);
		}

		public override string ToString()
		{
			string result = "";
			for(int i = 0; i < mParams.Count; i++)
			{
				result += mParams[i].Name + "(" + mParams[i].Type + ") : ";
				result += ""; // value
				result += "\r\n";
			}
			
			return result;
		}
	}
}
