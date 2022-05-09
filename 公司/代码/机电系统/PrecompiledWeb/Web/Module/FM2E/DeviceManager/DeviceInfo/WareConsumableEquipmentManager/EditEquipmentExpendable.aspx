<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_EditEquipmentExpendable, App_Web_7bfrofdf" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

	<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库设备易耗品信息管理"  HeadHelpTxt="点击进入仓库设备易耗品信息管理" HeadOPTxt="目前操作功能：仓库设备易耗品信息列表">
		<cc1:HeadMenuButtonItem ButtonIcon="xls.gif" ButtonName="批量导入" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="ImportEquipmentExpendable.aspx" />
		<cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="仓库设备易耗品信息列表" ButtonPopedom="List"  ButtonUrlType="href" ButtonUrl="EquipmentExpendable.aspx" />
		<cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"  ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
	</cc1:HeadMenuWebControls>
	<div style="width: 95%; ">
	
	    <asp:UpdatePanel ID="updatePanelEquipments" runat="server">
        <ContentTemplate>
                    
	    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
	        <tr>
	            <td class="table_body table_body_NoWidth" style="height: 30px">
			        仓库设备易耗品名称：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height: 30px">
			        <asp:TextBox ID="tbName" runat="server" title="请输入字符串~20:"></asp:TextBox>
		        </td>
		         <td class="table_body table_body_NoWidth">
                    所属系统：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:DropDownList ID="DDL_System" runat="server" title="请选择所属系统~!">
                    </asp:DropDownList>
                </td>
	        </tr>
	        <tr>
	            <td class="table_body table_body_NoWidth" style="height: 30px">
			        价格：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height: 30px">
			        <asp:TextBox ID="tbPrice" runat="server" title="请输入整数~float">0</asp:TextBox>
		        </td>
		        <td class="table_body table_body_NoWidth" style="height: 30px">
			        设备类型：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height: 30px">
			        <asp:TextBox ID="tbSerialNum" runat="server" title="请输入字符串~30:"></asp:TextBox>
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
		        <td class="table_body table_body_NoWidth" style="height: 30px">
			        资产编号：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height: 30px">
			        <asp:TextBox ID="tbAssertNumber" runat="server" title="请输入字符串~50:"></asp:TextBox>
		        </td>
		        <td class="table_body table_body_NoWidth" style="height: 30px">
                    单位：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="DDL_Unit" runat="server" title="请选择单位~"></asp:DropDownList>
                </td>
	        </tr>
	        <tr>
		        <td class="table_body table_body_NoWidth" style="height: 30px">
			        数量：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height: 30px">
			        <asp:TextBox ID="tbCount" runat="server" title="请输入整数~int">1</asp:TextBox>
		        </td>
		         <td class="table_body table_body_NoWidth" style="height: 30px">
			        保险库存：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height: 30px">
			        <asp:TextBox ID="tbProduceID" runat="server" title="请输入整数~int">0</asp:TextBox>
		        </td>
	        </tr>
	        <tr>
		        <td class="table_body table_body_NoWidth" style="height: 30px">
			        备注：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height: 30px">
			        <asp:TextBox ID="tbRemark" runat="server" title="请输入字符串~100:" MaxLength="200"></asp:TextBox>
		        </td>
		        <td class="table_body table_body_NoWidth" style="height:30px">
		        仓库：
		        </td>
		        <td class="table_none table_none_NoWidth" style="height:30px">
		        <asp:DropDownList ID="DropDownList_FilterWareHouse" runat="server" title="请选择仓库~!"
                        AutoPostBack="true" >
		        </asp:DropDownList><asp:Label runat="server" ID="Lable_Warehouse" 
                        ForeColor="#FF3300" ></asp:Label>
		        </td>
	        </tr>
	    </table>
	    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;"> 
            <tr>
                <td >
                    
                    <asp:GridView ID="GridViewEquipmentDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowCommand="gvMaintainedEquipmentDetail_RowCommand" OnRowDataBound="gvMaintainedEquipmentDetail_RowDataBound"
                        ShowFooter="true">
                        <EmptyDataTemplate>
                            暂未添加仓库设备易耗品分布列表
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    序号</HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %></ItemTemplate>
                                <ItemStyle Width="4%"/>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AddressName" HeaderText="地址名称">
                                <ItemStyle Width="10%"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="DetailLocation" HeaderText="安装地址">
                                <ItemStyle Width="13%"/>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="数量">
                                <ItemTemplate>
                                    <%# Eval("Count")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbTotalCount" runat="server"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle Width="12%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                <ItemStyle Width="13%"/>
                            </asp:BoundField>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                删除</HeaderTemplate>
                            <ItemStyle Width="4%" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此零件吗？')"
                                    CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                    
                </td>
            </tr>
	    </table>
	    
	    </ContentTemplate>
        </asp:UpdatePanel>
	    
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


