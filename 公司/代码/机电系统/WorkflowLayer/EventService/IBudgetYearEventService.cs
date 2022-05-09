using System;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface IBudgetYearEventService
    {
        #region ������Workflow Generator 2.0�Զ����ɣ����Ƽ��Ķ�
        event EventHandler<ExternalDataEventArgs> SaveNew;
        event EventHandler<ExternalDataEventArgs> SubmitNew;
        event EventHandler<ExternalDataEventArgs> SaveDraft;
        event EventHandler<ExternalDataEventArgs> SubmitDraft;
        event EventHandler<ExternalDataEventArgs> FinanceReturntoDraft;
        event EventHandler<ExternalDataEventArgs> SubmitFinanceApproval;
        event EventHandler<ExternalDataEventArgs> LeaderReturntoDraft;
        event EventHandler<ExternalDataEventArgs> LeaderReturntoFinance;
        event EventHandler<ExternalDataEventArgs> SubmitLeaderApproval;
        event EventHandler<ExternalDataEventArgs> CompanyReturntoDraft;
        event EventHandler<ExternalDataEventArgs> CompanyReturntoLeader;
        event EventHandler<ExternalDataEventArgs> SubmitCompanySuccess;
        event EventHandler<ExternalDataEventArgs> ToArchinveSuccess;
        void RaiseEvent( String eventName, Guid instanceId );
        #endregion
    }
}
