namespace CTP_交易
{
	partial class FormServers
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServers));
			this.dataGridViewServers = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewServers)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewServers
			// 
			this.dataGridViewServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewServers.Dock = System.Windows.Forms.DockStyle.Top;
			this.dataGridViewServers.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewServers.Name = "dataGridViewServers";
			this.dataGridViewServers.RowTemplate.Height = 23;
			this.dataGridViewServers.Size = new System.Drawing.Size(548, 264);
			this.dataGridViewServers.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(122, 275);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(267, 275);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "关闭";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// FormServers
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(548, 310);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGridViewServers);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormServers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "服务器";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewServers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.DataGridView dataGridViewServers;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

	}
}