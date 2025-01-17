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
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionReport_ViewMalfunctionSheet : System.Web.UI.Page
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
    private readonly Equipment EqBll = new Equipment();



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
         //   PermissionControl();
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
           //By L 4-27 屏蔽满意度调查*******************************************************************
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
            //**********************************************************************************************88
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
          //[5/15/2013 Tvk]
       // bool bDelete = SystemPermission.CheckButtonPermission(PopedomType.Delete);

          //[5/15/2013 Tvk]
       // if (CurrentSheet.Status != MalfunctionHandleStatus.Waiting4Accept && chedang != "true")
           

        if ((int)(CurrentSheet.Status)>=32 && CurrentSheet.Status != MalfunctionHandleStatus.Waiting4Accept && chedang != "true")
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

            CompanyInfo company = companyBll.GetCompany(item.CompanyID);
            if (company != null)
                lbCompany.Text = company.CompanyName;
            lbSheetNO.Text = item.SheetNO;
            lbDepartment.Text = item.DepartmentName;
            lbReporter.Text = item.Reporter;
            lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd HH:mm"); //时间显示到分钟，by L 4-23

            

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
            string[] approvalrecordSplit = approvalrecord.Split(aa,StringSplitOptions.RemoveEmptyEntries);
            string[] checkrecordSplit = checkrecord.Split(aa, StringSplitOptions.RemoveEmptyEntries);
            if (approvalrecordSplit.Length > 1)
            {
                List<FM2E.Model.Maintain.ApprovalRecord> arList = new List<ApprovalRecord>();
                for (int i = 0; i < approvalrecordSplit.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = approvalrecordSplit[i].Split(bb,StringSplitOptions.None);
                    if (arsplitsplit.Length == 6)
                    {
                        FM2E.Model.Maintain.ApprovalRecord ar = new ApprovalRecord(arsplitsplit[0], arsplitsplit[2], arsplitsplit[1], arsplitsplit[3], arsplitsplit[4], arsplitsplit[5]);
                        arList.Add(ar);
                    }
                }
                Repeater1.DataSource = arList;
                Repeater1.DataBind();
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
                HyperLink_File.NavigateUrl =  editreason1[1];
                HyperLink_File.Text = editreason1[0];
                HyperLink_File.Visible = true;
            }

            #endregion



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
                if (equipmentitem!=null)
                {
                    lbEqName.Text = equipmentitem.Name;
                    lbEqSystem.Text = equipmentitem.SystemName;
                    lbEqNo.Text = equipmentitem.EquipmentNO;
                }
                

            }

            DepartmentType currentDeptType = deptBll.GetDepartment(item.MaintainDept).DepartmentType;
            if (currentDeptType == DepartmentType.MaintainTeam)                    //自维
            {
                //  [4/5/2012 L]MaintainPlanTime.Visible = false;
                MaintainPlanRankTitle.Visible = false;
                MaintainPlanRankContent.Visible = false;

                pnMoneyStatistics.Visible = false;
                EqCostHeader.Visible = false;
            }
            if (currentDeptType == DepartmentType.MaintainTeamOther)               //外维
            {
                //  [4/5/2012 L]MaintainPlanTime.Visible = true;
                MaintainPlanRankTitle.Visible = true;
                MaintainPlanRankContent.Visible = true;

                pnMoneyStatistics.Visible = true;
                EqCostHeader.Visible = true;

                EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(item.SheetID);
                if (ecInfor != null)
                {
                    lbshenqingjiliang.Text = ecInfor.IsMeasure;
                    lbjiliang.Text = ecInfor.IsApplyMeasure;
                    lbjiagong.Text = ecInfor.IsProvider;
                    FillCostItemsData(ecInfor);
                }
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
                if(item.ActualRepairTime>0)
                {
                    lbConTime.Text = item.ReportDate2.AddMinutes(item.ActualRepairTime).ToString("yyyy-MM-dd HH:mm");
                }
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
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理历史失败，原因：" + ex.Message);
            }

            //故障处理满意度调查
            //By L 4-27屏蔽***********************************************************************************************
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
            //*********************************************************************************************************
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
            //if (item.Status == MalfunctionHandleStatus.Canceled)
            //{
            //    //如果已撤单则显示撤单原因
            //    lbCancelReason.Text = item.CancelReason;
            //    UserInfo cancelerInfo = userBll.GetUser(item.Canceler);
            //    if (cancelerInfo != null)
            //        lbCanceler.Text = cancelerInfo.PersonName;

            //    trCancelReason.Visible = true;
            //}
            //else trCancelReason.Visible = false;
            if(!string.IsNullOrEmpty(item.Canceler))
            {
                divCancelDetail.Visible = true;
                UserInfo cancelerInfo = userBll.GetUser(item.Canceler);
                if (cancelerInfo != null)
                    lbApplyCancelName.Text = cancelerInfo.PersonName;
                //lbApplyCancelName.Text = item.Canceler;
                lbApplyCancelReason.Text = item.CancelReason;
                lbCancelApplyTime.Text =item.CancelApplyTime.ToString("yyyy-MM-dd HH:mm");
                string CAName = item.CancelApproveName == null ? "" : userBll.GetUser(item.CancelApproveName).PersonName;
                lbCancelApprove.Text = "审批结果：" + item.CancelApproveResult + "    审批时间：" + (item.CancelApproveName == null ? null : item.CancelApproveTime.ToString("yyyy-MM-dd HH:mm")) + "    审批人：" +CAName+ "<br>" + "审批说明：  " + item.CancelApproveRemark;
            }
            else
            {
                divCancelDetail.Visible = false;
            }

            if(item.IsDelayApply==true)
            {
                divDelayApply.Visible = true;
                lbDelayApplyTime.Text = item.DelayApplyTime.ToString("yyyy-MM-dd HH:mm");
                lbDelayForTime.Text = item.DelayForTime.ToString() + EnumHelper.GetDescription(item.DelayForUnit);
                lbApplyDelayReason.Text = editreason[0];
                if (item.FirstDelayApprove != 0 )
                {
                    string tempFirstResult = "";
                    switch (item.FirstDelayApprove)
                    {
                        case 1:
                            tempFirstResult = "同意";
                            break;
                        case 2:
                            tempFirstResult = "不同意";
                            break;
                        case 3:
                            tempFirstResult = "协商时间" + item.FirstConsultTime.ToString() + EnumHelper.GetDescription(item.FirstConsultUnit);
                            break;
                        case 4:
                            tempFirstResult = "提交上一级审批";
                            break;
                        default:
                            tempFirstResult = "";
                            break;
                    }

                    lbDelayApprove.Text = "审批结果： " + tempFirstResult + "   审批人： " + userBll.GetUser(item.FirstApproveName).PersonName + "   审批说明： " + item.FirstDelayRemark;
                    if (item.FinalDelayApprove != 0 )
                    {
                        string tempFinalResult = "";
                        switch (item.FinalDelayApprove)
                        {
                            case 1:
                                tempFinalResult = "同意";
                                break;
                            case 2:
                                tempFinalResult = "不同意";
                                break;
                            case 3:
                                tempFinalResult = "协商时间" + item.FinalConsultTime.ToString() + EnumHelper.GetDescription(item.FinalConsultUnit);
                                break;
                            default:
                                tempFinalResult = "";
                                break;
                        }
                        lbDelayApprove.Text += "</br>" + "审批结果： " + tempFinalResult + "   审批人： " + userBll.GetUser(item.FinalApproveName).PersonName + "   审批说明： " + item.FinalDelayRemark;

                    }
                }
            }
            else
            {
                divDelayApply.Visible = false;
            }
            
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
        item.Status = MalfunctionHandleStatus.Wait4Canceled;
        item.Canceler = Common.Get_UserName;
        item.CancelReason = tbCancelReason.Text.Trim();
        item.UpdateTime = DateTime.Now;
        item.CancelApplyTime = DateTime.Now;
        try
        {
            malfunctionBll.UpdateMalfunctionSheetBasicData(item);

            //**********Modified by Xue 2011-6-27****************************************************************************************************  撤单没有待办事项
            string title = "您提交的故障处理单" + item.SheetNO + "已被申请撤单";
            string URL = "../MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id=" + item.SheetID;

            WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, item.Recorder);
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

    private void FillCostItemsData(EquipmentCostInfor item)
    {
        rpEquipmentItems.DataSource = item.EqCostItems;
        rpEquipmentItems.DataBind();

        lbSumTotal.Text = item.EqSumPrice.ToString();
        lbApprovalSumTotal.Text = item.EqSumApprovalPrice.ToString();

        lbMeasureCost.Text = item.MeasureCost.ToString();
        lbApprovalMeasureCost.Text = item.MeasureApprovalCost.ToString();
        lbGuiCost.Text = item.GuiCost.ToString();
        lbApprovalGuiCost.Text = item.GuiApprovalCost.ToString();
        lbTaxCost.Text = item.TaxCost.ToString();
        lbApprovalTaxCost.Text = item.TaxApprovalCost.ToString();
        lbTrafficCost.Text = item.TrafficCost.ToString();
        lbApprovalTrafficeCost.Text = item.TrafficApprovalCost.ToString();

        lbSumOther.Text = item.SumOtherCost.ToString();
        lbApprovalSumOther.Text = item.SumApprovalOtherCost.ToString();
        lbSumAll.Text = item.TotalSumCost.ToString();
        lbApprovalSumAll.Text = item.TotalSumApprovalCost.ToString();

        lbOtherCost.Text = item.OtherCost.ToString();
        lbApprovalOtherCost.Text = item.OtherApprovalCost.ToString();
        if (item.MarkOne == null)
        {
            lbMarkOne.Text = "";
        }
        else
        { lbMarkOne.Text = item.MarkOne.ToString(); }

        if (item.MarkTwo == null)
        {
            lbMarkTwo.Text = "";
        }
        else
        { lbMarkTwo.Text = item.MarkTwo.ToString(); }
        if (item.MarkThree == null)
        {
            lbMarkThree.Text = "";
        }
        else
        { lbMarkThree.Text = item.MarkThree.ToString(); }

        if (item.MarkFour == null)
        {
            lbMarkFour.Text = "";
        }
        else
        { lbMarkFour.Text = item.MarkFour.ToString(); }

        if (item.MarkFive == null)
        {
            lbMarkFive.Text = "";
        }
        else
        { lbMarkFive.Text = item.MarkFive.ToString(); }

    }

    protected void rptHistoryEquipments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            MaintainedEquipmentInfo MaintainEquipments = (MaintainedEquipmentInfo)e.Item.DataItem;
            Literal lt = (Literal)e.Item.FindControl("lbEqNO");
            if (lt!=null)
            {      
                 EquipmentInfoFacade Eqitem = EqBll.GetEquipmentBYNO(MaintainEquipments.EquipmentNO);
                 lt.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看设备信息','{0}Module/FM2E/DeviceManager/DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/ViewDeviceInfo.aspx?cmd=view&id={1}&companyid=SG&index=',800, 430, null,true,true);", Page.ResolveUrl("~/"), Eqitem.EquipmentID), Eqitem.EquipmentNO);

             
            }
        }

    }

}
