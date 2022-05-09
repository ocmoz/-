using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程施工计划项状态
    /// </summary>
    public enum SpecialProjectPlanStatus
    {
        NORMAL = 1,
        TERMINATED = 2
    }

    /// <summary>
    /// 专项工程施工计划项实体类
    /// </summary>
    public class SpecialProjectPlanInfo
    {
        public SpecialProjectPlanInfo()
		{}
		#region Model
		private long _projectid;
		private decimal _progress;
        private SpecialProjectPlanStatus _status;
        private long _itemid;
        private string _prfixitemname ="";
		private string _itemname ="";
		private long _prefixitemid;
		private DateTime _starttime= DateTime.MinValue;
		private DateTime _endtime= DateTime.MinValue;
		private int _days;
		private string _hrplan= "";
		private string _deviceplan= "";
        private int _daysafter;
		/// <summary>
		/// 专项工程ID
		/// </summary>
        public long ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 进度百分比
		/// </summary>
		public decimal Progress
		{
			set{ _progress=value;}
			get{return _progress;}
		}
		/// <summary>
		/// 状态
		/// </summary>
        public SpecialProjectPlanStatus Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 序号
		/// </summary>
        public long ItemID
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 项名称
		/// </summary>
		public string ItemName
		{
			set{ _itemname=value;}
			get{return _itemname;}
		}
		/// <summary>
		/// 前置项ID
		/// </summary>
		public long PrefixItemID
		{
			set{ _prefixitemid=value;}
			get{return _prefixitemid;}
		}

        /// <summary>
        /// 前置项名称 
        /// </summary>
        public string PrefixItemName
        {
            set
            {
                _prfixitemname = value;
            }
            get
            {
                if (_prefixitemid == 0)
                    return "无前置项";
                else 
                    return _prfixitemname;
            }
        }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 持续天数
		/// </summary>
		public int Days
		{
			set{ _days=value;}
			get{return _days;}
		}
		/// <summary>
		/// 人力资源安排
		/// </summary>
		public string HRPlan
		{
			set{ _hrplan=value;}
			get{return _hrplan;}
		}
		/// <summary>
		/// 设备安排
		/// </summary>
		public string DevicePlan
		{
			set{ _deviceplan=value;}
			get{return _deviceplan;}
		}
        /// <summary>
        /// 前一项结束后本项开始的天数
        /// </summary>
        public int DaysAfter
        {
            set { _daysafter = value; }
            get { return _daysafter; }
        }
		#endregion Model

        /// <summary>
        /// 状态字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                string str = "";
                switch(_status)
                {
                    case SpecialProjectPlanStatus.NORMAL:
                        str = "正常";
                        break;
                    case SpecialProjectPlanStatus.TERMINATED:
                        str = "终止";
                        break;
                    default :
                        break;
                }
                return str;
            }
        }

        private IList _progresscheckrecord = new List<SpecialProjectCheckRecordInfo>();

        /// <summary>
        /// 专项工程进度检查记录
        /// </summary>
        public IList ProgressCheckRecord
        {
            get { return _progresscheckrecord; }
            set { _progresscheckrecord = value; }
        }
    }
}
