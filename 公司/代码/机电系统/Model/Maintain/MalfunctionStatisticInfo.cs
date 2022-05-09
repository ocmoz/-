using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 故障恢复程序，统计时使用
    /// </summary>
    public enum MalfunctionRestoreType
    {
        [EnumDescription("未知状态")]
        Unknown=0,
        [EnumDescription("功能性恢复")]
        FunctionalityRestore=1,
        [EnumDescription("已恢复")]
        Fixed=2,
        [EnumDescription("未修复")]
        UnFixed=3
    }
    /// <summary>
    /// 故障报修统计实体类
    /// </summary>
    public class MalfunctionStatisticInfo
    {
        /// <summary>
        /// 故障等级
        /// </summary>
        public MalfunctionRank MalfunctionRank { get; set; }
        /// <summary>
        /// 故障修复状态
        /// </summary>
        public MalfunctionHandleStatus Status { get; set; }
        /// <summary>
        /// 故障处理部门
        /// </summary>
        public long MaintainDept { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 占总故障数量的比例
        /// </summary>
        public float Percent { get; set; }

        public DateTime ReportDate { get; set; }

        public int RepairTime { get; set; }

        public int RepairUnit { get; set; }

        public int ActualRepairTime { get; set; }
    }
}
