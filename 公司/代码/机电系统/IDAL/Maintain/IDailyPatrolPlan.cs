using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IDailyPatrolPlan
    {
        DailyPatrolPlanInfo GetDailyPatrolPlan(long PlanID);
        long InsertDailyPatrolPlan(DailyPatrolPlanInfo model);
        void UpdateDailyPatrolPlan(DailyPatrolPlanInfo model);
        void DelDailyPatrolPlan(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(DailyPatrolPlanInfo item,string [] WFstates);
        QueryParam GenerateSearchTerm(DailyPatrolPlanInfo item);
    }
}
