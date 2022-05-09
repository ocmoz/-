using System;
using System.Collections;
using System.Text;

using FM2E.Model.System;

namespace FM2E.IDAL.System
{
    public interface IRoleForWorkflow
    {
        IList GetList(long RoleID,int pageindex, out int recordCount);
        void InsertRoleForWorkflow(ArrayList Rolelist);
        RoleForWorkflowInfo GetRoleForWorkflow(long RoleForWorkflowID);
        void UpdateRoleForWorkflow(RoleForWorkflowInfo model);
        void DelRoleForWorkflow(long RoleForWorkflowID);
        IList GetAllWorkflow();
        IList GetAllRolesByWorkflow(string WorkflowID);
        IList GetRoleListForUser(string UserName, string WorkflowID);
    }
}
