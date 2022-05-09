using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Maintain;
using WebUtility.Components;
using FM2E.Model.Maintain;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.BLL.System;
using FM2E.Model.System;
using FM2E.BLL.Utils;
using System.Collections;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionMaintain_MalfunctionHandleFinished : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private string from = (string)Common.sink("from", MethodType.Get, 20, 1, DataType.Str);
    private int viewOnly = (int)Common.sink("viewOnly", MethodType.Get, 10, 0, DataType.Int);  //查看模式
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Department deptBll = new Department();
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    private readonly Address addressBll = new Address();
    private readonly EquipmentSystem systemBll = new EquipmentSystem();
    private readonly Equipment equipmentBll = new Equipment();

    /// <summary>
    /// 当前正在编辑的故障处理单对象
    /// </summary>
    public MalfunctionHandleInfo CurrentMalfunctionSheet
    {
        get
        {
            if (ViewState["MalfunctionSheet"] == null)
                return new MalfunctionHandleInfo();
            return (MalfunctionHandleInfo)ViewState["MalfunctionSheet"];
        }
        set
        {
            ViewState["MalfunctionSheet"] = value;
        }
    }
    /// <summary>
    /// 已维修的设备列表
    /// </summary>
    private ArrayList MaintainedEquipments
    {
        get
        {
            if (Session["MaintainedEquipments"] == null)
                return new ArrayList();
            return (ArrayList)Session["MaintainedEquipments"];
        }
        set
        {
            Session["MaintainedEquipments"] = value;
        }
    }
    /// <summary>
    /// 维修总费用
    /// </summary>
    private decimal TotalMaintainFee
    {
        get
        {
            if (ViewState["TotalMaintainFee"] == null)
                return 0;
            return (decimal)ViewState["TotalMaintainFee"];
        }
        set
        {
            ViewState["TotalMaintainFee"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
        BindButton();
    }
    private void InitialPage()
    {
        ListItem[] repairSituations = EnumHelper.GetListItemsEx(typeof(RepairSituation), (int)RepairSituation.CompletelyFixed, (int)RepairSituation.Unknown);
        ListItem[] maintainStatuses = EnumHelper.GetListItemsEx(typeof(MaintainedEquipmentStatus), (int)MaintainedEquipmentStatus.Fixed, (int)MaintainedEquipmentStatus.Unknown);

        rblRepairSituation.Items.Clear();
        rblRepairSituation.Items.AddRange(repairSituations);

        ddlStatus.Items.Clear();
        ddlStatus.Items.AddRange(maintainStatuses);

        //清除session
        Session.Remove("MaintainedEquipments");
        btSave.Attributes.Add("onclick", "javascript:return checkForm(document.forms[0],true)&&confirm('未添加任何经过维修的设备，确认提交？')");

        ListItem[] grade1 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);
        ListItem[] grade2 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);
        ListItem[] grade3 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);
        ListItem[] grade4 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);

        cblEffect.Items.Clear();
        cblEffect.Items.AddRange(grade1);
        cblAttitude.Items.Clear();
        cblAttitude.Items.AddRange(grade2);
        cblRationality.Items.Clear();
        cblRationality.Items.AddRange(grade3);
        cblTechnicEvaluate.Items.Clear();
        cblTechnicEvaluate.Items.AddRange(grade4);

        tbMaintenanceDetail.Text = "维修情况追加";

    }
    private void BindButton()
    {
        if (from.Trim() == "1")
        {
            //HeadMenuWebControls1.ButtonList[0].ButtonUrl = "MalfunctionList.aspx";
            //HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        }
        else
        {
            //HeadMenuWebControls1.ButtonList[0].ButtonUrl = "MyMalfunctionSheets.aspx";
            // HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        }
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "history.go(-1);";
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.JavaScript;
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            MalfunctionHandleInfo item = malfunctionBll.GetMalfunctionSheet(id);
            if (item == null)
                return;

            CurrentMalfunctionSheet = item;
            if (item.Status != MalfunctionHandleStatus.Waiting4Accept)
            {
                //已受理
                tableRepairRecord.Visible = true;
                btSave.Visible = true;
                btAccept.Visible = false;
            }
            else
            {
                tableRepairRecord.Visible = false;
                btSave.Visible = false;
                btAccept.Visible = true;
            }

            CompanyInfo company = companyBll.GetCompany(item.CompanyID);
            if (company != null)
                lbCompany.Text = company.CompanyName;
            lbSheetNO.Text = item.SheetNO;
            lbDepartment.Text = item.DepartmentName;
            lbReporter.Text = item.Reporter;
            lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd HH:mm");//时间显示至分钟，by L 4-23
            lbAddressDetail.Text = item.AddressDetail;

            repeatEquipments.DataSource = item.FaultyEquipments;
            repeatEquipments.DataBind();

            AddressInfo address = addressBll.GetAddress(item.AddressID);
            if (address != null)
                lbAddress.Text = address.AddressFullName;

            lbDescription.Text = item.MalfunctionDescription;
            if (string.IsNullOrEmpty(item.SystemID.ToString()))
                lbSystem.Text = "未知";
            else
            {
                lbSystem.Text = EnumHelper.GetDescription(item.SystemID);
            }

            lbMaintainTeam.Text = item.MaintainDeptName;

            lbMalfunctionRank.Text = EnumHelper.GetDescription(item.MalfunctionRank);
            //  [4/5/2012 L]lbResponseTime.Text = item.ResponseTime + EnumHelper.GetDescription(item.ResponseUnit);
            //  [4/5/2012 L]lbFunRestoreTime.Text = item.FunRestoreTime + EnumHelper.GetDescription(item.FunRestoreUnit);
            //  [4/5/2012 L]lbRepairTime.Text = item.RepairTime + EnumHelper.GetDescription(item.RepairUnit);
            lbRecorder.Text = item.RecorderName;
            //DepartmentInfo recordDept = deptBll.GetDepartment(item.RecordDept);
            //if (recordDept != null)
            //    lbRecordDept.Text = recordDept.Name;
            lbRecordDept.Text = item.RecordDeptName;

            lbStatus.Text = EnumHelper.GetDescription(item.Status);
            lbUpdateTime.Text = item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

            lbMaintainTeamx.Text = item.MaintainDeptName;
            UserInfo receiverInfo = userBll.GetUser(item.Receiver);
            if (receiverInfo != null)
            {
                lbReceiver.Text = receiverInfo.PersonName;
            }
            lbReceiveDate.Text = item.ReceiveDate.ToString("yyyy-MM-dd HH:mm");
            lbMaintainDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            rblRepairSituation.Visible = false;

            gvMaintainedEquipments.DataSource = new ArrayList();
            gvMaintainedEquipments.DataBind();

            if (item.ReceiveDate != DateTime.MinValue)
            {
                lbReceiveDate.Text = item.ReceiveDate.ToString("yyyy-MM-dd HH:mm");
                lbActResponseTime.Text = item.ActualResponseTimeString;
                //  [4/5/2012 L]lbActFunRestoreTime.Text = item.ActualFunRestoreTimeString;
                lbActRepairTime.Text = item.ActualRepairTimeString;
            }
            Label1.Text = item.MaintainDeptName; //维修单位
            if (receiverInfo != null)
            {
                Label2.Text = receiverInfo.PersonName;  //受理人
            }
            Label3.Text = item.ReceiveDate.ToString("yyyy-MM-dd HH:mm");  //受理时间

            //填充故障处理历史列表
            try
            {
                IList maintainHistory = malfunctionBll.GetMaintainHistory(item.SheetID);
                rptMaintainHistory.DataSource = maintainHistory;
                rptMaintainHistory.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理历史失败，原因：" + ex.Message);
            }

            //填充故障处理历史列表
            try
            {
                IList maintainHistory = malfunctionBll.GetMaintainHistory(item.SheetID);
                rptMaintainHistory1.DataSource = maintainHistory;
                rptMaintainHistory1.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理历史失败，原因：" + ex.Message);
            }

            //故障处理满意度调查
            if (item.Status == MalfunctionHandleStatus.Finished)
            {
                //已经完成故障处理满意度调查
                cblEffect.SelectedValue = ((int)item.Effect).ToString();
                cblAttitude.SelectedValue = ((int)item.Attitude).ToString();
                cblRationality.SelectedValue = ((int)item.Rationality).ToString();
                cblTechnicEvaluate.SelectedValue = ((int)item.TechnicEvaluate).ToString();

                lbFeeBackOpinion.Text = item.Feeback;
                cbIsResponseInTime.Checked = item.IsResponseInTime;
                cbIsFunRestoreInTime.Checked = item.IsFunRestoreInTime;
                cbIsRepairInTime.Checked = item.IsRepairInTime;
            }
            cblEffect.Enabled = false;
            cblAttitude.Enabled = false;
            cblRationality.Enabled = false;
            cblTechnicEvaluate.Enabled = false;
            cbIsResponseInTime.Enabled = false;
            cbIsFunRestoreInTime.Enabled = false;
            cbIsRepairInTime.Enabled = false;

            //获取修改历史
            if (viewOnly != 1)
            {
                try
                {
                    IList modifyHistory = malfunctionBll.GetModifyHistory(id);
                    if (modifyHistory.Count > 0)
                        rptModifyHistory.Visible = true;
                    rptModifyHistory.DataSource = modifyHistory;
                    rptModifyHistory.DataBind();
                }
                catch (Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "获取故障单修改历史失败，原因：" + ex.Message);
                }
            }

            //检查进入当前页面的人员的部门信息是否与维修单上的维修单位一致，不一致的话，需要给出警告
            if (UserData.CurrentUserData.DepartmentID == item.MaintainDept)
            {
                btAccept.Attributes.Add("onclick", "javascript:return confirm('确认要受理此故障吗?');");
            }
            else
            {
                btAccept.Attributes.Add("onclick", string.Format("javascript:return confirm('警告：这是 \"{0}\" 的故障处理单，确定要受理吗?');", item.MaintainDeptName));
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单内容失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 受理故障
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAccept_Click(object sender, EventArgs e)
    {
        try
        {
            MalfunctionHandleInfo model = CurrentMalfunctionSheet;
            model.Receiver = Common.Get_UserName;
            model.ActualResponseTime = (int)Math.Ceiling((DateTime.Now - model.ReportDate).TotalSeconds / 60);
            model.UpdateTime = DateTime.Now;
            model.ReceiveDate = DateTime.Now;
            model.Status = MalfunctionHandleStatus.Accepted;
            model.MaintainDept = UserData.CurrentUserData.DepartmentID;
            malfunctionBll.UpdateMalfunctionSheetBasicData(model);

            FillData();
            //lbReceiver.Text = UserData.CurrentUserData.PersonName;
            //lbMaintainTeam.Text=lbMaintainTeamx.Text=

            tableRepairRecord.Visible = true;
            btSave.Visible = true;
            btAccept.Visible = false;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "受理故障失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 保存维修信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            MalfunctionHandleInfo model = CurrentMalfunctionSheet;
            MalfuncitonMaintainInfo maintainInfo = new MalfuncitonMaintainInfo();

            RepairSituation repairSituation = RepairSituation.CompletelyFixed;
            if (repairSituation == RepairSituation.CompletelyFixed)
            {
                //完全修复
                model.ActualRepairTime = (int)Math.Ceiling((DateTime.Now - model.ReportDate).TotalSeconds / 60);
                model.Status = MalfunctionHandleStatus.Finished;
                model.IsDelivered = false;
            }
            

            maintainInfo.MaintainedEquipments = MaintainedEquipments;
            maintainInfo.MaintenanceDetail = tbMaintenanceDetail.Text.Trim();
            maintainInfo.MaintenanceStaff = Common.Get_UserName;
            maintainInfo.RepairSituation = repairSituation;
            maintainInfo.SheetID = id;
            maintainInfo.TotalFee = TotalMaintainFee;
            maintainInfo.IsDelivered = false;
            maintainInfo.UpdateTime = DateTime.Now;

            malfunctionBll.AddMaintainRecord(model, maintainInfo);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加维修情况失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        if (from.Trim() == "1")
        {
            Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("MalfunctionList.aspx"))));
        }
        else
        {
            Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("MalfunctionSheets.aspx"))));
        }
        //if (from.Trim() == "1")
        //    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加维修情况成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
        //else
        //    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加维修情况成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MyMalfunctionSheets.aspx"), UrlType.Href, "");
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btCancel_Click(object sender, EventArgs e)
    {
        if (from.Trim() == "1")
        {
            Response.Redirect("MalfunctionList.aspx");
        }
        else
        {
            Response.Redirect("MalfunctionSheets.aspx");
        }
    }

    /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalFee = 0;//每次postback都会自动初始化
    protected void gvMaintainedEquipments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            MaintainedEquipmentInfo item = e.Row.DataItem as MaintainedEquipmentInfo;
            totalFee += item.MaintainFee;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = System.Drawing.Color.YellowGreen;
            e.Row.Font.Bold = true;
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            for (int i = 1; i <= 4; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label lbTotal = e.Row.FindControl("lbTotalFee") as Label;
            if (lbTotal != null)
            {
                TotalMaintainFee = totalFee;
                lbTotal.Text = totalFee.ToString("#,0.##");
            }

        }
    }

    /// <summary>
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaintainedEquipments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = gvMaintainedEquipments.Rows[Convert.ToInt32(e.CommandArgument)];
        int rowNum = Convert.ToInt32(e.CommandArgument);

        if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        {
            ScriptManager1.AddHistoryPoint("MaintainedEquipments", "MaintainedEquipment");
        }

        if (e.CommandName == "del")
        {
            //删除
            ArrayList list = MaintainedEquipments;
            if (list == null || list.Count == 0)
                return;
            list.RemoveAt(rowNum);
            BindDataToGridView();
        }
    }

    /// <summary>
    ///添加设备到已维修列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSaveEquipment_Click(object sender, EventArgs e)
    {
        try
        {
            if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
            {
                ScriptManager1.AddHistoryPoint("MaintainedEquipments", "MaintainedEquipment");
            }

            ArrayList list = MaintainedEquipments;

            //if (tbEquipmentNO.Text.Trim() == "")
            //{
            //    lbMsg.Text = "错误：设备条形码不能为空";
            //    return;
            //}

            MaintainedEquipmentInfo item = new MaintainedEquipmentInfo();
            item.EquipmentNO = tbEquipmentNO.Text.Trim();
            item.EquipmentName = tbEquipmentName.Text.Trim();
            item.SheetID = id;
            item.Remark = tbRemark.Text.Trim();
            item.MaintainResult = (MaintainedEquipmentStatus)Convert.ToInt32(ddlStatus.SelectedValue);
            item.Model = tbModel.Text.Trim();
            if (!string.IsNullOrEmpty(tbMaintainTimes.Text.Trim()))
                item.MaintainTimes = Convert.ToInt32(tbMaintainTimes.Text.Trim());
            if (tbMaintainFee.Text.Trim() == "")
                item.MaintainFee = 0;
            else
            {
                try
                {
                    item.MaintainFee = Convert.ToDecimal(tbMaintainFee.Text.Trim());
                }
                catch
                {
                    lbMsg.Text = "错误：维修费用输入格式错误";
                    return;
                }
            }
            item.MaintainDate = DateTime.Now;

            foreach (MaintainedEquipmentInfo it in list)
            {
                if (!string.IsNullOrEmpty(item.EquipmentNO) && it.EquipmentNO == item.EquipmentNO)
                {
                    lbMsg.Text = string.Format("错误：条形码为{0}的设备已存在列表中，不可重复添加", it.EquipmentNO);
                    return;
                }
            }
            list.Insert(0, item);
            MaintainedEquipments = list;

            tbEquipmentNO.Text = "";
            tbEquipmentName.Text = "";
            tbModel.Text = "";
            tbMaintainTimes.Text = "";
            ddlStatus.SelectedValue = ((int)MaintainedEquipmentStatus.Fixed).ToString();
            tbRemark.Text = "";
            tbMaintainFee.Text = "0";
            btSaveEquipment.Disabled = false;
            updatePanelAddEquipment.Update();

            ModalPopupExtender_AddEquipment.Hide();
            BindDataToGridView();

        }
        catch (Exception ex)
        {
            lbMsg.Text = "错误：添加已维修设备失败，请检查输入是否正确";
            EventMessage.EventWriteLog(Msg_Type.Error, lbMsg.Text + "(" + ex.Message + ")");
        }
    }

    /// <summary>
    /// 为gvMaintainedEquipments进行数据绑定
    /// </summary>
    private void BindDataToGridView()
    {
        btSave.Attributes.Remove("onclick");
        if (MaintainedEquipments.Count <= 0)
        {
            btSave.Attributes.Add("onclick", "javascript:return checkForm(document.forms[0],true)&&confirm('未添加任何经过维修的设备，确认提交？')");
        }
        else btSave.Attributes.Add("onclick", "javascript:return checkForm(document.forms[0],true)&&confirm('是否已通知使用部门维修结果？')");
        gvMaintainedEquipments.DataSource = MaintainedEquipments;
        gvMaintainedEquipments.DataBind();
    }

    /// <summary>
    /// 条形码输入框输入改变时触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbEquipmentNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
            {
                ScriptManager1.AddHistoryPoint("MaintainedEquipments", "MaintainedEquipment");
            }

            string equipmentNO = tbEquipmentNO.Text.Trim();
            if (equipmentNO == "")
            {
                tbEquipmentName.Text = "";
                tbModel.Text = "";
                tbMaintainTimes.Text = "";
                btSaveEquipment.Disabled = false;
                return;
            }

            tbEquipmentName.Text = "";
            tbModel.Text = "";

            EquipmentInfoFacade item = equipmentBll.GetEquipmentBYNO(equipmentNO);
            if (item != null)
            {
                tbEquipmentName.Text = item.Name;
                tbModel.Text = item.Model;
                tbMaintainTimes.Text = item.MaintenanceTimes.ToString();
                btSaveEquipment.Disabled = false;
            }
            else
            {
                tbEquipmentName.Text = "找不到相应的设备";
                tbModel.Text = "找不到相应的设备";
                tbMaintainTimes.Text = "找不到相应的设备";
                btSaveEquipment.Disabled = true;
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 浏览器返回的时候
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ScriptManager1_Navigate(object sender, HistoryEventArgs e)
    {
        BindDataToGridView();
    }
}
