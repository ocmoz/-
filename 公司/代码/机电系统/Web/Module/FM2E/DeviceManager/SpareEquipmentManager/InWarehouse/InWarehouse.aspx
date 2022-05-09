<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="InWarehouse.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_InWarehouse_InWarehouse"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:headmenuwebcontrols id="HeadMenuWebControls1" runat="server" headtitletxt="设备入库信息维护"
        headoptxt="目前操作功能：入库信息列表" headhelptxt="入库列表默认显示新增入库">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加入库" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditInWarehouse.aspx?cmd=add" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="批量导入" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="Impotwarehouse.aspx" />
    </cc1:headmenuwebcontrols>
    <input type="hidden" id="Hidden_WarehouseAddressCode" runat="server" />
    <input type="hidden" id="Hidden_WarehouseAddressID" runat="server" />
    <input type="hidden" id="Hidden_WarehouseAddressName" runat="server" />
    <input type="hidden" id="Hidden_WarehouseID" runat="server" />
    <input type="hidden" id="Hidden_WarehouseName" runat="server" />
    <div style="width: 100%;">
        <cc2:tabcontainer id="TabContainer1" runat="server" activetabindex="0">
            <cc2:TabPanel runat="server" HeaderText="入库列表" ID="TabPanel1">
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
                                 <asp:BoundField DataField="SheetName" HeaderText="表单号"></asp:BoundField>
                                <asp:BoundField DataField="WarehouseName" HeaderText="仓库"></asp:BoundField>
                               
                                <asp:BoundField DataField="DepartmentName" HeaderText="入库部门"></asp:BoundField>
                                
                                <asp:BoundField DataField="ApplicantName" HeaderText="经办人"></asp:BoundField>
                                <asp:BoundField DataField="OperatorName" HeaderText="仓管员"></asp:BoundField>
                                <asp:BoundField DataField="SubmitTime" HeaderText="入库时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>
                                <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="查看" CommandName="view"></asp:ButtonField>
                            </Columns>
                            <EmptyDataTemplate>
                                <center>没有入库信息</center>
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
            <cc2:TabPanel runat="server" HeaderText="入库查询" ID="TabPanel2">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                组合查询（支持模糊查询）
                            </td>
                        </tr>
                        <tr>
                         <td class="table_body table_body_NoWidth">
                                表单编号：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_SheetID" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                部门：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DropDownList_Department" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                         <td class="table_body table_body_NoWidth">
                                经办人：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_ApplicantName" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                仓管员：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                 <asp:TextBox ID="TextBox_WarehouseKeeperName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                         <td class="table_body table_body_NoWidth">
                                入库时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_TimeLower" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>至
                                <asp:TextBox ID="TextBox_TimeUpper" runat="server"  CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                            </td>
                           
                        </tr>
                      
                    </table>
                    <center>
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </center>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:tabcontainer>
    </div>
</asp:Content>
