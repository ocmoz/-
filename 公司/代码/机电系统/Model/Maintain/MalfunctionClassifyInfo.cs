using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 故障等级
    /// </summary>
    public enum MalfunctionRank
    {
        [EnumDescription("未知类型")]
        Unknown=0,
        [EnumDescription("一级")]
        Common=1,
        [EnumDescription("二级")]
        Important=2,
        [EnumDescription("三级")]
        Urgent=3,
        [EnumDescription("其他")]
        Others=4
    }
    /// <summary>
    /// 故障类别实体类
    /// </summary>
    public class MalfunctionClassifyInfo
    {
        #region Model
        private long _id;
        private string _system;
        private string _systemName;
        private long _subsystem;
        private string _subsystemName;
        private string _malfunctionobject;
        private string _malfunctiondescription;
        private MalfunctionRank _rank;
        private int _responsetime;
        private TimeUnits _responseUnit;
        private int _funrestoretime;
        private TimeUnits _funrestoreUnit;
        private int _repairtime;
        private TimeUnits _repairUnit;
        #endregion Model
        /// <summary>
        /// 故障分类主键
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string System
        {
            set { _system = value; }
            get { return _system; }
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName
        {
            set { _systemName = value; }
            get { return _systemName; }
        }
        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystem
        {
            set { _subsystem = value; }
            get { return _subsystem; }
        }
        /// <summary>
        /// 子系统名称
        /// </summary>
        public string SubSystemName
        {
            set { _subsystemName = value; }
            get { return _subsystemName; }
        }
        /// <summary>
        /// 故障对象名称
        /// </summary>
        public string MalfunctionObject
        {
            set { _malfunctionobject = value; }
            get { return _malfunctionobject; }
        }
        /// <summary>
        /// 故障现象描述
        /// </summary>
        public string MalfunctionDescription
        {
            set { _malfunctiondescription = value; }
            get { return _malfunctiondescription; }
        }
        /// <summary>
        /// 故障等级
        /// </summary>
        public MalfunctionRank Rank
        {
            set { _rank = value; }
            get { return _rank; }
        }
        /// <summary>
        /// 响应时间
        /// </summary>
        public int ResponseTime
        {
            set { _responsetime = value; }
            get { return _responsetime; }
        }
        /// <summary>
        /// 响应时间单位
        /// </summary>
        public TimeUnits ResponseUnit
        {
            set { _responseUnit = value; }
            get { return _responseUnit; }
        }
        /// <summary>
        /// 响应时间分钟数表示
        /// </summary>
        public int ResponseMinutes
        {
            get
            {
                int result = 0;
                switch (_responseUnit)
                {
                    case TimeUnits.Minute:
                        result = _responsetime;
                        break;
                    case TimeUnits.Hour:
                        result = _responsetime * 60;
                        break;
                    case TimeUnits.Day:
                        result = _responsetime * 60 * 24;
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// 功能恢复时间
        /// </summary>
        public int FunRestoreTime
        {
            set { _funrestoretime = value; }
            get { return _funrestoretime; }
        }
        /// <summary>
        /// 功能恢复时间单位
        /// </summary>
        public TimeUnits FunRestoreUnit
        {
            set { _funrestoreUnit = value; }
            get { return _funrestoreUnit; }
        }
        /// <summary>
        /// 功能恢复时间分钟数表示
        /// </summary>
        public int FunRestoreTimeMinutes
        {
            get
            {
                int result = 0;
                switch (_funrestoreUnit)
                {
                    case TimeUnits.Minute:
                        result = _funrestoretime;
                        break;
                    case TimeUnits.Hour:
                        result = _funrestoretime*60;
                        break;
                    case TimeUnits.Day:
                        result = _funrestoretime * 60 * 24;
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// 修复时间
        /// </summary>
        public int RepairTime
        {
            set { _repairtime = value; }
            get { return _repairtime; }
        }
        /// <summary>
        /// 修复时间单位
        /// </summary>
        public TimeUnits RepairUnit
        {
            set { _repairUnit = value; }
            get { return _repairUnit; }
        }
        /// <summary>
        /// 修复时间分钟数表示
        /// </summary>
        public int RepairTimeMinutes
        {
            get
            {
                int result = 0;
                switch (_repairUnit)
                {
                    case TimeUnits.Minute:
                        result = _repairtime;
                        break;
                    case TimeUnits.Hour:
                        result = _repairtime * 60;
                        break;
                    case TimeUnits.Day:
                        result = _repairtime * 60 * 24;
                        break;
                }
                return result;
            }
        }
        
    }
}
