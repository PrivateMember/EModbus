namespace EModbus
{
	partial class PollMapDefinitionForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox_mapsList = new System.Windows.Forms.ListBox();
			this.button_newMap = new System.Windows.Forms.Button();
			this.textBox_mapName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_ok = new System.Windows.Forms.Button();
			this.dataGridView_modbusMap = new System.Windows.Forms.DataGridView();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_newParam = new System.Windows.Forms.Button();
			this.button_delParam = new System.Windows.Forms.Button();
			this.button_clearParams = new System.Windows.Forms.Button();
			this.button_delMap = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_modbusMap)).BeginInit();
			this.SuspendLayout();
			// 
			// listBox_mapsList
			// 
			this.listBox_mapsList.FormattingEnabled = true;
			this.listBox_mapsList.Location = new System.Drawing.Point(21, 72);
			this.listBox_mapsList.Name = "listBox_mapsList";
			this.listBox_mapsList.Size = new System.Drawing.Size(184, 355);
			this.listBox_mapsList.TabIndex = 0;
			this.listBox_mapsList.SelectedIndexChanged += new System.EventHandler(this.listBox_mapsList_SelectedIndexChanged);
			// 
			// button_newMap
			// 
			this.button_newMap.Location = new System.Drawing.Point(21, 31);
			this.button_newMap.Name = "button_newMap";
			this.button_newMap.Size = new System.Drawing.Size(57, 23);
			this.button_newMap.TabIndex = 1;
			this.button_newMap.Text = "New";
			this.button_newMap.UseVisualStyleBackColor = true;
			this.button_newMap.Click += new System.EventHandler(this.button_newMap_Click);
			// 
			// textBox_mapName
			// 
			this.textBox_mapName.Location = new System.Drawing.Point(291, 34);
			this.textBox_mapName.Name = "textBox_mapName";
			this.textBox_mapName.Size = new System.Drawing.Size(167, 20);
			this.textBox_mapName.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(226, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Map Name";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(684, 447);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(132, 30);
			this.button_ok.TabIndex = 4;
			this.button_ok.Text = "Ok";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// dataGridView_modbusMap
			// 
			this.dataGridView_modbusMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_modbusMap.Location = new System.Drawing.Point(229, 72);
			this.dataGridView_modbusMap.Name = "dataGridView_modbusMap";
			this.dataGridView_modbusMap.Size = new System.Drawing.Size(587, 355);
			this.dataGridView_modbusMap.TabIndex = 5;
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(532, 447);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(132, 30);
			this.button_cancel.TabIndex = 6;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// button_newParam
			// 
			this.button_newParam.Location = new System.Drawing.Point(532, 34);
			this.button_newParam.Name = "button_newParam";
			this.button_newParam.Size = new System.Drawing.Size(75, 23);
			this.button_newParam.TabIndex = 7;
			this.button_newParam.Text = "New";
			this.button_newParam.UseVisualStyleBackColor = true;
			this.button_newParam.Click += new System.EventHandler(this.button_newParam_Click);
			// 
			// button_delParam
			// 
			this.button_delParam.Location = new System.Drawing.Point(613, 34);
			this.button_delParam.Name = "button_delParam";
			this.button_delParam.Size = new System.Drawing.Size(75, 23);
			this.button_delParam.TabIndex = 8;
			this.button_delParam.Text = "Delete";
			this.button_delParam.UseVisualStyleBackColor = true;
			this.button_delParam.Click += new System.EventHandler(this.button_delParam_Click);
			// 
			// button_clearParams
			// 
			this.button_clearParams.Location = new System.Drawing.Point(741, 34);
			this.button_clearParams.Name = "button_clearParams";
			this.button_clearParams.Size = new System.Drawing.Size(75, 23);
			this.button_clearParams.TabIndex = 9;
			this.button_clearParams.Text = "Clear";
			this.button_clearParams.UseVisualStyleBackColor = true;
			this.button_clearParams.Click += new System.EventHandler(this.button_clearParams_Click);
			// 
			// button_delMap
			// 
			this.button_delMap.Location = new System.Drawing.Point(84, 32);
			this.button_delMap.Name = "button_delMap";
			this.button_delMap.Size = new System.Drawing.Size(57, 23);
			this.button_delMap.TabIndex = 10;
			this.button_delMap.Text = "Delete";
			this.button_delMap.UseVisualStyleBackColor = true;
			this.button_delMap.Click += new System.EventHandler(this.button_delMap_Click);
			// 
			// PollMapDefinitionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(838, 500);
			this.Controls.Add(this.button_delMap);
			this.Controls.Add(this.button_clearParams);
			this.Controls.Add(this.button_delParam);
			this.Controls.Add(this.button_newParam);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.dataGridView_modbusMap);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_mapName);
			this.Controls.Add(this.button_newMap);
			this.Controls.Add(this.listBox_mapsList);
			this.Name = "PollMapDefinitionForm";
			this.Text = "PollMapDefinitionForm";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_modbusMap)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox_mapsList;
		private System.Windows.Forms.Button button_newMap;
		private System.Windows.Forms.TextBox textBox_mapName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.DataGridView dataGridView_modbusMap;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Button button_newParam;
		private System.Windows.Forms.Button button_delParam;
		private System.Windows.Forms.Button button_clearParams;
		private System.Windows.Forms.Button button_delMap;
	}
}