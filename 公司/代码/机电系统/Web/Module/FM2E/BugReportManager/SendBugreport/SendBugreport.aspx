<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SendBugreport.aspx.cs" Inherits="Module_FM2E_BugReportManager_SendBugreport_SendBugreport" %>


<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>


<asp:Content ID="Content1"  ContentPlaceHolderID="PageBody" runat="Server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="用户反馈"
        HeadOPTxt="目前操作功能：发送意见">
        
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="发送历史" ButtonUrlType="Href"
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
            <td  style="height: 30px;width:10%;text-align: right;">
                标题：
            </td>
            <td class="table_body table_body_NoWidth" style="width:90%" colspan="3">
                <asp:TextBox ID="TextBox_Title" title="请输入标题~50:!" runat="server" Width="400px"></asp:TextBox>
                （50字以内）
            </td>
        </tr>
        <tr>
            <td  style="height: 30px;width:10%;text-align: right;">
                内容：
            </td>
            <td class="table_body table_body_NoWidth" style="width:90%" colspan="3">
                <asp:TextBox ID="tb_MessageContent" runat="server" Width="95%" Height="100px" TextMode="MultiLine"
                    title="请输入消息内容~200:!" BorderStyle="Groove" MaxLength="200" />
                <br />
                （200字以内）
            </td>
        </tr>
        <tr>
            <td  style="height: 30px;width:10%; text-align: right;">
                附件：
            </td>
            <td class="table_body table_body_NoWidth" style="width:90%" colspan="3">
                <asp:FileUpload ID="FileUpload_Attachment" runat="server" />（若发送失败,请重新选择.<a runat="server" id="template" style="color: Red">上传前请先点击此处下载模板</a>）
                
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
