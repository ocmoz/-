﻿using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.CodeDom;
using System.Collections;

using FM2E.SQLServerDAL .Workflow ;
using FM2E. Model. Workflow;
using FM2E.Model .System ;

namespace FM2E. WorkflowLayer
{
    public class WorkflowHelper
    {
        #region Properties
        static public WorkflowRuntime CurrentRuntime
        {
            get
            {
                return _currentRuntime;
            }
        }
        static internal WorkflowHelperDAL HelperDAL
        {
            get
            {
                return _workflowHelperDAL;
            }
        }
        #endregion

        #region Private Variables
        static WorkflowRuntime _currentRuntime;
        static SqlWorkflowPersistenceService _currentPersistenceService;
        static ExternalDataExchangeService _currentExchangeService;
        static ManualWorkflowSchedulerService _currentSchedulerService;

        //工作流配置文件的保存位置
        static private readonly String CONFIG_PATH = HttpContext. Current. Server. MapPath( "~" ) + "/Bin/workflow.xml";
        //缓存所有工作流的IWorkflowParser对象
        static private readonly Dictionary<String , IWorkflowParser> _WorkflowParserCollection = new Dictionary<string , IWorkflowParser>( );

        static private XmlDocument _workflowConfigXml = new XmlDocument( );
        //工作流定义文件的保存位置
        static private String _workflowDefinitionsDir;

        static private FM2E. SQLServerDAL. Workflow. WorkflowRole _workflowRoleDAL = new FM2E. SQLServerDAL. Workflow. WorkflowRole( );
        static private WorkflowHelperDAL _workflowHelperDAL = new WorkflowHelperDAL( );
        #endregion

        #region Public Functions
        /// <summary>
        /// 建立并初始化全局工作流引擎
        /// </summary>
        static public void InitializeWorkflowRuntime( )
        {
            //加载工作流配置文件
            string path = string. Format( "Fold:{0},Config_Path:{1}" , HttpContext. Current. Server. MapPath( "~" ) , CONFIG_PATH );

            _workflowConfigXml. Load( CONFIG_PATH );
            _workflowDefinitionsDir = HttpContext. Current. Server. MapPath( "~" ) + _workflowConfigXml. SelectSingleNode( "WorkflowConfig/DocumentDirectory" ). Attributes[ "value" ]. Value;

            //建立工作流引擎
            _currentRuntime = new System. Workflow. Runtime. WorkflowRuntime( );

            //加载持久化服务
            _workflowHelperDAL. ClearInstanceStateOwner( );
            string connectionString = ConfigurationManager. ConnectionStrings[ "PersistenceServiceConnString" ]. ConnectionString;
            _currentPersistenceService = new SqlWorkflowPersistenceService( connectionString , true , new TimeSpan( 365 , 0 , 0 , 0 , 0 ) , new TimeSpan( 0 , 0 , 0 , 30 , 0 ) );
            _currentRuntime. AddService( _currentPersistenceService );

            #region 加载数据交换服务（从workflow.xml配置中反射加载）
            _currentExchangeService = new ExternalDataExchangeService( );
            _currentRuntime. AddService( _currentExchangeService );

            XmlNode eventServicesNode = _workflowConfigXml. SelectSingleNode( "WorkflowConfig/EventServices" );
            List<XmlNode> toDeleteList = new List<XmlNode>( eventServicesNode. ChildNodes. Count );
            foreach ( XmlNode xn in eventServicesNode. ChildNodes )
            {
                String name = xn. Attributes[ "value" ]. Value;
                Type eventServiceType = Assembly. Load( "FM2E.WorkflowLayer" ). GetType( name );
                if ( eventServiceType != null )
                    _currentExchangeService. AddService( Activator. CreateInstance( eventServiceType ) as IBasicEventService );
                else
                    toDeleteList. Add( xn );
            }
            //自动删除已经废弃的节点
            if ( toDeleteList. Count > 0 )
            {
                foreach ( XmlNode xn in toDeleteList )
                    eventServicesNode. RemoveChild( xn );
                _workflowConfigXml. Save( CONFIG_PATH );
            }

            #endregion

            //加载手工调度服务
            _currentSchedulerService = new ManualWorkflowSchedulerService( true );
            _currentRuntime. AddService( _currentSchedulerService );

            //启动工作流引擎
            _currentRuntime. StartRuntime( );
        }

        /// <summary>
        /// 创建一个新的工作流实例
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="ruleData"></param>
        /// <returns></returns>
        static public Guid CreateNewInstance( long dataID , String workflowName )
        {
            WorkflowInstance newInstance = null;
            String name = workflowName. ToString( );
            using ( XmlReader workflowReader = XmlReader. Create( GetWorkflowDocName( workflowName ) ) )
            {
                Guid newInstanceId = Guid. NewGuid( );
                if ( HasRule( workflowName ) )
                {
                    using ( XmlReader ruleReader = XmlReader. Create( GetRuleDocName( workflowName ) ) )
                    {
                        Dictionary<String , object> transfer = new Dictionary<string , object>( );
                        transfer. Add( "DataID" , dataID );
                        transfer. Add( "InstanceId" , newInstanceId );
                        newInstance = _currentRuntime. CreateWorkflow( workflowReader , ruleReader , transfer , newInstanceId );
                    }
                }
                else
                {
                    Dictionary<String , object> transfer = new Dictionary<string , object>( );
                    transfer. Add( "InstanceId" , newInstanceId );
                    newInstance = _currentRuntime. CreateWorkflow( workflowReader , null , transfer , newInstanceId );
                }
            }
            newInstance. Start( );
            _currentSchedulerService. RunWorkflow( newInstance. InstanceId );

            Type t = Type. GetType( "FM2E.WorkflowLayer." + workflowName );
            if ( t != null )
                _workflowHelperDAL. BindInstanceToData( newInstance. InstanceId , workflowName , t. GetProperty( "TableName" ). GetValue( t , null ) as String , dataID );
            else
                throw new Exception( "没有找到相应工作流定义！" );

            return newInstance. InstanceId;
        }
        /// <summary>
        /// 删除FM2E_WorkflowInstance中相关的工作流实例记录（在删除数据项之前使用）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataID"></param>
        static public void DeleteWorkflowInstance( String tableName , long dataID )
        {
            _workflowHelperDAL. DeleteWorkflowInstance( tableName , dataID );
        }
        /// <summary>
        /// 触发事件使工作流运行到下一状态
        /// </summary>
        /// <typeparam name="T">事件服务类型</typeparam>
        /// <param name="instanceId">工作流实例Id</param>
        /// <param name="eventName">事件名称</param>
        static public void SetStateMachine<T>( Guid instanceID , String eventName )
            where T : IBasicEventService
        {
            _currentRuntime. GetService<T>( ). RaiseEvent( eventName , instanceID );
            _currentSchedulerService. RunWorkflow( instanceID );
            String stateName , stateDescription;
            GetCurrentStateNameAndDescription( instanceID , out stateName , out stateDescription );
            _workflowHelperDAL. UpdateInstanceStatus( instanceID , stateName , stateDescription );
        }

        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 触发事件使工作流运行到下一状态,并自动更新下一审批者
        /// </summary>
        /// <typeparam name="T">事件服务类型</typeparam>
        /// <param name="instanceId">工作流实例Id</param>
        /// <param name="eventName">事件名称</param>
        static public void SetStateAndNextUserMachine<T>(Guid instanceID, String eventName,string workflowName,out string nextUserName)
            where T : IBasicEventService
        {
            _currentRuntime.GetService<T>().RaiseEvent(eventName, instanceID);
            _currentSchedulerService.RunWorkflow(instanceID);
            String stateName, stateDescription;
            GetCurrentStateNameAndDescription(instanceID, out stateName, out stateDescription);
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, stateDescription, out nextUserName);
        }

        //********** Modified by ZhengJinLiang    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 触发事件使工作流运行到下一状态,根据有关的维护单位Company，并自动更新下一审批者
        /// </summary>
        /// <typeparam name="T">事件服务类型</typeparam>
        /// <param name="instanceId">工作流实例Id</param>
        /// <param name="eventName">事件名称</param>
        static public void SetStateAndNextUserMachine<T>(Guid instanceID, String eventName, string workflowName , String company, out string nextUserName)
            where T : IBasicEventService
        {
            _currentRuntime.GetService<T>().RaiseEvent(eventName, instanceID);
            _currentSchedulerService.RunWorkflow(instanceID);
            String stateName, stateDescription;
            GetCurrentStateNameAndDescription(instanceID, out stateName, out stateDescription);
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, stateDescription, company, out nextUserName);
        }

        static public void SetStateAndNextUserMachine2<T>(Guid instanceID, String eventName, string workflowName, String company, out string nextUserName)
            where T : IBasicEventService
        {
            _currentRuntime.GetService<T>().RaiseEvent(eventName, instanceID);
            _currentSchedulerService.RunWorkflow(instanceID);
            String stateName, stateDescription;
            GetCurrentStateNameAndDescription(instanceID, out stateName, out stateDescription);
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser2(workflowName, instanceID, stateName, stateDescription, company, out nextUserName);
        }

        static public void SetStateAndNextUserMachine3<T>(Guid instanceID, String eventName, string workflowName, String company,string delegateUserName, out string nextUserName)
          where T : IBasicEventService
        {
            _currentRuntime.GetService<T>().RaiseEvent(eventName, instanceID);
            _currentSchedulerService.RunWorkflow(instanceID);
            String stateName, stateDescription;
            GetCurrentStateNameAndDescription(instanceID, out stateName, out stateDescription);
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser3(workflowName, instanceID, stateName, stateDescription, company, delegateUserName, out nextUserName);
        }

        static public void SetStateAndNextUserMachine1<T>(Guid instanceID, String eventName, string workflowName, String company, out string nextUserName)
           where T : IBasicEventService
        {
            _currentRuntime.GetService<T>().RaiseEvent(eventName, instanceID);
            _currentSchedulerService.RunWorkflow(instanceID);
            String stateName, stateDescription;
            GetCurrentStateNameAndDescription(instanceID, out stateName, out stateDescription);
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser1(workflowName, instanceID, stateName, stateDescription, company, out nextUserName);
        }

        static public void SetStateAndNextUserMachine<T>(Guid instanceID, String eventName, string workflowName, bool stationCheck, string systemID,string next, out string nextUserName)
            where T : IBasicEventService
        {
            _currentRuntime.GetService<T>().RaiseEvent(eventName, instanceID);
            _currentSchedulerService.RunWorkflow(instanceID);
            String stateName, stateDescription;
            GetCurrentStateNameAndDescription(instanceID, out stateName, out stateDescription);
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, stateDescription, stationCheck, systemID, next, out nextUserName);
        }

        /// <summary>
        /// 触发事件使工作流运行到下一状态,根据故障类型：软件故障、硬件故障，根据系统工程师分开审批，并自动更新下一审批者
        /// </summary>
        /// <typeparam name="T">事件服务类型</typeparam>
        /// <param name="instanceId">工作流实例Id</param>
        /// <param name="eventName">事件名称</param>
        static public void SetStateAndNextUserMachine<T>(Guid instanceID, String eventName, string workflowName, bool stationCheck, string systemID,out string nextUserName)
            where T : IBasicEventService
        {
            _currentRuntime.GetService<T>().RaiseEvent(eventName, instanceID);
            _currentSchedulerService.RunWorkflow(instanceID);
            String stateName, stateDescription;
            GetCurrentStateNameAndDescription(instanceID, out stateName, out stateDescription);
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, stateDescription, stationCheck, systemID,out nextUserName);
        }
        static public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, String stateDescription, bool stationCheck, string systemID, out string nextUserName)
        {
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, stateDescription, stationCheck, systemID, out nextUserName);
        }

        static public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, String stateDescription, String company, out string nextUserName)
        {
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, stateDescription, company, out nextUserName);
        }

        static public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, String stateDescription, out string nextUserName)
        {
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, stateDescription, out nextUserName);
        }

        static public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, out string nextUserName)
        {
            _workflowHelperDAL.UpdateInstanceStatusAndNextUser(workflowName, instanceID, stateName, out nextUserName);
        }

        //********** Modification Finished 2011-11-28 **********************************************************************************************

        /// <summary>
        /// 获得指定工作流实例的当前状态名
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        static public String GetCurrentStateName( Guid instanceId )
        {
            return ( new StateMachineWorkflowInstance( _currentRuntime , instanceId ) ). CurrentStateName;
        }
        /// <summary>
        /// 获得指定工作流实例当前状态的名称和描述
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="stateName"></param>
        /// <param name="stateDescription"></param>
        static void GetCurrentStateNameAndDescription( Guid instanceId , out String stateName , out String stateDescription )
        {
            StateMachineWorkflowInstance t = new StateMachineWorkflowInstance( _currentRuntime , instanceId );
            stateName = t. CurrentStateName;
            stateDescription = t. CurrentState. Description;
        }
        /// <summary>
        /// 检测并行事件工作流中某个并行事件是否已被触发过
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instanceId"></param>
        /// <param name="eventMask">并行事件的掩码（请从工作流类的静态属性获得）</param>
        /// <returns></returns>
        static public bool CheckParallelEventInvoked<T>( Guid instanceId , long eventMask )
            where T : StateMachineWorkflowActivity , IParallelEventSupport
        {
            return ( ( ( new StateMachineWorkflowInstance( _currentRuntime , instanceId ) ). StateMachineWorkflow as T ). EventFlag & eventMask ) > 0;
        }

        /// <summary>
        /// 根据角色信息列表，获得角色所绑定的状态名称表
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        static public List<String> GetCorrelativeStateNameList( IList roleInfoList )
        {
            return _workflowHelperDAL. GetBindingStates( roleInfoList );
        }
        /// <summary>
        /// 根据工作流名称和用户名，获得用户可访问的状态名称表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        static public List<String> GetCorrelativeStateNameList( String workflowName , String userName )
        {
            IList roleInfoList = _workflowRoleDAL. GetWorkflowRoleList( userName , workflowName );
            return _workflowHelperDAL. GetBindingStates( roleInfoList );
        }
        /// <summary>
        /// 根据工作流名称和工作流角色ID，获得该角色可访问的状态名称表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        static public List<String> GetCorrelativeStateNameList( String workflowName , long roleID )
        {
            IList roleInfoList = new List<WorkflowRoleInfo>( 1 );
            roleInfoList. Add( new WorkflowRoleInfo( ) { WorkflowName = workflowName , WorkflowRoleID = roleID } );
            return _workflowHelperDAL. GetBindingStates( roleInfoList );
        }
        /// <summary>
        /// 获得指定工作流的所有状态
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        static public List<WorkflowStateInfo> GetAllStateInfo( String workflowName )
        {
            return GetWorkflowParser( workflowName , false ). StateInfoList;
        }
        /// <summary>
        /// 当instanceId为null时，获得一个工作流中某状态的所有审批者用户名; 
        /// 当instanceId不为null时返回专人专项的用户名
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="workflowStateName"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        static public List<String> GetAllApprover( String workflowName , String workflowStateName , Guid instanceId )
        {
            return _workflowHelperDAL. GetAllApprover( workflowName , workflowStateName , instanceId );
        }
        /// <summary>
        /// 获得指定工作流某一状态的下一状态信息（workflowStateName不适用于带规则的状态）
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="workflowStateName"></param>
        /// <param name="eventName"></param>
        /// <returns>当指定状态带规则时，返回null</returns>
        static public WorkflowStateInfo GetNextStateInfo(String workflowName, String workflowStateName, String eventName)
        {
            IWorkflowParser parser = GetWorkflowParser( workflowName , false );
            SetStateActivity setState = parser. StateActivityCollection[ workflowStateName ]. EventDrivenActivityCollection[ eventName ].SingleSetStateActivity;
            if ( setState != null )
                return parser. StateInfoList. FirstOrDefault( p => p. Name == setState. TargetStateName );
            else
                return null;
        }
        /// <summary>
        /// 获得指定工作流中一个工作流角色列表对应的所有工作流状态
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="roleInfoList"></param>
        /// <returns></returns>
        static public List<WorkflowStateInfo> GetStateInfosByRole( String workflowName , IList roleInfoList )
        {
            List<WorkflowStateInfo> ret = new List<WorkflowStateInfo>( );
            List<String> bindingStateList = GetCorrelativeStateNameList( roleInfoList );
            foreach ( WorkflowStateInfo wsi in GetAllStateInfo( workflowName ) )
                foreach ( String stateName in bindingStateList )
                    if ( stateName == wsi. Name )
                        ret. Add( wsi );
            return ret;
        }
        /// <summary>
        /// 获得指定工作流中单个工作流角色对应的工作流状态
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        static public List<WorkflowStateInfo> GetStateInfosByRole( String workflowName , WorkflowRoleInfo roleInfo )
        {
            List<WorkflowRoleInfo> list = new List<WorkflowRoleInfo>( 1 );
            list. Add( roleInfo );
            return GetStateInfosByRole( workflowName , list );
        }
        /// <summary>
        /// 根据工作流状态获得相关数据项ID
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="stateNames"></param>
        /// <returns></returns>
        static public List<long> GetDataIDByStateNames( String tableName , String[ ] stateNames )
        {
            return _workflowHelperDAL. GetDataIDListByStateNames( tableName , stateNames );
        }
        /// <summary>
        /// 根据数据项ID获得工作流实例信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataID"></param>
        /// <returns></returns>
        static public WorkflowInstanceInfo GetWorkflowInstanceInfo( String tableName , long dataID )
        {
            WorkflowInstanceInfo wii = _workflowHelperDAL. GetWorkflowInstanceInfo( tableName , dataID );
            return wii;
        }
        /// <summary>
        /// 获得指定工作流的指定状态中的非并行事件列表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="stateDescription"></param>
        /// <returns></returns>
        static public List<WorkflowEventInfo> GetEventInfoList( String workflowName , String stateName )
        {
            return GetWorkflowParser( workflowName , false ). GetEventInfoList( stateName , false );
        }
        /// <summary>
        /// 获得单个工作流的基本信息（包含工作流名称、描述、起始状态名、终结状态名）
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        static public WorkflowClassInfo GetWorkflowBasicInfo( String workflowName )
        {
            return GetWorkflowParser( workflowName , false ). WorkflowInfo;
        }
        /// <summary>
        /// 获得_workflowDefinitionsDir所指路径下的所有工作流基本信息
        /// </summary>
        /// <returns></returns>
        static public List<WorkflowClassInfo> GetAllWorkflowList( )
        {
            String[ ] _workflowNames = Directory. GetFiles( _workflowDefinitionsDir );
            List<WorkflowClassInfo> ret = new List<WorkflowClassInfo>( _workflowNames. Length );
            foreach ( String name in _workflowNames )
            {
                if ( name. Substring( name. LastIndexOf( '.' ) ) == ".xoml" )
                    ret. Add( GetWorkflowParser( ( name. Substring( name. LastIndexOf( '/' ) + 1 ) ). Split( '(' )[ 0 ] , false ). WorkflowInfo );
            }
            return ret;
        }
        /// <summary>
        /// 获得某枚举属性的选项列表
        /// </summary>
        /// <param name="workflowName">工作流名称</param>
        /// <param name="enumName">枚举属性名称</param>
        /// <returns>枚举选项列表</returns>
        static public List<EnumItemInfo> GetEnumItemList( String workflowName , String enumName )
        {
            RuleDataInfo info = GetWorkflowParser( workflowName , false ). RuleDataInfoCollection[ enumName ];
            if ( info != null )
                return info. EnumItemList;
            else
                return null;
        }
        /// <summary>
        /// 根据某枚举选项的Value值获得其Description
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="enumName"></param>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        static public String GetEnumItemDescription( String workflowName , String enumName , int itemValue )
        {
            RuleDataInfo rdi = GetWorkflowParser( workflowName , false ). RuleDataInfoCollection[ enumName ];
            if ( rdi == null || !rdi. IsEnum )
                return null;
            EnumItemInfo eii = rdi. EnumItemList. FirstOrDefault( p => p. Value == itemValue );
            if ( eii == null )
                return null;
            else
                return eii. Description;
        }

        #region 获得WorkflowParser的方法（不建议直接调用）
        /// <summary>
        /// WorkflowName为key的形式获得相应的IWorkflowParser
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="isUpdated">
        /// false: 若内存中已有该工作流parser，则直接获取
        /// true: 强制刷新内存中的工作流parser
        /// </param>
        /// <returns></returns>
        static public IWorkflowParser GetWorkflowParser( String workflowName , bool isUpdated )
        {
            if ( isUpdated == false && _WorkflowParserCollection. ContainsKey( workflowName ) )
                return _WorkflowParserCollection[ workflowName ];
            else
            {
                IWorkflowParser parser = GetNewWorkflowParser( workflowName );
                _WorkflowParserCollection[ workflowName ] = parser;
                return parser;
            }
        }
        /// <summary>
        /// 创建一个新的临时IWorkflowParser
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="parserGuid"></param>
        /// <returns></returns>
        static public IWorkflowParser GetTempWorkflowParser( String workflowName )
        {
            return GetNewWorkflowParser( workflowName );
        }
        /// <summary>
        /// 创建一个全新的IWorkflowParser
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        static private IWorkflowParser GetNewWorkflowParser( String workflowName )
        {
            using ( XmlReader workflowReader = XmlReader. Create( GetWorkflowDocName( workflowName ) ) )
            {
                Type specificType = Assembly. Load( "FM2E.WorkflowLayer" ). GetType( "FM2E.WorkflowLayer." + workflowName );
                Type genericType = typeof( WorkflowParser<> ). MakeGenericType( specificType );
                IWorkflowParser parser = Activator. CreateInstance( genericType ) as IWorkflowParser;

                if ( HasRule( workflowName ) )
                {
                    using ( XmlReader ruleReader = XmlReader. Create( GetRuleDocName( workflowName ) ) )
                    {
                        parser. Initialize( workflowReader , ruleReader );
                    }
                }
                else
                    parser. Initialize( workflowReader , null );
                return parser;
            }
        }
        #endregion

        #region 获得工作流定义文件信息的方法
        /// <summary>
        /// 判断是否存在相应工作流的规则定义文件
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        static public bool HasRule( String workflowName )
        {
            String filePath = _workflowDefinitionsDir + workflowName + "Rule({0}).rules";
            if ( File. Exists( String. Format( filePath , 0 ) ) || File. Exists( String. Format( filePath , 1 ) ) )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获得指定工作流的工作流定义文件名称（相对路径）
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        static public String GetWorkflowDocName( String workflowName )
        {

            String defaultName = _workflowDefinitionsDir + workflowName + "(0).xoml";
            if ( File. Exists( defaultName ) == false )
            {
                defaultName = _workflowDefinitionsDir + workflowName + "(1).xoml";
                if ( File. Exists( defaultName ) == false )
                    throw new Exception
                        ( "没有在" + _workflowDefinitionsDir + "中找到" + workflowName + "的工作流定义文件，请确定文件的存在和命名格式的正确！\n注：规则文件名应为\"" + workflowName + "(0或1).xoml\"" );
            }
            return defaultName;
        }

        /// <summary>
        /// 获得指定工作流的Rule定义文件名称（相对路径）
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        static public String GetRuleDocName( String workflowName )
        {
            String ruleName = workflowName + "Rule";
            String defaultName = _workflowDefinitionsDir + ruleName + "(0).rules";
            if ( File. Exists( defaultName ) == false )
                defaultName = _workflowDefinitionsDir + ruleName + "(1).rules";
            else
                if ( File. Exists( defaultName ) == false )
                    throw new Exception
                        ( "没有在" + _workflowDefinitionsDir + "中找到" + workflowName + "的Rule定义文件，请确定文件的存在和命名格式的正确！\n注：规则文件名应为\"" + workflowName + "Rule(0或1).rules\"" );
            return defaultName;
        }
        #endregion

        #region 保存工作流定义为文件的方法（非静态）
        public void SaveWorkflowAsNewName( IWorkflowParser parser )
        {
            //生成新文件的名字
            String workflowName = parser. WorkflowInfo. Name;
            Regex r = new Regex( @"(?<=\x28)(0|1)(?=\x29){1}" );
            String oldWFName = GetWorkflowDocName( workflowName );
            File. Delete( oldWFName );
            String newWFName = r. Replace( oldWFName , r. Match( oldWFName ). Value == "0" ? "1" : "0" );

            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer( );

            //写入.xoml文件
            using ( XmlWriter writer = XmlWriter. Create( newWFName ) )
            {
                serializer. Serialize( writer , parser. WorkflowDefinition );
            }

            //写入.rules文件
            if ( parser. WorkflowInfo. HasRule )
            {
                String newRuleName = null;
                if ( HasRule( workflowName ) )            //防止rules文件被无意中删除
                {
                    String oldRuleName = GetRuleDocName( workflowName );
                    File. Delete( oldRuleName );
                    newRuleName = r. Replace( oldRuleName , r. Match( oldRuleName ). Value == "0" ? "1" : "0" );
                }
                else
                    newRuleName = _workflowDefinitionsDir + workflowName + "Rule(0).rules";
                using ( XmlWriter writer = XmlWriter. Create( newRuleName ) )
                {
                    serializer. Serialize( writer , parser. RuleDefinition );
                }
            }

            //更新Cache中的工作流parser
            _WorkflowParserCollection[ parser. WorkflowInfo. Name ] = parser;

            //对新工作流中关于状态描述的更改进行数据库同步
            foreach ( WorkflowStateChangeInfo info in parser. StateChangeRecordList )
            {
                if ( info. OldDescription != info. NewDescription )
                    _workflowHelperDAL. UpdateStateDescription( parser. WorkflowInfo. Name , info. OldDescription , info. NewDescription );
            }
            parser. StateChangeRecordList. Clear( );
        }
        #endregion
        #endregion

        #region Private Functions
        /// <summary>
        /// 建立一个工作流实例与某个表中的数据项的关联
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="tableName"></param>
        /// <param name="dataID"></param>
        static void BindInstanceToData( Guid instanceID , String workflowName , String tableName , long dataID )
        {
            _workflowHelperDAL. BindInstanceToData( instanceID , workflowName , tableName , dataID );
        }
        #endregion

        #region Extension Functions ( 按工作流的专人专项扩展需求 ）
        /// <summary>
        /// 给工作流实例增加专人专项，如果有代理用户会自动添加
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="nextUserName"></param>
        static public void UpdateNextUserID( Guid instanceId , String nextUserName )
        {
            _workflowHelperDAL. UpdateNextUser( instanceId , nextUserName );
        }

        /// <summary>
        /// 给工作流实例增加专人专项，手工指定代理
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="nextUserName"></param>
        static public void UpdateNextUserID(Guid instanceId, String nextUserName,string delegateUserName)
        {
            _workflowHelperDAL.UpdateNextUser(instanceId, nextUserName, delegateUserName);
        }

        /// <summary>
        /// 仅根据用户名（专人专项，包括代理用户）获取相关的数据ID列表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        static public List<long> GetDataIDJustByUserName(String workflowName, String userName)
        {
            return _workflowHelperDAL. GetDataIDJustByUserName( workflowName , userName );
        }
        #endregion
    }
}