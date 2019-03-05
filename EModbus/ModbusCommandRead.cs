using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModbus
{
	public class ModbusCommandRead : ModbusCommand
	{
		public UInt16 Address { get; set; }
		public UInt16 Count { set; get; }

		public ModbusCommandRead(ObjectType type, UInt16 address, UInt16 count) : base (FunctionCode.DA_REG_PIOR_RR_HoldingRegisters)
		{
			Address = address;
			Count = count;

			switch(type)
			{
				case ObjectType.Coil: FuncCode = FunctionCode.DA_BIT_COIL_RR_Coils; break;
				case ObjectType.DiscreteInput: FuncCode = FunctionCode.DA_BIT_PHDI_RR_DiscreteInputs; break;
				case ObjectType.HoldingRegister: FuncCode = FunctionCode.DA_REG_PIOR_RR_HoldingRegisters; break;
				case ObjectType.InputRegister: FuncCode = FunctionCode.DA_REG_PHIR_RR_InputRegisters; break;
			}
		}

		public override byte[] ToBytes()
		{
			byte[] buff = new byte[5];
			buff[0] = (byte)FuncCode;
			buff[1] = (byte)(Address >> 8);
			buff[2] = (byte)(Address & 0xFF);
			buff[3] = (byte)(Count >> 8);
			buff[4] = (byte)(Count & 0xFF);
			return buff;
		}
	}
}
