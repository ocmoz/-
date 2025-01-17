﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.WorkflowLayer;
using FM2E.Model.PendingOrder;
using FM2E.Model.Maintain;
using FM2E.Model.System;
using FM2E.BLL.System;
using FM2E.Model.Workflow;
using System.Xml;
using System.Web;
using FM2E.Model.Message;

namespace FM2E.BLL.Utils
{
    public class WorkflowApplication
    {

        /// <summary>
        /// 仅用于给具备权限的用户发送待办事务（利用CompanyID来限定只发给固定公司的用户）
        /// </summary>
        /// <param name="thisID"></param>
        /// <param name="title"></param>
        /// <param name="WorkflowName"></param>
        /// <param name="TableName"></param>
        /// <param name="UserName">发送者ID</param>
        /// <param name="PersonName">发送者真实姓名</param>
        /// <param name="URL">连接</param>
        /// <param name="type">待办事务类型</param>
        /// <param name="CompanyID"></param>
        public static void SendingPendingOrderOnly(long thisID, string title, string WorkflowName, string TableName, string UserName, string PersonName, string URL, short type, string CompanyID)
        {
            //待办事务对象
            PendingOrderInfo poInfo = new PendingOrderInfo();
            poInfo.SenderPersonName = PersonName;
            poInfo.SendFrom = UserName;
            poInfo.SendTime = DateTime.Now;
            poInfo.Title = title;
            poInfo.Type = type;
            poInfo.URL = URL;
            poInfo.ReceiverAddress = "";
            //获取当前状态下需要通知的用户列表
            WorkflowInstanceInfo wfinfo = WorkflowHelper.GetWorkflowInstanceInfo(TableName, thisID);
            List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, WorkflowHelper.GetCurrentStateName(wfinfo.InstanceID), Guid.Empty);
            List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
            foreach (string approver in approverList)
            {
                LoginUserInfo info = userBll.GetLoginUser(approver);
                if (info.CompanyID != null && info.CompanyID == CompanyID)
                {
                    PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
                    receiver.UserName = approver;
                    receiver.HasRead = false;
                    receiverList.Add(receiver);
                    poInfo.ReceiverAddress += approver + ";";
                }
            }
            poInfo.Receivers = receiverList;
            FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
            poBll.SendPendingOrder(poInfo);
        }



        //用于故障维修
        private static string xmldocpath = HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/MaintainManager/MalFunctionManager/WorkflowStateConfig.xml";
        static User userBll = new User();
        /// <summary>
        /// 创建工作流并且发送待办事务给具备权限的用户（不限公司）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisID"></param>
        /// <param name="title"></param>
        /// <param name="WorkflowName"></param>
        /// <param name="WorkflowEvent"></param>
        /// <param name="UserName"></param>
        /// <param name="PersonName"></param>
        /// <param name="URL"></param>
        /// <param name="type">0表示普通待办事务，1表示紧急待办事务</param>
        public static void CreateWorkflowAndSendingPendingOrder<T>(long thisID, string title, string WorkflowName, string WorkflowEvent, string UserName, string PersonName, string URL, short type) where T : IBasicEventService
        {
            //此处应用工作流
            Guid instanceId = WorkflowHelper.CreateNewInstance(thisID, WorkflowName);
            WorkflowHelper.SetStateMachine<T>(instanceId, WorkflowEvent);

            PendingOrderInfo poInfo = new PendingOrderInfo();
            poInfo.SenderPersonName = PersonName;
            poInfo.SendFrom = UserName;
            poInfo.SendTime = DateTime.Now;
            poInfo.Title = title;
            poInfo.Type = type;
            poInfo.URL = URL;
            poInfo.ReceiverAddress = "";
            List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, WorkflowHelper.GetCurrentStateName(instanceId), Guid.Empty);
            List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
            foreach (string approver in approverList)
            {
                PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
                receiver.UserName = approver;
                receiver.HasRead = false;
                receiverList.Add(receiver);
                poInfo.ReceiverAddress += approver + ";";
            }
            poInfo.Receivers = receiverList;
            FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
            poBll.SendPendingOrder(poInfo);
        }
        /// <summary>
        /// 发送待办事务给用户，不触发工作流
        /// </summary>
        /// <param name="title"></param>
        /// <param name="WorkflowName"></param>
        /// <param name="WorkflowStateName"></param>
        /// <param name="UserName"></param>
        /// <param name="PersonName"></param>
        /// <param name="URL"></param>
        /// <param name="type"></param>
        public static void SendingPendingOrderToUsers(string title, string UserName, string PersonName, string URL, short type, string CompanyID, params string[] Receivers)
        {
            PendingOrderInfo poInfo = new PendingOrderInfo();
            poInfo.SenderPersonName = PersonName;
            poInfo.SendFrom = UserName;
            poInfo.SendTime = DateTime.Now;
            poInfo.Title = title;
            poInfo.Type = type;
            poInfo.URL = URL;
            poInfo.ReceiverAddress = "";
            List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
            for (int i = 0; i < Receivers.Length; i++)
            {
                if (string.IsNullOrEmpty(Receivers[i]) || Receivers[i] == "WorkflowDefaultUser")
                {
                    continue;
                }
                LoginUserInfo info = userBll.GetLoginUser(Receivers[i]);
                if ((info.CompanyID != null && info.CompanyID == CompanyID) || CompanyID == null)
                {
                    PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
                    receiver.UserName = Receivers[i];
                    receiver.HasRead = false;
                    receiverList.Add(receiver);
                    poInfo.ReceiverAddress += Receivers[i] + ";";
                }
            }
            poInfo.Receivers = receiverList;
            FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
            poBll.SendPendingOrder(poInfo);
        }
        /// <summary>
        /// 发送待办事务给具备工作流状态角色的用户，不触发工作流
        /// </summary>
        /// <param name="title"></param>
        /// <param name="WorkflowName"></param>
        /// <param name="WorkflowStateName"></param>
        /// <param name="UserName"></param>
        /// <param name="PersonName"></param>
        /// <param name="URL"></param>
        /// <param name="type"></param>
        public static void SendingPendingOrderToStateUsers(string title, string WorkflowName, string WorkflowStateName, string UserName, string PersonName, string URL, short type, string CompanyID)
        {
            PendingOrderInfo poInfo = new PendingOrderInfo();
            poInfo.SenderPersonName = PersonName;
            poInfo.SendFrom = UserName;
            poInfo.SendTime = DateTime.Now;
            poInfo.Title = title;
            poInfo.Type = type;
            poInfo.URL = URL;
            poInfo.ReceiverAddress = "";
            List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
            List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, WorkflowStateName, Guid.Empty);
            foreach (string approver in approverList)
            {
                LoginUserInfo info = userBll.GetLoginUser(approver);
                if (info.CompanyID != null && info.CompanyID == CompanyID)
                {
                    PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
                    receiver.UserName = approver;
                    receiver.HasRead = false;
                    receiverList.Add(receiver);
                    poInfo.ReceiverAddress += approver + ";";
                }
            }
            poInfo.Receivers = receiverList;
            FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
            poBll.SendPendingOrder(poInfo);
        }
        /// <summary>
        /// 创建工作流并且发送待办事务给具备权限的用户（利用CompanyID来限定只发给固定公司的用户）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisID"></param>
        /// <param name="title"></param>
        /// <param name="WorkflowName"></param>
        /// <param name="WorkflowEvent"></param>
        /// <param name="UserName"></param>
        /// <param name="PersonName"></param>
        /// <param name="URL"></param>
        /// <param name="type">0表示普通待办事务，1表示紧急待办事务</param>
        /// <param name="CompanyID">公司ID</param>
        public static void CreateWorkflowAndSendingPendingOrder<T>(long thisID, string title, string WorkflowName, string WorkflowEvent, string UserName, string PersonName, string URL, short type, string CompanyID) where T : IBasicEventService
        {
            //此处应用工作流
            Guid instanceId = WorkflowHelper.CreateNewInstance(thisID, WorkflowName);
            WorkflowHelper.SetStateMachine<T>(instanceId, WorkflowEvent);
            //待办事务对象
            PendingOrderInfo poInfo = new PendingOrderInfo();
            poInfo.SenderPersonName = PersonName;
            poInfo.SendFrom = UserName;
            poInfo.SendTime = DateTime.Now;
            poInfo.Title = title;
            poInfo.Type = type;
            poInfo.URL = URL;
            poInfo.ReceiverAddress = "";
            //获取当前状态下需要通知的用户列表
            List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, WorkflowHelper.GetCurrentStateName(instanceId), Guid.Empty);
            List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
            foreach (string approver in approverList)
            {
                LoginUserInfo info = userBll.GetLoginUser(approver);
                if (info.CompanyID != null && info.CompanyID == CompanyID)
                {
                    PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
                    receiver.UserName = approver;
                    receiver.HasRead = false;
                    receiverList.Add(receiver);
                    poInfo.ReceiverAddress += approver + ";";
                }
            }
            poInfo.Receivers = receiverList;
            FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
            poBll.SendPendingOrder(poInfo);
        }

        #region 创建工作流并且发送待办事务给具备权限的用户（利用CompanyID来限定只发给固定公司的用户）更改下一个用户
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisID"></param>
        /// <param name="title"></param>
        /// <param name="WorkflowName"></param>
        /// <param name="WorkflowEvent"></param>
        /// <param name="UserName"></param>
        /// <param name="PersonName"></param>
        /// <param name="URL"></param>
        /// <param name="type"></param>
        /// <param name="CompanyID"></param>
        public static void CreateWorkflowAndSendingPendingOrder1<T>(long thisID, string title, string WorkflowName, string WorkflowEvent, string UserName, string PersonName, string URL, short type, string CompanyID) where T : IBasicEventService
        {
            //此处应用工作流
            Guid instanceId = WorkflowHelper.CreateNewInstance(thisID, WorkflowName);
            string nextUserName = "";
            WorkflowHelper.SetStateAndNextUserMachine<T>(instanceId, WorkflowEvent, WorkflowName, out nextUserName);
            //待办事务对象
            PendingOrderInfo poInfo = new PendingOrderInfo();
            poInfo.SenderPersonName = PersonName;
            poInfo.SendFrom = UserName;
            poInfo.SendTime = DateTime.Now;
            poInfo.Title = title;
            poInfo.Type = type;
            poInfo.URL = URL;
            poInfo.ReceiverAddress = "";
            //获取当前状态下需要通知的用户列表
            List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, WorkflowHelper.GetCurrentStateName(instanceId), Guid.Empty);
            List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
            foreach (string approver in approverList)
            {
                LoginUserInfo info = userBll.GetLoginUser(approver);
                if (info.CompanyID != null && info.CompanyID == CompanyID)
                {
                    PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
                    receiver.UserName = approver;
                    receiver.HasRead = false;
                    receiverList.Add(receiver);
                    poInfo.ReceiverAddress += approver + ";";
                }
            }
            poInfo.Receivers = receiverList;
            FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
            poBll.SendPendingOrder(poInfo);
        }
        #endregion



        /// <summary>
        /// 状态跳转，并发送待办事务给具备权限的用户（利用CompanyID来限定只发给固定公司的用户）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisID"></param>
        /// <param name="workflowName"></param>
        /// <param name="tableName"></param>
        /// <param name="eventName"></param>
        /// <param name="companyID"></param>
        public static void SetStateMachineAndSendingPendingOrder<T>(long thisID, string title, string WorkflowName, string TableName, string WorkflowEvent, string UserName, string PersonName, string URL, short type, string CompanyID) where T : IBasicEventService
        {
            //此处应用工作流
            WorkflowInstanceInfo wfinfo = WorkflowHelper.GetWorkflowInstanceInfo(TableName, thisID);
            WorkflowHelper.SetStateMachine<T>(wfinfo.InstanceID, WorkflowEvent);
            //待办事务对象
            SendingPendingOrderOnly(thisID, title, WorkflowName, TableName, UserName, PersonName, URL, type, CompanyID);
        }

        /// <summary>
        /// 仅仅用于工作流状态跳转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisID"></param>
        /// <param name="TableName"></param>
        /// <param name="WorkflowEvent"></param>
        public static void SetStateMachineOnly<T>(long thisID, string TableName, string WorkflowEvent) where T : IBasicEventService
        {
            //此处应用工作流
            WorkflowInstanceInfo wfinfo = WorkflowHelper.GetWorkflowInstanceInfo(TableName, thisID);
            WorkflowHelper.SetStateMachine<T>(wfinfo.InstanceID, WorkflowEvent);
        }

      

        /// <summary>
        ///更改工作流状态，,并自动更新下一审批者，同时发送待办事务
        /// </summary>       
        public static void SetStateMachineAndSendingPendingOrderAndNextUserMachine<T>(long thisID,string title,string URL,string WorkflowName, string WorkflowEvent,string TableName,string UserName, string PersonName,short type, string CompanyID) where T : IBasicEventService
        {
            WorkflowInstanceInfo wfinfo = WorkflowHelper.GetWorkflowInstanceInfo(TableName, thisID);
            string nextUserName = "";
            WorkflowHelper.SetStateAndNextUserMachine<T>(wfinfo.InstanceID, WorkflowEvent,WorkflowName,out nextUserName);
            SendingPendingOrderOnly(thisID, title,  WorkflowName,  TableName,  UserName,  PersonName,  URL,  type,  CompanyID);
        }
        
        
        
        ///// <summary>
        ///// 故障处理专用：创建工作流并且发送待办事务给具备权限的用户（利用CompanyID来限定只发给固定公司的用户）
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="thisID"></param>
        ///// <param name="title"></param>
        ///// <param name="WorkflowName"></param>
        ///// <param name="WorkflowEvent"></param>
        ///// <param name="UserName"></param>
        ///// <param name="PersonName"></param>
        ///// <param name="URL"></param>
        ///// <param name="type">0表示普通待办事务，1表示紧急待办事务</param>
        ///// <param name="CompanyID">公司ID</param>
        //public static void CreateWorkflowAndSendingPendingOrderForMalfunction<T>(MalfunctionHandleInfo item, string WorkflowName, string TableName, string WorkflowEvent, string UserName, string PersonName, short type) where T : IBasicEventService
        //{
        //    //此处应用工作流
        //    Guid instanceId = WorkflowHelper.CreateNewInstance(item.ID, WorkflowName);
        //    WorkflowHelper.SetStateMachine<T>(instanceId, WorkflowEvent);
        //    //获取当前状态
        //    string currentState = WorkflowHelper.GetCurrentStateName(instanceId);
        //    XmlDocument xmldoc = new XmlDocument();
        //    xmldoc.Load(xmldocpath);
        //    //如果是终止状态，发送消息并返回
        //    XmlNode endNode = xmldoc.SelectSingleNode("WorkflowStateConfig/WorkflowEndStates");
        //    foreach (XmlNode n in endNode.ChildNodes)
        //    {
        //        if (n.Name.Equals(currentState))
        //        {
        //            sendMessage(item, WorkflowName, TableName, WorkflowEvent, UserName, PersonName, type);
        //            return;
        //        }
        //    }
        //    //待办事务对象
        //    PendingOrderInfo poInfo = new PendingOrderInfo();
        //    poInfo.SenderPersonName = PersonName;
        //    poInfo.SendFrom = UserName;
        //    poInfo.SendTime = DateTime.Now;
        //    poInfo.Title = "维修处理：" + item.SheetNO + item.StatusString; ;
        //    poInfo.Type = type;
        //    poInfo.ReceiverAddress = "";
        //    //确定要转入的URL

        //    XmlNode node = xmldoc.SelectSingleNode("WorkflowStateConfig/StatesUrl/" + currentState);
        //    poInfo.URL = node.Attributes["url"].Value + "&id=" + item.ID;
        //    //获取当前状态下需要通知的用户列表
        //    List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, currentState, Guid.Empty);
        //    List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
        //    foreach (string approver in approverList)
        //    {
        //        LoginUserInfo info = userBll.GetLoginUser(approver);
        //        if (info.CompanyID != null && info.CompanyID == item.CompanyID)
        //        {
        //            PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
        //            receiver.UserName = approver;
        //            receiver.HasRead = false;
        //            receiverList.Add(receiver);
        //            poInfo.ReceiverAddress += approver + ";";
        //        }
        //    }
        //    poInfo.Receivers = receiverList;
        //    FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
        //    poBll.SendPendingOrder(poInfo);
        //}

        ///// <summary>
        ///// 故障处理专用：状态跳转，并发送待办事务给具备权限的用户（利用CompanyID来限定只发给固定公司的用户）
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="thisID"></param>
        ///// <param name="workflowName"></param>
        ///// <param name="tableName"></param>
        ///// <param name="eventName"></param>
        ///// <param name="companyID"></param>
        //public static void SetStateMachineAndSendingPendingOrderForMalfunction<T>(MalfunctionHandleInfo item, string WorkflowName, string TableName, string WorkflowEvent, string UserName, string PersonName, short type) where T : IBasicEventService
        //{
        //    //此处应用工作流
        //    WorkflowInstanceInfo wfinfo = WorkflowHelper.GetWorkflowInstanceInfo(TableName, item.ID);
        //    WorkflowHelper.SetStateMachine<T>(wfinfo.InstanceID, WorkflowEvent);
        //    //获取当前状态
        //    string currentState = WorkflowHelper.GetCurrentStateName(wfinfo.InstanceID);
        //    XmlDocument xmldoc = new XmlDocument();
        //    xmldoc.Load(xmldocpath);
        //    //如果是终止状态，发送消息并返回
        //    XmlNode endNode = xmldoc.SelectSingleNode("WorkflowStateConfig/WorkflowEndStates");
        //    foreach (XmlNode n in endNode.ChildNodes)
        //    {
        //        if (n.Name.Equals(currentState))
        //        {
        //            sendMessage(item, WorkflowName, TableName, WorkflowEvent, UserName, PersonName, type);
        //            return;
        //        }
        //    }
        //    //待办事务对象
        //    PendingOrderInfo poInfo = new PendingOrderInfo();
        //    poInfo.SenderPersonName = PersonName;
        //    poInfo.SendFrom = UserName;
        //    poInfo.SendTime = DateTime.Now;
        //    poInfo.Title = "维修处理：" + item.SheetNO + item.StatusString;
        //    poInfo.Type = type;
        //    poInfo.ReceiverAddress = "";
        //    //确定要转入的URL
        //    XmlNode node = xmldoc.SelectSingleNode("WorkflowStateConfig/StatesUrl/" + currentState);
        //    poInfo.URL = node.Attributes["url"].Value + "&id=" + item.ID;
        //    //获取当前状态下需要通知的用户列表
        //    List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, currentState, Guid.Empty);
        //    List<PendingOrderReceiverInfo> receiverList = new List<PendingOrderReceiverInfo>();
        //    foreach (string approver in approverList)
        //    {
        //        LoginUserInfo info = userBll.GetLoginUser(approver);
        //        if (info.CompanyID != null && info.CompanyID == item.CompanyID)
        //        {
        //            PendingOrderReceiverInfo receiver = new PendingOrderReceiverInfo();
        //            receiver.UserName = approver;
        //            receiver.HasRead = false;
        //            receiverList.Add(receiver);
        //            poInfo.ReceiverAddress += approver + ";";
        //        }
        //    }
        //    poInfo.Receivers = receiverList;
        //    FM2E.BLL.PendingOrder.PendingOrder poBll = new FM2E.BLL.PendingOrder.PendingOrder();
        //    poBll.SendPendingOrder(poInfo);
        //}
        //private static void sendMessage(MalfunctionHandleInfo item, string WorkflowName, string TableName, string WorkflowEvent, string UserName, string PersonName, short type)
        //{
        //    try
        //    {
        //        MessageInfo msg = new MessageInfo();
        //        //消息类型
        //        msg.Type = 0;
        //        //发送方式
        //        msg.SendWay = 0;
        //        //标题
        //        msg.Title = "维修处理：" + item.SheetNO + item.StatusString;
        //        //消息内容
        //        msg.Message = msg.Title;
        //        //发送时间
        //        msg.MessageTime = DateTime.Now;
        //        //发送人
        //        msg.SendFrom = UserName;
        //        msg.Attachment = "";
        //        WorkflowInstanceInfo wfinfo = WorkflowHelper.GetWorkflowInstanceInfo(TableName, item.ID);
        //        string currentState = WorkflowHelper.GetCurrentStateName(wfinfo.InstanceID);
        //        List<string> approverList = WorkflowHelper.GetAllApprover(WorkflowName, currentState, Guid.Empty);
        //        List<MessageReceiverInfo> receiverList = new List<MessageReceiverInfo>();
        //        foreach (string approver in approverList)
        //        {
        //            LoginUserInfo info = userBll.GetLoginUser(approver);
        //            if (info.CompanyID != null && info.CompanyID == item.CompanyID)
        //            {
        //                MessageReceiverInfo receiver = new MessageReceiverInfo();
        //                receiver.UserName = approver;
        //                receiver.HasRead = false;
        //                receiverList.Add(receiver);
        //                msg.ReceiverAddress += approver + ";";
        //            }
        //        }
        //        msg.Receivers = receiverList;
        //        FM2E.BLL.Message.Message mBll = new FM2E.BLL.Message.Message();
        //        mBll.SendMessage(msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
