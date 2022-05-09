using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;

public partial class Module_FM2E_SystemManager_SystemState_SystemState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label1.Text = SystemConfig.Instance.SystemName + " " + SystemConfig.Instance.Version;
            Label2.Text = SystemPermission.UserOnlineList.AllCount.ToString();
            Label3.Text = HttpRuntime.Cache.Count.ToString();
            Label4.Text = string.Format("{0}M", HttpRuntime.Cache.EffectivePrivateBytesLimit / 1024 / 1024);
            Label5.Text = Common.GetServerOS;
            Label6.Text = GetSystemRunTime;
            Label7.Text = GetAppRunTime;

            System.Diagnostics.Process ps = System.Diagnostics.Process.GetCurrentProcess();

            Label8.Text = string.Format("{0}M", ps.WorkingSet64 / 1024 / 1024);
            Label9.Text = string.Format("{0}M", ps.VirtualMemorySize64 / 1024 / 1024);
            PermissionControl();
        }
        //需要加入是否可清空的权限验证
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            TabContainer1.Tabs[1].Visible = true;
        else TabContainer1.Tabs[1].Visible = false;
    }
    private string GetAppRunTime
    {
        get
        {
            TimeSpan span = DateTime.Now - SystemPermission.AppStartTime;
            string result = span.Days.ToString() + "天 ";
            result += span.Hours.ToString() + "小时 ";
            result += span.Minutes.ToString() + "分 ";
            result += span.Seconds.ToString() + "秒";
            return result;
        }
    }

    private string GetSystemRunTime
    {
        get
        {
            int t = Environment.TickCount;
            if (t < 0) t = t + int.MaxValue;
            t = t / 1000;
            TimeSpan span = TimeSpan.FromSeconds(t);
            string result = span.Days.ToString() + "天 ";
            result += span.Hours.ToString() + "小时 ";
            result += span.Minutes.ToString() + "分 ";
            result += span.Seconds.ToString() + "秒";
            return result;
        }
    }
    /// <summary>
    /// 清空缓存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        SystemPermission.CheckPermissionWithHint(PopedomType.Delete);

        IDictionaryEnumerator id = HttpRuntime.Cache.GetEnumerator();
        while (id.MoveNext())
        {
            DictionaryEntry abc = id.Entry;
            string Tempstring = (string)id.Key;
            HttpRuntime.Cache.Remove(Tempstring);
        }
        EventMessage.MessageBox(Msg_Type.Info, "清空缓存!", "成功清空所有web缓存.", Icon_Type.OK, Common.GetScriptUrl);
    }
    /// <summary>
    /// 重启应用程序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        SystemPermission.CheckPermissionWithHint(PopedomType.Delete);

        SystemLogin.UserOut();
        Response.Clear();
        Response.Write("Web应用程序已经重启, 请点击此处<a href=\"javascript:parent.window.location.href='" + Page.ResolveClientUrl("~/") + "Default.aspx'\">重新登入</a>.");
        Response.Flush();
        Response.Close();
        EventMessage.EventWriteLog(Msg_Type.Info, "重启Web应用程序成功!");
        HttpRuntime.UnloadAppDomain();      
    }
}
