<%@ Page Title="专项工程进场设备" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="EditDevice.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_EditDevice" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">

        //编辑的时候设置模式对话框的值
        function setModalPopup(button_id,edit) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            //ITEMID
            if (edit) {
                var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_ItemID')).value;
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;
                
                //产品名称
                var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DeviceName')).innerText;
                document.getElementById('<%= TextBox_Equipment.ClientID %>').value = name;

                //规格型号
                var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Model')).innerText;
                document.getElementById('<%= TextBox_Model.ClientID %>').value = model;

                //尺寸
                var price = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Size')).innerText;
                document.getElementById('<%= TextBox_Size.ClientID %>').value = price;

                //功能
                var unit = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Usage')).innerText;
                document.getElementById('<%= TextBox_Usage.ClientID %>').value = unit;

                //状况
                var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Status')).innerText;
                document.getElementById('<%= TextBox_Status.ClientID %>').value = remark;
                
                
                //数量
                var count = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PlanCount')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_PlanCount.ClientID %>').value = count;
                
                document.getElementById('Button_Save').value = "保存";
            }
            else {
                
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = 0;

               
                document.getElementById('<%= TextBox_Equipment.ClientID %>').value = "";

              
                document.getElementById('<%= TextBox_Model.ClientID %>').value = "";


                document.getElementById('<%= TextBox_Size.ClientID %>').value = "";


                document.getElementById('<%= TextBox_Usage.ClientID %>').value = "";


                document.getElementById('<%= TextBox_Status.ClientID %>').value = "";


                document.getElementById('<%= TextBox_PlanCount.ClientID %>').value = "";

                document.getElementById('Button_Save').value = "添加";
            }
        }

        //保存编辑的项
        function saveEditItem() {

            //产品名称
            var name = trim(document.getElementById('<%= TextBox_Equipment.ClientID %>').value);
            if (name.length == 0) {
                alert('请输入设备');
                return false;
            }
            //规格型号
            var model = trim(document.getElementById('<%= TextBox_Model.ClientID %>').value);
            if (model.length == 0) {
                alert('请输入规格型号');
                return;
            }
            //数量
            var count = trim(document.getElementById('<%= TextBox_PlanCount.ClientID %>').value);
            if (!checkFloat(count, '标书数量')) {
                return;
            }


            document.getElementById('Button_OK').click();
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }

        //浮点数检查
        function checkFloat(value, text) {
            var floatVal = parseFloat(value);
            if (isNaN(floatVal) || floatVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个数字。");
                return false;
            }
            return true;
        }


    </script>
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：进场设备计划" HeadHelpTxt="制定进场设备计划，以便在设备进场时候方便登记">
       <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="ViewProject.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    
    
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 200px;display:none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    设备进场<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    设备：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_Equipment" runat="server" ></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    规格型号：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
        <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    尺寸：</td> <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_Size" runat="server"></asp:TextBox>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    功能：</td><td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Usage" runat="server" ></asp:TextBox>
                </td>
           </tr>
           <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    状况：</td> <td class="table_none table_none_NoWidth" style="height: 30px;" >
                    <asp:TextBox ID="TextBox_Status" runat="server"></asp:TextBox>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    标书数量：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_PlanCount" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save" class="button_bak"
                type="button" value="保存" onclick="javascript:saveEditItem();"/>
            <input id="Button_OK" class="button_bak" style="display:none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" 
                Text="取消" />
        </center>
    </asp:Panel>
    
    
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    专项工程&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true" runat="server" ForeColor="Blue"></asp:Label>
                    &nbsp;进场设备清单
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridview_DeviceItemList" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px" OnRowDeleting="gridview_ItemList_RowDeleted"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="设备 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_DeviceName" runat="server" Text='<%# Eval("DeviceName") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格型号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="尺寸以及功能">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Size" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                            <asp:Label ID="Label_Usage" runat="server" Text='<%# Eval("Usage") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="状况">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="标书数量">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_PlanCount" runat="server" 
                                                Text='<%# Eval("PlanCount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False"
                                                ImageUrl="~/images/ICON/edit.gif" Text="修改" OnClientClick="javascript:setModalPopup(this.id,true);" />
                                                
                                             <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" 
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                                             </cc2:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未添设备</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                </td>
            </tr>
            
        </table>
      <center>
                    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                                onserverclick="Button_Save_Click" />
                                
                     <input id="Button_Add" type="button" runat="server" class="button_bak" value="添加设备" onclick="javascript:setModalPopup(this.id,false);" />
                      <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Add"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                      </cc2:ModalPopupExtender>
               </center>
    </div>
</asp:Content>
