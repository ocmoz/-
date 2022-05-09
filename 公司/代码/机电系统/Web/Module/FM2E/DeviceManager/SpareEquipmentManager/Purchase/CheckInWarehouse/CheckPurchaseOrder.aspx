<%@ Page Title="�鿴�ɹ���" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
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
        //�༭��ʱ������ģʽ�Ի����ֵ
        function setModalPopup(button_id) {
            var regS = new RegExp(",", "gi"); //ȥ������

            //ITEMID
            var itemid = document.getElementById(button_id.replace('Button_Check', 'Label_ItemID')).innerText.replace(regS, "");
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

            //��Ʒ����
            var name = document.getElementById(button_id.replace('Button_Check', 'Label_ProductName')).innerText;
            document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value = name;

            //����ͺ�
            var model = document.getElementById(button_id.replace('Button_Check', 'Label_Model')).innerText;
            document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value = model;



            //��λ
            var unit = document.getElementById(button_id.replace('Button_Check', 'Label_Unit')).innerText;
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].value == unit) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }

            //�ɹ�����
            document.getElementById('<%= TextBox_ActualCount.ClientID %>').value = "";

            //Ĭ����������
            document.getElementById('<%= TextBox_AcceptedCount.ClientID %>').value = "";
            //������
            document.getElementById('<%= TextBox_Producer.ClientID %>').value = "";
            //��Ӧ��
            document.getElementById('<%= TextBox_Supplier.ClientID %>').value = "";

            //�ɹ���ע
            document.getElementById('<%= TextBox_PurchaseRemark.ClientID %>').value = "";
            //���ձ�ע
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";

            //�ɹ���
            var purchaser = document.getElementById(button_id.replace('Button_Check', 'Label_Purchaser')).innerText;

            document.getElementById('<%= Label_Purchaser.ClientID %>').innerText = purchaser;

            //��ռ�����ԱID
            document.getElementById('<%= TextBox_TechnicianID.ClientID %>').value = '';
        }

        //�༭��ʱ������ģʽ�Ի����ֵ
        function setModalPopup2(button_id) {
            var regS = new RegExp(",", "gi"); //ȥ������
            //ITEMID
            var itemid = document.getElementById(button_id.replace('Button_Check2', 'Hidden_ItemID')).value;
            document.getElementById('<%= Hidden_EditItemID2.ClientID %>').value = itemid;

            var recordid = document.getElementById(button_id.replace('Button_Check2', 'Hidden_RecordID')).value;
            document.getElementById('<%= Hidden_EditRecordID.ClientID %>').value = recordid;
            //��Ʒ����
            var name = document.getElementById(button_id.replace('Button_Check2', 'Label_ProductName')).innerText;
            document.getElementById('<%= TextBox_SelectedProductName2.ClientID %>').value = name;
            //����ͺ�
            var model = document.getElementById(button_id.replace('Button_Check2', 'Label_Model')).innerText;
            document.getElementById('<%= TextBox_SelectedProductModel2.ClientID %>').value = model;

            //����
            var unitprice = document.getElementById(button_id.replace('Button_Check2', 'Label_PurchaseUnitPrice')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_UnitPrice2.ClientID %>').value = unitprice;
            
            //��λ
            var unit = document.getElementById(button_id.replace('Button_Check2', 'Label_Unit')).innerText;
            var s = document.getElementById('<%= DropDownList_Unit2.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].value == unit) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }
            //�ɹ�����
            var count = document.getElementById(button_id.replace('Button_Check2', 'Label_PurchaseCount')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_ActualCount2.ClientID %>').value = count;
            //Ĭ����������
            document.getElementById('<%= TextBox_AcceptedCount2.ClientID %>').value = "";
            //������
            var producer = document.getElementById(button_id.replace('Button_Check2', 'Label_Producer')).innerText;
            document.getElementById('<%= TextBox_Producer2.ClientID %>').value = producer;
            var supplier = document.getElementById(button_id.replace('Button_Check2', 'Label_Supplier')).innerText;
            //��Ӧ��
            document.getElementById('<%= TextBox_Supplier2.ClientID %>').value = supplier;
            //�ɹ���ע
            var remrk = document.getElementById(button_id.replace('Button_Check2', 'Label_PurchaseRemark')).innerText;
            document.getElementById('<%= TextBox_PurchaseRemark2.ClientID %>').value = remrk;
            //���ձ�ע
            document.getElementById('<%= TextBox_Remark2.ClientID %>').value = "";
            //�ɹ���
            var purchaser = document.getElementById(button_id.replace('Button_Check2', 'Label_Purchaser')).innerText;
            document.getElementById('<%= Label_Purchaser2.ClientID %>').innerText = purchaser;
            //��ռ�����ԱID
            document.getElementById('<%= TextBox_TechnicianID2.ClientID %>').value = '';
        }

        //����༭����
        function saveEditItem() {



            //��Ʒ����
            var name = trim(document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value);
            if (name.length == 0) {
                alert('�������Ʒ����');
                return;
            }
            //����ͺ�
            var model = trim(document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value);
            if (model.length == 0) {
                //alert('���������ͺ�');
                //return;
            }

            //�ɹ�����
            var unitprice = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
            if (!checkFloat(unitprice, '�ɹ�����')) {
                return;
            }

            //��λ
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('��ѡ��λ');
                return;
            }

            //�ɹ�����
            var count = trim(document.getElementById('<%= TextBox_ActualCount.ClientID %>').value);
            if (!checkFloat(count, '�ɹ�����')) {
                return;
            }

            //��������
            var acceptedcount = trim(document.getElementById('<%= TextBox_AcceptedCount.ClientID %>').value);
            if (!checkFloat(acceptedcount, '��������')) {
                return;
            }

            if (parseFloat(acceptedcount) > parseFloat(count)) {
                alert('�����������ܴ��ڲɹ�����');
                return false;
            }
            //������
            var producer = trim(document.getElementById('<%= TextBox_Producer.ClientID %>').value);
            if (producer.length == 0) {
                alert('������������');
                return;
            }
            //��Ӧ��
            var supplier = trim(document.getElementById('<%= TextBox_Supplier.ClientID %>').value);
            if (supplier.length == 0) {
                alert('�����빩Ӧ��');
                return;
            }
            //�ɹ�������
            var pp = trim(document.getElementById('<%= TextBox_PurchaserPassword.ClientID %>').value);
            if (pp.length == 0) {
                //alert('������ɹ�������');
                //return;
            }

            //�����������Լ���������������
            var t = trim(document.getElementById('<%= TextBox_TechnicianID.ClientID %>').value);
            if (t.length == 0) {
                alert('�����뼼���������û���');
                return;
            }

            var tp = trim(document.getElementById('<%= TextBox_TechnicianPassword.ClientID %>').value);
            if (tp.length == 0) {
                alert('�����뼼������������');
                return;
            }

            var cfm = confirm("����������޸ģ�ȷ�ϱ��棿");
            if (cfm == false)
                return false;

            document.getElementById('Button_Save').click();
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();


        }

        //����༭����
        function saveEditItem2() {
            //��Ʒ����
            var name = trim(document.getElementById('<%= TextBox_SelectedProductName2.ClientID %>').value);
            if (name.length == 0) {
                alert('�������Ʒ����');
                return;
            }
            //����ͺ�
            var model = trim(document.getElementById('<%= TextBox_SelectedProductModel2.ClientID %>').value);
            if (model.length == 0) {
                //alert('���������ͺ�');
                //return;
            }
            //�ɹ�����
            var unitprice = trim(document.getElementById('<%= TextBox_UnitPrice2.ClientID %>').value);
            if (!checkFloat(unitprice, '�ɹ�����')) {
                return;
            }
            //��λ
            var s = document.getElementById('<%= DropDownList_Unit2.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('��ѡ��λ');
                return;
            }
            //�ɹ�����
            var count = trim(document.getElementById('<%= TextBox_ActualCount2.ClientID %>').value);
            if (!checkFloat(count, '�ɹ�����')) {
                return;
            }
            //��������
            var acceptedcount = trim(document.getElementById('<%= TextBox_AcceptedCount2.ClientID %>').value);
            if (!checkFloat(acceptedcount, '��������')) {
                return;
            }

            if (parseFloat(acceptedcount) > parseFloat(count)) {
                alert('�����������ܴ��ڲɹ�����');
                return false;
            }
            //������
            var producer = trim(document.getElementById('<%= TextBox_Producer2.ClientID %>').value);
            if (producer.length == 0) {
                alert('������������');
                return;
            }
            //��Ӧ��
            var supplier = trim(document.getElementById('<%= TextBox_Supplier2.ClientID %>').value);
            if (supplier.length == 0) {
                alert('�����빩Ӧ��');
                return;
            }
            //�ɹ�������
            var pp = trim(document.getElementById('<%= TextBox_PurchaserPassword2.ClientID %>').value);
            if (pp.length == 0) {
                alert('������ɹ�������');
                return;
            }
            //�����������Լ���������������
            var t = trim(document.getElementById('<%= TextBox_TechnicianID2.ClientID %>').value);
            if (t.length == 0) {
                alert('�����뼼���������û���');
                return;
            }
            var tp = trim(document.getElementById('<%= TextBox_TechnicianPassword2.ClientID %>').value);
            if (tp.length == 0) {
                alert('�����뼼������������');
                return;
            }
            var cfm = confirm("����������޸ģ�ȷ�ϱ��棿");
            if (cfm == false)
                return false;
            document.getElementById('Button_Save2').click();
            document.getElementById('<%= Button_SaveItem2.ClientID %>').click();
        }

        //���������
        function checkFloat(value, text) {
            var floatVal = parseFloat(value);
            if (isNaN(floatVal) || floatVal != value) {
                alert(text + "\n���ʽ����ȷ:\n" + value + "����һ����������");
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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="��Ʒ�����ɹ�"
        HeadOPTxt="Ŀǰ�������ܣ�����" HeadHelpTxt="ǳ��ɫ����Ϊ��Ҫ���ֿ����յ���">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="�������յ��б�" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="Check.aspx" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel_CheckItem" runat="server" Style="width: 95%; 
        display: none;" CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    ���ղ��ϲɹ�����<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ��Ʒ���ƣ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductName" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ����ͺţ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductModel" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold"></span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �ɹ�����(Ԫ)��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_UnitPrice" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ��λ��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:DropDownList ID="DropDownList_Unit" runat="server">
                    </asp:DropDownList>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �ɹ�������
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_ActualCount" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ����������
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_AcceptedCount" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �����̣�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Producer" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ��Ӧ�̣�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Supplier" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �ɹ���ע��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_PurchaseRemark" runat="server" title="�����뱸ע~50:" Width="300px"></asp:TextBox>
                    �ɹ��ˣ�<asp:Label ID="Label_Purchaser" runat="server"></asp:Label>
                    &nbsp;&nbsp; ���룺<asp:TextBox ID="TextBox_PurchaserPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ���ձ�ע��
                </td>
                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="�����뱸ע~50:" Width="300px"></asp:TextBox>
                    �ֹܣ�<asp:Label ID="Label_WarehouseKeeper" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ���������ˣ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    �û�����<asp:TextBox ID="TextBox_TechnicianID" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span> ���룺<asp:TextBox ID="TextBox_TechnicianPassword" runat="server"
                            TextMode="Password"></asp:TextBox><span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save_a" class="button_bak" type="button" value="����" onclick="javascript:saveEditItem();" />
            <input id="Button_Save" class="button_bak" type="button" value="����" style="display: none" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="ȡ��" />
        </center>
    </asp:Panel>
    
    <asp:Panel ID="Panel_CheckItem2" runat="server" Style="width: 95%; 
        display: none;" CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    ���ղ��ϲɹ�����
                    <input type="hidden" value="" id="Hidden_EditItemID2" runat="server" />
                    <input type="hidden" value="" id="Hidden_EditRecordID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ��Ʒ���ƣ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductName2" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ����ͺţ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_SelectedProductModel2" runat="server"></asp:TextBox>
                    <span style="color: Red; font-weight: bold"></span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �ɹ�����(Ԫ)��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_UnitPrice2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ��λ��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:DropDownList ID="DropDownList_Unit2" runat="server">
                    </asp:DropDownList>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �ɹ�������
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_ActualCount2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ����������
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_AcceptedCount2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �����̣�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Producer2" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ��Ӧ�̣�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Supplier2" runat="server" Width="150px"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    �ɹ���ע��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_PurchaseRemark2" runat="server" title="�����뱸ע~50:" Width="300px"></asp:TextBox>
                    �ɹ��ˣ�<asp:Label ID="Label_Purchaser2" runat="server"></asp:Label>
                    &nbsp;&nbsp; ���룺<asp:TextBox ID="TextBox_PurchaserPassword2" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ���ձ�ע��
                </td>
                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Remark2" runat="server" title="�����뱸ע~50:" Width="300px"></asp:TextBox>
                    �ֹܣ�<asp:Label ID="Label_WarehouseKeeper2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    ���������ˣ�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    �û�����<asp:TextBox ID="TextBox_TechnicianID2" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span> ���룺<asp:TextBox ID="TextBox_TechnicianPassword2" runat="server"
                            TextMode="Password"></asp:TextBox><span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save_a2" class="button_bak" type="button" value="����" onclick="javascript:saveEditItem2();" />
            <input id="Button_Save2" class="button_bak" type="button" value="����" style="display: none" />
            <asp:Button ID="Button_Cancel_Edit2" runat="server" class="button_bak" Text="ȡ��" />
        </center>
    </asp:Panel>
    
    <input id="Button_SaveItem" type="button" runat="server" value="����" style="display: none"
        onserverclick="Button_Save_Click" />
    <input id="Button_SaveItem2" type="button" runat="server" value="����" style="display: none"
        onserverclick="Button_Save2_Click" />
    <input type="hidden" id="Hidden_WarehouseID" runat="server" />
    <input type="hidden" id="Hidden_WarehouseName" runat="server" />
    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    �����깺��
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="2" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="δ֪��˾"></asp:Label><br />
                    <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue"></asp:Label>&nbsp;��������깺��
                </td>
                <td style="text-align: center" colspan="2" class="Table_searchtitle">
                    �����
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
                                        ���
                                    </th>
                                      <th  style="width: 8%" class="Table_searchtitle"
                                    
                                    >ϵͳ</th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ��Ʒ����
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ����ͺ�
                                    </th>
                                    <th style="width: 15%" class="Table_searchtitle">
                                        ����<br />
                                        ����/�ɹ�/����
                                    </th>
                                    <th style="width: 5%" class="Table_searchtitle">
                                        ��λ
                                    </th>
                                    <th class="Table_searchtitle">
                                        ��ע
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ״̬
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        �ɹ���
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ����
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
                                                <asp:Button ID="Button_Check" runat="server" Text="����" CssClass="button_bak" OnClientClick="javascript:setModalPopup(this.id);" />
                                                <cc2:ModalPopupExtender ID="ModalPopupExtender_CheckItem" runat="server" TargetControlID="Button_Check"
                                                    PopupControlID="Panel_CheckItem" BackgroundCssClass="modalBackground" OkControlID="Button_Save"
                                                    CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                                </cc2:ModalPopupExtender>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td valign="top"  style="text-align:center">
                                                <a href='<%# "javascript:HideShow(\"" + Container.FindControl("gridview_ItemList").ClientID + "\");" %>'>
                                                    <span style="color: Blue">�ɹ���¼</span></a>
                                            </td>
                                            <td colspan="9">
                                                <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" DataSource='<%# Eval("PurchaseRecordList") %>'
                                                    RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="���">
                                                            <ItemTemplate>
                                                                [<asp:Label ID="Label_ID" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>]
                                                                <input type="hidden" id="Hidden_ItemID" runat="server" value='<%# Eval("PlanDetailItemID") %>' />
                                                                <input type="hidden" id="Hidden_RecordID" runat="server" value='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��Ʒ����">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����ͺ�">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����<br/>�ɹ�/����">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseCount" runat="server" Text='<%# Eval("PurchaseCount", "{0:#,0.#####}") %>'></asp:Label>
                                                                &nbsp;/&nbsp;
                                                                <asp:Label ID="Label_AcceptanceCount" runat="server" Text='<%# Eval("AcceptanceCount", "{0:#,0.#####}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��λ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����(Ԫ)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseUnitPrice" runat="server" Text='<%# Eval("PurchaseUnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="���(Ԫ)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount", "{0:#,0.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="������Ӧ">
                                                            <ItemTemplate>
                                                                �����̣�<asp:Label ID="Label_Producer" runat="server" Text='<%# Eval("Producer") %>'></asp:Label><br />
                                                                ��Ӧ�̣�<asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%"  HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="������">
                                                            <ItemTemplate>
                                                                �ɹ�Ա��<asp:Label ID="Label_Purchaser" runat="server" Text='<%# Eval("PurchaserName") %>'></asp:Label><br />
                                                                �ֹ�Ա��<asp:Label ID="Label_Checker_Warehouse" runat="server" Text='<%# Eval("WarehouseKeeperName") %>'></asp:Label><br />
                                                                ����Ա��<asp:Label ID="Label_Checker_Technician" runat="server" Text='<%# Eval("TechnicianName") %>'></asp:Label>
                                                                <br />
                                                                �ֿ⣺<asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%"  HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="״̬">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_StatusString" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��ע">
                                                            <ItemTemplate>
                                                                �ɹ���<asp:Label ID="Label_PurchaseRemark" runat="server" Text='<%# Eval("PurchaseRemark") %>'></asp:Label><br />
                                                                ���գ�<asp:Label ID="Label_AcceptanceRemark" runat="server" Text='<%# Eval("AcceptanceRemark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                    <asp:Button ID="Button_Check2" runat="server" Text="����" CssClass="button_bak" OnClientClick="javascript:setModalPopup2(this.id);" Visible='<%# (PurchaseRecordStatus)Eval("Status") == PurchaseRecordStatus.WAIT4CHECK && Eval("WarehouseID").ToString() == Hidden_WarehouseID.Value %>' />
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
                                                            ��δ�вɹ���¼</center>
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
                    �ύʱ�䣺
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    �����ˣ�
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    �����£�
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_UpdateTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    ��ǰ״̬��
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_Status" runat="server"></asp:Label><asp:Label ID="Label_Approvaling"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    ������ʷ
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
                                    <asp:TemplateField HeaderText="������ ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" Text='<%# Eval("FeeBack") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ϸ����">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Eval("ApprovalLog") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����ʱ��">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        δ������</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    �޸���ʷ
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
                                    <asp:TemplateField HeaderText="�޸��� ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Eval("ModifierName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�޸Ĳ���">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Eval("ModifyTypeString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" Text='<%# Eval("Content") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�޸�ʱ��">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Eval("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        δ���޸�</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
