using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.Model.Examine;
using WebUtility;
using FM2E.BLL.Examine;
using System.Web.UI.HtmlControls;
using WebUtility.Components;
using System.Collections;

public partial class Module_FM2E_ExamineManager_Examine_ViewExamine : System.Web.UI.Page
{
    /// <summary>
    /// 考核表ID
    /// </summary>
    private long id = (long)Common.sink("id", MethodType.Get, 20, 1, DataType.Long);
    /// <summary>
    /// 命令
    /// </summary>
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 1, DataType.Str);

    private int viewOnly = (int)Common.sink("viewonly", MethodType.Get, 5, 0, DataType.Int);
    /// <summary>
    /// 考核表业务处理对象
    /// </summary>
    private Examine examineBll = new Examine();
    /// <summary>
    /// 考核表类型
    /// </summary>
    protected ExamineType type = ExamineType.DailyExamine;
    /// <summary>
    /// 标题文字
    /// </summary>
    protected string title = "日常";

    /// <summary>
    /// 当前考核表对象
    /// </summary>
    private ExamineInfo CurrentExamine
    {
        get
        {
            if (Session["CurrentExamine"] == null)
            {
                return new ExamineInfo();
            }
            else return (ExamineInfo)Session["CurrentExamine"];
        }
        set
        {
            Session["CurrentExamine"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
        BindButton();
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Edit);
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Delete);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 绑定菜单按钮
    /// </summary>
    private void BindButton()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = string.Format("EditExamine.aspx?cmd=edit&id={0}&type={1}", id, CurrentExamine.ExamineType == ExamineType.DailyExamine ? "daily" : "season");

        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);

        if ((CurrentExamine.Status != ExamineSheetStatus.Draft)
            &&(CurrentExamine.Status!=ExamineSheetStatus.ExamineConfirmNotPassed)
            &&(CurrentExamine.Status!=ExamineSheetStatus.Waiting4ExamineConfirm))
        {
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }

        if (CurrentExamine.Status == ExamineSheetStatus.ExamineConfirmPassed)
        {
            //确认通过后，不可再修改
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
        }

        if (viewOnly == 1)
            HeadMenuWebControls1.Visible = false;
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
                ExamineInfo item = examineBll.GetExamine(id);
                title = EnumHelper.GetDescription(item.ExamineType);
                type = item.ExamineType;
                lbSheetNO.Text = item.ExamSheetNO;
                lbSheetName.Text = item.ExamSheetName;
                lbCompany.Text = item.CompanyName;
                lbExaminer.Text = item.ExaminerName;
                lbExamineTarget.Text = item.ExamineTargetName;
                lbExamineDate.Text = item.SaveTime.ToString("yyyy-MM-dd");
                lbConfirmer.Text = item.ExamineConfirmerName;
                lbConfirmResult.Text = EnumHelper.GetDescription(item.ExamineConfirmResult);
                lbConfirmRemark.Text = item.ExamineConfirmRemark;
                if (item.ExaminerConfirmDate == DateTime.MinValue)
                    lbConfirmDate.Text = "";
                else
                    lbConfirmDate.Text = item.ExaminerConfirmDate.ToString("yyyy-MM-dd");
                CurrentExamine = item;

                if (item.ExamineItems.Count > 0)
                {
                    if (item.ExamineType == ExamineType.DailyExamine)
                        examineBll.ComputeDailyExamineScore((ExamineDetailInfo)item.ExamineItems[0], item.ExamineItems);
                    else examineBll.ComputeSeasonExamineScore((ExamineDetailInfo)item.ExamineItems[0], item.ExamineItems);
                }

                FormatList(item.ExamineItems);
                rptExamineItems.DataSource = item.ExamineItems;
                rptExamineItems.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取考核表数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "delete")
        {
            try
            {
                examineBll.DeleteExamine(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除考核表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除考核表成功", Icon_Type.OK, true, Common.GetHomeBaseUrl("ExamineList.aspx"), UrlType.Href, "");

        }

    }

    /// <summary>
    /// 考核项列表格式化
    /// </summary>
    /// <param name="examineList"></param>
    private void FormatList(IList examineList)
    {
        if (examineList == null || examineList.Count <= 0)
            return;

        int lastLevel = 1;
        List<int> numberList = new List<int>();
        numberList.Add(0);
        foreach (ExamineDetailInfo item in examineList)
        {
            if (item.Level == lastLevel)
            {
                //item.Level-1位加1
                numberList[item.Level - 1]++;
            }
            else if (item.Level > lastLevel)
            {
                //strLastNO后接.1
                if (numberList.Count < item.Level)
                    numberList.Add(1);
                else
                    numberList[item.Level - 1] = 1;
            }
            else if (item.Level < lastLevel)
            {
                //取strLastNO的前item.Level位，并将最后一位加1
                //item.Level-1位加1
                numberList[item.Level - 1]++;
            }

            lastLevel = item.Level;
            item.NO = "";
            item.NO = GetNO(numberList, item.Level) + " ";
            for (int i = 0; i < item.Level - 1; i++)
            {
                item.NO = "&nbsp;&nbsp;&nbsp;" + item.NO;
            }
        }
    }

    private string GetNO(List<int> numberList, int level)
    {
        string strNO = "";

        for (int i = 0; i < level; i++)
        {
            if (i == 0)
                strNO += numberList[i];
            else
                strNO += ("." + numberList[i]);
        }

        return strNO;
    }
    protected void rptExamineItems_DataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlTableRow tr = e.Item.FindControl("trItem") as HtmlTableRow;
        if (tr != null)
        {
            //鼠标移动到每项时颜色交替效果    
            tr.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            tr.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
            //设置悬浮鼠标指针形状为"小手"    
            tr.Attributes["style"] = "Cursor:hand;text-align:left;";
        }
    }
}
