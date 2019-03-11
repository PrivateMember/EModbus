using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public class ModbusCommandReadInputRegisters : ModbusCommandRead
	{
		public ModbusCommandReadInputRegisters(UInt16 address, byte count) : base(ModbusObjectType.InputRegister, address, count)
		{
		}
	}
}
