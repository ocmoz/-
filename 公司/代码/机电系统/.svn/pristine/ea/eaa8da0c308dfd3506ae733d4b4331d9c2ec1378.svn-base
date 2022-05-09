using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程审查
    /// </summary>
    public enum SpecialProjectApprovalResult
    {
        /// <summary>
        /// 未审查
        /// </summary>
        NONE,
        /// <summary>
        /// 通过
        /// </summary>
        PASS,
        /// <summary>
        /// 不通过
        /// </summary>
        FAILED
    }
    /// <summary>
    /// 专项工程审批记录
    /// </summary>
    public class SpecialProjectApprovalInfo
    {
        

        public SpecialProjectApprovalInfo()
		{}
		#region Model
        private long _projectid;
		private long _itemid;
		private string _approvalname;
		private string _approvaler;
        private SpecialProjectApprovalResult _result;
		private string _feeback;
        private string _approvalfile = "";
		private DateTime _approvaldate;

        /// <summary>
        /// 审批附件
        /// </summary>
        public string ApprovalFile
        {
            set { _approvalfile = value; }
            get { return _approvalfile; }
        }
		/// <summary>
		/// 专项工程ID
		/// </summary>
		public long ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 审批序号
		/// </summary>
        public long ItemID
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 审批项内容名称
		/// </summary>
		public string ApprovalName
		{
			set{ _approvalname=value;}
			get{return _approvalname;}
		}
		/// <summary>
		/// 审批者账号
		/// </summary>
		public string Approvaler
		{
			set{ _approvaler=value;}
			get{return _approvaler;}
		}
		/// <summary>
		/// 审批结果
		/// </summary>
        public SpecialProjectApprovalResult Result
		{
			set{ _result=value;}
			get{return _result;}
		}
        /// <summary>
        /// 审批结果字符串
        /// </summary>
        public string ResultString
        {
            get
            {
                string str = "";

                switch (_result)
                {
                    case SpecialProjectApprovalResult.PASS:
                        str = "通过";
                        break;
                    case SpecialProjectApprovalResult.NONE:
                        str = "未审批";
                        break;
                    case SpecialProjectApprovalResult.FAILED:
                        str = "不通过";
                        break;
                    default:
                        break;
                }
                return str;
            }
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
