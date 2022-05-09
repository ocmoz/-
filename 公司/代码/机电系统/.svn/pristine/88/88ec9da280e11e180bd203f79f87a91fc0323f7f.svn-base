<%@ Page Title="查看采购单" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewPurchaseOrder.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_ViewPurchaseOrder" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="申请采购"
        HeadOPTxt="目前操作功能：查看申购单">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑表单" ButtonUrlType="Href" ButtonPopedom="Edit"
            ButtonUrl="PurchaseApply.aspx"/>
            <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="追加采购" ButtonUrlType="Href" ButtonPopedom="New"
            ButtonUrl="PurchaseApply.aspx"  />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回我的申购单" ButtonUrlType="Href" ButtonPopedom="List"
            ButtonUrl="PurchaseHistory.aspx" />
        
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="history.go(-1);"  />
    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    材料申购表
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="2" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="未知公司"></asp:Label><br />
                    <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue" ></asp:Label>&nbsp;机电材料申购表
                </td>
                <td style="text-align: center" colspan="2" class="Table_searchtitle">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="Label_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="gridview_ItemList_RowDataBound" ShowFooter="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="系统">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="产品名称 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格型号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量<br/>申请/审批/购买/验收">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_PlanCount" runat="server" 
                                                Text='<%# Eval("PlanCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustCount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualCount" runat="server"
                                                Text='<%# Eval("ActualCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AcceptedCount" runat="server"
                                                Text='<%# Eval("AcceptedCount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                     
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单价(元)<br/>申请/审批/购买">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_UnitPrice" runat="server" Text='<%# Eval("Price", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustPrice" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustPrice", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualPrice" runat="server" 
                                                Text='<%# Eval("ActualPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)<br/>申请/审批/购买">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_PlanAmount" runat="server" Text='<%# Eval("PlanAmount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustAmount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustAmount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualAmount" runat="server" 
                                                Text='<%# Eval("ActualAmount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustTotalAmount" runat="server" ForeColor="Red" >
                                            </asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualTotalAmount" runat="server"  ></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未添加申购材料</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
             <tr>
                <td class="Table_searchtitle" style="text-align:right; width:15%">提交时间：</td>
                <td style="width:35%">  <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label></td>
                 <td class="Table_searchtitle" style="text-align:right; width:15%">申请人：</td>
                 <td style="width:35%"> <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align:right; width:15%">最后更新：</td>
                <td style="width:35%">  <asp:Label ID="Label_UpdateTime" runat="server"></asp:Label></td>
                 <td class="Table_searchtitle" style="text-align:right; width:15%">当前状态：</td>
                 <td style="width:35%">  <asp:Label ID="Label_Status" runat="server"></asp:Label>
                 <asp:Label ID="Label_Approvaling" runat="server"></asp:Label>
                 </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                   审批历史
                </td>
            </tr>
                        <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_ApprovalRecord">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                 >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="审批人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Bind("ApprovalerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批结果">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Bind("ResultString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="反馈意见">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" 
                                                Text='<%# Bind("FeeBack") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细建议">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Bind("ApprovalLog") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Bind("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未经审批</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
             <tr>
                <td class="Table_searchtitle" colspan="4">
                   修改历史
                </td>
             </tr>
                        <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ModifyRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                 >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="修改人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Bind("ModifierName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改操作">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Bind("ModifyTypeString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="保存内容">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" 
                                                Text='<%# Bind("Content") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="修改时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Bind("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未作修改</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
             <tr>
                <td class="Table_searchtitle" colspan="4">
                   相关申购单
                </td>
             </tr>
                        <tr>
                <td colspan="4">

                            <asp:GridView ID="gridview_RelatedOrders" runat="server" AutoGenerateColumns="False" 
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" 
                                 RowStyle-Height="20px" Width="100%"
                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" 
                                >
                        <Columns>
                            <asp:TemplateField HeaderText="申购单">
                                <ItemTemplate>
                                    <a style="color:Blue" href='ViewPurchaseOrder.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history&return=1'>
                                    <asp:Label Text='<%# Bind("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label Text='<%# Bind("SubOrderIndex")%>' runat="server"
                                    ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                     <asp:Label Text='<%# Bind("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName" Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料申购单
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
                           
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

