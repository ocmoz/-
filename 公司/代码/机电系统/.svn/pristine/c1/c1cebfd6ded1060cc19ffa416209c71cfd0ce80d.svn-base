using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    class ScrapInfo
    {
        #region 状态类型
        enum  ScraptStatus:short
        {
            Staff = 0,
            Wait = 1,
            Past = 2,
            NotPast = 3,
            Finish = 4
        }
        #endregion

        #region Model
        private long scraptID;//报废单号
        private string sheetName;//报废单名
        private string companyID;//公司编号
        private string companyName;//公司名称
        private string applicant;//申请人
        private DateTime applyDate;//申请时间
        private short status;//申请状态
        private string remark;//备注
        private IList equipment;//设备

        public long ScraptID
        {
            set { scraptID = value; }
            get { return scraptID; }
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
        public string Applicant
        {
            set { applicant = value; }
            get { return applicant; }
        }
        public DateTime ApplyDate
        {
            set { applyDate = value; }
            get { return applyDate; }
        }
        private short Status
        {
            set { status = value; }
            get { return status; }
        }
        private string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
        public IList Equipment
        {
            set { equipment = value; }
            get { return equipment; }
        }
        #endregion
    }
}
