<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="History.aspx.cs" Inherits="Module_FM2E_PendingOrderMessage_History" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" OnNavigate="ScriptManager1_Navigate">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="待办事务管理"
        HeadOPTxt="目前操作功能：查看已阅读事务">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="未阅读" ButtonUrlType="Href"
            ButtonUrl="ViewPendingOrder.aspx" />
    </cc1:HeadMenuWebControls>
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                已办事务列表
            </td>
        </tr>
        <tr>
            <td class="table_none_NoWidth">
                <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_PendingOrderList" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="gridview_PendingOrderList_RowDataBound" OnRowCommand="gridview_PendingOrderList_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Title" HeaderText="标题">
                                        <HeaderStyle />
                                        <ItemStyle HorizontalAlign="Left" Width="45%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TypeString" HeaderText="类型">
                                        <HeaderStyle />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="发布人">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("SenderPersonName").ToString()+"("+Eval("SendFrom").ToString()+")" %>'
                                                runat="server" ID="Label_Sender"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SendTime" HeaderText="发布时间">
                                        <HeaderStyle />
                                        <ItemStyle Width="25%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该消息？');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorPendingOrder="页索引不是有效的数值！"
                                NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorPendingOrder="页索引超出范围！"
                                ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
