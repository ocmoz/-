using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ScrapApprovaFacadelInfo
    {
        //申请单信息
        private long scrapID;
        private string sheetNO;
        private string companyID;
        private string companyName;
        private long depID;
        private string depName;
        private string applicant;
        private string applicantName;
        private DateTime applyDate;
        private ScrapStatus status;
        //特定审批人的审批信息
        private string approvalerID;
        private string approvalerName;
        private int approvalResult;
        private DateTime approvalDate;

        /// <summary>
        /// 申请单ID
        /// </summary>
        public long ScrapID
        {
            get { return scrapID; }
            set { scrapID = value; }
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
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }
        public long DepID
        {
            get{return depID;}
            set{depID=value;}
        }
        public string DepName
        {
            get{return depName;}
            set{depName=value;}
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
        public ScrapStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 审批人用户名
        /// </summary>
        public string ApprovalerID
        {
            get { return approvalerID; }
            set { approvalerID = value; }
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
        /// <summary>
        /// 报废单状态描述
        /// </summary>
        public string StatusString
        {
            get
            {
                string str = "";
                switch (status)
                {
                    case ScrapStatus.Draft:
                        str = "草稿";
                        break;
                    case ScrapStatus.Unknown:
                        str = "未知状态";
                        break;
                    case ScrapStatus.Wait4ApprovalResult:
                        str = "等待审批结果";
                        break;
                    case ScrapStatus.ApprovalPassed:
                        str = "审批通过";
                        break;
                    case ScrapStatus.ApprovalNotPassed:
                        str = "审批不通过";
                        break;
                    case ScrapStatus.Finished:
                        str = "报废结束";
                        break;
                    default:
                        str = "未知状态";
                        break;
                }
                return str;
            }
        }
    }
}