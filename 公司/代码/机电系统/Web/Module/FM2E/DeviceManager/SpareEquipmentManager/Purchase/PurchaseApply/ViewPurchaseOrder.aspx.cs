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


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_ViewPurchaseOrder : System.Web.UI.Page
{
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();
    /// <summary>
    /// 需要查看的采购单ID
    /// </summary>
    protected long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);

    protected string sn = "";

    protected string cmd = (string)Common.sink("cmd", MethodType.Get, 0, 0, DataType.Str);

    protected int canreturn = (int)Common.sink("return", MethodType.Get, 0, 0, DataType.Int);
    /// <summary>
    /// 加载页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

        //绑定编辑按钮
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = canEdit;
        HeadMenuWebControls1.ButtonList[0].ButtonPopedom = PopedomType.Edit;

        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "PurchaseApply.aspx?id=" + id;
        if (cmd == "viewArchives")
        {
            canreturn = 1;
        }

        //返回
        if (canreturn == 1)
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[3].ButtonVisible = true;
        }
        else
        {
            //追加采购
            HeadMenuWebControls1.ButtonList[1].ButtonUrl = "PurchaseApply.aspx?parentid=" + id + "&sn=" + sn;
            HeadMenuWebControls1.ButtonList[3].ButtonVisible = false;
        }


    }


    private bool canEdit = false;

    /// <summary>
    /// 往对应的控件填入数据
    /// </summary>
    private void FillData()
    {
        PurchaseOrderInfo order = purchaseBll.GetPurchaseOrderByID(id);



        //公司ID
        CompanyInfo company = new Company().GetCompany(order.CompanyID);
        if (company != null)
            Label_CompanyName.Text = company.CompanyName;

        sn = order.PurchaseOrderID;
        Label_OrderName.Text = order.PurchaseOrderName;
        Label_OrderID.Text = order.PurchaseOrderID +"-" +order.SubOrderIndex;

        //Label_Status.Text = order.StatusString;
        Label_Status.Text = order.WorkFlowStateDescription;
        if (!string.IsNullOrEmpty(order.NextUserName))
        {
            Label_Approvaling.Text = order.NextUserPersonName + "  " + order.NextUserPositionName;
        }
        if (!string.IsNullOrEmpty(order.DelegateUserName))
        {
            Label_Approvaling.Text += "(由 " + order.DelegateUserPersonName + "  " + order.DelegateUserPositionName + " 代办)";
        }
        //if (order.Status == PurchaseOrderStatus.APPROVALING||order.Status== PurchaseOrderStatus.REAPPROVALING)
        //{
        //    Label_Approvaling.Text = "(" + ((order.Approvaling == null || order.Approvaling == "") ? "等待上一级审批" : order.Approvaling) + ")";
        //    Label_Approvaling.Visible = true;
        //}
        //else
        //     Label_Approvaling.Visible = false;

        Label_UpdateTime.Text = (order.UpdateTime == DateTime.MinValue)? "": order.UpdateTime.ToString("yyyy-MM-dd HH:mm");

        Label_SubmitTime.Text = (order.SubmitTime == DateTime.MinValue) ? "" : order.SubmitTime.ToString("yyyy-MM-dd HH:mm");
        //Label_Applicant.Text = order.Applicant;
        Label_ApplicantName.Text = order.ApplicantName;
        IList list = order.DetailList;
        if (list == null)
        {
            list = new List<PurchaseOrderDetailInfo>();
        }
        gridview_ItemList.DataSource = list;
        gridview_ItemList.DataBind();

        gridview_ApprovalRecord.DataSource = order.ApprovalList;
        gridview_ApprovalRecord.DataBind();

        gridview_ModifyRecord.DataSource = order.ModifyRecordSubmitList;
        gridview_ModifyRecord.DataBind();

        gridview_RelatedOrders.DataSource = order.RelatedOrders;
        gridview_RelatedOrders.DataBind();

        canEdit = order.CanEdit;
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmount = 0;//每次postback都会自动初始化
    private decimal totalAdjustAmount = 0;
    private decimal totalActualAmount = 0;
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PurchaseOrderDetailInfo item = e.Row.DataItem as PurchaseOrderDetailInfo;
            totalAmount += item.PlanAmount;
            totalAdjustAmount += item.AdjustAmount;
            totalActualAmount += item.ActualAmount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 6;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            for (int i = 1; i <= 5; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label LabelTotal = e.Row.FindControl("Label_TotalAmount") as Label;
            Label LabelTotalAdjust = e.Row.FindControl("Label_AdjustTotalAmount") as Label;
            Label LabelTotalActual = e.Row.FindControl("Label_ActualTotalAmount") as Label;
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalAdjust != null)
            {
                LabelTotalAdjust.Text = totalAdjustAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalActual != null)
            {
                LabelTotalActual.Text = totalActualAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
        }

    }
}
