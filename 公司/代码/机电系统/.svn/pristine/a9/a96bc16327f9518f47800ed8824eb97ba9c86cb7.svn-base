using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 维护记录核实结果
    /// </summary>
    public enum MaintainConfirmResult
    {
        [EnumDescription("未知")]
        Unknown = 0,
        [EnumDescription("未核实")]
        NotConfirm = 1,
        [EnumDescription("未执行")]
        NotExecute = 2,
        [EnumDescription("延迟完成")]
        NotOnTime = 4,
        [EnumDescription("按时完成")]
        OnTime = 8,
    }
    /// <summary>
    /// 维护表实体类
    /// </summary>
    public class MaintainSheetInfo
    {
        #region Model
        private long _sheetid;
        private string _remark="";
        private MaintainConfirmResult _confirmresult;
        private string _confirmer="";
        private string _confirmremark="";
        private DateTime _confirmtime;
        private long _templatesheetid;
        private string _sheetno="";
        private string _sheetname="";
        private long _departmentid;
        private string _maintainer="";
        private DateTime _maintaintime;
        private MaintainType _maintaintype;
        private string _result="";
        private int _period;
        private MaintainIntervalUnit _periodunit;
        /// <summary>
        /// 维护表ID
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 维护备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 核实结果
        /// </summary>
        public MaintainConfirmResult ConfirmResult
        {
            set { _confirmresult = value; }
            get { return _confirmresult; }
        }
        /// <summary>
        /// 核实人用户名
        /// </summary>
        public string Confirmer
        {
            set { _confirmer = value; }
            get { return _confirmer; }
        }
        /// <summary>
        /// 核实人姓名
        /// </summary>
        public string ConfirmerName { get; set; }
        /// <summary>
        /// 核实备注
        /// </summary>
        public string ConfirmRemark
        {
            set { _confirmremark = value; }
            get { return _confirmremark; }
        }
        /// <summary>
        /// 核实时间
        /// </summary>
        public DateTime ConfirmTime
        {
            set { _confirmtime = value; }
            get { return _confirmtime; }
        }
        /// <summary>
        /// 维护模板表ID
        /// </summary>
        public long TemplateSheetID
        {
            set { _templatesheetid = value; }
            get { return _templatesheetid; }
        }
        /// <summary>
        /// 维护地址
        /// </summary>
        public long AddressID { get; set; }
        /// <summary>
        /// 维护地址的编码(from view)
        /// </summary>
        public string AddressCode { get; set; }
        /// <summary>
        /// 维护地址的名称(from view)
        /// </summary>
        public string AddressName { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }
        /// <summary>
        /// 系统名称(from view)
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 维护表表单编号
        /// </summary>
        public string SheetNO
        {
            set { _sheetno = value; }
            get { return _sheetno; }
        }
        /// <summary>
        /// 维护表名
        /// </summary>
        public string SheetName
        {
            set { _sheetname = value; }
            get { return _sheetname; }
        }
        /// <summary>
        /// 维护部门ID
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 维护部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 维护人
        /// </summary>
        public string Maintainer
        {
            set { _maintainer = value; }
            get { return _maintainer; }
        }
        /// <summary>
        /// 维护人姓名
        /// </summary>
        public string MaintainerName { get; set; }
        /// <summary>
        /// 维护时间
        /// </summary>
        public DateTime MaintainTime
        {
            set { _maintaintime = value; }
            get { return _maintaintime; }
        }
        /// <summary>
        /// 维护类型
        /// </summary>
        public MaintainType MaintainType
        {
            set { _maintaintype = value; }
            get { return _maintaintype; }
        }
        /// <summary>
        /// 维护结果
        /// </summary>
        public string Result
        {
            set { _result = value; }
            get { return _result; }
        }

        /// <summary>
        /// 执行周期
        /// </summary>
        public int Period
        {
            set { _period = value; }
            get { return _period; }
        }
        /// <summary>
        /// 执行周期时间单位
        /// </summary>
        public MaintainIntervalUnit PeriodUnit
        {
            set { _periodunit = value; }
            get { return _periodunit; }
        }
        /// <summary>
        /// 执行周期（以天为单位）
        /// </summary>
        public int PeriodInDays
        {
            get
            {
                int days = 0;

                switch (_periodunit)
                {
                    case MaintainIntervalUnit.Day:
                        days = _period;
                        break;
                    case MaintainIntervalUnit.Month:
                        days = _period * 30;
                        break;
                    case MaintainIntervalUnit.Season:
                        days = _period * 30 * 3;
                        break;
                    case MaintainIntervalUnit.Week:
                        days = _period * 7;
                        break;
                    case MaintainIntervalUnit.Year:
                        break;
                    default:
                        days = _period * 365;
                        break;
                }

                return days;
            }
        }
        /// <summary>
        /// 是否临时执行
        /// </summary>
        public bool IsTemp { get; set; }

        /// <summary>
        /// 上一次执行时间
        /// </summary>
        public DateTime LastExecuteTime { get; set; }

        #endregion Model
        /// <summary>
        /// 是否包含异常设备
        /// </summary>
        public bool HasAbnormal { get; set; }

        /// <summary>
        /// 经过维护的设备列表
        /// </summary>
        public IList Equipments { get; set; }
        /// <summary>
        /// 异常设备列表
        /// </summary>
        public IList AbnormalEquipments
        {
            get
            {
                ArrayList list = new ArrayList();
                if (this.Equipments != null && this.Equipments.Count > 0)
                {
                    foreach (MaintainSheetEquipmentInfo item in this.Equipments)
                    {
                        if (!item.IsNormal)
                        {
                            list.Add(item);
                        }
                    }
                }
                return list;
            }
        }


        /// <summary>
        /// 超时天数
        /// </summary>
        public int ExpiredDays
        {
            get
            {
                DateTime last = SaveTime;
                if (LastExecuteTime != DateTime.MinValue)
                {
                    last = LastExecuteTime;
                }

                return (int)(MaintainTime - last).TotalDays - PeriodInDays;

            }
        }

        /// <summary>
        /// 计划保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }
    }
    /// <summary>
    /// 维护表查询实体类
    /// </summary>
    [Serializable]
    public class MaintainSheetSearchInfo
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 地址ID
        /// </summary>
        public long AddressID { get; set; }
        /// <summary>
        /// 地址名称
        /// </summary>
        public string AddressName { get; set; }
        /// <summary>
        /// 地址编码
        /// </summary>
        public string AddressCode { get; set; }
        /// <summary>
        /// 核实结果
        /// </summary>
        public MaintainConfirmResult ConfirmResult { get; set; }
        /// <summary>
        /// 核实人用户名
        /// </summary>
        public string Confirmer { get; set; }
        /// <summary>
        /// 核实人姓名
        /// </summary>
        public string ConfirmerName { get; set; }
        /// <summary>
        /// 核实时间
        /// </summary>
        public DateTime ConfirmTimeFrom { get; set; }
        /// <summary>
        /// 核实时间
        /// </summary>
        public DateTime ConfirmTimeTo { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string SheetName { get; set; }
        /// <summary>
        /// 表单编号
        /// </summary>
        public string SheetNO { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DepartmentID { get; set; }
        /// <summary>
        /// 维护人用户名
        /// </summary>
        public string Maintainer { get; set; }
        /// <summary>
        /// 维护人姓名
        /// </summary>
        public string MaintainerName { get; set; }
        /// <summary>
        /// 维护时间
        /// </summary>
        public DateTime MaintainTimeFrom { get; set; }
        /// <summary>
        /// 维护时间
        /// </summary>
        public DateTime MaintainTimeTo { get; set; }
        /// <summary>
        /// 维护类型
        /// </summary>
        public MaintainType MaintainType { get; set; }
        /// <summary>
        /// 是否包含异常设备
        /// </summary>
        public bool? HasAbnormal { get; set; }
        /// <summary>
        /// 维护周期
        /// </summary>
        public MaintainIntervalUnit PeriodUnit { get; set; }
        /// <summary>
        /// 是否临时计划
        /// </summary>
        public bool? IsTemp { get; set; }
    }
}
