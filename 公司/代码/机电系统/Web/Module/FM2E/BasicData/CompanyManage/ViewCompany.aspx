<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewCompany.aspx.cs" Inherits="Module_FM2E_BasicData_CompanyManage_ViewCompany"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="公司信息维护"
        HeadOPTxt="目前操作功能：查看公司详情">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="管理部门信息" ButtonPopedom="Edit"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="../DeptManage/Dept.aspx?companyid=" />
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="返回列表" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="Company.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
      
            <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                <tr>
                    <td class="Table_searchtitle" colspan="3">
                        公司详细信息
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        公司编号：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label1" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                    <td rowspan="13" style="vertical-align: top;">
                        <div style="width: 100%; text-align: center; vertical-align: middle;">
                            <asp:Image ID="Image1" runat="server" Width="220px" AlternateText="No Picture" /></div>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        公司名称：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label2" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        是否总公司：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label10" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px; height: 28px;">
                        地址：
                    </td>
                    <td style="width: 512px; height: 28px;">
                        <asp:Label ID="Label3" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        联系人：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label4" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        联系电话：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label5" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        网址：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label6" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        电子邮件：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label7" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        传真：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label8" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="width: 128px">
                        备注：
                    </td>
                    <td style="width: 512px">
                        <asp:Label ID="Label9" runat="server" Text="Label" Width="250px"></asp:Label>
                    </td>
                </tr>
                
            </table>
    </div>
</asp:Content>
