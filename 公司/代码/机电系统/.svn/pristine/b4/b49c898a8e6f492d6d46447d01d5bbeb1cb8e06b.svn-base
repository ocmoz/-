<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EditMaintainItem.aspx.cs" Inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainItem_EditMaintainItem" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="维护项目管理"
        HeadOPTxt="目前操作功能：维护项目列表" HeadHelpTxt="请按照系统、子系统筛选维护项目">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="维护标准项列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="MaintainItem.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="UpdatePanel_Edit" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <input type="hidden" id="Hidden_CurrentAction" runat="server" />
                <input type="hidden" id="Hidden_EditID" runat="server" />
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            系统：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:DropDownList ID="DropDownList_EditSystem" runat="server" title="请选择系统~!">
                            </asp:DropDownList>
                            <span style="color: Red">*</span>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            子系统：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:DropDownList ID="DropDownList_EditSubSystem" runat="server" title="请选择子系统~!">
                            </asp:DropDownList>
                            <span style="color: Red">*</span>
                        </td>
                        <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DropDownList_EditSystem"
                            Category="System" PromptText="请选择系统..." LoadingText="系统加载中..." ServicePath="SystemSubsystemService.asmx"
                            ServiceMethod="GetSystem" Enabled="True">
                        </cc2:CascadingDropDown>
                        <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DropDownList_EditSubSystem"
                            Category="Subsystem" PromptText="请选择子系统..." LoadingText="子系统加载中..." ServicePath="SystemSubsystemService.asmx"
                            ServiceMethod="GetSubsystem" ParentControlID="DropDownList_EditSystem" Enabled="True">
                        </cc2:CascadingDropDown>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            类型：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:DropDownList ID="DropDownList_EditType" runat="server" title="请选择类型~">
                            </asp:DropDownList>
                            <span style="color: Red">*</span>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            巡查周期：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TBPlanPeriod" runat="server" Width="25px" title="请输入数量~float!" Text="1"></asp:TextBox><asp:DropDownList
                                ID="DDLPeriodUnit" title="请选择单位~" runat="server">
                            </asp:DropDownList>
                            <span style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            巡查对象：
                        </td>
                        <td class="table_none table_none_NoWidth"">
                            <asp:TextBox ID="TBPlanObject" runat="server" title="请输入巡查对象~50:!" Width="50%"></asp:TextBox><span
                                style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            巡查内容：
                        </td>
                        <td class="table_none table_none_NoWidth" colspan="3">
                            <textarea id="TextArea_Content" style="width: 100%; height: 50px" runat="server"
                                title="请输入巡查内容~200:"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            验收标准：
                        </td>
                        <td class="table_none table_none_NoWidth" colspan="3">
                            <textarea id="TextArea_Standard" style="width: 100%; height: 50px" runat="server"
                                title="请输入验收标准~200:"></textarea>
                        </td>
                    </tr>
                </table>
                <center>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button_bak" Text="添加" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

