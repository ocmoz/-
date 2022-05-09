<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditUserWorkflowRole.aspx.cs" Inherits="Module_FM2E_SystemManager_UserManager_EditUserWorkflowRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="用户管理"
        HeadHelpTxt="帮助" HeadOPTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="用户列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="UserList.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="编辑用户工作流角色" ID="TabPanel1">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                        style="border-collapse: collapse;">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px; word-break: break-all;text-align: center" colspan="1">
                                            <asp:Label ID="lb_WorkflowName" runat="server" Text='<%# Eval("WorkflowName") %>' />
                                            <br />
                                            <asp:Label ID="lb_WorkflowDescription" runat="server" Text='<%# Eval("WorkflowDescription") %>' />
                                            <br />
                                            <asp:CheckBox ID="chkbox_SelectAll" runat="server" Checked="false" Text="全选" AutoPostBack="true"
                                                OnCheckedChanged="CheckAll_CheckedChanged" />
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                            <asp:CheckBoxList ID="chkboxlist_RoleList" runat="server" DataTextField="RoleName"
                                                DataValueField="WorkflowRoleID" DataSource='<%# Eval("AllRoleList") %>' RepeatDirection="Horizontal"  OnSelectedIndexChanged ="cbl_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
            <tr id="Tr1" runat="server">
                <td id="Td1" align="right" style="height: 38px" runat="server">
                    <asp:Button ID="btn_Save" runat="server" CssClass="button_bak" Text="保存修改" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
