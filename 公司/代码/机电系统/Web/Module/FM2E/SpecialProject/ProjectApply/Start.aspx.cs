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

public partial class Module_FM2E_SpecialProject_ProjectApply_Start : System.Web.UI.Page
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

    private string sessionName = "Module_FM2E_SpecialProject_ProjectApply_Start";

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
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "Apply.aspx?cmd=edit&projectid=" + id;

        }
        else
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
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
        Label_ProjectName.Text = project.ProjectName;
        Label_TeamName.Text = project.Bid.BiddenCompany;
    }



    protected void Button_Start_Click(object sender, EventArgs e)
    {
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }

        if (project != null)
        {
            string tip = "";
            try
            {
                project.Status = project.NextStatus(SpecialProjectEvents.START, out tip);
                id = specialProjectBll.SaveProjectBasicInfoDraft(project);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "发生错误", "专项工程开工操作发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            //跳转到工程量编辑页面
            EventMessage.MessageBox(Msg_Type.Error, "开工成功", tip + "<br/>点击按钮返回", Icon_Type.OK, false, "~/Module/FM2E/SpecialProject/ProjectManagement/Working/ViewProject.aspx?cmd=edit&projectid=" + id, UrlType.Href, "");
        }
        else
        {
            return;
        }
    }
}
