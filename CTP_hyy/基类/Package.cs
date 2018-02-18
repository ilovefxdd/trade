using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Core;

namespace CTP_交易.基类
{
    public class Package
    {

        /// <summary>
        /// 下单手数
        /// 默认1
        /// </summary>
        public int Qty = 1;

        /// <summary>
        /// 标志位
        /// </summary>
        public int Flag = 0;

        /// <summary>
        /// 极小值
        /// </summary>
        public double MinDouble = 0.000001;

        public double PriceTick = 0.2;
         

        public DateTime PMBegin;
        
        public DateTime AMEnd;

        public DateTime 上次操作时间;

        public int mytflag = 0;

        public double 入场价格 = 0;

        public Tick preTick;

        public double GapPoint = -1;
         
        public double maxp = 0;

        public DateTime maxt;

        public double minp = 100000000;

        public DateTime mint;

        /// <summary>
        /// 计数器
        /// </summary>
        public int clc = 0;


        public double Abs(double x)
        {
            return Math.Abs(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <param name="time"></param>
        /// <returns>True需要避开 False不需要</returns>
        public bool 避开涨跌停(string instrumentId, DateTime time)
        {
            //ru 涨跌停不交易
            #region 避开涨跌停
            int Year = time.Year;
            int Month = time.Month;
            int Day = time.Day;

            #region ru
            
            if (instrumentId.IndexOf("ru") > -1)
            {
                if ( Year == 2011)
                {
                    if ( Month == 11 )
                    {
                        if (Day == 10 || Day == 14)
                        {
                            return true;
                        } 
                    } 
                }
                if ( Year == 2014)
                {
                    if ( Month == 5  )
                    {
                        if( Day==19)
                        {
                            return true;
                        } 
                    }
                }
            }
            #endregion

            #endregion

            return false;
        }

       
    }
}
