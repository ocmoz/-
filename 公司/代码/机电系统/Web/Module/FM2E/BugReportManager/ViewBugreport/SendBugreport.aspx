<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SendBugreport.aspx.cs" Inherits="Module_FM2E_BugReportManager_ViewBugreport_SendBugreport" Title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="意见列表"
        HeadOPTxt="目前操作功能：填写反馈意见">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="用户意见列表" ButtonUrlType="Href"
            ButtonUrl="History.aspx" />
        </cc1:HeadMenuWebControls>
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                用户意见
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="height: 30px;width:10%;text-align: right;">
                意见标题：
            </td>
            <td style="width:90%" colspan="3">
                <asp:Label ID="LB_Title" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  class="table_body table_body_NoWidth" style="height: 30px;width:10%;text-align: right;">
                意见内容：
            </td>
            <td style="width:90%" colspan="3">
                <asp:Label ID="LB_Message" Width="95%" Height="100px" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  class="table_body table_body_NoWidth" style="height: 30px;width:10%; text-align: right;">
                意见附件：
            </td>
            <td style="width:90%" colspan="3">
                <a runat="server" id="attachment" style="color: Red">下载附件</a>
            </td>
        </tr>
        <tr>
            <td class="Table_searchtitle" colspan="4">
                请在下面填写您的反馈意见
            </td>
        </tr>
        <tr>
            <td  class="table_body table_body_NoWidth" style="height: 30px;width:10%;text-align: right;">
                反馈意见：
            </td>
            <td style="width:90%" colspan="3">
                <%--<asp:Label ID="LB_Report" runat="server"></asp:Label>--%>
                <asp:TextBox ID="TB_Report" Width="95%" Height="100px" TextMode="MultiLine" title="请输入反馈内容~200:!" MaxLength="200" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="table_body table_body_NoWidth" style="height: 30px;width:10%; text-align: right;">
                反馈附件：
            </td>
            <td style="width:90%" colspan="3">
                <%--<a runat="server" id="attachment" style="color: Red">下载附件</a>--%>
                <asp:FileUpload ID="FileUpload_Attachment" runat="server" />(请上传您的反馈附件)
            </td>
        </tr>
        <tr>
            <td class="Table_searchtitle" colspan="4">
                 <asp:Button ID="btn_Send" runat="server" Text="发送" CssClass="button_bak" OnClick="btn_Send_Click"/>
                &nbsp;<input id="btn_Clear" type="reset" value="清空" class="button_bak" 
                     onclick="javascipt:return confirm('确认清空？');" tabindex="2" />

            </td>
        </tr>
    </table>
</asp:Content>

