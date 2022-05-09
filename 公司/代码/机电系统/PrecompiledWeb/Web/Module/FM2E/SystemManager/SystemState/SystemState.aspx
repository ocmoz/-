<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SystemManager_SystemState_SystemState, App_Web_ccmw64as" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="系统运行状态"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：系统运行状态">
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="系统信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr id="roleIDTr" runat="server">
                                <td class="table_body table_body_NoWidth" style="height: 30px" runat="server">
                                    系统版本：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" runat="server">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    当前在线用户：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    当前使用缓存数：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    可用缓存大小：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    服务器操作系统：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    服务器运行时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    应用运行时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    物理内存使用：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    虚拟内存使用：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="系统维护">
                <ContentTemplate>
                    <div style="width: 880px; text-align: left; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr class="table_Title">
                                <td>
                                    缓存清空
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    执行此操作，所有的Web缓存将会清空。数据将重新从数据库中读取<br />
                                    <asp:Button ID="Button1" class="button_bak" runat="server" Text="清空" 
                                        onclick="Button1_Click" />
                                </td>
                            </tr>
                            <tr class="table_Title">
                                <td>
                                    重启Web应用程序
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    你可以进行强制关闭并重启Web应用程序。你需要对重启动作确认。Web应用程序将在第一次请求后重启。<br />
                                    <b>警告：</b>所有的会话都将丢失，用户操作可能会出错。<br />
                                    <b>提示：</b>结果只影响到本Web应用程序。<br />
                                    <asp:Button ID="Button2" class="button_bak" runat="server" Text="重启" 
                                        onclick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
