using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class RoutineInspectPlan
    {
        public RoutineInspectPlanInfo GetRoutineInspectPlan(long ID)
        {
            IRoutineInspectPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectPlan();
            return dal.GetRoutineInspectPlan(ID);
        }
        public long InsertRoutineInspectPlan(RoutineInspectPlanInfo model)
        {
            IRoutineInspectPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectPlan();
            return dal.InsertRoutineInspectPlan(model);
        }
        public void UpdateRoutineInspectPlan(RoutineInspectPlanInfo model)
        {
            IRoutineInspectPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectPlan();
            dal.UpdateRoutineInspectPlan(model);
        }
        public void DelRoutineInspectPlan(long ID)
        {
            IRoutineInspectPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectPlan();
            dal.DelRoutineInspectPlan(ID);
        }
        public QueryParam GenerateSearchTerm(RoutineInspectPlanInfo item, string[] WFStates)
        {
            IRoutineInspectPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectPlan();
            return dal.GenerateSearchTerm(item,WFStates);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineInspectPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectPlan();
            return dal.GetList(searchTerm, out recordCount);
        }
        public QueryParam GenerateSearchTerm(RoutineInspectPlanInfo item)
        {
            IRoutineInspectPlan dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectPlan();
            return dal.GenerateSearchTerm(item);
        }
    }
}
