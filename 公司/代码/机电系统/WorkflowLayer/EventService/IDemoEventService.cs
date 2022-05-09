using System;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface IDemoEventService
    {
        #region 以下由Workflow Generator 2.0自动生成，不推荐改动
        event EventHandler<ExternalDataEventArgs> AppSubmitted;
        event EventHandler<ExternalDataEventArgs> FinanceApproved1;
        event EventHandler<ExternalDataEventArgs> FinanceApproved2;
        event EventHandler<ExternalDataEventArgs> FinanceApproved3;
        event EventHandler<ExternalDataEventArgs> FinanceRejected1;
        event EventHandler<ExternalDataEventArgs> FinanceRejected2;
        event EventHandler<ExternalDataEventArgs> FinanceRejected3;
        event EventHandler<ExternalDataEventArgs> FinanceCountersigned;
        event EventHandler<ExternalDataEventArgs> ManagerApproved;
        event EventHandler<ExternalDataEventArgs> ManagerRejected;
        event EventHandler<ExternalDataEventArgs> _EstimationEvent_1;
        event EventHandler<ExternalDataEventArgs> _EstimationEvent_2;
        void RaiseEvent( String eventName, Guid instanceId );
        #endregion
    }
}
