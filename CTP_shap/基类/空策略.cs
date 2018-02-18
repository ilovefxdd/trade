using System;
using Ats.Core;
using Ats.Indicators;
namespace trade
{
    public class 空策略 : Strategy
    {
 
       [Parameter(Display = "委托数量", Description = "单位:张，股", Category = "下单")]
        public int 委托数量 = 1;
     
    }
}