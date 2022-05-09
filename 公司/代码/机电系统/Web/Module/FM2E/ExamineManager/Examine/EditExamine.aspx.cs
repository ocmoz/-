using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Examine;
using System.Web.UI.HtmlControls;
using System.Collections;
using FM2E.Model.Examine;
using FM2E.Model.System;
using WebUtility.Components;
using FM2E.BLL.Utils;
using FM2E.Model.Utils;

public partial class Module_FM2E_ExamineManager_Examine_EditExamine : System.Web.UI.Page
{
    /// <summary>
    /// 操作命令
    /// </summary>
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 1, DataType.Str);
    /// <summary>
    /// 考核表ID
    /// </summary>
    private long examineID = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    /// <summary>
    /// 考核表种类
    /// </summary>
    private string examineType = (string)Common.sink("type", MethodType.Get, 10, 1, DataType.Str);
    /// <summary>
    /// 考核业务处理对象
    /// </summary>
    private Examine examineBll = new Examine();

    protected string title = "日常";

    protected ExamineType type = ExamineType.DailyExamine;

    /// <summary>
    /// 当前正在编辑的考核表
    /// </summary>
    private ExamineInfo CurrentExamineSheet
    {
        get
        {
            if (Session["CurrentExamSheet"] == null)
            {
                return new ExamineInfo();
            }
            else return (ExamineInfo)Session["CurrentExamSheet"];
        }
        set
        {
            Session["CurrentExamSheet"] = value;
        }
    }
    /// <summary>
    /// 当前的考核项明细
    /// </summary>
    private ArrayList CurrentExamItems
    {
        get
        {
            if (Session["CurrentExamItems"] == null)
            {
                return new ArrayList();
            }
            else return (ArrayList)Session["CurrentExamItems"];
        }
        set
        {
            Session["CurrentExamItems"] = value;
        }
    }
    /// <summary>
    /// 当前考核项
    /// </summary>
    private ExamineDetailInfo CurrentExamItem
    {
        get
        {
            if (Session["CurrentItem"] == null)
                return new ExamineDetailInfo();
            else return (ExamineDetailInfo)Session["CurrentItem"];
        }
        set
        {
            Session["CurrentItem"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        type = (examineType == "daily") ? ExamineType.DailyExamine : ExamineType.SeasonExamine;
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
        ButtonBind();
    }
    /// <summary>
    /// 初始化页面 
    /// </summary>
    private void InitialPage()
    {
        //考核对象
        ddlExamineTarget.Items.Clear();
        //ddlExamineTarget.Items.Add(new ListItem("不限", "0"));
        ddlExamineTarget.Items.AddRange(ListItemHelper.GetAllMaintainTeams(""));

        CurrentExamItems = null;
        CurrentExamineSheet = null;
        CurrentExamItem = null;
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
                lbCompany.Text = UserData.CurrentUserData.CompanyName;
                lbExaminer.Text = UserData.CurrentUserData.PersonName;
                tbSaveTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ArrayList list = (ArrayList)examineBll.GetSubExamineItems(0); //获取所有考核项（树形结构）
                int index = 0;

                foreach (ExamineItemInfo item in list)
                {
                    if (item.ExamineType == type)
                    {
                        break;
                    }
                    index++;
                }
                LoginUserInfo loginUser = UserData.CurrentUserData;
                ArrayList examList = new ArrayList();
                for (int i = 0; i < list.Count; i++)
                {
                    ExamineItemInfo examineItem = (ExamineItemInfo)list[i];
                    if (examineItem.ExamineType == type)
                    {
                        ExamineDetailInfo item = new ExamineDetailInfo();
                        item.CanAddChild = (examineItem.ChildCount == 0) ? true : false;
                        item.ChildCount = examineItem.ChildCount;
                        item.Content = examineItem.Content;
                        item.ExamineDate = DateTime.Now;
                        item.Examiner = loginUser.UserName;
                        item.ExamItemID = examineItem.ExamItemID;
                        item.ItemName = examineItem.ItemName;
                        item.Level = examineItem.Level;
                        item.ParentItem = examineItem.ParentItem;
                        item.Score = examineItem.Score;
                        item.Standard = examineItem.Standard;
                        item.Threshold = examineItem.Threshold;
                        item.UpdateTime = DateTime.Now;
                        item.ConfirmResult = ExamineConfirmResult.NotConfirmed;
                        examList.Add(item);
                    }
                }
                CurrentExamItems = examList;
                BindRepeater();
            }
            else if (cmd == "edit")
            {
                ExamineInfo item = examineBll.GetExamine(examineID);
                CurrentExamineSheet = item;
                CurrentExamItems = (ArrayList)item.ExamineItems;

                lbSheetNO.Text = item.ExamSheetNO;
                tbSheetName.Text = item.ExamSheetName;
                lbCompany.Text = item.CompanyName;
                lbExaminer.Text = item.ExaminerName;
                ddlExamineTarget.SelectedValue = item.ExamineTarget.ToString();
                tbSaveTime.Text = item.SaveTime.ToString("yyyy-MM-dd");
                
                BindRepeater();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "填充页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 进行repeater的数据绑定
    /// </summary>
    private void BindRepeater()
    {
        ArrayList examList = CurrentExamItems;
        if (examList.Count > 0)
        {
            if (type == ExamineType.DailyExamine)
                examineBll.ComputeDailyExamineScore((ExamineDetailInfo)examList[0], examList);
            else examineBll.ComputeSeasonExamineScore((ExamineDetailInfo)examList[0], examList);
        }
        FormatList(examList);
        rptExamineItems.DataSource = examList;
        rptExamineItems.DataBind();
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
    /// <summary>
    /// 菜单绑定
    /// </summary>
    private void ButtonBind()
    {
        if (examineType == "daily")
            title = "日常";
        else if (examineType == "season")
            title = "季度";

        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadTitleTxt = ((examineType == "daily") ? "填写日常考核表" : "填写季度考核表");
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：" + ((examineType == "daily") ? "填写日常考核表" : "填写季度考核表");
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadTitleTxt = ((examineType == "daily") ? "编辑日常考核表" : "编辑季度考核表");
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：" + ((examineType == "daily") ? "编辑日常考核表" : "编辑季度考核表");
        }

        if (CurrentExamineSheet.Status != ExamineSheetStatus.Unknown && CurrentExamineSheet.Status != ExamineSheetStatus.Draft)
        {
            btSaveDraft.Visible = false;
        }
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
    protected void rptExamineItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        long id = long.Parse(e.CommandArgument.ToString());
        switch (e.CommandName)
        {
            case "AddItem":
                ExamineDetailInfo item = GetExamItem(id);
                lbParentItem.Text = item.ItemName;
                lbParentScore.Text = item.Score.ToString("0.##");
                lbStandard.Text = item.Standard;
                lbContent.Text = item.Content;
                tbExamineDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                CurrentExamItem = item;

                IList list = GetChildExamItems(id);
                rptSubExamineItems.DataSource = list;
                rptSubExamineItems.DataBind();
                popupAddExamItem.Show();
                break;
            case "EditItem":
                try
                {
                    ExamineDetailInfo it = GetExamItem(id);
                    CurrentExamItem = it;

                    ExamineDetailInfo parent = GetExamItem(it.ParentItem);
                    lbEditContent.Text = parent.Content;
                    lbEditParentItem.Text = parent.ItemName;
                    lbEditParentScore.Text = parent.Score.ToString();;
                    lbEditStandard.Text = parent.Standard;
                    tbEditDeduct.Text = it.Deduct.ToString(); ;
                    tbEditDeductReason.Text = it.DeductReason;
                    tbEditExamineDate.Text = it.ExamineDate.ToString("yyyy-MM-dd");
                    tbEditItemName.Text = it.ItemName;
                    tbEditRemark.Text = it.Remark;
               
                    lbEditError.Text = "";

                    popupEditExamItem.Show();
                }
                catch (Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "获取考核项信息失败，原因：" + ex.Message);
                }
                popupEditExamItem.Show();
                break;
            //删除考核项
            case "DeleteItem":
                int i = 0;
                long parentId = 0;
                for (i = 0; i < CurrentExamItems.Count; i++)
                {
                    ExamineDetailInfo it = (ExamineDetailInfo)CurrentExamItems[i];
                    if (it.ExamItemID == id)
                    {
                        parentId = it.ParentItem;
                        break;
                    }
                }
                CurrentExamItems.RemoveAt(i);
                for (i = 0; i < CurrentExamItems.Count; i++)
                {
                    ExamineDetailInfo it = (ExamineDetailInfo)CurrentExamItems[i];
                    if (it.ExamItemID == parentId)
                    {
                        it.ChildCount--;
                        break;
                    }
                }
                BindRepeater();
                break;

            default: break;
        }
    }

    /// <summary>
    /// 获取某个特定的考核项
    /// </summary>
    /// <param name="examItemID"></param>
    /// <returns></returns>
    private ExamineDetailInfo GetExamItem(long examItemID)
    {
        ExamineDetailInfo item = null;
        foreach (ExamineDetailInfo it in CurrentExamItems)
        {
            if (it.ExamItemID == examItemID)
            {
                item = it;
            }
        }
        return item;
    }
    /// <summary>
    /// 获取子考核项
    /// </summary>
    /// <param name="examItemID"></param>
    /// <returns></returns>
    private IList GetChildExamItems(long examItemID)
    {
        ArrayList list = new ArrayList();
        foreach (ExamineDetailInfo item in CurrentExamItems)
        {
            if (item.ParentItem == examItemID)
            {
                list.Add(item);
            }
        }
        return list;
    }
    /// <summary>
    ///添加子考核项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ExamineDetailInfo item = new ExamineDetailInfo();
            item.CanAddChild = false;
            item.ChildCount = 0;
            item.Deduct = Convert.ToSingle(tbDeduct.Text.Trim());
            item.DeductReason = tbDeductReason.Text.Trim();
            item.ExamineDate = Convert.ToDateTime(tbExamineDate.Text.Trim());
            item.Examiner = UserData.CurrentUserData.UserName;
            item.ExaminerName = UserData.CurrentUserData.PersonName;
            item.ExamItemID = FindNextExamItemID();
            item.ItemName = tbItemName.Text.Trim();
            item.ParentItem = CurrentExamItem.ExamItemID;
            item.Remark = tbRemark.Text.Trim();
            item.UpdateTime = DateTime.Now;
            item.Level = CurrentExamItem.Level + 1;

            int index = 0;
            foreach (ExamineDetailInfo it in CurrentExamItems)
            {
                if (it.ExamItemID == CurrentExamItem.ExamItemID)
                    break;
                index++;
            }
            if (index + 1 <= CurrentExamItems.Count - 1)
                CurrentExamItems.Insert(index+1, item);
            else CurrentExamItems.Add( item);

            CurrentExamItem.ChildCount++;

            IList list = GetChildExamItems(CurrentExamItem.ExamItemID);
            rptSubExamineItems.DataSource = list;
            rptSubExamineItems.DataBind();

            tbDeduct.Text = "";
            tbDeductReason.Text = "";
            tbItemName.Text = "";
            tbRemark.Text = "";
        }
        catch (Exception ex)
        {
            string errorMsg = "错误：添加子考核项失败，原因：" + ex.Message;
            EventMessage.EventWriteLog(Msg_Type.Error, errorMsg);
            lbErrorMsg.Text = errorMsg;
        }
    }
    /// <summary>
    /// 获取下一结点
    /// </summary>
    /// <returns></returns>
    private long FindNextExamItemID()
    {
        long max = 0;
        foreach (ExamineDetailInfo item in CurrentExamItems)
        {
            if (item.ExamItemID > max)
                max = item.ExamItemID;
        }
        return max + 1;
    }
    /// <summary>
    /// 关闭添加子考核项的面板
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btClose_Click(object sender, EventArgs e)
    {
        BindRepeater();
        popupAddExamItem.Hide();
    }
    /// <summary>
    /// 关闭修改考核项的面板
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btEditClose_Click(object sender, EventArgs e)
    {
        BindRepeater();
        popupEditExamItem.Hide();
    }
    /// <summary>
    /// 修改考核项信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btEdit_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ExamineDetailInfo item in CurrentExamItems)
            {
                if (item.ExamItemID == CurrentExamItem.ExamItemID)
                {
                    item.DeductReason = tbEditDeductReason.Text.Trim();
                    item.Deduct = Convert.ToSingle(tbEditDeduct.Text.Trim());
                    item.ItemName = tbEditItemName.Text;
                    item.Remark = tbEditRemark.Text.Trim();
                    item.ExamineDate = Convert.ToDateTime(tbEditExamineDate.Text.Trim());
                    break;
                }
            }
            popupEditExamItem.Hide();
            BindRepeater();
        }
        catch (Exception ex)
        {
            string msg = "错误：修改考核项信息失败，原因：" + ex.Message;
            EventMessage.EventWriteLog(Msg_Type.Error, msg + ",stack:\r\n" + ex.StackTrace);
            lbEditError.Text = msg;
        }
    }
    /// <summary>
    /// 保存草稿
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSaveDraft_Click(object sender, EventArgs e)
    {
        string sheetNO = "";
        try
        {
            ExamineInfo item = CurrentExamineSheet;
            item.ExamineConfirmResult = ExamineConfirmResult.NotConfirmed;
            item.ExamineTarget = Convert.ToInt64(ddlExamineTarget.SelectedValue);
            item.ExamineType = type;
            item.ExamSheetName = tbSheetName.Text.Trim();
            item.SaveTime = Convert.ToDateTime(tbSaveTime.Text.Trim());
            item.Status = ExamineSheetStatus.Draft;
            item.UpdateTime = DateTime.Now;
            item.TargetConfirmResult = ExamineConfirmResult.NotConfirmed;
            item.ExamineItems = CurrentExamItems;

            item.Score = ((ExamineDetailInfo)CurrentExamItems[0]).ExamScore;

            if (cmd == "add")
            {
                item.CompanyID = UserData.CurrentUserData.CompanyID;
                item.Examiner = UserData.CurrentUserData.UserName;
                item.ExamSheetNO = SheetNOGenerator.GetSheetNO(item.CompanyID, SheetType.EXAMINESHEET);
            }
            sheetNO = item.ExamSheetNO;
            examineBll.SaveExamine(item);
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "保存考核表失败,原因:" + ex.Message);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel_Button, this.GetType(), "ErrorMsg", string.Format("alert('保存考核表草稿失败，原因：{0}');",ex.Message), true);
            return;
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("保存考核表草稿成功,考核表单号为：{0}", sheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("ExamineList.aspx"), UrlType.Href, "");
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btCommit_Click(object sender, EventArgs e)
    {
        string sheetNO = "";
        try
        {
            ExamineInfo item = CurrentExamineSheet;
            item.ExamineConfirmResult = ExamineConfirmResult.NotConfirmed;
            item.ExamineTarget = Convert.ToInt64(ddlExamineTarget.SelectedValue);
            item.ExamineType = type;
            item.ExamSheetName = tbSheetName.Text.Trim();
            item.SaveTime = Convert.ToDateTime(tbSaveTime.Text.Trim());
            item.Status = ExamineSheetStatus.Waiting4ExamineConfirm;
            item.UpdateTime = DateTime.Now;
            item.TargetConfirmResult = ExamineConfirmResult.NotConfirmed;
            item.ExamineItems = CurrentExamItems;

            item.Score = ((ExamineDetailInfo)CurrentExamItems[0]).ExamScore;

            if (cmd == "add")
            {
                item.CompanyID = UserData.CurrentUserData.CompanyID;
                item.Examiner = UserData.CurrentUserData.UserName;
                item.ExamSheetNO = SheetNOGenerator.GetSheetNO(item.CompanyID, SheetType.EXAMINESHEET);
            }
            sheetNO = item.ExamSheetNO;
            examineBll.SaveExamine(item);
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "提交考核表失败,原因:" + ex.Message);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel_Button, this.GetType(), "ErrorMsg", string.Format("alert('提交考核表失败，原因：{0}');", ex.Message), true);
            return;
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("提交考核表成功,考核表单号为：{0}", sheetNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("ExamineList.aspx"), UrlType.Href, "");
    }
}
