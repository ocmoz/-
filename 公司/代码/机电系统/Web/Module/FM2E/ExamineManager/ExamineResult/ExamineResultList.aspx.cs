using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Examine;
using FM2E.BLL.Examine;
using System.Collections;

public partial class Module_FM2E_ExamineManager_ExamineResult_ExamineResultList : System.Web.UI.Page
{
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
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckPermission(PopedomType.New);
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

        int year = DateTime.Now.Year;
        ddlYears.Items.Clear();
        ddlYears.Items.Add(new ListItem("年度不限", "0"));
        for (int i = year - 8; i <= year; i++)
        {
            ddlYears.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYears.SelectedValue = year.ToString();

        ddlSeason.Items.Clear();
        ddlSeason.Items.Add(new ListItem("季度不限", ((int)ExamineSeason.Unknown).ToString()));
        ListItem[] seasons = EnumHelper.GetListItems(typeof(ExamineSeason), (int)ExamineSeason.Unknown);
        ddlSeason.Items.AddRange(seasons);

        tbExaminer.Text = UserData.CurrentUserData.PersonName;
    }

    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            int recordCount = 0;
            ExamineResultSearchInfo term = GetSearchTerm();
            IList list = examineBll.GetExamineResults(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            gvExamineSheets.DataSource = list;
            gvExamineSheets.DataBind();

            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取考核结果表列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
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
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private ExamineResultSearchInfo GetSearchTerm()
    {
        ExamineResultSearchInfo term = new ExamineResultSearchInfo();

        term.SheetNO = tbSheetNO.Text.Trim();
        term.SheetName = tbSheetName.Text.Trim();
        term.CompanyID = ddlCompany.SelectedValue;
        term.ExamineTarget = Convert.ToInt64(ddlExamineTarget.SelectedValue);
        term.Year = Convert.ToInt32(ddlYears.SelectedValue);
        term.Season = (ExamineSeason)Convert.ToInt32(ddlSeason.SelectedValue);
        term.ExaminerName = tbExaminer.Text.Trim();

        if (!string.IsNullOrEmpty(tbSaveTimeFrom.Text.Trim()))
        {
            term.SaveTimeFrom = Convert.ToDateTime(tbSaveTimeFrom.Text.Trim());
        }
        if (!string.IsNullOrEmpty(tbSaveTimeTo.Text.Trim()))
        {
            term.SaveTimeTo = Convert.ToDateTime(tbSaveTimeTo.Text.Trim());
        }

        return term;
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
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExamineSheets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long id = long.Parse(e.CommandArgument.ToString());
        switch (e.CommandName)
        {
            case "view":
                Response.Redirect("ViewExamineResult.aspx?cmd=view&id=" + id);
                break;
            case "del":
                //删除
                try
                {
                    examineBll.DeleteExamineResult(id);
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除考核结果表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }

                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除考核结果表成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ExamineResultList.aspx"), UrlType.Href, "");
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
}
