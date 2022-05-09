using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

using System. Workflow. Activities;
using System. Workflow. Activities. Rules;

namespace FM2E.WorkflowLayer
{
    /// <summary>
    /// 规则数据实体
    /// </summary>
    public class RuleDataInfo
    {
        public RuleDataInfo( )
        {
        }

        public RuleDataInfo( RuleExpressionCondition info )
        {
            _name = info. Name. Substring( 3 );
            _conditionBody = info;
        }
        RuleExpressionCondition _conditionBody;

        String _name;
        public String Name
        {
            get
            {
                return _name;
            }
        }
        public String Description
        {
            get
            {
                return _conditionBody. Expression. UserData[ "display" ] as String;
            }
            set
            {
                _conditionBody. Expression. UserData[ "display" ] = value;
            }
        }
        public bool IsEnum
        {
            get
            {
                return ( bool )_conditionBody. Expression. UserData[ "isEnum" ];
            }
        }
        List<EnumItemInfo> _enumItemList = new List<EnumItemInfo>( );
        public List<EnumItemInfo> EnumItemList
        {
            get
            {
                return _enumItemList;
            }
        }
        public int MaxEnumItemIndex
        {
            get
            {
                if ( IsEnum )
                {
                    return ( int )( _conditionBody. Expression. UserData[ "maxIndex" ] );
                }
                return 0;
            }
            set
            {
                if ( IsEnum )
                {
                    _conditionBody. Expression. UserData[ "maxIndex" ] = value;
                }
            }
        }
    }
    /// <summary>
    /// 枚举项实体
    /// </summary>
    public class EnumItemInfo
    {
        public EnumItemInfo( RuleExpressionCondition condition )
        {
            _condition = condition;
        }

        private RuleExpressionCondition _condition;
        internal String ConditionName
        {
            get
            {
                return _condition. Name;
            }
        }
        public int Order
        {
            get
            {
                return ( int )_condition. Expression. UserData[ "order" ];
            }
            internal set
            {
                _condition. Expression. UserData[ "order" ] = value;
            }
        }
        public int Value
        {
            get
            {
                return ( int )_condition. Expression. UserData[ "value" ];
            }
        }
        public String Description
        {
            get
            {
                return _condition. Expression. UserData[ "display" ] as String;
            }
            internal set
            {
                _condition. Expression. UserData[ "display" ] = value;
            }
        }
    }
    /// <summary>
    /// 规则分支实体
    /// </summary>
    public class IfElseBranchActivityInfo
    {
        public IfElseBranchActivityInfo( IfElseBranchActivity b , SetStateActivity s )
        {
            _ifElseBranchActivityBody = b;
            _setStateActivityBody = s;
        }

        IfElseBranchActivity _ifElseBranchActivityBody;
        SetStateActivity _setStateActivityBody;

        public IfElseBranchActivity IfElseBranchActivityBody
        {
            get
            {
                return _ifElseBranchActivityBody;
            }
        }
        public SetStateActivity SetStateActivityBody
        {
            get
            {
                return _setStateActivityBody;
            }
        }
        public String ExpString
        {
            get
            {
                return _ifElseBranchActivityBody. Description;
            }
        }
    }
    /// <summary>
    /// 工作流状态实体
    /// </summary>
    public class WorkflowStateInfo
    {
        public WorkflowStateInfo( String name , String description )
        {
            _name = name;
            _description = description;
        }

        String _name;
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        String _description;
        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
    }
    /// <summary>
    /// 工作流描述更改记录信息的实体
    /// </summary>
    public class WorkflowStateChangeInfo
    {
        public WorkflowStateChangeInfo(String stateName, String oldDescription , String newDescription )
        {
            stateName = _stateName;
            _oldDescription = oldDescription;
            _newDescription = newDescription ;
        }

        String _stateName;
        public String StateName
        {
            get
            {
                return _stateName;
            }
            set
            {
                _stateName = value;
            }
        }

        String _oldDescription;
        public String OldDescription
        {
            get
            {
                return _oldDescription;
            }
            set
            {
                _oldDescription = value;
            }
        }
        String _newDescription;
        public String NewDescription
        {
            get
            {
                return _newDescription;
            }
            set
            {
                _newDescription = value;
            }
        }
    }
    /// <summary>
    /// 工作流事件实体
    /// </summary>
    public class WorkflowEventInfo
    {
        public WorkflowEventInfo( String name , String description )
        {
            _name = name;
            _description = description;
        }

        String _name;
        public String Name
        {
            get
            {
                return _name;
            }
            internal set
            {
                _name = value;
            }
        }
        String _description;
        public String Description
        {
            get
            {
                return _description;
            }
            internal set
            {
                _description = value;
            }
        }
    }
    /// <summary>
    /// 工作流类信息实体
    /// </summary>
    public class WorkflowClassInfo
    {
        String _name;
        public String Name
        {
            get
            {
                return _name;
            }
            internal set
            {
                _name = value;
            }
        }

        String _description;
        public String Description
        {
            get
            {
                return _description;
            }
            internal set
            {
                _description = value;
            }
        }

        bool _hasRule;
        public bool HasRule
        {
            get
            {
                return _hasRule;
            }
            internal set
            {
                _hasRule = value;
            }
        }

        String _initialStateName;
        public String InitialStateName
        {
            get
            {
                return _initialStateName;
            }
            internal set
            {
                _initialStateName = value;
            }
        }

        String _completedStateName;
        public String CompletedStateName
        {
            get
            {
                return _completedStateName;
            }
            internal set
            {
                _completedStateName = value;
            }
        }
    }

    #region 自定义工作流异常
    public class WorkflowException : ApplicationException
    {
        public WorkflowException( String message , String data )
            : base( message )
        {
            WorkflowData = data;
        }

        public String WorkflowData
        {
            get;
            private set;
        }
    }
    #endregion
}
