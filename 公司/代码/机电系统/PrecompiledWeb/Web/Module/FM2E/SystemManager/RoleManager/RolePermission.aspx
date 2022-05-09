<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SystemManager_RoleManager_RolePermission, App_Web_7tzibf8u" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="角色管理"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：配置角色权限">
        <cc1:HeadMenuButtonItem ButtonName="角色资料" ButtonIcon="look.gif" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 900px; ">
                <div style="float: left; width: 80%">
                    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc2:TabPanel runat="server" HeaderText="配置角色权限" ID="TabPanel1">
                            <HeaderTemplate>
                                配置角色权限</HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;">
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            角色名称：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                                        <tr class="table_Title">
                                            <td colspan="9">
                                                <%=ModuleName %>
                                            </td>
                                        </tr>
                                        <tr class="table_body" align="center">
                                            <td width="25%" align="left">
                                                栏目名称
                                            </td>
                                            <td>
                                                查看&nbsp;
                                            </td>
                                            <td>
                                                新增&nbsp;
                                            </td>
                                            <td>
                                                修改&nbsp;
                                            </td>
                                            <td>
                                                删除&nbsp;
                                            </td>
                                            <td>
                                                打印&nbsp;
                                            </td>
                                            <td>
                                                审批&nbsp;
                                            </td>
                                            <td>
                                                权限A
                                            </td>
                                            <td>
                                                权限B
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="ModuleSub" runat="server" OnItemDataBound="Module_Sub_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="table_none" align="center">
                                                    <td align="left">
                                                        <input id="SelectButton" style="display: <%=(cmd=="edit"?"inline":"none")%>" type="button"
                                                            value="全选" onclick='selectAll(this,"<%#Eval("ModuleID")%>");' /><%# Eval("Name")%>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="Lab2_Txt" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="Lab4_Txt"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="Lab8_Txt"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="Lab16_Txt"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="Lab32_Txt"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="Lab64_Txt"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="Lab128_Txt"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="Lab256_Txt"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr style="display: <%=(cmd=="edit"?"inline":"none")%>">
                                            <td id="Td1" colspan="9" align="right" runat="server">
                                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" /><input
                                                    class="button_bak" type="button" value="重填" onclick="javascript:ClearSelect();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </cc2:TabPanel>
                    </cc2:TabContainer>
                </div>
                <div style="float: left; width: 20%; position: relative; top: 20px">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="1px">
                        <asp:TreeView ID="moduleTree" runat="server" ShowLines="true" OnSelectedNodeChanged="moduleTree_SelectedNodeChanged"
                            OnTreeNodePopulate="moduleTree_TreeNodePopulate">
                            <SelectedNodeStyle ForeColor="#FF5050" />
                        </asp:TreeView>
                    </asp:Panel>
                </div>
                <input id="Mode" type="hidden" runat="server" value="view" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" Style="display: none;" />

    <script language="javascript" type="text/javascript">

        function selectAll(obj, id) {
            var inputs = document.getElementsByName("PageCode" + id);

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.value == "全选")
                        inputs[i].checked = true;
                    else inputs[i].checked = false;
                }
            }
            if (obj.value == "全选")
                obj.value = "反选";
            else obj.value = "全选";
        }

        function ClearSelect() {
            var inputs = document.getElementById("<%=TabPanel1.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    inputs[i].checked = false;

                }
            }
        }

        function ChangeToEditMode() {
            //alert("aa");
            document.getElementById("<%= Mode.ClientID %>").value = "edit";
            __doPostBack("<%= Button2.ClientID %>", "");
        }

        function ChangeToViewMode() {
            //alert("bb");
            document.getElementById("<%= Mode.ClientID %>").value = "view";
            __doPostBack("<%= Button2.ClientID %>", "");
        }
    </script>

</asp:Content>
