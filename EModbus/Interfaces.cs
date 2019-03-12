using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
    public interface MBCommInterface
    {
		UInt32 ReadHoldingRegisters(UInt16 address, UInt16 count, ref UInt16[] data, ref Exception exp);
		UInt32 ReadInputRegisters(UInt16 address, UInt16 count, ref UInt16[] data, ref Exception exp);
		UInt32 ReadCoil();
		UInt32 ReadDI();

		UInt32 WriteHoldingRegister();
		UInt32 WriteHoldingRegisters();
		ExceptionCode WriteCoil();
		ExceptionCode WriteCoils();

		ExceptionCode ReadWriteMultipleRegisters();

		ExceptionCode MaskWriteRegister();

		ExceptionCode ReadFIFOQueue();
	}
}
