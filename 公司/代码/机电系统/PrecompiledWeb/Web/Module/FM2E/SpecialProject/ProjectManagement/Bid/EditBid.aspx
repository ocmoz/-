<%@ page title="编辑招标信息" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectManagement_Bid_EditBid, App_Web_e-btwkdn" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
  <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：招标信息编辑" HeadHelpTxt="保存招标信息后可以进行提交审批">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="../ViewProject.aspx?cmd=view&projectid=" ButtonPopedom="List"/>
    </cc1:HeadMenuWebControls>
     <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    专项工程招标信息
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%">
                    项目名称
                </td>
                <td colspan="3">
                    <asp:Label ID="Label_ProjectName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%">
                    中标单位
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox_BiddenCompany" runat="server"  Width="90%"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td class="Table_searchtitle" style="width:15%" rowspan="2">
                    中标单位简介
                </td>
                <td  colspan="3">
                    <asp:TextBox ID="TextBox_BiddenCompanyInfo" runat="server" TextMode="MultiLine" Width="90%" Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  colspan="3">
                    
                    相关附件：<asp:FileUpload ID="FileUpload_File" runat="server"></asp:FileUpload><br />
                    <asp:HyperLink ID="HyperLink_File" Font-Underline="true"  ForeColor="Blue" runat="server" Visible="false"></asp:HyperLink>
                </td>
            </tr>
             
           
            
            </table>
             
       <center>
                    <asp:Button ID="Button_Save" runat="server" Text="保存"  CssClass ="button_bak"
                        onclick="Button_Save_Click" />
                    
                </center>
    </div>
</asp:Content>

