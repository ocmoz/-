<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditWorkflowRole.aspx.cs" Inherits="Module_FM2E_WorkflowManager_EditWorkflowRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="工作流管理"
        HeadHelpTxt="帮助" HeadOPTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="工作流角色列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="WorkflowRoleList.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%s; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="编辑工作流角色信息" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                工作流角色详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                角色名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:TextBox ID="TextBox_RoleName" runat="server" Columns="20" title="请输入角色名称~50:!"
                                    Width="120px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                是否专一角色：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:RadioButtonList ID="RadioButtonList_IsSingle" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是"/>
                                    <asp:ListItem Text="否"/>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                是否接受待办事务：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:RadioButtonList ID="RadioButtonList_IsApprover" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是" />
                                    <asp:ListItem Text="否"/>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                角色与工作流状态的绑定：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 160px" colspan="3">
                                <cc1:MultiListBox ID="MultiListBox_BindingStateList" runat="server" DataTextField="Description"
                                    DataValueField="Name" Heigth="" Rows="4" SelectionMode="Multiple">
                                    <FirstListBox Style="">
                                        <StyleSheet Height="160px" Width="140px" />
                                    </FirstListBox>
                                    <SecondListBox Style="">
                                        <StyleSheet Height="160px" Width="140px" />
                                    </SecondListBox>
                                </cc1:MultiListBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" align="right" style="height: 38px" runat="server">
                                <asp:Button ID="Button_OK" runat="server" CssClass="button_bak" Text="确定"  OnClick="Button_OK_Click"/>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
