<%@ page title="开工通知" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectApply_Start, App_Web_i4s4q9u5" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--工程立项"
        HeadOPTxt="目前操作功能：发放开工通知" HeadHelpTxt="可以打印开工通知；点击“开工”可以使工程进入施工状态">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="Apply.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    专项工程开工通知
                </td>
            </tr>
            <tr>
                <td>
                    <p><asp:Label ID="Label_TeamName" runat="server"></asp:Label>：</p>
                    <br />
                    <p>&nbsp;&nbsp;&nbsp;&nbsp;工程<asp:Label ID="Label_ProjectName" Font-Underline="true" runat="server"></asp:Label>前期工作已经准备完毕，现发放开工通知，请开始施工工作。</p>
                    <br />
                    <p style="float:right">
                        ____________________
                    </p>
                    <br />
                    <p  style="float:right"> _____年____月_____日</p>
                    
                </td>

            </tr>
            
            <tr>
                <td class="Table_searchtitle">
                    
                        
                        <asp:Button ID="Button_Start" runat="server" Text="开工"  
                        CssClass ="button_bak" onclick="Button_Start_Click" OnClientClick="javascript:return confirm('确认开工？');" />
                    
                </td>

            </tr>
            
            </table>
    </div>
</asp:Content>

