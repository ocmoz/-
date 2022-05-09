<%@ page title="编辑设计方案" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectManagement_Design_EditDesign, App_Web_k_yhflah" %>
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
        HeadOPTxt="目前操作功能：设计方案编辑" HeadHelpTxt="点击保存后可以进行设计方案的提交">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="history.go(-1);" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
     <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    专项工程设计方案
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
                    方案名称
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox_DesignName" runat="server"  Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%">
                    设计费用
                </td>
                <td style="width:35%">
                    <asp:TextBox ID="TextBox_DesignCost" runat="server"  Width="25%"></asp:TextBox>元
                </td>
                <td class="Table_searchtitle" style="width:15%">
                    工程费用
                </td>
                <td style="width:35%">
                    <asp:TextBox ID="TextBox_ProjectCost" runat="server"  Width="25%"></asp:TextBox>元
                </td>
            </tr>
             <tr>
                <td class="Table_searchtitle" style="width:15%">
                    设计单位
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox_Designer" runat="server"  Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%" >
                    设计单位信息
                </td>
                <td  colspan="3">
                    <asp:TextBox ID="TextBox_DesignerInfo" runat="server" TextMode="MultiLine" Width="90%" Height="200px"></asp:TextBox>
                </td>
            </tr>
          
            <tr>
                <td class="Table_searchtitle" style="width:15%" rowspan="2">
                    设计方案
                </td>
                <td  colspan="3">
                    <asp:TextBox ID="TextBox_Design" runat="server" TextMode="MultiLine" Width="90%" Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  colspan="3">
                    
                    附件：<asp:FileUpload ID="FileUpload_Design" runat="server"></asp:FileUpload><br />
                    <asp:HyperLink ID="HyperLink_Design" runat="server" ForeColor="Blue" Font-Underline="true" Visible="false"></asp:HyperLink>
                </td>
            </tr>
             
            
          
            
            </table>
             <center>
                    <asp:Button ID="Button_Save" runat="server" Text="保存"  CssClass ="button_bak"
                        onclick="Button_Save_Click" />
                   
             </center>
    </div>
</asp:Content>

