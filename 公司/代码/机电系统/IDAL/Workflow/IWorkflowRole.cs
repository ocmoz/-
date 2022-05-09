using System;
using System. Collections;
using System. Collections. Generic;
using System. Text;
using FM2E. Model. Utils;
using FM2E. Model. Workflow;

namespace FM2E. IDAL. Workflow
{
    public interface IWorkflowRole
    {
        IList GetWorkflowRoleList( String workflowName , QueryParam searchTerm , out int recordCount );
        IList GetWorkflowRoleList( String workflowName );
        IList GetWorkflowRoleList( String userName , String workflowName );
        WorkflowRoleInfo GetWorkflowRoleInfo( long id );
        void InsertWorkflowRole( WorkflowRoleInfo item );
        void UpdateWorkflowRoleInfo( WorkflowRoleInfo item );
        void DeleteWorkflowRole( long id );

        void UpdateUserWorkflowRole( String userName , String workflowName , List<long> roleIDList );
        void DeleteUserWorkflowRole( String userName , String workflowName );
        void DeleteUserWorkflowRole( String userName );
        void DeleteUserWorkflowRole( long workflowRoleID );

        List<UserDelegateInfo> GetUserDelegateList( String chiefUserName );
        UserDelegateInfo GetUserDelegate( String chiefUserName , long workflowRoleID , DateTime currentTime );
        UserDelegateInfo GetUserDelegate(String chiefUserName, DateTime currentTime, string workflowName, string workflowState);
        void InsertUserDelegate( String chiefUserName , String delegateUserName , long workflowRoleID , DateTime delegateStartTime , DateTime delegateEndTime );
    }
}
