<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BugReportManager_SendBugreport_History, App_Web_aysapuls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="用户反馈"
        HeadOPTxt="目前操作功能：发送历史">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="发送意见" ButtonUrlType="Href"
            ButtonUrl="SendBugreport.aspx" />
    </cc1:HeadMenuWebControls>
    
    <script type="text/javascript">
        function HideShowForm(a) {
            var fm1 = document.getElementById(a.id.replace('tr_hideshow', 'tr_form1'));
            var fm2 = document.getElementById(a.id.replace('tr_hideshow', 'tr_form2'));
            var tr_attachement = document.getElementById(a.id.replace('tr_hideshow', 'tr_attachment'));
            var a_hs = document.getElementById(a.id.replace('tr_hideshow', 'Label_HideShow'));
            
            if (fm1.style.display == 'block') {
                a_hs.innerHTML = a_hs.innerHTML.replace('--收缩', '++展开');
                
                fm1.style.display = 'none';
                fm2.style.display = 'none';
                tr_attachement.style.display = 'none';
            }
            else {
                a_hs.innerHTML = a_hs.innerHTML.replace('++展开', '--收缩');
                
                fm1.style.display = 'block';
                
                fm2.style.display = 'block';
                
                tr_attachement.style.display = 'block';
            }
        }
    </script>
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                用户意见发送历史列表
            </td>
        </tr>
        <tr>
            <td colspan="4">
            
            <asp:Repeater ID="Repeater_ReportList" runat="server">
            <ItemTemplate>
             <table style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; border: solid 1px #a7c5e2;" border="1">
                <tr id="tr_hideshow" onclick="javascript:HideShowForm(this);" runat="server" style="Cursor:hand">
                    <th class="Table_searchtitle" style="width:8%;"><%# Container.ItemIndex +1 %></th>
                    <th class="Table_searchtitle" style=" text-align:left"><%# Eval("Title") %></th>
                    <th class="Table_searchtitle" style="width:15%"><%# Eval("ReportTime","{0:yyyy-MM-dd HH:mm}") %></th>
                    <th class="Table_searchtitle"  style="width:10%"><%# Eval("StatusShow") %></th>
                    <th class="Table_searchtitle" style="width:10%; text-align:right">
                    
                    <asp:Label ID="Label_HideShow"
                                        Text="[++展开]" Font-Underline="true"
                                        ForeColor="Blue" runat="server"></asp:Label></th>
                </tr>
                <tr id="tr_form1" runat="server" style="display:none">
                    <td class="Table_searchtitle" rowspan="2">报告内容：</td><td colspan="4"><%# Eval("Message") %></td>
                </tr>
                <tr id="tr_attachment" runat="server" style="display:none">
                    <td colspan="4">
                    <a id="HyperLink_Attachment"  style='<%#  
                     "color:Blue;display:"+ (string.IsNullOrEmpty(Eval("Attachment")==null?"":Eval("Attachment").ToString())? "none":"block")  %>' 
                    
                    href='<%# Page.ResolveUrl(Eval("Attachment")==null?"":Eval("Attachment").ToString()) %>' >
                    下载附件(请使用“右键另存为”)
                    </a>
                    </td>
                </tr>
                <tr  id="tr_form2" runat="server"  style="display:none"><td class="Table_searchtitle">反馈内容：</td><td colspan="4"><%# Eval("Report") %></td></tr>
            </table>
            
            </ItemTemplate>
            
            </asp:Repeater>
           

                
                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                    NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                    ShowCustomInfoSection="Left">
                </cc1:AspNetPager>
            </td>
        </tr>
    </table>
</asp:Content>
