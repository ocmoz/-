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

public partial class Module_FM2E_BasicData_PositionManage_Position : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

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
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }

    private void FillData()
    {
        try
        {
            Position position = new Position();
            int recordCount = 0;
            IList list = position.GetPositionByPage(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);

            AspNetPager1.RecordCount = recordCount;

            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string id = gvRow.Attributes["PositionID"];
            Response.Redirect("ViewPosition.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                long id = Convert.ToInt64(gvRow.Attributes["PositionID"]);
                Position bll = new Position();
                bll.DelPosition(id);

                FillData();
               // GridView1.Rows[row].Visible = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
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

            PositionInfo item = (PositionInfo)e.Row.DataItem;
            e.Row.Attributes["PositionID"] = item.PositionID.ToString();
        }    

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
}
