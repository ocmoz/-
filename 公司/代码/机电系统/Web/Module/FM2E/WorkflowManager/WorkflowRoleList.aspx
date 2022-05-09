<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="WorkflowRoleList.aspx.cs" Inherits="Module_FM2E_WorkflowManager_WorkflowRoleList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="工作流管理"
        HeadHelpTxt="帮助" HeadOPTxt="">
        <cc1:HeadMenuButtonItem ButtonName="添加角色" ButtonIcon="new.gif" ButtonPopedom="New"
            ButtonUrl="EditWorkflowRole.aspx?cmd=add" ButtonUrlType="Href" />
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="工作流列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="WorkflowList.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="WorkflowList.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="工作流角色列表" ID="TabPanel1">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    该工作流尚无角色
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="RoleName" HeaderText="角色名称" />
                                    <asp:BoundField DataField="BindingStatesDisplay" HeaderText="对应工作流状态" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            专一角色</HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Convert.ToBoolean(Eval("IsSingle"))==true?"是":"否"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        接受待办事务</HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Convert.ToBoolean(Eval("IsApprover"))==true?"是":"否"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                        CommandName="viewRole">
                                        <HeaderStyle Width="60px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/edit.gif" HeaderText="编辑"
                                        CommandName="editRole">
                                        <HeaderStyle Width="60px" />
                                    </asp:ButtonField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="60px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Button_Delete" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除该工作流角色吗？')"
                                                CausesValidation="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>

</asp:Content>
