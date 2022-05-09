<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ArchivesType.aspx.cs" Inherits="Module_FM2E_ArchivesManager_ArchivesTypeManage_ArchivesType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
	
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="档案类型信息管理"  HeadHelpTxt="部门列表默认显示新增档案类型信息" HeadOPTxt="目前操作功能：档案类型信息维护">
		<cc1:HeadMenuButtonItem ButtonName="添加档案类型信息" ButtonIcon="new.gif" ButtonPopedom="New"  ButtonUrl="EditArchivesType.aspx?cmd=add" ButtonUrlType="Href" />
	</cc1:HeadMenuWebControls>
	
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;" >
                    <tr>
                        <td style="width: 20%;" align="left" valign="top">
                            <div>
                                <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                </asp:TreeView>
                            </div>
                        </td>
                        <td valign="top">
	                        <div style="width: 100%; ">
		                        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
			                        <cc2:TabPanel runat="server" HeaderText="档案类型列表" ID="TabPanel1">
				                        <ContentTemplate>
					                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
						                        <EmptyDataRowStyle HorizontalAlign="Center" />
						                        <EmptyDataTemplate>
							                        没有信息
						                        </EmptyDataTemplate>
						                        <Columns>
							                        <asp:BoundField DataField="ArchivesTypeName" HeaderText="档案类型名称" />
							                        <asp:BoundField DataField="Description" HeaderText="档案类型描述" />
							                        <asp:BoundField DataField="ParentName" HeaderText="父节点名称" />
							                        <asp:BoundField DataField="Level" HeaderText="层次" />
							                        <asp:BoundField DataField="ChildCount" HeaderText="子节点个数" />
							                        <asp:BoundField DataField="Remark" HeaderText="描述" />
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
								                        档案类型名称：
							                        </td>
							                        <td class="table_none table_none_NoWidth" style="height: 30px">
								                        <asp:TextBox ID="tbArchivesTypeName" runat="server"></asp:TextBox>
							                        </td>
							                        <td class="table_body table_body_NoWidth" style="height: 30px">
								                        档案类型描述：
							                        </td>
							                        <td class="table_none table_none_NoWidth" style="height: 30px">
								                        <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
							                        </td>
						                        </tr>
						                        <tr>
							                        <td class="table_body table_body_NoWidth" style="height: 30px">
								                        父节点ID：
							                        </td>
							                        <td class="table_none table_none_NoWidth" style="height: 30px">
								                        <asp:TextBox ID="tbParentID" runat="server"></asp:TextBox>
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
	
	                </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
