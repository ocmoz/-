<%@ Page Title="申请记录--已完成申购" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="PurchaseFinishHistory.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_PurchaseFinishHistory" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <script type="text/javascript">
    //清空查询条件输入框
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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="采购申请"
        HeadOPTxt='目前操作功能：已经完成的申购单查看' HeadHelpTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回我的申购单" ButtonUrlType="Href"
            ButtonUrl="PurchaseHistory.aspx" />
    </cc1:HeadMenuWebControls>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <cc2:TabPanel runat="server" HeaderText="已完成申购单" ID="TabPanel0">
            <ContentTemplate>
                <div>
                    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                <span style="color: Blue">
                                    <%= WebUtility.UserData.CurrentUserData.PersonName %>(
                                    <%= WebUtility.UserData.CurrentUserData.UserName %>
                                    )</span>已经完成的申购单列表
                            </td>
                        </tr>
                        <tr>
                            <td class="table_none_NoWidth">
                                <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gridview_PurchaseApplyList" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                                    OnRowDeleting="gridview_PurchaseApplyList_RowDeleting" OnRowEditing="gridview_PurchaseApplyList_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="申购单">
                                                            <ItemTemplate>
                                                                <a style="color: Blue" href='ViewPurchaseOrder.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history'>
                                                                    <asp:Label Text='<%# Bind("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label
                                                                        Text='<%# Bind("SubOrderIndex")%>' runat="server" ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                                                    <asp:Label Text='<%# Bind("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName"
                                                                        Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料申购单 </a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="WorkFlowStateDescription" HeaderText="状态">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="提交时间">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_SubmitTime" Text='<%# (DateTime)Eval("SubmitTime") == DateTime.MinValue ? "": Eval("SubmitTime","{0:yyyy-MM-dd HH:mm}") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Remark" HeaderText="备注">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                                                    Text="修改" CommandName="Edit" Visible='<%# DataBinder.Eval(Container.DataItem,"CanEdit") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                                    Visible='<%# DataBinder.Eval(Container.DataItem,"CanDelete") %>' CommandName="Delete"
                                                                    ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" />
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
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="已完成的申购单查询" ID="TabPanel1">
            <ContentTemplate>
                <table id="Table1" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            <span style="color: Blue">
                                <%= WebUtility.UserData.CurrentUserData.PersonName %>(
                                <%= WebUtility.UserData.CurrentUserData.UserName %>
                                )</span>已经完成的申购单查询
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
                            申购总金额：
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
