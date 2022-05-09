using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WebUtility;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using System.Collections;
using WebUtility.Components;

public partial class Module_FM2E_MaintainManager_MaintainManager_MaintainVerify_MaintainSheetList : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private readonly Maintain maintainBll = new Maintain();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            FillData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        for (int i = 0; i < Repeater_MaintainSheet.Items.Count; i++)
        {
            Repeater_MaintainSheet.Items[i].FindControl("Button_Confirm").Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        }
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    private void CheckPermission()
    {
        //DropDownList_Department.Enabled = false;
        //TextBox_MaintainerName.ReadOnly = true;
        
        ////PermissionA为可以选择维护人
        //if (SystemPermission.CheckPermission(PopedomType.PermissionA))
        //{
        //    TextBox_MaintainerName.ReadOnly = false;
        //}

        ////PermissionB为可以选择任意部门，任意维护人
        //if (SystemPermission.CheckPermission(PopedomType.PermissionB))
        //{
        //    DropDownList_Department.Enabled = true;
        //    TextBox_MaintainerName.ReadOnly = false;
        //}
        
    }

    private void InitPage()
    {
        //系统
        DDLSystem.Items.Clear();
        DDLSystem.Items.AddRange(ListItemHelper.GetSystemListItemsWithBlank());
        DDLSystem.SelectedIndex = 0;

        //部门
        DropDownList_Department.Items.Clear();
        DropDownList_Department.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank());
        

        //类型
        DropDownList_Type.Items.Clear();
        DropDownList_Type.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainType)));

        //周期
        DropDownList_Period.Items.Clear();
        DropDownList_Period.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainIntervalUnit)));

        //异常
        DropDownList_HasAbnormal.Items.Clear();
        DropDownList_HasAbnormal.Items.Add(new ListItem("不限", "0"));
        DropDownList_HasAbnormal.Items.Add(new ListItem("有", "1"));
        DropDownList_HasAbnormal.Items.Add(new ListItem("无", "2"));

        //核实结果
        DropDownList_ConfirmResult.Items.Clear();
        DropDownList_ConfirmResult.Items.AddRange(EnumHelper.GetListItemsEx(typeof(MaintainConfirmResult),(int)MaintainConfirmResult.NotConfirm));

        //姓名
        //TextBox_MaintainerName.Text = UserData.CurrentUserData.PersonName;

        //时间
        //DateTime lower = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //DateTime upper = DateTime.Now;
        //TextBox_TimeLower.Text = lower.ToString("yyyy-MM-dd");
        //TextBox_TimeUpper.Text = upper.ToString("yyyy-MM-dd");

        //TextBox_ConfirmTimeLower.Text = lower.ToString("yyyy-MM-dd");
        //TextBox_ConfirmTimeUpper.Text = upper.ToString("yyyy-MM-dd");
    }


    /// <summary>
    /// 设置鼠标悬停
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_MaintainSheet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr_mainrow");
        if (tr != null)
        {
            //鼠标移动到每项时颜色交替效果    
            tr.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            tr.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
            //设置悬浮鼠标指针形状为"小手"    
            tr.Attributes["style"] = "Cursor:hand";
        }
        RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("RadioButtonList_Confirm");
        if (rbl != null)
        {
            rbl.Items.Clear();
            rbl.Items.AddRange(EnumHelper.GetListItemsEx(typeof(MaintainConfirmResult),(int)MaintainConfirmResult.OnTime, (int)MaintainConfirmResult.Unknown, (int)MaintainConfirmResult.NotConfirm));
        }
    }

    /// <summary>
    /// Repeater事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater_MaintainSheet_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        long id = long.Parse(e.CommandArgument.ToString());
        //编辑
        switch (e.CommandName)
        {
            case "ViewCMD":
                {
                    //转向查看页面
                    Response.Redirect("../MaintainSheet/ViewMaintainSheet.aspx?cmd=approval&id=" + id);
                    break;
                }
                //核实
            case "ConfirmCMD":
                {

                    RadioButtonList rbl = e.Item.FindControl("RadioButtonList_Confirm") as RadioButtonList;
                    MaintainConfirmResult result = (MaintainConfirmResult)Convert.ToInt32(rbl.SelectedValue);//核实结果
                    DateTime time = DateTime.Now;//核实时间
                    string confirmer = UserData.CurrentUserData.UserName;//核实人
                    string remark = (e.Item.FindControl("TextBox_Remark") as TextBox).Text.Trim();
                    maintainBll.DoConfirm(id, result, confirmer, time, remark);
                    FillData();
                    break;
                }
            default: break;
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    /// <summary>
    /// 当前用于筛选的信息
    /// </summary>
    private MaintainSheetSearchInfo CurrentSearchInfo
    {
        get
        {
            MaintainSheetSearchInfo info = (MaintainSheetSearchInfo)ViewState["SearchInfo"];
            if (info == null)
            {
                if (info == null)
                {
                    info = new MaintainSheetSearchInfo();
                    info.SystemID = DDLSystem.SelectedValue;
              
                    info.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
                    info.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_Period.SelectedValue);

                    info.DepartmentID = long.Parse(DropDownList_Department.SelectedValue);

                    info.AddressID = long.Parse(Hidden_AddressID.Value);
                    info.AddressCode = Hidden_AddressCode.Value;
                    info.SheetName = Common.inSQL(TextBox_SheetName.Text.Trim());
                    if (DropDownList_HasAbnormal.SelectedIndex == 1) 
                        info.HasAbnormal = true;
                    else
                        if (DropDownList_HasAbnormal.SelectedIndex == 2)
                            info.HasAbnormal = false;

                    info.ConfirmResult = (MaintainConfirmResult)Convert.ToInt32(DropDownList_ConfirmResult.SelectedValue);

                    info.MaintainerName = Common.inSQL(TextBox_MaintainerName.Text.Trim());

                    DateTime lower = new DateTime();
                    DateTime.TryParse(TextBox_TimeLower.Text.Trim(), out lower);

                    info.MaintainTimeFrom = lower;

                    DateTime upper = new DateTime();
                    DateTime.TryParse(TextBox_TimeUpper.Text.Trim(), out upper);

                    info.MaintainTimeTo = upper;

                    if (DropDownList_IsTemp.SelectedIndex == 1)
                        info.IsTemp = true;
                    else
                        if (DropDownList_IsTemp.SelectedIndex == 2)
                            info.IsTemp = false;
                }

            }
            return info;
        }
        set { ViewState["SearchInfo"] = value; }
    }

    private void FillData()
    {
        try
        {
            MaintainSheetSearchInfo info = CurrentSearchInfo;
            int recordCount = 0;
            IList list = maintainBll.SearchMaintainSheet(info, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            Repeater_MaintainSheet.DataSource = list;
            Repeater_MaintainSheet.DataBind();

            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 筛选记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Filter_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;

        MaintainSheetSearchInfo info = new MaintainSheetSearchInfo();
        info.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
        info.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_Period.SelectedValue);

        info.SystemID = DDLSystem.SelectedValue;
        info.DepartmentID = long.Parse(DropDownList_Department.SelectedValue);

        info.AddressID = long.Parse(Hidden_AddressID.Value);
        info.AddressCode = Hidden_AddressCode.Value;

        if (DropDownList_HasAbnormal.SelectedIndex == 1)
            info.HasAbnormal = true;
        else
            if (DropDownList_HasAbnormal.SelectedIndex == 2)
                info.HasAbnormal = false;

        info.ConfirmResult = (MaintainConfirmResult)Convert.ToInt32(DropDownList_ConfirmResult.SelectedValue);
        info.SheetName = Common.inSQL(TextBox_SheetName.Text.Trim());
        info.MaintainerName = Common.inSQL(TextBox_MaintainerName.Text.Trim());

        DateTime lower = new DateTime();
        DateTime.TryParse(TextBox_TimeLower.Text.Trim(), out lower);

        info.MaintainTimeFrom = lower;

        DateTime upper = new DateTime();
        DateTime.TryParse(TextBox_TimeUpper.Text.Trim(), out upper);

        info.MaintainTimeTo = upper;

        if (DropDownList_IsTemp.SelectedIndex == 1)
            info.IsTemp = true;
        else
            if (DropDownList_IsTemp.SelectedIndex == 2)
                info.IsTemp = false;

        CurrentSearchInfo = info;

        FillData();

        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        PermissionControl();
        //********** Modification Finished 2011-09-09 **********************************************************************************************
    }
}
