using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.BudgetManagement;
using FM2E.IDAL.BudgetManagement;
using System.Collections;

namespace FM2E.BLL.BudgetManagement
{
    public class QuarterlyBudget
    {
        public QueryParam GenerateSearchTerm(QuarterlyBudgetTotalInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.GenerateSearchTerm(item);
        }

        public IList GetQuarterlyBudgetTotalList(QueryParam term, out int recordCount, string companyid)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.GetQuarterlyBudgetTotalList(term, out recordCount, companyid);
        }

        public QuarterlyBudgetTotalInfo GetQuarterlyBudgetTotal(long id)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.GetQuarterlyBudgetTotal(id);
        }

        public long InsertQuarterlyBudgetTotal(QuarterlyBudgetTotalInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.InsertQuarterlyBudgetTotal(item);
        }

        public void UpdateQuarterlyBudgetTotal(QuarterlyBudgetTotalInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            dal.UpdateQuarterlyBudgetTotal(item);
        }

        public void DelQuarterlyBudgetTotal(long id)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            dal.DelQuarterlyBudgetTotal(id);
        }

        public IList<QuarterlyBudgetTotalInfo> Search(QuarterlyBudgetTotalInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.Search(item);
        }

        public IList<QuarterlyBudgetInfo> Search(QuarterlyBudgetInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.Search(item);
        }



        /*--------------------------------------------*/
        public IList<QuarterlyBudgetDetailInfo> Search(QuarterlyBudgetDetailInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.Search(item);
        }

        public void DelQuarterlyBudgetDetail(long id)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            dal.DelQuarterlyBudgetDetail(id);
        }

        public IList Statistics1(QuarterlyBudgetDetailInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.Statistics1(item);
        }

        public IList Summary1(QuarterlyBudgetDetailInfo item)
        {
            IQuarterlyBudget dal = FM2E.DALFactory.BudgetManagementAccess.CreateQuarterlyBudget();
            return dal.Summary1(item);
        }
    }
}
