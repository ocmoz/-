using System;
using System.Collections.Generic;
using System.Workflow .Activities;
using System.ComponentModel;
namespace FM2E.WorkflowLayer
{
    public class SpecialProjectWorkflow: StateMachineWorkflowActivity
    {
        public SpecialProjectWorkflow()
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
                return "SpecialProjectWorkflow";
            }
        }
        static public String TableName
        {
            get
            {
                return "FM2E_SpecialProject";
            }
        }

        static public String NewState
        {
            get
            {
                return "New";
            }
        }
        static public String DraftState
        {
            get
            {
                return "Draft";
            }
        }
        static public String BudgetCheckState
        {
            get
            {
                return "BudgetCheck";
            }
        }
        static public String BudgetApprovalState
        {
            get
            {
                return "BudgetApproval";
            }
        }
        static public String DesignInputState
        {
            get
            {
                return "DesignInput";
            }
        }
        static public String DesignCheckState
        {
            get
            {
                return "DesignCheck";
            }
        }
        static public String CostCheckState
        {
            get
            {
                return "CostCheck";
            }
        }
        static public String ProjectSetupState
        {
            get
            {
                return "ProjectSetup";
            }
        }
        static public String CompanyApprovalState
        {
            get
            {
                return "CompanyApproval";
            }
        }
        static public String MotherCompanyApprovalState
        {
            get
            {
                return "MotherCompanyApproval";
            }
        }
        static public String GroupApprovalState
        {
            get
            {
                return "GroupApproval";
            }
        }
        static public String BidCheckState
        {
            get
            {
                return "BidCheck";
            }
        }
        static public String BiddingState
        {
            get
            {
                return "Bidding";
            }
        }
        static public String Wait4StartState
        {
            get
            {
                return "Wait4Start";
            }
        }
        static public String WorkingState
        {
            get
            {
                return "Working";
            }
        }
        static public String FinishProjectState
        {
            get
            {
                return "FinishProject";
            }
        }
        static public String PassProjectState
        {
            get
            {
                return "PassProject";
            }
        }
        static public String CompleteState
        {
            get
            {
                return "Complete";
            }
        }

        static public String SaveNewEvent
        {
            get
            {
                return "SaveNew";
            }
        }
        static public String SubmitNewEvent
        {
            get
            {
                return "SubmitNew";
            }
        }
        static public String SaveDraftEvent
        {
            get
            {
                return "SaveDraft";
            }
        }
        static public String SubmitDraftEvent
        {
            get
            {
                return "SubmitDraft";
            }
        }
        static public String BudgetCheckPassEvent
        {
            get
            {
                return "BudgetCheckPass";
            }
        }
        static public String BudgetCheckFailedEvent
        {
            get
            {
                return "BudgetCheckFailed";
            }
        }
        static public String BudgetApprovalPassEvent
        {
            get
            {
                return "BudgetApprovalPass";
            }
        }
        static public String BudgetApprovalFailedEvent
        {
            get
            {
                return "BudgetApprovalFailed";
            }
        }
        static public String DesignInputSubmitEvent
        {
            get
            {
                return "DesignInputSubmit";
            }
        }
        static public String ProjectSetupSubmitEvent
        {
            get
            {
                return "ProjectSetupSubmit";
            }
        }
        static public String CompanyApprovalPassEvent
        {
            get
            {
                return "CompanyApprovalPass";
            }
        }
        static public String CompanyApprovalFailedEvent
        {
            get
            {
                return "CompanyApprovalFailed";
            }
        }
        static public String BiddingSubmitEvent
        {
            get
            {
                return "BiddingSubmit";
            }
        }
        static public String StartEvent
        {
            get
            {
                return "Start";
            }
        }
        static public String FinishEvent
        {
            get
            {
                return "Finish";
            }
        }
        static public String PassEvent
        {
            get
            {
                return "Pass";
            }
        }
        static public String CompleteEvent
        {
            get
            {
                return "Complete";
            }
        }
    }
}
