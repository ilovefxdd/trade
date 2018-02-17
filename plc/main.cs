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
        bool debug = true;

        //    RunState state = RunState.stop;
        public struct xhb
        {
            public int xh;
            public int dyh;
            public string bq;
            public int ps;
            public int cs;
            public int zs;
            public int wc;
            public int cxc;
            public bool h;
            public int v;
            public bool m;
        }
        xhb[] xlist = new xhb[45];
        struct dy
        {
            public bool kzcc;//可再出仓
            public bool ccxh;//出仓信号
            public int cchh;//出仓箱号

            public bool bsy;//不使用
            public bool yfjwc;//有分拣完成
            public bool plccxh;//排料出仓信号
            public int clkxh;//出料口箱号
        }
        dy[] dy3 = new dy[3];
        public struct plczt
        {
            public string tm;
            public int dy;
            public int ftm;
            public int xh;
            public int v;
            public bool zqfh;
            public int stm;
        }
        private const int READER_COUNT = 3;      // number of readers to connect
        private const int RECV_DATA_MAX = 512;
        private ClientSocket[] clinetSocketInstance;
        plczt pz;
        public struct idb
        {
            public string bar;
            public string ward;
            public int v;
        }
        idb[] idlist = new idb[0];
        public int cou;
        static int vl = 700;
        private UdpClient udpcSend;
        private string plcip = "192.168.10.1";
        private int outport = 9600;
        public delegate void ProcessDelegate();

        #region  //plc
        private string w_d = "80000200010000090000010282";
        private string w_w = "80000200010000090000010231";

        private void p_lx(int dy, int xh, int v, int tm)//入料
        {
            string fins = "";
            byte[] sendbyte = new byte[0];
            byte[] ret;
            switch (dy)
            {
                case 1: fins = w_d + "0007000001"; break;
                case 2: fins = w_d + "0008000001"; break;
                case 3: fins = w_d + "0009000001"; break;
            }
            byte[] tmp = wordtobyte(xh);
            fins = fins + ByteToString(tmp);
            sendbyte = stringtobyte(fins);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            ShowM1(debug_t, "/" + ByteToString(sendbyte));

            switch (dy)
            {
                case 1: fins = w_d + "001f000001"; break;
                case 2: fins = w_d + "0021000001"; break;
                case 3: fins = w_d + "0023000001"; break;
            }
            byte[] ttmp = wordtobyte(tm);
            fins = fins + ByteToString(ttmp);

            sendbyte = stringtobyte(fins);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            ShowM1(debug_t, "/" + ByteToString(sendbyte));
            switch (dy)
            {
                case 1: fins = w_d + "012d000001"; break;
                case 2: fins = w_d + "012f000001"; break;
                case 3: fins = w_d + "0131000001"; break;
            }
            byte[] vtmp = wordtobyte(v);
            fins = fins + ByteToString(vtmp);

            sendbyte = stringtobyte(fins);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            ShowM1(debug_t, "/" + ByteToString(sendbyte));
           
            /*
             * switch (dy)
                        {
                            case 1: fins = w_w + "0028000001"; break;
                            case 2: fins = w_w + "0028010001"; break;
                            case 3: fins = w_w + "0028020001"; break;
                        }
                        fins = fins + "01";
                        sendbyte = stringtobyte(fins);
                        udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
                        byte[] bytRecv = udpcSend.Receive(ref localIpep);
                        ret = new byte[bytRecv.Length - 14];
                        if (debug)
                        {
               
                            ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + ByteToString(bytRecv));
                        }
                        Thread.Sleep(2);
                        */
        }     //
        private string r_d0 = "80000200010000090000010182000000002D";
        private int[] getdq() //读plc袋数
        {
            int[] io = new int[45];
            byte[] sendb = new byte[0];
            sendb = stringtobyte(r_d0);
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "00|" + ByteToString(ret));
            Thread.Sleep(3);
            if (ret.Length == 90)
            {
                for (int s = 0; s < 45; s++)
                {
                    io[s] = (((ret[s * 2] << 8) & 0xFF00 | ret[s * 2 + 1] & 0xFF));
                }

            }
            return io;
        }
        private string r_d310 = "80000200010000090000010182013600002D";
        int[] s_d310 = new int[45];
        private int[] getds() //读plc袋数
        {
            int[] io = new int[45];
            byte[] sendb = stringtobyte(r_d310);
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_d310|" + ByteToString(sendb));
                ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_d310|" + ByteToString(bytRecv));
            Thread.Sleep(3);
            if (ret.Length == 90)
            {
                for (int s = 0; s < 45; s++)
                {
                    io[s] = (((ret[s * 2] << 8) & 0xFF00 | ret[s * 2 + 1] & 0xFF));
                }
                Panel tp = p1;
                for (int j = 0; j < 15; j++)
                {
                    foreach (Control c in tp.Controls)
                    {
                        if (c is TextBox && ((TextBox)c).Name == "p1t" + j)
                        {
                            ShowMessage((TextBox)c, io[j].ToString());
                            break;
                        }
                    }
                }
                tp = p2;
                for (int j = 15; j < 30; j++)
                {
                    foreach (Control c in tp.Controls)
                    {
                        if (c is TextBox && ((TextBox)c).Name == "p1t" + j)
                        {
                            ShowMessage((TextBox)c, io[j].ToString());
                            break;
                        }
                    }
                }
                tp = p3;
                for (int j1 = 30; j1 < 45; j1++)
                {
                    foreach (Control c in tp.Controls)
                    {
                        if (c is TextBox && ((TextBox)c).Name == "p1t" + j1)
                        {
                            ShowMessage((TextBox)c, io[j1].ToString());
                            break;
                        }
                    }
                }
                for (int i = 0; i < 45; i++)
                {
                    xlist[i].ps = io[i];
                    xlist[i].bq = "";
                }
            }
            return io;
        }

        private string w_d600 = "800002000100000900000102820258000003";
        int[] s_d600 = new int[3];
        private void setccxh(int[] xh)//出仓箱号
        {
            byte[] sendbyte = new byte[0];
            sendbyte = wprostr(w_d600, xh);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "wd600|" + sendbyte);
            Thread.Sleep(3);

        }

        private void setv()//设置体积
        {
            byte[] sendbyte = new byte[0];
            int[] vi = new int[1];
            vi[0] = 700;

            sendbyte = wprostr("80000200010000090000010282012c000001", vi);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "w_d501|");
            Thread.Sleep(3);

        }

        private string w_w40_5 = "800002000100000900000102310028050003";
        byte[] fj_finish = new byte[3];
        private void fj_wc(byte[] wc)//分拣完成 w40.5
        {
            string fins = w_w40_5;
            int len;

            byte[] tmp = new byte[0];
            tmp = stringtobyte(fins);
            len = tmp.Length + 3;
            byte[] sendb = new byte[len];
            for (int i = 0; i < tmp.Length; i++)
            {
                sendb[i] = tmp[i];
            }
            for (int j = 0; j < 3; j++)
            {
                sendb[tmp.Length + j] = wc[j];
            }
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "w_w45|" + fins);
            Thread.Sleep(3);
        }

        private string r_w100_15 = "8000020001000009000001013100640F0001";
        byte[] s_100 = new byte[1];
        private bool xtpl()  //排料
        {
            bool pl = false;
            byte[] sendb = new byte[0];
            sendb = stringtobyte(r_w100_15);
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            Thread.Sleep(3);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_w100|" + ByteToString(ret));
            if (ret[0] == 1) pl = true;
            return pl;
        }
        private void threadfun()
        {

           

                getds(); //读plc袋数


    
        }
        private string w_w50_0 = "80000200010000090000010231";//转盘出仓信号
        byte[] s_50_0 = new byte[3];
        private void setcc(int wi, byte[] cc)
        {
            string fins = w_w50_0;
            switch (wi)
            {
                case 1: fins = w_w50_0 + "0032000001"; break;
                case 2: fins = w_w50_0 + "0032010001"; break;
                case 3: fins = w_w50_0 + "0032020001"; break;
            }
            int len;

            byte[] tmp = new byte[0];
            tmp = stringtobyte(fins);
            len = tmp.Length + 1;
            byte[] sendb = new byte[len];
            for (int i = 0; i < tmp.Length; i++)
            {
                sendb[i] = tmp[i];
            }
            sendb[tmp.Length + 1] = cc[0];
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "w50_0|" + fins);
            Thread.Sleep(3);

        }//设置出仓信号

        private string r_w11_0 = "80000200010000090000010131000B000003";
        byte[] s_11 = new byte[3];

        private byte[] zppl()//转盘排料
        {
            byte[] sendb = new byte[0];
            sendb = stringtobyte(r_w11_0);
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_w11|");
            if (ret[0] == 1)
            {
                cb_pl1.Checked = true;
            }
            else
            {
                cb_pl1.Checked = false;
            }
            if (ret[1] == 1)
            {
                cb_pl2.Checked = true;
            }
            else
            {
                cb_pl2.Checked = false;
            }
            if (ret[2] == 1)
            {
                cb_pl3.Checked = true;
            }
            else
            {
                cb_pl3.Checked = false;
            }
            return ret;
        }

        private string r_d205 = "8000020001000009000001018200CD000003";
        int[] s_d205 = new int[3];

        private int[] zpclk()//出料口
        {
            int[] io = new int[3];
            byte[] sendb = new byte[0];
            sendb = stringtobyte(r_d205);
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            Thread.Sleep(3);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (!debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_d205|" + ByteToString(ret));
            if (ret.Length == 6)
            {
                for (int s = 0; s < 3; s++)
                {
                    io[s] = ((ret[s * 2] << 8) & 0xFF00) | (ret[s * 2 + 1] & 0xFF);
                }
                ShowMessage(tx_plh1, io[0].ToString());
                ShowMessage(tx_plh2, io[1].ToString());
                ShowMessage(tx_plh3, io[2].ToString());
            }
            return io;
        }
        private string r_w120_00 = "80000200010000090000010131007800002D";
        byte[] s_120 = new byte[45];

        private byte[] getfulled()//料箱满
        {
            Panel tp = p1;

            byte[] sendbyte = new byte[0];
            sendbyte = stringtobyte(r_w120_00);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            for (int i = 0; i < 45; i++)
            {
                if (ret[i] == 1)
                { xlist[i].m = true; }
                else { xlist[i].m = false; }
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_w120|");
            for (int i = 0; i < 45; i++)
            {
                if (i < 15) tp = p1;
                if (i >= 15 && i < 30) tp = p2;
                if (i >= 30) tp = p3;

                if (ret[i] == 1)
                {
                    foreach (Control c in tp.Controls)
                    {
                        if (c is PictureBox && ((PictureBox)c).Name == "pb" + i)
                        {
                            showpic((PictureBox)c, PIVASworker.Properties.Resources.low);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (Control c in tp.Controls)
                    {
                        if (c is PictureBox && ((PictureBox)c).Name == "pb" + i)
                        {
                            showpic((PictureBox)c, PIVASworker.Properties.Resources.high);
                            break;
                        }
                    }
                }
            }
            Thread.Sleep(3);
            return ret;
        }

        private string w_d610 = "80000200010000090000010282026200002D";

        private void settotal() //分拣总数  未设置
        {
            byte[] sendbyte = new byte[0];
            int[] a = new int[45];
            sendbyte = wprostr(w_d610, a);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "w_d610|");
            Thread.Sleep(3);
        }

        private string w_w50_10 = "8000020001000009000001023100320A0002";
        private void setccxz() //出仓选择
        {
            byte[] xz = new byte[2];
            string fins = w_w50_10;
            byte[] tmp = new byte[0];
            tmp = stringtobyte(fins);
            int len = tmp.Length + 2;
            byte[] sendb = new byte[len];
            for (int i = 0; i < tmp.Length; i++)
            {
                sendb[i] = tmp[i];
            }
            for (int j = 0; j < 2; j++)
            {
                sendb[tmp.Length + j] = xz[j];
            }
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "w_w50_10|" + fins);
            Thread.Sleep(3);

        }

        //   private string w_w50_12 = "8000020001000009000001023100320c0003";
        private void setkzcc() //可再出仓
        {
            byte[] xz = new byte[3];
            string fins = w_w50_10;

            byte[] tmp = new byte[0];
            tmp = stringtobyte(fins);
            int len = tmp.Length + 3;
            byte[] sendb = new byte[len];
            for (int i = 0; i < tmp.Length; i++)
            {
                sendb[i] = tmp[i];
            }
            for (int j = 0; j < 3; j++)
            {
                sendb[tmp.Length + j] = xz[j];
            }
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "w_w50_0|" + fins);
            Thread.Sleep(3);

        }

        private string r_w15_0 = "80000200010000090000010131000f000001";

        private bool getzd() //自动辅助状态
        {
            string fins = r_w15_0;

            byte[] sendb = stringtobyte(fins);
            bool zd = false;
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_w15_0|" + fins);
            Thread.Sleep(3);
            if (bytRecv[0] == 1) { zd = true; } else { zd = false; }
            return zd;
        }
        private void setyfjhc(int dy)//有转盘有分拣完成
        {
            string fins = "";
            byte[] sendbyte = new byte[0];
            switch (dy)
            {
                case 1: fins = w_w + "0028050001"; break;
                case 2: fins = w_w + "0028060001"; break;
                case 3: fins = w_w + "0028070001"; break;
            }
            fins = fins + "01";
            sendbyte = stringtobyte(fins);
            udpcSend.Send(sendbyte, sendbyte.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            ret = new byte[bytRecv.Length - 14];
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "w40.5|" + fins);
            Thread.Sleep(2);
        }
        private string r_w50_5 = "800002000100000900000101310032050003";
        byte[] s_50_5 = new byte[10];
        private byte[] getno()//转盘不使用
        {
            string fins = r_w50_5;
            byte[] sendb = stringtobyte(fins);
            udpcSend.Send(sendb, sendb.Length, remoteIpep);
            byte[] bytRecv = udpcSend.Receive(ref localIpep);
            byte[] ret = new byte[bytRecv.Length - 14];
            for (int r = 0; r < ret.Length; r++)
            {
                ret[r] = bytRecv[r + 14];
            }
            if (debug)
                ShowMessage(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "r_w50_5|" + fins);
            Thread.Sleep(3);
            return bytRecv;
        }
        #endregion
        private void online_plc_Click(object sender, EventArgs e)
        {

            for (int i =0; i <3; i++)
            {
                try
                {
                    if (clinetSocketInstance[i].commandSocket != null)
                    {
                        clinetSocketInstance[i].commandSocket.Close();
                    }
                    clinetSocketInstance[i].commandSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    textBox1.Text = clinetSocketInstance[i].readerCommandEndPoint.ToString() + " Connecting..";
                    textBox1.Update();
                    clinetSocketInstance[i].commandSocket.Connect(clinetSocketInstance[i].readerCommandEndPoint);
                    textBox1.Text = clinetSocketInstance[i].readerCommandEndPoint.ToString() + " Connected.";
                    textBox1.Update();
                }
                catch (SocketException ex)
                {
                    textBox1.Text = clinetSocketInstance[i].readerCommandEndPoint.ToString() + " Failed to connect.";
                    textBox1.Update();
                    MessageBox.Show(ex.Message);
                    clinetSocketInstance[i].commandSocket = null;
                    continue;
                }
                try
                {
                    if (clinetSocketInstance[i].dataSocket != null)
                    {
                        clinetSocketInstance[i].dataSocket.Close();
                    }
                    clinetSocketInstance[i].dataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    textBox1.Text = clinetSocketInstance[i].readerDataEndPoint.ToString() + " Connecting..";
                    textBox1.Update();
                    clinetSocketInstance[i].dataSocket.Connect(clinetSocketInstance[i].readerDataEndPoint);
                    textBox1.Text = clinetSocketInstance[i].readerDataEndPoint.ToString() + " Connected.";
                    textBox1.Update();
                    clinetSocketInstance[i].dataSocket.ReceiveTimeout = 100;
                }
                catch (SocketException ex)
                {
                    textBox1.Text = clinetSocketInstance[i].readerDataEndPoint.ToString() + " Failed to connect.";
                    textBox1.Update();
                    MessageBox.Show(ex.Message);
                    clinetSocketInstance[i].dataSocket = null;
                    continue;
                }
            }
           
        }

        act_box pivas = new act_box();
        //  plcdata plc = new plcdata();
        public online ol;
        public main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            clinetSocketInstance = new ClientSocket[READER_COUNT];
            int readerIndex = 0;
            ol = new online();
            byte[] ip1 = { 192, 168, 10, 100 };
            byte[] ip2 = { 192, 168, 10, 110 };
            byte[] ip3 = { 192, 168, 10, 120 };

            clinetSocketInstance[readerIndex++] = new ClientSocket(ip1, 9003, 9004);  // 9003 for command, 9004 for data
            clinetSocketInstance[readerIndex++] = new ClientSocket(ip2, 9003, 9004);  // 9003 for command, 9004 for data
            clinetSocketInstance[readerIndex++] = new ClientSocket(ip3, 9003, 9004);  // 9003 for command, 9004 for data

            for (int i = 0; i < 45; i++)
            {
                xlist[i].xh = i + 1;
            }
        }
        #region int word byte
        private byte[] wprostr(string w, int[] ia)
        {
            byte[] tmp = stringtobyte(w);

            int j = tmp.Length;
            int l = j + ia.Length * 2;
            byte[] b = new byte[l];
            for (int i = 0; i < j; i++)
            {
                b[i] = tmp[i];
            }
            byte[] bi = inttoby(ia);
            for (int i = 0; i < ia.Length * 2; i++)
            {
                b[i + j] = bi[i];
            }
            return b;
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
        #endregion
        private string getwad(string cad)
        {
            string st;
            SqlConnection conn = new SqlConnection(sqlc.localco);
            conn.Open();
            string sql1 = "select barcode,ward_code,	ward_name,dosage_units,dosage from  Barcode where   barcode=@code  ";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            //      SqlParameter dt = new SqlParameter("@dt", "2015-01-21");
            SqlParameter code = new SqlParameter("@code", cad);
            //    cmd.Parameters.Add(dt);
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
        public void getidlist() //预读表
        {
            string st;
            SqlConnection conn = new SqlConnection(sqlc.localco);
            conn.Open();
            string sql = "select count(barcode) from  Barcode where test_date<@dt ";
            SqlCommand ci = new SqlCommand();
            ci.Connection = conn;

            ci.CommandText = sql;
            SqlParameter dp = new SqlParameter("@dt", "2015-01-21");
            ci.Parameters.Add(dp);
            SqlDataReader rc = ci.ExecuteReader();
            int i = 0;
            while (rc.Read())
            {
                float f1 = Convert.ToSingle(rc[0].ToString());
                i = Convert.ToInt32(f1);
            }
            conn.Close();
            conn.Open();
            string sql1 = "select barcode,	ward_name,dosage from  Barcode where test_date<@dt ";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlParameter dt = new SqlParameter("@dt", "2015-01-21");
            cmd.Parameters.Add(dt);


            SqlDataReader rd = cmd.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象

            if (i > 0)
            {
                idlist = new idb[i];
                int l = 0;
                float f;
                int v;
                while (rd.Read())
                {
                    idlist[l].bar = rd[0].ToString();
                    idlist[l].ward = rd[1].ToString();
                    f = Convert.ToSingle(rd[2].ToString());
                    v = Convert.ToInt32(f);
                    idlist[l].v = v;
                    l++;
                }
            }
        }
        //    bool IsUdpcRecvStart = false;

        private void main_Load(object sender, EventArgs e) //初始界面
        {
            #region
            TextBox tb, tb1, tb2;
            Label lb;
            PictureBox pb;
            for (int c = 1; c < 15; c++)
            {
                tb = new TextBox();
                tb.Location = new Point(30 + 75 * (c - 10), 132);
                tb.Width = 80;
                tb.Height = 21;
                tb.Parent = plg;
                tb.Name = "ctb" + c;
                //this.Controls.Add(tb);
            }
            //    初始化动态输入表
            for (int i = 10; i < 25; i++)
            {
                lb = new Label();
                lb.Location = new Point(20, 78 + 32 * (i - 10));
                lb.Name = "lb1" + i;
                lb.Width = 45;
                lb.Height = 21;
                lb.Parent = p1;
                lb.Text = (i - 9).ToString() + "号箱";

                pb = new PictureBox();
                pb.Location = new Point(66, 75 + 32 * (i - 10));
                pb.Width = 30;
                pb.Height = 30;
                pb.Parent = p1;
                pb.Name = "pb" + (i - 10);
                pb.Image = PIVASworker.Properties.Resources.low;
                //  this.Controls.Add(pb);

                tb = new TextBox();
                tb.Location = new Point(102, 75 + 32 * (i - 10));
                tb.Width = 80;
                tb.Height = 21;
                tb.Parent = p1;
                tb.Name = "p1t" + (i - 10);

                tb1 = new TextBox();
                tb1.Location = new Point(188, 75 + 32 * (i - 10));
                tb1.Width = 80;
                tb1.Height = 21;
                tb1.Parent = p1;
                tb1.Name = "p2t" + (i - 10);
                //  this.Controls.Add(tb1);
                tb2 = new TextBox();
                tb2.Location = new Point(276, 75 + 32 * (i - 10));
                tb2.Width = 150;
                tb2.Height = 21;
                tb2.Parent = p1;
                tb2.Name = "p3t" + (i - 10);
                //   this.Controls.Add(tb2);
            }
            for (int i = 10; i < 25; i++)
            {
                //  this.Controls.Add(tb);

                lb = new Label();
                lb.Location = new Point(20, 78 + 32 * (i - 10));
                lb.Name = "lb2" + i;
                lb.Width = 45;
                lb.Height = 21;
                lb.Parent = p2;
                lb.Text = (i + 6).ToString() + "号箱";

                pb = new PictureBox();
                pb.Location = new Point(66, 75 + 32 * (i - 10));
                pb.Width = 30;
                pb.Height = 30;
                pb.Parent = p2;
                pb.Name = "pb" + (i + 5);
                pb.Image = PIVASworker.Properties.Resources.low;
                //   this.Controls.Add(pb);
                tb = new TextBox();
                tb.Location = new Point(102, 75 + 32 * (i - 10));
                tb.Width = 80;
                tb.Height = 21;
                tb.Parent = p2;
                tb.Name = "p1t" + (i + 5);

                tb1 = new TextBox();
                tb1.Location = new Point(188, 75 + 32 * (i - 10));
                tb1.Width = 80;
                tb1.Height = 21;
                tb1.Parent = p2;
                tb1.Name = "p2t" + (i + 5);
                //  this.Controls.Add(tb1);
                tb2 = new TextBox();
                tb2.Location = new Point(276, 75 + 32 * (i - 10));
                tb2.Width = 150;
                tb2.Height = 21;
                tb2.Parent = p2;
                tb2.Name = "p3t" + i;
                //  this.Controls.Add(tb2);
            }
            for (int i = 10; i < 25; i++)
            {
                lb = new Label();
                lb.Location = new Point(20, 78 + 32 * (i - 10));
                lb.Name = "lb1" + i;
                lb.Width = 45;
                lb.Height = 21;
                lb.Parent = p3;
                lb.Text = (i + 21).ToString() + "号箱";

                pb = new PictureBox();
                pb.Location = new Point(66, 75 + 32 * (i - 10));
                pb.Width = 30;
                pb.Height = 30;
                pb.Parent = p3;
                pb.Name = "pb" + (20 + i);
                pb.Image = PIVASworker.Properties.Resources.low;
                //  this.Controls.Add(pb);
                tb = new TextBox();
                tb.Location = new Point(102, 75 + 32 * (i - 10));
                tb.Width = 80;
                tb.Height = 21;
                tb.Parent = p3;
                tb.Name = "p1t" + (20 + i);
                //   this.Controls.Add(tb);

                tb1 = new TextBox();
                tb1.Location = new Point(188, 75 + 32 * (i - 10));
                tb1.Width = 80;
                tb1.Height = 21;
                tb1.Parent = p3;
                tb1.Name = "p2t" + (20 + i);
                //   this.Controls.Add(tb1);

                tb2 = new TextBox();
                tb2.Location = new Point(276, 75 + 32 * (i - 10));
                tb2.Width = 150;
                tb2.Height = 21;
                tb2.Parent = p3;
                tb2.Name = "p3t" + (20 + i);
                //   this.Controls.Add(tb2);
            }
            #endregion
            xlist = new xhb[45];
            for (int i = 0; i < 45; i++) { xlist[i].bq = ""; }
            dy3 = new dy[3];
         /*   getidlist();
            remoteIpep = new IPEndPoint(
             IPAddress.Parse(plcip), outport); // 发送到的IP地址和端口号
            localIpep = new IPEndPoint(IPAddress.Any, 9600); // 本机IP和监听端口号
            udpcSend = new UdpClient(localIpep);*/
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


                    //  ol.drawsd(plg);
                    // showboxs(ward_);
                };
                //plg.Invoke(showtplg);
                Thread.Sleep(5);
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
            //     dataGridView1.DataSource = dataTable;//dataGridView1中显示
            return dataSet.Tables[strSheetName];
        }
        string pid;
        private void excel_sql_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pid = idlist[i].bar;
                PrintDocument pd = new PrintDocument();
                //设置边距
                Margins margin = new Margins(120, 20, 20, 20);
                pd.DefaultPageSettings.Margins = margin;
                ////纸张设置默认
                //PaperSize pageSize = new PaperSize("First custom size", 800, 600);
                //pd.DefaultPageSettings.PaperSize = pageSize;
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();

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

        private void GetResultIntoImage(ref Image temp)
        {
            Graphics g = Graphics.FromImage(temp);
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);
            //填充数据到图片模板（位置要在制作图片模板的时候度量好）
            g.DrawImage(temp, 0, 0, temp.Width, temp.Height);
            g.Dispose();
        } //读取图片模板

        private void pd_PrintPage(object sender, PrintPageEventArgs e)//打印处理
        {
            string printstr = "";
            string excelstr = "";
            string bqstr = "";
            COMMON.ByteMatrix byteMatrix = new MultiFormatWriter().encode(pid, BarcodeFormat.QR_CODE, 100, 100);
            Bitmap bmap = toBitmap(byteMatrix);
            Image temp = bmap;
            GetResultIntoImage(ref temp);
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = temp.Width;
            int height = temp.Height;

            Rectangle destRect = new Rectangle(x, y, width, height);
            e.Graphics.DrawImage(temp, destRect, 0, 10, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
            SolidBrush Brush = new SolidBrush(Color.Black);
            Font drawFont = new Font("Times New Roman", 9);
            PointF drawPoint = new PointF(x + 10, y + height - 10);

            e.Graphics.DrawString(excelstr, drawFont, Brush, drawPoint);
            drawFont = new Font("Times New Roman", 8);
            drawPoint = new PointF(60, y + height / 2);
            e.Graphics.DrawString("ml :" + bqstr, drawFont, Brush, drawPoint);
        }

        #region
        private NetworkStream networkStream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private NetworkStream networkStream1;
        private StreamReader streamReader1;
        private StreamWriter streamWriter1;
        private NetworkStream networkStream2;
        private StreamReader streamReader2;
        private StreamWriter streamWriter2;

        private TcpClient myclient;
        private Thread recvThread;   // 接收信息线程
        private TcpClient myclient1;
        private Thread recvThread1;   // 接收信息线程

        private TcpClient myclient2;
        private Thread recvThread2;   // 接收信息线程

        private Thread serverThread;
        private Thread serverThread1;
        private Thread serverThread2;// 服务线程
        string txtIP1 = "192.168.10.100";
        string txtIP2 = "192.168.10.110";
        string txtIP3 = "192.168.10.120";
        string txtPort = "9004";
        #endregion
        #region  id_thread_receive
        private void plcrun_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(threadfun);
            thread.IsBackground = true;
            thread.Start();
        }

        private void stopscan_Click(object sender, EventArgs e)
        {
            idscan.Enabled = true;  // 按了停止之后，“连接”按钮可以用，“发送”不能用

            stopscan.Enabled = false;
            string exitMsg = "exit";  // 要退出时，发送 exit 信息给服务器
            streamWriter.WriteLine(exitMsg);
            //刷新当前数据流中的数据
            streamWriter.Flush();
            id1_t.Text = ("客户端关闭");
            ReleaseResouce();
        }
        private void Connection()//连接到摄像头 tcp
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(txtIP1);
                Int32 port = Int32.Parse(txtPort);
                string hostName = Dns.GetHostEntry(ipAddress).HostName;
                myclient = new TcpClient(hostName, port);
            }
            catch
            {
                MessageBox.Show("没有连接到摄像头!");
                return;
            }
            idscan.Enabled = false;     // 连接上了，不让按“连接”按钮
            stopscan.Enabled = true;
            networkStream = myclient.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
            // 创建接收信息线程，并启动
            recvThread = new Thread(new ThreadStart(RecvData));
            recvThread.Start();
        }

        private int pro(string tm, string wd, string does)
        {
            string ward_ = wd;
            ward_ = ward_.Trim();
            string d = does;
            float f = Convert.ToSingle(d);
            int i = Convert.ToInt32(f);
            string drug_spec = i.ToString();

            ProcessDelegate showtplg = delegate()
            {
                pivas.Insertward(ward_, drug_spec);
                ol.addsd(tm, pivas.getbox(ward_).dy, ol.gds[0]);
                pivas.setblh(ol.gds);
                gd1.Text = ol.gds[1].ToString();
                gd2.Text = ol.gds[2].ToString();
                gd3.Text = ol.gds[3].ToString();

                // showboxs(ward_);
            };
            plg.Invoke(showtplg);
            return i;
        }

        private void RecvData()
        {
            string s = streamReader.ReadLine();
            string tmp;
            int vs = 0;
            int llq;
            int plcs = 0;
            string[] wl;
            string ward = "";
            bool find;
            char[] sp = new char[] { '|' };
            int fi = 0;
            int i = 1111;
            while (s != "exit")
            {
                if (s != "ERROR::0%:0")
                {
                    //   p_lx(2, 16 , vs, Convert.ToInt32(s.Substring(10, 4)));
                    tmp = s.Substring(0, 14);
                    ward = getwad(tmp);
                    wl = ward.Split(sp);

                    ShowM1(debug_t, s + "\r");
                    if (wl[0] != "")
                    {
                        ward = wl[0];
                        find = false;
                        for (int j = 0; j < 15; j++)
                        {
                            if (xlist[j].bq == ward)
                            {
                                find = true;
                                fi = j;
                                //                 ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + fi.ToString() + ward + "\r");
                                break;
                            }
                        }
                        if (find)
                        {
                            p_lx(1, 1 + fi, vs, Convert.ToInt32(s.Substring(10, 4)));
                            ShowM1(debug_t, "|" + (1 + fi).ToString() + ward + "\r");
                            //         ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + fi.ToString() + "\r");
                        }
                        else
                        {
                            for (int f = 0; f < 15; f++)
                            {
                                if (xlist[f].bq == "")
                                {
                                    xlist[f].bq = ward;
                                    p_lx(1, 1 + f, vs, Convert.ToInt32(s.Substring(10, 4)));
                                    ShowM1(debug_t, "/" + (1 + f).ToString() + ward + "\r");
                                    break;
                                }
                            }
                        }


                        id1_t.Text = s;
                    }
                }
                s = streamReader.ReadLine();
            }
            idscan.Enabled = true;
            stopscan.Enabled = false;
            ReleaseResouce();
        }
        // 接收数据
        private void Connection1()//连接到摄像头 tcp
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(txtIP2);
                Int32 port = Int32.Parse(txtPort);
                string hostName = Dns.GetHostEntry(ipAddress).HostName;
                myclient1 = new TcpClient(hostName, port);
            }
            catch
            {
                MessageBox.Show("没有连接到摄像头!");
                return;
            }
            idscan.Enabled = false;
            stopscan.Enabled = true;
            networkStream1 = myclient1.GetStream();
            streamReader1 = new StreamReader(networkStream1);
            streamWriter1 = new StreamWriter(networkStream1);
            recvThread1 = new Thread(new ThreadStart(RecvData1));
            recvThread1.Start();
        }
        delegate void ShowMeDelegate(TextBox txtbox, string message);

        private void ShowM1(TextBox txtbox, string message)
        {
            if (txtbox.InvokeRequired)
            {
                ShowMeDelegate showMeDelegate = ShowM1;
                txtbox.Invoke(showMeDelegate, new object[] { txtbox, message });
            }
            else
            {
                txtbox.Text += message;
            }
        }
        private void RecvData1()
        {
            string s = streamReader1.ReadLine();
            string tmp;
            int vs = 0;
            int llq;
            int plcs = 0;
            string[] wl;
            string ward = "";
            bool find;
            char[] sp = new char[] { '|' };
            int fi = 0;
            int i = 1111;
            while (s != "exit")
            {
                if (s != "ERROR::0%:0")
                {
                    //   p_lx(2, 16 , vs, Convert.ToInt32(s.Substring(10, 4)));
                    tmp = s.Substring(0, 14);
                    ward = getwad(tmp);
                    wl = ward.Split(sp);

                    ShowM1(debug_t, s + "\r");
                    if (wl[0] != "")
                    {
                        ward = wl[0];
                        find = false;
                        for (int j = 0; j < 15; j++)
                        {
                            if (xlist[15 + j].bq == ward)
                            {
                                find = true;
                                fi = j;
                                //                 ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + fi.ToString() + ward + "\r");
                                break;
                            }
                        }
                        if (find)
                        {
                            p_lx(2, 16 + fi, vs, Convert.ToInt32(s.Substring(10, 4)));
                            ShowM1(debug_t, "|" + (16 + fi).ToString() + ward + "\r");
                            //         ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + fi.ToString() + "\r");
                        }
                        else
                        {
                            for (int f = 0; f < 15; f++)
                            {
                                if (xlist[15 + f].bq == "")
                                {
                                    xlist[15 + f].bq = ward;
                                    p_lx(2, 16 + f, vs, Convert.ToInt32(s.Substring(10, 4)));
                                    ShowM1(debug_t, "/" + (16 + f).ToString() + ward + "\r");
                                    break;
                                }
                            }
                        }


                        id2_t.Text = s;
                    }
                }
                s = streamReader1.ReadLine();
            }
            idscan.Enabled = true;
            stopscan.Enabled = false;
            ReleaseResouce();
        }

        private void Connection2()//连接到摄像头 tcp
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(txtIP3);
                Int32 port = Int32.Parse(txtPort);
                string hostName = Dns.GetHostEntry(ipAddress).HostName;
                myclient2 = new TcpClient(hostName, port);
            }
            catch
            {
                MessageBox.Show("没有连接到摄像头!");
                return;
            }
            idscan.Enabled = false;
            stopscan.Enabled = true;
            networkStream2 = myclient2.GetStream();
            streamReader2 = new StreamReader(networkStream2);
            streamWriter2 = new StreamWriter(networkStream2);
            recvThread2 = new Thread(new ThreadStart(RecvData2));
            recvThread2.Start();
        }
        private void RecvData2()
        {
            string s = streamReader2.ReadLine();
            string tmp;
            int vs = 0;
            int llq;
            int plcs = 0;
            string[] wl;
            string ward = "";
            bool find;
            char[] sp = new char[] { '|' };
            int fi = 0;
            int i = 1111;
            while (s != "exit")
            {
                if (s != "ERROR::0%:0")
                {
                    //   p_lx(2, 16 , vs, Convert.ToInt32(s.Substring(10, 4)));
                    tmp = s.Substring(0, 14);
                    ward = getwad(tmp);
                    wl = ward.Split(sp);

                    ShowM1(debug_t, s + "\r");
                    if (wl[0] != "")
                    {
                        ward = wl[0];
                        find = false;
                        for (int j = 0; j < 15; j++)
                        {
                            if (xlist[30 + j].bq == ward)
                            {
                                find = true;
                                fi = j;
                                //                 ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + fi.ToString() + ward + "\r");
                                break;
                            }
                        }
                        if (find)
                        {
                            p_lx(3, 31 + fi, vs, Convert.ToInt32(s.Substring(10, 4)));
                            ShowM1(debug_t, "|" + (31 + fi).ToString() + ward + "\r");
                            //         ShowM1(debug_t, DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString() + "|" + fi.ToString() + "\r");
                        }
                        else
                        {
                            for (int f = 0; f < 15; f++)
                            {
                                if (xlist[30 + f].bq == "")
                                {
                                    xlist[30 + f].bq = ward;
                                    p_lx(3, 31 + f, vs, Convert.ToInt32(s.Substring(10, 4)));
                                    ShowM1(debug_t, "/" + (31 + f).ToString() + ward + "\r");
                                    break;
                                }
                            }
                        }


                        id1_t.Text = s;
                    }
                }
                s = streamReader2.ReadLine();
            }
        }

        private void ReleaseResouce()
        {

            networkStream1.Close();
            streamReader1.Close();
            streamWriter1.Close();

            recvThread1.Abort();
            serverThread1.Abort();
            myclient1.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverThread = new Thread(new ThreadStart(Connection));
            serverThread.Start();
            serverThread1 = new Thread(new ThreadStart(Connection1));
            serverThread1.Start();
            serverThread2 = new Thread(new ThreadStart(Connection2));
            serverThread2.Start();
        }
        #endregion
        IPEndPoint remoteIpep;
        IPEndPoint localIpep;
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
                txtbox.Text = message;
            }
        }
        delegate void showpicDelegate(PictureBox pb, Image im);
        private void showpic(PictureBox pb, Image im)
        {
            if (pb.InvokeRequired)
            {
                showpicDelegate showpicDelegate = showpic;
                pb.Invoke(showpicDelegate, new object[] { pb, im });
            }
            else
            {
                pb.Image = im;
            }
        }

        private void print_b_Click(object sender, EventArgs e)
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
                id1_t.Text = mystr;
                shetlines++;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Thread showDateTimethread = new Thread(new ThreadStart(getid2));
            //该线程为后台线程
            showDateTimethread.IsBackground = true;
            //线程启动
            showDateTimethread.Start();
        }
        public string getbar()
        {
            string tmpid = ""; ;
        /*    string lon = "LON\r";   // CR is terminator
            Byte[] command = ASCIIEncoding.ASCII.GetBytes(lon);

            for (int i = 0; i < READER_COUNT; i++)
            {
                if (clinetSocketInstance[i].commandSocket != null)
                {
                    clinetSocketInstance[i].commandSocket.Send(command);
                }
                else
                {
                    MessageBox.Show(clinetSocketInstance[i].readerCommandEndPoint.ToString() + " is disconnected.");
                }
            }*/
            Byte[] recvBytes = new Byte[RECV_DATA_MAX];
            int recvSize = 0;

            for (int i = 0; i < 1; i++)
            {
                if (clinetSocketInstance[i].dataSocket != null)
                {
                    try
                    {
                        recvSize = clinetSocketInstance[i].dataSocket.Receive(recvBytes);
                    }
                    catch (SocketException)
                    {
                        recvSize = 0;
                    }
                }
                else
                {
                    MessageBox.Show(clinetSocketInstance[i].readerDataEndPoint.ToString() + " is disconnected.");
                    continue;
                }

                if (recvSize == 0)
                {
                }
                else
                {
                    recvBytes[recvSize] = 0;
                     tmpid = Encoding.GetEncoding("Shift_JIS").GetString(recvBytes);
                     ShowM1(debug_t, tmpid+"\r");
                    //         MessageBox.Show(clinetSocketInstance[i].readerDataEndPoint.ToString() + "\r\n" + Encoding.GetEncoding("Shift_JIS").GetString(recvBytes));
                }
            }
        
            return tmpid;
        }
        public string getbar1()
        {
            string tmpid = ""; ;
              string lon = "LON\r";   // CR is terminator
                Byte[] command = ASCIIEncoding.ASCII.GetBytes(lon);

                for (int i = 1; i <2; i++)
                {
                    if (clinetSocketInstance[i].commandSocket != null)
                    {
                        clinetSocketInstance[i].commandSocket.Send(command);
                    }
                    else
                    {
                        MessageBox.Show(clinetSocketInstance[i].readerCommandEndPoint.ToString() + " is disconnected.");
                    }
                }
            Byte[] recvBytes = new Byte[RECV_DATA_MAX];
            int recvSize = 0;

            for (int i = 1; i < 2; i++)
            {
                if (clinetSocketInstance[i].dataSocket != null)
                {
                    try
                    {
                        recvSize = clinetSocketInstance[i].dataSocket.Receive(recvBytes);
                    }
                    catch (SocketException)
                    {
                        recvSize = 0;
                    }
                }
                else
                {
                    MessageBox.Show(clinetSocketInstance[i].readerDataEndPoint.ToString() + " is disconnected.");
                    continue;
                }

                if (recvSize == 0)
                {
                }
                else
                {
                    recvBytes[recvSize] = 0;
                    tmpid = Encoding.GetEncoding("Shift_JIS").GetString(recvBytes);
                    ShowM1(debug_t, tmpid + "\r");
                    //         MessageBox.Show(clinetSocketInstance[i].readerDataEndPoint.ToString() + "\r\n" + Encoding.GetEncoding("Shift_JIS").GetString(recvBytes));
                }
            }

            return tmpid;
        }
        public string getbar2()
        {
            string tmpid = ""; ;
               string lon = "LON\r";   // CR is terminator
                Byte[] command = ASCIIEncoding.ASCII.GetBytes(lon);

                for (int i = 2; i < 3; i++)
                {
                    if (clinetSocketInstance[i].commandSocket != null)
                    {
                        clinetSocketInstance[i].commandSocket.Send(command);
                    }
                    else
                    {
                        MessageBox.Show(clinetSocketInstance[i].readerCommandEndPoint.ToString() + " is disconnected.");
                    }
                }
            Byte[] recvBytes = new Byte[RECV_DATA_MAX];
            int recvSize = 0;

            for (int i = 2; i <3; i++)
            {
                if (clinetSocketInstance[i].dataSocket != null)
                {
                    try
                    {
                        recvSize = clinetSocketInstance[i].dataSocket.Receive(recvBytes);
                    }
                    catch (SocketException)
                    {
                        recvSize = 0;
                    }
                }
                else
                {
                    MessageBox.Show(clinetSocketInstance[i].readerDataEndPoint.ToString() + " is disconnected.");
                    continue;
                }

                if (recvSize == 0)
                {
                }
                else
                {
                    recvBytes[recvSize] = 0;
                    tmpid = Encoding.GetEncoding("Shift_JIS").GetString(recvBytes);
                    ShowM1(debug_t, tmpid + "\r");
                    //         MessageBox.Show(clinetSocketInstance[i].readerDataEndPoint.ToString() + "\r\n" + Encoding.GetEncoding("Shift_JIS").GetString(recvBytes));
                }
            }

            return tmpid;
        }
        private void getid()
        {
            char[] sp = new char[] { '|' };
            string bar="";
            string[] wl;
            string s;
            string ward;
            while (true)
            {
                bar = getbar();
                ShowM1(debug_t,bar);
                if (bar == "") continue;
                bar = bar.Substring(0, 14);
                s = getwad(bar.Trim());
                ShowM1(debug_t, s);
                  wl = s.Split(sp);
                int fi = 0;
                bool find = false;
                if (wl[0] != "")
                {
                    ward = wl[0];
                    for (int j = 0; j < 15; j++)
                    {
                        if (xlist[j].bq == ward)
                        {
                            find = true;
                            fi = j;
                            break;
                        }
                    }
                    if (find)
                    {
                        p_lx(1, 1 + fi, 50, Convert.ToInt32(bar.Substring(10, 4)));
                        Thread.Sleep(10);
                        ShowM1(debug_t, "|" + (1 + fi).ToString() + ward + "\r");
                        Thread.Sleep(20);
                        getds();
                        Thread.Sleep(10);
                        getds();
                        
                    }
                    else
                    {
                        for (int f = 0; f < 15; f++)
                        {
                            if (xlist[f].bq == "")
                            {
                                xlist[f].bq = ward;
                                p_lx(1, 1 + f, 60, Convert.ToInt32(bar.Substring(10, 4)));
                                ShowM1(debug_t, "/////" + (1 + f).ToString() + ward + "\r");
                                Thread.Sleep(20);
                                getds();
                                Thread.Sleep(10);
                                getds();
                              
                                break;
                            }
                        }
                    }
                }
                id1_t.Text =bar;
                Thread.Sleep(5);
            }
        }
        private void getid1()
        {
            char[] sp = new char[] { '|' };
            string bar = "";
            string[] wl;
            string s;
            string ward;
            while (true)
            {
                bar = getbar1();
                ShowM1(debug_t, bar);
                if (bar == "") continue;
                bar = bar.Substring(0, 14);
                s = getwad(bar.Trim());
                ShowM1(debug_t, s);
                wl = s.Split(sp);
                int fi = 0;
                bool find = false;
                if (wl[0] != "")
                {
                    ward = wl[0];
                    for (int j = 15; j < 30; j++)
                    {
                        if (xlist[j].bq == ward)
                        {
                            find = true;
                            fi = j;
                            break;
                        }
                    }
                    if (find)
                    {
                        p_lx(2, 1 + fi, 50, Convert.ToInt32(bar.Substring(10, 4)));
                        Thread.Sleep(10);
                        ShowM1(debug_t, "|" + (1 + fi).ToString() + ward + "\r");
                        Thread.Sleep(20);
                        getds();
                        Thread.Sleep(10);
                        getds();

                    }
                    else
                    {
                        for (int f = 15; f < 30; f++)
                        {
                            if (xlist[f].bq == "")
                            {
                                xlist[f].bq = ward;
                                p_lx(2, 1 + f, 60, Convert.ToInt32(bar.Substring(10, 4)));
                                ShowM1(debug_t, "/////" + (1 + f).ToString() + ward + "\r");
                                Thread.Sleep(20);
                                getds();
                                Thread.Sleep(10);
                                getds();

                                break;
                            }
                        }
                    }
                }
                id1_t.Text = bar;
                Thread.Sleep(5);
            }
        }
        private void getid2()
        {
            char[] sp = new char[] { '|' };
            string bar = "";
            string[] wl;
            string s;
            string ward;
            while (true)
            {
                bar = getbar2();
                ShowM1(debug_t, bar);
                if (bar == "") continue;
                bar = bar.Substring(0, 14);
                s = getwad(bar.Trim());
                ShowM1(debug_t, s);
                wl = s.Split(sp);
                int fi = 0;
                bool find = false;
                if (wl[0] != "")
                {
                    ward = wl[0];
                    for (int j = 30; j < 45; j++)
                    {
                        if (xlist[j].bq == ward)
                        {
                            find = true;
                            fi = j;
                            break;
                        }
                    }
                    if (find)
                    {
                        p_lx(3, 1 + fi, 50, Convert.ToInt32(bar.Substring(10, 4)));
                        Thread.Sleep(10);
                        ShowM1(debug_t, "|" + (1 + fi).ToString() + ward + "\r");
                        Thread.Sleep(20);
                        getds();
                        Thread.Sleep(10);
                        getds();

                    }
                    else
                    {
                        for (int f = 30; f < 45; f++)
                        {
                            if (xlist[f].bq == "")
                            {
                                xlist[f].bq = ward;
                                p_lx(3, 1 + f, 60, Convert.ToInt32(bar.Substring(10, 4)));
                                ShowM1(debug_t, "/////" + (1 + f).ToString() + ward + "\r");
                                Thread.Sleep(20);
                                getds();
                                Thread.Sleep(10);
                                getds();

                                break;
                            }
                        }
                    }
                }
                id1_t.Text = bar;
                Thread.Sleep(5);
            }
        }
     

    }
}

class ClientSocket
{
    public Socket commandSocket;   // socket for command
    public Socket dataSocket;      // socket for data
    public IPEndPoint readerCommandEndPoint;
    public IPEndPoint readerDataEndPoint;

    public ClientSocket(byte[] ipAddress, int readerCommandPort, int readerDataPort)
    {
        IPAddress readerIpAddress = new IPAddress(ipAddress);
        readerCommandEndPoint = new IPEndPoint(readerIpAddress, readerCommandPort);
        readerDataEndPoint = new IPEndPoint(readerIpAddress, readerDataPort);
        commandSocket = null;
        dataSocket = null;
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
    public void setpi(int[] gd)
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
#region  // Oracle
// dataGridView1.AllowUserToAddRows = true;
//      如果选中DATAGRIDVIEW某行其中某个单元格（列）是空值，进行相应的特殊处理
//       data source,user id,password不区分大小写
//       OracleConnection con = new OracleConnection("data Source=orcl;user id=scott;password=system");
//       OracleCommand comm = new OracleCommand();
//       comm.Connection = con;
//       comm.CommandText = "select deptno,dname,loc from dept";
//       comm.CommandType = CommandType.Text;
//      da= new OracleDataAdapter(comm);
//       ds1 = new DataSet();
//       oda.Fill(ds1);
//      dataGridView1.DataSource = ds1.Tables[0];
#endregion

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
    #region
    /*
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

        public byte[] getplc(string add, int count, bool dw)
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

        public byte[] setplc(string add, int[] dat)
        {
            byte[] bytedat = new byte[dat.Length * 2];
            byte[] tmp = new byte[2];
            for (int i = 0; i < dat.Length; i++)
            {
                tmp = wordtobyte(dat[i]);
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
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(ip), outport);

            UdpClient udp = new UdpClient(locip);
            udp.Send(dat, dat.Length, remoteIpep);
            // ret = udp.Receive(ref locip);
            udp.Close();
            udp = null;
            Thread.Sleep(3);

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
    */
    #endregion

}




