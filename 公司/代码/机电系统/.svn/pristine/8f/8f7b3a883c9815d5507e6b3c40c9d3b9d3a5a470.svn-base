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
using FM2E.BLL.Utils;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_SpecialProject_ProjectApply_Apply : System.Web.UI.Page
{
    SpecialProject specialProjectBll = new SpecialProject();

    /// <summary>
    /// 命令，包括cmd=new新建、cmd=edit编辑
    /// </summary>
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 0, 0, DataType.Str);
    /// <summary>
    /// 编辑的ID
    /// </summary>
    protected long id = (long)Common.sink("projectid", MethodType.Get, 0, 0, DataType.Long);

    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    private const string UPLOADFOLDER = "SpecialProject/";

    private string sessionName = "Module_FM2E_SpecialProject_ProjectApply_Apply";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
        if (cmd == "edit")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditJobItems.aspx?cmd=edit&projectid=" + id;

            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[1].ButtonUrl = "EditBudgetItems.aspx?cmd=edit&projectid=" + id;

        }
        else
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            Button_Submit.Visible = false;
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {
        if (cmd == "edit")
        {
            //获取
            Session[sessionName] = specialProjectBll.GetSpecialProject(id);

        }
        else
        {
            Session[sessionName] = null;

        }
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
        TextBox_Budget.Text = project.Budget.ToString("#.######");
        TextBox_BudgetName.Text = project.BudgetName;
        TextBox_CurrentStatus.Text = project.CurrentStatus;
        TextBox_Problem.Text = project.Problem;
        TextBox_ProjectName.Text = project.ProjectName;
        TextBox_Solution.Text = project.Solution;
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
    }

    /// <summary>
    /// 保存基本的可行性报告，并进入工程量清单编辑页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        SpecialProjectInfo project = GetObject();
        if (project != null)
        {

            string tip = "";
            try
            {
                project.Status = project.Status = project.NextStatus(SpecialProjectEvents.SAVE_DRAFT, out tip);
                id = specialProjectBll.SaveProjectBasicInfoDraft(project);
            }
            catch(Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "发生错误", "保存专项工程信息发生错误",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            //跳转到工程量编辑页面
            EventMessage.MessageBox(Msg_Type.Error, "保存成功", tip + "<br/>点击按钮进行工程量编辑", Icon_Type.OK, false, "~/Module/FM2E/SpecialProject/ProjectApply/EditJobItems.aspx?projectid=" + id, UrlType.Href, "");
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 从输入页面获取输入的对象
    /// </summary>
    /// <returns>如果输入有误，返回null</returns>
    private SpecialProjectInfo GetObject()
    {
        SpecialProjectInfo project = null;
        if (cmd == "edit")
        {
            project = Session[sessionName] as SpecialProjectInfo;
            if (project == null)
                project = specialProjectBll.GetSpecialProjectBasicInfo(id);
        }
        if(project==null)
            project = new SpecialProjectInfo();
        //公司ID
        string companyid = UserData.CurrentUserData.CompanyID;
        //提交人
        string submiter = UserData.CurrentUserData.UserName;
        DateTime updatetime = DateTime.Now;
        DateTime submittime = DateTime.Now;
        //专项工程名称
        string projectname = TextBox_ProjectName.Text.Trim();
        if (projectname.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入专项工程名称", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }
        //预算来源
        string budgetname = TextBox_BudgetName.Text.Trim();

        //估计预算
        decimal budget = 0;

        if (!decimal.TryParse(TextBox_Budget.Text.Trim(), out budget))
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "预算金额未输入或者输入格式错误", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }

        //现状描述
        string currentstatus = TextBox_CurrentStatus.Text.Trim();
        if (currentstatus.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入解决方案", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }


        string currentstatus_file = "";
        //现状描述附件处理
        FileUpLoadCommon fileUtility_CurrentStatus = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_CurrentStatusFile.HasFile)
        {
            if (fileUtility_CurrentStatus.SaveFile(FileUpload_CurrentStatusFile, false))
            {
                currentstatus_file = fileUtility_CurrentStatus.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败：" + fileUtility_CurrentStatus.ErrorMsg, new WebException(fileUtility_CurrentStatus.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return null;
            }
        }

        //存在问题描述
        string problem = TextBox_CurrentStatus.Text.Trim();
        if (problem.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入解决方案", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }


        string problem_file = "";
        //存在问题附件处理
        FileUpLoadCommon fileUtility_Problem = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_ProblemFile.HasFile)
        {
            if (fileUtility_Problem.SaveFile(FileUpload_ProblemFile, false))
            {
                problem_file =  fileUtility_Problem.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败：" + fileUtility_Problem.ErrorMsg, new WebException(fileUtility_Problem.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return null;
            }
        }

        //解决方案描述
        string solution = TextBox_Solution.Text.Trim();
        if (solution.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入解决方案", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }


        string solution_file = "";
        //解决方案附件处理
        FileUpLoadCommon fileUtility_Solution = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_SolutionFile.HasFile)
        {
            if (fileUtility_Solution.SaveFile(FileUpload_SolutionFile, false))
            {
                solution_file =  fileUtility_Solution.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败：" + fileUtility_Solution.ErrorMsg, new WebException(fileUtility_Solution.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return null;
            }
        }

        project.Submitter = submiter;
        project.SolutionFile = solution_file;
        project.Solution = solution;
        project.ProjectName = projectname;
        project.ProblemFile = problem_file;
        project.Problem = problem;
        project.CurrentStatusFile = currentstatus_file;
        project.CurrentStatus = currentstatus;
        project.CompanyID = companyid;
        project.BudgetName = budgetname;
        project.Budget = budget;
        project.SubmitTime = submittime;
        project.UpdateTime = updatetime;
        return project;
    }
    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        SpecialProjectInfo project = GetObject();
        if (project.JobItems.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "提交不成功", "提交专项工程信息不成功，未添加工作量清单", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }

        if (project.BudgetItems.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "提交不成功", "提交专项工程信息不成功，未添加预算清单", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }

        if (project != null)
        {
            string tip = "";
            try
            {
                try
                {
                    project.Status = project.NextStatus(SpecialProjectEvents.SUBMIT, out tip);
                }
                catch (Exception)
                {
                    //状态完成都还保证可以编辑
                }
                id = specialProjectBll.SaveProjectBasicInfoDraft(project);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "发生错误", "提交专项工程信息发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            //跳转到工程量编辑页面

            try
            {

                specialProjectBll.SendPendingOrder(project, Common.Get_UserName, UserData.CurrentUserData.PersonName, UserData.CurrentUserData.CompanyID);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, ex.Message);
            }

            EventMessage.MessageBox(Msg_Type.Error, "保存成功", tip + "<br/>点击按钮返回列表", Icon_Type.OK, false, "~/Module/FM2E/SpecialProject/ProjectApply/SpecialProjectList.aspx?projectid=" + id, UrlType.Href, "");
        }
        else
        {
            return;
        }
    }
}
