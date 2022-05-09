<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewDept.aspx.cs" Inherits="Module_FM2E_BasicData_DeptManage_ViewDept"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="目前操作功能：查看部门详情"
        HeadTitleTxt="部门详细信息查看">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加子部门" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditDept.aspx?cmd=add" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回公司" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="../CompanyManage/Company.aspx?cmd=view&id=" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="height: 100%; width: 100%;">
                <tr>
                    <td style="width: 20%;" align="left" valign="top">
                        <div>
                            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                <SelectedNodeStyle ForeColor="#FF3300" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td valign="top">
                        <div style="width: 100%;">
                            <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                                text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                                <tr>
                                    <td class="Table_searchtitle" colspan="2">
                                        部门详细信息
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        部门编号：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label1" runat="server" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        分管公司：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label2" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        部门名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label3" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        联系电话：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label4" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        负责人：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label5" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        上级部门：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label6" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        部门类型：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label_DepartmentType" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        所属层次：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label7" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        子部门数：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label8" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 76px">
                                        备注：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 512px">
                                        <asp:Label ID="Label9" runat="server" Text="" Width="250px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
