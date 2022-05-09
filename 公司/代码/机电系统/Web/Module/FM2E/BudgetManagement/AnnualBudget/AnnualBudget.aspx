<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="AnnualBudget.aspx.cs" Inherits="Module_FM2E_BudgetManagement_AnnualBudget_AnnualBudget"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Import Namespace="FM2E.WorkflowLayer"%>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="年度预算查阅"
        HeadOPTxt="目前操作功能：年度预算查阅">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加年度预算" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="MakeAnnualBudget.aspx?cmd=add" />
<%--        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />--%>
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                            HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                            OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                            OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Year" HeaderText="年份"></asp:BoundField>
                                <asp:BoundField DataField="Maker" HeaderText="制作人"></asp:BoundField>
                                <asp:BoundField DataField="Title" HeaderText="部门"></asp:BoundField>
                                <asp:BoundField DataField="BudgetApply" HeaderText="申报总额"></asp:BoundField>
                                <%--<asp:BoundField DataField="BudgetApprove" HeaderText="审批总额"></asp:BoundField>--%>
                                <asp:BoundField DataField="MakeTime" HeaderText="制作时间"></asp:BoundField>
                                <%--<asp:BoundField DataField="WorkFlowStateDescription" HeaderText="审核状态"></asp:BoundField>--%>
                                <%--<asp:BoundField DataField="Approvaler" HeaderText="上次审核者"></asp:BoundField>--%>
                                <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="查看" CommandName="view"></asp:ButtonField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        删除
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此年度预算信息吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        制定季度预算
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="AddMonthlyBudget" runat="server" ImageUrl="~/images/ICON/new.gif"
                                            CommandName="AddMonthlyBudget" CommandArgument="<%# Container.DataItemIndex %>"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        制定季度预测
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="AddQuarterlyForecast" runat="server" ImageUrl="~/images/ICON/new.gif"
                                            CommandName="AddQuarterlyForecast" CommandArgument="<%# Container.DataItemIndex %>"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                没有年度预算信息
                            </EmptyDataTemplate>
                            <RowStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
