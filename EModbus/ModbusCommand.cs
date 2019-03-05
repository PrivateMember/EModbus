using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public abstract class ModbusCommand
	{
		public FunctionCode FuncCode { get; set; }

		public ModbusCommand(FunctionCode fCode)
		{
			FuncCode = fCode;
		}

		public abstract byte[] ToBytes();
	}
}
