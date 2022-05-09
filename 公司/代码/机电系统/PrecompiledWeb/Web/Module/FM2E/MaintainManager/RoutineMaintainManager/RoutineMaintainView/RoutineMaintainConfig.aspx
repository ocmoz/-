<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_MaintainManager_RoutineMaintainManager_RoutineMaintainView_RoutineMaintainConfig, App_Web_ux-0jlzx" title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="例行保养计划信息维护"
        HeadOPTxt="目前操作功能：例行保养计划信息维护" HeadHelpTxt="例行保养计划列表默认显示新添例行保养计划">
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="计划查询">
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
                                <asp:DropDownList ID="DDLSystem" runat="server" title="请选择系统~">
                                </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                子系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLSubsystem" runat="server" title="请选择子系统~">
                                </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                保养项目：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                <asp:DropDownList ID="DDLPlanObject" runat="server" title="请选择保养项目~">
                                </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DDLSystem"
                                Category="System" PromptText="请选择系统..." LoadingText="系统加载中..." ServicePath="SystemSubsystemMaintainObjectService.asmx"
                                ServiceMethod="GetSystem" Enabled="True">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DDLSubsystem"
                                Category="Subsystem" PromptText="请选择子系统..." LoadingText="子系统加载中..." ServicePath="SystemSubsystemMaintainObjectService.asmx"
                                ServiceMethod="GetSubsystem" ParentControlID="DDLSystem" Enabled="True">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="DDLPlanObject"
                                Category="MaintainObject" PromptText="请选择保养项目..." LoadingText="保养项目加载中..." ServicePath="SystemSubsystemMaintainObjectService.asmx"
                                ServiceMethod="GetRoutineMaintainObject" ParentControlID="DDLSubsystem" Enabled="True">
                            </cc2:CascadingDropDown>
                        </tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            开始时间：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TBMinTime" runat="server" title="请输入开始时间~!" class="input_calender"
                                onfocus="javascript:HS_setDate(this);"></asp:TextBox><span style="color: Red">*</span>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            结束时间：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TBMaxTime" runat="server" title="请输入结束时间~!" class="input_calender"
                                onfocus="javascript:HS_setDate(this);"></asp:TextBox><span style="color: Red">*</span>
                        </td>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" align="right" style="height: 38px" runat="server">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="例行保养计划列表" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            Width="100%" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CompanyName" HeaderText="公司"></asp:BoundField>
                                <asp:BoundField DataField="SystemName" HeaderText="系统"></asp:BoundField>
                                <asp:BoundField DataField="SubsystemName" HeaderText="子系统"></asp:BoundField>
                                <asp:BoundField DataField="PlanObject" HeaderText="保养项目"></asp:BoundField>
                                <asp:BoundField DataField="PlanPeriodString" HeaderText="保养周期"></asp:BoundField>
                                <asp:TemplateField HeaderText="查看">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/select.gif"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                没有计划信息
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
    <script type="text/javascript" language="javascript">
       function addtolist() {
       
        }
    </script>
</asp:Content>

