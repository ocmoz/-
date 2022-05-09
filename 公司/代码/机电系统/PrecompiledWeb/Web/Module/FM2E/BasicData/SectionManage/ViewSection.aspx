<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_SectionManage_ViewSection, App_Web_dkdufjdh" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="路段信息维护"
        HeadOPTxt="目前操作功能：查看路段详情">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="路段详细信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 800px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        &nbsp;
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td style="width: 150px">
                                    路段编号：
                                </td>
                                <td style="width: 450px">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                                <td rowspan="7" style="vertical-align: top; width: 200px">
                                    <div style="width: 100%; text-align: center; vertical-align: middle;">
                                        <asp:Image ID="Image1" runat="server" Width="200px" AlternateText="No Picture" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px">
                                    路段名称：
                                </td>
                                <td style="width: 450px">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px">
                                    路段所属公司：
                                </td>
                                <td style="width: 450px">
                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px">
                                    路段长度：
                                </td>
                                <td style="width: 450px">
                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px">
                                    路段启用时间：
                                </td>
                                <td style="width: 450px">
                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px">
                                    路段图片路径：
                                </td>
                                <td style="width: 450px">
                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left;">
                                    备注：<br />
                                    <br />
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
