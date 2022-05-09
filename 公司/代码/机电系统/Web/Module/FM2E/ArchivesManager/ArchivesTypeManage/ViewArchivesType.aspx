<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ViewArchivesType.aspx.cs" Inherits="Module_FM2E_ArchivesManager_ArchivesTypeManage_ViewArchivesType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="管理"  HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：资料查看">
		<cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
		<cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" />
		<cc1:HeadMenuButtonItem ButtonName="档案类型列表信息" ButtonIcon="list.gif" ButtonPopedom="list"  ButtonUrl="ArchivesType.aspx" ButtonUrlType="Href" />
	</cc1:HeadMenuWebControls>
	
	 <table style="height: 100%; width: 100%;">
        <tr>
            <td style="width: 20%;" align="left" valign="top">
                <div>
                    <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <SelectedNodeStyle ForeColor="#FF3300" />
                    </asp:TreeView>
                </div>
            </td>
            <td valign="top">
	            <div style="width: 95%; ">
		            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
			            <tr>
				            <td class="Table_searchtitle" colspan="4">
					            基本信息
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            档案类型ID：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbArchivesTypeID" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            档案类型名称：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbArchivesTypeName" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            档案类型描述：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            父节点档案名称：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbParentName" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            节点层次：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbLevel" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            子节点数量：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbChildCount" runat="server" Text=""></asp:Label>
				            </td>
			            </tr>
			            <tr>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
					            备注：
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
					            <asp:Label ID="lbRemark" runat="server" Text=""></asp:Label>
				            </td>
				            <td class="table_body table_body_NoWidth" style="height: 30px">
				            </td>
				            <td class="table_none table_none_NoWidth" style="height: 30px">
				            </td>
			            </tr>
		            </table>
	            </div>
            </td>
        </tr>
    </table>
</asp:Content>

