<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManager_MonthlyBudgetManager_MakeMonthlyBudget, App_Web_r-i6s5nb" title="无标题页" %>

<%@ Register Src="../../../../control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="月度预算编制"
        HeadOPTxt="目前操作功能：月度预算编制">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="AddDetail" />
        </Triggers>
        <ContentTemplate>
            <table id="popuptable" runat="server"></table>
            <table  border="1">
                <tr align="center" >
                    <td colspan="7">
                        预算年份:<%--<input type="text" id="Year" readonly="readonly" runat="server" />--%><asp:Label
                            ID="Year" runat="server"></asp:Label>年&nbsp;&nbsp;&nbsp; 预算月份:<%--<input type="text" id="Month" readonly="readonly" runat="server" />--%><asp:Label
                                ID="Month" runat="server"></asp:Label>月&nbsp;&nbsp;&nbsp;预算部门:<asp:Label ID="INPTitle"
                                    runat="server"></asp:Label>
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
                        <div>
                            <asp:TreeView ID="TreeView1" runat="server" onclick="javascript:setslectrowcolor();"
                                OnTreeNodeCollapsed="TreeView1_OnTreeNodeCollapsed" OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded">
                                <NodeStyle VerticalPadding="1px" Height="16px" />
                            </asp:TreeView>
                            <div id="treeviewitemdiv" style="display: none" runat="server">
                            </div>
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
                        <input type="text" style="width: 100px;" id="TotalBudget" readonly="readonly" value='<%=(Session["TotalBudgetApply"]!=null)?Convert.ToDecimal(Session["TotalBudgetApply"]).ToString("0.##"):"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" style="width: 100px;" id="TotalTotalExpenditure" readonly="readonly"
                            value='<%=(Session["TotalTotalExpenditure"]!=null)?Convert.ToDecimal(Session["TotalTotalExpenditure"]).ToString("0.##"):"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" style="width: 100px;" id="TotalNonPayment" readonly="readonly"
                            value='<%=(Session["TotalNonPayment"]!=null)?Convert.ToDecimal(Session["TotalNonPayment"]).ToString("0.##"):"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" style="width: 100px;" id="TotalBudgetPermonth" readonly="readonly"
                            value='<%=(Session["TotalBudgetPermonth"]!=null)?Convert.ToDecimal(Session["TotalBudgetPermonth"]).ToString("0.##"):"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" style="width: 100px;" id="TotalTotal" readonly="readonly" value='<%=(Session["TotalTotal"]!=null)?Convert.ToDecimal(Session["TotalTotal"]).ToString("0.##"):"" %>' />
                    </td>
                    <td align="left">
                        <input type="text" style="width: 100px;" id="TotalSurplusExpenditure" readonly="readonly"
                            value='<%=(Session["TotalSurplusExpenditure"]!=null)?Convert.ToDecimal(Session["TotalSurplusExpenditure"]).ToString("0.##"):"" %>' />
                    </td>
                </tr>
                <%--<tr align="center">
                    <td colspan="7">
                    备注:<asp:TextBox ID="Remark" runat="server" Height="50px" Width="240px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr align="center">
                    <td colspan="7">
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="7">
                        <input id="tatics" type="button" value="统计" onmouseover="javascript:causeValidate = false;"
                            class="button_bak" runat="server" onserverclick="Tatics_Click" />
                        <asp:Button ID="sure" Text="提交" OnClick="Sure_Click" class="button_bak" runat="server"
                            onmouseover="javascript:causeValidate = false;" OnClientClick="javascript:return  confirm('确定提交?')" />
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
                                <asp:BoundField DataField="ExpenditureDetail" HeaderText="开支内容及依据"></asp:BoundField>
                                <asp:BoundField DataField="Manager" HeaderText="经办人"></asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        申请与审批金额
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# "申请额:"+Eval("ExpenditureStr")%><br />
                                        <%# "审批额:"+Eval("BudgetApproveStr")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="ExpenditureStr" HeaderText="申请与审批金额"></asp:BoundField>--%>
                                <asp:BoundField DataField="Review" HeaderText="审批意见"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="备注"></asp:BoundField>
                                <%--<asp:BoundField DataField="BudgetApproveStr" HeaderText="审批金额"></asp:BoundField>--%>
                                <asp:BoundField DataField="Approvaler" HeaderText="最近审批"></asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        修改
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                            CommandName="edititem" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        删除
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此开支明细信息吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                <table width="100%">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            请在下面填写本月添加的开支明细信息
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            费用类型
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <input type="text" id="SubIDNametb" readonly="readonly" title="请选择费用类型~:!" runat="server" />
                            <input type="text" id="SubIDtb" readonly="readonly" title="请选择费用类型~:!" runat="server"
                                style="display: none" />
                        </td>
                        <td class="table_body table_body_NoWidth">
                            开支项目名
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox title="请输入开支项目名称~30:!" ID="ExpenditureNametb" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            开支依据
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="ExpenditureDetailtb" runat="server">
                            </asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            备注
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="Remarkstb" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            依据附件
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:FileUpload ID="FileUpload1tb" runat="server" /><a id="a_attachement" runat="server"
                                style="color: Red"></a>
                            <input type="hidden" runat="server" id="inp_index" />
                            <asp:CheckBox Visible="false" ID="sendagain" runat="server" Text="重新上传" />
                        </td>
                        <td class="table_body table_body_NoWidth">
                            经办人
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TBManager" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            申请金额
                        </td>
                        <td colspan="3" class="table_none table_none_NoWidth">
                            <table width="100%" border="1">
                                <tr id="expenditure" runat="server">
                                </tr>
                                <%--<input type="hidden" id="edityet" runat="server" />--%>
                            </table>
                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="4">
                            <input type="button" runat="server" id="AddDetail" value="确定" class="button_bak"
                                onmouseout="javascript:causeValidate = false;" onmouseover="javascript:causeValidate = true;"
                                onserverclick="AddDetail_Click" />
                            <input id="canceledit" runat="server" value="取消" visible="false" style="display:none"  type="button" class="button_bak" onserverclick="CancelEdit_Click" />
                            <input id="cancelpopup" runat="server" value="关闭" type="button" class="button_bak" onserverclick="CancelEdit_Click" />
                            <input id="companycount" runat="server" type="hidden" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="popuptable"
                PopupControlID="Panel1" DynamicServicePath="" Enabled="True" Position="Bottom"
                ExtenderControlID="">
            </cc2:PopupControlExtender>
            <input type="button" id="popcontrolbutton" style="display:none;" value="Popup" onclick="document.getElementById('<%=popuptable.ClientID%>').focus();" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        causeValidate = false;
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
                if (list.item(i).title == event.srcElement.title && list.item(i).title != "") {
                    oldcolor = list.item(i).style.background;
                    list.item(i).style.background = "Yellow";
                }
                else
                    if (list.item(i).style.background == "yellow") {
                    list.item(i).style.background = oldcolor2;
                }
            }
            document.getElementById("<%=SubIDNametb.ClientID%>").value = event.srcElement.title;
            document.getElementById("<%=SubIDtb.ClientID%>").value = document.getElementById(event.srcElement.title + 'treeviewitem').value;
            document.getElementById("<%=popuptable.ClientID%>").focus();

        }
               
    </script>

</asp:Content>
