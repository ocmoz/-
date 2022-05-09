<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="UserSet, App_Web_fublbnad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
        style="border-collapse: collapse;">
        <tr id="roleIDTr" runat="server">
            <td class="table_body table_body_NoWidth" style="height: 30px">
                用户名：
            </td>
            <td class="table_none table_none_NoWidth" style="height: 30px">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="height: 30px">
                原始密码：
            </td>
            <td class="table_none table_none_NoWidth" style="height: 30px">
                <asp:TextBox ID="TextBox1" runat="server" Columns="50" title="请输入原始密码~32:noChinese!"
                    TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="height: 30px">
                新密码：
            </td>
            <td class="table_none table_none_NoWidth" style="height: 30px">
                <asp:TextBox ID="TextBox2" runat="server" Columns="50" title="请输入新密码~32:noChinese!" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="height: 30px">
                重新输入新密码：
            </td>
            <td class="table_none table_none_NoWidth" style="height: 30px">
                <asp:TextBox ID="TextBox3" runat="server" Columns="50" title="请再一次输入新密码~32:noChinese!" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td align="left">
            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Label" 
                    ForeColor="#CC3300"></asp:Label></td>
            <td align="right">
                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />
                <input id="Button2" class="button_bak" type="button" value="关闭" onclick="javascript:window.top.hidePopWin();" />
            </td>
        </tr>
    </table>
</asp:Content>
