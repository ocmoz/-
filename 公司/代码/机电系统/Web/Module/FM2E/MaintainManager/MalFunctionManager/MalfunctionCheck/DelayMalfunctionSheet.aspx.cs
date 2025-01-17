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
using FM2E.Model.Utils;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionCheck_DelayMalfunctionSheet : System.Web.UI.Page
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
    //private string AnotherCheckNextUserName="";
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
        //bool bDelete = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        //HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        //HeadMenuWebControls1.ButtonList[1].ButtonVisible = bDelete;
        //HeadMenuWebControls1.ButtonList[3].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.Print);
        //btUndo.Visible = bDelete;
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
            string cancelreason = editreason[0];
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


            string eqmno = item.AddressDetail.Split('@')[0];
            if (eqmno != null && eqmno != "")
            {
                Equipment equipmentBll = new Equipment();
                EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);

                lbEqName.Text = equipmentitem.Name;
                lbEqSystem.Text = equipmentitem.SystemName;
                lbEqNo.Text = equipmentitem.EquipmentNO;

            }
            item.FirstDelayApprove = 1;
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
            //  [5/19/2013 Tvk]
            //if (item.Canceler!=null && item.Canceler!="")
            //{
            //lbApplyCancelName.Text = userBll.GetUser(item.Canceler).PersonName;
            //}
            //lbApplyCancelReason.Text = item.CancelReason;

            //  [5/19/2013 Tvk]
            lbMaintainTeam.Text = item.MaintainDeptName;
            lbDelayApplyTime.Text = item.DelayApplyTime.ToString("yyyy-MM-dd HH:mm");
            lbApplyDelayReason.Text = cancelreason;
            lbDelayForTime.Text = item.DelayForTime.ToString() + EnumHelper.GetDescription(item.DelayForUnit);
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
            TB_RepairedTime.Text = item.DelayForTime.ToString();
            
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
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "撤消故障处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("撤消故障处理单成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("DelayMalfunctionList.aspx"), UrlType.Href, "");
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
        MalfunctionHandleInfo item = CurrentSheet;
        item.FirstDelayApprove = 1;
        item.FirstDelayRemark = tbCheckInfor.Text.Trim();
        item.FirstApproveTime = DateTime.Now;
        item.FirstApproveName = Common.Get_UserName;
        item.FirstConsultTime = item.DelayForTime;
        item.FirstConsultUnit = item.DelayForUnit;
        item.UpdateTime = DateTime.Now;

        DepartmentType currentDeptType = deptBll.GetDepartment(item.MaintainDept).DepartmentType;
        if (currentDeptType == DepartmentType.MaintainTeam) //自维
        {
            item.Status = MalfunctionHandleStatus.Wait4EngineerCheck;
        }
        else
        {
            item.Status = MalfunctionHandleStatus.Waiting4Check;
        }

        try
        {
            malfunctionBll.UpdateMalfunctionSheetBasicData(item);


            string title = "您申请的故障处理单号为：" + item.SheetNO + "已同意延迟！";
            string URL = "../MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id=" + item.SheetID;
            string[] receiver = { item.Receiver };
            WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("审批延迟申请单成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("DelayMalfunctionList.aspx"), UrlType.Href, "");
    }
    protected void btCheckFailed_Click(object sender, EventArgs e)
    {

        MalfunctionHandleInfo item = CurrentSheet;
        item.FirstDelayApprove = 2;
        item.FirstDelayRemark = tbCheckInfor.Text.Trim();
        item.FirstApproveTime = DateTime.Now;
        item.FirstApproveName = Common.Get_UserName;
        item.FirstConsultTime = item.RepairTime;
        item.FirstConsultUnit = item.RepairUnit;
        item.UpdateTime = DateTime.Now;
        DepartmentType currentDeptType = deptBll.GetDepartment(item.MaintainDept).DepartmentType;
        if (currentDeptType == DepartmentType.MaintainTeam) //自维
        {
            item.Status = MalfunctionHandleStatus.Wait4EngineerCheck;
        }
        else
        {
            item.Status = MalfunctionHandleStatus.Waiting4Check;
        }
        try
        {
            malfunctionBll.UpdateMalfunctionSheetBasicData(item);
            string title = "您申请的故障处理单号为：" + item.SheetNO + "不同意延迟！";
            string URL = "../MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id=" + item.SheetID;
            string[] receiver = { item.Receiver };
            WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");

        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("审批延迟故障处理单成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("CancelMalfunctionList.aspx"), UrlType.Href, "");

                
  }

    protected void btCheckPassApprove_Click(object sender, EventArgs e)
    {

        MalfunctionHandleInfo item = CurrentSheet;
        item.FirstDelayApprove = 4;
        item.FirstDelayRemark = tbCheckInfor.Text.Trim();
        item.FirstApproveTime = DateTime.Now;
        item.FirstApproveName = Common.Get_UserName;
        item.FirstConsultTime = Convert.ToInt32(TB_RepairedTime.Text.Trim());
        item.FirstConsultUnit = (TimeUnits)Convert.ToInt32(DDL_RepairedUnit.SelectedValue);
        item.UpdateTime = DateTime.Now;

        User userBll = new User();

        int tempRoleID = Convert.ToInt32(ConfigurationManager.AppSettings["DelayFinalApprove"]);
        IList roleUsers = userBll.GetUsers(tempRoleID);
        List<string> tempStationCheckers = new List<string>();
        for (int k = 0; k < roleUsers.Count; k++)
        {
            tempStationCheckers.Add(((UserRoleInfo)roleUsers[k]).UserName);

        }
        string[] strStationCheckers = tempStationCheckers.ToArray();
      
        try
        {
            malfunctionBll.UpdateMalfunctionSheetBasicData(item);
            
            string title = "你的新的故障单延迟申请需要审批，故障单号为：" + item.SheetNO;
            string URL = "../MaintainManager/MalFunctionManager/MalfunctionCheck/DelayFinalMalfunctionList.aspx";

            WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, strStationCheckers);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("审批延迟申请单成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("DelayMalfunctionList.aspx"), UrlType.Href, "");

    }

    protected void btCheckPassTime_Click(object sender, EventArgs e)
    {

        MalfunctionHandleInfo item = CurrentSheet;
        item.FirstDelayApprove = 3;
        item.FirstDelayRemark = tbCheckInfor.Text.Trim();
        item.FirstApproveTime = DateTime.Now;
        item.FirstApproveName = Common.Get_UserName;
        item.FirstConsultTime = Convert.ToInt32(TB_RepairedTime.Text.Trim());
        item.FirstConsultUnit = (TimeUnits)Convert.ToInt32(DDL_RepairedUnit.SelectedValue);
        item.UpdateTime = DateTime.Now;
        DepartmentType currentDeptType = deptBll.GetDepartment(item.MaintainDept).DepartmentType;
        if (currentDeptType == DepartmentType.MaintainTeam) //自维
        {
            item.Status = MalfunctionHandleStatus.Wait4EngineerCheck;
        }
        else
        {
            item.Status = MalfunctionHandleStatus.Waiting4Check;
        }
        try
        {
            malfunctionBll.UpdateMalfunctionSheetBasicData(item);

            string title = "您申请的故障处理单号为：" + item.SheetNO + "已同意协商延迟！";
            string URL = "../MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id=" + item.SheetID;
            string[] receiver = { item.Receiver };
            WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("审批延迟申请单成功,故障处理单号为：{0}", item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("DelayMalfunctionList.aspx"), UrlType.Href, "");

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
