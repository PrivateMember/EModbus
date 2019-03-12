namespace EModbus
{
	partial class ModbusPollDefinitionForm
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
			this.numericUpDown_MBID = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown_regAddr = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDown_regCount = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox_types = new System.Windows.Forms.ComboBox();
			this.checkBox_devEnabled = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDown_rate = new System.Windows.Forms.NumericUpDown();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDown_timeout = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.button_maps = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MBID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_regAddr)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_regCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_rate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeout)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDown_MBID
			// 
			this.numericUpDown_MBID.Location = new System.Drawing.Point(117, 47);
			this.numericUpDown_MBID.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown_MBID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown_MBID.Name = "numericUpDown_MBID";
			this.numericUpDown_MBID.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown_MBID.TabIndex = 0;
			this.numericUpDown_MBID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_MBID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(27, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Modbus ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(27, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Data Address";
			// 
			// numericUpDown_regAddr
			// 
			this.numericUpDown_regAddr.Location = new System.Drawing.Point(117, 82);
			this.numericUpDown_regAddr.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown_regAddr.Name = "numericUpDown_regAddr";
			this.numericUpDown_regAddr.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown_regAddr.TabIndex = 2;
			this.numericUpDown_regAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(27, 119);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Data Count";
			// 
			// numericUpDown_regCount
			// 
			this.numericUpDown_regCount.Location = new System.Drawing.Point(117, 117);
			this.numericUpDown_regCount.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown_regCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown_regCount.Name = "numericUpDown_regCount";
			this.numericUpDown_regCount.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown_regCount.TabIndex = 4;
			this.numericUpDown_regCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_regCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(27, 156);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Default Type";
			// 
			// comboBox_types
			// 
			this.comboBox_types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_types.FormattingEnabled = true;
			this.comboBox_types.Location = new System.Drawing.Point(116, 153);
			this.comboBox_types.Name = "comboBox_types";
			this.comboBox_types.Size = new System.Drawing.Size(121, 21);
			this.comboBox_types.TabIndex = 7;
			// 
			// checkBox_devEnabled
			// 
			this.checkBox_devEnabled.AutoSize = true;
			this.checkBox_devEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox_devEnabled.Checked = true;
			this.checkBox_devEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_devEnabled.Location = new System.Drawing.Point(116, 260);
			this.checkBox_devEnabled.Name = "checkBox_devEnabled";
			this.checkBox_devEnabled.Size = new System.Drawing.Size(15, 14);
			this.checkBox_devEnabled.TabIndex = 8;
			this.checkBox_devEnabled.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(27, 260);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Enabled";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(26, 193);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Poll Rate (ms)";
			// 
			// numericUpDown_rate
			// 
			this.numericUpDown_rate.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDown_rate.Location = new System.Drawing.Point(116, 191);
			this.numericUpDown_rate.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numericUpDown_rate.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_rate.Name = "numericUpDown_rate";
			this.numericUpDown_rate.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown_rate.TabIndex = 10;
			this.numericUpDown_rate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_rate.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(137, 291);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(100, 34);
			this.button_ok.TabIndex = 12;
			this.button_ok.Text = "Ok";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(31, 291);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(100, 34);
			this.button_cancel.TabIndex = 13;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(27, 225);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(67, 13);
			this.label7.TabIndex = 15;
			this.label7.Text = "Timeout (ms)";
			// 
			// numericUpDown_timeout
			// 
			this.numericUpDown_timeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDown_timeout.Location = new System.Drawing.Point(116, 223);
			this.numericUpDown_timeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numericUpDown_timeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_timeout.Name = "numericUpDown_timeout";
			this.numericUpDown_timeout.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown_timeout.TabIndex = 14;
			this.numericUpDown_timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_timeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(28, 19);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(55, 13);
			this.label8.TabIndex = 16;
			this.label8.Text = "Poll Name";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(117, 16);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(217, 20);
			this.textBox_name.TabIndex = 17;
			// 
			// button_maps
			// 
			this.button_maps.Location = new System.Drawing.Point(256, 291);
			this.button_maps.Name = "button_maps";
			this.button_maps.Size = new System.Drawing.Size(100, 34);
			this.button_maps.TabIndex = 18;
			this.button_maps.Text = "Edit Maps";
			this.button_maps.UseVisualStyleBackColor = true;
			this.button_maps.Click += new System.EventHandler(this.button_maps_Click);
			// 
			// ModbusPollDefinitionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(379, 348);
			this.Controls.Add(this.button_maps);
			this.Controls.Add(this.textBox_name);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.numericUpDown_timeout);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.numericUpDown_rate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkBox_devEnabled);
			this.Controls.Add(this.comboBox_types);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numericUpDown_regCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDown_regAddr);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numericUpDown_MBID);
			this.Name = "ModbusPollDefinitionForm";
			this.Text = "Modbus Poll Definition";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MBID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_regAddr)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_regCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_rate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeout)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDown_MBID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown_regAddr;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDown_regCount;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox_types;
		private System.Windows.Forms.CheckBox checkBox_devEnabled;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDown_rate;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDown_timeout;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.Button button_maps;
	}
}