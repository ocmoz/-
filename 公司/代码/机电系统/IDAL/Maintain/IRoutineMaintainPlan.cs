using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IRoutineMaintainPlan
    {
        RoutineMaintainPlanInfo GetRoutineMaintainPlan(long PlanID);
        long InsertRoutineMaintainPlan(RoutineMaintainPlanInfo model);
        void UpdateRoutineMaintainPlan(RoutineMaintainPlanInfo model);
        void DelRoutineMaintainPlan(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(RoutineMaintainPlanInfo item, string[] WFstates);
        QueryParam GenerateSearchTerm(RoutineMaintainPlanInfo item);
    }
}
