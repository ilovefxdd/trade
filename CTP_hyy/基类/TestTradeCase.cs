using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Core;
using Ats.Indicators;

namespace CTP_交易.基类
{
    /// <summary>
    /// 测试交易例子
    /// </summary>
    public class TestTradeCase
    {
        public EnumMarket Market = EnumMarket.股票;
        public int Year = 2016;
        public int Month = 1;
        public int Day = 1;
        public EnumBuySell Direction = EnumBuySell.买入;
        public string insId;
        public double LimitPrice = 0;
        public int Volume = 100;
        public string ExchangeID = "SH";

        public TestTradeCase(
            string _insId,
            EnumMarket _market,
            string _exchange,
            EnumBuySell _direction,
            double _limitPrice,
            int _volume,
            int _year,
            int _month,
            int _day)
        {
            insId = _insId;
            Market = _market;
            ExchangeID = _exchange;
            Direction = _direction;
            LimitPrice = _limitPrice;
            Volume = _volume;
            Year = _year;
            Month = _month;
            Day = _day;
        }


    }
}
