<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="SendMessage.aspx.cs" Inherits="Module_FM2E_MessageManager_SendMessage" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="站内信息管理"
        HeadOPTxt="目前操作功能：发送信息">
        
      
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="发送历史" ButtonUrlType="Href"
            ButtonUrl="History.aspx" />
            
          <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="接收消息" ButtonUrlType="Href"
            ButtonUrl="ViewMessage.aspx" />
    </cc1:HeadMenuWebControls>
    
    <asp:Panel ID="Panel_SelectUser" runat="server" Style="display: none; width: 95%; height: 380px;"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
        <tr>
            <td class="Table_searchtitle" style="text-align:left" >用户：</td><td style="text-align:left"  class="Table_searchtitle">群组：</td>
        </tr>
        <tr><td style="text-align:center">
        <cc1:MultiListBox ID="MultiListBox_User" runat="server" DataTextField="UserName" DataValueField="UserName"
                                            SelectionMode="Multiple" Heigth="280px" Rows="10">
                                            <FirstListBox Style="">
                                                <StyleSheet Width="140px" Height="280px" />
                                            </FirstListBox>
                                            <SecondListBox Style="">
                                                <StyleSheet Width="140px" Height="280px" />
                                            </SecondListBox>
       </cc1:MultiListBox>
       
       </td>
       
       <td style="text-align:center">
           <cc1:MultiListBox ID="MultiListBox_Group" runat="server" DataTextField="RoleName" DataValueField="RoleName"
                                            SelectionMode="Multiple" Heigth="280px" Rows="10">
                                            <FirstListBox Style="">
                                                <StyleSheet Width="140px" Height="280px" />
                                            </FirstListBox>
                                            <SecondListBox Style="">
                                                <StyleSheet Width="140px" Height="280px" />
                                            </SecondListBox>
       </cc1:MultiListBox>
       </td>
       </tr>
       
       </table>
        <center>
        <input id="SelectedUserGroup" type="hidden"  />
        <input id="Button_OK_User" runat="server" onclick="javascript:setValue();" class="button_bak" type="button" value="确定" />
        <asp:Button ID="Button_Cancel_User" runat="server" class="button_bak" Text="关闭" />
        </center>
    </asp:Panel>

    
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                发送信息
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; width:10%; text-align: right;">
                类型：
            </td>
            <td class="table_body table_body_NoWidth"  style="width:40%">
                <asp:DropDownList ID="drplist_MessageType" runat="server">
                    <asp:ListItem Text="系统消息" Value="0" Selected="True" />
                    <asp:ListItem Text="预警消息" Value="1" />
                </asp:DropDownList>
            </td>
            <td style="width:10%; text-align: right;">
                发送方式：</td>
            <td class="table_body table_body_NoWidth" style="width:40%">
                <asp:RadioButtonList ID="radiolist_SendWay" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="站内信息" Value="0" Selected="True" />
                    <asp:ListItem Text="Email" Value="1" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; text-align: right;">
                收信人：
            </td>
            <td class="table_body table_body_NoWidth" colspan="3" >
                <asp:TextBox ID="TextBox_Receivers" runat="server" Width="400px" title="请输入收信人~!"></asp:TextBox>
                <input type="button" ID="btn_User" runat="server" value="选择" 
                    class="cbutton" tabindex="10" />
                <input type="button" ID="Button_All" onclick="javascript:selectAll();" value="全选" 
                    class="cbutton" tabindex="10" />
                （以逗号或分号分隔，群组用双引号括住例如"group"）
                
               <cc2:ModalPopupExtender ID="ModalPopupExtender_SelectUser" runat="server" TargetControlID="btn_User"
                                    PopupControlID="Panel_SelectUser" BackgroundCssClass="modalBackground" DropShadow="True"
                                    OkControlID="Button_OK_User" OnOkScript="onOk_User()" CancelControlID="Button_Cancel_User" DynamicServicePath=""
                                    Enabled="true">
               </cc2:ModalPopupExtender>
                    
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; text-align: right;">
                标题：
            </td>
            <td class="table_body table_body_NoWidth" colspan="3">
                <asp:TextBox ID="TextBox_Title" title="请输入标题~50:!" runat="server" Width="400px"></asp:TextBox>
                （50字以内）
            </td>
        </tr>
        <tr>
            <td  style="height: 30px;text-align: right;">
                内容：
            </td>
            <td class="table_body table_body_NoWidth" colspan="3">
                <asp:TextBox ID="tb_MessageContent" runat="server" Width="95%" Height="100px" TextMode="MultiLine"
                    title="请输入消息内容~200:!" BorderStyle="Groove" MaxLength="200" />
                <br />
                （200字以内）
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; text-align: right;">
                附件：
            </td>
            <td class="table_body table_body_NoWidth" colspan="3">
                <asp:FileUpload ID="FileUpload_Attachment" runat="server" />（若发送失败，请重新选择）
            </td>
        </tr>
        
         <tr>
            <td class="Table_searchtitle" colspan="4">
                 <asp:Button ID="btn_Send" runat="server" Text="发送" CssClass="button_bak" OnClick="btn_Send_Click" />
                &nbsp;<input id="btn_Clear" type="button" value="清空" class="button_bak" 
                     onclick="javascipt:return confirm('确认清空？');" tabindex="2" />
                &nbsp;<asp:Button ID="btn_ConfirmEmail" runat="server" Style="display: none" OnClick="btn_ConfirmEmail_Click" />
                <asp:TextBox ID="tb_Hide" runat="server" Style="display: none" AutoPostBack="true" />

            </td>
        </tr>
    </table>
    
   
    <script language="javascript">
        //把模式对话框的值写到TEXTBOX上面
        function onOk_User()
        {
            var users = document.getElementById("SelectedUserGroup").value;
            var target = document.all.<%=this.TextBox_Receivers.ClientID %>.value;
            var lastChar = ",";
            if(target.endsWith(",")||target.endsWith("，")||target.endsWith(";")||target.endsWith("；")||target.length==0)
            {
                lastChar = "";
            }
            if(users.length>0)
               document.all.<%=this.TextBox_Receivers.ClientID %>.value+=lastChar+users;     
            //删除textbox上面重复的收件人
            target = document.all.<%=this.TextBox_Receivers.ClientID %>.value;
            var a = target.split(/,|;|，|；/);
            var b = new Array();
            for(var i=0;i<a.length;i++)
            {
                var n = a[i];
                if(!isExist(b,n))
                {
                    b.push(n);
                }
            }
            target = "";
            for(var i=0;i<b.length;i++)
            {
                target+=b[i]+",";
            }
            target = target.substring(0,target.length-1);
            document.all.<%=this.TextBox_Receivers.ClientID %>.value = target;
        }
        
        function isExist(b,n)
        {
            for(var i=0;i<b.length;i++)
            {
                if(n==b[i])
                    return true;
            }
            return false;
        }
        
        function setValue()
        {
            var selectedUserBox = document.getElementById("<%=this.MultiListBox_User.ClientID %>"+"_secondListBox");
            var usr = "";
            var selectedGroupBox = document.getElementById("<%=this.MultiListBox_Group.ClientID %>"+"_secondListBox");
            for(var i=0;i<selectedUserBox.options.length;i++)
            {
                usr += selectedUserBox.options[i].value+",";
            }

            var group = "";
            for(var i=0;i<selectedGroupBox.options.length;i++)
            {
                group += "\""+selectedGroupBox.options[i].value+"\""+",";
            }
            
            var  v = usr + group;
            if(v.endsWith(","))
                v = v.substr(0,v.length-1);
            document.getElementById("SelectedUserGroup").value = v;
        
        }
        
        //全选用户
        function selectAll()
        {
            document.all.<%=this.TextBox_Receivers.ClientID %>.value = "*";
            
        }
    </script>

</asp:Content>
