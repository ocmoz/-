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
public partial class Module_FM2E_SpecialProject_ProjectManagement_Design_EditDesign : System.Web.UI.Page
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

    private string sessionName = "Module_FM2E_SpecialProject_ProjectManagement_EditDesign";

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
        TextBox_DesignName.Text = project.Design.DesignName;
        TextBox_DesignCost.Text = project.Design.DesignCost.ToString("#.##");
        TextBox_ProjectCost.Text = project.Design.ProjectCost.ToString("#.##");
        TextBox_Designer.Text = project.Design.Designer;
        TextBox_DesignerInfo.Text = project.Design.DesignerInfo;
        TextBox_Design.Text = project.Design.Design;
        
        if (project.Design.Attechment.Length > 0)
        {
            HyperLink_Design.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + project.Design.Attechment;
            HyperLink_Design.Text = project.Design.Attechment;
            HyperLink_Design.Visible = true;
        }
    }

    /// <summary>
    /// 保存基本的可行性报告，并进入工程量清单编辑页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        SpecialProjectDesignInfo design = GetObject();
        if (design != null)
        {
            try
            {
                specialProjectBll.SaveDesign(design);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "发生错误", "保存专项工程设计信息发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            //跳转到查看页面
            EventMessage.MessageBox(Msg_Type.Error, "保存成功", "点击返回查看工程信息", Icon_Type.OK, false, Common.GetHomeBaseUrl("ViewProject.aspx")+"?cmd=view&projectid=" + id, UrlType.Href, "");
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
    private SpecialProjectDesignInfo GetObject()
    {
        SpecialProjectDesignInfo design = null;
        if (cmd == "edit")
        {
            SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
            if (project == null)
                project = specialProjectBll.GetSpecialProject(id);
            design = project.Design;
            design.ProjectID = id;
        }
        if (design == null)
        {
            design = new SpecialProjectDesignInfo();
            design.ProjectID = id;
        }
        //设计方案名称
        string designName = TextBox_DesignName.Text.Trim();
        if (designName.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入设计方案名称", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }
      
        //设计费用
        decimal designCost = 0;

        if (!decimal.TryParse(TextBox_DesignCost.Text.Trim(), out designCost))
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "设计费用未输入或者输入格式错误", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }

        //工程费用
        decimal projectCost = 0;

        if (!decimal.TryParse(TextBox_ProjectCost.Text.Trim(), out projectCost))
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "工程费用未输入或者输入格式错误", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }

        //设计单位
        string designer = TextBox_Designer.Text.Trim();
        if (designer.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入设计单位", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }


        string designerInfo = TextBox_DesignerInfo.Text.Trim();

        //设计方案描述
        string designdes = TextBox_Design.Text.Trim();
        if (designdes.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入设计方案", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }


       


        string design_file = "";
        //设计方案附件处理
        FileUpLoadCommon fileUtility_Design = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_Design.HasFile)
        {
            if (fileUtility_Design.SaveFile(FileUpload_Design, false))
            {
                design_file = fileUtility_Design.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败", new WebException(fileUtility_Design.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return null;
            }
        }
        design.ProjectID = id;
        design.Attechment = design_file;
        design.Design = designdes;
        design.DesignCost = designCost;
        design.Designer = designer;
        design.DesignerInfo = designerInfo;
        design.DesignName = designName;
        design.ProjectCost = projectCost;

        return design;
    }
    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        SpecialProjectDesignInfo design = GetObject();
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        string tip="";
        if (design != null)
        {
            try
            {
                project.Status = project.NextStatus(SpecialProjectEvents.SUBMIT, out tip);
                
                specialProjectBll.SaveDesign(design);

                specialProjectBll.SaveProjectBasicInfoDraft(project);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "发生错误", "提交专项工程设计信息发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            //跳转到工程量编辑页面
            EventMessage.MessageBox(Msg_Type.Error, "提交成功", tip + "<br/>点击返回查看工程信息", Icon_Type.OK, false, "~/Module/FM2E/SpecialProject/ProjectApply/SpecialProjectList.aspx?cmd=view&projectid=" + id, UrlType.Href, "");
        }
        else
        {
            return;
        }
    }
}
