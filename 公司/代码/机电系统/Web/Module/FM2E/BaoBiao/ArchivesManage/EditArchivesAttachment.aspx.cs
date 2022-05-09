using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Archives;
using FM2E.BLL.Archives;

public partial class Module_FM2E_ArchivesManager_ArchivesManage_EditArchivesAttachment : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly ArchivesAttachment bll = new ArchivesAttachment();

    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    private const string UPLOADFOLDER = "ArchivesAttachmentFile/";

    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
    }
    // 页面初始化
    private void InitialPage()
    {
        try
        {
            if (cmd == "add")
            {
                HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息添加";
                tbArchivesID.Text = "系统自动生成";
                tbArchivesID.ReadOnly = true;
            }
            if (cmd == "edit")
            {
                HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息修改";
                tbArchivesID.Text = id.ToString();
                tbArchivesID.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //填充数据
    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                ArchivesAttachmentInfo item = bll.GetArchivesAttachment(id);
                //填充页面
                tbArchivesID.Text = (item.ArchivesID).ToString();
                tbDescription.Text = item.Description;
                if (item.SavePath.Length > 0)
                {
                    HyperLink_ArchivesAttachmentFile.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + item.SavePath;
                    HyperLink_ArchivesAttachmentFile.Text = item.SavePath;
                    HyperLink_ArchivesAttachmentFile.Visible = true;
                }
                tbRemark.Text = item.Remark;

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    //绑定按钮
    private void ButtonBind()
    {
        
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OnClick_btUpload(object sender, EventArgs e)
    {
        string archivesattachment_file = "";
        FileUpLoadCommon fileUtility_ArchivesAttachment = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_ArchivesAttachmentFile.HasFile)
        {
            if (fileUtility_ArchivesAttachment.SaveFile(FileUpload_ArchivesAttachmentFile, false))
            {
                archivesattachment_file = fileUtility_ArchivesAttachment.NewFileName;
                lbArchivesAttachmentName.Text = FileUpload_ArchivesAttachmentFile.FileName;
                lbSavePath.Text = fileUtility_ArchivesAttachment.Path + archivesattachment_file;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败：" + fileUtility_ArchivesAttachment.ErrorMsg, new WebException(fileUtility_ArchivesAttachment.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        
    }

    //确定按钮事件
    protected void btSave_Click(object sender, EventArgs e)
    {
        ArchivesAttachmentInfo item = new ArchivesAttachmentInfo();
        //获取填入数据
        item.ArchivesAttachmentID = id;
        item.ArchivesID = Convert.ToInt64(tbArchivesID.Text.Trim());
        item.Description = tbDescription.Text.Trim();

        string archivesattachment_file = "";
        FileUpLoadCommon fileUtility_ArchivesAttachment = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_ArchivesAttachmentFile.HasFile)
        {
            if (fileUtility_ArchivesAttachment.SaveFile(FileUpload_ArchivesAttachmentFile, false))
            {
                archivesattachment_file = fileUtility_ArchivesAttachment.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败：" + fileUtility_ArchivesAttachment.ErrorMsg, new WebException(fileUtility_ArchivesAttachment.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        item.SavePath = archivesattachment_file;
        item.Remark = tbRemark.Text.Trim();
        //增加
        if (cmd == "add")
        {
            bool bSuccess = false;
            try
            {
                bll.InsertArchivesAttachment(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesAttachment.aspx"), UrlType.Href, "");
            }
        }
        //修改
        else if (cmd == "edit")
        {
            bool bSuccess = false;
            try
            {
                bll.UpdateArchivesAttachment(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesAttachment.aspx"), UrlType.Href, "");
            }
        }
    }
}
