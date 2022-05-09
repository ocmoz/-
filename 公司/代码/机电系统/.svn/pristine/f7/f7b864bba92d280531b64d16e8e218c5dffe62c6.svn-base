using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Budget;
using System.Configuration;

namespace FM2E.DALFactory
{
     public class BudgetAccess
    {
        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        public static ISubjectRelation CreateSubjectRelation()
        {
            return (ISubjectRelation)InstanceCache.CreateInstance(path, ".Budget.SubjectRelation");
        }

        public static IAnnualBudget CreateAnnualBudget()
        {
            return (IAnnualBudget)InstanceCache.CreateInstance(path, ".Budget.AnnualBudget");
        }
    }
}
