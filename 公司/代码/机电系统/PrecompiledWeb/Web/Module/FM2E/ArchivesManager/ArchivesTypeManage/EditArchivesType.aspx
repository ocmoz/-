<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_ArchivesManager_ArchivesTypeManage_EditArchivesType, App_Web_egf41mgb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
	 <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="管理"  HeadHelpTxt="点击进入管理" HeadOPTxt="目前操作功能：列表">
		<cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="取消修改" ButtonPopedom="List" ButtonUrlType="Href" />
	</cc1:HeadMenuWebControls>
	<div style="width: 95%; ">
		<cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
			<cc2:TabPanel runat="server" HeaderText="基本信息" ID="TabPanel1">
				<HeaderTemplate>
					基本信息
				</HeaderTemplate>
				<ContentTemplate>
					<div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
						<table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								档案名称：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbArchivesTypeName" runat="server" title="请输入字符串~20:"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								档案描述：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbDescription" runat="server" title="请输入字符串~200:"></asp:TextBox>
							</td>
						</tr>
						<tr>
						    <td class="table_body table_body_NoWidth" style="height: 30px">
                                上级档案类型：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px; color: #FF0000;">
                                <asp:TextBox onfocus="javascript:causeValidate=false;" ID="tbParentName" runat="server"
                                    AutoPostBack="True"></asp:TextBox>
                                点击修改上级档案类型
                                <asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                            </asp:TreeView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="tbParentName"
                                    PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                    ExtenderControlID="">
                                </cc2:PopupControlExtender>
                            </td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								备注：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbRemark" runat="server" title="请输入字符串~50:"></asp:TextBox>
							</td>
						</tr>
                        <tr style="display: none">
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                子部门数：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbChildCount" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                            </td>
                        </tr>
						</table>
					</div>
				</ContentTemplate>
			</cc2:TabPanel>
		</cc2:TabContainer>
		<table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
			<tr id="Tr1" runat="server">
				<td id="Td1" align="right" style="height: 38px" runat="server">
					<asp:Button ID="btSave" runat="server" CssClass="button_bak" Text="确定" OnClick="btSave_Click" />&nbsp;&nbsp;
					<input id="Reset1" class="button_bak" type="reset" value="重填" />
				</td>
			</tr>
		</table>
	</div>
</asp:Content>

