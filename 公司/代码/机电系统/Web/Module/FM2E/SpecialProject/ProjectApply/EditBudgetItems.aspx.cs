using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.Model.SpecialProject;
using FM2E.Model.Exceptions;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.SpecialProject;
public partial class Module_FM2E_SpecialProject_ProjectApply_EditBudgetItems : System.Web.UI.Page
{
    SpecialProject specialProjectBll = new SpecialProject();

    /// <summary>
    /// 命令，包括cmd=new新建、cmd=edit编辑
    /// </summary>
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 0, 0, DataType.Str);
    /// <summary>
    /// 编辑专项工程的ID
    /// </summary>
    private long id = (long)Common.sink("projectid", MethodType.Get, 0, 0, DataType.Long);

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectApply_EditBudgetItems";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "Apply.aspx?cmd=edit&projectid=" + id;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = "EditJobItems.aspx?cmd=edit&projectid=" + id;
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        gridview_BudgetItemList.Columns[gridview_BudgetItemList.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        gridview_BudgetItemList.Columns[gridview_BudgetItemList.Columns.Count - 2].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {
        SpecialProjectInfo project = specialProjectBll.GetSpecialProject(id);
        Session[sessionName] = project;
        Label_ProjectName.Text = project.ProjectName;
        Lable_Direct.Text = project.DirectBugdet.ToString("#,#.##");
        FillData();

        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "Apply.aspx?cmd=edit&projectid=" + id;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = "EditJobItems.aspx?cmd=edit&projectid=" + id;

        TextBox_AMultiple.Attributes.Add("onchange", "javascript:onMultipleChange()");
        TextBox_Amount.Attributes.Add("onchange", "javascript:onAmountChange()");
    }

    /// <summary>
    /// 数据填充
    /// </summary>
    private void FillData()
    {
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        gridview_BudgetItemList.DataSource = project.BudgetItems;
        gridview_BudgetItemList.DataBind();
    }

    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        long itemID = long.Parse(Hidden_EditItemID.Value);
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        if (itemID == 0)//添加
        {
            IList budgetList = project.BudgetItems;
            SpecialProjectBudgetItemInfo budget = new SpecialProjectBudgetItemInfo();
            budget.ItemID = 0;
            budget.IsRelated2Direct = CheckBox_Related.Checked;
            budget.DirectAmount = project.DirectBugdet;
            if (budget.IsRelated2Direct)
            {
                budget.Amultiple = decimal.Parse(TextBox_AMultiple.Text.Trim()) / 100;
                budget.Amount = project.DirectBugdet * budget.Amultiple;// decimal.Parse(TextBox_Amount.Text.Trim());
            }
            else
            {
                budget.Amount =  decimal.Parse(TextBox_Amount.Text.Trim());
                if (project.DirectBugdet != 0)
                    budget.Amultiple = budget.Amount / project.DirectBugdet;
                else
                    budget.Amultiple = 0;
            }        
           

            budget.BudgetItemName = TextBox_BudgetItemName.Text.Trim();
            
            budget.ProjectID = project.ProjectID;
            budget.Remark = TextBox_Remark.Text.Trim();
            //插入数据库
            budget.ItemID = specialProjectBll.SaveBudgetItem(budget);
            project.BudgetItems.Add(budget);
        }
        else//编辑
        {
            IList budgetList = project.BudgetItems;
            SpecialProjectBudgetItemInfo budget = null;
            foreach (SpecialProjectBudgetItemInfo item in budgetList)
            {
                if (item.ItemID == itemID)
                {
                    budget = item;
                    break;
                }
            }
            if (budget == null)
            {
                budget = new SpecialProjectBudgetItemInfo();
                budget.ItemID = 0;
                budget.ProjectID = project.ProjectID;
                project.BudgetItems.Add(budget);
            }
            budget.IsRelated2Direct = CheckBox_Related.Checked;
            budget.DirectAmount = project.DirectBugdet;
            if (budget.IsRelated2Direct)
            {
                budget.Amultiple = decimal.Parse(TextBox_AMultiple.Text.Trim()) / 100;
                budget.Amount = project.DirectBugdet * budget.Amultiple;// decimal.Parse(TextBox_Amount.Text.Trim());
            }
            else
            {
                budget.Amount = decimal.Parse(TextBox_Amount.Text.Trim());
                if (project.DirectBugdet != 0)
                    budget.Amultiple = budget.Amount / project.DirectBugdet;
                else
                    budget.Amultiple = 0;
            }        
            budget.BudgetItemName = TextBox_BudgetItemName.Text.Trim();
            
            
            budget.Remark = TextBox_Remark.Text.Trim();
            budget.ItemID = specialProjectBll.SaveBudgetItem(budget);
        }
        Session[sessionName] = project;
        FillData();
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmount = 0;//每次postback都会自动初始化
    private decimal totalMultiple = 0;//
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectBudgetItemInfo item = e.Row.DataItem as SpecialProjectBudgetItemInfo;
            totalAmount += item.TrueAmount;
            totalMultiple += item.TrueMultiple;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 3;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            for (int i = 1; i <= 2; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label LabelTotal = e.Row.FindControl("Label_TotalAmount") as Label;
            Label LabelTotalMultiple = e.Row.FindControl("Label_TotalMultiple") as Label;
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalMultiple != null)
            {
                LabelTotalMultiple.Text = totalMultiple.ToString("P");
            }


        }

    }

    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        long itemID = (long)gridview_BudgetItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        list = project.BudgetItems;

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "工程项:" + itemID + " 已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        specialProjectBll.DeleteBudgetItem(itemID);

        foreach (SpecialProjectBudgetItemInfo item in list)
        {
            if (item.ItemID == itemID)
            {
                list.Remove(item);
                break;
            }
        }
        Session[sessionName] = project;

        FillData();
    }
}
