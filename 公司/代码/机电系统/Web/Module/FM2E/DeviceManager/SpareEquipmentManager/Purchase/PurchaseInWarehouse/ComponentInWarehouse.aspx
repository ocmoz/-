<%@ Page Title="��ֲ������" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="ComponentInWarehouse.aspx.cs"
    Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_ComponentInWarehouse" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">

        //���������
        function checkInt(value, text) {
            var intVal = parseInt(value);
            if (isNaN(intVal) || intVal != value) {
                alert(text + "\n���ʽ����ȷ:\n" + value + "����һ��������");
                return false;
            }
            return true;
        }

        //�豸���
        function cfm2() {

            var name = document.getElementById('<%= TextBox_ProductName.ClientID %>').value;
            if (name == null || name == "") {
                alert('�����벿������');
                return false;
            }


            //��λ
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('��ѡ��λ');
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
    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
        style="border-collapse: collapse;">
        <tr style="display: none">
            <td class="Table_searchtitle" colspan="4">
                �������Ϣ��Ϣ
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="text-align: right">
                ��Ʒ���ƣ�
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:TextBox ID="TextBox_ProductName" runat="server"></asp:TextBox><span style="color: Red;
                    font-weight: bold">*</span>
            </td>
            <td class="table_body table_body_NoWidth" style="text-align: right">
                ����ͺţ�
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="text-align: right">
                ����������
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="Label_Count" runat="server"></asp:Label>
            </td>
            <td class="table_body table_body_NoWidth" style="text-align: right">
                ��λ��
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="DropDownList_Unit" runat="server">
                </asp:DropDownList>
                <span style="color: Red; font-weight: bold">*</span>
            </td>
        </tr>
        <tr style="display: none">
            <td class="table_body table_body_NoWidth" style="text-align: right">
                ���ձ�ע��
            </td>
            <td class="table_none table_none_NoWidth" colspan="3">
                <asp:Label ID="Label_CheckRemark" runat="server"></asp:Label>
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
                ������/�ɹ��ˣ�
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="Label_ResponsiblityName" runat="server"></asp:Label>
                <input type="hidden" runat="server" id="Hidden_Responsiblity" />
                &nbsp;/&nbsp;
                <asp:Label ID="Label_Purchaser" runat="server"></asp:Label>
                <input type="hidden" runat="server" id="Hidden_Purchaser" />
            </td>
            <td class="table_body table_body_NoWidth" style="text-align: right">
                ����/�ֿ������ˣ�
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="Label_Technician" runat="server"></asp:Label><input type="hidden"
                    runat="server" id="Hidden_Techincian" />
                &nbsp;/&nbsp;
                <asp:Label ID="Label_WarehouserKeeper" runat="server"></asp:Label><input type="hidden"
                    runat="server" id="Hidden_WarehouseKeeper" />
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
                <asp:Label ID="Label_Price" runat="server" Text="0"></asp:Label>Ԫ(��ֲ�������Ҫ����۸�)
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
        <tr id="tr11" runat="server">
            <td runat="server">
            </td>
            <td id="Td1" runat="server" colspan="3">
                <div id="t1" style="display: none">
                    <img src="" id="myimg"></div>
            </td>
        </tr>
    </table>
    <center>
        <asp:Button ID="Button_DeviceInWarehouse" runat="server" CssClass="button_bak" Text="���"
            OnClick="Button_DeviceInWarehouse_Click" OnClientClick="javascript:return cfm2();" />&nbsp;&nbsp;
        <input class="button_bak" onclick="javascript:if(confirm('ȷ��ȡ����⣿')) window.parent.hidePopWin();"
            type="button" value="ȡ�����" />
    </center>
</asp:Content>
