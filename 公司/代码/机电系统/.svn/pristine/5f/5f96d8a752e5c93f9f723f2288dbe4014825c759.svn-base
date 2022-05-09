using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 报废申请状态
    /// </summary>
    public enum ScrapStatus
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 2,
        /// <summary>
        /// 等待审批结果
        /// </summary>
        Wait4ApprovalResult = 4,
        /// <summary>
        /// 审批通过
        /// </summary>
        ApprovalPassed = 8,
        /// <summary>
        /// 审批不通过
        /// </summary>
        ApprovalNotPassed = 16,
        /// <summary>
        /// 报废结束
        /// </summary>
        Finished = 32
    }

    public class ScrapApplyInfo
    {
        #region Model
        private long scrapID;//报废单号
        private string sheetName;//报废单名
        private string companyID;//公司编号
        private string companyName;//公司名称
        private long depID;
        private string depName;
        private string applicant;//申请人
        private string applicantName;//申请人名字
        private DateTime applyDate;//申请时间
        private ScrapStatus status;//申请状态
        private string remark;//备注
        private IList equipments;//设备
        private IList approvalList;
        private string attachment;
        public long ScrapID
        {
            set { scrapID = value; }
            get { return scrapID; }
        }
        public string SheetNO
        {
            set { sheetName = value; }
            get { return sheetName; }
        }
        public string SheetName
        {
            set { sheetName = value; }
            get { return sheetName; }
        }
        public string CompanyID
        {
            set { companyID = value; }
            get { return companyID; }
        }
        public string CompanyName
        {
            set { companyName = value; }
            get { return companyName; }
        }
        public long DepID
        {
            set { depID = value; }
            get { return depID; }
        }
        public string DepName
        {
            set { depName = value; }
            get { return depName; }
        }
        public string Applicant
        {
            set { applicant = value; }
            get { return applicant; }
        }
        public string ApplicantName
        {
            set { applicantName = value; }
            get { return applicantName; }
        }
        public DateTime ApplyDate
        {
            set { applyDate = value; }
            get { return applyDate; }
        }
        public ScrapStatus Status
        {
            set { status = value; }
            get { return status; }
        }
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
        public IList Equipments
        {
            set { equipments = value; }
            get { return equipments; }
        }
        public IList ApprovalList
        {
            set { approvalList = value; }
            get { return approvalList; }
        }
        public string Attachment
        {
            set { attachment = value; }
            get { return attachment; }
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
        #endregion
    }
}
