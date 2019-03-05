using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public class ModbusCommandReadCoils : ModbusCommandRead
	{
		public ModbusCommandReadCoils(UInt16 address, byte count) : base(ObjectType.Coil, address, count)
		{
		}
	}
}
