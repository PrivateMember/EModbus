using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public class ModbusCommandReadDiscreteInputs : ModbusCommandRead
	{
		public ModbusCommandReadDiscreteInputs(UInt16 address, byte count) : base(ObjectType.DiscreteInput, address, count)
		{
		}
	}
}
