<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_ConsumableEquipmentManager_ConsumableEquipment, App_Web_ftqgn5cp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备易耗品信息管理"  HeadHelpTxt="点击进入设备易耗品信息管理" HeadOPTxt="目前操作功能：设备易耗品信息列表">
		<cc1:HeadMenuButtonItem ButtonName="设备易耗品信息添加" ButtonIcon="new.gif" ButtonPopedom="New"  ButtonUrl="EditConsumableEquipment.aspx?cmd=add" ButtonUrlType="Href" />
		<cc1:HeadMenuButtonItem ButtonName="导入设备易耗品信息" ButtonIcon="xls.gif" ButtonPopedom="New"  ButtonUrl="ImportConsumableEquipment.aspx" ButtonUrlType="Href" />
	</cc1:HeadMenuWebControls>
	<div style="width: 95%; ">
		<cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
			<cc2:TabPanel runat="server" HeaderText="设备易耗品信息列表" ID="TabPanel1">
				<ContentTemplate>
					<asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
						<EmptyDataRowStyle HorizontalAlign="Center" />
						<EmptyDataTemplate>
							没有设备易耗品信息信息
						</EmptyDataTemplate>
						<Columns>
							<asp:BoundField DataField="Name" HeaderText="名称" />
							<asp:BoundField DataField="ConsumableEquipmentNO" HeaderText="条形码" />
							<asp:BoundField DataField="Model" HeaderText="型号" />
							<asp:BoundField DataField="Unit" HeaderText="单位" />
							<asp:BoundField DataField="Count" HeaderText="数量" />
							<asp:BoundField DataField="Price" HeaderText="单价" />
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
					 <div style="text-align: left;">当前设备总量为：<asp:Label ID="lbCurrentDeviceCount" runat="server"></asp:Label></div>
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
								易耗品信息条码：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbConsumableEquipmentNO" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								易耗品信息名称：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbName" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								所属系统：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbSystemID" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								设备类型：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbSerialNum" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								型号：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbModel" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								品牌：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbSpecification" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								资产编号：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbAssertNumber" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								单位：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbUnit" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								数量：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbCount" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								价格：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbPrice" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								维修次数：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbMaintenanceTimes" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								备注：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbRemark" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								维修单位：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:DropDownList ID="ddlMaintainTeam" runat="server">
                                </asp:DropDownList>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								所属公司：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:DropDownList ID="DDLCompany" runat="server">
                                        <asp:ListItem Value="" Text="不限"></asp:ListItem>
                                    </asp:DropDownList>
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
