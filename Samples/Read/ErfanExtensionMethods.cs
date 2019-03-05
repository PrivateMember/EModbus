using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace Read
{
	public static class ErfanExtensionMethods
	{
		public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
		{
			if (obj == null) return;

			if (obj.InvokeRequired)
			{
				var args = new object[0];
				obj.Invoke(action, args);
			}
			else
			{
				action();
			}
		}
	}
}
