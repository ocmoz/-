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

public partial class Module_FM2E_MaintainManager_MaintainManager_MaintainTemplate_MaintainTemplateList : System.Web.UI.Page
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
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.New);
        for (int i = 0; i < Repeater_MaintainPlan.Items.Count; i++)
        {
            Repeater_MaintainPlan.Items[i].FindControl("ImageButton_Do").Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        }
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

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

    }


    /// <summary>
    /// 设置鼠标悬停
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_MaintainPlan_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
    }

    /// <summary>
    /// Repeater事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater_MaintainPlan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        long id = long.Parse(e.CommandArgument.ToString());
        //编辑
        switch (e.CommandName)
        {
            case "ViewCMD":
                {
                    //转向查看页面
                    Response.Redirect("ViewMaintainTemplate.aspx?cmd=view&id=" + id);
                    break;
                }
            //删除
            case "DoCMD":
                {
                    //转向执行页面
                    Response.Redirect("../MaintainSheet/EditMaintainSheet.aspx?cmd=add&templateid=" + id);
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
    private TemplateMaintainSheetSearchInfo CurrentSearchInfo
    {
        get
        {
            TemplateMaintainSheetSearchInfo info = (TemplateMaintainSheetSearchInfo)ViewState["SearchInfo"];
            if (info == null)
            {
                if (info == null)
                {
                    info = new TemplateMaintainSheetSearchInfo();
                    info.SystemID = DDLSystem.SelectedValue;
              
                    info.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
                    info.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_Period.SelectedValue);

                    info.Department = long.Parse(DropDownList_Department.SelectedValue);

                    info.AddressID = long.Parse(Hidden_AddressID.Value);
                    info.AddressCode = Hidden_AddressCode.Value;

                    info.TemplateSheetName = Common.inSQL(TextBox_SheetName.Text.Trim());
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
            TemplateMaintainSheetSearchInfo info = CurrentSearchInfo;
            int recordCount = 0;
            IList list = maintainBll.SearchTemplateMaintainSheet(info, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            Repeater_MaintainPlan.DataSource = list;
            Repeater_MaintainPlan.DataBind();
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

        TemplateMaintainSheetSearchInfo info = new TemplateMaintainSheetSearchInfo();
        info.SystemID = DDLSystem.SelectedValue;

        info.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
        info.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_Period.SelectedValue);

        info.Department = long.Parse(DropDownList_Department.SelectedValue);

        info.AddressID = long.Parse(Hidden_AddressID.Value);
        info.AddressCode = Hidden_AddressCode.Value;

        info.TemplateSheetName = Common.inSQL(TextBox_SheetName.Text.Trim());

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
