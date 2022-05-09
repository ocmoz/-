<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_VieweApproval, App_Web_4xxpdmqi" title="公司信息维护" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="公司信息维护"
        HeadOPTxt="目前操作功能：出入库审批历史查阅">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table width="100%">
        <tr>
            <td colspan="4">
                <asp:Label ID="sheetname" runat="server" />
                <input id="sheettime" type="hidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView AutoGenerateColumns="False" Width="100%" ID="recordlist" runat="server" OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="物品名称">
                            <ItemTemplate>
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请时间">
                            <ItemTemplate>
                                <asp:Label ID="InOutTime" runat="server" Text='<%# Eval("InOutTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓管人">
                            <ItemTemplate>
                                <asp:Label ID="WarehousekeeperName" runat="server" Text='<%# Eval("WarehousekeeperName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="经手人">
                            <ItemTemplate>
                                <asp:Label ID="ReceiverName" runat="server" Text='<%# Eval("ReceiverName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出入库类型">
                            <ItemTemplate>
                                <asp:Label ID="Typestr" runat="server" Text='<%# Eval("Typestr") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出入库数量">
                            <ItemTemplate>
                                <asp:Label ID="Amountstr" runat="server" Text='<%# Eval("Amountstr") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位">
                            <ItemTemplate>
                                <asp:Label ID="Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="价格">
                            <ItemTemplate>
                                <asp:Label ID="Price" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Image" Text="审批历史" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="审批历史" CommandName="view">
                                    <ItemStyle Width="10%" HorizontalAlign="Center"/>
                                </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                行政业务意见:
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="xinzhenyewu" runat="server"></asp:Label>
            </td>
            <td class="table_body table_body_NoWidth">
                综合事务意见
            </td>
            <td class="table_none table_none_NoWidth">
                 <asp:Label ID="zongheshiwu" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                计划财务意见:
            </td>
            <td class="table_none table_none_NoWidth">
                 <asp:Label ID="jihuacaiwu" runat="server"></asp:Label>
            </td>
            <td class="table_body table_body_NoWidth">
                分管领导意见
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="fenguanlingdao" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                总经理意见:
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="zongjingli" runat="server"></asp:Label>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
        <td colspan="4" align="center">以下为历史审批记录</td>
        </tr>
        <tr>
        <td colspan="4">
            <asp:GridView AutoGenerateColumns="False" Width="100%" ID="modifygriview" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="物品名称">
                            <ItemTemplate>
                                <asp:Label ID="equipmentname" runat="server" Text='<%# Eval("equipmentname") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批时间">
                            <ItemTemplate>
                                <asp:Label ID="modifytime" runat="server" Text='<%# Eval("modifytime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批人">
                            <ItemTemplate>
                                <asp:Label ID="username" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="类型">
                            <ItemTemplate>
                                <asp:Label ID="type" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批前">
                            <ItemTemplate>
                                <asp:Label ID="oldnum" runat="server" Text='<%# Eval("oldnum") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批后">
                            <ItemTemplate>
                                <asp:Label ID="newnum" runat="server" Text='<%# Eval("newnum") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="savebutton" runat="server" Text="所有历史" OnClick="savebutton_Click" />
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
