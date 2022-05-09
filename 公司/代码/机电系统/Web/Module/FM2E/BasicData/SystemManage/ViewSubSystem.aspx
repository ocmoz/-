<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewSubSystem.aspx.cs" Inherits="Module_FM2E_BasicData_SystemManage_ViewSubSystem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" 
        HeadHelpTxt="点击模块查看进入配置"
        HeadOPTxt="目前操作功能：子系统管理" HeadTitleTxt="系统划分">
    <cc1:HeadMenuButtonItem ButtonIcon="edit.gif"
        ButtonName="编辑" ButtonPopedom="Edit" />
    <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" 
        ButtonName="删除" ButtonPopedom="Delete"/>
    <cc1:HeadMenuButtonItem ButtonIcon="back.gif"
        ButtonName="返回" ButtonUrlType="javaScript"
        ButtonPopedom="List"  ButtonUrl="window.history.go(-1);"/>
     <cc1:HeadMenuButtonItem ButtonIcon="list.gif"
        ButtonName="系统列表" ButtonUrlType="Href"
        ButtonPopedom="List"  ButtonUrl="system.aspx"/>         
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel runat="server" HeaderText="子系统信息">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style=" border-collapse:collapse;">
                    <tr>
                        <td class="Table_searchtitle" colspan="3">子系统详细信息</td>
                    </tr>
                    <tr>
                        <td  class="table_body table_body_NoWidth" style="height: 30px">
                            子系统编码：</td>
                        <td style="width: 693px">
                            <asp:Label ID="Label1" runat="server" Text="Label" Width="250px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td  class="table_body table_body_NoWidth" style="height: 30px">
                            子系统名称：</td>
                        <td style="width: 693px">
                            <asp:Label ID="Label2" runat="server" Text="Label" Width="250px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td  class="table_body table_body_NoWidth" style="height: 30px">
                            父系统编码：</td>
                        <td style="width: 693px">
                            <asp:Label ID="Label3" runat="server" Text="Label" Width="250px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td  class="table_body table_body_NoWidth" style="height: 30px">
                            备注：</td>
                        <td style="width: 693px">
                            <asp:Label ID="Label4" runat="server" Text="Label" Width="250px"></asp:Label></td>
                    </tr>
                </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>   
    </div>
</asp:Content>

