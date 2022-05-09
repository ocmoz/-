using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 包含有借调申请单信息的审批实体类
    /// </summary>
    public class BorrowApprovaFacadelInfo
    {
        //申请单信息
        private long borrowApplyID;
        private string sheetNO;
        private string borrowCompanyID;
        private string borrowCompanyName;
        private string applicant;
        private string applicantName;
        private DateTime applyDate;
        private BorrowApplyStatus status;
        //特定审批人的审批信息
        private string approvaler;
        private string approvalerName;
        private int approvalResult;
        private DateTime approvalDate;

        /// <summary>
        /// 申请单ID
        /// </summary>
        public long BorrowApplyID
        {
            get { return borrowApplyID; }
            set { borrowApplyID = value; }
        }
        /// <summary>
        /// 表单编号
        /// </summary>
        public string SheetNO
        {
            get { return sheetNO; }
            set { sheetNO = value; }
        }
        /// <summary>
        /// 借入方公司ID
        /// </summary>
        public string BorrowCompanyID
        {
            get { return borrowCompanyID; }
            set { borrowCompanyID = value; }
        }
        /// <summary>
        /// 借入方公司名
        /// </summary>
        public string BorrowCompanyName
        {
            get { return borrowCompanyName; }
            set { borrowCompanyName = value; }
        }
        /// <summary>
        /// 申请人用户名
        /// </summary>
        public string Applicant
        {
            get { return applicant; }
            set { applicant = value; }
        }
        /// <summary>
        /// 申请人真实姓名
        /// </summary>
        public string ApplicantName
        {
            get { return applicantName; }
            set { applicantName = value; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime ApplyDate
        {
            get { return applyDate; }
            set { applyDate = value; }
        }
        /// <summary>
        /// 申请单状态
        /// </summary>
        public BorrowApplyStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 当前申请单的状态文字描述
        /// </summary>
        public string StatusString
        {
            get
            {
                string statusString = "";
                switch (status)
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
        /// 审批人用户名
        /// </summary>
        public string Approvaler
        {
            get { return approvaler; }
            set { approvaler = value; }
        }
        /// <summary>
        /// 审批人真实姓名
        /// </summary>
        public string ApprovalerName
        {
            get { return approvalerName; }
            set { approvalerName = value; }
        }
        /// <summary>
        /// 审批结果
        /// </summary>
        public int ApprovalResult
        {
            get { return approvalResult; }
            set { approvalResult = value; }
        }
        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime ApprovalDate
        {
            get { return approvalDate; }
            set { approvalDate = value; }
        }
    }
}
