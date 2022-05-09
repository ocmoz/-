using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class RoutineMaintainPlan
    {
        public RoutineMaintainPlanInfo GetRoutineMaintainPlan(long ID)
        {
            IRoutineMaintainPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainPlan();
            return dal.GetRoutineMaintainPlan(ID);
        }
        public long InsertRoutineMaintainPlan(RoutineMaintainPlanInfo model)
        {
            IRoutineMaintainPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainPlan();
            return dal.InsertRoutineMaintainPlan(model);
        }
        public void UpdateRoutineMaintainPlan(RoutineMaintainPlanInfo model)
        {
            IRoutineMaintainPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainPlan();
            dal.UpdateRoutineMaintainPlan(model);
        }
        public void DelRoutineMaintainPlan(long ID)
        {
            IRoutineMaintainPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainPlan();
            dal.DelRoutineMaintainPlan(ID);
        }
        public QueryParam GenerateSearchTerm(RoutineMaintainPlanInfo item, string[] WFStates)
        {
            IRoutineMaintainPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainPlan();
            return dal.GenerateSearchTerm(item,WFStates);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineMaintainPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainPlan();
            return dal.GetList(searchTerm, out recordCount);
        }
        public QueryParam GenerateSearchTerm(RoutineMaintainPlanInfo item)
        {
            IRoutineMaintainPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainPlan();
            return dal.GenerateSearchTerm(item);
        }
    }
}
