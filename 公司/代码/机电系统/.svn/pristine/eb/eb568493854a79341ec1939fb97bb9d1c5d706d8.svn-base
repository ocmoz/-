using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebUtility;
using FM2E.Model.Exceptions;
using FM2E.Model.BugReportManager;
using WebUtility.Components;
using FM2E.BLL.BugReport;

public partial class Module_FM2E_BugReportManager_SendBugreport_SendBugreport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        template.HRef = "BugReportTemplate.doc";
    }

    protected void btn_Send_Click(object sender, EventArgs e)
    {
        long newid = 0;
        bool isSuccess = false;
        string attachmentstr = "";
        string errorMsg = "";
        if (FileUpload_Attachment.FileName != null && FileUpload_Attachment.FileName != string.Empty)
        {
            string fileUrl = UploadFile(ref isSuccess, ref errorMsg);
            if (fileUrl != "")
            {
                //if (cmd == "edit")
                //    FileUpLoadCommon.DeleteFile(string.Format("0", attachmentstr));
                attachmentstr = SystemConfig.Instance.UploadPath + "BugReportAttachement/" + fileUrl;

            }
            else
            {
                attachmentstr = fileUrl;
            }

        }
        else
        {
            attachmentstr = "";
            isSuccess = true;
        }
        if (!isSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传附件失败", new WebException(errorMsg), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        try
        {
            BugReportInfo item = new BugReportInfo();
            item.Title = TextBox_Title.Text;
            item.Message = tb_MessageContent.Text;
            item.Attachment = attachmentstr;
            item.SenderID = UserData.CurrentUserData.UserName;
            item.SenderName = UserData.CurrentUserData.PersonName;
            item.Status = 1;//状态为等待开发人员查阅状态
            item.Attachment2 = string.Empty;
            item.Report = string.Empty;
            item.ReportTime = DateTime.Now;

            BugReport bll = new BugReport();
            newid = bll.InsertBugReport(item);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "发送意见失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        if(newid != 0)
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "发送成功", Icon_Type.OK, true, Common.GetHomeBaseUrl("History.aspx"), UrlType.Href, "");

    }

    private string UploadFile(ref bool isSuccess, ref string errorMsg)
    {
        FileUpLoadCommon fc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + "BugReportAttachement/", false);
        isSuccess = fc.SaveFile(FileUpload_Attachment.PostedFile, false, false);
        if (!isSuccess)
            errorMsg = fc.ErrorMsg;
        return fc.NewFileName;
    }


}
