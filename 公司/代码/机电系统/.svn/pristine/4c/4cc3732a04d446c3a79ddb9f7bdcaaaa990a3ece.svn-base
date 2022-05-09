using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.BLL.Utils;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApply_EditBorrowApply : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    //private const string DETAILSESSION = "BorrowApplyDetail";
    private readonly Secondment secondmentBll = new Secondment();

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
            //Pager();
        }        
    }

    private BorrowApplyInfo CurrentApply
    {
        get {
            BorrowApplyInfo item = (BorrowApplyInfo)Session[this.ToString()];
            if (item == null)
            {
                item = new BorrowApplyInfo();
                item.Applicant = Common.Get_UserName;
                item.ApplicantName = UserData.CurrentUserData.PersonName;
                item.BorrowCompanyID = UserData.CurrentUserData.CompanyID;
                item.BorrowCompanyName = UserData.CurrentUserData.CompanyName;
                item.Status = BorrowApplyStatus.Draft;
                item.SubmitTime = DateTime.Now;
                Session[this.ToString()] = item;
            }
            return item;
        }
        set {
            Session[this.ToString()] = value;
        }
    }
    //当前动作
    private string CurrentAction
    {
        get
        {
            string ac = (string)ViewState["action"];
            if (ac == null)
            {
                ac = AddAction;
                ViewState["action"] = ac;
            }
            return ac;
        }
        set
        {
            ViewState["action"] = value;
        }
    }

    string AddAction = "addDetail";
    string EditAction = "editDetail";

    private void InitialPage()
    {
        //绑定公司到下拉列表
        Company companyBll = new Company();
        IList<CompanyInfo> companyList = companyBll.GetAllCompany();

        string companyID = UserData.CurrentUserData.CompanyID;
        ddlLendCompany.Items.Clear();
        ddlLendCompany.Items.Add(new ListItem("请选择借出方公司", ""));
        foreach (CompanyInfo item in companyList)
        {
            if (companyID == item.CompanyID)
                continue;
            ddlLendCompany.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
        }

        //绑定单位下拉列表
        ddlUnit.DataSource = Constants.GetUnits();
        ddlUnit.DataBind();

        CurrentAction = AddAction;

        try
        {
            if (cmd == "add")
            {
                BorrowApplyInfo item = new BorrowApplyInfo();
                item.Applicant = Common.Get_UserName;
                item.ApplicantName = UserData.CurrentUserData.PersonName;
                item.BorrowCompanyID = UserData.CurrentUserData.CompanyID;
                item.BorrowCompanyName = UserData.CurrentUserData.CompanyName;
                item.Status = BorrowApplyStatus.Draft;
                item.SubmitTime = DateTime.Now;
                CurrentApply = item;
            }
            else if (cmd == "edit")
            {
                BorrowApplyInfo model = secondmentBll.GetBorrowApply(id);
                CurrentApply = model;

                
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void FillData()
    {
        if (CurrentApply.Status != BorrowApplyStatus.Draft)
        {
            Button2.Visible = false;
            Button3.Visible = false;
        }

        lbSheetNO.Text = CurrentApply.SheetName;
        lbApplicant.Text = CurrentApply.ApplicantName;
        lbBorrowCompany.Text = CurrentApply.BorrowCompanyName;
        lbStatus.Text = CurrentApply.StatusString;
        
        try
        {
            ddlLendCompany.SelectedValue = CurrentApply.CompanyID;
        }
        catch { }

        GridView_Detail.DataSource = CurrentApply.DetailList;
        GridView_Detail.DataBind();
    }
    ///// <summary>
    ///// 明细分页
    ///// </summary>
    //private void Pager()
    //{
    //    //明细信息
    //    ArrayList list = (ArrayList)Session[DETAILSESSION];
    //    if (list == null)
    //    {
    //        list = new ArrayList();
    //        Session[DETAILSESSION] = list;
    //    }
    //    int min = (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize;
    //    int max = (AspNetPager1.CurrentPageIndex * AspNetPager1.PageSize) > list.Count ? list.Count : AspNetPager1.CurrentPageIndex * AspNetPager1.PageSize;
    //    max = max - 1;
    //    ArrayList thisList = list.GetRange(min, max - min + 1);
    //    AspNetPager1.RecordCount = list.Count;
    //    GridView1.DataSource = thisList;
    //    GridView1.DataBind();
    //}

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：添加设备借调申请";
            //TabContainer1.Tabs[0].HeaderText = "添加借调申请";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：修改设备借调申请";
            //TabContainer1.Tabs[0].HeaderText = "修改借调申请";
        }
    }
    //    /// <summary>
    ///// 编辑明细
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    TabContainer1.Tabs[1].Visible = true;
    //    TabContainer1.ActiveTabIndex = 1;
    //}
    /// <summary>
    /// 保存草稿
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        ValidateInput();

        BorrowApplyInfo item = CurrentApply;
        
            item.CompanyID = ddlLendCompany.SelectedValue;
            
            item.Status = BorrowApplyStatus.Draft;  //草稿
            item.SubmitTime = DateTime.Now;
       

        //ArrayList list = (ArrayList)CurrentApply.DetailList;
        //item.DetailList = list;

        if (cmd == "add")
        {
            try
            {
                CurrentApply.SheetName = SheetNOGenerator.GetSheetNO(CurrentApply.CompanyID, SheetType.SECONDMENT_BORROWAPPLY);
                secondmentBll.AddBorrowApply(CurrentApply);
                //Session.Remove(DETAILSESSION);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowApply.aspx"), UrlType.Href, "");
        }
        else if (cmd == "edit")
        {
            try
            {
                CurrentApply.BorrowApplyID = id;
                secondmentBll.UpdateBorrowApply(CurrentApply);
                //Session.Remove(DETAILSESSION);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowApply.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// 提交申请
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        ValidateInput();

        BorrowApplyInfo item = CurrentApply;
       
            item.CompanyID = ddlLendCompany.SelectedValue;
            
            item.Status = BorrowApplyStatus.Waiting4ApprovalResult;  //等待审批结果
            item.SubmitTime = DateTime.Now;
        

        //ArrayList list = (ArrayList)Session[DETAILSESSION];
        //item.DetailList = list;

        if (cmd == "add")
        {
            try
            {
                CurrentApply.SheetName = SheetNOGenerator.GetSheetNO(CurrentApply.CompanyID, SheetType.SECONDMENT_BORROWAPPLY);
                long thisID = secondmentBll.AddBorrowApply(CurrentApply);
                string title = "设备借调申请" + CurrentApply.SheetName + "待审批";
                string URL = "../DeviceManager/UsedEquipmentManager/EquipmentSecondment/BorrowApproval/Approval.aspx?cmd=approval&id=" + thisID;
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder<EquipmentBorrowEventService>(thisID, title, EquipmentBorrowWorkflow.WorkflowName, EquipmentBorrowWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0,  CurrentApply.CompanyID);
                //Session.Remove(DETAILSESSION);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowApply.aspx"), UrlType.Href, "");
        }
        else if (cmd == "edit")
        {
            try
            {
                CurrentApply.BorrowApplyID = id;
                secondmentBll.UpdateBorrowApply(CurrentApply);
                string title = "设备借调申请" + CurrentApply.SheetName + "待审批";
                string URL = "../DeviceManager/UsedEquipmentManager/EquipmentSecondment/BorrowApproval/Approval.aspx?cmd=approval&id=" + id;
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder<EquipmentBorrowEventService>(id, title, EquipmentBorrowWorkflow.WorkflowName, EquipmentBorrowWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, CurrentApply.CompanyID);
                //Session.Remove(DETAILSESSION);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowApply.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// 检查申请单输入是否合法
    /// </summary>
    private void ValidateInput()
    {
        string errorMsg = "";
        //if (lbSheetNO.Text.Trim() == string.Empty)
        //{
        //    errorMsg = "表单编号不能为空";
        //}
        //else 
        //if (lbBorrowCompany.Text.Trim() == string.Empty)
        //{
        //    errorMsg = "申请方不能为空";
        //}
        //else 
        if (ddlLendCompany.SelectedIndex == 0)
        {
            errorMsg = "请选择借出方";
        }
        //else if (lbApplicant.Text.Trim() == string.Empty)
        //{
        //    errorMsg = "申请人不能为空";
        //}
        else if (lbStatus.Text.Trim() == string.Empty)
        {
            errorMsg = "表单状态不能为空";
        }

        IList list = CurrentApply.DetailList;
        if (list == null || list.Count == 0)
            errorMsg = "没有申请借调任何的设备";

        if (errorMsg != "")
        {
           // EventMessage.EventWriteLog(Msg_Type.Error, errorMsg);
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 添加明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddUpdate_Click(object sender, EventArgs e)
    {
        errMsg.Text = "";
        if (!ValidateDetailInput())
            return;

        try
        {
            

            IList list = CurrentApply.DetailList;// (ArrayList)Session[DETAILSESSION];
            if (list == null)
                list = new ArrayList();

            if (CurrentAction == AddAction)
            {
                //获取用户输入
                BorrowApplyDetailInfo item = new BorrowApplyDetailInfo();
                item.EquipmentName = tbEquipmentName.Text.Trim();
                item.Model = tbModel.Text.Trim();
                item.Count = Convert.ToInt32(tbCount.Text.Trim());
                item.Unit = ddlUnit.SelectedItem.Text;
                item.ReturnDate = Convert.ToDateTime(tbReturnDate.Text.Trim());
                item.Reason = tbReason.Text.Trim();
                item.AddressID = long.Parse(Hidden_AddressID.Value);
                item.AddressName = TextBox_Address.Value.Trim();
                item.DetailLocation = TextBox_DetailLocation.Value.Trim();
                //检测列表中是否已有此项
                foreach (BorrowApplyDetailInfo it in list)
                {
                    if (it.EquipmentName == item.EquipmentName && it.Model == item.Model)
                    {
                        errMsg.Text = string.Format("错误：型号为{0}的物品{1}已存在列表中，不可重复添加", it.Model, it.EquipmentName);
                        return;
                    }
                }
                list.Insert(0, item);
            }
            else if (CurrentAction == EditAction)
            {
                
                int index = Convert.ToInt32(Hidden_EditItemIndex.Value);
                BorrowApplyDetailInfo targetItem = list[index] as BorrowApplyDetailInfo;
                for (int i = 0; i < list.Count; i++)
                {
                    BorrowApplyDetailInfo tempItem = list[i] as BorrowApplyDetailInfo;
                    if (i != index)
                    {
                        if (tempItem.EquipmentName == tbEquipmentName.Text.Trim() && tempItem.Model == tbModel.Text.Trim())
                        {
                            errMsg.Text = string.Format("错误：型号为{0}的物品{1}已存在列表中，不可修改", tempItem.Model, tempItem.EquipmentName);
                            return;
                        }
                    }
                }

                targetItem.EquipmentName = tbEquipmentName.Text.Trim();
                targetItem.Model = tbModel.Text.Trim();
                targetItem.Count = Convert.ToInt32(tbCount.Text.Trim());
                targetItem.Unit = ddlUnit.SelectedItem.Text;
                targetItem.ReturnDate = Convert.ToDateTime(tbReturnDate.Text.Trim());
                targetItem.Reason = tbReason.Text.Trim();
                targetItem.AddressID = long.Parse(Hidden_AddressID.Value);
                targetItem.AddressName = TextBox_Address.Value.Trim();
                targetItem.DetailLocation = TextBox_DetailLocation.Value.Trim();

                GridView_Detail.Columns[GridView_Detail.Columns.Count - 1].Visible = true;
                CurrentAction = AddAction;
                Button_AddUpdate.Text = "添加明细";
            }

            CurrentApply.DetailList = list;
            //Session[DETAILSESSION] = list;
            //AspNetPager1.CurrentPageIndex = 1;
            //AspNetPager1.RecordCount++;
            //Pager();
            //清空输入
            ClearDetailInput();
            FillData();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "原因：" + ex.Message,ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 校验添加明细时的输入是否合法
    /// </summary>
    private bool ValidateDetailInput()
    {
        string errorMsg = "";

        if (tbEquipmentName.Text.Trim() == string.Empty)
        {
            errorMsg = "物品名称不能为空";
        }
        else if (tbModel.Text.Trim() == string.Empty)
        {
            errorMsg = "规格型号不能为空";
        }
        else if (tbCount.Text.Trim() == string.Empty)
        {
            errorMsg = "物品数量不能为空";
        }
        else if (tbReturnDate.Text.Trim() == string.Empty)
        {
            errorMsg = "归还日期不能为空";
        }
        else if (string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
        {

            errorMsg = "必须选择使用地址";

        }
        if (string.IsNullOrEmpty(Hidden_AddressID.Value.Trim()))
        {

            errorMsg = "必须选择使用地址";

        }

        if (tbCount.Text.Trim() != string.Empty)
        {
            try
            {
                Convert.ToInt32(tbCount.Text.Trim());
            }
            catch
            {
                errorMsg = "物品数量只能为数字，请检查输入";
            }
        }

        if (tbReturnDate.Text.Trim() != string.Empty)
        {
            try
            {
                Convert.ToDateTime(tbReturnDate.Text.Trim());
            }
            catch
            {
                errorMsg = "不正确的归还日期，请检查输入";
            }
        }

       
        if (errorMsg != "")
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "输入有误：" + errorMsg);
            errMsg.Text = "输入有误：" + errorMsg;
            return false;
            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        return true;
    }
    /// <summary>
    /// 清空明细输入
    /// </summary>
    private void ClearDetailInput()
    {
        tbEquipmentName.Text = "";
        tbModel.Text = "";
        tbCount.Text = "";
        ddlUnit.SelectedIndex = 0;
        tbReturnDate.Text = "";
        tbReason.Text = "";
        TextBox_Address.Value = "";
        Hidden_AddressID.Value = "";
    }
    ///// <summary>
    ///// 结束编辑
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    //TabContainer1.ActiveTabIndex = 0;
    //    //TabContainer1.Tabs[1].Visible = false;
    //}

    //protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    //{
    //    //Pager();
    //}
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowNum = Convert.ToInt32(e.CommandArgument);
        IList list = CurrentApply.DetailList;

        if (e.CommandName == "del")
        {
            //删除
            if (list == null||list.Count==0) return;
            list.RemoveAt(rowNum);
            //BorrowApplyInfo apply = CurrentApply;
            //apply.DetailList = list;
            //CurrentApply = apply;
            ////list.RemoveAt((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize + rowNum);
            ////Session[DETAILSESSION] = list;
            ////Pager();
        }
        else if (e.CommandName == "view")
        {
            if (list == null || list.Count == 0)
                return;
            int index = rowNum;//= (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize + rowNum;
            BorrowApplyDetailInfo item = (BorrowApplyDetailInfo)list[index];
            tbEquipmentName.Text = item.EquipmentName;
            tbModel.Text = item.Model;
            tbCount.Text = item.Count.ToString();
            ddlUnit.SelectedValue = item.Unit;
            tbReturnDate.Text = item.ReturnDate.ToShortDateString();
            tbReason.Text = item.Reason;
            Hidden_AddressID.Value = item.AddressID.ToString();
            TextBox_Address.Value = item.AddressName;
            TextBox_DetailLocation.Value = item.DetailLocation;
            Hidden_EditItemIndex.Value = e.CommandArgument.ToString();
            CurrentAction = EditAction;
            GridView_Detail.Columns[GridView_Detail.Columns.Count - 1].Visible = false;

            Button_AddUpdate.Text = "更新明细";
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
