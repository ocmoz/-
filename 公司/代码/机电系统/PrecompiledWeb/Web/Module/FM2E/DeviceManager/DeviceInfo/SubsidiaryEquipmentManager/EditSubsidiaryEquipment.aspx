<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_SubsidiaryEquipmentManager_EditSubsidiaryEquipment, App_Web_x4wrinz-" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
	 
	<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
	 
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备配套设施管理"  HeadHelpTxt="点击进入管理" HeadOPTxt="目前操作功能：列表">
		<cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="设备配套设施列表" ButtonPopedom="List"  ButtonUrlType="href" ButtonUrl="SubsidiaryEquipment.aspx" />
		<cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"  ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
	</cc1:HeadMenuWebControls>
	<div style="width: 95%; ">
		<cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
			<cc2:TabPanel runat="server" HeaderText="设备配套设施基本信息" ID="TabPanel1">
				<HeaderTemplate>
					设备配套设施基本信息
				</HeaderTemplate>
				<ContentTemplate>
					<div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
						<table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								名称：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbName" runat="server" title="请输入字符串~20:"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								价格：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbPrice" runat="server" title="请输入整数~float">0</asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								公司：
							</td>
							<td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DDL_Company" runat="server">
                                </asp:DropDownList>
                            </td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
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
								<asp:TextBox ID="tbModel" runat="server" title="请输入字符串~20:"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								品牌：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbSpecification" runat="server" title="请输入字符串~60:"></asp:TextBox>
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
								<asp:TextBox ID="tbAssertNumber" runat="server" title="请输入字符串~50:"></asp:TextBox>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								维修次数：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbMaintenanceTimes" runat="server" title="请输入整数~int">0</asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								设备状态：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
							</td>
							<td class="table_body table_body_NoWidth" style="height: 30px">
								备注：
							</td>
							<td class="table_none table_none_NoWidth" style="height: 30px">
								<asp:TextBox ID="tbRemark" runat="server" title="请输入字符串~100:"></asp:TextBox>
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
