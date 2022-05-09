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
public partial class Module_FM2E_SpecialProject_ProjectManagement_Working_EditPay : System.Web.UI.Page
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

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectManagement_EditPay";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "ViewProject.aspx?cmd=edit&projectid=" + id;
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {
        SpecialProjectInfo project = specialProjectBll.GetSpecialProject(id);
        Session[sessionName] = project;
        Label_ProjectName.Text = project.ProjectName;
        Label_ProjectNameContract.Text = project.ProjectName;
        FillData();
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        gridview_PrePayItemList.Columns[gridview_PrePayItemList.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        gridview_PrePayItemList.Columns[gridview_PrePayItemList.Columns.Count - 2].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        //********** Modification Finished 2011-09-09 **********************************************************************************************

        //工作项下拉
        foreach (SpecialProjectPlanInfo job in project.PlanItems)
        {
            DropDownList_JobItem.Items.Add(new ListItem(job.ItemName,job.ItemID.ToString()));
        }

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
        gridview_PrePayItemList.DataSource = project.PrePayList;
        gridview_PrePayItemList.DataBind();

        gridview_ContractPayItemList.DataSource = project.ContractPayList;
        gridview_ContractPayItemList.DataBind();
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
            IList prepayList = project.PrePayList;
            SpecialProjectPrePayInfo prepay = new SpecialProjectPrePayInfo();
            prepay.ItemID = 0;
            prepay.Amount = decimal.Parse(TextBox_Amount.Text.Trim());
            prepay.ItemName = TextBox_ItemName.Text.Trim();
            prepay.Method = TextBox_Method.Text.Trim();
            prepay.ProjectID = project.ProjectID;
            prepay.Remark = TextBox_Remark.Text.Trim();
            prepay.Time = DateTime.Parse(TextBox_Time.Text.Trim());

            
            //插入数据库
            prepay.ItemID = specialProjectBll.SavePrePayItem(prepay);
            project.PrePayList.Add(prepay);
        }
        else//编辑
        {
            IList prepayList = project.PrePayList;
            SpecialProjectPrePayInfo prepay = null;
            foreach (SpecialProjectPrePayInfo item in prepayList)
            {
                if (item.ItemID == itemID)
                {
                    prepay = item;
                    break;
                }
            }
            if (prepay == null)
            {
                prepay = new SpecialProjectPrePayInfo();
                prepay.ItemID = 0;
                prepay.ProjectID = project.ProjectID;
                project.PrePayList.Add(prepay);
            }
            prepay.Amount = decimal.Parse(TextBox_Amount.Text.Trim());
            prepay.ItemName = TextBox_ItemName.Text.Trim();
            prepay.Method = TextBox_Method.Text.Trim();
            prepay.ProjectID = project.ProjectID;
            prepay.Remark = TextBox_Remark.Text.Trim();
            prepay.Time = DateTime.Parse(TextBox_Time.Text.Trim());
            prepay.ItemID = specialProjectBll.SavePrePayItem(prepay);
        }
        Session[sessionName] = project;
        FillData();
        TabContainer1.ActiveTabIndex = 0;
    }


    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveContract_Click(object sender, EventArgs e)
    {
        long itemID = long.Parse(Hidden_ItemIDContract.Value);
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        if (itemID == 0)//添加
        {
            IList contractpayList = project.ContractPayList;
            SpecialProjectContractPayInfo contractpay = new SpecialProjectContractPayInfo();
            contractpay.ItemID = 0;
            contractpay.Amount = decimal.Parse(TextBox_AmountContract.Text.Trim());
            contractpay.ItemName = TextBox_ItemNameContract.Text.Trim();
            contractpay.Method = TextBox_MethodContract.Text.Trim();
            contractpay.ProjectID = project.ProjectID;
            contractpay.Remark = TextBox_RemarkContract.Text.Trim();
            contractpay.DaysAfter = int.Parse(TextBox_DaysAfter.Text.Trim());
            contractpay.PlanItemID = long.Parse(DropDownList_JobItem.SelectedValue);
            contractpay.PlanItemName = DropDownList_JobItem.SelectedItem.Text;


            //插入数据库
            contractpay.ItemID = specialProjectBll.SaveContractPayItem(contractpay);
            project.ContractPayList.Add(contractpay);
        }
        else//编辑
        {
            IList contractpayList = project.ContractPayList;
            SpecialProjectContractPayInfo contractpay = null;
            foreach (SpecialProjectContractPayInfo item in contractpayList)
            {
                if (item.ItemID == itemID)
                {
                    contractpay = item;
                    break;
                }
            }
            if (contractpay == null)
            {
                contractpay = new SpecialProjectContractPayInfo();
                contractpay.ItemID = 0;
                contractpay.ProjectID = project.ProjectID;
                project.ContractPayList.Add(contractpay);
            }
            contractpay.Amount = decimal.Parse(TextBox_AmountContract.Text.Trim());
            contractpay.ItemName = TextBox_ItemNameContract.Text.Trim();
            contractpay.Method = TextBox_MethodContract.Text.Trim();
            contractpay.ProjectID = project.ProjectID;
            contractpay.Remark = TextBox_RemarkContract.Text.Trim();
            contractpay.DaysAfter = int.Parse(TextBox_DaysAfter.Text.Trim());
            contractpay.PlanItemID = long.Parse(DropDownList_JobItem.SelectedValue);
            contractpay.PlanItemName = DropDownList_JobItem.SelectedItem.Text;

            
        }
        Session[sessionName] = project;
        FillData();
        TabContainer1.ActiveTabIndex = 1;
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmount = 0;//每次postback都会自动初始化
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectPrePayInfo item = e.Row.DataItem as SpecialProjectPrePayInfo;
            totalAmount += item.Amount;
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
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }

        }
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmountContract = 0;//每次postback都会自动初始化
    protected void gridview_ItemContractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectContractPayInfo item = e.Row.DataItem as SpecialProjectContractPayInfo;
            totalAmountContract += item.Amount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 4;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            for (int i = 1; i <= 3; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label LabelTotal = e.Row.FindControl("Label_TotalAmountContract") as Label;
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmountContract.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
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
        long itemID = (long)gridview_PrePayItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        list = project.PrePayList;

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "预支付项:" + itemID + " 已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        specialProjectBll.DeletePrePayItem(itemID);

        foreach (SpecialProjectPrePayInfo item in list)
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


    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemContractList_RowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        long itemID = (long)gridview_ContractPayItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        list = project.ContractPayList;

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "合同支付项:" + itemID + " 已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        specialProjectBll.DeleteContractPayItem(itemID);

        foreach (SpecialProjectContractPayInfo item in list)
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

