using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class DailyPatrolPlan
    {
        public DailyPatrolPlanInfo GetDailyPatrolPlan(long ID)
        {
            IDailyPatrolPlan dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolPlan();
            return dal.GetDailyPatrolPlan(ID);
        }
        public long InsertDailyPatrolPlan(DailyPatrolPlanInfo model)
        {
            IDailyPatrolPlan dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolPlan();
            return dal.InsertDailyPatrolPlan(model);
        }
        public void UpdateDailyPatrolPlan(DailyPatrolPlanInfo model)
        {
            IDailyPatrolPlan dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolPlan();
            dal.UpdateDailyPatrolPlan(model);
        }
        public void DelDailyPatrolPlan(long ID)
        {
            IDailyPatrolPlan dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolPlan();
            dal.DelDailyPatrolPlan(ID);
        }
        public QueryParam GenerateSearchTerm(DailyPatrolPlanInfo item, string[] WFStates)
        {
            IDailyPatrolPlan dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolPlan();
            return dal.GenerateSearchTerm(item,WFStates);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IDailyPatrolPlan dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolPlan();
            return dal.GetList(searchTerm, out recordCount);
        }
        public QueryParam GenerateSearchTerm(DailyPatrolPlanInfo item)
        {
            IDailyPatrolPlan dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolPlan();
            return dal.GenerateSearchTerm(item);
        }
    }
}
