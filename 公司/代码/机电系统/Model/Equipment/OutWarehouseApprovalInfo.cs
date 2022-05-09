using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 出库申请审批信息实体类
    /// </summary>
    public class OutWarehouseApprovalInfo
    {
        public OutWarehouseApprovalInfo()
        { }
		#region Model
		private long _id;
		private long _outwarehouseapplyid;
		private string _companyid;
		private string _approvaler;
		private string _result;
		private string _feedback;
		private DateTime _approvaltime;
		/// <summary>
		/// 数据库记录ID
		/// </summary>
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 出库申请表ID
		/// </summary>
		public long OutWarehouseApplyID
		{
			set{ _outwarehouseapplyid=value;}
			get{return _outwarehouseapplyid;}
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
		/// 审批人
		/// </summary>
		public string Approvaler
		{
			set{ _approvaler=value;}
			get{return _approvaler;}
		}
		/// <summary>
		/// 审批结果，即事件名称
		/// </summary>
		public string Result
		{
			set{ _result=value;}
			get{return _result;}
		}
		/// <summary>
		/// 审批意见
		/// </summary>
		public string FeedBack
		{
			set{ _feedback=value;}
			get{return _feedback;}
		}
		/// <summary>
		/// 审批时间
		/// </summary>
		public DateTime ApprovalTime
		{
			set{ _approvaltime=value;}
			get{return _approvaltime;}
		}

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ApprovalerName { get; set; }
        /// <summary>
        /// 审批人职位
        /// </summary>
        public string ApprovalerPositionName { get; set; }
        /// <summary>
        /// 审批人部门ID
        /// </summary>
        public long ApprovalerDepartmentID { get; set; }
        /// <summary>
        /// 审批人部门名称
        /// </summary>
        public string ApprovalerDepartmentName { get; set; }
		#endregion Model
    }
}
