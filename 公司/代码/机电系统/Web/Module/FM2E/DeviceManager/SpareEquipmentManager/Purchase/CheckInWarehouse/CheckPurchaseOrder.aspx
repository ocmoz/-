<%@ Page Title="查看采购单" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="CheckPurchaseOrder.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckPurchaseOrder" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        //编辑的时候设置模式对话框的值
        function setModalPopup(button_id) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            //ITEMID
            var itemid = document.getElementById(button_id.replace('Button_Check', 'Label_ItemID')).innerText.replace(regS, "");
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

            //产品名称
            var name = document.getElementById(button_id.replace('Button_Check', 'Label_ProductName')).innerText;
            document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value = name;

            //规格型号
            var model = document.getElementById(button_id.replace('Button_Check', 'Label_Model')).innerText;
            document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value = model;



            //单位
            var unit = document.getElementById(button_id.replace('Button_Check', 'Label_Unit')).innerText;
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].value == unit) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }

            //采购数量
            document.getElementById('<%= TextBox_ActualCount.ClientID %>').value = "";

            //默认验收数量
            document.getElementById('<%= TextBox_AcceptedCount.ClientID %>').value = "";
            //生产商
            document.getElementById('<%= TextBox_Producer.ClientID %>').value = "";
            //供应商
            document.getElementById('<%= TextBox_Supplier.ClientID %>').value = "";

            //采购备注
            document.getElementById('<%= TextBox_PurchaseRemark.ClientID %>').value = "";
            //验收备注
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";

            //采购人
            var purchaser = document.getElementById(button_id.replace('Button_Check', 'Label_Purchaser')).innerText;

            document.getElementById('<%= Label_Purchaser.ClientID %>').innerText = purchaser;

            //清空技术人员ID
            document.getElementById('<%= TextBox_TechnicianID.ClientID %>').value = '';
        }

        //编辑的时候设置模式对话框的值
        function setModalPopup2(button_id) {
            var regS = new RegExp(",", "gi"); //去掉逗号
            //ITEMID
            var itemid = document.getElementById(button_id.replace('Button_Check2', 'Hidden_ItemID')).value;
            document.getElementById('<%= Hidden_EditItemID2.ClientID %>').value = itemid;

            var recordid = document.getElementById(button_id.replace('Button_Check2', 'Hidden_RecordID')).value;
            document.getElementById('<%= Hidden_EditRecordID.ClientID %>').value = recordid;
            //产品名称
            var name = document.getElementById(button_id.replace('Button_Check2', 'Label_ProductName')).innerText;
            document.getElementById('<%= TextBox_SelectedProductName2.ClientID %>').value = name;
            //规格型号
            var model = document.getElementById(button_id.replace('Button_Check2', 'Label_Model')).innerText;
            document.getElementById('<%= TextBox_SelectedProductModel2.ClientID %>').value = model;

            //单价
            var unitprice = document.getElementById(button_id.replace('Button_Check2', 'Label_PurchaseUnitPrice')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_UnitPrice2.ClientID %>').value = unitprice;
            
            //单位
            var unit = document.getElementById(button_id.replace('Button_Check2', 'Label_Unit')).innerText;
            var s = document.getElementById('<%= DropDownList_Unit2.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].value == unit) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }
            //采购数量
            var count = document.getElementById(button_id.replace('Button_Check2', 'Label_PurchaseCount')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_ActualCount2.ClientID %>').value = count;
            //默认验收数量
            document.getElementById('<%= TextBox_AcceptedCount2.ClientID %>').value = "";
            //生产商
            var producer = document.getElementById(button_id.replace('Button_Check2', 'Label_Producer')).innerText;
            document.getElementById('<%= TextBox_Producer2.ClientID %>').value = producer;
            var supplier = document.getElementById(button_id.replace('Button_Check2', 'Label_Supplier')).innerText;
            //供应商
            document.getElementById('<%= TextBox_Supplier2.ClientID %>').value = supplier;
            //采购备注
            var remrk = document.getElementById(button_id.replace('Button_Check2', 'Label_PurchaseRemark')).innerText;
            document.getElementById('<%= TextBox_PurchaseRemark2.ClientID %>').value = remrk;
            //验收备注
            document.getElementById('<%= TextBox_Remark2.ClientID %>').value = "";
            //采购人
            var purchaser = document.getElementById(button_id.replace('Button_Check2', 'Label_Purchaser')).innerText;
            document.getElementById('<%= Label_Purchaser2.ClientID %>').innerText = purchaser;
            //清空技术人员ID
            document.getElementById('<%= TextBox_TechnicianID2.ClientID %>').value = '';
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
                //alert('请输入规格型号');
                //return;
            }

            //采购单价
            var unitprice = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
            if (!checkFloat(unitprice, '采购单价')) {
                return;
            }

            //单位
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('请选择单位');
                return;
            }

            //采购数量
            var count = trim(document.getElementById('<%= TextBox_ActualCount.ClientID %>').value);
            if (!checkFloat(count, '采购数量')) {
                return;
            }

            //验收数量
            var acceptedcount = trim(document.getElementById('<%= TextBox_AcceptedCount.ClientID %>').value);
            if (!checkFloat(acceptedcount, '验收数量')) {
                return;
            }

            if (parseFloat(acceptedcount) > parseFloat(count)) {
                alert('验收数量不能大于采购数量');
                return false;
            }
            //生产商
            var producer = trim(document.getElementById('<%= TextBox_Producer.ClientID %>').value);
            if (producer.length == 0) {
                alert('请输入生产商');
                return;
            }
            //供应商
            var supplier = trim(document.getElementById('<%= TextBox_Supplier.ClientID %>').value);
            if (supplier.length == 0) {
                alert('请输入供应商');
                return;
            }
            //采购人密码
            var pp = trim(document.getElementById('<%= TextBox_PurchaserPassword.ClientID %>').value);
            if (pp.length == 0) {
                //alert('请输入采购人密码');
                //return;
            }

            //技术验收人以及技术验收人密码
            var t = trim(document.getElementById('<%= TextBox_TechnicianID.ClientID %>').value);
            if (t.length == 0) {
                alert('请输入技术验收人用户名');
                return;
            }

            var tp = trim(document.getElementById('<%= TextBox_TechnicianPassword.ClientID %>').value);
            if (tp.length == 0) {
                alert('请输入技术验收人密码');
                return;
            }

            var cfm = confirm("保存后不能再修改，确认保存？");
            if (cfm == false)
                return false;

            document.getElementById('Button_Save').click();
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();


        }

        //保存编辑的项
        function saveEditItem2() {
            //产品名称
            var name = trim(document.getElementById('<%= TextBox_SelectedProductName2.ClientID %>').value);
            if (name.length == 0) {
                alert('请输入产品名称');
                return;
            }
            //规格型号
            var model = trim(document.getElementById('<%= TextBox_SelectedProductModel2.ClientID %>').value);
            if (model.length == 0) {
                //alert('请输入规格型号');
                //return;
            }
            //采购单价
            var unitprice = trim(document.getElementById('<%= TextBox_UnitPrice2.ClientID %>').value);
            if (!checkFloat(unitprice, '采购单价')) {
                return;
            }
            //单位
            var s = document.getElementById('<%= DropDownList_Unit2.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('请选择单位');
                return;
            }
            //采购数量
            var count = trim(document.getElementById('<%= TextBox_ActualCount2.ClientID %>').value);
            if (!checkFloat(count, '采购数量')) {
                return;
            }
            //验收数量
            var acceptedcount = trim(document.getElementById('<%= TextBox_AcceptedCount2.ClientID %>').value);
            if (!checkFloat(acceptedcount, '验收数量')) {
                return;
            }

            if (parseFloat(acceptedcount) > parseFloat(count)) {
                alert('验收数量不能大于采购数量');
                return false;
            }
            //生产商
            var producer = trim(document.getElementById('<%= TextBox_Producer2.ClientID %>').value);
            if (producer.length == 0) {
                alert('请输入生产商');
                return;
            }
            //供应商
            var supplier = trim(document.getElementById('<%= TextBox_Supplier2.ClientID %>').value);
            if (supplier.length == 0) {
                alert('请输入供应商');
                return;
            }
            //采购人密码
            var pp = trim(document.getElementById('<%= TextBox_PurchaserPassword2.ClientID %>').value);
            if (pp.length == 0) {
                alert('请输入采购人密码');
                return;
            }
            //技术验收人以及技术验收人密码
            var t = trim(document.getElementById('<%= TextBox_TechnicianID2.ClientID %>').value);
            if (t.length == 0) {
                alert('请输入技术验收人用户名');
                return;
            }
            var tp = trim(document.getElementById('<%= TextBox_TechnicianPassword2.ClientID %>').value);
            if (tp.length == 0) {
                alert('请输入技术验收人密码');
                return;
            }
            var cfm = confirm("保存后不能再修改，确认保存？");
            if (cfm == false)
                return false;
            document.getElementById('Button_Save2').click();
            document.getElementById('<%= Button_SaveItem2.ClientID %>').click();
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


        function HideShow(objid) {

            var obj = document.getElementById(objid);

            if (obj != null) {
                obj.style.display = (obj.style.display == 'none' ? 'block' : 'none');
            }
        }
    </script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件采购"
        HeadOPTxt="目前操作功能：验收" HeadHelpTxt="浅黄色部分为需要本仓库验收的项">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回验收单列表" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="Check.aspx" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel_CheckItem" runat="server" Style="width: 95%; 
        display: none;" CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    验收材料采购数量<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    产品名称：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductName" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    规格型号：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductModel" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold"></span>
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
                    <asp:TextBox ID="TextBox_ActualCount" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    验收数量：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_AcceptedCount" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    生产商：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Producer" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    供应商：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Supplier" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_PurchaseRemark" runat="server" title="请输入备注~50:" Width="300px"></asp:TextBox>
                    采购人：<asp:Label ID="Label_Purchaser" runat="server"></asp:Label>
                    &nbsp;&nbsp; 密码：<asp:TextBox ID="TextBox_PurchaserPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    验收备注：
                </td>
                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="请输入备注~50:" Width="300px"></asp:TextBox>
                    仓管：<asp:Label ID="Label_WarehouseKeeper" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    技术验收人：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    用户名：<asp:TextBox ID="TextBox_TechnicianID" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span> 密码：<asp:TextBox ID="TextBox_TechnicianPassword" runat="server"
                            TextMode="Password"></asp:TextBox><span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save_a" class="button_bak" type="button" value="保存" onclick="javascript:saveEditItem();" />
            <input id="Button_Save" class="button_bak" type="button" value="保存" style="display: none" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    
    <asp:Panel ID="Panel_CheckItem2" runat="server" Style="width: 95%; 
        display: none;" CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    验收材料采购数量
                    <input type="hidden" value="" id="Hidden_EditItemID2" runat="server" />
                    <input type="hidden" value="" id="Hidden_EditRecordID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    产品名称：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductName2" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    规格型号：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductModel2" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold"></span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购单价(元)：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_UnitPrice2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    单位：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:DropDownList ID="DropDownList_Unit2" runat="server">
                    </asp:DropDownList>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购数量：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_ActualCount2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    验收数量：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_AcceptedCount2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    生产商：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Producer2" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    供应商：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Supplier2" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    采购备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_PurchaseRemark2" runat="server" title="请输入备注~50:" Width="300px"></asp:TextBox>
                    采购人：<asp:Label ID="Label_Purchaser2" runat="server"></asp:Label>
                    &nbsp;&nbsp; 密码：<asp:TextBox ID="TextBox_PurchaserPassword2" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    验收备注：
                </td>
                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Remark2" runat="server" title="请输入备注~50:" Width="300px"></asp:TextBox>
                    仓管：<asp:Label ID="Label_WarehouseKeeper2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    技术验收人：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    用户名：<asp:TextBox ID="TextBox_TechnicianID2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span> 密码：<asp:TextBox ID="TextBox_TechnicianPassword2" runat="server"
                            TextMode="Password"></asp:TextBox><span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save_a2" class="button_bak" type="button" value="保存" onclick="javascript:saveEditItem2();" />
            <input id="Button_Save2" class="button_bak" type="button" value="保存" style="display: none" />
            <asp:Button ID="Button_Cancel_Edit2" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    
    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
        onserverclick="Button_Save_Click" />
    <input id="Button_SaveItem2" type="button" runat="server" value="保存" style="display: none"
        onserverclick="Button_Save2_Click" />
    <input type="hidden" id="Hidden_WarehouseID" runat="server" />
    <input type="hidden" id="Hidden_WarehouseName" runat="server" />
    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    材料申购表
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="2" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="未知公司"></asp:Label><br />
                    <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue"></asp:Label>&nbsp;机电材料申购表
                </td>
                <td style="text-align: center" colspan="2" class="Table_searchtitle">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="Label_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <table id="Table_detaillist" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                                border: solid 1px #a7c5e2;" border="1">
                                <tr>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        序号
                                    </th>
                                      <th  style="width: 8%" class="Table_searchtitle"
                                    
                                    >系统</th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        产品名称
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        规格型号
                                    </th>
                                    <th style="width: 15%" class="Table_searchtitle">
                                        数量<br />
                                        审批/采购/验收
                                    </th>
                                    <th style="width: 5%" class="Table_searchtitle">
                                        单位
                                    </th>
                                    <th class="Table_searchtitle">
                                        备注
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        状态
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        采购人
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        验收
                                    </th>
                                </tr>
                                <asp:Repeater ID="Repeater_ItemList" runat="server" OnItemDataBound="Repeater_ItemList_RowDataBound">
                                    <ItemTemplate>
                                        <tr id="tr_item" runat="server">
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                              <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_AdjustCount" runat="server" ForeColor="Red" Text='<%# Eval("FinalCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                    ID="Label_ActualCount" runat="server" Text='<%# Eval("ActualCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                        ID="Label_AcceptedCount" runat="server" Text='<%# Eval("AcceptedCount", "{0:#,0.##}") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Purchaser" Text='<%# Eval("PurchaserName") %>' runat="server">
                                                </asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Button ID="Button_Check" runat="server" Text="验收" CssClass="button_bak" OnClientClick="javascript:setModalPopup(this.id);" />
                                                <cc2:ModalPopupExtender ID="ModalPopupExtender_CheckItem" runat="server" TargetControlID="Button_Check"
                                                    PopupControlID="Panel_CheckItem" BackgroundCssClass="modalBackground" OkControlID="Button_Save"
                                                    CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                                </cc2:ModalPopupExtender>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td valign="top"  style="text-align:center">
                                                <a href='<%# "javascript:HideShow(\"" + Container.FindControl("gridview_ItemList").ClientID + "\");" %>'>
                                                    <span style="color: Blue">采购记录</span></a>
                                            </td>
                                            <td colspan="9">
                                                <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" DataSource='<%# Eval("PurchaseRecordList") %>'
                                                    RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="序号">
                                                            <ItemTemplate>
                                                                [<asp:Label ID="Label_ID" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>]
                                                                <input type="hidden" id="Hidden_ItemID" runat="server" value='<%# Eval("PlanDetailItemID") %>' />
                                                                <input type="hidden" id="Hidden_RecordID" runat="server" value='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="产品名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="规格型号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量<br/>采购/验收">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseCount" runat="server" Text='<%# Eval("PurchaseCount", "{0:#,0.#####}") %>'></asp:Label>
                                                                &nbsp;/&nbsp;
                                                                <asp:Label ID="Label_AcceptanceCount" runat="server" Text='<%# Eval("AcceptanceCount", "{0:#,0.#####}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单价(元)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseUnitPrice" runat="server" Text='<%# Eval("PurchaseUnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="金额(元)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount", "{0:#,0.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="生产供应">
                                                            <ItemTemplate>
                                                                生产商：<asp:Label ID="Label_Producer" runat="server" Text='<%# Eval("Producer") %>'></asp:Label><br />
                                                                供应商：<asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%"  HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="责任人">
                                                            <ItemTemplate>
                                                                采购员：<asp:Label ID="Label_Purchaser" runat="server" Text='<%# Eval("PurchaserName") %>'></asp:Label><br />
                                                                仓管员：<asp:Label ID="Label_Checker_Warehouse" runat="server" Text='<%# Eval("WarehouseKeeperName") %>'></asp:Label><br />
                                                                技术员：<asp:Label ID="Label_Checker_Technician" runat="server" Text='<%# Eval("TechnicianName") %>'></asp:Label>
                                                                <br />
                                                                仓库：<asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%"  HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="状态">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_StatusString" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="备注">
                                                            <ItemTemplate>
                                                                采购：<asp:Label ID="Label_PurchaseRemark" runat="server" Text='<%# Eval("PurchaseRemark") %>'></asp:Label><br />
                                                                验收：<asp:Label ID="Label_AcceptanceRemark" runat="server" Text='<%# Eval("AcceptanceRemark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="验收">
                                                            <ItemTemplate>
                                                                    <asp:Button ID="Button_Check2" runat="server" Text="验收" CssClass="button_bak" OnClientClick="javascript:setModalPopup2(this.id);" Visible='<%# (PurchaseRecordStatus)Eval("Status") == PurchaseRecordStatus.WAIT4CHECK && Eval("WarehouseID").ToString() == Hidden_WarehouseID.Value %>' />
                                                            <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button_Check2"
                                                                PopupControlID="Panel_CheckItem2" BackgroundCssClass="modalBackground" OkControlID="Button_Save2"
                                                                CancelControlID="Button_Cancel_Edit2" DynamicServicePath="" Enabled="true">
                                                            </cc2:ModalPopupExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%"/>
                                                            
                                                        </asp:TemplateField>
                                                       
                                                    </Columns>
                                                    <RowStyle Height="20px" HorizontalAlign="Center"  />
                                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <center>
                                                            尚未有采购记录</center>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                    <asp:Label ID="Label_Status" runat="server"></asp:Label><asp:Label ID="Label_Approvaling"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    审批历史
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_ApprovalRecord">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="审批人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批结果">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="反馈意见">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" Text='<%# Eval("FeeBack") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细建议">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Eval("ApprovalLog") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
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
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    修改历史
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ModifyRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="修改人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Eval("ModifierName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改操作">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Eval("ModifyTypeString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="保存内容">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" Text='<%# Eval("Content") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Eval("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
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
    </div>
</asp:Content>
