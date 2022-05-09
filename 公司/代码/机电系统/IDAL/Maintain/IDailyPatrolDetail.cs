using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IDailyPatrolDetail
    {
        DailyPatrolDetailInfo GetDailyPatrolDetail(long PlanID);
        void InsertDailyPatrolDetail(DailyPatrolDetailInfo model);
        void UpdateDailyPatrolDetail(DailyPatrolDetailInfo model);
        void DelDailyPatrolDetail(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(DailyPatrolDetailInfo item);
    }
}
