<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="EditArchives.aspx.cs" Inherits="Module_FM2E_ArchivesManager_ArchivesManage_EditArchives" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

	<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        //编辑的时候设置模式对话框的值
        function setModalPopup(button_id) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            //ITEMID
            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'lb_ItemID')).innerText.replace(regS, "");
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

            //备注
            var description = document.getElementById(button_id.replace('ImageButton_Edit', 'lb_Description')).innerText;
            document.getElementById('<%= tbAttachmentDescription.ClientID %>').value = description;

            //档案保存路径
            var savepath = document.getElementById(button_id.replace('ImageButton_Edit', 'lb_SavePath')).innerText;
            document.getElementById('<%= tbAttachmentSavePath.ClientID %>').value = savepath;

            //档案附件名称
            var attachmentname = document.getElementById(button_id.replace('ImageButton_Edit', 'lb_ArchivesAttachmentName')).innerText;
            document.getElementById('<%= tbAttachmentName.ClientID %>').value = attachmentname;
            
            //备注
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'lb_Remark')).innerText;
            document.getElementById('<%= tbAttachmentRemark.ClientID %>').value = remark;
        }

    
        //保存编辑的项
        function saveEditItem() {
            //档案描述
            var description = trim(document.getElementById('<%= tbAttachmentDescription.ClientID %>').value);
            //if (description.length == 0) {
            //alert('请输入产品名称');
            //return;
            //}
            //档案保存路径
            var savepath = trim(document.getElementById('<%= tbAttachmentSavePath.ClientID %>').value);
            if (savepath.length == 0) {
                alert('请增加附件');
                return;
            }
            //备注
            var remark = trim(document.getElementById('<%= tbAttachmentRemark.ClientID %>').value);
            
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();  //触发按钮
        }
    </script>
    
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="档案录入修改管理"  HeadHelpTxt="点击进入档案管理" HeadOPTxt="目前操作功能：档案基本信息">
		<cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="档案列表" ButtonPopedom="List"  ButtonUrlType="href" ButtonUrl="Archives.aspx" />
		<cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"  ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
	</cc1:HeadMenuWebControls>
	
	<asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%;  display: none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    编辑档案附件资料<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    档案描述：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="tbAttachmentDescription" runat="server"></asp:TextBox>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    档案附件名称：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="tbAttachmentName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    保存路径：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="tbAttachmentSavePath" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="tbAttachmentRemark" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save" class="button_bak" type="button" value="保存" onclick="javascript:saveEditItem();" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
	
	<div style="width: 95%; ">
		
	    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
		    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
		    <tr>
			    <td class="table_body table_body_NoWidth" style="height: 30px">
				    档案名称：
			    </td>
			    <td class="table_none table_none_NoWidth" style="height: 30px">
				    <asp:TextBox ID="tbArchivesName" runat="server" title="请输入字符串~20:"></asp:TextBox>
			    </td>
			    <td class="table_body table_body_NoWidth" style="height: 30px">
                    档案类型：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; color: #FF0000;">
                    <asp:TextBox ID="tbArchivesTypeName" runat="server"></asp:TextBox>
                    <asp:TextBox ID="tbArchivesTypeID" runat="server" Visible="false"></asp:TextBox>
                    <asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                </asp:TreeView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="tbArchivesTypeName"
                        PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                        ExtenderControlID="">
                    </cc2:PopupControlExtender>
                    <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="tbArchivesTypeID"
                        PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                        ExtenderControlID="">
                    </cc2:PopupControlExtender>
                </td>
		    </tr>
		    <tr>
			    <td class="table_body table_body_NoWidth" style="height: 30px">
				    涉及系统：
			    </td>
			    <td class="table_none table_none_NoWidth" style="height: 30px">
				    <asp:TextBox ID="tbInvolvedSystem" runat="server" title="请输入字符串~80:"></asp:TextBox>
			    </td>
			    <td class="table_body table_body_NoWidth" style="height: 30px">
				    涉及设备：
			    </td>
			    <td class="table_none table_none_NoWidth" style="height: 30px">
				    <asp:TextBox ID="tbInvolvedEquipment" runat="server" title="请输入字符串~80:"></asp:TextBox>
			    </td>
		    </tr>
		    <tr>
			    <td class="table_body table_body_NoWidth" style="height: 50px">
				    档案描述：
			    </td>
			    <td class="table_none table_none_NoWidth" style="height: 50px" colspan="3">
				    <asp:TextBox ID="tbDescription" runat="server" title="请输入字符串~1000:" TextMode="MultiLine" MaxLength="200" Width="80%"></asp:TextBox>
			    </td>
			</tr>
            <tr>
			    <td class="table_body table_body_NoWidth" style="height: 30px">
				    备注：
			    </td>
			    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
				    <asp:TextBox ID="tbRemark" runat="server" title="请输入字符串~50:" TextMode="MultiLine" MaxLength="200" Width="80%"></asp:TextBox>
			    </td>
		    </tr>
		    
		    <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False" OnRowDeleting="gridview_ItemList_RowDeleted"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="gridview_ItemList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="档案附件描述">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="档案附件名称">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_ArchivesAttachmentName" runat="server" Text='<%# Eval("ArchivesAttachmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="档案附件存放路径">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_SavePath" runat="server" Text='<%# Eval("SavePath") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注 ">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                                Text="修改" OnClientClick="javascript:setModalPopup(this.id);" />
                                            <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                                PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_Save"
                                                CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                            </cc2:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未添加档案附件材料</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <input id="Button_AddItem" type="button" runat="server" value="添加" style="display: none"
                                onserverclick="Button_AddItem_Click" />
                            <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                                onserverclick="Button_Save_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
		    
		    <tr>
                <td colspan="4" style="text-align: center">
                    <input id="Button_Select" type="button" runat="server" class="button_bak" value="添加附件"
                        onclick="javascript:showPopWin('添加附件','EditArchivesAttachment.aspx?cmd=add', 900, 240, addtolist,true,true);" />
                    <input id="Hidden_SelectedItem" value="" runat="server" type="hidden" />
                </td>
            </tr>
		    </table>
	    </div>
					
		<table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
			<tr id="Tr1" runat="server">
				<td id="Td1" align="right" style="height: 38px" runat="server">
					<asp:Button ID="btSave" runat="server" CssClass="button_bak" Text="确定" OnClick="btSave_Click" />&nbsp;&nbsp;
					<input id="Reset1" class="button_bak" type="reset" value="重填" />
				</td>
			</tr>
		</table>
	</div>
	
	<script type="text/javascript" language="javascript">
        //添加一个附件
        function addtolist(addstring) {
            document.getElementById('<%= Hidden_SelectedItem.ClientID %>').value = addstring;
            document.getElementById('<%= Button_AddItem.ClientID %>').click();  //触发按钮动作
        }

    </script>
	
</asp:Content>