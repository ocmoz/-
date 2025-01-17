﻿using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E. Model. Workflow
{
    public class WorkflowInstanceInfo
    {
        public WorkflowInstanceInfo( Guid instanceID , String currentStateName,String nextUserName,String delegateUserName )
        {
            _instanceID = instanceID;
            _currentStateName = currentStateName;
            _nextUserName = nextUserName;
            _delegateUserName = delegateUserName;
        }
        Guid _instanceID;
        public Guid InstanceID
        {
            get
            {
                return _instanceID;
            }
        }
        String _currentStateName;

        public String CurrentStateName
        {
            get
            {
                return _currentStateName;
            }
        }
        String _nextUserName;

        public String NextUserName
        {
            get
            {
                return _nextUserName;
            }
        }

        String _delegateUserName;

        public String DelegateUserName
        {
            get
            {
                return _delegateUserName;
            }
        }
    }
}
