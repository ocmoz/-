<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManager_AnnualBudgetManager_ViewAnnualBudget, App_Web_aielruvk" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="年度预算查阅"
        HeadOPTxt="目前操作功能：年度预算查阅">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="年度预算列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="AnnualBudget.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加年度预算" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="MakeAnnualBudget.aspx?cmd=add" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="select.gif" ButtonName="查看流程图" ButtonPopedom="List" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%">
                <table width="100%;">
                    <tr align="center">
                        <td colspan="<%=companycount%>" align="center">
                            预算年份:<input type="text" id="Year" runat="server" readonly="readonly" size="4" />
                            预算部门:<input type="text" id="IPUTTitle" runat="server" readonly="readonly" />
                        </td>
                    </tr>
                </table>
                <table id="theObjTable" style="table-layout: fixed" border="1">
                    <tr id="TR_company" runat="server" align="center">
                        <td align="center" style="width: 200px">
                            <label style="position: relative; cursor: e-resize;" onmousedown="MouseDownToResize(this);"
                                onmousemove="MouseMoveToResize(this);" onmouseup="MouseUpToResize(this);">
                                预算科目</label>
                        </td>
                    </tr>
                    <tr id="TR_content" runat="server" align="center">
                        <td style="width: 100px;" align="left" valign="top">
                            <div>
                                <asp:TreeView ID="TreeView1" runat="server" onclick="javascript:setslectrowcolor();" OnTreeNodeCollapsed="TreeView1_OnTreeNodeCollapsed"
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
                            备注:<input type="text" id="Remark" runat="server" size="100" />
                        </td>
                    </tr>
                    <%--<tr align="center">
                    <td colspan="50">
                        <input id="tatics" type="button" value="统计" class="button_bak" runat="server" onserverclick="Tatics_Click" />
                        <input id="sure" type="button" value="确定" class="button_bak" runat="server" onserverclick="Sure_Click" />
                        <input id="saveastemp" type="button" value="保存为草稿" class="button_bak" runat="server"
                            onserverclick="SaveAsTemp_Click" />
                    </td>
                </tr>--%>
                </table>
            </div>
            <input type="hidden" id="sessionvalue" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function AddSession(id, inputvalue) {
            if (inputvalue != null && inputvalue != "") {
                if (isNaN(parseFloat(inputvalue)) || parseFloat(inputvalue) != inputvalue) {
                    alert("输入的金额不能包括其他符号");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                    return false;
                }
                document.getElementById("<%=sessionvalue.ClientID%>").value += id + "&" + inputvalue + "|";
            }
        }
        function MouseDownToResize(obj) {
            obj.mouseDownX = event.clientX;
            obj.pareneTdW = obj.parentElement.offsetWidth;
            obj.pareneTableW = theObjTable.offsetWidth;
            obj.setCapture();
        }
        function MouseMoveToResize(obj) {
            if (!obj.mouseDownX) return false;
            var newWidth = obj.pareneTdW * 1 + event.clientX * 1 - obj.mouseDownX;
            if (newWidth > 0) {
                obj.parentElement.style.width = newWidth;
                theObjTable.style.width = obj.pareneTableW * 1 + event.clientX * 1 - obj.mouseDownX;
            }
        }
        function MouseUpToResize(obj) {
            obj.releaseCapture();
            obj.mouseDownX = 0;
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
