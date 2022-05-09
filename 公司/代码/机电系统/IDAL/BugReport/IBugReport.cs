using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.Model.BugReportManager;
using System.Collections;

namespace FM2E.IDAL.BugReport
{
    public interface IBugReport
    {
        QueryParam GenerateSearchTerm(BugReportInfo item);
        IList GetBugReportList(QueryParam term, out int recordCount);
        BugReportInfo GetBugReport(long id);

        long InsertBugReport(BugReportInfo item);
        void UpdateBugReport(BugReportInfo item);
        IList<BugReportInfo> Search(BugReportInfo item);

    }
}
