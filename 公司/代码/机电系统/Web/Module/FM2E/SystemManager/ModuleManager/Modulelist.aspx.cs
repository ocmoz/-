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

using FM2E.BLL.System;
using FM2E.Model.System;
using WebUtility;
using WebUtility.Components;

public partial class Module_FM2E_SystemManager_ModuleManager_Modulelist : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            PermissionControl();
        }
    }
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[6].Visible = true ;
        else GridView1.Columns[6].Visible = false ;

        if (SystemPermission.CheckPermission(PopedomType.Edit))
            TabContainer1.Tabs[1].Visible = true;
        else TabContainer1.Tabs[1].Visible = false;
    }
    private void FillData()
    {
        try
        { 
            Module module = new Module();
           // int recordCount = 0;
          //  IList list = module.GetSubModuleByPage(Guid.Empty.ToString("N"), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize,out recordCount);
            IList list = module.GetSubModules(Guid.Empty.ToString("N"), true);

            GridView1.DataSource = list;
            GridView1.DataBind();

          //  AspNetPager1.RecordCount = recordCount;

            ReorderList1.DataSource = list;
            ReorderList1.DataBind();

            List<string> order = new List<string>();
            for(int i=0;i<list.Count;i++)
            {
                order.Add(((ModuleInfo)list[i]).ModuleID);
            }
            Session["order"] = order;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取模块分类数据失败" ,ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        try
        {
            List<string> order = (List<string>)Session["order"];
            Module module = new Module();
            module.OrderModules(order.ToArray());
            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "模块分类排序失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "模块分类排序成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Modulelist.aspx"), UrlType.Href, "");
        }
    }
    protected void ReorderList1_ItemReorder(object sender, AjaxControlToolkit.ReorderListItemReorderEventArgs e)
    {
        List<string> order = (List<string>)Session["order"];
        if (order == null)
            return;

        string tmp = order[e.OldIndex];
        order.RemoveAt(e.OldIndex);
        order.Insert(e.NewIndex, tmp);

        Session["order"] = order;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string id = gvRow.Attributes["ModuleID"];

        if (e.CommandName == "view")
        {
            Response.Redirect("ModuleManager.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            if (gvRow.Attributes["IsSystem"].ToLower()=="true")
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "不能删除系统模块", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            bool bSuccess = false;
            try
            {
                Module bll = new Module();
                bll.DeleteModule(id);
                bSuccess = true;
               // GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Visible = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除模块失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除模块成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Modulelist.aspx?"), UrlType.Href, "");
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

            ModuleInfo item = (ModuleInfo)e.Row.DataItem;
            e.Row.Attributes["ModuleID"] = item.ModuleID;
            e.Row.Attributes["IsSystem"] = item.IsSystem.ToString();
        }  
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
}
