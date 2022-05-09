<%@ Page Title="采购中心" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="CheckHistory.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckHistory" %>

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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件验收历史"
        HeadOPTxt="目前操作功能：验收单历史列表">
            <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回验收" ButtonUrlType="Href"
            ButtonUrl="Check.aspx" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    
      <input type="hidden" id="Hidden_WarehouseID" runat="server" />
      
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <cc2:TabPanel runat="server" HeaderText="申购单" ID="TabPanel0">
            <ContentTemplate>
    <div>
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" unat="server">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    <asp:Label ID="Label_WarehouseName" ForeColor="Blue" runat="server" Font-Bold="true"></asp:Label>验收过的采购单
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
                                                    <a style="color: Blue" href='CheckPurchaseOrderHistory.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history'>
                                                        <asp:Label Text='<%# Bind("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label
                                                            Text='<%# Bind("SubOrderIndex")%>' runat="server" ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                                        <asp:Label Text='<%# Bind("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName" Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料申购单
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
   </ContentTemplate>
    </cc2:TabPanel>
    <cc2:TabPanel runat="server" HeaderText="报验单" ID="TabPanel1">
            <ContentTemplate>
        <div>
        <table id="Table1" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    <asp:Label ID="Label_WarehouseName2" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                    验收过的报验单
                </td>
            </tr>
            <tr>
                <td class="table_none_NoWidth">
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gridview_check" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="报验单">
                                            <ItemTemplate>
                                                <a style="color: Blue" href='ViewCAForm.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history'>
                                                    <asp:Label Text='<%# Eval("SheetID") %>' runat="server" ID="Label_OrderID"></asp:Label>&nbsp;
                                                    <asp:Label Text='<%# Eval("SheetName") %>' runat="server" ID="Label_PurchaseOrderName"
                                                        Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料报验单</a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                            <HeaderStyle />
                                            <ItemStyle Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Applicant" HeaderText="申请人">
                                            <HeaderStyle />
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="备注">
                                            <HeaderStyle />
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager2_PageChanged"
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
    </ContentTemplate>
    </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
