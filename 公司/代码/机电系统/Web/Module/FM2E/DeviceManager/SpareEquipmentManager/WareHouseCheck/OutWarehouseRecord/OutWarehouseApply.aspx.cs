﻿using System;
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
using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseRecord_OutWarehouseApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    readonly OutWarehouse bll = new OutWarehouse();
    readonly Warehouse bllwh = new Warehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == string.Empty)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            CurrentWarehouse = warehouse; Label_WarehouseName.Text = warehouse.Name;
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
        GridView1.Columns[GridView1.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 当前的仓库信息
    /// </summary>
    private WarehouseInfo CurrentWarehouse
    {
        get
        {
            WarehouseInfo warehouse = (WarehouseInfo)ViewState["CurrentWarehouse"];
            if (warehouse == null)
            {
                warehouse = new Warehouse().GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            }
            return warehouse;
        }
        set
        {
            ViewState["CurrentWarehouse"] = value;
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            //Company companybll = new Company();
            //IList<CompanyInfo> companylist = companybll.GetAllCompany();

            //DropDownList1.Items.Clear();
            //DropDownList1.Items.Add(new ListItem("请选择公司", ""));
            //foreach (CompanyInfo item in companylist)
            //{
            //    DropDownList1.Items.Add(new ListItem(item.CompanyName, item.CompanyID.ToString()));
            //}
            //Warehouse bll = new Warehouse();
            //IList<WarehouseInfo> list = bll.GetAllWarehouse();
            //DropDownList4.Items.Clear();
            //DropDownList4.Items.Add(new ListItem("请选择仓库", ""));
            //foreach(WarehouseInfo item in list)
            //{
            //    DropDownList4.Items.Add(new ListItem(item.Name, item.WareHouseID));
            //}
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView1等数据
    /// </summary>
    private void FillData()
    {
        
        int listCount = 0;
        //QueryParam searchTerm = (QueryParam)ViewState["SearchTerm"];
        //if (searchTerm == null)
        //{
        //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
        //    item.SheetName = "";
        //    item.Applicant = "";
        //    item.ApplicantName = "";
        //    item.ApprovalerName = "";
        //    item.CompanyID = "";
        //    item.Status = OutWarehouseApplyStatus.ApprovalPassed;
        //    item.WarehouseID = "";
        //    searchTerm = bll.GenerateSearchTerm(item);
        //}
        //searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
        //IList list = bll.GetList(searchTerm, out listCount);
        OutWarehouseApplySearchInfo info = new OutWarehouseApplySearchInfo();
        info.WarehouseID = CurrentWarehouse.WareHouseID;
        info.WorkFlowStatusList.Add(OutWarehouseWorkflow.Wait4OutWarehouseState);
        //info.WorkFlowStatusList.Add(OutWarehouseWorkflow.OutWarehouseFinishState);

        IList list = bll.SearchOutWarehouseApply(info, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out listCount);
        AspNetPager1.RecordCount = listCount;

        GridView1.DataSource = list;
        GridView1.DataBind();
    }
    /// <summary>
    /// 初始化GridView2等数据
    /// </summary>
    private void FillData2()
    {
        //OutWarehouseApply bll = new OutWarehouseApply();
        int listCount = 0;
        //QueryParam searchTerm = (QueryParam)ViewState["SearchTerm2"];
        //if (searchTerm == null)
        //{
        //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
        //    item.SheetName = "";
        //    item.Applicant = "";
        //    item.ApplicantName = "";
        //    item.ApprovalerName = "";
        //    item.CompanyID = "";
        //    item.Status = OutWarehouseApplyStatus.Received;
        //    item.WarehouseID = "";
        //    searchTerm = bll.GenerateSearchTerm(item);
        //}
        //searchTerm.PageIndex = AspNetPager2.CurrentPageIndex;
        //IList list = bll.GetList(searchTerm, out listCount);

        OutWarehouseApplySearchInfo info = new OutWarehouseApplySearchInfo();
        info.WarehouseID = CurrentWarehouse.WareHouseID;
        info.WorkFlowStatusList.Add(OutWarehouseWorkflow.OutWarehouseFinishState);

        info.SheetName = TextBox_SheetName.Text.Trim();
        info.Applicant = TextBox_Applicant.Text.Trim();
        info.ApplicantName = TextBox_ApplicantName.Text.Trim();
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
        DateTime.TryParse(TextBox_OutTimeLower.Text.Trim(), out outtimeupper);
        info.OutTimeUpper = outtimeupper;

       IList list = bll.SearchOutWarehouseApply(info, AspNetPager2.CurrentPageIndex, AspNetPager2.PageSize, out listCount);


        AspNetPager2.RecordCount = listCount;

        GridView2.DataSource = list;
        GridView2.DataBind();
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
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData2();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long id = Convert.ToInt64(e.CommandArgument.ToString());
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewOutWarehouseApply.aspx?cmd=view&id=" + id);
        }
        if (e.CommandName == "outEquipments")
        {
            Response.Redirect("OutWarehouseRecord.aspx?cmd=outEquipments&id=" + id);
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
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long id = Convert.ToInt64(e.CommandArgument.ToString());
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewOutWarehouseRecord.aspx?cmd=view&id=" + id);
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
            AspNetPager2.CurrentPageIndex = 1;
            FillData2();
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
