<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="WareHouseList.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_FeeBackFill_WareHouseList" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库检查表签名"
        HeadOPTxt="目前操作功能：仓库检查单列表" HeadHelpTxt="列表默认显示最近仓库检查单">
       
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" e>
            <cc2:TabPanel runat="server" HeaderText="我的设备借调申请" ID="TabPanel1">
                <HeaderTemplate>
                    最近的仓库检查单
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有仓库检查单
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="FormNO" HeaderText="检查单编号" />
                                <asp:BoundField DataField="WareHouseName" HeaderText="检查仓库" />
                                <asp:BoundField DataField="CheckerName" HeaderText="检查人员" />
                                <asp:BoundField DataField="CheckDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" HeaderText="检查时间" />
                                <asp:BoundField DataField="StatusString" HeaderText="检查单状态" />
                                <asp:BoundField DataField="UpdateTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false" HeaderText="更新时间" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/Approval.gif" HeaderText="填写意见"
                                    CommandName="edit">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="查询" ID="TabPanel2">
                <HeaderTemplate>
                    查询
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
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
                                    <asp:TextBox ID="tbFormNO" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    检查的仓库：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlWareHouse" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    检查时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:TextBox ID="tbCheckDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入检查时间~date"></asp:TextBox>&nbsp;至&nbsp;<asp:TextBox ID="tbCheckDateTo" runat="server"
                                            class="input_calender" onfocus="javascript:HS_setDate(this);" title="请输入检查时间~date"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                            <tr>
                                <td align="right" style="height: 38px">
                                    <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="确定" OnClick="btSubmit_Click" />&nbsp;&nbsp;
                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>


