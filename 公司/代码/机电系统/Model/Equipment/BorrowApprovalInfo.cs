using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 设备借调审批实体类
    /// </summary>
    public class BorrowApprovalInfo
    {
        private long _borrowapplyid;
        private string _approvaler;
        private string _approvalerName;
        private bool _result;
        private string _feeback;
        private DateTime _approvaldate;
        /// <summary>
        /// 
        /// </summary>
        public long BorrowApplyID
        {
            set { _borrowapplyid = value; }
            get { return _borrowapplyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Approvaler
        {
            set { _approvaler = value; }
            get { return _approvaler; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalerName
        {
            set { _approvalerName = value; }
            get { return _approvalerName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FeeBack
        {
            set { _feeback = value; }
            get { return _feeback; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ApprovalDate
        {
            set { _approvaldate = value; }
            get { return _approvaldate; }
        }
    }
}
