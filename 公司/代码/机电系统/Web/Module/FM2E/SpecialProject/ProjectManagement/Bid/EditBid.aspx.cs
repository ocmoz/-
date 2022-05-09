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
public partial class Module_FM2E_SpecialProject_ProjectManagement_Bid_EditBid : System.Web.UI.Page
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

    private string sessionName = "Module_FM2E_SpecialProject_ProjectManagement_EditBid";

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
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "../ViewProject.aspx?cmd=view&projectid=" + id;
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
        TextBox_BiddenCompany.Text = project.Bid.BiddenCompany;
        TextBox_BiddenCompanyInfo.Text = project.Bid.BiddenCompanyInfo;

        
        if (project.Bid.Attechment.Length > 0)
        {
            HyperLink_File.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + project.Bid.Attechment;
            HyperLink_File.Text = project.Bid.Attechment;
            HyperLink_File.Visible = true;
        }
    }

    /// <summary>
    /// 保存招标信息，并进入工程查看页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        SpecialProjectBidInfo big = GetObject();
        if (big != null)
        {
            try
            {
                specialProjectBll.SaveBidding(big);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "发生错误", "保存专项工程招标信息发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            //跳转到工程量编辑页面
            EventMessage.MessageBox(Msg_Type.Error, "保存成功", "点击返回", Icon_Type.OK, false, Common.GetHomeBaseUrl("ViewProject.aspx")+"?cmd=view&projectid=" + id, UrlType.Href, "");
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
    private SpecialProjectBidInfo GetObject()
    {
        SpecialProjectBidInfo bid = null;
        if (cmd == "edit")
        {
            SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
            if (project == null)
                project = specialProjectBll.GetSpecialProject(id);
            bid = project.Bid;
            bid.ProjectID = id;
        }
        if (bid == null)
        {
            bid = new SpecialProjectBidInfo();
            bid.ProjectID = id;
        }
        //中标公司名称
        string companyname = TextBox_BiddenCompany.Text.Trim();
        if (companyname.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入中标公司名称", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }
      
        //中标公司简介
        //中标公司名称
        string companyinfo = TextBox_BiddenCompanyInfo.Text.Trim();
        if (companyinfo.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "信息未输入完整", "未输入中标公司简介", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return null;
        }

        

        string file = "";
        //设计方案附件处理
        FileUpLoadCommon fileUtility = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_File.HasFile)
        {
            if (fileUtility.SaveFile(FileUpload_File, false))
            {
                file = fileUtility.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败", new WebException(fileUtility.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return null;
            }
        }
        bid.ProjectID = id;
        bid.Attechment = file;
        bid.BiddenCompany = companyname;
        bid.BiddenCompanyInfo = companyinfo;


        return bid;
    }
    
}
