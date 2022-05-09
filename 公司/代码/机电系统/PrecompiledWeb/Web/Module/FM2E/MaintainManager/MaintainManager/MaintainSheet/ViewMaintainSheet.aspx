<%@ page title="查看维护记录" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainSheet_ViewMaintainSheet, App_Web_ltngjpvs" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="查看维护记录"
        HeadOPTxt="目前操作功能：查看维护记录" HeadHelpTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="打印" ButtonUrlType="JavaScript"
            ButtonPopedom="Print" ButtonUrl="alert('print')" />
      
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
            <tr><td colspan="4"  class="table_body_WithoutWidth">维护单：<asp:Label ID="Label_TemplateSheetName" runat="server"></asp:Label> </td></tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    系统：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_SystemName" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    维护部门：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_DeparmentName" runat="server"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    维护人：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_Maintainer" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    维护时间：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_MaintainTime" runat="server"></asp:Label> 
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
                    <asp:Image ID="Image_IsTemp" runat="server" ImageUrl="~/images/right.gif" Visible="false"/>
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
                <td colspan='<%= CountPerRow %>'  class="table_body_WithoutWidth">
                    维护设备
                </td>
               
            </tr>
           
                    
                    <asp:Repeater ID="Repeater_EquipmentList" runat="server">
                    
                    
                    <ItemTemplate>
                    
                    <%# Container.ItemIndex % CountPerRow ==0?"<tr>":""%> 
                    
                    <td>
                        <%# Eval("EquipmentName") %> 
                        
                        <%# (bool)Eval("IsNormal") ? "<font color='black'>" : "<font color='red'>"%>
                        
                        <%# EnumHelper.GetDescription((Enum)Eval("NewStatus")) %>
                        </font>
                        <br />
                        [<%# Eval("AddressName") %><%# Eval("DetailLocation") %>]
                    </td>
                    
                    <%# Container.ItemIndex % CountPerRow == (CountPerRow-1) ?"</tr>":""%> 
                    
                    </ItemTemplate>
                    
                    </asp:Repeater>
                 </table>
      <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">              
            <tr>
                <td colspan='4'  class="table_body_WithoutWidth">
                  <font color="red"> 异常设备</font>
                </td>
               
            </tr>
            
 <tr>
            <td colspan='4'>
            
            <div style="width: 100%;">
                        <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                            HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                             HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                             
                             
                           >
                            <Columns>
                               
                                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EquipmentName" HeaderText="设备名称">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EquipmentModel" HeaderText="型号">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="DetailLocation" HeaderText="安装位置">
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        状态</HeaderTemplate>
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                        <%# EnumHelper.GetDescription((FM2E.Model.Equipment.EquipmentStatus)Eval("NewStatus")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="Remark" HeaderText="备注">
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                               
                               
                               
                            </Columns>
                            <EmptyDataTemplate>
                                没有异常设备
                            </EmptyDataTemplate>
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        </asp:GridView>
                    </div>
            </td>
            
            </tr>

             <tr>
                <td colspan="4" class="table_body_WithoutWidth">
                   核实信息
                </td>
               
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    核实结果：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_ConfirmResult" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_ConfirmRemark" runat="server"></asp:Label> 
                </td>
            </tr>
             <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    核实人：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_Confirmer" runat="server"></asp:Label> 
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    时间：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:Label ID="Label_ConfirmTime" runat="server"></asp:Label> 
                </td>
            </tr>
        </table>
       
    </div>
</asp:Content>
