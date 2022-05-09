<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="UserList.aspx.cs" Inherits="Module_FM2E_SystemManager_UserManager_UserList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="用户管理"
        HeadHelpTxt="点击用户进入用户管理" HeadOPTxt="目前操作功能：用户列表">
        <cc1:HeadMenuButtonItem ButtonName="添加用户" ButtonIcon="new.gif" ButtonPopedom="New"
            ButtonUrl="EditUser.aspx?cmd=add" ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="用户列表" ID="TabPanel1">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            没有用户信息
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="UserName" HeaderText="用户名" />
                                            <asp:BoundField DataField="PersonName" HeaderText="用户姓名" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="所属公司" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="所属部门" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    用户类型</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#EnumHelper.GetDescription((Enum)Eval("UserType")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    用户状态</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#EnumHelper.GetDescription((Enum)Eval("Status"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                                CommandName="view">
                                                <HeaderStyle Width="60px" />
                                            </asp:ButtonField>
                                            <asp:TemplateField>
                                                <ItemStyle Width="60px" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                        CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此模块信息吗？')"
                                                        CausesValidation="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:GridView>
                                    <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                        AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                        InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                        PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                    </cc1:AspNetPager>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="查询">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="4">
                                        组合查询（支持模糊查询）
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        用户名：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        用户姓名：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="tbPersonName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        用户类型：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="ddlUserType" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        用户状态：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="ddlUserStatus" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        所属公司：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="ddlCompany" runat="server" >
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        所属部门：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="ddlDepartment" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                                runat="server">
                                <tr>
                                    <td align="right" style="height: 38px">
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                        <input id="Reset1" class="button_bak" type="reset" value="重填" language="javascript"
                                            onclick="return Reset1_onclick()" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
