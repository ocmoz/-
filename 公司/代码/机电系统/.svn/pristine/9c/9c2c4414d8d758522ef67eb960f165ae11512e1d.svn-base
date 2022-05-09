using System;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface IPurchaseEventService
    {
        #region 以下由Workflow Generator 2.0自动生成，不推荐改动
        event EventHandler<ExternalDataEventArgs> SaveNew;
        event EventHandler<ExternalDataEventArgs> SubmitNew;
        event EventHandler<ExternalDataEventArgs> SaveDraft;
        event EventHandler<ExternalDataEventArgs> SubmitDraft;
        event EventHandler<ExternalDataEventArgs> DoApprovalFailed;
        event EventHandler<ExternalDataEventArgs> ApprovalReturnModify;
        event EventHandler<ExternalDataEventArgs> LeaderApproval;
        event EventHandler<ExternalDataEventArgs> ApprovalPass;
        event EventHandler<ExternalDataEventArgs> SubmitReturnModify;
        event EventHandler<ExternalDataEventArgs> Finish;
        event EventHandler<ExternalDataEventArgs> ToArchinveFailed;
        event EventHandler<ExternalDataEventArgs> ToArchinveSuccess;
        void RaiseEvent( String eventName, Guid instanceId );
        #endregion
    }
}
