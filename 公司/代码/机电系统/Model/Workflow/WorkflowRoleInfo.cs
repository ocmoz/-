using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Workflow
{
    /// <summary>
    /// 工作流角色实体
    /// </summary>
    public class WorkflowRoleInfo
    {
        #region Model
        private long _workflowRoleID;
        private string _workflowName;
        private string _roleName;
        private List<String> _bindingStates;
        private bool _isSingle;
        private bool _isApprover;
        
        public long WorkflowRoleID
        {
            set
            {
                _workflowRoleID = value;
            }
            get
            {
                return _workflowRoleID;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowName
        {
            set { _workflowName = value; }
            get { return _workflowName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _roleName = value; }
            get { return _roleName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<String> BindingStates
        {
            set { _bindingStates = value; }
            get { return _bindingStates; }
        }
        /// <summary>
        /// 限制长度的显示字符串
        /// </summary>
        public String BindingStatesDisplay
        {
            get
            {
                StringBuilder sb = new StringBuilder( 64 );
                foreach (String s in _bindingStates)
                {
                    if ( sb. Length < 64 )
                        sb. Append( s );
                    else
                        break;
                }
                return sb. ToString( );
            }
        }
        public bool IsSingle
        {
            get
            {
                return _isSingle;
            }
            set
            {
                _isSingle = value;
            }
        }
        public bool IsApprover
        {
            get
            {
                return _isApprover;
            }
            set
            {
                _isApprover = value;
            }
        }

        //A temporary property
        public String WorkflowDescription
        {
            get;
            set;
        }
        #endregion Model
    }
}
