using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    public enum MaintainPlanPeriodUnit
    {
        /// <summary>
        /// 小时
        /// </summary>
        Hour = 0,
        /// <summary>
        /// 天
        /// </summary>
        Day = 1,
        /// <summary>
        /// 周
        /// </summary>
        Week = 2,
        /// <summary>
        /// 月
        /// </summary>
        Month = 3,
        /// <summary>
        /// 季度
        /// </summary>
        Season = 4,
        /// <summary>
        /// 年
        /// </summary>
        Year = 5

    }
    public enum MaintainPlanType
    {
        /// <summary>
        /// 日常巡查
        /// </summary>
        DailyPatrol = 0,
        /// <summary>
        /// 例行保养
        /// </summary>
        RoutineMaintain = 1,
        /// <summary>
        /// 例行检测
        /// </summary>
        RoutineInspect =2
    }
    public class MaintainPlanConfigInfo
    {
        #region Model
        private long _itemid;
        private int _planperiod;
        private MaintainPlanPeriodUnit _periodunit;
        private string _system;
        private long _subsystem;
        private string _planobject;
        private string _plancontent;
        private string _checkstandard;
        private string _companyid;
        private string _companyname;
        private MaintainPlanType _plantype;
        /// <summary>
        /// 
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PlanPeriod
        {
            set { _planperiod = value; }
            get { return _planperiod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public MaintainPlanPeriodUnit PeriodUnit
        {
            set { _periodunit = value; }
            get { return _periodunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string System
        {
            set { _system = value; }
            get { return _system; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long Subsystem
        {
            set { _subsystem = value; }
            get { return _subsystem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PlanObject
        {
            set { _planobject = value; }
            get { return _planobject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PlanContent
        {
            set { _plancontent = value; }
            get { return _plancontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckStandard
        {
            set { _checkstandard = value; }
            get { return _checkstandard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public MaintainPlanType PlanType
        {
            set { _plantype = value; }
            get { return _plantype; }
        }
        #endregion Model
        /// <summary>
        /// 
        /// </summary>
        private string _systemname;
        private string _subsystemname;
        public string SystemName
        {
            set { _systemname = value; }
            get { return _systemname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubsystemName
        {
            set { _subsystemname = value; }
            get { return _subsystemname; }
        }
        private IList _equipmentList;
        public IList EquipmentList
        {
            set { _equipmentList = value; }
            get { return _equipmentList; }
        }
        /// <summary>
        /// 巡查周期字符串
        /// </summary>
        public string PlanPeriodString
        {
            get
            {
                string periodString = _planperiod.ToString();
                switch (_periodunit)
                {
                    case MaintainPlanPeriodUnit.Hour:
                        periodString += "小时";
                        break;
                    case MaintainPlanPeriodUnit.Day:
                        periodString += "天";
                        break;
                    case MaintainPlanPeriodUnit.Week:
                        periodString += "周";
                        break;
                    case MaintainPlanPeriodUnit.Month:
                        periodString += "月";
                        break;
                    case MaintainPlanPeriodUnit.Season:
                        periodString += "季度";
                        break;
                    case MaintainPlanPeriodUnit.Year:
                        periodString += "年";
                        break;
                    default:
                        periodString = "未知";
                        break;
                }
                return periodString;
            }
        }
        /// <summary>
        /// 计划类型字符串
        /// </summary>
        public string PlanTypeString
        {
            get
            {
                string typestring = "";
                switch (_plantype)
                {
                    case MaintainPlanType.DailyPatrol:
                        typestring = "日常巡查";
                        break;
                    case MaintainPlanType.RoutineInspect:
                        typestring = "例行检测";
                        break;
                    case MaintainPlanType.RoutineMaintain:
                        typestring = "例行保养";
                        break;
                    default:
                        typestring = "未知";
                        break;
                }
                return typestring;
            }
        }
    }
}
