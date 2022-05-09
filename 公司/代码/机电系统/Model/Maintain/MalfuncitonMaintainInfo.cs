using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using FM2E.Model.Utils;
using System.IO;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 修复情况
    /// </summary>
    public enum RepairSituation
    {
        [EnumDescription("未知修复情况")]
        Unknown=0,
        [EnumDescription("功能性修复")]
        FunctionalityFixed=2,
        [EnumDescription("完全修复")]
        CompletelyFixed=4,
        [EnumDescription("阶段性进展")]
        ProcessingStage=8,
        [EnumDescription("转二级维修")]
        SecondRepaired = 16,
        
    }
    [Serializable]
    /// <summary>
    /// 故障处理单维修信息实体类
    /// </summary>
    public class MalfuncitonMaintainInfo
    {
        private long _maintainid;
        private long _sheetid;
        private string _maintenancestaff;
        private string _maintenancestaffname;
        private string _maintenanceteam;
        private string _maintenancedetail;
        private string _maintenancedescription;
        private string _maintenancemethod;
        private RepairSituation _repairsituation;
        private decimal _totalfee;
        private DateTime _updatetime;
        private IList maintainStaff;
        private IList maintainedEquipments;
        private bool _isDelivered;
        private string _noqeuipment;



        /// <summary>
        /// 维修人员用户名
        /// </summary>
        public string NoEquipment
        {
            set { _noqeuipment = value; }
            get { return _noqeuipment; }
        }
        /// <summary>
        /// 维修号（主键）
        /// </summary>
        public long MaintainID
        {
            set { _maintainid = value; }
            get { return _maintainid; }
        }
        /// <summary>
        /// 故障处理单ID号
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 维修人员用户名
        /// </summary>
        public string MaintenanceStaff
        {
            set { _maintenancestaff = value; }
            get { return _maintenancestaff; }
        }
        /// <summary>
        /// 维修详情记录
        /// </summary>
        public string MaintenanceDetail
        {
            set { _maintenancedetail = value; }
            get { return _maintenancedetail; }
        }
        /// <summary>
        /// 维修故障详细描述
        /// </summary>
        public string MaintenanceDescription
        {
            set { _maintenancedescription = value; }
            get { return _maintenancedescription; }
        }
        /// <summary>
        /// 维修办法描述
        /// </summary>
        public string MaintenanceMethod
        {
            set { _maintenancemethod = value; }
            get { return _maintenancemethod; }
        }
        /// <summary>
        /// 修复情况
        /// </summary>
        public RepairSituation RepairSituation
        {
            set { _repairsituation = value; }
            get { return _repairsituation; }
        }
        /// <summary>
        /// 维修总费用
        /// </summary>
        public decimal TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 维修人员姓名
        /// </summary>
        public string MaintenanceStaffName
        {
            set { _maintenancestaffname = value; }
            get { return _maintenancestaffname; }
        }
        /// <summary>
        /// 维修队名称
        /// </summary>
        public string MaintenanceTeam
        {
            set { _maintenanceteam = value; }
            get { return _maintenanceteam; }
        }
        /// <summary>
        /// 维修人员列表
        /// </summary>
        public IList MaintainStaff
        {
            set { maintainStaff = value; }
            get { return maintainStaff; }
        }
        /// <summary>
        /// 已维修的设备列表
        /// </summary>
        public IList MaintainedEquipments
        {
            set { maintainedEquipments = value; }
            get { return maintainedEquipments; }
        }
        /// <summary>
        /// 是否送修
        /// </summary>
        public bool IsDelivered
        {
            set { _isDelivered = value; }
            get { return _isDelivered; }
        }
        /// <summary>
        /// 返回一个内存副本(深层副本)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MalfuncitonMaintainInfo CloneObject(MalfuncitonMaintainInfo obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                return (MalfuncitonMaintainInfo)formatter.Deserialize(stream);
            }
        }   

    }
}
