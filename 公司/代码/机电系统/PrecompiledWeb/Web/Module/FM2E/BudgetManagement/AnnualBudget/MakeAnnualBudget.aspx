<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManagement_AnnualBudget_MakeAnnualBudget, App_Web_kize7vh6" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../../../control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="年度预算编制"
        HeadOPTxt="目前操作功能：年度预算编制">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div  style="width:100%;height:640px;overflow-x:auto;overflow-y:auto;">
                <table border="1">
                    <tr align="center">
                        <td colspan="<%=companycount%>" align="center">
                            预算年份:<input type="text" id="Year" runat="server" size="4" title="请输入所要编制预算的年份~:int" />
                            预算部门:<input type="text" id="IPUTTitle" runat="server" title="请输入所要编制预算的部门~50:!" />
                        </td>
                    </tr>
                    <tr id="TR_company" runat="server" align="center">
                        <td align="center">
                            预算科目
                        </td>
                    </tr>
                    <tr id="TR_content" runat="server" align="center">
                        <td align="left" valign="top">
                            <div>
                                <asp:TreeView ID="TreeView1" runat="server" onclick="javascript:setslectrowcolor();"  OnTreeNodeCollapsed="TreeView1_OnTreeNodeCollapsed"
                                    OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded">
                                    <NodeStyle VerticalPadding="1px" Height="16px" />
                                </asp:TreeView>
                            </div>
                        </td>
                    </tr>
                    <tr id="TR_total" runat="server" align="center">
                        <td>
                            合计:
                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="<%=companycount%>">
                            备注:<input type="text" id="Remark" runat="server" size="100"/>
                    </tr>
                    <%--<tr align="center">
                        <td colspan="<%=companycount%>">
                            <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
                        </td>
                    </tr>--%>
                    <tr align="center">
                        <td colspan="<%=companycount%>">
                            <input id="tatics" type="button" value="统计" class="button_bak" runat="server" onserverclick="Tatics_Click" />
                            <asp:Button id="sure" Text="提交" onclick="Sure_Click"
                            class="button_bak" runat="server" OnClientClick= "javascript:return  confirm('确定提交?')"/>
                            <%--<input id="sure" type="button" value="确定"  onclick="javascript:return confirm('确定提交?');" class="button_bak" runat="server" onserverclick="Sure_Click" />--%>
                            <%--<input id="saveastemp" type="button" value="保存为草稿" class="button_bak" runat="server"
                            onserverclick="SaveAsTemp_Click" />--%>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="sessionvalue" runat="server" />
            </div>
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
                document.getElementById("<%=sessionvalue.ClientID%>").value += id + "&" + inputvalue + "|";
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
