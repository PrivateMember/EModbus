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
	public partial class PollMapDefinitionForm : Form
	{
		private ModbusMaster.ModbusPoll mPoll = null;
		private List<PollInterpreterMap> mMaps = null;

		public PollMapDefinitionForm(ModbusMaster.ModbusPoll poll)
		{
			InitializeComponent();


			//dataGridView1.Columns.Add("DataAddress", "Address");
			//dataGridView1.Columns.Add("DataCount", "Count");
			//dataGridView1.Columns.Add("DataType", "Type");
			//dataGridView1.Columns.Add("DataName", "Name");

			var bindingList = new BindingList<ModbusParameter>(poll.DataMaps[0].mParams);
			var source = new BindingSource(bindingList, null);
			dataGridView1.DataSource = source;


			mPoll = poll;
			mMaps = new List<PollInterpreterMap>(poll.DataMaps);

			foreach (PollInterpreterMap map in mMaps)
			{
				listBox_mapsList.Items.Add(map.Name);
			}
		}

		private void LoadMap(int index)
		{
			if (index < 0) return;

			textBox_mapName.Text = mMaps[index].Name;
		}

		private void listBox_mapsList_DoubleClick(object sender, EventArgs e)
		{
			LoadMap(listBox_mapsList.SelectedIndex);
		}
	}
}
