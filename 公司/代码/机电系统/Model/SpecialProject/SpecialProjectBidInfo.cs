using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程招标信息实体类
    /// </summary>
    public class SpecialProjectBidInfo
    {
        public SpecialProjectBidInfo()
		{}
		#region Model
		private long _projectid;
        private string _biddencompany = "";
        private string _biddencompanyinfo = "";
        private string _attechment = "";
        private string _approvaler = "";
		private int _result;
        private string _feeback = "";
        private DateTime _approvaldate = DateTime.MinValue;
		/// <summary>
		/// 专项工程ID
		/// </summary>
        public long ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 中标单位名称
		/// </summary>
		public string BiddenCompany
		{
			set{ _biddencompany=value;}
			get{return _biddencompany;}
		}
		/// <summary>
		/// 中标单位信息
		/// </summary>
		public string BiddenCompanyInfo
		{
			set{ _biddencompanyinfo=value;}
			get{return _biddencompanyinfo;}
		}
		/// <summary>
		/// 招标附件
		/// </summary>
		public string Attechment
		{
			set{ _attechment=value;}
			get{return _attechment;}
		}
		/// <summary>
		/// 审批者ID
		/// </summary>
		public string Approvaler
		{
			set{ _approvaler=value;}
			get{return _approvaler;}
		}
		/// <summary>
		/// 审批结果
		/// </summary>
		public int Result
		{
			set{ _result=value;}
			get{return _result;}
		}
		/// <summary>
		/// 反馈意见
		/// </summary>
		public string FeeBack
		{
			set{ _feeback=value;}
			get{return _feeback;}
		}
		/// <summary>
		/// 审批时间
		/// </summary>
		public DateTime ApprovalDate
		{
			set{ _approvaldate=value;}
			get{return _approvaldate;}
		}
		#endregion Model
    }
}
