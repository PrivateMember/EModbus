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
		private ModbusMaster.ModbusPoll mPoll = null;
		public ModbusPollDefinitionForm()
		{
			InitializeComponent();

			CancelButton = button_cancel;
			AcceptButton = button_ok;

			comboBox_types.DataSource = Enum.GetNames(typeof(ModbusObjectType));
			comboBox_types.SelectedIndex = 3;
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

		private void ResetPoll()
		{
			UInt32 rateTime = (UInt32)numericUpDown_rate.Value;
			ModbusObjectType type = (ModbusObjectType)Enum.Parse(typeof(ModbusObjectType), (string)comboBox_types.SelectedItem);

			mPoll = new ModbusMaster.ModbusPoll(
				(byte)numericUpDown_MBID.Value,
				(UInt16)numericUpDown_regAddr.Value,
				(UInt16)numericUpDown_regCount.Value,
				type);

			mPoll.Enabled = checkBox_devEnabled.Checked;
			mPoll.Name = textBox_name.Text;
			mPoll.TimeoutMilisec = (UInt32)numericUpDown_timeout.Value;
		}


		public ModbusMaster.ModbusPoll GetPoll()
		{
			UInt32 rateTime = (UInt32)numericUpDown_rate.Value;

			ModbusObjectType type = ModbusObjectType.HoldingRegister;
			type = (ModbusObjectType)Enum.Parse(type.GetType(), (string)comboBox_types.SelectedItem);

			ModbusMaster.ModbusPoll poll = new ModbusMaster.ModbusPoll(
				(byte)numericUpDown_MBID.Value,
				(UInt16)numericUpDown_regAddr.Value,
				(UInt16)numericUpDown_regCount.Value,
				type);

			poll.Enabled = checkBox_devEnabled.Checked;
			poll.Name = textBox_name.Text;
			poll.TimeoutMilisec = (UInt32)numericUpDown_timeout.Value;

			return poll;
		}

		public void SetPoll(ModbusMaster.ModbusPoll poll)
		{
			mPoll = poll;

			numericUpDown_MBID.Value = mPoll.DeviceID;
			numericUpDown_regAddr.Value = mPoll.DataAddress;
			numericUpDown_regCount.Value = mPoll.DataCount;
			comboBox_types.SelectedIndex = Array.IndexOf(Enum.GetNames(typeof(ModbusObjectType)), mPoll.ObjectType.ToString());
			checkBox_devEnabled.Checked = mPoll.Enabled;
			textBox_name.Text = mPoll.Name;
			numericUpDown_timeout.Value = mPoll.TimeoutMilisec;
		}

		private void button_maps_Click(object sender, EventArgs e)
		{
			PollMapDefinitionForm form = new PollMapDefinitionForm(mPoll);

			if(form.ShowDialog() == DialogResult.OK)
			{

			}
		}
	}
}
