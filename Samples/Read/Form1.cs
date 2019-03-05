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
		private ModbusBusMaster mBusMaster = new ModbusBusMaster();
		private System.IO.Ports.SerialPort mPort = new System.IO.Ports.SerialPort();

		public Form1()
		{
			InitializeComponent();

			RefreshPorts();
			if (comboBox_ports.Items.Count > 0) comboBox_ports.SelectedIndex = 0;
		}

		private void button_addPoll_Click(object sender, EventArgs e)
		{

			ModbusPoll poll = ModbusPoll.PollWizard();

			if (poll != null)
			{
				mBusMaster.AddPoll(poll);
				RefreshTreeView();
			}
		}

		private void button_startPoll_Click(object sender, EventArgs e)
		{
			mPort.PortName = comboBox_ports.SelectedItem as string;
			mBusMaster.SetComm(mPort);
			mBusMaster.Start();
		}

		private void button_stopPoll_Click(object sender, EventArgs e)
		{
			mBusMaster.Stop();
		}

		private void RefreshPorts()
		{
			MethodInvoker method = (MethodInvoker)delegate
			{
				comboBox_ports.Items.Clear();
				string[] names = System.IO.Ports.SerialPort.GetPortNames();

				if (names.Length > 0)
				{
					foreach (string name in names)
					{
						System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort(name);
						try
						{
							port.Open();
							port.Close();
							comboBox_ports.Items.Add(name);
						}
						catch (SystemException e)
						{
						}
						port.Dispose();
					}
				}
			};
			if (comboBox_ports.InvokeRequired == true)
			{
				comboBox_ports.Invoke(method);
			}
			else
			{
				method.Invoke();
			}
		}

		private void RefreshTreeView()
		{
			treeView_polls.BeginUpdate();
			treeView_polls.Nodes.Clear();

			List<ModbusPoll> polls = mBusMaster.Polls;

			for (int i = 0; i < polls.Count; i++)
			{
				treeView_polls.Nodes.Add(polls[i].Name);
				treeView_polls.Nodes[i].Nodes.Add("ID : " + polls[i].DeviceID.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Address : " + polls[i].DataAddress.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Count : " + polls[i].DataCount.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Type : " + polls[i].ObjType.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Timeout : " + polls[i].TimeoutMilisec.ToString());
				treeView_polls.Nodes[i].Nodes.Add("Enabled : " + polls[i].Enabled.ToString());
			}
			treeView_polls.EndUpdate();
		}
	}
}
