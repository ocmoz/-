<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SystemManager_UserManager_ViewUser, App_Web_1z8ztek-" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="用户管理"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：用户资料查看">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="set.gif" ButtonName="配置工作流角色" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    用户基本信息
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    用户名：
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
                    用户姓名：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbPersonName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    所属公司 ：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    所属部门：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbDepartment" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    职位：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbPosition" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    性别：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbSex" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    用户类型：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbUserType" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    用户状态：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Label ID="lbUserStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    用户角色
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="2">
                    <asp:Literal ID="ltRoles" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4" style="height: 30px">
                    附加信息
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    员工工号：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbStaffNO" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    身份证：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbIDCard" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    出生日期：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbBirthday" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    办公电话：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbOfficePhone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    手机：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbMobilePhone" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    家庭电话：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbHomePhone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    传真：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbFax" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    住址：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    Email：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbEmail" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    所属系统工程师：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="lbIM" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    主要职责：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                    <asp:Label ID="lbRes" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
