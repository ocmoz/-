<%@ page title="执行入库" language="C#" masterpagefile="~/MasterPage/MasterPage.master" enableeventvalidation="false" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_DoInWarehouse, App_Web_e2vo_fnn" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">

        //显示隐藏对象
        function ShowHideObject(id, show) {
            document.getElementById(id).style.display = show ? "" : "none";
        }

        //整数检查
        function checkInt(value, text) {
            var intVal = parseInt(value);
            if (isNaN(intVal) || intVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个整数。");
                return false;
            }
            return true;
        }


        //下一步的提醒
        function cfm() {
            //如果选择的是消耗品
            var ck = document.getElementById('<%= RadioButton_Expendable.ClientID %>');
            var name = document.getElementById('<%= Label_ProductName.ClientID %>').innerText;
            var model = document.getElementById('<%= Label_Model.ClientID %>').innerText;
            var count = document.getElementById('<%= Label_Count.ClientID %>').innerText;
            var unit = document.getElementById('<%= Label_Unit.ClientID %>').innerText;
            if (ck.checked) {//消耗品
                //检查易耗品的种类
                var typeid = document.getElementById('<%= Hidden_ExpendableTypeID.ClientID %>').value;
                if (typeid == '') {

                    alert('请选择易耗品种类');
                    return false;
                }
                return confirm('入库后不能再修改，确认入库？');
            }
            else {

                var count = document.getElementById('<%= TextBox_BarCodeCount.ClientID %>').value;
                if (!checkInt(count, "条码数量")) {
                    return false;
                }
            }
        }

        //设备入库
        function cfm2() {
            //如果选择的是消耗品
            var count = document.getElementById('<%= TextBox_BarCodeCount.ClientID %>').value;
            if (!checkInt(count, "条码数量")) {
                return false;
            }

            var catagroy = document.getElementById('<%= Hidden_CategoryID.ClientID %>').value;
            if (catagroy == null || catagroy == "") {
                alert('请选择设备种类');
                return false;
            }

          

            return confirm('入库后不能再修改，确认入库？');

        }

        //地址选定
        function addAddress(val) {
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];
            if (addcode != '00') {
                document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
                document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
            }
        }
        function clearAddress() {
            document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '';
            document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
        }
    </script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" value="" runat="server" id="Hidden_WarehouseName" />
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <cc2:TabPanel runat="server" HeaderText="类型" ID="TabPanel_Type">
            <HeaderTemplate>
                选择产品类型
            </HeaderTemplate>
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            产品信息
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            产品名称：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_ProductName" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            规格型号：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Model" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            验收数量：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Count" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            单位：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Unit" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            验收备注：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                            <asp:Label ID="Label_CheckRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            产品类型
                        </td>
                    </tr>
                    <tr>
                        <td class="table_none table_none_NoWidth" style="height: 30px; text-align: center"
                            colspan="2">
                            <asp:RadioButton ID="RadioButton_Device" runat="server" Checked="true" Text="设备"
                                GroupName="Type" />
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px; text-align: center"
                            colspan="2">
                            <asp:RadioButton ID="RadioButton_Expendable" runat="server" GroupName="Type" Text="易耗品" />
                        </td>
                    </tr>
                    <tr id="tr_barcode">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            条形码数量：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_BarCodeCount" runat="server"></asp:TextBox><span style="color: Red;
                                font-weight: bold">*</span>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            设备可拆分：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:CheckBox ID="CheckBox_Divide" runat="server" />
                        </td>
                    </tr>
                    <tr id="tr_component">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            零配件：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                            <asp:CheckBox ID="CheckBox_Component" runat="server" />
                        </td>
                    </tr>
                    <tr id="tr_expandabletype" style="display:none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            易耗品种类：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox_ExpendableType" runat="server"></asp:TextBox><span style="color: Red;
                                        font-weight: bold">*</span>
                                   <input type="hidden" id="Hidden_ExpendableTypeID" runat="server" />
                                    <asp:Panel ID="Panel_SelectExpenableType" CssClass="popupControl" BackColor="White" runat="server">
                                        <asp:TreeView ID="TreeView_ExpendableType" ShowLines="true" OnTreeNodeExpanded="TreeView_ExpendableType_OnTreeNodeExpanded"
                                            runat="server" onclick="javascript:causeValidate = false;" OnSelectedNodeChanged="TreeView_ExpendableType_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </asp:Panel>
                                    <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server"
                                        TargetControlID="TextBox_ExpendableType" PopupControlID="Panel_SelectExpenableType"
                                        Position="Bottom" DynamicServicePath="" Enabled="True" ExtenderControlID="">
                                    </cc2:PopupControlExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                    runat="server">
                    <tr runat="server">
                        <td align="center" style="height: 30px" runat="server">
                            <asp:Button ID="Button_Next" runat="server" CssClass="button_bak" Text="下一步" OnClick="Button_Next_Click"
                                OnClientClick="javascript:return cfm();" />&nbsp;&nbsp;
                            <input class="button_bak" onclick="javascript:window.parent.hidePopWin();" type="button"
                                value="取消入库" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="设备信息" ID="TabPanel_DeviceInfo">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr style="display: none">
                        <td class="Table_searchtitle" colspan="4">
                            产品信息
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            产品名称：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_ProductName2" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            规格型号：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Model2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            验收数量：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Count2" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            单位：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Unit2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            验收备注：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                            <asp:Label ID="Label_Remark2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="Table_searchtitle" colspan="4">
                            设备详细信息
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            资产编号：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_AssertNumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            设备类型：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_SerialNum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            种类：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:UpdatePanel ID="selectcategory" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox_CategoryName" runat="server"></asp:TextBox><span style="color: Red;
                                        font-weight: bold">*</span>
                                    <input id="Hidden_CategoryID" runat="server" type="hidden" />
                                    <input id="Hidden_DepreciationMethod" runat="server" type="hidden" />
                                    <input id="Hidden_DepreciableLife" runat="server" type="hidden" />
                                    <input id="Hidden_ResidualRate" runat="server" type="hidden" />
                                    <asp:Panel ID="Panel_SelectCatagory" CssClass="popupControl" BackColor="White" runat="server"
                                        >
                                        <asp:TreeView ID="TreeView_Catagory" ShowLines="true" OnTreeNodeExpanded="TreeView_Catagory_OnTreeNodeExpanded"
                                            runat="server" onclick="javascript:causeValidate = false;" OnSelectedNodeChanged="TreeView_Catagory_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </asp:Panel>
                                    <cc2:PopupControlExtender ID="PopupControlExtender_SelectCatagory" runat="server"
                                        TargetControlID="TextBox_CategoryName" PopupControlID="Panel_SelectCatagory"
                                        Position="Bottom" DynamicServicePath="" Enabled="True" ExtenderControlID="">
                                    </cc2:PopupControlExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            系统：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:DropDownList ID="DropDownList_System" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            地址信息：
                        </td>
                        <td class="table_none table_none_NoWidth" colspan="3">
                            <input id="TextBox_Address" type="text" style="width: 70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../../BasicData/AddressManage/Address.aspx?operator=select', 880, 400, addAddress,true,true);" />
                            <input type="hidden" id="Hidden_AddressID" runat="server" />
                            <input class="cbutton" onclick="javascript:clearAddress();" type="button" value="清除"
                                id="Button_ClearAddress" />
                            <asp:TextBox ID="TextBox_DetailLocation" runat="server" Width="20%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            生产商：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Producer" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            供应商：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Supplier" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            责任人：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_ResponsiblityName" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="Hidden_Responsiblity" />
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            采购人：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Purchaser" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="Hidden_Purchaser" />
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            技术验收人：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Technician" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="Hidden_Techincian" />
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            仓库验收人：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <input type="hidden" runat="server" id="Hidden_WarehouseKeeper" />
                            <asp:Label ID="Label_WarehouserKeeper" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            采购日期：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_PurchaseDate" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            验收日期：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_AcceptanceDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            保修时长：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_Warranty" runat="server"></asp:TextBox>个月
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            使用年限：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_ServiceLife" runat="server"></asp:TextBox>年
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            采购单号：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_PurchaseOrderID" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            购买价格：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Price" runat="server"></asp:Label>元
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            备注：
                        </td>
                        <td class="table_none table_none_NoWidth" colspan="3">
                            <asp:TextBox ID="TextBox_Remark" runat="server" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td class="table_body table_body_NoWidth" style="text-align: right" >
                            条码Excel导入：
                        </td>
                        <td class="table_none table_none_NoWidth"  colspan="3">
                        <asp:FileUpload ID="ImportEqBar" runat="Server" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            图片：
                        </td>
                        <td class="table_none table_none_NoWidth" colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr id="tr11" runat="server" style="display: none">
                        <td runat="server">
                        </td>
                        <td id="Td1" runat="server" colspan="3">
                            <div id="t1" style="display: none">
                                <img src="" id="myimg"></div>
                        </td>
                    </tr>
                </table>
                <center>
                    <asp:Button ID="Button_PreStep" runat="server" CssClass="button_bak" Text="上一步" OnClientClick="javascript:return confirm('确认返回上一步？');"
                        OnClick="Button_PreStep_Click" Enabled="False" />&nbsp;&nbsp;
                    <asp:Button ID="Button_DeviceInWarehouse" runat="server" CssClass="button_bak" Text="入库"
                        Enabled="False" OnClick="Button_DeviceInWarehouse_Click" OnClientClick="javascript:return cfm2();" />&nbsp;&nbsp;
                    <input class="button_bak" onclick="javascript:if(confirm('确认取消入库？')) window.parent.hidePopWin();"
                        type="button" value="取消入库" />
                </center>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
