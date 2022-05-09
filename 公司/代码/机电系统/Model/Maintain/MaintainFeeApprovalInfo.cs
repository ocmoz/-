using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 维修费用申请审批实体类
    /// </summary>
    public class MaintainFeeApprovalInfo
    {
        private long _approvalid;
        private long _applyid;
        private string _approvaler;
        private int _approvalresult;
        private string _remark;
        private DateTime _approvaldate;
        /// <summary>
        /// 
        /// </summary>
        public long ApprovalID
        {
            set { _approvalid = value; }
            get { return _approvalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ApplyID
        {
            set { _applyid = value; }
            get { return _applyid; }
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
        public int ApprovalResult
        {
            set { _approvalresult = value; }
            get { return _approvalresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
