using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ScrapApplySearchInfo
    {
        private string sheetName;
        private string companyID;
        private long depID;
        private ScrapStatus status;
        private string applicant;
        private string applicantName;
        private DateTime submitTimeFrom;
        private DateTime submitTimeTo;

        /// <summary>
        /// 表单编号
        /// </summary>
        public string SheetName
        {
            get { return sheetName; }
            set { sheetName = value; }
        }
        /// <summary>
        /// 借出公司编号
        /// </summary>
        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }
        /// <summary>
        /// 借入公司编号
        /// </summary>
        public long DepID
        {
            get { return depID; }
            set { depID = value; }
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
        /// 申请人用户名
        /// </summary>
        public string Applicant
        {
            get { return applicant; }
            set { applicant = value; }
        }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName
        {
            get { return applicantName; }
            set { applicantName = value; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime SubmitTimeFrom
        {
            get { return submitTimeFrom; }
            set { submitTimeFrom = value; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime SubmitTimeTo
        {
            get { return submitTimeTo; }
            set { submitTimeTo = value; }
        }

    }
}
