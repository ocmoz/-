<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewAcceptanceReocrd.aspx.cs" Inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_ReturnEquipment_ViewAcceptanceReocrd" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备归还验收"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：验收设备详情">
          <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrl="window.history.go(-1);" ButtonUrlType="JavaScript" />
    </cc1:HeadMenuWebControls>
        <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="验收设备详细信息" ID="TabPanel1">
                <ContentTemplate>
                    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                        text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                        <tr>
                            <td style="width: 20%">
                                设备条形码：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbEquipmentNO" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td>
                          <td style="width: 20%">
                                设备名称：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbEquipmentName" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                          
                            <td style="width: 20%">
                                规格型号：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbModel" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td>
                             <td style="width: 20%">
                                归还方：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbReturnCompany" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="width: 20%">
                                归还人：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbReturner" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td>
                           <td style="width: 20%">
                                所属借调单：
                            </td>
                            <td style="width: 30%">
                                <asp:Literal ID="ltSheet" runat="server" Text="Literal"></asp:Literal>
                                &nbsp;
                            </td>
                        </tr>

                        <tr>
                            
                           <td style="width: 20%">
                                验收人：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbChecker" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td><td style="width: 20%">
                                验收时间：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbReturnDate" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="width: 20%">
                                验收结果：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbResult" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td> <td style="width: 20%">
                                验收备注：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbFeeBack" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                      
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

