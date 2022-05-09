<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Statistics.aspx.cs" Inherits="Module_FM2E_Plan_Statistics" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script src="<%=Page.ResolveUrl("~/") %>js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="统计表"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：统计表">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加月度用款" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="Schedule.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
        <table width="100%">
            <tr>               
                <td>
                    时间：<input type="text" id="BeginTime" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月'})"
                        class="Wdate" runat="server" />至<input type="text" id="EndTime" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月'})"
                        class="Wdate" runat="server" />
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="button_bak" 
                        onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="13">
                                深圳高速公路股份有限公司</br>月度资金计划使用差异统计表
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td class="table_body_WithoutWidth" rowspan="2">
                                序号
                            </td>
                            <td class="table_body_WithoutWidth" colspan="5">
                                上月用款情况
                            </td>
                            <td class="table_body_WithoutWidth" colspan="2">
                                差异率%
                            </td>
                            <td class="table_body_WithoutWidth" colspan="3">
                                上月收入情况
                            </td>
                            <td class="table_body_WithoutWidth" colspan="2">
                                差异率%
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td class="table_body_WithoutWidth">
                                年
                            </td>
                            <td class="table_body_WithoutWidth">
                                月
                            </td>
                            <td class="table_body_WithoutWidth">
                                计划用款
                            </td>
                            <td class="table_body_WithoutWidth">
                                实际用款
                            </td>
                            <td class="table_body_WithoutWidth">
                                差额
                            </td>
                            <td class="table_body_WithoutWidth">
                                正差异率
                            </td>
                            <td class="table_body_WithoutWidth">
                                负差异率
                            </td>
                            <td class="table_body_WithoutWidth">
                                计划收入
                            </td>
                            <td class="table_body_WithoutWidth">
                                实际收入
                            </td>
                            <td class="table_body_WithoutWidth">
                                差额
                            </td>
                            <td class="table_body_WithoutWidth">
                                正差异率
                            </td>
                            <td class="table_body_WithoutWidth">
                                负差异率
                            </td>
                        </tr>
                        <asp:Repeater ID="r_Statistics" runat="server">
                            <ItemTemplate runat="Server">
                                <tr>
                                    <td class="table_body_WithoutWidth">
                                        <%# (Container.ItemIndex+1)%>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="r_lbPlanName"><%#Eval("Year")%></asp:Label>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="r_lbcontent"><%#Eval("Month")%></asp:Label>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="r_lbContractNo"><%#Eval("SumSchedule")%></asp:Label>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="r_lbAmount"><%#Eval("SumScheduleActual")%></asp:Label>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="r_lbExpectPaymentTime"><%#Eval("SumScheduleDifferent")%></asp:Label>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="r_lbRemark"><%#Convert.ToDecimal(Eval("SumScheduleDifferent")) >= 0 ? Eval("SumScheduleDifferentRatio") : ""%></asp:Label>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                      <asp:Label runat="server" ID="Label1"><%#Convert.ToDecimal(Eval("SumScheduleDifferent")) >= 0 ? "" : Eval("SumScheduleDifferentRatio")%></asp:Label>
                                    </td>
                                     <td class="table_none_WithoutWidth">
                                      <asp:Label runat="server" ID="Label2"><%#Eval("SumScheduleIncome")%></asp:Label>
                                    </td>
                                     <td class="table_none_WithoutWidth">
                                      <asp:Label runat="server" ID="Label3"><%#Eval("SumScheduleIncomeActual")%></asp:Label>
                                    </td>
                                     <td class="table_none_WithoutWidth">
                                      <asp:Label runat="server" ID="Label4"><%#Eval("SumScheduleIncomeDifferent")%></asp:Label>
                                    </td>
                                     <td class="table_none_WithoutWidth">
                                      <asp:Label runat="server" ID="Label5"><%#Convert.ToDecimal(Eval("SumScheduleIncomeDifferent")) >= 0 ? Eval("SumScheduleDifferentRatio") : ""%></asp:Label>
                                    </td>
                                     <td class="table_none_WithoutWidth">
                                      <asp:Label runat="server" ID="Label6"><%#Convert.ToDecimal(Eval("SumScheduleIncomeDifferent")) >= 0 ? "" : Eval("SumScheduleDifferentRatio")%></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>