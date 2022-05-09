<%@ page title="专项工程立项申请--填写可行性报告" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectApply_Apply, App_Web_i4s4q9u5" %>

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
        HeadOPTxt="目前操作功能：可行性报告编辑" HeadHelpTxt="保存可行性报告后，可以进行“工作量清单”以及“预算清单”的编辑">
        
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="工作量清单" ButtonUrlType="Href"
            ButtonUrl="EditJobItems.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
             <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="预算清单" ButtonUrlType="Href"
            ButtonUrl="EditBudgetItems.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
            <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回工程列表" ButtonUrlType="Href"
            ButtonUrl="SpecialProjectList.aspx" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="2">
                    专项工程可行性报告
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%">
                    项目名称
                </td>
                <td>
                    <asp:TextBox ID="TextBox_ProjectName" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%">
                    预算来源
                </td>
                <td>
                    <asp:TextBox ID="TextBox_BudgetName" runat="server"  Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%">
                    大概预算
                </td>
                <td>
                    <asp:TextBox ID="TextBox_Budget" runat="server"  Width="10%"></asp:TextBox>元
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%" rowspan="2">
                    系统现状
                </td>
                <td>
                    <asp:TextBox ID="TextBox_CurrentStatus" runat="server" TextMode="MultiLine" Width="90%" Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    附件：<asp:FileUpload ID="FileUpload_CurrentStatusFile" runat="server"></asp:FileUpload><br />
                    <asp:HyperLink ID="HyperLink_CurrentStatusFile" ForeColor="Blue" Font-Underline="true" runat="server" Visible="false"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="width:15%" rowspan="2">
                    存在的问题
                </td>
                <td>
                    <asp:TextBox ID="TextBox_Problem" runat="server" TextMode="MultiLine" Width="90%" Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    
                    附件：<asp:FileUpload ID="FileUpload_ProblemFile" runat="server"></asp:FileUpload><br />
                    <asp:HyperLink ID="HyperLink_ProblemFile" ForeColor="Blue" Font-Underline="true" runat="server" Visible="false"></asp:HyperLink>
                </td>
            </tr>
             <tr>
                <td class="Table_searchtitle" style="width:15%" rowspan="2">
                    拟解决的技术方案
                </td>
                <td>
                    <asp:TextBox ID="TextBox_Solution" runat="server" TextMode="MultiLine" Width="90%" Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    附件：<asp:FileUpload ID="FileUpload_SolutionFile" runat="server"></asp:FileUpload><br />
                    <asp:HyperLink ID="HyperLink_SolutionFile" ForeColor="Blue" Font-Underline="true" runat="server" Visible="false"></asp:HyperLink>
                </td>
            </tr>
            
           
            
            </table>
          <center>
                    <asp:Button ID="Button_Save" runat="server" Text="保存草稿"  CssClass ="button_bak"
                        onclick="Button_Save_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="Button_Submit" runat="server" Text="提交申请"  
                        CssClass ="button_bak" onclick="Button_Submit_Click"/>
                    
               </center>
    </div>
</asp:Content>
