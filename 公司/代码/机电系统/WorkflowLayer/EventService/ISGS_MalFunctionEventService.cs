using System;
using System.Workflow.Activities;

namespace FM2E.WorkflowLayer
{
    [ExternalDataExchange]
    public interface ISGS_MalFunctionEventService
    {

        event EventHandler<ExternalDataEventArgs> SubmitRegister;

        event EventHandler<ExternalDataEventArgs> MaintainDeptManagerAccepted;
        event EventHandler<ExternalDataEventArgs> MaintainDeptManagerRejectedAndReturnModify;

        event EventHandler<ExternalDataEventArgs> ElectDeptEngineerAccepted;
        event EventHandler<ExternalDataEventArgs> ElectDeptEngineerRejected;
        event EventHandler<ExternalDataEventArgs> ElectDeptEngineerPassed;//  [4/9/2012 L]

        /// <summary>
        /// 等待维护员确定
        /// </summary>
        event EventHandler<ExternalDataEventArgs> MaintainMaintenanceAccepted;
        event EventHandler<ExternalDataEventArgs> MaintainMaintenanceRejectedAndReturnModify;

        /// <summary>
        /// 等待值班站长确定
        /// </summary>
        event EventHandler<ExternalDataEventArgs> DutyStationAccepted;
        event EventHandler<ExternalDataEventArgs> DutyStationRejectedAndReturnModify;

        event EventHandler<ExternalDataEventArgs> ElectDeptManagerConfirmAccepted;
        event EventHandler<ExternalDataEventArgs> ElectDeptManagerConfirmRejected;
        event EventHandler<ExternalDataEventArgs> ElectDeptManagerConfirmBackToJiliang;//4-22-L

        event EventHandler<ExternalDataEventArgs> ElectDeptSeniorManagerAccepted;
        event EventHandler<ExternalDataEventArgs> ElectDeptSeniorManagerRejected;

        event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerAccepted;
        event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected;
        event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerPass;

        //event EventHandler<ExternalDataEventArgs> ElectDeptAffairManagerAccepted;//  [4/6/2012 L]
        //event EventHandler<ExternalDataEventArgs> ElectDeptAffairManagerRejected;//  [4/6/2012 L]

        event EventHandler<ExternalDataEventArgs> SubmitReturnModify;

        event EventHandler<ExternalDataEventArgs> SubmitNewApprovel;


        //预留事件
        //Manager A
        event EventHandler<ExternalDataEventArgs> ManagerAAccepted;
        event EventHandler<ExternalDataEventArgs> ManagerARejected;

        //Manager B
        event EventHandler<ExternalDataEventArgs> ManagerBAccepted;
        event EventHandler<ExternalDataEventArgs> ManagerBRejected;

        //Manager C
        event EventHandler<ExternalDataEventArgs> ManagerCAccepted;
        event EventHandler<ExternalDataEventArgs> ManagerCRejected;

        //Manager D
        event EventHandler<ExternalDataEventArgs> ManagerDAccepted;
        event EventHandler<ExternalDataEventArgs> ManagerDRejected;

        event EventHandler<ExternalDataEventArgs> GeneralManagerConfirm1;
        event EventHandler<ExternalDataEventArgs> GeneralManagerConfirm2;
        event EventHandler<ExternalDataEventArgs> GeneralManagerConfirm3;
        event EventHandler<ExternalDataEventArgs> GeneralManagerPass1;
        event EventHandler<ExternalDataEventArgs> GeneralManagerPass2;
        event EventHandler<ExternalDataEventArgs> GeneralManagerPass3;
        event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected1;
        event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected2;
        event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected3;
    }
}
