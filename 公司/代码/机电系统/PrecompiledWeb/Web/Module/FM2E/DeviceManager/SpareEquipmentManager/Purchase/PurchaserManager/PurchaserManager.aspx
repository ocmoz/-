<%@ page title="采购员管理" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaserManager_PurchaserManager, App_Web_j56zncbu" %>
<%@ Import Namespace="WebUtility" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">

        //编辑的时候设置模式对话框的值
        function setModalPopup(button_id) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            //ITEMID
            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_PurchaserID')).value;
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;
            
            //用户名
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_UserID')).innerText;
            document.getElementById('<%= TextBox_UserID.ClientID %>').value = name;

            //姓名
            var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PurchaserName')).innerText;
            document.getElementById('<%= TextBox_PersonName.ClientID %>').value = model;


            //备注
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;
        }

        //保存编辑的项
        function saveEditItem() {

            

            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }



    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件采购"
        HeadOPTxt="目前操作功能：采购员管理">
       
    </cc1:HeadMenuWebControls>
    
    
    <asp:Panel ID="Panel_EditPurchaser" runat="server" Style="width: 95%; height: 200px; display:none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    编辑采购员<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
               &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    用户名：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_UserID" runat="server" ></asp:TextBox>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    姓名：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_PersonName" runat="server"></asp:TextBox>
                </td>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    描述：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>(可编辑)
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_SaveEditPurchaser" class="button_bak"
                type="button" value="保存" onclick="javascript:saveEditItem();"/>
            <asp:Button ID="Button_Cancel_EditPurchaser" runat="server" class="button_bak" 
                Text="取消" />
        </center>
    </asp:Panel>
    
    <div>
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" unat="server">
            <tr>
                <td class="Table_searchtitle" >
                    <%= WebUtility.UserData.CurrentUserData.CompanyName%> 的采购员列表
                </td>
            </tr>
            <tr>
                <td class="table_none_NoWidth">
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gridview_PurchaserList" runat="server" AutoGenerateColumns="False"
                                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" 
                                        HeaderStyle-Height="25px" RowStyle-Height="20px"
                                        Width="98%" HeaderStyle-HorizontalAlign="center" 
                                        RowStyle-HorizontalAlign="center" 
                                        onrowdeleting="gridview_PurchaserList_RowDeleting">
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
     
                                                   <asp:Label Text='<%# Container.DataItemIndex + 1%>' runat="server" ID="Label_Index"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="用户名">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="Hidden_PurchaserID" value='<%# Eval("ID") %>' />
                                                   <asp:Label Text='<%# Eval("UserID") %>' runat="server" ID="Label_UserID"></asp:Label>
                                                    
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="姓名">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_PurchaserName" runat="server" Text='<%# Eval("PurchaserName") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="描述">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                </ItemTemplate>
                                      
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="编辑" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False"
                                                        ImageUrl="~/images/ICON/edit.gif"  OnClientClick="javascript:setModalPopup(this.id);" />
                                                        
                                                     <cc2:ModalPopupExtender ID="ModalPopupExtender_EditPurchaser" runat="server" TargetControlID="ImageButton_Edit"
                                              
                                                                    PopupControlID="Panel_EditPurchaser" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_SaveEditPurchaser"  CancelControlID="Button_Cancel_EditPurchaser" DynamicServicePath=""
                                                                    Enabled="true">
                                             </cc2:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField ShowHeader="False" HeaderText="删除">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False" 
                                                        CommandName="Delete"   ImageUrl="~/images/ICON/delete.gif"  OnClientClick="javascript:return confirm('确认删除采购员？');"></asp:ImageButton>
                                                </ItemTemplate>
                                                 <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <RowStyle Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            暂无采购员，请添加
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                </td>
            </tr>
             <tr>
                <td  style="text-align: center">
                    <input id="Button_Select" type="button" runat="server" class="button_bak" value="添加"
                        onclick="javascript:showPopWin('选择采购员','Select.aspx', 900, 450, addtolist,true,true);" />
                    <input id="Hidden_SelectedUser" value="" runat="server" type="hidden" />
                    <input type="button" style="display:none" id="Button_Save" onserverclick="Button_Save_Click" runat="server"   />
                    <input type="button" style="display:none" id="Button_SaveItem" onserverclick="Button_SaveItem_Click" runat="server"   />
                </td>
            </tr>
        </table>
    </div>
    
    <script type="text/javascript" language="javascript">
        //添加一个采购员，回传的信息包括，采购员的ID和描述
        function addtolist(addstring) {
            document.getElementById('<%= Hidden_SelectedUser.ClientID %>').value = addstring;
            document.getElementById('<%= Button_Save.ClientID %>').click();
        }

    </script>
</asp:Content>
