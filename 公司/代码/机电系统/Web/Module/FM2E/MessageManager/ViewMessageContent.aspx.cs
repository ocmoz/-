using System;
using System. Collections. Generic;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;
using System. Configuration;

using WebUtility;
using WebUtility. WebControls;
using WebUtility. Components;
using FM2E. BLL.Message;
using FM2E.Model.Message;

public partial class Module_FM2E_MessageManager_ViewMessageContent : System. Web. UI. Page
{
    /// <summary>
    /// 消息发送处理业务逻辑处理类
    /// </summary>
    Message messageBll = new Message();

    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);

    string cmd = (string)Common.sink("cmd", MethodType.Get, 0, 0, DataType.Str);
    protected void Page_Load( object sender , EventArgs e )
    {
        if (!IsPostBack)
        {
            MessageInfo msg = null;

            if (cmd == "view")
               msg = messageBll.GetMessageMarkRead(id, Common.Get_UserName);
            else
                msg = messageBll.GetMessage(id);
            lb_Title.Text =  msg.Title;
            lb_Type.Text = msg.TypeString;
            lb_SendWay.Text = msg.SendWayString;
            lb_SendFrom.Text = msg.SenderPersonName + "(" + msg.SendFrom + ")";
            lb_MessageTime.Text = msg.MessageTime.ToString();
            tb_MessageContent.Text = msg.Message;
            Label_Receivers.Text = msg.ReceiverAddress;
            if (msg.Attachment == string.Empty || msg.Attachment == null)
            {
                hl_Download.Visible = false;
            }
            else
            {
                hl_Download.NavigateUrl = SystemConfig.Instance.UploadPath + msg.Attachment;
            }
        }
    }

}
