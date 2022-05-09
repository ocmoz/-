using System;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface ISpecialProjectEventService
    {
        event EventHandler<ExternalDataEventArgs> SaveNew;
        event EventHandler<ExternalDataEventArgs> SubmitNew;
        event EventHandler<ExternalDataEventArgs> SaveDraft;
        event EventHandler<ExternalDataEventArgs> SubmitDraft;
        event EventHandler<ExternalDataEventArgs> BudgetCheckPass;
        event EventHandler<ExternalDataEventArgs> BudgetCheckFailed;
        event EventHandler<ExternalDataEventArgs> BudgetApprovalPass;
        event EventHandler<ExternalDataEventArgs> BudgetApprovalFailed;
        event EventHandler<ExternalDataEventArgs> DesignInputSubmit;
        event EventHandler<ExternalDataEventArgs> ProjectSetupSubmit;
        event EventHandler<ExternalDataEventArgs> CompanyApprovalPass;
        event EventHandler<ExternalDataEventArgs> CompanyApprovalFailed;
        event EventHandler<ExternalDataEventArgs> BiddingSubmit;
        event EventHandler<ExternalDataEventArgs> Start;
        event EventHandler<ExternalDataEventArgs> Finish;
        event EventHandler<ExternalDataEventArgs> Pass;
        event EventHandler<ExternalDataEventArgs> Complete;
    }
}
