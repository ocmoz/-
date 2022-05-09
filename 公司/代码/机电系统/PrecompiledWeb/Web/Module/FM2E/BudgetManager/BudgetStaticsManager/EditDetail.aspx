<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManager_BudgetStaticsManager_EditDetail, App_Web_7mmdcik8" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="Table_searchtitle" colspan="4">
                        请在下面填入各项的实际开支
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        费用类型
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="SubIDNametb" title="请选择费用类型~:!" ReadOnly="true" runat="server">
                        </asp:TextBox>
                        <asp:TextBox title="请选择费用类型~:!" ID="SubIDtb" runat="server" Visible="false"></asp:TextBox>
                        <%--<asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                    <div style="border: 1px outset white; width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TreeView ID="TreeView2" runat="server" onclick="javascript:causeValidate = false;"
                                    OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                </asp:TreeView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>--%>
                        <%--<cc2:popupcontrolextender id="PopupControlExtender1" runat="server" targetcontrolid="SubIDNametb"
                    popupcontrolid="Panel1" position="Bottom" dynamicservicepath="" enabled="True"
                    extendercontrolid="">
                        </cc2:popupcontrolextender>
                <cc2:popupcontrolextender id="PopupControlExtender2" runat="server" targetcontrolid="SubIDtb"
                    popupcontrolid="Panel1" position="Bottom" dynamicservicepath="" enabled="True"
                    extendercontrolid="">
                        </cc2:popupcontrolextender>--%>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        开支项目
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox title="请输入开支项目名称~50:!" ID="ExpenditureNametb" ReadOnly="true" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        准予金额
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox title="请输入准予金额~:float!" ID="BudgetApprove" ReadOnly="true" runat="server">
                        </asp:TextBox>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        公司名
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="LB_CompanyName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        实际开支
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox title="请输入实际开支~:float" ID="RealExpendituretb" runat="server">
                        </asp:TextBox>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        收款方
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="Supplier" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        收款日期
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="PayDate" runat="server" title="请输入收款日期~date" class="input_calender"
                                    onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                    </td>
                    <td class="table_body table_body_NoWidth">
                    </td>
                    <td class="table_none table_none_NoWidth">
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="4">
                        <input type="button" runat="server" id="AddDetail" value="确定" class="button_bak"
                            onmouseover="javascript:causeValidate = true;" onserverclick="AddDetail_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
    document.getElementById("<%=RealExpendituretb.ClientID%>").focus();
    </script>

</asp:Content>
