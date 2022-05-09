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


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_ExecutePurchasing : System.Web.UI.Page
{ 
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();

    CheckAcceptance checkacceptanceBll = new CheckAcceptance();

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
            IList list = purchaseBll.GetPurchaseOrdersByPurchaser(pageIndex, AspNetPager1.PageSize, out listCount,Common.Get_UserName);
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

            CheckAcceptanceSearchInfo info = new CheckAcceptanceSearchInfo();
            info.CompanyID = UserData.CurrentUserData.CompanyID;
            info.PurchaserConfirm = false;
            info.PurchaserID = Common.Get_UserName;
            IList list = checkacceptanceBll.SearchCheckAcceptanceForm(info, pageIndex, AspNetPager2.PageSize, out listCount);
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
