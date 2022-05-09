<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewDeviceType.aspx.cs" Inherits="Module_FM2E_BasicData_DeviceTypeManage_ViewDeviceType"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备种类信息维护" HeadOPTxt="目前操作功能：查看设备种类详情">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="设备种类列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="DeviceType.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加子类型" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditDeviceType.aspx?cmd=add" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript" ButtonPopedom="List" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <iframe style="Z-INDEX:-1;WIDTH:99%;POSITION:absolute;TOP:0px;" frameborder="0"></iframe>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style=" width: 99%;">
                <tr>
                    <td style="width: 15%;" align="left" valign="top">
                        <div style="width:100px; overflow:scroll;overflow:hidden">
                            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                <SelectedNodeStyle ForeColor="#FF3300" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td valign="top" style="width:85%;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div style="width: 100%;">
                                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                        &nbsp;
                                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                                            <tr>
                                                <td class="Table_searchtitle" colspan="4">
                                                    设备种类信息
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    种类编码：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="CategoryID" runat="server"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    种类名称：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="CategoryName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    设备单位：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="Unit" runat="server"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    上级类型名称：
                                                </td>
                                                <td class="table_none table_none_NoWidth">                                                   
                                                    <asp:HyperLink ID="ParentCategoryName" ForeColor="Red"  runat="server"></asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    所在层次：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="Level" runat="server"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    下级类型数目：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="Childrencount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    折旧方法：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="DepreciationMethod" runat="server"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    折旧年限：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="DepreciationLife" runat="server"></asp:Label>年
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    净残值率：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="ResidualRate" runat="server"></asp:Label>%
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    下级类型信息：
                                                </td>
                                                <td class="table_none table_none_NoWidth" colspan="3">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                                                        HeaderStyle-Height="25px" RowStyle-Height="20px" OnRowCommand="GridView1_RowCommand" Width="100%"
                                                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" OnRowDataBound="GridView1_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="CategoryID" HeaderText="种类编码">
                                                                <HeaderStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CategoryName" HeaderText="种类名称">
                                                                <HeaderStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Unit" HeaderText="设备单位">
                                                                <HeaderStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ParentName" HeaderText="上一级种类">
                                                                <HeaderStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DepreciableLife" HeaderText="折旧年限">
                                                                <HeaderStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ResidualRate" DataFormatString="{0:#,0.##}" HeaderText="净残值率%">
                                                                <HeaderStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                                                HeaderText="查看" CommandName="view">
                                                                <HeaderStyle Width="60px" />
                                                            </asp:ButtonField>
                                                            <asp:TemplateField>
                                                                <ItemStyle Width="60px" /><HeaderTemplate>删除</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                                        CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此设备种类信息吗？')"
                                                                        CausesValidation="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            没有子类信息
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
