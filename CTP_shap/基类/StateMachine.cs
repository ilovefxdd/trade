using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP_交易.基类
{
    /// <summary>
    /// 策略状态机
    /// </summary>
    public class StateMachine
    {
        #region 状态机


        /// <summary>
        /// 策略实际标志位，当前实际持仓数量
        /// </summary>
        public int Flag = 0;

        /// <summary>
        /// 策略目标标志位，对应持仓数量
        /// </summary>
        public int TargetFlag = 0;

        /// <summary>
        /// 开仓目标数量(绝对值)
        /// </summary>
        public int KN = 0;

        /// <summary>
        /// 开仓成交数量(绝对值)
        /// </summary>
        public int KN_Trd = 0;

        /// <summary>
        /// 平仓目标数量(绝对值)
        /// </summary>
        public int PN = 0;

        /// <summary>
        /// 平仓成交数量(绝对值)
        /// </summary>
        public int PN_Trd = 0;

        /// <summary>
        /// 状态机是否正在转换状态
        /// </summary>
        public bool IsDoing = false;

        public bool IsOpenRefuse = false;

        /// <summary>
        /// 计数器
        /// </summary>
        public int Clc = 0;

        /// <summary>
        /// 旧的状态位
        /// 为了开仓资金不足设计
        /// </summary>
        public int OldFlag = 0;

        #endregion

        public StateMachine()
        {
        }

        public StateMachine(int 初始量)
        {
            Clc = 0;
            IsDoing = false;
            IsOpenRefuse = false;
            KN = 初始量;
            KN_Trd = 初始量;
            OldFlag = 初始量;
            PN = 0;
            PN_Trd = 0;
            TargetFlag = 初始量;
            Flag = 初始量;
        }

        public void Clear()
        {
            IsDoing = false;
            KN = 0;
            KN_Trd = 0;
            PN = 0;
            PN_Trd = 0;
            Flag = 0;
        }

        public override string ToString()
        {
            string str = "";
            str += "Flag=" + Flag;
            str += ",TargetFlag=" + TargetFlag;
            str += ",KN=" + KN;
            str += ",KN_Trd=" + KN_Trd;
            str += ",PN=" + PN;
            str += ",PN_Trd=" + PN_Trd;
            str += ",IsDoing=" + IsDoing;
            return str;
        }

    }
}
