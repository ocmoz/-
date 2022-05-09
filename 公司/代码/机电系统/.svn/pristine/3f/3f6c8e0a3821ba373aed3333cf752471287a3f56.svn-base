<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewExpendable.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ViewExpendable" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品信息维护"
        HeadOPTxt="目前操作功能：查看易耗品信息">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="易耗品详细信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 5px; border: solid 1px #a7c5e2;" border="1px">
                             <tr>
                                <td  class="table_body table_body_NoWidth" style="width: 15%">
                                    公司：
                                </td>
                                <td  class="table_none table_none_NoWidth" style="width: 35%">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                </td>
                            
                                <td  class="table_body table_body_NoWidth" style="width: 15%">
                                    仓库：
                                </td>
                                <td  class="table_none table_none_NoWidth" style="width: 35%">
                                    <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                </td>
                                 <td  class="table_none table_none_NoWidth" rowspan="5" style="vertical-align: top; width: 200px">
                                    <div style="width: 100%; text-align: center; vertical-align: middle;">
                                        <asp:Image ID="Image1" runat="server" Width="200px" AlternateText="No Picture" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td  class="table_body table_body_NoWidth" >
                                    名称：
                                </td>
                                <td  class="table_none table_none_NoWidth" >
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                               
                           
                                <td  class="table_body table_body_NoWidth" >
                                    型号品牌：
                                </td>
                                <td class="table_none table_none_NoWidth" >
                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            
                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                           
                            <tr>
                                <td class="table_body table_body_NoWidth" >
                                    库存量：
                                </td>
                                <td class="table_none table_none_NoWidth" >
                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                </td>
                           
                                <td class="table_body table_body_NoWidth" >
                                    单位：
                                </td>
                                <td class="table_none table_none_NoWidth" >
                                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" >
                                    最近更新时间：
                                </td>
                                <td  class="table_none table_none_NoWidth" colspan="3">
                                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" >
                                    备注：
                                </td>
                                <td  class="table_none table_none_NoWidth" colspan="3">
                                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
