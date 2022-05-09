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
using FM2E.WorkflowLayer;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_PurchaseApproval : System.Web.UI.Page
{ 
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();
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
    /// 填充列表
    /// </summary>
    private void FillData()
    {
        try
        {
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int listCount = 0;

            PurchaseOrderSearchInfo info = new PurchaseOrderSearchInfo();
            info.WorkFlowStatus.Add(PurchaseWorkflow.ApprovalState);
            info.WorkFlowUserName = Common.Get_UserName;
            IList list = purchaseBll.SearchPurchaseOrder(info,pageIndex, AspNetPager1.PageSize, out listCount);
            AspNetPager1.RecordCount = listCount;
            //排序，先把正在审批的排在前面，然后按照Submit顺序
            gridview_PurchaseApplyList.DataSource = list;
            gridview_PurchaseApplyList.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
