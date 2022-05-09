using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IRoutineInspectDetail
    {
        RoutineInspectDetailInfo GetRoutineInspectDetail(long PlanID);
        void InsertRoutineInspectDetail(RoutineInspectDetailInfo model);
        void UpdateRoutineInspectDetail(RoutineInspectDetailInfo model);
        void DelRoutineInspectDetail(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(RoutineInspectDetailInfo item);
    }
}
