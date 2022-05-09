<%@ page title="采购查询" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_QueryPurchase, App_Web_qqri7mb6" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="WebUtility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        function clearbox() {

            document.getElementById('<%= TextBox_OrderSn.ClientID %>').value = "";
            document.getElementById('<%= TextBox_OrderName.ClientID %>').value = "";
            document.getElementById('<%= TextBox_AmountLower.ClientID %>').value = "";
            document.getElementById('<%= TextBox_TimeLower.ClientID %>').value = "";
            document.getElementById('<%= TextBox_TimeUpper.ClientID %>').value = "";
            document.getElementById('<%= TextBox_AmountUpper.ClientID %>').value = "";
            document.getElementById('<%= TextBox_ProductName.ClientID %>').value = "";
            document.getElementById('<%= TextBox_Model.ClientID %>').value = "";
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="采购单查询"
        HeadOPTxt='目前操作功能：查询采购单' HeadHelpTxt="">
    </cc1:HeadMenuWebControls>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <cc2:TabPanel runat="server" HeaderText="申购单列表" ID="TabPanel0">
            <ContentTemplate>
                <asp:GridView ID="gridview_PurchaseApplyList" runat="server" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" RowStyle-Height="20px"
                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("CompanyName") %>' runat="server" ID="Label_CompanyName"></asp:Label>
                            </ItemTemplate>
                            <HeaderTemplate>
                                公司
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申购单">
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label
                                    Text='<%# Bind("SubOrderIndex")%>' runat="server" ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                <asp:Label Text='<%# Bind("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName"
                                    Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料申购单
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="PlanTotalAmount" HeaderText="申请金额(元)" DataFormatString="{0:#,0.##}">
                            <HeaderStyle />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StatusString" HeaderText="状态">
                            <HeaderStyle />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <HeaderStyle />
                            <ItemStyle Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Remark" HeaderText="备注">
                            <HeaderStyle />
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" NavigateUrl='<%# "~/Module/FM2E/DeviceManager/SpareEquipmentManager/Purchase/PurchaseApply/ViewPurchaseOrder.aspx?id="+Eval("ID").ToString()+"&cmd=viewArchives" %>'
                                    ImageUrl="~/images/ICON/select.gif"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
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
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="报验单" ID="TabPanel2">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gridview_check" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                            DataKeyNames="ID" HeaderStyle-Height="25px" RowStyle-Height="20px" Width="100%"
                            HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("CompanyName") %>' runat="server" ID="Label_CompanyName"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        公司
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="报验单">
                                    <ItemTemplate>
                                            <asp:Label Text='<%# Eval("SheetID") %>' runat="server" ID="Label_OrderID"></asp:Label>&nbsp;
                                            <asp:Label Text='<%# Eval("SheetName") %>' runat="server" ID="Label_PurchaseOrderName"
                                                Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料报验单
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
                                <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" NavigateUrl='<%# "ExecutePurchasing/ViewCAForm.aspx?id="+Eval("ID").ToString()+"&cmd=viewArchives" %>'
                                    ImageUrl="~/images/ICON/select.gif"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                            </Columns>
                            <RowStyle Height="20px" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="采购单查询" ID="TabPanel1">
            <ContentTemplate>
                <table id="Table1" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            采购单查询
                        </td>
                    </tr>
                    <tr style='<%= UserData.CurrentUserData.IsParentCompany? "display:block": "display:none"  %>'>
                        <td class="Table_searchtitle">
                            公司或单位：
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="DropDownList_Company" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle">
                            表单编号：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_OrderSn" runat="server"></asp:TextBox>
                        </td>
                        <td class="Table_searchtitle">
                            申购单名称：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_OrderName" runat="server"></asp:TextBox>材料申购单
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle">
                            申购金额：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_AmountLower" runat="server"></asp:TextBox>-<asp:TextBox
                                ID="TextBox_AmountUpper" runat="server"></asp:TextBox>元
                        </td>
                        <td class="Table_searchtitle">
                            时间：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_TimeLower" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>-
                            <asp:TextBox ID="TextBox_TimeUpper" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle">
                            产品名称：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_ProductName" runat="server"></asp:TextBox>
                        </td>
                        <td class="Table_searchtitle">
                            产品型号：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            <asp:Button ID="Button_Query" runat="server" Text="查询" CssClass="button_bak" OnClick="Button_Query_Click" />
                            <input type="button" id="button_clear" value="清空" class="button_bak" onclick="javascript:clearbox();" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
