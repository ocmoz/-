using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using FM2E.BLL.Basic;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using FM2E.BLL.Utils;



public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApply_EditOutWarehouseApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    //private string APPLYDETAILLIST = "OutWarehouseApplyDetailList";

    private readonly OutWarehouse bll = new OutWarehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
    }

    private OutWarehouseApplyInfo CurrentOutWarehouseApplyInfo
    {
        get {
            OutWarehouseApplyInfo item = (OutWarehouseApplyInfo)Session[this.ToString()];
            if (item == null)
            {
                if (cmd == "edit")
                {
                    item = bll.GetOutWarehouseApplyInfo(id);
                    Session[this.ToString()] = item;
                }
                if (cmd == "new")
                {
                    item = new OutWarehouseApplyInfo();
                    Session[this.ToString()] = item;
                }
            }
            return item;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }

    private string CurrentAction
    {
        get
        {
            string action = (string)ViewState["action"];
            if (string.IsNullOrEmpty(action))
                action = ADD_DETAIL;
            return action;
        }
        set { ViewState["action"] = value; }
    }

    private const string ADD_DETAIL = "addDetail";
    private const string UPDATE_DETAIL = "updateDetail";

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            string workflowstate = "";
            Warehouse wbll = new Warehouse();
            IList<WarehouseInfo> list = wbll.GetAllWarehouse();

            DropDownList_Warehouse.Items.Clear();
            DropDownList_Warehouse.Items.Add(new ListItem("请选择仓库", ""));
            foreach (WarehouseInfo item in list)
            {
                DropDownList_Warehouse.Items.Add(new ListItem(item.Name, item.WareHouseID.ToString()));
            }
            DDLUnit.Items.Clear();
            IList list1 = Constants.GetUnits();
            foreach (string unit in list1)
                DDLUnit.Items.Add(new ListItem(unit, unit));
            DDLUnit.Items.Insert(0, new ListItem("请选择单位", ""));

            DDLSystem.Items.Clear();
            DDLSystem.Items.AddRange(ListItemHelper.GetSystemListItemsWithBlank());


            OutWarehouseApplyInfo applyitem = null;
            if (cmd == "add")
            {
                Label1.Text = "_____________";
                applyitem = new OutWarehouseApplyInfo();
                applyitem.CompanyID = UserData.CurrentUserData.CompanyID;
                workflowstate = WorkflowHelper.GetWorkflowBasicInfo(OutWarehouseWorkflow.WorkflowName).InitialStateName;
            }
            else if (cmd == "edit")
            {
                applyitem = bll.GetOutWarehouseApplyInfo(id);
                Label1.Text = applyitem.SheetName.ToString();
                try
                {
                    DropDownList_Warehouse.SelectedValue = applyitem.WarehouseID;
                }
                catch { }
                TextArea1.Value = applyitem.ApplyRemark;
                if (applyitem.WorkFlowStateName != OutWarehouseWorkflow.DraftState&& applyitem.WorkFlowStateName!=OutWarehouseWorkflow.ReturnModifyState )
                {
                    
                    btSubmit.Visible = false;
                }
                workflowstate = applyitem.WorkFlowStateName;
            }
            CurrentOutWarehouseApplyInfo = applyitem;

            //工作流控件
            WorkFlowUserSelectControl1.EventIDField = "Name";
            WorkFlowUserSelectControl1.EventNameField = "Description";
            WorkFlowUserSelectControl1.WorkFlowState = workflowstate;
            WorkFlowUserSelectControl1.WorkFlowName = OutWarehouseWorkflow.WorkflowName;
            WorkFlowUserSelectControl1.EventListDataSource = WorkflowHelper.GetEventInfoList(OutWarehouseWorkflow.WorkflowName, workflowstate);
            WorkFlowUserSelectControl1.EventListDataBind();
            WorkFlowUserSelectControl1.ShowCompanySelect = true;
            WorkFlowUserSelectControl1.SelectedCompanyID = UserData.CurrentUserData.CompanyID;
            WorkFlowUserSelectControl1.SelectedDepartmentID = UserData.CurrentUserData.DepartmentID;
            WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitNewEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitDraftEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitReturnModifyEvent);
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
        try
        {
            GridView1.DataSource = CurrentOutWarehouseApplyInfo.ApplyDetailList;
            GridView1.DataBind();
            LblErrorMessage.Text = "";
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：出库申请添加";

            //TabPanel1.HeaderText = "添加申请";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：出库申请修改";

            //TabPanel1.HeaderText = "修改申请";
        }
    }
    /// <summary>
    /// 按钮提交申请的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {


        if (DropDownList_Warehouse.SelectedValue == "")
        {
            LblErrorMessage.Text += "请选择仓库";
            return;
        }

        if (!WorkFlowUserSelectControl1.ProperlySelected)
        {
            LblErrorMessage.Text += "请选择下一审批用户";
            return;
        }
        OutWarehouseApplyInfo item = CurrentOutWarehouseApplyInfo;
        if (item.ApplyDetailList.Count==0)
        {
            LblErrorMessage.Text += "未添加出库信息";
            return;
        }



        
        try
        {
            item.WarehouseID = DropDownList_Warehouse.SelectedValue;
            item.ApplyRemark = TextArea1.Value.Trim();
            item.ApplyTime = DateTime.Now;
            item.Applicant = Common.Get_UserName;
            item.CompanyID = UserData.CurrentUserData.CompanyID;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        bool bSuccess = false; string title = ""; string URL = "";
        if (cmd == "add")
        {
            try
            {
                item.SheetName = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.OUTWAREHOUSEAPPLY);
                item.ID = bll.SavaOutWarehouseApply(item);
                //使用工作流
                Guid guid = WorkflowHelper.CreateNewInstance(item.ID, OutWarehouseWorkflow.WorkflowName);
                WorkflowHelper.SetStateMachine<OutWarehouseEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
                WorkflowHelper.UpdateNextUserID(guid, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
                title = "你有新的设备出库申请" + item.SheetName + "待审批";
                URL = "../DeviceManager/SpareEquipmentManager/OutWarehouse/OutWarehouseApproval/OutWarehouseApply.aspx";
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            
        }
        else if (cmd == "edit")
        {
            try
            {
                bll.SavaOutWarehouseApply(item);
                title = "你有新的设备出库申请" + item.SheetName + "待审批";
                URL = "../DeviceManager/SpareEquipmentManager/OutWarehouse/OutWarehouseApproval/OutWarehouseApply.aspx";
                Guid guid = new Guid(item.WorkFlowInstanceID);
                WorkflowHelper.SetStateMachine<OutWarehouseEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
                WorkflowHelper.UpdateNextUserID(guid, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);

                //**********Modified by Xue 2011-6-27****************************************************************************************************
                //FM2E.BLL.PendingOrder.PendingOrder pobll = new FM2E.BLL.PendingOrder.PendingOrder();
                //pobll.DeletePendingOrder(pobll.GetPendingOrderIDByURL(URL));
                //**********Modification Finished 2011-6-27**********************************************************************************************

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }

        if (bSuccess)
        {
            try
            {
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL,
                       0, null, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
            }
            EventMessage.MessageBox(Msg_Type.Info, "提交成功", "出库申请单提交成功",
             Icon_Type.OK, false, Common.GetHomeBaseUrl("OutWarehouseApply.aspx"), UrlType.Href, "");
        }
    }

    /// <summary>
    /// 添加明细和更新明细响应的事件
    /// 分两种情况处理
    /// 1.添加申请时，明细保存在Session[APPLYDETAILLIST]中，提交申请时使用事务一次性提交数据库
    /// 2.编辑申请时，编辑明细直接操作数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (!ValidateDetailInput())
            return;

        OutWarehouseDetailInfo targetItem = null;
        
        LblErrorMessage.Text = "";
        try
        {
            OutWarehouseApplyInfo applyitem = CurrentOutWarehouseApplyInfo;
            IList list = applyitem.ApplyDetailList;

            if (CurrentAction == ADD_DETAIL)
            {
                targetItem = new OutWarehouseDetailInfo();
                
                list.Insert(0, targetItem);
            }
            else if (CurrentAction == UPDATE_DETAIL)
            {
                if (!string.IsNullOrEmpty(Hidden_Edit_RowIndex.Value))
                {
                    int index = 0;
                    int.TryParse(Hidden_Edit_RowIndex.Value, out index);

                    targetItem = (OutWarehouseDetailInfo)list[index];
                }
                else
                {
                    targetItem = new OutWarehouseDetailInfo();
                    list.Insert(0, targetItem);
                }
                
            }
            targetItem.ProductName = ProductName.Value.Trim();
            targetItem.Model = TextBox_Model.Text.Trim();
            targetItem.Count = Convert.ToDecimal(TextBox_Count.Text.Trim());
            targetItem.Unit = DDLUnit.SelectedValue;
            targetItem.Usage = TextBox_Usage.Value.Trim();
            targetItem.AddressID = long.Parse(Hidden_AddressID.Value);
            targetItem.AddressName = TextBox_Address.Value;
            targetItem.Remark = TextBox_ApplyRemark.Value.Trim();
            targetItem.SystemID = DDLSystem.SelectedValue;
            targetItem.SystemName = DDLSystem.SelectedItem.Text;
            targetItem.DetailLocation = TextBox_DetailLocation.Value.Trim();

            CurrentOutWarehouseApplyInfo = applyitem;

            FillData();

            CurrentAction = ADD_DETAIL;
            Button3.Text = "添加明细";
            Hidden_Edit_RowIndex.Value = "";
            ProductName.Value = TextBox_Model.Text = TextBox_Count.Text = TextBox_Usage.Value = TextBox_ApplyRemark.Value = TextBox_DetailLocation.Value = "";
            DDLUnit.SelectedIndex = 0;
            DDLSystem.SelectedIndex = 0;
            Hidden_AddressID.Value = "";
            TextBox_Address.Value = "";
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加明细失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }
    /// <summary>
    /// 校验添加明细时的输入是否合法
    /// </summary>
    private bool ValidateDetailInput()
    {
        string errorMsg = "";
        if (ProductName.Value.Trim() == string.Empty)
        {
            errorMsg += "产品名称不能为空";
        }
        else if (TextBox_Model.Text.Trim() == string.Empty)
        {
            errorMsg += "规格型号不能为空";
        }
        else if (TextBox_Count.Text.Trim() == string.Empty)
        {
            errorMsg += "数量不能为空";
        }
        else if (DDLUnit.SelectedValue == string.Empty)
        {
            errorMsg += "请选择单位";
        }
        else
            if (string.IsNullOrEmpty(Hidden_AddressID.Value)||Hidden_AddressID.Value=="0")
            {
                errorMsg += "请选择地址";
            }
            else
        if (TextBox_Count.Text.Trim() != string.Empty)
        {
            try
            {
                Convert.ToDecimal(TextBox_Count.Text.Trim());
            }
            catch
            {
                errorMsg += "数量只能为数字，请检查输入";
            }
        }
        else
        if (TextBox_Count.Text.Trim() != string.Empty)
        {
            try
            {
                Convert.ToDecimal(TextBox_Count.Text.Trim());
            }
            catch
            {
                errorMsg += "数量只能为数字，请检查输入";
            }
        }

        if (errorMsg != "")
        {
            //EventMessage.EventWriteLog(Msg_Type.Error, "输入有误：" + errorMsg);
            LblErrorMessage.Text = "输入有误：" + errorMsg;
            return false;
            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        return true;
    }
    ///// <summary>
    ///// “完成”操作
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    TabContainer1.Tabs[0].Visible = true;
    //    TabContainer1.Tabs[1].Visible = false;
    //    LblErrorMessage.Text = "";
    //}
    ///// <summary>
    ///// “编辑明细”操作
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    TabContainer1.Tabs[0].Visible = false;
    //    TabContainer1.Tabs[1].Visible = true;
    //    FillData();
    //}


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowNum = Convert.ToInt32(e.CommandArgument);

        OutWarehouseApplyInfo applyitem = CurrentOutWarehouseApplyInfo;
        IList list = applyitem.ApplyDetailList;

        if (e.CommandName == "del")
        {
            //删除
            if (list == null) return;
            list.RemoveAt(rowNum);
            CurrentOutWarehouseApplyInfo = applyitem;
            FillData();
        }
        else if (e.CommandName == "edititem")
        {
            if (list == null || list.Count == 0)
                return;
            int index =  rowNum;
            OutWarehouseDetailInfo item = (OutWarehouseDetailInfo)list[index];
            ProductName.Value = item.ProductName;
            TextBox_Model.Text = item.Model;
            TextBox_Count.Text = item.Count.ToString("0.#####");
            DDLUnit.SelectedValue = item.Unit;
            TextBox_Usage.Value = item.Usage;
            TextBox_ApplyRemark.Value = item.Remark;
            Hidden_AddressID.Value = item.AddressID.ToString();
            TextBox_Address.Value = item.AddressName;
            try
            {
                DDLSystem.SelectedValue = item.SystemID;
            }
            catch { }
            TextBox_DetailLocation.Value = item.DetailLocation;
            Hidden_Edit_RowIndex.Value = rowNum.ToString();

            CurrentAction = UPDATE_DETAIL;

            Button3.Text = "更新明细";

            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
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
        }
    }
}
