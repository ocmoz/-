using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    public enum PriceApplyStatus
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        UnKnownStatus,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 1,
        /// <summary>
        /// 等待审批结果
        /// </summary>
        Waiting4ApprovalResult = 2,
        /// <summary>
        /// 审批通过，等待领用
        /// </summary>
        ApprovalPassed = 3,
        /// <summary>
        /// 已领用
        /// </summary>
        PartApprovalPassed = 4,
        /// <summary>
        /// 已拒绝
        /// </summary>
        ApprovalFailed = 5,
        /// <summary>
        /// 审批通过的所有状态
        /// </summary>
        Approvaled = 6
    }
    public class PriceApplyInfo
    {
        #region Model
        private long _applyformid;
        private string _companyid;
        private string _applicant;
        private string _approvaler;
        private DateTime _approvaldate;
        private IList _detailList;
        private DateTime _applydate;
        private PriceApplyStatus _status;
        private string _applicantname;
        private string _approvalername;
        /// <summary>
        /// 
        /// </summary>
        public DateTime ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ApplyFormID
        {
            set { _applyformid = value; }
            get { return _applyformid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Applicant
        {
            set { _applicant = value; }
            get { return _applicant; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApplicantName
        {
            set { _applicantname = value; }
            get { return _applicantname; }
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
            set { _approvalername = value; }
            get { return _approvalername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ApprovalDate
        {
            set { _approvaldate = value; }
            get { return _approvaldate; }
        }
        public IList DetailList
        {
            set { _detailList = value; }
            get { return _detailList; }
        }
        /// <summary>
        /// 
        /// </summary>
        public PriceApplyStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model
        public string StatusName
        {
            get
            {
                switch (_status)
                {
                    case PriceApplyStatus.Draft: return "草稿";
                    case PriceApplyStatus.Waiting4ApprovalResult: return "等待审批";
                    case PriceApplyStatus.ApprovalPassed: return "审批通过";
                    case PriceApplyStatus.PartApprovalPassed: return "部分通过";
                    case PriceApplyStatus.ApprovalFailed: return "已拒绝";
                    default: return "未知状态";
                }
            }
        }
    }
}
