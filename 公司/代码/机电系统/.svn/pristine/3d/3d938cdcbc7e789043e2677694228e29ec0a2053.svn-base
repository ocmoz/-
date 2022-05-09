<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MalfunctionList.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionConfirm_MalfunctionList" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="故障处理满意度调查"
        HeadHelpTxt="默认显示待进行满意度调查的故障处理单" HeadOPTxt="目前操作功能：故障处理单列表">
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
            <asp:Timer ID="Timer_Refresh" runat="server" OnTick="Timer_Refresh_Tick">
        </asp:Timer>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer_Refresh" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="故障处理单列表" ID="TabPanel1">
                <ContentTemplate>
                 <table width="100%">
                    <tr>
                    <td>报障部门：<asp:DropDownList ID="ddlFilterDepartment" runat="server">
                        </asp:DropDownList></td>
                    <td>所属系统：<asp:DropDownList ID = "ddlSystem" runat="Server" ></asp:DropDownList></td>
                    <td>维修单位：<asp:DropDownList ID="ddlFilterMaintainTeam" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>故障等级：<asp:DropDownList ID="ddlFilterRank" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td><asp:Button ID="btFilter" runat="server" Text="GO" OnClick="btFilter_Click" /></td>
                    <td style="width:15%">&nbsp;</td>
                    </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        <EmptyDataTemplate>
                            没有任何未进行满意度调查的故障处理单
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="SheetNO" HeaderText="故障单编号" />
          <%--                  <asp:BoundField DataField="EquipmentName" HeaderText="故障设备" />--%>
                            <asp:BoundField DataField="DepartmentName" HeaderText="报障部门" />
                            <asp:BoundField DataField="ReportDate" HeaderText="报修时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="RecorderName" HeaderText="记录人" />
                            <asp:BoundField DataField="MaintainDeptName" HeaderText="维修单位" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    处理状态</HeaderTemplate>
                                <ItemTemplate>
                                  <%#EnumHelper.GetDescription((Enum)Eval("Status")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/Approval.gif" HeaderText="确认"
                                CommandName="view">
                                <HeaderStyle Width="60px" />
                            </asp:ButtonField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                        InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="{0}"
                        PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="高级查询" ID="TabPanel2">
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
                                故障处理单号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbSheetNO" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                报障部门：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlDepartment" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                报修人：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbReporter" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                报修时间：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbReportTimeFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入报修时间~date"></asp:TextBox>&nbsp;至&nbsp;<asp:TextBox ID="tbReportTimeTo"
                                        runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入报修时间~date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                故障记录人：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbRecorderName" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                维修单位：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlMaintainTeam" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
                </ContentTemplate>
        </asp:UpdatePanel>
    </div>
        <script language="javascript" type="text/javascript">
        //截获回车事件
        function document.onkeydown() {
            var tagName = event.srcElement.tagName.toUpperCase();
            if (event.keyCode == 13) {
                $get('<%=btFilter.ClientID %>').click();
            }
        }
    </script>
</asp:Content>

