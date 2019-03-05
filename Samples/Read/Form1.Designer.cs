namespace Read
{
	partial class Form1
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
			this.treeView_polls = new System.Windows.Forms.TreeView();
			this.button_addPoll = new System.Windows.Forms.Button();
			this.button_delPoll = new System.Windows.Forms.Button();
			this.button_stopPoll = new System.Windows.Forms.Button();
			this.button_startPoll = new System.Windows.Forms.Button();
			this.comboBox_ports = new System.Windows.Forms.ComboBox();
			this.richTextBox_messages = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// treeView_polls
			// 
			this.treeView_polls.Location = new System.Drawing.Point(12, 46);
			this.treeView_polls.Name = "treeView_polls";
			this.treeView_polls.Size = new System.Drawing.Size(228, 433);
			this.treeView_polls.TabIndex = 0;
			// 
			// button_addPoll
			// 
			this.button_addPoll.Location = new System.Drawing.Point(12, 12);
			this.button_addPoll.Name = "button_addPoll";
			this.button_addPoll.Size = new System.Drawing.Size(92, 28);
			this.button_addPoll.TabIndex = 1;
			this.button_addPoll.Text = "Add Poll";
			this.button_addPoll.UseVisualStyleBackColor = true;
			this.button_addPoll.Click += new System.EventHandler(this.button_addPoll_Click);
			// 
			// button_delPoll
			// 
			this.button_delPoll.Location = new System.Drawing.Point(148, 12);
			this.button_delPoll.Name = "button_delPoll";
			this.button_delPoll.Size = new System.Drawing.Size(92, 28);
			this.button_delPoll.TabIndex = 2;
			this.button_delPoll.Text = "Delete Poll";
			this.button_delPoll.UseVisualStyleBackColor = true;
			// 
			// button_stopPoll
			// 
			this.button_stopPoll.Location = new System.Drawing.Point(418, 67);
			this.button_stopPoll.Name = "button_stopPoll";
			this.button_stopPoll.Size = new System.Drawing.Size(92, 28);
			this.button_stopPoll.TabIndex = 4;
			this.button_stopPoll.Text = "Stop";
			this.button_stopPoll.UseVisualStyleBackColor = true;
			this.button_stopPoll.Click += new System.EventHandler(this.button_stopPoll_Click);
			// 
			// button_startPoll
			// 
			this.button_startPoll.Location = new System.Drawing.Point(304, 67);
			this.button_startPoll.Name = "button_startPoll";
			this.button_startPoll.Size = new System.Drawing.Size(92, 28);
			this.button_startPoll.TabIndex = 3;
			this.button_startPoll.Text = "Start";
			this.button_startPoll.UseVisualStyleBackColor = true;
			this.button_startPoll.Click += new System.EventHandler(this.button_startPoll_Click);
			// 
			// comboBox_ports
			// 
			this.comboBox_ports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_ports.FormattingEnabled = true;
			this.comboBox_ports.Location = new System.Drawing.Point(304, 12);
			this.comboBox_ports.Name = "comboBox_ports";
			this.comboBox_ports.Size = new System.Drawing.Size(121, 21);
			this.comboBox_ports.TabIndex = 5;
			// 
			// richTextBox_messages
			// 
			this.richTextBox_messages.Location = new System.Drawing.Point(267, 115);
			this.richTextBox_messages.Name = "richTextBox_messages";
			this.richTextBox_messages.Size = new System.Drawing.Size(263, 364);
			this.richTextBox_messages.TabIndex = 6;
			this.richTextBox_messages.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(542, 491);
			this.Controls.Add(this.richTextBox_messages);
			this.Controls.Add(this.comboBox_ports);
			this.Controls.Add(this.button_stopPoll);
			this.Controls.Add(this.button_startPoll);
			this.Controls.Add(this.button_delPoll);
			this.Controls.Add(this.button_addPoll);
			this.Controls.Add(this.treeView_polls);
			this.Name = "Form1";
			this.Text = "s";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView_polls;
		private System.Windows.Forms.Button button_addPoll;
		private System.Windows.Forms.Button button_delPoll;
		private System.Windows.Forms.Button button_stopPoll;
		private System.Windows.Forms.Button button_startPoll;
		private System.Windows.Forms.ComboBox comboBox_ports;
		private System.Windows.Forms.RichTextBox richTextBox_messages;
	}
}

