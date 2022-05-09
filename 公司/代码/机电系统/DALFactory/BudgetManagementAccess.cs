using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.BudgetManagement;

namespace FM2E.DALFactory
{
    public sealed class BudgetManagementAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        public static ISubjectRelation CreateSubjectRelation()
        {
            return (ISubjectRelation)InstanceCache.CreateInstance(path, ".BudgetManagement.SubjectRelation");
        }

        public static IAnnualBudget CreateAnnualBudget()
        {
            return (IAnnualBudget)InstanceCache.CreateInstance(path, ".BudgetManagement.AnnualBudget");
        }

        public static IQuarterlyBudget CreateQuarterlyBudget()
        {
            return (IQuarterlyBudget)InstanceCache.CreateInstance(path, ".BudgetManagement.QuarterlyBudget");
        }

        public static IQuarterlyForecast CreateQuarterlyForecast()
        {
            return (IQuarterlyForecast)InstanceCache.CreateInstance(path, ".BudgetManagement.QuarterlyForecast");
        }
    }
}
