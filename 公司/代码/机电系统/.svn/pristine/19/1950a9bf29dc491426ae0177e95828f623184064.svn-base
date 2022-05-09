using System;
using System. Text;
using System. Data;
using System. Configuration;
using System. Collections;
using System. Web;
using System. Web. Security;
using System. Web. UI;
using System. Web. UI. WebControls;
using System. Web. UI. WebControls. WebParts;
using System. Web. UI. HtmlControls;
using System. Collections. Generic;
using System. Net. Mail;
using System. Net. Mime;

using WebUtility;
using WebUtility. WebControls;
using WebUtility. Components;
using FM2E. BLL.Message;
using FM2E. Model.Message;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.BLL.System;
using FM2E.Model.Exceptions;
using System.Text.RegularExpressions;
using System.IO;

public partial class Module_FM2E_MessageManager_SendMessage : System.Web.UI.Page
{
    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    private const string UPLOADFOLDER = "MessageAttachment/";

    /// <summary>
    /// 消息发送处理业务逻辑处理类
    /// </summary>
    Message messageBll = new Message();
    //用户角色员工业务逻辑处理类
    FM2E.BLL.System.User userBll = new User();
    Role roleBll = new Role();

    /// <summary>
    /// 加载页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load( object sender , EventArgs e )
    {

        if (!IsPostBack)
        {
            InitialPage();
        }

    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        //TextBox_Receivers.Attributes.Add("readonly", "readonly");
        FillData();
    }

    /// <summary>
    /// 往模式对话框填入数据
    /// </summary>
    private void FillData()
    {
        //获取所有用户
        IList userList = null;

        if (UserData.CurrentUserData.CompanyID == null ||
            UserData.CurrentUserData.CompanyID == "")
            userList = userBll.GetAllUser();
        else
        {
            userList = userBll.GetUsersByCompanyID(UserData.CurrentUserData.CompanyID);
        }
        
        foreach(UserInfo u in userList)
        {
            MultiListBox_User.FirstListBox.Items.Add(new ListItem(u.PersonName + "(" + u.UserName + ")", u.UserName));
        }
        
        //获取所有角色
        IList roleList = roleBll.GetAllRole();
        foreach (RoleInfo r in roleList)
        {
            MultiListBox_Group.FirstListBox.Items.Add(
                new ListItem(r.RoleName + "[" + r.Description + "]", r.RoleName));
        }
       

    }

    /// <summary>
    /// 发送按钮的Click处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Send_Click( object sender , EventArgs e )
    {
        //消息主体
        MessageInfo msg = new MessageInfo();
        //消息类型
        msg.Type = short.Parse(drplist_MessageType.SelectedValue);
        //发送方式
        msg.SendWay = short.Parse(radiolist_SendWay.SelectedValue);
        //如果发送方式为EMAIL，则默认已读
        bool hasRead = false;
        if (msg.SendWay == 1)
            hasRead = true;
        //标题
        if (msg.SendWay == 1)
            msg.Title = "FM2E邮件，主题：" + TextBox_Title.Text.Trim() + "   发送自用户：" + Common.Get_UserName;
        else
            msg.Title = TextBox_Title.Text.Trim();
        //消息内容
        msg.Message = tb_MessageContent.Text.Trim();
        //发送时间
        msg.MessageTime = DateTime.Now;
        //发送人
        msg.SendFrom = Common.Get_UserName;

        //附件处理
        FileUpLoadCommon fuc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        string attachement = "";
        if (FileUpload_Attachment.HasFile)
        {
            if (fuc.SaveFile(FileUpload_Attachment, false))
            {
                attachement = UPLOADFOLDER + "/" + fuc.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败", new WebException(fuc.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
        }
        msg.Attachment = attachement;

        //用户hashtable
        Hashtable ht_Users = new Hashtable();
        IList usersList =  userBll.GetAllUser();
        foreach (UserInfo u in usersList)
        {
            ht_Users.Add(u.UserName.ToLower(), u);
        }

        //群组hashtable
        Hashtable ht_Groups = new Hashtable();
        IList groupList = roleBll.GetAllRole();
        foreach (RoleInfo r in groupList)
        {
            ht_Groups.Add(r.RoleName.ToLower(), r);
        }

        //收信人 
        List<MessageReceiverInfo> receivers = new List<MessageReceiverInfo>();
        //收信EMAIL
        List<string> emailList = new List<string>();
        string invalidEmail = "";//不是合法EMAIL
        string noEmailUser = "";//没有EMAIL的用户
        string[] receiverIDs = TextBox_Receivers.Text.Trim().Split(new string[] { ",", ";","，","；" }, StringSplitOptions.RemoveEmptyEntries);//以逗号和分号分割
        Hashtable ht_Receiver = new Hashtable();
        string notReceiverIDs = "";
        string availableReceiverIDs = "";
        Regex regex = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");//EMAIL校验

        for (int i = 0; i < receiverIDs.Length; i++)
        {
           
            //判断群组
            string rid = receiverIDs[i].Trim();

            //判断是否全选
            if (rid == "*")
            {
                foreach (UserInfo u in usersList)
                {
                    if (!ht_Receiver.Contains(u.UserName))
                    //在当前接收者列表中还没有添加（不重复发送）
                    {
                        MessageReceiverInfo mri = new MessageReceiverInfo();
                        mri.UserName = u.UserName;
                        mri.HasRead = hasRead;
                        receivers.Add(mri);
                        bool emailValid = false ;
                        
                                string email = u.Email;
                                if (email.Trim().Length > 0)
                                {
                                    if (regex.IsMatch(email))
                                    {
                                        emailList.Add(u.Email);
                                        emailValid = true;
                                    }
                                    else
                                    {
                                        invalidEmail += u.UserName + " " + email + ",";
                                    }
                                }
                                else
                                    noEmailUser += u.UserName + ",";
                            
                        
                        if (msg.SendWay == 0 || msg.SendWay == 1 && emailValid)
                            ht_Receiver.Add(u.UserName, u.UserName);
                    }
                }
                availableReceiverIDs = "所有系统用户";
                break;
            }

            if (rid.StartsWith("\"") && rid.EndsWith("\""))//群组
            {
                string group = rid.Substring(1, rid.Length - 2);
                if (ht_Groups.Contains(group.ToLower()))//存在该群组
                {
                    //获取群组用户
                    RoleInfo ri = (RoleInfo)ht_Groups[group.ToLower()];
                    IList groupUser = userBll.GetUsers(ri.RoleID);
                    foreach (UserRoleInfo u in groupUser)
                    {
                        if (!ht_Receiver.Contains(u.UserName))
                        //在当前接收者列表中还没有添加（不重复发送）
                        {
                            MessageReceiverInfo mri = new MessageReceiverInfo();
                            mri.UserName = u.UserName;
                            mri.HasRead = hasRead;
                            receivers.Add(mri);
                            UserInfo user = userBll.GetUser(u.UserName);
                            bool emailValid = false;
                            if (user != null)
                            {
                              
                                        string email = user.Email;
                                        if (email.Trim().Length > 0)
                                        {
                                            if (regex.IsMatch(email))
                                            {
                                                emailList.Add(user.Email);
                                                emailValid = true;
                                            }
                                            else
                                            {
                                                invalidEmail += u.UserName + " " + email + ",";
                                            }
                                        }
                                        else
                                        {
                                            noEmailUser += u.UserName + ",";
                                        }
                                    
                                
                            }
                            if (msg.SendWay == 0 || msg.SendWay == 1 && emailValid)
                                ht_Receiver.Add(u.UserName, u.UserName);
                        }
                    }
                    availableReceiverIDs += "\"" + ri.RoleName + "\"" + ",";
                }
                else
                {
                    notReceiverIDs += rid + ",";
                }
            }
            else
            {
                if (ht_Users.Contains(rid.ToLower()))//存在该用户
                {
                   
                    UserInfo u = (UserInfo)ht_Users[rid.ToLower()];
                    if (!ht_Receiver.Contains(u.UserName))
                    {
                        //在当前接收者列表中还没有添加（不重复发送）
                        MessageReceiverInfo mri = new MessageReceiverInfo();
                        mri.UserName = u.UserName;
                        mri.HasRead = hasRead;
                        receivers.Add(mri);
                        bool emailValid = false;
                       
                                string email = u.Email;

                                if (!string.IsNullOrEmpty(email))
                                {
                                    if (regex.IsMatch(email))
                                    {
                                        emailList.Add(u.Email);
                                        emailValid = true;
                                    }
                                    else
                                    {
                                        invalidEmail += u.UserName + " " + email + ",";
                                    }
                                }
                                else
                                {
                                    noEmailUser += u.UserName + ",";
                          
                        }
                        if (msg.SendWay == 0 || msg.SendWay == 1 && emailValid)
                        {
                            ht_Receiver.Add(u.UserName, u.UserName);
                            availableReceiverIDs += u.UserName + ",";
                        }
                    }
                }
                else
                {
                    notReceiverIDs += rid + ",";
                }
            }
        }
        //添加收信人
        msg.Receivers = receivers;
        if (availableReceiverIDs.EndsWith(","))
            availableReceiverIDs = availableReceiverIDs.Substring(0, availableReceiverIDs.Length - 1);//去掉最后的字符
        if (notReceiverIDs.EndsWith(","))
            notReceiverIDs = notReceiverIDs.Substring(0, notReceiverIDs.Length - 1);//去掉最后的字符
        if (invalidEmail.EndsWith(","))
            invalidEmail = invalidEmail.Substring(0, invalidEmail.Length - 1);//去掉最后的字符
        if (noEmailUser.EndsWith(","))
            noEmailUser = noEmailUser.Substring(0, noEmailUser.Length - 1);//去掉最后的字符
        msg.ReceiverAddress = availableReceiverIDs;

        //没有有效收信人
        if (receivers.Count <= 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "消息发送失败", "原因：用户列表" + TextBox_Receivers.Text.Trim()+"中不包含有效用户", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        //没有有效的EMAIL
        if (msg.SendWay == 1)
        {
            if (emailList.Count <= 0)
            {
                EventMessage.MessageBox(Msg_Type.Error, "消息发送失败", "原因：用户列表" + TextBox_Receivers.Text.Trim() + "中不包含有效的EMAIL", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }
        }

        bool success = false;

        try
        {
           
            if (msg.SendWay == 1)//发送EMAIL
            {
                foreach (string email in emailList)
                {
                    if (FileUpload_Attachment.HasFile)
                    {
                        Common.SendEmail(email, msg.Title, msg.Message, Server.MapPath(SystemConfig.Instance.UploadPath + attachement), FileUpload_Attachment.PostedFile.ContentType);
                    }
                    else
                    {
                        Common.SendEmail(email, msg.Title, msg.Message, null, null);
                    }
                }
            }
            //记录发送历史，如果不是发送EMAIL则是发送站内消息
            messageBll.SendMessage(msg);
            success = true;
        }
        catch(Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "消息发送失败", "消息发送失败，请重试",ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

        if (success)
        {
            if(msg.SendWay==0)//发送站内消息
                EventMessage.MessageBox(Msg_Type.Info, "消息发送成功", "消息已经成功发送至：" + availableReceiverIDs + (notReceiverIDs.Length > 0 ? "<br/><font color='red'>以下无效用户未发送：" + notReceiverIDs + "</font>" : ""), Icon_Type.OK, "~/Module/FM2E/MessageManager/ViewMessage.aspx", UrlType.Href);
            else//发送EMAIL
                EventMessage.MessageBox(Msg_Type.Info, "消息发送成功", "成功发送EMAIL至：" + availableReceiverIDs + (noEmailUser.Length > 0 ? "<br/><font color='red'>但以下用户信息不包含EMAIL，未发送：" + noEmailUser + "</font>" : "")
                    + (invalidEmail.Length > 0 ? "<br/><font color='red'>但以下用户信息EMAIL格式非法，未发送：" + invalidEmail + "</font>" : ""), Icon_Type.OK, "~/Module/FM2E/MessageManager/ViewMessage.aspx", UrlType.Href);
        }

        //MessageInfo messageItem = new MessageInfo( );
        //List<MessageReceiverInfo> receiverItems = new List<MessageReceiverInfo>();
        //String attachmentURL = null;
        //if ( FileUpload1. HasFile == true )
        //{
        //    bool isSuccess = false;
        //    String errorMsg = String. Empty;
        //    messageItem. Attachment = UploadFile( ref isSuccess , ref errorMsg );
        //    attachmentURL = System. Web. HttpContext. Current. Request. PhysicalApplicationPath + ConfigurationManager. AppSettings[ "AttachmentDirectory" ] + messageItem. Attachment;
        //    if ( !isSuccess )
        //        EventMessage. MessageBox( 1 , "上传文件失败" , errorMsg , Icon_Type. Error , false , String. Format( "history.go(-{0})" , ( int )ViewState[ "PostNum" ] ) , UrlType. JavaScript , "" );
        //}
        //else
        //    messageItem. Attachment = null;

        //messageItem. ID = Guid. NewGuid( ). ToString( );
        //messageItem. SendFrom = "admin"; //Session[""];
        //messageItem. Type = drplist_MessageType. SelectedValue.Trim();
        //messageItem. Message = tb_MessageContent. Text. Trim( );
        //messageItem. MessageTime = DateTime. Now;
        //messageItem. Delivery = radiolist_SendWay. SelectedValue. Trim( ); ;

        //ListItemCollection items = listbox_ReceiverList. Items;
        //switch ( ViewState[ "Receiver" ] as String )
        //{
        //    case "User":
        //        for ( int i = 0 ; i < items. Count ; ++i )
        //            if ( items[ i ]. Selected == true )
        //                receiverItems. Add( new ReceiverInfo( messageItem. ID , items[ i ]. Value , null ) );
        //        break;
        //    case "Role":
        //        List<String> idList = Session[ "AllRoleID" ] as List<String>;
        //        for ( int i = 0 ; i < items. Count ; ++i )
        //            if ( items[ i ]. Selected == true )
        //                receiverItems. Add( new ReceiverInfo( messageItem. ID , null , idList[ i ] ) );
        //        break;
        //}

        //bool bSuccess = false;
        //try
        //{
        //    new Message( ). InsertMessage( messageItem );
        //    new Receiver( ). InsertReceiver( receiverItems );
        //    if ( messageItem. Delivery == "email" )
        //    {
        //        IList<String> emailList = null;
        //        List<String> requiredList = new List<string>( );
        //        List<String> noEmailList = new List<string>( );
        //        if ( ViewState[ "Receiver" ] as String == "Role" )
        //        {
        //            foreach ( ReceiverInfo item in receiverItems )
        //            {
        //                requiredList. Add( item. RoleID );
        //            }
        //            emailList = new User( ). GetUserEmail( null , requiredList , noEmailList );
        //        }
        //        else
        //        {
        //            foreach ( ReceiverInfo item in receiverItems )
        //            {
        //                requiredList. Add( item. UserName );
        //            }
        //            emailList = new User( ). GetUserEmail( requiredList , null , noEmailList );
        //        }
        //        //如果选中的收信人中存在缺少Email信息的情况，则先不发送，等页面再次回传时弹出确认框；否则直接发送
        //        if ( noEmailList. Count > 0 )
        //        {
        //            StringBuilder confirmString = new StringBuilder( 100 );
        //            if ( ViewState[ "Receiver" ] as String == "User" )
        //            {
        //                if ( emailList. Count > 0 )
        //                {
        //                    foreach ( String item in noEmailList )
        //                    {
        //                        confirmString. Append( item + "," );
        //                    }
        //                    confirmString. Remove( confirmString. Length - 1 , 1 );
        //                    confirmString. Append( "等收信人没有Email信息，仍然确认发送Email？" );
        //                }
        //                else
        //                    confirmString. Append( "所选中收信人均无Email信息，仍然确认发送Email？" );
        //            }
        //            else
        //                confirmString. Append( "所选角色中存在收信人无Email信息，仍然确认发送Email？" );
        //            Session[ "ConfirmNoEmail" ] = new EmailSendingInfo( emailList , confirmString. ToString( ) , messageItem , attachmentURL );
        //            return;
        //        }
        //        else
        //            SendEmails( emailList , messageItem , attachmentURL );
        //    }
        //    bSuccess = true;
        //}
        //catch
        //{
        //    Session[ "OldInput" ] = RestorePage( );
        //    EventMessage. MessageBox( 1 , "操作失败" , "信息发送失败，请重新发送！" , Icon_Type. Error , false , String. Format( "history.go(-{0});" , ( int )ViewState[ "PostNum" ] ) , UrlType. JavaScript , "" );
        //}
        //if ( bSuccess )
        //{
        //    EventMessage. MessageBox( 1 , "操作成功" , String. Concat( radiolist_SendWay. SelectedItem. Text , " 发送成功！" ) , Icon_Type. OK , false , String. Format( "history.go(-{0})" , ( int )ViewState[ "PostNum" ] ) , UrlType. JavaScript , "" );
        //    Session[ "OldInput" ] = null;
        //}
    }










    /// <summary>
    /// 隐藏按钮方法，用于JS代码调用（在确认发送Email后）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_ConfirmEmail_Click( object sender , EventArgs e )
    {
        //bool bSuccess = false;
        //try
        //{
        //    EmailSendingInfo tmpInfo = ( EmailSendingInfo )Session[ "EmailSendingInfo" ];
        //    if ( tmpInfo. emailList. Count > 0 )
        //    {
        //        SendEmails( tmpInfo. emailList , tmpInfo. message , tmpInfo. attachmentURL );
        //        bSuccess = true;
        //    }
        //}
        //catch
        //{
        //    Session[ "OldInput" ] = RestorePage( );
        //    EventMessage. MessageBox( 1 , "操作失败" , "信息发送失败，请重新发送！" , Icon_Type. Error , false , String. Format( "history.go(-{0});" , ( int )ViewState[ "PostNum" ] ) , UrlType. JavaScript , "" );
        //}
        //if ( bSuccess )
        //    EventMessage. MessageBox( 1 , "操作成功" , String. Concat( radiolist_SendWay. SelectedItem. Text , " 发送成功！" ) , Icon_Type. OK , false , String. Format( "history.go(-{0})" , ( int )ViewState[ "PostNum" ] ) , UrlType. JavaScript , "" );
        //else
        //    EventMessage. MessageBox( 1 , "操作完成" , "没有Email地址需要发送" , Icon_Type. OK , false , String. Format( "history.go(-{0})" , ( int )ViewState[ "PostNum" ] ) , UrlType. JavaScript , "" );
    }

    /// <summary>
    /// 执行Email发送
    /// </summary>
    /// <param name="emailList"></param>
    /// <param name="messageItem"></param>
    /// <param name="attachmentURL"></param>
    protected void SendEmails( IList<String> emailList , MessageInfo messageItem , String attachmentURL )
    {
        foreach ( String email in emailList )
        {
            Common. SendEmail(
                email ,
                drplist_MessageType. SelectedItem. Text ,
                messageItem. Message ,
                attachmentURL ,
                FileUpload_Attachment.PostedFile.ContentType);
        }
    }


}
