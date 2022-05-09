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
public partial class Module_FM2E_SpecialProject_ProjectManagement_Working_Pay : System.Web.UI.Page
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
        Label_ProjectNameMonthly.Text = project.ProjectName;

        for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 2; i++)
        {
            DropDownList_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        DropDownList_Year.SelectedValue = DateTime.Now.Year.ToString();

        for (int i = 1; i <= 12; i++)
        {
            DropDownList_Month.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        DropDownList_Month.SelectedValue = DateTime.Now.Month.ToString();

        FillData();

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

        gridview_MonthlyPayList.DataSource = project.MonthlyPayRecordList;
        gridview_MonthlyPayList.DataBind();

        foreach (SpecialProjectPayRecordInfo item in project.MonthlyPayRecordList)
        {
            Hidden_ExsitMonths.Value += (item.Year * 12 + item.Month).ToString() + ",";
        }

        Label_PlanPre.Text = project.TotalPlanPrePay.ToString("#,0.##");
        Label_PlanContract.Text = project.TotalPlanContractPay.ToString("#,0.##");
        Label_PlanMonthly.Text = project.TotalPlanMonthlyPay.ToString("#,0.##");
        Label_PlanTotal.Text = project.TotalPlanPay.ToString("#,0.##");

        Label_PaidPre.Text = project.TotalPaidPrePay.ToString("#,0.##");
        Label_PaidContract.Text = project.TotalPaidContractPay.ToString("#,0.##");
        Label_PaidMonthly.Text = project.TotalMonthlyPay.ToString("#,0.##");
        Label_PaidTotal.Text = project.TotalPaid.ToString("#,0.##");

        Label_DiffPre.Text = (project.TotalPlanPrePay - project.TotalPaidPrePay).ToString("#,0.##");
        Label_DiffContract.Text = (project.TotalPlanContractPay - project.TotalPaidContractPay).ToString("#,0.##");
        Label_DiffMonthly.Text = (project.TotalPlanMonthlyPay - project.TotalMonthlyPay).ToString("#,0.##");
        Label_DiffTotal.Text = (project.TotalPlanPay - project.TotalPaid).ToString("#,0.##");

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
            return;
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
                return;
            }
            prepay.Paid = decimal.Parse(TextBox_Paid.Text.Trim());
            prepay.Payee = UserData.CurrentUserData.UserName;
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
            return;
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
                return;
            }
            contractpay.Payee = UserData.CurrentUserData.UserName;
            contractpay.Paid = decimal.Parse(TextBox_PaidContract.Text.Trim());
            specialProjectBll.SaveContractPayItem(contractpay);
        
        }
        Session[sessionName] = project;
        FillData();
        TabContainer1.ActiveTabIndex = 1;
    }

    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveMonthly_Click(object sender, EventArgs e)
    {
        long itemID = long.Parse(Hidden_EditItemIDMonthly.Value);
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        if (itemID == 0)//添加
        {
            IList monthlypayList = project.MonthlyPayRecordList;
            SpecialProjectPayRecordInfo record = new SpecialProjectPayRecordInfo();
            record.Amount = decimal.Parse(TextBox_AmountMonthly.Text.Trim());
            record.Method = TextBox_MethodMonthly.Text.Trim();
            record.Month = int.Parse(DropDownList_Month.SelectedValue);
            record.Paid = decimal.Parse(TextBox_PaidMonthly.Text.Trim());
            record.Payee = UserData.CurrentUserData.UserName;
            record.PayTime = DateTime.Parse(TextBox_PayTimeMonthly.Text.Trim());
            record.Progress = decimal.Parse(TextBox_ProgressMonthly.Text.Trim()) / 100;
            record.ProjectID = project.ProjectID;
            record.Remark = TextBox_RemarkMonthly.Text.Trim();
            record.Year = int.Parse(DropDownList_Year.SelectedValue);


            //插入数据库
            specialProjectBll.SaveMonthlyPayRecord(record);
            project.MonthlyPayRecordList.Add(record);
        }
        else//编辑
        {
            IList monthlypayList = project.MonthlyPayRecordList;
            SpecialProjectPayRecordInfo record = null;
            foreach (SpecialProjectPayRecordInfo item in monthlypayList)
            {
                if (item.Year == int.Parse(DropDownList_Year.SelectedValue) && item.Month==int.Parse(DropDownList_Month.SelectedValue))
                {
                    record = item;
                    break;
                }
            }
            if (record == null)
            {
                record = new SpecialProjectPayRecordInfo();
                record.Year = int.Parse(DropDownList_Year.SelectedValue);
                record.Month = int.Parse(DropDownList_Month.SelectedValue);
                record.ProjectID = project.ProjectID;
                project.MonthlyPayRecordList.Add(record);
            }
            record.Amount = decimal.Parse(TextBox_AmountMonthly.Text.Trim());
            record.Method = TextBox_MethodMonthly.Text.Trim();
            
            record.Paid = decimal.Parse(TextBox_Paid.Text.Trim());
            record.Payee = UserData.CurrentUserData.UserName;
            record.PayTime = DateTime.Parse(TextBox_PayTimeMonthly.Text.Trim());
            record.Progress = decimal.Parse(TextBox_ProgressMonthly.Text.Trim()) / 100;
            record.Remark = TextBox_RemarkMonthly.Text.Trim();

            specialProjectBll.SaveMonthlyPayRecord(record);
        }
        Session[sessionName] = project;
        FillData();
        TabContainer1.ActiveTabIndex = 2;
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmount = 0;//每次postback都会自动初始化
    private decimal totalPaid = 0;
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectPrePayInfo item = e.Row.DataItem as SpecialProjectPrePayInfo;
            totalAmount += item.Amount;
            totalPaid += item.Paid;
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
            Label LabelTotalPaid = e.Row.FindControl("Label_TotalPaid") as Label;
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalPaid != null)
                LabelTotalPaid.Text = totalPaid.ToString("#,0.##");

        }
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmountContract = 0;//每次postback都会自动初始化
    private decimal totalPaidContract = 0;
    protected void gridview_ItemContractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectContractPayInfo item = e.Row.DataItem as SpecialProjectContractPayInfo;
            totalAmountContract += item.Amount;
            totalPaidContract += item.Paid;
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
            Label LabelTotalPaid = e.Row.FindControl("Label_TotalPaidContract") as Label;
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmountContract.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalPaid != null)
            {
                LabelTotalPaid.Text = totalPaidContract.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }

        }
    }

    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmountMonthly = 0;//每次postback都会自动初始化
    private decimal totalPaidMonthly = 0;
    protected void gridview_ItemMonthlyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectPayRecordInfo item = e.Row.DataItem as SpecialProjectPayRecordInfo;
            totalAmountMonthly += item.Amount;
            totalPaidMonthly += item.Paid;
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
            Label LabelTotal = e.Row.FindControl("Label_TotalAmountMonthly") as Label;
            Label LabelTotalPaid = e.Row.FindControl("Label_TotalPaidMonthly") as Label;
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmountMonthly.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalPaid != null)
            {
                LabelTotalPaid.Text = totalPaidMonthly.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }

        }
    }


}