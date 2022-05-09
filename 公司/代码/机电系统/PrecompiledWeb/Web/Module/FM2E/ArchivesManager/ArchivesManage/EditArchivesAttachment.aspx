<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_ArchivesManager_ArchivesManage_EditArchivesAttachment, App_Web_sjqrm1y9" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
	 <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
	 <script type="text/javascript">
	 //添加材料
        function addItem() {
            //返回的值包括 档案描述、档案名称、档案保存路径及备注

            //档案描述
            var description = trim(document.getElementById('<%= tbDescription.ClientID %>').value);


            //档案保存路径
            var savepath = trim(document.getElementById('<%= lbSavePath.ClientID %>').innerText);
            if (savepath.length == 0) {
                alert('请增加附件');
                return;
            }
            
            //档案名称
            var archivesattachmentname = trim(document.getElementById('<%= lbArchivesAttachmentName.ClientID %>').innerText);
            
            //备注
            var remark = trim(document.getElementById('<%= tbRemark.ClientID %>').value);

            window.returnVal = description + "|" + savepath + "|" + archivesattachmentname + "|" + remark;
            window.parent.hidePopWin(true);
            
        }
    </script>
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="添加档案附件管理"  HeadHelpTxt="点击进入添加档案附件管理" HeadOPTxt="目前操作功能：添加档案附件">
	</cc1:HeadMenuWebControls>
	<div style="width: 95%; ">
		<div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
			<table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					档案ID：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
					<asp:TextBox ID="tbArchivesID" runat="server" title="请输入字符串~8:"></asp:TextBox>
				</td>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					档案描述：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
					<asp:TextBox ID="tbDescription" runat="server" title="请输入字符串~1000:"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					档案保存路径：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
					附件：<asp:FileUpload ID="FileUpload_ArchivesAttachmentFile" runat="server"></asp:FileUpload>
                    <asp:HyperLink ID="HyperLink_ArchivesAttachmentFile" ForeColor="Blue" Font-Underline="true" runat="server" Visible="false"></asp:HyperLink>
                    <asp:Button ID="btUpload" OnClick="OnClick_btUpload" runat="server" Text="上传" /><br />
                    <asp:Label ID="lbArchivesAttachmentName" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbSavePath" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					备注：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
					<asp:TextBox ID="tbRemark" runat="server" title="请输入字符串~50:" TextMode="MultiLine" MaxLength="200" Width="85%"></asp:TextBox>
				</td>
			</tr>
			</table>
		</div>
		<table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
			<tr align="center">
                <td colspan="6">
                    <input class="button_bak" type="button" value="添加" 
                    onclick="javascript:addItem();" />
                    <input class="button_bak" type="button" value="关闭" onclick="javascript:window.parent.hidePopWin();" />
                </td>
            </tr>
		</table>
	</div>
</asp:Content>

