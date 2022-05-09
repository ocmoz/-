<%@ page title="ִ�����" language="C#" masterpagefile="~/MasterPage/MasterPage.master" enableeventvalidation="false" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_DoInWarehouse, App_Web_e2vo_fnn" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">

        //��ʾ���ض���
        function ShowHideObject(id, show) {
            document.getElementById(id).style.display = show ? "" : "none";
        }

        //�������
        function checkInt(value, text) {
            var intVal = parseInt(value);
            if (isNaN(intVal) || intVal != value) {
                alert(text + "\n���ʽ����ȷ:\n" + value + "����һ��������");
                return false;
            }
            return true;
        }


        //��һ��������
        function cfm() {
            //���ѡ���������Ʒ
            var ck = document.getElementById('<%= RadioButton_Expendable.ClientID %>');
            var name = document.getElementById('<%= Label_ProductName.ClientID %>').innerText;
            var model = document.getElementById('<%= Label_Model.ClientID %>').innerText;
            var count = document.getElementById('<%= Label_Count.ClientID %>').innerText;
            var unit = document.getElementById('<%= Label_Unit.ClientID %>').innerText;
            if (ck.checked) {//����Ʒ
                //����׺�Ʒ������
                var typeid = document.getElementById('<%= Hidden_ExpendableTypeID.ClientID %>').value;
                if (typeid == '') {

                    alert('��ѡ���׺�Ʒ����');
                    return false;
                }
                return confirm('���������޸ģ�ȷ����⣿');
            }
            else {

                var count = document.getElementById('<%= TextBox_BarCodeCount.ClientID %>').value;
                if (!checkInt(count, "��������")) {
                    return false;
                }
            }
        }

        //�豸���
        function cfm2() {
            //���ѡ���������Ʒ
            var count = document.getElementById('<%= TextBox_BarCodeCount.ClientID %>').value;
            if (!checkInt(count, "��������")) {
                return false;
            }

            var catagroy = document.getElementById('<%= Hidden_CategoryID.ClientID %>').value;
            if (catagroy == null || catagroy == "") {
                alert('��ѡ���豸����');
                return false;
            }

          

            return confirm('���������޸ģ�ȷ����⣿');

        }

        //��ַѡ��
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
        <cc2:TabPanel runat="server" HeaderText="����" ID="TabPanel_Type">
            <HeaderTemplate>
                ѡ���Ʒ����
            </HeaderTemplate>
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            ��Ʒ��Ϣ
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ��Ʒ���ƣ�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_ProductName" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ����ͺţ�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Model" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ����������
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Count" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ��λ��
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Unit" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ���ձ�ע��
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                            <asp:Label ID="Label_CheckRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            ��Ʒ����
                        </td>
                    </tr>
                    <tr>
                        <td class="table_none table_none_NoWidth" style="height: 30px; text-align: center"
                            colspan="2">
                            <asp:RadioButton ID="RadioButton_Device" runat="server" Checked="true" Text="�豸"
                                GroupName="Type" />
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px; text-align: center"
                            colspan="2">
                            <asp:RadioButton ID="RadioButton_Expendable" runat="server" GroupName="Type" Text="�׺�Ʒ" />
                        </td>
                    </tr>
                    <tr id="tr_barcode">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ������������
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_BarCodeCount" runat="server"></asp:TextBox><span style="color: Red;
                                font-weight: bold">*</span>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            �豸�ɲ�֣�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:CheckBox ID="CheckBox_Divide" runat="server" />
                        </td>
                    </tr>
                    <tr id="tr_component">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            �������
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                            <asp:CheckBox ID="CheckBox_Component" runat="server" />
                        </td>
                    </tr>
                    <tr id="tr_expandabletype" style="display:none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            �׺�Ʒ���ࣺ
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
                            <asp:Button ID="Button_Next" runat="server" CssClass="button_bak" Text="��һ��" OnClick="Button_Next_Click"
                                OnClientClick="javascript:return cfm();" />&nbsp;&nbsp;
                            <input class="button_bak" onclick="javascript:window.parent.hidePopWin();" type="button"
                                value="ȡ�����" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="�豸��Ϣ" ID="TabPanel_DeviceInfo">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr style="display: none">
                        <td class="Table_searchtitle" colspan="4">
                            ��Ʒ��Ϣ
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ��Ʒ���ƣ�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_ProductName2" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ����ͺţ�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Model2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ����������
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Count2" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ��λ��
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:Label ID="Label_Unit2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            ���ձ�ע��
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                            <asp:Label ID="Label_Remark2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="Table_searchtitle" colspan="4">
                            �豸��ϸ��Ϣ
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �ʲ���ţ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_AssertNumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �豸���ͣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_SerialNum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ���ࣺ
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
                            ϵͳ��
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:DropDownList ID="DropDownList_System" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ��ַ��Ϣ��
                        </td>
                        <td class="table_none table_none_NoWidth" colspan="3">
                            <input id="TextBox_Address" type="text" style="width: 70%" runat="server" onfocus="javascript:showPopWin('ѡ���ַ','../../../../BasicData/AddressManage/Address.aspx?operator=select', 880, 400, addAddress,true,true);" />
                            <input type="hidden" id="Hidden_AddressID" runat="server" />
                            <input class="cbutton" onclick="javascript:clearAddress();" type="button" value="���"
                                id="Button_ClearAddress" />
                            <asp:TextBox ID="TextBox_DetailLocation" runat="server" Width="20%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �����̣�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Producer" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ��Ӧ�̣�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Supplier" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �����ˣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_ResponsiblityName" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="Hidden_Responsiblity" />
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �ɹ��ˣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Purchaser" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="Hidden_Purchaser" />
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ���������ˣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Technician" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="Hidden_Techincian" />
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �ֿ������ˣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <input type="hidden" runat="server" id="Hidden_WarehouseKeeper" />
                            <asp:Label ID="Label_WarehouserKeeper" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �ɹ����ڣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_PurchaseDate" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �������ڣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_AcceptanceDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ����ʱ����
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_Warranty" runat="server"></asp:TextBox>����
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ʹ�����ޣ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:TextBox ID="TextBox_ServiceLife" runat="server"></asp:TextBox>��
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            �ɹ����ţ�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_PurchaseOrderID" runat="server"></asp:Label>
                        </td>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ����۸�
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:Label ID="Label_Price" runat="server"></asp:Label>Ԫ
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ��ע��
                        </td>
                        <td class="table_none table_none_NoWidth" colspan="3">
                            <asp:TextBox ID="TextBox_Remark" runat="server" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td class="table_body table_body_NoWidth" style="text-align: right" >
                            ����Excel���룺
                        </td>
                        <td class="table_none table_none_NoWidth"  colspan="3">
                        <asp:FileUpload ID="ImportEqBar" runat="Server" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="text-align: right">
                            ͼƬ��
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
                    <asp:Button ID="Button_PreStep" runat="server" CssClass="button_bak" Text="��һ��" OnClientClick="javascript:return confirm('ȷ�Ϸ�����һ����');"
                        OnClick="Button_PreStep_Click" Enabled="False" />&nbsp;&nbsp;
                    <asp:Button ID="Button_DeviceInWarehouse" runat="server" CssClass="button_bak" Text="���"
                        Enabled="False" OnClick="Button_DeviceInWarehouse_Click" OnClientClick="javascript:return cfm2();" />&nbsp;&nbsp;
                    <input class="button_bak" onclick="javascript:if(confirm('ȷ��ȡ����⣿')) window.parent.hidePopWin();"
                        type="button" value="ȡ�����" />
                </center>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
