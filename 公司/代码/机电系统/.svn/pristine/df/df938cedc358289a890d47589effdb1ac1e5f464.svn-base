using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Examine;
using System.Collections;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using FM2E.Model.Examine;

public partial class Module_FM2E_ExamineManager_ExamineItemConfig_ExamineItemConfig : System.Web.UI.Page
{
    /// <summary>
    /// 考核业务处理对象
    /// </summary>
    private Examine examineBll = new Examine();

    /// <summary>
    /// 当前正在编辑的考核项
    /// </summary>
    private ExamineItemInfo CurrentExamineItem
    {
        get
        {
            if (ViewState["CurrentExamineItem"] == null)
                return new ExamineItemInfo();
            else return ViewState["CurrentExamineItem"] as ExamineItemInfo;
        }
        set
        {
            ViewState["CurrentExamineItem"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
        }
        
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        bool bedit=SystemPermission.CheckPermission(PopedomType.Edit);
        bool bnew=SystemPermission.CheckPermission(PopedomType.New);
        bool bdel=SystemPermission.CheckPermission(PopedomType.Delete);
        for(int i=0;i<rptExamineItems.Items.Count;i++)
        {
            LinkButton lb = (LinkButton)rptExamineItems.Items[i].FindControl("linkButtonEdit");
            lb.Enabled = bedit;
            rptExamineItems.Items[i].FindControl("imgBtAdd").Visible = bnew;
            rptExamineItems.Items[i].FindControl("imgBtDel").Visible = bdel;
        }
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************


    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            IList list = examineBll.GetSubExamineItems(0);    //获取所有考核项
            FormatList(list);
            rptExamineItems.DataSource = list;
            rptExamineItems.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取考核项数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        PermissionControl();
        //********** Modification Finished 2011-09-09 **********************************************************************************************
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
        foreach (ExamineItemInfo item in examineList)
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
                    numberList[item.Level-1] = 1;
            }
            else if (item.Level < lastLevel)
            {
                //取strLastNO的前item.Level位，并将最后一位加1
                //item.Level-1位加1
                numberList[item.Level - 1]++;
                //for (int i = item.Level; i < numberList.Count; i++)
                //    numberList[i] = 0;
            }

            lastLevel = item.Level;
            item.ItemName = GetNO(numberList, item.Level) +" "+ item.ItemName;
            for (int i = 0; i < item.Level - 1; i++)
            {
                item.ItemName = "&nbsp;&nbsp;&nbsp;" + item.ItemName;
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
    protected void rptExamineItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        long id = long.Parse(e.CommandArgument.ToString());
        switch (e.CommandName)
        {
            case "AddItem":

                try
                {
                    ExamineItemInfo item = examineBll.GetExamineItem(id);
                    CurrentExamineItem = item;
                    lbParentItem.Text = item.ItemName;
                    lbParentScore.Text = item.Score.ToString("0.##") ;
                    lbExamType.Text = EnumHelper.GetDescription(item.ExamineType);

                    if (item.ExamineType == ExamineType.SeasonExamine)
                    {
                        float tmp = item.Score - item.ScoreOfChild;
                        if (tmp > 0)
                        {
                            tbScore.Text = tmp.ToString();
                        }
                        else if (tmp < 0)
                        {
                            tbScore.Text = "子项之和大于父项分数";
                        }
                        else tbScore.Text = "";
                    }
                    else tbScore.Text = "";
  
                    rptSubExamineItems.DataSource = examineBll.GetChildExamineItems(id);
                    rptSubExamineItems.DataBind();
                    divAdd.Visible = true;
                    divEmpty.Visible = false;
                }
                catch (Exception ex)
                {
                    divAdd.Visible = false;
                    divEmpty.Visible = true;
                    EventMessage.EventWriteLog(Msg_Type.Error, "获取考核项信息失败，原因：" + ex.Message);
                }
                popupAddExamItem.Show();
                break;
            case "EditItem":
                try
                {
                    ExamineItemInfo item = examineBll.GetExamineItem(id);
                    CurrentExamineItem = item;
                    tbEditItemName.Text = item.ItemName;
                    tbEditContent.Text = item.Content;
                    tbEditScore.Text = item.Score.ToString();
                    tbEditThreshold.Text = item.Threshold.ToString();
                    tbEditStandard.Text = item.Standard;
                    lbEditExamType.Text = EnumHelper.GetDescription(item.ExamineType);
                    lbEditError.Text = "";
                }
                catch(Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "获取考核项信息失败，原因：" + ex.Message);
                }
                popupEditExamItem.Show();

                break;
            //删除考核项
            case "DeleteItem":
                try
                {
                    examineBll.DeleteExamineItem(id);
                    FillData();
                }
                catch (Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "删除考核项信息失败，原因：" + ex.Message);
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel_List, this.GetType(), "DeleteError", "alert('删除考核项失败');", true);
                }
                break;
                
            default: break;
        }
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
            ExamineItemInfo item = new ExamineItemInfo();
            item.Content = tbContent.Text.Trim();
            item.ExamineType = CurrentExamineItem.ExamineType;
            item.ChildCount = 0;
            item.ItemName = tbItemName.Text.Trim();
            item.Level = CurrentExamineItem.Level + 1;
            item.ParentItem = CurrentExamineItem.ExamItemID;
            item.Score = Convert.ToSingle(tbScore.Text.Trim());
            item.Standard = tbStandard.Text.Trim();
            if (!string.IsNullOrEmpty(tbThreshold.Text.Trim()))
                item.Threshold = Convert.ToSingle(tbThreshold.Text.Trim());
            else item.Threshold = 0;

            examineBll.SaveExamineItem(item);

            //生新绑定子列表
            rptSubExamineItems.DataSource = examineBll.GetChildExamineItems(CurrentExamineItem.ExamItemID);
            rptSubExamineItems.DataBind();

            CurrentExamineItem.ScoreOfChild += item.Score;

            tbContent.Text = "";
            tbItemName.Text = "";
            tbScore.Text = "";
            if (item.ExamineType == ExamineType.SeasonExamine)
            {
                float tmp = CurrentExamineItem.Score - CurrentExamineItem.ScoreOfChild;
                if (tmp > 0)
                {
                    tbScore.Text = tmp.ToString();
                }
                else if (tmp < 0)
                {
                    tbScore.Text = "子项之和大于父项分数";
                }
            }
            
            tbStandard.Text = "";
            tbThreshold.Text = "0";

        }
        catch (Exception ex)
        {
            string errorMsg = "错误：添加子考核项失败，原因：" + ex.Message;
            EventMessage.EventWriteLog(Msg_Type.Error, errorMsg);
            lbErrorMsg.Text = errorMsg;
        }
    }
    /// <summary>
    /// 关闭添加子考核项的面板
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btClose_Click(object sender, EventArgs e)
    {
        popupAddExamItem.Hide();
        FillData();
    }
    /// <summary>
    /// 关闭修改考核项的面板
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btEditClose_Click(object sender, EventArgs e)
    {
        popupEditExamItem.Hide();
        FillData();
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
            ExamineItemInfo item = CurrentExamineItem;
            item.Content = tbEditContent.Text.Trim();
            item.ItemName = tbEditItemName.Text.Trim();
            item.Score = Convert.ToSingle(tbEditScore.Text.Trim());
            item.Standard = tbEditStandard.Text.Trim();
            if (!string.IsNullOrEmpty(tbEditThreshold.Text.Trim()))
                item.Threshold = Convert.ToSingle(tbEditThreshold.Text.Trim());
            else item.Threshold = 0;

            examineBll.SaveExamineItem(item);

            popupEditExamItem.Hide();
            FillData();
        }
        catch (Exception ex)
        {
            string msg = "错误：修改考核项信息失败，原因：" + ex.Message;
            EventMessage.EventWriteLog(Msg_Type.Error, msg+",stack:\r\n"+ex.StackTrace);
            lbEditError.Text = msg;
        }
    }
}
