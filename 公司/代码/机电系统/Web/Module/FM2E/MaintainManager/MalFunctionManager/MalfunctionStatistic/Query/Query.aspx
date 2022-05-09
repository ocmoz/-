<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Query.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionStatistic_Query_Query" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="维修记录查询"
        HeadOPTxt="目前操作功能：维修记录查询" HeadHelpTxt="默认查询本月的维修记录">
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    <div style="float: left; width: 20%;">
                    </div>
                    <div style="float: left; width: 60%;">
                        查询条件</div>
                    <div style="float: left; width: 20%; text-align: right;">
                        <span id="CloseSpan" style="cursor: pointer; color: Black" onclick="javascript:CollapseOrExpand();">
                            --折叠</span>&nbsp;&nbsp;&nbsp;</div>
                </td>
            </tr>
            <tr id="tr1">
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    故障处理单号：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:TextBox ID="tbSheetNO" runat="server" ></asp:TextBox>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    故障部门：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="ddlDepartment" runat="server" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr2">
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    故障原因：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="ddlMalReason" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="table_body table_body_NoWidth">
                    故障设备条码：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEquipmentName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr3">
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    系统：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="ddlSystem" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    故障等级：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="ddlRank" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr4">
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    维修单位：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="ddlMaintainTeam" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    时间范围：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:TextBox ID="tbDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                        title="请输入开始时间~date"></asp:TextBox>
                    至
                    <asp:TextBox ID="tbDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                        title="请输入结束时间~date"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr6">
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    是否申请计量：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="ddlMalMeasure" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="table_body table_body_NoWidth">
                    故障设备名称：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr  id="tr7">
                <td class="table_body table_body_NoWidth">
                    故障设备品牌：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbSpecification" runat="server"></asp:TextBox>
                </td>
                <td class="table_body table_body_NoWidth">
                    故障设备型号：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbModel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr8">
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    是否计量：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="jiliangOrNot" runat="server">
                    <asp:ListItem Value="0">不限</asp:ListItem>
                    
                    <asp:ListItem Value="是">是</asp:ListItem>
                    <asp:ListItem Value="否">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="table_body table_body_NoWidth">
                    是否甲供：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:DropDownList ID="jiagongOrNot" runat="server">
                    <asp:ListItem Value="0">不限</asp:ListItem>
                    
                    <asp:ListItem Value ="是">是</asp:ListItem>
                    <asp:ListItem Value ="否">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth">
                    表单状态：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:DropDownList ID="ddlFilterStatus" runat="server">
                            </asp:DropDownList>
                </td>
                <%-- <td class="table_body table_body_NoWidth" style="height: 30px">
                    确认修复时间范围：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:TextBox ID="tbConfirmTimeFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                        title="请输入开始时间~date"></asp:TextBox>
                    至
                    <asp:TextBox ID="tbConfirmTimeTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                        title="请输入结束时间~date"></asp:TextBox>
                </td>--%>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="tr5">
            <tr>
                <td align="right" style="height: 38px">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button_bak" Text="查询" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%;">
<%--        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="维修记录" ID="TabPanel1">
                <ContentTemplate>--%>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False">
                        <EmptyDataTemplate>
                            没有符合条件的维修记录
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <Columns>
                            <%--<asp:BoundField DataField="SheetNO" HeaderText="故障处理单号" />--%>
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    故障处理单号</HeaderTemplate>
                                <ItemStyle/>
                                <ItemTemplate>
                                    <a style="color: Blue" href="javascript:showPopWin('查看故障单','../../MalfunctionReport/ViewMalfunctionSheet.aspx?id=<%# Eval("SheetID")%>&viewOnly=1',800, 430, null,true,true);"><%# Eval("SheetNO")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码" />--%>
                           <%-- <asp:BoundField DataField="EquipmentName" HeaderText="设备名称" />--%>
                            <asp:BoundField DataField="DepartmentName" HeaderText="报障部门" />
                            <asp:BoundField DataField="ReportDate" HeaderText="报修时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="RecorderName" HeaderText="记录人" />
                            <asp:BoundField DataField="MaintainDeptName" HeaderText="维修单位" />
                            <asp:BoundField DataField="AddressName" HeaderText="故障地址" />
                            <%--<asp:BoundField DataField="MaintainDate" HeaderText="维修时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />--%>
                            <%--<asp:BoundField DataField="MaintainFee" HeaderText="维修费用（元）" DataFormatString="{0:#,0.#}" />--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    处理状态</HeaderTemplate>
                                <ItemTemplate>
                                    <%#EnumHelper.GetDescription((Enum)Eval("Status"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    最后审批人</HeaderTemplate>
                                <ItemTemplate>
                                    <%#lostname(Convert.ToString(Eval("Editreason")))%>
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
<%--                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>--%>
    </div>

    <script language="javascript" type="text/javascript">
        function CollapseOrExpand() {
            var obj = $get("CloseSpan");
            if (obj == null)
                return;

            if (obj.innerText == "--折叠") {
                $get("tr1").style.display = "none";
                $get("tr2").style.display = "none";
                $get("tr3").style.display = "none";
                $get("tr4").style.display = "none";
                $get("tr5").style.display = "none";
                $get("tr6").style.display = "none";
                $get("tr7").style.display = "none";
                $get("tr8").style.display = "none";
                obj.innerText = "+展开";
            } else if (obj.innerText == "+展开") {
                $get("tr1").style.display = "inline";
                $get("tr2").style.display = "inline";
                $get("tr3").style.display = "inline";
                $get("tr4").style.display = "inline";
                $get("tr5").style.display = "inline";
                $get("tr6").style.display = "inline";
                $get("tr7").style.display = "inline";
                $get("tr8").style.display = "inline";
                obj.innerText = "--折叠";
            }
        }
    </script>
    <div style="text-align:center; padding:5px;">
        <asp:Button ID="btExport" runat="server" Text="导 出" CssClass="button_bak" 
            onclick="btExport_Click" />
    </div>
</asp:Content>
