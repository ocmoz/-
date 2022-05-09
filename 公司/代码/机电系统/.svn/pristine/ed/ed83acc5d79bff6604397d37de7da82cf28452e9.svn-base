using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class RoutineInspectDetail
    {
        public RoutineInspectDetailInfo GetRoutineInspectDetail(long ItemID)
        {
            IRoutineInspectDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectDetail();
            return dal.GetRoutineInspectDetail(ItemID);
        }
        public void InsertRoutineInspectDetail(RoutineInspectDetailInfo model)
        {
            IRoutineInspectDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectDetail();
            dal.InsertRoutineInspectDetail(model);
        }
        public void UpdateRoutineInspectDetail(RoutineInspectDetailInfo model)
        {
            IRoutineInspectDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectDetail();
            dal.UpdateRoutineInspectDetail(model);
        }
        public void DelRoutineInspectDetail(long ItemID)
        {
            IRoutineInspectDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectDetail();
            dal.DelRoutineInspectDetail(ItemID);
        }
        public QueryParam GenerateSearchTerm(RoutineInspectDetailInfo item)
        {
            IRoutineInspectDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectDetail();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineInspectDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectDetail();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}