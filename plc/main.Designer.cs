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
            this.typ2 = new System.Windows.Forms.TableLayoutPanel();
            this.p1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tx_plh1 = new System.Windows.Forms.TextBox();
            this.tx_rl1 = new System.Windows.Forms.TextBox();
            this.cb_pl1 = new System.Windows.Forms.CheckBox();
            this.cb_wc1 = new System.Windows.Forms.CheckBox();
            this.cb_cc1 = new System.Windows.Forms.CheckBox();
            this.ck_zcc1 = new System.Windows.Forms.CheckBox();
            this.p2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tx_plh2 = new System.Windows.Forms.TextBox();
            this.tx_rl2 = new System.Windows.Forms.TextBox();
            this.cb_pl2 = new System.Windows.Forms.CheckBox();
            this.cb_wc2 = new System.Windows.Forms.CheckBox();
            this.cb_cc2 = new System.Windows.Forms.CheckBox();
            this.cb_zcc2 = new System.Windows.Forms.CheckBox();
            this.p3 = new System.Windows.Forms.Panel();
            this.debug_t = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tx_plh3 = new System.Windows.Forms.TextBox();
            this.tx_rl3 = new System.Windows.Forms.TextBox();
            this.cb_pl3 = new System.Windows.Forms.CheckBox();
            this.cb_wc3 = new System.Windows.Forms.CheckBox();
            this.cb_cc3 = new System.Windows.Forms.CheckBox();
            this.ck_zcc3 = new System.Windows.Forms.CheckBox();
            this.plg = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.id3_t = new System.Windows.Forms.TextBox();
            this.id2_t = new System.Windows.Forms.TextBox();
            this.gd3 = new System.Windows.Forms.TextBox();
            this.gd2 = new System.Windows.Forms.TextBox();
            this.plcrun = new System.Windows.Forms.Button();
            this.gd1 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.print_b = new System.Windows.Forms.Button();
            this.excel_sql = new System.Windows.Forms.Button();
            this.idscan = new System.Windows.Forms.Button();
            this.stopscan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.id1_t = new System.Windows.Forms.TextBox();
            this.plctest = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.typ2.SuspendLayout();
            this.p1.SuspendLayout();
            this.p2.SuspendLayout();
            this.p3.SuspendLayout();
            this.plg.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.typ2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.plg, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 742);
            this.tableLayoutPanel1.TabIndex = 49;
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
            this.typ2.Location = new System.Drawing.Point(3, 103);
            this.typ2.Name = "typ2";
            this.typ2.RowCount = 1;
            this.typ2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.typ2.Size = new System.Drawing.Size(1364, 636);
            this.typ2.TabIndex = 55;
            // 
            // p1
            // 
            this.p1.Controls.Add(this.label3);
            this.p1.Controls.Add(this.label1);
            this.p1.Controls.Add(this.tx_plh1);
            this.p1.Controls.Add(this.tx_rl1);
            this.p1.Controls.Add(this.cb_pl1);
            this.p1.Controls.Add(this.cb_wc1);
            this.p1.Controls.Add(this.cb_cc1);
            this.p1.Controls.Add(this.ck_zcc1);
            this.p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1.Location = new System.Drawing.Point(3, 3);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(448, 630);
            this.p1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "排料品箱号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "入料箱号";
            // 
            // tx_plh1
            // 
            this.tx_plh1.Location = new System.Drawing.Point(279, 37);
            this.tx_plh1.Name = "tx_plh1";
            this.tx_plh1.Size = new System.Drawing.Size(100, 21);
            this.tx_plh1.TabIndex = 6;
            // 
            // tx_rl1
            // 
            this.tx_rl1.Location = new System.Drawing.Point(111, 37);
            this.tx_rl1.Name = "tx_rl1";
            this.tx_rl1.Size = new System.Drawing.Size(95, 21);
            this.tx_rl1.TabIndex = 5;
            // 
            // cb_pl1
            // 
            this.cb_pl1.AutoSize = true;
            this.cb_pl1.Location = new System.Drawing.Point(333, 15);
            this.cb_pl1.Name = "cb_pl1";
            this.cb_pl1.Size = new System.Drawing.Size(48, 16);
            this.cb_pl1.TabIndex = 4;
            this.cb_pl1.Text = "料排";
            this.cb_pl1.UseVisualStyleBackColor = true;
            // 
            // cb_wc1
            // 
            this.cb_wc1.AutoSize = true;
            this.cb_wc1.Location = new System.Drawing.Point(222, 15);
            this.cb_wc1.Name = "cb_wc1";
            this.cb_wc1.Size = new System.Drawing.Size(84, 16);
            this.cb_wc1.TabIndex = 3;
            this.cb_wc1.Text = "有分拣完成";
            this.cb_wc1.UseVisualStyleBackColor = true;
            // 
            // cb_cc1
            // 
            this.cb_cc1.AutoSize = true;
            this.cb_cc1.Location = new System.Drawing.Point(142, 15);
            this.cb_cc1.Name = "cb_cc1";
            this.cb_cc1.Size = new System.Drawing.Size(72, 16);
            this.cb_cc1.TabIndex = 2;
            this.cb_cc1.Text = "出仓信号";
            this.cb_cc1.UseVisualStyleBackColor = true;
            // 
            // ck_zcc1
            // 
            this.ck_zcc1.AutoSize = true;
            this.ck_zcc1.Location = new System.Drawing.Point(61, 15);
            this.ck_zcc1.Name = "ck_zcc1";
            this.ck_zcc1.Size = new System.Drawing.Size(72, 16);
            this.ck_zcc1.TabIndex = 1;
            this.ck_zcc1.Text = "可再出仓";
            this.ck_zcc1.UseVisualStyleBackColor = true;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.label4);
            this.p2.Controls.Add(this.label5);
            this.p2.Controls.Add(this.tx_plh2);
            this.p2.Controls.Add(this.tx_rl2);
            this.p2.Controls.Add(this.cb_pl2);
            this.p2.Controls.Add(this.cb_wc2);
            this.p2.Controls.Add(this.cb_cc2);
            this.p2.Controls.Add(this.cb_zcc2);
            this.p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p2.Location = new System.Drawing.Point(457, 3);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(448, 630);
            this.p2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "排料品箱号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "入料箱号";
            // 
            // tx_plh2
            // 
            this.tx_plh2.Location = new System.Drawing.Point(272, 37);
            this.tx_plh2.Name = "tx_plh2";
            this.tx_plh2.Size = new System.Drawing.Size(100, 21);
            this.tx_plh2.TabIndex = 14;
            // 
            // tx_rl2
            // 
            this.tx_rl2.Location = new System.Drawing.Point(104, 37);
            this.tx_rl2.Name = "tx_rl2";
            this.tx_rl2.Size = new System.Drawing.Size(95, 21);
            this.tx_rl2.TabIndex = 13;
            // 
            // cb_pl2
            // 
            this.cb_pl2.AutoSize = true;
            this.cb_pl2.Location = new System.Drawing.Point(326, 15);
            this.cb_pl2.Name = "cb_pl2";
            this.cb_pl2.Size = new System.Drawing.Size(48, 16);
            this.cb_pl2.TabIndex = 12;
            this.cb_pl2.Text = "料排";
            this.cb_pl2.UseVisualStyleBackColor = true;
            // 
            // cb_wc2
            // 
            this.cb_wc2.AutoSize = true;
            this.cb_wc2.Location = new System.Drawing.Point(215, 15);
            this.cb_wc2.Name = "cb_wc2";
            this.cb_wc2.Size = new System.Drawing.Size(84, 16);
            this.cb_wc2.TabIndex = 11;
            this.cb_wc2.Text = "有分拣完成";
            this.cb_wc2.UseVisualStyleBackColor = true;
            // 
            // cb_cc2
            // 
            this.cb_cc2.AutoSize = true;
            this.cb_cc2.Location = new System.Drawing.Point(135, 15);
            this.cb_cc2.Name = "cb_cc2";
            this.cb_cc2.Size = new System.Drawing.Size(72, 16);
            this.cb_cc2.TabIndex = 10;
            this.cb_cc2.Text = "出仓信号";
            this.cb_cc2.UseVisualStyleBackColor = true;
            // 
            // cb_zcc2
            // 
            this.cb_zcc2.AutoSize = true;
            this.cb_zcc2.Location = new System.Drawing.Point(54, 15);
            this.cb_zcc2.Name = "cb_zcc2";
            this.cb_zcc2.Size = new System.Drawing.Size(72, 16);
            this.cb_zcc2.TabIndex = 9;
            this.cb_zcc2.Text = "可再出仓";
            this.cb_zcc2.UseVisualStyleBackColor = true;
            // 
            // p3
            // 
            this.p3.Controls.Add(this.debug_t);
            this.p3.Controls.Add(this.label6);
            this.p3.Controls.Add(this.label7);
            this.p3.Controls.Add(this.tx_plh3);
            this.p3.Controls.Add(this.tx_rl3);
            this.p3.Controls.Add(this.cb_pl3);
            this.p3.Controls.Add(this.cb_wc3);
            this.p3.Controls.Add(this.cb_cc3);
            this.p3.Controls.Add(this.ck_zcc3);
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.Location = new System.Drawing.Point(911, 3);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(450, 630);
            this.p3.TabIndex = 2;
            // 
            // debug_t
            // 
            this.debug_t.Location = new System.Drawing.Point(0, 426);
            this.debug_t.Multiline = true;
            this.debug_t.Name = "debug_t";
            this.debug_t.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.debug_t.Size = new System.Drawing.Size(623, 143);
            this.debug_t.TabIndex = 94;
            this.debug_t.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "排料品箱号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "入料箱号";
            // 
            // tx_plh3
            // 
            this.tx_plh3.Location = new System.Drawing.Point(280, 37);
            this.tx_plh3.Name = "tx_plh3";
            this.tx_plh3.Size = new System.Drawing.Size(100, 21);
            this.tx_plh3.TabIndex = 14;
            // 
            // tx_rl3
            // 
            this.tx_rl3.Location = new System.Drawing.Point(112, 37);
            this.tx_rl3.Name = "tx_rl3";
            this.tx_rl3.Size = new System.Drawing.Size(95, 21);
            this.tx_rl3.TabIndex = 13;
            // 
            // cb_pl3
            // 
            this.cb_pl3.AutoSize = true;
            this.cb_pl3.Location = new System.Drawing.Point(334, 15);
            this.cb_pl3.Name = "cb_pl3";
            this.cb_pl3.Size = new System.Drawing.Size(48, 16);
            this.cb_pl3.TabIndex = 12;
            this.cb_pl3.Text = "料排";
            this.cb_pl3.UseVisualStyleBackColor = true;
            // 
            // cb_wc3
            // 
            this.cb_wc3.AutoSize = true;
            this.cb_wc3.Location = new System.Drawing.Point(223, 15);
            this.cb_wc3.Name = "cb_wc3";
            this.cb_wc3.Size = new System.Drawing.Size(84, 16);
            this.cb_wc3.TabIndex = 11;
            this.cb_wc3.Text = "有分拣完成";
            this.cb_wc3.UseVisualStyleBackColor = true;
            // 
            // cb_cc3
            // 
            this.cb_cc3.AutoSize = true;
            this.cb_cc3.Location = new System.Drawing.Point(143, 15);
            this.cb_cc3.Name = "cb_cc3";
            this.cb_cc3.Size = new System.Drawing.Size(72, 16);
            this.cb_cc3.TabIndex = 10;
            this.cb_cc3.Text = "出仓信号";
            this.cb_cc3.UseVisualStyleBackColor = true;
            // 
            // ck_zcc3
            // 
            this.ck_zcc3.AutoSize = true;
            this.ck_zcc3.Location = new System.Drawing.Point(62, 15);
            this.ck_zcc3.Name = "ck_zcc3";
            this.ck_zcc3.Size = new System.Drawing.Size(72, 16);
            this.ck_zcc3.TabIndex = 9;
            this.ck_zcc3.Text = "可再出仓";
            this.ck_zcc3.UseVisualStyleBackColor = true;
            // 
            // plg
            // 
            this.plg.Controls.Add(this.button2);
            this.plg.Controls.Add(this.button1);
            this.plg.Controls.Add(this.textBox1);
            this.plg.Controls.Add(this.id3_t);
            this.plg.Controls.Add(this.id2_t);
            this.plg.Controls.Add(this.gd3);
            this.plg.Controls.Add(this.gd2);
            this.plg.Controls.Add(this.plcrun);
            this.plg.Controls.Add(this.gd1);
            this.plg.Controls.Add(this.panel3);
            this.plg.Controls.Add(this.idscan);
            this.plg.Controls.Add(this.stopscan);
            this.plg.Controls.Add(this.label2);
            this.plg.Controls.Add(this.id1_t);
            this.plg.Controls.Add(this.plctest);
            this.plg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plg.Location = new System.Drawing.Point(3, 3);
            this.plg.Name = "plg";
            this.plg.Size = new System.Drawing.Size(1364, 94);
            this.plg.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1156, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 98;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 97;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1116, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(239, 21);
            this.textBox1.TabIndex = 96;
            // 
            // id3_t
            // 
            this.id3_t.Location = new System.Drawing.Point(602, 44);
            this.id3_t.Name = "id3_t";
            this.id3_t.Size = new System.Drawing.Size(257, 21);
            this.id3_t.TabIndex = 95;
            // 
            // id2_t
            // 
            this.id2_t.Location = new System.Drawing.Point(336, 43);
            this.id2_t.Name = "id2_t";
            this.id2_t.Size = new System.Drawing.Size(250, 21);
            this.id2_t.TabIndex = 94;
            // 
            // gd3
            // 
            this.gd3.Location = new System.Drawing.Point(616, 13);
            this.gd3.Name = "gd3";
            this.gd3.Size = new System.Drawing.Size(40, 21);
            this.gd3.TabIndex = 1;
            this.gd3.Visible = false;
            // 
            // gd2
            // 
            this.gd2.Location = new System.Drawing.Point(698, 15);
            this.gd2.Name = "gd2";
            this.gd2.Size = new System.Drawing.Size(40, 21);
            this.gd2.TabIndex = 2;
            this.gd2.Visible = false;
            // 
            // plcrun
            // 
            this.plcrun.Location = new System.Drawing.Point(484, 13);
            this.plcrun.Name = "plcrun";
            this.plcrun.Size = new System.Drawing.Size(75, 23);
            this.plcrun.TabIndex = 92;
            this.plcrun.Text = "plc";
            this.plcrun.UseVisualStyleBackColor = true;
            this.plcrun.Click += new System.EventHandler(this.plcrun_Click);
            // 
            // gd1
            // 
            this.gd1.Location = new System.Drawing.Point(770, 16);
            this.gd1.Name = "gd1";
            this.gd1.Size = new System.Drawing.Size(40, 21);
            this.gd1.TabIndex = 0;
            this.gd1.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.print_b);
            this.panel3.Controls.Add(this.excel_sql);
            this.panel3.Location = new System.Drawing.Point(865, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 63);
            this.panel3.TabIndex = 88;
            // 
            // print_b
            // 
            this.print_b.Location = new System.Drawing.Point(165, 34);
            this.print_b.Name = "print_b";
            this.print_b.Size = new System.Drawing.Size(75, 23);
            this.print_b.TabIndex = 94;
            this.print_b.Text = "print";
            this.print_b.UseVisualStyleBackColor = true;
            this.print_b.Click += new System.EventHandler(this.print_b_Click);
            // 
            // excel_sql
            // 
            this.excel_sql.Location = new System.Drawing.Point(3, 7);
            this.excel_sql.Name = "excel_sql";
            this.excel_sql.Size = new System.Drawing.Size(109, 23);
            this.excel_sql.TabIndex = 91;
            this.excel_sql.Text = "打印";
            this.excel_sql.UseVisualStyleBackColor = true;
            this.excel_sql.Click += new System.EventHandler(this.excel_sql_Click);
            // 
            // idscan
            // 
            this.idscan.Location = new System.Drawing.Point(47, 9);
            this.idscan.Name = "idscan";
            this.idscan.Size = new System.Drawing.Size(75, 23);
            this.idscan.TabIndex = 89;
            this.idscan.Text = "开始扫描";
            this.idscan.UseVisualStyleBackColor = true;
            this.idscan.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopscan
            // 
            this.stopscan.Location = new System.Drawing.Point(137, 8);
            this.stopscan.Name = "stopscan";
            this.stopscan.Size = new System.Drawing.Size(75, 23);
            this.stopscan.TabIndex = 91;
            this.stopscan.Text = "停止扫描";
            this.stopscan.UseVisualStyleBackColor = true;
            this.stopscan.Click += new System.EventHandler(this.stopscan_Click);
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
            // id1_t
            // 
            this.id1_t.Location = new System.Drawing.Point(68, 43);
            this.id1_t.Name = "id1_t";
            this.id1_t.Size = new System.Drawing.Size(260, 21);
            this.id1_t.TabIndex = 57;
            // 
            // plctest
            // 
            this.plctest.Location = new System.Drawing.Point(376, 11);
            this.plctest.Name = "plctest";
            this.plctest.Size = new System.Drawing.Size(75, 23);
            this.plctest.TabIndex = 51;
            this.plctest.Text = "plc test";
            this.plctest.UseVisualStyleBackColor = true;
            this.plctest.Click += new System.EventHandler(this.online_plc_Click);
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
            this.typ2.ResumeLayout(false);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            this.p3.ResumeLayout(false);
            this.p3.PerformLayout();
            this.plg.ResumeLayout(false);
            this.plg.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel plg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox id1_t;
        private System.Windows.Forms.Button plctest;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox gd2;
        private System.Windows.Forms.TextBox gd3;
        private System.Windows.Forms.TextBox gd1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button print_b;
        private System.Windows.Forms.Button excel_sql;
        private System.Windows.Forms.Button idscan;
        private System.Windows.Forms.Button stopscan;
        private System.Windows.Forms.Button plcrun;
        private System.Windows.Forms.TableLayoutPanel typ2;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.CheckBox ck_zcc1;
        private System.Windows.Forms.TextBox id3_t;
        private System.Windows.Forms.TextBox id2_t;
        private System.Windows.Forms.CheckBox cb_wc1;
        private System.Windows.Forms.CheckBox cb_cc1;
        private System.Windows.Forms.TextBox tx_rl1;
        private System.Windows.Forms.CheckBox cb_pl1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tx_plh1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tx_plh2;
        private System.Windows.Forms.TextBox tx_rl2;
        private System.Windows.Forms.CheckBox cb_pl2;
        private System.Windows.Forms.CheckBox cb_wc2;
        private System.Windows.Forms.CheckBox cb_cc2;
        private System.Windows.Forms.CheckBox cb_zcc2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tx_plh3;
        private System.Windows.Forms.TextBox tx_rl3;
        private System.Windows.Forms.CheckBox cb_pl3;
        private System.Windows.Forms.CheckBox cb_wc3;
        private System.Windows.Forms.CheckBox cb_cc3;
        private System.Windows.Forms.CheckBox ck_zcc3;
        private System.Windows.Forms.TextBox debug_t;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

