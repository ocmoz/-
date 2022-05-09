using System;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface IMalFunctionEventService
    {
        event EventHandler<ExternalDataEventArgs> AppSubmited;
        event EventHandler<ExternalDataEventArgs> SupervisorAccepted;
        event EventHandler<ExternalDataEventArgs> SupervisorRejected;
        event EventHandler<ExternalDataEventArgs> ElectDeptAccepted;
        event EventHandler<ExternalDataEventArgs> ApprovalAccepted;
        event EventHandler<ExternalDataEventArgs> ApprovalFailedAndReturn;
        event EventHandler<ExternalDataEventArgs> ApprovalFailedAndBreak;
        event EventHandler<ExternalDataEventArgs> Maintain;
        event EventHandler<ExternalDataEventArgs> Accepted;
        event EventHandler<ExternalDataEventArgs> AcceptanceFailedAndContinue;
        event EventHandler<ExternalDataEventArgs> AcceptanceFailedAndBreak;
        event EventHandler<ExternalDataEventArgs> Confirmed;
        event EventHandler<ExternalDataEventArgs> ConfirmFailedAndContinue;
        event EventHandler<ExternalDataEventArgs> ConfirmFailedAndBreak;
    }
}
