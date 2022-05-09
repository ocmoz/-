using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using System.Collections;
using WebUtility.Components;

public partial class Module_FM2E_SystemManager_OnlineUserManager_OnlineUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            InitialPage();
        }
    }

    private void InitialPage()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[5].Visible = true;
        else GridView1.Columns[5].Visible = false;

    }
    private void BindData()
    {

        AspNetPager1.RecordCount = SystemPermission.UserOnlineList.AllCount;
        ArrayList lst = new ArrayList();
        for (int i = AspNetPager1.StartRecordIndex; i <= AspNetPager1.EndRecordIndex; i++)
        {
            lst.Add(SystemPermission.UserOnlineList.GetList[i - 1]);
        }
        GridView1.DataSource = lst;
        GridView1.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "OutOnline")
        {
            SystemPermission.CheckPermissionWithHint(PopedomType.Delete);

            SystemLogin.RemoveOnlineUser(e.CommandArgument.ToString().ToLower());

            MessageBox MBx = new MessageBox();
            MBx.M_Type = Msg_Type.Info;
            MBx.M_Title = "强制用户退出!";
            MBx.M_IconType = Icon_Type.OK;
            MBx.M_Body = string.Format("强制用户({0})退出成功！", e.CommandArgument.ToString());
            MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", Common.GetHomeBaseUrl("OnlineUser.aspx"), "", UrlType.Href, true));
            EventMessage.MessageBox(MBx);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
        }
    }
}
