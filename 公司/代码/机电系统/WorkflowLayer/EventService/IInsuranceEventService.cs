using System;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface IInsuranceEventService
    {
        event EventHandler<ExternalDataEventArgs> AppSubmited;
        event EventHandler<ExternalDataEventArgs> Repair;
        event EventHandler<ExternalDataEventArgs> RepairApproved;
        event EventHandler<ExternalDataEventArgs> RepairRejected;
    }
}
