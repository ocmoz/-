<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewBorrowRecord.aspx.cs" Inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowRecord_ViewBorrowRecord" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备借出登记"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：借出设备详情">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrl="window.history.go(-1);" ButtonUrlType="JavaScript" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="借出设备详细信息" ID="TabPanel1">
                <ContentTemplate>
                    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                        text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                        <tr>
                            <td style="width: 20%">
                                设备条形码：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbEquipmentNO" runat="server" Text=""></asp:Label>
      
                            </td>
                          <td style="width: 20%">
                                设备名称：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbEquipmentName" runat="server" Text=""></asp:Label>
                
                            </td>
                        </tr>
                        <tr>
                          
                            <td style="width: 20%">
                                规格型号：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbModel" runat="server" Text=""></asp:Label>
                           
                            </td>
                             <td style="width: 20%">
                                借用方：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbBorrowCompany" runat="server" Text=""></asp:Label>
              
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="width: 20%">
                                申请人：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbApplicant" runat="server" Text=""></asp:Label>
                           
                            </td>
                           <td style="width: 20%">
                                所属申请单：
                            </td>
                            <td style="width: 30%">
                                <asp:Literal ID="ltSheet" runat="server" Text=""></asp:Literal>
                      
                            </td>
                        </tr>

                        <tr>
                            
                           <td style="width: 20%">
                                领用人：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbBorrower" runat="server" Text=""></asp:Label>
                             
                            </td><td style="width: 20%">
                                借出时间：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbBorrowTime" runat="server" Text=""></asp:Label>
                           
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="width: 20%">
                                归还时间：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbReturnTime" runat="server" Text=""></asp:Label>
                              
                            </td> <td style="width: 20%">
                                是否已归还：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbIsReturned" runat="server" Text=""></asp:Label>
                           
                            </td>
                        </tr>
                         <tr>
                         
                           <td style="width: 20%">
                                借用原因：
                            </td>
                            <td style="width: 30%" colspan="3">
                                <asp:Label ID="lbReason" runat="server" Text=""></asp:Label>
                           
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
