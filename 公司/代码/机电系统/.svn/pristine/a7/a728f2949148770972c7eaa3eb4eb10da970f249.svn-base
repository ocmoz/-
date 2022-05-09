
using System;
using System.Collections;
using FM2E.DALFactory;
using FM2E.IDAL.Insurance;
using FM2E.Model.Insurance;
using FM2E.Model.Utils;


namespace FM2E.BLL.Insurance
{   [Serializable]
    public class Insurance
    {
        
        /// <summary>
        /// 插入保险单
        /// </summary>
        /// <param name="insuranceInfo"></param>
        public void InsertInsurance(InsuranceInfo insuranceInfo)
        {
            IInsurance dal = InsuranceAccess.CreateInsurance();
            dal.InsertInsurance(insuranceInfo);
        }

        public IList GetAllInsurance()
        {
            IInsurance dal = InsuranceAccess.CreateInsurance();
            return dal.GetAllInsurance();
        }

        public InsuranceInfo GetInsuranceInfo(long id)
        {
            IInsurance dal = InsuranceAccess.CreateInsurance();
            return dal.GetInsuranceInfo(id);    
        }

        /// <summary>
        /// 更新一条保险单
        /// </summary>
        /// <param name="item"></param>
        public void UpdateInsurance(InsuranceInfo item)
        {
            IInsurance dal = InsuranceAccess.CreateInsurance();
            dal.UpdateInsurance(item);
        }

        /// <summary>
        /// 通过条件查询保险单
        /// </summary>
        /// <param name="term"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetInsurance(InsuranceSearchInfo term, int pageIndex, int pageSize, out int recordCount)
        {
            IInsurance dal = InsuranceAccess.CreateInsurance();
            QueryParam qp = dal.GetSearchTerm(term);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.GetInsurances(qp, out recordCount);
        }
        /// <summary>
        /// 通过Id删除一条保险单
        /// </summary>
        /// <param name="id"></param>
        public void DelInsurance(string id)
        {
            IInsurance dal = InsuranceAccess.CreateInsurance();
            dal.DelInsurance(id);
        }

       
    }
}
