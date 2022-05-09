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
using FM2E.BLL.Utils;
using FM2E.WorkflowLayer;



public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_PurchaseApply : System.Web.UI.Page
{
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();

    /// <summary>
    /// 该页在新增的时候使用SESSION的名称
    /// </summary>
    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_PurchaseApply";

    /// <summary>
    /// 该页在编辑的时候使用的Session名称
    /// </summary>
    string sessionName4Edit = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_PurchaseApplyEdit";

    /// <summary>
    /// 编辑页面的ID，如果为0，则为新增，不为0，则为编辑
    /// </summary>
    long editId = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);

    /// <summary>
    /// 追加订单的父ID 
    /// </summary>
    long parentid = (long)Common.sink("parentid", MethodType.Get, 0, 0, DataType.Long);

    /// <summary>
    /// 追加订单都父序列号
    /// </summary>
    string sn = (string)Common.sink("sn", MethodType.Get, 0, 0, DataType.Str);

    User userBll = new User();

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
        else
        {
            Label_UpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        //填充公司ID，公司名称
        string workflowstate = "";
        if (editId == 0)
        {
            Label_CompanyName.Text = UserData.CurrentUserData.CompanyName;
            Session[sessionName] = null;//清空该页面使用的SESSION
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;

            Label_Status.Text = "草稿";
            Label_SubmitTime.Text = "";
            Label_UpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            
            Label_ApplicantName.Text = UserData.CurrentUserData.PersonName;
            TextBox_OrderID.Text = "___________________";
            workflowstate = WorkflowHelper.GetWorkflowBasicInfo(PurchaseWorkflow.WorkflowName).InitialStateName;
        }
        else//编辑
        {
            PurchaseOrderInfo order = purchaseBll.GetPurchaseOrderByID(editId);
            //公司ID
            CompanyInfo company = new Company().GetCompany(order.CompanyID);
            if (company != null)
                Label_CompanyName.Text = company.CompanyName;
            TextBox_OrderID.Text = order.PurchaseOrderID + "-" + order.SubOrderIndex;
            TextBox_OrderName.Text = order.PurchaseOrderName;
            Session[sessionName4Edit] = order;

            HeadMenuWebControls1.ButtonList[1].ButtonUrl = "ViewPurchaseOrder.aspx?id=" + editId;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;

            Label_Status.Text = order.StatusString;
            Label_SubmitTime.Text = order.SubmitTime.ToString("yyyy-MM-dd HH:mm");
            Label_UpdateTime.Text = order.UpdateTime.ToString("yyyy-MM-dd HH:mm");
      

            Label_ApplicantName.Text = order.ApplicantName;
            workflowstate = order.WorkFlowStateName;
        }

        //编辑项的时候，有些项不能编辑
        TextBox_SelectedProductName.Attributes.Add("readonly", "readonly");
        TextBox_SelectedProductModel.Attributes.Add("readonly", "readonly");
        TextBox_Amount.Attributes.Add("readonly", "readonly");
        
        TextBox_Count.Attributes.Add("onchange", "javascript:onCountChange()");
        TextBox_UnitPrice.Attributes.Add("onchange", "javascript:onCountChange()");

        //单位
        DropDownList_Unit.DataSource = Constants.GetUnits();
        DropDownList_Unit.DataBind();

        DropDownList_Unit.Items.Insert(0, new ListItem("--请选择单位--", ""));

        //追加采购单
        if (sn != null && sn != "")
        {
            PurchaseOrderInfo parentOrder = purchaseBll.GetPurchaseOrderByID(parentid);
            if (parentOrder != null)
            {
                HyperLink_Parent.Text = parentOrder.PurchaseOrderID + "-" + parentOrder.SubOrderIndex + " " + parentOrder.PurchaseOrderName + " 机电材料申购单";
                HyperLink_Parent.NavigateUrl = "ViewPurchaseOrder.aspx?id=" + parentid + "&return=1";
                Tr_Parent.Visible = true;
            }
        }
        EquipmentSystem eqmtsysBll =new EquipmentSystem();
        //系统划分
        DropDownList_System.Items.AddRange(eqmtsysBll.GenerateListItemCollectionWithBlank());

        //工作流控件
        WorkFlowUserSelectControl1.EventIDField = "Name";
        WorkFlowUserSelectControl1.EventNameField = "Description";
        WorkFlowUserSelectControl1.WorkFlowState = workflowstate;
        WorkFlowUserSelectControl1.WorkFlowName = PurchaseWorkflow.WorkflowName;
        WorkFlowUserSelectControl1.EventListDataSource = WorkflowHelper.GetEventInfoList(PurchaseWorkflow.WorkflowName,workflowstate);
        WorkFlowUserSelectControl1.EventListDataBind();
        WorkFlowUserSelectControl1.ShowCompanySelect = true;
        WorkFlowUserSelectControl1.SelectedCompanyID = UserData.CurrentUserData.CompanyID;
        WorkFlowUserSelectControl1.SelectedDepartmentID = UserData.CurrentUserData.DepartmentID;
        WorkFlowUserSelectControl1.AddShowSelectDivValue(PurchaseWorkflow.SubmitNewEvent);
        WorkFlowUserSelectControl1.AddShowSelectDivValue(PurchaseWorkflow.SubmitDraftEvent);
        WorkFlowUserSelectControl1.AddShowSelectDivValue(PurchaseWorkflow.SubmitReturnModifyEvent);
        FillData();
    }

    /// <summary>
    /// 往模式对话框填入数据
    /// </summary>
    private void FillData()
    {
        IList list = null;
        if (editId == 0)
        {
            list = (IList)Session[sessionName];
            Table_ApprovalRecord.Visible = false;
            Table_ModifyRecord.Visible = false;
            Table_RelatedOrders.Visible = false;
        }
        else
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = order.DetailList;
            gridview_ApprovalRecord.DataSource = order.ApprovalList;
            gridview_ApprovalRecord.DataBind();
            Table_ApprovalRecord.Visible = true;

            gridview_ModifyRecord.DataSource = order.ModifyRecordList;
            gridview_ModifyRecord.DataBind();
            Table_ModifyRecord.Visible = true;

            gridview_RelatedOrders.DataSource = order.RelatedOrders;
            gridview_RelatedOrders.DataBind();
            Table_RelatedOrders.Visible = true;
        }
        if (list == null)//如果列表为空，填充回去
        {
            list = new List<PurchaseOrderDetailInfo>();
            if (editId == 0)
            {
                Session[sessionName] = list;
                Table_ApprovalRecord.Visible = false;

                Table_ModifyRecord.Visible = false;

                Table_RelatedOrders.Visible = false;
            }
            else
            {
                PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
                order.DetailList = list;
                Session[sessionName4Edit] = order;
                gridview_ApprovalRecord.DataSource = order.ApprovalList;
                gridview_ApprovalRecord.DataBind();
                Table_ApprovalRecord.Visible = true;

                gridview_ModifyRecord.DataSource = order.ModifyRecordList;
                gridview_ModifyRecord.DataBind();
                Table_ModifyRecord.Visible = true;

                gridview_RelatedOrders.DataSource = order.RelatedOrders;
                gridview_RelatedOrders.DataBind();
                Table_RelatedOrders.Visible = true;
            }
        }

        gridview_ItemList.DataSource = list;
        gridview_ItemList.DataBind();

        
    }



    /// <summary>
    /// 后台添加新项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddItem_Click(object sender, EventArgs e)
    {
//        EventMessage.MessageBox(Msg_Type.Info, "test", "test", Icon_Type.OK,false, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
        string selectedString = Hidden_SelectedItem.Value;
        string[] array = selectedString.Split('|');//分割，分别是产品名称、型号、单价、单位、数量、备注

        if (array.Length != 8)
        {
            EventMessage.MessageBox(Msg_Type.Error, "选取材料失败", "产品名称、规格型号、单价、单位、数量、备注中有项缺失，请重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        string name = array[0];
        string model = array[1];
        decimal unitprice = decimal.Parse(array[2]);
        string unit = array[3];
        decimal count = decimal.Parse(array[4]);
        string remark = array[5];
        string sysid = array[6];
        string sysname = array[7];
        //获取当前工作的LIST
        IList list = null;
        if (editId == 0)
            list = (IList)Session[sessionName];
        else
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = order.DetailList;
        }
        if (list == null)//如果列表为空，填充回去
        {
            list = new List<PurchaseOrderDetailInfo>();
            if (editId == 0)
            {
                Session[sessionName] = list;
            }
            else
            {
                PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
                order.DetailList = list;
                Session[sessionName4Edit] = order;
            }
        }

        //判断是不是已经存在相同的名称、型号、单价、单位、备注，如果是，则加到原来的那个项里面去
        PurchaseOrderDetailInfo item = new PurchaseOrderDetailInfo();
        bool findSame = false;
        foreach (PurchaseOrderDetailInfo detail in list)
        {
            if (detail.ProductName == name &&
                detail.Model == model &&
                detail.Unit == unit &&
                detail.Price == unitprice &&
                detail.Remark == remark &&
                detail.SystemID == sysid)
            {
                item = detail;
                findSame = true;
                break;
            }
        }
        if (!findSame)//找不到相同的
        {
            item.ItemID = (short)(list.Count + 1);//下一序号
            item.IsAsset = true;
            item.CompanyID = UserData.CurrentUserData.CompanyName;
            item.ProductName = name;
            item.Model = model;
            item.Unit = unit;
            item.Price = unitprice;
            item.PlanCount = count;
            item.AdjustCount = count;////调整的默认值
            item.AdjustPrice = unitprice;//调整的默认值
            item.Remark = remark;
            item.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.NONE;
            item.SystemID = sysid;
            item.SystemName = sysname;
            list.Add(item);
        }
        else
            item.PlanCount += count;//找到相同的，直接加上

        FillData();
    }

    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmount = 0;//每次postback都会自动初始化
    private decimal totalAdjustAmount = 0;
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PurchaseOrderDetailInfo item = e.Row.DataItem as PurchaseOrderDetailInfo;
            totalAmount += item.PlanAmount;
            totalAdjustAmount += item.AdjustAmount;
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
            if (LabelTotal != null)
            {
                LabelTotal.Text += totalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalAdjust != null)
            {
                LabelTotalAdjust.Text += totalAdjustAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            
        }

    }
    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        short itemID = (short)gridview_ItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        if (editId == 0)
            list = (IList)Session[sessionName];
        else
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = order.DetailList;
        }

        if (list == null||list.Count==0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "材料项:"+itemID+"已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }
       
        list.RemoveAt(itemID - 1);
        //重新更新ITEMID
        short id = 1;
        foreach (PurchaseOrderDetailInfo item in list)
        {
            item.ItemID = id;
            id++;
        }
        FillData();
    }

    ///// <summary>
    ///// 编辑行
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void gridview_ItemList_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    short itemID = (short)gridview_ItemList.DataKeys[e.NewEditIndex].Values["ItemID"];
    //    IList list = null;
    //    if (editId == 0)
    //        list = (IList)Session[sessionName];
    //    else
    //    {
    //        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
    //        if (order == null)
    //        {
    //            EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
    //            Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
    //            return;
    //        }
    //        list = order.DetailList;
    //    }
    //    if (list == null || list.Count == 0)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "编辑失败", "材料项:" + itemID + "已经不存在，请刷新",
    //           Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
    //        return;
    //    }
    //}

    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        short itemID = short.Parse(Hidden_EditItemID.Value);
        IList list = null;
        if (editId == 0)
            list = (IList)Session[sessionName];
        else
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = order.DetailList;
        }
        if (list == null)//如果列表为空，填充回去
        {
            list = new List<PurchaseOrderDetailInfo>();
            if (editId == 0)
            {
                Session[sessionName] = list;
            }
            else
            {
                PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
                order.DetailList = list;
                Session[sessionName4Edit] = order;
            }
        }
        PurchaseOrderDetailInfo target = null;
        foreach (PurchaseOrderDetailInfo item in list)
        {
            if (item.ItemID == itemID)
                target = item;
        }

        string name = TextBox_SelectedProductName.Text.Trim();
        string model = TextBox_SelectedProductModel.Text.Trim();
        decimal unitprice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
        string unit = DropDownList_Unit.SelectedValue;
        decimal count = decimal.Parse(TextBox_Count.Text.Trim());
        string remark = TextBox_Remark.Text.Trim();
        string sysid = DropDownList_System.SelectedValue;
        string sysname = DropDownList_System.SelectedItem.Text;
        if (target != null)
        {
            target.ProductName = name;
            target.Model = model;
            target.Unit = unit;
            target.Price = unitprice;
            target.PlanCount = count;
            target.Remark = remark;
            target.SystemID = sysid;
            target.SystemName = sysname;
        }
        FillData();
    }


    /// <summary>
    /// 保存草稿
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveDraft_Click(object sender, EventArgs e)
    {
        //依赖是否已经有ORDER_sn来判断是否第一次编辑
        string order_sn = TextBox_OrderID.Text.Trim();
        string order_name = TextBox_OrderName.Text.Trim();

        IList list = null;
        if (editId == 0)
            list = (IList)Session[sessionName];
        else
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = order.DetailList;
        }

        if(list==null||list.Count==0)
        {
            EventMessage.MessageBox(Msg_Type.Warn, "无法保存草稿", "未添加申购材料",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }
        if (editId == 0)// ADD DRAFT
        {
            
            PurchaseOrderInfo order = new PurchaseOrderInfo();
            order.CompanyID = UserData.CurrentUserData.CompanyID;
            
            order.PurchaseOrderName = order_name;
            order.Status = PurchaseOrderStatus.DRAFT;
            order.UpdateTime = order.SubmitTime = DateTime.Now;
            order.DetailList = list;
            order.Applicant = Common.Get_UserName;
            order.Remark = TextBox_OrderRemark.Text.Trim();
            if (sn == null || sn == "")
            {
                order_sn = purchaseBll.GenerateNextPurchaseOrderID(UserData.CurrentUserData.CompanyID);
                order.SubOrderIndex = 1;//新的子采购单
            }
            else
            {
                order_sn = sn;
                order.SubOrderIndex = purchaseBll.GenerateNextPurchaseOrderSubID(sn);//追加子采购单
            }
            order.PurchaseOrderID = order_sn;

            order.ModifyInfo = purchaseBll.GenerateModifyRecord(order, PurchaseOrderModifyType.SAVE, Common.Get_UserName);
            //保存起来
            bool success = false;
            try
            {
                purchaseBll.AddPurchaseApply(order);
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",ex,
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            if (success)
            {
                EventMessage.MessageBox(Msg_Type.Info, "保存草稿成功", "申购单保存成功",
               Icon_Type.OK, false, Common.GetHomeBaseUrl("PurchaseHistory.aspx"), UrlType.Href, "");
            }
        }
        else//UPDATE DRAFT
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            order.PurchaseOrderName = order_name;
           
            //order.Status = PurchaseOrderStatus.DRAFT;
            order.UpdateTime = order.SubmitTime = DateTime.Now;
            order.Remark = TextBox_OrderRemark.Text.Trim();
            order.ModifyInfo = purchaseBll.GenerateModifyRecord(order, PurchaseOrderModifyType.SAVE, Common.Get_UserName);
            //保存起来
            bool success = false;
            try
            {
                purchaseBll.UpdatePurchaseOrder(order);
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "保存草稿失败", "申购单保存失败，请刷新后重试", ex,
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            if (success)
            {
                EventMessage.MessageBox(Msg_Type.Info, "保存草稿成功", "申购单保存成功",
               Icon_Type.OK, false, Common.GetHomeBaseUrl("PurchaseHistory.aspx"), UrlType.Href, "");
            }
        }
    }

    /// <summary>
    /// 提交审批
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        //依赖是否已经有ORDER_sn来判断是否第一次编辑
        string order_sn = TextBox_OrderID.Text.Trim();
        string order_name = TextBox_OrderName.Text.Trim();

        IList list = null;
        if (editId == 0)
            list = (IList)Session[sessionName];
        else
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = order.DetailList;
        }

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Warn, "无法提交申请单", "未添加申购材料",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        if (!WorkFlowUserSelectControl1.ProperlySelected)
        {
            EventMessage.MessageBox(Msg_Type.Warn, "无法提交申请单", "无选择下一处理用户",
              Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        
        if (editId == 0)//SUBMIT
        {
           
            PurchaseOrderInfo order = new PurchaseOrderInfo();
           
            order.CompanyID = UserData.CurrentUserData.CompanyID;
            order.PurchaseOrderName = order_name;
            if (order.Status == PurchaseOrderStatus.APPROVALANDRETURN)
                order.Status = PurchaseOrderStatus.REAPPROVALING;
            else
                order.Status = PurchaseOrderStatus.WAITING4APPROVAL;
            order.UpdateTime = order.SubmitTime = DateTime.Now;
            order.DetailList = list;
            order.Applicant = Common.Get_UserName;
            order.Remark = TextBox_OrderRemark.Text.Trim();
            if (sn == null || sn == "")
            {
                order_sn = purchaseBll.GenerateNextPurchaseOrderID(UserData.CurrentUserData.CompanyID);
                order.SubOrderIndex = 1;//新的子采购单
            }
            else
            {
                order_sn = sn;
                order.SubOrderIndex = purchaseBll.GenerateNextPurchaseOrderSubID(sn);//追加子采购单
            }
            order.PurchaseOrderID = order_sn;
            order.ModifyInfo = purchaseBll.GenerateModifyRecord(order, PurchaseOrderModifyType.SUBMIT, Common.Get_UserName);
            //保存起来
             bool success = false;
            try
            {
                order.ID 
                    = purchaseBll.SavePurchaseOrder(order);

                //**********Modified by Xue 2011-6-27****************************************************************************************************
                FM2E.BLL.PendingOrder.PendingOrder pobll = new FM2E.BLL.PendingOrder.PendingOrder();
                string lastURL = Request.Url.AbsolutePath + "?" + Request.QueryString.ToString();
                if (lastURL.Contains("/Web/Module/FM2E"))
                {
                    lastURL = lastURL.Replace("/Web/Module/FM2E", "..");
                }
                if (lastURL.Contains("/Module/FM2E"))
                {
                    lastURL = lastURL.Replace("/Module/FM2E", "..");
                }
                pobll.MarkReadByURL(lastURL);
                //**********Modification Finished 2011-6-27**********************************************************************************************

                //使用工作流
                Guid guid = WorkflowHelper.CreateNewInstance(order.ID, PurchaseWorkflow.WorkflowName);
                WorkflowHelper.SetStateMachine<PurchaseEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
                WorkflowHelper.UpdateNextUserID(guid, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "提交失败", "采购申请单提交失败，请刷新后重试", ex,
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            if (success)
            {
                //发送通知消息
                long thisID = order.ID;
                string title = "采购申请：" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + WorkFlowUserSelectControl1.SelectedEventName;
                string URL = "../DeviceManager/SpareEquipmentManager/Purchase/PurchaseApproval/ApprovalViewPurchaseOrder.aspx?id=" + thisID + "&cmd=history";
                try
                {
                    WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 
                        0, null,WorkFlowUserSelectControl1.NextUserName,WorkFlowUserSelectControl1.DelegateUserName);
                }
                catch (Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误"+ex.Message);
                }


                EventMessage.MessageBox(Msg_Type.Info, "提交成功", "采购申请单提交成功",
               Icon_Type.OK, false, Common.GetHomeBaseUrl("PurchaseHistory.aspx"), UrlType.Href, "");
            }
        }
        else//UPDATE AND SUBMIT
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName4Edit];
            if (order == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取采购申请单失败", "该申购单可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
           
            order.PurchaseOrderName = order_name;
            if (order.Status == PurchaseOrderStatus.APPROVALANDRETURN)//如果是修改后再审核的状态，则下一个审核的人，应该是原来审核过的第一个
            {
                order.Status = PurchaseOrderStatus.REAPPROVALING;
                order.Approvaling = order.FirstApprovaler;
            }
            else
                order.Status = PurchaseOrderStatus.WAITING4APPROVAL;
            order.UpdateTime = order.SubmitTime = DateTime.Now;

            order.Remark = TextBox_OrderRemark.Text.Trim();
            order.ModifyInfo = purchaseBll.GenerateModifyRecord(order, PurchaseOrderModifyType.SUBMIT, Common.Get_UserName);
              bool success = false;
            try
            {
                purchaseBll.SavePurchaseOrder(order);
                Guid guid = new Guid(order.WorkFlowInstanceID);
                WorkflowHelper.SetStateMachine<PurchaseEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
                WorkflowHelper.UpdateNextUserID(guid, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "提交失败", "采购申请单提交失败，请刷新后重试", ex,
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            if (success)
            {
                try
                {
                    if (order.Status == PurchaseOrderStatus.WAITING4APPROVAL)
                    {
                        long thisID = order.ID;
                        string title = "采购申请：" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + WorkFlowUserSelectControl1.SelectedEventName;
                        string URL = "../DeviceManager/SpareEquipmentManager/Purchase/PurchaseApproval/ApprovalViewPurchaseOrder.aspx?id=" + thisID + "&cmd=history";
                        WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL,
                            0, null, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
                    }
                    else
                    {
                        long thisID = order.ID;
                        string title = "采购申请：" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + WorkFlowUserSelectControl1.SelectedEventName;
                        string URL = "../DeviceManager/SpareEquipmentManager/Purchase/PurchaseApproval/ApprovalViewPurchaseOrder.aspx?id=" + thisID + "&cmd=history";
                        WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL,
                            0, null, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
                    }
                }
                catch (Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
                }

                EventMessage.MessageBox(Msg_Type.Info, "提交成功", "采购申请单提交成功",
               Icon_Type.OK, false, Common.GetHomeBaseUrl("PurchaseHistory.aspx"), UrlType.Href, "");
            }
        }
    }
}
