<%@ Page Title="查询指导价" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="QueryStorage.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApproval_QueryStorage" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
<iframe style="Z-INDEX:-1;WIDTH:99%;POSITION:absolute;TOP:0px;HEIGHT:435px" frameborder="0"></iframe>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        //清空查询参数
        function clearQueryInput() {
            document.getElementById('<%= TextBox_ProductName.ClientID %>').value = "";
            document.getElementById('<%= TextBox_Model.ClientID %>').value = "";
        }
    </script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <cc2:TabPanel runat="server" HeaderText="查询" ID="TabPanel_Query">
            <HeaderTemplate>
                查询
            </HeaderTemplate>
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            组合查询（支持模糊查询）
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            产品名称：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_ProductName" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            规格型号：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                           仓库：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:DropDownList ID="DDLWarehouse" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                    runat="server">
                    <tr runat="server">
                        <td align="center" style="height: 30px" runat="server">
                            <asp:Button ID="Button_Query" runat="server" CssClass="button_bak" Text="确定" OnClick="Button_Query_Click" />&nbsp;&nbsp;
                            <input class="button_bak" type="button" value="重填" onclick="javascript:clearQueryInput();" />&nbsp;&nbsp;
                            <input class="button_bak" onclick="javascript:window.parent.hidePopWin();" type="button"
                                value="关闭" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="查询结果--设备" ID="TabPanel_ResultDevice">
            <ContentTemplate>

                            <div style="width: 100%; overflow:auto; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_ResultDevice" runat="server" AutoGenerateColumns="False"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="仓库">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Bind("WareHouseName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="产品名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Bind("EquipmentName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格型号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductModel" runat="server" Text='<%# Bind("EquipmentModel") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="库存量">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_LowerPrice" runat="server" Text='<%# Bind("Storage","{0:#,0.##}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单位">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                       
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" CloneFrom="" OnPageChanged="AspNetPager1_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left">
                                </cc1:AspNetPager>
                            </div>

            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="查询结果--消耗品" ID="TabPanel_ResultExpendable">
            <ContentTemplate>
                
                            <div style="width: 100%; overflow:auto; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_ResultExpendable" runat="server" AutoGenerateColumns="False"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="仓库">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Bind("WareHouseName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="产品名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Bind("EquipmentName") %>'></asp:Label>
                                            </ItemTemplate><ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格型号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductModel" runat="server" Text='<%# Bind("EquipmentModel") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="库存量">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_LowerPrice" runat="server" Text='<%# Bind("Storage","{0:#,0.##}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单位">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" CloneFrom=""  OnPageChanged="AspNetPager2_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left">
                                </cc1:AspNetPager>
                            </div>

            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
