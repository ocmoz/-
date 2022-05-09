using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.System
{
    public class RoleForWorkflowInfo
    {
        #region Model
        private long _id;
        private long _roleid;
        private string _workflowid;
        private string _workflowrole;
        private string _workflowdescription;
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowID
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowRole
        {
            set { _workflowrole = value; }
            get { return _workflowrole; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowDescription
        {
            set { _workflowdescription = value; }
            get { return _workflowdescription; }
        }
        #endregion Model
    }
}
