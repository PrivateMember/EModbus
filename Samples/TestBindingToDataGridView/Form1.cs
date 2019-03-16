using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestBindingToDataGridView
{
	public partial class Form1 : Form
	{
		private List<EModbus.ModbusPollParameter> mList = null;
		private BindingList<EModbus.ModbusPollParameter> mBindingList;
		private BindingSource mBindingSource1 = null;
		private BindingSource mBindingSource2 = null;

		public Form1()
		{
			InitializeComponent();

			dataGridView1.AllowUserToAddRows = false;
			dataGridView2.AllowUserToAddRows = false;

			mList = new List<EModbus.ModbusPollParameter>();
			mBindingList = new BindingList<EModbus.ModbusPollParameter>();


			mBindingSource1 = new BindingSource();
			mBindingSource1.AllowNew = true;
			mBindingSource1.AddingNew += MBindingSource_AddingNew;
			mBindingSource1.CurrentChanged += MBindingSource1_CurrentChanged;
			mBindingSource1.CurrentItemChanged += MBindingSource1_CurrentItemChanged;
			mBindingSource1.DataError += MBindingSource1_DataError;
			mBindingSource1.ListChanged += MBindingSource1_ListChanged;
			mBindingSource1.DataSource = mList;

			mBindingSource2 = new BindingSource();
			mBindingSource2.AllowNew = true;
			mBindingSource2.AddingNew += MBindingSource_AddingNew;
			mBindingSource2.CurrentChanged += MBindingSource2_CurrentChanged;
			mBindingSource2.CurrentItemChanged += MBindingSource2_CurrentItemChanged;
			mBindingSource2.DataError += MBindingSource2_DataError;
			mBindingSource2.ListChanged += MBindingSource2_ListChanged;
			mBindingSource2.DataSource = mBindingList;

			dataGridView1.DataSource = mBindingSource1;
			dataGridView2.DataSource = mBindingSource2;
		}

		private void MBindingSource2_ListChanged(object sender, ListChangedEventArgs e)
		{
			BindingSource bs = sender as BindingSource;

			if (bs.DataSource != null)
			{
				List<EModbus.ModbusPollParameter> list = bs.DataSource as List<EModbus.ModbusPollParameter>;

				int startIndex = bs.Position;

				//	ModbusTableAutoAddress(list, startIndex);
				//	dataGridView_modbusMap.DataSource = mBindingSourceModbus;
				dataGridView2.Refresh();
			}
		}

		private void MBindingSource2_DataError(object sender, BindingManagerDataErrorEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void MBindingSource2_CurrentItemChanged(object sender, EventArgs e)
		{
			dataGridView2.AutoResizeColumns();
		}

		private void MBindingSource2_CurrentChanged(object sender, EventArgs e)
		{
			dataGridView2.AutoResizeColumns();
		}

		private void MBindingSource1_ListChanged(object sender, ListChangedEventArgs e)
		{
			BindingSource bs = sender as BindingSource;

			if (bs.DataSource != null)
			{
				List<EModbus.ModbusPollParameter> list = bs.DataSource as List<EModbus.ModbusPollParameter>;

				int startIndex = bs.Position;

				//	ModbusTableAutoAddress(list, startIndex);
				//	dataGridView_modbusMap.DataSource = mBindingSourceModbus;
				dataGridView1.Refresh();
			}
		}

		private void MBindingSource1_DataError(object sender, BindingManagerDataErrorEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void MBindingSource1_CurrentItemChanged(object sender, EventArgs e)
		{
			dataGridView1.AutoResizeColumns();
		}

		private void MBindingSource1_CurrentChanged(object sender, EventArgs e)
		{
			dataGridView1.AutoResizeColumns();
		}

		private void MBindingSource_AddingNew(object sender, AddingNewEventArgs e)
		{
			e.NewObject = new EModbus.ModbusPollParameter();
		}

		private void button_add_Click(object sender, EventArgs e)
		{
			mBindingSource1.AddNew();
		}

		private void button_add2_Click(object sender, EventArgs e)
		{
			mBindingSource2.AddNew();
		}
	}
}