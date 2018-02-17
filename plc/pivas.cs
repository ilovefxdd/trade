using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Drawing.Printing;
using PIVASWork;
//using System.Data.OracleClient;
using System.Data.SqlClient;
using com.google.zxing;
using COMMON = com.google.zxing.common;
using System.Data.OleDb;
using System.IO;

namespace PIVASWork
{
    public partial class main : Form
    {
        public enum RunState
        {
            running,
            pause,
            stop
        }

        private int i = 0;

        RunState state = RunState.stop;
     
        public int cou;
      
        private UdpClient udpcSend;
        private string plcip = "192.168.10.1";
        
        private int outport = 9600;
      
        public delegate void ProcessDelegate();
        Thread fzthread;

        #region
        private string w_d10="80000200010000090000010282000a00000b";
        int[] s_d10=new int[13];
        private string w_d301 = "80000200010000090000010282012d00005";
        int[] s_d301=new int[5];

        private string r_d205 = "8000020001000009000001018200CD000003";
        int[] s_d205=new int[3];

        private string r_d310 = "80000200010000090000010182013600002D";
        int[] s_d310=new int[45];
        private string w_d600 = "800002000100000900000102820258000003";
        int[] s_d600 = new int[3];

        private string w_w40_0 = "800002000100000900000102310028000007";
        byte[] s_40 = new byte[7];

        private string r_w100_15 = "8000020001000009000001013100640F0001";
        byte[] s_100 = new byte[1];

        private string r_w11_0 = "80000200010000090000010131000B000003";
        byte[] s_11 = new byte[3];

        private string r_w120_00 = "80000200010000090000010131007800002D";
        byte[] s_120 = new byte[3];

        private string w_d610 = "80000200010000090000010282026200002D";
        byte[] s_610 = new byte[45];

        private string w_w50_10 = "8000020001000009000001023100320A0002";
        byte[] s_50_10 = new byte[2];

        private string w_w50_0 = "800002000100000900000102310032000003";
        byte[] s_50_0 = new byte[3];


        private string r_w15_0 = "80000200010000090000010131000f000001";
        byte[] s_15_0 = new byte[1];

        private string r_w50_5 = "80000200010000090000010131003205000A";
        byte[] s_50_5 = new byte[10];
        #endregion
        private void online_plc_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < 13; i++)
            {
                s_d10[i] = i;
               
            }
            
            for (int j = 0; j < 1; j++)
            {

                listBox1.Items.Add(DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + ByteToString(SendMessage(ByteToString(prostr(w_d10, s_d10)))));
                
            }
        }

        private byte[] prostr(string w, int[] ia)
        {
            byte[] tmp = stringtobyte(w);
            
            int j=tmp.Length;
            int l=j+ia.Length*2;
            byte[] b=new byte[l];
            for (int i = 0; i < j; i++)
            {
                b[i] = tmp[i];
            }
            byte[] bi=inttoby(ia);
            for (int i = 0; i < ia.Length*2; i++) 
            {
                b[i+j] = bi[i];
            }
            return b;
        }
        act_box pivas = new act_box();
        plcdata plc = new plcdata();
        public online ol;
        public main()
        {

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ol = new online();
          
        }

      
        static string[] SplitByLen(string tempstr, int length)
        {
            int len = tempstr.Length / length;
            string[] returnstr = new string[length];
            for (int i = 0; i < length; i++)
            {
                returnstr[i] = tempstr.Substring(i * 4, 4);
            }
       
            return returnstr;
        }

        static int[] makeint(string[] sour)
        {
            int len;
            len = 0;
            foreach (string st in sour)
            {
                len++;
            }
            int[] des = new int[len];
            for (int i = 0; i < len; i++)
            {
                des[i] = Int32.Parse(sour[i]);
            }
            return des;
        }
    
        
        Thread thrRecv;

        public static byte[] wordtobyte(int i)
        {
            byte[] sb;
            string tmp = i.ToString("X4");
            sb = Enumerable.Range(0, tmp.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(tmp.Substring(x, 2), 16))
                 .ToArray();

            return sb;
        }

    public static byte[] stringtobyte(string input)
    {
        return Enumerable.Range(0, input.Length)
             .Where(x => x % 2 == 0)
             .Select(x => Convert.ToByte(input.Substring(x, 2), 16))
             .ToArray();
    }
        private byte[] inttoby(int[] dat)
        {

            byte[] bytedat = new byte[dat.Length * 2];
            byte[] tmp = new byte[2];
            for (int i = 0; i < dat.Length; i++)
            {
                tmp = wordtobyte(dat[i]);
                bytedat[i * 2] = tmp[0];
                bytedat[i * 2 + 1] = tmp[1];

            }
            return bytedat;
        }
        public static string ByteToString(byte[] bytes)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (byte bt in bytes)
            {
                strBuilder.AppendFormat("{0:X2}", bt);
            }
            return strBuilder.ToString();
        }

        delegate void ShowMessageDelegate(TextBox txtbox, string message);

        private void ShowMessage(TextBox txtbox, string message)
        {
            if (txtbox.InvokeRequired)
            {
                ShowMessageDelegate showMessageDelegate = ShowMessage;
                txtbox.Invoke(showMessageDelegate, new object[] { txtbox, message });
            }
            else
            {
              
                txtbox.Text += message + "\r\n";
            }
        }
        private byte[] SendMessage(object obj)
        {
            char[] SendBuffer;

            string inStr = (string)obj;
            SendBuffer = inStr.ToCharArray();
            byte[] sendbyte = Enumerable.Range(0, inStr.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(inStr.Substring(x, 2), 16))
                 .ToArray();
            // byte[] sendbyte = Encoding.ASCII.GetBytes(SendBuffer);   

            IPEndPoint remoteIpep = new IPEndPoint(
                IPAddress.Parse(plcip), outport); // 发送到的IP地址和端口号
            IPEndPoint localIpep = new IPEndPoint(IPAddress.Any, 9600); // 本机IP和监听端口号
            udpcSend = new UdpClient(localIpep);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            Thread.Sleep(10);
           
            udpcSend.Close();
            udpcSend = null;
            
            return bytRecv;
        }
        
        private string getwad(string cad)
        {
            string st;
            SqlConnection conn = new SqlConnection(sqlc.localco);
            conn.Open();
            string sql1 = "select barcode,ward_code,	ward_name,dosage_units,dosage from  Barcode where test_date>@dt and barcode=@code  ";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlParameter dt = new SqlParameter("@dt", "2015-03-07");
            SqlParameter code = new SqlParameter("@code", cad);
            cmd.Parameters.Add(dt);
            cmd.Parameters.Add(code);
            SqlDataReader rd = cmd.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            string ward_ = "";
            string d = "";
            string drug_spec = "";

            while (rd.Read())
            {
                ward_ = rd[1].ToString();
                ward_ = ward_.Trim();
                d = rd[4].ToString();
                float f = Convert.ToSingle(d);
                int i = Convert.ToInt32(f);
                drug_spec = i.ToString();
            }
            st = ward_ + "|" + d + "|" + drug_spec;
            return st;
        }
      

        
        Thread sr;
       
        
        // 向TextBox中添加文本
        bool IsUdpcRecvStart = false;

        private void main_Load(object sender, EventArgs e) //初始界面
        {
            TextBox tb, tb1, tb2;
            Label lb;
            PictureBox pb;
            for (int c = 10; c < 25; c++)
            {
                tb = new TextBox();
                tb.Location = new Point(30 + 50 * (c - 10), 132);
                tb.Width = 40;
                tb.Height = 21;
                tb.Parent = panel1;
                tb.Name = "ctb" + c;
                //this.Controls.Add(tb);
            }
            //    初始化动态输入表
            for (int i = 10; i < 25; i++)
            {
                tb = new TextBox();
                tb.Location = new Point(102, 51 + 32 * (i - 10));
                tb.Width = 40;
                tb.Height = 21;
                tb.Parent = p1;
                tb.Name = "ptd1" + i;

                lb = new Label();
                lb.Location = new Point(23, 51 + 32 * (i - 10));
                lb.Name = "lb1" + i;
                lb.Width = 40;
                lb.Height = 21;
                lb.Parent = p1;
                lb.Text = i.ToString() + "号";

                pb = new PictureBox();
                pb.Location = new Point(66, 51 + 32 * (i - 10));
                pb.Width = 30;
                pb.Height = 30;
                pb.Parent = p1;
                pb.Name = "pb1" + i;
                pb.Image = PIVASworker.Properties.Resources.low;
                //  this.Controls.Add(pb);

                tb1 = new TextBox();
                tb1.Location = new Point(148, 51 + 32 * (i - 10));
                tb1.Width = 60;
                tb1.Height = 21;
                tb1.Parent = p1;
                tb1.Name = "ptd2" + i;
                //  this.Controls.Add(tb1);
                tb2 = new TextBox();
                tb2.Location = new Point(216, 51 + 32 * (i - 10));
                tb2.Width = 40;
                tb2.Height = 21;
                tb2.Parent = p1;
                tb2.Name = "ptd3" + i;
                //   this.Controls.Add(tb2);
            }

            for (int i = 10; i < 25; i++)
            {
                tb = new TextBox();
                tb.Location = new Point(102, 51 + 32 * (i - 10));
                tb.Width = 40;
                tb.Height = 21;
                tb.Parent = p2;
                tb.Name = "pte1" + i;
                //  this.Controls.Add(tb);

                lb = new Label();
                lb.Location = new Point(23, 51 + 32 * (i - 10));
                lb.Name = "lb1" + i;
                lb.Width = 40;
                lb.Height = 21;
                lb.Parent = p2;
                lb.Text = i.ToString() + "号";

                pb = new PictureBox();
                pb.Location = new Point(66, 51 + 32 * (i - 10));
                pb.Width = 30;
                pb.Height = 30;
                pb.Parent = p2;
                pb.Name = "pb1" + i;
                pb.Image = PIVASworker.Properties.Resources.low;
                //   this.Controls.Add(pb);

                tb1 = new TextBox();
                tb1.Location = new Point(148, 51 + 32 * (i - 10));
                tb1.Width = 60;
                tb1.Height = 21;
                tb1.Parent = p2;
                tb1.Name = "pte2" + i;
                //  this.Controls.Add(tb1);

                tb2 = new TextBox();
                tb2.Location = new Point(216, 51 + 32 * (i - 10));
                tb2.Width = 40;
                tb2.Height = 21;
                tb2.Parent = p2;
                tb2.Name = "pte3" + i;
                //  this.Controls.Add(tb2);
            }


            for (int i = 10; i < 25; i++)
            {
                tb = new TextBox();
                tb.Location = new Point(102, 51 + 32 * (i - 10));
                tb.Width = 40;
                tb.Height = 21;
                tb.Parent = p3;
                tb.Name = "ptf1" + i;
                //   this.Controls.Add(tb);

                lb = new Label();
                lb.Location = new Point(23, 51 + 32 * (i - 10));
                lb.Name = "lb1" + i;
                lb.Width = 40;
                lb.Height = 21;
                lb.Parent = p3;
                lb.Text = i.ToString() + "号";

                pb = new PictureBox();
                pb.Location = new Point(66, 51 + 32 * (i - 10));
                pb.Width = 30;
                pb.Height = 30;
                pb.Parent = p3;
                pb.Name = "pb1" + i;
                pb.Image = PIVASworker.Properties.Resources.low;
                //  this.Controls.Add(pb);

                tb1 = new TextBox();
                tb1.Location = new Point(148, 51 + 32 * (i - 10));
                tb1.Width = 60;
                tb1.Height = 21;
                tb1.Parent = p3;
                tb1.Name = "ptf2" + i;
                //   this.Controls.Add(tb1);

                tb2 = new TextBox();
                tb2.Location = new Point(216, 51 + 32 * (i - 10));
                tb2.Width = 40;
                tb2.Height = 21;
                tb2.Parent = p3;
                tb2.Name = "ptf3" + i;
                //   this.Controls.Add(tb2);
            }
      
        } 

        private bool domain(string tmpid)// 处理
        {
            string[] sendmess = new string[2] { "", "" };
            char[] separator = { '|' };
            String[] tmp = new String[2];
            tmp = tmpid.Split(separator);

            sendmess = bq_from_sql(tmp[0]);
            box b = pivas.getbox(tmp[0]);

            

            if (b.finished) { }
            if (b.full) { }

            return true;

        }
        private string[] bq_from_sql(string bar)//select ward_code,dosage from  Barcode where  barcode=@bar  
        {
            string[] bq = new string[2] { "", "" };
            SqlConnection conn = new SqlConnection(sqlc.localco);
            conn.Open();
            string sql1 = "select ward_code,dosage from  Barcode where  barcode=@bar  ";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlParameter dt = new SqlParameter("@bar", bar);
            cmd.Parameters.Add(dt);
            SqlDataReader rd = cmd.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            string ward_;
            string drug_spec;

            while (rd.Read())
            {
                ward_ = rd[0].ToString();
                ward_ = ward_.Trim();
                string d = rd[1].ToString();
                float f = Convert.ToSingle(d);
                int i = Convert.ToInt32(f);
                drug_spec = i.ToString();
                bq[0] = ward_;
                bq[1] = d;

                ProcessDelegate showtplg = delegate()
                {
                    pivas.Insertward(ward_, drug_spec);
                    ol.addsd(rd[0].ToString(), pivas.getbox(ward_).dy, ol.gds[0]);
                    gd1.Text = ol.gds[1].ToString();
                    gd2.Text = ol.gds[2].ToString();
                    gd3.Text = ol.gds[3].ToString();
                    gd1.Text = ol.gds[4].ToString();

                    ol.drawsd(plg);
                    showboxs(ward_);
                };
                plg.Invoke(showtplg);
                Thread.Sleep(10);
            }
            return bq;
        }

        private string[] getbq(string num)//SELECT     ward_code, count(drug_spec)FROM     Barcode group by  ward_code
        {
            string[] tp = new string[3];
            try
            {
                SqlConnection conn = new SqlConnection(sqlc.localco);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT     ward_code, count(drug_spec)FROM     Barcode group by  ward_code ";
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    tp[0] = sdr[0].ToString();
                    tp[1] = sdr[1].ToString();
                    tp[2] = sdr[2].ToString();
                }
                else
                {
                    //  MessageBox.Show("密码或用户名不正确！");
                }
                conn.Close();    //关闭数据库的连接
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return tp;
        }

       
        private void GetResultIntoImage(ref Image temp)
        {

            Graphics g = Graphics.FromImage(temp);

            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            //填充数据到图片模板（位置要在制作图片模板的时候度量好）
            g.DrawImage(temp, 0, 0, temp.Width, temp.Height);
            g.Dispose();
        } //读取图片模板


        private void button2_Click(object sender, EventArgs e) //画水袋测试
        {
           
        }

       

  

        public DataTable ExcelToDataTable(string strExcelFileName, string strSheetName)
        {
            //源的定义
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + strExcelFileName + "; Extended properties=EXCEL 12.0";
            OleDbCommand command;
            OleDbConnection connection;
            OleDbDataAdapter adapter;
            DataSet dataSet;
            DataTable dataTable;
            connection = new OleDbConnection();//1
            connection.ConnectionString = conStr;//2
            connection.Open();//3
            command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "select * from [sheet1$]";
            dataSet = new DataSet();
            adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, "sheet1");
            dataTable = dataSet.Tables["sheet1"];
            dataGridView1.DataSource = dataTable;//dataGridView1中显示
            return dataSet.Tables[strSheetName];
        }

        private void excel_sql_Click(object sender, EventArgs e)
        {
            int shetlines = 2;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            { return; }
            DataTable myT = ExcelToDataTable(openFileDialog1.FileName, "sheet1");
            String mystr = myT.Rows[shetlines][0].ToString();
            SqlConnection conn = new SqlConnection(sqlc.localco);
            conn.Open();
            while (shetlines < 1224)
            {
                //  Print
                /*   printstr = mystr + pst;
                PrintDocument pd = new PrintDocument();
                //设置边距
                Margins margin = new Margins(120, 20, 20, 20);
                pd.DefaultPageSettings.Margins = margin;
                ////纸张设置默认
                //PaperSize pageSize = new PaperSize("First custom size", 800, 600);
                //pd.DefaultPageSettings.PaperSize = pageSize;
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();

                //PrintDocment 默认的 PrintController 是 PrintControllerWithStatusDialog。显示提示窗口


                //打印事件设置
               pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

                try
                {

                    pd.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
                }
                pd.Dispose();
                textBox1.Text = bqstr;
                textBox2.Text = mystr;*/

                string sql1 = "INSERT INTO Barcode(test_date,barcode,batch,ward_code,	ward_name,drug_spec,dosage,dosage_units)  VALUES ";
                string sql2 = "(@test_date,@barcode,@batch,@ward_code,@ward_name,@drug_spec,@dosage,@dosage_units)";
                SqlCommand cmd = new SqlCommand(sql1 + sql2, conn);
                SqlParameter spm_0 = new SqlParameter("@test_date", myT.Rows[shetlines][0].ToString());
                cmd.Parameters.Add(spm_0);
                SqlParameter spm_1 = new SqlParameter("@barcode", myT.Rows[shetlines][1].ToString());
                cmd.Parameters.Add(spm_1);
                SqlParameter spm_2 = new SqlParameter("@batch", myT.Rows[shetlines][2].ToString());
                cmd.Parameters.Add(spm_2);
                SqlParameter spm_3 = new SqlParameter("@ward_code", myT.Rows[shetlines][3].ToString());
                cmd.Parameters.Add(spm_3);
                SqlParameter spm_4 = new SqlParameter("@ward_name", myT.Rows[shetlines][4].ToString());
                cmd.Parameters.Add(spm_4);
                SqlParameter spm_5 = new SqlParameter("@drug_spec", myT.Rows[shetlines][5].ToString());
                cmd.Parameters.Add(spm_5);
                SqlParameter spm_6 = new SqlParameter("@dosage", myT.Rows[shetlines][6].ToString());
                cmd.Parameters.Add(spm_6);
                SqlParameter spm_7 = new SqlParameter("@dosage_units", myT.Rows[shetlines][7].ToString());
                cmd.Parameters.Add(spm_7);
                //   另一种参数方法
                /*     SqlParameter[] paras = new SqlParameter[] 
                    { 
                        new SqlParameter("@br_name", br_xm.Items[i].ToString().Trim()),
                        new SqlParameter("@br_xb", br_xb.Items[i].ToString().Trim()),
                        new SqlParameter("@br_phone",br_phone.Text.Trim()),
                        new SqlParameter("@br_sfz",br_sfz.Text.Trim()),
                        new SqlParameter("@br_dz",br_dz.Text.Trim()),
                        new SqlParameter("@br_zy",getypzyh(this)),
                        new SqlParameter("@br_sbh",br_sbh.Text.Trim())
                    };
                cmd.Parameters.AddRange(paras);*/

                cmd.ExecuteNonQuery();
                shetlines++;
            }
        }

        private void Sort(int[] list, int low, int high)
        {
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = 0; j < list.Length - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
        //   病区 仓位显示数量
        private void showboxs(string ward_)
        {
            int dy = 0;
            int box = 0;
            string pt = "pt";
            box tb = pivas.getbox(ward_);
            dy = tb.dy;
            box = tb.boxid;
            Panel tp = p1;
            switch (dy)
            {
                case 0: tp = p1;
                    pt = "ptd";
                    break;
                case 1: tp = p2;
                    pt = "pte";
                    break;
                case 2: tp = p3;
                    pt = "ptf";
                    break;
            
            }
            foreach (Control c in tp.Controls)
            {
                if (c is TextBox && ((TextBox)c).Name == pt + "1" + (box + 10))
                {
                    ((TextBox)c).Text = tb.total.ToString();
                    ((TextBox)c).Refresh();
                    //          MessageBox.Show(((TextBox)c).Text);
                    break;
                }
            }
            foreach (Control c in tp.Controls)
            {
                if (c is TextBox && ((TextBox)c).Name == pt + "2" + (box + 10))
                {
                    ((TextBox)c).Text = tb.bq;
                    ((TextBox)c).Refresh();
                    break;
                }
            }
            SqlConnection conn;
            conn = new SqlConnection(sqlc.localco);
            conn.Open();
            string sql1;
            SqlParameter spm_0;
            SqlCommand cmd;
            SqlDataReader reader;
            sql1 = "select  count(dosage) from  Barcode where ward_code=@wcd";
            cmd = new SqlCommand(sql1, conn);
            spm_0 = new SqlParameter("@wcd", tb.bq);
            cmd.Parameters.Add(spm_0);
            try
            {
                conn.Close();
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (Control c in tp.Controls)
                    {
                        if (c is TextBox && ((TextBox)c).Name == pt + "3" + (box + 10))
                        {
                            ((TextBox)c).Text = String.Format("{0}", reader[0]);
                            ((TextBox)c).Refresh();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void print_fz_Click(object sender, EventArgs e)
        {
            fzthread = new Thread(new ThreadStart(onlin));
            fzthread.IsBackground = true;
            fzthread.Start();
        }
        private string searchward(string id)
        {
            string ward_id="";
            return ward_id;
        }
        private void onlin()//流水线
        {
            while (true)
            {
                string strDate = DateTime.Now.ToString("HH:mm:ss tt");
               // searchward()
            }
           
        }
        
       #region
        /*
       
            SqlConnection conn = new SqlConnection(sqlc.localco);
            conn.Open();
            string sql1 = "select barcode,ward_code,	ward_name,dosage_units,dosage from  Barcode where test_date>@dt   ";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlParameter dt = new SqlParameter("@dt", "2015-03-07");
            cmd.Parameters.Add(dt);
            SqlDataReader rd = cmd.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            string ward_;
            string drug_spec;

            while (rd.Read())
            {
                ward_ = rd[1].ToString();
                ward_ = ward_.Trim();
                string d = rd[4].ToString();
                float f = Convert.ToSingle(d);
                int i = Convert.ToInt32(f);
                drug_spec = i.ToString();
                while (state == RunState.running)
                {
                }
               ProcessDelegate showtplg = delegate()
                {
                    pivas.Insertward(ward_, drug_spec);
                    ol.addsd(rd[0].ToString(), pivas.getbox(ward_).dy, ol.gds[0]);
                    //   pivas.setblh(ol.gds);
                    gd1.Text = ol.gds[1].ToString();
                    gd2.Text = ol.gds[2].ToString();
                    gd3.Text = ol.gds[3].ToString();
                    gd1.Text = ol.gds[4].ToString();
                    cou++;
                    ret=plc.getplc();
               
                      plc.SendMessage();
              //       textBox4.Text = ByteToString(ret);
                     int[] io = new[] { (int)cou };
               //      ret = plc.setplc(io);
                //     textBox1.Text = ByteToString(ret);
                    ol.drawsd(plg);
                    showboxs(ward_);
                };
               plg.Invoke(showtplg);
            
            }
         */
        #endregion
        UdpClient udpstr;
        //  test udp receive
                                                 

        //  程序退出时结束线程
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        public void writeToFile(COMMON.ByteMatrix matrix, System.Drawing.Imaging.ImageFormat format, string file)//保存二维
        {
            Bitmap bmap = toBitmap(matrix);
            //   bmap.Save(file, format);
            //    pictureBox1.Image = bmap;
        }

        public Bitmap toBitmap(COMMON.ByteMatrix matrix)//画二维
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }
            return bmap;
        }

      

        private void pd_PrintPage(object sender, PrintPageEventArgs e)//打印处理
        {

            //读取图片模板
            string printstr = "";
            string excelstr = "";
            string bqstr = "";
            COMMON.ByteMatrix byteMatrix = new MultiFormatWriter().encode(printstr, BarcodeFormat.QR_CODE, 100, 100);
            Bitmap bmap = toBitmap(byteMatrix);
            Image temp = bmap;
            GetResultIntoImage(ref temp);
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = temp.Width;
            int height = temp.Height;

            Rectangle destRect = new Rectangle(x, y, width, height);
            e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
            SolidBrush Brush = new SolidBrush(Color.Black);
            Font drawFont = new Font("Times New Roman", 9);
            PointF drawPoint = new PointF(x + 10, y + height - 10);

            e.Graphics.DrawString(excelstr, drawFont, Brush, drawPoint);
            drawFont = new Font("Times New Roman", 8);
            drawPoint = new PointF(60, y + height / 2);
            e.Graphics.DrawString("ml :" + bqstr, drawFont, Brush, drawPoint);
        }

        private void button9_Click(object sender, EventArgs e)//打印标签
        {

            int shetlines = 39;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            { return; }
            DataTable myT = ExcelToDataTable(openFileDialog1.FileName, "sheet1");
            String mystr = myT.Rows[shetlines][0].ToString();

            while (shetlines < 80)
            {
                mystr = myT.Rows[shetlines][0].ToString();
                //     excelstr = mystr;
                //    bqstr = myT.Rows[shetlines][2].ToString();
                //     printstr = mystr + pst; 
                PrintDocument pd = new PrintDocument();
                //设置边距
                Margins margin = new Margins(120, 20, 20, 20);
                pd.DefaultPageSettings.Margins = margin;
                ////纸张设置默认
                //PaperSize pageSize = new PaperSize("First custom size", 800, 600);
                //pd.DefaultPageSettings.PaperSize = pageSize;
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();

                //PrintDocment 默认的 PrintController 是 PrintControllerWithStatusDialog。显示提示窗口
                //打印事件设置
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                try
                {
                    pd.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
                }
                pd.Dispose();
                //     textBox1.Text = bqstr;
                textBox4.Text = mystr;
                shetlines++;
            }

        }

        private NetworkStream networkStream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private TcpClient myclient;
        private Thread recvThread;   // 接收信息线程
       
        private Thread serverThread;// 服务线程
        string txtIP = "192.168.10.110";
        string txtPort = "9004";

        private void Connection()//连接到摄像头 tcp
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(txtIP);
                Int32 port = Int32.Parse(txtPort);
                string hostName = Dns.GetHostEntry(ipAddress).HostName;
                myclient = new TcpClient(hostName, port);
            }
            catch
            {
                MessageBox.Show("没有连接到摄像头!");
                return;
            }

            listBox1.Items.Add("客户端成功连接上摄像头!");
            button1.Enabled = false;     // 连接上了，不让按“连接”按钮

            button3.Enabled = true;

            networkStream = myclient.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
            // 创建接收信息线程，并启动
            recvThread = new Thread(new ThreadStart(RecvData));
            recvThread.Start();
        }

        // 接收数据
        void plcpro(string s)
        {


        }
        private void RecvData()
        {
            string s = streamReader.ReadLine();
            // 如果没接到服务器退出的消息，则继续接收信息
            while (!s.Equals("exit"))
            {
                listBox1.Items.Add(s);
                textBox4.Text = s;

                plcpro(s);  //处理2维码
                s = streamReader.ReadLine();
            }
            // 收到服务器退出的消息，释放资源
            listBox1.Items.Add("摄像头关闭");
            listBox1.Items.Add("客户端关闭");
            button1.Enabled = true;

            button3.Enabled = false;

            //		streamReader.Close();
            //		streamWriter.Close();
            //		networkStream.Close();
            //		myclient.Close();
            ReleaseResouce();
        }
        private void SendData()
        {

        }
        private void ReleaseResouce()
        {
            networkStream.Close();
            streamReader.Close();
            streamWriter.Close();
            //   sendThread.Abort();
            recvThread.Abort();
            serverThread.Abort();
            myclient.Close();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            serverThread = new Thread(new ThreadStart(Connection));
            serverThread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;  // 按了停止之后，“连接”按钮可以用，“发送”不能用

            button3.Enabled = false;
            string exitMsg = "exit";  // 要退出时，发送 exit 信息给服务器
            streamWriter.WriteLine(exitMsg);
            //刷新当前数据流中的数据
            streamWriter.Flush();
            listBox1.Items.Add("客户端关闭");
            ReleaseResouce();
        }

        
      
       
    }
}


/// <summary>
/// 
/// </summary>
//仓位结构
public struct box
{
    public int boxid;
    public int dy;
    public string bq;
    public int num;
    public bool ischecked;
    public bool full;
    public bool finished;
    public bool plc_full;
    public int prints;
    public byte xh;
    public string[,] ma_list;
    public int times;
    public int total;
    public int nows;
    public int ver;
}


//箱表
public class act_box
{
    int[] cu_pu;  //当前仓号表
    int[] py;     //转动角度往偏移
    int[] mx;
    int[] blh;
    int[] cch;
    public int[] sendbyte;
    public box[,] boxes;
    public box tb;
    public string[] wardlist;
    public bool pushed;
    public box cutbox;
    public static int ycl = 7500;//溢出


    public void setfull(int dy, int boxid)
    {
        boxes[dy, boxid].full = true;
    }

    public void printl()
    {
    }

    public bool canpush(int dy, int boxid)
    {
        if (!boxes[dy, boxid].full)
        {
            pushed = true;
            return true;
        }
        else
        {
            pushed = false;
            return false;
        }
    }

    private void Sort(int[] list, int low, int high)
    {
        for (int i = 0; i < list.Length; i++)
        {
            for (int j = 0; j < list.Length - 1; j++)
            {
                if (list[j] > list[j + 1])
                {
                    int temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    public bool pop()
    {
        return true;
    }

    //   找到病区
    public int[] wardxh(string ward)
    {
        int[] bb = new int[2];
        foreach (box b in boxes)
        {
            if (b.bq == ward)
            {
                bb[0] = b.dy;
                bb[1] = b.boxid;
            }
        }
        return bb;
    }
    public void setwardlist()
    {
        string[] ward_codes, ward_1, ward_2, ward_3, ward_4;
        ward_1 = new string[] { "0031-1", "0031-2", "0031-3", "0041-1", "0041-2", "0041-3", "0061-1", "0061-2", "0G1D-2", "0M01-1", "0M05-1", "0N01-1", "0Q01-1", "1-14", "1-15" };
        ward_2 = new string[] { "0061-3", "0111-1", "0121-1", "0126-1", "0131-1", "0135-1", "0136-1", "0141-1", "0147-1", "0149-1", "014O-1", "0151-1", "2-12", "2-14", "2-15" };
        ward_3 = new string[] { "0181-1", "0181-2", "0211-1", "021C-1", "0911-1", "0B11-1", "0B11-2", "0B11-3", "0B11-4", "0B11-5", "0B11-6", "0G13-1", "3-13", "3-14", "3-15" };
        ward_4 = new string[] { "0G14-1", "0G14-2", "0G15-1", "0G15-2", "0G16-1", "0G16-2", "0G19-1", "0G19-2", "0G1A-1", "0G1A-2", "0G1B-1", "0G1C-1", "0G1D-1", "4-14", "4-15" };
        ward_codes = new string[ward_1.Length + ward_2.Length + ward_3.Length + ward_4.Length];

        for (int i = 0; i < 15; i++)
        {
            ward_codes[i] = ward_1[i];
        }
        for (int i = 0; i < 15; i++)
        {
            ward_codes[15 + i] = ward_2[i];
        }
        for (int i = 0; i < 15; i++)
        {
            ward_codes[30 + i] = ward_3[i];
        }
        for (int i = 0; i < 15; i++)
        {
            ward_codes[45 + i] = ward_4[i];
        }
        wardlist = ward_codes;
    }

    public void Insertward(string ward_, string drug_spec)
    {
        foreach (string wd in wardlist)
        {
            if (wd.Trim() == ward_)
            {
                if (!findward(ward_))
                {
                    //       MessageBox.Show("jjj");                       
                    int[] td = new int[3];
                    for (int fg = 0; fg < 3; fg++)
                    {
                        td[fg] = 15;
                    }
                    for (int d = 0; d < 3; d++)
                    {
                        for (int t = 0; t < 15; t++)
                        {
                            if (boxes[d, t].total == 0)
                            {
                                td[d] = t;
                                break;
                            }
                        }
                    }
                    int d0 = td[0];
                    int d1 = td[1];
                    int d2 = td[2];
                  //  int d3 = td[3];

                    Sort(td, 0, 3);
                    if (td[0] == d0)
                    {
                        setward(ward_, 0, d0);
                        setnum(ward_, Int32.Parse(drug_spec));
                    }
                    else
                    {
                        if (td[0] == d1)
                        {
                            setward(ward_, 1, td[0]);
                            setnum(ward_, Int32.Parse(drug_spec));
                        }
                        else
                        {
                            if (td[0] == d2)
                            {
                                setward(ward_, 2, td[0]);
                                setnum(ward_, Int32.Parse(drug_spec));
                            }
                            /*else
                            {
                                if (td[0] == d3)
                                {
                                    setward(ward_, 3, td[0]);
                                    setnum(ward_, Int32.Parse(drug_spec));
                                }
                            }*/
                        }
                    }
                }
                else
                {
                    setnum(ward_, Int32.Parse(drug_spec));
                }
                break;
            }
        }
    }
    public bool findward(string ward)
    {
        bool bb = false;
        foreach (box b in boxes)
        {
            if (b.bq == ward)
            {
                bb = true;
                break;
            }
        }
        return bb;
    }
    //  初始化箱子
    public act_box()
    {
        sendbyte = new int[50];
        cu_pu = new int[10];//当前仓号表
        py = new int[10];//转动角度 偏移
        mx = new int[10];
        blh = new int[10];
        cch = new int[10];
        tb = new box();
        tb.bq = "";
        boxes = new box[4, 15];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                boxes[i, j].boxid = j;
                boxes[i, j].dy = i;
                boxes[i, j].bq = "";
                boxes[i, j].num = 0;
                boxes[i, j].nows = 0;
                boxes[i, j].total = 0;
                boxes[i, j].full = false;
                boxes[i, j].finished = false;
            }
        }
        setwardlist();
        pushed = false;
    }
    public void setblh(int[] gd)
    {
       
        for (int i = 0; i < 3; i++)
        {
            blh[i] = gd[i];
        }
    }
    public void setmx(int[] gd)
    {
        for (int i = 0; i < 3; i++)
        {
           mx[i] = gd[i];
        }
    }
    public void setpi (int[] gd)
    {
        for (int i = 0; i < 3; i++)
        {
           py[i] = gd[i];
        }
    }
    public void setcc(int[] gd)
    {
        for (int i = 0; i < 3; i++)
        {
            cch[i] = gd[i];
        }
    }
    public void setcu(int[] gd)
    {
        for (int i = 0; i < 3; i++)
        {
            cu_pu[i] = gd[i];
        }
    }
    //  getbox
    public box getbox(string ward)
    {
        foreach (box b in boxes)
        {
            if (b.bq == ward)
            {
                tb = b;
                break;
            }
        }
        return tb;
    }

    public void setfinish(int dy, int boxid)
    {
        boxes[dy, boxid].finished = true;
    }

    public bool finished(string ward)
    {
        bool f = false;
        box b = getbox(ward);
        if (b.nows == b.total)
        {
            f = true;
        }
        return f;
    }
    //  设置病区
    public void setward(string ward, int dy, int box)
    {
        boxes[dy, box].bq = ward;
    }

    public int[] jspy(int cu, int nex)    //计算偏移
    {
        int[] ii = new int[2];
        return ii;
    }

    //  设置数量
    public void setnum(string ward, int vers)
    {
        box tb;
        foreach (box b in boxes)
        {
            if (b.bq == ward)
            {

                tb = b;
                boxes[tb.dy, tb.boxid].num = boxes[tb.dy, tb.boxid].num + 1;
                boxes[tb.dy, tb.boxid].nows = boxes[tb.dy, tb.boxid].nows + 1;
                boxes[tb.dy, tb.boxid].total = boxes[tb.dy, tb.boxid].total + 1;
                boxes[tb.dy, tb.boxid].ver = boxes[tb.dy, tb.boxid].ver + vers;
                if (b.ver + vers > ycl)
                {

                    setfull(tb.dy, tb.boxid);
                    mx[tb.dy] = tb.boxid;


                }
                else
                {
                    cu_pu[tb.dy] = tb.boxid;

                }
                break;
            }
        }

    }



}
//线上水袋
public struct sd
{
    public string barcode;
    public bool finish;
    public int d;
    public int lx;
    public Point ws;
    public int bs;
}

public class plcdata
{
    #region
    public static string PLCIP = "192.168.10.1";
    private int inport = 9600;
    private int outport = 9600;
    public static byte[] backdat;
    public byte[] writeplc;
    #endregion

    public plcdata()
    {
 
    }


    public bool[] gd;
    // string message;
    public static byte[] wordtobyte(int i)
    {
        byte[] sb;
        string tmp = i.ToString("X4");
        sb = Enumerable.Range(0, tmp.Length)
             .Where(x => x % 2 == 0)
             .Select(x => Convert.ToByte(tmp.Substring(x, 2), 16))
             .ToArray();

        return sb;
    }
    public static byte[] stringtobyte(string input)
    {
        return Enumerable.Range(0, input.Length)
             .Where(x => x % 2 == 0)
             .Select(x => Convert.ToByte(input.Substring(x, 2), 16))
             .ToArray();
    }

    public byte[] getplc(string add,int count,bool dw)
    {
        bool b = false;
        byte[] fins = new byte[0];
        byte[] back = SendMessage(PLCIP, fins);
        if (back.Length > 0)
        {
            b = true;
        }
        return back;
    }

    public byte[] setplc(string add,int[] dat)
    {
        byte[] bytedat=new byte[dat.Length*2];
        byte[] tmp=new byte[2];
        for (int i = 0; i < dat.Length; i++) 
        {
            tmp=wordtobyte(dat[i]);
            bytedat[i * 2] = tmp[0];
            bytedat[i * 2 + 1] = tmp[1];

        }
        byte[] fins = new byte[0];
        byte[] back = SendMessage(PLCIP, fins);
     
        return back;
    }
  

    //   add+count to str
    public static string addtostr(string add, int count)
    {

        string add_s = add;
        string add_b = add_s.Substring(4, 2);
        add_s = add_s.Substring(0, 4);
        int i = Int32.Parse(add_s);
        String add_hx = i.ToString("X4");
        i = Int32.Parse(add_b);
        add_b = i.ToString("X2");
        string cou = count.ToString("X4");
        string tmp = add_hx + add_b + cou;
        return tmp;
    }

  

    public static string ByteToString(byte[] bytes)
    {
        StringBuilder strBuilder = new StringBuilder();
        foreach (byte bt in bytes)
        {
            strBuilder.AppendFormat("{0:X2}", bt);
        }
        return strBuilder.ToString();
    }

    public void sendmessage1(string ip, int[] idat)
    {
        byte[] dat = new byte[idat.Length * 2];
        byte[] tmp = new byte[2];
      
        for (int i = 0; i < idat.Length; i++)
        {
            tmp = wordtobyte(idat[i]);
            dat[i * 2] = tmp[0];
            dat[i * 2 + 1] = tmp[1];

        }
        IPEndPoint locip = new IPEndPoint(IPAddress.Any, inport);
        IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(ip),outport);

        UdpClient udp = new UdpClient(locip);
        udp.Send(dat, dat.Length, remoteIpep);
       // ret = udp.Receive(ref locip);
        udp.Close();
        udp = null;
        Thread.Sleep(1);
        
    }
    public byte[] SendMessage(string ip, byte[] dat)//发送plc fins
    {
        byte[] bytRecv = new byte[0];
        try
        {
            IPEndPoint locip = new IPEndPoint(IPAddress.Any, inport);
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(ip), outport);

            UdpClient udp = new UdpClient(locip);
            udp.Send(dat, dat.Length, remoteIpep);

            bytRecv = udp.Receive(ref locip);
       
            udp.Close();
            udp = null;
           // Thread.Sleep(2);
            return bytRecv;
        }
        catch (Exception ex)
        {
            MessageBox.Show("端口占用！");
        }
        return bytRecv;
    }
}


public class online
{
    public sd[] sds;
    public int[] gds;

    

   
    public online()
    {
    
        gds = new int[5] { 1, 0, 0, 0, 0 };
    }

    public void incpoint()
    {
        if (sds != null)
        {
            for (int i = 0; i < sds.Length; i++)
            {
                sds[i].ws.X = sds[i].ws.X - 130;
                sds[i].ws.Y = sds[i].ws.Y - 130;
            }
        }
    }

    public void move()
    {
        foreach (sd tsd in sds)
        {
            if (tsd.ws.X + 130 > 191 && tsd.ws.X < 191)
            {
                gds[4] = gds[4] + 1;
            }
            if (tsd.ws.X + 130 > 451 && tsd.ws.X < 451)
            {
                gds[3] = gds[3] + 1;
            }
            if (tsd.ws.X + 130 > 711 && tsd.ws.X < 711)
            {
                gds[2] = gds[2] + 1;
            }
            if (tsd.ws.X + 130 > 971 && tsd.ws.X < 971)
            {
                gds[1] = gds[1] + 1;
            }
        }
    }

    //  水代上线
    public void addsd(string bar, int which, int c)
    {
        incpoint();
        sd tsd = new sd();
        sd[] tmpsds;
        tsd.barcode = bar;
        tsd.finish = false;
        tsd.d = which + 1;
        tsd.lx = which + 1;
        tsd.bs = c;
        tsd.ws = new Point(1200, 1000);
        if (sds == null)
        {
            sds = new sd[1];
            sds[0] = tsd;
        }
        else
        {
            tmpsds = new sd[sds.Length];
            tmpsds = sds;
            sds = new sd[sds.Length + 1];
            sds[sds.Length - 1] = tsd;
            for (int k = 0; k < tmpsds.Length; k++)
            {
                sds[k] = tmpsds[k];
            }
        }
        gds[0] = gds[0] + 1;
        tuch();
        move();
    }

    public void tuch()
    {
        foreach (sd tsd in sds)
        {
            if (tsd.bs == gds[tsd.d] && tsd.bs != 0)
            {
                delsd(tsd.bs);
            }
        }
    }

    public bool setward(int[] add)
    {
        return false;
    }

    public int[] gettime()
    {
        int[] adds = new int[5];
        return adds;
    }


    public void delsd(int sd)
    {

        for (int i = 0; i < sds.Length; i++)
        {
            if (sd == sds[i].bs)
            {
                sds[i].lx = 0;
                break;
            }
        }

    }

    public void drawsd(Panel plg)
    {
        plg.Refresh();

        Graphics graphics = plg.CreateGraphics();
        foreach (sd tsd in sds)
        {
            Bitmap bmap = null;
            switch (tsd.lx)
            {
                case 1: bmap = new Bitmap(@"e:\pivas\水袋1.png");
                    break;
                case 2: bmap = new Bitmap(@"e:\pivas\水袋.png");
                    break;
                case 3: bmap = new Bitmap(@"e:\pivas\水袋2.png");
                    break;
                case 4: bmap = new Bitmap(@"e:\pivas\水袋.png");
                    break;
            }
            if (bmap != null)
            {
                int yb = bmap.Height;
                Image temp = bmap;
                Point px = new Point(tsd.ws.X, (yb - bmap.Height) / 2);
                int cs = tsd.bs;
                graphics.DrawImage(temp, px);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                Font drawFont = new Font("Arial", 9);
                graphics.DrawString(tsd.barcode.ToString() + "d:" + tsd.d.ToString(), drawFont, drawBrush, px);
            }
        }
    }

    
}


