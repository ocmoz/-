using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class RoutineMaintainDetail
    {
        public RoutineMaintainDetailInfo GetRoutineMaintainDetail(long ItemID)
        {
            IRoutineMaintainDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainDetail();
            return dal.GetRoutineMaintainDetail(ItemID);
        }
        public void InsertRoutineMaintainDetail(RoutineMaintainDetailInfo model)
        {
            IRoutineMaintainDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainDetail();
            dal.InsertRoutineMaintainDetail(model);
        }
        public void UpdateRoutineMaintainDetail(RoutineMaintainDetailInfo model)
        {
            IRoutineMaintainDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainDetail();
            dal.UpdateRoutineMaintainDetail(model);
        }
        public void DelRoutineMaintainDetail(long ItemID)
        {
            IRoutineMaintainDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainDetail();
            dal.DelRoutineMaintainDetail(ItemID);
        }
        public QueryParam GenerateSearchTerm(RoutineMaintainDetailInfo item)
        {
            IRoutineMaintainDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainDetail();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineMaintainDetail dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainDetail();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}