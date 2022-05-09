using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FM2E.BLL.System;
using System.Collections;
using FM2E.Model.System;
using System.Text;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using WebUtility;

public partial class Default : System.Web.UI.Page
{
    LoginUserInfo userInfo = UserData.CurrentUserData;
    Module module = new Module();
    public StringBuilder sb_TopHTMLSrc = new StringBuilder();
    public StringBuilder sb_DownHTMLSrc = new StringBuilder();
    public StringBuilder sb1 = new StringBuilder();
    public string menuTitleSrc = "";

    public int TopMenuCount = 0;

    public string currentUser = "未知用户";

    Warehouse warehouseBll = new Warehouse();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            currentUser = Common.Get_UserName;
            if (currentUser == null)
                currentUser = "未知用户";

            WarehouseInfo warehouse = warehouseBll.GetWarehouseByUserName(currentUser);
            if (warehouse != null && warehouse.WareHouseID != null && warehouse.WareHouseID != "")
            {
                Label_WarehouseName.Text = "仓库：" + warehouse.Name;
            }
            CreateMenu();
            Create();
        }
    }

    private void CreateMenu()
    {
        if (currentUser == "未知用户")
            return;

        int i = 1;
        IList moduleList = null;
        if (userInfo.IsAdministrator)   //超级管理员拥有所有权限
            moduleList = module.GetSubModules(Guid.Empty.ToString("N"), false);
        else moduleList = module.GetUserModules(currentUser, Guid.Empty.ToString("N"));

        TopMenuCount = moduleList.Count;
        foreach (ModuleInfo item in moduleList)
        {
            if (item.ChildCount > 0)  //有子模块
            {
                sb_TopHTMLSrc.AppendFormat(" <td id={0}menu_{1}{0} class={0}topmenuoff{0} onmouseover={0}javascript:ImageOverOROut('menu_{1}','v'){0} onmouseout={0}javascript:ImageOverOROut('menu_{1}','o'){0} onclick={0}javascript:NowShow('menu_','{1}'){0} style={0}width: 120px{0}><font size='3'>{2}</font></td>", "\"", i, item.Name);//
                sb_TopHTMLSrc.Append("\n");

                IList subList = null;
                if (userInfo.IsAdministrator)  //超级管理员
                    subList = module.GetSubModules(item.ModuleID, false);
                else subList = module.GetUserModules(currentUser, item.ModuleID);

                if (subList.Count <= 0)
                {
                    i++;
                    continue;
                }

                sb_DownHTMLSrc.AppendFormat("<table id={0}menu_{1}_table{0} border={0}0{0} cellpadding={0}0{0} cellspacing={0}0{0} width={0}100%{0} style={0}position:relative; left:10px; top:0px;display:none; {0}><tr><td class={0}topmenuLeft{0}>&nbsp;</td>", "\"", i);
                int j = 1;

                foreach (ModuleInfo subItem in subList)
                {
                    if (subItem.ChildCount > 0)
                        sb_DownHTMLSrc.AppendFormat("<td class={0}topmenuoff2 topmenuItem{0} valign={0}middle{0} id={0}menu_{1}_{2}{0} onmouseover={0}javascript:xImageOverOROut('menu_{1}_{2}','v'){0} onmouseout={0}javascript:xImageOverOROut('menu_{1}_{2}','o'){0} onclick={0}javascript:xNowShowWithSubMenu('menu_{1}_{2}','{4}','{5}','{6}'){0}><font size='3'>{3}</font></td>", "\"", i, j, subItem.Name, subItem.Directory, subItem.ModuleID, Common.Get_UserName);
                    else sb_DownHTMLSrc.AppendFormat("<td class={0}topmenuoff2 topmenuItem{0} valign={0}middle{0} id={0}menu_{1}_{2}{0} onmouseover={0}javascript:xImageOverOROut('menu_{1}_{2}','v'){0} onmouseout={0}javascript:xImageOverOROut('menu_{1}_{2}','o'){0} onclick={0}javascript:xNowShow('menu_{1}_{2}','{4}'){0}><font size='3'>{3}</font></td>", "\"", i, j, subItem.Name, subItem.Directory);
                    sb_DownHTMLSrc.Append("\n");
                    if (j != subList.Count)
                    {
                        //加分隔符
                        sb_DownHTMLSrc.AppendFormat("  <td class={0}topmenuSeperator{0}><img style={0} border-width:0;{0} src={0}images/Index/top-b4-c.gif{0} width={0}1px{0} height={0}19{0}/></td>", "\"");
                        sb_DownHTMLSrc.Append("\n");
                    }
                    j++;
                }
                while (j <= i)
                {
                    sb_DownHTMLSrc.AppendFormat("<td class={0}topmenuoff2 topmenuItem{0} valign={0}middle{0}>&nbsp;</td>", "\"");
                    sb_DownHTMLSrc.Append("\n");
                    j++;
                }
                sb_DownHTMLSrc.AppendFormat("<td class={0}topmenuRight{0}>&nbsp;</td><td>&nbsp;</td></tr></table>", "\"");
                sb_DownHTMLSrc.Append("\n");
            }
            else
            {
                sb_TopHTMLSrc.AppendFormat(" <td id={0}menu_{1}{0} class={0}topmenuoff{0} onmouseover={0}javascript:ImageOverOROut('menu_{1}','v'){0} onmouseout={0}javascript:ImageOverOROut('menu_{1}','o'){0} onclick={0}javascript:NowClick('menu_','{1}','{3}'){0} style={0}width: 120px{0}><font size='3'>{2}</font></td>", "\"", i, item.Name, item.Directory);
                sb_TopHTMLSrc.Append("\n");
            }
            i++;
        }
        sb_TopHTMLSrc.AppendFormat("<td class={0}topmenuoff{0}>&nbsp;</td>", "\"");
    }

    private void Create()
    {
        if (currentUser == "未知用户")
            return;
        IList moduleList = null;

        if (userInfo.IsAdministrator)   //超级管理员拥有所有权限
            moduleList = module.GetSubModules(Guid.Empty.ToString("N"), false);
        else
            moduleList = module.GetUserModules(currentUser, Guid.Empty.ToString("N"));
        GetChildMenu(moduleList, 1);
    }
    int j = 1;
    public void GetChildMenu(IList moduleList, int i)
    {
        foreach (ModuleInfo item in moduleList)
        {
            if (item.ChildCount > 0)
            {//<img src='images/tree/ico/2.gif' align='absMiddle'/></span>
                sb1.AppendFormat("<li class='{2}1'><a href='javascript:c(m{0}{3});' id='m{0}{3}'><span><font size='3'>{1}</font></a></li>", i, item.Name, "l" + i.ToString(), j);
                sb1.Append("\n");
                IList subList = null;
                if (userInfo.IsAdministrator)  //超级管理员
                    subList = module.GetSubModules(item.ModuleID, false);
                else
                    subList = module.GetUserModules(currentUser, item.ModuleID);
                sb1.AppendFormat("<ul id='m{0}{1}d' style='display:none;' class='U1'>", i, j);
                GetChildMenu(subList, i + 1);
                sb1.AppendFormat("</ul>");
            }
            else
            {
                sb1.AppendFormat("<li class='{3}2'><a href={0}javascript:NowClick1('{2}');{0}><span><img src='images/tree/ico/2.gif' align='absMiddle'/><font size='3'>{1}</font></span></a></li>", "\"", item.Name, item.Directory, "l" + i.ToString());
            }
            sb1.Append("\n");
            j++;
        }
    }
}
