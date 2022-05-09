using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 维护模板表实体类
    /// </summary>
    public class TemplateMaintainSheetInfo
    {
        #region Model
        private long _templatesheetid;
        private string _remark = "";
        private string _modifier;
        private bool _isnotused;
        private DateTime _savetime;
        private string _templatesheetname = "";
        private long _departmentid;
        private long _addressid;
        private string _systemid;
        private MaintainType _maintaintype;
        private int _period;
        private MaintainIntervalUnit _periodunit;
        private bool _istemp;
        /// <summary>
        /// 维护模板表ID
        /// </summary>
        public long TemplateSheetID
        {
            set { _templatesheetid = value; }
            get { return _templatesheetid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 制表人
        /// </summary>
        public string Modifier
        {
            set { _modifier = value; }
            get { return _modifier; }
        }
        /// <summary>
        /// 制表人姓名(from view)
        /// </summary>
        public string ModifierName { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsNotUsed
        {
            set { _isnotused = value; }
            get { return _isnotused; }
        }
        /// <summary>
        /// 制表时间
        /// </summary>
        public DateTime SaveTime
        {
            set { _savetime = value; }
            get { return _savetime; }
        }
        /// <summary>
        /// 模板表名称
        /// </summary>
        public string TemplateSheetName
        {
            set { _templatesheetname = value; }
            get { return _templatesheetname; }
        }
        /// <summary>
        /// 制表部门ID
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 制表人部门(from view)
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 维护地址
        /// </summary>
        public long AddressID
        {
            set { _addressid = value; }
            get { return _addressid; }
        }
        /// <summary>
        /// 维护地址的编码(from view)
        /// </summary>
        public string AddressCode { get; set; }
        /// <summary>
        /// 维护地址的名称(from view)
        /// </summary>
        public string AddressName { get; set; }
        /// <summary>
        /// 维护系统
        /// </summary>
        public string SystemID
        {
            set { _systemid = value; }
            get { return _systemid; }
        }
        /// <summary>
        /// 维护系统名称(from view)
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 维护类型
        /// </summary>
        public MaintainType MaintainType
        {
            set { _maintaintype = value; }
            get { return _maintaintype; }
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
        /// 是否临时计划
        /// </summary>
        public bool IsTemp
        {
            set { _istemp = value; }
            get { return _istemp; }
        }
        #endregion Model
        /// <summary>
        /// 上一次执行时间
        /// </summary>
        public DateTime LastExecuteTime { get; set; }
        
        /// <summary>
        /// 需要进行维护的设备列表
        /// </summary>
        public IList Equipments { get; set; }

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

                return (int)(DateTime.Now - last).TotalDays - PeriodInDays;
            
            }
        }
    }
    /// <summary>
    /// 维护模块表查询实体类
    /// </summary>
    [Serializable]
    public class TemplateMaintainSheetSearchInfo
    {
        /// <summary>
        /// 制表人姓名
        /// </summary>
        public string ModifierName { get; set; }
        /// <summary>
        /// 制表人用户名
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool? IsNotUsed { get; set; }
        /// <summary>
        /// 制表时间
        /// </summary>
        public DateTime SaveTimeFrom { get; set; }
        /// <summary>
        /// 制表时间
        /// </summary>
        public DateTime SaveTimeTo { get; set; }
        /// <summary>
        /// 模板表名
        /// </summary>
        public string TemplateSheetName { get; set; }
        /// <summary>
        /// 制表部门
        /// </summary>
        public long Department { get; set; }
        /// <summary>
        /// 地址ID
        /// </summary>
        public long AddressID { get; set; }
        /// <summary>
        /// 地址编码
        /// </summary>
        public string AddressCode { get; set; }
        /// <summary>
        /// 地址名
        /// </summary>
        public string AddressName { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }
        /// <summary>
        /// 维护类型
        /// </summary>
        public MaintainType MaintainType { get; set; }
        /// <summary>
        /// 是否临时计划
        /// </summary>
        public bool? IsTemp { get; set; }
        /// <summary>
        /// 周期单位
        /// </summary>
        public MaintainIntervalUnit PeriodUnit { get; set; }
    }
}
