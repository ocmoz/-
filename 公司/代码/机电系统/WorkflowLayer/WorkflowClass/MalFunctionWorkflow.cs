using System;
using System.Collections.Generic;
using System.Workflow .Activities;
using System.ComponentModel;
namespace FM2E.WorkflowLayer
{
    public class MalFunctionWorkflow: StateMachineWorkflowActivity
    {
        public MalFunctionWorkflow()
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
                return "MalFunctionWorkflow";
            }
        }
        static public String TableName
        {
            get
            {
                return "FM2E_MalfunctionHandle";
            }
        }

        static public String WaitSubmintState
        {
            get
            {
                return "WaitSubmint";
            }
        }
        static public String Waiting4SupervisorConfirmState
        {
            get
            {
                return "Waiting4SupervisorConfirm";
            }
        }
        static public String Waiting4ElectDeptConfirmState
        {
            get
            {
                return "Waiting4ElectDeptConfirm";
            }
        }
        static public String Waiting4ApprovalResultState
        {
            get
            {
                return "Waiting4ApprovalResult";
            }
        }
        static public String Waiting4MaintainResultState
        {
            get
            {
                return "Waiting4MaintainResult";
            }
        }
        static public String Waiting4AcceptanceState
        {
            get
            {
                return "Waiting4Acceptance";
            }
        }
        static public String Waiting4ConfirmState
        {
            get
            {
                return "Waiting4Confirm";
            }
        }
        static public String FinishedNormalState
        {
            get
            {
                return "FinishedNormal";
            }
        }
        static public String FinishedUnNormalState
        {
            get
            {
                return "FinishedUnNormal";
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
        static public String SupervisorAcceptedEvent
        {
            get
            {
                return "SupervisorAccepted";
            }
        }
        static public String SupervisorRejectedEvent
        {
            get
            {
                return "SupervisorRejected";
            }
        }
        static public String ElectDeptAcceptedEvent
        {
            get
            {
                return "ElectDeptAccepted";
            }
        }
        static public String ApprovalAcceptedEvent
        {
            get
            {
                return "ApprovalAccepted";
            }
        }
        static public String ApprovalFailedAndReturnEvent
        {
            get
            {
                return "ApprovalFailedAndReturn";
            }
        }
        static public String ApprovalFailedAndBreakEvent
        {
            get
            {
                return "ApprovalFailedAndBreak";
            }
        }
        static public String MaintainEvent
        {
            get
            {
                return "Maintain";
            }
        }
        static public String AcceptedEvent
        {
            get
            {
                return "Accepted";
            }
        }
        static public String AcceptanceFailedAndContinueEvent
        {
            get
            {
                return "AcceptanceFailedAndContinue";
            }
        }
        static public String AcceptanceFailedAndBreakEvent
        {
            get
            {
                return "AcceptanceFailedAndBreak";
            }
        }
        static public String ConfirmedEvent
        {
            get
            {
                return "Confirmed";
            }
        }
        static public String ConfirmFailedAndContinueEvent
        {
            get
            {
                return "ConfirmFailedAndContinue";
            }
        }
        static public String ConfirmFailedAndBreakEvent
        {
            get
            {
                return "ConfirmFailedAndBreak";
            }
        }
    }
}
