using System;
using System. CodeDom;
using System. Reflection;
using System. IO;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Xml;
using System. Web;

using System. Workflow. Activities;
using System. Workflow. Activities. Rules;
using System. Workflow. ComponentModel;
using System. Workflow. ComponentModel. Serialization;

namespace FM2E.WorkflowLayer
{
    public interface IWorkflowParser
    {
        #region Functions
        void Initialize( XmlReader workflowSource , XmlReader ruleSource );
        List<WorkflowEventInfo> GetEventInfoList( String stateName , bool includeParallel );
        StateActivityInfo GetStateActivityInfoByName( String stateName );
        void ChangeTargetState( String stateName , String eventName , String setStateName , String newTargetName );
        void AddNewRule( String stateName , String eventName , String ruleExp );
        void DeleteOldRule( String stateName , String eventName , String setStateName );
        void ModifyRule( String stateName , String eventName , String setStateName , String ruleExp , bool onlyRecord );
        void ChangeWorkflowDescription( String newDescription );
        void ChangeStateDescription( String stateName , String newDescription );
        void ChangeEventDescription( String eventName , String newDescription );
        String ChangeRuleDataDescription( String ruleDataName , String newDescription );
        bool RemoveEnumItem( String enumName , int itemValue );
        bool AddEnumItem( String enumName , String newItemDisplay );
        String ChangeEnumItemDescription( String enumName , int itemValue , String newDescription );
        void UpdateEnumItemOrder( String enumName );
        #endregion

        #region Properties
        List<WorkflowStateInfo> StateInfoList
        {
            get;
        }

        WorkflowClassInfo WorkflowInfo
        {
            get;
        }

        Dictionary<String , StateActivityInfo> StateActivityCollection
        {
            get;
        }

        StateMachineWorkflowActivity WorkflowDefinition
        {
            get;
        }
        RuleDefinitions RuleDefinition
        {
            get;
        }
        List<RuleDataInfo> DecimalInfoList
        {
            get;
        }
        List<RuleDataInfo> EnumInfoList
        {
            get;
        }
        Dictionary<String , RuleDataInfo> RuleDataInfoCollection
        {
            get;
        }
        List<WorkflowStateChangeInfo> StateChangeRecordList
        {
            get;
        }
        #endregion
    }

    internal interface ISharedContent
    {
        List<HandleExternalEventActivity> HandleEventActivityList
        {
            get;
        }
        List<DelayActivity> DelayActivityList
        {
            get;
        }
        List<WorkflowStateInfo> AllStateInfoList
        {
            get;
        }

    }

    public class WorkflowParser<T> : IWorkflowParser , ISharedContent
        where T : StateMachineWorkflowActivity
    {

        #region Private Variables

        #region 整体信息记录
        T _workflowDefinition = null;
        RuleDefinitions _ruleDefinition = null;
        WorkflowClassInfo _workflowClassInfo = new WorkflowClassInfo( );
        #endregion

        #region 状态事件相关记录
        Dictionary<String , StateActivityInfo> _stateActivityCollection = null;
        List<WorkflowStateInfo> _stateInfoList = new List<WorkflowStateInfo>( );
        List<HandleExternalEventActivity> _handleEventActivityList = new List<HandleExternalEventActivity>( );
        List<DelayActivity> _delayActivityList = new List<DelayActivity>( );
        List<WorkflowStateChangeInfo> _stateChangeRecordList = new List<WorkflowStateChangeInfo>( );
        #endregion

        #region 规则的相关记录
        Dictionary<String , RuleDataInfo> _ruleDataInfos = new Dictionary<string , RuleDataInfo>( );
        List<RuleDataInfo> _decimalList = new List<RuleDataInfo>( );
        List<RuleDataInfo> _enumList = new List<RuleDataInfo>( );
        #endregion

        #region 表达式分析用变量
        String _expString;
        int _scanIndex;
        #endregion
        #endregion

        #region 实现ISharedContent
        public List<HandleExternalEventActivity> HandleEventActivityList
        {
            get
            {
                return _handleEventActivityList;
            }
        }
        public List<DelayActivity> DelayActivityList
        {
            get
            {
                return _delayActivityList;
            }
        }
        public List<WorkflowStateInfo> AllStateInfoList
        {
            get
            {
                return _stateInfoList;
            }
        }
        #endregion

        #region 实现IWorkflowParser（部分）
        public StateActivityInfo GetStateActivityInfoByName( String stateName )
        {
            return _stateActivityCollection[ stateName ];
        }
        public List<WorkflowStateInfo> StateInfoList
        {
            get
            {
                return _stateInfoList;
            }
        }
        public List<WorkflowEventInfo> GetEventInfoList( String stateName , bool includeParallel )
        {
            if ( includeParallel )
                return _stateActivityCollection[ stateName ]. AllEventList;
            else
                return _stateActivityCollection[ stateName ]. AllNonParallelEventList;
        }
        public WorkflowClassInfo WorkflowInfo
        {
            get
            {
                return _workflowClassInfo;
            }
        }
        public StateMachineWorkflowActivity WorkflowDefinition
        {
            get
            {
                return _workflowDefinition;
            }
        }
        public RuleDefinitions RuleDefinition
        {
            get
            {
                return _ruleDefinition;
            }
        }
        public Dictionary<String , StateActivityInfo> StateActivityCollection
        {
            get
            {
                return _stateActivityCollection;
            }
        }
        public List<RuleDataInfo> DecimalInfoList
        {
            get
            {
                return _decimalList;
            }
        }
        public List<RuleDataInfo> EnumInfoList
        {
            get
            {
                return _enumList;
            }
        }
        public Dictionary<String , RuleDataInfo> RuleDataInfoCollection
        {
            get
            {
                return _ruleDataInfos;
            }
        }
        public List<WorkflowStateChangeInfo> StateChangeRecordList
        {
            get
            {
                return _stateChangeRecordList;
            }
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// 初始化WorkflowParser
        /// </summary>
        /// <param name="workflowSource"></param>
        /// <param name="ruleSource">不存在时为null</param>
        public void Initialize( XmlReader workflowSource , XmlReader ruleSource )
        {
            #region 对xml定义进行反序列化，并获取工作流基本信息
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer( );
            if ( ruleSource != null )
            {
                _ruleDefinition = serializer. Deserialize( ruleSource ) as RuleDefinitions;
                _workflowClassInfo. HasRule = true;
            }
            else
            {
                _ruleDefinition = null;
                _workflowClassInfo. HasRule = false;
            }
            _workflowDefinition = serializer. Deserialize( workflowSource ) as T;
            _workflowClassInfo. Name = _workflowDefinition. Name;
            _workflowClassInfo. Description = _workflowDefinition. Description;
            _workflowClassInfo. InitialStateName = _workflowDefinition. InitialStateName;
            _workflowClassInfo. CompletedStateName = _workflowDefinition. CompletedStateName;
            #endregion

            if ( _ruleDefinition != null )
            {
                #region 从规则定义中提取规则数据的信息
                var ruleDataList =
                    from c in _ruleDefinition. Conditions where c. Name. StartsWith( "ba_" ) select c as RuleExpressionCondition;
                foreach ( RuleExpressionCondition rec in ruleDataList )
                {
                    RuleDataInfo rdi = new RuleDataInfo( rec );
                    _ruleDataInfos. Add( rdi. Name , rdi );
                    if ( rdi. IsEnum )     //进一步提取枚举信息
                    {
                        foreach ( RuleExpressionCondition condition in ( from c in _ruleDefinition. Conditions
                                                                         where c. Name. StartsWith( "a_" + rdi. Name )
                                                                         select c as RuleExpressionCondition ) )
                        {
                            rdi. EnumItemList. Add( new EnumItemInfo( condition ) );
                        }
                        rdi.EnumItemList.Sort( CompareEnumItemInfo );
                        _enumList. Add( rdi );
                    }
                    else
                    {
                        _decimalList. Add( rdi );
                    }
                }
                #endregion
            }

            #region 从工作流定义中提取各StateActivity
            _stateActivityCollection = new Dictionary<string , StateActivityInfo>( _workflowDefinition. Activities. Count );
            foreach ( StateActivity s in _workflowDefinition. Activities )
            {
                _stateActivityCollection. Add( s. Name , new StateActivityInfo( s , this ) );
                _stateInfoList. Add( new WorkflowStateInfo( s. Name , s. Description ) );
            }
            #endregion
        }

        #region 工作流定义、规则修改方法集合
        /// <summary>
        /// 改变SetStateActivity的TargetState属性
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="eventName"></param>
        /// <param name="setStateName"></param>
        /// <param name="newTargetName"></param>
        public void ChangeTargetState( String stateName , String eventName , String setStateName , String newTargetName )
        {
            EventDrivenActivityInfo info = _stateActivityCollection[ stateName ]. EventDrivenActivityCollection[ eventName ];
            if ( info. HasRule )
                info. IfElseBranchCollection[ setStateName ]. SetStateActivityBody. TargetStateName = newTargetName;
            else
                info. SingleSetStateActivity. TargetStateName = newTargetName;
        }

        /// <summary>
        /// 改变工作流的描述
        /// </summary>
        /// <param name="newDescription"></param>
        public void ChangeWorkflowDescription( String newDescription )
        {
            _workflowDefinition. Description = newDescription;
            _workflowClassInfo. Description = newDescription;
        }

        /// <summary>
        /// 改变一个状态的描述（需捕捉异常）
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="newDescription"></param>
        public void ChangeStateDescription( String stateName , String newDescription )
        {
            //判断状态描述是否重复
            if ( _stateInfoList. FirstOrDefault( p => p. Description == newDescription && p. Name != stateName ) != null )
                throw new WorkflowException( "名称重复！" , _stateActivityCollection[ stateName ]. StateDescription );

            //将更改记录下来
            WorkflowStateChangeInfo info = 
                _stateChangeRecordList. FirstOrDefault( p => p. StateName == stateName );
            if(info != null)
            {
                info. NewDescription = newDescription;
            }
            else
            {
                _stateChangeRecordList. Add( new WorkflowStateChangeInfo( stateName , _stateActivityCollection[ stateName ]. StateDescription , newDescription ) );
            }

            //改变定义中的描述
            _stateActivityCollection[ stateName ]. StateDescription = newDescription;

            //改变记录中的描述
            WorkflowStateInfo wsi = _stateInfoList. FirstOrDefault( p => p. Name == stateName );
            if ( wsi != null )
                wsi. Description = newDescription;
        }

        /// <summary>
        /// 改变一个事件的描述（需捕捉异常）
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="newDescription"></param>
        public void ChangeEventDescription( String eventName , String newDescription )
        {
            HandleExternalEventActivity heea = _handleEventActivityList. FirstOrDefault( p => p. EventName == eventName );

            //判断事件描述是否重复
            if ( _handleEventActivityList. FirstOrDefault( p => p. Description == newDescription && p. EventName != eventName ) != null )
                throw new WorkflowException( "名称重复！" , heea. Description );

            //改变定义中的描述
            if ( heea != null )
            {
                heea. Description = newDescription;
            }
            else
            {
                //暂时不用
                DelayActivity da = _delayActivityList. FirstOrDefault( p => p. Name == eventName );
                if ( da != null )
                    da. Description = newDescription;
            }

            //改变记录中的描述
            String stateName = ( heea. Parent. Parent as StateActivity ). Name;
            WorkflowEventInfo wei = _stateActivityCollection[ stateName ]. AllEventList. FirstOrDefault( p => p. Name == eventName );
            wei. Description = newDescription;
        }

        /// <summary>
        /// 改变一个规则数据的描述（需捕捉异常）
        /// </summary>
        /// <param name="ruleDataName"></param>
        /// <param name="newDescription"></param>
        /// <returns>原来的规则数据描述</returns>
        public String ChangeRuleDataDescription( String ruleDataName , String newDescription )
        {
            String old = _ruleDataInfos[ ruleDataName ]. Description;

            //判断重复
            if ( _decimalList. FirstOrDefault( p => p. Description == newDescription && p. Name != ruleDataName ) != null || _enumList. FirstOrDefault( p => p. Description == newDescription && p. Name != ruleDataName ) != null )
                throw new WorkflowException( "名称重复！" , old );

            _ruleDataInfos[ ruleDataName ]. Description = newDescription;
            return old;
        }

        /// <summary>
        /// 改变一个枚举项的描述（需捕捉异常）
        /// </summary>
        /// <param name="itemConditionName"></param>
        /// <param name="newDescription"></param>
        public String ChangeEnumItemDescription( String enumName , int itemValue , String newDescription )
        {
            RuleDataInfo enumInfo = _ruleDataInfos[ enumName ];
            EnumItemInfo itemInfo = enumInfo. EnumItemList. FirstOrDefault( p => p. Value == itemValue );

            //判断重复
            if ( enumInfo. EnumItemList. FirstOrDefault( p => p. Description == newDescription && p. Value != itemValue ) != null )
                throw new WorkflowException( "名称重复！" , itemInfo. Description );

            String old = itemInfo. Description;
            itemInfo. Description = newDescription;
            return old;
        }

        /// <summary>
        /// 更新各枚举项的顺序（此函数专为AjaxControlTooltik中的ReorderList控件的行为设计）
        /// </summary>
        /// <param name="enumName"></param>
        public void UpdateEnumItemOrder(String enumName)
        {
            List<EnumItemInfo> itemList = _ruleDataInfos[ enumName ].EnumItemList;
            for(int i = 0; i<itemList.Count; ++i)
            {
                itemList[i].Order = i;
            }
        }

        /// <summary>
        /// EnumItem共被两处记录所引用：_ruleDefinition, 相应Enum中的EnumItemList（需捕捉异常）
        /// </summary>
        /// <param name="enumName"></param>
        /// <param name="itemValue"></param>
        /// <returns>
        /// true: 修改成功
        /// false: 被禁止修改
        /// </returns>
        public bool RemoveEnumItem( String enumName , int itemValue )
        {
            IEnumItems enumItems = _workflowDefinition as IEnumItems;
            if ( enumItems != null )
            {
                if ( enumItems. CanEnumItemDeleted( enumName , itemValue ) )
                {
                    RuleDataInfo rdi = _ruleDataInfos[ enumName ];
                    int index = rdi.EnumItemList. FindIndex( p => p. Value == itemValue );
                    String name = rdi.EnumItemList[ index ]. ConditionName;

                    for ( int i = 0 ; i < _ruleDefinition. Conditions. Count ; ++i )
                    //此处有非常奇怪的bug，直接根据Name或引用本身来Remove会出错，而一定要找到其index用RemoveAt
                    {
                        if ( _ruleDefinition. Conditions[ i ]. Name == name )
                        {
                            _ruleDefinition. Conditions. RemoveAt( i );
                            break;
                        }
                    }

                    rdi.EnumItemList. RemoveAt( index );
                    
                    //如果删除的是value最大项，则更新MaxEnumItemIndex为删除后的最大项
                    if(rdi.MaxEnumItemIndex == itemValue )
                    {
                        if ( rdi. EnumItemList. Count > 0 )
                            rdi. MaxEnumItemIndex = rdi. EnumItemList. Max( p => p. Value );
                        else
                            rdi. MaxEnumItemIndex = 0;
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                throw new WorkflowException( "工作流定义存在错误！" , null );
        }

        /// <summary>
        /// 向一个Enum中添加一项
        /// </summary>
        /// <param name="enumName"></param>
        /// <param name="newItemDisplay"></param>
        /// <returns>false：同名的Item已存在</returns>
        public bool AddEnumItem( String enumName , String newItemDisplay )
        {
            RuleDataInfo rdi = _ruleDataInfos[ enumName ];
            int index = ( ++rdi. MaxEnumItemIndex );
            String newName = "a_" + enumName + index;

            //检查是否重名
            if ( rdi. EnumItemList. FindIndex( p => p. Description == newItemDisplay ) > -1 )
                return false;

            //向_ruleDefinition中添加规则
            RuleExpressionCondition rec = new RuleExpressionCondition( );
            rec. Name = newName;
            CodeExpression record = new CodeExpression( );
            record. UserData[ "display" ] = newItemDisplay;
            record. UserData[ "value" ] = index;
            record. UserData[ "order" ] = rdi. EnumItemList. Count;
            rec. Expression = record;
            _ruleDefinition. Conditions. Add( rec );

            //向相应枚举记录中的EnumItemList中添加新项
            rdi. EnumItemList. Add( new EnumItemInfo( rec ) );
            return true;
        }

        /// <summary>
        /// 添加一个新规则
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="eventDrivenName"></param>
        /// <param name="ruleExp"></param>
        public void AddNewRule( String stateName , String eventName , String ruleExp )
        {
            //解析表达式，一切正确后才进行定义修改
            RuleExpressionCondition newCondition = ParseRuleExp( ruleExp );

            //修改xoml定义
            newCondition. Name =
                _stateActivityCollection[ stateName ]. EventDrivenActivityCollection[ eventName ]. AddNewRule( ruleExp );

            //修改rules定义
            _ruleDefinition. Conditions. Add( newCondition );
        }

        /// <summary>
        /// 删除一个旧规则
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="eventDrivenName"></param>
        /// <param name="setStateName"></param>
        public void DeleteOldRule( String stateName , String eventName , String setStateName )
        {
            //修改xoml定义
            String oldConditionName =
            _stateActivityCollection[ stateName ]. EventDrivenActivityCollection[ eventName ]. DeleteRule( setStateName );

            //修改rules定义
            _ruleDefinition. Conditions. Remove( oldConditionName );
        }

        /// <summary>
        /// 修改一个规则的定义
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="eventDrivenName"></param>
        /// <param name="setStateName"></param>
        /// <param name="ruleExp"></param>
        public void ModifyRule( String stateName , String eventName , String setStateName , String ruleExp , bool onlyRecord )
        {
            if ( !onlyRecord )
            {            //解析表达式，一切正确后才进行定义修改
                RuleExpressionCondition newCondition = ParseRuleExp( ruleExp );
                //修改rules定义
                newCondition. Name = ( _stateActivityCollection[ stateName ]. EventDrivenActivityCollection[ eventName ]. IfElseBranchCollection[ setStateName ]. IfElseBranchActivityBody. Condition as RuleConditionReference ). ConditionName;
                _ruleDefinition. Conditions. Remove( newCondition. Name );
                _ruleDefinition. Conditions. Add( newCondition );
            }
            //修改表达式记录
            _stateActivityCollection[ stateName ]. EventDrivenActivityCollection[ eventName ]. ModifyRule( setStateName , ruleExp );
        }
        #endregion
        #endregion

        #region Private Functions

        //EnumItemInfo的顺序比较器
        int CompareEnumItemInfo(EnumItemInfo left, EnumItemInfo right)
        {
            if ( left. Order == right. Order )
                return 0;
            return left. Order > right. Order ? 1 : -1;
        }

        #region 微型表达式分析器：表达式字符串->RuleExpressionCondition

        /// <summary>
        /// 将表达式字符串解析成等价RuleExpressionCondition
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        RuleExpressionCondition ParseRuleExp( String exp )
        {
            if ( exp == String. Empty )
            {
                RuleExpressionCondition retExp = new RuleExpressionCondition( );
                retExp. Expression = new CodePrimitiveExpression( true );
                return retExp;
            }

            exp = exp. Replace( ' ' , '\0' );                                      //消除所有空格

            _expString = exp;
            _scanIndex = 0;
            CodeBinaryOperatorExpression newBinaryExpression = ParseBoolExp( );
            if ( _scanIndex >= _expString. Length )
            {
                RuleExpressionCondition newCondition = new RuleExpressionCondition( newBinaryExpression );
                return newCondition;
            }
            else
            {
                throw new Exception( "表达式错误：无法识别字符串 \"" + _expString. Substring( _scanIndex ) + "\" !" );
            }
        }
        /// <summary>
        /// 规则表达式LL(1)语法描述：
        ///<BoolExp> → <Exp><BoolOp><BoolExp> | (<BoolExp>) | (<BoolExp>)<BoolOp><BoolExp>|<CmpExp>
        ///<CmpExp> → <Item> <CmpOp> <Item>
        ///<BoolOp> -> && | ||     (注：为了简便，布尔运算不分优先级）
        ///<CmpOp> → > | < | >= | <= | = | ==(与=同义)
        ///<Item> → <RuleDataName> | <EnumItem> | <Decimal>
        ///<RuleDataName> → 规则数据的描述
        ///<EnumItem> → 枚举项的描述
        ///<Decimal> → Decimal类型的数据
        /// 
        /// 规则表达式递归下降分析原则：
        /// (1) 语法、词法和语义分析同步进行
        /// (2) 进入下一层递归前，将位置推进到下一层开始位置
        /// (3) 回到上一层前，将位置推进到下一个符号开始
        /// (4) 解析失败要保证上层的位置不变
        /// </summary>

        /// <summary>
        /// 解析布尔表达式
        /// </summary>
        CodeBinaryOperatorExpression ParseBoolExp( )
        {
            CodeBinaryOperatorExpression retExp;

            //解析括号
            if ( _expString[ _scanIndex ] == '(' )
            {
                ++_scanIndex;
                retExp = ParseBoolExp( );
                if ( _scanIndex >= _expString. Length || _expString[ _scanIndex ] != ')' )
                    throw new Exception( "表达式错误：括号不匹配！" );
                ++_scanIndex;

                //判断是否还有布尔运算符
                CodeBinaryOperatorExpression tmpExp = new CodeBinaryOperatorExpression( );
                if ( ParseBoolOp( tmpExp ) )
                {
                    //有的话，建立更高一层表达式，并继续解析
                    tmpExp. Left = retExp;
                    retExp = tmpExp;
                    retExp. Right = ParseBoolExp( );
                }
                return retExp;
            }

            retExp = new CodeBinaryOperatorExpression( );
            //解析左比较表达式
            retExp. Left = ParseCmpExp( );
            //解析布尔运算符
            if ( ParseBoolOp( retExp ) )
            {
                //解析右比较表达式
                retExp. Right = ParseBoolExp( );
            }
            else
            {
                //对于单独的比较表达式，将之提升到本层次
                retExp = retExp. Left as CodeBinaryOperatorExpression;
            }
            return retExp;
        }
        /// <summary>
        /// 解析布尔运算符
        /// </summary>
        /// <param name="?"></param>
        /// <returns>false: 没有找到bool运算符and或or</returns>
        bool ParseBoolOp( CodeBinaryOperatorExpression binaryExp )
        {
            int end = _scanIndex;
            while ( end < _expString. Length && ( _expString[ end ] == '&' || _expString[ end ] == '|' ) )
            {
                ++end;
            }
            String boolOp = _expString. Substring( _scanIndex , end - _scanIndex );
            if ( boolOp == "&&" )
            {
                binaryExp. Operator = CodeBinaryOperatorType. BooleanAnd;
                _scanIndex = end;
                return true;
            }
            if ( boolOp == "||" )
            {
                binaryExp. Operator = CodeBinaryOperatorType. BooleanOr;
                _scanIndex = end;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 解析比较表达式
        /// </summary>
        /// <returns></returns>
        CodeBinaryOperatorExpression ParseCmpExp( )
        {
            CodeBinaryOperatorExpression retExp = new CodeBinaryOperatorExpression( );
            int leftType;

            //解析左项
            retExp. Left = ParseLeftItem( out leftType );

            //解析中间符号
            ParseComparisonOp( retExp );

            //检测运算符是否匹配数据类型
            if ( leftType == 1 && retExp. Operator != CodeBinaryOperatorType. ValueEquality )
                throw new Exception( "表达式错误：不能对枚举类型使用不等式！" );

            //解析右项
            if ( leftType == 1 )  //枚举类型
            {
                retExp. Right = ParseRightItem( ( retExp. Left as CodePropertyReferenceExpression ). PropertyName );
            }
            else                      //Decimal类型
            {
                retExp. Right = ParseRightItem( null );
            }
            return retExp;
        }
        /// <summary>
        /// 解析左Item
        /// </summary>
        /// <param name="type">
        /// 0: retExp是Decimal类型
        /// 1: retExp是枚举类型
        /// </param>
        /// <returns></returns>
        CodePropertyReferenceExpression ParseLeftItem( out int type )
        {
            int end = _scanIndex;
            while ( end < _expString. Length && ( _expString[ end ] != '=' && _expString[ end ] != '>' && _expString[ end ] != '<' && _expString[ end ] != ')' && _expString[ end ] != '(' && _expString[ end ] != '&' && _expString[ end ] != '|' ) )            //提取数据项
            {
                ++end;
            }
            String itemName = _expString. Substring( _scanIndex , end - _scanIndex );
            CodePropertyReferenceExpression retExp = new CodePropertyReferenceExpression( );
            retExp. TargetObject = new CodeThisReferenceExpression( );
            RuleDataInfo ruleData = _ruleDataInfos. FirstOrDefault( p => p. Value. Description == itemName || p. Value. Name == itemName ). Value as RuleDataInfo;
            if ( ruleData != null )
            {
                type = ruleData. IsEnum ? 1 : 0;
                _scanIndex = end;
                retExp. PropertyName = ruleData. Name;
                return retExp;
            }
            else
            {
                if ( itemName == String. Empty )
                    throw new Exception( "表达式错误：不能出现空项！" );
                else
                    throw new Exception( "表达式错误：工作流定义中没有包含 \"" + itemName + "\" 这项数据！" );
            }
        }
        /// <summary>
        /// 解析右Item
        /// </summary>
        /// <param name="enumName"></param>
        /// <returns></returns>
        CodeExpression ParseRightItem( String enumName )
        {
            int end = _scanIndex;
            if ( enumName == null )
            {
                if ( ( _expString[ end ] >= '0' && _expString[ end ] <= '9' ) )
                //Decimal数值
                {
                    while ( end < _expString. Length && ( ( _expString[ end ] >= '0' && _expString[ end ] <= '9' ) || ( _expString[ end ] == '.' ) ) )
                    {
                        ++end;
                    }
                    String decimalValue = _expString. Substring( _scanIndex , end - _scanIndex );
                    CodePrimitiveExpression retDecimalExp = new CodePrimitiveExpression( );
                    try
                    {
                        retDecimalExp. Value = Decimal. Parse( decimalValue );
                    }
                    catch
                    {
                        if ( decimalValue == String. Empty )
                            throw new Exception( "表达式错误：不等式右侧不能为空" );
                        else
                            throw new Exception( "表达式错误：数值 " + decimalValue + " 无法被解析！" );
                    }
                    _scanIndex = end;
                    return retDecimalExp;
                }
                else      //Decimal变量
                {
                    while ( end < _expString. Length && ( _expString[ end ] != '=' && _expString[ end ] != '>' && _expString[ end ] != '<' && _expString[ end ] != ')' && _expString[ end ] != '(' && _expString[ end ] != '&' && _expString[ end ] != '|' ) )
                    {
                        ++end;
                    }
                    String decimalName = _expString. Substring( _scanIndex , end - _scanIndex );
                    RuleDataInfo ruleData = _decimalList. FirstOrDefault( p => ( p. Description == decimalName || p. Name == decimalName ) );
                    if ( ruleData == null )
                    {
                        if ( decimalName == String. Empty )
                            throw new Exception( "表达式错误：不等式右侧不能为空!" );
                        else
                            throw new Exception( "表达式错误：没有找到非枚举规则数据 \"" + decimalName + "\" !" );
                    }
                    CodePropertyReferenceExpression retDecimalRuleDataExp = new CodePropertyReferenceExpression( );
                    retDecimalRuleDataExp. TargetObject = new CodeThisReferenceExpression( );
                    retDecimalRuleDataExp. PropertyName = ruleData. Name;
                    _scanIndex = end;
                    return retDecimalRuleDataExp;
                }
            }
            else          //枚举值
            {
                while ( end < _expString. Length && ( _expString[ end ] != '=' && _expString[ end ] != '>' && _expString[ end ] != '<' && _expString[ end ] != ')' && _expString[ end ] != '(' && _expString[ end ] != '&' && _expString[ end ] != '|' ) )
                {
                    ++end;
                }
                String enumItemName = _expString. Substring( _scanIndex , end - _scanIndex );
                EnumItemInfo enumItem = _ruleDataInfos[ enumName ]. EnumItemList. FirstOrDefault
                    ( p => ( p. Description == enumItemName ) );
                if ( enumItem != null )
                {
                    CodePrimitiveExpression retEnumValueExp = new CodePrimitiveExpression( );
                    retEnumValueExp. Value = enumItem. Value;
                    _scanIndex = end;
                    return retEnumValueExp;
                }
                if ( enumItemName == String. Empty )
                    throw new Exception( "表达式错误：不等式右侧不能为空" );
                else
                    throw new Exception( "表达式错误：在 \"" + enumName + " \" 选项中找不到 \"" + enumItemName + "\" !" );
            }
        }
        /// <summary>
        /// 解析比较运算符
        /// </summary>
        /// <param name="binaryExp"></param>
        void ParseComparisonOp( CodeBinaryOperatorExpression binaryExp )
        {
            int end = _scanIndex;
            while ( end < _expString. Length && ( _expString[ end ] == '>' || _expString[ end ] == '<' || _expString[ end ] == '=' ) )
            {
                ++end;
            }
            String cmpOp = _expString. Substring( _scanIndex , end - _scanIndex );
            switch ( cmpOp )
            {
                case ">":
                    binaryExp. Operator = CodeBinaryOperatorType. GreaterThan;
                    break;
                case "<":
                    binaryExp. Operator = CodeBinaryOperatorType. LessThan;
                    break;
                case ">=":
                    binaryExp. Operator = CodeBinaryOperatorType. GreaterThanOrEqual;
                    break;
                case "<=":
                    binaryExp. Operator = CodeBinaryOperatorType. LessThanOrEqual;
                    break;
                case "=":
                case "==":
                    binaryExp. Operator = CodeBinaryOperatorType. ValueEquality;
                    break;
                default:
                    if ( cmpOp != String. Empty )
                        throw new Exception( "表达式错误：无法解析符号\"" + cmpOp + "\"!" );
                    else
                        throw new Exception( "表达式错误：表达式不完整!" );
            }
            _scanIndex = end;
        }
        #endregion

        #endregion
    }

    public class StateActivityInfo
    {
        StateActivity _stateActivityBody;
        ISharedContent _parserContent;
        List<WorkflowEventInfo> _allNonParallelEventList = new List<WorkflowEventInfo>( );
        List<WorkflowEventInfo> _allEventList = new List<WorkflowEventInfo>( );
        Dictionary<String , EventDrivenActivityInfo> _eventDrivenActivityCollection = null;

        internal StateActivityInfo( StateActivity state , ISharedContent content )
        {
            _stateActivityBody = state;
            _parserContent = content;
            _eventDrivenActivityCollection = new Dictionary<String , EventDrivenActivityInfo>( );

            #region 提取并行事件
            var parallelControlList =
                from c in _stateActivityBody. Activities where c. Name. StartsWith( "ba_" ) select c as EventDrivenActivity;
            foreach ( EventDrivenActivity eda in parallelControlList )
            {
                String index = eda. Name. Substring( eda. Name. LastIndexOf( '_' ) + 1 );
                var parallelBranchList =
                    from c in _stateActivityBody. Activities where ( c. Name. StartsWith( "a_" ) && c. Name. EndsWith( index ) ) select c;
                List<EventDrivenActivityInfo> infoList = new List<EventDrivenActivityInfo>( parallelBranchList. Count( ) );
                foreach ( EventDrivenActivity parallelBranch in parallelBranchList )
                {
                    EventDrivenActivityInfo info = new EventDrivenActivityInfo( parallelBranch , content );
                    _eventDrivenActivityCollection. Add( info. EventName , info );
                    infoList. Add( info );
                    _allEventList. Add( new WorkflowEventInfo( info. EventName , info. EventDescription ) );
                }
                EventDrivenActivityInfo controlInfo = new EventDrivenActivityInfo( eda , infoList , _parserContent );
                _eventDrivenActivityCollection. Add( controlInfo. EventName , controlInfo );
            }
            #endregion

            #region 提取独立事件
            foreach ( EventDrivenActivity e in state. Activities )
            {
                if ( !( e. Name. StartsWith( "a_" ) || e. Name. StartsWith( "ba_" ) ) )
                {
                    EventDrivenActivityInfo info = new EventDrivenActivityInfo( e , _parserContent );
                    _eventDrivenActivityCollection. Add( info. EventName , info );
                    HandleExternalEventActivity heea = e. Activities[ 0 ] as HandleExternalEventActivity;
                    if ( heea != null )
                    {
                        _allNonParallelEventList. Add( new WorkflowEventInfo( e. Name , heea. Description ) );
                        _allEventList. Add( new WorkflowEventInfo( info. EventName , heea. Description ) );
                    }
                }
            }
            #endregion
        }

        #region Properties
        public String StateName
        {
            get
            {
                return _stateActivityBody. Name;
            }
        }
        public String StateDescription
        {
            get
            {
                return _stateActivityBody. Description;
            }
            set
            {
                _stateActivityBody. Description = value;
            }
        }
        public Dictionary<String , EventDrivenActivityInfo> EventDrivenActivityCollection
        {
            get
            {
                return _eventDrivenActivityCollection;
            }
        }
        public List<WorkflowEventInfo> AllNonParallelEventList
        {
            get
            {
                return _allNonParallelEventList;
            }
        }
        public List<WorkflowEventInfo> AllEventList
        {
            get
            {
                return _allEventList;
            }
        }
        public List<WorkflowStateInfo> AllStateInfoList
        {
            get
            {
                return _parserContent. AllStateInfoList;
            }
        }
        #endregion
    }

    public class EventDrivenActivityInfo
    {
        ISharedContent _parserContent;
        EventDrivenActivity _eventDrivenActivityBody;

        bool _isParallel;
        bool _hasRule;

        //(无规则下使用)
        SetStateActivity _singleSetStateActivity;
        //(有规则下使用)用SetStateActivity.Name作为key
        Dictionary<String , IfElseBranchActivityInfo> _ifElseBranchCollection;
        //目前最大的规则分支的编号
        int _maxBranchSN;
        //并行事件下使用
        List<EventDrivenActivityInfo> _parallelBranchList;

        #region Public Functions
        /// <summary>
        /// 非并行事件初始化
        /// </summary>
        internal EventDrivenActivityInfo( EventDrivenActivity e , ISharedContent content )
        {
            _isParallel = false;
            _eventDrivenActivityBody = e;

            #region 提取HandleExternelEvent或DelayEvent
            HandleExternalEventActivity heea = e. Activities. FirstOrDefault( p => p is HandleExternalEventActivity ) as HandleExternalEventActivity;
            if ( heea != null )
            {
                content. HandleEventActivityList. Add( heea );
            }
            else
            {
                DelayActivity da = e. Activities. FirstOrDefault( p => p is DelayActivity ) as DelayActivity;
                if ( da != null )
                    content. DelayActivityList. Add( da );
            }
            #endregion

            #region 提取隶属该EventDrivenActivity的SetStateActivity
            IfElseActivity ifelse = e. Activities. FirstOrDefault( p => p is IfElseActivity ) as IfElseActivity;
            if ( ifelse == null )
            {
                _hasRule = false;
                _ifElseBranchCollection = null;
                _singleSetStateActivity = e. Activities. FirstOrDefault( p => p is SetStateActivity ) as SetStateActivity;
            }
            else
            {
                _hasRule = true;
                _ifElseBranchCollection = new Dictionary<string , IfElseBranchActivityInfo>( );
                _singleSetStateActivity = null;
                foreach ( IfElseBranchActivity b in ifelse. Activities )
                {
                    SetStateActivity s = b. Activities. FirstOrDefault( p => p is SetStateActivity ) as SetStateActivity;
                    IfElseBranchActivityInfo branch = new IfElseBranchActivityInfo( b , s );
                    _ifElseBranchCollection. Add( s. Name , branch );

                    int sn = Int32. Parse( b. Name. Substring( b. Name. LastIndexOf( '_' ) + 1 ) );
                    if ( _maxBranchSN < sn )
                        _maxBranchSN = sn;
                }
            }
            #endregion
        }

        /// <summary>
        /// 并行事件初始化
        /// </summary>
        /// <param name="parallelControl"></param>
        /// <param name="parallelBranches"></param>
        /// <param name="content"></param>
        internal EventDrivenActivityInfo( EventDrivenActivity parallelControl , List<EventDrivenActivityInfo> parallelBranchList , ISharedContent content )
        {
            _isParallel = true;
            _eventDrivenActivityBody = parallelControl;
            _parallelBranchList = parallelBranchList;
            _parserContent = content;

            #region 提取隶属该EventDrivenActivity的SetStateActivity
            IfElseBranchActivity estimationBranch = ( parallelControl. Activities. First( p => p is IfElseActivity ) as IfElseActivity ). Activities[ 0 ] as IfElseBranchActivity;
            IfElseActivity ifelse = estimationBranch. Activities. FirstOrDefault( p => p is IfElseActivity ) as IfElseActivity;
            if ( ifelse == null )
            {
                _hasRule = false;
                _singleSetStateActivity = estimationBranch. Activities. FirstOrDefault( p => p is SetStateActivity ) as SetStateActivity;
                _ifElseBranchCollection = null;
            }
            else
            {
                _hasRule = true;
                _ifElseBranchCollection = new Dictionary<string , IfElseBranchActivityInfo>( );
                _singleSetStateActivity = null;
                foreach ( IfElseBranchActivity b in ifelse. Activities )
                {
                    SetStateActivity s = b. Activities. FirstOrDefault( p => p is SetStateActivity ) as SetStateActivity;
                    IfElseBranchActivityInfo branch = new IfElseBranchActivityInfo( b , s );
                    _ifElseBranchCollection. Add( s. Name , branch );

                    int sn = Int32. Parse( b. Name. Substring( b. Name. LastIndexOf( '_' ) + 1 ) );
                    if ( _maxBranchSN < sn )
                        _maxBranchSN = sn;
                }
            }
            #endregion
        }

        /// <summary>
        /// 在xoml定义中增加一个规则的位置
        /// </summary>
        /// <returns>新规则的Name</returns>
        internal String AddNewRule( String exp )
        {
            IfElseActivity ifElse = null;
            int newIndex;
            String targetStateName;
            newIndex = ++_maxBranchSN;         //每个新加入的规则分支都是编号最大的
            if ( _hasRule )
            {
                targetStateName = _ifElseBranchCollection. Last( ). Value. SetStateActivityBody. TargetStateName;
                ifElse = _ifElseBranchCollection. First( ). Value. IfElseBranchActivityBody. Parent as IfElseActivity;
            }
            else
            {
                //原来没有规则时，需要先加上一个IfElseActivity对象，并删除原来的SetStateActivity
                _hasRule = true;
                _ifElseBranchCollection = new Dictionary<string , IfElseBranchActivityInfo>( );
                ifElse = new IfElseActivity( this. EventName + "_IfElse_1" );
                targetStateName = _singleSetStateActivity. TargetStateName;
                _eventDrivenActivityBody. Activities. Remove( _singleSetStateActivity );

                //添加规则用的IfElseActivity时要区分有否并行事件
                if ( _isParallel )
                    ( ( _eventDrivenActivityBody. Activities[ 1 ] as IfElseActivity ). Activities[ 0 ] as IfElseBranchActivity ). Activities. Add( ifElse );
                else
                    _eventDrivenActivityBody. Activities. Add( ifElse );

                _singleSetStateActivity = null;
            }

            //建立新的IfElseBranchActivity，并将新的SetStateActivity加入其中
            RuleConditionReference newConRef = new RuleConditionReference( );
            newConRef. ConditionName = this. EventName + "_Condition_" + newIndex. ToString( );
            SetStateActivity newSetState = new SetStateActivity( this. EventName + "_SetState_" + newIndex. ToString( ) );
            newSetState. TargetStateName = targetStateName;
            IfElseBranchActivity newBranch =
                new IfElseBranchActivity( this. EventName + "_Branch_" + newIndex. ToString( ) );
            newBranch. Condition = newConRef;
            newBranch. Description = exp;                    //存储规则表达式
            newBranch. Activities. Add( newSetState );
            ifElse. Activities. Add( newBranch );              //将新建的IfElseBranchActivity加入IfElseActivity中

            //修改相关记录
            IfElseBranchActivityInfo newBranchInfo = new IfElseBranchActivityInfo( newBranch , newSetState );
            _ifElseBranchCollection. Add( newSetState. Name , newBranchInfo );

            return newConRef. ConditionName;
        }

        /// <summary>
        /// 删除xoml定义中的一个规则
        /// </summary>
        internal String DeleteRule( String setStateName )
        {
            //在IfElseActivity中删除相关Branch
            IfElseBranchActivityInfo oldBranchInfo = _ifElseBranchCollection[ setStateName ];
            IfElseActivity ifElse = oldBranchInfo. IfElseBranchActivityBody. Parent as IfElseActivity;
            ifElse. Activities. Remove( oldBranchInfo. IfElseBranchActivityBody );
            //在相关记录中删除
            _ifElseBranchCollection. Remove( setStateName );
            int sn = Int32. Parse( oldBranchInfo. IfElseBranchActivityBody. Name. Substring( oldBranchInfo. IfElseBranchActivityBody. Name. LastIndexOf( '_' ) + 1 ) );
            if ( _maxBranchSN == sn )
                --_maxBranchSN;

            if ( _ifElseBranchCollection. Count == 0 )
            //所有规则已被删除，因此IfElseActivity也应该删除，并将最后的SetStateActivity转移到EventDriventActivity之下
            {
                //删除规则用的IfElseActivity时要区分有否并行事件
                if ( _isParallel )
                    ( ( _eventDrivenActivityBody. Activities[ 1 ] as IfElseActivity ). Activities[ 0 ] as IfElseBranchActivity ). Activities. Remove( ifElse );
                else
                    _eventDrivenActivityBody. Activities. Remove( ifElse );

                oldBranchInfo. IfElseBranchActivityBody. Activities. Remove( oldBranchInfo. SetStateActivityBody );
                _eventDrivenActivityBody. Activities. Add( oldBranchInfo. SetStateActivityBody );

                //修改相关记录
                _singleSetStateActivity = oldBranchInfo. SetStateActivityBody;
                _hasRule = false;
                _ifElseBranchCollection = null;
                _maxBranchSN = 0;
            }
            return ( oldBranchInfo. IfElseBranchActivityBody. Condition as RuleConditionReference ). ConditionName;
        }

        /// <summary>
        /// 修改xoml中对规则表达式的记录
        /// </summary>
        /// <param name="setStateName"></param>
        /// <param name="ruleExp"></param>
        internal void ModifyRule( String setStateName , String ruleExp )
        {
            _ifElseBranchCollection[ setStateName ]. IfElseBranchActivityBody. Description = ruleExp;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 是否包含规则分支
        /// </summary>
        public bool HasRule
        {
            get
            {
                return _hasRule;
            }
        }
        /// <summary>
        /// 是否并行事件集
        /// </summary>
        public bool IsParallel
        {
            get
            {
                return _isParallel;
            }
        }
        /// <summary>
        /// 是否并行事件集中的并行分支
        /// </summary>
        public bool IsParallelBranch
        {
            get
            {
                return _isParallel == false && _hasRule == false && _singleSetStateActivity == null;
            }
        }
        /// <summary>
        /// HandleExternalEvent.EventName属性
        /// （除了平行事件特殊外，其余的EventDrivenActivity.Name = HandleExternalEvent.EventName）
        /// </summary>
        public String EventName
        {
            get
            {
                if ( _eventDrivenActivityBody. Name. StartsWith( "a" ) )
                {
                    int length = _eventDrivenActivityBody. Name. LastIndexOf( '_' ) - 2;
                    return _eventDrivenActivityBody. Name. Substring( 2 , length );      //前2位是a_
                }
                if ( _eventDrivenActivityBody. Name. StartsWith( "ba" ) )
                    return _eventDrivenActivityBody. Name. Substring( 2 );      //前2位是ba
                return _eventDrivenActivityBody. Name;
            }
        }
        public String EventDescription
        {
            get
            {
                return _eventDrivenActivityBody. Activities[ 0 ]. Description;
            }
        }
        /// <summary>
        /// (无规则且非并行事件分支时使用)
        /// </summary>
        public SetStateActivity SingleSetStateActivity
        {
            get
            {
                return _singleSetStateActivity;
            }
        }
        /// <summary>
        /// (有规则下使用)用SetStateActivity.Name作为key
        /// </summary>
        public Dictionary<String , IfElseBranchActivityInfo> IfElseBranchCollection
        {
            get
            {
                return _ifElseBranchCollection;
            }
        }
        /// <summary>
        /// (仅对并行事件有效)并行子事件列表
        /// </summary>
        public List<EventDrivenActivityInfo> ParallelEventDrivenList
        {
            get
            {
                return _parallelBranchList;
            }
        }
        #endregion
    }
}