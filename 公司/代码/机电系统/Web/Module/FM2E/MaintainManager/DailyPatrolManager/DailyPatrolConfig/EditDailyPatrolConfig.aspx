<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="EditDailyPatrolConfig.aspx.cs" Inherits="Module_FM2E_MaintainManager_DailyPatrolManager_DailyPatrolConfig_EditDailyPatrolConfig"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="日常巡查计划信息维护"
        HeadOPTxt="目前操作功能：日常巡查计划维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="计划列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="DailyPatrolConfig.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="添加明细">
                <ContentTemplate>
                    <table width="880px" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLSystem" runat="server" title="请选择系统~!">
                                </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                子系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLSubsystem" runat="server" title="请选择子系统~!">
                                </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DDLSystem"
                                Category="System" PromptText="请选择系统..." LoadingText="系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                ServiceMethod="GetSystem" Enabled="True">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DDLSubsystem"
                                Category="Subsystem" PromptText="请选择子系统..." LoadingText="子系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                ServiceMethod="GetSubsystem" ParentControlID="DDLSystem" Enabled="True">
                            </cc2:CascadingDropDown>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                巡查周期：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TBPlanPeriod" runat="server" Width="25px" title="请输入数量~float!"></asp:TextBox><asp:DropDownList
                                    ID="DDLPeriodUnit" title="请选择单位~" runat="server">
                                </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                巡查对象：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TBPlanObject" runat="server" title="请输入巡查对象~50:!"></asp:TextBox><span
                                    style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 78px">
                                巡查内容：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 78px">
                                <textarea id="TextArea2" style="width: 723px; height: 67px" runat="server" title="请输入巡查内容~200:"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 78px">
                                验收标准：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 78px">
                                <textarea id="TextArea3" style="width: 723px; height: 67px" runat="server" title="请输入验收标准~200:"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td1" colspan="2" runat="server">
                                <asp:Label ID="LblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                            <td id="Td2" colspan="2" align="right" style="height: 38px" runat="server">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button_bak" Text="确定" OnClick="btnSubmit_Click" />
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
</asp:Content>
