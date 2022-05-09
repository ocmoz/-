<%@ Page Title="采购申请" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="PurchaseApply.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_PurchaseApply" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../../../../control/WorkFlowUserSelectControl.ascx" tagname="WorkFlowUserSelectControl" tagprefix="uc1" %>
<%@ Import Namespace="FM2E.WorkflowLayer" %>
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
            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemID')).innerText.replace(regS, "");
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

            //产品名称
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ProductName')).innerText;
            document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value = name;

            //规格型号
            var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Model')).innerText;
            document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value = model;

            //单价
            var price = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_UnitPrice')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = price;

            //单位
            var unit = trim(document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Unit')).innerText);
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].value == unit) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }

            //数量
            var count = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PlanCount')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_Count.ClientID %>').value = count;

            //小计
            var amount = parseFloat(price) * parseFloat(count);
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = amount;

            //系统
            var sysid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_SystemID')).value;
            var ss = document.getElementById('<%= DropDownList_System.ClientID %>');
            for (i = 0; i < ss.options.length; i++) {
                if (ss.options[i].value == sysid) {
                    ss.options[i].selected = true;
                }
                else
                    ss.options[i].selected = false;
            }
            
            //备注
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;
        }

        //保存编辑的项
        function saveEditItem() {

            //产品名称
            var name = trim(document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value);
            if (name.length == 0) {
                alert('请输入产品名称');
                return;
            }
            //规格型号
            var model = trim(document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value);
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
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('请选择单位');
                return;
            }
            //数量
            var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
            if (!checkFloat(count, '数量')) {
                return;
            }
            //备注
            var remark = trim(document.getElementById('<%= TextBox_Remark.ClientID %>').value);

            //系统
            var ss = document.getElementById('<%= DropDownList_System.ClientID %>');
            if (ss.selectedIndex == 0) {
                if (!confirm('未选定系统，确认继续？')) {
                    return;
                }
            }
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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件采购"
        HeadOPTxt="目前操作功能：采购申请" HeadHelpTxt="点击“添加材料”可以进行价格查询">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回我的采购单" ButtonUrlType="Href"
            ButtonUrl="PurchaseHistory.aspx"/>
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="取消修改" ButtonUrlType="Href"
            ButtonUrl="ViewPurchaseOrder.aspx" ButtonVisible="false" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%;  display: none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    编辑材料采购数量以及单价<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
              <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    系统：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:DropDownList ID="DropDownList_System" runat="server"></asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    产品名称：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductName" runat="server"></asp:TextBox>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    规格型号：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductModel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    单价(元)：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_UnitPrice" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>(可编辑)
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    单位：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:DropDownList ID="DropDownList_Unit" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    数量：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Count" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>(可编辑)
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    小计(元)：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Amount" runat="server"></asp:TextBox>
                </td>
            </tr>
          
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>(可编辑)
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save" class="button_bak" type="button" value="保存" onclick="javascript:saveEditItem();" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    材料申购表
                </td>
            </tr>
            <tr runat="server" id="Tr_Parent" visible="false">
                <td class="Table_searchtitle" colspan="4" style="text-align: left">
                    在申购单<asp:HyperLink ID="HyperLink_Parent" runat="server"></asp:HyperLink>
                    的基础上追加
                </td>
            </tr>
            <tr>
                <td rowspan="2" colspan="2" style="text-align: center">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="Label"></asp:Label><br />
                    <asp:TextBox ID="TextBox_OrderName" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">&nbsp;*&nbsp;</span>机电材料申购表
                </td>
                <td style="text-align: center" colspan="2">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="TextBox_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    提交时间：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    申请人：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    最后更新：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_UpdateTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    当前状态：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_Status" runat="server"></asp:Label>
                    <asp:Label ID="Label_Approvaling" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                 <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    备注：
                </td>
                <td  colspan="3">
                    <asp:TextBox ID="TextBox_OrderRemark" Width="100%" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False" OnRowDeleting="gridview_ItemList_RowDeleted"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="gridview_ItemList_RowDataBound" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="系统">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="产品名称 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格型号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量<br/>申请/审批">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_PlanCount" runat="server" Text='<%# Eval("PlanCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                ID="Label_AdjustCount" runat="server" ForeColor="Red" Text='<%# Eval("AdjustCount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单价(元)<br/>申请/审批">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_UnitPrice" runat="server" Text='<%# Eval("Price", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                ID="Label_AdjustPrice" runat="server" ForeColor="Red" Text='<%# Eval("AdjustPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)<br/>申请/审批">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_PlanAmount" runat="server" Text='<%# Eval("PlanAmount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                ID="Label_AdjustAmount" runat="server" ForeColor="Red" Text='<%# Eval("AdjustAmount", "{0:#,0.##}") %>'></asp:Label>
                                            </FooterTemplate>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                ID="Label_AdjustTotalAmount" runat="server" ForeColor="Red"></asp:Label>
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
                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                                Text="修改" OnClientClick="javascript:setModalPopup(this.id);" />
                                            <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                                PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_Save"
                                                CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
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
                                        未添加申购材料</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <input id="Button_AddItem" type="button" runat="server" value="添加" style="display: none"
                                onserverclick="Button_AddItem_Click" />
                            <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                                onserverclick="Button_Save_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <input id="Button_Select" type="button" runat="server" class="button_bak" value="添加材料"
                        onclick="javascript:showPopWin('选择材料','Select.aspx', 900, 420, addtolist,true,true);" />
                    <input id="Hidden_SelectedItem" value="" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right">
                    <asp:Button ID="Button_SaveDraft" runat="server" Text="保存草稿" CssClass="button_bak"
                        OnClick="Button_SaveDraft_Click" Visible="false" />&nbsp;&nbsp;
                    <asp:Button ID="Button_Submit" runat="server" Text="确定" CssClass="button_bak" OnClick="Button_Submit_Click"  OnClientClick="javascript:return confirm('确定提交审批？');"/>
                </td>
            </tr>
        </table>
        <table id="Table_ApprovalRecord" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
            <tr>
                <td class="Table_searchtitle" colspan="6">
                    审批历史
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_ApprovalRecord">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="审批人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批结果">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="反馈意见">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" Text='<%# Eval("FeeBack") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细建议">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Eval("ApprovalLog") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未经审批</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table id="Table_ModifyRecord" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
            <tr>
                <td class="Table_searchtitle" colspan="6">
                    修改历史
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ModifyRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="修改人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Eval("ModifierName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改操作">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Eval("ModifyTypeString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="保存内容">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" Text='<%# Eval("Content") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Eval("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未作修改</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table id="Table_RelatedOrders" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
            <tr>
                <td class="Table_searchtitle" colspan="6">
                    相关申购单
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="gridview_RelatedOrders" runat="server" AutoGenerateColumns="False"
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" RowStyle-Height="20px"
                        Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                        <Columns>
                            <asp:TemplateField HeaderText="申购单">
                                <ItemTemplate>
                                    <a style="color: Blue" href='ViewPurchaseOrder.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history&return=1'>
                                        <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label
                                            Text='<%# Eval("SubOrderIndex")%>' runat="server" ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                        <asp:Label Text='<%# Eval("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName"
                                            Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料申购单 </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="WorkFlowStateDescription" HeaderText="状态">
                                <HeaderStyle />
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                <HeaderStyle />
                                <ItemStyle Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                <HeaderStyle />
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        //添加一个材料
        function addtolist(addstring) {
            document.getElementById('<%= Hidden_SelectedItem.ClientID %>').value = addstring;
            document.getElementById('<%= Button_AddItem.ClientID %>').click();
        }

    </script>

</asp:Content>
