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
			this.button_startPoll = new System.Windows.Forms.Button();
			this.comboBox_ports = new System.Windows.Forms.ComboBox();
			this.richTextBox_data = new System.Windows.Forms.RichTextBox();
			this.label_status = new System.Windows.Forms.Label();
			this.button_pauseResume = new System.Windows.Forms.Button();
			this.richTextBox_messages = new System.Windows.Forms.RichTextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.pollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pollToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.editMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView_polls
			// 
			this.treeView_polls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.treeView_polls.Location = new System.Drawing.Point(12, 74);
			this.treeView_polls.Name = "treeView_polls";
			this.treeView_polls.Size = new System.Drawing.Size(228, 433);
			this.treeView_polls.TabIndex = 0;
			this.treeView_polls.DoubleClick += new System.EventHandler(this.treeView_polls_DoubleClick);
			// 
			// button_addPoll
			// 
			this.button_addPoll.Location = new System.Drawing.Point(12, 40);
			this.button_addPoll.Name = "button_addPoll";
			this.button_addPoll.Size = new System.Drawing.Size(92, 28);
			this.button_addPoll.TabIndex = 1;
			this.button_addPoll.Text = "Add Poll";
			this.button_addPoll.UseVisualStyleBackColor = true;
			this.button_addPoll.Click += new System.EventHandler(this.button_addPoll_Click);
			// 
			// button_delPoll
			// 
			this.button_delPoll.Location = new System.Drawing.Point(148, 40);
			this.button_delPoll.Name = "button_delPoll";
			this.button_delPoll.Size = new System.Drawing.Size(92, 28);
			this.button_delPoll.TabIndex = 2;
			this.button_delPoll.Text = "Delete Poll";
			this.button_delPoll.UseVisualStyleBackColor = true;
			this.button_delPoll.Click += new System.EventHandler(this.button_delPoll_Click);
			// 
			// button_startPoll
			// 
			this.button_startPoll.Location = new System.Drawing.Point(492, 33);
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
			this.comboBox_ports.Location = new System.Drawing.Point(304, 40);
			this.comboBox_ports.Name = "comboBox_ports";
			this.comboBox_ports.Size = new System.Drawing.Size(121, 21);
			this.comboBox_ports.TabIndex = 5;
			// 
			// richTextBox_data
			// 
			this.richTextBox_data.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.richTextBox_data.Location = new System.Drawing.Point(267, 74);
			this.richTextBox_data.Name = "richTextBox_data";
			this.richTextBox_data.Size = new System.Drawing.Size(460, 433);
			this.richTextBox_data.TabIndex = 6;
			this.richTextBox_data.Text = "";
			// 
			// label_status
			// 
			this.label_status.AutoSize = true;
			this.label_status.Location = new System.Drawing.Point(797, 43);
			this.label_status.Name = "label_status";
			this.label_status.Size = new System.Drawing.Size(35, 13);
			this.label_status.TabIndex = 7;
			this.label_status.Text = "label1";
			// 
			// button_pauseResume
			// 
			this.button_pauseResume.Location = new System.Drawing.Point(605, 35);
			this.button_pauseResume.Name = "button_pauseResume";
			this.button_pauseResume.Size = new System.Drawing.Size(92, 28);
			this.button_pauseResume.TabIndex = 8;
			this.button_pauseResume.Text = "Pause";
			this.button_pauseResume.UseVisualStyleBackColor = true;
			this.button_pauseResume.Click += new System.EventHandler(this.button_pauseResume_Click);
			// 
			// richTextBox_messages
			// 
			this.richTextBox_messages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.richTextBox_messages.Location = new System.Drawing.Point(733, 74);
			this.richTextBox_messages.Name = "richTextBox_messages";
			this.richTextBox_messages.Size = new System.Drawing.Size(190, 433);
			this.richTextBox_messages.TabIndex = 9;
			this.richTextBox_messages.Text = "";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pollToolStripMenuItem,
            this.pollToolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(935, 24);
			this.menuStrip1.TabIndex = 10;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// pollToolStripMenuItem
			// 
			this.pollToolStripMenuItem.Name = "pollToolStripMenuItem";
			this.pollToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.pollToolStripMenuItem.Text = "File";
			// 
			// pollToolStripMenuItem1
			// 
			this.pollToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMapsToolStripMenuItem});
			this.pollToolStripMenuItem1.Name = "pollToolStripMenuItem1";
			this.pollToolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
			this.pollToolStripMenuItem1.Text = "Poll";
			// 
			// editMapsToolStripMenuItem
			// 
			this.editMapsToolStripMenuItem.Name = "editMapsToolStripMenuItem";
			this.editMapsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.editMapsToolStripMenuItem.Text = "Edit Maps";
			this.editMapsToolStripMenuItem.Click += new System.EventHandler(this.editMapsToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(935, 522);
			this.Controls.Add(this.richTextBox_messages);
			this.Controls.Add(this.button_pauseResume);
			this.Controls.Add(this.label_status);
			this.Controls.Add(this.richTextBox_data);
			this.Controls.Add(this.comboBox_ports);
			this.Controls.Add(this.button_startPoll);
			this.Controls.Add(this.button_delPoll);
			this.Controls.Add(this.button_addPoll);
			this.Controls.Add(this.treeView_polls);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "s";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeView_polls;
		private System.Windows.Forms.Button button_addPoll;
		private System.Windows.Forms.Button button_delPoll;
		private System.Windows.Forms.Button button_startPoll;
		private System.Windows.Forms.ComboBox comboBox_ports;
		private System.Windows.Forms.RichTextBox richTextBox_data;
		private System.Windows.Forms.Label label_status;
		private System.Windows.Forms.Button button_pauseResume;
		private System.Windows.Forms.RichTextBox richTextBox_messages;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem pollToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pollToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem editMapsToolStripMenuItem;
	}
}

