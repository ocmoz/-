using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.BLL.Basic;
using FM2E.BLL.Equipment;
using FM2E.BLL.System;

using FM2E.Model.Basic;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;
using FM2E.Model.System;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using System.Collections;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_QueryPurchase : System.Web.UI.Page
{
    protected bool IsMotherCompany = false;
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

        //绑定公司到下拉列表
        Company companyBll = new Company();
        IList<CompanyInfo> companyList = companyBll.GetAllCompany();

        DropDownList_Company.Items.Clear();
        DropDownList_Company.Items.Add(new ListItem("请选择公司", ""));
        foreach (CompanyInfo item in companyList)
        {
            DropDownList_Company.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
        }

        try
        {
            DropDownList_Company.SelectedValue = UserData.CurrentUserData.CompanyID;
        }
        catch
        {
            EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Warn, "在公司下拉列表中找不到用户所在公司，用户名：" + UserData.CurrentUserData.UserName + " 用户公司：" + UserData.CurrentUserData.CompanyID);
        }
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

            Array array = Enum.GetValues(typeof(PurchaseOrderStatus));
            List<PurchaseOrderStatus> liststatus = new List<PurchaseOrderStatus>();

            for (int i = 0; i < array.Length; i++)
            {
                if ((PurchaseOrderStatus)array.GetValue(i) != PurchaseOrderStatus.DRAFT)
                {
                    liststatus.Add((PurchaseOrderStatus)array.GetValue(i));
                }
            }
            PurchaseOrderStatus[] ar = liststatus.ToArray();

            PurchaseOrderSearchInfo info = new PurchaseOrderSearchInfo();
            info.StatusArray = ar;
            info.CompanyID = DropDownList_Company.SelectedValue;

            info.OrderSn = TextBox_OrderSn.Text.Trim();

            info.OrderName = TextBox_OrderName.Text.Trim();
            try { info.AmountLower = decimal.Parse(TextBox_AmountLower.Text.Trim()); }
            catch { }
            try { info.AmountUpper = decimal.Parse(TextBox_AmountUpper.Text.Trim()); }
            catch { }
            info.ProductName = TextBox_ProductName.Text.Trim();
            info.Model = TextBox_Model.Text.Trim();
            try { info.TimeLower = DateTime.Parse(TextBox_TimeLower.Text.Trim()); }
            catch { }
            try { info.TimeUpper = DateTime.Parse(TextBox_TimeUpper.Text.Trim()); }
            catch { }

            IList list = purchaseBll.SearchPurchaseOrder(info, pageIndex, AspNetPager1.PageSize, out listCount);
            AspNetPager1.RecordCount = listCount;

            TabContainer1.Tabs[0].HeaderText = "申购单(" + listCount + ")";
            if (listCount == 0)
                TabContainer1.ActiveTabIndex = 1;

            gridview_PurchaseApplyList.DataSource = list;
            gridview_PurchaseApplyList.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 填充列表
    /// </summary>
    private void FillData2()
    {
        try
        {
            int pageIndex = AspNetPager2.CurrentPageIndex;
            int listCount = 0;

            CheckAcceptanceSearchInfo info = new CheckAcceptanceSearchInfo();
            info.CompanyID = DropDownList_Company.SelectedValue;
            info.SheetID = TextBox_OrderSn.Text.Trim();

            info.SheetName = TextBox_OrderName.Text.Trim();
            
            info.ProductName = TextBox_ProductName.Text.Trim();
            info.Model = TextBox_Model.Text.Trim();
          

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
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 点击查找按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        TabContainer1.ActiveTabIndex = 0;
        FillData();
        FillData2();
    }
}
