using System;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface IPriceEventService
    {
        event EventHandler<ExternalDataEventArgs> AppSubmited;
        event EventHandler<ExternalDataEventArgs> ManagerApproved;
        event EventHandler<ExternalDataEventArgs> PartApproved;
        event EventHandler<ExternalDataEventArgs> ManagerRejected;
    }
}
