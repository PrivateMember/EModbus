using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrivialArrayCopyTests
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] array1 = { 1, 2, 3, 4, 5 };

			byte[] array2 = array1.Clone() as byte[];

			array2[0] = 10;

			Console.WriteLine(BitConverter.ToString(array1));
			Console.WriteLine(BitConverter.ToString(array2));
		}
	}
}
