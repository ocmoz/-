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
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionConfirm_Confirm : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Department deptBll = new Department();
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    private readonly Address addressBll = new Address();
    private readonly EquipmentSystem systemBll = new EquipmentSystem();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        btSave.Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    private void InitialPage()
    {
        ListItem[] grade1 = EnumHelper.GetListItemsEx(typeof(Grade), (int)Grade.Excellent, (int)Grade.Unknown);
        ListItem[] grade2 = EnumHelper.GetListItemsEx(typeof(Grade), (int)Grade.Excellent, (int)Grade.Unknown);
        ListItem[] grade3 = EnumHelper.GetListItemsEx(typeof(Grade), (int)Grade.Excellent, (int)Grade.Unknown);
        ListItem[] grade4= EnumHelper.GetListItemsEx(typeof(Grade), (int)Grade.Excellent, (int)Grade.Unknown);

        cblEffect.Items.Clear();
        cblEffect.Items.AddRange(grade1);
        cblAttitude.Items.Clear();
        cblAttitude.Items.AddRange(grade2);
        cblRationality.Items.Clear();
        cblRationality.Items.AddRange(grade3);
        cblTechnicEvaluate.Items.Clear();
        cblTechnicEvaluate.Items.AddRange(grade4);
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

            CompanyInfo company = companyBll.GetCompany(item.CompanyID);
            if (company != null)
                lbCompany.Text = company.CompanyName;
            lbSheetNO.Text = item.SheetNO;
            lbDepartment.Text = item.DepartmentName;
            lbReporter.Text = item.Reporter;
            lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd HH:mm");  //时间显示至分钟，by L 4-23
            //lbAddressDetail.Text = item.AddressDetail;



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
            lbActResponseTime.Text = item.ActualResponseTimeString;
            //  [4/5/2012 L]lbActFunRestoreTime.Text = item.ActualFunRestoreTimeString;
            lbActRepairTime.Text = item.ActualRepairTimeString ;

            //填充故障处理历史列表
            try
            {
                IList maintainHistory = malfunctionBll.GetMaintainHistory(item.SheetID);
                rptMaintainHistory.DataSource = maintainHistory;
                rptMaintainHistory.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理历史失败，原因：" + ex);
            }

            //自动评价维修时间
            if (item.ActualResponseTime <= item.ResponseTimeInMinutes)
                cbIsResponseInTime.Checked = true;
            else cbIsResponseInTime.Checked = false;

            if (item.ActualFunRestoreTime <= item.FunRestoreTimeInMinutes)
                cbIsFunRestoreInTime.Checked = true;
            else cbIsFunRestoreInTime.Checked = false;

            if (item.ActualRepairTime <= item.RepairTimeInMinutes)
                cbIsRepairInTime.Checked = true;
            else cbIsRepairInTime.Checked = false;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单内容失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaintainedEquipments_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            MalfunctionHandleInfo model = CurrentMalfunctionSheet;
            model.Feeback = tbFeebackOpinion.Text.Trim();
            model.Effect=(Grade)Convert.ToInt32(cblEffect.SelectedValue);
            model.Attitude = (Grade)Convert.ToInt32(cblAttitude.SelectedValue);
            model.Rationality = (Grade)Convert.ToInt32(cblRationality.SelectedValue);
            model.TechnicEvaluate = (Grade)Convert.ToInt32(cblTechnicEvaluate.SelectedValue);
            model.UpdateTime = DateTime.Now;
            model.Status = MalfunctionHandleStatus.Finished;
            model.Investigator = Common.Get_UserName;
            model.IsResponseInTime = cbIsResponseInTime.Checked;
            model.IsFunRestoreInTime = cbIsFunRestoreInTime.Checked;
            model.IsRepairInTime = cbIsRepairInTime.Checked;

            malfunctionBll.UpdateMalfunctionSheetBasicData(model);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加满意度调查结果失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加满意度调查结果成功", Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
    }
}
