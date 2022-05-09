using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.BudgetManagement;
using System.Collections;

namespace FM2E.IDAL.BudgetManagement
{
    public interface IQuarterlyBudget
    {
        QueryParam GenerateSearchTerm(QuarterlyBudgetTotalInfo item);
        IList GetQuarterlyBudgetTotalList(QueryParam term, out int recordCount, string companyid);
        QuarterlyBudgetTotalInfo GetQuarterlyBudgetTotal(long id);

        long InsertQuarterlyBudgetTotal(QuarterlyBudgetTotalInfo item);
        void UpdateQuarterlyBudgetTotal(QuarterlyBudgetTotalInfo item);
        void DelQuarterlyBudgetTotal(long id);
        IList<QuarterlyBudgetTotalInfo> Search(QuarterlyBudgetTotalInfo item);
        IList<QuarterlyBudgetInfo> Search(QuarterlyBudgetInfo item);



        /*----------------------------------------*/
        IList<QuarterlyBudgetDetailInfo> Search(QuarterlyBudgetDetailInfo item);

        void DelQuarterlyBudgetDetail(long id);

        IList Statistics1(QuarterlyBudgetDetailInfo item);
        IList Summary1(QuarterlyBudgetDetailInfo item);

    }
}
