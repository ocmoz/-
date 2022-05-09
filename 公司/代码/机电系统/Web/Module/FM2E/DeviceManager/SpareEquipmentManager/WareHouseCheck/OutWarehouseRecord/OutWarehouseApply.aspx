<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="OutWarehouseApply.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseRecord_OutWarehouseApply"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备出库信息登记"
        HeadOPTxt="目前操作功能：设备出库信息列表" HeadHelpTxt="申请列表默认显示新增申请">
        <%--<cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加申请" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditOutWarehouseApply.aspx?cmd=add" />--%>
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="等待出库的申请单" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                            Width="100%" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SheetName" HeaderText="申请单号"></asp:BoundField>
                                <asp:BoundField DataField="WarehouseName" HeaderText="仓库"></asp:BoundField>
                                <asp:BoundField DataField="ApplicantName" HeaderText="申请人"></asp:BoundField>
                                <asp:BoundField DataField="ApplyTime" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"></asp:BoundField>
                                <asp:BoundField DataField="WorkFlowStateDescription" HeaderText="状态"></asp:BoundField>
                                <asp:TemplateField HeaderText="出库">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/b.gif"
                                            CommandName="outEquipments" CommandArgument='<%# Eval("ID") %>'
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                             <center><span style="color:Red">没有申请信息</span></center>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left" CloneFrom="">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="出库历史" ID="TabPanel2">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand" Width="100%"
                            OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                               <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SheetName" HeaderText="申请单号"></asp:BoundField>
                                <asp:BoundField DataField="WarehouseName" HeaderText="仓库"></asp:BoundField>
                                <asp:BoundField DataField="ApplicantName" HeaderText="申请人"></asp:BoundField>
                                <asp:BoundField DataField="ApplyTime" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"></asp:BoundField>
                                <asp:BoundField DataField="WorkFlowStateDescription" HeaderText="状态"></asp:BoundField>
                                <asp:TemplateField HeaderText="查看">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/select.gif"
                                            CommandName="view" CommandArgument='<%# Eval("ID") %>'
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                               <center><span style="color:Red">没有符合条件的出库历史信息</span> </center>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager2_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left" CloneFrom="">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="出库历史查询" ID="TabPanel3">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                组合查询（支持模糊查询）
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                申请单编号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox_SheetName" runat="server"></asp:TextBox>
                            </td>
                           <td class="table_body table_body_NoWidth" style="height: 30px">
                                仓库：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:Label ID="Label_WarehouseName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                申请人账号：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_Applicant" runat="server"></asp:TextBox>
                            </td>
<td class="table_body table_body_NoWidth">
                                申请人姓名：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_ApplicantName" runat="server"></asp:TextBox>
                            </td>  
                        </tr>
                        
                         <tr>
                         <td class="table_body table_body_NoWidth">
                                领用人账号：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_Receiver" runat="server"></asp:TextBox>
                            </td>
                         <td class="table_body table_body_NoWidth">
                                领用人姓名：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_ReceiverName" runat="server"></asp:TextBox>
                            </td>  
                        </tr>
                          <tr>
                         <td class="table_body table_body_NoWidth">
                                申请时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_ApplyTimeLower" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>至
                                <asp:TextBox ID="TextBox_ApplyTimeUpper" runat="server"  CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                            </td>
                         <td class="table_body table_body_NoWidth">
                                出库时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_OutTimeLower" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>至
                                <asp:TextBox ID="TextBox_OutTimeUpper" runat="server"  CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                            </td>  
                        </tr>
                    </table>
                    <center>
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </center>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
