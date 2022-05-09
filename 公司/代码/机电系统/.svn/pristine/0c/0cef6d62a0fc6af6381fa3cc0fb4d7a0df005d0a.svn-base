using System;
using System.Collections.Generic;
using System.Workflow .Activities;
using System.ComponentModel;
namespace FM2E.WorkflowLayer
{
    public class ArchivesDestroyWorkflow: StateMachineWorkflowActivity
    {
        public ArchivesDestroyWorkflow()
            : base()
        {
        }
        public Guid InstanceId
        {
            get;
            set;
        }

        static public String WorkflowName
        {
            get
            {
                return "ArchivesDestroyWorkflow";
            }
        }
        static public String TableName
        {
            get
            {
                return "FM2E_ArchivesDestroyApply";
            }
        }

        static public String WaitSubmintState
        {
            get
            {
                return "WaitSubmint";
            }
        }
        static public String WaitManagerApproveState
        {
            get
            {
                return "WaitManagerApprove";
            }
        }
        static public String AppRejectedState
        {
            get
            {
                return "AppRejected";
            }
        }
        static public String AppApprovedState
        {
            get
            {
                return "AppApproved";
            }
        }
        static public String EndState
        {
            get
            {
                return "End";
            }
        }

        static public String AppSubmitedEvent
        {
            get
            {
                return "AppSubmited";
            }
        }
        static public String ManagerApprovedEvent
        {
            get
            {
                return "ManagerApproved";
            }
        }
        static public String ManagerRejectedEvent
        {
            get
            {
                return "ManagerRejected";
            }
        }
    }
}
