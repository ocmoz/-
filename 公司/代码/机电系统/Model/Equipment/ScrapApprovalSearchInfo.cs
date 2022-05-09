using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ScrapApprovalSearchInfo
    {
        private string sheetName;
        private string companyID;
        private long depID;
        private string applicantName;
        private DateTime applyDateFrom;
        private DateTime applyDateTo;
        private DateTime approvalDateFrom;
        private DateTime approvalDateTo;
        private string approvalerID;
        private int approvalResult;
        private string equipmentNO;

        public string EquipmentNO
        {
            get { return equipmentNO; }
            set { equipmentNO = value; }
        }

        public string SheetNO
        {
            get { return sheetName; }
            set { sheetName = value; }
        }

        public string SheetName
        {
            get { return sheetName; }
            set { sheetName = value; }
        }

        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }

        public long DepID
        {
            get { return depID; }
            set { depID = value; }
        }

        public string ApplicantName
        {
            get { return applicantName; }
            set { applicantName = value; }
        }

        public DateTime ApplyDateFrom
        {
            get { return applyDateFrom; }
            set { applyDateFrom = value; }
        }

        public DateTime ApplyDateTo
        {
            get { return applyDateTo; }
            set { applyDateTo = value; }
        }

        public DateTime ApprovalDateFrom
        {
            get { return approvalDateFrom; }
            set { approvalDateFrom = value; }
        }

        public DateTime ApprovalDateTo
        {
            get { return approvalDateTo; }
            set { approvalDateTo = value; }
        }

        public int ApprovalResult
        {
            get { return approvalResult; }
            set { approvalResult = value; }
        }

        public string ApprovalerID
        {
            get { return approvalerID; }
            set { approvalerID = value; }
        }
    }
}
