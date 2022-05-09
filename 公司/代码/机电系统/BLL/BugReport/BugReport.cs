using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.BugReportManager;
using FM2E.IDAL.BugReport;

namespace FM2E.BLL.BugReport
{
    public class BugReport
    {
        public QueryParam GenerateSearchTerm(BugReportInfo item)
        {
            IBugReport dal = FM2E.DALFactory.BugReportAccess.CreateBugReport();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetBugReportList(QueryParam term, out int recordCount)
        {
            IBugReport dal = FM2E.DALFactory.BugReportAccess.CreateBugReport();
            return dal.GetBugReportList(term, out recordCount);
        }
        public BugReportInfo GetBugReport(long id)
        {
            IBugReport dal = FM2E.DALFactory.BugReportAccess.CreateBugReport();
            return dal.GetBugReport(id);
        }


        public long InsertBugReport(BugReportInfo item)
        {
            IBugReport dal = FM2E.DALFactory.BugReportAccess.CreateBugReport();
            return dal.InsertBugReport(item);
        }
        public void UpdateBugReport(BugReportInfo item)
        {
            IBugReport dal = FM2E.DALFactory.BugReportAccess.CreateBugReport();
            dal.UpdateBugReport(item);
        }
        public IList<BugReportInfo> Search(BugReportInfo item)
        {
            IBugReport dal = FM2E.DALFactory.BugReportAccess.CreateBugReport();
            return dal.Search(item);
        }
    }
}
