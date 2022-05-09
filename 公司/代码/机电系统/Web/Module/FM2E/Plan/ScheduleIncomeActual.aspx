<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ScheduleIncomeActual.aspx.cs" Inherits="Module_FM2E_Plan_ScheduleIncomeActual" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script src="<%=Page.ResolveUrl("~/") %>js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="月度收入明细"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：月度收入明细">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" ButtonVisible="false"/>
        <%--<cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="打印" ButtonPopedom="NotControl" />--%>
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="upMain" runat="server">
            <ContentTemplate>
                <table  width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;
                    position: inherit; z-index: inherit;">
                    <tr>
                        <td colspan="5" class="table_body_WithoutWidth">
                            <b style="font-family: 宋体; font-size: medium">深圳高速公路股份有限公司 项目月度实际收入明细表</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            编制部门：
                        </td>
                        <td style="width: 18%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbDepartment" runat="server"></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            日期：
                        </td>
                        <td style="width: 17%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbPlanTime" runat="server"></asp:Label>                           
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            单位：元
                        </td>                        
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">                              
                                <tr style="font-weight: bold;">
                                    <td class="table_body_WithoutWidth">
                                        序号
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        项目
                                    </td>
                                      <td class="table_none_WithoutWidth">
                                        内容
                                    </td>
                                      <td class="table_none_WithoutWidth">
                                        合同编号
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        金额
                                    </td>
                                      <td class="table_none_WithoutWidth">
                                        预计支付时间
                                    </td>
                                      <td class="table_none_WithoutWidth">
                                        备注
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                    </td>
                                </tr>
                                <asp:Repeater ID="r_UsePlan" runat="server" OnItemCommand="rpUsePlanItems_ItemCommand">
                                    <ItemTemplate runat="Server">
                                        <tr>
                                            <td class="table_body_WithoutWidth">
                                                <%# (Container.ItemIndex+1)%>
                                            </td>
                                            <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="r_lbPlanName"><%#Eval("ProjectName")%></asp:Label><asp:TextBox
                                                    runat="server" ID="r_tbPlanName" Text='<%#Eval("ProjectName")%>'  Width="80%" ></asp:TextBox>
                                            </td>
                                            <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="r_lbcontent"><%#Eval("content")%></asp:Label><asp:TextBox
                                                    runat="server" ID="r_tbcontent" Text='<%#Eval("content")%>' Width="80%" ></asp:TextBox>
                                            </td>
                                              <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="r_lbContractNo"><%#Eval("ContractNo")%></asp:Label><asp:TextBox
                                                    runat="server" ID="r_tbContractNo" Text='<%#Eval("ContractNo")%>' Width="80%" ></asp:TextBox>
                                            </td>
                                              <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="r_lbAmount"><%#Eval("Amount")%></asp:Label><asp:TextBox
                                                    runat="server" ID="r_tbAmount" Text='<%#Eval("Amount")%>' Width="80%" ></asp:TextBox>
                                            </td>
                                              <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="r_lbExpectPaymentTime"><%#Eval("PaymentTime")%></asp:Label><asp:TextBox
                                                    runat="server" ID="r_tbExpectPaymentTime" Text='<%#Eval("PaymentTime")%>' Width="80%" ></asp:TextBox>
                                            </td>
                                              <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="r_lbRemark"><%#Eval("Remark")%></asp:Label><asp:TextBox
                                                    runat="server" ID="r_tbRemark" Text='<%#Eval("Remark")%>' Width="80%" ></asp:TextBox>
                                            </td>
                                            <td class="table_none_WithoutWidth">
                                                <asp:ImageButton ID="ibDelEqItems" runat="server" ImageUrl="~/images/ICON/delete.gif" 
                                                    CommandArgument="<%# Container.ItemIndex %>" CommandName="del" OnClientClick="return confirm('确认要删除此设备费用吗？')"
                                                    CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr id="Tr4" runat ="server">
                                    <td class="table_none_WithoutWidth">
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:TextBox ID="tbPlanName" runat="server" Width="70"></asp:TextBox>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:TextBox ID="tbcontent" runat="server" Width="70"></asp:TextBox>
                                    </td>
                                      <td class="table_none_WithoutWidth">
                                        <asp:TextBox ID="tbContractNo" runat="server" Width="70"></asp:TextBox>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:TextBox ID="tbAmount" runat="server" Width="70"></asp:TextBox>
                                    </td>
                                      <td class="table_none_WithoutWidth">
                                        <input id="tbExpectPaymentTime" type="text" onClick="WdatePicker()" runat="server"/>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:TextBox ID="tbRemark" runat="server" Width="70"></asp:TextBox>
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Button runat="server" ID="Button2" CssClass="button_bak" Text="添加" OnClick="btAddUsePlanItems_Click" />
                                    </td>
                                </tr>
                                <tr id="Tr2">
                                    <td class="table_body_WithoutWidth" colspan="4">
                                        合计
                                    </td>
                                    <td class="table_none_WithoutWidth" colspan="3">
                                        <asp:Label runat="server" ID="lbUseTotalAmount" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                  
                </table>
                <div id="ApprovalDiv" style="text-align: center; padding: 10px 10px 20px 10px;">
                    <asp:Button ID="Button1" runat="server" Text="提 交" CssClass="button_bak" OnClientClick="javascript:return confirm('确定提交？');"
                        OnClick="Button1_Click" Height="20px" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
