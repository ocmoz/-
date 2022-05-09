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

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_PurchaseHistory : System.Web.UI.Page
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

            //Array array = Enum.GetValues(typeof(PurchaseOrderStatus));
            //List<PurchaseOrderStatus> liststatus = new List<PurchaseOrderStatus>();
            
            //for (int i = 0; i < array.Length; i++)
            //{
            //    if ((PurchaseOrderStatus)array.GetValue(i) != PurchaseOrderStatus.INWAREHOUSEFINISH)
            //    {
            //        liststatus.Add((PurchaseOrderStatus)array.GetValue(i));
            //    }
            //}
            //PurchaseOrderStatus[] ar = liststatus.ToArray();
             
            PurchaseOrderSearchInfo info = new PurchaseOrderSearchInfo();
            List<WorkflowStateInfo> states = WorkflowHelper.GetAllStateInfo(PurchaseWorkflow.WorkflowName);
            foreach (WorkflowStateInfo state in states)
            {
                if (state.Name != PurchaseWorkflow.ArchiveState)
                {
                    info.WorkFlowStatus.Add(state.Name);
                }
            }

            //info.StatusArray = ar;
            info.CompanyID = UserData.CurrentUserData.CompanyID;
            info.Applicant = Common.Get_UserName;

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

            gridview_PurchaseApplyList.DataSource = list;
            gridview_PurchaseApplyList.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 删除草稿状态下的申请单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_PurchaseApplyList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long id = (long)gridview_PurchaseApplyList.DataKeys[e.RowIndex].Values["ID"];
        
        bool success = false;
        try
        {
            
            purchaseBll.DeletePurchase(id);
            success = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "删除失败，请刷新后重试", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (success)
        {
            FillData(); 
        }
    }

    protected void OnGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long id = long.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "Archive")
        {
            bool success = false;
            try
            {
                PurchaseOrderInfo order = purchaseBll.GetPurchaseOrderByID(id);
                Guid guid = new Guid(order.WorkFlowInstanceID);
                List<WorkflowEventInfo> eventlist = WorkflowHelper.GetEventInfoList(PurchaseWorkflow.WorkflowName, order.WorkFlowStateName);
                if (eventlist.Count > 0)
                {
                   
                    WorkflowHelper.SetStateMachine<PurchaseEventService>(guid, eventlist[0].Name);
                }
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "归档失败", "归档失败，请刷新后重试", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (success)
            {
                FillData();
            }
        }
        
                    
    }


    /// <summary>
    /// 编辑草稿状态下的申请单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_PurchaseApplyList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        long id = (long)gridview_PurchaseApplyList.DataKeys[e.NewEditIndex].Values["ID"];
        Response.Redirect("PurchaseApply.aspx?id=" + id);
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
    }
}




