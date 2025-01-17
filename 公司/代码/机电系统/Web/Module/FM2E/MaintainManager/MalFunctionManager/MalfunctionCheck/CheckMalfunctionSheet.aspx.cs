﻿using System;
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
using FM2E.WorkflowLayer;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using System.Data.SqlClient;
using System.Configuration;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionCheck_CheckMalfunctionSheet : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private int viewOnly = (int)Common.sink("viewOnly", MethodType.Get, 10, 0, DataType.Int);  //查看模式
    private string returnUrl = (string)Common.sink("returnurl", MethodType.Get, 500, 0, DataType.Str);  //返回的url
    private string chedang = (string)Common.sink("chedang", MethodType.Get, 20, 0, DataType.Str);
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Department deptBll = new Department();
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    private readonly Address addressBll = new Address();
    private readonly EquipmentSystem systemBll = new EquipmentSystem();
    private readonly EquipmentCost ecBLL = new EquipmentCost();

    //private MalfunctionHandleStatus status = MalfunctionHandleStatus.Unknown;
    private string AnotherCheckNextUserName="";
    public List<EquipmentCostItems> EqCostItems
    {
        get
        {
            if (ViewState["EqCostItems"] == null)
            {
                return new List<EquipmentCostItems>();
            }
            return (List<EquipmentCostItems>)ViewState["EqCostItems"];
        }
        set
        {
            ViewState["EqCostItems"] = value;
        }
    }

    public EquipmentCostInfor EqCostInfor
    {
        get
        {
            if (ViewState["EqCostInfor"] == null)
            {
                return new EquipmentCostInfor();
            }
            return (EquipmentCostInfor)ViewState["EqCostInfor"];
        }
        set
        {
            ViewState["EqCostInfor"] = value;
        }
    }
    /// <summary>
    /// 存放当前正在查看的故障处理单
    /// </summary>
    private MalfunctionHandleInfo CurrentSheet
    {
        get
        {
            if (ViewState["malfunctionSheet"] == null)
                return new MalfunctionHandleInfo();

            return (MalfunctionHandleInfo)ViewState["malfunctionSheet"];
        }
        set
        {
            ViewState["malfunctionSheet"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        returnUrl = Server.HtmlDecode(returnUrl);
        //SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
        ButtonBind();
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        bool bDelete = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = bDelete;
        HeadMenuWebControls1.ButtonList[3].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.Print);
        btUndo.Visible = bDelete;
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 页面初始化
    /// </summary>
    private void InitialPage()
    {
        try
        {
            ListItem[] grade1 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);
            ListItem[] grade2 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);
            ListItem[] grade3 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);
            ListItem[] grade4 = EnumHelper.GetListItems(typeof(Grade), (int)Grade.Unknown);
            //By L 4-27 满意度调查屏蔽************************************************************************************
            /*
            cblEffect.Items.Clear();
            cblEffect.Items.AddRange(grade1);
            cblAttitude.Items.Clear();
            cblAttitude.Items.AddRange(grade2);
            cblRationality.Items.Clear();
            cblRationality.Items.AddRange(grade3);
            cblTechnicEvaluate.Items.Clear();
            cblTechnicEvaluate.Items.AddRange(grade4);
            */
            //By L 4-27 满意度调查屏蔽************************************************************************************

            tbCancelReason.Focus();
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败，原因：" + ex.Message);
        }
    }
    /// <summary>
    /// 菜单绑定
    /// </summary>
    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "RecordMalfunction.aspx?cmd=edit&id=" + id;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;

        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("javascript:{0}.click();", btUndoMode.ClientID);
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;

        if (CurrentSheet.Status != MalfunctionHandleStatus.Waiting4Accept&&chedang!="true")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            
        }
        else
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
            if (chedang == "true")
            {
                divUndo.Visible = true;
                tbCancelReason.Focus();
            }
        }

        if (!string.IsNullOrEmpty(returnUrl))
        {
            HeadMenuWebControls1.ButtonList[2].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[2].ButtonUrl = returnUrl;
            HeadMenuWebControls1.ButtonList[2].ButtonUrlType = UrlType.Href;
            HeadMenuWebControls1.ButtonList[4].ButtonVisible = false;
        }
        else
        {
            HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[4].ButtonVisible = true;
        }
        HeadMenuWebControls1.ButtonList[3].ButtonUrl = string.Format("window.open('MalfunctionSheetPrint.aspx?id={0}','故障单打印','width=1000,top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');", id);
        HeadMenuWebControls1.ButtonList[3].ButtonUrlType = UrlType.JavaScript;
        if (CurrentSheet.IsPrinted)
        {
            HeadMenuWebControls1.ButtonList[3].ButtonName = "打印(已打印)";
        }
        else
        {
            HeadMenuWebControls1.ButtonList[3].ButtonName = "打印";
        }
        if (viewOnly == 1)
        {
            HeadMenuWebControls1.Visible = false;
        }
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

            CurrentSheet = item;
            EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(item.SheetID);
           
            if (ecInfor != null)
            {
                EqCostInfor = ecInfor;
                EqCostItems = ecInfor.EqCostItems;

                FillCostItemsData(EqCostInfor);

            }

            CompanyInfo company = companyBll.GetCompany(item.CompanyID);
            if (company != null)
                lbCompany.Text = company.CompanyName;
            lbSheetNO.Text = item.SheetNO;
            lbDepartment.Text = item.DepartmentName;
            lbReporter.Text = item.Reporter;
            lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd HH:mm"); //时间显示至分钟，by L 4-23
            //lbAddressDetail.Text = item.AddressDetail;

            //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
            string separatorStr = "@First@";
            if (item.Editreason != null)
            {
                if (!item.Editreason.Contains(separatorStr))
                {
                    item.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
                }
            }
            else
            {
                item.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
            }
            string[] split = { separatorStr };
            string[] editreason = item.Editreason.Split(split, StringSplitOptions.None);
            string approvalrecord = editreason[1];
            string checkrecord = editreason[2];
            string[] aa = { "→" };
            string[] approvalrecordSplit = approvalrecord.Split(aa, StringSplitOptions.RemoveEmptyEntries);
            string[] checkrecordSplit = checkrecord.Split(aa, StringSplitOptions.RemoveEmptyEntries);
            if (approvalrecordSplit.Length > 1)
            {
                List<FM2E.Model.Maintain.ApprovalRecord> arList = new List<ApprovalRecord>();
                for (int i = 0; i < approvalrecordSplit.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = approvalrecordSplit[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 6)
                    {
                        FM2E.Model.Maintain.ApprovalRecord ar = new ApprovalRecord(arsplitsplit[0], arsplitsplit[2], arsplitsplit[1], arsplitsplit[3], arsplitsplit[4], arsplitsplit[5]);
                        arList.Add(ar);
                    }
                }
                Repeater1.DataSource = arList;
                Repeater1.DataBind();
            }

            if (checkrecordSplit.Length > 1)
            {
                List<FM2E.Model.Maintain.CheckRecord> crList = new List<CheckRecord>();
                for (int i = 0; i < checkrecordSplit.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] chsplitsplit = checkrecordSplit[i].Split(bb, StringSplitOptions.None);
                    if (chsplitsplit.Length == 5)
                    {
                        FM2E.Model.Maintain.CheckRecord cr = new CheckRecord(chsplitsplit[0], chsplitsplit[2], chsplitsplit[1], chsplitsplit[3], chsplitsplit[4]);
                        crList.Add(cr);
                    }
                }
                Repeater4Check.DataSource = crList;
                Repeater4Check.DataBind();
            }

            #region 附件绑定
            if (item.attachment != null)
            {
                if (!item.attachment.Contains(separatorStr))
                {
                    item.attachment += " " + separatorStr + " " ;  //附件名称+附件地址
                }
            }
            else
            {
                item.attachment += " " + separatorStr + " ";  //附件名称+附件地址
            }
            string[] editreason1 = item.attachment.Split(split, StringSplitOptions.None);
            if (item.attachment.Length > 0)
            {
                HyperLink_File.NavigateUrl = editreason1[1];
                HyperLink_File.Text = editreason1[0];
                HyperLink_File.Visible = true;
            }
            #endregion

            string eqmno = item.AddressDetail.Split('@')[0];
            if (eqmno != null && eqmno != "")
            {
                Equipment equipmentBll = new Equipment();
                EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);

                lbEqName.Text = equipmentitem.Name;
                lbEqSystem.Text = equipmentitem.SystemName;
                lbEqNo.Text = equipmentitem.EquipmentNO;

            }

            DepartmentType currentDeptType = deptBll.GetDepartment(item.MaintainDept).DepartmentType;
            if (currentDeptType == DepartmentType.MaintainTeam)                    //自维
            {
                //  [4/5/2012 L]MaintainPlanTime.Visible = false;
                MaintainPlanRankTitle.Visible = false;
                MaintainPlanRankContent.Visible = false;
            }
            if (currentDeptType == DepartmentType.MaintainTeamOther)               //外维
            {
                //  [4/5/2012 L]MaintainPlanTime.Visible = true;
                MaintainPlanRankTitle.Visible = true;
                MaintainPlanRankContent.Visible = true;
            }
            //********** Modification Finished 2011-11-28 **********************************************************************************************

            //repeatEquipments.DataSource = item.FaultyEquipments;
            //repeatEquipments.DataBind();

            AddressInfo address = addressBll.GetAddress(item.AddressID);
            if (address != null)
                lbAddress.Text = address.AddressFullName;

            lbDescription.Text = item.MalfunctionDescription;
            //if (string.IsNullOrEmpty(item.SystemID.Trim()))
            //    lbSystem.Text = "其它";
            //else
            //{
            //    EquipmentSystemInfo system = systemBll.GetSystem(item.SystemID);
            //    if (system != null)
            //        lbSystem.Text = system.SystemName;
            //}

            lbMalReason.Text = EnumHelper.GetDescription(item.SystemID);

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
            if (item.ReceiveDate != DateTime.MinValue)
            {
                lbReceiveDate.Text = item.ReceiveDate.ToString("yyyy-MM-dd HH:mm");
                lbActResponseTime.Text = item.ActualResponseTimeString;
                //  [4/5/2012 L]lbActFunRestoreTime.Text = item.ActualFunRestoreTimeString;
                lbActRepairTime.Text = item.ActualRepairTimeString;
            }

            //填充故障处理历史列表
            try
            {
                IList maintainHistory = malfunctionBll.GetMaintainHistory(item.SheetID);
                rptMaintainHistory.DataSource = maintainHistory;
                rptMaintainHistory.DataBind();
                IList staffList = new ArrayList();
                Hashtable ht = new Hashtable();
                foreach (MalfuncitonMaintainInfo detailitem in maintainHistory)
                {
                    if (detailitem.MaintainStaff != null)
                    {
                        foreach (MalfunctionMaintainStaffInfo staffitem in detailitem.MaintainStaff)
                        {
                            if (ht.Contains(staffitem.MaintenanceStaff))
                                continue;
                            staffList.Add(staffitem);
                            ht.Add(staffitem.MaintenanceStaff, staffitem);
                        }
                    }
                }
                lbMaintainStaffList.Text = "";
                if (staffList.Count == 0 || staffList == null)
                {
                    lbMaintainStaffList.Text = "暂无维修人员";
                }
                else if (staffList.Count > 0)
                {
                    for (int i = 0; i < staffList.Count; i++)
                    {
                        lbMaintainStaffList.Text += ((MalfunctionMaintainStaffInfo)staffList[i]).MaintenanceStaff + "  ";
                    }
                }

                for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
                {
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqName"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqNum"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqModel"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqUnit"))).Visible = false;

                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Visible = false; //备注可审批编辑

                    

                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqName"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqNum"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqModel"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqUnit"))).Visible = true;

                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqRemark"))).Visible = false; //备注可审批编辑

                    //((Label)(rpEquipmentItems.Items[ii].FindControl("lbReMark"))).Visible = false;
                    //((Label)(rpEquipmentItems.Items[ii].FindControl("tbReMark"))).Visible = true;



                }
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理历史失败，原因：" + ex.Message);
            }

            //故障处理满意度调查
            //By L 4-27 满意度调查屏蔽************************************************************************************
/*
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
*/
            //By L 4-27 满意度调查屏蔽************************************************************************************

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
            //撤单原因
            if (item.Status == MalfunctionHandleStatus.Canceled)
            {
                //如果已撤单则显示撤单原因
                lbCancelReason.Text = item.CancelReason;
                UserInfo cancelerInfo = userBll.GetUser(item.Canceler);
                if (cancelerInfo != null)
                    lbCanceler.Text = cancelerInfo.PersonName;

                trCancelReason.Visible = true;
            }
            else trCancelReason.Visible = false;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单内容失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }
    /// <summary>
    /// 撤消故障处理单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btUndo_Click(object sender, EventArgs e)
    {
        MalfunctionHandleInfo item = CurrentSheet;
        item.Status = MalfunctionHandleStatus.Canceled;
        item.Canceler = Common.Get_UserName;
        item.CancelReason = tbCancelReason.Text.Trim();
        item.UpdateTime = DateTime.Now;

        try
        {
            malfunctionBll.UpdateMalfunctionSheetBasicData(item);

            //**********Modified by Xue 2011-6-27****************************************************************************************************  撤单没有待办事项
            //string title = "您提交的报修单"+item.SheetNO+"被撤单";
            //string URL = "../MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id="+item.SheetID;

            //    WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL,
            //        0, null,item.Recorder);
            //**********Modification Finished 2011-6-27**********************************************************************************************

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "撤消故障处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("撤消故障处理单成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
    }
    /// <summary>
    /// 显示撤消框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btUndoMode_Click(object sender, EventArgs e)
    {
        divUndo.Visible = true;
        tbCancelReason.Focus();
    }

    /// <summary>
    /// 隐藏撤消框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btClose_Click(object sender, EventArgs e)
    {
        divUndo.Visible = false;
    }

    //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
    protected void btCheckPass_Click(object sender, EventArgs e)
    {
        bool finishFlag = false;
        bool anotherCheckFlag = false;
        bool finalFinishFlag = false;
        MalfunctionHandleInfo item = CurrentSheet;
        
        string nextUserName="";

        if (deptBll.GetDepartment(item.MaintainDept).DepartmentType == DepartmentType.MaintainTeam) //自维
        {
            item.Status = MalfunctionHandleStatus.Fixed;
            finishFlag = true;
        }
        if (deptBll.GetDepartment(item.MaintainDept).DepartmentType == DepartmentType.MaintainTeamOther)  //外维
        {
            item.Status = MalfunctionHandleStatus.Waiting4MoneyApproval;

            FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID);
            //int a = 1,b = 1;
            //if (a == b)
            //{
            //    WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);
            //    return;
            //}

            String company = item.MaintainDeptName;
            if (wii == null)           //新建工作流
            {
                Guid guid = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                finishFlag = true;
            }
            else
            {
                if (wii.CurrentStateName == "Wait4ElectDeptEngineerConfirm")
                {
                    anotherCheckFlag = true;
                    //AnotherCheckNextUserName = wii.NextUserName;

                    item.Status = MalfunctionHandleStatus.AnotherCheck;
                }
                else if (wii.CurrentStateName == "Wait4Register")
                {
                    WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);
                    Guid guid = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                }
                else if (wii.CurrentStateName == "Wait4DutyStationConfirm" || wii.CurrentStateName == "Wait4MaintenanceConfirm")
                {
                    string nUser = wii.DelegateUserName;
                    WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);
                    Guid guid = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                    if (!string.IsNullOrEmpty(wii.DelegateUserName))
                    {
                        WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                    }
                    else
                    {
                        WorkflowHelper.SetStateAndNextUserMachine3<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, nUser, out nextUserName);
                    }
                }
                else
                {

                    Guid guid = wii.InstanceID;
                    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName)[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                    finishFlag = true;
                    //finalFinishFlag = true;
                }

                //if (wii.CurrentStateName == "Wait4MaintenanceConfirm")
                //{
                //    item.Status = MalfunctionHandleStatus.AnotherCheck;
                //    String company = item.MaintainDeptName;

                //    Guid guid = wii.InstanceID;
                //    WorkflowHelper.SetStateAndNextUserMachine2<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName)[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                //}
                //else
                //{
                //    String company = item.MaintainDeptName;

                //    Guid guid = wii.InstanceID;
                //    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName)[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                //    finishFlag = true;
                //    //finalFinishFlag = true;
                //}
                
            }
            
        }

        item.UpdateTime = DateTime.Now;

        #region 添加处理记录
        string separatorStr = "@First@";
        if (item.Editreason != null)
        {
            if (!item.Editreason.Contains(separatorStr))
            {
                item.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
            }
        }
        else
        {
            item.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
        }
        string[] split = { separatorStr };
        string[] editreason = item.Editreason.Split(split, StringSplitOptions.None);
        FM2E.Model.System.UserInfo userinfor = (new FM2E.BLL.System.User().GetUser(WebUtility.Common.Get_UserName));
        if (tbCheckInfor.Text.Trim() == "")
        {
            tbCheckInfor.Text = "验收通过";
        }
        editreason[2] += "→" + userinfor.PersonName + "#" + userinfor.PositionName + "#" + userinfor.DepartmentName + "#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "#" + tbCheckInfor.Text.Trim();
        item.Editreason = editreason[0] + separatorStr + editreason[1] + separatorStr + editreason[2];
        #endregion

        try
        {
            malfunctionBll.UpdateMalfunctionSheetBasicData(item);

            if (finishFlag )  //完成并提交至审批
            {
                string title = "您有新的故障处理单要审批，故障处理单号为：" + item.SheetNO;
                string URL = "../MaintainManager/MalFunctionManager/MalfunctionApproval/MalfunctionApprovalList.aspx";
                string[] receiver = { nextUserName };
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
            }
            if (anotherCheckFlag)
            {


                User userBll = new User();

                int tempRoleID = Convert.ToInt32(ConfigurationManager.AppSettings["StationChecker"]);
                IList roleUsers = userBll.GetUsers(tempRoleID);
                //strStationCheckers = new string[roleUsers.Count];
                List<string> tempStationCheckers = new List<string>();
                for (int k = 0; k < roleUsers.Count; k++)
                {
                    UserInfo temUserInfo = null;
                    temUserInfo = userBll.GetUser(((UserRoleInfo)roleUsers[k]).UserName);
                    if (temUserInfo.DepartmentID == item.DepartmentID)
                    {
                        tempStationCheckers.Add(temUserInfo.UserName) ;
                    }
                }
                string[] strStationCheckers = tempStationCheckers.ToArray();

                string title = "您有新的故障处理单要验收确认，故障处理单号为：" + item.SheetNO;
                string URL = "../MaintainManager/MalFunctionManager/MalfunctionStationCheck/MalfunctionStationCheckList.aspx";
                string[] receiver = {AnotherCheckNextUserName};
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, strStationCheckers);
            }
            if (finalFinishFlag)  //完成并提交至审批
            {


                //User userBll = new User();

                //int tempRoleID = Convert.ToInt32(ConfigurationManager.AppSettings["StationChecker"]);
                //IList roleUsers = userBll.GetUsers(tempRoleID);
                //string[] strStationCheckers = null;
                //strStationCheckers = new string[roleUsers.Count];
                //for (int k = 0; k < roleUsers.Count; k++)
                //{
                //    UserInfo temUserInfo = null;
                //    temUserInfo = userBll.GetUser(((UserInfo)roleUsers[k]).UserName);
                //    if (temUserInfo.DepartmentID==item.RecordDept)
                //    {
                //        strStationCheckers[k] = temUserInfo.UserName;
                //    }
                //}


                string title = "您有新的故障处理单要验收，故障处理单号为：" + item.SheetNO;
                string URL = "../MaintainManager/MalFunctionManager/MalfunctionStationCheck/MalfunctionStationCheckList.aspx";
                string[] receiver = { nextUserName };
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "验收故障处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        //*********************************************************************************************************
        int i = 0;
        i = Convert.ToInt32(ExecuteScalar("select count(ID) from FM2E_PendingOrder where title like '%已登记完成%" + item.SheetNO + "%' and Address like '%" + Common.Get_UserName + "%'"));
        if (i == 1)
        {
            ExecuteNonQuery("Update  FM2E_PendingOrderReceiver set HasRead = 1 where ID =(select ID from FM2E_PendingOrder where title like '%已登记完成%" + item.SheetNO + "%' and Address like '%" + Common.Get_UserName + "%')");
        }
        int j = 0;
        j = Convert.ToInt32(ExecuteScalar("select count(ID) from FM2E_PendingOrder where title like '%处理单要验收%" + item.SheetNO + "%' and Address like '%" + Common.Get_UserName + "%'"));
        if (j== 1)
        {
            ExecuteNonQuery("Update  FM2E_PendingOrderReceiver set HasRead = 1 where ID =(select ID from FM2E_PendingOrder where title like '%处理单要验收%" + item.SheetNO + "%' and Address like '%" + Common.Get_UserName + "%')");
        }
        //*********************************************************************************************************



        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("验收故障处理单成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionCheckList.aspx"), UrlType.Href, "");
    }
    //  [5/21/2013 Tvk]
    protected void btNotice_Click(object sender, EventArgs e)
    {
        MalfunctionHandleInfo item = CurrentSheet;
        item.RecordDept = item.DepartmentID;
        try
        {

        malfunctionBll.UpdateMalfunctionSheetBasicData(item);
        User userBll = new User();

     
        IList DeptUsers = userBll.GetUsersByDepartmentId(item.DepartmentID);
        //strStationCheckers = new string[roleUsers.Count];
        List<string> tempStationCheckers = new List<string>();
        for (int k = 0; k < DeptUsers.Count; k++)
        {
           
            tempStationCheckers.Add(((UserInfo)DeptUsers[k]).UserName);
            
        }
        string[] strStationCheckers = tempStationCheckers.ToArray();

        string title = "您有新的故障处理单要验收确认，故障处理单号为：" + item.SheetNO;
        string URL = "../MaintainManager/MalFunctionManager/MalfunctionCheck/MalfunctionCheckList.aspx";        
        WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, strStationCheckers);
        }    
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "故障单转交验收失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("故障单转交验收成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionCheckList.aspx"), UrlType.Href, "");
    
    }
    //  [5/21/2013 Tvk]
    protected void btCheckFailed_Click(object sender, EventArgs e)
    {
        bool finishFlag = false;
        //bool anotherCheckFlag = false;
        bool selfSecondFlag = false;
        bool otherFlag = false;
        MalfunctionHandleInfo item = CurrentSheet;
        item.Status = MalfunctionHandleStatus.ReturnModify;
        item.UpdateTime = DateTime.Now;

        if (deptBll.GetDepartment(item.MaintainDept).DepartmentType == DepartmentType.MaintainTeam) //自维
        {
            try
            {
                item.RecordDept = Convert.ToInt64(FM2E.BLL.System.ConfigItems.SelfMaintianRecordDeptID);
                item.MaintainDept = Convert.ToInt64(FM2E.BLL.System.ConfigItems.SelfMaintianMaintainDeptID);
                item.Receiver = FM2E.BLL.System.ConfigItems.SelfMaintianReceiverName;
                selfSecondFlag = true;
            }
            catch (Exception exx)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "部门ID配置转换失败", exx, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else
        {
            otherFlag = true;
        }



        #region 添加处理记录
        string separatorStr = "@First@";
        if (item.Editreason != null)
        {
            if (!item.Editreason.Contains(separatorStr))
            {
                item.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
            }
        }
        else
        {
            item.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
        }
        string[] split = { separatorStr };
        string[] editreason = item.Editreason.Split(split, StringSplitOptions.None);
        FM2E.Model.System.UserInfo userinfor = (new FM2E.BLL.System.User().GetUser(WebUtility.Common.Get_UserName));
        if (tbCheckInfor.Text.Trim() == "")
        {
            tbCheckInfor.Text = "验收不通过";
        }
        editreason[2] += "→" + userinfor.PersonName + "#" + userinfor.PositionName + "#" + userinfor.DepartmentName + "#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "#" + tbCheckInfor.Text.Trim();
        item.Editreason = editreason[0] + separatorStr + editreason[1] + separatorStr + editreason[2];
        #endregion

        try
        {
            String company = item.MaintainDeptName;
            string nextUserName = "";
            FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID);
            Guid guid = wii.InstanceID;
            WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(
                guid,
                WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName)[1].Name,
                SGS_MalFunctionWorkflow.WorkflowName, company,
                out nextUserName);

            malfunctionBll.UpdateMalfunctionSheetBasicData(item);



            finishFlag = true;

            if (finishFlag)  //不通过返回修改
            {
                if (otherFlag)
                {
                    string title = "故障处理单验收不通过，请重新进行登记，故障处理单号为：" + item.SheetNO;
                    string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionSheets.aspx";
                    string[] receiver = { item.Receiver };
                    WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
                }
                if (selfSecondFlag)    // 自维
                {
                    string title = "故障处理单一级维修未能修复，请在‘我受理的故障单’中重新进行二次维修登记，故障处理单号为：" + item.SheetNO;
                    string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionSheets.aspx";
                    string[] receiver = { item.Receiver };
                    WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
                }
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "验收故障处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("验收故障处理单成功,该单已返回修复。故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionCheckList.aspx"), UrlType.Href, "");
    }
    //********** Modification Finished 2011-11-28 **********************************************************************************************
    private void FillCostItemsData(EquipmentCostInfor item)
    {
        rpEquipmentItems.DataSource = item.EqCostItems;
        rpEquipmentItems.DataBind();
    }

    public int ExecuteNonQuery(string Sql, params System.Data.SqlClient.SqlParameter[] parameters)
    {
        String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = Sql; //清除就数据
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                return cmd.ExecuteNonQuery();
            }
        }
    }
    public static object ExecuteScalar(string Sql, params System.Data.SqlClient.SqlParameter[] parameters)
    {
        String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = Sql; //清除就数据
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                return cmd.ExecuteScalar();
            }
        }
    }
}
