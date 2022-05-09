using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_BasicData_SystemManage_ViewSubSystem : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string id = Convert.ToString((int)Common.sink("id", MethodType.Get, 50, 0, DataType.Int));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            BindData();
        }
    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[1];
        button.ButtonUrlType = UrlType.JavaScript;
        button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);

        button = HeadMenuWebControls1.ButtonList[0];
        button.ButtonUrlType = UrlType.Href;
        button.ButtonUrl = string.Format("EditSubSystem.aspx?cmd=edit&id={0}", id);
        if (cmd == "delete")
        {
            bool success = false;
            string pid="";
            try
            {
                EquipmentSystem bll = new EquipmentSystem();
                pid = bll.GetSubSystem(id).ParentSystemID;
                bll.DelSubSystem(id);
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除子系统失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (success)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除系统(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ViewSystem.aspx?cmd=view&id="+pid), UrlType.Href, "");

            }
        }
    }

    private void BindData()
    {
        try
        {
            EquipmentSystem bll = new EquipmentSystem();
            SubEquipmentSystemInfo item = bll.GetSubSystem(id);

            Session["EquipmentSystemInfo"] = item;

            Label1.Text = item.SubSystemID.ToString();
            Label2.Text = item.SubSystemName;
            Label3.Text = item.ParentSystemID;
            Label4.Text = item.Remark;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
