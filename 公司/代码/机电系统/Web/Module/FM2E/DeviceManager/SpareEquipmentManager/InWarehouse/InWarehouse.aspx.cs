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
using FM2E.Model.Exceptions;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_InWarehouse_InWarehouse : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    private readonly Warehouse bllwh = new Warehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //判断是否仓管员
            
            WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == string.Empty)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            Hidden_WarehouseAddressID.Value = warehouse.AddressID.ToString();
            Hidden_WarehouseAddressName.Value = warehouse.AddressName;
            Hidden_WarehouseAddressCode.Value = warehouse.AddressCode;
            Hidden_WarehouseID.Value = warehouse.WareHouseID;
            Hidden_WarehouseName.Value = warehouse.Name;

            InitialPage();
            FillData();
            //Process();

            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        bool bnew = SystemPermission.CheckPermission(PopedomType.New);
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = bnew;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = bnew;
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************


    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            //Warehouse bll = new Warehouse();
            //IList<WarehouseInfo> list = bll.GetAllWarehouse();
            //DDLWarehouse.Items.Clear();
            //DDLWarehouse.Items.Add(new ListItem("请选择仓库", ""));
            //foreach(WarehouseInfo item in list)
            //{
            //    DDLWarehouse.Items.Add(new ListItem(item.Name, item.WareHouseID));
            //}
            Department deptBll = new Department();
            DropDownList_Department.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank(UserData.CurrentUserData.CompanyID));
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
        InWarehouse bll = new InWarehouse();
        int listCount = 0;
        QueryParam searchTerm = (QueryParam)ViewState["SearchTerm"];
        if (searchTerm == null)
        {
            
            InWarehouseInfo item = new InWarehouseInfo();
            item.WarehouseID = Hidden_WarehouseID.Value;
            item.SheetName = TextBox_SheetID.Text.Trim();

            item.ApplicantName = TextBox_ApplicantName.Text.Trim();
            item.OperatorName = TextBox_WarehouseKeeperName.Text.Trim();

            DateTime lower = DateTime.MinValue, upper = DateTime.MinValue;
            DateTime.TryParse(TextBox_TimeLower.Text.Trim(), out lower);
            DateTime.TryParse(TextBox_TimeUpper.Text.Trim(), out upper);

            long deparmentid = 0;
            long.TryParse(DropDownList_Department.SelectedValue, out deparmentid);

            item.DepartmentID = deparmentid;

            item.TimeLower = lower;
            item.TimeUpper = upper;
            searchTerm = bll.GenerateSearchTerm(item);
        }
        searchTerm.PageSize = AspNetPager1.PageSize;
        searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
        IList list = bll.GetList(searchTerm, out listCount);
        AspNetPager1.RecordCount = listCount;

        GridView1.DataSource = list;
        GridView1.DataBind();
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
        bool success = false;
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewInWarehouse.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                InWarehouse bll = new InWarehouse();
                bll.DelInWarehouse(id);
                FillData();
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (success == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("InWarehouse.aspx"), UrlType.Href, "");
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

            InWarehouseInfo item = (InWarehouseInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ID.ToString();
        }

    }
    /// <summary>
    /// 查询操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            InWarehouseInfo item = new InWarehouseInfo();
            //item.CompanyName = Common.inSQL(TbCompany.Text.Trim());
            //item.DepartmentName = Common.inSQL(TbDepartment.Text.Trim());
            //if (DDLWarehouse.SelectedValue != "")
            //    item.WarehouseID = DDLWarehouse.SelectedValue;
            //else
            //    item.WarehouseID = "";
            item.WarehouseID = Hidden_WarehouseID.Value;
            item.SheetName = TextBox_SheetID.Text.Trim();
            
            item.ApplicantName = TextBox_ApplicantName.Text.Trim();
            item.OperatorName = TextBox_WarehouseKeeperName.Text.Trim();

            DateTime lower = DateTime.MinValue, upper = DateTime.MinValue;
            DateTime.TryParse(TextBox_TimeLower.Text.Trim(), out lower);
            DateTime.TryParse(TextBox_TimeUpper.Text.Trim(), out upper);

            long deparmentid = 0;
            long.TryParse(DropDownList_Department.SelectedValue,out deparmentid);

            item.DepartmentID = deparmentid;

            item.TimeLower = lower;
            item.TimeUpper = upper;

            InWarehouse bll = new InWarehouse();
            QueryParam searchTerm = bll.GenerateSearchTerm(item);
            AspNetPager1.CurrentPageIndex = 1;
            
            TabContainer1.ActiveTabIndex = 0;
            ViewState["SearchTerm"] = searchTerm;
            FillData();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询申请失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    //private void Process()
    //{
    //    if (cmd == "export")
    //    {
    //        //导出
    //        string file = Server.MapPath("~/public/2.xls");
    //        FileStream stream = File.Open(file, FileMode.Open);

    //        byte[] Buffer = null;
    //        long size;
    //        size = stream.Length;
    //        Buffer = new byte[size];
    //        stream.Read(Buffer, 0, int.Parse(stream.Length.ToString()));
    //        stream.Close();
    //        stream = null;

    //        HttpContext.Current.Response.ContentType = "application/xls";
    //        string header = "attachment; filename=" + file;
    //        HttpContext.Current.Response.AddHeader("content-disposition", header);
    //        HttpContext.Current.Response.BinaryWrite(Buffer);
    //        HttpContext.Current.Response.End();
    //        HttpContext.Current.Response.Flush();

    //    }
    //}
}
