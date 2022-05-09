using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FM2E.BLL.PendingOrder;
using FM2E.Model.PendingOrder;
using System.Text;

public class NewPendingOrderInfo
{
    private string id;
    private string sender;
    private string senderName;
    private string sendTime;
    private string msg;
    private string msgType;
    private string url;
    private string count="0";

    public string MsgID
    {
        get { return id; }
        set { id = value; }
    }
    public string Sender
    {
        get { return sender; }
        set { sender = value; }
    }
    public string SenderName
    {
        get { return senderName; }
        set { senderName = value; }
    }
    public string SendTime
    {
        get { return sendTime; }
        set { sendTime = value; }
    }
    public string Msg
    {
        get { return msg; }
        set { msg = value; }
    }
    public string MsgType
    {
        get { return msgType; }
        set { msgType = value; }
    }
    public string URL
    {
        get { return url; }
        set { url = value; }
    }
    public string Count
    {
        get { return count; }
        set { count = value; }
    }
}

/// <summary>
///WebService_NewPendingOrder 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
 [System.Web.Script.Services.ScriptService]
public class WebService_NewPendingOrder : System.Web.Services.WebService
{
    private readonly PendingOrder orderBll = new PendingOrder();
    private const string LASTTIMEORDER_SESSION = "lastTimeSession";

    public WebService_NewPendingOrder()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    /// <summary>
    /// 获取指定用户的新待办事务数量
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [WebMethod]
    public int GetNewPendingOrderCount()
    {
        //onsuccess //onfail //usercontext
        int count = 0;
        try
        {
            string username = WebUtility.Common.Get_UserName;
            count = orderBll.GetNewPendingOrderNumber(username);
        }
        catch(Exception ex)
        {
            WebUtility.EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Error, "无法更新用户" + "的消息记录。原因：" + ex.Message);
        }
        return count;
    }

    /// <summary>
    /// 获取弹出式消息通知框
    /// </summary>
    /// <returns>消息通知框的html代码</returns>
    [WebMethod]
    public string GetNofityBox()
    {
        //onsuccess //onfail //usercontext
        string htmlString= @"<div id=""HintBox"">
          <table style=""width: 100%; "" cellpadding=""0"" cellspacing=""0"">
            <tr>
                <td  class=""HintBoxtitle"">
	                <div class=""CloseButton""></div>
	            </td>
            </tr>
            <tr>
                <td class=""HintBoxbody"">
                 <div class=""SendBy"">{0} {1} ：</div>
                    <div class=""NewMessage"">
                        <span style=""color: Red;"">NEW&nbsp;&nbsp;</span>
                        <a href=""{5}"" hidefocus=""true"" id='btCommand1'>{2}<span style=""color: Red;"">（{3}）</span></a>
                    </div>
                    <div class=""OtherMessage"">
                        <a href=""Module/FM2E/PendingOrderMessage/ViewPendingOrder.aspx"" hidefocus=""true"" id='btCommand2'>您还有{4}件待办事务</a>
                    </div>
                </td>
            </tr>
        </table></div>";


        try
        {
            string username = WebUtility.Common.Get_UserName;
            PendingOrderInfo item = null;

            DateTime lastTime = DateTime.MinValue; ;
            //if (Session[LASTTIMEORDER_SESSION] != null)
            //    lastTime = (DateTime)Session[LASTTIMEORDER_SESSION];

            int count = 0;
            if (DateTime.Compare(DateTime.MinValue,lastTime)==0)
                item = orderBll.GetNewPendingOrder(username,out count);
            else item = orderBll.GetNewPendingOrder(username, lastTime, out count);

            if (item != null)
            {
                htmlString = string.Format(htmlString, item.SenderPersonName + "(" + item.SendFrom + ")", item.SendTime.ToString("yyyy-MM-dd HH:mm:ss"), item.Title, item.TypeString, count, item.URL);
            }
        }
        catch (Exception ex)
        {
            htmlString = "";
            WebUtility.EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Error, "无法获取用户最新待办事项。原因：" + ex.Message);
        }
        return htmlString;
    }

    /// <summary>
    /// 获取弹出式消息通知框
    /// </summary>
    /// <returns>消息通知框的html代码</returns>
    [WebMethod]
    public NewPendingOrderInfo GetNewPendingOrder()
    {
        NewPendingOrderInfo newMsg = null;
        try
        {
            string username = WebUtility.Common.Get_UserName;
            PendingOrderInfo item = null;

            DateTime lastTime = DateTime.MinValue; ;
            //if (Session[LASTTIMEORDER_SESSION] != null)
            //    lastTime = (DateTime)Session[LASTTIMEORDER_SESSION];

            int count = 0;
            if (DateTime.Compare(DateTime.MinValue, lastTime) == 0)
                item = orderBll.GetNewPendingOrder(username, out count);
            else item = orderBll.GetNewPendingOrder(username, lastTime, out count);

            if (item != null)
            {
                newMsg = new NewPendingOrderInfo();
                newMsg.MsgID = item.ID.ToString();
                newMsg.MsgType = item.TypeString;
                newMsg.Sender = item.SendFrom;
                newMsg.SenderName = item.SenderPersonName;
                newMsg.SendTime = item.SendTime.ToString("yyyy-MM-dd HH:mm:ss");
                newMsg.Msg = item.Title;
                newMsg.URL = "Module/FM2E" + item.URL.Substring(2, item.URL.Length - 2);
                newMsg.Count = count.ToString();
            }
        }
        catch (Exception ex)
        {
            newMsg = null;
            WebUtility.EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Error, "无法获取用户最新待办事项。原因：" + ex.Message);
        }
        return newMsg;
    }
}

