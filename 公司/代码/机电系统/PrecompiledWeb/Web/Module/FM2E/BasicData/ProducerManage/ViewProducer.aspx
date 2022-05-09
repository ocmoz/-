<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_ProducerManage_ViewProducer, App_Web_xyozrjue" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="目前操作功能：查看生产商明细" HeadTitleTxt="生产商信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript" ButtonPopedom="List" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="生产商详细信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 800px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        &nbsp;
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td style="width: 87px">
                                    生产商号：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label1" runat="server" Text="Label" Width="441px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    生产商名称：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label2" runat="server" Text="Label" Width="441px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    信用等级：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label12" runat="server" Text="Label" Width="441px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    地址：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label3" runat="server" Text="Label" Width="439px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    电话：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label4" runat="server" Text="Label" Width="437px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    传真：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label5" runat="server" Text="Label" Width="437px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    Email：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label6" runat="server" Text="Label" Width="435px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    主页：
                                </td>
                                <td style="width: 512px">
                                    <asp:Label ID="Label7" runat="server" Text="Label" Width="445px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px; text-align: left">
                                    联系人：
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label8" runat="server" Text="Label" Width="447px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px; text-align: left">
                                    联系人电话：
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label9" runat="server" Text="Label" Width="445px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px; height: 62px; text-align: left">
                                    产品：
                                </td>
                                <td colspan="2" style="height: 62px">
                                    <asp:Label ID="Label10" runat="server" Height="54px" Text="Label" Width="445px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px; text-align: left;">
                                    备注：<br />
                                    <br />
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label11" runat="server" Height="50px" Text="Label" Width="441px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
