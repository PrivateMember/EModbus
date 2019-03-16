using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_ReadonlyReference
{
	class Program
	{
		class ClassA
		{
			private byte[] bytes = new byte[] { 0, 1, 2, 3, 4 };

			public byte[] TheBytesRef { get { return bytes; } }
			public byte this[int index] {get{ return bytes[index]; }}
		}

		class ClassB
		{
			private List<byte> bytes = new List<byte>() { 0, 1, 2, 3, 4 };

			public List<byte> TheBytesRef { get { return bytes; } }
			public byte this[int index] { get { return bytes[index]; } }
		}


		static void Main(string[] args)
		{
			ClassA a = new ClassA();
			ClassB b = new ClassB();

			byte[] bytesA = a.TheBytesRef;
			List<byte> bytesB = b.TheBytesRef;

			bytesA[0] = 10; // this changes the value inside the object a
			bytesB[0] = 10; // this changes the value inside the object b
			Console.WriteLine(a[0].ToString());
			Console.WriteLine(b[0].ToString());

		}
	}
}
