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
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_WarehouseInventory_WarehouseInventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            LblError.Text = "";
            Warehouse bll = new Warehouse();
            IList<WarehouseInfo> list = bll.GetAllWarehouse();
            DDLWarehouse.Items.Clear();
            DDLWarehouse.Items.Add(new ListItem("请选择仓库", ""));
            foreach(WarehouseInfo item in list)
            {
                DDLWarehouse.Items.Add(new ListItem(item.Name, item.WareHouseID));
            }
            if (UserData.CurrentUserData.CompanyID != null && UserData.CurrentUserData.CompanyID != string.Empty)
            {
                CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
            }
            DateTime dt1 = DateTime.Now.Date;
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, 1);
            TBMinTime.Text = dt2.ToString("yyyy-MM-dd");
            TBMaxTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        ExpendableInOut bll = new ExpendableInOut();

        int listCount = 0;

        ExpendableInOutRecordSearchInfo searchTerm = CurrentExpendableSearchInfo;
        if (checktypeDDL.SelectedValue.Equals("1"))
        {
            IList list = bll.SearchRecord(searchTerm, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        else 
        {
            IList list = bll.SearchRecordOut(searchTerm, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }      
    }

    private void FillData2()
    {
        InEquipments bll = new InEquipments();

        int listCount = 0;

        ExpendableInOutRecordSearchInfo searchTerm = CurrentExpendableSearchInfo;

        InEquipmentsInfo initem = new InEquipmentsInfo();

        initem.WarehouseID = searchTerm.WarehouseID;
        initem.InOutTimeLower = searchTerm.InOutTimeLower;
        initem.InOutTimeUpper = searchTerm.InOutTimeUpper;

        IList list = bll.SearchRecord(initem, 1, Int32.MaxValue, out listCount);
        GridView2.DataSource = list;
        GridView2.DataBind();
    }
    private void FillData3()
    {
        InEquipments bll = new InEquipments();

        int listCount = 0;

        ExpendableInOutRecordSearchInfo searchTerm = CurrentExpendableSearchInfo;

        InEquipmentsInfo initem = new InEquipmentsInfo();

        initem.WarehouseID = searchTerm.WarehouseID;
        initem.InOutTimeLower = searchTerm.InOutTimeLower;
        initem.InOutTimeUpper = searchTerm.InOutTimeUpper;

        IList list = bll.SearchRecordForOut(initem, 1, Int32.MaxValue, out listCount);
        GridView3.DataSource = list;
        GridView3.DataBind();
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewOutWarehouseApply.aspx?cmd=view&id=" + id);
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

            //OutWarehouseApplyInfo item = (OutWarehouseApplyInfo)e.Row.DataItem;
            //e.Row.Attributes["ID"] = item.ID.ToString();
        }

    }
    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (checktypeDDL.SelectedValue.Equals("1") || checktypeDDL.SelectedValue.Equals("4"))
            {
                AspNetPager1.CurrentPageIndex = 1;
                TabContainer1.ActiveTabIndex = 1;
                FillData();
            }
            else if (checktypeDDL.SelectedValue.Equals("2"))
            {
                TabContainer1.ActiveTabIndex = 2;
                FillData2();
            }
            else if (checktypeDDL.SelectedValue.Equals("3"))
            {
                TabContainer1.ActiveTabIndex = 3;
                FillData3();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询申请失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private ExpendableInOutRecordSearchInfo CurrentExpendableSearchInfo
    {
        get
        {
            ExpendableInOutRecordSearchInfo info = (ExpendableInOutRecordSearchInfo)ViewState["SearchTerm"];
            if (info == null)
            {
                info = new ExpendableInOutRecordSearchInfo();
                info.WarehouseID = DDLWarehouse.SelectedValue;
                
               
                DateTime lower = DateTime.MinValue;
                DateTime.TryParse(TBMinTime.Text.Trim(), out lower);
                info.InOutTimeLower = lower;

                DateTime upper = DateTime.MinValue;
                DateTime.TryParse(TBMaxTime.Text.Trim(), out upper);
                info.InOutTimeUpper = upper;
            }

            return info;
        }
        set { ViewState["SearchTerm"] = value; }
    }

}
