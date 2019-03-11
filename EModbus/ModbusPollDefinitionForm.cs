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

			comboBox_types.DataSource = Enum.GetNames(typeof(ModbusObjectType));
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

			ModbusObjectType type = ModbusObjectType.HoldingRegister;
			type = (ModbusObjectType)Enum.Parse(type.GetType(), (string)comboBox_types.SelectedItem);

			ModbusPoll poll = new ModbusPoll(
				(byte)numericUpDown_MBID.Value,
				(UInt16)numericUpDown_regAddr.Value,
				(UInt16)numericUpDown_regCount.Value,
				type);

			poll.Enabled = checkBox_devEnabled.Checked;
			poll.Name = textBox_name.Text;
			poll.TimeoutMilisec = (UInt32)numericUpDown_timeout.Value;

			return poll;
		}

		public void SetPoll(ModbusPoll poll)
		{
			numericUpDown_MBID.Value = poll.DeviceID;
			numericUpDown_regAddr.Value = poll.DataAddress;
			numericUpDown_regCount.Value = poll.DataCount;
			comboBox_types.SelectedIndex = Array.IndexOf(Enum.GetNames(typeof(ModbusObjectType)), poll.ObjectType.ToString());
			checkBox_devEnabled.Checked = poll.Enabled;
			textBox_name.Text = poll.Name;
			numericUpDown_timeout.Value = poll.TimeoutMilisec;
		}
	}
}
