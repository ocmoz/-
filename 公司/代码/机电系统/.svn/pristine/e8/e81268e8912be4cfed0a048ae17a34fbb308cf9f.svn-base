using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Examine;
using FM2E.Model.Examine;
using System.Collections;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Utils;

public partial class Module_FM2E_ExamineManager_ExamineResult_GetExamineResult : System.Web.UI.Page
{
    private Examine examineBll = new Examine();

    /// <summary>
    /// 考核结果
    /// </summary>
    protected ExamineResultInfo CurrentExamineResult
    {
        get
        {
            if (Session["CurrentExamineResult"] == null)
            {
                return new ExamineResultInfo();
            }
            else return (ExamineResultInfo)Session["CurrentExamineResult"];
        }
        set
        {
            Session["CurrentExamineResult"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        DailyExamineRatio_inp.Value = Common.DailyExamineRatio.ToString();
        SeasonExamineRatio_inp.Value = Common.SeasonExamineRatio.ToString();
        //考核对象
        ddlExamineTarget.Items.Clear();
        ddlExamineTarget.Items.AddRange(ListItemHelper.GetAllMaintainTeams(""));

        int year = DateTime.Now.Year;
        ddlYears.Items.Clear();
        for (int i = year - 8; i <= year; i++)
        {
            ddlYears.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYears.SelectedValue = year.ToString();

        ddlSeason.Items.Clear();
        ListItem[] seasons = EnumHelper.GetListItems(typeof(ExamineSeason), (int)ExamineSeason.Unknown);
        ddlSeason.Items.AddRange(seasons);

        CurrentExamineResult = null;
    }
    /// <summary>
    /// 生成考核结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            ExamineSearchInfo term = new ExamineSearchInfo();
            term.ExamineTarget = Convert.ToInt64(ddlExamineTarget.SelectedValue);
            int year = Convert.ToInt32(ddlYears.SelectedValue);
            ExamineSeason season = (ExamineSeason)Convert.ToInt32(ddlSeason.SelectedValue);

            int monthFrom = 0;
            int monthTo = 0;
            switch (season)
            {
                case ExamineSeason.SeasonOne:
                    monthFrom = 1;
                    monthTo = 3;
                    break;
                case ExamineSeason.SeasonTwo:
                    monthFrom = 4;
                    monthTo = 6;
                    break;
                case ExamineSeason.SeasonThree:
                    monthFrom = 7;
                    monthTo = 9;
                    break;
                case ExamineSeason.SeasonFour:
                    monthFrom = 10;
                    monthTo = 12;
                    break;
            }
            DateTime dateFrom = DateTime.Parse(string.Format("{0}-{1}-{2}", year, monthFrom, 1));
            DateTime dateTo = DateTime.Parse(string.Format("{0}-{1}-{2}", year, monthTo, DateTime.DaysInMonth(year,monthTo)));

            term.SaveTimeFrom = dateFrom;
            term.SaveTimeTo = dateTo;
            term.Status = ExamineSheetStatus.ExamineConfirmPassed;

            ExamineResultInfo temp = CurrentExamineResult;
            temp.CompanyID = UserData.CurrentUserData.CompanyID;

            float dailyexamineratio = 0;
            float seasonexamineratio = 0;

            try
            {
                dailyexamineratio = (float)Convert.ToSingle(DailyExamineRatio_inp.Value);
                seasonexamineratio = (float)Convert.ToSingle(SeasonExamineRatio_inp.Value);
            }
            catch (Exception ee)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入的比例必须是数字", ee, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            Common.DailyExamineRatio = dailyexamineratio;
            Common.SeasonExamineRatio = seasonexamineratio;


            temp.DailyExamineRatio = dailyexamineratio/100;
            temp.DailyExamineResult = 0;
            temp.ExamineConfirmResult = ExamineConfirmResult.NotConfirmed;
            temp.Examiner = UserData.CurrentUserData.UserName;
            temp.ExamineTarget = Convert.ToInt32(ddlExamineTarget.SelectedValue);
            temp.SaveTime = DateTime.Now;
            temp.Season = season;
            temp.SeasonExamineRatio = seasonexamineratio/100;
            temp.SeasonExamineResult = 0;
            temp.SheetName = "";
            temp.TargetConfirmResult = ExamineConfirmResult.NotConfirmed;
            temp.UpdateTime = DateTime.Now;
            temp.Year = year;
            CurrentExamineResult = temp;

            IList list = examineBll.GetExamines(term);
            Repeater1.DataSource = list;
            Repeater1.DataBind();

            if (list.Count <= 0)
                ButtonTable.Visible = false;
            else ButtonTable.Visible = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取考核表数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");

        }
    }

    /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private int dailyCount = 0;//每次postback都会自动初始化
    private int seasonCount = 0;//每次postback都会自动初始化
    protected void Repeater1_RowDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ExamineInfo item = e.Item.DataItem as ExamineInfo;
            ExamineResultInfo temp = CurrentExamineResult;
            if (item.ExamineType == ExamineType.DailyExamine)
            {
                dailyCount++;
                temp.DailyExamineResult += item.Score;
            }
            else
            {
                seasonCount++;
                temp.SeasonExamineResult += item.Score;
            }
            CurrentExamineResult = temp;

            Literal lt = e.Item.FindControl("ltSheetNO") as Literal;
            if (lt != null)
            {
                lt.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看考核表','../Examine/ViewExamine.aspx?cmd=view&id={0}&viewonly=1',800, 430, null,true,true);",item.ExamSheetID), item.ExamSheetNO);
            }
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            ExamineResultInfo temp = CurrentExamineResult;
            if(seasonCount!=0)
                temp.SeasonExamineResult = temp.SeasonExamineResult / seasonCount;
            if(dailyCount!=0)
                temp.DailyExamineResult = temp.DailyExamineResult / dailyCount;
            //temp.Score = temp.DailyExamineRatio * temp.DailyExamineResult + temp.SeasonExamineRatio * temp.SeasonExamineResult;
            CurrentExamineResult = temp;
        }
    }
    /// <summary>
    /// 保存考核结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            ExamineResultInfo item = CurrentExamineResult;
            item.SheetName = tbSheetName.Text.Trim();
            item.SheetNO = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, FM2E.Model.Utils.SheetType.EXAMINERESULT);
            examineBll.SaveExamineResult(CurrentExamineResult);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存考核结果失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存考核结果成功", Icon_Type.OK, true,Common.GetHomeBaseUrl("ExamineResultList.aspx"), UrlType.Href, "");

    }

}
