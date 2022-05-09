using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程设备进场记录信息实体类
    /// </summary>
    public class SpecialProjectDeviceInfo
    {
        public SpecialProjectDeviceInfo()
		{}
		#region Model
		private long _projectid;
		private decimal _lastincount;
		private DateTime _time;
        private long _itemid;
        private string _devicename = "";
        private string _model = "";
        private string _size = "";
        private string _usage = "";
        private string _status = "";
		private decimal _plancount;
		private decimal _actualcount;
		/// <summary>
		/// 专项工程ID
		/// </summary>
        public long ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 上次进场数量
		/// </summary>
		public decimal LastInCount
		{
			set{ _lastincount=value;}
			get{return _lastincount;}
		}
		/// <summary>
		/// 上次进场时间
		/// </summary>
		public DateTime Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// 项序号
		/// </summary>
        public long ItemID
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 进场设备名称
		/// </summary>
		public string DeviceName
		{
			set{ _devicename=value;}
			get{return _devicename;}
		}
		/// <summary>
		/// 进场设备型号
		/// </summary>
		public string Model
		{
			set{ _model=value;}
			get{return _model;}
		}
		/// <summary>
		/// 进场设备规格尺寸
		/// </summary>
		public string Size
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// 设备用途
		/// </summary>
		public string Usage
		{
			set{ _usage=value;}
			get{return _usage;}
		}
		/// <summary>
		/// 设备状况
		/// </summary>
		public string Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 合同数量
		/// </summary>
		public decimal PlanCount
		{
			set{ _plancount=value;}
			get{return _plancount;}
		}
		/// <summary>
		/// 实际数量
		/// </summary>
		public decimal ActualCount
		{
			set{ _actualcount=value;}
			get{return _actualcount;}
		}
		#endregion Model

        private IList _deviceinrecord = new List<SpecialProjectDeviceInRecord>();

        /// <summary>
        /// 设备进场记录
        /// </summary>
        public IList DeviceInRecordList
        {
            get
            {
                return _deviceinrecord;
            }
            set
            {
                _deviceinrecord = value;
            }
        }
    }
}
