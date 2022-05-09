using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
///WebService_NewMessage 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
 [System.Web.Script.Services.ScriptService]
public class WebService_NewMessage : System.Web.Services.WebService
{

    public WebService_NewMessage()
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
    /// 获取指定用户的新消息数量
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [WebMethod]
    public int GetNewMessageCount()
    {
        //onsuccess //onfail //usercontext
        int count = 0;
        try
        {
            string username = WebUtility.Common.Get_UserName;
            count =  new FM2E.BLL.Message.Message().GetNewMessageNumber(username);
        }
        catch(Exception ex)
        {
            WebUtility.EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Error, "无法更新用户" + "的消息记录。原因：" + ex.Message);
        }
        return count;
    }

}

