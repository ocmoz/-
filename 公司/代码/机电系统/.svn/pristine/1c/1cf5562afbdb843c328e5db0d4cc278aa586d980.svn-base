using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FM2E.BLL.System;
using System.Collections;
using FM2E.Model.System;
using System.Text;
using WebUtility;
using WebUtility.Components;

/// <summary>
///MenuService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class MenuService : System.Web.Services.WebService
{

    public MenuService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string GetSubMenu(string userName,string moduleID)
    {
        StringBuilder htmlText=new StringBuilder();
        try
        {

            Module module = new Module();
            IList moduleList = null;
            LoginUserInfo userInfo = UserData.CurrentUserData;
            if (userInfo.IsAdministrator)   //超级管理员拥有所有权限
                moduleList = module.GetSubModules(moduleID, false);
            else moduleList = module.GetUserModules(userName, moduleID);

            int i = 0;
            foreach (ModuleInfo item in moduleList)
            {
                if (i != 0)
                    htmlText.Append("<div class=\"subMenuDiv\">");
                else htmlText.Append("<div class=\"subMenuFirstDiv\">");
                i++;
                htmlText.Append("\n");

                htmlText.AppendFormat("<span class=\"subMenuTitle\">{0}</span>", item.Name);
                htmlText.Append("\n");

                if (item.ChildCount > 0)
                {
                    IList subList = null;
                    if (userInfo.IsAdministrator)  //超级管理员
                        subList = module.GetSubModules(item.ModuleID, false);
                    else subList = module.GetUserModules(userName, item.ModuleID);

                    if (subList.Count != 0)
                    {
                        htmlText.Append("<ul class=\"subMenuUlItem\">");
                        htmlText.Append("\n");

                        foreach (ModuleInfo subItem in subList)
                        {
                            htmlText.AppendFormat("<li onmouseover=\"javascript:menuMouseOver(this);\" onmouseout=\"javascript:menuMouseOut(this);\" onclick=\"javascript:ClickMenu('{0}');\">{1}</li>", subItem.Directory.Trim(), subItem.Name);
                        }
                        htmlText.Append("</ul>\n");
                    }
                }
                else
                {
                    htmlText.Append("<ul class=\"subMenuUlItem\">");
                    htmlText.Append("\n");
                    htmlText.AppendFormat("<li onmouseover=\"javascript:menuMouseOver(this);\" onmouseout=\"javascript:menuMouseOut(this);\" onclick=\"javascript:ClickMenu('{0}');\">{1}</li>", item.Directory.Trim(), item.Name);
                    htmlText.Append("</ul>\n");
                }

                htmlText.Append("</div>\n");
            }
        }
        catch (Exception e)
        {
            htmlText.Remove(0, htmlText.Length);
            htmlText.Append("获取子菜单失败");
            EventMessage.EventWriteLog(Msg_Type.Error, "获取子菜单失败：" + e.Message);
        }
        return htmlText.ToString();
    }
}

