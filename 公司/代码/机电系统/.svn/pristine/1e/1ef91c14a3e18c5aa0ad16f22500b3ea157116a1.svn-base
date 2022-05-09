using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 维修状态
    /// </summary>
    public enum MaintainedEquipmentStatus
    {
        [EnumDescription("未知状态")]
        Unknown = 0,
        [EnumDescription("已修复")]
        Fixed = 1,
        [EnumDescription("功能性恢复")]
        FunctionalityRestore = 2,
        [EnumDescription("无法修复")]
        UnFixed = 3,
        [EnumDescription("替换")]
        Replace = 4,
        [EnumDescription("故障待修")]
        Wait4Repair = 5,
        [EnumDescription("已报废")]
        Scrapped = 6,
    }
    [Serializable]
    public class MaintainedEquipmentInfo
    {
        #region Model
        private long _id;
        private long _maintainid;
        private long _sheetid;
        private string _equipmentno;
        private string _equipmentname;
        private string _model;
        private int _maintainTimes;
        private MaintainedEquipmentStatus _maintainresult;
        private decimal _maintainfee;
        private string _remark;
        private DateTime _maintaindate;
        private IList maintainedEquipmentParts;
        private string _serialnum;
        private string _lastaddress;
        public int RepairTime { set; get; }

        public int RepairUnit { set; get; }

        public int ActualRepairTime { set; get; }

        public DateTime ReportDate { set; get; }

        public string ActualRepairedTimestr { set; get; }

        public long MainteamID { set; get; }

        public string AddressCode { set; get; }
        /// <summary>
        ///设备类型：新件 二手件
        /// </summary>
        public string SerialNum
        {
            set { _serialnum = value; }
            get { return _serialnum; }
        }
        /// <summary>
        ///设备上一地址
        /// </summary>
        public string LastAddress
        {
            set { _lastaddress = value; }
            get { return _lastaddress; }
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 维修情况ID
        /// </summary>
        public long MaintainID
        {
            set { _maintainid = value; }
            get { return _maintainid; }
        }
        /// <summary>
        /// 故障处理单ID
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 维修的故障设备条形码
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        /// <summary>
        /// 维修的设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 维修次数
        /// </summary>
        public int MaintainTimes
        {
            set { _maintainTimes = value; }
            get { return _maintainTimes; }
        }
        /// <summary>
        /// 维修备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 维修状态
        /// </summary>
        public MaintainedEquipmentStatus MaintainResult
        {
            set { _maintainresult = value; }
            get { return _maintainresult; }
        }
        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime MaintainDate
        {
            set { _maintaindate = value; }
            get { return _maintaindate; }
        }
        /// <summary>
        /// 维修费用
        /// </summary>
        public decimal MaintainFee
        {
            set { _maintainfee = value; }
            get { return _maintainfee; }
        }
        /// <summary>
        /// 维修零件列表
        /// </summary>
        public IList MaintainedEquipmentParts
        {
            set { maintainedEquipmentParts = value; }
            get { return maintainedEquipmentParts; }
        }
        #endregion Model
    }
}
