<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewBorrowApply.aspx.cs" Inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApply_ViewBorrowApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备借调申请"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：设备借调单查看">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="借调申请单内容" ID="TabPanel1">
                <ContentTemplate>
                    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                        text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                        <tr>
                            <td style="width: 20%">
                                申请单编号：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbSheetName" runat="server" Text=""></asp:Label>
                                &nbsp;
                            </td>
                            <td style="width: 20%">
                                借出方：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbLendCompany" runat="server" Text=""></asp:Label>  
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                申请方：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbBorrowCompany" runat="server" Text=""></asp:Label>
                                &nbsp;
                            </td>
                            <td style="width: 20%">
                                申请人：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbApplicant" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                申请单状态：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 20%">
                                申请时间：
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbSubmitTime" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EquipmentName" HeaderText="物品名称">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="规格型号">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Count" HeaderText="数量">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit" HeaderText="单位">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Reason" HeaderText="借用原因">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("AddressName") %><%# Eval("DetailLocation") %>
                                </ItemTemplate>
                                <HeaderTemplate>使用地点</HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReturnDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="归还日期"
                                HtmlEncode="False">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            没有借调申请明细信息
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="审批历史" ID="TabPanel2">
                <ContentTemplate>
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
                            <asp:BoundField DataField="ApprovalDate" HeaderText="审批时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FeeBack" HeaderText="审批备注"></asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            暂没审批记录
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="借用的设备" ID="TabPanel3">
                <ContentTemplate>
                     <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码">
                            </asp:BoundField>
                            <asp:BoundField DataField="EquipmentName" HeaderText="设备名称">
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="规格型号"></asp:BoundField>
                             <asp:BoundField DataField="BorrowerName" HeaderText="领用人"></asp:BoundField>
                            <asp:BoundField DataField="BorrowTime" HeaderText="借用时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}"></asp:BoundField>
                            <asp:BoundField DataField="ReturnDate" HeaderText="应归还期限" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false"></asp:BoundField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    是否已归还</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("IsReturned")) ? "是" : "否"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            暂没借用到的设备
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="归还验收记录" ID="TabPanel4">
                <ContentTemplate>
                     <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码">
                            </asp:BoundField>
                            <asp:BoundField DataField="EquipmentName" HeaderText="设备名称">
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="规格型号"></asp:BoundField>
                             <asp:BoundField DataField="ReturnDate" HeaderText="验收时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"></asp:BoundField>
                              <asp:TemplateField>
                                <HeaderTemplate>
                                    验收结果</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("Result")) ? "验收通过" : "验收不通过"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FeeBack" HeaderText="验收备注"></asp:BoundField>
                            <asp:BoundField DataField="CheckerName" HeaderText="验收人"></asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            暂没归还验收记录
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
