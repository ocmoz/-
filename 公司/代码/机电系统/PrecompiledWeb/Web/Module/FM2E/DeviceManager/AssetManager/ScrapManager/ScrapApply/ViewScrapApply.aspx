<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapApply_ViewScrapApply, App_Web_no8k4bt3" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="报废申请单查看"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：报废申请单查看">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="报废申请单查看" ID="TabPanel1">
                <ContentTemplate>
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
                            <td style="width: 30%" >
                                <asp:Label ID="lbEquipment" runat="server" Text=""></asp:Label>
                                &nbsp;
                            </td>
                            <td style="width: 20%">
                                报废设备条码：
                            </td>
                            <td style="width: 30%" >
                                <asp:Literal ID="lbEquipmentNo" runat="server"></asp:Literal>
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
                        <tr>
                            <td style="height: 30px;">
                                附件记录：
                            </td>
                            <td colspan="3">                               
                                <asp:HyperLink ID="HyperLink_File" ForeColor="Blue" Font-Underline="true"
                                runat="server" ></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
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
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td class="Table_searchtitle" style="width: 20%">
                                    审批历史
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovalerName" HeaderText="审批人">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="审批结果">
                                    <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("Result"))?"通过":"不通过" %>
                                    </ItemTemplate>
                                     <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovalDate" HeaderText="审批时间">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FeeBack" HeaderText="审批备注">
                                            </asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            暂没审批记录
                                        </EmptyDataTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

