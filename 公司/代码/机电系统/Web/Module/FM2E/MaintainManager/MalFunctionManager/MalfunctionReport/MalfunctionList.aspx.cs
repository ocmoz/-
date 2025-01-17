﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Maintain;
using FM2E.BLL.Maintain;
using System.Collections;
using FM2E.BLL.Basic;
using FM2E.Model.System;
using FM2E.BLL.System;
using FM2E.Model.Basic;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionReport_MalfunctionList : System.Web.UI.Page
{
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();

    private readonly Department deptBll = new Department();
    /// <summary>
    /// 查询条件
    /// </summary>
    private MalfunctionSearchInfo SearchTerm
    {
        get
        {
            if (ViewState["SearchTerm"] == null)
            {
                MalfunctionSearchInfo term = new MalfunctionSearchInfo();
                if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
                {
                    //如果没有查看全部故障处理单的权限
                    //term.RecordDept = UserData.CurrentUserData.DepartmentID;
                }
                //term.Status = (int)MalfunctionHandleStatus.NotRepaired;
                return term;
            }
            else return (MalfunctionSearchInfo)ViewState["SearchTerm"];
        }
        set
        {
            ViewState["SearchTerm"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
        }
        ButtonBind();
    }
    /// <summary>
    /// 初始化页面数据
    /// </summary>
    private void InitialPage()
    {
        LoginUserInfo loginUser = UserData.CurrentUserData;

        if (loginUser.DepartmentID == 0 && !SystemPermission.CheckPermission(PopedomType.PermissionA))
        {
            //如果没有部门号，并且没有查看全部故障单的特殊权限
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "没有查看故障处理单的权限", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }

        try
        {
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("不限", "0"));
            ddlDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItemsByCompany(loginUser.CompanyID));

            //ListItem[] statusItems = EnumHelper.GetListItemTree(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.NotRepaired, (int)MalfunctionHandleStatus.Unknown);
            ListItem[] statusItems = EnumHelper.GetListItems(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.Unknown);

            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem("不限", ((int)MalfunctionHandleStatus.Unknown).ToString()));
            ddlStatus.Items.AddRange(statusItems);

            //维修单位
            ddlMaintainTeam.Items.Clear();
            ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));

            //过滤条件
            ddlFilterDepartment.Items.Clear();
            ddlFilterDepartment.Items.Add(new ListItem("不限", "0"));
            ddlFilterDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItems(loginUser.CompanyID));

            ddlFilterMaintainTeam.Items.Clear();
            ddlFilterMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlFilterMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));

            ListItem[] statusItems1 = EnumHelper.GetListItemTree(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.Unknown, (int)MalfunctionHandleStatus.Unknown);
            //ListItem[] statusItems1 = EnumHelper.GetListItems(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.Unknown);
            //ListItem[] statusItems1 = EnumHelper.GetListItemTree(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.NotRepaired, (int)MalfunctionHandleStatus.Unknown);

            ListItem i = statusItems1[3];
            i.Text = "　" + i.Text;
            List<ListItem> itemList = statusItems1.ToList();
            itemList.RemoveRange(3, 3);
            itemList.Insert(6, i);
            statusItems1 = itemList.ToArray();

            ddlFilterStatus.Items.Clear();
            ddlFilterStatus.Items.Add(new ListItem("不限", ((int)MalfunctionHandleStatus.Unknown).ToString()));
            ddlFilterStatus.Items.AddRange(statusItems1);
            

            ListItem[] rankTypes = EnumHelper.GetListItems(typeof(MalfunctionRank), (int)MalfunctionRank.Unknown);

            ddlFilterRank.Items.Clear();
            ddlFilterRank.Items.Add(new ListItem("不限", ((int)MalfunctionRank.Unknown).ToString()));
            ddlFilterRank.Items.AddRange(rankTypes);

            //By L 5-3   将原记录人查询更改为故障设备所属系统查询****************************************************
            //系统
            ddlSystem.Items.Clear();
            ddlSystem.Items.Add(new ListItem("不限", ""));
            ddlSystem.Items.AddRange(ListItemHelper.GetSystemListItems());
            //BY L 5-3 ********************************************************

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void ButtonBind()
    {
        if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
        {
            HeadMenuWebControls1.HeadHelpTxt = string.Format("当前用户部门为{0}，默认显示本部门及其下属部门最新的故障单", UserData.CurrentUserData.DepartmentName);
        }
        else
        {
            HeadMenuWebControls1.HeadHelpTxt = "默认显示各部门最新的故障处理单";
        }
    }
    /// <summary>
    /// 权限控制
    /// </summary>
    private void PermissionControl()
    {
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.New);
        //********** Modification Finished 2011-09-09 **********************************************************************************************

        LoginUserInfo loginUser = UserData.CurrentUserData;
        if (loginUser.DepartmentID == 0)
        {
            //没有部门号，不能添加故障单
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }

        //if (SystemPermission.CheckPermission(PopedomType.Delete))
        //{
        //    //有删除权限
        //    GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        //}
        //else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            MalfunctionSearchInfo term = SearchTerm;
            //查询
            int recordCount = 0;
            IList list = malfunctionBll.GetMalfunctionList(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);

            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
            TabContainer1.ActiveTabIndex = 0;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 填充页面数据上报时间排序
    /// </summary>
    private void FillData2()
    {
        try
        {
            MalfunctionSearchInfo term = SearchTerm;
            //查询
            int recordCount = 0;
            IList list = malfunctionBll.GetMalfunctionListByReportTime(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);

            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
            TabContainer1.ActiveTabIndex = 0;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (SortDropDownList.SelectedValue == "ReportTime")
        {
            FillData2();
        }
        else
        {
            FillData();
        }
    }
    /// <summary>
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LoginUserInfo userdata1 = UserData.CurrentUserData;
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["SheetID"]);
        //************************By L 4-26 获取用户角色,通过用户角色来控制站长确认的权限*******************************************
        User userBll = new User();
        UserInfo userInfo = userBll.GetUser(userdata1.UserName);
        if (userInfo == null)
            return;
        String RoleText = null;
        Boolean b = false;
        if (userInfo.Roles != null && userInfo.Roles.Count > 0)
        {
            foreach (object var in userInfo.Roles)
            {
                UserRoleInfo item = (UserRoleInfo)var;
                RoleText += string.Format("·{0}&nbsp;&nbsp;", item.RoleName);
            }
            string a = "值班站长";
            if (RoleText.Contains(a))
            {
                b = true;
            }
        }

        //*************************By L 4-26***********************************************************
        if (e.CommandName == "view")
        {
            //查看
            string url = "ViewMalfunctionSheet.aspx?cmd=view&id=" + id;
            //Response.Redirect(url);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, Page.GetType(), "ajaxjs", "window.open('" + url + "','_blank');", true);
        }
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        if (e.CommandName == "check" && userdata1.CompanyID == "SG")
        {
            MalfunctionHandleInfo model = malfunctionBll.GetMalfunctionSheet(id);
            //*****************************************************
            string nextUserName = "";
            try
            {

                if (deptBll.GetDepartment(model.MaintainDept).DepartmentType == DepartmentType.MaintainTeamOther)  //外维
                {
                    model.Status = MalfunctionHandleStatus.Waiting4Mark;

                    FM2E.Model.Workflow.WorkflowInstanceInfo wii = WorkflowHelper.GetWorkflowInstanceInfo(SGS_MalFunctionWorkflow.TableName, model.SheetID);
                    if (wii == null)           //新建工作流
                    {
                        String company = model.MaintainDeptName;
                        Guid guid = WorkflowHelper.CreateNewInstance(model.SheetID, SGS_MalFunctionWorkflow.WorkflowName);
                        WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, 
                            WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, "New")[0].Name, 
                            SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);
                    }
                    else
                    {
                        if (wii.CurrentStateName == "Wait4ElectDeptEngineerConfirm")
                        {
                            model.Status = MalfunctionHandleStatus.AnotherCheck;
                        }
                        else
                        {
                            String company = model.MaintainDeptName;

                            Guid guid = wii.InstanceID;
                            WorkflowHelper.SetStateAndNextUserMachine<SGS_MalFunctionEventService>(guid, 
                                WorkflowHelper.GetEventInfoList(SGS_MalFunctionWorkflow.WorkflowName, wii.CurrentStateName)[0].Name, 
                                SGS_MalFunctionWorkflow.WorkflowName, company, out nextUserName);

                        }

                    }

                }
                //*****************************************************
                model.ActualRepairTime = (int)Math.Ceiling((DateTime.Now - model.ReportDate2).TotalSeconds / 60); //确认故障单修复时间 edit by L 2012-3-20
                model.UpdateTime = DateTime.Now;
                model.TimeConfirmer = userdata1.PersonName;
                model.Status = MalfunctionHandleStatus.Waiting4Mark;
                malfunctionBll.UpdateMalfunctionSheetBasicData(model);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "故障处理单实际修复时间确认失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "故障处理单实际修复时间确认成功，故障处理单号为：" + model.SheetNO, Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
        }
        //********** Modification Finished 2012-4-3 **********************************************************************************************
        if (e.CommandName == "stationcheck" && b == true)
        {
            MalfunctionHandleInfo model = malfunctionBll.GetMalfunctionSheet(id);
            try
            {

                model.Stationcheck = true;
                malfunctionBll.UpdateMalfunctionSheetBasicData(model);
                //model.ActualRepairTime = (int)Math.Ceiling((DateTime.Now - model.ReportDate2).TotalSeconds / 60); //确认故障单修复时间 edit by L 2012-3-20
                //model.UpdateTime = DateTime.Now;
                //malfunctionBll.UpdateMalfunctionSheetBasicData(model);

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "故障处理单确认失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "故障处理单确认成功，故障处理单号为：" + model.SheetNO, Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
        }
        //********** Modification Finished 2012-4-3 ********************************************************************************************** 
        else if (e.CommandName == "del")
        {
            try
            {
                malfunctionBll.DelMalfunctionSheet(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除故障处理单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除故障处理单成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionList.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            MalfunctionHandleInfo item = (MalfunctionHandleInfo)e.Row.DataItem;
            e.Row.Attributes["SheetID"] = item.SheetID.ToString();
        }
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetSearchTerm();
        AspNetPager1.CurrentPageIndex = 1;
        if (SortDropDownList.SelectedValue == "ReportTime")
        {
            FillData2();
        }
        else
        {
            FillData();
        }
    }
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private void GetSearchTerm()
    {
        MalfunctionSearchInfo term = new MalfunctionSearchInfo();

        if (!string.IsNullOrEmpty(tbSheetNO.Text.Trim()))
            term.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());

        if (ddlDepartment.SelectedValue != "0" && ddlDepartment.SelectedValue != "")
            term.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);

        if (!string.IsNullOrEmpty(tbEquipmentNO.Text.Trim()))
            term.EquipmentNO = Common.inSQL(tbEquipmentNO.Text.Trim());

        if (!string.IsNullOrEmpty(tbEquipmentName.Text.Trim()))
            term.EquipmentName = Common.inSQL(tbEquipmentName.Text.Trim());

        if (!string.IsNullOrEmpty(tbReporter.Text.Trim()))
            term.Reporter = Common.inSQL(tbReporter.Text.Trim());

        if (!string.IsNullOrEmpty(tbReportTimeFrom.Text.Trim()))
            term.ReportDateFrom = Convert.ToDateTime(tbReportTimeFrom.Text.Trim());

        if (!string.IsNullOrEmpty(tbReportTimeTo.Text.Trim()))
            term.ReportDateTo = Convert.ToDateTime(tbReportTimeTo.Text.Trim());

        if (!string.IsNullOrEmpty(tbRecorderName.Text.Trim()))
            term.RecorderName = Common.inSQL(tbRecorderName.Text.Trim());

        if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
        {
            //如果没有查看全部故障处理单的权限
            //term.RecordDept = UserData.CurrentUserData.DepartmentID;
        }
        if (ddlStatus.SelectedValue != "0")
            term.Status = Convert.ToInt32(ddlStatus.SelectedValue);

        if (ddlMaintainTeam.SelectedValue != "0")
            term.MaintainDept = Convert.ToUInt32(ddlMaintainTeam.SelectedValue);

        SearchTerm = term;
    }
    /// <summary>
    /// 列表项筛选
    /// </summary>
    private void Filter()
    {
        MalfunctionSearchInfo term = new MalfunctionSearchInfo();

        term.DepartmentID = Convert.ToInt64(ddlFilterDepartment.SelectedValue);
        //term.RecorderName = Common.inSQL(tbFilterRecorder.Text.Trim());
        term.SystemID = ddlSystem.SelectedValue;
        term.MaintainDept = Convert.ToInt64(ddlFilterMaintainTeam.SelectedValue);
        term.MalfunctionRank = Convert.ToInt32(ddlFilterRank.SelectedValue);
        term.Status = Convert.ToInt32(ddlFilterStatus.SelectedValue);
        if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
        {
            //如果没有查看全部故障处理单的权限
            //term.RecordDept = UserData.CurrentUserData.DepartmentID;
        }

        SearchTerm = term;
    }
    /// <summary>
    /// 列表条件筛选
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btFilter_Click(object sender, EventArgs e)
    {
        Filter();
        AspNetPager1.CurrentPageIndex = 1;
        if (SortDropDownList.SelectedValue == "ReportTime")
        {
            FillData2();
        }
        else
        {
            FillData();
        }
    }

    //////////////////////////////////////////////////////////////////////////
    protected void OnFilter(object sender, EventArgs e)
    {
        if (SortDropDownList.SelectedValue == "ReportTime")
        {
            Filter();
            AspNetPager1.CurrentPageIndex = 1;
            FillData2();
        }
        else
        {
            Filter();
            AspNetPager1.CurrentPageIndex = 1;
            FillData();
        }
    }
}
