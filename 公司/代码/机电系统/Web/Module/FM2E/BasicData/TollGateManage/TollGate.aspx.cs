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
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using FM2E.Model.Utils;

public partial class Module_FM2E_BasicData_TollGateManage_TollGate : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            Process();
            PermissionControl();
        }
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }

    private void FillData()
    {
        TollGate bll = new TollGate();
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
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string id = Convert.ToString(gvRow.Attributes["TollGateID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewTollGate.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                TollGate TollGate = new TollGate();
                TollGate.DelTollGate(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
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
            TollGateInfo item = (TollGateInfo)e.Row.DataItem;
            e.Row.Attributes["TollGateID"] = item.TollGateID.ToString();
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            TollGateInfo item = new TollGateInfo();
            item.TollGateID = Common.inSQL(TextBox1.Text.Trim());
            item.TollGateName = Common.inSQL(TextBox2.Text.Trim());
            item.CompanyName = Common.inSQL(TextBox3.Text.Trim());
            item.SectionName = Common.inSQL(TextBox4.Text.Trim());
            item.IsDeleted = false;
            TollGate bll = new TollGate();
            QueryParam searchTerm = bll.GenerateSearchTerm(item);
            TabContainer1.ActiveTabIndex = 0;
            ViewState["SearchTerm"] = searchTerm;
            FillData();
        }
        catch(Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询收费站失败", ex , Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void Process()
    {
        if (cmd == "export")
        {
            //导出
            string file = Server.MapPath("~/public/2.xls");
            FileStream stream = File.Open(file, FileMode.Open);

            byte[] Buffer = null;
            long size;
            size = stream.Length;
            Buffer = new byte[size];
            stream.Read(Buffer, 0, int.Parse(stream.Length.ToString()));
            stream.Close();
            stream = null;

            HttpContext.Current.Response.ContentType = "application/xls";
            string header = "attachment; filename=" + file;
            HttpContext.Current.Response.AddHeader("content-disposition", header);
            HttpContext.Current.Response.BinaryWrite(Buffer);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
          
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = "";
        TabContainer1.ActiveTabIndex = 1;
        //TabOptionWebControls1.SelectIndex = 1;
    }
}
