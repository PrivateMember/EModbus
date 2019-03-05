using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public class ModbusCommandReadHoldingRegisters : ModbusCommandRead
	{
		public ModbusCommandReadHoldingRegisters(UInt16 address, UInt16 count) : base(ObjectType.HoldingRegister, address, count)
		{
		}
	}
}
