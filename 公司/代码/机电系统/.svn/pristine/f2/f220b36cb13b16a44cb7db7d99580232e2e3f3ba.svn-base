using System;
using System.Collections.Generic;
using System.Workflow .Activities;
using System.ComponentModel;
namespace FM2E.WorkflowLayer
{
    public class InsuranceWorkflow: StateMachineWorkflowActivity
    {
        public InsuranceWorkflow()
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
                return "ScrapWorkflow";
            }
        }
        static public String TableName
        {
            get
            {
                return "FM2E_Insurance";
            }
        }

        static public String WaitSubmintState
        {
            get
            {
                return "WaitSubmint";
            }
        }
        static public String WaitRepairState
        {
            get
            {
                return "WaitRepair";
            }
        }
        static public String WaitReviewState
        {
            get
            {
                return "WaitReview";
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
        static public String RepairEvent
        {
            get
            {
                return "Repair";
            }
        }
        static public String RepairApprovedEvent
        {
            get
            {
                return "RepairApproved";
            }
        }
        static public String RepairRejectedEvent
        {
            get
            {
                return "RepairRejected";
            }
        }
    }
}
