<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewDepot.aspx.cs" Inherits="Module_FM2E_BasicData_DepotManage_ViewDepot"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="目前操作功能：查看仓库详情"  HeadTitleTxt="仓库信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript" ButtonPopedom="List" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 99%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="仓库详细信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        &nbsp;
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <th style="width: 100px" class="Table_searchtitle">
                                    仓库编号：
                                </th>
                                <td style="width: 512px">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 100px" class="Table_searchtitle">
                                    仓库名称：
                                </th>
                                <td style="width: 512px">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 100px" class="Table_searchtitle">
                                    仓库地点：
                                </th>
                                <td style="width: 512px">
                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 100px" class="Table_searchtitle">
                                    公司名称：
                                </th>
                                <td style="width: 512px">
                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 100px" class="Table_searchtitle">
                                    仓库负责人：
                                </th>
                                <td style="width: 512px">
                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 100px" class="Table_searchtitle">
                                    仓库联系人：
                                </th>
                                <td style="width: 512px">
                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 100px" class="Table_searchtitle">
                                    仓库电话：
                                </th>
                                <td style="width: 512px">
                                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 100px; text-align: left;" class="Table_searchtitle">
                                    备注：<br />
                                    <br />
                                </th>
                                <td >
                                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                
                                    <div id="Div1" runat="server" style="text-align: center">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    仓管员列表
                                </td>
                            </tr>
                            <tr>
                                <asp:GridView ID="GridView_WareHouseKeeper" runat="server" AutoGenerateColumns="False" Width="100%"
                                   >
                                    <Columns>
                                    <asp:BoundField DataField="UserName" HeaderText="用户名">
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StaffNo" HeaderText="工号">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PersonName" HeaderText="姓名">
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PositionName" HeaderText="职位">
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="公司">
                                            <HeaderStyle Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DepartmentName" HeaderText="部门">
                                            <HeaderStyle Width="25%" />
                                        </asp:BoundField>
                                        
                                        
                                    </Columns>
                                    <EmptyDataTemplate>
                                        没有仓管员信息
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left" CloneFrom="">
                                </cc1:AspNetPager>
                            </tr>
                            
                        </table>

                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
