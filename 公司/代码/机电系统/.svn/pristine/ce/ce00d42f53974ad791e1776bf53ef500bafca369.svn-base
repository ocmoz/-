using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{

    /// <summary>
    /// 报验单状态
    /// </summary>
    public enum CheckAcceptanceStatus
    {
        NONE
    }
    /// <summary>
    /// 采购报验单
    /// </summary>
    public class CheckAcceptanceInfo
    {
        public CheckAcceptanceInfo()
		{}
		#region Model
		private long _id;
        private string _sheetid = "";
        private string _sheetname = "";
        private string _companyid = "";
       

        private string _applicant = "";
        private DateTime _submittime = DateTime.MinValue;
        private CheckAcceptanceStatus _status;
        private string _remark = "";
        private DateTime _updatetime = DateTime.MinValue;
		/// <summary>
		/// 报验单数据库自增ID
		/// </summary>
        public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 表单编码
		/// </summary>
		public string SheetID
		{
			set{ _sheetid=value;}
			get{return _sheetid;}
		}
		/// <summary>
		/// 表单名称
		/// </summary>
		public string SheetName
		{
			set{ _sheetname=value;}
			get{return _sheetname;}
		}
		/// <summary>
		/// 公司ID
		/// </summary>
		public string CompanyID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 申请人ID
		/// </summary>
		public string Applicant
		{
			set{ _applicant=value;}
			get{return _applicant;}
		}

        private string _applicantname;
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName
        {
            set { _applicantname = value; }
            get { return _applicantname; }
        }

		/// <summary>
		/// 提交时间
		/// </summary>
		public DateTime SubmitTime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		/// <summary>
		/// 报验单状态
		/// </summary>
        public CheckAcceptanceStatus Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

        private string _companyname;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return _companyname; }
            set { _companyname = value; }
        }

        /// <summary>
        /// 状态字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                string str = "";
                switch (_status)
                {
                    case CheckAcceptanceStatus.NONE:
                        str = "未知";
                        break;
                    default:
                        break;

                }
                return str;

            }
        }


        private IList _detaillist = new List<CheckAcceptanceDetailInfo>();

        /// <summary>
        /// 详情列表
        /// </summary>
        public IList DetailList
        {
            set { _detaillist = value; }
            get { return _detaillist; }
        }

    }
}
