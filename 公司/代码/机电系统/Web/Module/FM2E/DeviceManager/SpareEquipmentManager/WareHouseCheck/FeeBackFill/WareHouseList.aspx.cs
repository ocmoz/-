using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Equipment;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using System.Collections;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_FeeBackFill_WareHouseList : System.Web.UI.Page
{
    private readonly WareHouseCheck wareHouseCheck = new WareHouseCheck();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
        }
    }
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Edit))
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        }
        else
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        }
    }
    private void InitialPage()
    {
        try
        {
            Warehouse bll = new Warehouse();
            IList<FM2E.Model.Basic.WarehouseInfo> list = bll.GetAllWarehouse();
            ddlWareHouse.Items.Clear();
            ddlWareHouse.Items.Add(new ListItem("不限", "0"));
            foreach (WarehouseInfo item in list)
            {
                ddlWareHouse.Items.Add(new ListItem(item.Name, item.WareHouseID));
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            //获取查询条件
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                WareHouseCheckSearchInfo searchTerm = new WareHouseCheckSearchInfo();
                searchTerm.CompanyID = UserData.CurrentUserData.CompanyID;
                searchTerm.Status = WareHouseFormStatus.Committed;
                qp = wareHouseCheck.GenerateSearchTerm(searchTerm);
            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;

            //查询
            int recordCount = 0;
            IList list = wareHouseCheck.GetWareHouseCheckList(qp, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string formNO = Convert.ToString(gvRow.Attributes["FormNO"]);

        if (e.CommandName == "edit")
        {
            //查看
            Response.Redirect("SignForm.aspx?cmd=edit&id=" + formNO);
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

            WareHouseCheckInfo item = (WareHouseCheckInfo)e.Row.DataItem;
            e.Row.Attributes["FormNO"] = item.FormNO;
        }
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        WareHouseCheckSearchInfo term = new WareHouseCheckSearchInfo();
        try
        {
            if (tbFormNO.Text.Trim() != string.Empty)
                term.FormNO = Common.inSQL(tbFormNO.Text.Trim());

            if (ddlWareHouse.SelectedValue != "0")
                term.WareHouseID = ddlWareHouse.SelectedValue;

            term.Status = WareHouseFormStatus.Committed;

            if (tbCheckDateFrom.Text.Trim() != string.Empty)
                term.CheckDateFrom = Convert.ToDateTime(tbCheckDateFrom.Text.Trim());

            if (tbCheckDateTo.Text.Trim() != string.Empty)
                term.CheckDateTo = Convert.ToDateTime(tbCheckDateTo.Text.Trim());

            term.CompanyID = UserData.CurrentUserData.CompanyID;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        try
        {
            QueryParam qp = wareHouseCheck.GenerateSearchTerm(term);
            ViewState["SearchTerm"] = qp;
            FillData();
            TabContainer1.ActiveTabIndex = 0;
            AspNetPager1.CurrentPageIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询出错", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
