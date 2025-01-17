﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
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
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.WorkflowLayer;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionMaintain_MalfunctionHandle : System.Web.UI.Page
{
    protected int CountPerRow = 5;
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private string from = (string)Common.sink("from", MethodType.Get, 20, 1, DataType.Str);
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Department deptBll = new Department();
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    private readonly Address addressBll = new Address();
    private readonly EquipmentSystem systemBll = new EquipmentSystem();
    private readonly Equipment equipmentBll = new Equipment();
    private readonly ConsumableEquipment consumablebll = new ConsumableEquipment();
    private readonly SubsidiaryEquipment subsidiarybll = new SubsidiaryEquipment();
    private readonly EquipmentCost ecBLL = new EquipmentCost();
    private string addressname = "";
    private DepartmentType tempDeptType = 0 ;
    //private string tempSystemID = "";
    private bool delayflag = false;
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

    public bool changemark
    {
        get
        {
            if (ViewState["changemark"] == null)
            {
                return false;
            }
            return (bool)ViewState["changemark"];
        }
        set
        {
            ViewState["changemark"] = value;
        }
    }
    public string tempSystemID
    {
        get
        {
            if (ViewState["tempSystemID"] == null)
            {
                return "";
            }
            return (string)ViewState["tempSystemID"];
        }
        set
        {
            ViewState["tempSystemID"] = value;
        }
    }
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
    /// 已维修的设备零件列表
    /// </summary>
    private ArrayList MaintainedEquipmentsParts
    {
        get
        {
            if (Session["MaintainedEquipmentsParts"] == null)
                return new ArrayList();
            return (ArrayList)Session["MaintainedEquipmentsParts"];
        }
        set
        {
            Session["MaintainedEquipmentsParts"] = value;
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
            FillStaffData();
            FillEquipmentPartGridView();

            //FillEquipmentData();
            //FillSubsidiaryEquipmentData();
            //FillConsumableEquipmentData();

            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            //PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
        BindButton();
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    //private void PermissionControl()
    //{
    //    bool bEdit = SystemPermission.CheckButtonPermission(PopedomType.Edit);
    //    btAccept.Visible = bEdit;
    //    btSave.Visible = bEdit;
    //    gvMaintainedEquipments.Columns[gvMaintainedEquipments.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
    //}
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    private void InitialPage()
    {
        chedang_bt.PostBackUrl = Page.ResolveUrl("~") + "Module/FM2E/MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?chedang=true&cmd=view&id="+id;

        tbAddress.Attributes.Add("onclick", string.Format("javascript:showPopWin('地址选择','{0}Module/FM2E/BasicData/AddressManage/Address.aspx?operator=select',700, 400, RecordAddress,false,true);document.getElementById('{1}').click();", Page.ResolveUrl("~"), Button_OK.ClientID));

        ListItem[] repairSituations = EnumHelper.GetListItemsEx(typeof(RepairSituation), (int)RepairSituation.CompletelyFixed, (int)RepairSituation.Unknown, (int)RepairSituation.FunctionalityFixed, (int)RepairSituation.SecondRepaired);
        ListItem[] maintainStatuses = EnumHelper.GetListItemsEx(typeof(MaintainedEquipmentStatus), (int)MaintainedEquipmentStatus.Fixed, (int)MaintainedEquipmentStatus.Unknown);

        rblRepairSituation.Items.Clear();
        rblRepairSituation.Items.AddRange(repairSituations);

        ddlStatus.Items.Clear();
        ddlStatus.Items.AddRange(maintainStatuses);

        Warehouse warehousebll = new Warehouse();
        IList<WarehouseInfo> warehouselist = warehousebll.GetAllWarehouse();

        ddlCurrentEquipmentWarehouse.Items.Clear();
        ddlCurrentEquipmentWarehouse.Items.Add(new ListItem("请选择仓库", "0"));
        foreach (WarehouseInfo item in warehouselist)
        {
            ddlCurrentEquipmentWarehouse.Items.Add(new ListItem(item.Name, item.WareHouseID.ToString()));
        }

        ddlCurrentEquipmentStatus.Items.Clear();
        ddlCurrentEquipmentStatus.Items.Add(new ListItem(EnumHelper.GetDescription(MaintainedEquipmentStatus.Wait4Repair), ((int)MaintainedEquipmentStatus.Wait4Repair).ToString()));
        ddlCurrentEquipmentStatus.Items.Add(new ListItem(EnumHelper.GetDescription(MaintainedEquipmentStatus.Scrapped), ((int)MaintainedEquipmentStatus.Scrapped).ToString()));

        //系统列表
        //ddlSystem.Items.Clear();
        //ddlSystem.Items.Add(new ListItem("请选择系统", "0"));
        //ddlSystem.Items.AddRange(ListItemHelper.GetSystemListItems());

        //清除session
        Session.Remove("MaintainedEquipments");
        Session.Remove("MaintainedEquipmentsParts");
        //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
        //btSave.Attributes.Add("onclick", "javascript:return checkForm(document.forms[0],true)&&confirm('未添加任何经过维修的设备，确认提交?')");
        //**********Modification Finished 2011-6-27**********************************************************************************************

        CurentStaffList = null;
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
            if (item.Status == MalfunctionHandleStatus.Waiting4Mark || item.Status == MalfunctionHandleStatus.ReturnModify)
                //if (item.Status != MalfunctionHandleStatus.Waiting4Accept)
            {
                //已受理
                tableRepairRecord.Visible = true;
                btSave.Visible = true;
                btAccept.Visible = false;
                //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
                btTrans.Visible = false;
                ddlMaintainTeam.Visible = false;
                ddlMaintainTeamType.Visible = false;
                //ddlApproveler.Visible = false;
                repairDiv.Visible = true;
                pnMoneyStatistics.Visible = true;
                //**********Modification Finished 2011-6-27**********************************************************************************************
            }
            else
            {
                tableRepairRecord.Visible = false;
                btSave.Visible = false;
                btAccept.Visible = true;
                //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
                btTrans.Visible = true;
                ddlMaintainTeam.Visible = true;
                ddlMaintainTeamType.Visible = true;
                //ddlApproveler.Visible = true;
                repairDiv.Visible = false;
                pnMoneyStatistics.Visible = false;
                //**********Modification Finished 2011-6-27**********************************************************************************************
            }

            CompanyInfo company = companyBll.GetCompany(item.CompanyID);
            if (company != null)
                lbCompany.Text = company.CompanyName;
            lbSheetNO.Text = item.SheetNO;
            lbDepartment.Text = item.DepartmentName;
            lbReporter.Text = item.Reporter;
            lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd HH:mm");  //时间显示到分钟 by L 4-23
            //lbAddressDetail.Text = item.AddressDetail;

            //repeatEquipments.DataSource = item.FaultyEquipments;
            //repeatEquipments.DataBind();

            addressname = item.AddressName;
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
            TB_RepairedTime.Text = item.RepairTime.ToString();
            DDL_RepairedUnit.SelectedValue = ((int)item.RepairUnit).ToString();

            //if (item.Editor != null)
            //    editor.Text = item.Editor + " " + Common.Get_UserName;
            //else
                editor.Text = Common.Get_UserName;

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
                item.Editreason = item.Editreason.Replace(',', '，');
                string[] split = { separatorStr };
                string[] seditreason = item.Editreason.Split(split, StringSplitOptions.None);

            if (seditreason[0] != null)
                editreason.Text = seditreason[0];



            string eqmno = item.AddressDetail.Split('@')[0];
            if (eqmno != null || eqmno != "")
            {
                EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);

                lbEqName.Text = equipmentitem.Name;
                lbEqSystem.Text = equipmentitem.SystemName;
                lbEqNo.Text = equipmentitem.EquipmentNO;
                tempSystemID = equipmentitem.SystemID;
            }


            
            DepartmentInfo itemDeptInfor = deptBll.GetDepartment(item.MaintainDept);
            tempDeptType = itemDeptInfor.DepartmentType;
            if (itemDeptInfor.DepartmentType == DepartmentType.MaintainTeam)      //自维
            {
                pnMoneyStatistics.Visible = false;
                ListItem[] repairSituations = EnumHelper.GetListItemsEx(typeof(RepairSituation), (int)RepairSituation.CompletelyFixed, (int)RepairSituation.Unknown, (int)RepairSituation.FunctionalityFixed);
                rblRepairSituation.Items.Clear();
                rblRepairSituation.Items.AddRange(repairSituations);
                if (itemDeptInfor.DepartmentID.ToString() == FM2E.BLL.System.ConfigItems.SelfMaintianMaintainDeptID)   //自维 二次维修不可删除记录
                {
                    for (int ii = 0; ii < rptMaintainHistory.Items.Count; ii++)
                    {
                        ((ImageButton)(rptMaintainHistory.Items[ii].FindControl("itemDeptInfor"))).Visible = false;
                    }
                }
            }
            if (itemDeptInfor.DepartmentType == DepartmentType.MaintainTeamOther)      //外维
            {
               // pnMoneyStatistics.Visible = true ;        // 屏蔽费用统计项目，将其部分转到外维经理审批的流程上 [4/12/2012 L]
                //Get EquipmentCostInfor Data
                EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(item.SheetID);
                if (ecInfor != null)
                {
                    EqCostItems = ecInfor.EqCostItems;
                    tbMeasureCost.Text = ecInfor.MeasureCost.ToString();
                    //tbMeasureCostMark.Text = ecInfor.MeasureCostMark.ToString();

                    tbGuiCost.Text = ecInfor.GuiCost.ToString();
                    tbTaxCost.Text = ecInfor.TaxCost.ToString();
                    tbTrafficCost.Text = ecInfor.TrafficCost.ToString();
                    //tbMeasureCostMark.Text = ecInfor.MeasureCostMark.ToString();//  [3/30/2012 L]
                }
                FillCostItemsData();
            }
             //********** Modification Finished 2011-11-28 **********************************************************************************************

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

            gvMaintainedEquipments.DataSource = new ArrayList();
            gvMaintainedEquipments.DataBind();

            //填充故障处理历史列表
            try
            {
                IList maintainHistory = malfunctionBll.GetMaintainHistory(item.SheetID);
                rptMaintainHistory.DataSource = maintainHistory;
                rptMaintainHistory.DataBind();

                //ArrayList maintainList = new ArrayList();
                //foreach (MalfuncitonMaintainInfo mmi in maintainHistory)
                //{
                //    maintainList.AddRange(mmi.MaintainedEquipments);
                //}
                //MaintainedEquipments = maintainList;
                //BindDataToGridView();
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理历史失败，原因：" + ex);
            }

            //检查进入当前页面的人员的部门信息是否与维修单上的维修单位一致，不一致的话，需要给出警告
            //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
            if (UserData.CurrentUserData.DepartmentID == item.MaintainDept || itemDeptInfor.DepartmentType==DepartmentType.MaintainTeam)
            {
                btAccept.Attributes.Add("onclick", "javascript:return confirm('确认要受理此故障吗?');");
                btTrans.Attributes.Add("onclick", "javascript:return checkForm(document.forms[0],true)&&confirm('确定要转移该维修单?')");                         //new
                btSave.Attributes.Add("onclick", "javascript:return checkForm(document.forms[0],true)&&confirm('未添加任何经过维修的设备，确认提交?')");          //new
                //btAccept.Enabled = true;                                                                                                                          //new
                //btTrans.Enabled = true;                                                                                                                           //new    
                //btSave.Enabled = true;                                                                                                                            //new
                //btReturn.Enabled = true;                                                                                                                          //new
            }
            else
            {
                btAccept.Attributes.Add("onclick", string.Format("javascript:return confirm('警告：这是 \"{0}\" 的故障处理单，你无权受理！')&&false;", item.MaintainDeptName));
                btTrans.Attributes.Add("onclick", string.Format("javascript:return checkForm(document.forms[0],true)&&confirm('警告：这是 \"{0}\" 的故障处理单，你无权转移！')&&false;", item.MaintainDeptName));  //new
                btSave.Attributes.Add("onclick", string.Format("javascript:return confirm('警告：这是 \"{0}\" 的故障处理单，你无权登记！')&&false;", item.MaintainDeptName));          //new
                btReturn.Attributes.Add("onclick", string.Format("javascript:return confirm('警告：这是 \"{0}\" 的故障处理单，你无权撤单！')&&false;", item.MaintainDeptName));        //new
                //btAccept.Enabled = false;                                                                                                                           //new
                //btTrans.Enabled = false;                                                                                                                            //new
                //btSave.Enabled = false;                                                                                                                             //new
                //btReturn.Enabled = false;                                                                                                                           //new
            }
            //**********Modification Finished 2011-6-27**********************************************************************************************
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
            //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************

            MalfunctionHandleInfo model = malfunctionBll.GetMalfunctionSheet(id);
            if ((int)model.Status < (int)MalfunctionHandleStatus.Accepted)   //未受理前
            {
                #region 添加处理记录
                //string separatorStr = "@First@";
                //if (model.Editreason != null)
                //{
                //    if (!model.Editreason.Contains(separatorStr))
                //    {
                //        model.Editreason += " " + separatorStr + " ";
                //    }
                //}
                //else
                //{
                //    model.Editreason += " " + separatorStr + " ";
                //}
                //string[] split = { separatorStr };
                //string[] editreason = model.Editreason.Split(split, StringSplitOptions.None);
                //editreason[1] += " → " + UserData.CurrentUserData.PersonName + "(受理)";
                //model.Editreason = editreason[0] + separatorStr + editreason[1];
                #endregion


                //********** Modification Finished 2011-11-28 **********************************************************************************************
                model.Receiver = Common.Get_UserName;
                model.ActualResponseTime = (int)Math.Ceiling((DateTime.Now - model.ReportDate).TotalSeconds / 60);
                model.UpdateTime = DateTime.Now;
                model.ReceiveDate = DateTime.Now;
                model.Status = MalfunctionHandleStatus.Accepted;
                //2013-1-20 model.MaintainDept = UserData.CurrentUserData.DepartmentID;
                malfunctionBll.UpdateMalfunctionSheetBasicData(model);

                //**********Modified by Xue 2011-6-27****************************************************************************************************
                //FM2E.BLL.PendingOrder.PendingOrder pobll = new FM2E.BLL.PendingOrder.PendingOrder();
                //string lastURL = Request.Url.AbsolutePath + "?" + Request.QueryString.ToString();
                //if (lastURL.Contains("/Web/Module/FM2E"))
                //{
                //    lastURL = lastURL.Replace("/Web/Module/FM2E", "..");
                //}
                //if (lastURL.Contains("/Module/FM2E"))
                //{
                //    lastURL = lastURL.Replace("/Module/FM2E", "..");
                //}
                //pobll.MarkReadByURL(lastURL);
                //**********Modification Finished 2011-6-27**********************************************************************************************


                //FillData();

                //tableRepairRecord.Visible = true;
                //btSave.Visible = true;
                //btAccept.Visible = false;
                //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
                //btTrans.Visible = false;
                //ddlMaintainTeam.Visible = false;
                //ddlMaintainTeamType.Visible = false;
                
                //**********Modification Finished 2011-6-27**********************************************************************************************
                int i = 0;
                i = Convert.ToInt32(ExecuteScalar("select ID from FM2E_PendingOrder where title like '%提交了%" + model.SheetNO + "%'"));
                if (i>0)
                {
                    ExecuteNonQuery("Update  FM2E_PendingOrderReceiver set HasRead = 1 where ID =(select ID from FM2E_PendingOrder where title like '%提交了%" + model.SheetNO + "%')");                    
                }

                //受理完之后返回列表
                //EventMessage.MessageBox(Msg_Type.Info, "操作成功", "受理成功，该故障单已被受理！", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");


                    //查询
                   
            }
            else   //此单已受理
            {
                //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "受理失败，该故障单已被受理！", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "受理失败，该故障单已被受理！", Icon_Type.Error, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "受理故障失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "受理成功，该故障单已被受理！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
    }
    /// <summary>
    /// 保存维修信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        bool finishFlag = false;
        delayflag = false;

        bool isReturn = false;

        

        //检验故障处理情况填写是否要求
        if (tbMaintenanceDescription.Text.Length <= 10)
        {
            lgerrmsg.Text = "错误：故障详细描述不能少于10个字";
            return;
        }
        if (tbMaintenanceMethod.Text.Length <= 10)
        {
            lgerrmsg.Text = "错误：故障处理办法不能少于10个字";
            return;
        }
        if (MaintainedEquipments.Count <= 0 && noequipment.Value.Equals(""))
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "请至少填写设备或非设备故障中的一样", new WebException("请至少填写设备或非设备故障中的一样"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }


        isReturn = CurrentMalfunctionSheet.Status == MalfunctionHandleStatus.ReturnModify ? true : false;

        try
        {
            MalfunctionHandleInfo model = CurrentMalfunctionSheet;

            

            //malfunctionBll.UpdateMalfunctionSheetBasicData(model);

            MalfuncitonMaintainInfo maintainInfo = new MalfuncitonMaintainInfo();

            RepairSituation repairSituation = (RepairSituation)Convert.ToInt32(rblRepairSituation.SelectedValue);

            #region 完全修复
            if (repairSituation == RepairSituation.CompletelyFixed)                                             
            {
                if (model.ActualRepairTime == 0)
                {
                    if (DateTime.Now < model.ReportDate2) model.ActualRepairTime = 0;
                    else
                        model.ActualRepairTime = (int)Math.Ceiling((DateTime.Now - model.ReportDate2).TotalSeconds / 60); //Waiting4MoneyApproval
                }
                //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************

                DepartmentType currentDeptType = deptBll.GetDepartment(model.MaintainDept).DepartmentType;
                tempDeptType = currentDeptType;
                model.Status = MalfunctionHandleStatus.Waiting4MoneyApproval;
                if (currentDeptType == DepartmentType.MaintainTeam) //自维
                {
                    model.Status = MalfunctionHandleStatus.Wait4EngineerCheck;
                }
                finishFlag = true;

                if (currentDeptType == DepartmentType.MaintainTeamOther)               //外维
                {
                    EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(model.SheetID);
                    if (ecInfor == null)  // Add
                    {
                        ecInfor = new EquipmentCostInfor();
                        ecInfor.EqCostItems = EqCostItems;
                        ecInfor.SheetID = model.SheetID;

                        ecInfor.EqSumPrice = Convert.ToDecimal(lbSumTotal.Text);
                        ecInfor.EqSumApprovalPrice = 0;

                        ecInfor.MeasureCost = Convert.ToDecimal(tbMeasureCost.Text.Trim());
                        //ecInfor.MeasureCostMark = Convert.ToString(tbMeasureCostMark.Text.Trim());

                        ecInfor.GuiCost = Convert.ToDecimal(tbGuiCost.Text.Trim());
                        ecInfor.TaxCost = Convert.ToDecimal(tbTaxCost.Text.Trim());
                        ecInfor.TrafficCost = Convert.ToDecimal(tbTrafficCost.Text.Trim());
                        ecInfor.SumOtherCost = Convert.ToDecimal(lbSumOther.Text);
                        //ecInfor.MeasureCostMark = Convert.ToString(tbMeasureCostMark.Text.Trim());



                        ecInfor.MeasureApprovalCost = 0;
                        ecInfor.GuiApprovalCost = 0;
                        ecInfor.TaxApprovalCost = 0;
                        ecInfor.TrafficApprovalCost = 0;
                        ecInfor.SumApprovalOtherCost = 0;

                        ecInfor.OtherCost = 0;
                        ecInfor.OtherApprovalCost = 0;
                        ecInfor.MarkOne = " ";
                        ecInfor.MarkTwo = " ";
                        ecInfor.MarkThree = " ";
                        ecInfor.MarkFour = " ";
                        ecInfor.MarkFive = " ";

                        ecInfor.TotalSumCost = Convert.ToDecimal(lbSumAll.Text);
                        ecInfor.TotalSumApprovalCost = 0;

                        ecBLL.AddEquipmentCostInfor(ecInfor);
                    }
                    else
                    {
                        ecInfor.EqCostItems = EqCostItems;

                        ecInfor.EqSumPrice = Convert.ToDecimal(lbSumTotal.Text);

                        ecInfor.MeasureCost = Convert.ToDecimal(tbMeasureCost.Text.Trim());
                        ecInfor.GuiCost = Convert.ToDecimal(tbGuiCost.Text.Trim());
                        ecInfor.TaxCost = Convert.ToDecimal(tbTaxCost.Text.Trim());
                        ecInfor.TrafficCost = Convert.ToDecimal(tbTrafficCost.Text.Trim());
                        ecInfor.SumOtherCost = Convert.ToDecimal(lbSumOther.Text);
                        //ecInfor.MeasureCostMark = Convert.ToString(tbMeasureCostMark.Text.Trim());

                        ecInfor.TotalSumCost = Convert.ToDecimal(lbSumAll.Text);

                        ecBLL.UpdateEquipmentCostInfor(ecInfor);
                    }
                }

                #region 添加处理记录
                //string separatorStr = "@First@";
                //if (model.Editreason != null)
                //{
                //    if (!model.Editreason.Contains(separatorStr))
                //    {
                //        model.Editreason += " " + separatorStr + " ";
                //    }
                //}
                //else
                //{
                //    model.Editreason += " " + separatorStr + " ";
                //}
                //string[] split = { separatorStr };
                //string[] editreason = model.Editreason.Split(split, StringSplitOptions.None);
                //editreason[1] += " → " + UserData.CurrentUserData.PersonName + "(登记)";
                //model.Editreason = editreason[0] + separatorStr + editreason[1];
                #endregion

                //********** Modification Finished 2011-11-28 **********************************************************************************************
            }
            #endregion

            #region 功能性修复
            else if (repairSituation == RepairSituation.FunctionalityFixed)                                     
            {
                if (model.ActualFunRestoreTime == 0)    //以第一次功能性修复时间为准
                    model.ActualFunRestoreTime = (int)Math.Ceiling((DateTime.Now - model.ReportDate).TotalSeconds / 60);
                model.Status = MalfunctionHandleStatus.FunctionalityRestore;
            }
            #endregion

            if (editreason.Text != null && editreason.Text.Trim() != "")      //延迟修复时间
            {
                delayflag = true;
                model.IsDelayApply = true;
                model.DelayApplyTime = DateTime.Now;
                model.DelayForTime = Convert.ToInt32(TB_RepairedTime.Text);
                model.DelayForUnit = (TimeUnits)Convert.ToInt32(DDL_RepairedUnit.SelectedValue);
                model.Editor = editor.Text;
                string separatorStr = "@First@";
                if (model.Editreason != null)
                {
                    if (!model.Editreason.Contains(separatorStr))
                    {
                        model.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
                    }
                }
                else
                {
                    model.Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
                }
                string[] split = { separatorStr };
                string[] seditreason = model.Editreason.Split(split, StringSplitOptions.None);
                seditreason[0] = editreason.Text.Trim();
                model.Editreason = seditreason[0] + separatorStr + seditreason[1] + separatorStr + seditreason[2];

                model.Status = MalfunctionHandleStatus.Delay;
            }
          

            maintainInfo.MaintainedEquipments = MaintainedEquipments;
            //maintainInfo.MaintenanceDetail = tbMaintenanceDetail.Text.Trim();
            maintainInfo.MaintenanceDetail = "";
            maintainInfo.MaintenanceDescription = tbMaintenanceDescription.Text.Trim().Replace(',', '，');
            maintainInfo.NoEquipment = noequipment.Value;
            maintainInfo.MaintenanceMethod = tbMaintenanceMethod.Text.Trim().Replace(',', '，');
            maintainInfo.MaintenanceStaff = Common.Get_UserName;
            maintainInfo.RepairSituation = repairSituation;
            maintainInfo.SheetID = id;
            maintainInfo.TotalFee = TotalMaintainFee;
            maintainInfo.UpdateTime = DateTime.Now;
            maintainInfo.MaintainStaff = CurentStaffList;

            if (Convert.ToInt32(rbDelivered.SelectedValue) == 1)  //是否送修
            {
                model.IsDelivered = true;
                maintainInfo.IsDelivered = true;
            }
            else
            {
                model.IsDelivered = false;
                maintainInfo.IsDelivered = false;
            }
            if (maintainInfo.MaintainedEquipments.Count > 1 && changemark == false)  //进行分单处理
            {
                string sheetno = model.SheetNO;
                int sheetnum = 1;
                foreach (MaintainedEquipmentInfo item in maintainInfo.MaintainedEquipments)  //分单 更新原单表单号，并增加相关数量的维修单
                {
                    if (sheetnum == 1)
                    {
                        MalfunctionHandleInfo malfunctionhandle_temp_item = MalfunctionHandleInfo.CloneObject(model);  //深复制
                        MalfuncitonMaintainInfo malfuncitonmaintain_temp_item = MalfuncitonMaintainInfo.CloneObject(maintainInfo);  //深复制
                        malfunctionhandle_temp_item.SheetNO = sheetno + (char)(64 + sheetnum);  //更新表单号
                        IList tempequipment = new ArrayList();
                        tempequipment.Add(item);
                        malfuncitonmaintain_temp_item.MaintainedEquipments = tempequipment;
                        malfuncitonmaintain_temp_item.TotalFee = item.MaintainFee;
                        malfunctionBll.AddMaintainRecord(malfunctionhandle_temp_item, malfuncitonmaintain_temp_item);  //更新
                    }
                    else
                    {
                        MalfunctionHandleInfo malfunctionhandle_temp_item = MalfunctionHandleInfo.CloneObject(model);  //深复制
                        MalfuncitonMaintainInfo malfuncitonmaintain_temp_item = MalfuncitonMaintainInfo.CloneObject(maintainInfo);  //深复制
                        malfunctionhandle_temp_item.SheetNO = sheetno + (char)(64 + sheetnum);  //更新表单号
                        long malfunctionhandleid = malfunctionBll.AddMalfunctionSheet(malfunctionhandle_temp_item);  //增加维修单
                        malfunctionhandle_temp_item.SheetID = malfunctionhandleid;
                        malfuncitonmaintain_temp_item.SheetID = malfunctionhandleid;
                        IList tempequipment = new ArrayList();
                        item.SheetID = malfunctionhandleid;
                        tempequipment.Add(item);
                        malfuncitonmaintain_temp_item.MaintainedEquipments = tempequipment;
                        malfuncitonmaintain_temp_item.TotalFee = item.MaintainFee;
                        malfunctionBll.AddMaintainRecord(malfunctionhandle_temp_item, malfuncitonmaintain_temp_item);  //更新
                    }
                    sheetnum++;
                }
            }
            else
            {
                //更换设备信息
                if (changemark == true)
                {
                    MaintainedEquipmentInfo item1 = (MaintainedEquipmentInfo)maintainInfo.MaintainedEquipments[0];
                    MaintainedEquipmentInfo item2 = (MaintainedEquipmentInfo)maintainInfo.MaintainedEquipments[1];

                    if (item2.MaintainResult == MaintainedEquipmentStatus.Replace)
                    {
                        EquipmentInfoFacade equipmentitem1 = equipmentBll.GetEquipmentBYNO(item1.EquipmentNO);
                        EquipmentInfoFacade equipmentitem2 = equipmentBll.GetEquipmentBYNO(item2.EquipmentNO);
                        //修改更换设备的地址
                        equipmentitem2.AddressCode = equipmentitem1.AddressCode;
                        equipmentitem2.AddressID = equipmentitem1.AddressID;
                        equipmentitem2.LastAddress = equipmentitem2.AddressName;
                        equipmentitem2.AddressName = equipmentitem1.AddressName;
                        equipmentitem2.AddressType = equipmentitem1.AddressType;
                        //*******************************************By L 2012-9更换设备时替换相关的资产编号，换下的资产号清空*****************

                        equipmentitem2.AssertNumber = equipmentitem1.AssertNumber;
                        equipmentitem1.AssertNumber = "";



                        //********************************************************************************************
                        equipmentitem2.Status = EquipmentStatus.Normal;
                        equipmentBll.UpdateEquipment(equipmentitem2);
                        if (item1.MaintainResult == MaintainedEquipmentStatus.Scrapped)
                        {
                            equipmentitem1.Status = EquipmentStatus.Scrapped;
                        }
                        else if (item1.MaintainResult == MaintainedEquipmentStatus.Wait4Repair)
                        {
                            equipmentitem1.Status = EquipmentStatus.Failure;
                        }
                        Warehouse warehousebll = new Warehouse();
                        WarehouseInfo warehouseitem = warehousebll.GetWarehouse(ddlCurrentEquipmentWarehouse.SelectedValue);
                        equipmentitem1.AddressCode = warehouseitem.AddressCode;
                        equipmentitem1.AddressID = warehouseitem.AddressID;
                        equipmentitem1.LastAddress = equipmentitem1.AddressName;
                        equipmentitem1.AddressName = warehouseitem.AddressName;
                        equipmentitem1.AddressType = AddressType.Warehouse;
                        // 将设备入库状态整理为缓存状态设备字段warming=9999 [5/27/2013 Tvk]
                        equipmentitem1.Warming = 9999;
                        //  [5/27/2013 Tvk]
                        equipmentBll.UpdateEquipment(equipmentitem1);
                        // 入库增加记录 [7/31/2013 Genland]
                        InEquipmentsInfo inEquipmentsRecord = new InEquipmentsInfo();
                        inEquipmentsRecord.ID = 777777;//现场维修下来直接进入仓库记录
                        //inEquipmentsRecord.ItemID = ;
                        inEquipmentsRecord.IsAsset = true;
                        inEquipmentsRecord.EquipmentNO = equipmentitem1.EquipmentNO;
                        inEquipmentsRecord.Name = inEquipmentsRecord.EquipmentName = equipmentitem1.Name;
                        inEquipmentsRecord.Model = inEquipmentsRecord.EquipmentModel = equipmentitem1.Model;
                        inEquipmentsRecord.EquipmentType = equipmentitem1.SerialNum;
                        inEquipmentsRecord.Count = 1;
                        inEquipmentsRecord.WarehouseID = warehouseitem.WareHouseID;
                        inEquipmentsRecord.InTime = DateTime.Now;
                        inEquipmentsRecord.Unit = equipmentitem1.Unit;
                        inEquipmentsRecord.Remark = CurrentMalfunctionSheet.SheetNO;
                        InEquipments inEqBll = new InEquipments();
                        inEqBll.InsertInEquipments(inEquipmentsRecord);
                        // 直接添加设备增加入库的记录 [7/31/2013 Genland]
                    }
                }

                if (repairSituation == RepairSituation.SecondRepaired)
                {
                    string temEqNO = CurrentMalfunctionSheet.AddressDetail.Split('@')[0];
                    if (temEqNO != null && temEqNO != "")
                    {
                        EquipmentInfoFacade tempitem = equipmentBll.GetEquipmentBYNO(temEqNO);
                        if (tempitem.SystemID != null && tempitem.SystemID != "")
                        {
                            int temp = 0;
                            temp = Convert.ToInt32(ExecuteScalar("select count(*) from FM2E_User where IM ='" + tempitem.SystemID + "'"));
                            if (temp == 1 || temp == -1)
                            {
                                string tempEngineerName = Convert.ToString(ExecuteScalar("select UserName from FM2E_User where IM ='" + tempitem.SystemID + "'"));
                                if (tempEngineerName != null)
                                {
                                    string R2 = model.Receiver;
                                    model.Receiver = tempEngineerName;
                                    string title = R2 + " 提交了一张自维的二级维修单，请您处理，故障处理单号为：" + model.SheetNO;
                                    string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionSheets.aspx";
                                    string[] receiver = { model.Receiver };
                                    WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
                                }
                            }

                        }


                    }
                }
                malfunctionBll.AddMaintainRecord(model, maintainInfo);
                //**********************************************************
                string nextUserName = "";
                String company = model.MaintainDeptName;
                FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, model.SheetID);
                if (isReturn)
                {
                    if (deptBll.GetDepartment(model.MaintainDept).DepartmentType == DepartmentType.MaintainTeamOther)
                    {
                       
                        if (wii == null)           //新建工作流
                        {
                            Guid guid = WorkflowHelper.CreateNewInstance(model.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                            WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                            finishFlag = true;
                        }
                        else if(wii.CurrentStateName == "ReturnModify")
                        {
                            Guid guid = wii.InstanceID;
                            WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName)[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                            finishFlag = true;
                        }
                    }
                }


                #region

                //兼容旧的单子
                if (wii.CurrentStateName == "Wait4Register")
                {
                    WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, model.SheetID);
                    Guid guid = WorkflowHelper.CreateNewInstance(model.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                }
                else if (wii.CurrentStateName == "Wait4DutyStationConfirm" || wii.CurrentStateName == "Wait4MaintenanceConfirm")
                {
                    string nUser = wii.DelegateUserName;
                    WorkflowHelper.DeleteWorkflowInstance(SGS_MalFunctionWorkflow.TableName, model.SheetID);
                    Guid guid = WorkflowHelper.CreateNewInstance(model.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                    WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                    WorkflowHelper.SetStateAndNextUserMachine3<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, nUser, out nextUserName);
                }

                //string nextUserName = "";
                //if (deptBll.GetDepartment(model.MaintainDept).DepartmentType == DepartmentType.MaintainTeamOther)  //外维
                //{
                //    //model.Status = MalfunctionHandleStatus.Waiting4MoneyApproval;

                //    FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, model.SheetID);
                //    if (wii == null)           //新建工作流
                //    {
                //        String company = model.MaintainDeptName;
                //        Guid guid = WorkflowHelper.CreateNewInstance(model.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                //        WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                //        finishFlag = true;
                //    }
                //    else
                //    {
                //        if (wii.CurrentStateName == "Wait4ElectDeptEngineerConfirm")
                //        {
                //            model.Status = MalfunctionHandleStatus.AnotherCheck;
                //        }
                //        else if (wii.CurrentStateName == "Wait4DutyStationConfirm")
                //        {
                //            finishFlag = true;
                //        }
                //        else if (delayflag)
                //        {
                //        }
                //        else
                //        {
                //            String company = model.MaintainDeptName;

                //            Guid guid = wii.InstanceID;
                //            WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName)[0].Name, SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                //            finishFlag = true;
                //            //finalFinishFlag = true;
                //        }

                //    }

                //}

                #endregion

                //**********************************************************
                



            }
            if (delayflag)
            {
                User userBll = new User();

                int tempRoleID = Convert.ToInt32(ConfigurationManager.AppSettings["DelayFirstApprove"]);
                IList roleUsers = userBll.GetUsers(tempRoleID);
                List<string> tempStationCheckers = new List<string>();
                for (int k = 0; k < roleUsers.Count; k++)
                {
                    tempStationCheckers.Add(((UserRoleInfo)roleUsers[k]).UserName);

                }
                string[] strStationCheckers = tempStationCheckers.ToArray();

                string title = "您有新的故障处理单申请延迟审批，请审批，故障处理单号为：" + model.SheetNO;
                string URL = "../MaintainManager/MalFunctionManager/MalfunctionCheck/MalfunctionCheckList.aspx";
                string[] receiver = { model.Recorder };
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, strStationCheckers);
            }

            else
            {

                if (finishFlag && tempDeptType != DepartmentType.MaintainTeam)  //完成并提交至验收
                {
                    string title = "您提交的故障处理单已登记完成，请进行验收，故障处理单号为：" + model.SheetNO;
                    string URL = "../MaintainManager/MalFunctionManager/MalfunctionCheck/MalfunctionCheckList.aspx";
                    string[] receiver = { model.Recorder };
                    WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
                }
                if (finishFlag && tempDeptType == DepartmentType.MaintainTeam)  //完成并提交至自维工程师验收
                {

                    //通知自维工程师验收
                    string title = "您提交的故障处理单已登记完成，请进行验收，故障处理单号为：" + model.SheetNO;
                    string URL = "../MaintainManager/MalFunctionManager/MalfunctionCheck/InteriorEgMalfunctionCheckList.aspx";
                    IList systemUserlist = userBll.GetUsersByIM(tempSystemID);
                    if (systemUserlist.Count != 0)
                    {
                        string[] strSystemUserList = null;
                        strSystemUserList = new string[systemUserlist.Count];
                        for (int i = 0; i < systemUserlist.Count; i++)
                        {
                            strSystemUserList[i] = ((UserInfo)systemUserlist[i]).UserName;
                        }
                        WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, strSystemUserList);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加维修情况失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        DepartmentType currentDeptType2 = deptBll.GetDepartment(CurrentMalfunctionSheet.MaintainDept).DepartmentType;
        
        if (currentDeptType2==DepartmentType.MaintainTeam)
        {
            if (from.Trim() == "1")
            {
                Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("InteriorMalfunctionList.aspx"))));
            }
            else
            {
                Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("InteriorMyMalfunctionSheets.aspx"))));
            }
        } 
        else
        {
            if (from.Trim() == "1")
            {
                Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("MalfunctionList.aspx"))));
            }
            else
            {
                Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("MyMalfunctionSheets.aspx"))));
            }
        }

        if (from.Trim() == "1")
        {
            Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("MalfunctionList.aspx"))));
        }
        else
        {
            Response.Redirect(string.Format("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id={0}&returnurl={1}", id, Server.HtmlEncode(Common.GetHomeBaseUrl("MyMalfunctionSheets.aspx"))));
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

            if (item.SerialNum == "新件")
            {

                Label tempid = (Label)e.Row.FindControl("serialnum");
                tempid.ForeColor = Color.Blue;
            }
                 
            
            
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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalMaintainFee = 0;//每次postback都会自动初始化
    protected void gvMaintainedEquipmentPart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
            MaintainedEquipmentPartInfo item = e.Row.DataItem as MaintainedEquipmentPartInfo;
            totalMaintainFee += item.MaintainFee;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = System.Drawing.Color.YellowGreen;
            e.Row.Font.Bold = true;
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 2;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            for (int i = 1; i <= 1; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label lbTotal = e.Row.FindControl("lbTotalMaintainFee") as Label;
            if (lbTotal != null)
            {
                TotalMaintainEquipmentPartFee = totalMaintainFee;
                //lbTotalMaintainFee.Text = TotalMaintainEquipmentPartFee.ToString();
                tbMaintainFee.Text = TotalMaintainEquipmentPartFee.ToString();
            }
        }
    }
    /// <summary>
    /// 设备详细个数
    /// </summary>
    private decimal TotalMaintainEquipmentPartFee
    {
        get
        {
            if (ViewState["TotalMaintainEquipmentPartFee"] == null)
                return 0;
            return (decimal)ViewState["TotalMaintainEquipmentPartFee"];
        }
        set
        {
            ViewState["TotalMaintainEquipmentPartFee"] = value;
        }
    }
    /// <summary>
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaintainedEquipmentPart_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridViewEquipmentPart.Rows[Convert.ToInt32(e.CommandArgument)];
        int rowNum = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "del")
        {
            //删除
            ArrayList list = MaintainedEquipmentsParts;
            if (list == null || list.Count == 0)
                return;
            list.RemoveAt(rowNum);
            FillEquipmentPartGridView();
        }
    }
    /// <summary>
    /// 填充
    /// </summary>
    private void FillEquipmentPartGridView()
    {
        GridViewEquipmentPart.DataSource = MaintainedEquipmentsParts;
        GridViewEquipmentPart.DataBind();
    }
    /// <summary>
    /// 添加设备零件信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddEquipmentPart_Click(object sender, EventArgs e)
    {
        MaintainedEquipmentPartInfo ep = new MaintainedEquipmentPartInfo();
        ep.MaintainFee = Convert.ToDecimal(tbEquipmentMaintainFee.Text.Trim());
        ep.PartName = tbEquipmentPartName.Text.Trim();
        ArrayList equipmentparts = MaintainedEquipmentsParts;
        equipmentparts.Add(ep);
        MaintainedEquipmentsParts = equipmentparts;
        FillEquipmentPartGridView();
        tbEquipmentMaintainFee.Text = "0";
        tbEquipmentPartName.Text = "";
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

            if (tbEquipmentNO.Text.Trim() == "")
            {
                lbMsg.Text = "错误：设备条形码不能为空";
                return;
            }

            MaintainedEquipmentInfo item = new MaintainedEquipmentInfo();
            item.EquipmentNO = tbEquipmentNO.Text.Trim();
            item.EquipmentName = tbEquipmentName.Text.Trim();
            item.SheetID = id;
            item.MaintainedEquipmentParts = MaintainedEquipmentsParts;
            item.Remark = tbRemark.Text.Trim();
            item.SerialNum = equipmentBll.GetEquipmentBYNO(item.EquipmentNO).SerialNum;
            item.LastAddress = equipmentBll.GetEquipmentBYNO(item.EquipmentNO).AddressName;
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
            Session["MaintainedEquipmentsParts"] = null;  //清空
            FillEquipmentPartGridView();
            BindDataToGridView();

            changemark = false;

        }
        catch (Exception ex)
        {
            lbMsg.Text = "错误：添加已维修设备失败，请检查输入是否正确";
            EventMessage.EventWriteLog(Msg_Type.Error, lbMsg.Text + "(" + ex.Message + ")");
        }
    }


    /// <summary>
    ///添加设备到已维修列表  （更换故障设备事件）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btChangeEquipment_Click(object sender, EventArgs e)
    {
        try
        {
            if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
            {
                ScriptManager1.AddHistoryPoint("MaintainedEquipments", "MaintainedEquipment");
            }

            ArrayList list = MaintainedEquipments;

            if (tbCurrentEquipmentNO.Text.Trim() == "")
            {
                lberrmsg.Text = "错误：原设备条形码不能为空";
                return;
            }
            if (tbChangeEquipmentNO.Text.Trim() == "")
            {
                lberrmsg.Text = "错误：更换设备条形码不能为空";
                return;
            }
            
            if (ddlCurrentEquipmentWarehouse.SelectedValue == "0")
            {
               lberrmsg.Text = "错误：请选择设备要放进哪个仓库";
                return;
            }

            //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
            if (FM2E.BLL.System.ConfigItems.UseApplyForChangeEquipment)  //使用了通过先申请出库，后更换设备的步骤
            {
                if (!(new OutWarehouse()).ExistsOutEquipmentInfoByEquipmentNO(tbChangeEquipmentNO.Text.Trim()))
                {
                    lberrmsg.Text = "错误：更换的设备没有经过出库确认。";
                    return;
                }
                
            }
            //**********Modification Finished 2011-6-27**********************************************************************************************

            EquipmentInfoFacade currentequipmentitem = equipmentBll.GetEquipmentBYNO(tbCurrentEquipmentNO.Text.Trim());
            if (currentequipmentitem == null)
            {
                lberrmsg.Text = "错误：找不到原设备信息。";
                return;
            }
            EquipmentInfoFacade changeequipmentitem = equipmentBll.GetEquipmentBYNO(tbChangeEquipmentNO.Text.Trim());
            if (changeequipmentitem == null)
            {
                lberrmsg.Text = "错误：找不到原设备信息。";
                return;
            }

            MaintainedEquipmentInfo item1 = new MaintainedEquipmentInfo();
            item1.EquipmentNO = currentequipmentitem.EquipmentNO;
            item1.EquipmentName = currentequipmentitem.Name;
            item1.SheetID = id;
            item1.MaintainedEquipmentParts = null;
            item1.Remark = tbChangeRemark.Text.Trim();
            item1.MaintainResult = (MaintainedEquipmentStatus)Convert.ToInt32(ddlCurrentEquipmentStatus.SelectedValue);
            item1.Model = currentequipmentitem.Model;
            item1.MaintainTimes = Convert.ToInt32(currentequipmentitem.MaintenanceTimes);
            item1.MaintainFee = 0;
            item1.MaintainDate = DateTime.Now;
            item1.SerialNum = currentequipmentitem.SerialNum;
            item1.LastAddress = currentequipmentitem.AddressName;
            foreach (MaintainedEquipmentInfo it in list)
            {
                if (!string.IsNullOrEmpty(item1.EquipmentNO) && it.EquipmentNO == item1.EquipmentNO)
                {
                    lbMsg.Text = string.Format("错误：条形码为{0}的设备已存在列表中，不可重复添加", it.EquipmentNO);
                    return;
                }
            }

            MaintainedEquipmentInfo item2 = new MaintainedEquipmentInfo();
            item2.EquipmentNO = changeequipmentitem.EquipmentNO;
            item2.EquipmentName = changeequipmentitem.Name;
            item2.SheetID = id;
            item2.MaintainedEquipmentParts = null;
            item2.Remark = tbChangeRemark.Text.Trim();
            item2.MaintainResult = MaintainedEquipmentStatus.Replace;
            item2.Model = changeequipmentitem.Model;
            item2.MaintainTimes = Convert.ToInt32(changeequipmentitem.MaintenanceTimes);
            item2.MaintainFee = 0;
            item2.MaintainDate = DateTime.Now;
            item2.SerialNum = changeequipmentitem.SerialNum;
            item2.LastAddress = changeequipmentitem.AddressName;
            foreach (MaintainedEquipmentInfo it in list)
            {
                if (!string.IsNullOrEmpty(item2.EquipmentNO) && it.EquipmentNO == item2.EquipmentNO)
                {
                    lbMsg.Text = string.Format("错误：条形码为{0}的设备已存在列表中，不可重复添加", it.EquipmentNO);
                    return;
                }
            }


            list.Insert(0, item2);
            list.Insert(0, item1);

            MaintainedEquipments = list;

            tbCurrentEquipmentNO.Text = "";
            tbCurrentEquipmentName.Text = "";
            lbCurrentEquipmentDetail.Text = "";
            tbChangeEquipmentNO.Text = "";
            tbChangeEquipmentName.Text = "";
            lbChangeEquipmentDetail.Text = "";
            //ddlCurrentEquipmentWarehouse.SelectedValue = "0";
            lberrmsg.Text = "";
            updatePanelChangeEquipment.Update();

            ModalPopupExtender_ChangeEquipment.Hide();
            Session["MaintainedEquipmentsParts"] = null;  //清空
            FillEquipmentPartGridView();
            BindDataToGridView();
            changemark = true;

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
            btSave.Attributes.Add("onclick", "javascript:return checkForm(document.forms[0],true)&&confirm('未添加任何经过维修的设备，请添加维修设备')");
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

            EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(equipmentNO);
            SubsidiaryEquipmentInfo subsidiaryitem = subsidiarybll.GetSubsidiaryEquipmentByNO(equipmentNO);
            ConsumableEquipmentInfo consumableitem = consumablebll.GetConsumableEquipmentByNO(equipmentNO);

            tbTotalFee.Text = new FM2E.SQLServerDAL.Maintain.MalfunctionHandle().CountPriceByNO(equipmentNO).ToString("#.00");

            if (equipmentitem != null || subsidiaryitem != null || consumableitem != null)
            {
                if (equipmentitem != null)
                {
                    tbEquipmentName.Text = equipmentitem.Name;
                    tbModel.Text = equipmentitem.Model;
                    tbMaintainTimes.Text = equipmentitem.MaintenanceTimes.ToString();
                    tbPrice.Text = equipmentitem.Price.ToString("#.00");
                }
                else if (subsidiaryitem != null)
                {
                    tbEquipmentName.Text = subsidiaryitem.Name;
                    tbModel.Text = subsidiaryitem.Model;
                    tbMaintainTimes.Text = subsidiaryitem.MaintenanceTimes.ToString();
                    tbPrice.Text = subsidiaryitem.Price.ToString("#.00");
                }
                else if (consumableitem != null)
                {
                    tbEquipmentName.Text = consumableitem.Name;
                    tbModel.Text = consumableitem.Model;
                    tbMaintainTimes.Text = consumableitem.MaintenanceTimes.ToString();
                    //tbPrice.Text = consumableitem.Price.ToString("#.00");
                }
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
    /// 原设备条形码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbCurrentEquipmentNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
            {
                ScriptManager1.AddHistoryPoint("MaintainedEquipments", "MaintainedEquipment");
            }

            string equipmentNO = tbCurrentEquipmentNO.Text.Trim();
            if (equipmentNO == "")
            {
                lbCurrentEquipmentDetail.Text = "";
                btChangeEquipment.Disabled = false;
                return;
            }

            EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(equipmentNO);

            if (equipmentitem != null)
            {
                tbCurrentEquipmentName.Text = equipmentitem.Name;
                lbCurrentEquipmentDetail.Text = "设备名称：" + equipmentitem.Name + " 设备型号：" + tbModel.Text + " 维修次数：" + equipmentitem.MaintenanceTimes.ToString() + " 设备地址：" + equipmentitem.AddressName;

                btSaveEquipment.Disabled = false;
            }
            else
            {
                lbCurrentEquipmentDetail.Text = "找不到相应的设备";
                btSaveEquipment.Disabled = true;
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }


    protected void tbEquipmentName_TextChanged(object sender, EventArgs e)
    {
        if (tbEquipmentName.Text.Trim() != "")
        {
            FillEquipmentData();
            FillSubsidiaryEquipmentData();
            FillConsumableEquipmentData();
        }
    }

    /// <summary>
    /// 更换设备条形码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbChangeEquipmentNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
            {
                ScriptManager1.AddHistoryPoint("MaintainedEquipments", "MaintainedEquipment");
            }

            string equipmentNO = tbChangeEquipmentNO.Text.Trim();
            if (equipmentNO == "")
            {
                lbChangeEquipmentDetail.Text = "";
                btChangeEquipment.Disabled = false;
                return;
            }

            EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(equipmentNO);

            if (equipmentitem != null)
            {
                tbChangeEquipmentName.Text = equipmentitem.Name;
                lbChangeEquipmentDetail.Text = "设备名称：" + equipmentitem.Name + " 设备型号：" + tbModel.Text + " 维修次数：" + equipmentitem.MaintenanceTimes.ToString() + " 设备地址：" + equipmentitem.AddressName;

                btSaveEquipment.Disabled = false;
            }
            else
            {
                lbChangeEquipmentDetail.Text = "找不到相应的设备";
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
    /// <summary>
    /// 维修人员列表 行命令
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater_StaffList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        switch (e.CommandName)
        {
            //删除维修人员
            case "DeleteCMD":
                {
                    List<MalfunctionMaintainStaffInfo> list = CurentStaffList;
                    list.RemoveAt(index);
                    FillStaffData();
                    break;
                }
            default: break;
        }
    }
    /// <summary>
    /// 维修人员列表 数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_StaffList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        HtmlTableCell tr = (HtmlTableCell)e.Item.FindControl("td_item");
        if (tr != null)
        {
            //鼠标移动到每项时颜色交替效果    
            tr.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            tr.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
            //设置悬浮鼠标指针形状为"小手"    
            tr.Attributes["style"] = "Cursor:hand;text-align:left";
        }
    }
    /// <summary>
    /// 添加维护人员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddStaff_Click(object sender, EventArgs e)
    {
        MalfunctionMaintainStaffInfo eq = new MalfunctionMaintainStaffInfo();
        eq.MaintenanceStaff = TextBox_StaffName.Text.Trim();

        List<MalfunctionMaintainStaffInfo> stafflist = CurentStaffList;
        stafflist.Add(eq);
        CurentStaffList = stafflist;
        //CurrentTempateSheet.Equipments.Add(eq);
        FillStaffData();

        TextBox_StaffName.Text = "";
    }

    private void FillStaffData()
    {
        Repeater_StaffList.DataSource = CurentStaffList;
        Repeater_StaffList.DataBind();
    }

    private List<MalfunctionMaintainStaffInfo> CurentStaffList
    {
        get
        {
            List<MalfunctionMaintainStaffInfo> list = (List<MalfunctionMaintainStaffInfo>)Session[this.ToString()];
            if (list == null)
            {
                list = new List<MalfunctionMaintainStaffInfo>();
                MalfunctionMaintainStaffInfo eq = new MalfunctionMaintainStaffInfo();
                eq.MaintenanceStaff = UserData.CurrentUserData.PersonName;  //增加当前维修人
                list.Add(eq);
            }
            return list;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }

    /// <summary>
    /// 选择设备按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSelectEquipment_Click(object sender, EventArgs e)
    {
        ModalPopupExtender_SelectEquipment.Hide();
    }

    /// <summary>
    /// 行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Equipment_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    /// <summary>
    /// 列表显示初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Equipment_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// 绑定在用设备数据
    /// </summary>
    private void FillEquipmentData()
    {
        Equipment bll = new Equipment();
        EquipmentSearchInfo item = new EquipmentSearchInfo();
        
        item.Name = tbEquipmentName.Text.Trim();
        if (tbAddress.Text.Trim()!="")
            item.AddressName = tbAddress.Text.Trim(); 
        QueryParam qp = bll.GenerateSearchTerm(item);
        IList list = bll.GetExportList(qp);
        Equipment_GridView.DataSource = list;
        Equipment_GridView.DataBind();
    }

    /// <summary>
    /// 行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SubsidiaryEquipment_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    /// <summary>
    /// 列表显示初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SubsidiaryEquipment_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// 绑定在用设备数据
    /// </summary>
    private void FillSubsidiaryEquipmentData()
    {
        IList list = subsidiarybll.GetAllSubsidiaryEquipment();
        SubsidiaryEquipment_GridView.DataSource = list;
        SubsidiaryEquipment_GridView.DataBind();
    }

    /// <summary>
    /// 行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ConsumableEquipment_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    /// <summary>
    /// 列表显示初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ConsumableEquipment_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// 绑定在用设备数据
    /// </summary>
    private void FillConsumableEquipmentData()
    {
        IList list = consumablebll.GetAllConsumableEquipment();
        ConsumableEquipment_GridView.DataSource = list;
        ConsumableEquipment_GridView.DataBind();
    }

    /// <summary>
    /// 行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CurrentEquipment_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    /// <summary>
    /// 列表显示初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CurrentEquipment_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// 绑定在用设备数据
    /// </summary>
    private void FillCurrentEquipmentData()
    {
        Equipment bll = new Equipment();
        EquipmentSearchInfo item = new EquipmentSearchInfo();
        item.Name = tbCurrentEquipmentName.Text.Trim();
        QueryParam qp = bll.GenerateSearchTerm(item);
        IList list = bll.GetExportList(qp);
        CurrentEquipment_GridView.DataSource = list;
        CurrentEquipment_GridView.DataBind();
        updatePanelChangeEquipment.Update();
        plChangeEquipment.Visible = true;
    }

    protected void btSelectCurrentEquipment_Click(object sender, EventArgs e)
    {
        ModalPopupExtender_SelectCurrentEquipment.Hide();
    }

    protected void btChangeEquipment_CancelClick(object sender, EventArgs e)
    {
        ModalPopupExtender_ChangeEquipment.Hide();
    }

    protected void tbCurrentEquipmentName_TextChanged(object sender, EventArgs e)
    {
        if (tbCurrentEquipmentName.Text.Trim() != "")
        {
            FillCurrentEquipmentData();
        }
    }

    //**********Modified by Xue 2011-6-27****************************************************************************************************
    protected void btTrans_Click(object sender, EventArgs e)
    {
        //string title = "你有新的报修单要处理";
        ////---string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionList.aspx";
        //string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionHandle.aspx?cmd=edit&from=1&id=" + id;   

        long deptId = long.Parse(ddlMaintainTeam.SelectedValue);
        IList list = userBll.GetUsersByDepartmentId(deptId);

        //List<FM2E.Model.PendingOrder.PendingOrderReceiverInfo> poriList = new List<FM2E.Model.PendingOrder.PendingOrderReceiverInfo>();
        string[] strUserList = null;

        strUserList = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            string uName = ((UserInfo)list[i]).UserName;
            strUserList[i] = uName;
            //poriList.Add(new FM2E.Model.PendingOrder.PendingOrderReceiverInfo(0,uName));
        }

        //if (ddlApproveler.SelectedValue == "")  //全体部门
        //{
        //    strUserList = new string[list.Count];
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        string uName = ((UserInfo)list[i]).UserName;
        //        strUserList[i] = uName;
        //        //poriList.Add(new FM2E.Model.PendingOrder.PendingOrderReceiverInfo(0,uName));
        //    }
        //}
        //else
        //{
        //    string uName = ddlApproveler.SelectedValue;
        //    strUserList = new string[] { uName };
        //    //poriList.Add(new FM2E.Model.PendingOrder.PendingOrderReceiverInfo(0, uName));
        //}

        MalfunctionHandleInfo model = CurrentMalfunctionSheet;
        model.MaintainDept = Convert.ToInt64(ddlMaintainTeam.SelectedValue);
        model.MaintainDeptName = ddlMaintainTeam.SelectedItem.Text;
        malfunctionBll.UpdateMalfunctionSheetBasicData(model);

        //FM2E.Model.PendingOrder.PendingOrderInfo poi = new FM2E.Model.PendingOrder.PendingOrderInfo();
        //poi.Receivers = poriList;
        //poi.ReceiverAddress = URL;
        //FM2E.BLL.PendingOrder.PendingOrder po = new FM2E.BLL.PendingOrder.PendingOrder();
        //po.UpdateReceiversByURL(URL, poi);                        //更新新的受理人。


        string title = "您有新的转移故障处理单要处理,故障单号为：" + model.SheetNO;
        string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionList.aspx";

        WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, strUserList);

        if (from.Trim() == "1")
        {
            Response.Redirect("MalfunctionList.aspx");
        }
        else
        {
            Response.Redirect("MalfunctionSheets.aspx");
        }

    }
    //**********Modification Finished 2011-6-27**********************************************************************************************


    protected void rptrptMaintainHistory_onItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //RepeaterItem ri = rptMaintainHistory.Items[e.Item.ItemIndex];
        try
        {
            string maintainid = ((ImageButton)(e.CommandSource)).CommandArgument;
            malfunctionBll.DelMaintainedEquipmentByMaintainID(Convert.ToInt64(maintainid), this.CurrentMalfunctionSheet.SheetID);
            FillData();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除故障记录失败！", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void FillCostItemsData()
    {
        rpEquipmentItems.DataSource = EqCostItems;
        rpEquipmentItems.DataBind();

        decimal totalPrice = 0;
        foreach (EquipmentCostItems item in EqCostItems)
        {
            totalPrice += item.EqTotalPrice;
        }
        lbSumTotal.Text = totalPrice.ToString();

        decimal sumOther = 0;
        try
        {
            sumOther = (decimal)(Convert.ToDecimal(tbMeasureCost.Text.Trim()) + Convert.ToDecimal(tbGuiCost.Text.Trim()) + Convert.ToDecimal(tbTaxCost.Text.Trim()) + Convert.ToDecimal(tbTrafficCost.Text.Trim()));
            lbSumOther.Text = sumOther.ToString();
            lbSumAll.Text = ((decimal)(totalPrice + sumOther)).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v4", "alert('错误，输入的格式不正确！(" + ex.Message + ")')", true);
        }
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
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v3", "alert('错误，输入的格式不正确！("+ex.Message+")')", true);
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
    protected void tbMeasureCost_TextChanged(object sender, EventArgs e)
    {
        decimal sumOther = 0;
        try
        {
            sumOther = (decimal)(Convert.ToDecimal(tbMeasureCost.Text.Trim()) + Convert.ToDecimal(tbGuiCost.Text.Trim()) + Convert.ToDecimal(tbTaxCost.Text.Trim()) + Convert.ToDecimal(tbTrafficCost.Text.Trim()));
            lbSumOther.Text = sumOther.ToString();
            lbSumAll.Text = ((decimal)(Convert.ToDecimal(lbSumTotal.Text.Trim()) + sumOther)).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v5", "alert('错误，输入的格式不正确！(" + ex.Message + ")')", true);
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
}
