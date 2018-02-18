using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ats.Core;
using Ats.Indicators;

namespace CTP_交易.基类
{
    class Class1 : Strategy
    {
        private BarSeries _1mlist, _5mlist, _15mlist, _60mlist, _1dlist;
        private BarSeries vlist;
        public enum macd壮态 { 金叉, 死叉, zdif, zdea, _dif, _dea }
        public struct 操盘
        {
            public DateTime dt;
            public macd壮态 macd壮态;
            public double 高低点差;
            public double 上一结构价格;
            public double 当前位置的百分比;
            public 操作 操作;
            public int 距离;
            public 结构 前一位置结构;
            public double 浮动赢亏;
            public bool 信号对错;
        }
        private AtsList<操盘> 操盘表;
        private MACD macd, vmacd;
        private MA ma5, mv5;
        private bool isout = false;
        int barsCount = 0;
        int x = 0;
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

       


        #endregion
        #region
        public enum 方向 { }
        public enum 风险 { }
        public enum 结构 { 顶, 底, 中继, 起点 }
        public enum 价格趋势 { }
        public enum 成交趋势 { }
        public enum 仓位趋势 { }
        public bool 最低;
        public bool 最记;
        public enum 操作 { 空, 多, 无 }
        #endregion


        #region   K线
        public class barslist : Object
        {
            List<K线> bar;
            K线 b;
            public int count;
            public void barlist()
            {
                bar = new List<K线>();

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
        List<Tick> TickBuffer = new List<Tick>();
        int 大于均价次数;
        int 小于均价次数;
        List<double> zd;
        public struct 盘口
        {
            public double 价格;
            public double 剩余买单量;
            public double 剩余卖单量;
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
        public AtsList<盘口> 盘口表;
        public void 处理盘口(Tick tick)
        {
            bool find = false;

            foreach (盘口 pk in 盘口表)
            {
                if (pk.价格 == tick.LastPrice)
                { }
            }
        }
        public double 最新价 = 0;
        public double LastPrice = 0;
        public double 买价 = 0;
        public double 卖价 = 0;
        public int 买量 = 0;
        public int 卖量 = 0;
        public double 现手 = 0;
        public double 最高价 = 0;
        public double 最低价 = 0;
        public double 仓 = 0;
        public bool 多or空 = false;
        int io = 0;
        public void 计算开平多空(Tick tick)
        {
            io++;
            if (preTick != null)
            {
                现手 = tick.Volume - preTick.Volume;
                仓 = tick.OpenInterest - preTick.OpenInterest;
                if (tick.LastPrice >= preTick.AskPrice1) { 多or空 = true; } else { 多or空 = false; }

                if (多or空)
                {
                    多空.多量 += 现手;
                    if (仓 > 0)
                        多空.开多数 += 仓;
                    else 多空.平空数 -= 仓;
                }
                else
                {
                    多空.空量 += 现手;
                    if (仓 > 0)
                        多空.开空数 += 仓;
                    else 多空.平多数 -= 仓;
                }
            }
            else
            {
                多空.多量 = 0;
                多空.空量 = 0;
                多空.开多数 = 0;
                多空.开空数 = 0;
                多空.平多数 = 0;
                多空.平空数 = 0;
            }

            if (tick.Volume != 0)
            {
                均价 = tick.Turnover / tick.Volume;
                preTick = tick;
            }
            else
            {
                均价 = tick.PreSettlementPrice;
            }
            #region 显示
          
            #endregion
        }

        public void 处理tick(Tick tick)
        {
            计算开平多空(tick);
            处理盘口(tick);
            计算局部涨跌();
        }
        public void 计算局部涨跌()
        {
        }
        public void openan()
        {
            double ohigh, olow;

            if (blist1.Last().zd == 结构.底)
            {
                olow = blist1.Last().low;
                ohigh = blist1[blist1.Count - 2].high;
            }
            else
            {
                ohigh = blist1.Last().high;
                olow = blist1[blist1.Count - 2].low;
            }
        }
        public override void OnTick(Tick tick)
        {
            TickBuffer.Add(tick);
            处理tick(tick);
            openan();
        }
        #region  计算开盘前涨跌
        public double _1m_highest = 0;
        public double _5m_highest = 0;
        public double _15m_highest = 0;
        public double _1m_lowest = 0;
        public double _5m_lowest = 0;
        public double _15m_lowest = 0;
        public double _1m_pr = 0;
        public double _5m_pr = 0;
        public double _15m_pr = 0;
        public double curr = 0;
        #endregion
        public void 计算开盘前涨跌()
        {
            _1m_highest = _1mlist.High.Max();
            _5m_highest = _5mlist.High.Max();
            _15m_highest = _15mlist.High.Max();
            _1m_lowest = _1mlist.Low.Min();
            _5m_lowest = _5mlist.Low.Min();
            _15m_lowest = _15mlist.Low.Min();
            curr = _1mlist.Last.Close;
            if (_1m_highest - _1m_lowest != 0)
                _1m_pr = (curr - _1m_lowest) / (_1m_highest - _1m_lowest);
            else _1m_pr = 1;
            if (_5m_highest - _5m_lowest != 0)
                _5m_pr = (curr - _5m_lowest) / (_5m_highest - _5m_lowest);
            else _5m_pr = 1;
            if (_15m_highest - _15m_lowest != 0)
                _15m_pr = (curr - _15m_lowest) / (_15m_highest - _15m_lowest);
            else _15m_pr = 1;
            /*     Print(_1m_highest.ToString() + "|" + _1m_lowest.ToString() + "|" + curr.ToString() + "|" + _1m_pr.ToString());
                 Print(_5m_highest.ToString() + "|" + _5m_lowest.ToString() + "|" + curr.ToString() + "|" + _5m_pr.ToString());
                 Print(_15m_highest.ToString() + "|" + _15m_lowest.ToString() + "|" + curr.ToString() + "|" + _15m_pr.ToString());
     */
        }
        public void 设置指标()
        {

            操盘 cp = new 操盘();
            TickBuffer = new List<Tick>();
            操盘表 = new AtsList<操盘>();
            盘口表 = new AtsList<盘口>();
   //         _1mlist = GetBarSeries(EnumMarket.期货, DefaultFutureCode, 1, EnumBarType.分钟, 240, EnumRestoration.前复权);
     //       _5mlist = GetBarSeries(EnumMarket.期货, "ru1609", 5, EnumBarType.分钟, 100, EnumRestoration.前复权);
       //     _15mlist = GetBarSeries(EnumMarket.期货, "ru1609", 15, EnumBarType.分钟, 100, EnumRestoration.前复权);
            //        _1dlist = GetBarSeries(EnumMarket.期货, "ru1605", 1, EnumBarType.日线, 100, EnumRestoration.前复权);
            if (_1mlist != null)
            {
                for (int i = 0; i < _1mlist.Count; i++)
                {
                    cp.dt = _1mlist[i].BeginTime;
                    cp.高低点差 = _1mlist.HighestHigh(0, i) - _1mlist.LowestLow(0, i);
                    操盘表.Add(cp);
                    //   Print("k:" + _1mlist[i].ToString());
                }
            }
            计算开盘前涨跌();
            多空.多量 = 0;
            多空.开多数 = 0;
            多空.空量 = 0;
            多空.平多数 = 0;
            多空.平空数 = 0;
        }
        多空开平 多空;
        public override void Init()
        {
            多空 = new 多空开平();
            设置指标();
            成笔();

            生成(_1mlist);
            //     Print("onbar bars[0]: " + bars[0].ToString());
            //      Print("生成(_1mlist); init bars.count: " + bars.Count.ToString());
            blistcount = blist1.Count;
        }
        public void 统计k(Bar bar)
        {

        }
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
            public double dif_;
            public double dea_;
        }

        public List<笔> blist1;
        public BarSeries bars;
        public BarSeries bartmp;
        public int 顶底状态;
        int index, bindex;
        public void 成笔()
        {
            顶底状态 = (int)结构.起点;
            blist1 = new List<笔>();
            bars = new BarSeries();
            index = 0;
            bindex = 0;
        }
        public void 生成(BarSeries bs)
        {
            笔 bi = new 笔();
            bars.Clear();
            blist1.Clear();
            for (int i = 0; i < bs.Count; i++)
            {
                if (i == 0)
                {/*
                        bi.zd = 结构.起点;
                        bi.o = bs[0].Close;
                        bi.high = 0;
                        bi.ba = bs[0];
                        bi.low = 0;
                        blist1.Add(bi);
                        bars.Add(bs[0]);*/
                }
                else
                {
                    bars.Add(bs[i]);
                    包含计算();
                }
            }
        }

        public void 包含计算()
        {
            #region
            Bar bar1, bar2;
            int ind = bars.Count - 1;
            int bh1, bh2, bh3;
            int bl1, bl2, bl3;
            if (ind > 2)
            {
                bar1 = bars[ind];
                bar2 = bars[ind - 1];
                bh1 = MathHelper.ConvertToInt(bar1.High);
                bh2 = MathHelper.ConvertToInt(bar2.High);
                bl1 = MathHelper.ConvertToInt(bar1.Low);
                bl2 = MathHelper.ConvertToInt(bar2.Low);
                if ((bh1 >= bh2) && (bl1 <= bl2))
                {
                    if (blist1.Count == 0) { return; }
                    if (blist1.Last().zd == 结构.底)
                    {
                        bar1.Low = bar2.Low;
                        bars.Remove(bar2);
                    }
                    if (blist1.Last().zd == 结构.顶)
                    {
                        bar1.High = bar2.High;
                        bars.Remove(bar2);
                    }
                    return;
                }
                if ((bh1 <= bh2 && (int)bl1 >= bl2))
                {
                    if (blist1.Count == 0) { return; }
                    if (blist1.Last().zd == 结构.底)
                    {
                        bar2.Low = bar1.Low;
                        bars.Remove(bar1);
                    }
                    if (blist1.Last().zd == 结构.顶)
                    {
                        bar2.High = bar1.High;
                        bars.Remove(bar1);
                    }
                }
            }
            #endregion
            Bar ba1, ba2, ba3;
            笔 bi = new 笔();
            ind = bars.Count - 1;
            if (ind > 7)
            {
                ba1 = bars[ind - 2];
                ba2 = bars[ind - 1];
                ba3 = bars[ind];
                bh1 = MathHelper.ConvertToInt(ba1.High);
                bh2 = MathHelper.ConvertToInt(ba2.High);
                bh3 = MathHelper.ConvertToInt(ba3.High);
                bl1 = MathHelper.ConvertToInt(ba1.Low);
                bl2 = MathHelper.ConvertToInt(ba2.Low);
                bl3 = MathHelper.ConvertToInt(ba3.Low);
                if ((bh2 > bh1 && bh2 > bh3) && (bl2 > bl1 && bl2 > bl3))
                {
                    if (blist1.Count == 0)
                    {
                        bi.zd = 结构.顶;
                        bi.o = 0;
                        bi.high = bh2;
                        bi.low = bl2;
                        bi.ba = ba2;
                        bi.index = ind - 1;
                        blist1.Add(bi);

                    }
                    if (blist1.Last().zd == 结构.顶 && bh1 > MathHelper.ConvertToInt(blist1.Last().high))
                    {

                        blist1.Remove(blist1.Last());
                        bi.zd = 结构.顶;
                        bi.o = 0;
                        bi.high = bh2;
                        bi.low = bl2;
                        bi.ba = ba2;
                        bi.index = ind - 1;
                        blist1.Add(bi);

                    }
                    if (blist1.Last().zd == 结构.底 && ind > blist1.Last().index + 4 && bh1 > MathHelper.ConvertToInt(blist1.Last().high))
                    {
                        bi.zd = 结构.顶;
                        bi.o = 0;
                        bi.high = bh2;
                        bi.low = bl2;
                        bi.ba = ba2;
                        bi.index = ind - 1;
                        blist1.Add(bi);

                    }

                }
                if ((bh2 < bh1 && bh2 < bh3) && (bl2 < bl1 && bl2 < bl3))
                {
                    if (blist1.Count == 0)
                    {
                        bi.zd = 结构.底;
                        bi.o = 0;
                        bi.high = bh2;
                        bi.low = bl2;
                        bi.ba = ba2;
                        bi.index = ind - 1;
                        blist1.Add(bi);

                    }
                    if (blist1.Last().zd == 结构.底 && bl1 < MathHelper.ConvertToInt(blist1.Last().low))
                    {
                        blist1.Remove(blist1.Last());
                        bi.zd = 结构.底;
                        bi.o = 0;
                        bi.high = bh2;
                        bi.low = bl2;
                        bi.ba = ba2;
                        bi.index = ind - 1;
                        blist1.Add(bi);

                    }
                    if (blist1.Last().zd == 结构.顶 && ind > blist1.Last().index + 4 && bl1 < MathHelper.ConvertToInt(blist1.Last().low))
                    {
                        bi.zd = 结构.底;
                        bi.o = 0;
                        bi.high = bh1;
                        bi.low = bl1;
                        bi.ba = ba1;
                        bi.index = ind - 1;
                        blist1.Add(bi);

                    }
                }
            }
        }


        public int 计算顶底()
        {
            int 当前 = (int)结构.中继;
            return 当前;
        }
        public void 一买() { }
        public void 二买() { }
        public void 三买() { }
        public void 一卖() { }
        public void 二卖() { }
        public void 三卖() { }
        public void 中枢() { }
        public void 包含处理() { }

        struct K线
        {
            double high;
            double low;
            double open;
            double close;
            double volum;
            int ser;
            DateTime start;
            DateTime end;
            bool yx;
        }
        public override void OnBar(Bar bar)
        {
            {
                macd = new MACD(_1mlist);
                mv5 = new MA(_1mlist, BarData.Volume, 5);
                ma5 = new MA(_1mlist, 5); ;
                AddIndicator(macd);
                AddIndicator(ma5);
                AddIndicator(mv5);
                /*  Print("k:" + _1mlist.Ago(0).ToString());
                             Print("ma:  " + macd.Ago(0).ToString());
                             Print("vma:  " + mv5.Ago(0).ToString());
                             Print("ma:  " + ma5.Ago(0).ToString());
                             Print("dif:" + macd.DIF(0).ToString() + "dea" + macd.DEA(0).ToString());*/
                //         Print(klist(0).)
                生成(_1mlist);
                /*
                Print("onbar bars[0]: " + bars[0].ToString());
                Print("onbar bars.Count: " + bars.Count.ToString());
                Print("onbar blist1.Count: " + blist1.Count.ToString());*/
                for (int j = 0; j < blist1.Count; j++)
                {
                    if (blist1[j].zd == 结构.顶)
                        Print("blist1" + "^|" + blist1[j].high.ToString() + "|" + blist1[j].ba.BeginTime.ToString());
                    else
                        Print("blist1" + "v|" + blist1[j].low.ToString() + "|" + blist1[j].ba.EndTime.ToString());
                }
            }

            var 当前多头持仓 = GetPosition(EnumMarket.期货, DefaultAccount, DefaultFutureCode, EnumPositionDirection.多头);
            var 当前空头持仓 = GetPosition(EnumMarket.期货, DefaultAccount, DefaultFutureCode, EnumPositionDirection.空头);





            if (blist1.Last().zd == 结构.底)
            {
                if (当前空头持仓 != null)
                {
                    Print(当前空头持仓.ToString());

                    if (当前空头持仓.YdPosition > 0) { order = LimitOrder(当前空头持仓.Volume, blist1.Last().ba.Close, EnumBuySell.买入, EnumOpenClose.平仓); }
                    if (当前空头持仓.TodayPosition > 0) { order = LimitOrder(当前空头持仓.Volume, blist1.Last().ba.Close, EnumBuySell.买入, EnumOpenClose.平今仓); }

                }
                if (当前多头持仓 != null) { return; }
                else
                {
                    order = LimitOrder(1, blist1.Last().ba.Close, EnumBuySell.买入, EnumOpenClose.开仓);
                }

            }
            if (blist1.Last().zd == 结构.顶)
            {
                if (当前多头持仓 != null)
                {
                    Print(当前多头持仓.ToString());

                    if (当前多头持仓.YdPosition > 0) { order = LimitOrder(当前多头持仓.Volume, blist1.Last().ba.Close, EnumBuySell.卖出, EnumOpenClose.平仓); }
                    if (当前多头持仓.TodayPosition > 0) { order = LimitOrder(当前多头持仓.Volume, blist1.Last().ba.Close, EnumBuySell.卖出, EnumOpenClose.平今仓); }

                }
                if (当前空头持仓 != null) { return; }
                else
                {
                    order = LimitOrder(1, blist1.Last().ba.Close, EnumBuySell.卖出, EnumOpenClose.开仓);
                }
            }
        }


        int blistcount = 0;
        Order order;
        public void OnBarOpen(Bar bar)
        {

        }


        #region 行情瞬间信息

        /// <summary>
        /// 上次开仓时间
        /// </summary>
        public DateTime T0;

        /// <summary>
        /// 最新价
        /// </summary>
        public double 均价 = 0;

        public Tick tmpTick;

        public Tick preTick;


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

    }
}
