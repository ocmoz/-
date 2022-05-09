using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 设备维修记录查询用的实体类
    /// </summary>
    public class EquipmentMaintainRecordSearchInfo
    {
        /// <summary>
        /// 故障处理单编号
        /// </summary>
        public string SheetNO { get; set; }
        /// <summary>
        /// 故障部门ID
        /// </summary>
        public long DepartmentID { get; set; }
        /// <summary>
        /// 故障设备名称
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// 故障设备条形码
        /// </summary>
        public string EquipmentNO { get; set; }
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public string SystemID { get; set; }
        /// <summary>
        /// 故障等级
        /// </summary>
        public int MalfunctionRank { get; set; }
        /// <summary>
        /// 维修队ID
        /// </summary>
        public long MaintainTeam { get; set; }
        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime MaintainDateFrom { get; set; }
        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime MaintainDateTo { get; set; }
    }
}
