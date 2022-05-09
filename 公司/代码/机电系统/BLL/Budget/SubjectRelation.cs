using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.Budget;
using FM2E.IDAL.Budget;
using System.Collections;

namespace FM2E.BLL.Budget
{
    public class SubjectRelation
    {
        public QueryParam GenerateSearchTerm(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.GenerateSearchTerm(item);
        }

        public QueryParam GenerateSearchTermByYear(SubjectPerYear item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.GenerateSearchTermByYear(item);
        }

        public IList GetList(QueryParam term, out int recordCount, string companyid)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.GetList(term, out recordCount, companyid);
        }

        public IList GetListByYear(QueryParam term, out int recordCount, string companyid)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.GetListByYear(term, out recordCount, companyid);
        }

        public SubjectRelationInfos GetSubjectRelation(long id)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.GetSubjectRelation(id);
        }

        public void InsertSubjectRelation(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            dal.InsertSubjectRelation(item);
        }
        public void UpdateSubjectRelation(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            dal.UpdateSubjectRelation(item);
        }
        public void DelSubjectRelation(long id)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            dal.DelSubjectRelation(id);
        }
        public IList<SubjectRelationInfos> Search(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.Search(item);
        }

        public IList<SubjectRelationInfos> Search(SubjectPerYear item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.Search(item);
        }
        public IList<SubjectRelationInfos> SearchName(SubjectPerYear item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetAccess.CreateSubjectRelation();
            return dal.SearchName(item);
        }
    }
}
