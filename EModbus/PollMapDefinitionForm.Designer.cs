﻿namespace EModbus
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
			this.button_new = new System.Windows.Forms.Button();
			this.textBox_mapName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_ok = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.button_cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// listBox_mapsList
			// 
			this.listBox_mapsList.FormattingEnabled = true;
			this.listBox_mapsList.Location = new System.Drawing.Point(21, 72);
			this.listBox_mapsList.Name = "listBox_mapsList";
			this.listBox_mapsList.Size = new System.Drawing.Size(150, 355);
			this.listBox_mapsList.TabIndex = 0;
			this.listBox_mapsList.DoubleClick += new System.EventHandler(this.listBox_mapsList_DoubleClick);
			// 
			// button_new
			// 
			this.button_new.Location = new System.Drawing.Point(21, 31);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(75, 23);
			this.button_new.TabIndex = 1;
			this.button_new.Text = "New";
			this.button_new.UseVisualStyleBackColor = true;
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
			this.button_ok.Location = new System.Drawing.Point(264, 475);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(132, 30);
			this.button_ok.TabIndex = 4;
			this.button_ok.Text = "Ok";
			this.button_ok.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(187, 72);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(629, 355);
			this.dataGridView1.TabIndex = 5;
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(126, 475);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(132, 30);
			this.button_cancel.TabIndex = 6;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			// 
			// PollMapDefinitionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(838, 532);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_mapName);
			this.Controls.Add(this.button_new);
			this.Controls.Add(this.listBox_mapsList);
			this.Name = "PollMapDefinitionForm";
			this.Text = "PollMapDefinitionForm";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox_mapsList;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.TextBox textBox_mapName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button button_cancel;
	}
}