<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewExamineResult.aspx.cs" Inherits="Module_FM2E_ExamineManager_ExamineResult_ViewExamineResult" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
  <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="查看考核结果表"
        HeadOPTxt="目前操作功能：查看考核结果表" HeadHelpTxt="帮助">
         <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width:100%">
    <asp:Repeater ID="Repeater1" runat="server"  OnItemDataBound="Repeater1_RowDataBound">
        <HeaderTemplate>
        <table  width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
        <tr><td class="Table_searchtitle" colspan="6"><%=CurrentExamineResult.SheetNO %>---<%=CurrentExamineResult.SheetName %></td></tr>
        <tr>
        <td class="table_none_WithoutWidth" style="height:30px;" colspan="6">
        考核对象：<%=CurrentExamineResult.ExamineTargetName %><br />
        考核时间：<%=CurrentExamineResult.Year %>年<%=EnumHelper.GetDescription(CurrentExamineResult.Season) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        结果单生成时间：<%=CurrentExamineResult.SaveTime.ToString("yyyy-MM-dd") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        考核人：<%=CurrentExamineResult.ExaminerName %></td>
        </tr>
        <tr>
        <td class="Table_searchtitle">考核表编号</td>
        <td class="Table_searchtitle">考核表名称</td>
        <td class="Table_searchtitle">考核类型</td>
        <td class="Table_searchtitle">考核人</td>
        <td class="Table_searchtitle">考核得分</td>
        <td class="Table_searchtitle">考核时间</td>
        </tr>
        </HeaderTemplate>
        <ItemTemplate>
        <tr>
        <td class="table_none_WithoutWidth">
            <asp:Literal ID="ltSheetNO" runat="server"></asp:Literal></td>
        <td class="table_none_WithoutWidth"><%#Eval("ExamSheetName") %></td>
        <td class="table_none_WithoutWidth"><%#EnumHelper.GetDescription((Enum)Eval("ExamineType")) %></td>
        <td class="table_none_WithoutWidth"><%#Eval("ExaminerName") %></td>
        <td class="table_none_WithoutWidth"><%#Eval("Score") %>分</td>
        <td  class="table_none_WithoutWidth"><%#Eval("SaveTime","{0:yyyy-MM-dd}") %></td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
        <tr>
        <td colspan="6" class="table_none_WithoutWidth">
        日常考核得分：<%=CurrentExamineResult.DailyExamineResult.ToString("0.##") %>&nbsp;&nbsp;&nbsp;&nbsp;
        季度考核得分：<%=CurrentExamineResult.SeasonExamineResult.ToString("0.##") %>&nbsp;&nbsp;&nbsp;&nbsp;
        总分：<%=CurrentExamineResult.DailyExamineResult.ToString("0.##") %>×<%=CurrentExamineResult.DailyExamineRatio*100 %>%+ 
            <%=CurrentExamineResult.SeasonExamineResult.ToString("0.##") %>×<%=CurrentExamineResult.SeasonExamineRatio*100 %>%＝
            <%=CurrentExamineResult.Score.ToString("0.##") %>分
        </td>
        </tr>
        </table></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

