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
    
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="վ����Ϣ����"
        HeadOPTxt="Ŀǰ�������ܣ�������Ϣ">
        
      
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="������ʷ" ButtonUrlType="Href"
            ButtonUrl="History.aspx" />
            
          <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="������Ϣ" ButtonUrlType="Href"
            ButtonUrl="ViewMessage.aspx" />
    </cc1:HeadMenuWebControls>
    
    <asp:Panel ID="Panel_SelectUser" runat="server" Style="display: none; width: 95%; height: 380px;"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
        <tr>
            <td class="Table_searchtitle" style="text-align:left" >�û���</td><td style="text-align:left"  class="Table_searchtitle">Ⱥ�飺</td>
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
        <input id="Button_OK_User" runat="server" onclick="javascript:setValue();" class="button_bak" type="button" value="ȷ��" />
        <asp:Button ID="Button_Cancel_User" runat="server" class="button_bak" Text="�ر�" />
        </center>
    </asp:Panel>

    
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                ������Ϣ
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; width:10%; text-align: right;">
                ���ͣ�
            </td>
            <td class="table_body table_body_NoWidth"  style="width:40%">
                <asp:DropDownList ID="drplist_MessageType" runat="server">
                    <asp:ListItem Text="ϵͳ��Ϣ" Value="0" Selected="True" />
                    <asp:ListItem Text="Ԥ����Ϣ" Value="1" />
                </asp:DropDownList>
            </td>
            <td style="width:10%; text-align: right;">
                ���ͷ�ʽ��</td>
            <td class="table_body table_body_NoWidth" style="width:40%">
                <asp:RadioButtonList ID="radiolist_SendWay" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="վ����Ϣ" Value="0" Selected="True" />
                    <asp:ListItem Text="Email" Value="1" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; text-align: right;">
                �����ˣ�
            </td>
            <td class="table_body table_body_NoWidth" colspan="3" >
                <asp:TextBox ID="TextBox_Receivers" runat="server" Width="400px" title="������������~!"></asp:TextBox>
                <input type="button" ID="btn_User" runat="server" value="ѡ��" 
                    class="cbutton" tabindex="10" />
                <input type="button" ID="Button_All" onclick="javascript:selectAll();" value="ȫѡ" 
                    class="cbutton" tabindex="10" />
                ���Զ��Ż�ֺŷָ���Ⱥ����˫������ס����"group"��
                
               <cc2:ModalPopupExtender ID="ModalPopupExtender_SelectUser" runat="server" TargetControlID="btn_User"
                                    PopupControlID="Panel_SelectUser" BackgroundCssClass="modalBackground" DropShadow="True"
                                    OkControlID="Button_OK_User" OnOkScript="onOk_User()" CancelControlID="Button_Cancel_User" DynamicServicePath=""
                                    Enabled="true">
               </cc2:ModalPopupExtender>
                    
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; text-align: right;">
                ���⣺
            </td>
            <td class="table_body table_body_NoWidth" colspan="3">
                <asp:TextBox ID="TextBox_Title" title="���������~50:!" runat="server" Width="400px"></asp:TextBox>
                ��50�����ڣ�
            </td>
        </tr>
        <tr>
            <td  style="height: 30px;text-align: right;">
                ���ݣ�
            </td>
            <td class="table_body table_body_NoWidth" colspan="3">
                <asp:TextBox ID="tb_MessageContent" runat="server" Width="95%" Height="100px" TextMode="MultiLine"
                    title="��������Ϣ����~200:!" BorderStyle="Groove" MaxLength="200" />
                <br />
                ��200�����ڣ�
            </td>
        </tr>
        <tr>
            <td  style="height: 30px; text-align: right;">
                ������
            </td>
            <td class="table_body table_body_NoWidth" colspan="3">
                <asp:FileUpload ID="FileUpload_Attachment" runat="server" />��������ʧ�ܣ�������ѡ��
            </td>
        </tr>
        
         <tr>
            <td class="Table_searchtitle" colspan="4">
                 <asp:Button ID="btn_Send" runat="server" Text="����" CssClass="button_bak" OnClick="btn_Send_Click" />
                &nbsp;<input id="btn_Clear" type="button" value="���" class="button_bak" 
                     onclick="javascipt:return confirm('ȷ����գ�');" tabindex="2" />
                &nbsp;<asp:Button ID="btn_ConfirmEmail" runat="server" Style="display: none" OnClick="btn_ConfirmEmail_Click" />
                <asp:TextBox ID="tb_Hide" runat="server" Style="display: none" AutoPostBack="true" />

            </td>
        </tr>
    </table>
    
   
    <script language="javascript">
        //��ģʽ�Ի����ֵд��TEXTBOX����
        function onOk_User()
        {
            var users = document.getElementById("SelectedUserGroup").value;
            var target = document.all.<%=this.TextBox_Receivers.ClientID %>.value;
            var lastChar = ",";
            if(target.endsWith(",")||target.endsWith("��")||target.endsWith(";")||target.endsWith("��")||target.length==0)
            {
                lastChar = "";
            }
            if(users.length>0)
               document.all.<%=this.TextBox_Receivers.ClientID %>.value+=lastChar+users;     
            //ɾ��textbox�����ظ����ռ���
            target = document.all.<%=this.TextBox_Receivers.ClientID %>.value;
            var a = target.split(/,|;|��|��/);
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
        
        //ȫѡ�û�
        function selectAll()
        {
            document.all.<%=this.TextBox_Receivers.ClientID %>.value = "*";
            
        }
    </script>

</asp:Content>
