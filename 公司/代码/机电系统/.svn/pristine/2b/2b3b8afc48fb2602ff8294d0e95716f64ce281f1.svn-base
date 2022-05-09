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

public partial class Module_FM2E_SpecialProject_ProjectFinish_CheckAccept : System.Web.UI.Page
{
    SpecialProject specialProjectBll = new SpecialProject();
    /// <summary>
    /// 命令，包括cmd=view查看、cmd=edit编辑
    /// </summary>
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 0, 0, DataType.Str);
    /// <summary>
    /// 查看 的ID
    /// </summary>
    protected long id = (long)Common.sink("projectid", MethodType.Get, 0, 0, DataType.Long);

    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    protected const string UPLOADFOLDER = "SpecialProject/";

    private string sessionName = "Module_FM2E_SpecialProject_ViewProject";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }

    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {

        Session[sessionName] = specialProjectBll.GetSpecialProject(id);
        FillData();
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        bool bEdit = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        Button_SaveComplete.Visible = bEdit;
        Button_SaveFinish.Visible = bEdit;
        Button_SavePass.Visible = bEdit;
        //********** Modification Finished 2011-09-09 **********************************************************************************************
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

        //项目基本信息
        Label_Budget.Text = project.Budget.ToString("#,#.######");
        Label_BudgetName.Text = project.BudgetName;
        Label_CurrentStatus.Text = project.CurrentStatus;
        Label_Problem.Text = project.Problem;
        Label_ProjectName.Text = project.ProjectName;
        Label_Solution.Text = project.Solution;
        Label_Status.Text = project.StatusString;
        Label_Submitter.Text = project.Submitter;
        Label_SubmitTime.Text = project.SubmitTime.ToString("yyyy-MM-dd HH:mm");
        Label_UpdateTime.Text = project.UpdateTime.ToString("yyyy-MM-dd HH:mm");
        if (project.SolutionFile.Length > 0)
        {
            HyperLink_SolutionFile.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + project.SolutionFile;
            HyperLink_SolutionFile.Text = project.SolutionFile;
            HyperLink_SolutionFile.Visible = true;
        }
        if (project.ProblemFile.Length > 0)
        {
            HyperLink_ProblemFile.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + project.ProblemFile;
            HyperLink_ProblemFile.Text = project.ProblemFile;
            HyperLink_ProblemFile.Visible = true;
        }
        if (project.CurrentStatusFile.Length > 0)
        {
            HyperLink_CurrentStatusFile.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + project.CurrentStatusFile;
            HyperLink_CurrentStatusFile.Text = project.CurrentStatusFile;
            HyperLink_CurrentStatusFile.Visible = true;
        }

        //有变更生效
        if (project.ModifyOKCount > 0)
        {
            tr_aftermodifydetail.Visible = true;
            tr_aftermodifyheader.Visible = true;
            gridview_JobItemList_AfterModify.DataSource = project.JobItemListAfterModify;
            gridview_JobItemList_AfterModify.DataBind();
        }
        else
        {
            tr_aftermodifydetail.Visible = false;
            tr_aftermodifyheader.Visible = false;
            gridview_JobItemList_AfterModify.Visible = false;
        }


        //原计划工程量
        gridview_JobItemList.DataSource = project.JobItems;
        gridview_JobItemList.DataBind();

        //预算项
        gridview_BudgetItemList.DataSource = project.BudgetItems;
        gridview_BudgetItemList.DataBind();

        //设计
        Label_DesignName.Text = project.Design.DesignName;
        Label_DesignCost.Text = project.Design.DesignCost.ToString("#,#.##");
        Label_ProjectCost.Text = project.Design.ProjectCost.ToString("#,#.##");
        Label_Designer.Text = project.Design.Designer;
        Label_DesignerInfo.Text = project.Design.DesignerInfo;
        Label_Design.Text = project.Design.Design;
        if (project.Design.Attechment.Length > 0)
        {
            HyperLink_Design.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + project.Design.Attechment;
            HyperLink_Design.Text = project.Design.Attechment;
            HyperLink_Design.Visible = true;
        }

        //招标信息
        Label_BiddenCompany.Text = project.Bid.BiddenCompany;
        Label_BiddenCompanyInfo.Text = project.Bid.BiddenCompanyInfo;
        if (project.Bid.Attechment.Length > 0)
        {
            HyperLink_File.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + project.Bid.Attechment;
            HyperLink_File.Text = project.Bid.Attechment;
            HyperLink_File.Visible = true;
        }

        //施工计划以及进度情况
        Repeater_PlanItemList.DataSource = project.PlanItems;
        Repeater_PlanItemList.DataBind();

        //横道图
        div_gant.InnerHtml = specialProjectBll.MakeGant(project.PlanItems);

        //进场设备
        Repeater_DeviceItemList.DataSource = project.DeviceList;
        Repeater_DeviceItemList.DataBind();

        //预支付项
        gridview_PrePayItemList.DataSource = project.PrePayList;
        gridview_PrePayItemList.DataBind();

        //合同支付项
        gridview_ContractPayItemList.DataSource = project.ContractPayList;
        gridview_ContractPayItemList.DataBind();

        //月进度支付
        gridview_MonthlyPayList.DataSource = project.MonthlyPayRecordList;
        gridview_MonthlyPayList.DataBind();

        //支付情况
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

        //变更申请单
        Repeater_ModifyList.DataSource = project.ModifyList;
        Repeater_ModifyList.DataBind();

        //工程验收
        if (project.CanCheckAccept)//已经进入验收流程
        {

            Label_Complete.Text = project.CheckAcceptInfo.Complete ? "已完工" : "未完工";
            Label_CompleteApprovaler.Text = project.CheckAcceptInfo.Complete ? project.CheckAcceptInfo.CompleteApprovaler:""  ;
            Label_CompleteDate.Text = project.CheckAcceptInfo.Complete ? project.CheckAcceptInfo.CompleteDate.ToString("yyyy-MM-dd"):"";
            Label_CompleteRemark.Text = project.CheckAcceptInfo.Complete ?   project.CheckAcceptInfo.CompleteRemark:"";

            Label_Pass.Text = project.CheckAcceptInfo.Pass ? "已交工" : "未交工";
            Label_PassApprovaler.Text = project.CheckAcceptInfo.Pass ?  project.CheckAcceptInfo.PassApprovaler: "";
            Label_PassDate.Text = project.CheckAcceptInfo.Pass ?  project.CheckAcceptInfo.PassDate.ToString("yyyy-MM-dd"):"" ;
            Label_PassRemark.Text = project.CheckAcceptInfo.Pass ?   project.CheckAcceptInfo.PassRemark:"";

            Label_Finish.Text = project.CheckAcceptInfo.Finish ? "已竣工" : "未竣工";
            Label_FinishApprovaler.Text = project.CheckAcceptInfo.Finish ? project.CheckAcceptInfo.CompleteApprovaler : "";
            Label_FinishDate.Text = project.CheckAcceptInfo.Finish ? project.CheckAcceptInfo.CompleteDate.ToString("yyyy-MM-dd") : "";
            Label_FinishRemark.Text = project.CheckAcceptInfo.Finish ? project.CheckAcceptInfo.CompleteRemark : "";

            if (project.CheckAcceptInfo.Complete)
            {
                div_completeinfo.Visible = true;
                div_complete.Visible = false;
                if (project.CheckAcceptInfo.Pass)
                {
                    div_pass.Visible = false;
                    div_passinfo.Visible = true;
                    if (project.CheckAcceptInfo.Finish)
                    {
                        div_finishinfo.Visible = true;
                        div_finish.Visible = false;
                    }
                    else
                    {
                        div_finishinfo.Visible = false;
                        div_finish.Visible = true;
                 
                    }
                }
                else
                {
                    div_pass.Visible = true;
                    div_passinfo.Visible = false;
       
                } 
            }
            else
            {
                div_completeinfo.Visible = false;
                div_complete.Visible = true;
                div_finish.Visible = div_pass.Visible = false;
                div_finishinfo.Visible = div_passinfo.Visible = true;

            }

        }
        else//未进入验收流程
        {
            Label_FinishTip.Text = "工作项尚未全部完成，未能进行验收工作";

            div_complete.Visible = false;
            div_finish.Visible = false;
            div_pass.Visible = false;
            div_completeinfo.Visible = true;
            div_finishinfo.Visible = true;
            div_passinfo.Visible = true;
        }
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalJobAmount = 0;//每次postback都会自动初始化
    protected void gridview_JobItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectJobItemInfo item = e.Row.DataItem as SpecialProjectJobItemInfo;
            totalJobAmount += item.Amount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 6;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            for (int i = 1; i <= 5; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label LabelTotal = e.Row.FindControl("Label_TotalAmount") as Label;

            if (LabelTotal != null)
            {
                LabelTotal.Text += totalJobAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }


        }

    }

    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalJobAfterModifyAmount = 0;//每次postback都会自动初始化
    protected void gridview_JobItemList_AfterModify_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectJobItemInfo item = e.Row.DataItem as SpecialProjectJobItemInfo;
            totalJobAfterModifyAmount += item.Amount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 6;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            for (int i = 1; i <= 5; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label LabelTotal = e.Row.FindControl("Label_TotalAmount") as Label;

            if (LabelTotal != null)
            {
                LabelTotal.Text += totalJobAfterModifyAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }


        }

    }



    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalBudgetAmount = 0;//每次postback都会自动初始化
    private decimal totalMultiple = 0;//
    protected void gridview_BudgetItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectBudgetItemInfo item = e.Row.DataItem as SpecialProjectBudgetItemInfo;
            totalBudgetAmount += item.TrueAmount;
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
                LabelTotal.Text = totalBudgetAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelTotalMultiple != null)
            {
                LabelTotalMultiple.Text = totalMultiple.ToString("P");
            }


        }

    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalPrePayAmount = 0;//每次postback都会自动初始化
    protected void gridview_PrePayItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectPrePayInfo item = e.Row.DataItem as SpecialProjectPrePayInfo;
            totalPrePayAmount += item.Amount;
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
                LabelTotal.Text = totalPrePayAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }

        }
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmountContract = 0;//每次postback都会自动初始化
    protected void gridview_ContractPayItemList_RowDataBound(object sender, GridViewRowEventArgs e)
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

    /// <summary>
    /// 保存完工验收情况
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveComplete_Click(object sender, EventArgs e)
    {
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }

        string name = TextBox_CompleteApprovaler.Text.Trim();
        if (name.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "完工确认失败", "完工确认失败，未输入负责人签名", Icon_Type.Error, false, "javascript:history.go(-1);", UrlType.JavaScript, "");
            return;
        }
        string remark = TextBox_CompleteRemark.Text.Trim();

        project.CheckAcceptInfo.Complete = true;
        project.CheckAcceptInfo.CompleteApprovaler = name;
        project.CheckAcceptInfo.CompleteDate = DateTime.Now;
        project.CheckAcceptInfo.CompleteRemark = remark;
        string tip = "";
        try
        {
            project.CheckAcceptInfo.ProjectID = id;

            specialProjectBll.SaveCheckAccept(project.CheckAcceptInfo);

            project.Status = project.NextStatus(SpecialProjectEvents.NETX, out tip);

            id = specialProjectBll.SaveProjectBasicInfoDraft(project);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "发生错误", "专项工程完工操作发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        string url = Common.GetHomeBaseUrl("SpecialProjectList.aspx");
        
        EventMessage.MessageBox(Msg_Type.Info, "完工确认成功", "完工确认成功，点击转到工程查看", Icon_Type.OK, false, url, UrlType.Href, "");

    }
    protected void Button_SavePass_Click(object sender, EventArgs e)
    {
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }

        string name = TextBox_PassApprovaler.Text.Trim();
        if (name.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "交工确认失败", "交工确认失败，未输入负责人签名", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "");
            return;
        }
        string remark = TextBox_PassRemark.Text.Trim();

        project.CheckAcceptInfo.Pass = true;
        project.CheckAcceptInfo.PassApprovaler = name;
        project.CheckAcceptInfo.PassDate = DateTime.Now;
        project.CheckAcceptInfo.PassRemark = remark;
        string tip = "";
        try
        {
            project.CheckAcceptInfo.ProjectID = id;
            specialProjectBll.SaveCheckAccept(project.CheckAcceptInfo);

            project.Status = project.NextStatus(SpecialProjectEvents.NETX, out tip);

            id = specialProjectBll.SaveProjectBasicInfoDraft(project);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "发生错误", "专项工程交工操作发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        string url = Common.GetHomeBaseUrl("SpecialProjectList.aspx");
        EventMessage.MessageBox(Msg_Type.Info, "交工确认成功", "交工确认成功，点击转到工程查看", Icon_Type.OK, false, url, UrlType.Href, "");
    }
    protected void Button_SaveFinish_Click(object sender, EventArgs e)
    {
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }

        string name = TextBox_FinishApprovaler.Text.Trim();
        if (name.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "竣工确认失败", "竣工确认失败，未输入负责人签名", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "");
            return;
        }
        string remark = TextBox_FinishRemark.Text.Trim();

        project.CheckAcceptInfo.Finish = true;
        project.CheckAcceptInfo.FinishApprovaler = name;
        project.CheckAcceptInfo.FinishDate = DateTime.Now;
        project.CheckAcceptInfo.FinishRemark = remark;
        string tip = "";
        try
        {
            project.CheckAcceptInfo.ProjectID = id;
            specialProjectBll.SaveCheckAccept(project.CheckAcceptInfo);

            project.Status = project.NextStatus(SpecialProjectEvents.NETX, out tip);

            id = specialProjectBll.SaveProjectBasicInfoDraft(project);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "发生错误", "专项工程竣工操作发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        string url = Common.GetHomeBaseUrl("SpecialProjectList.aspx");
        EventMessage.MessageBox(Msg_Type.Info, "竣工确认成功", "竣工确认成功，点击转到工程查看", Icon_Type.OK, false, url, UrlType.Href, "");
    }
}
