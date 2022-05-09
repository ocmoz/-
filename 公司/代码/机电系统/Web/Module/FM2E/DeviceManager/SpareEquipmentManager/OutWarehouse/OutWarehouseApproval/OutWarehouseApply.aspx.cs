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
using FM2E.BLL.System;
using FM2E.Model.Equipment;
using FM2E.WorkflowLayer;
using System.Text;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApproval_OutWarehouseApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    readonly OutWarehouse bll = new OutWarehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            FillData2();
            //Process();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        GridView1.Columns[GridView1.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Approval);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            ViewState["isSearch"] = false;
            Warehouse bll = new Warehouse();
            IList<WarehouseInfo> list = bll.GetAllWarehouse();
            DropDownList_Warehouse.Items.Clear();
            DropDownList_Warehouse.Items.Add(new ListItem("请选择仓库", ""));
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
    /// 初始化GridView1等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            bool search = Convert.ToBoolean(ViewState["isSearch"]);
           
            int listCount = 0;
            //List<long> list = new List<long>();
            //List<string> states = WorkflowHelper.GetCorrelativeStateNameList(OutWarehouseWorkflow.WorkflowName, Common.Get_UserName);

            //if (states.Count == 0)//该用户没有任何权限
            //    return;

            //QueryParam searchTerm = new QueryParam();
            //if (!search)
            //{
            //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
            //    item.SheetName = "";
            //    item.Applicant = "";
            //    item.ApplicantName = "";
            //    item.ApprovalerName = "";
            //    item.CompanyID = UserData.CurrentUserData.CompanyID;
            //    item.Status = OutWarehouseApplyStatus.Waiting4ApprovalResult;
            //    item.WarehouseID = "";
            //    searchTerm = bll.GenerateSearchTerm(item, states.ToArray());
            //}
            //else
            //{
            //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
            //    item.SheetName = Common.inSQL(TextBox1.Text.Trim());
            //    item.Applicant = "";
            //    item.ApplicantName = Common.inSQL(TextBox5.Text.Trim());
            //    item.ApprovalerName = "";
            //    item.CompanyID = UserData.CurrentUserData.CompanyID;
            //    item.Status = OutWarehouseApplyStatus.Waiting4ApprovalResult;
            //    item.WarehouseID = DropDownList4.SelectedValue;
            //    searchTerm = bll.GenerateSearchTerm(item, states.ToArray());
            //}
            //searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            //int thiscount = 0;
            //IList l = bll.GetList(searchTerm, out thiscount);
            OutWarehouseApplySearchInfo info = new OutWarehouseApplySearchInfo();
            info.NextUserName = Common.Get_UserName;
            IList list = bll.SearchOutWarehouseApply(info, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView2等数据
    /// </summary>
    private void FillData2()
    {
        try
        {
            bool search = Convert.ToBoolean(ViewState["isSearch"]);

            int listCount = 0;
            //List<long> list = new List<long>();
            //List<string> states = WorkflowHelper.GetCorrelativeStateNameList(OutWarehouseWorkflow.WorkflowName, Common.Get_UserName);

            //if (states.Count == 0)//该用户没有任何权限
            //    return;

            //QueryParam searchTerm = new QueryParam();
            //if (!search)
            //{
            //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
            //    item.SheetName = "";
            //    item.Applicant = "";
            //    item.ApplicantName = "";
            //    item.ApprovalerName = UserData.CurrentUserData.PersonName;
            //    item.CompanyID = UserData.CurrentUserData.CompanyID;
            //    item.Status = 0;
            //    item.WarehouseID = "";
            //    searchTerm = bll.GenerateSearchTerm(item);
            //}
            //else
            //{
            //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
            //    item.SheetName = Common.inSQL(TextBox1.Text.Trim());
            //    item.Applicant = "";
            //    item.ApplicantName = Common.inSQL(TextBox5.Text.Trim());
            //    item.ApprovalerName = UserData.CurrentUserData.PersonName;
            //    item.CompanyID = UserData.CurrentUserData.CompanyID;
            //    item.Status = 0;
            //    item.WarehouseID = DropDownList4.SelectedValue;
            //    searchTerm = bll.GenerateSearchTerm(item);
            //}
            //searchTerm.PageIndex = AspNetPager2.CurrentPageIndex;
            //int thiscount = 0;
            //IList l = bll.GetList(searchTerm, out thiscount);
            OutWarehouseApplySearchForApprovalerInfo info = new OutWarehouseApplySearchForApprovalerInfo();
            info.Approvaler = Common.Get_UserName;
            info.SheetName = TextBox_SheetName.Text.Trim();
            info.WarehouseID = DropDownList_Warehouse.SelectedValue;
            info.Applicant = TextBox_Applicant.Text.Trim();
            info.ApplicantName = TextBox_ApplicantName.Text.Trim();
            DateTime applytimelower = DateTime.MinValue;
            DateTime.TryParse(TextBox_ApplyTimeLower.Text.Trim(), out applytimelower);
            info.ApplyTimeLower = applytimelower;

            DateTime applytimeupper = DateTime.MinValue;
            DateTime.TryParse(TextBox_ApplyTimeUpper.Text.Trim(), out applytimeupper);
            info.ApplyTimeUpper = applytimeupper;

            DateTime approvaltimelower = DateTime.MinValue;
            DateTime.TryParse(TextBox_ApprovalTimeLower.Text.Trim(), out approvaltimelower);
            info.ApprovalTimeLower = approvaltimelower;

            DateTime approvaltimeupper = DateTime.MinValue;
            DateTime.TryParse(TextBox_ApprovalTimeUpper.Text.Trim(), out approvaltimeupper);
            info.ApprovalTimeUpper = approvaltimeupper;

            IList list = bll.SearchOutWarehouseApply(info, AspNetPager2.CurrentPageIndex, AspNetPager2.PageSize, out listCount);
            AspNetPager2.RecordCount = listCount;
            GridView2.DataSource = list;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
        TabContainer1.ActiveTabIndex = 0;
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData2();
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long id = Convert.ToInt64(e.CommandArgument.ToString());
        if (e.CommandName == "approval")
        {
            Response.Redirect("OutWarehouseApproval.aspx?cmd=approval&id=" + id);
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        long id = Convert.ToInt64(e.CommandArgument.ToString());
        if (e.CommandName == "view")
        {
            Response.Redirect("../OutWarehouseApply/ViewOutWarehouseApply.aspx?cmd=viewArchives&id=" + id);
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

            OutWarehouseApplyInfo item = (OutWarehouseApplyInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ID.ToString();
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            OutWarehouseApplyInfo item = (OutWarehouseApplyInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ID.ToString();
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
            TabContainer1.ActiveTabIndex = 1;
            ViewState["isSearch"] = true;
            AspNetPager2.CurrentPageIndex = 1;
            //FillData();
            FillData2();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
