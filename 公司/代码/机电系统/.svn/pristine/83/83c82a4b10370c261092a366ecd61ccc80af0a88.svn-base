<%@ Page Title="采购审批历史列表" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="PurchaseApprovalHistory.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_PurchaseApprovalHistory" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="申购单审批历史"
        HeadOPTxt="目前操作功能：我审批过的申购单">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="审批" ButtonUrlType="Href"
            ButtonUrl="PurchaseApproval.aspx" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <div>
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    <span style="color:Blue; font-weight:bold"> <%= WebUtility.UserData.CurrentUserData.PersonName %>(<%= WebUtility.UserData.CurrentUserData.UserName %>)</span>&nbsp;审批过的申购单
                </td>
            </tr>
            <tr>
                <td class="table_none_NoWidth">
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gridview_PurchaseApplyList" runat="server" AutoGenerateColumns="False"
                                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                        Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="申购单">
                                                <ItemTemplate>
                                                    <a style="color: Blue" href='ApprovalHistoryViewPurchaseOrder.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history'>
                                                        <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label
                                                            Text='<%# Eval("SubOrderIndex")%>' runat="server" ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                                        <asp:Label Text='<%# Eval("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName" Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料申购单
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkFlowStateDescription" HeaderText="状态">
                                                <HeaderStyle />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SubmitTime" HeaderText="提交时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                                <HeaderStyle />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                                <HeaderStyle />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="申请人">
                                                <ItemTemplate>
                                                   <asp:Label ID="Label_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                                <HeaderStyle />
                                                <ItemStyle Width="15%" />
                                            </asp:BoundField>
                                           
                                        </Columns>
                                        <RowStyle Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                    </asp:GridView>
                                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                        NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                        ShowCustomInfoSection="Left">
                                    </cc1:AspNetPager>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
