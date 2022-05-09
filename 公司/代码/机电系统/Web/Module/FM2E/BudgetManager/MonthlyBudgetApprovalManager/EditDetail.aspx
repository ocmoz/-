<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditDetail.aspx.cs" Inherits="Module_FM2E_BudgetManager_MonthlyBudgetApprovalManager_EditDetail"
    Title="无标题页" %>

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
        <div style="width:100%;height:665px;overflow-x:auto;overflow-y:auto;">
       
            <table width="100%">
                <tr>
                    <td class="Table_searchtitle" colspan="4">
                        请在下面填写你审批的结果
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        费用类型
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="SubIDNametb" title="请选择费用类型~:!" runat="server">
                        </asp:TextBox>
                        <asp:TextBox title="请选择费用类型~:!" ID="SubIDtb" runat="server" Visible="false"></asp:TextBox>
                        <asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                            <div style="border: 1px outset white; width: 100px">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TreeView ID="TreeView2" runat="server" onclick="javascript:causeValidate = false;"
                                            OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </asp:Panel>
                        <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="SubIDNametb"
                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                            ExtenderControlID="">
                        </cc2:PopupControlExtender>
                        <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="SubIDtb"
                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                            ExtenderControlID="">
                        </cc2:PopupControlExtender>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        开支项目
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox title="请输入开支项目名称~50:!" ID="ExpenditureNametb" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        总经理审批意见
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="Reviewtb" title="请输入总经理审批意见~100:" runat="server">
                        </asp:TextBox>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        备注
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="Remarks" title="请输入备注~100:" runat="server"></asp:TextBox>
                    </td>
                </tr>
                               	
                <tr>
                    <td class="table_body table_body_NoWidth">
                        开支依据
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="ExpenditureDetailtb" runat="server">
                        </asp:TextBox>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        依据附件
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <a runat="server" id="attachment" style="color: Red">下载附件</a>
                    </td>
                </tr>
                 </table>
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="table_body table_body_NoWidth">
                        申请金额
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                        <table border="1">
                        <tr id="expenditure" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        准予金额
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                        <table border="1">
                         <tr id="budgetapprove" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="4">
                        <input type="button" runat="server" id="AddDetail" value="确定" class="button_bak"
                            onmouseover="javascript:causeValidate = true;" onserverclick="AddDetail_Click" />
                    </td>
                </tr>
            </table>
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input type="hidden" id="sessionvalue" runat="server" />
    <input id="companycount" runat="server" type="hidden" />
    <script type="text/javascript" language="javascript">
        function AddSession(id, inputvalue) {
            if (inputvalue != null) {
                if (inputvalue == "")
                    inputvalue = "0";
                if (isNaN(parseFloat(inputvalue)) || parseFloat(inputvalue) != inputvalue) {
                    alert("输入的金额不能包括其他符号");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                    return false;
                }
                document.getElementById("<%=sessionvalue.ClientID%>").value += id + "," + inputvalue + "|";
            }

        }
    
    </script>
</asp:Content>
