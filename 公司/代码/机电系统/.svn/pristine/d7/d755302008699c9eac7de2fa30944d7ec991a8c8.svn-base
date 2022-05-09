<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ArchivesManager.aspx.cs" Inherits="Module_FM2E_ArchivesManager_ArchivesManager"
    EnableEventValidation="false" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="档案信息查询"
        HeadOPTxt="目前操作功能：档案查询">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回申请表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="查询条件" ID="TabPanel1">
                <ContentTemplate>
                    <table id="outwarehouse" width="100%" cellpadding="0" cellspacing="0" border="1"
                        bordercolor="#cccccc" style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                查询条件
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                系统模块：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DropDownList1" runat="server" titel="请选择系统模块~!">
                                </asp:DropDownList>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                档案类型：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DropDownList2" runat="server" titel="请选择档案类型~!">
                                </asp:DropDownList>
                            </td>
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DropDownList1"
                                Category="Module" PromptText="请选择系统模块..." LoadingText="系统模块加载中..." ServicePath="ArchivesService.asmx"
                                ServiceMethod="GetModule" Enabled="True">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DropDownList2"
                                Category="Submodule" PromptText="请选择档案类型..." LoadingText="档案类型加载中..." ServicePath="ArchivesService.asmx"
                                ServiceMethod="GetSubmodule" ParentControlID="DropDownList1" Enabled="True">
                            </cc2:CascadingDropDown>
                        </tr>
                    </table>
                    <center><br />
                        <asp:Label ID="lbErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button_bak" Text="查询" OnClick="btnSearch_Click" />
                    </center> 
                    <br />
                    <br />
                     <div style="width:100%">
                     <table id="Table1" width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
                         <tr>
                            <td class="Table_searchtitle">
                                当前用户借阅档案列表
                            </td>
                        </tr>
                        <tr>
                            <td>
                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Module" HeaderText="系统模块"></asp:BoundField>
                                    <asp:BoundField DataField="ArchivesType" HeaderText="档案类型"></asp:BoundField>
                                    <asp:BoundField DataField="ArchivesName" HeaderText="档案名称"></asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                        HeaderText="查看" CommandName="view">
                                        <ItemStyle Width="5%" />
                                    </asp:ButtonField>
                                </Columns>
                                <EmptyDataTemplate>
                                    没有申请明细信息
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                ShowCustomInfoSection="Left" CloneFrom="">
                            </cc1:AspNetPager>
                            </td>
                        </tr>
                    </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
</asp:Content>
