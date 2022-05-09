using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.Model.Insurance;
using FM2E.Model.Utils;
using System.Collections;
using System.Data.Common;

namespace FM2E.IDAL.Insurance
{
    public interface IInsurance
    {
        /// <summary>
        /// 获取所有得保险单
        /// </summary>
        /// <returns></returns>
        IList GetAllInsurance();
       /// <summary>
       /// 插入一条保险单
       /// </summary>
       /// <param name="item"></param>
        void InsertInsurance(InsuranceInfo item);
       /// <summary>
       /// 更新一条保险单
       /// </summary>
       /// <param name="item"></param>
        void UpdateInsurance(InsuranceInfo item);
        /// <summary>
        /// 通过id删除一条保险单
        /// </summary>
        /// <param name="id"></param>
        void DelInsurance(string id);
        /// <summary>
        /// 通过id获取一条保险单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InsuranceInfo GetInsuranceInfo(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insuranceNo"></param>
        /// <returns></returns>
        InsuranceInfo GetInsuranceInfo(string insuranceNo);

        QueryParam GetSearchTerm(InsuranceSearchInfo term);

        IList GetInsurances(QueryParam term, out int recordCount);

    }
}
