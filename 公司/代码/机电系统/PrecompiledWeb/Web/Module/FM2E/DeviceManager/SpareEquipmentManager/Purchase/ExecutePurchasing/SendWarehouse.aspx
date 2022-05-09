<%@ page title="送仓报验" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_SendWarehouse, App_Web_j4wherg8" %>

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
        function setModalPopup(button_id, isedit) {
            var regS = new RegExp(",", "gi"); //去掉逗号
            if (isedit) {
                //ITEMID
                var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemID')).innerText.replace(regS, "");
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;
                //产品名称
                var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ProductName')).innerText;
                document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value = name;
                //规格型号
                var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Model')).innerText;
                document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value = model;
                //送验仓库
                var s = document.getElementById('<%= DropDownList_Warehouse.ClientID %>');
                var whid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_WarehouseID')).value;
                for (i = 0; i < s.options.length; i++) {
                    if (s.options[i].value == whid) {
                        s.options[i].selected = true;
                    }
                    else
                        s.options[i].selected = false;
                }
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
                //单价
                var unitprice = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PurchaseUnitPrice')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = unitprice;
                //采购数量
                var count = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PurchaseCount')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_Count.ClientID %>').value = count;
                //生产商
                var producer = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Producer')).innerText;
                document.getElementById('<%= TextBox_Producer.ClientID %>').value = producer;
                //供应商
                var supplier = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Supplier')).innerText;
                document.getElementById('<%= TextBox_Supplier.ClientID %>').value = supplier;
                //采购备注
                var remrk = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PurchaseRemark')).innerText;
                document.getElementById('<%= TextBox_Remark.ClientID %>').value = remrk;
                //金额小计
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = parseFloat(unitprice) * parseFloat(count);

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
            }
            //新增
            else {
                //ITEMID
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = "0";
                //产品名称
                document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value = "";
                //规格型号
                document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value = "";
                //送验仓库
                var s = document.getElementById('<%= DropDownList_Warehouse.ClientID %>');
                s.selectedIndex = 0;
                //单位
                var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
                s.selectedIndex = 0;
                //单价
                document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = "";
                //采购数量
                document.getElementById('<%= TextBox_Count.ClientID %>').value = "";
                //生产商
                document.getElementById('<%= TextBox_Producer.ClientID %>').value = "";
                //供应商
                document.getElementById('<%= TextBox_Supplier.ClientID %>').value = "";
                //采购备注
                document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";
                //金额小计
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
                //系统
                var ss = document.getElementById('<%= DropDownList_System.ClientID %>');
                ss.selectedIndex = 0;
            }
        }

        //保存编辑的项
        function saveEditItem() {
            //产品名称
            var name = trim(document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value);
            if (name.length == 0) {
                alert('请输入产品名称');
                return false;
            }
            //规格型号
            var model = trim(document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value);
            if (model.length == 0) {
                //alert('请输入规格型号');
                //return false;
            }
            //单价
            var price = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
            if (!checkFloat(price, '单价')) {

                return false;
            }
            //单位
            var s_unit = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            if (s_unit.selectedIndex == 0) {
                alert('请选择单位');
                return false;
            }
            //数量
            var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
            if (!checkFloat(count, '数量')) {
                return false;
            }
            //生产商
            var p = trim(document.getElementById('<%= TextBox_Producer.ClientID %>').value);
            if (p.length == 0) {
                alert('请输入生产商');
                return false;
            }
            //供应商
            var sp = trim(document.getElementById('<%= TextBox_Supplier.ClientID %>').value);
            if (sp.length == 0) {
                alert('请输入供应商');
                return false;
            }
            //送验仓库
            var s = document.getElementById('<%= DropDownList_Warehouse.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('请选择送验仓库');
                return false;
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


            document.getElementById('Button_Save').click();

            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }

        //浮点数检查
        function checkFloat(value, text) {
            var floatVal = parseFloat(value);
            if (isNaN(floatVal) || floatVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个浮点数。");
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
        HeadOPTxt="目前操作功能：送仓报验" HeadHelpTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回采购单列表" ButtonUrlType="Href"
            ButtonUrl="ExecutePurchasing.aspx" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%;  display: none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    编辑材料采购数量以及单价<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                    <asp:TextBox ID="TextBox_SelectedProductName" runat="server"></asp:TextBox><span
                        style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    规格型号：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductModel" runat="server"></asp:TextBox><span
                        style="color: Red; font-weight: bold"></span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购单价(元)：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_UnitPrice" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    单位：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:DropDownList ID="DropDownList_Unit" runat="server">
                    </asp:DropDownList>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购数量：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Count" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购金额小计(元)：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Amount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    生产商：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Producer" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    供应商：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Supplier" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    送验仓库：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:DropDownList ID="DropDownList_Warehouse" runat="server" DataTextField="Name"
                        DataValueField="WareHouseID">
                    </asp:DropDownList>
                    <span style="color: Red; font-weight: bold">*</span> 
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save2" class="button_bak" type="button" value="保存" onclick="javascript:saveEditItem();" />
            <input id="Button_Save" class="button_bak" style="display: none;" type="button" value="保存" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    送仓报验单
                </td>
            </tr>
            <tr>
                <td rowspan="2" colspan="2" style="text-align: center">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="Label"></asp:Label><br />
                    <asp:TextBox ID="TextBox_SheetName" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">&nbsp;*&nbsp;</span> 送仓报验单
                </td>
                <td style="text-align: center" colspan="2">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="Label_SheetID" runat="server" Font-Underline="true"></asp:Label>
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
                    <asp:Label ID="Label_Applicant" runat="server"></asp:Label>
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
                <td colspan="4">
                    <div runat="server" id="div_recordlist">
                        <table id="table_PurchaseRecordList" style="width: 100%; border-collapse: collapse;
                            vertical-align: middle; border: solid 1pt #000000;" border="1">
                            <tr>
                                <th style="width: 4%" class="Table_searchtitle">
                                    序号
                                </th>
                                 <th style="width: 7%" class="Table_searchtitle">
                                    系统
                                </th>
                                <th style="width: 7%" class="Table_searchtitle">
                                    产品名称
                                </th>
                                <th style="width: 8%" class="Table_searchtitle">
                                    规格型号
                                </th>
                                <th style="width: 7%" class="Table_searchtitle">
                                    数量<br />
                                    采购/验收
                                </th>
                                <th style="width: 5%" class="Table_searchtitle">
                                    单位
                                </th>
                                <th style="width: 5%" class="Table_searchtitle">
                                    单价
                                </th>
                                <th style="width: 5%" class="Table_searchtitle">
                                    金额
                                </th>
                                <th style="width: 10%" class="Table_searchtitle">
                                    生产供应
                                </th>
                                <th style="width: 10%" class="Table_searchtitle">
                                    责任人
                                </th>
                                <th style="width: 8%" class="Table_searchtitle">
                                    状态
                                </th>
                                <th style="width: 12%" class="Table_searchtitle">
                                    备注
                                </th>
                                <th style="width: 4%" class="Table_searchtitle">
                                    修改
                                </th>
                                <th style="width: 4%" class="Table_searchtitle">
                                    删除
                                </th>
                            </tr>
                            <asp:Repeater ID="Repeater_PurchaseRecordList" runat="server" OnItemCommand="Repeater_PurchaseRecordListCommand">
                                <ItemTemplate>
                                    <tr id="tr_item" runat="server">
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                            <input id="Hidden_FormID" runat="server" value='<%# Eval("ID") %>' type="hidden" />
                                        </td>
                                         <td>
                                            <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_PurchaseCount" runat="server" Text='<%# Eval("PurchaseCount", "{0:#,0.#####}") %>'></asp:Label>
                                            &nbsp;/&nbsp;
                                            <asp:Label ID="Label_AcceptanceCount" runat="server" Text='<%# Eval("AcceptanceCount", "{0:#,0.#####}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_PurchaseUnitPrice" runat="server" Text='<%# Eval("PurchaseUnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_PurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount", "{0:#,0.##}") %>'></asp:Label>
                                        </td>
                                        <td>
                                            生产商：<asp:Label ID="Label_Producer" runat="server" Text='<%# Eval("Producer") %>'></asp:Label><br />
                                            供应商：<asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                                        </td>
                                        <td>
                                            采购员：<asp:Label ID="Label_Purchaser" runat="server" Text='<%# Eval("Purchaser") %>'></asp:Label><br />
                                            仓管员：<asp:Label ID="Label_Checker_Warehouse" runat="server" Text='<%# Eval("Checker_Warehouse") %>'></asp:Label><br />
                                            技术员：<asp:Label ID="Label_Checker_Technician" runat="server" Text='<%# Eval("Checker_Technician") %>'></asp:Label><br />
                                            仓库：<asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_WarehouseID" runat="server" value='<%# Eval("WarehouseID") %>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_StatusString" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                        </td>
                                        <td>
                                            采购：<asp:Label ID="Label_PurchaseRemark" runat="server" Text='<%# Eval("PurchaseRemark") %>'></asp:Label><br />
                                            验收：<asp:Label ID="Label_AcceptanceRemark" runat="server" Text='<%# Eval("AcceptanceRemark") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                                Text="修改" OnClientClick="javascript:setModalPopup(this.id,true);" />
                                            <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                                PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_Save"
                                                CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                            </cc2:ModalPopupExtender>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandArgument='<%# Eval("ItemID") %>' CommandName="Delete" ImageUrl="~/images/ICON/delete.gif"
                                                Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                        onserverclick="Button_Save_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <input id="Button_Select" type="button" runat="server" class="button_bak" value="添加报验"
                        onclick="javascript:setModalPopup(this.id,false);" />
                    <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Select"
                        PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_Select"
                        CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                    </cc2:ModalPopupExtender>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right">
                    <asp:Button ID="Button_Submit" runat="server" Text="送仓报验" CssClass="button_bak" OnClick="Button_Submit_Click"
                        OnClientClick="javascript:return confirm('确认送仓？');" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
