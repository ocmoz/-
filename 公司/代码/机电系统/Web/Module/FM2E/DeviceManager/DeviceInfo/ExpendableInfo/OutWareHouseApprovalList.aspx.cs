using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Maintain;
using FM2E.BLL.Maintain;
using System.Collections;
using FM2E.BLL.Basic;
using FM2E.Model.System;


using FM2E.Model.Basic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_OutWarehouseApprovalList : System.Web.UI.Page
{
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    /// <summary>
    /// 查询条件
    /// </summary>
    private MalfunctionSearchInfo SearchTerm
    {
        get
        {
            LoginUserInfo Manager = UserData.CurrentUserData;
            if (ViewState["SearchTerm"] == null)
            {
                MalfunctionSearchInfo term = new MalfunctionSearchInfo();
                //if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
                //{
                //    //如果没有查看全部故障处理单的权限
                //    term.RecordDept = UserData.CurrentUserData.DepartmentID;
                //}
                //if (Manager.PositionName == "高级经理")
                //    term.IsDelayApply = 1;
                term.Status = (int)MalfunctionHandleStatus.Waiting4MoneyApproval;
                
                return term;
            }
            else return (MalfunctionSearchInfo)ViewState["SearchTerm"];
        }
        set
        {
            ViewState["SearchTerm"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
        }
    }
    /// <summary>
    /// 初始化页面数据
    /// </summary>
    private void InitialPage()
    {
        try
        {
            ViewState["isSearch"] = false;
            Company companybll = new Company();
            IList<CompanyInfo> companylist = companybll.GetAllCompany();

            Warehouse bll = new Warehouse();
            IList<WarehouseInfo> list = bll.GetAllWarehouse();
            DropDownList_Warehouse.Items.Clear();
            DropDownList_Warehouse.Items.Add(new ListItem("请选择出库部门", ""));
            foreach (WarehouseInfo item in list)
            {
                DropDownList_Warehouse.Items.Add(new ListItem(item.Name, item.WareHouseID));
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 权限控制
    /// </summary>
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Approval))
        {
            //有删除权限
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        }
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            bool search = Convert.ToBoolean(ViewState["isSearch"]);
            int listCount = 0;

            OutWarehouseInfo info = new OutWarehouseInfo();
            info.WarehouseID = DropDownList_Warehouse.SelectedValue;
            info.SheetName = TextBox_SheetName.Text.Trim();
            DateTime applytimelower = DateTime.MinValue;
            DateTime.TryParse(TextBox_ApplyTimeLower.Text.Trim(), out applytimelower);
            info.ApplyTimeLower = applytimelower;

            DateTime applytimeupper = DateTime.MinValue;
            DateTime.TryParse(TextBox_ApplyTimeUpper.Text.Trim(), out applytimeupper);
            info.ApplyTimeUpper = applytimeupper;
            int recordCount = 0;
            Expendable bll = new Expendable();

            IList list = bll.SearchOutWarehouseApply(info, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, WebUtility.Common.Get_UserName, out listCount);
            

           GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
            TabContainer1.ActiveTabIndex = 0;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    //定时刷新页面
    protected void Timer_Refresh_Tick(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
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
    /// <summary>
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);

        if (e.CommandName == "view")
        {
            //查看
            Response.Redirect("OutWarehouseApprove.aspx?cmd=view&id=" + id);  
        }
    }
    /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            OutWarehouseInfo item = (OutWarehouseInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ID.ToString();
        }
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        GetSearchTerm();
        FillData();
    }
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private void GetSearchTerm()
    {
        //MalfunctionSearchInfo term = new MalfunctionSearchInfo();

        //if (!string.IsNullOrEmpty(tbSheetNO.Text.Trim()))
        //    term.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());

        //if (ddlDepartment.SelectedValue != "0" && ddlDepartment.SelectedValue != "")
        //    term.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);

        //if (!string.IsNullOrEmpty(tbReporter.Text.Trim()))
        //    term.Reporter = Common.inSQL(tbReporter.Text.Trim());

        //if (!string.IsNullOrEmpty(tbReportTimeFrom.Text.Trim()))
        //    term.ReportDateFrom = Convert.ToDateTime(tbReportTimeFrom.Text.Trim());

        //if (!string.IsNullOrEmpty(tbReportTimeTo.Text.Trim()))
        //    term.ReportDateTo = Convert.ToDateTime(tbReportTimeTo.Text.Trim());

        //if (!string.IsNullOrEmpty(tbRecorderName.Text.Trim()))
        //    term.RecorderName = Common.inSQL(tbRecorderName.Text.Trim());

        //if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
        //{
        //    //如果没有查看全部故障处理单的权限
        //    term.DepartmentID = UserData.CurrentUserData.DepartmentID;
        //}

        ////term.Status = (int)MalfunctionHandleStatus.Fixed;
        //term.Status = (int)MalfunctionHandleStatus.Waiting4MoneyApproval;

        //if (ddlMaintainTeam.SelectedValue != "0")
        //    term.MaintainDept = Convert.ToUInt32(ddlMaintainTeam.SelectedValue);

        //SearchTerm = term;
    }
    /// <summary>
    /// 列表项筛选
    /// </summary>
    private void Filter()
    {
        //MalfunctionSearchInfo term = new MalfunctionSearchInfo();

        //term.DepartmentID = Convert.ToInt64(ddlFilterDepartment.SelectedValue);
        ////term.RecorderName = Common.inSQL(tbFilterRecorder.Text.Trim());
        //term.SystemID = ddlSystem.SelectedValue;
        //term.MaintainDept = Convert.ToInt64(ddlFilterMaintainTeam.SelectedValue);
        //term.MalfunctionRank = Convert.ToInt32(ddlFilterRank.SelectedValue);
        //if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
        //{
        //    //如果没有查看全部故障处理单的权限
        //    term.RecordDept = UserData.CurrentUserData.DepartmentID;
        //}

        ////term.Status = (int)MalfunctionHandleStatus.Fixed;
        //term.Status = (int)MalfunctionHandleStatus.Waiting4MoneyApproval;//By L 5-3修正

        //SearchTerm = term;
    }
    /// <summary>
    /// 列表条件筛选
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btFilter_Click(object sender, EventArgs e)
    {
        Filter();
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }
}
