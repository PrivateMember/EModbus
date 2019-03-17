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
		private string[] mPortNames = null;
		private ModbusMaster mBusMaster = new ModbusMaster();
		private System.IO.Ports.SerialPort mPort = new System.IO.Ports.SerialPort();

		public Form1()
		{
			InitializeComponent();

			System.Threading.Thread.CurrentThread.Name = "UI Thread";

			RefreshPorts();

			mBusMaster.OnPollFinished += MBusMaster_OnPollFinished;
			mBusMaster.OnStatusChanged += MBusMaster_OnStatusChanged;

			button_pauseResume.Enabled = false;
		}

		private delegate void UpdateControlsDelegate(MasterStatus status);

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
				}
				else if (status == MasterStatus.Stopped)
				{
					button_startPoll.Text = "Start";
					button_pauseResume.Text = "Resume";
					button_pauseResume.Enabled = false;
					comboBox_ports.Enabled = true;
				}
			}
		}

		private void MBusMaster_OnStatusChanged(MasterStatus status)
		{

			UpdateControls(status);
			//label_status.InvokeIfRequired(() =>
			//{
			//	label_status.Text = status.ToString();

			//	if (status == MasterStatus.Paused) button_pauseResume.Text = "Resume";
			//	else if (status == MasterStatus.Running)
			//	{
			//		button_pauseResume.Text = "Pause";
			//		button_startPoll.Text = "Stop";
			//		button_pauseResume.Enabled = true;
			//	}
			//	else if (status == MasterStatus.Stopped)
			//	{
			//		button_startPoll.Text = "Start";
			//		button_pauseResume.Text = "Resume";
			//		button_pauseResume.Enabled = false;
			//	}
			//});
		}

		private void MBusMaster_OnPollFinished(string data, ModbusMaster.ModbusPoll poll)
		{
			richTextBox_data.InvokeIfRequired(() =>
			{
				if(poll.DataValid)
				{
					RichTechBoxAddData(richTextBox_data, poll.MapToString());
				}
				else
				{
					RichTechBoxAddData(richTextBox_data, data);
				}
			});
		}

		private void button_addPoll_Click(object sender, EventArgs e)
		{
			ModbusMaster.ModbusPoll poll = ModbusMaster.ModbusPoll.PollWizard();
			
			if (poll != null)
			{
				mBusMaster.AddPoll(poll, OnPollAdded);
			}
		}

		private void OnPollAdded(bool state, string message)
		{
			this.InvokeIfRequired(() =>
			{
				RefreshTreeView();

				RichTechBoxAddData(richTextBox_messages, message);
			});
		}

		private void button_delPoll_Click(object sender, EventArgs e)
		{
			TreeNode node = treeView_polls.SelectedNode;
			if (node != null)
			{
				if (node.Parent != null) node = node.Parent;

				mBusMaster.RemovePoll((UInt32)node.Index, OnRemovePollHandler);
			}
		}

		void OnRemovePollHandler(bool state, string message)
		{
			this.InvokeIfRequired(() =>
			{
				RefreshTreeView();
				RichTechBoxAddData(richTextBox_messages, message);
			});
		}

		private void button_startPoll_Click(object sender, EventArgs e)
		{
			if (mBusMaster.Status == MasterStatus.Stopped)
			{
				mPort.PortName = comboBox_ports.SelectedItem as string;
				mBusMaster.SetComm(mPort);
				mBusMaster.Start();
			}
			else
			{
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

		private void RefreshPorts()
		{
			mPortNames = Utilities.Utils.QuerySerialPorts();

			MethodInvoker method = (MethodInvoker)delegate
			{
				comboBox_ports.Items.Clear();
				if (mPortNames.Length > 0)
				{
					comboBox_ports.Items.AddRange(mPortNames);
					comboBox_ports.SelectedIndex = 0;
				}
			};

			if (comboBox_ports.InvokeRequired == true) comboBox_ports.Invoke(method);
			else method.Invoke();
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

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			mBusMaster.OnPollFinished -= MBusMaster_OnPollFinished;
			mBusMaster.OnStatusChanged -= MBusMaster_OnStatusChanged;
			mBusMaster.Stop();
		}

		private void treeView_polls_DoubleClick(object sender, EventArgs e)
		{
			TreeNode node = treeView_polls.SelectedNode;

			if (node == null) return;

			if (node.Parent != null) node = node.Parent;

			int index = node.Index;

			ModbusMaster.ModbusPoll poll = mBusMaster.GetPoll((uint)index);


			ModbusPollDefinitionForm form = new ModbusPollDefinitionForm();
			form.SetPoll(poll);
			if(form.ShowDialog() == DialogResult.OK)
			{
				poll = form.GetPoll();
				mBusMaster.ReplacePoll((uint)index, poll, OnReplacePollHandler);
			}
		}

		void OnReplacePollHandler(UInt32 index, ModbusMaster.ModbusPoll poll, bool state, string message)
		{
			this.InvokeIfRequired(() =>
			{
				RichTechBoxAddData(richTextBox_messages, message);

				if (state)
				{
					treeView_polls.Nodes.RemoveAt((int)index);
					treeView_polls.Nodes.Insert((int)index, poll.Name);
					treeView_polls.Nodes[(int)index].Nodes.Add("ID : " + poll.DeviceID.ToString());
					treeView_polls.Nodes[(int)index].Nodes.Add("Address : " + poll.DataAddress.ToString());
					treeView_polls.Nodes[(int)index].Nodes.Add("Count : " + poll.DataCount.ToString());
					treeView_polls.Nodes[(int)index].Nodes.Add("Type : " + poll.ObjectType.ToString());
					treeView_polls.Nodes[(int)index].Nodes.Add("Timeout : " + poll.TimeoutMilisec.ToString());
					treeView_polls.Nodes[(int)index].Nodes.Add("Enabled : " + poll.Enabled.ToString());

					treeView_polls.Nodes[(int)index].Expand();
				}
			});
		}

		void RichTechBoxAddData(RichTextBox rtb, String str)
		{
			rtb.InvokeIfRequired(() =>
			{
				rtb.Text += str + "\r\n";
				rtb.SelectionStart = rtb.Text.Length;
				rtb.SelectionLength = 0;
				rtb.ScrollToCaret();
			});
		}

		private void editMapsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
	}
}
