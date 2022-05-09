using System;
using System. Collections. Generic;
using System. Collections;
using System. Text;
using FM2E. IDAL. Workflow;
using FM2E. Model. Workflow;
using FM2E. Model. Utils;
using FM2E. DALFactory;

namespace FM2E. BLL. Workflow
{
    public class WorkflowRole
    {
        public IList GetWorkflowRoleList( String workflowName , QueryParam searchTerm , out int recordCount )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            return dal. GetWorkflowRoleList( workflowName , searchTerm , out recordCount );
        }

        public IList GetWorkflowRoleList(String workflowName)
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            return dal. GetWorkflowRoleList( workflowName );
        }

        public IList GetWorkflowRoleList(String userName, String workflowName)
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            return dal. GetWorkflowRoleList( userName , workflowName );
        }

        public WorkflowRoleInfo GetWorkflowRoleInfo( long id )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            return dal. GetWorkflowRoleInfo( id );
        }

        public void InsertWorkflowRole( WorkflowRoleInfo item )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. InsertWorkflowRole( item );
        }

        public void UpdateWorkflowRoleInfo( WorkflowRoleInfo item )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. UpdateWorkflowRoleInfo( item );
        }

        public void DeleteWorkflowRole( long id )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. DeleteWorkflowRole( id );
        }

        public void UpdateUserWorkflowRole( String userName , String workflowName , List<long> roleIDList )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. UpdateUserWorkflowRole( userName , workflowName , roleIDList );
        }
        public void DeleteUserWorkflowRole( String userName , String workflowName )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. DeleteUserWorkflowRole( userName , workflowName );
        }
        public void DeleteUserWorkflowRole( String userName )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. DeleteUserWorkflowRole( userName );
        }
        public void DeleteUserWorkflowRole( long workflowRoleID )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. DeleteUserWorkflowRole( workflowRoleID );
        }

        public List<UserDelegateInfo> GetUserDelegateList( String chiefUserName )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            return dal. GetUserDelegateList( chiefUserName );
        }

        public UserDelegateInfo GetUserDelegate( String chiefUserName , long workflowRoleID , DateTime currentTime )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            return dal. GetUserDelegate( chiefUserName , workflowRoleID , currentTime );
        }



        public UserDelegateInfo GetUserDelegate(String chiefUserName, DateTime currentTime, string workflowName, string workflowState)
        {
            IWorkflowRole dal = WorkflowAccess.CreateWorkflowRole();
            return dal.GetUserDelegate(chiefUserName, currentTime, workflowName, workflowState);
        }

        public void InsertUserDelegate( String chiefUserName , String delegateUserName , long workflowRoleID , DateTime delegateStartTime , DateTime delegateEndTime )
        {
            IWorkflowRole dal = WorkflowAccess. CreateWorkflowRole( );
            dal. InsertUserDelegate( chiefUserName , delegateUserName , workflowRoleID , delegateStartTime , delegateEndTime );
        }
    }
}
