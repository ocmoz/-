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
public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionReport_MalfunctionSheetPrint : System.Web.UI.Page
{
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Department deptBll = new Department();
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    private readonly Address addressBll = new Address();
    private readonly EquipmentSystem systemBll = new EquipmentSystem();
    //private MalfunctionHandleStatus status = MalfunctionHandleStatus.Unknown;
    private readonly EquipmentCost ecBLL = new EquipmentCost();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();

        }
    }
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
 
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败，原因：" + ex.Message);
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        MalfunctionHandleInfo item = null;
        try
        {
            item = malfunctionBll.GetMalfunctionSheet(id);
            if (item == null)
                return;
            CompanyInfo company = companyBll.GetCompany(item.CompanyID);
            if (company != null)
                lbCompany.Text = company.CompanyName;
            lbSheetNO.Text = item.SheetNO;
            lbDepartment.Text = item.DepartmentName;
            lbReporter.Text = item.Reporter;
            lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd HH:mm");//时间显示至分钟，by L 4-23
            //lbAddressDetail.Text = item.AddressDetail;
            string eqmno = item.AddressDetail.Split('@')[0];
            if (eqmno != null && eqmno != "")
            {
                Equipment equipmentBll = new Equipment();
                EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);

                lbEquipmentName.Text = equipmentitem.Name;
                //lbEqSystem.Text = equipmentitem.SystemName;
                lbEquipmentNO.Text = equipmentitem.EquipmentNO;

            }
            //lbEquipmentName.Text = item.AddressDetail.Split('@')[0];
            //repeatEquipments.DataSource = item.FaultyEquipments;
            //repeatEquipments.DataBind();
            //  [4/5/2012 L]lbActFunRestoreTime.Text = item.ActualFunRestoreTimeString;
            lbActRepairTime.Text = item.ActualRepairTimeString;
            if (item.ActualRepairTime > 0)
            {
                lbConTime.Text = item.ReportDate2.AddMinutes(item.ActualRepairTime).ToString("yyyy-MM-dd HH:mm");
            }
            AddressInfo address = addressBll.GetAddress(item.AddressID);
            if (address != null)
                lbAddress.Text = address.AddressFullName;

            lbDescription.Text = item.MalfunctionDescription;
            if (item.SystemID==null||item.SystemID.ToString().Equals(""))
                lbSystem.Text = "未知";
            else
            {
                //EquipmentSystemInfo system = systemBll.GetSystem(item.SystemID);
                //if (system != null)
                    //lbSystem.Text = system.SystemName;
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

            //*****************************************************************************************
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
             EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(item.SheetID);
             //jiliang.Text = ecInfor.IsMeasure;

             if (ecInfor != null)
             {
                 if (ecInfor.IsMeasure == "需要计量")
                 {
                     jiliang.Text = "是";
                 }
                 else
                 {
                     jiliang.Text = "否";
                 }
                 FillCostItemsData(ecInfor);

             }

            //填充故障处理历史列表
            try
            {
                IList maintainHistory = malfunctionBll.GetMaintainHistory(item.SheetID);
                rptMaintainHistory.DataSource = maintainHistory;
                rptMaintainHistory.DataBind();

                if (maintainHistory.Count <=0)
                    trHandleOpinion.Visible = true;

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
                trHandleOpinion.Visible = false;
                EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理历史失败，原因：" + ex);
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

            //撤单原因
            if (item.Status == MalfunctionHandleStatus.Canceled)
            {
                //如果已撤单则显示撤单原因
                lbCancelReason.Text = item.CancelReason;
                UserInfo cancelerInfo = userBll.GetUser(item.Canceler);
                if (cancelerInfo != null)
                    lbCanceler.Text = cancelerInfo.PersonName;

                trCancelReason.Visible = true;
                trCancelStatus.Visible = true;
            }
            else
            {
                trCancelStatus.Visible = false;
                trCancelReason.Visible = false;
            }


        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单内容失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        //更新是否打印的标记
        //try
        //{
        //    item.IsPrinted = true;
        //    malfunctionBll.UpdateMalfunctionSheetBasicData(item);
        //}
        //catch (Exception ex)
        //{
        //    EventMessage.EventWriteLog(Msg_Type.Error, "更新故障单打印标记位失败:" + ex.Message);
        //}
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
}
