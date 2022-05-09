using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;

using FM2E.IDAL.Equipment;
using FM2E.IDAL.Insurance;

namespace FM2E.DALFactory
{
    public sealed class InsuranceAccess
    {
    // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private InsuranceAccess() { }

        public static IInsurance CreateInsurance()
        {
            return (IInsurance)InstanceCache.CreateInstance(path, ".Insurance.Insurance");
        }
        public static IInsuranceReport CreateInsuranceReport()
        {
            return (IInsuranceReport)InstanceCache.CreateInstance(path, ".Insurance.InsuranceReport");
        }

       
    }
}
