<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MonthFundsUsePlan.aspx.cs" Inherits="Module_FM2E_Plan_MonthFundsUsePlan" %>

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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="月度资金使用计划"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：月度资金使用计划申请">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />       
        <%--<cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="打印" ButtonPopedom="NotControl" />--%>
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="upMain" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse; width:100%;
                    position: inherit; z-index: inherit;">
                    <tr>
                        <td colspan="6" class="table_body_WithoutWidth">
                            <b style="font-family: 宋体; font-size: medium">深圳高速公路股份有限公司 月度资金使用计划</b>
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
                            <input type="text" id="PlanTime" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月'})"
                                class="Wdate" runat="server" />                           
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            单位：
                        </td>
                        <td style="width: 17%" class="table_none_WithoutWidth">
                        万元
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="3">
                                        本月资金使用计划
                                    </td>
                                </tr>
                                <tr style="font-weight: bold;">
                                    <td class="table_body_WithoutWidth">
                                        序号
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        项目
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        金额
                                    </td>                                  
                                </tr>
                                <asp:Repeater ID="r_UsePlan" runat="server">
                                    <ItemTemplate runat="Server">
                                        <tr>
                                            <td class="table_none_WithoutWidth">
                                                <%# (Container.ItemIndex+1)%>
                                            </td>
                                            <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="lbUsePlanName"><%#Eval("ProjectName")%></asp:Label>
                                                <asp:TextBox runat="server" ID="tbUsePlanName" Text='<%#Eval("ProjectName")%>' Width="80%"
                                                    Visible="false"></asp:TextBox>
                                            </td>
                                            <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="lbUsePlanAmount"><%#Eval("SumAmount")%></asp:Label>
                                                <asp:TextBox runat="server" ID="tbUsePlanAmount" Text='<%#Eval("SumAmount")%>'
                                                    Width="80%" Visible="false"></asp:TextBox>                                           
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>                                
                                <tr id="Tr2">
                                    <td class="table_body_WithoutWidth" colspan="2">
                                        合计
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="lbUseTotalAmount" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="3">
                                        本月预计收入计划
                                    </td>
                                </tr>
                                <tr style="font-weight: bold;">
                                    <td class="table_body_WithoutWidth">
                                        序号
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        项目
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        金额
                                    </td>                                  
                                </tr>
                                <asp:Repeater ID="r_IncomePlan" runat="server" >
                                    <ItemTemplate runat="Server">
                                        <tr>
                                           <td class="table_none_WithoutWidth">
                                                <%# (Container.ItemIndex+1)%>
                                            </td>
                                            <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="lbIncomePlanName"><%#Eval("ProjectName")%></asp:Label><asp:TextBox
                                                    runat="server" ID="tbIncomePlanName" Text='<%#Eval("ProjectName")%>' Width="80%"
                                                    Visible="false"></asp:TextBox>
                                            </td>
                                            <td class="table_none_WithoutWidth">
                                                <asp:Label runat="server" ID="lbIncomePlanAmount"><%#Eval("SumAmount")%></asp:Label><asp:TextBox
                                                    runat="server" ID="tbIncomePlanAmount" Text='<%#Eval("SumAmount")%>' Width="80%"
                                                    Visible="false"></asp:TextBox>
                                            </td>                                         
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>                              
                                <tr id="Tr6">
                                    <td class="table_body_WithoutWidth" colspan="2">
                                        合计
                                    </td>
                                    <td class="table_none_WithoutWidth">
                                        <asp:Label runat="server" ID="lbincomeTotalAmount" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="5">
                                        上月资金使用情况
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        计划用款
                                    </td>
                                    <td>
                                        实际用款
                                    </td>
                                    <td>
                                        差异率%
                                    </td>
                                    <td rowspan="2">
                                        差异原因
                                    </td>
                                    <td rowspan="2">
                                    <asp:Label runat="server" ID="lb_UseDifferencesReasons" />
                                        <asp:TextBox ID="tb_UseDifferencesReasons" runat="server" Height="70px" MaxLength="200" TextMode="MultiLine"
                                            Width="414px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lb_LastMonthUse" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lb_Use" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lb_UseDifferences" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="5">
                                        上月资金收入情况
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        计划收入
                                    </td>
                                    <td>
                                        实际收入
                                    </td>
                                    <td>
                                        差异率%
                                    </td>
                                    <td rowspan="2">
                                        差异原因
                                    </td>
                                    <td rowspan="2">
                                     <asp:Label runat="server" ID="lb_IncomeDifferencesReasons" />                                      
                                        <asp:TextBox ID="tb_IncomeDifferencesReasons" runat="server" Height="70px" MaxLength="200" TextMode="MultiLine"
                                            Width="414px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lb_LastMonthincome" />
                                    </td>
                                    <td>                                     
                                        <asp:Label runat="server" ID="lb_Income" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lb_IncomeDifferences" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            审批记录：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;">
                            审批人:<asp:Label runat="server" ID="Label1" /><br>
                            审批意见:
                            <asp:Label ID="Label2" runat="server" /><br>
                            审批日期:<asp:Label ID="Label3" runat="server" />
                        </td>
                    </tr>
                    <tr id="opinion2" runat="server">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            审批意见：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;">
                            <asp:TextBox ID="tbApprovalRemark" runat="server" Height="70px" MaxLength="200" TextMode="MultiLine"
                                Width="414px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="ApprovalDiv" style="text-align: center; padding: 10px 10px 20px 10px;">
                    <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="提 交" CssClass="button_bak" OnClientClick="javascript:return confirm('确定提交？');"
                        OnClick="Button1_Click" Height="20px" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
