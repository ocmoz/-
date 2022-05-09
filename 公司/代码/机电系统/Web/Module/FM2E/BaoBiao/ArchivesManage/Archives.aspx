<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Archives.aspx.cs" Inherits="Module_FM2E_ArchivesManager_ArchivesManage_Archives" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="档案录入管理"  HeadHelpTxt="点击进入档案录入" HeadOPTxt="目前操作功能：档案列表">
		<cc1:HeadMenuButtonItem ButtonName="档案录入" ButtonIcon="new.gif" ButtonPopedom="New"  ButtonUrl="EditArchives.aspx?cmd=add" ButtonUrlType="Href" />
	</cc1:HeadMenuWebControls>


	<div style="width: 95%; ">
		<cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
			<cc2:TabPanel runat="server" HeaderText="档案信息列表" ID="TabPanel1">
				<ContentTemplate>
				    <table width="100%">
                        <tr>
                            <td valign="top"  style="width:20%">
                                <div style="width: 100%; overflow: auto; height: 100%; border: solid 1px #ffffff;">
                                    
                                    <asp:TreeView ID="ArchivesTypeTree" runat="server" ShowLines="true" OnSelectedNodeChanged="ArchivesTypeTree_SelectedNodeChanged"
                                        OnTreeNodePopulate="ArchivesTypeTree_TreeNodePopulate">
                                        <SelectedNodeStyle ForeColor="#FF5050" />
                                    </asp:TreeView>
                                </div>
                            </td>
                            <td  valign="top" style="width:80%">
					            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
						            <EmptyDataRowStyle HorizontalAlign="Center" />
						            <EmptyDataTemplate>
							            没有信息
						            </EmptyDataTemplate>
						            <Columns>
							            <asp:BoundField DataField="ArchivesName" HeaderText="档案名称" />
							            <asp:BoundField DataField="ArchivesTypeName" HeaderText="档案类型名称" />
							            <asp:BoundField DataField="InvolvedSystem" HeaderText="涉及系统" />
							            <asp:BoundField DataField="InvolvedEquipment" HeaderText="涉及设备" />
							            <asp:BoundField DataField="Description" HeaderText="档案描述" />
							            <asp:BoundField DataField="Remark" HeaderText="备注" />
							            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"  CommandName="view">
								            <HeaderStyle Width="60px" />
							            </asp:ButtonField>
							            <asp:TemplateField>
								            <ItemStyle Width="60px" />
								            <ItemTemplate>
									             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此模块信息吗？')" CausesValidation="false" />
								            </ItemTemplate>
							            </asp:TemplateField>
						            </Columns>
						            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
						            <RowStyle HorizontalAlign="Center" Height="20px" />
					            </asp:GridView>
					            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
					            </cc1:AspNetPager>
					        </td>
					    </tr>
					</table>
				</ContentTemplate>
			</cc2:TabPanel>
			<cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="查询">
				<ContentTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
						<tr>
							<td class="Table_searchtitle" colspan="4">
								组合查询（支持模糊查询）
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								档案名称：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbArchivesName" runat="server"></asp:TextBox>
							</td>
							 <td class="table_body table_body_NoWidth" style="height: 30px">
                                档案类型：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px; color: #FF0000;">
                                <asp:TextBox onfocus="javascript:causeValidate=false;" ID="tbArchivesTypeName" runat="server"
                                    AutoPostBack="True"></asp:TextBox>
                                点击选择档案类型
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
                            </td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								涉及系统：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbInvolvedSystem" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								涉及设备：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbInvolvedEquipment" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								档案描述：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								备注：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbRemark" runat="server"></asp:TextBox>
							</td>
						</tr>
					</table>
					<table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton" runat="server">
						<tr>
							<td align="right" style="height: 38px">
								<asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="BtnSearch_Click" />
							</td>
						</tr>
					</table>
				</ContentTemplate>
			</cc2:TabPanel>
		</cc2:TabContainer>
	</div>
</asp:Content>

