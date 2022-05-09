using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.BugReport;

namespace FM2E.DALFactory
{
    public class BugReportAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        public static IBugReport CreateBugReport()
        {
            return (IBugReport)InstanceCache.CreateInstance(path, ".BugReport.BugReport");
        }

     
    }
}
