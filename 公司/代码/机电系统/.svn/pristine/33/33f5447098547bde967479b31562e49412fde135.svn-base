<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeFile="EditClassify.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalFunctionClassify_EditClassify" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="故障类别设定" HeadOPTxt="新增故障类别" HeadHelpTxt="帮助">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="故障类别列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="MalfunctionClassify.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; height: 300px;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="新增故障类别" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                故障类别详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                 <asp:DropDownList ID="ddlSystem" runat="server" title="请选择系统~!">
                                    </asp:DropDownList><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                子系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlSubsystem" runat="server">
                                    </asp:DropDownList>
                            </td>
                            <cc2:CascadingDropDown ID="cddSystem" runat="server" TargetControlID="ddlSystem"
                                    Category="System" PromptText="请选择系统" LoadingText="系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                    ServiceMethod="GetSystem" Enabled="True">
                                </cc2:CascadingDropDown>
                                <cc2:CascadingDropDown ID="cddSubSystem" runat="server" TargetControlID="ddlSubsystem"
                                    Category="Subsystem" PromptText="请选择子系统" LoadingText="子系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                    ServiceMethod="GetSubsystem" ParentControlID="DDLSystem" Enabled="True">
                                </cc2:CascadingDropDown>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                故障对象：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:TextBox ID="tbMalfunctionObject" runat="server" title="请输入故障对象~50:!" MaxLength="50"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                           
                        </tr>
                        <tr>
                             <td class="table_body table_body_NoWidth" style="height: 30px">
                                故障现象描述：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:TextBox ID="tbMalfunctionDescription" runat="server" title="请输入故障现象描述~100:" MaxLength="100" TextMode="MultiLine" Width="95%" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                响应类型：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="ddlRank" runat="server" title="请选择响应类型~">
                                    </asp:DropDownList><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                响应时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="tbResponseTime" runat="server" title="请输入响应时间~int!" MaxLength="20" Width="40px"></asp:TextBox>
                                <asp:DropDownList ID="ddlResonseTime" runat="server">
                                </asp:DropDownList>
                                <span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                功能恢复时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="tbFunRestoreTime" runat="server" title="请输入功能恢复时间~int!" MaxLength="20" Width="40px"></asp:TextBox>
                                 <asp:DropDownList ID="ddlFunTime" runat="server">
                                </asp:DropDownList>
                                <span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                修复时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="tbRepairTime" runat="server" title="请输入修复时间~int!" MaxLength="20" Width="40px"></asp:TextBox>
                                 <asp:DropDownList ID="ddlRepairTime" runat="server">
                                </asp:DropDownList>
                                <span style="color:Red">*</span>
                            </td>
                            
                        </tr>
                        
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                        <tr>
                         <td align="left"><asp:Label ID="errMsg" ForeColor="Red" runat="server"></asp:Label></td>
                            <td align="right" style="height: 38px">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="继续添加" OnClick="Button2_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
