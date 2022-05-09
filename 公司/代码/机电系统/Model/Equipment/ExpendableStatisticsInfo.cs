using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 易耗品统计实体类
    /// </summary>
    public class ExpendableStatisticsInfo
    {
        /// <summary>
        /// 入库数量
        /// </summary>
        public int InCount { get; set; }
        /// <summary>
        /// 入库金额
        /// </summary>
        public decimal InFee { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public int OutCount { get; set; }
        /// <summary>
        /// 出库金额
        /// </summary>
        public decimal OutFee { get; set; }
    }
}
