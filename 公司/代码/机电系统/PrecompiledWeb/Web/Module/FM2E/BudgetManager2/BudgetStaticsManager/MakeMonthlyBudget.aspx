<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManager_BudgetStaticsManager_MakeMonthlyBudget, App_Web_p5s0fcs8" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
                        function refreshclick(ddd)
                        {
                        document.getElementById("<%=refresh.ClientID%>").click();
                        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="填写月度实际开支"
        HeadOPTxt="目前操作功能：填写月度实际开支">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="月度预算列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="MonthlyBudget.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="1">
                <tr align="center">
                    <td colspan="7">
                        预算年份<input type="text" size="4" id="Year" readonly="readonly" runat="server" />
                        预算月份<input type="text" size="2" id="Month" readonly="readonly" runat="server" />
                        预算部门<input type="text" id="INPTitle" readonly="readonly" runat="server" />
                    </td>
                </tr>
                <tr align="center">
                    <td align="center" style="font-weight: bold;">
                        费用项目
                    </td>
                    <td align="center" style="font-weight: bold;">
                        公司年度预算
                    </td>
                    <td align="center" style="font-weight: bold;">
                        累计已开支
                    </td>
                    <td align="center" style="font-weight: bold;">
                        未付款金额
                    </td>
                    <td align="center" style="font-weight: bold;">
                        月度预估数
                    </td>
                    <td align="center" style="font-weight: bold;">
                        合计数
                    </td>
                    <td align="center" style="font-weight: bold;">
                        还可开支
                    </td>
                </tr>
                <tr align="center">
                    <td align="left" valign="top">
                        <div runat="server" id="treediv">
                            <asp:TreeView ID="TreeView1" OnSelectedNodeChanged="TreeView1_OnSelectedNodeChanged"
                                runat="server" OnTreeNodeCollapsed="TreeView1_OnTreeNodeCollapsed" OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded">
                                <NodeStyle VerticalPadding="1px" Height="16px" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="width: 100px;">
                        <div id="BudgetApplTotalydiv" runat="server">
                        </div>
                    </td>
                    <td style="width: 100px;">
                        <div id="TotalExpenditurediv" runat="server">
                        </div>
                    </td>
                    <td style="width: 100px;">
                        <div id="NonPaymentdiv" runat="server">
                        </div>
                    </td>
                    <td style="width: 100px;">
                        <div id="BudgetPermonthdiv" runat="server">
                        </div>
                    </td>
                    <td style="width: 100px;">
                        <div id="Totaldiv" runat="server">
                        </div>
                    </td>
                    <td style="width: 100px;">
                        <div id="SurplusExpenditurediv" runat="server">
                        </div>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        合计:
                    </td>
                    <td align="left">
                        <input type="text" size="14" id="TotalBudget" readonly="readonly" value='<%=(Session["TotalBudgetApply"]!=null)?Session["TotalBudgetApply"]:"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" size="14" id="TotalTotalExpenditure" readonly="readonly" value='<%=(Session["TotalTotalExpenditure"]!=null)?Session["TotalTotalExpenditure"]:"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" size="14" id="TotalNonPayment" readonly="readonly" value='<%=(Session["TotalNonPayment"]!=null)?Session["TotalNonPayment"]:"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" size="14" id="TotalBudgetPermonth" readonly="readonly" value='<%=(Session["TotalBudgetPermonth"]!=null)?Session["TotalBudgetPermonth"]:"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" size="14" id="TotalTotal" readonly="readonly" value='<%=(Session["TotalTotal"]!=null)?Session["TotalTotal"]:"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" size="14" id="TotalSurplusExpenditure" readonly="readonly" value='<%=(Session["TotalSurplusExpenditure"]!=null)?Session["TotalSurplusExpenditure"]:"" %>' />
                    </td>
                </tr>
                <%--<tr align="center">
                    <td colspan="7">
                    备注:<asp:TextBox ID="Remark" runat="server" Height="50px" Width="240px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr align="center">
                    <td colspan="7">
                        <input id="tatics" type="button" value="统计" onmouseover="javascript:causeValidate = false;"
                            class="button_bak" runat="server" onserverclick="Tatics_Click" />
                        <input id="sure" type="button" value="提交" onmouseover="javascript:causeValidate = false;"
                            class="button_bak" runat="server" onserverclick="Sure_Click" />
                        <input id="saveastemp" visible="false" type="button" value="保存为草稿" onmouseover="javascript:causeValidate = false;"
                            class="button_bak" runat="server" onserverclick="SaveAsTemp_Click" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="sessionvalue" runat="server" />
            <table width="100%">
                
                <asp:Repeater ID="GridView1" runat="server">
                    <ItemTemplate>
                        <tr style='display: <%# Convert.ToDecimal(Eval("BudgetApprove"))==0?"none":"block"%>'>
                            <td>
                                <%# Eval("SubName")%>
                            </td>
                            <td>
                                <%# Eval("ExpenditureName")%>
                            </td>
                            <td>
                                <%# Eval("Manager")%>
                            </td>
                            <td>
                                <%# Eval("Expenditure")%>
                            </td>
                            <td>
                                <%# Eval("BudgetApprove")%>
                            </td>
                            
                            <td>
                                <%# Eval("Remarks")%>
                            </td>
                            <td>
                                <%# Eval("CompanyName")%>
                            </td>
                            <td>
                                <%# Eval("Approvaler")%>
                            </td>
                            <td>
                                <%# Eval("Supplier")%>
                            </td>
                            <td>
                                <%# Eval("RealExpenditure")%>
                            </td>
                            <td>
                                <%# DateTime.Compare(Convert.ToDateTime(Eval("PayDate")), DateTime.MinValue) == 0 ? "" : Convert.ToDateTime(Eval("PayDate")).ToString("yyyy-MM-dd")%>
                            </td>
                            <td>
                                <a href='<%# "javascript:showPopWin(\"请输入该项的实际开支\",\"EditDetail.aspx?index=" + Eval("DetailID") + "\", 900, 400, refreshclick,true,true);" %>'>
                                    填写开支 </a>
                            </td>
                        </tr>
                </ItemTemplate> 
                <HeaderTemplate>
                <tr style='background-color: #8f8f8f; '>
                    <td>
                        费用类型
                    </td>
                    <td>
                        开支项目
                    </td>
                    <td>
                        经办人
                    </td>
                    <td>
                        申请金额
                    </td>
                    <td>
                        审批金额
                    </td>
                    
                    <td>
                        备注
                    </td>
                    <td>
                        公司名
                    </td>
                    <td>
                        审批人
                    </td>
                    <td>
                        收款方
                    </td>
                    <td>
                        实际开支
                    </td>
                    <td>
                        收款日期
                    </td>
                    <td>
                    </td>
                </tr>
                </HeaderTemplate>
                </asp:Repeater>
                <%--<asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                    OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SubName" HeaderText="费用类型"></asp:BoundField>
                        <asp:BoundField DataField="ExpenditureName" HeaderText="开支项目"></asp:BoundField>
                        <asp:BoundField DataField="Manager" HeaderText="经办人"></asp:BoundField>
                        <asp:BoundField DataField="Expenditure" HeaderText="申请金额"></asp:BoundField>
                        <asp:BoundField DataField="BudgetApprove" HeaderText="审批金额"></asp:BoundField>
                        <asp:BoundField DataField="Review" HeaderText="审批意见"></asp:BoundField>
                        <asp:BoundField DataField="Remarks" HeaderText="备注"></asp:BoundField>
                        <asp:BoundField DataField="CompanyName" HeaderText="公司名"></asp:BoundField>
                        <asp:BoundField DataField="Approvaler" HeaderText="审批人"></asp:BoundField>
                        <asp:BoundField DataField="Supplier" HeaderText="收款方"/><asp:BoundField />
                        <asp:BoundField DataField="RealExpenditure" HeaderText="实际开支"></asp:BoundField>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='<%# "javascript:showPopWin(\"请输入该项的实际开支\",\"EditDetail.aspx?index=" + Eval("DetailID") + "\", 900, 400, refreshclick,true,true);" %>'>
                                    填写开支 </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>--%>
                <tr>
                    <td align="center" colspan="50">
                        <input type="button" class="locked3" value="显示所有开支" runat="server" id="showallexpenditure"
                            onserverclick="showallexpenditure_click" />
                    </td>
                </tr>
            </table>
            <input id="refresh" runat="server" type="button" style="display: none" value="true"
                onserverclick="GridviewRefresh" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function AddSession(id, inputvalue) {
            if (inputvalue != null) {
                if (inputvalue == "")
                    inputvalue = "0";
                if (isNaN(parseFloat(inputvalue)) || parseFloat(inputvalue) != inputvalue) {
                    alert("输入的金额不能包括其他符号");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                    return false;
                }
                document.getElementById("<%=sessionvalue.ClientID%>").value += id + "," + inputvalue + "|";
            }

        }
        var oldcolor = "";
        function setslectrowcolor(str) {
            var list = document.getElementsByTagName("input");
            var oldcolor2 = oldcolor;
            for (var i = 0; i < list.length; i++) {
                if (list.item(i).title == str && list.item(i).title != "")
                {
                    oldcolor = list.item(i).style.background;
                    list.item(i).style.background = "Yellow";
                    }
                else
                if( list.item(i).style.background == "yellow")
                {
                    list.item(i).style.background = oldcolor2;
                    }
            }
        }
    
    </script>

</asp:Content>
