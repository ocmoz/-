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
using FM2E.BLL.BugReport;
using FM2E.Model.BugReportManager;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_BugReportManager_ViewBugreport_SendBugreport : System.Web.UI.Page
{
    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);
    protected void Page_Load(object sender, EventArgs e)
    {
        InitiaPage();
    }

    private void InitiaPage()
    {
        BugReport bll = new BugReport();
        BugReportInfo item = bll.GetBugReport(id);
        LB_Title.Text = item.Title;
        LB_Message.Text = item.Message;
        attachment.HRef = item.Attachment;
    }

    protected void btn_Send_Click(object sender, EventArgs e)
    {
        bool isuploadSuccess = false;
        bool isupdateSuccess = false;
        string attachmentstr = attachment.HRef;
        string errorMsg = "";
        if (FileUpload_Attachment.FileName != null && FileUpload_Attachment.FileName != string.Empty)
        {
            string fileUrl = UploadFile(ref isuploadSuccess, ref errorMsg);
            if (fileUrl != "")
            {
                if (attachment.HRef!=string.Empty)
                    FileUpLoadCommon.DeleteFile(string.Format("{0}", attachmentstr));
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
            isuploadSuccess = true;
        }
        if (!isuploadSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传附件失败", new WebException(errorMsg), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            return;
        }


        try
        {
            BugReport bll = new BugReport();
            BugReportInfo item = bll.GetBugReport(id);
     
            item.Attachment = attachmentstr;
           
            item.Status = 2;//状态为已完成修改状态
            item.Report = TB_Report.Text;
            item.ResponserID = Common.Get_UserName;
            item.ResponserName = UserData.CurrentUserData.PersonName;
            bll.UpdateBugReport(item);
            isupdateSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "发送意见失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        if (isupdateSuccess)
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
