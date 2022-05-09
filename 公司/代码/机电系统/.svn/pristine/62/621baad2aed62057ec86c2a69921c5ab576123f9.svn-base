<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="RoleList.aspx.cs" Inherits="Module_FM2E_SystemManager_RoleManager_RoleList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="角色管理"
        HeadHelpTxt="点击角色进入角色管理" HeadOPTxt="目前操作功能：角色列表">
        <cc1:HeadMenuButtonItem ButtonName="添加角色" ButtonIcon="new.gif" ButtonUrl="RoleManage.aspx?cmd=add" ButtonPopedom="New"
            ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="角色列表" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"  onrowcommand="GridView1_RowCommand" 
                            onrowdatabound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有角色信息
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="RoleID" HeaderText="角色ID" />
                                <asp:BoundField DataField="RoleName" HeaderText="角色名称" />
                                <asp:BoundField DataField="Description" HeaderText="角色介绍" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此模块信息吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" 
                            OnPageChanged="AspNetPager1_PageChanged" AlwaysShow="True" CloneFrom="" 
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" 
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="" 
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left" >
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
