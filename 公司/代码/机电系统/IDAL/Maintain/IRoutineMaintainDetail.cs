using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IRoutineMaintainDetail
    {
        RoutineMaintainDetailInfo GetRoutineMaintainDetail(long PlanID);
        void InsertRoutineMaintainDetail(RoutineMaintainDetailInfo model);
        void UpdateRoutineMaintainDetail(RoutineMaintainDetailInfo model);
        void DelRoutineMaintainDetail(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(RoutineMaintainDetailInfo item);
    }
}
