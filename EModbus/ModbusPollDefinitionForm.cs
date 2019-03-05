using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EModbus
{
	public partial class ModbusPollDefinitionForm : Form
	{
		public ModbusPollDefinitionForm()
		{
			InitializeComponent();


			comboBox_types.DataSource = Enum.GetNames(typeof(ObjectType));
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		public ModbusPoll GetPoll()
		{
			UInt32 rateTime = (UInt32)numericUpDown_rate.Value;

			ModbusPoll poll = new ModbusPoll(
				(byte)numericUpDown_MBID.Value,
				(UInt16)numericUpDown_regAddr.Value,
				(UInt16)numericUpDown_regCount.Value,
				ObjectType.HoldingRegister);

			ObjectType type = ObjectType.HoldingRegister;
			type = (ObjectType)Enum.Parse(type.GetType(), (string)comboBox_types.SelectedItem);

			poll.Enabled = checkBox_devEnabled.Checked;
			poll.Name = textBox_name.Text;
			poll.TimeoutMilisec = (UInt32)numericUpDown_timeout.Value;

			return poll;
		}
	}
}
