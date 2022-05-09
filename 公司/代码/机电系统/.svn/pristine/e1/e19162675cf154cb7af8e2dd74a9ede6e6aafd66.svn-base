using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IRoutineInspectPlan
    {
        RoutineInspectPlanInfo GetRoutineInspectPlan(long PlanID);
        long InsertRoutineInspectPlan(RoutineInspectPlanInfo model);
        void UpdateRoutineInspectPlan(RoutineInspectPlanInfo model);
        void DelRoutineInspectPlan(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(RoutineInspectPlanInfo item, string[] WFstates);
        QueryParam GenerateSearchTerm(RoutineInspectPlanInfo item);
    }
}
