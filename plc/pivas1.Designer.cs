namespace PIVASWork
{
    partial class main
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.print_fz = new System.Windows.Forms.Button();
            this.excel_sql = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.plg = new System.Windows.Forms.Panel();
            this.gd2 = new System.Windows.Forms.TextBox();
            this.gd3 = new System.Windows.Forms.TextBox();
            this.gd1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.online_plc = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.ctwo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typ2 = new System.Windows.Forms.TableLayoutPanel();
            this.p1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.p2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.plg.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.typ2.SuspendLayout();
            this.p1.SuspendLayout();
            this.p2.SuspendLayout();
            this.p3.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataBits = 7;
            this.serialPort1.Parity = System.IO.Ports.Parity.Even;
            this.serialPort1.StopBits = System.IO.Ports.StopBits.Two;
            this.serialPort1.WriteTimeout = 100;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 742);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.plg);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.online_plc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1364, 174);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button9);
            this.panel3.Controls.Add(this.print_fz);
            this.panel3.Controls.Add(this.excel_sql);
            this.panel3.Location = new System.Drawing.Point(865, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 63);
            this.panel3.TabIndex = 88;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(165, 34);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 94;
            this.button9.Text = "print";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // print_fz
            // 
            this.print_fz.Location = new System.Drawing.Point(84, 34);
            this.print_fz.Name = "print_fz";
            this.print_fz.Size = new System.Drawing.Size(75, 23);
            this.print_fz.TabIndex = 92;
            this.print_fz.Text = "仿真";
            this.print_fz.UseVisualStyleBackColor = true;
            this.print_fz.Click += new System.EventHandler(this.print_fz_Click);
            // 
            // excel_sql
            // 
            this.excel_sql.Location = new System.Drawing.Point(3, 7);
            this.excel_sql.Name = "excel_sql";
            this.excel_sql.Size = new System.Drawing.Size(109, 23);
            this.excel_sql.TabIndex = 91;
            this.excel_sql.Text = "excel to sql";
            this.excel_sql.UseVisualStyleBackColor = true;
            this.excel_sql.Visible = false;
            this.excel_sql.Click += new System.EventHandler(this.excel_sql_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 89;
            this.button1.Text = "开始扫描";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(137, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 91;
            this.button3.Text = "停止扫描";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // plg
            // 
            this.plg.Controls.Add(this.gd2);
            this.plg.Controls.Add(this.gd3);
            this.plg.Controls.Add(this.gd1);
            this.plg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plg.ForeColor = System.Drawing.SystemColors.WindowText;
            this.plg.Location = new System.Drawing.Point(0, 77);
            this.plg.Name = "plg";
            this.plg.Size = new System.Drawing.Size(1364, 97);
            this.plg.TabIndex = 77;
            // 
            // gd2
            // 
            this.gd2.Location = new System.Drawing.Point(704, 73);
            this.gd2.Name = "gd2";
            this.gd2.Size = new System.Drawing.Size(40, 21);
            this.gd2.TabIndex = 2;
            // 
            // gd3
            // 
            this.gd3.Location = new System.Drawing.Point(444, 75);
            this.gd3.Name = "gd3";
            this.gd3.Size = new System.Drawing.Size(40, 21);
            this.gd3.TabIndex = 1;
            // 
            // gd1
            // 
            this.gd1.Location = new System.Drawing.Point(933, 73);
            this.gd1.Name = "gd1";
            this.gd1.Size = new System.Drawing.Size(40, 21);
            this.gd1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 59;
            this.label2.Text = "二维码：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(68, 42);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(756, 21);
            this.textBox4.TabIndex = 57;
            // 
            // online_plc
            // 
            this.online_plc.Location = new System.Drawing.Point(437, 1);
            this.online_plc.Name = "online_plc";
            this.online_plc.Size = new System.Drawing.Size(75, 37);
            this.online_plc.TabIndex = 51;
            this.online_plc.Text = "连接Plc";
            this.online_plc.UseVisualStyleBackColor = true;
            this.online_plc.Click += new System.EventHandler(this.online_plc_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Controls.Add(this.typ2);
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 183);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1364, 556);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView1.Location = new System.Drawing.Point(1192, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(172, 556);
            this.dataGridView1.TabIndex = 75;
            this.dataGridView1.Visible = false;
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ctwo,
            this.d1,
            this.d2,
            this.d3,
            this.d4,
            this.d5,
            this.d6,
            this.d7,
            this.d8,
            this.d9});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(1381, 0);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(0, 556);
            this.dgv.TabIndex = 55;
            this.dgv.Visible = false;
            // 
            // ctwo
            // 
            this.ctwo.HeaderText = "二维码";
            this.ctwo.Name = "ctwo";
            // 
            // d1
            // 
            this.d1.HeaderText = "D1";
            this.d1.Name = "d1";
            this.d1.Width = 50;
            // 
            // d2
            // 
            this.d2.HeaderText = "d2";
            this.d2.Name = "d2";
            this.d2.Width = 50;
            // 
            // d3
            // 
            this.d3.HeaderText = "d3";
            this.d3.Name = "d3";
            this.d3.Width = 50;
            // 
            // d4
            // 
            this.d4.HeaderText = "d4";
            this.d4.Name = "d4";
            this.d4.Width = 50;
            // 
            // d5
            // 
            this.d5.HeaderText = "d5";
            this.d5.Name = "d5";
            this.d5.Width = 50;
            // 
            // d6
            // 
            this.d6.HeaderText = "d6";
            this.d6.Name = "d6";
            this.d6.Width = 50;
            // 
            // d7
            // 
            this.d7.HeaderText = "d7";
            this.d7.Name = "d7";
            this.d7.Width = 50;
            // 
            // d8
            // 
            this.d8.HeaderText = "d8";
            this.d8.Name = "d8";
            this.d8.Width = 50;
            // 
            // d9
            // 
            this.d9.HeaderText = "d9";
            this.d9.Name = "d9";
            this.d9.Width = 50;
            // 
            // typ2
            // 
            this.typ2.ColumnCount = 3;
            this.typ2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.typ2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.typ2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.typ2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.typ2.Controls.Add(this.p1, 0, 0);
            this.typ2.Controls.Add(this.p2, 1, 0);
            this.typ2.Controls.Add(this.p3, 2, 0);
            this.typ2.Dock = System.Windows.Forms.DockStyle.Left;
            this.typ2.Location = new System.Drawing.Point(341, 0);
            this.typ2.Name = "typ2";
            this.typ2.RowCount = 1;
            this.typ2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.typ2.Size = new System.Drawing.Size(1040, 556);
            this.typ2.TabIndex = 54;
            // 
            // p1
            // 
            this.p1.Controls.Add(this.label7);
            this.p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1.Location = new System.Drawing.Point(3, 3);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(340, 550);
            this.p1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "一单元";
            // 
            // p2
            // 
            this.p2.Controls.Add(this.label9);
            this.p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p2.Location = new System.Drawing.Point(349, 3);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(340, 550);
            this.p2.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(82, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "二单元";
            // 
            // p3
            // 
            this.p3.Controls.Add(this.label10);
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.Location = new System.Drawing.Point(695, 3);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(342, 550);
            this.p3.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(90, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "三单元";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(341, 556);
            this.listBox1.TabIndex = 53;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "main";
            this.Text = "PIVAS Worker V3.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Load += new System.EventHandler(this.main_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.plg.ResumeLayout(false);
            this.plg.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.typ2.ResumeLayout(false);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            this.p3.ResumeLayout(false);
            this.p3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button online_plc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel typ2;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctwo;
        private System.Windows.Forms.DataGridViewTextBoxColumn d1;
        private System.Windows.Forms.DataGridViewTextBoxColumn d2;
        private System.Windows.Forms.DataGridViewTextBoxColumn d3;
        private System.Windows.Forms.DataGridViewTextBoxColumn d4;
        private System.Windows.Forms.DataGridViewTextBoxColumn d5;
        private System.Windows.Forms.DataGridViewTextBoxColumn d6;
        private System.Windows.Forms.DataGridViewTextBoxColumn d7;
        private System.Windows.Forms.DataGridViewTextBoxColumn d8;
        private System.Windows.Forms.DataGridViewTextBoxColumn d9;
        private System.Windows.Forms.Panel plg;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox gd2;
        private System.Windows.Forms.TextBox gd3;
        private System.Windows.Forms.TextBox gd1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button print_fz;
        private System.Windows.Forms.Button excel_sql;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}

