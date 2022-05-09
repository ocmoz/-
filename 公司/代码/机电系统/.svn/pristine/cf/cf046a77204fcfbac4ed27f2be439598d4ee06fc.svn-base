using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.BudgetManagement;
using System.Collections;

namespace FM2E.IDAL.BudgetManagement
{
    public interface ISubjectRelation
    {
        QueryParam GenerateSearchTerm(SubjectRelationInfos item);
        QueryParam GenerateSearchTermByYear(SubjectPerYear item);
        IList GetList(QueryParam term, out int recordCount, string companyid);
        IList GetListByYear(QueryParam term, out int recordCount, string companyid);
        SubjectRelationInfos GetSubjectRelation(long id);

        void InsertSubjectRelation(SubjectRelationInfos item);
        void UpdateSubjectRelation(SubjectRelationInfos item);
        void DelSubjectRelation(long id);
        IList<SubjectRelationInfos> Search(SubjectRelationInfos item);
        IList<SubjectRelationInfos> Search(SubjectPerYear item);
        IList<SubjectRelationInfos> SearchName(SubjectPerYear item);
    }

}
