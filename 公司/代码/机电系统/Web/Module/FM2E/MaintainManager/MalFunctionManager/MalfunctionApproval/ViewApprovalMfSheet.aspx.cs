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
using FM2E.WorkflowLayer;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using System.Data.SqlClient;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionApproval_ViewApprovalMfSheet : System.Web.UI.Page
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
    private readonly string stateName4EqCostModify = FM2E.BLL.System.ConfigItems.StateName4EqCostModify;
    //private MalfunctionHandleStatus status = MalfunctionHandleStatus.Unknown;
    private readonly Equipment EqBll = new Equipment();
    public long ScrapEqID = 1;
    public string ScrapEqNO = "";
    public string ScrapEqName = "";
    public readonly Scrap scrapBll = new Scrap();


    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    protected const string UPLOADFOLDER = "ApprovalMf/";


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

        if (UserData.CurrentUserData.UserName != "zhangkq")
        {
            rdoWanghai.Visible = false;
            rdoHuangLiang.Visible = false;
        }

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

        if (CurrentSheet.Status != MalfunctionHandleStatus.Waiting4Accept && chedang != "true")
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
                lbshenqingjiliang.Text = ecInfor.IsMeasure;
            }

            //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
            FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID);
            WorkFlowUserSelectControl1.EventIDField = "Name";
            WorkFlowUserSelectControl1.EventNameField = "Description";
            WorkFlowUserSelectControl1.WorkFlowState = wii.CurrentStateName;
            WorkFlowUserSelectControl1.WorkFlowName = SGS_MalFunctionWorkflow.WorkflowName;
            //  [4/11/2012 L]
            string nextUserName = "";

            //  [3/31/2013 Tvk]
            if (wii.CurrentStateName == "Wait4ElectDeptASeniorManagerConfirm")//高级经理重新获取
            {
                WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);

                String newcompany = item.MaintainDeptName;
                Guid newguid = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, newcompany, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, "MaintainDeptManagerAccepted", SGS_MalFunctionWorkflow.WorkflowName, newcompany, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, "ElectDeptEngineerAccepted", SGS_MalFunctionWorkflow.WorkflowName, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, "ElectDeptManagerConfirmAccepted", SGS_MalFunctionWorkflow.WorkflowName, out nextUserName);
                wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID);

            }

            //  [3/31/2013 Tvk]
            //  [5/19/2013 Tvk]
            if (wii.CurrentStateName == "Wait4ElectDeptAManagerConfirm")//事务经理重新获取
            {
                WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);

                String newcompany = item.MaintainDeptName;
                Guid newguid = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, newcompany, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, "MaintainDeptManagerAccepted", SGS_MalFunctionWorkflow.WorkflowName, newcompany, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, "ElectDeptEngineerAccepted", SGS_MalFunctionWorkflow.WorkflowName, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, "ElectDeptManagerConfirmAccepted", SGS_MalFunctionWorkflow.WorkflowName, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(newguid, "ManagerDAccepted", SGS_MalFunctionWorkflow.WorkflowName, out nextUserName);
                wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID);

            }

           if (wii.CurrentStateName == "Wait4DutyStationConfirm")
            {
                String newcompany = item.MaintainDeptName;
                string nUser = wii.DelegateUserName;
                WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);
                Guid guid = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, newcompany, out nextUserName);
                WorkflowHelper.SetStateAndNextUserMachine3<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "Wait4MaintainDeptManagerConfirm")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, newcompany, nUser, out nextUserName);
            }
            //  [5/19/2013 Tvk]
            List<WorkflowEventInfo> eventlist = WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName);
            List<WorkflowEventInfo> temlist = eventlist;
            WorkFlowUserSelectControl1.EventListDataSource = temlist;
            //  [4/11/2012 L]
            WorkFlowUserSelectControl1.EventListDataBind();
            //  [3/31/2013 Tvk]
            if (wii.CurrentStateName == "Wait4ElectDeptSeniorManagerConfirm")
            {
                WorkFlowUserSelectControl1.SelectedEvent = "ManagerDAccepted";
            }
            //  [3/31/2013 Tvk]
            WorkFlowUserSelectControl1.ShowCompanySelect = false;
            //WorkFlowUserSelectControl1.SelectedCompanyID = UserData.CurrentUserData.CompanyID;
            //WorkFlowUserSelectControl1.SelectedDepartmentID = UserData.CurrentUserData.DepartmentID;
            //WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitNewEvent);
            //WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitDraftEvent);
            //WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitReturnModifyEvent);
            //********** Modification Finished 2011-11-28 **********************************************************************************************



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
            string delayapply = editreason[0];
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
                    item.attachment += " " + separatorStr + " ";  //附件名称+附件地址
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


            if (wii.CurrentStateName == "Wait4ElectDeptAManagerConfirm")
            {
                tbApprovalRemark.Text = "";
            }
            else
                tbApprovalRemark.Text = "审批通过";
            if (wii.CurrentStateName == "Wait4ElectDeptEngineerConfirm")
            {
                ScrapChoose.Visible = true;
            }
            else
            {
                ScrapChoose.Visible = false;
            }
            string eqmno = item.AddressDetail.Split('@')[0];
            if (eqmno != null && eqmno != "")
            {
                Equipment equipmentBll = new Equipment();
                EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);
                if (equipmentitem != null)
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
                //pnMoneyStatistics2.Visible = false;
                pnMoneyStatistics.Visible = false;
                EqCostHeader.Visible = false;
            }
            if (currentDeptType == DepartmentType.MaintainTeamOther)               //外维
            {
                MaintainPlanRankTitle.Visible = true;
                MaintainPlanRankContent.Visible = true;
                EqCostHeader.Visible = true;
                if (ecInfor != null)
                {
                    EqCostInfor = ecInfor;
                    EqCostItems = ecInfor.EqCostItems;//-by L 4-19 
                    FillCostItemsData(EqCostInfor);
                }
            }
            #region
            if (wii.CurrentStateName == stateName4EqCostModify)  //机电工程师审批状态时  计量审批
            {
                //pnMoneyStatistics2.Visible = false;
                jiliang.Visible = false;
                checkbox1.Visible = false;
                tbApprovalGuiCost.Visible = true;
                tbApprovalMeasureCost.Visible = true;
                tbApprovalTaxCost.Visible = true;
                tbApprovalTrafficeCost.Visible = true;
                jilianga.Visible = true;
                jiliangb.Visible = true;
                // 屏蔽计量输入 [4/12/2012 L]y
                tbEqName.Visible = false;
                tbEqNum.Visible = false;
                tbEqModel.Visible = false;
                tbEqUnit.Visible = false;
                tbEqSinglePrice.Visible = false;
                tbEqTotalPrice.Visible = false;
                tbEqRemark.Visible = false;           //备注可编辑
                tbGuiCost.Visible = false;
                tbMeasureCost.Visible = false;
                tbTaxCost.Visible = false;
                tbTrafficCost.Visible = false;
                btAddEquipmentItems.Visible = false;
                lbOtherCost.Visible = true;
                tbOtherCost.Visible = false;
                lbApprovalGuiCost.Visible = false;
                lbApprovalMeasureCost.Visible = false;
                lbApprovalTaxCost.Visible = false;
                lbApprovalTrafficeCost.Visible = false;
                lbApprovalOtherCost.Visible = false;
                tbApprovalOtherCost.Visible = true;
                lbMarkOne.Visible = false;
                lbMarkTwo.Visible = false;
                lbMarkThree.Visible = false;
                lbMarkFour.Visible = false;
                lbMarkFive.Visible = false;

                jilianga.Visible = true;
                jiliangb.Visible = true;
                lbshenqingjiliang.Visible = true;
                lbjiliang.Visible = false;
                jiagonga.Visible = true;
                jiagongb.Visible = true;
                lbjiagong.Visible = false;

                for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
                {
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqName"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqNum"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqModel"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqUnit"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSinglePrice"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSumPrice"))).Visible = false;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Visible = true; //备注可审批编辑

                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Visible = true;
                    ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalUnitPrice"))).Visible = false;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalTotalPrice"))).Visible = false;
                    ((ImageButton)(rpEquipmentItems.Items[ii].FindControl("ibDelEqItems"))).Visible = false;  // 删除控件屏蔽 [4/12/2012 L]

                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqName"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqNum"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqModel"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqUnit"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSinglePrice"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSumPrice"))).Visible = true;
                    ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqRemark"))).Visible = false; //备注可审批编辑

                    //((Label)(rpEquipmentItems.Items[ii].FindControl("lbReMark"))).Visible = false;
                    //((Label)(rpEquipmentItems.Items[ii].FindControl("tbReMark"))).Visible = true;
                }

            }
            else
            {
                if (wii.CurrentStateName == "Wait4MaintainDeptManagerConfirm")//-外维经理审批和计量填写
                {
                    //pnMoneyStatistics.Visible = false;
                    //pnMoneyStatistics2.Visible = true;

                    tbApprovalGuiCost.Visible = false;
                    tbApprovalMeasureCost.Visible = false;
                    tbApprovalTaxCost.Visible = false;
                    tbApprovalTrafficeCost.Visible = false;
                    checkbox1.Visible = true;                  //计量按键可见
                    jiliang.Visible = true;                  //计量按键可见
                    lbGuiCost.Visible = false;
                    lbMeasureCost.Visible = false;
                    lbTaxCost.Visible = false;
                    lbTrafficCost.Visible = false;

                    lbOtherCost.Visible = false;
                    lbApprovalOtherCost.Visible = true;
                    lbMarkOne.Visible = false;
                    lbMarkTwo.Visible = false;
                    lbMarkThree.Visible = false;
                    lbMarkFour.Visible = false;
                    lbMarkFive.Visible = false;

                    lbApprovalGuiCost.Visible = true;
                    lbApprovalMeasureCost.Visible = true;
                    lbApprovalTaxCost.Visible = true;
                    lbApprovalTrafficeCost.Visible = true;
                    tbGuiCost.Visible = true;
                    tbMeasureCost.Visible = true;
                    tbTaxCost.Visible = true;
                    tbTrafficCost.Visible = true;

                    tbMarkOne.Visible = true;
                    tbMarkTwo.Visible = true;
                    tbMarkThree.Visible = true;
                    tbMarkFour.Visible = true;
                    tbMarkFive.Visible = true;
                    tbOtherCost.Visible = true;
                    tbApprovalOtherCost.Visible = false;
                    jilianga.Visible = false;
                    jiliangb.Visible = false;
                    lbshenqingjiliang.Visible = true;
                    lbjiliang.Visible = true;
                    jiagonga.Visible = false;
                    jiagongb.Visible = false;
                    lbjiagong.Visible = true;

                    for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
                    {

                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqName"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqNum"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqModel"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqUnit"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSinglePrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSumPrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Visible = false; //备注可审批编辑


                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalUnitPrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalTotalPrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqName"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqNum"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqModel"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqUnit"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSinglePrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSumPrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqRemark"))).Visible = true; //备注可审批编辑
                    }
                }
                else
                {
                    checkbox1.Visible = false;
                    jiliang.Visible = false;
                    btAddEquipmentItems.Visible = false;
                    tbApprovalGuiCost.Visible = false;
                    tbApprovalMeasureCost.Visible = false;
                    tbApprovalTaxCost.Visible = false;
                    tbApprovalTrafficeCost.Visible = false;
                    //rpEquipmentItems.Visible = false;
                    jilianga.Visible = false;
                    jiliangb.Visible = false;
                    lbshenqingjiliang.Visible = true;
                    lbjiliang.Visible = true;
                    jiagonga.Visible = false;
                    jiagongb.Visible = false;
                    lbjiagong.Visible = true;
                    tbEqName.Visible = false;
                    tbEqNum.Visible = false;
                    tbEqModel.Visible = false;
                    tbEqUnit.Visible = false;
                    tbEqSinglePrice.Visible = false;
                    tbEqTotalPrice.Visible = false;
                    tbEqRemark.Visible = false;

                    tbGuiCost.Visible = false;
                    tbMeasureCost.Visible = false;
                    tbTaxCost.Visible = false;
                    tbTrafficCost.Visible = false;

                    lbApprovalGuiCost.Visible = true;
                    lbApprovalMeasureCost.Visible = true;
                    lbApprovalTaxCost.Visible = true;
                    lbApprovalTrafficeCost.Visible = true;



                    tbOtherCost.Visible = false;
                    tbApprovalOtherCost.Visible = false;
                    tbMarkOne.Visible = false;
                    tbMarkTwo.Visible = false;
                    tbMarkThree.Visible = false;
                    tbMarkFour.Visible = false;
                    tbMarkFive.Visible = false;

                    lbMarkOne.Visible = true;
                    lbMarkTwo.Visible = true;
                    lbMarkThree.Visible = true;
                    lbMarkFour.Visible = true;
                    lbMarkFive.Visible = true;
                    lbOtherCost.Visible = true;
                    lbApprovalOtherCost.Visible = true;

                    for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
                    {
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqName"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqNum"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqModel"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqUnit"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSinglePrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSumPrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Visible = false;
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Visible = false; //备注可审批编辑


                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalUnitPrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalTotalPrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqName"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqNum"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqModel"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqUnit"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSinglePrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSumPrice"))).Visible = true;
                        ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqRemark"))).Visible = true; //备注可审批编辑
                        ((ImageButton)(rpEquipmentItems.Items[ii].FindControl("ibDelEqItems"))).Visible = false;  // 删除控件屏蔽 [4/12/2012 L]

                    }
                }


            }
            #endregion 按钮控制

            //********** Modification Finished 2011-11-28 **********************************************************************************************

            CompanyInfo company = companyBll.GetCompany(item.CompanyID);
            if (company != null)
                lbCompany.Text = company.CompanyName;
            lbSheetNO.Text = item.SheetNO;
            lbDepartment.Text = item.DepartmentName;
            lbReporter.Text = item.Reporter;
            lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd HH:mm");//时间显示至分钟，by L 4-23
            //lbAddressDetail.Text = item.AddressDetail;

            //repeatEquipments.DataSource = item.FaultyEquipments;
            //repeatEquipments.DataBind();

            AddressInfo address = addressBll.GetAddress(item.AddressID);
            if (address != null)
                lbAddress.Text = address.AddressFullName;
            if (ecInfor != null)
            {
                lbjiagong.Text = ecInfor.IsProvider;
                lbjiliang.Text = ecInfor.IsApplyMeasure;
            }
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
            lbRecorder.Text = item.RecorderName;
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
                if (item.ActualRepairTime > 0)
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
                IList eqNO = new ArrayList();
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
                    if (detailitem.MaintainedEquipments != null)
                    {
                        foreach (MaintainedEquipmentInfo mainEq in detailitem.MaintainedEquipments)
                        {
                            eqNO.Add(mainEq.EquipmentNO);
                        }
                    }
                }
                string a = "";
                if (eqNO != null)
                {
                    a = Convert.ToString(eqNO[0]);
                    EquipmentInfoFacade Eqitem = EqBll.GetEquipmentBYNO(a);
                    a = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看设备信息','{0}Module/FM2E/DeviceManager/DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/ViewDeviceInfo.aspx?cmd=view&id={1}&companyid=SG&index=',800, 430, null,true,true);", Page.ResolveUrl("~/"), Eqitem.EquipmentID), Eqitem.EquipmentNO);
                    ScrapEqID = Eqitem.EquipmentID;
                    ScrapEqNO = Eqitem.EquipmentNO;
                    ScrapEqName = Eqitem.Name;
                }
                ScrapEquipmentNo.Text = a;
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
    /// <summary>
    /// 提交审批
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool returnFlag = false;
        bool finishFlag = false;
        bool anothercheckFlag = false;
        MalfunctionHandleInfo item = CurrentSheet;
        //2013-1***********************************************************************************

        IList maintainHistory = malfunctionBll.GetMaintainHistory(item.SheetID);
        IList eqNO = new ArrayList();
        Hashtable ht = new Hashtable();
        foreach (MalfuncitonMaintainInfo detailitem in maintainHistory)
        {
            if (detailitem.MaintainedEquipments != null)
            {
                foreach (MaintainedEquipmentInfo mainEq in detailitem.MaintainedEquipments)
                {
                    eqNO.Add(mainEq.EquipmentNO);
                }
            }
        }
        string aaa = "";
        if (eqNO.Count > 0)
        {
            aaa = Convert.ToString(eqNO[0]);
            EquipmentInfoFacade Eqitem = EqBll.GetEquipmentBYNO(aaa);
            if (Eqitem != null)
            {
                ScrapEqID = Eqitem.EquipmentID;
                ScrapEqNO = Eqitem.EquipmentNO;
                ScrapEqName = Eqitem.Name;
            }

        }
        try
        {
            string nextUserName = "";
            FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID);
            #region 更新费用统计表
            if (wii.CurrentStateName == "Wait4MaintainDeptManagerConfirm")  //外维单位，增加费用统计表
            {

                EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(item.SheetID);
                if (ecInfor == null)  // Add
                {
                    ecInfor = new EquipmentCostInfor();
                    ecInfor.EqCostItems = EqCostItems;
                    ecInfor.SheetID = item.SheetID;

                    ecInfor.EqSumPrice = Convert.ToDecimal(lbSumTotal.Text);
                    ecInfor.EqSumApprovalPrice = Convert.ToDecimal(lbApprovalSumTotal.Text);
                    ecInfor.MeasureCost = Convert.ToDecimal(tbMeasureCost.Text.Trim());
                    ecInfor.GuiCost = Convert.ToDecimal(tbGuiCost.Text.Trim());
                    ecInfor.TaxCost = Convert.ToDecimal(tbTaxCost.Text.Trim());
                    ecInfor.TrafficCost = Convert.ToDecimal(tbTrafficCost.Text.Trim());
                    ecInfor.SumOtherCost = Convert.ToDecimal(lbSumOther.Text);
                    if (checkbox1.Checked)
                    {
                        ecInfor.IsMeasure = "需要计量";
                    }
                    else
                    {
                        ecInfor.IsMeasure = "不需要计量";
                    }
                    ecInfor.MeasureApprovalCost = 0;
                    ecInfor.GuiApprovalCost = 0;
                    ecInfor.TaxApprovalCost = 0;
                    ecInfor.TrafficApprovalCost = 0;
                    ecInfor.SumApprovalOtherCost = 0;
                    ecInfor.OtherApprovalCost = 0;
                    ecInfor.TotalSumCost = Convert.ToDecimal(lbSumAll.Text);
                    ecInfor.TotalSumApprovalCost = 0;
                    ecInfor.IsApplyMeasure = "";
                    ecInfor.IsProvider = "";
                    ecBLL.AddEquipmentCostInfor(ecInfor);
                }
                else
                {
                    ecInfor.EqCostItems = EqCostItems;
                    ecInfor.EqSumPrice = Convert.ToDecimal(lbSumTotal.Text);
                    ecInfor.EqSumApprovalPrice = Convert.ToDecimal(lbApprovalSumTotal.Text);
                    ecInfor.MeasureCost = Convert.ToDecimal(tbMeasureCost.Text.Trim());
                    ecInfor.GuiCost = Convert.ToDecimal(tbGuiCost.Text.Trim());
                    ecInfor.TaxCost = Convert.ToDecimal(tbTaxCost.Text.Trim());
                    ecInfor.TrafficCost = Convert.ToDecimal(tbTrafficCost.Text.Trim());
                    ecInfor.OtherCost = Convert.ToDecimal(tbOtherCost.Text.Trim());
                    ecInfor.MarkOne = tbMarkOne.Text;
                    ecInfor.MarkTwo = tbMarkTwo.Text;
                    ecInfor.MarkThree = tbMarkThree.Text;
                    ecInfor.MarkFour = tbMarkFour.Text;
                    ecInfor.MarkFive = tbMarkFive.Text;
                    ecInfor.SumOtherCost = ecInfor.MeasureCost + ecInfor.GuiCost + ecInfor.TaxCost + ecInfor.TrafficCost + ecInfor.OtherCost;
                    ecInfor.TotalSumCost = ecInfor.SumOtherCost + ecInfor.EqSumPrice;
                    if (checkbox1.Checked)
                    {
                        ecInfor.IsMeasure = "需要计量";
                    }
                    else
                    {
                        ecInfor.IsMeasure = "不需要计量";
                    }
                    ecBLL.UpdateEquipmentCostInfor(ecInfor);
                }

            }
            if (wii.CurrentStateName == "Wait4ElectDeptManagerConfirm")  //更新费用统计表 业务经理审核
            {
                decimal a, b, c, d, ee, f, g, h;
                a = Convert.ToDecimal(tbApprovalMeasureCost.Text.Trim());
                EqCostInfor.MeasureApprovalCost = a;
                b = Convert.ToDecimal(tbApprovalGuiCost.Text.Trim());
                EqCostInfor.GuiApprovalCost = b;
                c = Convert.ToDecimal(tbApprovalTaxCost.Text.Trim());
                EqCostInfor.TaxApprovalCost = c;
                d = Convert.ToDecimal(tbApprovalTrafficeCost.Text.Trim());
                EqCostInfor.TrafficApprovalCost = d;
                if (jilianga.Checked)
                {
                    EqCostInfor.IsApplyMeasure = "是";
                }
                if (jiliangb.Checked)
                {
                    EqCostInfor.IsApplyMeasure = "否";
                }
                if (jiagonga.Checked)
                {
                    EqCostInfor.IsProvider = "是";
                }
                if (jiagongb.Checked)
                {
                    EqCostInfor.IsProvider = "否";
                }
                h = Convert.ToDecimal(tbApprovalOtherCost.Text.Trim());
                EqCostInfor.OtherApprovalCost = h;
                g = a + b + c + d + h;
                EqCostInfor.SumApprovalOtherCost = g;
                ee = 0;
                f = 0;
                EqCostInfor.MarkOne = tbMarkOne.Text;
                EqCostInfor.MarkTwo = tbMarkTwo.Text;
                EqCostInfor.MarkThree = tbMarkThree.Text;
                EqCostInfor.MarkFour = tbMarkFour.Text;
                EqCostInfor.MarkFive = tbMarkFive.Text;
                for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
                {
                    EqCostInfor.EqCostItems[ii].EqApprovalUnitPrice = Convert.ToDecimal(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Text.Trim());
                    ee = Convert.ToDecimal(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Text.Trim());
                    EqCostInfor.EqCostItems[ii].EqApprovalTotalPrice = ee;
                    f += ee;
                    EqCostInfor.EqCostItems[ii].EqRemark = Convert.ToString(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Text.Trim());

                }
                EqCostInfor.EqSumApprovalPrice = f;
                EqCostInfor.TotalSumApprovalCost = f + g;
                ecBLL.UpdateEquipmentCostInfor(EqCostInfor);
            }

            #endregion

            Guid guid = wii.InstanceID;
            String company = item.MaintainDeptName;//-by L 4-19
            bool stationCheck = item.Stationcheck;
            string systemID = "FFFF";
            if (item.AddressDetail != null)
            {
                systemID = item.AddressDetail.Split('@')[1];
            }

            string nextUser = "";
            if (rdoHuangLiang.Checked)
            {
                nextUser = "huangliang";
            }
            else if (rdoWanghai.Checked)
            {
                nextUser = "wangHai";
            }

            if (WorkFlowUserSelectControl1.SelectedEvent == "ElectDeptManagerConfirmBackToJiliang" || WorkFlowUserSelectControl1.SelectedEvent == "ElectDeptEngineerRejected")//返回计量，根据外维单位返回相应人员处理
            {
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
            }
            else if (WorkFlowUserSelectControl1.SelectedEvent == "MaintainDeptManagerAccepted" || WorkFlowUserSelectControl1.SelectedEvent == "ElectDeptManagerConfirmRejected")
            {

                if (UserData.CurrentUserData.UserName == "zhangkq" && !string.IsNullOrEmpty(nextUser))
                {
                    //if (wii.CurrentStateName == "Wait4MaintainDeptManagerConfirm")
                    //{
                    //    string nUser = wii.DelegateUserName;
                    //    WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);
                    //    Guid guid1 = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                    //    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid1, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                    //    WorkflowHelper.SetStateAndNextUserMachine3<SGS_MalFunctionEventService>(guid1, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "Wait4MaintainDeptManagerConfirm")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, nUser, out nextUserName);
                    //}
                    //else
                    //{
                    try
                    {
                        WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent, SGS_MalFunctionWorkflow.WorkflowName, stationCheck, systemID, nextUser, out nextUserName);
                    }
                    catch (Exception ex)
                    {
                        //兼容旧单
                        WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, item.SheetID);
                        Guid guid1 = WorkflowHelper.CreateNewInstance(item.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                        WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid1, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                        WorkflowHelper.SetStateAndNextUserMachine3<SGS_MalFunctionEventService>(guid1, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "Wait4MaintainDeptManagerConfirm")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, nextUser, out nextUserName);
                    }
                    //}  
                }
                else
                {
                    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent, SGS_MalFunctionWorkflow.WorkflowName, stationCheck, systemID, out nextUserName);
                }
            }
            else
            {
                WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent, SGS_MalFunctionWorkflow.WorkflowName, out nextUserName);
            }

            if (WorkFlowUserSelectControl1.SelectedEvent == "MaintainDeptManagerRejectedAndReturnModify")  //返回修改
            {
                item.Status = MalfunctionHandleStatus.ReturnModify;
                returnFlag = true;
            }
            string currentStateName = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID).CurrentStateName;
            if (currentStateName == "FinishedNormal")  //审批完毕
            {
                item.Status = MalfunctionHandleStatus.Fixed;
                finishFlag = true;
            }
            //2012-12***************************
            if (WorkFlowUserSelectControl1.SelectedEvent == "MaintainDeptManagerAccepted")//外围单位计量后，进入再验收环节
            {
                item.Status = MalfunctionHandleStatus.Waiting4ReporterCheck;
                anothercheckFlag = true;
            }
            //2012-12***********************
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
            // 用户真名，职位名，部门名，时间，事件名，意见
            if (wii.CurrentStateName == "Wait4ElectDeptAManagerConfirm" && tbApprovalRemark.Text.Trim() == "")
            {
                tbApprovalRemark.Text = WorkFlowUserSelectControl1.SelectedEventName;
            }
            if (wii.CurrentStateName == "Wait4ElectDeptAManagerConfirm")
            {
                editreason[1] += "→" + userinfor.PersonName + "#" + userinfor.PositionName + "#" + userinfor.DepartmentName + "#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "#" + "通过" + "#" + tbApprovalRemark.Text.Replace('#', '。').Trim();
            }
            else
            {
                editreason[1] += "→" + userinfor.PersonName + "#" + userinfor.PositionName + "#" + userinfor.DepartmentName + "#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "#" + WorkFlowUserSelectControl1.SelectedEventName + "#" + tbApprovalRemark.Text.Replace('#', '。').Trim();
            }
            item.Editreason = editreason[0] + separatorStr + editreason[1] + separatorStr + editreason[2];
            #endregion

            item.UpdateTime = DateTime.Now;


            #region 添加附件记录
            if (HiddenField1.Value != null)
            {
                item.attachment = HiddenField1.Value + separatorStr + HiddenField2.Value;
            }

            #endregion


            malfunctionBll.UpdateMalfunctionSheetBasicData(item);



            //2013-1***********************************************************************************报废存稿
            if (ScrapButton.Checked && ScrapEqNO != "")
            {
                int ii = 1;
                ii = Convert.ToInt32(ExecuteScalar("select count(ScrapID) from FM2E_ScrapEquipments where EquipmentNO = '" + ScrapEqNO + "'"));
                if (ii == 0)
                {
                    //表单编号
                    string lbSheetNO = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.SCRAP_SCRAPAPPLY);
                    long ddlDep = UserData.CurrentUserData.DepartmentID;
                    //申请人
                    string lbApplicant = UserData.CurrentUserData.PersonName;
                    string lbStatus = "草稿";
                    ScrapApplyInfo itemScrap = new ScrapApplyInfo();

                    itemScrap.SheetName = lbSheetNO.Trim();
                    itemScrap.CompanyID = UserData.CurrentUserData.CompanyID;
                    itemScrap.DepID = ddlDep;
                    itemScrap.Applicant = UserData.CurrentUserData.UserName;
                    itemScrap.Status = ScrapStatus.Draft;  //草稿
                    itemScrap.ApplyDate = DateTime.Now;
                    itemScrap.Remark = "";

                    ArrayList list = new ArrayList();
                    ScrapApplyDetailInfo detail = new ScrapApplyDetailInfo();
                    detail.EquipmentNo = ScrapEqNO;
                    detail.EquipmentName = ScrapEqName.Trim();
                    detail.ScrapReason = "未知";
                    list.Add(detail);
                    itemScrap.Equipments = list;
                    scrapBll.AddScrapApply(itemScrap);
                }


            }


            //2013-1***********************************************************************************
            if (returnFlag)  //不通过返回修改
            {
                string title = "故障处理单审批不通过，请重新进行登记，故障处理单号为：" + item.SheetNO;
                string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionSheets.aspx";
                string[] receiver = { item.Receiver };
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
            }
            if (finishFlag)  //完成审批
            {

            }
            if (anothercheckFlag)//进行再验收环节
            {
                string title = "您有新的故障处理单要验收，故障处理单号为：" + item.SheetNO;
                string URL = "../MaintainManager/MalFunctionManager/MalfunctionCheck/MalfunctionCheckList.aspx";
                string[] receiver = { item.Recorder };
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
            }
            if (returnFlag == false && finishFlag == false && anothercheckFlag == false)  //进入下一审批
            {
                string title = "您有新的故障处理单要审批，故障处理单号为：" + item.SheetNO;
                string URL = "../MaintainManager/MalFunctionManager/MalfunctionApproval/MalfunctionApprovalList.aspx";
                string[] receiver = { nextUserName };
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "故障处理单审批失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        int i = 0;
        i = Convert.ToInt32(ExecuteScalar("select count(ID) from FM2E_PendingOrder where title like '%要审批%" + item.SheetNO + "%' and Address like '%" + Common.Get_UserName + "%'"));
        if (i == 1)
        {
            ExecuteNonQuery("Update  FM2E_PendingOrderReceiver set HasRead = 1 where ID =(select ID from FM2E_PendingOrder where title like '%要审批%" + item.SheetNO + "%' and Address like '%" + Common.Get_UserName + "%')");
        }

        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("故障处理单审批成功,故障处理单号为：{0}", CurrentSheet.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionApprovalList.aspx"), UrlType.Href, "");
        //Response.Write("<script>window.opener=null;window.close();</script>");
    }
    //********** Modification Finished 2011-11-28 **********************************************************************************************


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

        lbOtherCost.Text = item.OtherCost.ToString();
        lbApprovalOtherCost.Text = item.OtherApprovalCost.ToString();
        if (item.MarkOne != null)
        {
            lbMarkOne.Text = item.MarkOne.ToString();
            tbMarkOne.Text = item.MarkOne.ToString();
        }

        if (item.MarkTwo != null)
        {
            lbMarkTwo.Text = item.MarkTwo.ToString();
        }
        if (item.MarkThree != null)
        {
            lbMarkThree.Text = item.MarkThree.ToString();
        }
        if (item.MarkFour != null)
        {
            lbMarkFour.Text = item.MarkFour.ToString();
        }
        if (item.MarkFive != null)
        {
            lbMarkFive.Text = item.MarkFive.ToString();
        }

        tbOtherCost.Text = item.OtherCost.ToString();
        tbApprovalOtherCost.Text = item.OtherApprovalCost.ToString();

        tbMarkTwo.Text = item.MarkTwo;
        tbMarkThree.Text = item.MarkThree;
        tbMarkFour.Text = item.MarkFour;
        tbMarkFive.Text = item.MarkFive;

        tbApprovalGuiCost.Text = item.GuiApprovalCost.ToString();
        tbApprovalMeasureCost.Text = item.MeasureApprovalCost.ToString();
        tbApprovalTaxCost.Text = item.TaxApprovalCost.ToString();
        tbApprovalTrafficeCost.Text = item.TrafficApprovalCost.ToString();
        tbApprovalOtherCost.Text = item.OtherApprovalCost.ToString();//2012-12
        //By L 4-26计量表获取值可编辑***********************************************************
        tbMeasureCost.Text = item.MeasureCost.ToString();
        tbGuiCost.Text = item.GuiCost.ToString();
        tbTaxCost.Text = item.TaxCost.ToString();
        tbTrafficCost.Text = item.TrafficCost.ToString();
        tbOtherCost.Text = item.OtherCost.ToString();//2012-12
        //**************************************************************************************
        lbSumOther.Text = item.SumOtherCost.ToString();
        lbApprovalSumOther.Text = item.SumApprovalOtherCost.ToString();
        lbSumAll.Text = item.TotalSumCost.ToString();
        lbApprovalSumAll.Text = item.TotalSumApprovalCost.ToString();

        FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, item.SheetID);

        if (wii.CurrentStateName == "Wait4MaintainDeptManagerConfirm")//- 审批和计量填写 业务经理
        {
            tbApprovalGuiCost.Text = item.GuiApprovalCost.ToString();
            tbApprovalMeasureCost.Text = item.MeasureApprovalCost.ToString();
            tbApprovalTaxCost.Text = item.TaxApprovalCost.ToString();
            tbApprovalTrafficeCost.Text = item.TrafficApprovalCost.ToString();
            tbApprovalOtherCost.Text = item.OtherApprovalCost.ToString();
            if (item.MarkOne != null)
            {
                tbMarkOne.Text = item.MarkOne.ToString();
            }
            if (item.MarkTwo != null)
            {
                tbMarkTwo.Text = item.MarkTwo.ToString();
            }
            if (item.MarkThree != null)
            {
                tbMarkThree.Text = item.MarkThree.ToString();
            }
            if (item.MarkFour != null)
            {
                tbMarkFour.Text = item.MarkFour.ToString();
            }
            if (item.MarkFive != null)
            {
                tbMarkFive.Text = item.MarkFive.ToString();
            }
        }
        else
        {
            lbSumOther.Text = item.SumOtherCost.ToString();
            lbApprovalSumOther.Text = item.SumApprovalOtherCost.ToString();
            lbSumAll.Text = item.TotalSumCost.ToString();
            lbApprovalSumAll.Text = item.TotalSumApprovalCost.ToString();
            lbApprovalOtherCost.Text = item.OtherApprovalCost.ToString();
        }
    }

    protected void rpEquipmentItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        int index = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "delEq")
        {
            EqCostItems.RemoveAt(index);
        }
        FillCostItemsData();
        for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
        {
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqName"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqNum"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqModel"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqUnit"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSinglePrice"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSumPrice"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Visible = false;
            ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Visible = false; //备注可审批编辑


            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalUnitPrice"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalTotalPrice"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqName"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqNum"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqModel"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqUnit"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSinglePrice"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSumPrice"))).Visible = true;
            ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqRemark"))).Visible = true; //备注可审批编辑
        }
        attachmentId.Style.Add("display", "block");//隐藏
    }


    protected void btAddEquipmentItems_Click1(object sender, EventArgs e)
    {
        if (tbEqName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v1", "alert('请输入设备名称！')", true);
            return;
        }
        if (tbEqModel.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v2", "alert('请输入设备型号！')", true);
            return;
        }
        EquipmentCostItems item = new EquipmentCostItems();
        List<EquipmentCostItems> list = EqCostItems;

        try
        {
            item.EqName = tbEqName.Text.Trim();
            item.EqModel = tbEqModel.Text.Trim();
            item.EqUnit = tbEqUnit.Text.Trim();
            item.EqNum = Convert.ToInt32(tbEqNum.Text.Trim().ToString());
            item.EqUnitPrice = Convert.ToDecimal(tbEqSinglePrice.Text.Trim().ToString());
            item.EqTotalPrice = Convert.ToDecimal(tbEqTotalPrice.Text.Trim().ToString());
            item.EqRemark = tbEqRemark.Text.Trim();

            list.Add(item);
            EqCostItems = list;
            FillCostItemsData();
            tbApprovalGuiCost.Visible = false;
            tbApprovalMeasureCost.Visible = false;
            tbApprovalTaxCost.Visible = false;
            tbApprovalTrafficeCost.Visible = false;
            tbApprovalOtherCost.Visible = false;
            checkbox1.Visible = true;                  //计量按键可见
            jiliang.Visible = true;                  //计量按键可见

            lbApprovalGuiCost.Visible = true;
            lbApprovalMeasureCost.Visible = true;
            lbApprovalTaxCost.Visible = true;
            lbApprovalTrafficeCost.Visible = true;
            lbApprovalOtherCost.Visible = true;
            for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
            {
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqName"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqNum"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqModel"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqUnit"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSinglePrice"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqSumPrice"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Visible = false;
                ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Visible = false; //备注可审批编辑


                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalUnitPrice"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqApprovalTotalPrice"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqName"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqNum"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqModel"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqUnit"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSinglePrice"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqSumPrice"))).Visible = true;
                ((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqRemark"))).Visible = true; //备注可审批编辑
            }
            attachmentId.Style.Add("display", "block");//隐藏
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v3", "alert('错误，输入的格式不正确！(" + ex.Message + ")')", true);
        }
    }

    protected void tbMeasureCost_TextChanged(object sender, EventArgs e)
    {
        decimal sumOther = 0;
        decimal approvalSumOther = 0;
        try
        {
            decimal totalPrice = 0;
            decimal approvalTotalPrice = 0;
            foreach (EquipmentCostItems item in EqCostItems)
            {
                totalPrice += item.EqTotalPrice;
                //approvalTotalPrice += item.EqApprovalTotalPrice;
            }
            decimal a = 0, b = 0, c = 0, d = 0, ee = 0, f = 0, g = 0, h = 0;
            a = Convert.ToDecimal(tbApprovalMeasureCost.Text.Trim());
            EqCostInfor.MeasureApprovalCost = a;
            b = Convert.ToDecimal(tbApprovalGuiCost.Text.Trim());
            EqCostInfor.GuiApprovalCost = b;
            c = Convert.ToDecimal(tbApprovalTaxCost.Text.Trim());
            EqCostInfor.TaxApprovalCost = c;
            d = Convert.ToDecimal(tbApprovalTrafficeCost.Text.Trim());
            EqCostInfor.TrafficApprovalCost = d;
            h = Convert.ToDecimal(tbApprovalOtherCost.Text.Trim());
            g = a + b + c + d + h;
            EqCostInfor.SumApprovalOtherCost = g;
            ee = 0;
            f = 0;
            FM2E.Model.Workflow.WorkflowInstanceInfo tempWii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, CurrentSheet.SheetID);

            if (tempWii.CurrentStateName == stateName4EqCostModify)
            {
                for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
                {
                    if ((((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Text.Trim()) != null)
                    {
                        EqCostInfor.EqCostItems[ii].EqApprovalUnitPrice = Convert.ToDecimal(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Text.Trim());
                    }
                    if ((((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Text.Trim()) != null)
                    {
                        ee = Convert.ToDecimal(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Text.Trim());
                    }
                    EqCostInfor.EqCostItems[ii].EqApprovalTotalPrice = ee;
                    f += ee;
                    if ((((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Text.Trim()) != null)
                    {
                        EqCostInfor.EqCostItems[ii].EqRemark = Convert.ToString(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Text.Trim());
                    }
                }
            }

            approvalTotalPrice = f;
            EqCostInfor.TotalSumApprovalCost = f + g;
            ecBLL.UpdateEquipmentCostInfor(EqCostInfor);

            lbSumTotal.Text = totalPrice.ToString();
            lbApprovalSumTotal.Text = approvalTotalPrice.ToString();
            sumOther = (decimal)(Convert.ToDecimal(tbMeasureCost.Text.Trim()) + Convert.ToDecimal(tbGuiCost.Text.Trim()) + Convert.ToDecimal(tbTaxCost.Text.Trim()) + Convert.ToDecimal(tbTrafficCost.Text.Trim()) + Convert.ToDecimal(tbOtherCost.Text.Trim()));
            approvalSumOther = (decimal)(Convert.ToDecimal(tbApprovalMeasureCost.Text.Trim()) + Convert.ToDecimal(tbApprovalGuiCost.Text.Trim()) + Convert.ToDecimal(tbApprovalTaxCost.Text.Trim()) + Convert.ToDecimal(tbApprovalTrafficeCost.Text.Trim()) + Convert.ToDecimal(tbApprovalOtherCost.Text.Trim()));
            lbSumOther.Text = sumOther.ToString();
            lbApprovalSumOther.Text = approvalSumOther.ToString();
            lbSumAll.Text = ((decimal)(Convert.ToDecimal(lbSumTotal.Text.Trim()) + sumOther)).ToString();
            lbApprovalSumAll.Text = ((decimal)(Convert.ToDecimal(lbApprovalSumTotal.Text.Trim()) + approvalSumOther)).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v5", "alert('错误，输入的格式不正确！(" + ex.Message + ")')", true);
        }
    }
    protected void tbMeasureCost_TextChanged2(object sender, EventArgs e)
    {
        decimal sumOther = 0;
        decimal approvalSumOther = 0;
        try
        {

            decimal totalPrice = 0;
            decimal approvalTotalPrice = 0;
            foreach (EquipmentCostItems item in EqCostItems)
            {
                totalPrice += item.EqTotalPrice;
                //approvalTotalPrice += item.EqApprovalTotalPrice;
            }
            decimal a = 0, b = 0, c = 0, d = 0, ee = 0, f = 0, g = 0, h = 0;
            a = Convert.ToDecimal(tbApprovalMeasureCost.Text.Trim());
            EqCostInfor.MeasureApprovalCost = a;
            b = Convert.ToDecimal(tbApprovalGuiCost.Text.Trim());
            EqCostInfor.GuiApprovalCost = b;
            c = Convert.ToDecimal(tbApprovalTaxCost.Text.Trim());
            EqCostInfor.TaxApprovalCost = c;
            d = Convert.ToDecimal(tbApprovalTrafficeCost.Text.Trim());
            EqCostInfor.TrafficApprovalCost = d;
            h = Convert.ToDecimal(tbApprovalOtherCost.Text.Trim());
            g = a + b + c + d + h;
            EqCostInfor.SumApprovalOtherCost = g;
            ee = 0;
            f = 0;
            FM2E.Model.Workflow.WorkflowInstanceInfo tempWii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, CurrentSheet.SheetID);

            if (tempWii.CurrentStateName == stateName4EqCostModify)
            {
                for (int ii = 0; ii < rpEquipmentItems.Items.Count; ii++)
                {
                    if ((((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Text.Trim()) != null)
                    {
                        EqCostInfor.EqCostItems[ii].EqApprovalUnitPrice = Convert.ToDecimal(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalUnitPrice"))).Text.Trim());
                        decimal templbEqnum = Convert.ToDecimal(((Label)(rpEquipmentItems.Items[ii].FindControl("lbEqNum"))).Text.Trim());
                        ((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Text = Convert.ToString(templbEqnum * EqCostInfor.EqCostItems[ii].EqApprovalUnitPrice);
                    }
                    if ((((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Text.Trim()) != null)
                    {
                        ee = Convert.ToDecimal(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqApprovalTotalPrice"))).Text.Trim());
                    }
                    EqCostInfor.EqCostItems[ii].EqApprovalTotalPrice = ee;
                    f += ee;
                    if ((((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Text.Trim()) != null)
                    {
                        EqCostInfor.EqCostItems[ii].EqRemark = Convert.ToString(((TextBox)(rpEquipmentItems.Items[ii].FindControl("tbEqRemark"))).Text.Trim());
                    }
                }
            }

            approvalTotalPrice = f;
            EqCostInfor.TotalSumApprovalCost = f + g;
            ecBLL.UpdateEquipmentCostInfor(EqCostInfor);

            lbSumTotal.Text = totalPrice.ToString();
            lbApprovalSumTotal.Text = approvalTotalPrice.ToString();
            sumOther = (decimal)(Convert.ToDecimal(tbMeasureCost.Text.Trim()) + Convert.ToDecimal(tbGuiCost.Text.Trim()) + Convert.ToDecimal(tbTaxCost.Text.Trim()) + Convert.ToDecimal(tbTrafficCost.Text.Trim()) + Convert.ToDecimal(tbOtherCost.Text.Trim()));
            approvalSumOther = (decimal)(Convert.ToDecimal(tbApprovalMeasureCost.Text.Trim()) + Convert.ToDecimal(tbApprovalGuiCost.Text.Trim()) + Convert.ToDecimal(tbApprovalTaxCost.Text.Trim()) + Convert.ToDecimal(tbApprovalTrafficeCost.Text.Trim()) + Convert.ToDecimal(tbApprovalOtherCost.Text.Trim()));
            lbSumOther.Text = sumOther.ToString();
            lbApprovalSumOther.Text = approvalSumOther.ToString();
            lbSumAll.Text = ((decimal)(Convert.ToDecimal(lbSumTotal.Text.Trim()) + sumOther)).ToString();
            lbApprovalSumAll.Text = ((decimal)(Convert.ToDecimal(lbApprovalSumTotal.Text.Trim()) + approvalSumOther)).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v5", "alert('错误，输入的格式不正确！(" + ex.Message + ")')", true);
        }
    }

    protected void rpEquipmentItems_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "delEq")
        {
            EqCostItems.RemoveAt(index);
        }
        FillCostItemsData();
    }



    private void FillCostItemsData()
    {
        rpEquipmentItems.DataSource = EqCostItems;
        rpEquipmentItems.DataBind();

        decimal totalPrice = 0;
        decimal approvalTotalPrice = 0;
        foreach (EquipmentCostItems item in EqCostItems)
        {
            totalPrice += item.EqTotalPrice;
            approvalTotalPrice += item.EqApprovalTotalPrice;
        }
        lbSumTotal.Text = totalPrice.ToString();
        lbApprovalSumTotal.Text = approvalTotalPrice.ToString();
        decimal sumOther = 0;
        decimal ApprovalSumOther = 0;
        try
        {
            sumOther = (decimal)(Convert.ToDecimal(tbMeasureCost.Text.Trim()) + Convert.ToDecimal(tbGuiCost.Text.Trim()) + Convert.ToDecimal(tbTaxCost.Text.Trim()) + Convert.ToDecimal(tbTrafficCost.Text.Trim()) + Convert.ToDecimal(tbOtherCost.Text.Trim()));
            ApprovalSumOther = (decimal)(Convert.ToDecimal(tbApprovalMeasureCost.Text.Trim()) + Convert.ToDecimal(tbApprovalGuiCost.Text.Trim()) + Convert.ToDecimal(tbApprovalTaxCost.Text.Trim()) + Convert.ToDecimal(tbApprovalTrafficeCost.Text.Trim()) + Convert.ToDecimal(tbApprovalOtherCost.Text.Trim()));
            lbSumOther.Text = sumOther.ToString();
            lbApprovalSumOther.Text = ApprovalSumOther.ToString();
            lbSumAll.Text = ((decimal)(totalPrice + sumOther)).ToString();
            lbApprovalSumAll.Text = ((decimal)(approvalTotalPrice + ApprovalSumOther)).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v4", "alert('错误，输入的格式不正确！(" + ex.Message + ")')", true);
        }
    }
    protected void Check(object sender, EventArgs e)
    {

    }

    protected void rptHistoryEquipments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            MaintainedEquipmentInfo MaintainEquipments = (MaintainedEquipmentInfo)e.Item.DataItem;
            Literal lt = (Literal)e.Item.FindControl("lbEqNO");
            if (lt != null)
            {
                EquipmentInfoFacade Eqitem = EqBll.GetEquipmentBYNO(MaintainEquipments.EquipmentNO);
                lt.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看设备信息','{0}Module/FM2E/DeviceManager/DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/ViewDeviceInfo.aspx?cmd=view&id={1}&companyid=SG&index=',800, 430, null,true,true);", Page.ResolveUrl("~/"), Eqitem.EquipmentID), Eqitem.EquipmentNO);
            }
        }

    }

    public int ExecuteNonQuery(string Sql, params SqlParameter[] parameters)
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
    public static object ExecuteScalar(string Sql, params SqlParameter[] parameters)
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
    protected void uploadfile_Click(object sender, EventArgs e)
    {
        FileUpLoadCommon fileUtility_ArchivesAttachment = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_ArchivesAttachmentFile.HasFile)
        {
            if (fileUtility_ArchivesAttachment.SaveFile(FileUpload_ArchivesAttachmentFile, false))
            {
                HiddenField1.Value = FileUpload_ArchivesAttachmentFile.FileName;
                HiddenField2.Value = SystemConfig.Instance.UploadPath + UPLOADFOLDER + fileUtility_ArchivesAttachment.NewFileName;
                uploadname.Text = FileUpload_ArchivesAttachmentFile.FileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败：" + fileUtility_ArchivesAttachment.ErrorMsg, new FM2E.Model.Exceptions.WebException(fileUtility_ArchivesAttachment.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
}
