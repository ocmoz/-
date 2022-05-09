<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SystemManager_UserManager_ViewUser, App_Web_1z8ztek-" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�û�����"
        HeadHelpTxt="����" HeadOPTxt="Ŀǰ�������ܣ��û����ϲ鿴">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="�༭" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="set.gif" ButtonName="���ù�������ɫ" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="ɾ��" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    �û�������Ϣ
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �û�����
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbUserName" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_none table_none_NoWidth" rowspan="9">
                    <div style="width: 100%; text-align: center; vertical-align: middle;">
                        <asp:Image ID="imPhoto" runat="server" Width="200px" AlternateText="No Picture" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �û�������
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbPersonName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    ������˾ ��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �������ţ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbDepartment" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    ְλ��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbPosition" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �Ա�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbSex" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �û����ͣ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbUserType" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �û�״̬��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbUserStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �û���ɫ
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Literal ID="ltRoles" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4" style="height: 30px">
                    ������Ϣ
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    Ա�����ţ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbStaffNO" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    ���֤��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbIDCard" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �������ڣ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbBirthday" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �칫�绰��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbOfficePhone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    �ֻ���
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbMobilePhone" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    ��ͥ�绰��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbHomePhone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    ���棺
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbFax" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    סַ��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    Email��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbEmail" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    ����ϵͳ����ʦ��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbIM" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    ��Ҫְ��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                    <asp:Label ID="lbRes" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
