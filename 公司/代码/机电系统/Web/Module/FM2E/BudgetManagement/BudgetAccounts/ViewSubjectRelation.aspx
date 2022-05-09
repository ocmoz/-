<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewSubjectRelation.aspx.cs" Inherits="Module_FM2E_BudgetManagement_BudgetAccounts_ViewSubjectRelation"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="预算科目信息维护"
        HeadOPTxt="目前操作功能：查看预算详情">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="预算科目列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="SubjectRelation.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加子科目" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditSubjectRelation.aspx?cmd=add" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 900px;" border="1">
                <tr>
                    <td style="width: 200px;" align="left" valign="top">
                        <div style="width: 200px; overflow: scroll; overflow-y: hidden">
                            <asp:TreeView ID="TreeView1" runat="server">
                            <SelectedNodeStyle ForeColor="#FF3300" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td valign="top" style="width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div style="width: 100%;">
                                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                        &nbsp;
                                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                                            <tr>
                                                <td class="Table_searchtitle" colspan="4">
                                                    预算科目信息
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style=" width:20%">
                                                    科目名称:
                                                </td>
                                                <td class="table_none table_none_NoWidth" style=" width:30%">
                                                    <asp:Label ID="Name" runat="server"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth" style=" width:20%">
                                                上级科目名:
                                                </td>
                                                <td class="table_none table_none_NoWidth" style=" width:30%">
                                                    <asp:HyperLink ID="ParentName" ForeColor="Red" runat="server"></asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style=" width:20%">
                                                是否底层科目:
                                                </td>
                                                <td class="table_none table_none_NoWidth" style=" width:30%">
                                                    <asp:Label ID="IsLeafshow" runat="server"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth"  style=" width:20%">
                                                </td>
                                                <td class="table_none table_none_NoWidth" style=" width:30%">
                                                <input id="SubID" runat="server" type="hidden" />
                                                <input id="IsLeaf" runat="server" type="hidden" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style=" width:20%">
                                                下级科目信息：
                                                </td>
                                                <td class="table_none table_none_NoWidth" colspan="3">
                                                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                                        OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                                        OnRowDataBound="GridView1_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="Name" HeaderText="科目名称"></asp:BoundField>
                                                            <asp:BoundField DataField="ParentName" HeaderText="上级科目名称"></asp:BoundField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                是否最底层科目
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#(Convert.ToInt32(Eval("IsLeaf"))==1)?"是":"否"%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                                                HeaderText="查看" CommandName="view"></asp:ButtonField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    删除
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                                        CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此预算科目信息吗？')"
                                                                        CausesValidation="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            没有预算科目信息
                                                        </EmptyDataTemplate>
                                                        <RowStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
