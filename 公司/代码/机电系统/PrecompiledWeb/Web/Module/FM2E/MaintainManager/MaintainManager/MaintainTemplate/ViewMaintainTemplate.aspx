<%@ page title="查看维护计划" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainTemplate_ViewMaintainTemplate, App_Web_9mbj7h6r" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="查看维护计划"
        HeadOPTxt="目前操作功能：查看维护计划" HeadHelpTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="打印" ButtonUrlType="JavaScript"
            ButtonPopedom="Print" ButtonUrl="alert('print')" />
        <cc1:HeadMenuButtonItem ButtonIcon="move.gif" ButtonName="执行" ButtonUrlType="Href"
            ButtonPopedom="New" ButtonUrl="" />
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonUrlType="Href"
            ButtonPopedom="Edit" ButtonUrl="" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonUrlType="JavaScript"
            ButtonPopedom="Delete" ButtonUrl="" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <input type="button" style="display:none" runat="server" value="删除"  id="Button_Delete" onserverclick="Button_Delete_Click" />
    <div style="width: 100%;">
        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr><td colspan="4" class="table_body_WithoutWidth">维护单：<asp:Label ID="Label_TemplateSheetName" runat="server"></asp:Label> </td></tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    系统：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_SystemName" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    制定部门：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_DeparmentName" runat="server"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    类型：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                   <asp:Label ID="Label_TypeName" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    周期：
                </td>
                <td class="table_none table_none_NoWidth"  style="height: 30px">
                   <asp:Label ID="Label_Period" runat="server"></asp:Label> 
                </td>
               
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                   地址：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                   <asp:Label ID="Label_AddressName" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    临时：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                   <asp:Image ID="Image_Temp" runat="server" ImageUrl="~/images/right.gif" Visible="false" />
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" colspan="3">
                   <asp:Label ID="Label_Remark" runat="server"></asp:Label> 
                </td>
            </tr>

          
        </table>
                    
                     <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
                      <tr>
                <td class="table_body_WithoutWidth" colspan='<%= CountPerRow %>'>
                    维护设备
                </td>
               
            </tr>
                    
                    <asp:Repeater ID="Repeater_EquipmentList" runat="server">
                    
                    
                    <ItemTemplate>
                    
                    <%# Container.ItemIndex % CountPerRow ==0?"<tr>":""%> 
                    
                    <td >
                        <%# Eval("EquipmentName") %><br />
                        [<%# Eval("AddressName") %><%# Eval("DetailLocation") %>]
                    </td>
                    
                        <%# Container.ItemIndex % CountPerRow == (CountPerRow-1) ?"</tr>":""%> 
                    
                    </ItemTemplate>
                    
                    </asp:Repeater>
                    
                    </table>
           

       
    </div>
</asp:Content>
