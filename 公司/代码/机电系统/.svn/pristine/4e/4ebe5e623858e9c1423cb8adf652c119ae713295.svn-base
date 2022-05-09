<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Approval.aspx.cs" Inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApproval_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备借调审批"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：借调申请审批">
        <cc1:HeadMenuButtonItem ButtonName="返回" ButtonIcon="back.gif" ButtonPopedom="List"
            ButtonUrl="window.history.go(-1);" ButtonUrlType="JavaScript" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="借调申请审批" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td style="width: 20%">
                                    申请单编号：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lbSheetName" runat="server" Text=""></asp:Label>
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
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unit" HeaderText="单位">
                                    <ItemStyle Width="10%" />
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
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
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
                                            <asp:BoundField DataField="FeeBack" HeaderText="审批备注"></asp:BoundField>
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
                        <div style="width: 100%" id="ApprovalPanel" runat="server">
                            <hr />
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="2">
                                        设备借调申请单审批
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        审批结果：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                        <asp:DropDownList ID="DDLApproval" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        反馈意见：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                        <asp:TextBox ID="tbFeeBack" runat="server" TextMode="MultiLine" title="请输入反馈意见~50:"
                                            Height="58px" Width="507px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                          <center>
                                        <asp:Label ID="errMsg" ForeColor="Red" runat="server"></asp:Label><br />
                                   
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="提交审批" OnClick="Button1_Click" OnClientClick="javascript:return confirm('确认审批？');" />
                                        <input type="button" class="button_bak" value="返回" onclick="javascript:window.location.href='BorrowApproval.aspx'" />
                                    </center>
                        </div>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
