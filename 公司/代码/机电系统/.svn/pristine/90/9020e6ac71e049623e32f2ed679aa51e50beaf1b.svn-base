<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeFile="MalfunctionRankSearch.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalFunctionClassify_MalfunctionRankSearch" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="查询" ID="TabPanel2">
                <HeaderTemplate>
                    查询
                </HeaderTemplate>
                <ContentTemplate>
                   <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    组合查询（支持模糊查询）
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    系统：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlSystem" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    子系统：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlSubsystem" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <cc2:CascadingDropDown ID="cddSystem" runat="server" TargetControlID="ddlSystem"
                                    Category="System" PromptText="请选择系统..." LoadingText="系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                    ServiceMethod="GetSystem" Enabled="True">
                                </cc2:CascadingDropDown>
                                <cc2:CascadingDropDown ID="cddSubSystem" runat="server" TargetControlID="ddlSubsystem"
                                    Category="Subsystem" PromptText="请选择子系统..." LoadingText="子系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                    ServiceMethod="GetSubsystem" ParentControlID="DDLSystem" Enabled="True">
                                </cc2:CascadingDropDown>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    故障对象：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbMalfunctionObject" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    响应类型：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlRank" runat="server">
                                        <%--                                      <asp:ListItem Value="0" Text="请选择响应类型"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="一般故障"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="重大故障"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="紧急故障"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="故障类别列表" ID="TabPanel1">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="GridView1_RowDataBound">
                        <EmptyDataTemplate>
                            暂时没有设定任何的故障类别
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SystemName" HeaderText="系统" />
                            <asp:BoundField DataField="SubSystemName" HeaderText="子系统" />
                            <asp:BoundField DataField="MalfunctionObject" HeaderText="故障对象" />
                            <asp:BoundField DataField="MalfunctionDescription" HeaderText="故障现象" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    故障类型</HeaderTemplate>
                                <ItemTemplate>
                                    <%#EnumHelper.GetDescription((Enum)Eval("Rank"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    响应时间</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("ResponseTime")%><%#EnumHelper.GetDescription((Enum)Eval("ResponseUnit"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    功能恢复时间</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("FunRestoreTime")%><%#EnumHelper.GetDescription((Enum)Eval("FunRestoreUnit"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    修复时间</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("RepairTime")%><%#EnumHelper.GetDescription((Enum)Eval("RepairUnit"))%>
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
                    <input id="hdRank" type="hidden" />
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <input id="ok" name="ok" type="button" class="button_bak" value="选择" onclick="javascript:onOK();" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <input id="cancel" name="cancel" type="button" class="button_bak" value="关闭" onclick="javascript:onCancel();" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>

    <script language="javascript" type="text/javascript">
        function onClientClick(selectedId,rank,responseTime,responseUnit,funTime,funUnit,repairTime,repairUnit) {
            //用隐藏控件记录下选中的行号
            document.getElementById("hdRank").value = rank + "|" + responseTime + "|" + responseUnit + "|" + funTime + "|" + funUnit + "|" + repairTime + "|" + repairUnit;
            var inputs = document.getElementById("<%=GridView1.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (inputs[i].id == selectedId)
                        inputs[i].checked = true;
                    else
                        inputs[i].checked = false;
                }
            }
        }
        function onOK() {
            window.returnVal = document.getElementById("hdRank").value;
            window.parent.hidePopWin(true);
        }

        function onCancel() {
            window.parent.hidePopWin(true);
        }
    </script>

</asp:Content>
