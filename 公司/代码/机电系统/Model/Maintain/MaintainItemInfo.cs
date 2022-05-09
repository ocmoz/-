using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Maintain
{

    /// <summary>
    /// 维护周期单位
    /// </summary>
    public enum MaintainIntervalUnit
    {
        [EnumDescription("未知单位")]
        Unknown = 0,
        [EnumDescription("天")]
        Day = 1,
        [EnumDescription("周")]
        Week = 2,
        [EnumDescription("月")]
        Month = 4,
        [EnumDescription("季")]
        Season = 8,
        [EnumDescription("年")]
        Year = 16
    }

    /// <summary>
    /// 维护类型，包括日常巡查、例行保养以及例行检测
    /// </summary>
    public enum MaintainType
    {
        [EnumDescription("未知类型")]
        Unknown = 0,
        [EnumDescription("日常巡查")]
        DailyPatrol = 1,
        [EnumDescription("例行保养")]
        RoutineMaintain = 2,
        [EnumDescription("例行检测")]
        RoutineInspect = 4
    }
    /// <summary>
    /// 维护项实体类
    /// </summary>
    public class MaintainItemInfo
    {
        public MaintainItemInfo()
		{}
		#region Model
		private long _id;
        private MaintainType _maintaintype;
		private string _systemid;
		private long _subsystemid;
		private int _period;
        private MaintainIntervalUnit _periodunit;
		private string _object;
		private string _content;
		private string _standard;
		/// <summary>
		/// 主键
		/// </summary>
		public long ID
		{
            set { _id = value; }
            get { return _id; }
		}
		/// <summary>
		/// 维护类型
		/// </summary>
        public MaintainType MaintainType
		{
			set{ _maintaintype=value;}
			get{return _maintaintype;}
		}
		/// <summary>
		/// 系统ID
		/// </summary>
		public string SystemID
		{
			set{ _systemid=value;}
			get{return _systemid;}
		}
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
		/// <summary>
		/// 子系统ID
		/// </summary>
		public long SubSystemID
		{
			set{ _subsystemid=value;}
			get{return _subsystemid;}
		}
        /// <summary>
        /// 子系统名称
        /// </summary>
        public string SubSystemName { get; set; }
		/// <summary>
		/// 维护周期
		/// </summary>
		public int Period
		{
			set{ _period=value;}
			get{return _period;}
		}
		/// <summary>
		/// 维护周期单位
		/// </summary>
        public MaintainIntervalUnit PeriodUnit
		{
			set{ _periodunit=value;}
			get{return _periodunit;}
		}
        /// <summary>
        /// 执行周期（以天为单位）
        /// </summary>
        public int PeriodInDays
        {
            get
            {
                int days=0;

                switch(_periodunit)
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
		/// 维护对象
		/// </summary>
		public string Object
		{
			set{ _object=value;}
			get{return _object;}
		}
		/// <summary>
		/// 保养内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 验收标准
		/// </summary>
		public string Standard
		{
			set{ _standard=value;}
			get{return _standard;}
		}
		#endregion Model
    }

    /// <summary>
    /// 维护项目查询实体类
    /// </summary>
    [Serializable]
    public class MaintainItemSearchInfo
    {
        /// <summary>
        /// 维护类型s
        /// </summary>
        public MaintainType MaintainType { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }
        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystemID { get; set; }
        /// <summary>
        /// 维护对象
        /// </summary>
        public string Object { get; set; }
        /// <summary>
        /// 维护周期
        /// </summary>
        public MaintainIntervalUnit PeriodUnit { get; set; }
    }
}
