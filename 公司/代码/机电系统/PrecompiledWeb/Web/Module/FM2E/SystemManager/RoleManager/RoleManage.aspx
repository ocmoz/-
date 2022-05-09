<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SystemManager_RoleManager_RoleManage, App_Web_7tzibf8u" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="角色管理"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：查看角色">
        <cc1:HeadMenuButtonItem ButtonName="角色列表" ButtonIcon="list.gif" ButtonUrl="RoleList.aspx" ButtonPopedom="List" 
            ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="查看角色" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                             <tr id="roleIDTr" runat="server">
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    角色ID：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                 <td class="table_body table_body_NoWidth" style="height: 30px">
                                     角色名称：</td>
                                 <td class="table_none table_none_NoWidth" style="height: 30px">
                                     <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                     <asp:TextBox ID="TextBox1" runat="server" Columns="50" title="请输入角色名称~50:!"></asp:TextBox>
                                     <span style='color:Red;display:<%=(cmd=="view")?"none":"inline"%>'>*</span>
                                 </td>
                             </tr>
                             <tr>
                                 <td class="table_body table_body_NoWidth" style="height: 30px">
                                     角色介绍：</td>
                                 <td class="table_none table_none_NoWidth" style="height: 30px">
                                     <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                     <asp:TextBox ID="TextBox2" runat="server" Columns="50" title="请输入角色介绍~255:"></asp:TextBox>
                                 </td>
                             </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton" runat="server">
		<tr>
			<td align="right" style="height: 38px">
            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
            <input id="Reset1" class="button_bak" type="reset" value="重填" />
            </td>
        </tr>
    </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
