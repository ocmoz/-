<%@ page language="C#" autoeventwireup="true" masterpagefile="~/MasterPage/MasterPage.master" inherits="Module_FM2E_MessageManager_ViewMessageContent, App_Web_qefdawc4" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" >
                <asp:Label ID="lb_Title" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="table_none table_none_NoWidth" style="text-align: center">
                �����ˣ�
                <asp:Label ID="lb_SendFrom" runat="server" Text="" />
                &nbsp;&nbsp;����ʱ�䣺
                <asp:Label ID="lb_MessageTime" runat="server" Text="" />
            &nbsp;&nbsp; ��Ϣ���ͣ�<asp:Label ID="lb_Type" runat="server" Text="" />
            &nbsp; ���ͷ�ʽ��<asp:Label ID="lb_SendWay" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="table_none table_none_NoWidth" style="text-align :center ">
                <asp:TextBox ID="tb_MessageContent" runat="server" BorderStyle="None"
                    BackColor="Transparent" Width ="100%" Height="250px" ReadOnly="true" ForeColor ="Black" TextMode ="MultiLine"  style="overflow-y:auto"/>
               
              
            </td>
        </tr>
       <tr>
             <td style="text-align:left" class="table_none table_none_NoWidth"> 
                �����ˣ� <asp:Label ID="Label_Receivers" runat="server" Text="" />
             </td>

        </tr>
        <tr>
             <td style="text-align:right;" class="table_none table_none_NoWidth"> 
                <asp:HyperLink ID="hl_Download" runat="server" ForeColor="Red" NavigateUrl="">������ظ���</asp:HyperLink>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             </td>

        </tr>
        <tr>
            <td class="table_none table_none_NoWidth" style="text-align: center" >
                <input type="button" id="btn_Close" value="����" class="button_bak" onclick="javascript:history.go(-1);"/>
            </td>
            
        </tr>
    </table>

    <script language="javascript">
    </script>

</asp:Content>
