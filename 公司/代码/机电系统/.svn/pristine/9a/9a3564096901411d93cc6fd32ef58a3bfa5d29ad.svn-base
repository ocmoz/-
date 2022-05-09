<%@ Page Title="采购记录" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseHistory.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseCenter_PurchaseHistory" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
     <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件采购" 
        HeadOPTxt='目前操作功能：处理过的采购单' >
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="采购中心" ButtonUrlType="Href"
            ButtonUrl="PurchaseCenter.aspx" />
    </cc1:HeadMenuWebControls>
    
    <div>
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                已经处理过的采购单
            </td>
        </tr>
        <tr>
            <td class="table_none_NoWidth">
                <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                     <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>

                   <asp:GridView ID="gridview_PurchaseApplyList" runat="server" AutoGenerateColumns="False" 
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" 
                                 RowStyle-Height="20px" Width="100%"
                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" 
                                 onrowdeleting="gridview_PurchaseApplyList_RowDeleting" 
                                 onrowediting="gridview_PurchaseApplyList_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="申购单">
                                <ItemTemplate>
                                    <a style="color:Blue" href='ViewPurchaseOrder.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history'>
                                    <asp:Label Text='<%# Bind("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label Text='<%# Bind("SubOrderIndex")%>' runat="server"
                                    ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                     <asp:Label Text='<%# Bind("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName"></asp:Label>机电材料申购单
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%"  />
                            </asp:TemplateField>
                            <asp:BoundField DataField="StatusString" HeaderText="状态">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                <HeaderStyle />
                                <ItemStyle  Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False"
                                                ImageUrl="~/images/ICON/edit.gif" Text="修改" CommandName="Edit"
                                                 Visible='<%# DataBinder.Eval(Container.DataItem,"CanEdit") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False" Visible='<%# DataBinder.Eval(Container.DataItem,"CanDelete") %>' 
                                                CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>
                    
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged" 
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" 
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="" 
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
                                             
                         </ContentTemplate>
                         </asp:UpdatePanel>
                </div>
                </div>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

