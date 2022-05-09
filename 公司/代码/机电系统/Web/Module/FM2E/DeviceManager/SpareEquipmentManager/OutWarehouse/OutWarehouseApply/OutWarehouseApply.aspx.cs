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
using FM2E.BLL.System;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApply_OutWarehouseApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckPermission(PopedomType.New);
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
            Company companybll = new Company();
            IList<CompanyInfo> companylist = companybll.GetAllCompany();

            //DropDownList1.Items.Clear();
            //DropDownList1.Items.Add(new ListItem("请选择公司", ""));
            //foreach (CompanyInfo item in companylist)
            //{
            //    DropDownList1.Items.Add(new ListItem(item.CompanyName, item.CompanyID.ToString()));
            //}
            Warehouse bll = new Warehouse();
            IList<WarehouseInfo> list = bll.GetAllWarehouse();
            DropDownList_Warehouse.Items.Clear();
            DropDownList_Warehouse.Items.Add(new ListItem("请选择仓库", ""));
            foreach (WarehouseInfo item in list)
            {
                DropDownList_Warehouse.Items.Add(new ListItem(item.Name, item.WareHouseID));
            }
            //DropDownList2.Items.Clear();
            //DropDownList2.Items.Add(new ListItem("请选择状态", "0"));
            //DropDownList2.Items.Add(new ListItem("草稿", "1"));
            //DropDownList2.Items.Add(new ListItem("等待审批", "2"));
            //DropDownList2.Items.Add(new ListItem("等待出库", "3"));
            //DropDownList2.Items.Add(new ListItem("已出库", "4"));
            //DropDownList2.Items.Add(new ListItem("已拒绝", "5"));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        bool search = Convert.ToBoolean(ViewState["isSearch"]);
        OutWarehouse bll = new OutWarehouse();
        int listCount = 0;
        //ArrayList list = new ArrayList();
        //QueryParam searchTerm = new QueryParam();
        //if (!search)
        //{
        //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
        //    item.SheetName = TextBox_SheetName.Text.Trim();
        //    item.Applicant = Common.Get_UserName;
        //    item.ApplicantName = "";
        //    item.ApprovalerName = "";
        //    item.CompanyID = UserData.CurrentUserData.CompanyID;
        //    item.Status = 0;
        //    item.WarehouseID = "";
        //    searchTerm = bll.GenerateSearchTerm(item);
        //}
        //else
        //{
        //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
        //    item.SheetName = Common.inSQL(TextBox1.Text.Trim());
        //    item.Applicant = Common.Get_UserName;
        //    item.ApplicantName = "";
        //    item.CompanyID = UserData.CurrentUserData.CompanyID;
        //    item.Status = (OutWarehouseApplyStatus)Convert.ToInt32(DropDownList2.SelectedValue);
        //    if (DropDownList4.SelectedValue != "")
        //        item.WarehouseID = Convert.ToString(DropDownList4.SelectedValue);
        //    else
        //        item.WarehouseID = "";
        //    item.ApprovalerName = "";
        //    searchTerm = bll.GenerateSearchTerm(item);
        //}
        //searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
        //int thiscount = 0;
        //IList l = bll.GetList(searchTerm, out thiscount);
        //list.AddRange(l);
        //listCount += thiscount;
        OutWarehouseApplySearchInfo info = new OutWarehouseApplySearchInfo();
        info.Applicant = Common.Get_UserName;
        info.WarehouseID = DropDownList_Warehouse.SelectedValue;
        info.SheetName = TextBox_SheetName.Text.Trim();
        DateTime applytimelower = DateTime.MinValue;
        DateTime.TryParse(TextBox_ApplyTimeLower.Text.Trim(), out applytimelower);
        info.ApplyTimeLower = applytimelower;

        DateTime applytimeupper = DateTime.MinValue;
        DateTime.TryParse(TextBox_ApplyTimeUpper.Text.Trim(), out applytimeupper);
        info.ApplyTimeUpper = applytimeupper;

        DateTime outtimelower = DateTime.MinValue;
        DateTime.TryParse(TextBox_OutTimeLower.Text.Trim(), out outtimelower);
        info.OutTimeLower = outtimelower;

        DateTime outtimeupper = DateTime.MinValue;
        DateTime.TryParse(TextBox_OutTimeUpper.Text.Trim(), out outtimeupper);
        info.OutTimeUpper = outtimeupper;

        IList list = bll.SearchOutWarehouseApply(info, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out listCount);

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
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewOutWarehouseApply.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                OutWarehouse bll = new OutWarehouse();
                bll.DeleteApplyInfo(id);
                FillData();
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
            TabContainer1.ActiveTabIndex = 0;
            ViewState["isSearch"] = true;
            FillData();
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
