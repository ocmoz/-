<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" 
EnableEventValidation="false" CodeFile="ViewRoutineMaintainConfig.aspx.cs" Inherits="Module_FM2E_MaintainManager_RoutineMaintainManager_RoutineMaintainConfig_ViewRoutineMaintainConfig" Title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="例行保养计划信息维护"
        HeadOPTxt="目前操作功能：查看例行保养计划明细详情">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="配置设备" ButtonPopedom="PermissionA" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="例行保养计划明细信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    系统：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    子系统：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    保养周期：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    保养对象：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    保养内容：
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    验收标准：
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
