<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewSubsidiaryEquipment.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_SubsidiaryEquipmentManager_ViewSubsidiaryEquipment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备配套设施管理"  HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：设备配套设施资料查看">
		<cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
		<cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
		<cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript" ButtonUrl="window.history.go(-1);" />
	</cc1:HeadMenuWebControls>
	<div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="设备配套设施基本信息" ID="TabPanel1">
                <ContentTemplate>
		            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
			            <tr>
				            <td class="Table_searchtitle" colspan="4">
					            设备配套设施基本信息
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            设备配套设施ID：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbSubsidiaryEquipmentID" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            设备配套设施编号：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbSubsidiaryEquipmentNO" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            设备配套设施名称：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbName" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            公司：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbCompanyName" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            系统：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbSystemName" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            型号：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbModel" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            品牌：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbSpecification" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            设备类型：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbAssertNumber" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            地址：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbAddressName" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            安装地点：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbDetailLocation" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            价格：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbPrice" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            状态：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            建档日期：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbFileDate" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            维修次数：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbMaintenanceTimes" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
			                <td class="table_body table_body_NoWidth" style="height: 30px">
					            更新日期：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbUpdateTime" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            备注：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbRemark" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
		            </table>
		        </ContentTemplate>
		    </cc2:TabPanel>
		    <cc2:TabPanel runat="server" HeaderText="维修记录" ID="TabPanel2">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
                            <EmptyDataTemplate>
                                没有维修信息
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="故障处理单">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltSheetNOTxt" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DepartmentName" HeaderText="故障部门" />
                                <asp:TemplateField HeaderText="维修费用（元）">
                                    <ItemTemplate>
                                        <%# Eval("MaintainFee", "{0:#,0.##}") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbTotalFee" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        维修结果</HeaderTemplate>
                                    <ItemTemplate>
                                        <%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MaintainDeptName" HeaderText="维修单位" />
                                <asp:BoundField DataField="MaintainDate" HeaderText="维修时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                        <asp:Label ID="Label_MalFunctionRecordError" ForeColor="Red" Visible="false" Text="维修记录读取失败" runat="server"></asp:Label>

                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
	</div>
</asp:Content>
