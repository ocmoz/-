<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SubsidiaryEquipment.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_SubsidiaryEquipmentManager_SubsidiaryEquipment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备配套设施管理"  HeadHelpTxt="点击进入设备配套设施管理" HeadOPTxt="目前操作功能：设备配套设施列表">
		<cc1:HeadMenuButtonItem ButtonName="添加设备配套设施" ButtonIcon="new.gif" ButtonPopedom="New"  ButtonUrl="EditSubsidiaryEquipment.aspx?cmd=add" ButtonUrlType="Href" />
	</cc1:HeadMenuWebControls>
	<div style="width: 95%; ">
		<cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
			<cc2:TabPanel runat="server" HeaderText="设备配套设施列表" ID="TabPanel1">
				<ContentTemplate>
					<asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
						<EmptyDataRowStyle HorizontalAlign="Center" />
						<EmptyDataTemplate>
							没有设备配套设施信息
						</EmptyDataTemplate>
						<Columns>
							<asp:BoundField DataField="Name" HeaderText="名称" />
							<asp:BoundField DataField="CompanyName" HeaderText="公司" />
							<asp:BoundField DataField="SystemName" HeaderText="系统" />
							<asp:BoundField DataField="Model" HeaderText="型号" />
							<asp:BoundField DataField="Specification" HeaderText="规格" />
							<asp:BoundField DataField="AddressName" HeaderText="地址" />
							<asp:BoundField DataField="CatalogName" HeaderText="类型" />
							<asp:BoundField DataField="AssertNumber" HeaderText="序列号" />
							<asp:BoundField DataField="Price" HeaderText="价格" />
							<asp:TemplateField>
								<HeaderTemplate>状态</HeaderTemplate>
								<ItemTemplate>
									<%#EnumHelper.GetDescription((Enum)Eval("Status")) %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="MaintenanceTimes" HeaderText="维修次数" />
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
								配套设施NO：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbSubsidiaryEquipmentNO" runat="server"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								配套设施名称：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbName" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth">
                                公司：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DDL_Company" runat="server">
                                </asp:DropDownList>
                            </td>
							 <td class="table_body table_body_NoWidth">
                                所属系统：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DDL_System" runat="server">
                                </asp:DropDownList>
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
                            <td class="table_body table_body_NoWidth">
                                地址信息：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <input id="TextBox_Address" type="text" style="width: 70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);" />
                                <input type="hidden" id="Hidden_AddressID" runat="server" />
                                <input class="cbutton" onclick="javascript:clearAddress();" type="button" value="清除"
                                    id="Button_ClearAddress" />
                                <asp:TextBox ID="TextBox_DetailLocation" Width="20%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								设备类型：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbAssertNumber" runat="server"></asp:TextBox>
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
								状态：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:DropDownList ID="ddlStatusType" runat="server"></asp:DropDownList>
							</td>
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
	
    <script language="javascript" type="text/javascript">
    //地址选定
    function addAddress(val) {
        var arr = new Array;
        arr = val.split('|');
        var addid = arr[0];
        var addcode = arr[1];
        var addname = arr[2];
        if (addcode != '00') {
            document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
            document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
        }
    }
    function clearAddress() {
        document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '';
        document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
    }
    </script>
    
</asp:Content>

