namespace TestBindingToDataGridView
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.button_add1 = new System.Windows.Forms.Button();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.button_add2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(28, 50);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(477, 270);
			this.dataGridView1.TabIndex = 0;
			// 
			// button_add1
			// 
			this.button_add1.Location = new System.Drawing.Point(28, 10);
			this.button_add1.Name = "button_add1";
			this.button_add1.Size = new System.Drawing.Size(95, 34);
			this.button_add1.TabIndex = 1;
			this.button_add1.Text = "Add";
			this.button_add1.UseVisualStyleBackColor = true;
			this.button_add1.Click += new System.EventHandler(this.button_add_Click);
			// 
			// dataGridView2
			// 
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Location = new System.Drawing.Point(511, 50);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.Size = new System.Drawing.Size(475, 270);
			this.dataGridView2.TabIndex = 2;
			// 
			// button_add2
			// 
			this.button_add2.Location = new System.Drawing.Point(511, 10);
			this.button_add2.Name = "button_add2";
			this.button_add2.Size = new System.Drawing.Size(95, 34);
			this.button_add2.TabIndex = 3;
			this.button_add2.Text = "Add";
			this.button_add2.UseVisualStyleBackColor = true;
			this.button_add2.Click += new System.EventHandler(this.button_add2_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(998, 343);
			this.Controls.Add(this.button_add2);
			this.Controls.Add(this.dataGridView2);
			this.Controls.Add(this.button_add1);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button button_add1;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.Button button_add2;
	}
}

