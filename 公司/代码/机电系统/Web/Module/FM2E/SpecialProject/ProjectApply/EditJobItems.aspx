<%@ Page Title="编辑专项工程工作量清单" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="EditJobItems.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectApply_EditJobItems" %>

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
                var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Equipment')).innerText;
                document.getElementById('<%= TextBox_Equipment.ClientID %>').value = name;

                //规格型号
                var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Model')).innerText;
                document.getElementById('<%= TextBox_Model.ClientID %>').value = model;

                //单价
                var price = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_UnitPrice')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = price;

                //单位
                var unit = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Unit')).innerText;
                document.getElementById('<%= TextBox_Unit.ClientID %>').value = unit;

                //数量
                var count = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Count')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_Count.ClientID %>').value = count;

                //小计
                var amount = parseFloat(price) * parseFloat(count);
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = amount;

                //备注
                var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
                document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;

                document.getElementById('Button_Save').value = "保存";
            }
            else {
                
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = 0;

               
                document.getElementById('<%= TextBox_Equipment.ClientID %>').value = "";

              
                document.getElementById('<%= TextBox_Model.ClientID %>').value = "";

              
                document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = "";

              
                document.getElementById('<%= TextBox_Unit.ClientID %>').value = "";

               
                document.getElementById('<%= TextBox_Count.ClientID %>').value = "";

               
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";


                document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";

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
            //单价
            var price = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
            if (!checkFloat(price, '单价')) {
                return;
            }
            //单位
            var unit = trim(document.getElementById('<%= TextBox_Unit.ClientID %>').value);
            if (unit.length == 0) {
                alert('请输入单位');
                return;
            }
            //数量
            var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
            if (!checkFloat(count, '数量')) {
                return;
            }
            //备注
            var remark = trim(document.getElementById('<%= TextBox_Remark.ClientID %>').value);

            document.getElementById('Button_OK').click();
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }

        //浮点数检查
        function checkFloat(value, text) {
            var floatVal = parseFloat(value);
            if (isNaN(floatVal) || floatVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个整数。");
                return false;
            }
            return true;
        }

        //数量/单价变化的时候，自动更新金额小计
        function onCountChange() {
            var regS = new RegExp(",", "gi"); //去掉逗号
            var control_count = document.getElementById('<%= TextBox_Count.ClientID %>');
            var control_price = document.getElementById('<%= TextBox_UnitPrice.ClientID %>');
            var count = control_count.value.replace(/(^\s*)|(\s*$)/g, "").replace(regS, "");
            var price = control_price.value.replace(/(^\s*)|(\s*$)/g, "").replace(regS, "");
            if (count.length == 0) {
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
                return;
            }
            if (price.length == 0) {
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
                return;
            }
            try {
                count = parseFloat(count);
            }
            catch (e) {
                alert("数量" + count + "不是数字，请输入数字");
                control_count.focus();
                return;
            }
            try {
                price = parseFloat(price);
            }
            catch (e) {
                alert("单价" + price + "不是数字，请输入数字");
                control_price.focus();
                return;
            }
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = price * count;

        }
    </script>
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--工程立项"
        HeadOPTxt="目前操作功能：工作量清单编辑" HeadHelpTxt="为专项工程的可行性报告添加工作量清单；金额合计作为预算的直接费计算">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="可行性报告" ButtonUrlType="Href"
            ButtonUrl="Apply.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
              <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="预算项" ButtonUrlType="Href"
            ButtonUrl="EditBudgetItems.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    
    
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 200px;display:none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    工程项<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
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
                    单价：</td> <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_UnitPrice" runat="server"></asp:TextBox>元<span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    单位：</td><td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Unit" runat="server" ></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
           </tr>
           <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    数量：</td> <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Count" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                   ">
                    金额：</td>
                   <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Amount" runat="server"></asp:TextBox>元
                </td>
           </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    备注：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>
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
                    &nbsp;工作量清单
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridview_JobItemList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gridview_ItemList_RowDataBound"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" OnRowDeleting="gridview_ItemList_RowDeleted"
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
                                            <asp:Label ID="Label_Equipment" runat="server" Text='<%# Eval("Equipment") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格型号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Count" runat="server" 
                                                Text='<%# Eval("Count", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单价(元)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_UnitPrice" runat="server" Text='<%# Eval("UnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>
                                        </FooterTemplate>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                         <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
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
                                        未添工作项</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                </td>
            </tr>
           
        </table>
        
      <center>
                    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                                onserverclick="Button_Save_Click" />
                                
                     <input id="Button_Add" type="button" runat="server" class="button_bak2" value="添加工程项" onclick="javascript:setModalPopup(this.id,false);" />
                      <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Add"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                      </cc2:ModalPopupExtender>
               </center>
    </div>
</asp:Content>
