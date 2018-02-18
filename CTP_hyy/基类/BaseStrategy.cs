using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ats.Core;


namespace CTP_交易.基类
{
    /// <summary>
    /// 期货日内交易基类
    /// 支持隔夜和夜盘
    /// </summary>
    public class BaseStrategy : Strategy
    {

        /// <summary>
        /// 这里不要用一样的，防止发生踩踏
        /// </summary>
        [Parameter(Display = "收盘秒", Description = "", Category = "日内")]
        public double 收盘秒 = 300;
         
        [Parameter(Display = "PCJP", Description = "平仓加跳", Category = "算法交易")]
        public int PCJP = 1;

        [Parameter(Display = "KCJP", Description = "开仓加跳", Category = "算法交易")]
        public int KCJP = 1;


        [Parameter(Display = "多空模式", Description = "0多空都做 1只做多 2只做空", Category = "算法交易")]
        public int 多空模式 = 0;

        /// <summary>
        /// 是否是夜盘
        /// </summary>
        [Parameter(Display = "IsNight", Description = "True夜盘策略 False白盘策略", Category = "系统")]
        public bool IsNight = false;

        #region 常规系统变量

        public double 最大亏损 = 0;

        /// <summary>
        /// 最大手数
        /// </summary>
        public int MaxQty = 1;


        /// <summary>
        /// 开仓单持续N秒不成交追单
        /// </summary>
        double KCBC = 0.6;

        /// <summary>
        /// 平仓单持续N秒不成交追单
        /// </summary>
        double PCBC = 0.6;

         
        /// <summary>
        /// 开盘的时间（单位分钟）
        /// </summary>
        public double 已开盘分钟 = 0;


        /// <summary>
        /// 是否日内
        /// True 日内策略
        /// False 隔夜策略
        /// </summary>
        public bool IfInDay = true;

        /// <summary>
        /// 开仓是否追单
        /// </summary>
        public bool KCZhui = true;

        /// <summary>
        /// 是否触发止损
        /// </summary>
        private bool _ifTrigStopLoss = false;

        /// <summary>
        /// 是否触发止损
        /// </summary>
        public bool IfTrigStopLoss
        {
            get
            {
                return _ifTrigStopLoss;
            }
        }

        private Exchange _MyExchange;

        /// <summary>
        /// 交易所
        /// </summary>
        public Exchange MyExchange
        {
            get
            {
                if ( _MyExchange == null)
                {
                    if (DefaultFuture != null)
                    {
                        _MyExchange = GetExchange(DefaultFuture.ExchangeID);
                    }
                  
                }
                return _MyExchange;
            }
        }


        #endregion


        #region 变量

        /// <summary>
        /// 策略版本
        /// </summary>
        public string VersionInfo = "";

        /// <summary>
        /// 期货账户
        /// </summary>
        public string FutureAccountID = "";

        /// <summary>
        /// 逻辑账户
        /// </summary>
        public string LogAccount = "";


        #region 行情瞬间信息
        
        /// <summary>
        /// 上次开仓时间
        /// </summary>
        public DateTime T0;

        /// <summary>
        /// 最新价
        /// </summary>
        public double 最新价 = 0;

        public double 中间价 = 0;

        public double LastPrice = 0;

        public Tick 当前Tick;

        public Tick 前一Tick;


        public double 买1价 = 0;

        public double 卖1价 = 0;

        public int 买1量 = 0;

        public int 卖1量 = 0;

        public double 总成交量 = 0;

        public double 最高价 = 0;

        public double 最低价 = 0;

        public double 持仓量 = 0;

        public double 当前Tick成交量 = 0;

        /// <summary>
        /// 状态机
        /// </summary>
        public StateMachine Machine = new StateMachine();

        /// <summary>
        /// 开盘时间
        /// 按照自然日时间体系
        /// </summary>
        public DateTime tOpen = default(DateTime);

        /// <summary>
        /// 收盘时间
        /// 按照自然日时间体系
        /// </summary>
        public DateTime tClose = default(DateTime);

        /// <summary>
        /// 清仓时间
        /// </summary>
        public DateTime tFinish = default(DateTime);

        /// <summary>
        /// 停止开仓时间
        /// </summary>
        public DateTime tStop = default(DateTime);

        /// <summary>
        /// 价格乘数
        /// </summary>
        public double VolumeMultiple = 300;

        /// <summary>
        /// 跳
        /// </summary>
        public double PriceTick = 0.2;

        /// <summary>
        /// 最新Tick的时间戳
        /// </summary>
        public DateTime TickNow;

        #endregion

        /// <summary>
        /// 上午结束时间
        /// </summary>
        public DateTime AMEnd
        {
            get
            {
                return new DateTime(Year, Month, Day, 11, 30, 0);
            }
        }

        /// <summary>
        /// 下午开盘时间
        /// </summary>
        public DateTime PMBegin
        {
            get
            {
                return new DateTime(Year, Month, Day, 13, 0, 0);
            }
        }

        /// <summary>
        /// 中午停止交易的时间
        /// </summary>
        public DateTime TPauseAM
        {
            get
            {
                return new DateTime(Year, Month, Day, 11, 29, 50);
            }
        }


        int MySignal = 0;

        /// <summary>
        /// 策略信号
        /// 1做多
        /// 0不持仓
        /// -1空仓
        /// </summary>
        public int StrategySignal
        {
            get
            {
                return MySignal;
            }
            set
            {
                MySignal = value;
            }
        }


        /// <summary>
        /// 状态机的文件路径
        /// </summary>
        public string MachineFileName = "C:\\machine.txt";

        #endregion
         

        #region 策略事件

        /// <summary>
        /// 开仓间隔
        /// </summary>
        double KCJG = 30;

        void 核心交易()
        {
            #region 核心交易处理
            //注意：只要目标Flag!=实际Flag
            //这个时候要看Flag                   
            if (Machine.Flag != Machine.TargetFlag && !Machine.IsDoing)
            {
                //如果是开仓被拒绝，那要等30秒再操作
                double Dt = (TickNow - T0).TotalSeconds;
                if (TickNow >= AMEnd && T0 <= PMBegin)
                {
                    Dt = Dt - (PMBegin - AMEnd).TotalSeconds;
                }
                if (!Machine.IsOpenRefuse || (Machine.IsOpenRefuse && Dt > KCJG))
                {
                    Machine.OldFlag = Machine.Flag;//先把Flag记住 
                    Machine.IsOpenRefuse = false;//过了30秒，就不记忆了
                    Machine.IsDoing = true;//正在操作


                    #region

                    string info = "";
                    //绝对工作量
                    int AbsWork = Abs(Machine.TargetFlag - Machine.Flag);
                    if (Machine.TargetFlag == 0)
                    {
                        #region 把Old的全部平掉
                        if (Machine.Flag > 0)
                        {
                            info += "目标仓单=0,平全部初始多单,";
                            Machine.KN = 0;
                            Machine.KN_Trd = 0;
                            Machine.PN = AbsWork;
                            Machine.PN_Trd = 0;
                            显示当前状态("触发平多单");
                            SafeOrder(PCJP,
                                DefaultFutureCode,
                                AbsWork,
                                EnumBuySell.卖出,
                                EnumOpenClose.平今仓);
                        }
                        else if (Machine.Flag < 0)
                        {
                            info += "目标理论仓单=0,平全部初始空单,";

                            Machine.KN = 0;
                            Machine.KN_Trd = 0;
                            Machine.PN = AbsWork;
                            Machine.PN_Trd = 0;
                            显示当前状态("触发平空单");
                            SafeOrder(PCJP,
                                DefaultFutureCode,
                                AbsWork,
                                EnumBuySell.买入,
                                EnumOpenClose.平今仓);

                        }
                        #endregion
                    }
                    else
                    {
                        #region 目标位有持仓
                        if (Machine.Flag == 0)
                        {
                            #region 如果当前不持仓，那么直接开
                            if (Machine.TargetFlag > 0)
                            {
                                info += "初始理论仓单=0,直接开目标多单,";
                                Machine.KN = AbsWork;
                                Machine.KN_Trd = 0;
                                Machine.PN = 0;
                                Machine.PN_Trd = 0;
                                显示当前状态("触发开仓做多");
                                SafeOrder(KCJP,
                                    DefaultFutureCode,
                                    AbsWork,
                                           EnumBuySell.买入,
                                           EnumOpenClose.开仓);
                            }
                            else if (Machine.TargetFlag < 0)
                            {
                                info += "初始理论仓单=0,直接开目标空单,";
                                Machine.KN = AbsWork;
                                Machine.KN_Trd = 0;
                                Machine.PN = 0;
                                Machine.PN_Trd = 0;
                                显示当前状态("触发开仓做空");
                                SafeOrder(KCJP, DefaultFutureCode, AbsWork,
                                           EnumBuySell.卖出, EnumOpenClose.开仓);
                            }
                            #endregion
                        }
                        else
                        {
                            #region New理论仓单 Old理论仓单 都不为0
                            if (Machine.TargetFlag * Machine.Flag > 0)
                            {
                                #region 同向
                                if (Abs(Machine.TargetFlag) > Abs(Machine.Flag))
                                {
                                    #region 同向增仓   新开仓
                                    if (Machine.TargetFlag > 0)
                                    {
                                        info += "同向,目标理论仓单>初始理论仓单,同向增仓做多,";
                                        //目标开仓位在原有基础上增加!
                                        Machine.KN = Machine.KN + AbsWork;
                                        Machine.KN_Trd = Machine.KN_Trd + 0;
                                        Machine.PN = 0;
                                        Machine.PN_Trd = 0;
                                        显示当前状态("触发增仓做多");
                                        SafeOrder(KCJP,
                                            DefaultFutureCode,
                                            AbsWork,
                                            EnumBuySell.买入,
                                            EnumOpenClose.开仓);
                                    }
                                    else if (Machine.TargetFlag < 0)
                                    {
                                        info += "同向,目标理论仓单>初始理论仓单,同向增仓做空,";
                                        //目标开仓位在原有基础上增加!
                                        Machine.KN = Machine.KN + AbsWork;
                                        Machine.KN_Trd = Machine.KN_Trd + 0;
                                        Machine.PN = 0;
                                        Machine.PN_Trd = 0;
                                        显示当前状态("触发增仓做空");
                                        SafeOrder(KCJP, DefaultFutureCode, AbsWork,
                                                   EnumBuySell.卖出, EnumOpenClose.开仓);
                                    }
                                    #endregion
                                }
                                else if (Abs(Machine.TargetFlag) < Abs(Machine.Flag))
                                {
                                    #region 同向减仓 （平掉部分单子）
                                    if (Machine.TargetFlag > 0)
                                    {
                                        info += "反向,目标理论仓单<初始理论仓单,减仓做空,";
                                        //这里注意把KN降下来
                                        Machine.KN = Machine.KN - AbsWork;
                                        Machine.KN_Trd = Machine.KN_Trd - AbsWork;
                                        Machine.PN = AbsWork;
                                        Machine.PN_Trd = 0;
                                        显示当前状态("触发减仓做空");
                                        SafeOrder(PCJP, DefaultFutureCode,
                                            AbsWork,
                                            EnumBuySell.卖出,
                                            EnumOpenClose.平今仓);
                                    }
                                    else if (Machine.TargetFlag < 0)
                                    {
                                        info += "反向,目标理论仓单<初始理论仓单,减仓做多,";
                                        //这里注意把KN降下来
                                        Machine.KN = Machine.KN - AbsWork;
                                        Machine.KN_Trd = Machine.KN_Trd - AbsWork;
                                        Machine.PN = AbsWork;
                                        Machine.PN_Trd = 0;
                                        显示当前状态("触发减仓做多");
                                        SafeOrder(PCJP,
                                            DefaultFutureCode,
                                            AbsWork,
                                            EnumBuySell.买入,
                                            EnumOpenClose.平今仓);
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                #region 方向，有一个先平后开的问题，这里要非常精细！！
                                if (Machine.Flag > 0)
                                {
                                    //先打平仓单，只有等平仓单全部成交了，才开仓！！
                                    info += "反向,多单全平,开目标空单,";
                                    Machine.KN = Abs(Machine.TargetFlag);
                                    Machine.KN_Trd = 0;
                                    Machine.PN = Abs(Machine.Flag);
                                    Machine.PN_Trd = 0;
                                    显示当前状态("触发先平后开做空");
                                    //把多单全平了
                                    //然后开目标空单                                       
                                    SafeOrder(PCJP,
                                        DefaultFutureCode,
                                        Machine.PN,
                                        EnumBuySell.卖出,
                                        EnumOpenClose.平今仓);
                                }
                                else if (Machine.Flag < 0)
                                {
                                    //先打平仓单，只有等平仓单全部成交了，才开仓！！
                                    info += "反向,空单全平,开目标多单,";
                                    Machine.KN = Abs(Machine.TargetFlag);
                                    Machine.KN_Trd = 0;
                                    Machine.PN = Abs(Machine.Flag);
                                    Machine.PN_Trd = 0;
                                    显示当前状态("触发先平后开做多");
                                    SafeOrder(PCJP,
                                        DefaultFutureCode,
                                        Machine.PN,
                                        EnumBuySell.买入,
                                        EnumOpenClose.平今仓);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    Machine.Flag = Machine.TargetFlag;//最终标志位强制设置成目标标志位!!
                    Print(info);
                    #endregion
                }

                //状态变化写盘
                SaveMachineFile(MachineFileName);

            }
            #endregion

        }

        public int 累积开仓手数 = 0;


        /// <summary>
        /// 成交回报
        /// </summary>
        /// <param name="trade"></param>
        public override void OnTrade(Trade trade)
        {
            #region

            string ordID = trade.OrderSysID;

            //肯定是先平后开
            if (trade.OpenOrClose == EnumOpenClose.开仓)
            {
                累积开仓手数 += trade.Volume;

                Machine.KN_Trd += trade.Volume;
                if (Machine.KN_Trd >= Machine.KN)
                {
                    if (Machine.PN_Trd >= Machine.PN)
                    {
                        Machine.IsDoing = false;
                        显示当前状态("开仓完成,逻辑结束");
                    }
                    else
                    {
                        显示当前状态("开仓完成,但还有平仓任务没完成");
                    }
                }
            }
            else
            {
                #region
                Machine.PN_Trd += trade.Volume;

                if (Machine.PN_Trd >= Machine.PN)
                {
                    显示当前状态("平仓动作完成");
                    if (Machine.KN == 0)
                    {
                        Print("没有开仓任务，逻辑结束");
                        Machine.IsDoing = false;
                    }
                    else
                    {
                        if (Machine.KN_Trd >= Machine.KN)
                        {
                            Print("开仓任务全部完成,KN_Trd=" + Machine.KN_Trd + ",KN=" + Machine.KN + "，逻辑结束");
                            Machine.IsDoing = false;
                        }
                        else
                        {
                            //把开仓的任务做出来
                            Print("KN=" + Machine.KN + ",KN_Trd=" + Machine.KN_Trd + "需要执行开仓任务");
                            int K_Vol = Machine.KN - Machine.KN_Trd;
                            Print("实际开仓量=" + K_Vol);
                            SafeOrder(KCJP, trade.InstrumentID, K_Vol, trade.Direction, EnumOpenClose.开仓);
                        }
                    }
                }
                #endregion
            }
            string OrdGUID = trade.OrderGUID;
            Print("###成交回报" + trade.ToString());
            #endregion

            #region 记录成交
            TRADES.Add(trade);
            HoldTRADES.Add(trade);

            保存成交();
            重新整理盈亏公式();

            #endregion
        }


        /// <summary>
        /// 委托拒绝
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderRejected(Order order)
        {
            Print("委托被拒绝!!!理由:" + order.OrderRejectReason.ToString());
            Print(order.ToString());

            //如果开仓被拒绝，要回滚
            if (order.OpenOrClose == EnumOpenClose.开仓)
            {
                //这里设置开仓被拒绝
                Machine.IsOpenRefuse = true;
                Print("开仓被拒绝，放弃开仓");
                RollBackK(order);
                return;
            }
            else
            {
                //如果是平今仓位不足---则不用再平否则会死循环
                if (order.OrderRejectReason == EnumRejectReason.平今仓位不足 ||
                    order.OrderRejectReason == EnumRejectReason.平昨仓位不足)
                {
                    Print("平仓仓位不足，不做任何处理");
                }
                else
                {
                    ResendOrder(order, 2);
                }
            }
        }


        public override void OnOrder(Order order)
        {
            //委托回报
            Print("委托回报=" + order.ToString() );

            //if (IsNight)
            //{
            //    if (WorkMode == StrategyWorkMode.Live)
            //    {
            //        //DateTime ordTime = TradingDayHelper.GetNaturalDateTime(order.InsertTime.TimeOfDay, TradingDate, preTradingDay, preTradingDayT1);
            //        //Print("夜盘-InsertTime=" + order.InsertTime.ToString()+",转换成自然时间之后="+ ordTime.ToString() );
            //    } 
            //}
        }

        public override void OnCancelOrderFailed(string orderGuid, string msg)
        {
            Print("注意：撤单失败了，orderGuid=" + orderGuid + ",msg=" + msg);
            //尝试查询一下Order
            Order xOrder = GetOrder(orderGuid);
            if ( xOrder != null)
            {
                Print("该Order=" + xOrder.ToString());
            }
            else
            {
                Print("内存中无法查询到该撤单失败的委托");
            }
        }

        /// <summary>
        /// 撤单成功
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderCanceled(Order order)
        {
            Print("撤单成功回报" + order.ToString());
            string OrdSysID = order.OrderSysID;
            string GUID = order.GUID;
            if (order.OpenOrClose == EnumOpenClose.开仓)
            {
                if (KCZhui)
                {  //开仓被撤单，追单开仓
                    ResendOrder(order, KCJP);
                }
                else
                {
                    //如果开仓超时不追，则需要回滚
                    RollBackK(order);
                    Print("开仓撤单不追!!");
                }

            }
            else
            {
                //撤单时按照2
                ResendOrder(order, PCJP);
            }
        }

        #endregion

        #region 交易辅助

        /// <summary>
        /// 前一个交易日
        /// </summary>
        public DateTime preTradingDay;

        /// <summary>
        /// 前一个交易日的下一个自然天
        /// </summary>
        public DateTime preTradingDayT1;

        /// <summary>
        /// 预初始化
        /// </summary>
        /// <param name="verinfo">版本信息</param>
        /// <param name="_MainName">策略主名称</param>
        /// <param name="_IfInDay">True日内False隔夜</param>
        /// <param name="_tExpire">策略过期日期</param>
        /// <param name="_IfKCZhui">True开仓追 False开仓不追</param>
        public void PreInit(
            string verinfo,
            string _MainName,
            bool _IfInDay,
            DateTime _tExpire,
            int _MaxKC = 100,
            bool _IfKCZhui = true)
        {
            #region

            Print("V3.0模板 模板开发日期20160126 MagicQuant");
            Print( "支持夜盘" );
            Print( "还不支持隔夜持久化" );
            Print( "必须在MQ4.1.7版本以及更高版本下运行" );

            Print("策略版本信息|" + verinfo); 
            Print("该模板适合单品种，单策略");
            Print("操作系统日期=" + Year + "-" + Month + "-" + Day);
            Print("交易所日期=" + TradingDate.ToShortDateString());
            Print("输出目录" + MyTradeOutDir);

            #region
            
            MaxQty = _MaxKC;

   

            LogAccount = IdxToLogAccount(0);
            Print("逻辑账户ID=" + LogAccount);

            VersionInfo = verinfo;

            tExpire = _tExpire;

            IfInDay = _IfInDay;

            MainName = _MainName;

            KCZhui = _IfKCZhui;

           

            Wait(1000);

            var MyFutureAccount = GetFutureAccount();
            //这里要把账户ID弄出来
            if (MyFutureAccount != null)
            {
                FutureAccountID = MyFutureAccount.ID;
                Print("策略启动账户A=" + FutureAccountID);


                Print("初始时账户动态权益=" 
                    + Math.Round(MyFutureAccount.Balance / 10000, 1).ToString()
          + "万,系统时间=" + Now.ToString());
            }
            else
            {
                FutureAccountID = LogAccount;
                Print("策略启动账户B=" + FutureAccountID);

                Print("GetFutureAccount返回为Null");
            }

            AccLst = new List<string>();
            #endregion

             

            #region 开盘收盘时间的处理
            //夜盘的重点就是收盘时间和结束时间
            //这里需要获得品种的开盘时间和结束时间
            List<TimeSlice> 交易时段 = GetInstrumentTradingTime(DefaultFutureCode);
            if (交易时段 != null && 交易时段.Count > 0)
            {
                if (IsNight)
                {
                    #region

                    Print("注意策略是夜盘模式!");

                    //前一个交易日
                    preTradingDay = TradingDayHelper.GetPreTradingDay(TradingDate);

                    //前一个交易日的第二个自然天
                    preTradingDayT1 = preTradingDay.AddDays(1);
                    Print("前一个交易日=" + preTradingDay.ToShortDateString()
                                + ",前一个交易日的第二个自然日=" + preTradingDayT1.ToShortDateString());


                    //这里把第一个交易时段取出来，就是夜盘的交易时段

                    //白盘开盘收盘的时刻搜索
                    TimeSlice firtT = new TimeSlice(new TimeSpan(21, 0, 0), new TimeSpan(2, 30, 0)); 
                    //夜盘只有一个时间段
                    foreach (var timeSlice in 交易时段)
                    {
                        //20：00~24：00 是夜盘开始
                        if (timeSlice.BeginTime > new TimeSpan(20, 0, 0) && timeSlice.BeginTime < new TimeSpan(23, 0, 0))
                        {
                            firtT = timeSlice;
                            break;
                        }
                    }

          

                    tOpen = TradingDayHelper.GetNaturalDateTime(firtT.BeginTime, TradingDate, preTradingDay, preTradingDayT1);
                    tClose = TradingDayHelper.GetNaturalDateTime(firtT.EndTime, TradingDate, preTradingDay, preTradingDayT1);
                    Print("夜盘模式,期货品种" + DefaultFutureCode + "-开盘自然时间=" + tOpen.ToString() + ",收盘自然时间=" + tClose.ToString());
                    #endregion

                }
                else
                {
                    #region 解析白盘的开盘收盘时间
                   
                    //白盘开盘收盘的时刻搜索
                    TimeSlice firtT = new TimeSlice(new TimeSpan(9, 0, 0), new TimeSpan(11, 30, 0));
                    TimeSlice endtT = new TimeSlice(new TimeSpan(14, 0, 0), new TimeSpan(15, 0, 0));
                    foreach (var timeSlice in 交易时段)
                    {
                        //5:00 到 10点之间 必然是白天开盘
                        if (timeSlice.BeginTime > new TimeSpan(5, 0, 0) && timeSlice.BeginTime < new TimeSpan(10, 0, 0))
                        {
                            firtT = timeSlice;
                            break;
                        }
                    }

                    foreach (var timeSlice in 交易时段)
                    {
                        //14：50 到 16：00 之间 必然收下午收盘
                        if (timeSlice.EndTime > new TimeSpan(14, 50, 0) && timeSlice.EndTime < new TimeSpan(16, 0, 0))
                        {
                            endtT = timeSlice;
                            break;
                        }
                    }

                    TimeSpan tStart = firtT.BeginTime;
                    TimeSpan tEnd = endtT.EndTime;
                    tOpen = new DateTime(Year, Month, Day, tStart.Hours, tStart.Minutes, tStart.Seconds);
                    tClose = new DateTime(Year, Month, Day, tEnd.Hours, tEnd.Minutes, tEnd.Seconds);
                    Print("普通白盘模式,期货品种" + DefaultFutureCode + "-开盘自然时间=" + tOpen.ToString() + ",收盘自然时间=" + tClose.ToString());
                    #endregion 
                }
            }
            else
            {  
                Print("发生严重错误:没有找到" + DefaultFutureCode + "交易时间段");
            }
            #endregion


            tFinish = tClose.AddSeconds(-收盘秒); 
            tStop = tClose.AddSeconds(-2 * 收盘秒);
            Print("默认品种=" + DefaultFutureCode);
            Print("交易账户=" + FutureAccountID);
            Print("开盘时间=" + tOpen.ToString());
            Print("收盘时间=" + tClose.ToString());
            Print("收盘秒=" + 收盘秒);
            Print("清仓时间=" + tFinish.ToString());
            Print("停止开仓时间=" + tStop.ToString());
            Print("临近中午不做时间" + TPauseAM.ToString());

            if (WorkMode == StrategyWorkMode.Live)
            {
                if (tOpen.Year != 2016)
                {
                    Print("实盘模式:请注意操作系统年份不对啊！！！！！=" + tOpen.Year);
                } 
            }  
            Print("策略过期时间=" + tExpire.ToShortDateString());


            T0 = tOpen;
            TickNow = tOpen;
            PriceTick = DefaultFuture.PriceTick;

            VolumeMultiple = DefaultFuture.VolumeMultiple;
            Print("PriceTick=" + PriceTick + ",VolumeMultiple=" + VolumeMultiple);
            if (IfInDay)
            {
                Print("日内策略，收盘底层会自动平仓");
            }
            else
            {
                Print("隔夜策略，收盘底层不会自动处理!!!!!!!!!");
                //这里需要读取隔夜的文件

                Print( "状态机文件="+ MachineFileName );

                LoadMachineFile(MachineFileName);

            }
            #endregion
        }


        /// <summary>
        /// 第一个5：00以后的时间段就是白盘的
        /// </summary>
        /// <param name="交易时段"></param>
        /// <returns></returns>
        public TimeSlice 找到白盘时间段(List<TimeSlice>  交易时段)
        {
            for (int i = 0; i < 交易时段.Count; i++)
            {
                TimeSlice ts = 交易时段[i];
                TimeSpan tStart = ts.BeginTime;
                if (tStart.Hours >= 5)
                {
                    return ts;
                }
            }
            return null;
        }
 
        /// <summary>
        /// 载入状态机文件
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadMachineFile(string fileName )
        { 
            //从文件中读取状态机文件
            //还没有写完
        }


        /// <summary>
        /// 保存状态机文件
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveMachineFile(string fileName)
        { 
            //将状态机信息写入文件
            //还没有写完
        }

        /// <summary>
        /// 计算作为一个虚函数，在基类中声明
        /// </summary>
        /// <param name="tick"></param>
        public virtual void Calculate(Tick tick)
        {
        }

        /// <summary>
        /// Tick触发
        /// </summary>
        /// <param name="tick"></param>
        public override void OnTick(Tick tick)
        {
            if (PreOnTick(tick))
            {
                Calculate(tick);

                AutoTrade();
            }
        }



        /// <summary>
        /// Tick预处理
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public bool PreOnTick(Tick tick)
        {
            #region 常用信息刷新

            当前Tick = tick;

            //这里的Tick是自然时间
            TickNow = tick.DateTime;

            if ( WorkMode == StrategyWorkMode.Simulate)
            {
                //复盘要转换
                TimeSpan timeSpan = tick.DateTime.TimeOfDay;
                TickNow = TradingDayHelper.GetNaturalDateTime(timeSpan, TradingDate, preTradingDay, preTradingDayT1);
            }
              
            最新价 = tick.LastPrice;

            中间价 = 最新价;
            if ( tick.AskPrice1 > 0 && tick.BidPrice1 > 0)
            {
                中间价 = (tick.AskPrice1 + tick.BidPrice1) / 2;
            }

            LastPrice = tick.LastPrice;
            买1价 = tick.BidPrice1;
            卖1价 = tick.AskPrice1;
            买1量 = tick.BidVolume1;
            卖1量 = tick.AskVolume1;
            最高价 = tick.HighPrice;
            最低价 = tick.LowPrice;
            总成交量 = tick.Volume;
            持仓量 = tick.OpenInterest;
            if (前一Tick != null)
            {
                当前Tick成交量 = tick.Volume - 前一Tick.Volume;
            }
            前一Tick = tick;
            #endregion

            #region 保护

            if ( TickNow < tOpen)
            {
                if (WorkMode == StrategyWorkMode.Live)
                {
                    Print("过滤开盘集合竞价" + tick.ToString());
                    Print("tOpen=" + tOpen.ToString() + ",TickNow=" + TickNow.ToString());
                } 
                return false;
            }

            if (TickNow >= tExpire)
            {
                Print("策略过期=" + tExpire.ToString());
                return false;
            }

            //if (AccountPass || DefaultAccount == "")
            //{
            //}
            //else
            //{
            //    //账户不对的话，也不能启动!!
            //    Print("账户被策略禁用" + FutureAccountID);
            //    return false;
            //}
            #endregion

            #region 坏点过滤

            //涨跌停的时候，也让策略自己处理
            if ( 
                ( tick.AskPrice1<=0 && tick.BidPrice1<= 0) ||
tick.AskPrice1 > 1000000 ||
tick.AskVolume1 > 1000000 ||
tick.BidPrice1 > 1000000 ||
tick.BidVolume1 > 1000000)
            {
                return false;
            }


            Machine.Clc++;

            追单();

            //计算开盘分钟，考虑夜盘
            已开盘分钟 = (TickNow - tOpen ).TotalMinutes;

            if (IsNight)
            {
                //夜盘不用扣除
            }
            else
            {
                //注意排除中午休息时间 
                if (TickNow >= PMBegin)
                {
                    已开盘分钟 = 已开盘分钟 - (PMBegin - AMEnd).TotalMinutes;
                }
            }
      

            已开盘分钟 = Math.Round(已开盘分钟, 1);

            return true;
            #endregion
        }

        void 追单()
        {
            #region 追单
            OrderSeries cancelLst = GetCanCancelOrders(EnumMarket.期货, DefaultAccount);
            if (cancelLst != null && cancelLst.Count > 0)
            {
                //注意开仓单也追
                Print("可撤单列表行数=" + cancelLst.Count);
                foreach (Order order in cancelLst)
                {
                    Print("可撤单委托ID=" + order.OrderSysID);
                    DateTime ordTime = order.InsertTime;
                    double Dt = 0 ;
                    //追单的时候注意，订单的InsertTime的日期部分是交易所给的，需要转换成自然时间

                    if (IsNight)
                    {
                        //夜盘需要转换报单时间
                        //ordTime = TradingDayHelper.GetNaturalDateTime(ordTime.TimeOfDay, TradingDate, preTradingDay, preTradingDayT1);
                        //夜盘需要转换时间
                        Dt = (TickNow - ordTime).TotalSeconds;

                        //Print("夜盘订单时间转换前=" + order.InsertTime.ToString()+",转换成自然时间之后="+ ordTime.ToString() );
                    }
                    else
                    {
                        Dt = (TickNow - ordTime).TotalSeconds; 

                        //注意考虑中午休息时间
                        if (TickNow >= PMBegin && order.InsertTime <= AMEnd)
                        {
                            Dt = Dt - (PMBegin - AMEnd).TotalSeconds;
                        } 
                    }

          

                    if (order.OpenOrClose == EnumOpenClose.开仓)
                    {
                        if ( Dt > KCBC && order.OrderSysID != "")
                        {
                            CancelOrder(order);
                        }
                    }
                    else
                    {
                        if (Dt > PCBC && order.OrderSysID != "")
                        {
                            CancelOrder(order);
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 显示策略核心状态位
        /// </summary>
        void 显示当前状态(string info)
        {
            //显示核心信息 
            Print(info);
            Print(Machine.ToString());
        }

        public void AutoTrade()
        {
            //中午屏蔽

            if (WorkMode == StrategyWorkMode.Live)
            {
                #region 白盘临近中午收盘不下单
               
                if (IsNight)
                {
          
                }
                else
                {
                    if (TickNow >= TPauseAM && TickNow <= PMBegin)
                    {
                        if (WorkMode == StrategyWorkMode.Live)
                        {
                            Print("临近中午屏蔽交易,TickNow=" + TickNow.ToString());
                        }

                        return;
                    }
                }
                #endregion
            }
   

            Machine.TargetFlag = 计算策略目标状态位();

            核心交易();

            //成交回报时就会保存
            快速计算盈亏();

        }
      
        /// <summary>
        /// 计算当前策略的持仓量 3表示3张多单，-2表示2张空单
        /// 模拟策略信号
        /// </summary> 
        /// <returns></returns>
        int 计算策略目标状态位()
        {
            #region 获取策略当前信号
            //策略分为3个阶段：正常开仓，不开仓，收盘清仓
            int 计算信号 = StrategySignal;
            int 返回信号 = 0;
            //如果是日内，则要平仓
            if (IfInDay)
            {
                if (IfTrigStopLoss)
                {
                    //触发止损
                    返回信号 = 0;
                }
                else
                {

                    #region 需要根据信号计算

                    if ( tStop < TickNow && TickNow < tFinish)
                    {
                        //这段时间不开新仓，只平仓，保持策略持仓
                        if (计算信号 == 0 || 计算信号 * Machine.Flag < 0)
                        {
                            //如果策略需要平仓或者反向，则直接平仓 
                            返回信号 = 0;
                        }
                        else
                        {
                            //如果策略主动要求减仓，则可以平仓
                            if (计算信号 * Machine.Flag > 0 && Abs(计算信号) < Abs(Machine.Flag))
                            {
                                返回信号 = 计算信号;
                            }
                            else
                            {
                                返回信号 = Machine.Flag;
                            }
                        }
                    }
                    else if (tFinish < TickNow)
                    {
                        //收盘清理时间
                        返回信号 = 0;
                    }
                    else
                    {
                        if (tStop < TickNow)
                        {
                            返回信号 = 0;
                        }
                        else
                        {
                            返回信号 = 计算信号;
                        }

                    }
                    #endregion

                    if (多空模式 == 1)
                    { 
                        //只做多
                        返回信号 = Math.Max(0, 返回信号);
                    }
                    else if (多空模式 == 2)
                    {  
                        //只做空
                        返回信号 = Math.Min(0, 返回信号);
                    }
                }

            }
            else
            {
                返回信号 = 计算信号;
            }


            //临近涨跌停不交易
            if (当前Tick != null)
            {
                if (当前Tick.UpLimit > 0)
                {
                    if ( 当前Tick.LastPrice >= 当前Tick.UpLimit - 30 * PriceTick  )
                    {
                        Print("注意临近涨停:LastPrice=" + 当前Tick.LastPrice + ",UpLimit=" + 当前Tick.UpLimit);
                        if ( 返回信号 < 0 )
                        {
                            Print( "接近涨停不做空，调整返回信号="+返回信号+",到0" );
                            返回信号 = 0;
                        }
                    }
                }
                if (当前Tick.DropLimit > 0)
                {
                    if ( 当前Tick.LastPrice <= 当前Tick.DropLimit + 30 * PriceTick  )
                    {
                        Print("注意临近跌停:LastPrice=" + 当前Tick.LastPrice + ",DropLimit=" + 当前Tick.DropLimit);
                        if (返回信号 < 0)
                        {
                            Print("接近跌停不做空，调整返回信号=" + 返回信号 + ",到0");
                            返回信号 = 0;
                        }
                    }
                }
            } 
            return 返回信号;
            #endregion
        }


        /// <summary>
        /// 重发委托
        /// </summary>
        /// <param name="order"></param>
        /// <param name="jump"></param>
        /// <returns></returns>
        Order ResendOrder(Order order, int jump)
        {
            Print("重发委托" + order.ToString());
            if ( order.VolumeLeft > 0)
            {
                Order neworder = SafeOrder(
                    jump,
                    order.InstrumentID,
                    order.VolumeLeft,
                    order.Direction,
                    order.OpenOrClose);
                return neworder;
            }
            else
            {
                Print("委托剩余量为0，不需要重发:" + order.ToString());
            }
            return null;
        }

        /// <summary>
        /// 保守下单，在盘口浮动跳打单
        /// </summary>
        /// <param name="jump"></param>
        /// <param name="InsID"></param>
        /// <param name="Qty"></param>
        /// <param name="direction"></param>
        /// <param name="oc"></param>
        /// <returns></returns>
        Order SafeOrder(int jump, string InsID, int Qty, EnumBuySell direction, EnumOpenClose oc)
        {
            if (Qty > 0)
            {
                #region 保守发单
                Tick tick = LastFutureTick(InsID);
                double ordPrice = tick.LastPrice;

                //开盘第一分钟和收盘时用2跳

                //?
                //if ((TickNow.Hour == 9 && TickNow.Minute == 15) 
                //    || (TickNow.Hour == 15 && TickNow.Minute == 14))
                //{
                //    jump = 2;
                //}

                if (direction == EnumBuySell.买入)
                {
                    if (tick.BidPrice1 > 0)
                    {
                        ordPrice = tick.BidPrice1 + jump * PriceTick;
                    }
                   
                }
                else
                {
                    if (tick.AskPrice1 > 0)
                    {
                        ordPrice = tick.AskPrice1 - jump * PriceTick;
                    }
                   
                }
                //注意不能超过涨停或者跌停价
                ordPrice = TrimFuturePrice(DefaultFuture, ordPrice);

                if (tick.UpLimit > MinDouble && ordPrice > tick.UpLimit)
                {
                    Print("密切注意：价格超出涨停！按照涨停价发单");
                    ordPrice = tick.UpLimit;
                }

                if (tick.DropLimit > MinDouble && ordPrice < tick.DropLimit)
                {
                    Print("密切注意：价格超出跌停！按照跌停价发单");
                    ordPrice = tick.DropLimit;
                }

                Print("安全下单" + Qty + "手" + oc.ToString() + direction.ToString() + InsID
                    + "@" + ordPrice.ToString() + ",KPBZ=" + TickNow.ToString());
                return LimitOrder(InsID, Qty, ordPrice, direction, oc, EnumHedgeFlag.投机);
                #endregion
            }
            else
            {
                Print("下单量=" + Qty);
                return null;
            }
        }

        /// <summary>
        /// 回滚
        /// </summary>
        /// <param name="order"></param>
        public void RollBackK(Order order)
        {
            //资金不足----开仓失败！！
            //策略的KN 和 KN_Trd要修改成0 PN 
            Machine.KN = 0;
            Machine.KN_Trd = 0;
            Machine.PN = 0;
            Machine.PN_Trd = 0;
            Machine.IsDoing = false;//操作已经完成

            //资金不足或者开仓超时被撤单（且开仓不追），则状态位需要回到老的状态

            #region 状态位的确定

            int 部分成交量 = order.VolumeTraded;
            if (order.Direction == EnumBuySell.买入)
            {
                部分成交量 = order.VolumeTraded;
            }
            else if (order.Direction == EnumBuySell.卖出)
            {
                部分成交量 = -order.VolumeTraded;
            }
            int 终态 = Machine.OldFlag + 部分成交量;
            Print("部分成交量=" + 部分成交量 + ",终态=" + 终态);


            if (Machine.OldFlag == 0)
            {
                Machine.Flag = 终态;
                Print("原来老的标志位=0，没有开出来,认可标志位=" + Machine.Flag);

            }
            else if (Machine.OldFlag > 0)
            {
                if (order.Direction == EnumBuySell.买入)
                {
                    Print("原来老的标志位=" + Machine.OldFlag + "，同向增仓做多,恢复到原来老的标志位");


                    Machine.KN = 终态;
                    Machine.KN_Trd = 终态;
                    Machine.Flag = 终态;

                }
                else
                {
                    Print("原来老的标志位=" + Machine.OldFlag + "，但是拒绝的是卖开，逻辑有问题，需要排查");

                    Machine.KN = 0;
                    Machine.KN_Trd = 0;
                    Machine.Flag = 0;
                }

            }
            else if (Machine.OldFlag < 0)
            {
                if (order.Direction == EnumBuySell.卖出)
                {
                    Print("原来老的标志位=" + Machine.OldFlag + "，同向增仓做空,恢复到原来老的标志位");

                    Machine.KN = Abs(终态);
                    Machine.KN_Trd = Abs(终态);
                    Machine.Flag = 终态;

                }
                else
                {
                    Print("原来老的标志位=" + Machine.OldFlag + "，但是拒绝的是买开，逻辑有问题，需要排查");


                    Machine.KN = 0;
                    Machine.KN_Trd = 0;
                    Machine.Flag = 0;
                }

             
            }
            T0 = TickNow;//开仓拒绝的时候把时间戳修改过来
            Print("回滚后最终的状态位=" + Machine.Flag);
            #endregion
        }

        #endregion

        #region 附加功能

        
        #region 利润计算

        public string MainName = "TOM_A";

        public double 毛利润 = 0;

        public double 手续费 = 0;

        public double 净利润 = 0;

        double 手续费率 = 0.26;

        TradeSeries TRADES = new TradeSeries();

        TradeSeries HoldTRADES = new TradeSeries();

        double 盈亏A = 0;

        double 盈亏C = 0;

        void 保存成交()
        {
            //成交还是要记录的！！！！
            try
            {
                //这里记录临时成交
                SaveTrades(TRADES, MainName);
                TRADES.Clear();
            }
            catch (Exception ex)
            {
                Print("记录成交失败-" + ex.Message);
            }
        }

        /// <summary>
        /// 记录成交的目录
        /// </summary>
        string MyTradeOutDir = "c:\\TOM\\";



        /// <summary>
        /// 保存成交
        /// </summary>
        /// <param name="lstTrd"></param>
        /// <param name="strPost"></param>
        void SaveTrades(List<Trade> lstTrd, string strPost)
        {
            #region
            Print("保存成交");
            if (FutureAccountID != "" && lstTrd.Count > 0)
            {
                string outStr = "";
                foreach (Trade trd in lstTrd)
                {
                    string str = trd.TradeTime.ToString() + ",";
                    str += trd.Direction.ToString() + ",";
                    str += trd.OpenOrClose.ToString() + ",";
                    str += trd.Volume.ToString() + ",";
                    str += trd.Price.ToString() + ",";
                    str += trd.InstrumentID + ",";
                    str += trd.TradeID + ",";
                    str += trd.OrderSysID + ",";
                    outStr += str + "\r\n";
                }

                if (!Directory.Exists(MyTradeOutDir))
                {
                    Directory.CreateDirectory(MyTradeOutDir);
                }
                //把策略名称加上
                string FileName = MyTradeOutDir + FutureAccountID + "_"+ DefaultFutureCode+"_" + MainName + "_" + strPost + ".csv";
                try
                {
                    if (!File.Exists(FileName))
                    {
                        string head = "";
                        head += "成交时间,";
                        head += "买卖,";
                        head += "开平,";
                        head += "成交数量,";
                        head += "成交价格,";
                        head += "品种代码,";
                        head += "成交编号,";
                        head += "委托编号,";
                        head += "\r\n";
                        File.AppendAllText(FileName, head, Encoding.Default);
                    }
                    File.AppendAllText(FileName, outStr, Encoding.Default);

                }
                catch (Exception ex)
                {
                    Print("保存文件" + FileName + "出错" + ex.ToString());
                }
            }
            #endregion
        }

    

        /// <summary>
        /// 快速计算盈亏
        /// </summary>
        /// <returns></returns>
        private void 快速计算盈亏()
        {
            净利润 = Math.Round(盈亏A + 盈亏C * 最新价, 2);
            if (!IfTrigStopLoss)
            {
                //触发止损是不可逆的

                if ( Abs(最大亏损) > MinDouble)
                {
                    //注意单位是万
                    if (净利润 < -Abs(最大亏损) * 10000 - MinDouble)
                    {
                        _ifTrigStopLoss = true;
                        //一旦触发止损，要马上出场
                        PCJP = 30;
                        Print("！！！！风控触发，立刻平仓，不再交易:当日亏损超过最大亏损金额="
                            + 最大亏损.ToString() + "万");
                    }
                }
            }
        }

        /// <summary>
        /// 计算盈亏
        /// </summary>
        void 重新整理盈亏公式()
        {
            #region
            // 如果成交没有了结，用最新价计算

            //成交的买卖能对应上
            int buyCount = 0;
            int sellCount = 0;
            //注意计算盈亏时要用不清空那个列表
            foreach (Trade trade in HoldTRADES)
            {
                if (trade.Direction == EnumBuySell.买入)
                {
                    buyCount += trade.Volume;
                }
                else
                {
                    sellCount += trade.Volume;
                }
            }

            double BuyMoney = 0;
            double SellMoney = 0;
            if (HoldTRADES != null && HoldTRADES.Count > 0)
            {
                //把所有的
                foreach (Trade trade in HoldTRADES)
                {
                    if (trade.Direction == EnumBuySell.买入)
                    {
                        BuyMoney += trade.Volume * trade.Price * VolumeMultiple;

                    }
                    else
                    {
                        SellMoney += trade.Volume * trade.Price * VolumeMultiple;
                    }
                }
            }

            //用公式表达
            手续费 = (SellMoney + BuyMoney) * 手续费率 / 10000;
            盈亏A = SellMoney - BuyMoney - 手续费;

            //最新价
            if (sellCount > buyCount)
            {
                盈亏C = -(sellCount - buyCount) * VolumeMultiple;
            }
            else if (sellCount < buyCount)
            {
                //虚构卖出金额
                盈亏C = (buyCount - sellCount) * VolumeMultiple;
            }
            else
            {
                盈亏C = 0;
            }

            快速计算盈亏();
            Print("净利润=" + 净利润 + "元");

            #endregion
        }

        #endregion


        #region 控制策略使用账户
        
        /// <summary>
        /// 允许账户总数
        /// </summary>
        List<string> AccLst = new List<string>();

        /// <summary>
        /// 策略有效时间
        /// </summary>
        DateTime tExpire;

        /// <summary>
        /// 账户是否通过
        /// </summary>
        bool AccountPass = false;

        /// <summary>
        /// 添加允许交易账户
        /// </summary>
        /// <param name="accid"></param>
        public void ProtectAccount(string accid)
        {
            if (!AccLst.Contains(accid))
            {
                AccLst.Add(accid);
                if (FutureAccountID == accid)
                {
                    Print("账户[" + accid + "]可启动策略");
                }
            }

            AccountPass = AccLst.Contains(FutureAccountID);

        }
        #endregion

        #endregion
    }


}