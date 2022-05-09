<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" 
EnableEventValidation="false" CodeFile="EquipmentExpendableRecord.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_EquipmentExpendableRecord" Title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript" language="javascript">
           function WriteSelect(file_name)
            {
                if (file_name!=undefined)
                {
                    var ShValues = file_name.split('||');
                    switch(ShValues[2])
                    {
                        default:break;
    	            
                    }
	            }
	        }
	        function cancel() {
	            window.location.href = "EquipmentExpendable.aspx";
	        }
     </script>
     


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server" Style="display: none; width: 300px">
        <asp:Image ID="Image1" runat="server" Width="300px" />
    </asp:Panel>
    <div style="width: 100%;">
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
                    <asp:DropDownList ID="DDL_System" runat="server">
                        <asp:ListItem Value="">--请选择--</asp:ListItem>
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
		        <asp:DropDownList ID="DropDownList_FilterWareHouse" runat="server" 
                        AutoPostBack="true" 
                        onselectedindexchanged="DropDownList_FilterWareHouse_SelectedIndexChanged" >
		        <asp:ListItem Value="select">--请选择--</asp:ListItem>
		        </asp:DropDownList>
		        </td>
	        </tr>
	    </table>
        <center>
            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
            <input id="Reset1" class="button_bak" type="button" value="取消" onclick="javascript:cancel();" />
        </center>
    </div>
</asp:Content>
