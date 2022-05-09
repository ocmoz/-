
using FM2E.Model.Insurance;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Insurance
{
    public interface IInsuranceReport
    {
        /// <summary>
        /// 获取所有得保险单
        /// </summary>
        /// <returns></returns>
        IList GetAllInsuranceReport();
       /// <summary>
       /// 插入一条保险单
       /// </summary>
       /// <param name="item"></param>
        long InsertInsuranceReport(InsuranceReportInfo item);
       /// <summary>
       /// 更新一条保险单
       /// </summary>
       /// <param name="item"></param>
        void UpdateInsuranceReport(InsuranceReportInfo item);
        /// <summary>
        /// 通过id删除一条保险单
        /// </summary>
        /// <param name="id"></param>
        void DelInsuranceReport(string id);
        /// <summary>
        /// 通过id获取一条保险单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InsuranceReportInfo GetInsuranceReportInfo(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insuranceReportNo"></param>
        /// <returns></returns>
        InsuranceReportInfo GetInsuranceReportInfo(string insuranceReportNo);

        QueryParam GetSearchTerm(InsuranceReportSearchInfo term);

        IList GetInsuranceReports(QueryParam term, out int recordCount);

        void RepairRegister(InsuranceReportInfo insuranceReportInfo);
        void ReviewRegister(InsuranceReportInfo insuranceReportInfo);

    }
}
