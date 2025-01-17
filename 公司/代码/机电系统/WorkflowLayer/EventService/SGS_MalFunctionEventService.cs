using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class SGS_MalFunctionEventService : ISGS_MalFunctionEventService, IBasicEventService
    {
        public void RaiseEvent( String eventName, Guid instanceID)
        {
            EventHandler<ExternalDataEventArgs> handler = _eventList [eventName ];
            if (handler != null)
                handler(null, new ExternalDataEventArgs(instanceID));
        }

        Dictionary<String, EventHandler<ExternalDataEventArgs>> _eventList = new Dictionary<string, EventHandler<ExternalDataEventArgs>>();


        public event EventHandler<ExternalDataEventArgs> SubmitRegister
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitRegister", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> MaintainDeptManagerAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("MaintainDeptManagerAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> MaintainDeptManagerRejectedAndReturnModify
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("MaintainDeptManagerRejectedAndReturnModify", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> MaintainMaintenanceAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("MaintainMaintenanceAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> MaintainMaintenanceRejectedAndReturnModify
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("MaintainMaintenanceRejectedAndReturnModify", value);
            }
            remove
            {
            }
        }


        public event EventHandler<ExternalDataEventArgs> DutyStationAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("DutyStationAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> DutyStationRejectedAndReturnModify
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("DutyStationRejectedAndReturnModify", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptEngineerAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptEngineerAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptEngineerPassed
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptEngineerPassed", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptEngineerRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptEngineerRejected", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptManagerConfirmAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptManagerConfirmAccepted", value);
            }
            remove
            {
            }
        }


        public event EventHandler<ExternalDataEventArgs> ElectDeptManagerConfirmBackToJiliang
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptManagerConfirmBackToJiliang", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ElectDeptManagerConfirmRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptManagerConfirmRejected", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptSeniorManagerAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptSeniorManagerAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptSeniorManagerRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptSeniorManagerRejected", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptGeneralManagerAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptGeneralManagerRejected", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> SubmitReturnModify
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitReturnModify", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> SubmitNewApprovel
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitNewApprovel", value);
            }
            remove
            {
            }
        }


        //Ԥ���¼�
        //Manager A
        public event EventHandler<ExternalDataEventArgs> ManagerAAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerAAccepted", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ManagerARejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerARejected", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerPass
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptGeneralManagerPass", value);
            }
            remove
            {
            }
        }
        //Manager B
        public event EventHandler<ExternalDataEventArgs> ManagerBAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerBAccepted", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ManagerBRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerBRejected", value);
            }
            remove
            {
            }
        }

        //Manager C
        public event EventHandler<ExternalDataEventArgs> ManagerCAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerCAccepted", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ManagerCRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerCRejected", value);
            }
            remove
            {
            }
        }

        //Manager D
        public event EventHandler<ExternalDataEventArgs> ManagerDAccepted
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerDAccepted", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ManagerDRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerDRejected", value);
            }
            remove
            {
            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public event EventHandler<ExternalDataEventArgs> GeneralManagerConfirm1
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("GeneralManagerConfirm1", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> GeneralManagerConfirm2
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("GeneralManagerConfirm2", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> GeneralManagerConfirm3
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("GeneralManagerConfirm3", value);
            }
            remove
            {
            }
        }

        
        public event EventHandler<ExternalDataEventArgs> GeneralManagerPass1
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("GeneralManagerPass1", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> GeneralManagerPass2
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("GeneralManagerPass2", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> GeneralManagerPass3
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("GeneralManagerPass3", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected1
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptGeneralManagerRejected1", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected2
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptGeneralManagerRejected2", value);
            }
            remove
            {
            }
        }
              public event EventHandler<ExternalDataEventArgs> ElectDeptGeneralManagerRejected3
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptGeneralManagerRejected3", value);
            }
            remove
            {
            }
        }
    }
}
