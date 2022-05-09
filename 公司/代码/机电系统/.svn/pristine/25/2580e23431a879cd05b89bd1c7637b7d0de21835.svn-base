<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageGroupCheck.master" AutoEventWireup="true"
    CodeFile="ImplementationOfTheBudget.aspx.cs" Inherits="Module_FM2E_BudgetManagement_ImplementationOfTheBudget_ImplementationOfTheBudget"
    Title="无标题页" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="预算执行情况"
        HeadOPTxt="目前操作功能：预算执行情况">
        <%-- <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加月度预算" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="MakeMonthlyBudget.aspx?cmd=add" />--%>
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc2:TabPanel runat="server" HeaderText="实际开支录入" ID="TabPanel1">
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
                                            <asp:BoundField DataField="Quarter" HeaderText="季度"></asp:BoundField>
                                            <asp:BoundField DataField="Title" HeaderText="部门"></asp:BoundField>
                                            <asp:BoundField DataField="BudgetApply" HeaderText="年度预算总额"></asp:BoundField>
                                            <asp:BoundField DataField="TotalExpenditure" HeaderText="累计已开支"></asp:BoundField>
                                            <asp:BoundField DataField="NonPayment" HeaderText="未付款"></asp:BoundField>
                                            <asp:BoundField DataField="BudgetPermonth" HeaderText="预估数"></asp:BoundField>
                                            <asp:BoundField DataField="Total" HeaderText="合计数"></asp:BoundField>
                                            <asp:BoundField DataField="SurplusExpenditure" HeaderText="还可开支"></asp:BoundField>
                                            <asp:BoundField DataField="MakeTime" HeaderText="制作时间"></asp:BoundField>
                                            <%--<asp:BoundField DataField="StatusShow" HeaderText="是否已填写开支"></asp:BoundField>--%>
                                            <asp:ButtonField ButtonType="Image" Text="实际开支" ImageUrl="~/images/ICON/select.gif"
                                                HeaderText="实际开支" CommandName="approval"></asp:ButtonField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            没有季度预算信息
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
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="统计各类费用比例" ID="TabPanel2" Visible="false">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    请选择统计的起止年和起止季度
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    起始年份：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="请输入起始年份~A:int!" id="BeginYear" runat="server" />年
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    起始季度：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="BeginMonth" title="~A!" runat="server">
                                        <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">第一季</asp:ListItem>
                                        <asp:ListItem Value="2">第二季</asp:ListItem>
                                        <asp:ListItem Value="3">第三季</asp:ListItem>
                                        <asp:ListItem Value="4">第四季</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    截止年份：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="请输入截止年份~A:int!" id="EndYear" runat="server" />年
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    截止季度：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="EndMonth" title="~A!" runat="server">
                                        <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">第一季</asp:ListItem>
                                        <asp:ListItem Value="2">第二季</asp:ListItem>
                                        <asp:ListItem Value="3">第三季</asp:ListItem>
                                        <asp:ListItem Value="4">第四季</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="table_body table_body_NoWidth">
                                    部门：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                <input type="text" id="Title1" runat="server" />(默认为全部部门)
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="确定" class="button_bak" onmouseover="javascript:causeValidate = true;group='A';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Click" id="Sure" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" DisplayGroupTree="False" runat="server"
                                        AutoDataBind="true" DisplayToolbar="False" PrintMode="ActiveX" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="跟踪费用使用进度" ID="TabPanel3" Visible="false">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    请选择统计的起止年和起止季度
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    起始年份：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="请输入起始年份~B:int!" id="BeginYear2" runat="server" />
                                    年
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    起始季度：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="BeginMonth2" title="~B!" runat="server">
                                        <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">第一季</asp:ListItem>
                                        <asp:ListItem Value="2">第二季</asp:ListItem>
                                        <asp:ListItem Value="3">第三季</asp:ListItem>
                                        <asp:ListItem Value="4">第四季</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    截止年份：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="请输入截止年份~B:int!" id="EndYear2" runat="server" />
                                    年
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    截止季度：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="EndMonth2" title="~B!" runat="server">
                                        <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">第一季</asp:ListItem>
                                        <asp:ListItem Value="2">第二季</asp:ListItem>
                                        <asp:ListItem Value="3">第三季</asp:ListItem>
                                        <asp:ListItem Value="4">第四季</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    费用类型：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:TextBox title="请选择费用类型~B:!" onfocus='javascript:document.getElementById("CrystalView").style.display = "none";'
                                        ID="SubIDNametb" runat="server"></asp:TextBox>
                                    <asp:TextBox title="请选择费用类型~B:!" ID="SubIDtb" runat="server" Visible="False"></asp:TextBox>
                                    <asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                                        <div style="border: 1px outset white; width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="TreeView2" runat="server" onclick="javascript:causeValidate = false;"
                                                        OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                                    </asp:TreeView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </asp:Panel>
                                    <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="SubIDNametb"
                                        PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                        ExtenderControlID="">
                                    </cc2:PopupControlExtender>
                                    <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="SubIDtb"
                                        PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                        ExtenderControlID="">
                                    </cc2:PopupControlExtender>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="确定" class="button_bak" onmouseover="javascript:causeValidate = true;group='B';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure2_Click" id="Sure2" />
                                </td>
                            </tr>
                            <tr id="CrystalView">
                                <td colspan="4">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer2" DisplayGroupTree="False" runat="server"
                                        AutoDataBind="True" DisplayToolbar="False" PrintMode="ActiveX" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="统计实际开支明细" ID="TabPanel4">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    请选择统计的起止年和起止季度
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    起始年份：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="请输入起始年份~C:int!" id="StartYear4" runat="server" />年
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    起始季度：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="StartMonth4" title="~C!" runat="server">
                                        <asp:ListItem Value="12">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">第一季</asp:ListItem>
                                        <asp:ListItem Value="2">第二季</asp:ListItem>
                                        <asp:ListItem Value="3">第三季</asp:ListItem>
                                        <asp:ListItem Value="4">第四季</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    截止年份：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="请输入截止年份~C:int!" id="EndYear4" runat="server" />年
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    截止季度：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="EndMonth4" title="~C!" runat="server">
                                        <asp:ListItem Value="12">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">第一季</asp:ListItem>
                                        <asp:ListItem Value="2">第二季</asp:ListItem>
                                        <asp:ListItem Value="3">第三季</asp:ListItem>
                                        <asp:ListItem Value="4">第四季</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    部门：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input type="text" id="Title4" runat="server" />(默认为全部部门)
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    收款方：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input type="text" id="Supplier5" runat="server" />默认为所有收款方
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="确定" class="button_bak" onmouseover="javascript:causeValidate = true;group='C';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Click4" id="SureButton4" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr style="font-weight: bold" align="center">
                                            <td colspan="50">
                                                统计实际开支明细报表
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="HeadRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr style="font-weight: bold">
                                                    <td>
                                                        类别
                                                    </td>
                                                    <td>
                                                        项目
                                                    </td>
                                                    <td>
                                                        收款方
                                                    </td>
                                                    <td>
                                                        经办人
                                                    </td>
                                                    <asp:Repeater ID="AllTotalList" runat="server" DataSource='<%# Eval("Totallist") %>'>
                                                        <ItemTemplate>
                                                            <td>
                                                                <%# Eval("CompanyName")%>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td>
                                                        <%# Eval("ExpenditureName")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="StaticsBudgetDetail" runat="server">
                                            <ItemTemplate>
                                                <tr style='background-color: #efefef; display: <%# Convert.ToDecimal(Eval("RealExpenditure"))==0?"none":"block" %>'>
                                                    <td align="left" colspan="50">
                                                        <%# Container.ItemIndex+1%>、<%# Eval("SubName")%>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="Detail" runat="server" DataSource='<%# Eval("BudgetDetailList") %>'>
                                                    <ItemTemplate>
                                                        <tr style='background-color: #8f8f8f; display: <%# Convert.ToDecimal(Eval("RealExpenditure"))==0?"none":"block" %>''>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <%# Container.ItemIndex+1%>、<%# Eval("ExpenditureName")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Supplier")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Manager")%>
                                                            </td>
                                                            <asp:Repeater ID="DetailCompany" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <%# Eval("RealExpenditure")%>
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <td>
                                                                <%# Eval("RealExpenditure")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr style='background-color: #bfbfbf; display: <%# Convert.ToDecimal(Eval("RealExpenditure"))==0?"none":"block" %>''>
                                                    <td>
                                                        小计
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <asp:Repeater ID="DetailTotal" runat="server" DataSource='<%# Eval("Totallist") %>'>
                                                        <ItemTemplate>
                                                            <td>
                                                                <%# Eval("CompanyExpenditure")%>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td>
                                                        <%# Eval("RealExpenditure") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="TotalRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr style="background-color: Red">
                                                    <td>
                                                        总计
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <asp:Repeater ID="AllTotalList" runat="server" DataSource='<%# Eval("Totallist") %>'>
                                                        <ItemTemplate>
                                                            <td>
                                                                <%# Eval("CompanyExpenditure")%>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td>
                                                        <%# Eval("RealExpenditure") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="跟踪预算使用情况" ID="TabPanel5" Visible="false">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    请选择要跟踪预算的年份和月份
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    年份：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="请输入年份~D:int!" id="Year5" runat="server" />年
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    季度：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="Month5" title="~D!" runat="server">
                                        <asp:ListItem Value="12">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">第一季</asp:ListItem>
                                        <asp:ListItem Value="2">第二季</asp:ListItem>
                                        <asp:ListItem Value="3">第三季</asp:ListItem>
                                        <asp:ListItem Value="4">第四季</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    部门：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input type="text" id="Title5" title="请输入部门~D!" runat="server" />
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="确定" class="button_bak" onmouseover="javascript:causeValidate = true;group='D';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Click5" id="Sure_Button5" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr style="font-weight: bold" align="center">
                                <td colspan="50">
                                    预算费用使用进度报表
                                </td>
                            </tr>
                            <asp:Repeater ID="FirstHeadRepeater" runat="server">
                                <ItemTemplate>
                                    <tr style="font-weight: bold; background-color: #8f8f8f" align="center">
                                        <td>
                                            <%#Eval("SubName")%>
                                        </td>
                                        <asp:Repeater ID="FirstHeadRepeater2" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                            <ItemTemplate>
                                                <td colspan="3">
                                                    <%#Eval("CompanyName")%>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="SecondHeadRepeater" runat="server">
                                <ItemTemplate>
                                    <tr style="font-weight: bold" align="center">
                                        <td>
                                            <%#Eval("SubName")%>
                                        </td>
                                        <asp:Repeater ID="SecondHeadRepeater2" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                            <ItemTemplate>
                                                <td>
                                                    年度预算
                                                </td>
                                                <td>
                                                    累计开支
                                                </td>
                                                <td>
                                                    还可开支
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr style='background-color: <%# (Container.ItemIndex%2)==0?"#bfbfbf":"#efefef"%>'>
                                        <td>
                                            <%#Eval("SubName")%>
                                        </td>
                                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                            <ItemTemplate>
                                                <td>
                                                    <%#Eval("BudgetYear")%>
                                                </td>
                                                <td>
                                                    <%#Eval("HavePaid")%>
                                                </td>
                                                <td>
                                                    <%#Eval("LeftMoney")%>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        causeValidate = false;
    </script>

</asp:Content>

