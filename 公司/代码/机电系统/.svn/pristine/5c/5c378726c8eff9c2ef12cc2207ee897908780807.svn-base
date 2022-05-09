using System;
using System.Collections.Generic;
using System.Text;

using FM2E.IDAL.System;
using FM2E.Model.System;
using System.Collections;

namespace FM2E.BLL.System
{
    public class RoleForWorkflow
    {
        public IList GetList(long RoleID,int pageindex, out int recordCount)
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            return dal.GetList(RoleID,pageindex,out recordCount);
        }
        public void InsertRoleForWorkflow(ArrayList Rolelist)
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            dal.InsertRoleForWorkflow(Rolelist);
        }
        public void UpdateRoleForWorkflow(RoleForWorkflowInfo model)
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            dal.UpdateRoleForWorkflow(model);
        }
        public void DelRoleForWorkflow(long RoleForWorkflowID)
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            dal.DelRoleForWorkflow(RoleForWorkflowID);
        }
        public RoleForWorkflowInfo GetRoleForWorkflow(long RoleForWorkflowID)
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            return dal.GetRoleForWorkflow(RoleForWorkflowID);
        }
        public IList GetAllWorkflow()
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            return dal.GetAllWorkflow();
        }
        public IList GetAllRolesByWorkflow(string WorkflowID)
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            return dal.GetAllRolesByWorkflow(WorkflowID);
        }
        public IList GetRoleListForUser(string UserName,string WorkflowID)
        {
            IRoleForWorkflow dal = FM2E.DALFactory.SystemAccess.CreateRoleForWorkflow();
            return dal.GetRoleListForUser(UserName,WorkflowID);
        }
    }
}
