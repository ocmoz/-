using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Examine;
using WebUtility.Components;
using FM2E.Model.Examine;
using System.Collections;

public partial class Module_FM2E_ExamineManager_ExamineResult_ViewExamineResult : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 1, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 1, DataType.Long);
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
            FillData();
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        if (cmd == "view")
        {
            try
            {
                ExamineResultInfo item = examineBll.GetExamineResult(id);
                //item.Score = item.SeasonExamineRatio * item.SeasonExamineResult + item.DailyExamineRatio * item.DailyExamineResult;

                ExamineSearchInfo term = new ExamineSearchInfo();
                term.ExamineTarget = item.ExamineTarget;
                int year = item.Year;
                ExamineSeason season = item.Season;

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
                DateTime dateTo = DateTime.Parse(string.Format("{0}-{1}-{2}", year, monthTo, DateTime.DaysInMonth(year, monthTo)));

                term.SaveTimeFrom = dateFrom;
                term.SaveTimeTo = dateTo;
                term.Status = ExamineSheetStatus.ExamineConfirmPassed;

                CurrentExamineResult = item;
                IList list = examineBll.GetExamines(term);
                Repeater1.DataSource = list;
                Repeater1.DataBind();

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取考核结果表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    protected void Repeater1_RowDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ExamineInfo item = e.Item.DataItem as ExamineInfo;

            Literal lt = e.Item.FindControl("ltSheetNO") as Literal;
            if (lt != null)
            {
                lt.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看考核表','../Examine/ViewExamine.aspx?cmd=view&id={0}&viewonly=1',800, 430, null,true,true);", item.ExamSheetID), item.ExamSheetNO);
            }
        }
    }

}
