<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BugReportManager_ViewBugreport_ViewBugreport, App_Web_uggv5sxo" %>


<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" >
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="意见列表"
        HeadOPTxt="目前操作功能：查看用户意见">
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
                标题：
            </td>
            <td style="width:90%" colspan="3">
                <asp:Label ID="LB_Title" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  class="table_body table_body_NoWidth" style="height: 30px;width:10%;text-align: right;">
                内容：
            </td>
            <td style="width:90%" colspan="3">
                <asp:Label ID="LB_Message" Width="95%" Height="100px" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  class="table_body table_body_NoWidth" style="height: 30px;width:10%; text-align: right;">
                附件：
            </td>
            <td style="width:90%" colspan="3">
                <a runat="server" id="attachment" style="color: Red">下载附件</a>
            </td>
        </tr>
        <tr>
            <td  class="table_body table_body_NoWidth" style="height: 30px;width:10%;text-align: right;">
                反馈意见：
            </td>
            <td style="width:90%" colspan="3">
                <asp:Label ID="LB_Report" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
