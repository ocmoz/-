<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewPosition.aspx.cs" Inherits="Module_FM2E_BasicData_PositionManage_ViewPosition"
    Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="目前操作功能：查看职位详情" HeadTitleTxt="职位信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div style="width: 900px; ">
         <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="职位详细信息" ID="TabPanel1">
                <ContentTemplate>
                <div align="left" style="width: 800px; text-align: left; vertical-align: top; padding: 0px 0px 0px 0px;">
                    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                        text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td style="width: 76px">
                                职位编号：
                            </td>
                            <td style="width: 693px">
                                <asp:Label ID="Label1" runat="server" Text="Label" Width="250px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 76px">
                                职位名称：
                            </td>
                            <td style="width: 693px">
                                <asp:Label ID="Label2" runat="server" Text="Label" Width="250px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 76px; text-align: left;">
                                职位描述：<br />
                                <br />
                            </td>
                            <td colspan="1" style="width: 693px">
                                <asp:Label ID="Label3" runat="server" Text="Label" Width="250px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
           </ContentTemplate>
           </cc2:TabPanel>
           </cc2:TabContainer>
    </div>
</asp:Content>
