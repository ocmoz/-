using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class DailyPatrolDetail
    {
        public DailyPatrolDetailInfo GetDailyPatrolDetail(long ItemID)
        {
            IDailyPatrolDetail dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolDetail();
            return dal.GetDailyPatrolDetail(ItemID);
        }
        public void InsertDailyPatrolDetail(DailyPatrolDetailInfo model)
        {
            IDailyPatrolDetail dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolDetail();
            dal.InsertDailyPatrolDetail(model);
        }
        public void UpdateDailyPatrolDetail(DailyPatrolDetailInfo model)
        {
            IDailyPatrolDetail dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolDetail();
            dal.UpdateDailyPatrolDetail(model);
        }
        public void DelDailyPatrolDetail(long ItemID)
        {
            IDailyPatrolDetail dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolDetail();
            dal.DelDailyPatrolDetail(ItemID);
        }
        public QueryParam GenerateSearchTerm(DailyPatrolDetailInfo item)
        {
            IDailyPatrolDetail dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolDetail();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IDailyPatrolDetail dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolDetail();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}