using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Equipment;
using FM2E.Model.Utils;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckHistory : System.Web.UI.Page
{ 
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();

    /// <summary>
    /// 员工业务逻辑处理类对象
    /// </summary>
    Warehouse Warehousebll = new Warehouse();

    CheckAcceptance checkacceptanceBll = new CheckAcceptance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
        }
    }
    string WarehouseName = "";
    string WarehouseId = "";
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        IList<WarehouseInfo> Warehouselist = Warehousebll.GetWarehouseListByUserName(UserData.CurrentUserData.UserName);
        if (Warehouselist.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        foreach (WarehouseInfo item in Warehouselist)
        {
            WarehouseName += item.Name + " ";
            if (WarehouseId == "")
                WarehouseId = "'" + item.WareHouseID + "'";
            else
                WarehouseId += ",'" + item.WareHouseID + "'";

        }
        Label_WarehouseName.Text = WarehouseName;
        Label_WarehouseName2.Text = WarehouseName;

        FillData();
        FillData2();
    }

    /// <summary>
    /// 换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        //if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        //{
        //    ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        //}
        FillData();
        TabContainer1.ActiveTabIndex = 0;
    }

    /// <summary>
    /// 换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        //if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        //{
        //    ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        //}
        FillData2();
        TabContainer1.ActiveTabIndex = 1;
    }

    /// <summary>
    /// 填充列表
    /// </summary>
    private void FillData()
    {
        try
        {
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int listCount = 0;

            //PurchaseOrderCheckSearchInfo info = new PurchaseOrderCheckSearchInfo();
            //info.CompanyID = UserData.CurrentUserData.CompanyID;
            //info.DetailStatusList.Add(PurchaseOrderDetailStatus.CHECKFINISH);
            //info.WareHouseID = Hidden_WarehouseID.Value;
            //info.PurchaseRecordStatusList.Add(PurchaseRecordStatus.INWAREHOUSEFINISH);
            //info.PurchaseRecordStatusList.Add(PurchaseRecordStatus.WAIT4INWAREHOUSE);

            //IList list = purchaseBll.SearchPurchaseOrder(info, pageIndex, AspNetPager1.PageSize, out listCount);

            QueryParam p = new QueryParam();
            p.PageIndex = AspNetPager1.CurrentPageIndex; ;
            p.PageSize = AspNetPager1.PageSize;
            p.TableName = "FM2E_PurchasePlanView s1 left join FM2E_PurchasePlanDetailView s2 on s1.ID=s2.ID  left join FM2E_PurchaseRecordView s3 on s2.ID = s3.OrderID and s2.ItemID = s3.PlanDetailItemID ";

            string sqlSearch = "where 1=1";
            if (WarehouseName != "")
            {
                sqlSearch += " and s3.[WarehouseID] in(" + WarehouseId + ")";
            }
            sqlSearch += " and s2.Status=" + (int)PurchaseOrderDetailStatus.CHECKFINISH;
            sqlSearch += " and s3.Status in(" + (int)PurchaseRecordStatus.WAIT4INWAREHOUSE + ","  +(int)PurchaseRecordStatus.WAIT4INWAREHOUSE + ")";
            p.Where = sqlSearch;
            p.ReturnFields = "s1.*";
            p.OrderBy = "order by UpdateTime desc";
            IList list = purchaseBll.SearchPurchaseOrder(p, out listCount);


            AspNetPager1.RecordCount = listCount;
            //排序，先把等待中的排在前面，然后是正在采购的，然后按照Submit顺序
            gridview_PurchaseApplyList.DataSource = list;
            gridview_PurchaseApplyList.DataBind();

            TabContainer1.Tabs[0].HeaderText = "申购单(" + listCount + ")";
            if (listCount == 0)
                TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

       
    }

    private void FillData2()
    {
        try
        {
            int pageIndex = AspNetPager2.CurrentPageIndex;
            int listCount = 0;

            //CheckAcceptanceSearchInfo info = new CheckAcceptanceSearchInfo();
            //info.CompanyID = UserData.CurrentUserData.CompanyID;
            //info.WareHouseID = Hidden_WarehouseID.Value;
            //info.DetailStatusList.Add(PurchaseRecordStatus.WAIT4INWAREHOUSE);
            //info.DetailStatusList.Add(PurchaseRecordStatus.INWAREHOUSEFINISH);
            //IList list = checkacceptanceBll.SearchCheckAcceptanceForm(info, pageIndex, AspNetPager2.PageSize, out listCount);

            QueryParam p = new QueryParam();
            p.PageIndex = AspNetPager2.CurrentPageIndex; ;
            p.PageSize = AspNetPager2.PageSize;
            p.TableName = "FM2E_CheckAcceptanceSearchView";

            string sqlSearch = "where 1=1";

            sqlSearch += " and ( DetailStatus in(" + (int)PurchaseRecordStatus.WAIT4INWAREHOUSE + "," + (int)PurchaseRecordStatus.INWAREHOUSEFINISH + ") )";
            if (WarehouseName != "")
            {
                sqlSearch += " and [WarehouseID] in(" + WarehouseId + ")";
            }

            p.ReturnFields = "*";
            p.OrderBy = "order by UpdateTime desc";
            p.Where = sqlSearch;
            IList list = checkacceptanceBll.SearchCheckAcceptanceForm(p, out listCount);

            AspNetPager2.RecordCount = listCount;
            gridview_check.DataSource = list;
            gridview_check.DataBind();
            TabContainer1.Tabs[1].HeaderText = "报验单(" + listCount + ")";

            if (listCount == 0)
                TabContainer1.ActiveTabIndex = 0;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取报验单列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
    }

}
