using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EModbus;

namespace Read
{
	public partial class Form1 : Form
	{
		private ModbusMaster mBusMaster = new ModbusMaster();
		private System.IO.Ports.SerialPort mPort = new System.IO.Ports.SerialPort();
		private delegate void UpdateControlsDelegate(MasterStatus status);

		public Form1()
		{
			InitializeComponent();

			System.Threading.Thread.CurrentThread.Name = "EModbus UI Thread";

			Utilities.Controls.PopulateComboboxWithSerialPortNames(comboBox_ports);

			mBusMaster.OnPollFinished += MBusMaster_OnPollFinished;
			mBusMaster.OnStatusChanged += MBusMaster_OnStatusChanged;
			mBusMaster.OnException += MBusMaster_OnException;
			
			button_pauseResume.Enabled = false;
		}

		void UpdateControls(MasterStatus status)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new UpdateControlsDelegate(this.UpdateControls), status);
			}
			else
			{
				label_status.Text = status.ToString();

				if (status == MasterStatus.Paused) button_pauseResume.Text = "Resume";
				else if (status == MasterStatus.Running)
				{
					button_pauseResume.Text = "Pause";
					button_startPoll.Text = "Stop";
					comboBox_ports.Enabled = false;
					button_pauseResume.Enabled = true;
					button_startPoll.Enabled = true;
				}
				else if (status == MasterStatus.Stopped)
				{
					button_startPoll.Text = "Start";
					button_pauseResume.Text = "Resume";
					button_pauseResume.Enabled = false;
					comboBox_ports.Enabled = true;
					button_startPoll.Enabled = true;
				}
			}
		}

		private void MBusMaster_OnStatusChanged(MasterStatus status)
		{
			UpdateControls(status);
		}

		private void MBusMaster_OnException(string msg)
		{
			Utilities.Controls.RichTextBoxAddDataAndScroll(richTextBox_messages, msg);
		}

		private void MBusMaster_OnPollFinished(string data, ModbusMaster.ModbusPoll poll)
		{
			richTextBox_data.InvokeIfRequired(() =>
			{
				if (poll.DataValid)
				{
					Utilities.Controls.RichTextBoxAddDataAndScroll(richTextBox_data, poll.MapToString());
				}
				else
				{
					Utilities.Controls.RichTextBoxAddDataAndScroll(richTextBox_data, data);
				}
			});
		}

		private void OnPollAdded(bool state, string message)
		{
			this.InvokeIfRequired(() =>
			{
				RefreshTreeView();

				Utilities.Controls.RichTextBoxAddDataAndScroll(richTextBox_messages, message);
			});
		}

		void OnPollRemove(bool state, string message)
		{
			this.InvokeIfRequired(() =>
			{
				RefreshTreeView();
				Utilities.Controls.RichTextBoxAddDataAndScroll(richTextBox_messages, message);
			});
		}

		void OnPollReplace(UInt32 index, ModbusMaster.ModbusPoll poll, bool state, string message)
		{
			Utilities.Controls.RichTextBoxAddDataAndScroll(richTextBox_messages, message);

			if (state)
			{
				ReplaceNodeOnTreeView((int)index, poll);
			}
		}

		private void RefreshTreeView()
		{
			treeView_polls.BeginUpdate();
			treeView_polls.Nodes.Clear();

			List<ModbusMaster.ModbusPoll> polls = mBusMaster.Polls;

			for (int i = 0; i < polls.Count; i++)
			{
				treeView_polls.Nodes.Add(polls[i].Name);
				treeView_polls.Nodes[i].Nodes.Add("ID : " + polls[i].DeviceID.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Address : " + polls[i].DataAddress.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Count : " + polls[i].DataCount.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Type : " + polls[i].ObjectType.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Timeout : " + polls[i].TimeoutMilisec.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Enabled : " + polls[i].Enabled.ToString());
			}
			treeView_polls.EndUpdate();
		}

		private TreeNode GetSelectedPollNodeFromTreeView()
		{
			TreeNode node = treeView_polls.SelectedNode;
			if (node == null) return null;
			if (node.Parent != null) node = node.Parent;
			return node;
		}

		private void ReplaceNodeOnTreeView(int index, ModbusMaster.ModbusPoll poll)
		{
			if (index < 0 || index >= treeView_polls.Nodes.Count || poll == null) return;
			treeView_polls.InvokeIfRequired(() =>
			{
				treeView_polls.BeginUpdate();
				treeView_polls.Nodes.RemoveAt(index);
				treeView_polls.Nodes.Insert(index, poll.Name);
				treeView_polls.Nodes[index].Nodes.Add("ID : " + poll.DeviceID.ToString());
				treeView_polls.Nodes[index].Nodes.Add("Address : " + poll.DataAddress.ToString());
				treeView_polls.Nodes[index].Nodes.Add("Count : " + poll.DataCount.ToString());
				treeView_polls.Nodes[index].Nodes.Add("Type : " + poll.ObjectType.ToString());
				treeView_polls.Nodes[index].Nodes.Add("Timeout : " + poll.TimeoutMilisec.ToString());
				treeView_polls.Nodes[index].Nodes.Add("Enabled : " + poll.Enabled.ToString());
				treeView_polls.Nodes[index].Expand();
				treeView_polls.EndUpdate();
			});
		}

		private void EditPoll(uint index)
		{
			ModbusMaster.ModbusPoll poll = mBusMaster.GetPoll(index);
			poll = ModbusMaster.ModbusPoll.PollWizard(poll);
			mBusMaster.ReplacePoll(index, poll, OnPollReplace);
		}

		private void editMapsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			mBusMaster.SetScanRate((UInt32)numericUpDown_scanRate.Value);
		}

		private void button_startPoll_Click(object sender, EventArgs e)
		{
			button_startPoll.Enabled = false;
			if (mBusMaster.Status == MasterStatus.Stopped)
			{
				mPort.PortName = comboBox_ports.SelectedItem as string;
				mBusMaster.SetComm(mPort);
				mBusMaster.Start();
			}
			else
			{
				button_startPoll.Enabled = false;
				mBusMaster.Stop();
			}
		}

		private void button_pauseResume_Click(object sender, EventArgs e)
		{
			if (mBusMaster.Status == MasterStatus.Paused)
			{
				mBusMaster.Resume();
			}
			else
			{
				mBusMaster.Pause();
			}
		}

		private void button_scanPorts_Click(object sender, EventArgs e)
		{
			Utilities.Controls.PopulateComboboxWithSerialPortNames(comboBox_ports);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			mBusMaster.OnPollFinished -= MBusMaster_OnPollFinished;
			mBusMaster.OnStatusChanged -= MBusMaster_OnStatusChanged;
			mBusMaster.Stop();
		}

		private void treeView_polls_DoubleClick(object sender, EventArgs e)
		{
			TreeNode node = GetSelectedPollNodeFromTreeView();
			if (node != null) { EditPoll((uint)node.Index); }
		}

		private void button_addPoll_Click(object sender, EventArgs e)
		{
			mBusMaster.AddPoll(ModbusMaster.ModbusPoll.PollWizard(), OnPollAdded);
		}

		private void button_delPoll_Click(object sender, EventArgs e)
		{
			TreeNode node = treeView_polls.SelectedNode;
			if (node != null)
			{
				if (node.Parent != null) node = node.Parent;

				mBusMaster.RemovePoll((UInt32)node.Index, OnPollRemove);
			}
		}
	}
}
