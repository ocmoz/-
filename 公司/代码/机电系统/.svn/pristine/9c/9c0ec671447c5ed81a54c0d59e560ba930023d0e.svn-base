<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditSubSystem.aspx.cs" Inherits="Module_FM2E_BasicData_SystemManage_EditSubSystem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="系统划分"
        HeadHelpTxt="点击模块查看进入配置" HeadOPTxt="目前操作功能：子系统管理">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel runat="server" HeaderText="子系统信息" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="3">
                                系统详细信息
                            </td>
                        </tr>
                        <tr id="SystemCode" runat="server">
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                子系统编码：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="2" style="height: 30px">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                子系统名称：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="2" style="height: 30px">
                                <asp:TextBox ID="TextBox2" runat="server" title="请输入系统名称~20:!" MaxLength="20"></asp:TextBox>
                                <span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                父系统编码：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="2" style="height: 30px">
                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 112px">
                                系统描述：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="2" style="height: 112px">
                                <asp:TextBox ID="TextBox4" title="请输入备注~1000:" runat="server" Height="87px" TextMode="MultiLine"
                                    Width="723px" MaxLength="1000"></asp:TextBox>
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
    </div>
</asp:Content>
