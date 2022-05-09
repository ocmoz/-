using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.Model.System;
using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.Model.Basic;
using FM2E.BLL.Utils;
using FM2E.BLL.Maintain;
using WebUtility.Components;
using FM2E.Model.Maintain;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.WorkflowLayer;
using System.Collections;
using FM2E.Model.Utils;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Configuration;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionReport_RecordMalfunction : System.Web.UI.Page
{
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Equipment equipmentBll = new Equipment();
    private readonly Address addressBll = new Address();
    private readonly EquipmentSystem systemBll = new EquipmentSystem();
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 1, DataType.Str);
    private string strEqName = "";
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
    /// 当前旧的故障处理单对象 
    /// </summary>
    public MalfunctionHandleInfo OldMalfunctionSheet
    {
        get
        {
            if (ViewState["OldMalfunctionSheet"] == null)
                return new MalfunctionHandleInfo();
            return (MalfunctionHandleInfo)ViewState["OldMalfunctionSheet"];
        }
        set
        {
            ViewState["OldMalfunctionSheet"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "InitialEquipmentList", "InitEquipmentDiv();", true);
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            LoginUserInfo userData = UserData.CurrentUserData;
            string companyID = userData.CompanyID;
            ViewState["CompanyID"] = companyID;
            lbCompany.Text = userData.CompanyName;
            tbReporter.Text = userData.PersonName;
            cddCompany.SelectedValue = userData.CompanyID;

            //系统列表
            //ddlSystem.Items.Clear();
            ////ddlSystem.Items.Add(new ListItem("请选择系统", "0"));
            //ddlSystem.Items.AddRange(ListItemHelper.GetSystemListItems());
            //ddlSystem.Items.Add(new ListItem("其它", ""));
           
            //故障等级
            ListItem[] rankTypes = EnumHelper.GetListItemsEx(typeof(MalfunctionRank), (int)MalfunctionRank.Common, (int)MalfunctionRank.Unknown);
            ddlRank.Items.Clear();
            ddlRank.Items.AddRange(rankTypes);


            //故障原因
            ListItem[] malReason = EnumHelper.GetListItemsEx(typeof(MalfunctionReason), (int)MalfunctionReason.CommonMalfunction, (int)MalfunctionReason.Unknown);
            ddlMalReason.Items.Clear();
            ddlMalReason.Items.AddRange(malReason);


            //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
            //维修单位
            //ddlMaintainTeam.Items.Clear();
            //ddlMaintainTeam.Items.Add(new ListItem("请选择部门", "0"));
            //ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(userData.CompanyID));
            //**********Modification Finished 2011-6-27**********************************************************************************************

            tbAddress.Attributes["ReadOnly"] = "true";

            ListItem[] timeUnits1 = EnumHelper.GetListItemsEx(typeof(TimeUnits), (int)TimeUnits.Hour, (int)TimeUnits.Unknown, (int)TimeUnits.Month, (int)TimeUnits.Year);
            ListItem[] timeUnits2 = EnumHelper.GetListItemsEx(typeof(TimeUnits), (int)TimeUnits.Hour, (int)TimeUnits.Unknown, (int)TimeUnits.Month, (int)TimeUnits.Year);
            ListItem[] timeUnits3 = EnumHelper.GetListItemsEx(typeof(TimeUnits), (int)TimeUnits.Hour, (int)TimeUnits.Unknown, (int)TimeUnits.Month, (int)TimeUnits.Year);
            ddlResponseTime.Items.Clear();
            ddlResponseTime.Items.AddRange(timeUnits1);

            ddlFunRestoreTime.Items.Clear();
            ddlFunRestoreTime.Items.AddRange(timeUnits2);

            ddlRepairTime.Items.Clear();
            ddlRepairTime.Items.AddRange(timeUnits3);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 菜单绑定
    /// </summary>
    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：添加故障处理单";
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：修改故障处理单";
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            if (cmd == "add")
            {
                //lbReportTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                tbReportDate.Text = now.ToString("yyyy-MM-dd");
                tbReportHour.Text = now.Hour.ToString();
                tbReportMinute.Text = now.Minute.ToString();
            }
            else if (cmd == "edit")
            {
                MalfunctionHandleInfo item = malfunctionBll.GetMalfunctionSheet(id);
                if (item == null)
                    return;

                CurrentMalfunctionSheet = item;
                OldMalfunctionSheet = item;

                Company companyBll = new Company();
                CompanyInfo companyInfo = companyBll.GetCompany(item.CompanyID);
                lbCompany.Text = companyInfo.CompanyName;
                cddCompany.SelectedValue = item.CompanyID;
                lbSheetNO.Text = item.SheetNO;
                cddDepartment.SelectedValue = item.DepartmentID.ToString();
                tbReporter.Text = item.Reporter;
                //lbReportTime.Text = item.ReportDate.ToString("yyyy-MM-dd");
                tbReportDate.Text = item.ReportDate.ToString("yyyy-MM-dd");
                tbReportHour.Text = item.ReportDate.Hour.ToString();
                tbReportMinute.Text = item.ReportDate.Minute.ToString();

                IList equipments = item.FaultyEquipments;
                //hdOpenDivNum.Value = "0";
                //if (equipments != null)
                //{
                //    hdOpenDivNum.Value = equipments.Count.ToString();
                //    int i=1;
                //    foreach (FaultyEquipmentInfo it in equipments)
                //    {
                //        ((TextBox)hdOpenDivNum.Parent.FindControl("tbEquipmentNO" + i)).Text = it.EquipmentNO;
                //        ((TextBox)hdOpenDivNum.Parent.FindControl("tbEquipmentName" + i)).Text = it.EquipmentName;
                //        i++;
                //    }
                //}

                hdAddressID.Value = item.AddressID.ToString();
                AddressInfo address = new Address().GetAddress(item.AddressID);
                if (address != null)
                    tbAddress.Text = address.AddressFullName;

                //tbAddressDetail.Text = item.AddressDetail;
                string eqmno = item.AddressDetail.Split('@')[0];
                if (eqmno != null || eqmno != "")
                {
                    EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);

                    tbEqName.Text = equipmentitem.Name;
                    strEqName = equipmentitem.Name;
                    tbEqSystem.Text = equipmentitem.SystemName;
                    tbEqNo.Text = equipmentitem.EquipmentNO;
                }


                DepartmentInfo depti = (new Department()).GetDepartment(item.MaintainDept);
                if (depti.DepartmentType != DepartmentType.Unknown)
                {
                    ddlMaintainType.SelectedValue = ((int)depti.DepartmentType).ToString();
                }
                ddlMaintainTeam.SelectedValue = depti.DepartmentID.ToString();


                tbMalfunctionDescription.Text = item.MalfunctionDescription;
                //ddlSystem.SelectedValue = !string.IsNullOrEmpty(item.SystemID) ? item.SystemID : "0";
                ddlRank.SelectedValue = ((int)item.MalfunctionRank).ToString();
                ddlMaintainTeam.SelectedValue = item.MaintainDept.ToString();

                ddlMalReason.SelectedValue = ((int)item.SystemID).ToString();

                //填充响应时间等
                tbResponseTime.Text = item.ResponseTime.ToString();
                ddlResponseTime.SelectedValue = ((int)item.ResponseUnit).ToString();
                tbFunRestoreTime.Text = item.FunRestoreTime.ToString();
                ddlFunRestoreTime.SelectedValue = ((int)item.FunRestoreUnit).ToString();
                tbRepairTime.Text = item.RepairTime.ToString();
                ddlRepairTime.SelectedValue = ((int)item.RepairUnit).ToString();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "填充页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    
    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        if (ddlMaintainTeam.SelectedValue == "0")
            EventMessage.MessageBox(Msg_Type.Error, "操作失败","没有维护队可选择！", Icon_Type.Error, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
        //********** Modification Finished 2011-11-28 **********************************************************************************************

        MalfunctionHandleInfo item = CurrentMalfunctionSheet;
        //EquipmentInfoFacade GetEqName = equipmentBll.GetEquipmentBYNO(item.eq);

        
        item.CompanyID = (string)ViewState["CompanyID"];
        item.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);
        item.DepartmentName = ddlDepartment.SelectedItem.Text;     //获取故障报门名称
        //2012-3-28 by L更改故障单前面的缩写,以自然站计算*****************************************************************************
        string stationname = item.DepartmentName;
        //sheetname = "XL";
        if (item.DepartmentName.Contains("隧道所"))
        {
            stationname = "SDS";//隧道所统一编号
        }
        else
        {
            if (item.DepartmentName.Contains("客服中心"))
            {
                stationname = "KFZX";//客服中心统一编号
            } 
            else
            {
                
                string tempAddress = "";
               
                if (tbAddress.Text.Split(' ').Count()>2)
                {
                    tempAddress = tbAddress.Text.Split(' ')[2];
                }
                else if (tbAddress.Text.Split(' ').Count() > 1)
                {
                    tempAddress = tbAddress.Text.Split(' ')[1];
                }
                else if (tbAddress.Text.Split(' ').Count() > 0)
                {
                    tempAddress = tbAddress.Text.Split(' ')[0];
                }
                else
                {
                    tempAddress = "";
                }
                if (tempAddress!=null&&tempAddress!="")
                {
                    stationname = StrToFirstPinyin.GetChineseSpell(tempAddress);
                }
                else
                {
                    stationname = "SG";
                }
            }
        }
        
        // 2012-3-28 by L* ******************************************************************************/
        item.Reporter = tbReporter.Text.Trim();
        item.AddressID = Convert.ToInt64(hdAddressID.Value.Trim());

        item.AddressDetail = tbEqNo.Text.Trim() + "@" + hdSystemID.Value;
        string eqmno = item.AddressDetail.Split('@')[0];
        if (eqmno != null && eqmno != "")
        {
            EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);

           
            strEqName = equipmentitem.Name;
            
        }
        item.MalfunctionDescription = tbMalfunctionDescription.Text.Trim().Replace(',', '，');

        item.MalfunctionRank = (MalfunctionRank)Convert.ToInt32(ddlRank.SelectedValue);
        item.Stationcheck = dlMalfunctionType.SelectedValue=="1"?true:false;

        item.SystemID = (MalfunctionReason)Convert.ToInt32(ddlMalReason.SelectedValue);


        //item.SystemID = ddlSystem.SelectedValue;
        item.MaintainDept = Convert.ToInt64(ddlMaintainTeam.SelectedValue);
        item.MaintainDeptName = ddlMaintainTeam.SelectedItem.Text;
        item.UpdateTime = DateTime.Now;
        item.RecordDept = UserData.CurrentUserData.DepartmentID;

        //记录响应时间等
        item.ResponseTime = Convert.ToInt32(tbResponseTime.Text.Trim());
        item.ResponseUnit = (TimeUnits)Convert.ToInt32(ddlResponseTime.SelectedValue);
        item.FunRestoreTime = Convert.ToInt32(tbFunRestoreTime.Text.Trim());
        item.FunRestoreUnit = (TimeUnits)Convert.ToInt32(ddlFunRestoreTime.SelectedValue);
        item.RepairTime = Convert.ToInt32(tbRepairTime.Text.Trim());
        item.RepairUnit = (TimeUnits)Convert.ToInt32(ddlRepairTime.SelectedValue);
        item.IsDelayApply = false;
        //item.IsDelayCheck1 = 2;
        //item.IsDelayCheck2 = 1;
        //int equipmentCount = Convert.ToInt32(hdOpenDivNum.Value);
        ArrayList equipments = new ArrayList();
        //for (int i = 1; i <= equipmentCount; i++)
        //{
        //    string equipmentNO = ((TextBox)hdOpenDivNum.Parent.FindControl("tbEquipmentNO" + i)).Text.Trim();
        //    string equipmentName = ((TextBox)hdOpenDivNum.Parent.Parent.FindControl("tbEquipmentName" + i)).Text.Trim();
        //    FaultyEquipmentInfo eq = new FaultyEquipmentInfo();
        //    eq.EquipmentNO = equipmentNO;
        //    eq.EquipmentName = equipmentName;
        //    equipments.Add(eq);
        //}
        item.FaultyEquipments = equipments;

        //报修时间*******************************************************
        string reportTime = string.Format("{0} {1}:{2}:00", tbReportDate.Text.Trim(), tbReportHour.Text.Trim(), tbReportMinute.Text.Trim());
        item.ReportDate = DateTime.Parse(reportTime);
        //if (item.ReportDate.Hour < 9)
        //    item.ReportDate2 = DateTime.Parse(string.Format("{0} {1}:{2}:00", tbReportDate.Text.Trim(), "09", "0"));
        //else if(item.ReportDate.Hour < 14)
        //    item.ReportDate2 = DateTime.Parse(string.Format("{0} {1}:{2}:00", tbReportDate.Text.Trim(), "14","0"));
        if (item.ReportDate.Hour < 21)
            item.ReportDate2 = DateTime.Parse(string.Format("{0} {1}:{2}:00", tbReportDate.Text.Trim(), tbReportHour.Text.Trim(), tbReportMinute.Text.Trim()));
        else
        {
            item.ReportDate2 = DateTime.Parse(string.Format("{0} {1}:{2}:00", tbReportDate.Text.Trim(), "08", "0"));
            item.ReportDate2 = DateTime.Parse(string.Format("{0} {1}:{2}:00", item.ReportDate2.AddDays(1).ToShortDateString(), "08", "0"));
        }

        //******************************************************************edit by L 2012-3-20
        MalfunctionModifyRecordInfo modifyRecord = null;

        #region 添加处理记录
        //string separatorStr = "@First@";
        //if (item.Editreason != null)
        //{
        //    if (!item.Editreason.Contains(separatorStr))
        //    {
        //        item.Editreason += " " + separatorStr + " ";
        //    }
        //}
        //else
        //{
        //    item.Editreason += " " + separatorStr + " ";
        //}
        //string[] split = { separatorStr };
        //string[] editreason = item.Editreason.Split(split, StringSplitOptions.None);
        #endregion

        if (cmd == "add")
        {
            item.SheetNO = SheetNOGenerator.GetSheetNO(stationname,FM2E.Model.Utils.SheetType.MALFUNCTION_HANDLEFORM);
            item.Recorder = Common.Get_UserName;
            item.Status = MalfunctionHandleStatus.Waiting4Accept;
            //*********************2013-1-10自维先转到工程师上面，然后再确定让上报人是否维修***********************************************
            if (Convert.ToInt32(ddlMaintainType.SelectedValue) == DepartmentType.MaintainTeam.GetHashCode())
            {
                item.Status = MalfunctionHandleStatus.InteriorEngineerCheck;
            }



            //*********************2013-1-10自维先转到工程师上面，然后再确定让上报人是否维修***********************************************
            //editreason[1] += UserData.CurrentUserData.PersonName + "(上报)";
        }
        else if (cmd == "edit")
        {
            modifyRecord = GetModifyRecord(OldMalfunctionSheet, CurrentMalfunctionSheet);
            //editreason[1] += " → " + UserData.CurrentUserData.PersonName + "(修改上报)";
        }
        //item.Editreason = editreason[0] + separatorStr + editreason[1];

        try
        {
            //**********Modified by Xue 2011-6-27****************************************************************************************************
            //---malfunctionBll.SaveMalfunctionSheet(item,modifyRecord);
            long id = 0;                                                                                                                        //new
            if (item.SheetID == 0)                                                                                                              //new
            {                                                                                                                                   //new
                id = malfunctionBll.AddMalfunctionSheet(item);                                                                                  //new
            }                                                                                                                                   //new
            else                                                                                                                                //new
            {                                                                                                                                   //new
                id = item.SheetID;                                                                                                              //new
                malfunctionBll.UpdateMalfunctionSheet(item, modifyRecord);                                                                      //new
            }                                                                                                                                   //new

            string title = item.DepartmentName+"提交了"+strEqName+"设备的故障，故障单号为"+item.SheetNO;
            //---string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionList.aspx";
            string URL = "../MaintainManager/MalFunctionManager/MalfunctionMaintain/MalfunctionList.aspx";          //new 
            //**********Modification Finished 2011-6-27***********************************************************************************************


            User userBll = new User();



            UserSearchInfo usersearchitem = new UserSearchInfo();

            usersearchitem.DepartmentID = Convert.ToInt64(ddlMaintainTeam.SelectedValue);
            int recordCount = 0;

            IList list = userBll.GetList(usersearchitem, 1, int.MaxValue, out recordCount);

            //**********Modified by Xue 2011-6-27****************************************************************************************************
            string[] strUserList = null;
            //if (ddlApproveler.SelectedItem.Value == "")
            //{
                strUserList = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    strUserList[i] = ((UserInfo)list[i]).UserName;
                }
            //}
            //else
            //{
            //    strUserList = new string[] { ddlApproveler.SelectedValue };
            //}
            //foreach (UserInfo info in list)
            //{
            //    WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL,
            //     0, null, info.UserName);
            //}
            if (cmd == "add")   //编辑或修改不会增加待办事项
            {
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, strUserList);

                //知会系统工程师
                IList systemUserlist = userBll.GetUsersByIM(hdSystemID.Value);
                if (systemUserlist.Count!=0)
                {
                    string[] strSystemUserList = null;
                    strSystemUserList = new string[systemUserlist.Count];
                    for (int i = 0; i < systemUserlist.Count; i++)
                    {
                        strSystemUserList[i] = ((UserInfo)systemUserlist[i]).UserName;
                    }
                    string stitle = item.DepartmentName + "上报了" + strEqName + "设备的故障，故障单号为" + item.SheetNO;
                    string sURL = "../MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id=" + id;
                    WorkflowApplication.SendingPendingOrderToUsers(stitle, Common.Get_UserName, UserData.CurrentUserData.PersonName, sURL, 0, null, strSystemUserList);
                }
            }
            //**********Modification Finished 2011-6-27***********************************************************************************************

            

            //WorkflowApplication.CreateWorkflowAndSendingPendingOrderForMalfunction<MalFunctionEventService>(item, MalFunctionWorkflow.WorkflowName, MalFunctionWorkflow.TableName, MalFunctionWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, 1);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交故障处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("提交故障处理单成功,故障处理单号为：{0}",item.SheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
    }
    /// <summary>
    /// 获取故障单的修改内容
    /// </summary>
    /// <returns></returns>
    private MalfunctionModifyRecordInfo GetModifyRecord(MalfunctionHandleInfo oldSheet,MalfunctionHandleInfo newSheet)
    {
        //int equipmentCount = Convert.ToInt32(hdOpenDivNum.Value);
        //ArrayList equipments = new ArrayList();
        //for (int i = 1; i <= equipmentCount; i++)
        //{
        //    string equipmentNO = ((TextBox)hdOpenDivNum.Parent.FindControl("tbEquipmentNO" + i)).Text.Trim();
        //    string equipmentName = ((TextBox)hdOpenDivNum.Parent.Parent.FindControl("tbEquipmentName" + i)).Text.Trim();
        //    FaultyEquipmentInfo eq = new FaultyEquipmentInfo();
        //    eq.EquipmentNO = equipmentNO;
        //    eq.EquipmentName = equipmentName;
        //    equipments.Add(eq);
        //}
        //item.FaultyEquipments = equipments;

        ////报修时间
        //string reportTime = string.Format("{0} {1}:{2}:00", tbReportDate.Text.Trim(), tbReportHour.Text.Trim(), tbReportMinute.Text.Trim());
        //item.ReportDate = DateTime.Parse(reportTime);

        StringBuilder strDiff = new StringBuilder();

        if (oldSheet.ReportDate != newSheet.ReportDate)
        {
            strDiff.AppendFormat("报修时间：{0}--->{1}<br/>", oldSheet.ReportDate.ToString("yyyy-MM-dd HH:mm"), newSheet.ReportDate.ToString("yyyy-MM-dd HH:mm"));
        }
        if (oldSheet.DepartmentID != newSheet.DepartmentID)
        {
            strDiff.AppendFormat("故障部门：{0}--->{1}<br/>", oldSheet.DepartmentName, newSheet.DepartmentName);
        }
        string oldEquipments = "";
        int index = 0;
        foreach (FaultyEquipmentInfo it in oldSheet.FaultyEquipments)
        {
            oldEquipments += it.EquipmentName;
            strEqName = it.EquipmentName;
            if (!string.IsNullOrEmpty(it.EquipmentNO))
                oldEquipments += "(" + it.EquipmentNO + ")";
            if (index != oldSheet.FaultyEquipments.Count - 1)
                oldEquipments += ",";
            index++;
        }
        string newEquipments = "";
        index = 0;
        foreach (FaultyEquipmentInfo it in newSheet.FaultyEquipments)
        {
            newEquipments += it.EquipmentName;
            strEqName = it.EquipmentName;
            if (!string.IsNullOrEmpty(it.EquipmentNO))
                newEquipments += "(" + it.EquipmentNO + ")";
            if (index != newSheet.FaultyEquipments.Count - 1)
                newEquipments += ",";
            index++;
        }

        if (oldEquipments != newEquipments)
        {
            strDiff.AppendFormat("故障设备：{0}--->{1}<br/>", oldEquipments, newEquipments);
        }
        if (oldSheet.AddressID != newSheet.AddressID)
        {
            AddressInfo addressInfo = addressBll.GetAddress(oldSheet.AddressID);
            string oldAddress = addressInfo != null ? addressInfo.AddressFullName : " ";
            addressInfo = addressBll.GetAddress(newSheet.AddressID);
            string newAddress = addressInfo != null ? addressInfo.AddressFullName : " ";
            strDiff.AppendFormat("故障地址: {0}--->{1}<br/>", oldAddress, newAddress);
        }
        if (oldSheet.AddressDetail != newSheet.AddressDetail)
        {
            strDiff.AppendFormat("地址详细描述：{0}--->{1}<br/>", oldSheet.AddressDetail, newSheet.AddressDetail);
        }
        if (oldSheet.MalfunctionDescription != newSheet.MalfunctionDescription)
        {
            strDiff.AppendFormat("故障描述：{0}--->{1}<br/>", oldSheet.MalfunctionDescription, newSheet.MalfunctionDescription);
        }

        if (oldSheet.MalfunctionRank != newSheet.MalfunctionRank)
        {
            strDiff.AppendFormat("故障等级：{0}--->{1}<br/>", EnumHelper.GetDescription(oldSheet.MalfunctionRank), EnumHelper.GetDescription(newSheet.MalfunctionRank));
        }
        if (oldSheet.SystemID != newSheet.SystemID)
        {
            //EquipmentSystemInfo systemInfo = systemBll.GetSystem(oldSheet.SystemID);
            //string oldSystem = systemInfo != null ? systemInfo.SystemName : " ";
            //systemInfo = systemBll.GetSystem(newSheet.SystemID);
            //string newSystem = systemInfo != null ? systemInfo.SystemName : " ";

            string oldSystem = EnumHelper.GetDescription(oldSheet.SystemID).ToString();
            string newSystem = EnumHelper.GetDescription(newSheet.SystemID).ToString();

            strDiff.AppendFormat("故障原因：{0}--->{1}<br/>", oldSystem, newSystem);
        }
        if (oldSheet.MaintainDept != newSheet.MaintainDept)
        {
            strDiff.AppendFormat("维修单位：{0}--->{1}<br/>", oldSheet.MaintainDeptName, newSheet.MaintainDeptName);
        }
        if (oldSheet.ResponseTime != newSheet.ResponseTime || oldSheet.ResponseUnit != newSheet.ResponseUnit)
        {
            strDiff.AppendFormat("响应时间：{0}--->{1}<br/>", oldSheet.ResponseTime + EnumHelper.GetDescription(oldSheet.ResponseUnit), newSheet.ResponseTime + EnumHelper.GetDescription(newSheet.ResponseUnit));
        }
        if (oldSheet.FunRestoreTime != newSheet.FunRestoreTime || oldSheet.FunRestoreUnit != newSheet.FunRestoreUnit)
        {
            strDiff.AppendFormat("功能性恢复时间：{0}--->{1}<br/>", oldSheet.FunRestoreTime + EnumHelper.GetDescription(oldSheet.FunRestoreUnit), newSheet.FunRestoreTime + EnumHelper.GetDescription(newSheet.FunRestoreUnit));
        }
        if (oldSheet.RepairTime != newSheet.RepairTime || oldSheet.RepairUnit != newSheet.RepairUnit)
        {
            strDiff.AppendFormat("功能性恢复时间：{0}--->{1}<br/>", oldSheet.RepairTime + EnumHelper.GetDescription(oldSheet.RepairUnit), newSheet.RepairTime + EnumHelper.GetDescription(newSheet.RepairUnit));
        }

        MalfunctionModifyRecordInfo item = null;
        if (!string.IsNullOrEmpty(strDiff.ToString()))
        {
            item = new MalfunctionModifyRecordInfo();
            item.Modifier = Common.Get_UserName;
            item.ModifyDate = DateTime.Now;
            item.ModifyDescription = strDiff.ToString();
            item.SheetID = id;
        }

        return item;
    }
    /// <summary>
    /// 条形码输入框输入改变时触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbEquipmentNO_TextChanged(object sender, EventArgs e)
    {
        //TextBox tb = (TextBox)sender;
        //TextBox targetBox = null;
        //HtmlGenericControl lt = null;
        //int index = 0;
        //switch (tb.ID)
        //{
        //    case "tbEquipmentNO1":
        //        index = 0;
        //        targetBox = tbEquipmentName1;
        //        lt = span1;
        //        break;
        //    case "tbEquipmentNO2":
        //        index = 1;
        //        targetBox = tbEquipmentName2;
        //        lt = span2;
        //        break;
        //    case "tbEquipmentNO3":
        //        index = 2;
        //        targetBox = tbEquipmentName3;
        //        lt = span3;
        //        break;
        //    case "tbEquipmentNO4":
        //        index = 3;
        //        targetBox = tbEquipmentName4;
        //        lt = span4;
        //        break;
        //    case "tbEquipmentNO5":
        //        index = 4;
        //        targetBox = tbEquipmentName5;
        //        lt = span5;
        //        break;
        //}

        //lt.InnerHtml = "";
        //string leftString = hdErrorFlag.Value.Substring(0, index);
        //string rightString = hdErrorFlag.Value.Substring(index + 1, hdErrorFlag.Value.Length - index - 1);
        //if (tb.Text.Trim() == "")
        //{
        //    targetBox.Text = "";
        //    hdErrorFlag.Value = leftString + "0" + rightString;
        //    if (hdErrorFlag.Value == "00000")
        //        btSubmit.Attributes.Remove("disabled");
        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "InitialEquipmentList", "InitEquipmentDiv();", true);
        //    return;
        //}

        //try
        //{
        //    //检查此设备的故障是否已经上报，并正在处理
        //    MalfunctionSearchInfo searchTerm = new MalfunctionSearchInfo();
        //    searchTerm.EquipmentNO = tb.Text.Trim();
        //    int recordCount = 0;
        //    IList sheets = malfunctionBll.GetMalfunctionList(searchTerm, 1, 1, out recordCount);    //如果存在一条记录，并且状态为未完全修复的话，说明此故障已上报
        //    if (sheets != null && sheets.Count != 0)
        //    {
        //        MalfunctionHandleInfo sheet = sheets[0] as MalfunctionHandleInfo;
        //        if (sheet != null && sheet.Status != MalfunctionHandleStatus.Fixed
        //            && sheet.Status != MalfunctionHandleStatus.Finished
        //            &&sheet.Status!=MalfunctionHandleStatus.Canceled)
        //        {
        //            //故障已上报，禁止提交
        //            targetBox.Text = "";
        //            hdErrorFlag.Value = leftString + "1" + rightString;
        //            btSubmit.Attributes["disabled"] = "disabled";
        //            lt.InnerHtml = string.Format("<a href=\"{0}\" style=\"color:Red\">此设备故障已上报，点击查看</a>", string.Format("javascript:showPopWin('故障单查看','ViewMalfunctionSheet.aspx?cmd=view&id={0}&viewOnly=1',800, 430, null,true,true)",sheet.SheetID));
        //            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "InitialEquipmentList", "InitEquipmentDiv();", true);
        //            return;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    EventMessage.EventWriteLog(Msg_Type.Error, ex.Message);
        //}

        //try
        //{
        //    EquipmentInfoFacade item = equipmentBll.GetEquipmentBYNO(tb.Text.Trim());
        //    if (item == null)
        //    {
        //        targetBox.Text = "找不到相应的设备";
        //        hdErrorFlag.Value = leftString + "1" + rightString;
        //        btSubmit.Attributes["disabled"] = "disabled";
        //        return;
        //    }

        //    targetBox.Text = item.Name;
        //    hdErrorFlag.Value = leftString + "0" + rightString;
        //    if (hdErrorFlag.Value == "00000")
        //        btSubmit.Attributes.Remove("disabled");
        //}
        //catch (Exception ex)
        //{
        //    targetBox.Text = "找不到相应的设备";
        //    hdErrorFlag.Value = leftString + "1" + rightString;
        //    btSubmit.Attributes["disabled"] = "disabled";
        //    EventMessage.EventWriteLog(Msg_Type.Warn, ex.Message);
        //}
        //finally
        //{
        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "InitialEquipmentList", "InitEquipmentDiv();", true);
        //}
    }
    protected void tbEqNo_TextChanged(object sender, EventArgs e)
    {
        //if (tbEqNo.Text.Trim() != "")
        //{
        //    EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(tbEqNo.Text.Trim());
        //    if (equipmentitem.EquipmentNO != null || equipmentitem.EquipmentNO != "")
        //    {
        //        tbEqName.Text = equipmentitem.Name;
        //        tbEqSystem.Text = equipmentitem.SystemName;
        //        tbAddress.Text = equipmentitem.AddressName;
        //    }
        //    else
        //    {
        //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "条形码有误！", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        //    }
        //}
    }
}
