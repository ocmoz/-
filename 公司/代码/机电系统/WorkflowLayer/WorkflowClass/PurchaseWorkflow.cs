using System;
using System.Collections.Generic;
using System.Workflow .Activities;
using System.ComponentModel;

namespace FM2E.WorkflowLayer
{
    public class PurchaseWorkflow: StateMachineWorkflowActivity
    {
        #region 以下由Workflow Generator 2.0自动生成，不推荐改动
        public PurchaseWorkflow()
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
                return "PurchaseWorkflow";
            }
        }
        static public String TableName
        {
            get
            {
                return "FM2E_PurchasePlan";
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
        static public String ApprovalState
        {
            get
            {
                return "Approval";
            }
        }
        static public String ReturnModifyState
        {
            get
            {
                return "ReturnModify";
            }
        }
        static public String Wait4PurchaseState
        {
            get
            {
                return "Wait4Purchase";
            }
        }
        static public String ApprovalFailedState
        {
            get
            {
                return "ApprovalFailed";
            }
        }
        static public String InWarehouseFinishState
        {
            get
            {
                return "InWarehouseFinish";
            }
        }
        static public String ArchiveState
        {
            get
            {
                return "Archive";
            }
        }
        static public String EndState
        {
            get
            {
                return "End";
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
        static public String DoApprovalFailedEvent
        {
            get
            {
                return "DoApprovalFailed";
            }
        }
        static public String ApprovalReturnModifyEvent
        {
            get
            {
                return "ApprovalReturnModify";
            }
        }
        static public String LeaderApprovalEvent
        {
            get
            {
                return "LeaderApproval";
            }
        }
        static public String ApprovalPassEvent
        {
            get
            {
                return "ApprovalPass";
            }
        }
        static public String SubmitReturnModifyEvent
        {
            get
            {
                return "SubmitReturnModify";
            }
        }
        static public String FinishEvent
        {
            get
            {
                return "Finish";
            }
        }
        static public String ToArchinveFailedEvent
        {
            get
            {
                return "ToArchinveFailed";
            }
        }
        static public String ToArchinveSuccessEvent
        {
            get
            {
                return "ToArchinveSuccess";
            }
        }

        #endregion
    }
}
