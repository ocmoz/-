<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_ArchivesManager_ArchivesManage_ViewArchives, App_Web_wzudfcjp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="管理"  HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：资料查看">
		<cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
		<cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
		<cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript" ButtonUrl="window.history.go(-1);" />
	</cc1:HeadMenuWebControls>
	<div style="width: 95%; ">
		<table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
			<tr>
				<td class="Table_searchtitle" colspan="4">
					基本信息
				</td>
			</tr>
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					档案ID：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
					<asp:Label ID="lbArchivesID" runat="server" Text=""></asp:Label>
				</td>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					档案名称：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
					<asp:Label ID="lbArchivesName" runat="server" Text=""></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					档案类型：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
					<asp:Label ID="lbArchivesTypeName" runat="server" Text=""></asp:Label>
				</td>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					涉及系统：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
					<asp:Label ID="lbInvolvedSystem" runat="server" Text=""></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					涉及设备：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
					<asp:Label ID="lbInvolvedEquipment" runat="server" Text=""></asp:Label>
				</td>
				<td class="table_body table_body_NoWidth" style="height: 30px">
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px">
				</td>
			</tr>
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					档案描述：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
					<asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="table_body table_body_NoWidth" style="height: 30px">
					备注：
				</td>
				<td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
					<asp:Label ID="lbRemark" runat="server" Text=""></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="Table_searchtitle" colspan="4">
					档案附件列表
				</td>
			</tr>
			<tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px"
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
                                     <asp:TemplateField HeaderText="档案附件存放路径">
                                        <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_ArchivesAttachmentFile" Font-Underline="true" ForeColor="Blue" runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注 ">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未添加档案附件材料</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
		</table>
	</div>
</asp:Content>

