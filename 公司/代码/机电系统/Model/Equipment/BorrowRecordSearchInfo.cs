using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class BorrowRecordSearchInfo
    {
        private string sheetNO;
        private string borrowCompanyID;
        private string borrowerName;
        private string recorder;
        private DateTime borrowTimeFrom;
        private DateTime borrowTimeTo;
        private DateTime returnDateFrom;
        private DateTime returnDateTo;

        /// <summary>
        /// 申请单编号
        /// </summary>
        public string SheetNO
        {
            set { sheetNO = value; }
            get { return sheetNO; }
        }
        /// <summary>
        /// 借方公司ID
        /// </summary>
        public string BorrowCompanyID
        {
            set { borrowCompanyID = value; }
            get { return borrowCompanyID; }
        }
        /// <summary>
        /// 领用人姓名
        /// </summary>
        public string BorrowerName
        {
            set { borrowerName = value; }
            get { return borrowerName; }
        }
        /// <summary>
        /// 借出登记人用户名
        /// </summary>
        public string Recorder
        {
            set { recorder = value; }
            get { return recorder; }
        }
        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime BorrowTimeFrom
        {
            set { borrowTimeFrom = value; }
            get { return borrowTimeFrom; }
        }
        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime BorrowTimeTo
        {
            set { borrowTimeTo = value; }
            get { return borrowTimeTo; }
        }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime ReturnDateFrom
        {
            set { returnDateFrom = value; }
            get { return returnDateFrom; }
        }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime ReturnDateTo
        {
            set { returnDateTo = value; }
            get { return returnDateTo; }
        }
    }
}
