
using System;
using System.Collections;
using FM2E.DALFactory;
using FM2E.IDAL.Insurance;
using FM2E.Model.Insurance;
using FM2E.Model.Utils;


namespace FM2E.BLL.Insurance
{   [Serializable]
    public class InsuranceReport
    {
        
        /// <summary>
        /// 插入保险单
        /// </summary>
        /// <param name="insuranceInfo"></param>
        public long InsertInsuranceReport(InsuranceReportInfo insuranceInfo)
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
           return dal.InsertInsuranceReport(insuranceInfo);
        }

        public IList GetAllInsuranceReport()
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
            return dal.GetAllInsuranceReport();
        }

        public InsuranceReportInfo GetInsuranceReportInfo(long id)
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
            return dal.GetInsuranceReportInfo(id);    
        }

        /// <summary>
        /// 更新一条保险单
        /// </summary>
        /// <param name="item"></param>
        public void UpdateInsuranceReport(InsuranceReportInfo item)
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
            dal.UpdateInsuranceReport(item);
        }

        /// <summary>
        /// 通过条件查询保险单
        /// </summary>
        /// <param name="term"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetInsuranceReport(InsuranceReportSearchInfo term, int pageIndex, int pageSize, out int recordCount)
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
            QueryParam qp = dal.GetSearchTerm(term);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.GetInsuranceReports(qp, out recordCount);
        }
        /// <summary>
        /// 通过Id删除一条保险单
        /// </summary>
        /// <param name="id"></param>
        public void DelInsuranceReport(string id)
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
            dal.DelInsuranceReport(id);
        }
        /// <summary>
        /// 修复登记
        /// </summary>
        /// <param name="insuranceReportInfo"></param>
        public void RepairRegister(InsuranceReportInfo insuranceReportInfo)
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
            dal.RepairRegister(insuranceReportInfo);
        }
        /// <summary>
        /// 审核登记
        /// </summary>
        /// <param name="insuranceReportInfo"></param>
        public void ReviewRegister(InsuranceReportInfo insuranceReportInfo)
        {
            IInsuranceReport dal = InsuranceAccess.CreateInsuranceReport();
            dal.ReviewRegister(insuranceReportInfo);
        }
       
    }
}
