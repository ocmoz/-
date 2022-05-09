using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.System
{
    public class RolePermissionInfo
    {
        private long _roleid;
        private string _moduleid;
        private int _permission;

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
        public string ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Permission
        {
            set { _permission = value; }
            get { return _permission; }
        }
    }
}
