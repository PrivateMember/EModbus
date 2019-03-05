using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public enum DataType
	{
		Float,
		Double,
		Int64,
		Int32,
		Int16,
		Int8,
		UInt64,
		UInt32,
		UInt16,
		UInt8,
		String,
		Custom16,
		Custom32,
		Custom64,
		Custom128,
		Custom256
	}

	public enum RegisterOrder
	{
		Normal,
		Inverse
	}

	public enum ByteOrder
	{
		LSBFirst,	// Least Significant Byte First
		MSBFirst    // Most Significant Byte First
	}

	public enum ErrorCode : int
	{
		Stopped = -1,
		None = 0,
		Timeout = 1,
		BusDataInvalid = 2
	}
}
