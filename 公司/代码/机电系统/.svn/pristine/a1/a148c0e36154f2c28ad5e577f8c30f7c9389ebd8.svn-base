<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="BarCode.aspx.cs" Inherits="Module_FM2E_DeviceManager_BarCode_BarCode" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="WebUtility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
   <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="条形码批量生成"
        HeadOPTxt="目前操作功能：条形码批量生成" HeadHelpTxt="使用Code128的条形码标准批量生成条形码">
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel_Input" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                 <tr style='<%= UserData.CurrentUserData.IsParentCompany? "display:block": "display:none" %>'>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        选择公司：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <asp:DropDownList ID="ddlCompany" runat="server" title="请选择公司~!">
                        </asp:DropDownList>
                        <span style="color: Red">*</span>
                    </td>
                 </tr>
                 <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        采购年月：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <asp:TextBox ID="tbPurchaseDate" runat="server" title="请输入采购年月~date!" class="input_calender"
                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        是否零配件：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:CheckBox ID="cbIsComponent" runat="server" Text="是否零配件" />
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        生成条形码的数量：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="tbCount" runat="server" title="请输入生成条形码的数量~int!" MaxLength="4" Width="60px"></asp:TextBox>个
                                    <span style="color: Red">*</span>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btGenerate" runat="server" Text="生成条形码" CssClass="button_bak2" 
                                        onclick="btGenerate_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lbErrMsg" runat="server" Text=""></asp:Label>
            <br />
             <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;" runat="server" id="barCodeTable" visible="false">
                <tr><td class="Table_searchtitle">生成的条形码</td></tr>
                <tr>
                    <td class="table_none_NoWidth" style="text-align:left">
                         <asp:Literal ID="ltBarCodes" runat="server" Text="" ></asp:Literal>
                    </td>
                </tr>
                <tr><td align="right">
                    <input id="btPrint" type="button" value="打印条形码" class="button_bak2" onclick="javascript:GoToPrint();" />
                    </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
        <script language="javascript" type="text/javascript">
    <!--
        function GoToPrint() {
            if (detectPlugin("905418AC-23D7-4BB4-9E53-FFF52C734EDB", "LabelWidth")) {
                //有安装
                showPopWin('打印条形码', 'BarCodePrint.aspx', 800, 330, null, true, true);
            } else {
                alert("请先安装打印控件(PrintActiveX)");
                window.location.href = "../../../../Plugin/PrintActiveX.rar";
            }
        }
    -->
    </script>
</asp:Content>
