<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SelectDevices.aspx.cs" Inherits="Module_FM2E_DeviceManager_BarCode_BatchBarCode_SelectDevices" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
    
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" OnNavigate="ScriptManager1_Navigate">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="ѡ�������豸"
        HeadOPTxt="Ŀǰ�������ܣ�ѡ�������豸" HeadHelpTxt="�豸�б�Ĭ����ʾ�豸">
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
        <ContentTemplate>
            <div style="width: 100%;">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <cc2:TabPanel runat="server" HeaderText="�豸��Ϣ�б�" ID="TabPanel1">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td valign="top"  style="width:20%">
                                        <div style="width: 100%; overflow: auto; height: 100%; border: solid 1px #ffffff;">
                                            
                                            <asp:TreeView ID="addressTree" runat="server" ShowLines="true" OnSelectedNodeChanged="addressTree_SelectedNodeChanged"
                                                OnTreeNodePopulate="addressTree_TreeNodePopulate">
                                                <SelectedNodeStyle ForeColor="#FF5050" />
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                    <td  valign="top" style="width:80%">
                                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <div id="PrintDiv" style="width: 100%;">
                                                <input id="Button_AddItem" type="button" runat="server" value="���" style="display: none"  />
                                                <input id="Hidden_SelectedItem" value="" runat="server" type="hidden" />
                                                <input id="Hidden_Mark" value="" runat="server" type="hidden" />
                                                <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                                    OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                                    OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ѡȡ"> 
                                                            <HeaderTemplate> 
                                                                <%--ȫѡ<asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" Checked="true" />--%> 
                                                            </HeaderTemplate> 
                                                            <ItemTemplate> 
                                                                <%--<input type="checkbox" ID="chkSelectThis" runat="server" /> --%>
                                                                <asp:CheckBox ID="chkSelectThis" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectThis_CheckedChanged" />
                                                             </ItemTemplate> 
                                                            <ItemStyle Width="50px" /> 
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="�豸������">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_EquipmentNO" runat="server" Text='<%# Eval("EquipmentNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Name" HeaderText="�豸����">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Model" HeaderText="�ͺ�">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="��˾" ItemStyle-Width="7%" />
                                                        <asp:BoundField DataField="SystemName" HeaderText="ϵͳ" ItemStyle-Width="7%" />
                                                        
                                                        
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                ״̬</HeaderTemplate>
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <%# EnumHelper.GetDescription((EquipmentStatus)Eval("Status")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AddressName" HeaderText="��ǰλ��">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        û���豸��Ϣ
                                                    </EmptyDataTemplate>
                                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                                </asp:GridView>
                                            </div>
                                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                                                InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr align="center">
                                    <td>
                                        <input type="hidden" id="SelectedEqmNo" runat="server" />
                                        <asp:Button ID="btReturn" runat="server" Text="ȷ��" CssClass="button_bak" OnClientClick="javascript:OnOK();" />
                                        <input type="button" value="�ر�" class="button_bak" onclick="javascript:window.parent.hidePopWin();" />
                                        <%--<input id="Printcurrentpage" runat="server" type="button" value="��ӡ��ҳ" onserverclick="PrintPreview" />--%>
                                        <%--<input id="PrintCurrentPageBarCode" runat="server" type="button" value="��ӡ��ҳ������" style="width:auto" onserverclick="PrintPreviewBarCode" />--%>
                                        <%--<input id="Printallpage" runat="server" type="button" class="button_bak" value="��ӡ����"
                                            onserverclick="PrintPreviewAll" />--%>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="�߼���ѯ" ID="TabPanel2">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="4">
                                        ��ϲ�ѯ��֧��ģ����ѯ��
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        �豸�����룺
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="EquipmentNO" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        �ʲ���ţ�
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="TextBox_AssertNumber" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        ������˾��
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="DDLCompany" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <cc2:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="CompanyInfo"
                                            Enabled="True" LoadingText="��˾��Ϣ������..." PromptText="��ѡ�������Ĺ�˾..." ServiceMethod="GetCompanyInfo"
                                            ServicePath="LocationService.asmx" TargetControlID="DDLCompany">
                                        </cc2:CascadingDropDown>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        �ɹ����ţ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="PurchaseOrderID" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        �豸���ƣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="Name" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        �ͺţ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="Model" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        Ʒ�ƣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="Specification" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        �豸���ͣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="ddlEqType" runat="server" Height="16px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        �������ƣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="CategoryName" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="CategoryID" runat="server" Visible="false"></asp:TextBox>
                                        <asp:Panel ID="Panel1" CssClass="popupControl" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="TreeView1" OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded" runat="server"
                                                        OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                                    </asp:TreeView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                        <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="CategoryName"
                                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                        <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="CategoryID"
                                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        ����ϵͳ��
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="DropDownList_System" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        ��ַ��Ϣ��
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3">
                                        <input id="TextBox_Address" type="text" style="width: 50%" runat="server" />
                                        <input type="hidden" id="Hidden_AddressCode" runat="server" />
                                        <input class="cbutton" onclick="javascript:showPopWin('ѡ���ַ','../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"
                                            type="button" value="ѡ��" id="Button_SelectAddress" />
                                        <input class="cbutton" onclick="javascript:clearAddress();" type="button" value="���"
                                            id="Button_ClearAddress" />
                                        <asp:TextBox ID="TextBox_DetailLocation" Width="20%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        ��Ӧ�̣�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="SupplierName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        �����̣�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="ProducerName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        �ɹ��ˣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="PurchaserName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        �ɹ����ڣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="PurchaseDate1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="������ɹ�����~date"></asp:TextBox>&nbsp; ��&nbsp;
                                        <asp:TextBox ID="PurchaseDate2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="������ɹ�����~date"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        �����ˣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="CheckerName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        ������ ��
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="ResponsibilityName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        �������ڣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="ExamDate1" runat="server" title="��������������~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>&nbsp; ��&nbsp;
                                        <asp:TextBox ID="ExamDate2" runat="server" title="��������������~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        �������ڣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="OpeningDate1" runat="server" title="��������������~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>&nbsp; ��&nbsp;
                                        <asp:TextBox ID="OpeningDate2" runat="server" title="��������������~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        �������ڣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="FileDate1" runat="server" title="�����뽨������~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>&nbsp; ��&nbsp;
                                        <asp:TextBox ID="FileDate2" runat="server" title="�����뽨������~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        �������ʱ�䣺
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="UpdateTime1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="�������������ʱ��~date"></asp:TextBox>&nbsp; ��&nbsp;
                                        <asp:TextBox ID="UpdateTime2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="�������������ʱ��~date"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        �豸״̬��
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="Status" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        �Ƿ���ע���ʲ���
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="IsCancel" runat="server">
                                            <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                            <asp:ListItem Value="1">��</asp:ListItem>
                                            <asp:ListItem Value="2">��</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                       �۸���ڣ�
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="tbPrice1" runat="server" title="������۸�~float">0</asp:TextBox>
                                    </td>
                                     <td class="table_body table_body_NoWidth">
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="ȷ��" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="����" />
                       
                            </center>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

        function printdiv(PrintDivID, param)//��ӡ��Ԥ���ú���
        {
            var headstr = "<html><head><title>aaa</title></head><body><object id='WebBrowser' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2' style='display:none'></object><div style='width: 649px;'>";
            var footstr = "</div></body>";
            var newstr = document.all.item(PrintDivID).innerHTML;
            var oldlocation = window.location.href;
            winname = window.open("", "_blank");
            winname.document.write(headstr + newstr + footstr);
            try {
                winname.document.title = '';
                winname.document.body.innerHTML = headstr + newstr + footstr;
                winname.document.all.WebBrowser.ExecWB(7, 1);
                winname.close();
            }
            catch (e) {
            }
            window.location.href = oldlocation;
        }

        //��ַѡ��
        function addAddress(val) {
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];
            if (addcode != '00') {
                document.getElementById('<%= Hidden_AddressCode.ClientID %>').value = addcode;
                document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
            }
        }
        function clearAddress() {
            document.getElementById('<%= Hidden_AddressCode.ClientID %>').value = '';
            document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
        }
        
        //������°�ť
        function selectItem(checkbox_id) 
        {
            var inputs = document.getElementsByTagName("INPUT");
            for (i = 0; i < inputs.length; i++) {
                if (inputs[i].type == 'checkbox' && inputs[i].id == checkbox_id) 
                {
                    if (inputs[i].checked == false) {
                        document.getElementById('<%= Hidden_Mark.ClientID %>').value = 1;
                    }
                    else if (inputs[i].checked == true) {
                        document.getElementById('<%= Hidden_Mark.ClientID %>').value = 0;
                    }
                }
            }
            var regS = new RegExp(",", "gi"); //ȥ������
            //��Ʒ����
            var equipmentno = document.getElementById(checkbox_id.replace('chkSelectThis', 'Label_EquipmentNO')).innerText;
            document.getElementById('<%= Hidden_SelectedItem.ClientID %>').value = equipmentno;
            document.getElementById('<%= Button_AddItem.ClientID %>').click();
        }


        function OnOK()
        {
            window.returnVal = document.all.<%=SelectedEqmNo.ClientID %>.value;
            window.parent.hidePopWin(true);
        }
        
        
        
    </script>

</asp:Content>

