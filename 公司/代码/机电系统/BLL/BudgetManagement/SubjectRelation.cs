using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.BudgetManagement;
using FM2E.IDAL.BudgetManagement;
using System.Collections;

namespace FM2E.BLL.BudgetManagement
{
    public class SubjectRelation
    {
        public QueryParam GenerateSearchTerm(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.GenerateSearchTerm(item);
        }

        public QueryParam GenerateSearchTermByYear(SubjectPerYear item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.GenerateSearchTermByYear(item);
        }

        public IList GetList(QueryParam term, out int recordCount, string companyid)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.GetList(term, out recordCount, companyid);
        }

        public IList GetListByYear(QueryParam term, out int recordCount, string companyid)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.GetListByYear(term, out recordCount, companyid);
        }

        public SubjectRelationInfos GetSubjectRelation(long id)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.GetSubjectRelation(id);
        }

        public void InsertSubjectRelation(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            dal.InsertSubjectRelation(item);
        }
        public void UpdateSubjectRelation(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            dal.UpdateSubjectRelation(item);
        }
        public void DelSubjectRelation(long id)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            dal.DelSubjectRelation(id);
        }
        public IList<SubjectRelationInfos> Search(SubjectRelationInfos item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.Search(item);
        }

        public IList<SubjectRelationInfos> Search(SubjectPerYear item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.Search(item);
        }
        public IList<SubjectRelationInfos> SearchName(SubjectPerYear item)
        {
            ISubjectRelation dal = FM2E.DALFactory.BudgetManagementAccess.CreateSubjectRelation();
            return dal.SearchName(item);
        }
    }
}
