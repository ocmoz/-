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

public partial class Module_FM2E_BasicData_SystemManage_ViewSystem : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string id = (string)Common.sink("id", MethodType.Get, 2, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            BindData();
            FillData();
        }
    }
    private void FillData()
    {
        try
        {
            EquipmentSystem bll = new EquipmentSystem();
            IList list = bll.GetAllSubSystem(id);

            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void BindData()
    {
        try
        {
            EquipmentSystem equipmentSystem = new EquipmentSystem();
            EquipmentSystemInfo item = equipmentSystem.GetSystem(id);

            Session["EquipmentSystemInfo"] = item;

            Label1.Text = item.SystemID;
            Label2.Text = item.SystemName;
            Label3.Text = item.Remark;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void ButtonBind()
    {
        //删除
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[2];
        button.ButtonUrlType = UrlType.JavaScript;
        button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
        //修改
        button = HeadMenuWebControls1.ButtonList[1];
        button.ButtonUrlType = UrlType.Href;
        button.ButtonUrl = string.Format("EditSystem.aspx?cmd=edit&id={0}", id);
        //增加子系统
        button = HeadMenuWebControls1.ButtonList[0];
        button.ButtonUrlType = UrlType.Href;
        button.ButtonUrl = string.Format("EditSubSystem.aspx?cmd=add&id={0}", id);
        //返回
        button = HeadMenuWebControls1.ButtonList[3];
        button.ButtonUrlType = UrlType.Href;
        button.ButtonUrl = "System.aspx";
        if (cmd == "delete")
        {
            bool success = false;
            try
            {
                EquipmentSystem bll = new EquipmentSystem();
                bll.DelSystem(id);
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除系统失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (success == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除系统(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("System.aspx"), UrlType.Href, "");
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string d = gvRow.Attributes["SubSystemID"];
            Response.Redirect("ViewSubSystem.aspx?cmd=view&id=" + d);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                string d = Convert.ToString(gvRow.Attributes["SubSystemID"]);
                EquipmentSystem bll = new EquipmentSystem();
                bll.DelSubSystem(d);
                FillData();
                ButtonBind();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
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

            SubEquipmentSystemInfo item = (SubEquipmentSystemInfo)e.Row.DataItem;
            e.Row.Attributes["SubSystemID"] = item.SubSystemID.ToString();
        }
    }

}