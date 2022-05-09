<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewInWarehouse.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_InWarehouse_ViewInWarehouse"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备入库信息维护" HeadOPTxt="目前操作功能：查看设备入库信息">
    <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit"
            ButtonVisible="false" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <%--<cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="入库详细信息" ID="TabPanel1">
                <ContentTemplate>--%>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    公司：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                </td>
                           
                                <td class="table_body table_body_NoWidth">
                                    部门：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    入库单号：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    仓库：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </td>
                            
                                <td class="table_body table_body_NoWidth">
                                    入库时间：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    经办人：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                </td>
                           
                                <td class="table_body table_body_NoWidth">
                                    仓管员：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    入库备注：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3">
                                    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="产品名称"></asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="型号"></asp:BoundField>
                                        <asp:BoundField DataField="Count" HeaderText="数量" DataFormatString="{0:#,0.####}"></asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="单位"></asp:BoundField>
                                        <asp:BoundField DataField="InTime" HeaderText="入库时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <center>没有入库明细信息</center>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                
                                </td>
                            </tr>
                        </table>
                    </div>
               <%-- </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>--%>
    </div>
</asp:Content>
