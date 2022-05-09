<%@ Page Title="打印维护表" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="MaintainTemplatePrint.aspx.cs" Inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainTemplate_MaintainTemplatePrint" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>js/Common.js"></script>
   <p></p>
    <div style="width: 100%; text-align:center;">
        <table width="649px" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr><td colspan="4" class="table_body_WithoutWidth">维护单：<asp:Label ID="Label_TemplateSheetName" runat="server"></asp:Label> </td></tr>
            <tr>
                <td class="table_body table_body_NoWidth"  style="height: 30px; color: Black; background-color: White;">
                    系统：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; color: Black; background-color: White;">
                    <asp:Label ID="Label_SystemName" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; color: Black; background-color: White;">
                    制定部门：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; color: Black; background-color: White;">
                    <asp:Label ID="Label_DeparmentName" runat="server"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; color: Black; background-color: White;">
                    类型：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; color: Black; background-color: White;">
                   <asp:Label ID="Label_TypeName" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; color: Black; background-color: White;">
                    周期：
                </td>
                <td class="table_none table_none_NoWidth"  style="height: 30px; color: Black; background-color: White;">
                   <asp:Label ID="Label_Period" runat="server"></asp:Label> 
                </td>
               
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; color: Black; background-color: White;">
                   地址：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; color: Black; background-color: White;">
                   <asp:Label ID="Label_AddressName" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; color: Black; background-color: White;">
                    临时：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; color: Black; background-color: White;">
                   <asp:Image ID="Image_Temp" runat="server" ImageUrl="~/images/right.gif" Visible="false" />
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; color: Black; background-color: White;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" colspan="3">
                   <asp:Label ID="Label_Remark" runat="server"></asp:Label> 
                </td>
            </tr>

          
        </table>
                    
                     <table width="649px" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
                      <tr>
                <td class="table_body_WithoutWidth" colspan='<%= CountPerRow %>' style="height: 30px; color: Black; background-color: White;">
                    维护设备
                </td>
               
            </tr>
                    
                    <asp:Repeater ID="Repeater_EquipmentList" runat="server">
                    
                    
                    <ItemTemplate>
                    
                    <%# Container.ItemIndex % CountPerRow ==0?"<tr>":""%> 
                    
                    <td style="text-align:left">
                    <input type="checkbox" />
                        <%# Eval("EquipmentName") %><br />
                        [<%# Eval("DetailLocation") %>]
                    </td>
                    
                    <%# Container.ItemIndex % CountPerRow == (CountPerRow-1) ?"</tr>":""%> 
                    
                    </ItemTemplate>
                    
                    </asp:Repeater>
                    
                    </table>
           

       
    </div>
    <object ID='WebBrowser' WIDTH=0 HEIGHT=0 CLASSID='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'></object>
 <script language="javascript" type="text/javascript">
     PrintPreview('WebBrowser');
</script>
</asp:Content>
