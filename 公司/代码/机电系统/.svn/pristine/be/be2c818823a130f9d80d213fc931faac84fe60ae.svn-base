using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.Model.Examine;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Examine;
using System.Collections;

public partial class Module_FM2E_ExamineManager_ExamineConfirm_ExamineList : System.Web.UI.Page
{
    /// <summary>
    /// 考核业务处理对象
    /// </summary>
    private Examine examineBll = new Examine();

    protected void Page_Load(object sender, EventArgs e)
    {
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
        gvExamineSheets.Columns[gvExamineSheets.Columns.Count - 1].Visible = SystemPermission.CheckPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        //公司
        ListItem[] companyList = ListItemHelper.GetCompanyListItemsWithBlank();
        companyList[0].Text = "不限";
        ddlCompany.Items.AddRange(companyList);

        ddlCompany.SelectedValue = UserData.CurrentUserData.CompanyID;

        if (!SystemPermission.CheckPermission(PopedomType.PermissionA))
        {
            //如果没有查看各公司考核单的权限
            ddlCompany.Enabled = false;
        }

        //考核对象
        ddlExamineTarget.Items.Clear();
        ddlExamineTarget.Items.Add(new ListItem("不限", "0"));
        ddlExamineTarget.Items.AddRange(ListItemHelper.GetAllMaintainTeams(""));


        //考核类型
        ListItem[] examineTypes = EnumHelper.GetListItems(typeof(ExamineType), (int)ExamineType.Unknown);

        ddlExamineType.Items.Clear();
        ddlExamineType.Items.Add(new ListItem("不限", ((int)ExamineType.Unknown).ToString()));
        ddlExamineType.Items.AddRange(examineTypes);

        tbExaminer.Text = UserData.CurrentUserData.PersonName;
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            ExamineSearchInfo term = GetSearchTerm();
            int recordCount = 0;
            IList list=examineBll.GetExamines(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            gvExamineSheets.DataSource = list;
            gvExamineSheets.DataBind();
            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取考核表列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private ExamineSearchInfo GetSearchTerm()
    {
        ExamineSearchInfo item = new ExamineSearchInfo();

        item.ExamSheetNO = tbSheetNO.Text.Trim();
        item.CompanyID = ddlCompany.SelectedValue;
        item.ExamineTarget =Convert.ToInt64(ddlExamineTarget.SelectedValue);
        item.ExaminerName = tbExaminer.Text.Trim();
        item.ExamineType = Convert.ToInt32(ddlExamineType.SelectedValue);
        item.Status = ExamineSheetStatus.Waiting4ExamineConfirm;

        if (!string.IsNullOrEmpty(tbSaveTimeFrom.Text.Trim()))
        {
            item.SaveTimeFrom = Convert.ToDateTime(tbSaveTimeFrom.Text.Trim());
        }
        if (!string.IsNullOrEmpty(tbSaveTimeTo.Text.Trim()))
        {
            item.SaveTimeTo = Convert.ToDateTime(tbSaveTimeTo.Text.Trim());
        }

        return item;
    }
    /// <summary>
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExamineSheets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
          long id = long.Parse(e.CommandArgument.ToString());
          switch (e.CommandName)
          {
              case "approval":
                  Response.Redirect("ConfirmExamine.aspx?cmd=approval&id=" + id);
                  break;
          }
    }
    /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExamineSheets_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }
}
