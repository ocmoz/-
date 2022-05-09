using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;
using System.Workflow.Activities;

using FM2E.WorkflowLayer;
using WebUtility;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global: System.Web.HttpApplication
{
    public Global()
    {
    }

    protected void Application_Start( object sender, EventArgs e )
    {
        try
        {
            //建立并初始化全局工作流引擎
            WorkflowHelper.InitializeWorkflowRuntime();
            EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Info, "FM2E application started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Error, "FM2E 应用程序启动失败：" + ex.Message);
        }
    }

    protected void Application_End( object sender, EventArgs e )
    {
        try
        {
            WorkflowHelper.CurrentRuntime.StopRuntime();
            WorkflowHelper.CurrentRuntime.Dispose();
            EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Info, "FM2E application ended at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Error, "FM2E 应用程序关闭时发生错误：" + ex.Message);
        }
    }
}