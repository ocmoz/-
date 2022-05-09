<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_BarCode_BarCodePrint, App_Web_pm8v3-0g" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td class="Table_searchtitle" colspan="4">
                        ��ӡ����
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ѡ���ӡ����
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:DropDownList ID="ddlPrinters" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPrinters_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlPort" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPort_SelectedIndexChanged">
                            <asp:ListItem Value="1">COM1</asp:ListItem>
                            <asp:ListItem Value="2">COM2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��ӡ������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:TextBox ID="tbCount" runat="server" MaxLength="2" Width="20px" Text="1" title="�������ӡ����~int!"
                            onchange="javascript:ChangePrintCount();"></asp:TextBox>��&nbsp;��&nbsp;<span id="barCodeCount"
                                runat="server"></span>&nbsp;=&nbsp;<span id="sumSpan" runat="server"></span>&nbsp;��������
                    </td>
                </tr>
                <tr>
                    <td class="Table_searchtitle" colspan="4">
                        ������ӡ����
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��ӡ��IP��ַ��
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <asp:Label ID="lbIPAddress" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��ǩ��ȣ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbLabelWidth" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��߾ࣺ
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbLeftMargin" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��ǩ��϶��
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbLabelGap" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        �������׼��
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbBarCodeType" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ����������ԭ�㣺
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbBarCodeOrg" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��������ԭ�㣺
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbTitleOrg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��������ԭ�㣺
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbRemarkOrg" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ������߶ȣ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbBarCodeHeight" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��խ�ߴ�С������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbBarCodeRatio" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        �������߷Ŵ��ʣ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbBarMag" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        �������壺
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbFont" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ���ִ�С��
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lbFontSize" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:Label ID="lbErrMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button ID="btPrint" runat="server" Text="��ӡ" CssClass="button_bak" OnClick="btPrint_Click" />&nbsp;&nbsp;
                        <input id="Button2" class="button_bak" type="button" value="�ر�" onclick="javascript:window.parent.hidePopWin();" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <object id="PrintActiveX" width="350" height="50" classid="CLSID:905418AC-23D7-4BB4-9E53-FFF52C734EDB">
    </object>

    <script language="javascript" type="text/javascript">
    function ChangePrintCount() {
        var obj=document.all.<%=tbCount.ClientID %>;
        if(obj=="undefined")
        {
           return;
        }
           
        var count=document.all.<%=barCodeCount.ClientID %>.innerText;
        var sum=obj.value*count;
        document.all.<%=sumSpan.ClientID %>.innerText=sum;
    }
    </script>
    <script for="PrintActiveX" event="RecvMsg(Message)">
<!-- 
  alert(Message);
-->
    </script>

</asp:Content>
