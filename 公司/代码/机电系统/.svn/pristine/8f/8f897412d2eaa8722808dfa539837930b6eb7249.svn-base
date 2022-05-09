using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using System.Collections.Generic;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;





public partial class Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_AllEquipmentInfo_Selectsupplier : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string selecttype = (string)Common.sink("selecttype", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            //Process();
        }
    }
    private void FillData()
    {
        try
        {
            Supplier bll = new Supplier();
            int listCount = 0;
            QueryParam searchTerm = (QueryParam)ViewState["SearchTerm"];
            if (searchTerm == null)
            {
                searchTerm = new QueryParam(1, 10);
                searchTerm.Where = "";
            }
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            IList list = bll.GetList(searchTerm, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["SupplierID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewSupplier.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                Supplier bll = new Supplier();
                bll.DelSupplier(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败：" + ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            SupplierInfo item = (SupplierInfo)e.Row.DataItem;
            e.Row.Attributes["SupplierID"] = item.SupplierID.ToString();
            CheckBox cb = (CheckBox)e.Row.FindControl("checkBox1");
            if (cb != null)
                cb.Attributes.Add("onclick", "onClientClick('" + cb.ClientID + "','" + item.Name + "','" + item.SupplierID + "')");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SupplierInfo item = new SupplierInfo();
        item.Name = Common.inSQL(TextBox1.Text.Trim());
        item.Product = Common.inSQL(TextBox2.Text.Trim());
        item.Address = Common.inSQL(TextBox3.Text.Trim());
        item.ResName = Common.inSQL(TextBox4.Text.Trim());
        if (TextBox5.Text.Trim() != "")
            item.Credit = Convert.ToInt32(TextBox5.Text.Trim());
        else
            item.Credit = 0;

        Supplier bll = new Supplier();
        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        TabContainer1.ActiveTabIndex = 0;
        ViewState["SearchTerm"] = searchTerm;
        FillData();
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(typeof(string), "closeModalPopup", "<script language='javascript'>window.returnVal='" + SelectedID.Value + "||" + SelectedName.Value+"||"+selecttype + "';window.parent.hidePopWin(true);</script>");
    }



}
