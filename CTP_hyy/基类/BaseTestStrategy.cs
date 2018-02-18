using System;
using Ats.Core;
using Ats.Indicators;
namespace CTP_交易.基类
{
    public class BaseTestStrategy : Strategy
    {
        [Parameter(Display = "市场", Description = "0 期货  1股票  2股票期权  3期货期权", Category = "市场")]
        public int 市场 = 0;

        [Parameter(Display = "开平标志", Description = "0开仓  1平仓  2平今", Category = "下单")]
        public int 开平标志 = 0;
         

        [Parameter(Display = "品种代码", Description = "", Category = "下单")]
        public string 品种代码 = "ag1506";

        [Parameter(Display = "委托价格", Description = "不要成交，买入 跌停", Category = "下单")]
        public double 委托价格 = 2500;

        [Parameter(Display = "委托数量", Description = "单位:张，股", Category = "下单")]
        public int 委托数量 = 1;

        [Parameter(Display = "买卖方向", Description = " 0 买入  1 卖出", Category = "下单")]
        public int 买卖方向 = 0;

        public Order myOrder;

        /// <summary>
        /// 测试是否通过
        /// </summary>
        public bool TestPass = false;

        /// <summary>
        /// 交易所ID
        /// </summary>
        public string ExchangeID
        {
            get
            {
                string ExchangeID = "";
                if (MyMarket == EnumMarket.股票)
                {
                    if (AllStocks.Contains(品种代码))
                    {
                        ExchangeID = AllStocks[品种代码].ExchangeID;
                    }

                }
                else if (MyMarket == EnumMarket.期货)
                {
                    if (this.AllFutures.Contains(品种代码))
                    {
                        ExchangeID = AllFutures[品种代码].ExchangeID;
                    }
                }
                else if (MyMarket == EnumMarket.股票期权)
                {
                    if (this.AllStockOptions.Contains(品种代码))
                    {
                        ExchangeID = AllStockOptions[品种代码].ExchangeID;
                    }
                }
                else if (MyMarket == EnumMarket.期货期权)
                {
                    if (this.AllFutureOptions.Contains(品种代码))
                    {
                        ExchangeID = AllFutureOptions[品种代码].ExchangeID;
                    }
                }

                if (ExchangeID == "")
                {
                    Print("注意:没有获取到交易所代码，使用中金所");
                    ExchangeID = "CFFEX";
                }

                

                return ExchangeID;
            }
        }

        public EnumMarket MyMarket
        {
            get
            {
                if (市场 == 1)
                {
                    return EnumMarket.股票;
                }
                else if (市场 == 2)
                {
                    return EnumMarket.股票期权;
                }
                else if (市场 == 3)
                {
                    return EnumMarket.期货期权;
                }
                return EnumMarket.期货;
            }
        }

        public EnumOpenClose MyOC
        {
            get
            {
                if (开平标志 == 1)
                {
                    return EnumOpenClose.平仓;
                }
                else if (开平标志 == 2)
                {
                    return EnumOpenClose.平今仓;
                }
                return EnumOpenClose.开仓;
            }
        }

        public EnumBuySell MyDirection
        {
            get
            {
                if (买卖方向 == 1)
                {
                    return EnumBuySell.卖出;
                }
                return EnumBuySell.买入;
            }
        }


        public override void Init()
        {
            Print("交易日=" + TradingDate.ToShortDateString());
        }

        #region 事件回报日志
        

        public override void OnOrder(Order order)
        {
            Print("委托回报"+order.ToString());
        }

        public override void OnTrade(Trade trade)
        {
            Print( "成交回报-"+trade.ToString() );
        }


        public override void OnOrderCanceled(Order order)
        { 
            Print("撤单成功回报" + order.ToString());
            
        }

        public override void OnOrderRejected(Order order)
        { 
            Print("委托拒绝:"+order.ToString() +","+order.OrderRejectReason.ToString() );
        }
          

        public override void OnCancelOrderFailed(string GUID, string msg)
        {
            Print("撤单失败回报:ordId=" + GUID + ",msg=" + msg);

            try
            {
                Order order = QueryOrder(GUID);
                Print("期货撤单失败[" + msg + "]回报" + order.ToString());
            }
            catch (Exception ex)
            {
                Print("查询委托时发生异常:" + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 报警
        /// </summary>
        public void AlarmError()
        {
            PlaySound(EnumSound.alarm);
        }
    }
}