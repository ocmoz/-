using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.Budget;
using System.Collections;

namespace FM2E.IDAL.Budget
{
    public interface IAnnualBudget
    {

        long SelectMaxBudgetYearRecord();

        QueryParam GenerateSearchTerm(BudgetYearInfo item);
        IList GetBudgetYearList(QueryParam term, out int recordCount, string companyid);
        BudgetYearInfo GetBudgetYear(long id);

        long InsertBudgetYear(BudgetYearInfo item);
        void UpdateBudgetYear(BudgetYearInfo item);
        void DelBudgetYear(long id);
        IList<BudgetYearInfo> Search(BudgetYearInfo item);


        QueryParam GenerateSearchTerm(BudgetYearDetailInfo item);
        IList GetBudgetYearDetailList(QueryParam term, out int recordCount, string companyid);
        BudgetYearDetailInfo GetBudgetYearDetail(long id);

        void InsertBudgetYearDetail(BudgetYearDetailInfo item);
        void UpdateBudgetYearDetail(BudgetYearDetailInfo item);
        void DelBudgetYearDetail(long id);
        IList<BudgetYearDetailInfo> Search(BudgetYearDetailInfo item);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(BudgetPerMonthTotalInfo item);
        IList GetBudgetPerMonthTotalList(QueryParam term, out int recordCount, string companyid);
        BudgetPerMonthTotalInfo GetBudgetPerMonthTotal(long id);

        long InsertBudgetPerMonthTotal(BudgetPerMonthTotalInfo item);
        void UpdateBudgetPerMonthTotal(BudgetPerMonthTotalInfo item);
        void DelBudgetPerMonthTotal(long id);
        IList<BudgetPerMonthTotalInfo> Search(BudgetPerMonthTotalInfo item);


        QueryParam GenerateSearchTerm(BudgetPermonthInfo item);
        IList GetBudgetPermonthList(QueryParam term, out int recordCount, string companyid);
        BudgetPermonthInfo GetBudgetPermonth(long id);

        void InsertBudgetPermonth(BudgetPermonthInfo item);
        void UpdateBudgetPermonth(BudgetPermonthInfo item);
        void DelBudgetPermonth(long id);
        IList<BudgetPermonthInfo> Search(BudgetPermonthInfo item);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(BudgetDetailInfo item);
        IList GetBudgetDetailList(QueryParam term, out int recordCount, string companyid);
        BudgetDetailInfo GetBudgetDetail(long id);

        void InsertBudgetDetail(BudgetDetailInfo item);
        void UpdateBudgetDetail(BudgetDetailInfo item);
        void DelBudgetDetail(long id);
        IList<BudgetDetailInfo> Search(BudgetDetailInfo item);

        IList Statistics1(BudgetDetailInfo item);
        IList Summary1(BudgetDetailInfo item);

        void SaveCurrentSubject(long Year, ref Hashtable ht);
        void UpdateCurrentSubject(long Year);
    }
}
