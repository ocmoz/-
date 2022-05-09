using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    public enum DailyPatrolPeriodUnit1
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
    public class DailyPatrolDetailInfo
    {
        #region Model
        private long _planid;
        private long _itemid;
        private int _patrolperiod;
        private DailyPatrolPeriodUnit _periodunit;
        private string _system;
        private long _subsystem;
        private string _systemname;
        private string _subsystemname;
        private string _patrolobject;
        private string _patrolcontent;
        private string _checkstandard;
        /// <summary>
        /// 
        /// </summary>
        public long PlanID
        {
            set { _planid = value; }
            get { return _planid; }
        }
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
        public int PatrolPeriod
        {
            set { _patrolperiod = value; }
            get { return _patrolperiod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DailyPatrolPeriodUnit PeriodUnit
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
        /// <summary>
        /// 
        /// </summary>
        public string PatrolObject
        {
            set { _patrolobject = value; }
            get { return _patrolobject; }
        }
        /// <summary>
        /// 用于在列表中显示
        /// </summary>
        public string PatrolObjectString
        {
            get
            {
                if (_patrolobject.Length > 10)
                    return _patrolobject.Substring(0, 10) + "...";
                else
                    return _patrolobject;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PatrolContent
        {
            set { _patrolcontent = value; }
            get { return _patrolcontent; }
        }
        /// <summary>
        /// 用于在列表中显示
        /// </summary>
        public string PatrolContentString
        {
            get
            {
                if (_patrolcontent.Length > 10)
                    return _patrolcontent.Substring(0, 10) + "...";
                else
                    return _patrolcontent;
            }
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
        /// 用于在列表中显示
        /// </summary>
        public string CheckStandardString
        {
            get
            {
                if (_checkstandard.Length > 10)
                    return _checkstandard.Substring(0, 10) + "...";
                else
                    return _checkstandard;
            }
        }
        #endregion Model
        /// <summary>
        /// 巡查周期字符串
        /// </summary>
        public string PatrolPeriodString
        {
            get 
            {
                string periodString = _patrolperiod.ToString();
                switch (_periodunit)
                {
                    case DailyPatrolPeriodUnit.Hour:
                        periodString += "小时";
                        break;
                    case DailyPatrolPeriodUnit.Day:
                        periodString += "天";
                        break;
                    case DailyPatrolPeriodUnit.Week:
                        periodString += "周";
                        break;
                    case DailyPatrolPeriodUnit.Month:
                        periodString += "月";
                        break;
                    case DailyPatrolPeriodUnit.Season:
                        periodString += "季度";
                        break;
                    case DailyPatrolPeriodUnit.Year:
                        periodString += "年";
                        break;
                    default:
                        periodString = "未知";
                        break;
                }
                return periodString;
            }
        }
    }
}
