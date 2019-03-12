using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public enum DataType
	{
		Bit,
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
		Custom256,
		CustomN
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
		BusDataInvalid = 2,
		CRCError
	}

	public enum MBProtocolVersion
	{
		RTU,
		ASCII,
		TCP
	}

	public enum ModbusObjectType
	{
		Coil,           // Read-write	1 bit
		DiscreteInput,  // Read-only	1 bit
		InputRegister,  // Read-only	16 bits
		HoldingRegister,// Read-write	16 bits
		FileRecord
	}

	/// <summary>
	/// DA	------->	Data Access
	/// DG	------->	Diagnostics
	/// 
	/// BIT	------->	Bit Access
	/// REG	------->	Registers Access
	/// FLR ------->	File Record Access
	/// 
	/// PHDI------->	Physical Discrete Inputs
	/// COIL------->	Internal Bits or Physical Coils
	/// PHIR------->	Physical Input Registers
	/// PIOR------->	Internal Registers Physical Output Registers
	/// 
	/// RR	------->	Read
	/// RW	------->	Read-Write ( The write operation is performed before the read )
	/// WW	------->	Write
	/// MW	------->	Mask Write ( modify the contents of a specified holding register using a combination of an AND mask, an OR mask, and the register's current contents )
	/// </summary>
	public enum FunctionCode : byte
	{
		DA_BIT_PHDI_RR_DiscreteInputs = 2,
		DA_BIT_COIL_RR_Coils = 1,
		DA_BIT_COIL_WW_SingleCoil = 5,
		DA_BIT_COIL_WW_MultipleCoils = 15,
		DA_REG_PHIR_RR_InputRegisters = 4,
		DA_REG_PIOR_RR_HoldingRegisters = 3,
		DA_REG_PIOR_WW_HoldingRegister = 6,
		DA_REG_PIOR_WW_HoldingRegisters = 16,
		DA_REG_PIOR_RW_HoldingRegisters = 23,
		DA_REG_PIOR_MW_HoldingRegister = 22,
		DA_REG_PIOR_RR_FIFOQueue = 24,
		DA_FLR_RR_FileRecord = 20,
		DA_FLR_WW_FileRecord = 21,
		DG_RR_ExceptionStatus = 7,
		DG_Diagnostic = 8,
		DG_GetComEventCounter = 11,
		DG_GetComEventLog = 12,
		DG_ReportSlaveID = 17,
		DG_ReadDeviceIdentification = 43,
		Other_EncapsulatedInterfaceTransport = 43
	}

	public enum ExceptionCode : byte
	{
		none = 0,
		IllegalFunction = 1,            // Function code received in the query is not recognized or allowed by slave
		IllegalDataAddress = 2,         // Data address of some or all the required entities are not allowed or do not exist in slave
		IllegalDataValue = 3,           // Value is not accepted by slave
		SlaveDeviceFailure = 4,         // Unrecoverable error occurred while slave was attempting to perform requested action
		Acknowledge = 5,                // Slave has accepted request and is processing it, but a long duration of time is required. This response is returned to prevent a timeout error from occurring in the master. Master can next issue a Poll Program Complete message to determine whether processing is completed
		SlaveDeviceBusy = 6,            // Slave is engaged in processing a long-duration command. Master should retry later
		NegativeAcknowledge = 7,        // Slave cannot perform the programming functions. Master should request diagnostic or error information from slave
		MemoryParityError = 8,          // Slave detected a parity error in memory. Master can retry the request, but service may be required on the slave device
		GatewayPathUnavailable = 10,    // Specialized for Modbus gateways. Indicates a misconfigured gateway
		GatewayTargetDeviceFailed = 11  // Specialized for Modbus gateways. Sent when slave fails to respond
	}

	public enum ResponseType
	{
		Data,
		Exception
	}
}
