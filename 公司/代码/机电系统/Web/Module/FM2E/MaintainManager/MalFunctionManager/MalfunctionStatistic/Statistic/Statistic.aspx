<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Statistic.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionStatistic_Statistic_Statistic" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="故障报修统计"
        HeadOPTxt="目前操作功能：故障报修统计" HeadHelpTxt="默认统计本月的故障报修情况">
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="查询条件" ID="TabPanel1">
                <HeaderTemplate>
                    查询条件
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                查询条件
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
                                故障部门：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlDepartment" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                位置(默认全部位置)：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <input id="hdAddressID" runat="server" type="hidden" />
                                <asp:TextBox ID="tbAddress" runat="server" onclick="javascript:showPopWin('地址选择','../../../../BasicData/AddressManage/Address.aspx?operator=select',700, 400, RecordAddress,false,true);"
                                    Width="75%"></asp:TextBox>
                                <input class="button_bak" onclick="javascript:ClearAddress();" type="button" value="清空" />
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                维修单位：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlMaintainTeam" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                维修结果：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlMaintainStatus" runat="server">
                                    <asp:ListItem Text="不限" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="功能性修复" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="完全修复" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="未修复" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                故障处理人：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <input runat="server" id="MaintainStaff" type="text" />
                                <asp:DropDownList ID="DDLreporttype" runat="server">
                                    <asp:ListItem Text="按维护队" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="按人员" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                所属公司：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLCompany" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                故障记录部门：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlRecordDepartment" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                时间范围：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入开始时间~date"></asp:TextBox>至
                                <asp:TextBox ID="tbDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入结束时间~date"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr runat="server">
                            <td align="right" style="height: 38px" runat="server">
                                <asp:Button ID="btnSearch" runat="server" CssClass="button_bak" Text="查询" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="按维护队统计结果">
                <ContentTemplate>
                    <asp:Literal ID="ltStatisticResult" runat="server"></asp:Literal>
                    <br />
                    <div id="divSheets" runat="server">
                        <asp:Repeater ID="rptMalfunctionSheets" runat="server" OnItemDataBound="rptMalfunctionSheets_ItemDataBound">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            故障处理单
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            报障部门
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            故障记录部门
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            报障时间
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            维修单位
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            处理状态
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            是否超期
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="height: 30px; text-align: center">
                                        <asp:Literal ID="ltSheetNOTxt" runat="server" Text=""></asp:Literal>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("DepartmentName")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("RecordDeptName")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("ReportDate","{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("MaintainDeptName")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <asp:Label ID="lbisintime" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="7" class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                                        <asp:Button ID="btnExport" runat="server" CssClass="button_bak" Text="导出" OnClick="btnExport_Click">
                                        </asp:Button>
                                        <asp:Label ID="lbSheetCount" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:Label ID="lbErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="按人员统计结果">
                <ContentTemplate>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                     <br />
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer><script type="text/javascript" language="javascript">
                               function ClearAddress() {
                                   $get('<%=tbAddress.ClientID %>').value = '';
                                   $get('<%=hdAddressID.ClientID %>').value = '';
                               }
                               function RecordAddress(val) {
                                   //alert(val);
                                   var arr = new Array;
                                   arr = val.split('|');
                                   var addid = arr[0];
                                   var addcode = arr[1];
                                   var addname = arr[2];

                                   document.getElementById('<%= hdAddressID.ClientID %>').value = addcode;
                                   if (addcode != '00') {
                                       document.getElementById('<%=tbAddress.ClientID %>').value = addname;
                                   }
                                   else document.getElementById('<%=tbAddress.ClientID %>').value = "";
                               }
        </script>
</asp:Content>
