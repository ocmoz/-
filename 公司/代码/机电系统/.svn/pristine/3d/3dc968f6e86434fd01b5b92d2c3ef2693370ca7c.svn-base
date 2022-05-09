<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Record.aspx.cs" Inherits="Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapRecord_Record"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备报废登记"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：报废登记">
        <cc1:HeadMenuButtonItem ButtonName="返回" ButtonIcon="back.gif" ButtonPopedom="List"
            ButtonUrl="ScrapRecord.aspx" ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="报废登记" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td style="width: 20%">
                                    报废单编号：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lbSheetName" runat="server" Text="Label"></asp:Label>
                                    &nbsp;
                                </td>
                                <td style="width: 20%">
                                    公司：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lbCompany" runat="server" Text="Label"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    部门：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lbDep" runat="server" Text="Label"></asp:Label>
                                    &nbsp;
                                </td>
                                <td style="width: 20%">
                                    申请人：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lbApplicant" runat="server" Text="Label"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    报废设备：
                                </td>
                                <td style="width: 30%" colspan="3">
                                    <asp:Label ID="lbEquipment" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    报废原因：
                                </td>
                                <td style="width: 30%" colspan="3">
                                    <asp:Label ID="lbReason" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <td style="width: 20%">
                                备注：
                            </td>
                            <td style="width: 30%" colspan="3">
                                <asp:Label ID="lbRemark" runat="server" Text=""></asp:Label>
                                &nbsp;
                            </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    申请单状态：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lbStatus" runat="server" Text="Label"></asp:Label>
                                    &nbsp;
                                </td>
                                <td style="width: 20%">
                                    申请时间：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lbApplyDate" runat="server" Text="Label"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style="width: 100%" id="ApprovalPanel" runat="server">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="注销资产" OnClick="Button1_Click" OnClientClick="return confirm('确认要注销资产吗？')" />
                                        <input type="button" class="button_bak" value="关闭" onclick="javascript:window.location.href='ScrapRecord.aspx'" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
