using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.BudgetManagement;
using FM2E.IDAL.BudgetManagement;
using System.Collections;

namespace FM2E.BLL.BudgetManagement
{
    public class QuarterlyForecast
    {
        public QueryParam GenerateSearchTerm(QuarterlyForecastTotalInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.GenerateSearchTerm(item);
        }

        public IList GetQuarterlyForecastTotalList(QueryParam term, out int recordCount, string companyid)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.GetQuarterlyForecastTotalList(term, out recordCount, companyid);
        }

        public QuarterlyForecastTotalInfo GetQuarterlyForecastTotal(long id)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.GetQuarterlyForecastTotal(id);
        }

        public long InsertQuarterlyForecastTotal(QuarterlyForecastTotalInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.InsertQuarterlyForecastTotal(item);
        }

        public void UpdateQuarterlyForecastTotal(QuarterlyForecastTotalInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            dal.UpdateQuarterlyForecastTotal(item);
        }

        public void DelQuarterlyForecastTotal(long id)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            dal.DelQuarterlyForecastTotal(id);
        }

        public IList<QuarterlyForecastTotalInfo> Search(QuarterlyForecastTotalInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.Search(item);
        }

        public IList<QuarterlyForecastInfo> Search(QuarterlyForecastInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.Search(item);
        }



        /*--------------------------------------------*/
        public IList<QuarterlyForecastDetailInfo> Search(QuarterlyForecastDetailInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.Search(item);
        }

        public void DelQuarterlyForecastDetail(long id)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            dal.DelQuarterlyForecastDetail(id);
        }

        public IList Statistics1(QuarterlyForecastDetailInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.Statistics1(item);
        }

        public IList Summary1(QuarterlyForecastDetailInfo item)
        {
            IQuarterlyForecast dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyForecast();
            return dal.Summary1(item);
        }
    }
}
