using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface ISGS_InWarehouseEventService
    {
        event EventHandler<ExternalDataEventArgs> AppSubmited;
        event EventHandler<ExternalDataEventArgs> EngineerApproved;
        event EventHandler<ExternalDataEventArgs> EngineerRejected;
        event EventHandler<ExternalDataEventArgs> ManagerApproved;
        event EventHandler<ExternalDataEventArgs> RetuenEngineer;
        event EventHandler<ExternalDataEventArgs> ManagerRejected;
        event EventHandler<ExternalDataEventArgs> WarehouseKeeperApproved;
        event EventHandler<ExternalDataEventArgs> SubmitReturnModify;
    }
}
