using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Media;
using System.Resources;
using System.Globalization;
using System.Net;
using Ats.Core;
using Ats.Indicators;

namespace CTP_交易
{
    public partial class FormTrade : Form
    {
        class ObjectAndKey
        {
            /// <summary>
            /// 添加到listview的实例
            /// </summary>
            public object Object { get; set; }
            /// <summary>
            /// 此listviewitem的Key
            /// </summary>
            public string Key { get; set; }
            /// <summary>
            /// 被添加listviewitem的HFListView
            /// </summary>
            //public HFListView LV { get; set; }
            public ObjectAndKey(object obj, string key)
            {
                this.Object = obj;
                this.Key = key;
                //this.LV = lv;
            }
        }

        enum EnumProgessState
        {
            OnMdConnected, OnMdDisConnected,	//行情连接/断开
            Connect, OnConnected, DisConnect, OnDisConnect, Login, OnLogin, Logout, OnLogout,	//连接/登录/注销
            QrySettleConfirmInfo, OnQrySettleConfirmInfo, QrySettleInfo, OnQrySettleInfo, SettleConfirm, OnSettleConfirm,	//结算
            QryInstrument, OnQryInstrument, QryOrder, OnQryOrder, QryTrade, OnQryTrade, QryPosition, OnQryPosition, QryAccount, OnQryAccount,	//查询
            QryParkedOrder, OnQryParkedOrder, QryParkedOrderAction, OnQryParkedOrderAction,	//预埋
            QryDepthMarketData, OnQryDepthMarketData, //深度行情
            QryPositionDetail, OnQryPositionDetail,	//查持仓明细
            Other, //其它
            OnError, //有错误响应
            OrderInsert, OnErrOrderInsert, OnRtnOrder, OnRtnTrade,	//下单
            OrderAction, OnErrOrderAction, OnOrderAction,		//撤单
            OnRtnTradingNotice, //交易信息
            OnRtnInstrumentStatus, //合约状态
            RemovePackedOrder, OnRemovePackedOrder, RemovePackedOrderAction, OnRemovePackedOrderAction, ParkedOrder, OnParkedOrder, ParkedOrderAction, OnParkedOrderAction, //预埋
            QuickClose,//快速平仓
            QuickLock,  //锁仓
            UpdateUserPassword, OnUpdateUserPassword, UpdateAccountPassword, OnUpdateAccountPassword,	//修改密码
            FutureToBank, OnFutureToBank, BankToFuture, OnBankToFuture, QryBankAccountMoney, OnQryBankAccountMoney,//银期
            QuickCover,
            OnQryInstrumentMarginRate, QryInstrumentMarginRate
        }

        public FormTrade()
        {
            InitializeComponent();
        }

        private string strInfo = "";				//结算信息
        private FormLoginTrade ul = new FormLoginTrade();
        private TradeApi tradeApi = null;
        private MdApi mdApi = null;//new MdApi();		//接口
        private bool freshOrderPrice = true;		//是否刷新价格
        private SoundPlayer snd = null;			//声音
        private TimeSpan tsSHFE = TimeSpan.Zero;
        private TimeSpan tsCZCE = TimeSpan.Zero;
        private TimeSpan tsDCE = TimeSpan.Zero;
        private TimeSpan tsCFFEX = TimeSpan.Zero;			//交易所与本地时间之差
        private ListViewItem lviCovert;

        private Action actionFreshExchengeTime = null;
        private Thread threadFreshExchangeTime = null;		//刷新交易所时间
        private Thread threadFreshMarketData = null;		//刷新行情的线程
        private Thread threadQry = null;					//执行查询队列
        private DataTable dtMarketData = new DataTable("MarketData");				//行情用表
        private DataTable dtInstruments = new DataTable("Instruments");			//合约用表
        private List<CThostFtdcDepthMarketDataField> listMarketDatas = new List<CThostFtdcDepthMarketDataField>();
        //private List<DataRow> listDataRowWaitQryMarginRate = new List<DataRow>();//待查保证金的数据行
        private List<QryOrder> listQry = new List<QryOrder>();	//待查询的队列
        private bool apiIsBusy = false;		//接口是否处于查询中
        private string instrument4QryRate = null;//正在查询手续费的合约:因有时返回合约类型,而加以判断
        private bool isQryHistoryTrade = false;		//程序是否启动完成

        class QryOrder
        {
            public QryOrder(EnumQryOrder _qryType, string _params = null, object _field = null)
            { this.QryOrderType = _qryType; Params = _params; Field = _field; }
            public EnumQryOrder QryOrderType { get; set; }
            public string Params = null;
            public object Field = null;
        }
        enum EnumQryOrder
        {
            QryOrder, QryTrade, QryIntorverPosition, QryInstrumentCommissionRate, QryTradingAccount, QryParkedOrderAction,
            QryParkedOrder, QryContractBank, QueryBankAccountMoneyByFuture, QrySettlementInfo,
            QryHistoryTrade,
            QryTransferSerial
        }
        //查询列表
        void execQryList()
        {
            while (true)
            {
                if (apiIsBusy || this.listQry.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }
                QryOrder qry = listQry[0];
                Thread.Sleep(1000);
                apiIsBusy = true;
                switch (qry.QryOrderType)
                {
                    case EnumQryOrder.QryInstrumentCommissionRate: //手续费
                        this.instrument4QryRate = qry.Params;		//正在查询的合约
                        this.tradeApi.QryInstrumentCommissionRate(qry.Params);
                        break;
                    case EnumQryOrder.QryIntorverPosition:	//持仓
                        this.tradeApi.QryInvestorPosition(qry.Params);
                        break;
                    case EnumQryOrder.QryOrder: //查委托
                        this.tradeApi.QryOrder();
                        break;
                    case EnumQryOrder.QryParkedOrder:	//查预埋
                        this.tradeApi.ReqQryParkedOrder();
                        break;
                    case EnumQryOrder.QryParkedOrderAction:
                        this.tradeApi.ReqQryParkedOrderAction();
                        break;
                    case EnumQryOrder.QrySettlementInfo:
                        this.tradeApi.QrySettlementInfo(qry.Params);
                        break;
                    case EnumQryOrder.QryTrade:
                        this.tradeApi.QryTrade();
                        break;
                    case EnumQryOrder.QryHistoryTrade:
                        this.tradeApi.QryTrade(this.dateTimePickerStart.Value, this.dateTimePickerEnd.Value);	//查历史成交
                        break;
                    case EnumQryOrder.QryTradingAccount:
                        this.tradeApi.QryTradingAccount();
                        break;
                    case EnumQryOrder.QryTransferSerial:
                        this.tradeApi.ReqQryTransferSerial(qry.Params);
                        break;
                    case EnumQryOrder.QueryBankAccountMoneyByFuture:
                        this.tradeApi.ReqQueryBankAccountMoneyByFuture((CThostFtdcReqQueryAccountField)qry.Field);
                        break;
                    default:
                        apiIsBusy = false;	//恢复正常查询
                        break;
                }
                listQry.Remove(qry);
            }
        }
        //启动:初始化
        FileStream log_fs;
        StreamWriter log_sw;
        private void FormTrade_Load(object sender, EventArgs e)
        {
            ilist = new List<Instrument>();
            for (int x = 0; x < idlist.Length; x++)
            {
                ilist.Add(new Instrument(idlist[x]));
            }
            log_fs = new FileStream(@"c:\ls\log.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            log_sw = new StreamWriter(log_fs);
            this.Size = Properties.Settings.Default.FormSize;	//设置大小
            if (this.panelTop.Visible)
            {
                this.panelTop.Height = Properties.Settings.Default.TopHeigh;	//行情栏有显示时设置高度
            }
            //if (Properties.Settings.Default.TopHeigh == null)
            //	Properties.Settings.Default.TopHeigh = this.panelTop.Height;	//行情栏高
            if (Properties.Settings.Default.Instruments == null)				//存取自选合约
                Properties.Settings.Default.Instruments = new StringCollection();
            if (Properties.Settings.Default.Log == null)						//存取日志
                Properties.Settings.Default.Log = new StringCollection();
            if (Properties.Settings.Default.CustomVolume == null)				//自定义手数
                Properties.Settings.Default.CustomVolume = new StringCollection();
            if (Properties.Settings.Default.Servers == null || Properties.Settings.Default.Servers.Count == 0)
            {
                Properties.Settings.Default.Servers = new StringCollection();	//服务器列表

                Properties.Settings.Default.Servers.Add("上期技术(模拟)|9999|tcp://180.168.146.187:10000|tcp://180.168.146.187:10010|351962|");
            }
            Properties.Settings.Default.Servers.Clear();
            Properties.Settings.Default.Servers.Add("东航|7070|tcp://116.236.239.136:41205|tcp://116.236.239.136:41213|");
            actionFreshExchengeTime = new Action(this.freshTime);				//刷新时间代理

            this.tabControl1.SelectedIndex = 4;					//进入自选合约
            this.comboBoxOffset.SelectedIndex = 0;
            this.comboBoxDirector.SelectedIndex = 0;
            this.comboBoxTransferType.SelectedIndex = 0;
            this.webBrowser1.Url = new System.Uri(AppDomain.CurrentDomain.BaseDirectory + @"Resources\软件帮助_HF.mht");
            ul.btnLogin.Click += new EventHandler(btnLogin_Click); //登录画面:按钮事件
            ul.btnExit.Click += new EventHandler(btnExit_Click);

            #region 定义合约表/数据表
            //合约/名称/交易所/合约数量乘数/最小波动/多头保证金率/空头保证金率/限价单下单最大量/最小量
            //this.dtInstruments = new DataTable("instruments");
            #region dtInstruments
            this.dtInstruments.Columns.Add("合约", string.Empty.GetType());
            this.dtInstruments.Columns.Add("名称", string.Empty.GetType());
            this.dtInstruments.Columns.Add("交易所", string.Empty.GetType());
            this.dtInstruments.Columns.Add("合约数量", int.MinValue.GetType());
            this.dtInstruments.Columns.Add("最小波动", double.NaN.GetType());
            this.dtInstruments.Columns.Add("保证金-多", double.NaN.GetType());
            this.dtInstruments.Columns.Add("保证金-空", double.NaN.GetType());
            this.dtInstruments.Columns.Add("手续费", double.NaN.GetType());	//单独查询获得
            this.dtInstruments.Columns.Add("手续费-平仓", double.NaN.GetType());	//
            this.dtInstruments.Columns.Add("最大下单量-限", int.MinValue.GetType());
            this.dtInstruments.Columns.Add("最小下单量-限", int.MinValue.GetType());
            //this.dtInstruments.Columns.Add("(经纪)保证金-多", double.NaN.GetType());	//单独查询获得:重新计算后覆盖上面的"保证金"
            //this.dtInstruments.Columns.Add("(经纪)保证金-空", double.NaN.GetType());	//
            //this.dtInstruments.Columns.Add("相对交易所", int.MinValue.GetType());
            this.dtInstruments.Columns.Add("自选", true.GetType());
            this.dtInstruments.Columns["自选"].DefaultValue = false;
            this.dtInstruments.Columns.Add("套利", true.GetType());
            this.dtInstruments.Columns["套利"].DefaultValue = false;
            this.dtInstruments.PrimaryKey = new DataColumn[] { this.dtInstruments.Columns["合约"] };	//主键
            setDataGridViewOfInstrument();								//设置所有合约显示
            #endregion
            //合约/名称/交易所/最新价/涨跌/涨幅/现手/总手/持仓/仓差/买价/买量/卖价/卖量/均价/最高/最低/涨停/跌停/开盘/昨结/时间/时间差/自选
            this.dtMarketData = new DataTable("marketdata");
            #region dtMarketData
            this.dtMarketData.Columns.Add("合约", string.Empty.GetType());
            this.dtMarketData.Columns.Add("名称", string.Empty.GetType());
            this.dtMarketData.Columns.Add("交易所", string.Empty.GetType());
            this.dtMarketData.Columns.Add("最新价", double.NaN.GetType());
            this.dtMarketData.Columns.Add("涨跌", double.NaN.GetType());
            this.dtMarketData.Columns.Add("涨幅", double.NaN.GetType());
            this.dtMarketData.Columns.Add("现手", int.MinValue.GetType());
            this.dtMarketData.Columns.Add("总手", int.MinValue.GetType());
            this.dtMarketData.Columns.Add("持仓", double.NaN.GetType());
            this.dtMarketData.Columns.Add("仓差", double.NaN.GetType());
            this.dtMarketData.Columns.Add("买价", double.NaN.GetType());
            this.dtMarketData.Columns.Add("买量", double.NaN.GetType());
            this.dtMarketData.Columns.Add("卖价", double.NaN.GetType());
            this.dtMarketData.Columns.Add("卖量", double.NaN.GetType());
            this.dtMarketData.Columns.Add("均价", double.NaN.GetType());
            this.dtMarketData.Columns.Add("最高", double.NaN.GetType());
            this.dtMarketData.Columns.Add("最低", double.NaN.GetType());
            this.dtMarketData.Columns.Add("涨停", double.NaN.GetType());
            this.dtMarketData.Columns.Add("跌停", double.NaN.GetType());
            this.dtMarketData.Columns.Add("开盘", double.NaN.GetType());
            this.dtMarketData.Columns.Add("昨结", double.NaN.GetType());
            this.dtMarketData.Columns.Add("时间", string.Empty.GetType());
            this.dtMarketData.Columns.Add("时间差", double.NaN.GetType());
            this.dtMarketData.Columns.Add("自选", true.GetType());
            this.dtMarketData.Columns.Add("套利", true.GetType());
            this.dtMarketData.Columns["自选"].DefaultValue = false;
            this.dtMarketData.Columns["套利"].DefaultValue = false;
            this.dtMarketData.PrimaryKey = new DataColumn[] { this.dtMarketData.Columns["合约"] };	//主键
            //this.setDataGridViewOfMarketData();	
            //设置所有行情栏=============假死,放在ul前假死
            #endregion
            #endregion

            this.Hide();	//显示登录画面
            if (ul.ShowDialog() == DialogResult.OK)
            {
                if (strInfo != string.Empty)	//在userlogin中调用qrysettleinfo确保此条件成立
                {
                    //显示确认结算窗口
                    using (SettleInfo info = new SettleInfo())
                    {
                        info.richTextInfo.Text = strInfo;
                        if (info.ShowDialog(this) != DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                }
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.SettleConfirm, "确认结算...");
                tradeApi.SettlementInfoConfirm();	//确认结算
                Thread.Sleep(2000);
                this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryIntorverPosition, null));	//持仓
                this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryTrade, null));	//成交
                this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryOrder, null));//委托

                this.setDataGridViewOfMarketData();						//设置所有行情栏=============假死,放在ul前假死
                //行情接收线程
                mdApi = new MdApi("035135", "625117", "9999", "tcp://180.168.146.187:10010");

                //      mdApi = new MdApi("8703980", "WM3319tg", "7070", "tcp://116.236.239.136:41213");
                mdApi.OnFrontConnected += new MdApi.FrontConnected(mdApi_OnFrontConnected);
                mdApi.OnFrontDisconnected += new MdApi.FrontDisconnected(mdApi_OnFrontDisconnected);
                mdApi.OnRspUserLogin += new MdApi.RspUserLogin(mdApi_OnRspUserLogin);
               // mdApi.OnRspSubMarketData += new CTPMdApi.MdApi.RspSubMarketData(mdApi_OnRspSubMarketData);
                mdApi.OnRtnDepthMarketData += new MdApi.RtnDepthMarketData(mdApi_OnRtnDepthMarketData);
                mdApi.OnRspUnSubMarketData += new MdApi.RspUnSubMarketData(mdApi_OnRspUnSubMarketData);
                mdApi.Connect();

                threadFreshExchangeTime = new Thread(new ThreadStart(this.freshExchangeTime));
                threadFreshExchangeTime.Start();	//刷新各交易所时间
                this.threadFreshMarketData = new Thread(new ThreadStart(this.freshMarketData));	//
                this.threadFreshMarketData.Start();	//刷新行情栏
                this.threadQry = new Thread(new ThreadStart(execQryList));
                this.threadQry.Start();				//刷新查询

                this.dataGridViewSHFE.Sort(this.dataGridViewSHFE.Columns["合约"], ListSortDirection.Ascending);
                this.dataGridViewCZCE.Sort(this.dataGridViewCZCE.Columns["合约"], ListSortDirection.Ascending);
                this.dataGridViewDCE.Sort(this.dataGridViewDCE.Columns["合约"], ListSortDirection.Ascending);
                this.dataGridViewCFFEX.Sort(this.dataGridViewCFFEX.Columns["合约"], ListSortDirection.Ascending);
                this.dataGridViewSelected.Sort(this.dataGridViewSelected.Columns["合约"], ListSortDirection.Ascending);
                this.dataGridViewArbitrage.Sort(this.dataGridViewArbitrage.Columns["合约"], ListSortDirection.Ascending);
                this.dataGridViewInstruments.Sort(this.dataGridViewInstruments.Columns["合约"], ListSortDirection.Ascending);
                this.dataGridViewInstrumentsSelected.Sort(this.dataGridViewInstrumentsSelected.Columns["合约"], ListSortDirection.Ascending);
                this.comboBoxInstrument.Sorted = true;

                this.Text = tradeApi.InvestorID + "-" + this.Text;		//窗口名称+帐号
                this.Show();		//显示主窗口
                this.MaximizeBox = this.panelTop.Visible;


            }
            else
            {
                this.Close();
            }
        }
        /// <summary>
        /// class id
        /// </summary>
        ///  
        public enum 结构 { 顶, 底, 中继, 起点 }
        public class Instrument : Strategy
        {
            public struct 成交汇总
            {
                public double 涨幅;
                public double 增仓;
                public double 成交;
                public double 价格;
                public double 成交买量;
                public double 成交卖量;
                public int 空加;
                public int 多加;
                public int 空减;
                public int 多减;
            }
            public 成交汇总 plst;
            public struct 盘口
            {
                public double 涨幅;
                public double 增仓;

                public double 成交买量;
                public double 成交卖量;
                public int 空加;
                public int 多加;


                public double 震幅;

                public double 均价;
                public double 分钟20ma;
                public int 分转折次数;
                public int t转折次数;
                public double 均量;

                public int 连涨跳数;
                public int 连跌跳数;
                public int zzt;
                public int zxt;
                public int 上一跳;
            }
            public 盘口 pk;
            public List<double> 外盘, 内盘;
            int cj = 0;
            public void 计算盘口(CThostFtdcDepthMarketDataField t)
            {

                tickcount++;
                if (p.LastPrice != 0)
                {
                    cj = t.Volume - p.Volume;
                    pk.均量 = t.Volume / tickcount;
                    if (t.LastPrice > p.LastPrice)
                    {
                        if (pk.连跌跳数 >= 0)
                        {
                            pk.连涨跳数++;
                            pk.zzt++;
                            pk.连跌跳数 = 0;
                        }
                        pk.上一跳 = 1;
                        pk.成交买量 += cj;
                    }
                    if (t.LastPrice < p.LastPrice)
                    {
                        if (pk.连涨跳数 >= 0)
                        {
                            pk.连跌跳数++;
                            pk.zxt++;
                            pk.连涨跳数 = 0;
                        }
                        pk.上一跳 = -1;
                        pk.成交卖量 += cj;
                    }
                }
                else { }
                pk.涨幅 = t.LastPrice - t.PreClosePrice;
                pk.增仓 = t.OpenInterest - p.OpenInterest;
                p = t;
                Print("均价:" + tickcount.ToString() + "volume:" + pk.均量.ToString());
            }

            #region
         

            public struct 笔
            {
                public double high;
                public double low;
                public double o;
                public Bar ba;
                public 结构 zd;
                public int index;

            }
            public struct 走势
            {
                public double 涨幅;
                public double high;
                public double low;
                public bool z;
                public int 仓加;
                public double vol;
                public double 平均涨跌幅;
                public double 平均成交量;
            }
            public struct 多空开平
            {
                public double 多量;
                public double 空量;
                public double 开多数;
                public double 开空数;
                public double 平多数;
                public double 平空数;
            }
            #endregion
            #region
            private string k_path = "c:\\1m";//分钟线文件夹
            private string t_path = "c:\\tick";//分笔文件夹
            string kext = ".min";//分钟扩展名
            string text = ".tk";//分笔扩展名
            private DateTime d_tmp = DateTime.MinValue;
            private int f_t = 0;

            private double k_h = 0;
            private double k_l = 0;
            private string id;//品种
            public int tickcount;
            public CThostFtdcDepthMarketDataField p;
            public List<CThostFtdcDepthMarketDataField> tick;//tick列
            public List<CThostFtdcDepthMarketDataField> kmak;//tick计算K线
            private BarSeries bs;//1M K线
            private BarSeries tmpbs;//转折计算流
            private List<笔> bl;//转折
            private BarSeries 二次K;
            private BarSeries 三次K;
            public List<笔> bl2;
            public List<笔> bl3;
            public FileStream kfs;  //文件流
            public BinaryWriter kbw;//二进制流
            public FileStream tfs;//tick文件流
            public BinaryWriter tbw;//
            public int 今买量;//今天多单
            public int 今卖量;//今天空单
            public int 昨买量;//昨单
            public int 昨卖量;//昨单
            public double 买成本价;
            public double 卖成本价;
            public string 市场;

            public MA ma20;
            public MACD macd;
            #endregion
            public void read_tick()
            {
                FileStream fs;
                CThostFtdcDepthMarketDataField t_b;
                if (!File.Exists(t_path + "\\" + id + init_tick_file() + text)) return;
                fs = new FileStream(t_path + "\\" + id + init_tick_file() + text, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader br = new BinaryReader(fs);
                try
                {
                    while (true)
                    {
                        t_b = new CThostFtdcDepthMarketDataField();
                        t_b.AskPrice1 = br.ReadDouble();
                        t_b.AskVolume1 = br.ReadInt32();
                        t_b.BidPrice1 = br.ReadDouble();
                        t_b.BidVolume1 = br.ReadInt32();
                        t_b.PreClosePrice = br.ReadDouble();
                        t_b.LastPrice = br.ReadDouble();
                        t_b.Volume = br.ReadInt32();
                        t_b.OpenInterest = br.ReadDouble();
                        t_b.OpenPrice = br.ReadDouble();
                        t_b.LowestPrice = br.ReadDouble();
                        t_b.HighestPrice = br.ReadDouble();
                        //        t_b.UpperLimitPrice = br.ReadDouble();
                        //      t_b.LowerLimitPrice = br.ReadDouble();
                        t_b.UpdateTime = br.ReadString();
                        tick.Add(t_b);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\n读取结束！");
                }
                fs.Close();
                br.Close();
            }//读分笔
            public void read_mk()
            {
                FileStream fs;
                Bar t_b;
                if (!File.Exists(t_path + "\\" + id + init_tick_file() + text)) return;
                fs = new FileStream(k_path + "\\" + id + kext, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (fs.Length == 0) return;
                BinaryReader br = new BinaryReader(fs);
                byte[] bt = br.ReadBytes(1344);
                try
                {
                    while (true)
                    {
                        t_b = new Bar();
                        t_b.BeginTime = new DateTime(br.ReadInt64());
                        t_b.EndTime = new DateTime(br.ReadInt64());
                        br.ReadInt64();
                        t_b.Open = br.ReadDouble();
                        t_b.Close = br.ReadDouble();
                        t_b.High = br.ReadDouble();
                        t_b.Low = br.ReadDouble();
                        br.ReadDouble();
                        t_b.Volume = br.ReadDouble();
                        t_b.Turnover = br.ReadDouble();
                        t_b.OpenInterest = br.ReadDouble();
                        bs.Add(t_b);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\n读取结束！");
                }
                fs.Close();
                br.Close();
                if (bs.Count > 3600)
                {
                    bs.Ago(bs.Count - 1);
                    for (int i = 3600; i < bs.Count; i++)
                        bs.RemoveAt(0);
                }
            }//读K线
            public void closef()
            {
                tbw.Close();
                tfs.Close();
                kbw.Close();
                kfs.Close();
            }//文件关闭
            public void 开盘()
            {

            }
            public void 收盘()
            {
            }
            public double z1;
            public double z2;
            public double z3;
            public double z4;
            public double z5;
            public void 计算多空()
            {
                if (bl.Count < 5) return;
                double 最高, 最低;

                if (bl[bl.Count - 1].zd == 结构.底)
                {
                    z1 = bl[bl.Count - 1].ba.Low;
                    z2 = bl[bl.Count - 2].ba.High;
                    z3 = bl[bl.Count - 3].ba.Low;
                    z4 = bl[bl.Count - 4].ba.High;
                    z5 = bl[bl.Count - 5].ba.Low;
                }
                else
                {
                    z1 = bl[bl.Count - 1].ba.High;
                    z2 = bl[bl.Count - 2].ba.Low;
                    z3 = bl[bl.Count - 3].ba.High;
                    z4 = bl[bl.Count - 4].ba.Low;
                    z5 = bl[bl.Count - 5].ba.High;
                }
                最高 = bl[bl.Count - 5].ba.High;
                最低 = bl[bl.Count - 5].ba.Low;
                for (int i = bl.Count - 5; i < bl.Count; i++)
                {
                    最高 = 最高 > bl[i].ba.High ? 最高 : bl[i].ba.High;
                    最低 = 最低 > bl[i].ba.Low ? 最低 : bl[i].ba.Low;
                }


                if (z1 > z2) { }
            }

            public void 指标()
            {
                ma20 = new MA(bs, 20);//20均线
                AddIndicator(ma20);
                macd = new MACD(bs);//macd
                AddIndicator(macd);
            }
            public void onbar(Bar bar)
            {
                BinaryWriter kw = null;
                生成(bs);
                生成二级(this.bl2, this.二次K);
                生成二级(this.bl3, this.三次K);
                指标();
                kw = kbw;
                kw.Write(bar.BeginTime.ToBinary()); kw.Write(bar.EndTime.ToBinary()); kw.Write(bar.EndTime.ToBinary());
                kw.Write(bar.Open); kw.Write(bar.Close); kw.Write(bar.High); kw.Write(bar.Low);
                kw.Write(bar.Close); kw.Write(bar.Volume); kw.Write(bar.Turnover); kw.Write(bar.OpenInterest);
            }//1分钟K线生成后

            public int bic;
            public void 生成(BarSeries bar)
            {
                this.tmpbs.Clear();
                this.bl.Clear();
                for (int i = 0; i < bar.Count; i++)
                {
                    if (i == 0)
                    {
                    }
                    else
                    {
                        tmpbs.Add(bar[i]);
                        包含计算();
                    }
                }
            }
            public void 生成二级(List<笔> bl, BarSeries 次K)
            {

                
                this.bl2.Clear();
                Bar tmpbar = new Bar();
                if (bl.Count == 0) return;
                笔 b1 = bl[0];
                bool dd1, dd2;
                dd1 = b1.zd == 结构.底 ? true : false;

                笔 b2;
                for (int i = 0; i < bl.Count; i++)
                {
                    b2 = bl[i];
                    dd2 = b2.zd == 结构.底 ? true : false;
                    if (dd1)
                    {
                        tmpbar.High = b2.high;
                        tmpbar.Low = b1.low;
                        tmpbar.Open = b1.low;
                        tmpbar.Close = b2.high;
                    }
                    else
                    {
                        tmpbar.High = b1.high;
                        tmpbar.Low = b2.low;
                        tmpbar.Open = b2.low;
                        tmpbar.Close = b1.high;
                    }
                    次K.Add(tmpbar);
                    dd1 = dd2;
                }
                if (次K.Count == 0) return;
                包含计算2(bl2, 次K);
            }
            public void 包含计算2(List<笔> bl, BarSeries tmpbs)
            {
                Bar bar1, bar2;
                int ind = tmpbs.Count - 1;
                int bh1, bh2, bh3;
                int bl1, bl2, bl3;
                if (ind > 2)
                {
                    bar1 = tmpbs[ind];
                    bar2 = tmpbs[ind - 1];
                    bh1 = MathHelper.ConvertToInt(bar1.High);
                    bh2 = MathHelper.ConvertToInt(bar2.High);
                    bl1 = MathHelper.ConvertToInt(bar1.Low);
                    bl2 = MathHelper.ConvertToInt(bar2.Low);
                    if ((bh1 >= bh2) && (bl1 <= bl2))
                    {
                        if (bl.Count == 0) { return; }
                        if (bl.Last().zd == 结构.底)
                        {
                            bar1.Low = bar2.Low;
                            tmpbs.Remove(bar2);
                        }
                        if (bl.Last().zd == 结构.顶)
                        {
                            bar1.High = bar2.High;
                            tmpbs.Remove(bar2);
                        }
                        return;
                    }
                    if ((bh1 <= bh2 && (int)bl1 >= bl2))
                    {
                        if (bl.Count == 0) { return; }
                        if (bl.Last().zd == 结构.底)
                        {
                            bar2.Low = bar1.Low;
                            tmpbs.Remove(bar1);
                        }
                        if (bl.Last().zd == 结构.顶)
                        {
                            bar2.High = bar1.High;
                            tmpbs.Remove(bar1);
                        }
                    }
                }
                Bar ba1, ba2, ba3;
                笔 bi = new 笔();
                ind = tmpbs.Count - 1;
                if (ind > 7)
                {
                    ba1 = tmpbs[ind - 2];
                    ba2 = tmpbs[ind - 1];
                    ba3 = tmpbs[ind];
                    bh1 = MathHelper.ConvertToInt(ba1.High);
                    bh2 = MathHelper.ConvertToInt(ba2.High);
                    bh3 = MathHelper.ConvertToInt(ba3.High);
                    bl1 = MathHelper.ConvertToInt(ba1.Low);
                    bl2 = MathHelper.ConvertToInt(ba2.Low);
                    bl3 = MathHelper.ConvertToInt(ba3.Low);
                    if ((bh2 > bh1 && bh2 > bh3) && (bl2 > bl1 && bl2 > bl3))
                    {
                        if (bl.Count == 0)
                        {
                            bi.zd = 结构.顶;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.顶 && bh1 > MathHelper.ConvertToInt(bl.Last().high))
                        {
                            bl.Remove(bl.Last());
                            bi.zd = 结构.顶;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.底 && ind > bl.Last().index + 2 && bh1 > MathHelper.ConvertToInt(bl.Last().high))
                        {
                            bi.zd = 结构.顶;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                    }
                    if ((bh2 < bh1 && bh2 < bh3) && (bl2 < bl1 && bl2 < bl3))
                    {
                        if (bl.Count == 0)
                        {
                            bi.zd = 结构.底;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.底 && bl1 < MathHelper.ConvertToInt(bl.Last().low))
                        {
                            bl.Remove(bl.Last());
                            bi.zd = 结构.底;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.顶 && ind > bl.Last().index + 2 && bl1 < MathHelper.ConvertToInt(bl.Last().low))
                        {
                            bi.zd = 结构.底;
                            bi.o = 0;
                            bi.high = bh1;
                            bi.low = bl1;
                            bi.ba = ba1;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                    }
                }
            }
            public void 包含计算()
            {
                Bar bar1, bar2;
                int ind = tmpbs.Count - 1;
                int bh1, bh2, bh3;
                int bl1, bl2, bl3;
                if (ind > 2)
                {
                    bar1 = tmpbs[ind];
                    bar2 = tmpbs[ind - 1];
                    bh1 = MathHelper.ConvertToInt(bar1.High);
                    bh2 = MathHelper.ConvertToInt(bar2.High);
                    bl1 = MathHelper.ConvertToInt(bar1.Low);
                    bl2 = MathHelper.ConvertToInt(bar2.Low);
                    if ((bh1 >= bh2) && (bl1 <= bl2))
                    {
                        if (bl.Count == 0) { return; }
                        if (bl.Last().zd == 结构.底)
                        {
                            bar1.Low = bar2.Low;
                            tmpbs.Remove(bar2);
                        }
                        if (bl.Last().zd == 结构.顶)
                        {
                            bar1.High = bar2.High;
                            tmpbs.Remove(bar2);
                        }
                        return;
                    }
                    if ((bh1 <= bh2 && (int)bl1 >= bl2))
                    {
                        if (bl.Count == 0) { return; }
                        if (bl.Last().zd == 结构.底)
                        {
                            bar2.Low = bar1.Low;
                            tmpbs.Remove(bar1);
                        }
                        if (bl.Last().zd == 结构.顶)
                        {
                            bar2.High = bar1.High;
                            tmpbs.Remove(bar1);
                        }
                    }
                }
                Bar ba1, ba2, ba3;
                笔 bi = new 笔();
                ind = tmpbs.Count - 1;
                if (ind > 7)
                {
                    ba1 = tmpbs[ind - 2];
                    ba2 = tmpbs[ind - 1];
                    ba3 = tmpbs[ind];
                    bh1 = MathHelper.ConvertToInt(ba1.High);
                    bh2 = MathHelper.ConvertToInt(ba2.High);
                    bh3 = MathHelper.ConvertToInt(ba3.High);
                    bl1 = MathHelper.ConvertToInt(ba1.Low);
                    bl2 = MathHelper.ConvertToInt(ba2.Low);
                    bl3 = MathHelper.ConvertToInt(ba3.Low);
                    if ((bh2 > bh1 && bh2 > bh3) && (bl2 > bl1 && bl2 > bl3))
                    {
                        if (bl.Count == 0)
                        {
                            bi.zd = 结构.顶;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.顶 && bh1 > MathHelper.ConvertToInt(bl.Last().high))
                        {
                            bl.Remove(bl.Last());
                            bi.zd = 结构.顶;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.底 && ind > bl.Last().index + 4 && bh1 > MathHelper.ConvertToInt(bl.Last().high))
                        {
                            bi.zd = 结构.顶;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                    }
                    if ((bh2 < bh1 && bh2 < bh3) && (bl2 < bl1 && bl2 < bl3))
                    {
                        if (bl.Count == 0)
                        {
                            bi.zd = 结构.底;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.底 && bl1 < MathHelper.ConvertToInt(bl.Last().low))
                        {
                            bl.Remove(bl.Last());
                            bi.zd = 结构.底;
                            bi.o = 0;
                            bi.high = bh2;
                            bi.low = bl2;
                            bi.ba = ba2;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                        if (bl.Last().zd == 结构.顶 && ind > bl.Last().index + 4 && bl1 < MathHelper.ConvertToInt(bl.Last().low))
                        {
                            bi.zd = 结构.底;
                            bi.o = 0;
                            bi.high = bh1;
                            bi.low = bl1;
                            bi.ba = ba1;
                            bi.index = ind - 1;
                            bl.Add(bi);
                        }
                    }
                }
            }
            public int 赢亏计算()
            {
                return 0;
            }
            public void make_k(CThostFtdcDepthMarketDataField pDepthMarketData)
            {
                Bar tmp_bar;
                string[] sp = null;
                sp = pDepthMarketData.UpdateTime.Split(':');
                int c_t = Convert.ToInt32(sp[0] + sp[1]);
                if (c_t > f_t)
                {
                    f_t = c_t;
                    if (kmak.Count > 2)
                    {
                        tmp_bar = new Bar();
                        tmp_bar.Volume = kmak[kmak.Count - 1].Volume - kmak[0].Volume;
                        tmp_bar.OpenInterest = kmak[kmak.Count - 1].OpenInterest;
                        tmp_bar.Open = kmak[0].LastPrice;
                        tmp_bar.Close = kmak[kmak.Count - 1].LastPrice;
                        tmp_bar.High = k_h;
                        tmp_bar.Low = k_l;
                        tmp_bar.Turnover = kmak[kmak.Count - 1].Turnover - kmak[0].Turnover;
                        tmp_bar.BeginTime = Convert.ToDateTime(kmak[0].UpdateTime);
                        tmp_bar.EndTime = Convert.ToDateTime(kmak[kmak.Count - 1].UpdateTime);
                        bs.Add(tmp_bar);
                        onbar(tmp_bar);
                    }
                    kmak.Clear();
                    kmak.Add(pDepthMarketData);
                    k_h = pDepthMarketData.LastPrice;
                    k_l = pDepthMarketData.LastPrice;
                }
                if (c_t == f_t)
                {
                    kmak.Add(pDepthMarketData);
                    if (pDepthMarketData.LastPrice > k_h) k_h = pDepthMarketData.LastPrice;
                    if (pDepthMarketData.LastPrice < k_l) k_l = pDepthMarketData.LastPrice;
                }
            }
            public void w_list(笔 bi)
            {
                /*   bl.Write(bi.high);
                   bl.Write(bi.low);
                   bl.Write(bi.o);
                   if (bi.zd == 结构.底)
                       bl.Write(1);
                   if (bi.zd == 结构.顶)
                       bl.Write(0);
                   bl.Write(bi.ba.BeginTime.ToBinary());*/
            }
            public void read_mk(string id)
            {
                FileStream fs;
                int i;
                Bar t_b;
                if (id != "")
                {
                    {
                        fs = new FileStream(k_path + "\\" + id + kext, FileMode.Open, FileAccess.Read, FileShare.Read);
                        if (fs.Length == 0) return;
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bt = br.ReadBytes(1344);
                        try
                        {
                            while (true)
                            {
                                t_b = new Bar();
                                t_b.BeginTime = new DateTime(br.ReadInt64());
                                t_b.EndTime = new DateTime(br.ReadInt64());
                                br.ReadInt64();
                                t_b.Open = br.ReadDouble();
                                t_b.Close = br.ReadDouble();
                                t_b.High = br.ReadDouble();
                                t_b.Low = br.ReadDouble();
                                br.ReadDouble();
                                t_b.Volume = br.ReadDouble();
                                t_b.Turnover = br.ReadDouble();
                                t_b.OpenInterest = br.ReadDouble();
                                bs.Add(t_b);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\n\n读取结束！");
                        }
                        fs.Close();
                        br.Close();
                    }
                }
            }
            string init_tick_file()
            {
                string st = (100 + DateTime.Now.Month).ToString();
                st = st.Remove(0, 1);
                string dt = st;
                st = (100 + DateTime.Now.Day).ToString();
                st = st.Remove(0, 1);
                return dt + st;
            }
           
            public void w_tick(CThostFtdcDepthMarketDataField pDepthMarketData)
            {
                BinaryWriter bw = null;
                bw = tbw;

                if (intim())
                {
                    bw.Write(pDepthMarketData.AskPrice1); bw.Write(pDepthMarketData.AskVolume1); bw.Write(pDepthMarketData.BidPrice1);
                    bw.Write(pDepthMarketData.BidVolume1); bw.Write(pDepthMarketData.PreClosePrice);
                    bw.Write(pDepthMarketData.LastPrice); bw.Write(pDepthMarketData.Volume);
                    bw.Write(pDepthMarketData.OpenInterest); bw.Write(pDepthMarketData.OpenPrice);
                    bw.Write(pDepthMarketData.LowestPrice); bw.Write(pDepthMarketData.HighestPrice);
                    bw.Write(pDepthMarketData.UpperLimitPrice); bw.Write(pDepthMarketData.LowerLimitPrice);
                    bw.Write(pDepthMarketData.UpdateTime);
                }
            }
            public bool intim()
            {
                bool f = false;
                DateTime st = DateTime.Now;
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                DateTime hbTime = new System.DateTime(year, month, day, 8, 59, 0);
                DateTime heTime = new System.DateTime(year, month, day, 10, 15, 5);
                DateTime zbTime = new System.DateTime(year, month, day, 10, 29, 58);
                DateTime zeTime = new System.DateTime(year, month, day, 11, 30, 05);
                DateTime xbTime = new System.DateTime(year, month, day, 13, 29, 55);
                DateTime xeTime = new System.DateTime(year, month, day, 15, 00, 05);
                DateTime ybTime = new System.DateTime(year, month, day, 20, 59, 55);
                DateTime yeTime = new System.DateTime(year, month, day, 23, 59, 59);
                DateTime bbTime = new System.DateTime(year, month, day, 0, 0, 0);
                DateTime beTime = new System.DateTime(year, month, day, 1, 0, 0);
                if (((st > hbTime) && (st < heTime)) || ((st > zbTime) && (st < zeTime)) || ((st > zbTime) && (st < zeTime)) || ((st > ybTime) && (st < yeTime)) || ((st > bbTime) && (st < beTime)))
                {
                    f = true;
                }
                return f;
            }

           public void 初始文件流()
            {
                if (!Directory.Exists(k_path))
                {
                    Directory.CreateDirectory(k_path);
                }
                if (!Directory.Exists(t_path))
                {
                    Directory.CreateDirectory(t_path);
                }
                kfs = new FileStream(k_path + "\\" + id + kext, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                kbw = new BinaryWriter(kfs);
                kbw.Seek(0, SeekOrigin.End);
                tfs = new FileStream(t_path + "\\" + id + init_tick_file() + text, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                tbw = new BinaryWriter(tfs);
                tbw.Seek(0, SeekOrigin.End);
            }
            void  初始化()
            {
                tick = new List<CThostFtdcDepthMarketDataField>();
                kmak = new List<CThostFtdcDepthMarketDataField>();
                bs = new BarSeries();
                tmpbs = new BarSeries();
                bl = new List<笔>();
                bl2 = new List<笔>();
                bl3 = new List<笔>();
                //    zs = new List<走势>();
                read_mk(ID);
                指标();
                //    read_tick();
                bic = 0;
                今买量 = 0;
                今卖量 = 0;
                昨买量 = 0;
                昨卖量 = 0;
                tickcount = 0;
                pk.成交买量 = 0;
                pk.成交卖量 = 0;
                pk.上一跳 = 0;
                pk.连跌跳数 = 0;
                pk.连涨跳数 = 0;
            }
            public Instrument(string inid)
            {
                this.id = inid;
                初始化();
                初始文件流();

                p.LastPrice = 0;
            }
            public string ID
            {
                get { return id; }
            }
            public BarSeries bars
            {
                get { return bs; }
            }
            public List<笔> bis
            {
                get { return bl; }
            }
            public List<CThostFtdcDepthMarketDataField> ticks
            {
                get { return tick; }
            }
        }
        List<Instrument> ilist;
        void w_tra(CThostFtdcDepthMarketDataField pDepthMarketData, CThostFtdcDepthMarketDataField ptick)
        {/*
            trab.Write(pDepthMarketData.AskPrice1);
            trab.Write(pDepthMarketData.AskVolume1 - ptick.AskVolume1);
            trab.Write(pDepthMarketData.BidPrice1);
            trab.Write(pDepthMarketData.BidVolume1 - ptick.BidVolume1);
            trab.Write(pDepthMarketData.LastPrice);
            trab.Write(pDepthMarketData.LastPrice - ptick.LastPrice);
            trab.Write(pDepthMarketData.Volume - ptick.Volume);
            trab.Write(pDepthMarketData.OpenInterest - ptick.OpenInterest);*/
        }

        string[] idlist = new string[] { "ru1709" };//, "CF609", "i1609", "jm1609", "rb1610", "RM609", "SR609", "bu1609" };

        #region//查手续费响应

        void tradeApi_OnRspQryInstrumentCommissionRate(ref CThostFtdcInstrumentCommissionRateField pInstrumentCommissionRate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast)
            {
                if (pRspInfo.ErrorID == 0 && this.instrument4QryRate.StartsWith(pInstrumentCommissionRate.InstrumentID))
                {
                    DataRow dr = this.dtInstruments.Rows.Find(this.instrument4QryRate);
                    if (dr == null) //无此合约,查下一个
                    {
                        this.apiIsBusy = false;	//查询完成
                    }
                    else
                    {
                        if (pInstrumentCommissionRate.OpenRatioByMoney == 0) //手续费率=0:手续费值
                        {
                            dr["手续费"] = pInstrumentCommissionRate.OpenRatioByVolume + pInstrumentCommissionRate.CloseTodayRatioByVolume;	//手续费
                            this.BeginInvoke(new Action<string, string, string>(setInstrumentSelectedCellStyle), "手续费", (string)dr["合约"], "F2");
                            dr["手续费-平仓"] = pInstrumentCommissionRate.CloseRatioByVolume;
                            this.BeginInvoke(new Action<string, string, string>(setInstrumentSelectedCellStyle), "手续费", (string)dr["合约"], "F2");
                        }
                        else
                        {
                            dr["手续费"] = pInstrumentCommissionRate.OpenRatioByMoney + pInstrumentCommissionRate.CloseTodayRatioByMoney;	//手续费率
                            this.BeginInvoke(new Action<string, string, string>(setInstrumentSelectedCellStyle), "手续费", (string)dr["合约"], "P3");
                            dr["手续费-平仓"] = pInstrumentCommissionRate.CloseRatioByMoney;
                            this.BeginInvoke(new Action<string, string, string>(setInstrumentSelectedCellStyle), "手续费-平仓", (string)dr["合约"], "P3");
                        }
                        Thread.Sleep(1000);
                        this.tradeApi.QryInstrumentMarginRate((string)dr["合约"]);
                    }
                }
                else
                    this.apiIsBusy = false;
            }
        }
        //查保证金响应
        void tradeApi_OnRspQryInstrumentMarginRate(ref CThostFtdcInstrumentMarginRateField pInstrumentMarginRate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            DataRow dr = this.dtInstruments.Rows.Find(pInstrumentMarginRate.InstrumentID);
            if (dr != null)
            {
                if (pInstrumentMarginRate.IsRelative == EnumBoolType.No) //交易所收取总额度
                {
                    dr["保证金-多"] = pInstrumentMarginRate.LongMarginRatioByMoney;
                    dr["保证金-空"] = pInstrumentMarginRate.ShortMarginRatioByMoney;
                }
                else //相对交易所收取
                {
                    dr["保证金-多"] = (double)this.dtInstruments.Rows.Find(pInstrumentMarginRate.InstrumentID)["保证金-多"] + pInstrumentMarginRate.LongMarginRatioByMoney;
                    dr["保证金-空"] = (double)this.dtInstruments.Rows.Find(pInstrumentMarginRate.InstrumentID)["保证金-空"] + pInstrumentMarginRate.ShortMarginRatioByMoney;
                }
            }
            if (bIsLast)
            {
                this.apiIsBusy = false;	//查询完成
            }
        }
        //设置单元格样式
        void setInstrumentSelectedCellStyle(string _columnName, string _instrument, string _format)
        {
            int rowIndex = -1;
            for (int i = 0; i < this.dataGridViewInstrumentsSelected.Rows.Count; i++)
            {
                if ((string)this.dataGridViewInstrumentsSelected.Rows[i].Cells["合约"].Value == _instrument)
                {
                    rowIndex = i;
                    break;
                }
            }
            if (rowIndex != -1)	//在自选里:设置"自选"中的样式
            {
                this.dataGridViewInstrumentsSelected[_columnName, rowIndex].Style.Format = _format;
            }
            else //设置在各"交易所"中的样式
            {
                for (int i = 0; i < this.dataGridViewInstruments.Rows.Count; i++)
                {
                    if ((string)this.dataGridViewInstruments.Rows[i].Cells["合约"].Value == _instrument)
                    {
                        rowIndex = i;
                        break;
                    }
                }
                if (rowIndex != -1)
                    this.dataGridViewInstruments[_columnName, rowIndex].Style.Format = _format;
            }
        }

        private void FormTrade_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save();	//保存设置
            }
            catch { }
            try { tradeApi.DisConnect(); }
            catch { }
            try { mdApi.DisConnect(); }
            catch { }
            try
            {
                //  this.dtInstruments.WriteXml("Resources\\rate" + "_" + tradeApi.InvestorID + ".xml", XmlWriteMode.WriteSchema);//保存合约信息
                if (threadFreshExchangeTime != null)
                    threadFreshExchangeTime.Abort();
                if (this.threadFreshMarketData != null)
                    this.threadFreshMarketData.Abort();
                if (this.threadQry != null)
                    this.threadQry.Abort();

            }
            catch { }
        }
        //退出
        void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        //登录:生成tradeApi/注册事件
        void btnLogin_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            if (ul.cbServer.SelectedIndex >= 0)
            {

                //保存用户/密码
                /*	Properties.Settings.Default.Servers[ul.cbServer.SelectedIndex] = server[0] + "|" + server[1] + "|" + server[2] + "|"
                        + server[3] + "|" + server[4] + "|" + server[5] + "|" + ul.tbUserID.Text //用户名
                        +"|" + (Properties.Settings.Default.SavePWD ? ul.tbPassword.Text : "");//密码+ */
                tradeApi = new TradeApi("035135", "625117", "9999", "tcp://180.168.146.187:10000");//new TradeApi(ul.tbUserID.Text, ul.tbPassword.Text, server[1], server[4] + ":" + server[5]);
                //         tradeApi = new TradeApi("8703980", "061513", "7070", "tcp://116.236.239.136:41205");
                regEvents();
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Connect, "连接...");
                tradeApi.Connect();
            }
        }

        //注册tradeapi事件:包括4trade的事件,两个api在此区分功能

        System.Timers.Timer timer = new System.Timers.Timer();

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 得到 hour minute second  如果等于某个值就开始执行某个程序。  
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;


            // 定制时间； 比如 在10：30 ：00 的时候执行某个函数  
            if ((intHour < 8 && intMinute == 58) && (intHour > 2 && intMinute == 00))
            {
                for (int i = 0; i < ilist.Count; i++)
                {
                    ilist[i].closef();
                }
            }
            if ((intHour == 8 && intMinute == 58) || (intHour == 13 && intMinute == 28) || (intHour == 20 && intMinute == 58) || (intHour == 10 && intMinute == 28))
            {
                for (int i = 0; i < ilist.Count; i++)
                {
                    ilist[i].closef();
                    ilist[i].初始文件流();
                }
            }
            if ((intHour == 11 && intMinute == 31) || (intHour == 15 && intMinute == 1) || (intHour == 1 && intMinute == 1) || (intHour == 10 && intMinute == 16))
            {
                for (int i = 0; i < ilist.Count; i++)
                {
                    ilist[i].closef();



                }
            }
            if ((intHour == 14 && intMinute == 58))
            {
                for (int i = 0; i < ilist.Count; i++)
                {
                    ilist[i].收盘();
                }
            }
        }
        void regEvents()
        {
            timer.Enabled = true;
            timer.Interval = 1000;//执行间隔时间,单位为毫秒  
            timer.Start();
            tradeApi.OnRspError += new TradeApi.RspError(tradeApi_OnRspError);
            tradeApi.OnRspUserLogin += new TradeApi.RspUserLogin(tradeApi_OnRspUserLogin);

            tradeApi.OnFrontConnect += new TradeApi.FrontConnect(tradeApi_OnFrontConnect);
            tradeApi.OnDisConnected += new TradeApi.DisConnected(tradeApi_OnDisConnected);
            tradeApi.OnErrRtnOrderAction += new TradeApi.ErrRtnOrderAction(tradeApi_OnErrRtnOrderAction);
            tradeApi.OnErrRtnOrderInsert += new TradeApi.ErrRtnOrderInsert(tradeApi_OnErrRtnOrderInsert);

            tradeApi.OnHeartBeatWarning += new TradeApi.HeartBeatWarning(tradeApi_OnHeartBeatWarning);
            tradeApi.OnRspOrderAction += new TradeApi.RspOrderAction(tradeApi_OnRspOrderAction);
            tradeApi.OnRspOrderInsert += new TradeApi.RspOrderInsert(tradeApi_OnRspOrderInsert);
            tradeApi.OnRspQryBrokerTradingAlgos += new TradeApi.RspQryBrokerTradingAlgos(tradeApi_OnRspQryBrokerTradingAlgos);
            tradeApi.OnRspQryBrokerTradingParams += new TradeApi.RspQryBrokerTradingParams(tradeApi_OnRspQryBrokerTradingParams);
            tradeApi.OnRspQryCFMMCTradingAccountKey += new TradeApi.RspQryCFMMCTradingAccountKey(tradeApi_OnRspQryCFMMCTradingAccountKey);
            tradeApi.OnRspQryDepthMarketData += new TradeApi.RspQryDepthMarketData(tradeApi_OnRspQryDepthMarketData);
            tradeApi.OnRspQryExchange += new TradeApi.RspQryExchange(tradeApi_OnRspQryExchange);
            tradeApi.OnRspQryInstrument += new TradeApi.RspQryInstrument(tradeApi_OnRspQryInstrument);
            tradeApi.OnRspQryInstrumentCommissionRate += new TradeApi.RspQryInstrumentCommissionRate(tradeApi_OnRspQryInstrumentCommissionRate);
            tradeApi.OnRspQryInstrumentMarginRate += new TradeApi.RspQryInstrumentMarginRate(tradeApi_OnRspQryInstrumentMarginRate);
            tradeApi.OnRspQryInvestor += new TradeApi.RspQryInvestor(tradeApi_OnRspQryInvestor);
            tradeApi.OnRspQryInvestorPosition += new TradeApi.RspQryInvestorPosition(tradeApi_OnRspQryInvestorPosition);
            tradeApi.OnRspQryInvestorPositionCombineDetail += new TradeApi.RspQryInvestorPositionCombineDetail(tradeApi_OnRspQryInvestorPositionCombineDetail);
            tradeApi.OnRspQryInvestorPositionDetail += new TradeApi.RspQryInvestorPositionDetail(tradeApi_OnRspQryInvestorPositionDetail);
            tradeApi.OnRspQryNotice += new TradeApi.RspQryNotice(tradeApi_OnRspQryNotice);
            tradeApi.OnRspQryOrder += new TradeApi.RspQryOrder(tradeApi_OnRspQryOrder);
            tradeApi.OnRspQrySettlementInfo += new TradeApi.RspQrySettlementInfo(tradeApi_OnRspQrySettlementInfo);
            tradeApi.OnRspQrySettlementInfoConfirm += new TradeApi.RspQrySettlementInfoConfirm(tradeApi_OnRspQrySettlementInfoConfirm);
            tradeApi.OnRspQryTrade += new TradeApi.RspQryTrade(tradeApi_OnRspQryTrade);
            tradeApi.OnRspQryTradingAccount += new TradeApi.RspQryTradingAccount(tradeApi_OnRspQryTradingAccount);
            tradeApi.OnRspQryTradingCode += new TradeApi.RspQryTradingCode(tradeApi_OnRspQryTradingCode);
            tradeApi.OnRspQryTradingNotice += new TradeApi.RspQryTradingNotice(tradeApi_OnRspQryTradingNotice);
            tradeApi.OnRspQueryMaxOrderVolume += new TradeApi.RspQueryMaxOrderVolume(tradeApi_OnRspQueryMaxOrderVolume);
            tradeApi.OnRspSettlementInfoConfirm += new TradeApi.RspSettlementInfoConfirm(tradeApi_OnRspSettlementInfoConfirm);
            tradeApi.OnRspTradingAccountPasswordUpdate += new TradeApi.RspTradingAccountPasswordUpdate(tradeApi_OnRspTradingAccountPasswordUpdate);
            tradeApi.OnRspUserLogout += new TradeApi.RspUserLogout(tradeApi_OnRspUserLogout);
            tradeApi.OnRspUserPasswordUpdate += new TradeApi.RspUserPasswordUpdate(tradeApi_OnRspUserPasswordUpdate);
            tradeApi.OnRtnErrorConditionalOrder += new TradeApi.RtnErrorConditionalOrder(tradeApi_OnRtnErrorConditionalOrder);
            tradeApi.OnRtnInstrumentStatus += new TradeApi.RtnInstrumentStatus(tradeApi_OnRtnInstrumentStatus);
            tradeApi.OnRtnOrder += new TradeApi.RtnOrder(tradeApi_OnRtnOrder);
            tradeApi.OnRtnTrade += new TradeApi.RtnTrade(tradeApi_OnRtnTrade);
            tradeApi.OnRtnTradingNotice += new TradeApi.RtnTradingNotice(tradeApi_OnRtnTradingNotice);
            //银期转帐
            tradeApi.OnRspQryContractBank += new TradeApi.RspQryContractBank(tradeApi_OnRspQryContractBank);
            tradeApi.OnRspQryTransferBank += new TradeApi.RspQryTransferBank(tradeApi_OnRspQryTransferBank);
            tradeApi.OnRspFromFutureToBankByFuture += new TradeApi.RspFromFutureToBankByFuture(tradeApi_OnRspFromFutureToBankByFuture);
            tradeApi.OnRtnFromFutureToBankByFuture += new TradeApi.RtnFromFutureToBankByFuture(tradeApi_OnRtnFromFutureToBankByFuture);
            tradeApi.OnErrRtnFutureToBankByFuture += new TradeApi.ErrRtnFutureToBankByFuture(tradeApi_OnErrRtnFutureToBankByFuture);
            tradeApi.OnRspFromBankToFutureByFuture += new TradeApi.RspFromBankToFutureByFuture(tradeApi_OnRspFromBankToFutureByFuture);
            tradeApi.OnRtnFromBankToFutureByFuture += new TradeApi.RtnFromBankToFutureByFuture(tradeApi_OnRtnFromBankToFutureByFuture);
            //查银行余额
            tradeApi.OnRspQueryBankAccountMoneyByFuture += new TradeApi.RspQueryBankAccountMoneyByFuture(tradeApi_OnRspQueryBankAccountMoneyByFuture);
            tradeApi.OnRtnQueryBankBalanceByFuture += new TradeApi.RtnQueryBankBalanceByFuture(tradeApi_OnRtnQueryBankBalanceByFuture);
            //查转帐
            tradeApi.OnRspQryTransferSerial += new TradeApi.RspQryTransferSerial(tradeApi_OnRspQryTransferSerial);
            //预埋单
            tradeApi.OnRspQryParkedOrder += new TradeApi.RspQryParkedOrder(tradeApi_OnRspQryParkedOrder);
            tradeApi.OnRspQryParkedOrderAction += new TradeApi.RspQryParkedOrderAction(tradeApi_OnRspQryParkedOrderAction);
            tradeApi.OnRspParkedOrderInsert += new TradeApi.RspParkedOrderInsert(tradeApi_OnRspParkedOrderInsert);
            tradeApi.OnRspParkedOrderAction += new TradeApi.RspParkedOrderAction(tradeApi_OnRspParkedOrderAction);
            tradeApi.OnRspRemoveParkedOrder += new TradeApi.RspRemoveParkedOrder(tradeApi_OnRspRemoveParkedOrder);
            tradeApi.OnRspRemoveParkedOrderAction += new TradeApi.RspRemoveParkedOrderAction(tradeApi_OnRspRemoveParkedOrderAction);
        }

        void tradeApi_OnErrRtnFutureToBankByFuture(ref CThostFtdcReqTransferField pReqTransfer, ref CThostFtdcRspInfoField pRspInfo)
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, "银转出错.");
        }

        //日志记录
        void progress(EnumProgessState _state, string _msg)
        {
            if (ul.Visible)
            {
                if (ul.Progressbar.Value <= 85)
                    ul.Progressbar.Value += 15;
                ul.labelState.Text = _msg;
                if (_state == EnumProgessState.OnLogin && !_msg.EndsWith("完成")) //未完成登录
                {
                    MessageBox.Show(_msg);
                    ul.btnLogin.Enabled = true;
                    ul.Progressbar.Value = 0;
                }
            }
            else if (!(_state.ToString().StartsWith("Qry") || _state.ToString().StartsWith("OnQry"))) //查询事件不显示
            {
                this.comboBoxErrMsg.Items.Insert(0, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + "|" + _state.ToString() + ":" + _msg);
                this.comboBoxErrMsg.SelectedIndex = 0;
                if (_state == EnumProgessState.OnError || _state == EnumProgessState.OnDisConnect || _state == EnumProgessState.OnErrOrderInsert ||
                    _state == EnumProgessState.OnErrOrderAction || _state == EnumProgessState.OnMdDisConnected)
                    this.toolTipInfo.ToolTipIcon = ToolTipIcon.Warning;
                else
                    this.toolTipInfo.ToolTipIcon = ToolTipIcon.Info;
                this.toolTipInfo.Show(_msg, this.tabControSystem, 0, this.tabControSystem.Height - 50, 5000);	//冒泡
            }
            Properties.Settings.Default.Log.Add(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + "|" + _state.ToString() + ":" + _msg);

            if (_state == EnumProgessState.OnConnected)		//连接断开标志:只在断开重连时有效,在首次连接时,要放在form_load中
            {
                this.radioButtonTrade.ForeColor = Color.Green;
                this.radioButtonTrade.Checked = true;
            }
            else if (_state == EnumProgessState.OnDisConnect)
            {
                this.radioButtonTrade.ForeColor = Color.Red;
                this.radioButtonTrade.Checked = false;
            }
            else if (_state == EnumProgessState.OnMdConnected) //行情连接/断开
            {
                this.radioButtonMd.ForeColor = Color.Green;
                this.radioButtonMd.Checked = true;
            }
            else if (_state == EnumProgessState.OnMdDisConnected)
            {
                this.radioButtonMd.ForeColor = Color.Red;
                this.radioButtonMd.Checked = false;
            }
            //声音
            if (Properties.Settings.Default.PlaySound)
            {
                switch (_state)
                {
                    case EnumProgessState.OnErrOrderInsert: //下单错误
                        snd = new SoundPlayer(@"Resources\指令单错误.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnRtnOrder:
                        snd = new SoundPlayer(@"Resources\报入成功.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnRtnTrade: //成交
                        this.listViewOrder.Sort();
                        snd = new SoundPlayer(@"Resources\成交通知.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnErrOrderAction: //撤单错误
                        snd = new SoundPlayer(@"Resources\指令单错误.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnOrderAction:	//撤单成功
                        snd = new SoundPlayer(@"Resources\撤单.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnConnected:		//连接成功
                        if (!ul.Visible)	//登录后发声
                        {
                            snd = new SoundPlayer(@"Resources\信息到达.wav");
                            snd.Play();
                        }
                        break;
                    case EnumProgessState.OnDisConnect:		//连接中断
                        snd = new SoundPlayer(@"Resources\连接中断.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnRtnTradingNotice:	//事件通知
                        snd = new SoundPlayer(@"Resources\信息到达.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnRtnInstrumentStatus:	//合约状态
                        snd = new SoundPlayer(@"Resources\信息到达.wav");
                        snd.Play();
                        break;
                    case EnumProgessState.OnError:
                        snd = new SoundPlayer(@"Resources\指令单错误.wav");
                        snd.Play();
                        break;
                }
            }
        }

        //订阅所有合约的行情
        void subAllInstruments()
        {
            //Properties.Settings.Default.CustomVolume.Clear();//测试清空
            //合约列表
            for (int i = 0; i < this.dtInstruments.Rows.Count; i++)
            {
                string instrument = (string)this.dtInstruments.Rows[i]["合约"];
                for (int k = 0; k < idlist.Length; k++)
                {
                    if (instrument == idlist[k])
                    {
                        this.comboBoxInstrument.Items.Add(instrument);	//添加到合约输入列表
                        mdApi.SubMarketData(instrument);
                        //订阅行情
                        //自定义手数:默认为1
                        int idx = Properties.Settings.Default.CustomVolume.IndexOf(instrument);
                        if (idx >= 0)
                            this.dataGridViewCustomVolume.Rows.Add(instrument, Properties.Settings.Default.CustomVolume[idx + 1]);
                        else
                        {
                            Properties.Settings.Default.CustomVolume.Add(instrument);
                            Properties.Settings.Default.CustomVolume.Add("1");
                            this.dataGridViewCustomVolume.Rows.Add(instrument, "1");
                        }
                    }
                }
            }
            this.dataGridViewCustomVolume.Sort(this.dataGridViewCustomVolume.Columns["合约"], ListSortDirection.Ascending);		//按合约升序排列
            this.dataGridViewCustomVolume.Sort(this.dataGridViewCustomVolume.Columns["手数"], ListSortDirection.Descending);	//按手数降序排列
            this.comboBoxInstrument.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        /// 将获得的响应struct填充到HFListView中
        void showStructInListView(ObjectAndKey oak)
        {
            //报单返回
            if (oak.Object.GetType() == typeof(CThostFtdcOrderField))
            {
                CThostFtdcOrderField field = (CThostFtdcOrderField)oak.Object;
                ListViewItem lvi = this.listViewOrder.Items.Find(oak.Key, false).FirstOrDefault();
                if (lvi == null)
                {
                    lvi = new ListViewItem(field.InstrumentID);	//1:frondid+sessionid,2:ordersysid
                    lvi.Name = oak.Key;
                    lvi.SubItems.Add(field.Direction == EnumDirectionType.Buy ? "买" : "卖");
                    lvi.SubItems.Add(field.CombOffsetFlag_0 == EnumOffsetFlagType.Open ? "开仓" : field.CombOffsetFlag_0 == EnumOffsetFlagType.Close ? "平仓" : "平今");
                    lvi.SubItems.Add(field.LimitPrice.ToString());				//报单价格
                    lvi.SubItems.Add("0");										//成交均价
                    lvi.SubItems.Add(field.VolumeTotalOriginal.ToString());		//委托数量
                    lvi.SubItems.Add(field.VolumeTraded.ToString());			//成交量
                    lvi.SubItems.Add(field.InsertTime);							//报单时间
                    lvi.SubItems.Add(field.InsertTime);							//成交时间
                    lvi.SubItems.Add(field.OrderSysID.Trim());							//报单编号:有成交时对应
                    lvi.SubItems.Add(field.StatusMsg);							//报单状态
                    //建组
                    lvi.Group = this.listViewOrder.Groups[field.OrderStatus.ToString()];
                    this.listViewOrder.Items.Add(lvi);
                }
                else
                {
                    lvi.SubItems[6].Text = field.VolumeTraded.ToString();			//成交量
                    lvi.SubItems[8].Text = field.UpdateTime;						//成交时间:最后更新时间
                    lvi.SubItems[9].Text = field.OrderSysID.Trim();						//报单编号:有成交时对应
                    lvi.SubItems[10].Text = field.StatusMsg;						//报单状态
                    lvi.Group = this.listViewOrder.Groups[field.OrderStatus.ToString()];
                }
            } //成交响应
            else if (oak.Object.GetType() == typeof(CThostFtdcTradeField))
            {
                CThostFtdcTradeField field = (CThostFtdcTradeField)oak.Object;
                if (this.isQryHistoryTrade)	//历史成交
                {
                    ListViewItem lvi = new ListViewItem(field.InstrumentID);
                    //lvi.SubItems.Add(string.Format("{0}.{1}.{2}", field.TradeDate.Substring(0, 4), field.TradeDate.Substring(4, 2), field.TradeDate.Substring(6, 2)));//日期
                    lvi.SubItems.Add(field.TradeTime);				//时间
                    lvi.SubItems.Add(field.ExchangeID);
                    lvi.SubItems.Add(field.OrderSysID.Trim());
                    lvi.SubItems.Add(field.Direction == EnumDirectionType.Buy ? "买" : "卖");
                    lvi.SubItems.Add(field.OffsetFlag == EnumOffsetFlagType.Open ? "开" : "平");
                    lvi.SubItems.Add(field.Price.ToString("F2"));	//价格
                    lvi.SubItems.Add(field.Volume.ToString());		//数量
                    this.hfListViewTrade.Items.Add(lvi);
                    this.hfListViewTrade.SortColumn = 1;
                    this.hfListViewTrade.Sorting = SortOrder.Descending;
                    this.hfListViewTrade.Sort();
                }
                else
                {
                    ListViewItem lvi = this.listViewOrder.FindItemWithText(oak.Key.Trim(), true, 0);
                    if (lvi != null)
                    {
                        lvi.SubItems[4].Text = field.Price.ToString();					//成交均价
                        lvi.SubItems[6].Text = field.Volume.ToString();					//成交量
                        lvi.SubItems[8].Text = field.TradeTime;							//成交时间
                    }
                }
            } //持仓
            else if (oak.Object.GetType() == typeof(CThostFtdcInvestorPositionField))
            {
                CThostFtdcInvestorPositionField field = (CThostFtdcInvestorPositionField)oak.Object;
                ListViewItem lvi = this.listViewPosition.Items.Find(oak.Key, false).FirstOrDefault();
                int unit = (int)this.dtInstruments.Rows.Find(field.InstrumentID)["合约数量"];	//数量乘积,用于计算价格
                if (lvi == null)
                {
                    lvi = new ListViewItem(field.InstrumentID);
                    lvi.Name = oak.Key;
                    lvi.SubItems.Add(field.PosiDirection == EnumPosiDirectionType.Long ? "多" : "空");
                    lvi.SubItems.Add(field.YdPosition.ToString());		//昨日持仓
                    lvi.SubItems.Add(field.Position.ToString());		//今日持仓
                    lvi.SubItems.Add((field.PositionCost / unit / field.Position).ToString("F2"));	//持仓成本
                    lvi.SubItems.Add(field.PositionProfit.ToString("F2"));	//持仓盈亏
                    lvi.SubItems.Add(field.OpenVolume == 0 ? "0" : (field.OpenAmount / unit / field.OpenVolume).ToString("F2"));		//开仓成本
                    lvi.SubItems.Add(field.OpenVolume.ToString());		//开仓手数
                    lvi.SubItems.Add(field.CloseVolume == 0 ? "0" : (field.CloseAmount / unit / field.CloseVolume).ToString("F2"));		//平仓价格
                    lvi.SubItems.Add(field.CloseVolume.ToString());		//平仓手数
                    lvi.SubItems.Add(field.CloseProfit.ToString("F2"));		//平仓盈亏
                    lvi.SubItems.Add(field.Commission.ToString("F2"));		//手续费
                    //分组
                    lvi.Group = this.listViewPosition.Groups[field.PositionDate.ToString()];
                    this.listViewPosition.Items.Add(lvi);
                }
                else //更新
                {
                    lvi.SubItems[3].Text = field.Position.ToString();	//今日持仓
                    lvi.SubItems[4].Text = (field.PositionCost / unit / field.Position).ToString("F2");	//持仓成本
                    lvi.SubItems[5].Text = field.PositionProfit.ToString("F2");	//持仓盈亏
                    lvi.SubItems[8].Text = field.CloseVolume == 0 ? "0" : (field.CloseAmount / unit / field.CloseVolume).ToString("F2");	//平仓价格
                    lvi.SubItems[9].Text = field.CloseVolume.ToString();	//平仓手数
                    lvi.SubItems[10].Text = field.CloseProfit.ToString("F2");	//平仓盈亏
                    lvi.SubItems[11].Text = field.Commission.ToString("F2");	//手续费
                } //无持仓删除
                if (lvi.SubItems[3].Text == "0")		//无持仓:删除
                    this.listViewPosition.Items.Remove(lvi);
            } //预埋报单
            else if (oak.Object.GetType() == typeof(CThostFtdcParkedOrderField))
            {
                CThostFtdcParkedOrderField field = (CThostFtdcParkedOrderField)oak.Object;
                ListViewItem lvi = this.hfListViewParkedOrder.Items.Find(oak.Key, false).FirstOrDefault();
                if (lvi == null)
                {
                    lvi = new ListViewItem(field.InstrumentID);
                    lvi.Name = oak.Key;
                    lvi.SubItems.Add(field.StopPrice.ToString());			//触发价格
                    lvi.SubItems.Add(field.Direction == EnumDirectionType.Buy ? "买" : "卖");
                    lvi.SubItems.Add(field.CombOffsetFlag_0 == EnumOffsetFlagType.Open ? "开" : "平");
                    lvi.SubItems.Add(field.OrderPriceType.ToString());		//价格条件
                    lvi.SubItems.Add(field.LimitPrice.ToString());			//报单价格
                    lvi.SubItems.Add(field.VolumeTotalOriginal.ToString());	//报单数量
                    lvi.SubItems.Add(field.Status.ToString());				//埋单状态
                    lvi.SubItems.Add(field.ErrorMsg);						//错误信息
                    this.hfListViewParkedOrder.Items.Add(lvi);
                }
                else //更新
                {
                    lvi.SubItems[7].Text = field.Status.ToString();				//埋单状态
                    lvi.SubItems[8].Text = field.ErrorMsg;						//错误信息
                }
            } //合约
        }

        #region mdapi
        void mdApi_OnFrontConnected()
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnMdConnected, "行情连接完成");
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Login, "登录...");
            mdApi.UserLogin();
        }
        void mdApi_OnFrontDisconnected(int nReason)
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnMdDisConnected, "行情断开");
        }

        void mdApi_OnRspUserLogin(ref CThostFtdcRspUserLoginField pRspUserLogin, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast && pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action(subAllInstruments));
            }
            else
                MessageBox.Show(pRspInfo.ErrorID + ":" + pRspInfo.ErrorMsg);
        }

        void mdApi_OnRspUnSubMarketData(ref CThostFtdcSpecificInstrumentField pSpecificInstrument, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast)
                this.BeginInvoke(new Action(unSubMarketDate));
        }

        void mdApi_OnRtnDepthMarketData(ref CThostFtdcDepthMarketDataField pDepthMarketData)
        {

            //if (!string.IsNullOrWhiteSpace(pDepthMarketData.InstrumentID) && !pDepthMarketData.InstrumentID.StartsWith("SP"))
            this.listMarketDatas.Add(pDepthMarketData);

            //   this.k_MarketDatas.Add(pDepthMarketData);
            //this.BeginInvoke(new Action<CTPMdApi.CThostFtdcDepthMarketDataField>(freshMarketData), pDepthMarketData);
        }

        void unSubMarketDate()
        {
            this.numericUpDownPrice.Minimum = 0;
            this.numericUpDownPrice.Maximum = 0;
            this.numericUpDownPrice.Value = 0;
        }
        //void freshMarketData(CTPMdApi.CThostFtdcDepthMarketDataField pDepthMarketData)

        //

        public delegate void drtickDelegate();
        public void 下单(CThostFtdcDepthMarketDataField pDepthMarketData, int i)
        {

            #region
            if (ilist[i].bic < ilist[i].bis.Count)
            {
                if (ilist[i].bis[ilist[i].bis.Count - 1].zd == 结构.底)
                {
                    if (ilist[i].今买量 == 0 && ilist[i].今卖量 == 0 && ilist[i].昨买量 == 0 && ilist[i].昨卖量 == 0)
                    {
                        tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Buy, pDepthMarketData.AskPrice1, 1);
                        ilist[i].bic = ilist[i].bis.Count;
                        return;
                    }
                    else
                    {

                        if (ilist[i].昨卖量 > 0)
                        {
                            tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Close, EnumDirectionType.Buy, pDepthMarketData.AskPrice1, ilist[i].昨卖量);
                            //        tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Buy, pDepthMarketData.AskPrice1, 1);
                            ilist[i].bic = ilist[i].bis.Count;
                            return;
                        }
                        if (ilist[i].今卖量 > 0)
                        {
                            if (pDepthMarketData.ExchangeID == "SHFE")
                                tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.CloseToday, EnumDirectionType.Buy, pDepthMarketData.AskPrice1, ilist[i].今卖量);
                            else
                                tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Close, EnumDirectionType.Buy, pDepthMarketData.AskPrice1, ilist[i].今卖量);
                            //          tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Buy, pDepthMarketData.AskPrice1, 1);
                            ilist[i].bic = ilist[i].bis.Count;
                            return;
                        }
                    }
                }
                if (ilist[i].bis[ilist[i].bis.Count - 1].zd == 结构.顶)
                {
                    if (ilist[i].今买量 == 0 && ilist[i].今卖量 == 0 && ilist[i].昨买量 == 0 && ilist[i].昨卖量 == 0)
                    {
                        tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Sell, pDepthMarketData.BidPrice1, 1);
                        ilist[i].bic = ilist[i].bis.Count;
                        return;
                    }
                    else
                    {
                        if (ilist[i].今买量 > 0)
                        {
                            if (pDepthMarketData.ExchangeID == "SHFE")
                                tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.CloseToday, EnumDirectionType.Sell, pDepthMarketData.BidPrice1, ilist[i].今买量);
                            else
                                tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Close, EnumDirectionType.Sell, pDepthMarketData.BidPrice1, ilist[i].今买量);
                            //           tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Sell, pDepthMarketData.BidPrice1, 1);
                            ilist[i].bic = ilist[i].bis.Count;
                            return;
                        }

                        if (ilist[i].昨买量 > 0)
                        {
                            tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Close, EnumDirectionType.Sell, pDepthMarketData.BidPrice1, ilist[i].昨买量);
                            //    tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Sell, pDepthMarketData.BidPrice1, 1);
                            ilist[i].bic = ilist[i].bis.Count;
                            return;
                        }
                    }
                }

            }

            #endregion

        }
        // 报单成交响应
        void tradeApi_OnRtnTrade(ref CThostFtdcTradeField pTrade)
        {
            this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pTrade, pTrade.OrderSysID));
            if (this.lviCovert != null)			//反手
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QuickCover, "快速反手...");
                tradeApi.OrderInsert(pTrade.InstrumentID, EnumOffsetFlagType.Open, pTrade.Direction, pTrade.Price, pTrade.Volume);	//反手
                this.lviCovert = null;
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnRtnTrade, "成交");
            //重新查询持仓
            if (this.lviCovert == null) //快平/快锁/反手均已完成:查持仓
            {
                Thread.Sleep(1000);
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QryPosition, "查持仓...");
                //tradeApi.QryInvestorPosition(pTrade.InstrumentID);
                this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryIntorverPosition, pTrade.InstrumentID));	//查持仓
            }
            for (int i = 0; i < ilist.Count; i++)
            {
                if (pTrade.InstrumentID == ilist[i].ID)
                {
                    #region
                    switch (pTrade.OffsetFlag)
                    {
                        case EnumOffsetFlagType.Open:
                            if (pTrade.Direction == EnumDirectionType.Buy)
                            {
                                ilist[i].买成本价 = (ilist[i].买成本价 * ilist[i].今买量 + pTrade.Volume * pTrade.Price) / (ilist[i].今买量 + pTrade.Volume);
                                ilist[i].今买量 = ilist[i].今买量 + pTrade.Volume;
                            }
                            else
                            {
                                ilist[i].卖成本价 = (ilist[i].卖成本价 * ilist[i].今卖量 + pTrade.Volume * pTrade.Price) / (ilist[i].今卖量 + pTrade.Volume);
                                ilist[i].今卖量 = ilist[i].今卖量 + pTrade.Volume;
                            }
                            break;
                        case EnumOffsetFlagType.CloseToday:
                            if (pTrade.Direction == EnumDirectionType.Buy)
                            {
                                if (ilist[i].今卖量 - pTrade.Volume != 0)
                                {
                                    ilist[i].卖成本价 = (ilist[i].卖成本价 * ilist[i].今卖量 - pTrade.Volume * pTrade.Price) / (ilist[i].今卖量 - pTrade.Volume);
                                    ilist[i].今卖量 = ilist[i].今卖量 - pTrade.Volume;
                                }
                                else
                                {
                                    ilist[i].卖成本价 = 0;
                                    ilist[i].今卖量 = 0;
                                }
                            }
                            else
                            {
                                if (ilist[i].今买量 - pTrade.Volume != 0)
                                {
                                    ilist[i].买成本价 = (ilist[i].买成本价 * ilist[i].今买量 - pTrade.Volume * pTrade.Price) / (ilist[i].今买量 - pTrade.Volume);
                                    ilist[i].今买量 = ilist[i].今买量 - pTrade.Volume;
                                }
                                else
                                {
                                    ilist[i].买成本价 = 0;

                                    ilist[i].今买量 = 0;
                                }

                            }
                            break;
                        case EnumOffsetFlagType.Close:
                            if (pTrade.ExchangeID == "SHFE")
                            {
                                if (pTrade.Direction == EnumDirectionType.Buy)
                                {
                                    if (ilist[i].昨卖量 - pTrade.Volume != 0)
                                    {
                                        ilist[i].卖成本价 = (ilist[i].卖成本价 * ilist[i].昨卖量 - pTrade.Volume * pTrade.Price) / (ilist[i].昨卖量 - pTrade.Volume);
                                        ilist[i].昨卖量 = ilist[i].昨卖量 - pTrade.Volume;

                                    }
                                    else
                                    {
                                        ilist[i].卖成本价 = 0;
                                        ilist[i].昨卖量 = 0;
                                    }
                                }
                                else
                                {
                                    if (ilist[i].昨买量 - pTrade.Volume != 0)
                                    {
                                        ilist[i].买成本价 = (ilist[i].买成本价 * ilist[i].昨买量 - pTrade.Volume * pTrade.Price) / (ilist[i].昨买量 - pTrade.Volume);
                                        ilist[i].昨买量 = ilist[i].昨买量 - pTrade.Volume;
                                    }
                                    else
                                    {
                                        ilist[i].买成本价 = 0;
                                        ilist[i].昨买量 = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (pTrade.Direction == EnumDirectionType.Buy)
                                {
                                    if (ilist[i].今卖量 - pTrade.Volume != 0)
                                    {
                                        ilist[i].卖成本价 = (ilist[i].卖成本价 * ilist[i].今卖量 - pTrade.Volume * pTrade.Price) / (ilist[i].今卖量 - pTrade.Volume);
                                        ilist[i].今卖量 = ilist[i].今卖量 - pTrade.Volume;

                                    }
                                    else
                                    {
                                        ilist[i].卖成本价 = 0;
                                        ilist[i].今卖量 = 0;
                                    }

                                }
                                else
                                {
                                    if (ilist[i].今买量 - pTrade.Volume != 0)
                                    {

                                        ilist[i].买成本价 = (ilist[i].买成本价 * ilist[i].今买量 - pTrade.Volume * pTrade.Price) / (ilist[i].今买量 - pTrade.Volume);
                                        ilist[i].今买量 = ilist[i].今买量 - pTrade.Volume;
                                    }
                                    else
                                    {
                                        ilist[i].买成本价 = 0;

                                        ilist[i].今买量 = 0;
                                    }
                                }
                            }

                            break;
                    }
                    #endregion //保存成效信息

                    break;
                }
            }
        }

        void freshMarketData()
        {
            while (true)
            {
                if (this.listMarketDatas.Count == 0)
                {
                    Thread.Sleep(50);
                    continue;
                }
                try
                {
                    CThostFtdcDepthMarketDataField pDepthMarketData = this.listMarketDatas[0];// (CTPMdApi.CThostFtdcDepthMarketDataField)_pDepthMarketData;

                    //更新行情数据
                    #region
                    for (int i = 0; i < ilist.Count(); i++)
                    {
                        if (pDepthMarketData.InstrumentID == ilist[i].ID)
                        {
                            ilist[i].市场 = pDepthMarketData.ExchangeID;
                            ilist[i].make_k(pDepthMarketData);

                            if (ilist[i].tick.Count == 1)
                            {
                                ilist[i].开盘();
                            }
                            // 下单(pDepthMarketData, i);
                            ilist[i].计算盘口(pDepthMarketData);
                            /*
                                        tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Buy, pDepthMarketData.AskPrice1, 1);
                                        tradeApi.OrderInsert(ilist[i].ID, EnumOffsetFlagType.Open, EnumDirectionType.Sell, pDepthMarketData.BidPrice1, 1);                       
                               */
                            ilist[i].w_tick(pDepthMarketData);
                            ilist[i].tick.Add(pDepthMarketData);
                            if (ilist[i].tick.Count > 600)
                                ilist[i].tick.RemoveAt(0);
                            break;
                        }
                    }
                    #endregion  委托下单

                    #region 显示行情
                    /*
                    DataRow drMarketData = this.dtMarketData.Rows.Find(pDepthMarketData.InstrumentID);
                    if (drMarketData == null)// || pDepthMarketData.LastPrice == double.MinValue || pDepthMarketData.LastPrice == double.MaxValue)
                    {
                        this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, "freshMarketData:" + pDepthMarketData.InstrumentID + ":合约不在列表中.");
                        this.listMarketDatas.Remove(pDepthMarketData);
                        continue;
                    }

                    drMarketData["最新价"] = pDepthMarketData.LastPrice;		//最新价
                    drMarketData["涨跌"] = pDepthMarketData.LastPrice - pDepthMarketData.PreSettlementPrice;//涨跌
                    drMarketData["涨幅"] = ((pDepthMarketData.LastPrice - pDepthMarketData.PreSettlementPrice) / pDepthMarketData.PreSettlementPrice * 100);//涨幅
                    drMarketData["现手"] = pDepthMarketData.Volume - (int)drMarketData["总手"];			//现手
                    drMarketData["总手"] = pDepthMarketData.Volume;				//总手
                    drMarketData["仓差"] = pDepthMarketData.OpenInterest - pDepthMarketData.PreOpenInterest;	//仓差
                    if (pDepthMarketData.OpenInterest != double.MinValue && pDepthMarketData.OpenInterest != double.MaxValue)
                        drMarketData["持仓"] = pDepthMarketData.OpenInterest;	//持仓
                    else
                        drMarketData["持仓"] = 0;
                    if (pDepthMarketData.LastPrice != double.MinValue && pDepthMarketData.LastPrice != double.MaxValue)
                        drMarketData["最新价"] = pDepthMarketData.LastPrice;		//最新价
                    else
                        drMarketData["最新价"] = 0;
                    if (pDepthMarketData.BidPrice1 != double.MinValue && pDepthMarketData.BidPrice1 != double.MaxValue)
                        drMarketData["买价"] = pDepthMarketData.BidPrice1;						//买价
                    else
                        drMarketData["买价"] = drMarketData["最新价"];
                    drMarketData["买量"] = pDepthMarketData.BidVolume1;			//买量
                    if (pDepthMarketData.AskPrice1 != double.MinValue && pDepthMarketData.AskPrice1 != double.MaxValue)
                        drMarketData["卖价"] = pDepthMarketData.AskPrice1;					//卖价
                    else
                        drMarketData["卖价"] = drMarketData["最新价"];
                    drMarketData["卖量"] = pDepthMarketData.AskVolume1;			//卖量
                    int volumeMulti = (int)this.dtInstruments.Rows.Find(pDepthMarketData.InstrumentID)["合约数量"];
                    if (pDepthMarketData.AveragePrice != double.MinValue && pDepthMarketData.AveragePrice != double.MaxValue)
                    {
                        if (volumeMulti == 0)	//套利合约此值为0
                            drMarketData["均价"] = pDepthMarketData.AveragePrice;
                        else
                            drMarketData["均价"] = pDepthMarketData.AveragePrice / (drMarketData["交易所"].ToString() == "CZCE" ? 1 : volumeMulti);	//均价
                    }
                    if (pDepthMarketData.HighestPrice != double.MinValue && pDepthMarketData.HighestPrice != double.MaxValue)
                        drMarketData["最高"] = pDepthMarketData.HighestPrice;	//最高
                    if (pDepthMarketData.LowestPrice != double.MinValue && pDepthMarketData.LowestPrice != double.MaxValue)
                        drMarketData["最低"] = pDepthMarketData.LowestPrice;
                    if (pDepthMarketData.UpperLimitPrice != double.MinValue && pDepthMarketData.UpperLimitPrice != double.MaxValue)
                        drMarketData["涨停"] = pDepthMarketData.UpperLimitPrice;
                    if (pDepthMarketData.LowerLimitPrice != double.MinValue && pDepthMarketData.LowerLimitPrice != double.MaxValue)
                        drMarketData["跌停"] = pDepthMarketData.LowerLimitPrice;
                    if (pDepthMarketData.OpenPrice != double.MinValue && pDepthMarketData.OpenPrice != double.MaxValue)
                        drMarketData["开盘"] = pDepthMarketData.OpenPrice;
                    if (pDepthMarketData.PreSettlementPrice != double.MinValue && pDepthMarketData.PreSettlementPrice != double.MaxValue)
                        drMarketData["昨结"] = pDepthMarketData.PreSettlementPrice;

                    drMarketData["时间"] = pDepthMarketData.UpdateTime;
                    TimeSpan ts = DateTime.Now.TimeOfDay;
                    try
                    {
                        ts = TimeSpan.Parse(pDepthMarketData.UpdateTime + "." + pDepthMarketData.UpdateMillisec);
                    }
                    catch { }
                    drMarketData["时间差"] = (ts - DateTime.Now.TimeOfDay).TotalSeconds;				//与本地

                    this.BeginInvoke(new Action<DataRow, CThostFtdcDepthMarketDataField>(freshOrderRange), drMarketData, pDepthMarketData);
                    */
                    this.listMarketDatas.Remove(pDepthMarketData);//原来是 CThostFtdcDepthMarketDataField pDepthMarketData = this.listMarketDatas[0]; 改为CThostFtdcDepthMarketDataField pDepthMarketData = this.listMarketDatas[this.listMarketDatas.Count-1]; 后不删除，保留队列，让算法使用
                    #endregion
                }
                catch (Exception err)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, "freshMarketData:" + err.Message);
                }
            }
        }
        void freshOrderRange(DataRow dr, CThostFtdcDepthMarketDataField pDepthMarketData)
        {
            try
            {
                //刷新持仓盈亏
                for (int i = 0; i < this.listViewPosition.Items.Count; i++)
                {
                    ListViewItem lvi = this.listViewPosition.Items[i];
                    if (lvi.Text == pDepthMarketData.InstrumentID)
                    {
                        double positionPrice = double.Parse(lvi.SubItems[4].Text);	//持仓价格
                        int position = int.Parse(lvi.SubItems[3].Text);				//持仓数量
                        if (lvi.SubItems[1].Text == "多")
                            lvi.SubItems[5].Text = ((pDepthMarketData.LastPrice - positionPrice) * position * (int)this.dtInstruments.Rows.Find(pDepthMarketData.InstrumentID)["合约数量"]).ToString("F2");	//持仓盈亏
                        else
                            lvi.SubItems[5].Text = ((positionPrice - pDepthMarketData.LastPrice) * position * (int)this.dtInstruments.Rows.Find(pDepthMarketData.InstrumentID)["合约数量"]).ToString("F2");	//持仓盈亏
                    }
                }

                //刷新下单区域
                if (this.freshOrderPrice && this.comboBoxInstrument.Text == pDepthMarketData.InstrumentID && pDepthMarketData.LastPrice > 0)	//跟盘价//是否在下单区域显示行情
                {
                    this.numericUpDownPrice.Minimum = (decimal)(double)pDepthMarketData.LowerLimitPrice;
                    this.numericUpDownPrice.Maximum = (decimal)(double)pDepthMarketData.UpperLimitPrice;
                    //买:卖1价
                    if (this.comboBoxDirector.SelectedIndex == 0)
                        this.numericUpDownPrice.Value = (decimal)(double)dr["卖价"];
                    //卖:买1价
                    else
                        this.numericUpDownPrice.Value = (decimal)(double)dr["买价"];
                    this.numericUpDownPrice.Increment = (decimal)(double)this.dtInstruments.Rows.Find(pDepthMarketData.InstrumentID)["最小波动"];		//下单:最小波动

                    this.labelInstrumentName.Text = dr["名称"].ToString();
                    this.labelAskPrice.Text = dr["卖价"].ToString();		//卖
                    this.labelAskVolume.Text = dr["卖量"].ToString();		//量
                    this.labelBidPrice.Text = dr["买价"].ToString();		//买
                    this.labelBidVolume.Text = dr["买量"].ToString();		//量
                    this.labelWeiCha.Text = (pDepthMarketData.BidVolume1 - pDepthMarketData.AskVolume1).ToString();	//委差
                    this.labelWeiBi.Text = (pDepthMarketData.BidVolume1 + pDepthMarketData.AskVolume1 == 0 ? 0 :
                        (pDepthMarketData.BidVolume1 - pDepthMarketData.AskVolume1) / (pDepthMarketData.BidVolume1 + pDepthMarketData.AskVolume1)).ToString("P2");	//委比
                    this.labelLastPrice.Text = dr["最新价"].ToString();			//最新价
                    this.labelSettlementPrice.Text = ((double)dr["均价"]).ToString("F2");		//均价
                    this.labelUpDown.Text = dr["涨跌"].ToString();				//
                    this.labelPreSettlementPrice.Text = ((double)dr["昨结"]).ToString("F2");	//昨结
                    this.labelTotalVolume.Text = dr["总手"].ToString();			//总手
                    this.labelOpenPrice.Text = dr["开盘"].ToString();			//
                    this.labelUpperLimit.Text = dr["涨停"].ToString();			//
                    this.labelLowerLimit.Text = dr["跌停"].ToString();			//	
                    this.labelVolume.Text = dr["现手"].ToString();				//
                    this.labelHighest.Text = dr["最高"].ToString();				//
                    this.labelOpenInstetest.Text = dr["持仓"].ToString();		//
                    this.labelLowest.Text = dr["最低"].ToString();				//
                    this.labelPreOI.Text = pDepthMarketData.PreOpenInterest.ToString("F0");			//昨仓
                    this.labelOIDiff.Text = dr["仓差"].ToString();				//

                    this.labelUpper.Text = dr["涨停"].ToString();				//下单:涨板
                    this.labelLower.Text = dr["跌停"].ToString();				//下单:跌板	

                    //  drtick("ru1609");

                    if (this.comboBoxOffset.SelectedIndex == 0)		//开仓-最大开仓量:开仓且有可用资金时:可用资金/(价格*数量*保证金率)
                    {
                        this.labelVolumeMax.Text = this.textBoxFutureFetchAmount.Text == "-" ? "0"
                    : Math.Floor(double.Parse(this.textBoxFutureFetchAmount.Text.ToString()) / ((double)dr["最新价"] * (int)this.dtInstruments.Rows.Find(pDepthMarketData.InstrumentID)["合约数量"]
                    * (this.comboBoxDirector.SelectedIndex == 0 ? (double)this.dtInstruments.Rows.Find(pDepthMarketData.InstrumentID)["保证金-多"] : (double)this.dtInstruments.Rows.Find(pDepthMarketData.InstrumentID)["保证金-空"]))).ToString();	//可用资金/(价格*数量乘积*保证金率)
                    }
                    else //最大平仓量:从持仓中找
                    {
                        ListViewItem lviClose = this.listViewPosition.Items.Find(pDepthMarketData.InstrumentID
                            + (this.comboBoxDirector.SelectedIndex == 0 ? "Short" : "Long")
                            + (this.comboBoxOffset.SelectedIndex == 1 ? "History" : "Today"), false).FirstOrDefault();
                        if (lviClose == null)
                            this.labelVolumeMax.Text = "0";
                        else
                            this.labelVolumeMax.Text = lviClose.SubItems[3].Text;
                    }
                }
            }
            catch (Exception err)
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryDepthMarketData, "freshOrderRange,行情有错误:" + err.Message);
            }
        }
        #endregion

        #region 接口事件:连接/断开/登录/注销/查结算确认结果/查结算信息/确认结算/查合约
        //连接
        void tradeApi_OnFrontConnect()
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnConnected, "连接完成");
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Login, "登录...");
            tradeApi.UserLogin();	//登录
        }

        //登录响应:查询结算确认结果
        void tradeApi_OnRspUserLogin(ref CThostFtdcRspUserLoginField pRspUserLogin, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast)
            {
                if (pRspInfo.ErrorID != 0)
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnLogin, pRspInfo.ErrorMsg);
                else
                {
                    //  if (File.Exists("Resources\\rate" + "_" + tradeApi.InvestorID + ".xml"))
                    //    this.dtInstruments.ReadXml("Resources\\rate" + "_" + tradeApi.InvestorID + ".xml");	//读取合约信息

                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnLogin, "登录完成");
                    tradeApi.FrontID = pRspUserLogin.FrontID;
                    tradeApi.SessionID = pRspUserLogin.SessionID;
                    tradeApi.MaxOrderRef = Int32.Parse(pRspUserLogin.MaxOrderRef);
                    //时间设置
                    try
                    {
                        tsSHFE = DateTime.Now.TimeOfDay - TimeSpan.Parse(pRspUserLogin.SHFETime);
                    }
                    catch
                    {
                        tsSHFE = new TimeSpan(0, 0, 0);
                    }
                    try
                    {
                        tsCZCE = DateTime.Now.TimeOfDay - TimeSpan.Parse(pRspUserLogin.CZCETime);
                    }
                    catch
                    {
                        tsCZCE = tsSHFE;
                    }
                    try
                    {
                        tsDCE = DateTime.Now.TimeOfDay - TimeSpan.Parse(pRspUserLogin.DCETime);
                    }
                    catch
                    {
                        tsDCE = tsSHFE;
                    }
                    try
                    {
                        tsCFFEX = DateTime.Now.TimeOfDay - TimeSpan.Parse(pRspUserLogin.FFEXTime);
                    }
                    catch
                    {
                        tsCFFEX = tsSHFE;
                    }

                    if (ul.Visible)	//登录:首次连接才执行确认
                    {
                        //Properties.Settings.Default.Servers[ul.cbServer.SelectedIndex] = Properties.Settings.Default.Servers[ul.cbServer.SelectedIndex].
                        this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QrySettleConfirmInfo, "查结算确认结果...");
                        tradeApi.QrySettlementInfoConfirm();//查询结算信息确认
                    }
                }
            }
        }

        //查询确认结算响应:没确认过,则查询结算信息予以确认
        void tradeApi_OnRspQrySettlementInfoConfirm(ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast && pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQrySettleConfirmInfo, "查询确认结果完成.");
                Thread.Sleep(1000);
                //今天确认过:不再显示确认信息
                if (pSettlementInfoConfirm.BrokerID != "" && DateTime.ParseExact(pSettlementInfoConfirm.ConfirmDate, "yyyyMMdd", null) >= DateTime.Today)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QryInstrument, "查合约...");
                    tradeApi.QryInstrument();
                }
                else
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQrySettleInfo, "查结算信息...");
                    tradeApi.QrySettlementInfo();	//查结算信息
                }
            }
            if (pRspInfo.ErrorID != 0)
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQrySettleInfo, pRspInfo.ErrorMsg);
                this.Close();
            }
        }

        //查结算信息响应:查合约
        void tradeApi_OnRspQrySettlementInfo(ref CThostFtdcSettlementInfoField pSettlementInfo, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            strInfo += pSettlementInfo.Content;
            if (bIsLast)
            {
                if (strInfo == string.Empty)	//无结算
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, "无结算信息.");
                else
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQrySettleInfo, "查询确认信息完成.");
                if (pRspInfo.ErrorID == 0)
                {
                    if (ul.Visible)
                    {
                        Thread.Sleep(1000);
                        this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QryInstrument, "查合约...");
                        tradeApi.QryInstrument();
                    }
                    else //登录后,查历史结算
                    {
                        this.BeginInvoke(new Action(showHistorySettleInfo));
                    }
                }
                else
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQrySettleInfo, pRspInfo.ErrorMsg);
                    this.Close();
                }
                this.apiIsBusy = false;
            }
        }
        void showHistorySettleInfo()
        { this.richTextBoxSettleInfo.Text = strInfo; strInfo = string.Empty; }//清空信息
        // 查合约响应
        void tradeApi_OnRspQryInstrument(ref CThostFtdcInstrumentField pInstrument, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                //合约/名称/交易所/合约数量乘数/最小波动/多头保证金率/空头保证金率/手续费/限价单下单最大量/最小量/自选
                DataRow drInstrument;
                if ((drInstrument = this.dtInstruments.Rows.Find(pInstrument.InstrumentID)) == null)
                {

                    drInstrument = this.dtInstruments.Rows.Add(pInstrument.InstrumentID, pInstrument.InstrumentName, pInstrument.ExchangeID, pInstrument.VolumeMultiple, pInstrument.PriceTick
              , pInstrument.LongMarginRatio, pInstrument.ShortMarginRatio, 0, pInstrument.MaxLimitOrderVolume, pInstrument.MinMarketOrderVolume, false);
                }
                //"合约","名称","交易所"最新价""涨跌","涨幅","现手","总手","持仓","仓差","买价","买量","卖价","卖量","均价","最高","最低","涨停","跌停","开盘",
                //"昨结","时间","时间差,"自选",
                DataRow drMartketData;
                for (int il = 0; il < idlist.Length; il++)
                {
                    if (pInstrument.InstrumentID == idlist[il])
                    {
                        drMartketData = this.dtMarketData.Rows.Add(pInstrument.InstrumentID, pInstrument.InstrumentName, pInstrument.ExchangeID, 0, 0, 0,
                           0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, false, false);
                        //是否在自选中
                        if (Properties.Settings.Default.Instruments.IndexOf(pInstrument.InstrumentID) >= 0)
                        {
                            drInstrument["自选"] = true;
                            drMartketData["自选"] = true;
                            this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryInstrumentCommissionRate, drInstrument["合约"].ToString())); //待查手续费
                        }
                        else
                            this.listQry.Add(new QryOrder(EnumQryOrder.QryInstrumentCommissionRate, drInstrument["合约"].ToString()));	//非自选,放在后面
                        if (pInstrument.InstrumentID.StartsWith("SP"))
                        {
                            drMartketData["套利"] = true;
                            drInstrument["套利"] = true;
                        }
                    }
                }
                if (bIsLast)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryInstrument, "合约查询完成.");
                    ul.DialogResult = System.Windows.Forms.DialogResult.OK;	//显示主界面
                }
            }
        }

        //确认结算响应
        void tradeApi_OnRspSettlementInfoConfirm(ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnSettleConfirm, "确认结算完成");
                //Thread.Sleep(1000);
                //this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QryPosition, "查持仓...");
                //tradeApi.QryInvestorPosition();	//查持仓
            }
            else
            { this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnSettleConfirm, pRspInfo.ErrorMsg); this.Close(); }
        }

        void tradeApi_OnRspUserLogout(ref CThostFtdcUserLogoutField pUserLogout, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast)
            {
                if (pRspInfo.ErrorID != 0)
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnLogout, pRspInfo.ErrorMsg);
            }
        }

        void tradeApi_OnRspError(ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnHeartBeatWarning(int pTimeLapes)
        {
            //showStructInListView();
        }

        void tradeApi_OnDisConnected(int reason)
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnDisConnect, "断开");
        }
        #endregion

        #region 响应: 查询委托/成交/持仓/资金
        // 查委托响应
        void tradeApi_OnRspQryOrder(ref CThostFtdcOrderField pOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                if (pOrder.BrokerID != "")
                    this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pOrder, pOrder.FrontID + "," + pOrder.SessionID + "," + pOrder.OrderRef));
                if (bIsLast)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryOrder, "查报单完成");
                    //if (pOrder.BrokerID != "")	//返回空记录不再查成交
                    //{
                    //Thread.Sleep(1000);
                    //this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QryTrade, "查成交...");
                    //tradeApi.QryTrade();	//查成交
                    //}
                }
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryOrder, pRspInfo.ErrorMsg);
            if (bIsLast)
                this.apiIsBusy = false;	//查询完成
        }

        // 查询成交响应
        void tradeApi_OnRspQryTrade(ref CThostFtdcTradeField pTrade, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                if (pTrade.BrokerID != "")
                    this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pTrade, pTrade.OrderSysID));
                if (bIsLast)
                {
                    if (pTrade.BrokerID != "")
                        this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryTrade, "查成交完成");
                    else
                        this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, "无成交");
                }
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryTrade, pRspInfo.ErrorMsg);
            if (bIsLast)
            {
                this.apiIsBusy = false;	//查询完成
            }
        }

        // 查持仓汇总响应
        void tradeApi_OnRspQryInvestorPosition(ref CThostFtdcInvestorPositionField pInvestorPosition, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                if (pInvestorPosition.BrokerID != "")
                {
                    for (int i = 0; i < ilist.Count; i++)
                    {
                        if (pInvestorPosition.InstrumentID == ilist[i].ID)
                        {
                            if (pInvestorPosition.TodayPosition > 0)
                            {
                                if (pInvestorPosition.PosiDirection == EnumPosiDirectionType.Long)
                                {
                                    ilist[i].今买量 = pInvestorPosition.TodayPosition;
                                    ilist[i].买成本价 = pInvestorPosition.SettlementPrice;
                                }

                                if (pInvestorPosition.PosiDirection == EnumPosiDirectionType.Short)
                                {
                                    ilist[i].今卖量 = pInvestorPosition.TodayPosition;
                                    ilist[i].卖成本价 = pInvestorPosition.SettlementPrice;
                                }

                            }
                            if (pInvestorPosition.YdPosition > 0)
                            {
                                if (pInvestorPosition.PosiDirection == EnumPosiDirectionType.Long)
                                {
                                    ilist[i].昨买量 = pInvestorPosition.YdPosition;
                                    ilist[i].买成本价 = pInvestorPosition.PreSettlementPrice;
                                }

                                if (pInvestorPosition.PosiDirection == EnumPosiDirectionType.Short)
                                {
                                    ilist[i].昨卖量 = pInvestorPosition.YdPosition;
                                    ilist[i].卖成本价 = pInvestorPosition.PreSettlementPrice;
                                }
                            }
                            break;
                        }
                    }
                    this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pInvestorPosition, pInvestorPosition.InstrumentID + pInvestorPosition.PosiDirection + pInvestorPosition.PositionDate));

                }
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryPosition, pRspInfo.ErrorMsg);
            if (bIsLast)
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryPosition, "查持仓完成");
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryPosition, "查资金...");
                this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryTradingAccount, null));//查资金
                this.apiIsBusy = false;	//查询完成
            }
        }
        // 查持仓明细响应 == 暂时未用
        void tradeApi_OnRspQryInvestorPositionDetail(ref CThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pInvestorPositionDetail, pInvestorPositionDetail.InstrumentID + pInvestorPositionDetail.Direction + pInvestorPositionDetail.TradeID));
                if (bIsLast)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryPositionDetail, "查持仓明细完成");
                }
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryPositionDetail, pRspInfo.ErrorMsg);
        }

        // 查询帐户资金响应
        void tradeApi_OnRspQryTradingAccount(ref CThostFtdcTradingAccountField pTradingAccount, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                if (bIsLast)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryAccount, "查资金完成");
                    this.BeginInvoke(new Action<CThostFtdcTradingAccountField>(freshAccount), pTradingAccount);
                }
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryAccount, pRspInfo.ErrorMsg);
            if (bIsLast)
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryPosition, "查资金完成");
                this.apiIsBusy = false;	//查询完成
            }
        }
        void freshAccount(CThostFtdcTradingAccountField pTradingAccount)
        {
            this.userControlTradeAccount1.Account = pTradingAccount;
            this.textBoxFutureFetchAmount.Text = pTradingAccount.WithdrawQuota.ToString();	//可用资金:银转
        }
        #endregion

        #region 响应:下单/撤单
        //CTP:下单有误
        void tradeApi_OnErrRtnOrderInsert(ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo)
        {
            //if (pRspInfo.ErrorID != 0)
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnErrOrderInsert, pRspInfo.ErrorMsg);
        }

        //Exchange:下单有误:使用CTP即可接收此回报
        void tradeApi_OnRspOrderInsert(ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            //if (pRspInfo.ErrorID != 0)
            //	this.BeginInvoke(new Action<EnumProgessState, string>(progress),EnumProgessState, pRspInfo.ErrorMsg);
        }

        // 委托成功
        void tradeApi_OnRtnOrder(ref CThostFtdcOrderField pOrder)
        {
            //刷新显示
            this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pOrder, pOrder.FrontID + "," + pOrder.SessionID + "," + pOrder.OrderRef));
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnRtnOrder, pOrder.StatusMsg);
        }



        //CTP:撤单有误
        void tradeApi_OnErrRtnOrderAction(ref CThostFtdcOrderActionField pOrderAction, ref CThostFtdcRspInfoField pRspInfo)
        {
            //if (pRspInfo.ErrorID != 0)
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnErrOrderAction, "撤单失败:" + pRspInfo.ErrorMsg);
        }

        //Exchange:撤单成功
        void tradeApi_OnRspOrderAction(ref CThostFtdcInputOrderActionField pInputOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnOrderAction, pInputOrderAction.OrderSysID);
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnOrderAction, pRspInfo.ErrorMsg);
        }
        #endregion

        #region 单次行情响应
        void tradeApi_OnRspQryDepthMarketData(ref CThostFtdcDepthMarketDataField pDepthMarketData, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
        }

        void tradeApi_OnRtnTradingNotice(ref CThostFtdcTradingNoticeInfoField pTradingNoticeInfo)
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnRtnTradingNotice, pTradingNoticeInfo.SendTime + ":" + pTradingNoticeInfo.FieldContent);
            MessageBox.Show(pTradingNoticeInfo.FieldContent, "提示", MessageBoxButtons.OK);
        }

        void tradeApi_OnRtnInstrumentStatus(ref CThostFtdcInstrumentStatusField pInstrumentStatus)
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnRtnInstrumentStatus, pInstrumentStatus.InstrumentID + ":" + pInstrumentStatus.InstrumentStatus.ToString());
        }

        void tradeApi_OnRtnErrorConditionalOrder(ref CThostFtdcErrorConditionalOrderField pErrorConditionalOrder)
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pErrorConditionalOrder.OrderSysID + ":" + pErrorConditionalOrder.ErrorMsg);
        }

        void tradeApi_OnRspQueryMaxOrderVolume(ref CThostFtdcQueryMaxOrderVolumeField pQueryMaxOrderVolume, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryTradingNotice(ref CThostFtdcTradingNoticeField pTradingNotice, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryTradingCode(ref CThostFtdcTradingCodeField pTradingCode, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryNotice(ref CThostFtdcNoticeField pNotice, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryInvestorPositionCombineDetail(ref CThostFtdcInvestorPositionCombineDetailField pInvestorPositionCombineDetail, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryInvestor(ref CThostFtdcInvestorField pInvestor, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryExchange(ref CThostFtdcExchangeField pExchange, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryCFMMCTradingAccountKey(ref CThostFtdcCFMMCTradingAccountKeyField pCFMMCTradingAccountKey, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryBrokerTradingParams(ref CThostFtdcBrokerTradingParamsField pBrokerTradingParams, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }

        void tradeApi_OnRspQryBrokerTradingAlgos(ref CThostFtdcBrokerTradingAlgosField pBrokerTradingAlgos, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.Other, pRspInfo.ErrorMsg);
        }
        #endregion

        #region 修改密码
        void tradeApi_OnRspUserPasswordUpdate(ref CThostFtdcUserPasswordUpdateField pUserPasswordUpdate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnUpdateUserPassword, pRspInfo.ErrorMsg);
            else
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnUpdateUserPassword, "修改用户密码完成");
                MessageBox.Show(this, "密码修改成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.BeginInvoke(new Action<object, EventArgs>(this.buttonReset_Click), null, null);
            }
        }

        void tradeApi_OnRspTradingAccountPasswordUpdate(ref CThostFtdcTradingAccountPasswordUpdateField pTradingAccountPasswordUpdate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnUpdateAccountPassword, pRspInfo.ErrorMsg);
            else
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnUpdateAccountPassword, "修改资金密码完成");
                MessageBox.Show(this, "密码修改成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.BeginInvoke(new Action<object, EventArgs>(this.buttonReset_Click), null, null);
            }
        }
        #endregion

        #region 银期转帐

        void tradeApi_OnRspQryContractBank(ref CThostFtdcContractBankField pContractBank, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0 && pContractBank.BrokerID != "")
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(pContractBank.BankID);
                lvi.SubItems.Add(pContractBank.BankName);
                lvi.SubItems.Add(pContractBank.BankBrchID);
                this.listViewBank.Items.Add(lvi);
                if (bIsLast)
                {
                    this.textBoxBankID.Text = this.listViewBank.Items[0].SubItems[0].Text;
                    this.textBoxBankName.Text = this.listViewBank.Items[0].SubItems[1].Text;
                    //tradeApi.QryTradingAccount();	//查可取资金
                }
            }
            if (bIsLast)
                this.apiIsBusy = false;
        }

        //查询转帐银行响应:与查询签约银行结果相同:不使用
        void tradeApi_OnRspQryTransferBank(ref CThostFtdcTransferBankField pTransferBank, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            return;
        }

        //期-银:银行处理完成
        void tradeApi_OnRtnFromFutureToBankByFuture(ref CThostFtdcRspTransferField pRspTransfer)
        {
            //插入到流水中
            //ListViewItem lvi = new ListViewItem(pRspTransfer.PlateSerial.ToString());
            //lvi.SubItems.Add(pRspTransfer.BankID);
            //lvi.SubItems.Add(pRspTransfer.BankAccount);
            //lvi.SubItems.Add(pRspTransfer.TradeDate + " " + pRspTransfer.TradeTime);
            //lvi.SubItems.Add(pRspTransfer.TradeCode);
            //lvi.SubItems.Add(pRspTransfer.TradeAmount.ToString());
            //lvi.SubItems.Add(pRspTransfer.TransferStatus == EnumTransferStatusType.Normal ? "正常" : "被冲正");
            //lvi.SubItems.Add(pRspTransfer.ErrorMsg);	//处理结果
            this.BeginInvoke(new Action<CThostFtdcTransferSerialField>(addLvi2ListViewSeries), pRspTransfer);
        }
        //期-银:交易核心响应
        void tradeApi_OnRspFromFutureToBankByFuture(ref CThostFtdcReqTransferField pReqTransfer, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast && pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnFutureToBank, pRspInfo.ErrorMsg);
        }
        //银-期
        void tradeApi_OnRtnFromBankToFutureByFuture(ref CThostFtdcRspTransferField pRspTransfer)
        {
            //插入到流水中
            //ListViewItem lvi = new ListViewItem(pRspTransfer.PlateSerial.ToString());
            //lvi.SubItems.Add(pRspTransfer.BankID);
            //lvi.SubItems.Add(pRspTransfer.BankAccount);
            //lvi.SubItems.Add(pRspTransfer.TradeDate + " " + pRspTransfer.TradeTime);
            //lvi.SubItems.Add(pRspTransfer.TradeCode);
            //lvi.SubItems.Add(pRspTransfer.TradeAmount.ToString());
            //lvi.SubItems.Add(pRspTransfer.TransferStatus == EnumTransferStatusType.Normal ? "正常" : "被冲正");
            //lvi.SubItems.Add(pRspTransfer.ErrorMsg);	//处理结果
            this.BeginInvoke(new Action<CThostFtdcTransferSerialField>(addLvi2ListViewSeries), pRspTransfer);
        }

        void tradeApi_OnRspFromBankToFutureByFuture(ref CThostFtdcReqTransferField pReqTransfer, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast && pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnBankToFuture, pRspInfo.ErrorMsg);
        }
        //查银行余额
        void tradeApi_OnRspQueryBankAccountMoneyByFuture(ref CThostFtdcReqQueryAccountField pReqQueryAccount, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (bIsLast && pRspInfo.ErrorID != 0)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryBankAccountMoney, pRspInfo.ErrorMsg);
            if (bIsLast)
                this.apiIsBusy = false;
        }

        void tradeApi_OnRtnQueryBankBalanceByFuture(ref CThostFtdcNotifyQueryAccountField pNotifyQueryAccount)
        {
            MessageBox.Show("可用余额:" + pNotifyQueryAccount.BankUseAmount.ToString());
            this.apiIsBusy = false;
        }
        //查转帐流水
        void tradeApi_OnRspQryTransferSerial(ref CThostFtdcTransferSerialField pTransferSerial, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action<CThostFtdcTransferSerialField>(addLvi2ListViewSeries), pTransferSerial);
            }
            if (bIsLast)
                this.apiIsBusy = false;
        }
        void addLvi2ListViewSeries(CThostFtdcTransferSerialField pTransferSerial)
        {
            //ListViewItem[] lvis = this.listViewSeries.Items.Find(pTransferSerial.BankID + pTransferSerial.BankSerial, false);
            //if (lvis.Length == 0)
            {
                ListViewItem lvi = new ListViewItem(pTransferSerial.PlateSerial.ToString());	//流水号
                lvi.Name = pTransferSerial.BankID + pTransferSerial.BankSerial;
                lvi.SubItems.Add(pTransferSerial.BankID);						//银行
                lvi.SubItems.Add(pTransferSerial.TradeDate + " " + pTransferSerial.TradeTime);//时间
                lvi.SubItems.Add(pTransferSerial.TradeCode);					//交易代码
                lvi.SubItems.Add(pTransferSerial.TradeAmount.ToString());		//交易金额
                lvi.SubItems.Add(pTransferSerial.AvailabilityFlag == EnumAvailabilityFlagType.Invalid ? "未确认" :
                    pTransferSerial.AvailabilityFlag == EnumAvailabilityFlagType.Repeal ? "冲正" : "有效");
                lvi.SubItems.Add(pTransferSerial.ErrorMsg);						//处理结果 
                this.listViewSeries.Items.Add(lvi);
            }
        }
        #endregion

        #region 预埋单
        //预埋报单
        void tradeApi_OnRspParkedOrderInsert(ref CThostFtdcParkedOrderField pParkedOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pParkedOrder, pParkedOrder.ParkedOrderID));
                if (bIsLast)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnParkedOrder, "查预埋单完成");
                }
            }
            else if (bIsLast)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnParkedOrder, pRspInfo.ErrorMsg);
        }
        //预埋撤单
        void tradeApi_OnRspParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pParkedOrderAction, pParkedOrderAction.ParkedOrderActionID));
                if (bIsLast)
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnParkedOrderAction, "删除预埋撤单完成");
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnParkedOrderAction, pRspInfo.ErrorMsg);
        }
        //删除预埋报单
        void tradeApi_OnRspRemoveParkedOrder(ref CThostFtdcRemoveParkedOrderField pRemoveParkedOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
                this.hfListViewParkedOrder.Items.RemoveByKey(pRemoveParkedOrder.ParkedOrderID);
            else if (bIsLast)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnRemovePackedOrder, pRspInfo.ErrorMsg);
        }
        //删除预埋撤单
        void tradeApi_OnRspRemoveParkedOrderAction(ref CThostFtdcRemoveParkedOrderActionField pRemoveParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
                this.hfListViewParkedAction.Items.RemoveByKey(pRemoveParkedOrderAction.ParkedOrderActionID);
            else if (bIsLast)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnRemovePackedOrderAction, pRspInfo.ErrorMsg);
        }
        //查预埋单
        void tradeApi_OnRspQryParkedOrder(ref CThostFtdcParkedOrderField pParkedOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                if (pParkedOrder.BrokerID != "")
                    this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pParkedOrder, pParkedOrder.ParkedOrderID));
                if (bIsLast)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryParkedOrder, "查预埋单完成");
                    Thread.Sleep(1000);
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QryParkedOrderAction, "查预埋撤单...");
                    //tradeApi.ReqQryParkedOrderAction();	//查撤单
                    this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryParkedOrderAction, null));
                }
            }
            else if (bIsLast)
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryParkedOrder, pRspInfo.ErrorMsg);
            if (bIsLast)
                this.apiIsBusy = false;
        }
        //查预埋撤单
        void tradeApi_OnRspQryParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID == 0)
            {
                this.BeginInvoke(new Action<ObjectAndKey>(showStructInListView), new ObjectAndKey(pParkedOrderAction, pParkedOrderAction.ParkedOrderActionID));
                if (bIsLast)
                {
                    this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryParkedOrderAction, "查预埋撤单完成");
                }
            }
            else
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnQryParkedOrderAction, pRspInfo.ErrorMsg);
            if (bIsLast)
                this.apiIsBusy = false;
        }
        #endregion

        #region //拆分器:拆分栏单击:显/隐行情
        private void splitter2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)  //最大化
                this.WindowState = FormWindowState.Normal;
            this.panelTop.Visible = !this.panelTop.Visible;
            if (this.panelTop.Visible)
            {
                this.MaximizeBox = true;        //显示行情栏时才显示最大化按钮
                this.Height += Properties.Settings.Default.TopHeigh; //267;// this.panelTop.Height;
                this.Top -= Properties.Settings.Default.TopHeigh;
                this.splitter2.Top += Properties.Settings.Default.TopHeigh;
                this.splitter2.Cursor = Cursors.PanSouth;
            }
            else
            {
                this.MaximizeBox = false;        //显示行情栏时才显示最大化按钮
                this.Height -= Properties.Settings.Default.TopHeigh;
                this.Top += Properties.Settings.Default.TopHeigh;
                this.splitter2.Top -= Properties.Settings.Default.TopHeigh;
                this.splitter2.Cursor = Cursors.PanNorth;
            }
        }
        //拆分器:禁止移动拆分栏
        private void splitter2_SplitterMoving(object sender, SplitterEventArgs e)
        {
            e.SplitY = (sender as Splitter).Top;
        }

        //行情栏:选择不同的交易所:刷新显示
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name != "tabPage1" && tabControl1.SelectedTab.Name != "tabPage2")
            {
                DoubleBufferDGV dgv = (DoubleBufferDGV)this.tabControl1.SelectedTab.Controls["dataGridView" + this.tabControl1.SelectedTab.Name.Replace("tabPage", "")];
                dgv.ScrollBars = ScrollBars.Vertical;
                dgv.ScrollBars = ScrollBars.Both;
            }
        }
        //行情栏:设置行情显示
        private void setDataGridViewOfMarketData()
        {
            foreach (string _name in (new string[] { "SHFE", "CZCE", "DCE", "CFFEX", "Selected", "Arbitrage" }))
            {
                DoubleBufferDGV dgv = (DoubleBufferDGV)this.Controls.Find("dataGridView" + _name, true).First();
                dgv.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(dgv_CellToolTipTextNeeded);
                dgv.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvMD_CellMouseClick);
                dgv.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dgvMD_CellMouseDoubleClick);
                dgv.CellMouseEnter += new DataGridViewCellEventHandler(dgvMD_CellMouseEnter);
                dgv.CellMouseLeave += new DataGridViewCellEventHandler(dgv_CellMouseLeave);
                dgv.DataError += new DataGridViewDataErrorEventHandler(dgv_DataError);
                dgv.ReadOnly = true;		//只读
                dgv.AutoGenerateColumns = true;
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//列标题对齐
                dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;		//列标题不换行
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;//单元格居中对齐
                dgv.DefaultCellStyle.NullValue = "-";		//空值显示
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;//.AllCells;//列宽调整方式:此处会影响刷新速度
                dgv.ScrollBars = ScrollBars.Both;

                DataView dv = new DataView(this.dtMarketData);
                if (_name == "Selected")
                    dv.RowFilter = "自选=true";
                else if (_name == "Arbitrage")
                    dv.RowFilter = "套利=true";
                else
                    dv.RowFilter = "交易所='" + _name.ToString() + "'";
                dgv.DataSource = dv;
                //dgv.DataMember = this.dtMarketData.TableName;
                dgv.Columns["买价"].DefaultCellStyle.BackColor = Color.LawnGreen;
                dgv.Columns["卖价"].DefaultCellStyle.BackColor = Color.Orange;
                dgv.Columns["涨停"].DefaultCellStyle.ForeColor = Color.Red;
                dgv.Columns["跌停"].DefaultCellStyle.ForeColor = Color.CadetBlue;
                dgv.Columns["交易所"].Visible = false;
                dgv.Columns["自选"].Visible = false;
                dgv.Columns["套利"].Visible = false;
                //格式
                dgv.Columns["合约"].Frozen = true;		//冻结
                //dgv.Columns["合约"].DefaultCellStyle.Format = "";
                //dgv.Columns["名称"].DefaultCellStyle.Format = "";
                //dgv.Columns["交易所"].DefaultCellStyle.Format = "";
                dgv.Columns["最新价"].DefaultCellStyle.Format = "F2";
                dgv.Columns["涨跌"].DefaultCellStyle.Format = "F2";
                dgv.Columns["涨幅"].DefaultCellStyle.Format = "F2";
                //dgv.Columns["现手"].DefaultCellStyle.Format = "";
                //dgv.Columns["总手"].DefaultCellStyle.Format = "";
                //dgv.Columns["持仓"].DefaultCellStyle.Format = "";
                //dgv.Columns["仓差"].DefaultCellStyle.Format = "";
                dgv.Columns["买价"].DefaultCellStyle.Format = "F2";
                //dgv.Columns["买量"].DefaultCellStyle.Format = "";
                dgv.Columns["卖价"].DefaultCellStyle.Format = "F2";
                //dgv.Columns["卖量"].DefaultCellStyle.Format = "";
                dgv.Columns["均价"].DefaultCellStyle.Format = "F2";
                dgv.Columns["最高"].DefaultCellStyle.Format = "F2";
                dgv.Columns["最低"].DefaultCellStyle.Format = "F2";
                dgv.Columns["涨停"].DefaultCellStyle.Format = "F2";
                dgv.Columns["跌停"].DefaultCellStyle.Format = "F2";
                dgv.Columns["开盘"].DefaultCellStyle.Format = "F2";
                dgv.Columns["昨结"].DefaultCellStyle.Format = "F2";
                //dgv.Columns["时间"].DefaultCellStyle.Format = "";
                dgv.Columns["时间差"].DefaultCellStyle.Format = "F2";
                //列宽
                dgv.Columns["合约"].Width = 60;
                dgv.Columns["名称"].Width = 60;
                dgv.Columns["交易所"].Width = 60;
                dgv.Columns["最新价"].Width = 60;
                dgv.Columns["涨跌"].Width = 60;
                dgv.Columns["涨幅"].Width = 60;
                dgv.Columns["现手"].Width = 60;
                dgv.Columns["总手"].Width = 60;
                dgv.Columns["持仓"].Width = 60;
                dgv.Columns["仓差"].Width = 60;
                dgv.Columns["买价"].Width = 60;
                dgv.Columns["买量"].Width = 40;
                dgv.Columns["卖价"].Width = 60;
                dgv.Columns["卖量"].Width = 40;
                dgv.Columns["均价"].Width = 60;
                dgv.Columns["最高"].Width = 60;
                dgv.Columns["最低"].Width = 60;
                dgv.Columns["涨停"].Width = 60;
                dgv.Columns["跌停"].Width = 60;
                dgv.Columns["开盘"].Width = 60;
                dgv.Columns["昨结"].Width = 60;
                dgv.Columns["时间"].Width = 60;
                dgv.Columns["时间差"].Width = 60;
            }
        }
        //行情栏:显示提示
        void dgv_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (Properties.Settings.Default.ShowTootip)
            {
                if ((sender as DataGridView).Name.EndsWith("Instruments")) //合约
                    e.ToolTipText = "双击添加到自选合约";
                else if ((sender as DataGridView).Name.EndsWith("InstrumentsSelected")) //自选合约
                    e.ToolTipText = "单击选择合约,下单区开仓.\n双击删除自选合约";
                else if ((sender as DataGridView).Name.EndsWith("CustomVolume")) //自定义手数
                    e.ToolTipText = "自定义快速开仓手数";
                else if ((sender as DataGridView).Parent.Parent == this.tabControl1) //行情栏
                    e.ToolTipText = "单击买价/卖价选择合约到下单区\n双击买价快速开空仓\n双击卖价快速开多仓";
            }
        }
        //行情栏:鼠标移过:反转颜色
        void dgvMD_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DoubleBufferDGV dgv = (sender as DoubleBufferDGV);
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                DataGridViewColumn dgvc = dgv.Columns[e.ColumnIndex];
                if (dgvc.Name == "买价" || dgvc.Name == "卖价")
                {
                    DataGridViewCell cell = dgv[e.ColumnIndex, e.RowIndex];
                    cell.Style.BackColor = Color.BlanchedAlmond;// dgvc.DefaultCellStyle.ForeColor;//无效
                    cell.Style.ForeColor = dgvc.DefaultCellStyle.BackColor;
                }
            }
        }
        //行情栏:颜色恢复正常
        void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            DoubleBufferDGV dgv = (sender as DoubleBufferDGV);
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                DataGridViewColumn dgvc = dgv.Columns[e.ColumnIndex];
                if (dgvc.Name == "买价" || dgvc.Name == "卖价")
                {
                    DataGridViewCell cell = dgv[e.ColumnIndex, e.RowIndex];
                    cell.Style.BackColor = dgvc.DefaultCellStyle.BackColor;
                    cell.Style.ForeColor = dgvc.DefaultCellStyle.ForeColor;
                }
            }
        }
        //行情栏:单击:买卖价,选择合约
        void dgvMD_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DoubleBufferDGV dgv = (DoubleBufferDGV)sender;
                    string instrument = (string)dgv["合约", e.RowIndex].Value;
                    if (dgv.Columns[e.ColumnIndex].Name == "买价")
                    {
                        this.orderRangeChangeInstrument(instrument, EnumDirectionType.Sell, EnumOffsetFlagType.Open);
                    }
                    else if (dgv.Columns[e.ColumnIndex].Name == "卖价")
                    {
                        this.orderRangeChangeInstrument(instrument, EnumDirectionType.Buy, EnumOffsetFlagType.Open);
                    }
                }
            }
            catch (Exception err)
            {
                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, err.Message);
            }
        }
        #endregion
        #region//下单区合约改变
        void orderRangeChangeInstrument(string _instrument, EnumDirectionType _direction, EnumOffsetFlagType _offset, decimal _closeLots = 0)
        {
            if (_instrument != this.comboBoxInstrument.Text) //不在合约显示中
            {
                //清除行情显示
                foreach (Control crl in this.tableLayoutPanel1.Controls)
                {
                    if (crl.Text != string.Empty && char.IsDigit(crl.Text[crl.Text.Length - 1]))	//已有显示数字
                    {
                        crl.Text = "-";
                    }
                }
            }
            this.comboBoxDirector.SelectedIndex = _direction == EnumDirectionType.Buy ? 0 : 1;		//买/卖
            this.comboBoxOffset.SelectedIndex = _offset == EnumOffsetFlagType.Open ? 0 : _offset == EnumOffsetFlagType.CloseToday ? 2 : 1;			//开
            this.comboBoxInstrument.Text = _instrument;
            DataRow dr = this.dtMarketData.Rows.Find(_instrument);
            this.labelInstrumentName.Text = (string)dr["名称"];

            this.labelUpper.Text = dr["涨停"].ToString();			//涨停
            this.labelLower.Text = dr["跌停"].ToString();			//跌停
            this.numericUpDownPrice.Maximum = decimal.Parse(this.labelUpper.Text);	//涨停
            this.numericUpDownPrice.Minimum = decimal.Parse(this.labelLower.Text);	//跌停
            this.numericUpDownPrice.Value = (decimal)(double)(_direction == EnumDirectionType.Buy ? dr["卖价"] : dr["买价"]);	//价格
            if (_offset == EnumOffsetFlagType.Open)
            {
                string lots = Properties.Settings.Default.CustomVolume[Properties.Settings.Default.CustomVolume.IndexOf(_instrument) + 1];//手数
                this.numericUpDownVolume.Value = lots == null ? 1 : int.Parse(lots);	//手数:默认为1
                this.labelVolumeMax.Text = this.textBoxFutureFetchAmount.Text == "-" ? "0"
                : Math.Floor(double.Parse(this.textBoxFutureFetchAmount.Text.ToString()) / ((double)dr["最新价"] * (int)this.dtInstruments.Rows.Find(_instrument)["合约数量"]
                * (this.comboBoxDirector.SelectedIndex == 0 ? (double)this.dtInstruments.Rows.Find(_instrument)["保证金-多"] : (double)this.dtInstruments.Rows.Find(_instrument)["保证金-空"]))).ToString();	//可用资金/(价格*数量乘积*保证金率)
            }
            else
            {
                this.numericUpDownVolume.Value = _closeLots;
                this.labelVolumeMax.Text = _closeLots.ToString();
            }
        }

        //行情栏:双击:快速开仓
        void dgvMD_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DoubleBufferDGV dgv = (DoubleBufferDGV)sender;
                string instrument = (string)dgv["合约", e.RowIndex].Value;
                double price = (double)dgv["最新价", e.RowIndex].Value;
                if (dgv.Columns[e.ColumnIndex].Name == "买价" || dgv.Columns[e.ColumnIndex].Name == "卖价")
                {
                    this.progress(EnumProgessState.OrderInsert, "行情-快速下单.");
                    string lots = Properties.Settings.Default.CustomVolume[Properties.Settings.Default.CustomVolume.IndexOf(instrument) + 1];//手数
                    double ticks = Properties.Settings.Default.FlowPriceAddTick ? (int)Properties.Settings.Default.FlowPriceTick		//增加波动
                           * (double)this.dtInstruments.Rows.Find(instrument)["最小波动"] : 0;
                    tradeApi.OrderInsert(instrument, EnumOffsetFlagType.Open, dgv.Columns[e.ColumnIndex].Name == "买价" ? EnumDirectionType.Sell : EnumDirectionType.Buy,
                        dgv.Columns[e.ColumnIndex].Name == "买价" ? price - ticks : price + ticks,	//价格
                        lots == null ? 1 : int.Parse(lots));//手数
                }
            }
        }
        //行情栏:数据错误
        void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.OnError, (sender as DoubleBufferDGV).Name + ":" + e.RowIndex + "," + e.ColumnIndex + ":" + e.Exception.Message);
        }
        //委托页:双击撤单
        private void listViewOrder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = this.listViewOrder.SelectedItems[0];
            if (lvi.Group.Name != EnumOrderStatusType.AllTraded.ToString() && lvi.Group.Name != EnumOrderStatusType.Canceled.ToString())
            {
                this.progress(EnumProgessState.OrderAction, "撤单...");
                string[] ps = lvi.Name.Split(',');
                tradeApi.OrderAction(lvi.Text, int.Parse(ps[0]), int.Parse(ps[1]), ps[2]);
            }
        }
        //持仓页:单击选择
        private void listViewPosition_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = this.listViewPosition.SelectedItems[0];
            this.comboBoxInstrument.Text = lvi.Text;
            this.orderRangeChangeInstrument(lvi.Text, lvi.SubItems[1].Text == "多" ? EnumDirectionType.Sell : EnumDirectionType.Buy,
                lvi.Group.Name == "Today" ? EnumOffsetFlagType.CloseToday : EnumOffsetFlagType.Close, decimal.Parse(lvi.SubItems[3].Text));
        }
        //持仓页:双击:快速平仓(不支持市价单)
        private void listViewPosition_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = this.listViewPosition.SelectedItems[0];
            double ticks = 0;
            if (Properties.Settings.Default.FastCloseAddTick) //增加波动
                ticks = (int)Properties.Settings.Default.FastCloseTick * (double)this.dtInstruments.Rows.Find(lvi.Text)["最小波动"];

            this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QuickClose, "快速平仓...");
            //限价单
            if (lvi.SubItems[1].Text == "多")
                tradeApi.OrderInsert(lvi.Text, lvi.Group.Name == "Today" ? EnumOffsetFlagType.CloseToday : EnumOffsetFlagType.Close,
                    EnumDirectionType.Sell, (double)this.dtMarketData.Rows.Find(lvi.Text)["买价"] - ticks, int.Parse(lvi.SubItems[3].Text));
            else
                tradeApi.OrderInsert(lvi.Text, lvi.Group.Name == "Today" ? EnumOffsetFlagType.CloseToday : EnumOffsetFlagType.Close,
                    EnumDirectionType.Buy, (double)this.dtMarketData.Rows.Find(lvi.Text)["卖价"] + ticks, int.Parse(lvi.SubItems[3].Text));
        }
        //持仓页:快速平仓
        private void buttonQuickClose_Click(object sender, EventArgs e)
        {
            if (this.listViewPosition.SelectedItems.Count > 0)
                this.listViewPosition_MouseDoubleClick(null, null);
        }
        //持仓页:反手
        private void buttonCovert_Click(object sender, EventArgs e)
        {
            if (this.listViewPosition.SelectedItems.Count > 0)
            {
                this.lviCovert = this.listViewPosition.SelectedItems[0];	//赋值给反手
                this.buttonQuickClose_Click(null, null);					//快平
            }
        }
        //持仓页:锁仓
        private void buttonQuickLock_Click(object sender, EventArgs e)
        {
            if (this.listViewPosition.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.listViewPosition.SelectedItems[0];
                double ticks = 0;
                if (Properties.Settings.Default.FastCloseAddTick) //增加波动
                    ticks = (int)Properties.Settings.Default.FastCloseTick * (double)this.dtInstruments.Rows.Find(lvi.Text)["最小波动"];

                this.BeginInvoke(new Action<EnumProgessState, string>(progress), EnumProgessState.QuickLock, "快速锁仓...");
                //限价单
                if (lvi.SubItems[1].Text == "多")
                    tradeApi.OrderInsert(lvi.Text, EnumOffsetFlagType.Open, EnumDirectionType.Sell, (double)this.dtMarketData.Rows.Find(lvi.Text)["买价"] - ticks, int.Parse(lvi.SubItems[3].Text));
                else
                    tradeApi.OrderInsert(lvi.Text, EnumOffsetFlagType.Open, EnumDirectionType.Buy, (double)this.dtMarketData.Rows.Find(lvi.Text)["卖价"] + ticks, int.Parse(lvi.SubItems[3].Text));
            }
        }
        //预埋页:预埋
        private void btnParked_Click(object sender, EventArgs e)
        {
            //if (this.comboBoxOffset.SelectedIndex > 0) //埋撤单
            //    tradeApi.ReqParkedOrderAction(this.comboBoxInstrument.Text, this.comboBoxOffset.SelectedIndex == 0 ? EnumOffsetFlagType.Open : this.comboBoxOffset.SelectedIndex == 1 ? EnumOffsetFlagType.Close : EnumOffsetFlagType.CloseToday,
            //    this.comboBoxDirector.SelectedIndex == 0 ? EnumDirectionType.Buy : EnumDirectionType.Sell, (double)this.numericUpDownPrice.Value, (int)this.numericUpDownVolume.Value);
            this.progress(EnumProgessState.ParkedOrder, "查预埋报单...");
            tradeApi.ParkedOrderInsert(this.comboBoxInstrument.Text, this.comboBoxOffset.SelectedIndex == 0 ? EnumOffsetFlagType.Open : this.comboBoxOffset.SelectedIndex == 1 ? EnumOffsetFlagType.Close : EnumOffsetFlagType.CloseToday,
                this.comboBoxDirector.SelectedIndex == 0 ? EnumDirectionType.Buy : EnumDirectionType.Sell, (double)this.numericUpDownPrice.Value, (int)this.numericUpDownVolume.Value);
        }
        //预埋页:查预埋
        private void tabPageParked_Enter(object sender, EventArgs e)
        {
            this.progress(EnumProgessState.QryParkedOrder, "查预埋撤单...");
            //tradeApi.ReqQryParkedOrder();
            this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryParkedOrder, null));
        }
        //预埋页:删除预埋
        private void hfListViewParkedOrder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.progress(EnumProgessState.RemovePackedOrder, "删除预埋单...");
            tradeApi.ReqRemoveParkedOrder((sender as HFListView).SelectedItems[0].Name);
        }
        //预埋页:删除预埋撤单
        private void hfListViewParkedAction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.progress(EnumProgessState.RemovePackedOrderAction, "删除预埋撤单...");
            tradeApi.ReqRemoveParkedOrderAction((sender as HFListView).SelectedItems[0].Name);
        }
        #endregion
        #region//资金页
        private void tabPageAccount_Enter(object sender, EventArgs e)
        {
            if (!userControlTradeAccount1.IsActive)
            {
                this.buttonQryAccount_Click(null, null);
            }
        }
        //资金页
        private void buttonQryAccount_Click(object sender, EventArgs e)
        {
            this.progress(EnumProgessState.QryAccount, "查资金...");
            //tradeApi.QryTradingAccount();
            this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryTradingAccount, null));
        }

        //合约/自选合约页:设置合约显示
        private void setDataGridViewOfInstrument()
        {
            foreach (string _name in (new string[] { "", "Selected" }))
            {
                DoubleBufferDGV dgv = (DoubleBufferDGV)this.Controls.Find("dataGridViewInstruments" + _name, true).First();
                DataView dv = new DataView(this.dtInstruments);
                if (_name == "Selected")
                {
                    dgv.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvInstrument_CellMouseClick);
                    dv.RowFilter = "自选=true";
                }
                else
                    dv.RowFilter = "自选=false";
                dgv.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dgvInstrument_CellMouseDoubleClick);
                dgv.DataError += new DataGridViewDataErrorEventHandler(dgv_DataError);
                dgv.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(dgv_CellToolTipTextNeeded);
                dgv.ReadOnly = true;		//只读
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//列标题对齐
                dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;		//列标题不换行
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//单元格居中对齐
                dgv.DefaultCellStyle.NullValue = "-";		//空值显示
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;//.AllCells;//列宽调整方式
                dgv.ScrollBars = ScrollBars.Both;

                dgv.DataSource = dv;
                dgv.Columns["自选"].Visible = false;
                dgv.Columns["套利"].Visible = false;
                dgv.Columns["合约"].Frozen = true;
                //dgv.Columns["合约"].DefaultCellStyle.Format = "";
                //dgv.Columns["名称"].DefaultCellStyle.Format = "";
                //dgv.Columns["交易所"].DefaultCellStyle.Format = "";
                //dgv.Columns["合约数量"].DefaultCellStyle.Format = "";
                dgv.Columns["最小波动"].DefaultCellStyle.Format = "F2";
                dgv.Columns["保证金-多"].DefaultCellStyle.Format = "F2";
                dgv.Columns["保证金-空"].DefaultCellStyle.Format = "F2";
                //dgv.Columns["最大下单量-限"].DefaultCellStyle.Format = "";
                //dgv.Columns["最小下单量-限"].DefaultCellStyle.Format = "";
            }
        }
        //自选合约页:单击:选择合约
        void dgvInstrument_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                this.orderRangeChangeInstrument((string)(sender as DoubleBufferDGV).Rows[e.RowIndex].Cells["合约"].Value, EnumDirectionType.Buy, EnumOffsetFlagType.Open);
            }
        }
        //合约/自选合约页:双击:添加/删除
        #endregion
        void dgvInstrument_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataRow dr = this.dtInstruments.Rows.Find((sender as DoubleBufferDGV).Rows[e.RowIndex].Cells["合约"].Value);
                dr["自选"] = !(bool)dr["自选"];
                DataRow drData = this.dtMarketData.Rows.Find(dr["合约"]);	//此处引用上面的dr而非sender,因为sender已经有所改变
                drData["自选"] = !(bool)drData["自选"];
                if ((bool)dr["自选"])	//添加
                {
                    Properties.Settings.Default.Instruments.Add(dr["合约"].ToString());
                    QryOrder qry = this.listQry.FirstOrDefault(n => n.QryOrderType == EnumQryOrder.QryInstrumentCommissionRate && n.Params == dr["合约"].ToString());
                    if (qry != null)
                    {
                        this.listQry.Remove(qry);	//放到前面待查
                        this.listQry.Insert(0, qry);
                    }
                }
                else //删除
                    Properties.Settings.Default.Instruments.Remove(dr["合约"].ToString());
                //qryRate();	//查手续费/保证金
            }
        }

        #region //银期转帐页:选择银行
        private void listViewBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewBank.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.listViewBank.SelectedItems[0];
                this.textBoxBankID.Text = lvi.SubItems[0].Text;
                this.textBoxBankName.Text = lvi.SubItems[1].Text;
                //tradeApi.ReqQryTransferBank(lvi.SubItems[0].Text,lvi.SubItems[2].Text);
            }
        }
        //银期转帐页:转帐类别
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxBankPwd.Enabled = false;
            if ((sender as ComboBox).SelectedIndex == 1)
                this.textBoxBankPwd.Enabled = true;	//银行密码
            else
                this.textBoxBankPwd.Text = "";
        }
        //银期转帐页:转帐
        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            switch (this.comboBoxTransferType.SelectedIndex)
            {
                case 0:		//期-银
                    if (this.textBoxAccountPwd.Enabled && string.IsNullOrWhiteSpace(this.textBoxAccountPwd.Text.Trim()))
                    {
                        MessageBox.Show("请输入密码!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(this.textBoxTradeAmount.Text.Trim()))
                    {
                        MessageBox.Show("请输入金额!");
                        return;
                    }
                    CThostFtdcReqTransferField fieldF2B = new CThostFtdcReqTransferField();
                    fieldF2B.TradeCode = EnumFBTTradeCodeEnumType.BrokerLaunchBrokerToBank_202002.ToString().Split('_')[1];
                    fieldF2B.BankID = this.textBoxBankID.Text;
                    fieldF2B.BankBranchID = "0000";
                    fieldF2B.BrokerID = tradeApi.BrokerID;
                    fieldF2B.BankPassWord = this.textBoxBankPwd.Text;
                    fieldF2B.AccountID = tradeApi.InvestorID;
                    fieldF2B.Password = this.textBoxAccountPwd.Text;
                    fieldF2B.BankPwdFlag = EnumPwdFlagType.BlankCheck;
                    fieldF2B.CurrencyID = "RMB";
                    fieldF2B.TradeAmount = double.Parse(this.textBoxTradeAmount.Text);

                    tradeApi.ReqFromFutureToBankByFuture(fieldF2B);
                    break;
                case 1:		//银-期
                    if (this.textBoxAccountPwd.Enabled && string.IsNullOrWhiteSpace(this.textBoxAccountPwd.Text.Trim()))
                    {
                        MessageBox.Show("请输入密码!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(this.textBoxTradeAmount.Text.Trim()))
                    {
                        MessageBox.Show("请输入金额!");
                        return;
                    }
                    CThostFtdcReqTransferField fieldB2F = new CThostFtdcReqTransferField();
                    fieldB2F.TradeCode = EnumFBTTradeCodeEnumType.BrokerLaunchBankToBroker_202001.ToString().Split('_')[1];
                    fieldB2F.BankID = this.textBoxBankID.Text;
                    fieldB2F.BankBranchID = "0000";
                    fieldB2F.BrokerID = tradeApi.BrokerID;
                    fieldB2F.BankPassWord = this.textBoxBankPwd.Text;
                    fieldB2F.AccountID = tradeApi.InvestorID;
                    fieldB2F.Password = this.textBoxAccountPwd.Text;
                    fieldB2F.BankPwdFlag = EnumPwdFlagType.BlankCheck;
                    fieldB2F.CurrencyID = "RMB";
                    fieldB2F.TradeAmount = double.Parse(this.textBoxTradeAmount.Text);

                    tradeApi.ReqFromBankToFutureByFuture(fieldB2F);
                    break;
                case 2:		//查余额
                    CThostFtdcReqQueryAccountField fieldAccount = new CThostFtdcReqQueryAccountField();
                    fieldAccount.AccountID = tradeApi.InvestorID;
                    //fieldAccount.BankAccount
                    fieldAccount.BankBranchID = "0000";
                    fieldAccount.BankID = this.textBoxBankID.Text;
                    fieldAccount.BankPassWord = this.textBoxBankPwd.Text;
                    fieldAccount.BankPwdFlag = EnumPwdFlagType.BlankCheck;
                    fieldAccount.BrokerID = tradeApi.BrokerID;
                    fieldAccount.CurrencyID = "RMB";
                    fieldAccount.Password = this.textBoxAccountPwd.Text;
                    //tradeApi.ReqQueryBankAccountMoneyByFuture(fieldAccount);
                    this.listQry.Insert(0, new QryOrder(EnumQryOrder.QueryBankAccountMoneyByFuture, _field: fieldAccount));
                    break;
            }
        }
        //银期转帐页:查转帐流水
        private void buttonQryTransferSeries_Click(object sender, EventArgs e)
        {
            this.listViewSeries.Items.Clear();
            this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryTransferSerial, this.textBoxBankID.Text));
        }

        //系统页:修改密码
        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (this.textBoxNewPassWord.Text != this.textBoxNewPwdConfirm.Text)
                MessageBox.Show("核对输入的密码.");
            else
            {
                if (this.radioButtonUser.Checked)
                {
                    this.progress(EnumProgessState.UpdateUserPassword, "修改用户密码...");
                    tradeApi.UserPasswordupdate(tradeApi.InvestorID, this.textBoxOldPassword.Text, this.textBoxNewPassWord.Text);
                }
                else
                {
                    this.progress(EnumProgessState.UpdateAccountPassword, "修改资金密码...");
                    tradeApi.TradingAccountPasswordUpdate(tradeApi.InvestorID, this.textBoxOldPassword.Text, this.textBoxNewPassWord.Text);
                }
            }
        }
        //系统页:密码重置
        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.textBoxNewPassWord.Text = "";
            this.textBoxOldPassword.Text = "";
            this.textBoxNewPwdConfirm.Text = "";
        }
        //系统页:显示日志
        private void buttonShowLog_Click(object sender, EventArgs e)
        {
            for (int i = Properties.Settings.Default.Log.Count - 1; i >= 0 && i > Properties.Settings.Default.Log.Count - 300; i--)
            {
                string str = Properties.Settings.Default.Log[i];
                if (this.hfListViewLog.FindItemWithText(str.Split('|')[0]) == null)
                {
                    ListViewItem lvi = new ListViewItem(str.Split('|')[0]);
                    lvi.Name = lvi.Text;
                    lvi.SubItems.Add(str.Split(new char[] { '|' }, 2)[1]);
                    this.hfListViewLog.Items.Add(lvi);
                }
                this.hfListViewLog.Sort();
            }
        }
        //系统页:清除日志
        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Log.Clear();
            this.hfListViewLog.Clear();
        }
        //保存日志
        private void buttonSaveLog_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string str = string.Empty;
                foreach (ListViewItem lvi in this.hfListViewLog.Items)
                {
                    for (int i = 0; i < lvi.SubItems.Count; i++)
                        str += lvi.SubItems[i].Text + ",";
                    str = str.Remove(str.Length - 1) + "\r\n";	//换行
                }
                using (StreamWriter sw = new StreamWriter(this.saveFileDialog1.FileName, false))
                    sw.Write(str);
            }
        }

        //设置页:重置属性
        private void buttonResetSetting_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }
        //设置页:是否显示提示
        private void checkBoxShowTootip_CheckedChanged(object sender, EventArgs e)
        {
            this.toolTip1.Active = (sender as CheckBox).Checked;
        }
        //设置页:自定义手数完成
        private void dataGridViewCustomVolume_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int idx = Properties.Settings.Default.CustomVolume.IndexOf((string)(sender as DoubleBufferDGV)["合约", e.RowIndex].Value);//合约索引
            Properties.Settings.Default.CustomVolume[idx + 1] = (sender as DoubleBufferDGV)["手数", e.RowIndex].Value.ToString() ?? "1";		//修改手数
        }
        //恢复设置
        private void buttonResetSetting_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("某些设置需要重启软件,重置才能生效!\n确认要清除选择的设置吗?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (this.checkBoxResetAll.Checked)
                    Properties.Settings.Default.Reset();
                else
                {
                    if (this.checkBoxClearCosumVolume.Checked)
                        Properties.Settings.Default.CustomVolume.Clear();
                    if (this.checkBoxClearLog.Checked)
                        Properties.Settings.Default.Log.Clear();
                    if (this.checkBoxClearSelected.Checked)
                        Properties.Settings.Default.Instruments.Clear();
                    if (this.checkBoxClearServers.Checked)
                        Properties.Settings.Default.Servers.Clear();
                }
                this.checkBoxClearCosumVolume.Checked = false;
                this.checkBoxClearLog.Checked = false;
                this.checkBoxClearSelected.Checked = false;
                this.checkBoxClearServers.Checked = false;
                this.checkBoxResetAll.Checked = false;
            }
        }

        //下单区:合约改变
        private void comboBoxInstrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = this.dtInstruments.Rows.Find(this.comboBoxInstrument.Text);
            if (dr != null)
            {
                if ((bool)dr["自选"] == false)
                {
                    dr["自选"] = true;	//
                    this.dtMarketData.Rows.Find(dr["合约"])["自选"] = true;	//行情自选
                    Properties.Settings.Default.Instruments.Add(dr["合约"].ToString());
                    QryOrder qry = this.listQry.FirstOrDefault(n => n.QryOrderType == EnumQryOrder.QryInstrumentCommissionRate && n.Params == dr["合约"].ToString());
                    if (qry != null)
                    {
                        this.listQry.Remove(qry);	//放到前面待查
                        this.listQry.Insert(0, qry);
                    }
                }
                this.orderRangeChangeInstrument(dr["合约"].ToString(), EnumDirectionType.Buy, EnumOffsetFlagType.Open);
            }
        }
        //下单区:价格方式
        private void buttonPrice_Click(object sender, EventArgs e)
        {
            if (this.buttonPrice.Text == "跟盘价")
            {
                this.freshOrderPrice = false;
                this.numericUpDownPrice.Enabled = true;
                this.buttonPrice.Text = "指定价";
                this.buttonPrice.BackColor = Color.Transparent;
            }
            else
            {
                this.buttonPrice.Text = "跟盘价";
                this.buttonPrice.BackColor = Color.FromArgb(255, 128, 128);
                this.numericUpDownPrice.Enabled = false;
                this.freshOrderPrice = true;
            }
        }
        //下单区:下单
        private void buttonOrder_Click(object sender, EventArgs e)
        {
            this.progress(EnumProgessState.OrderInsert, "下单...");
            if (this.freshOrderPrice)	//跟盘价
            {
                double ticks = Properties.Settings.Default.FlowPriceAddTick ? (int)Properties.Settings.Default.FlowPriceTick		//增加波动
                    * (double)this.dtInstruments.Rows.Find(this.comboBoxInstrument.Text)["最小波动"] : 0;
                tradeApi.OrderInsert(this.comboBoxInstrument.Text, this.comboBoxOffset.SelectedIndex == 0 ? EnumOffsetFlagType.Open  //开
                    : this.comboBoxOffset.SelectedIndex == 1 ? EnumOffsetFlagType.Close : EnumOffsetFlagType.CloseToday
                    , this.comboBoxDirector.SelectedIndex == 0 ? EnumDirectionType.Buy : EnumDirectionType.Sell
                    , this.comboBoxDirector.SelectedIndex == 0 ? (double)this.numericUpDownPrice.Value + ticks : (double)this.numericUpDownPrice.Value - ticks
                    , (int)this.numericUpDownVolume.Value);
            }
            else //指定价
                tradeApi.OrderInsert(this.comboBoxInstrument.Text, this.comboBoxOffset.SelectedIndex == 0 ? EnumOffsetFlagType.Open  //开
                    : this.comboBoxOffset.SelectedIndex == 1 ? EnumOffsetFlagType.Close : EnumOffsetFlagType.CloseToday
                    , this.comboBoxDirector.SelectedIndex == 0 ? EnumDirectionType.Buy : EnumDirectionType.Sell
                    , this.comboBoxDirector.SelectedIndex == 0 ? (double)this.numericUpDownPrice.Value : (double)this.numericUpDownPrice.Value
                    , (int)this.numericUpDownVolume.Value);
            if (!Properties.Settings.Default.KeepLots)	//不保留手数
                this.numericUpDownVolume.Value = 0;
            if (!Properties.Settings.Default.KeepInstrument) //不保留合约
                this.comboBoxInstrument.Text = "";
        }
        //下单区:清除下单页面填写的内容
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.comboBoxInstrument.Text = "";
            this.comboBoxDirector.SelectedIndex = 0;
            this.comboBoxOffset.SelectedIndex = 0;
            this.numericUpDownPrice.Minimum = 0;
            this.numericUpDownPrice.Maximum = 100;
            this.numericUpDownPrice.Value = 0;
            this.numericUpDownVolume.Value = 0;
        }
        #endregion

        //状态栏:刷新交易所时间
        void freshExchangeTime()
        {
            while (true)
            {
                this.BeginInvoke(actionFreshExchengeTime);// new Action(freshTime));
                Thread.Sleep(1000);
            }
        }
        void freshTime()
        {
            this.labelSHFE.Text = (DateTime.Now - tsSHFE).ToString("HH:mm:ss");
            this.labelCZCE.Text = (DateTime.Now - tsCZCE).ToString("HH:mm:ss");
            this.labelDCE.Text = (DateTime.Now - tsDCE).ToString("HH:mm:ss");
            this.labelFFEX.Text = (DateTime.Now - tsCFFEX).ToString("HH:mm:ss");
        }

        #region//历史成交:
        private void buttonQryPosition_Click(object sender, EventArgs e)
        {
            this.isQryHistoryTrade = true;	//查历史
            this.hfListViewTrade.Items.Clear();
            progress(EnumProgessState.QryTrade, "查历史成交...");
            //tradeApi.QryTrade(this.dateTimePickerStart.Value, this.dateTimePickerEnd.Value);
            this.listQry.Insert(0, new QryOrder(EnumQryOrder.QryHistoryTrade));//查历史
        }
        //保存历史成交
        private void buttonSaveTrade_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string str = string.Empty;
                foreach (ListViewItem lvi in this.hfListViewTrade.Items)
                {
                    for (int i = 0; i < lvi.SubItems.Count; i++)
                        str += lvi.SubItems[i].Text + ",";
                    str = str.Remove(str.Length - 1) + "\r\n";	//换行
                }
                using (StreamWriter sw = new StreamWriter(this.saveFileDialog1.FileName, false))
                    sw.Write(str);
            }
        }

        //查结算
        private void buttonQrySettleInfo_Click(object sender, EventArgs e)
        {
            progress(EnumProgessState.QrySettleInfo, "查结算信息...");
            //tradeApi.QrySettlementInfo(this.dateTimePickerQrySettleInfo.Value);
            this.listQry.Insert(0, new QryOrder(EnumQryOrder.QrySettlementInfo, this.dateTimePickerQrySettleInfo.Value.ToString("yyyyMMdd")));
        }
        //保存结算信息
        private void buttonSaveInfo_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.richTextBoxSettleInfo.SaveFile(this.saveFileDialog1.FileName, RichTextBoxStreamType.TextTextOleObjs);
            }
        }

        //价格改变:调整格式
        private void numericUpDownPrice_ValueChanged(object sender, EventArgs e)
        {
            DataRow dr = this.dtInstruments.Rows.Find(this.comboBoxInstrument.Text);
            if (dr != null)
            {
                this.numericUpDownPrice.Increment = (decimal)(double)dr["最小波动"];
                string place = this.numericUpDownPrice.Increment.ToString();
                this.numericUpDownPrice.DecimalPlaces = place.IndexOf('.') == -1 ? 0 : place.Length - place.IndexOf('.') - 1;
            }
        }
        //窗口大小改变:下单区高度固定
        private void FormTrade_SizeChanged(object sender, EventArgs e)
        {
            this.splitContainer1.Height = 275;		//下边栏高度
            if (this.panelTop.Visible)
                Properties.Settings.Default.TopHeigh = this.panelTop.Height;
            else //行情栏隐藏时高度不可移动
                this.Height = 600 - 255;
            Properties.Settings.Default.FormSize = this.Size;
        }
        //位置改动:防止显示行情栏时上边出屏幕
        private void FormTrade_LocationChanged(object sender, EventArgs e)
        {
            if (this.Top < 0)
                this.Top = 0;
        }
        //导出保证金
        private void buttonExportRate_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "XML文件|*.xml";
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.dtInstruments.WriteXml(this.saveFileDialog1.FileName, XmlWriteMode.WriteSchema);
            }
        }
        #endregion
        private void draw_k(Bar bar, float lt, float rt, float h, float o, float c, float l)
        {
            Graphics graphics = tabPage1.CreateGraphics();//声明一个Graphics对象  
            Pen kpen;
            if (bar.Close > bar.Open)
            {
                kpen = new Pen(Color.Red, 1);
                graphics.DrawLine(kpen, new PointF(lt + rt / 2, h), new PointF(lt + rt / 2, c));
                graphics.DrawLine(kpen, new PointF(lt + rt / 2, o), new PointF(lt + rt / 2, l));

                graphics.DrawRectangle(kpen, lt, c, rt, o - c);
            }
            if (bar.Close < bar.Open)
            {
                kpen = new Pen(Color.Green, 1);
                graphics.DrawLine(kpen, new PointF(lt + rt / 2, h), new PointF(lt + rt / 2, l));
                RectangleF rect = new RectangleF(lt, o, rt, c - o);
                Brush b = new SolidBrush(Color.Green);
                graphics.FillRectangle(b, rect);

            }
            if (bar.Close == bar.Open)
            {
                kpen = new Pen(Color.Aqua, 1);
                graphics.DrawLine(kpen, new PointF(lt + rt / 2, h), new PointF(lt + rt / 2, l));
                graphics.DrawLine(kpen, new PointF(lt, o), new PointF(lt + rt, o));
            }

        }
        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            drk("ru1609");
        }
        void drk(string id)
        {
            float x, y, h, w;
            h = tabPage1.Height - 20;
            w = tabPage1.Width - 20;

            Graphics graphics = tabPage1.CreateGraphics();//声明一个Graphics对象  
            Pen myPen = new Pen(Color.Blue, 1);//实例化Pen类
            Rectangle rect = new Rectangle(10, 10, tabPage1.Width - 20, tabPage1.Height - 20);

            Brush b = new SolidBrush(Color.Black);
            graphics.FillRectangle(b, rect);
            graphics.DrawRectangle(myPen, 10, 10, w, h);
            Brush fontBrush = SystemBrushes.ControlText;
            PointF drawPoint = new PointF(150.0F, 150.0F);
            Font drawFont = new Font("Arial", 16);
            double hi = 0;

            double lo = 0;
            /*
            for (int si = 0; si < ol.Count; si++)
            {
                if (id == ol[si].id)
                {
                    int counts = ol[si].bs.Count;
                    if (counts > 0)
                    {
                        #region  //求最高最低
                        for (int j = 0; j < counts; j++)
                        {
                            if (j == 0)
                            {
                                hi = ol[si].bs[counts - 1].High;
                                lo = ol[si].bs[counts - 1].Low;
                            }
                            if (j < 600 && j > 0)
                            {
                                if (ol[si].bs[counts - 1 - j].High > hi)
                                    hi = ol[si].bs[counts - 1 - j].High;
                                if (ol[si].bs[counts - 1 - j].Low < lo)
                                    lo = ol[si].bs[counts - 1 - j].Low;
                            }

                        }
                        #endregion
                        po.Clear();
                        //      生成(ol[si].bs, id);
                        float th = (float)(hi - lo);
                        float rh, rl, ro, rc;
                        double tw = w / 602;
                        生成(ol[si].bs, id);

                        #region //计算K线位置
                        for (int i = 0; i < counts; i++)
                        {
                            if (i < 600)
                            {
                                rh = (float)(ol[si].bs[counts - i - 1].High - (float)lo);
                                rh = h + 10 - rh * h / th;
                                rc = ((float)ol[si].bs[counts - i - 1].Close - (float)lo);
                                rc = h + 10 - rc * h / th;
                                rl = ((float)ol[si].bs[counts - i - 1].Low - (float)lo);
                                rl = h + 10 - rl * h / th;
                                ro = ((float)ol[si].bs[counts - i - 1].Open - (float)lo);
                                ro = h + 10 - ro * h / th;
                                float l = w - i * (float)tw;
                                draw_k(ol[si].bs[counts - i - 1], l, (float)tw, rh, ro, rc, rl);
                                for (int t = 0; t < ol[si].bl.Count; t++)
                                {
                                    if (ol[si].bl[ol[si].bl.Count - 1 - t].ba.BeginTime == ol[si].bs[counts - i - 1].BeginTime)
                                    {
                                        if (ol[si].bl[ol[si].bl.Count - 1 - t].zd == 结构.顶)
                                            po.Add(new PointF(l, rh));
                                        if (ol[si].bl[ol[si].bl.Count - 1 - t].zd == 结构.底)
                                            po.Add(new PointF(l, rl));
                                    }
                                }
                            }
                        }
                        #endregion
                        drawlis(graphics);
                    }
                }
            }*/
        }
        List<PointF> po = new List<PointF>();
        void drawlis(Graphics graphics)
        {
            Pen kpen;
            kpen = new Pen(Color.Red, 1);
            if (po.Count > 2)
            {
                for (int i = 1; i < po.Count; i++)
                {
                    graphics.DrawLine(kpen, po[po.Count - i], po[po.Count - i - 1]);
                }
            }
        }
        void drtick(string id)
        {
         

        }
        private void draw_tick(int lt, int rt)
        {
         
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ilist.Count; i++)
            {
                for (int l = 0; l < ilist[i].bl2.Count; l++)
                {
                    log_sw.WriteLine(ilist[i].bl2[l].ToString());

                }
                for (int l = 0; l < ilist[i].bl3.Count; l++)
                {
                    log_sw.WriteLine(ilist[i].bl3[l].ToString());

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
