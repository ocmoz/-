using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 借调申请单状态
    /// </summary>
    public enum BorrowApplyStatus
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
        /// 审批通过
        /// </summary>
        ApprovalPassed = 3,
        /// <summary>
        /// 审批不通过
        /// </summary>
        ApprovalFailed = 4,
        /// <summary>
        /// 已借出
        /// </summary>
        HasLent = 5
    }
    /// <summary>
    /// 设备借调申请实体类
    /// </summary>
    public class BorrowApplyInfo
    {
        private long _borrowapplyid;
        private string _sheetname;
        private string _companyid;
        private string _companyName;
        private string _borrowcompanyid;
        private string _borrowcompanyname;
        private string _applicant;
        private string _applicantName;
        private BorrowApplyStatus _status;
        private DateTime _submittime;
        private IList _detailList;
        private IList _approvalList;
        /// <summary>
        /// 申请单ID
        /// </summary>
        public long BorrowApplyID
        {
            set { _borrowapplyid = value; }
            get { return _borrowapplyid; }
        }
        /// <summary>
        /// 申请单编号
        /// </summary>
        public string SheetName
        {
            set { _sheetname = value; }
            get { return _sheetname; }
        }
        /// <summary>
        /// 借出方公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 借出方公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyName = value; }
            get { return _companyName; }
        }
        /// <summary>
        /// 借用方公司ID
        /// </summary>
        public string BorrowCompanyID
        {
            set { _borrowcompanyid = value; }
            get { return _borrowcompanyid; }
        }
        /// <summary>
        /// 借用方公司名称
        /// </summary>
        public string BorrowCompanyName
        {
            set { _borrowcompanyname = value; }
            get { return _borrowcompanyname; }
        }
        /// <summary>
        /// 申请人用户名
        /// </summary>
        public string Applicant
        {
            set { _applicant = value; }
            get { return _applicant; }
        }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName
        {
            set { _applicantName = value; }
            get { return _applicantName; }
        }
        /// <summary>
        /// 申请单状态
        /// </summary>
        public BorrowApplyStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 当前申请单的状态文字描述
        /// </summary>
        public string StatusString
        {
            get
            {
                string statusString = "";
                switch (_status)
                {
                    case BorrowApplyStatus.Draft:
                        statusString = "草稿";
                        break;
                    case BorrowApplyStatus.Waiting4ApprovalResult:
                        statusString = "等待审批结果";
                        break;
                    case BorrowApplyStatus.ApprovalPassed:
                        statusString = "审批通过";
                        break;
                    case BorrowApplyStatus.ApprovalFailed:
                        statusString = "审批不通过";
                        break;
                    case BorrowApplyStatus.HasLent:
                        statusString = "已借出";
                        break;
                    default:
                        statusString = "未知状态";
                        break;
                }
                return statusString;
            }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime SubmitTime
        {
            set { _submittime = value; }
            get { return _submittime; }
        }
        /// <summary>
        /// 申请明细
        /// </summary>
        public IList DetailList
        {
            set { _detailList = value; }
            get { return _detailList; }
        }
        /// <summary>
        /// 审批历史列表
        /// </summary>
        public IList ApprovalList
        {
            set { _approvalList = value; }
            get { return _approvalList; }
        }
    }
}
