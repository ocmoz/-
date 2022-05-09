using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.BudgetManagement;
using System.Collections;

namespace FM2E.IDAL.BudgetManagement
{
    public interface IQuarterlyForecast
    {
        QueryParam GenerateSearchTerm(QuarterlyForecastTotalInfo item);
        IList GetQuarterlyForecastTotalList(QueryParam term, out int recordCount, string companyid);
        QuarterlyForecastTotalInfo GetQuarterlyForecastTotal(long id);

        long InsertQuarterlyForecastTotal(QuarterlyForecastTotalInfo item);
        void UpdateQuarterlyForecastTotal(QuarterlyForecastTotalInfo item);
        void DelQuarterlyForecastTotal(long id);
        IList<QuarterlyForecastTotalInfo> Search(QuarterlyForecastTotalInfo item);
        IList<QuarterlyForecastInfo> Search(QuarterlyForecastInfo item);



        /*----------------------------------------*/
        IList<QuarterlyForecastDetailInfo> Search(QuarterlyForecastDetailInfo item);

        void DelQuarterlyForecastDetail(long id);

        IList Statistics1(QuarterlyForecastDetailInfo item);
        IList Summary1(QuarterlyForecastDetailInfo item);
    }
}
