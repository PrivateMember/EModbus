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
		private int mModbusSelectedRowIndex = 0;
		private ComboBox mComboModbusDataType = null;
	//	private ComboBox mComboModbusDataFormat = null;
	//	private ComboBox mComboModbusDataAccess = null;
		private BindingSource mBindingSourceModbus = null;
		private BindingList<PollInterpreterMap> mBindingListMapNames = null;

		/// <summary>
		/// returns a cloned ModbusPoll object
		/// </summary>
		public ModbusMaster.ModbusPoll Poll { get { return mPoll.Clone() as ModbusMaster.ModbusPoll; } }

		public PollMapDefinitionForm(ModbusMaster.ModbusPoll poll)
		{
			mPoll = poll.Clone() as ModbusMaster.ModbusPoll;

			InitializeComponent();

			CancelButton = button_cancel;
			AcceptButton = button_ok;

			mBindingSourceModbus = new BindingSource();
			mBindingSourceModbus.AllowNew = true;
			mBindingSourceModbus.AddingNew += bindindSourceModbus_AddingNew;
			mBindingSourceModbus.CurrentItemChanged += bindingSourceModbus_CurrentItemChanged;
			mBindingSourceModbus.CurrentChanged += bindingSourceModbus_CurrentChanged;
			mBindingSourceModbus.DataError += bindingSource_DataError;
			mBindingSourceModbus.ListChanged += bindingSourceModbus_ListChanged;
			mBindingSourceModbus.DataSource = mPoll.DataMaps[0].mParams;

			mComboModbusDataType = new ComboBox();
			mComboModbusDataType.DropDownStyle = ComboBoxStyle.DropDownList;
			mComboModbusDataType.DataSource = Enum.GetValues(typeof(DataType));
			mComboModbusDataType.DropDownClosed += MComboModbusDataType_DropDownClosed;

			//mComboModbusDataFormat = new ComboBox();
			//mComboModbusDataFormat.DropDownStyle = ComboBoxStyle.DropDownList;
			//mComboModbusDataFormat.DataSource = Enum.GetValues(typeof(ModbusDataFormat));
			//mComboModbusDataFormat.DropDownClosed += mComboModbusDataFormat_DropDownClosed;

			//mComboModbusDataAccess = new ComboBox();
			//mComboModbusDataAccess.DropDownStyle = ComboBoxStyle.DropDownList;
			//mComboModbusDataAccess.DataSource = Enum.GetValues(typeof(ModbusDataAccess));
			//mComboModbusDataAccess.DropDownClosed += mComboModbusDataAccess_DropDownClosed;

			mComboModbusDataType.Hide();
			//mComboModbusDataFormat.Hide();
			//mComboModbusDataAccess.Hide();

			this.Controls.Add(mComboModbusDataType);

			dataGridView_modbusMap.KeyDown += DataGridView_modbusMap_KeyDown;
			dataGridView_modbusMap.CellClick += DataGridView_modbusMap_CellClick;
			dataGridView_modbusMap.AllowUserToAddRows = false;
			dataGridView_modbusMap.AllowUserToDeleteRows = false;
			dataGridView_modbusMap.SelectionMode = DataGridViewSelectionMode.CellSelect;
			dataGridView_modbusMap.CellValidated += DataGridView_modbusMap_CellValidated; ;
			dataGridView_modbusMap.DataError += DataGridView_modbusMap_DataError; ;
			dataGridView_modbusMap.Font = new Font(new FontFamily("Courier New"), 10, FontStyle.Regular);
			dataGridView_modbusMap.DataSource = mBindingSourceModbus;

			//foreach (PollInterpreterMap map in mPoll.DataMaps)
			//{
			//	listBox_mapsList.Items.Add(map.Name);
			//}

			textBox_mapName.TextChanged += TextBox_mapName_TextChanged;


			mBindingListMapNames = new BindingList<PollInterpreterMap>(mPoll.DataMaps);
			listBox_mapsList.DataSource = mBindingListMapNames;
			listBox_mapsList.DisplayMember = "Name";
			listBox_mapsList.SelectedIndex = 0;
		}

		private void TextBox_mapName_TextChanged(object sender, EventArgs e)
		{
			ChangeCurrentMapName(textBox_mapName.Text);
		}

		private void ChangeCurrentMapName(string str)
		{
			mBindingListMapNames[listBox_mapsList.SelectedIndex].Name = str;
			listBox_mapsList.DisplayMember = ""; // to force the listbox to redraw items
			listBox_mapsList.DisplayMember = "Name";
		}

		private void DataGridView_modbusMap_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			
		}

		private void DataGridView_modbusMap_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dgv = (sender as DataGridView);

			if (dgv == dataGridView_modbusMap)
			{
				if (dgv.Columns[e.ColumnIndex].HeaderText == "Address" ||
					dgv.Columns[e.ColumnIndex].HeaderText == "RegCount")
				{
				//	ModbusTableAutoAddress(mListModbus, e.RowIndex);
				//	//	dataGridView_modbus.Refresh();
				}
			}

			dgv.AutoResizeColumns();
		}

		private void bindingSourceModbus_ListChanged(object sender, ListChangedEventArgs e)
		{
		//	if (mModbusAutoAddressEnable)
			{
				BindingSource bs = sender as BindingSource;

				if (bs.DataSource != null)
				{
					List<ModbusPollParameter> list = bs.DataSource as List<ModbusPollParameter>;

					int startIndex = bs.Position;

					//	ModbusTableAutoAddress(list, startIndex);
					//	dataGridView_modbusMap.DataSource = mBindingSourceModbus;
					dataGridView_modbusMap.Refresh();
				}
			}
		}

		private void bindingSource_DataError(object sender, BindingManagerDataErrorEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void bindingSourceModbus_CurrentChanged(object sender, EventArgs e)
		{
			dataGridView_modbusMap.AutoResizeColumns();
		}

		private void bindingSourceModbus_CurrentItemChanged(object sender, EventArgs e)
		{
			dataGridView_modbusMap.AutoResizeColumns();
		}

		private void bindindSourceModbus_AddingNew(object sender, AddingNewEventArgs e)
		{
			e.NewObject = new ModbusPollParameter();
		}

		private void MComboModbusDataType_DropDownClosed(object sender, EventArgs e)
		{
			mComboModbusDataType.Hide();

			ModbusPollParameter modParam = new ModbusPollParameter(mPoll.DataMaps[0].mParams[mModbusSelectedRowIndex]);
			modParam.Type = (DataType)mComboModbusDataType.SelectedItem;
			mBindingSourceModbus[mModbusSelectedRowIndex] = modParam;

			this.ActiveControl = dataGridView_modbusMap;
			dataGridView_modbusMap.Focus();
			dataGridView_modbusMap.Refresh();
		}

		private void DataGridView_modbusMap_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
			{
				ComboBox combo = null;
				mModbusSelectedRowIndex = e.RowIndex;

				if (dataGridView_modbusMap.Columns[e.ColumnIndex].HeaderText == "Type")
				{
					combo = mComboModbusDataType;
				}
				else if (dataGridView_modbusMap.Columns[e.ColumnIndex].HeaderText == "Format")
				{
				//	combo = mComboModbusDataFormat;
				}
				else if (dataGridView_modbusMap.Columns[e.ColumnIndex].HeaderText == "Access")
				{
				//	combo = mComboModbusDataAccess;
				}

				if (combo != null)
				{
					ShowComboInDataGrid(dataGridView_modbusMap, combo, e.RowIndex, e.ColumnIndex);
				}
			}
		}

		private void DataGridView_modbusMap_KeyDown(object sender, KeyEventArgs e)
		{
			int colIndex = (sender as DataGridView).CurrentCell.ColumnIndex;
			int rowIndex = (sender as DataGridView).CurrentCell.RowIndex;
			mModbusSelectedRowIndex = rowIndex;

			if (e.KeyCode == Keys.Space)
			{
				ComboBox combo = null;

				if (dataGridView_modbusMap.Columns[colIndex].HeaderText == "Type")
				{
					combo = mComboModbusDataType;
				}
				else if (dataGridView_modbusMap.Columns[colIndex].HeaderText == "Format")
				{
				//	combo = mComboModbusDataFormat;
				}
				else if (dataGridView_modbusMap.Columns[colIndex].HeaderText == "Access")
				{
				//	combo = mComboModbusDataAccess;
				}

				if (combo != null)
				{
					ShowComboInDataGrid(dataGridView_modbusMap, combo, rowIndex, colIndex);
				}
			}
		}

		void ShowComboInDataGrid(DataGridView dgv, ComboBox combo, int rowIndex, int colIndex)
		{
			if (combo != null && dgv != null)
			{
				var cellRectangle = dgv.GetCellDisplayRectangle(colIndex, rowIndex, true);
				Point parentLocation = dgv.FindForm().PointToClient(dgv.Parent.PointToScreen(dgv.Location));
				Point comboLocation = new Point(cellRectangle.Location.X + parentLocation.X, cellRectangle.Location.Y + parentLocation.Y);

				combo.SelectedItem = dgv.Rows[rowIndex].Cells[colIndex].Value;
				combo.Size = cellRectangle.Size;
				combo.Location = comboLocation;
				combo.Show();
				SetComboDropListWidthMax(combo);
				combo.Focus();
				combo.DroppedDown = true;
			}
		}

		void SetComboDropListWidthMax(ComboBox combo)
		{
			int maxWidth = 0, temp = 0;

			string[] list = new string[combo.Items.Count];

			for (int i = 0; i < combo.Items.Count; i++)
			{
				list[i] = combo.Items[i].ToString();
			}


			for (int i = 0; i < list.Length; i++)
			{
				temp = TextRenderer.MeasureText(list[i], combo.Font).Width;
				if (temp > maxWidth)
				{
					maxWidth = temp;
				}
			}

			combo.DropDownWidth = maxWidth;
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void button_newParam_Click(object sender, EventArgs e)
		{
			mBindingSourceModbus.AddNew();
		}

		private void button_newMap_Click(object sender, EventArgs e)
		{
			mBindingListMapNames.Add(new PollInterpreterMap(mPoll.ResponseDataLengthBytes));
			listBox_mapsList.SelectedIndex = mBindingListMapNames.Count - 1;
		}

		private void listBox_mapsList_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = listBox_mapsList.SelectedIndex;

			mBindingSourceModbus.DataSource = mPoll.DataMaps[index].mParams;
			textBox_mapName.Text = mBindingListMapNames[index].Name;
		}

		private void button_delParam_Click(object sender, EventArgs e)
		{
			if (mBindingSourceModbus.Count > 1)
			{
				mBindingSourceModbus.RemoveAt(dataGridView_modbusMap.CurrentCell.RowIndex);
			}
		}

		private void button_clearParams_Click(object sender, EventArgs e)
		{
			mBindingSourceModbus.Clear();
			mBindingSourceModbus.AddNew();
		}

		private void button_delMap_Click(object sender, EventArgs e)
		{
			if (mBindingListMapNames.Count > 1)
			{
				mBindingListMapNames.RemoveAt(listBox_mapsList.SelectedIndex);
			}
		}
	}
}
