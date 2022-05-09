<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManager_MonthlyBudgetApprovalManager_MakeMonthlyBudget, App_Web_8vdrp-xb" title="无标题页" %>

<%@ Register Src="../../../../control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function refreshclick(ddd) {
            document.getElementById("<%=refresh.ClientID%>").click();
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="月度预算审批"
        HeadOPTxt="目前操作功能：月度预算审批">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="月度预算审批" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="MonthlyBudget.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 950px;">
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
                                <asp:TreeView ID="TreeView1"  onclick="javascript:setslectrowcolor();" runat="server" OnTreeNodeCollapsed="TreeView1_OnTreeNodeCollapsed"
                                    OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded">
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
                        <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
                    </td>
                </tr>
                    <tr align="center">
                        <td colspan="7">
                            <input id="tatics" type="button" value="统计" onmouseover="javascript:causeValidate = false;"
                                class="button_bak" runat="server" onserverclick="Tatics_Click" />
                                <asp:Button id="sure" Text="提交" onclick="Sure_Click"
                            class="button_bak" runat="server" OnClientClick= "javascript:return  confirm('确定提交?')"/>
                            <%--<input id="sure" type="button" value="提交"  onclick="javascript:return confirm('确定提交?');" onmouseover="javascript:causeValidate = false;"
                                class="button_bak" runat="server" onserverclick="Sure_Click" />--%>
                            <%--<input id="saveastemp" type="button" value="保存为草稿" onmouseover="javascript:causeValidate = false;"
                                class="button_bak" runat="server" onserverclick="SaveAsTemp_Click" />--%>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="sessionvalue" runat="server" />
                <table width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="SubName" HeaderText="费用类型"></asp:BoundField>
                                    <asp:BoundField DataField="ExpenditureName" HeaderText="开支项目"></asp:BoundField>
                                    <asp:TemplateField>
                                    <HeaderTemplate>
                                        申请与审批金额
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <%# "申请额:"+Eval("ExpenditureStr")%><br /><%# "审批额:"+Eval("BudgetApproveStr")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <%--<asp:BoundField DataField="ExpenditureStr" HeaderText="申请金额"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Review" HeaderText="审批意见"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="备注"></asp:BoundField>
                                    <%--<asp:BoundField DataField="BudgetApproveStr" HeaderText="审批金额"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Approvaler" HeaderText="上次审批"></asp:BoundField>
                                    <%--<asp:ButtonField ButtonType="Image" Text="审批" ImageUrl="~/images/ICON/Approval.gif"
                                    HeaderText="审批" CommandName="approval"></asp:ButtonField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href='<%# "javascript:showPopWin(\"请输入你审批的金额和意见\",\"EditDetail.aspx?index=" + Container.DataItemIndex + "&yearid="+YearID +"\", 900, 400, refreshclick,true,true);" %>'>
                                                审批 </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <input id="companycount" runat="server" type="hidden" />
                <input id="refresh" runat="server" type="button" style="display: none" onserverclick="GridviewRefresh" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>

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
        function setslectrowcolor() {
            var list = document.getElementsByTagName("input");
            var oldcolor2 = oldcolor;

            for (var i = 0; i < list.length; i++) {
                if (list.item(i).title == event.srcElement.title && list.item(i).title != "")
                {
                    oldcolor = list.item(i).style.background;
                    list.item(i).style.background = "Yellow";
                    }
                else
                    if(list.item(i).style.background == "yellow")
                    {
                    list.item(i).style.background = oldcolor2;
                    }
            }
        }
    
    </script>

</asp:Content>
