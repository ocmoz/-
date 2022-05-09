<%@ Page Title="编辑专项工程预算项清单" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="EditBudgetItems.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectApply_EditBudgetItems" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
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
                
                //名称
                var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_BudgetItemName')).innerText;
                document.getElementById('<%= TextBox_BudgetItemName.ClientID %>').value = name;

                //是否与直接费相关
                var r = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Related')).innerText;
                if (r == '是') {
                    document.getElementById('<%= CheckBox_Related.ClientID %>').checked = true;
                    document.getElementById('<%= TextBox_AMultiple.ClientID %>').disabled = false;
                    document.getElementById('<%= TextBox_Amount.ClientID %>').disabled = true;
                }
                else {
                    document.getElementById('<%= CheckBox_Related.ClientID %>').checked = false;
                    document.getElementById('<%= TextBox_AMultiple.ClientID %>').disabled = true;
                    document.getElementById('<%= TextBox_Amount.ClientID %>').disabled = false;
                }
                
                //倍数
                var m = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_TrueMultiple')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_AMultiple.ClientID %>').value = m;


                //金额
                var a = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_TrueAmount')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = a;


                //备注
                var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
                document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;

                document.getElementById('Button_Save').value = "保存";
            }
            else {
                
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = 0;

                document.getElementById('<%= CheckBox_Related.ClientID %>').checked = false;
                document.getElementById('<%= TextBox_AMultiple.ClientID %>').disabled = true;
                document.getElementById('<%= TextBox_Amount.ClientID %>').disabled = false;
                
                document.getElementById('<%= TextBox_BudgetItemName.ClientID %>').value = "";


                document.getElementById('<%= TextBox_AMultiple.ClientID %>').value = "";


                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";

                document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";

                document.getElementById('Button_Save').value = "添加";
            }
        }

        //保存编辑的项
        function saveEditItem() {

            //名称
            var name = trim(document.getElementById('<%= TextBox_BudgetItemName.ClientID %>').value);
            if (name.length == 0) {
                alert('请预算设备');
                return false;
            }
 
            //比例
            var price = trim(document.getElementById('<%= TextBox_AMultiple.ClientID %>').value);
            if (!checkFloat(price, '比例')) {
                return;
            }
            //金额
            var count = trim(document.getElementById('<%= TextBox_Amount.ClientID %>').value);
            if (!checkFloat(count, '金额')) {
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
                alert(text + "\n其格式不正确:\n" + value + "不是一个数字。");
                return false;
            }
            return true;
        }

        //倍数变化的时候，自动更新金额小计
        function onMultipleChange() {

            var regS = new RegExp(",", "gi"); //去掉逗号
            var direct = parseFloat(document.getElementById('<%= Lable_Direct.ClientID %>').innerText.replace(regS, ""));
   
           
            var m_control = document.getElementById('<%= TextBox_AMultiple.ClientID %>');
            var m = m_control.value.replace(/(^\s*)|(\s*$)/g, "").replace(regS, "");
            
            if (m.length == 0) {
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
                return;
            }
            
            try {
                m = parseFloat(m);
            }
            catch (e) {
                alert("数量" + m + "不是数字，请输入数字");
                m_control.focus();
                return;
            }

            document.getElementById('<%= TextBox_Amount.ClientID %>').value = direct * parseFloat(m) / 100;
        }

        //金额变化的时候，自动更新倍数
        function onAmountChange() {

            var regS = new RegExp(",", "gi"); //去掉逗号
            var direct = parseFloat(document.getElementById('<%= Lable_Direct.ClientID %>').innerText.replace(regS, ""));

           
            var m_control = document.getElementById('<%= TextBox_Amount.ClientID %>');
            var m = m_control.value.replace(/(^\s*)|(\s*$)/g, "").replace(regS, "");

            if (m.length == 0) {
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
                return;
            }

            try {
                m = parseFloat(m);
            }
            catch (e) {
                alert("数量" + m + "不是数字，请输入数字");
                m_control.focus();
                return;
            }
            if (direct != 0)
                document.getElementById('<%= TextBox_AMultiple.ClientID %>').value = m / direct * 100;
            else
                document.getElementById('<%= TextBox_AMultiple.ClientID %>').value = 0;
        }

        //
        function onRelatedChange(ck) {

            var regS = new RegExp(",", "gi"); //去掉逗号
            var direct = parseFloat(document.getElementById('<%= Lable_Direct.ClientID %>').innerText.replace(regS, ""));
            var m = document.getElementById('<%= TextBox_AMultiple.ClientID %>');
            var a = document.getElementById('<%= TextBox_Amount.ClientID %>');
            if (ck.checked == true) {
                m.disabled = false;
                a.disabled = true;
                if (m.value == '')
                    a.value = 0;
                else                   
                a.value = direct * parseFloat(m.value)/100;
            }
            else {
                m.disabled = true;
                a.disabled = false;
                if (direct != 0) {
                    if (a.value == '')
                        m.value = 0;
                        else
                    m.value = parseFloat(a.value) / direct * 100;
                }
            }
        }
    </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--工程立项"
        HeadOPTxt="目前操作功能：预算清单编辑" HeadHelpTxt="为专项工程的可行性报告添加预算清单；预算的直接费等于工作量清单的金额合计">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="可行性报告" ButtonUrlType="Href"
            ButtonUrl="Apply.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
              <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="工程项" ButtonUrlType="Href"
            ButtonUrl="EditJobItems.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    
        <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 200px;display:none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    预算项<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    根据工程量清单计算的直接费为&nbsp;<asp:Label ID="Lable_Direct" runat="server" Font-Bold="true" Font-Underline="true" ForeColor="Blue"></asp:Label>&nbsp;元</td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    项名称：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_BudgetItemName" runat="server" ></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    与直接费相关：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <input type="checkbox" id="CheckBox_Related" runat="server" onclick="javascript:onRelatedChange(this);" />
                </td>
                 </tr> 
                             
        <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    比例：</td> <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_AMultiple" runat="server"></asp:TextBox>%<span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    金额：</td><td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Amount" runat="server" ></asp:TextBox>元<span style="color:Red; font-weight:bold">*</span>
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
                    &nbsp;预算清单
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridview_BudgetItemList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gridview_ItemList_RowDataBound"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" OnRowDeleting="gridview_ItemList_RowDeleted"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Index" runat="server" Text='<%# (char)(System.Text.Encoding.ASCII.GetBytes("A")[0] + Container.DataItemIndex) %>'></asp:Label>
                                            <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="名称">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_BudgetItemName" runat="server" Text='<%# Eval("BudgetItemName") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否与直接费相关">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Related" runat="server" Text='<%# Convert.ToBoolean(Eval("IsRelated2Direct"))== true ? "是":"否" %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="与直接费比例(%)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_TrueMultiple" runat="server" Text='<%# (Convert.ToDecimal(Eval("TrueMultiple"))*100).ToString("#.##") %>'></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="Label_TotalMultiple" runat="server"></asp:Label>
                                        </FooterTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_TrueAmount" runat="server" Text='<%# Eval("TrueAmount", "{0:#,0.##}") %>'></asp:Label>
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
                                        未添预算项</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                </td>
            </tr>      </table>
            <center>
                    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                                onserverclick="Button_Save_Click" />
                                
                     <input id="Button_Add" type="button" runat="server" class="button_bak" value="添加预算" onclick="javascript:setModalPopup(this.id,false);" />
                      <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Add"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                      </cc2:ModalPopupExtender>
              </center>
    </div>
</asp:Content>

