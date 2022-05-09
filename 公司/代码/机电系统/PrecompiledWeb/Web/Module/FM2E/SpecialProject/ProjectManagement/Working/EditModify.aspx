<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_EditModify, App_Web_fzwntbc0" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

<script type="text/javascript">

    //�༭��ʱ������ģʽ�Ի����ֵ
    function setModalPopup(button_id, edit) {
        var regS = new RegExp(",", "gi"); //ȥ������

        //ITEMID
        if (edit) {
            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Index')).innerText;
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

            var isadd = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_IsAdd')).value;
            if (isadd == 'True') {
                document.getElementById('<%= RadioButton_Add.ClientID %>').checked = true;
                document.getElementById('<%= RadioButton_Cut.ClientID %>').checked = false;
            }
            else {
                document.getElementById('<%= RadioButton_Add.ClientID %>').checked = false;
                document.getElementById('<%= RadioButton_Cut.ClientID %>').checked = true;
            }
            

            //��Ʒ����
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DeviceName')).innerText;
            document.getElementById('<%= TextBox_Equipment.ClientID %>').value = name;

            //����ͺ�
            var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Model')).innerText;
            document.getElementById('<%= TextBox_Model.ClientID %>').value = model;

            var s = s = document.getElementById('<%= DropDownList_JobItems.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].innerText == name + ' ' + model) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }

            //����
            var price = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_UnitPrice')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = price;

            //��λ
            var unit = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Unit')).innerText;
            document.getElementById('<%= TextBox_Unit.ClientID %>').value = unit;

            //����
            var count = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Count')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_Count.ClientID %>').value = count;

            //С��
            var amount = parseFloat(price) * parseFloat(count);
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = amount;

            //��ע
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;

            document.getElementById('Button_Save').value = "����";
        }
        else {

            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = "-1";
            
            document.getElementById('<%= RadioButton_Add.ClientID %>').checked = true;
            document.getElementById('<%= RadioButton_Cut.ClientID %>').checked = false;

            document.getElementById('<%= TextBox_Equipment.ClientID %>').value = "";

            var s = document.getElementById('<%= DropDownList_JobItems.ClientID %>');

            s.options[0].selected = true;
            
            document.getElementById('<%= TextBox_Model.ClientID %>').value = "";


            document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Unit.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Count.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";

            document.getElementById('Button_Save').value = "���";
        }
    }

    //����༭����
    function saveEditItem() {

        //��Ʒ����
        var name = trim(document.getElementById('<%= TextBox_Equipment.ClientID %>').value);
        if (name.length == 0) {
            alert('�������豸');
            return false;
        }
        //����ͺ�
        var model = trim(document.getElementById('<%= TextBox_Model.ClientID %>').value);
        if (model.length == 0) {
            alert('���������ͺ�');
            return;
        }
        //����
        var price = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
        if (!checkFloat(price, '����')) {
            return;
        }
        //��λ
        var unit = trim(document.getElementById('<%= TextBox_Unit.ClientID %>').value);
        if (unit.length == 0) {
            alert('�����뵥λ');
            return;
        }
        //����
        var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
        if (!checkFloat(count, '����')) {
            return;
        }
        //��ע
        var remark = trim(document.getElementById('<%= TextBox_Remark.ClientID %>').value);

        document.getElementById('Button_OK').click();
        document.getElementById('<%= Button_SaveItem.ClientID %>').click();
    }

    //���������
    function checkFloat(value, text) {
        var floatVal = parseFloat(value);
        if (isNaN(floatVal) || floatVal != value) {
            alert(text + "\n���ʽ����ȷ:\n" + value + "����һ��������");
            return false;
        }
        return true;
    }

    //����/���۱仯��ʱ���Զ����½��С��
    function onCountChange() {
        
        var regS = new RegExp(",", "gi"); //ȥ������
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
            alert("����" + count + "�������֣�����������");
            control_count.focus();
            return;
        }
        try {
            price = parseFloat(price);
        }
        catch (e) {
            alert("����" + price + "�������֣�����������");
            control_price.focus();
            return;
        }
        document.getElementById('<%= TextBox_Amount.ClientID %>').value = price * count;

    }
    </script>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="ר��̹���--ʩ������"
        HeadOPTxt="Ŀǰ�������ܣ����̱������༭" HeadHelpTxt="���빤�̱���ľ���������Ա���������Լ�������Ŀ�ĸı�">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="�����б�" ButtonUrlType="Href"
            ButtonUrl="ModifyList.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
    
     <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 250px; display:none"
        CssClass="modalPopup">
        
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    ����豸��ϸ<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ���ӻ���٣�
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:RadioButton ID="RadioButton_Add" GroupName="Add" Text="����" runat="server" Checked="true" />
                    <asp:RadioButton ID="RadioButton_Cut" GroupName="Add" Text="����" runat="server" />
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    �豸�</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:DropDownList ID="DropDownList_JobItems" runat="server"  AutoPostBack="true"
                         onselectedindexchanged="DropDownList_JobItems_SelectedIndexChanged">
                    
                    </asp:DropDownList>
                </td>
                </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    �豸��
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_Equipment" runat="server" ></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ����ͺţ�</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                </tr>
        <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ���ۣ�</td> <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_UnitPrice" runat="server"></asp:TextBox>Ԫ<span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ��λ��</td><td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Unit" runat="server" ></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
           </tr>
           <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ������</td> <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Count" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                   ">
                    ��</td>
                   <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Amount" runat="server"></asp:TextBox>Ԫ
                </td>
           </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ��ע��</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="�����뱸ע~50:" Width="400px"></asp:TextBox>
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <center>
            <input id="Button_Save" class="button_bak"
                type="button" value="����" onclick="javascript:saveEditItem();"/>
            <input id="Button_OK" class="button_bak" style="display:none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" 
                Text="ȡ��" />
        </center>
    </asp:Panel>
    <div id="div_table">
        <center>
        <span style="font-size:large">
            ר���&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true"
                runat="server" ForeColor="Blue"></asp:Label>
            &nbsp;���������뱨�浥 [  <asp:Label ID="Label_Status" runat="server"></asp:Label>]</span>
        </center>
        <br />
        ��&nbsp;&nbsp;Ŀ�� <br />
        �а��ˣ�����λǩ�£�
        <table width="100%" cellpadding="0" cellspacing="0" border="1"
            style="border-collapse: collapse;">
            <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                <th style="width: 10%; text-align: center">
                    �����������
                </th>
                <td colspan="2" style="width: 40%;">
                    <asp:Label ID="Label_ProjectName2" runat="server"></asp:Label>
                </td>
                <th style="width: 10%; text-align: center">
                    ��������
                </th>
                <td colspan="2" style="width: 40%; ">
                    <asp:Label ID="Label_ApplyTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 30px">
                <th style="width: 3%; text-align: center">
                    �������<br />
                    ��Ԫ��
                </th>
                <td>
                    <asp:TextBox ID="TextBox_BudgetChange"
                        runat="server">
                                    
                    </asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <th style="width: 10%; text-align: center">
                    �������<br />
                    ��Ԫ��
                </th>
                <td>
                    <asp:TextBox ID="TextBox_BudgetIncDesc"
                        runat="server">
                                    
                    </asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <th style="width: 10%; text-align: center">
                    �����ӳ�����<br />
                    ���죩
                </th>
                <td>
                    <asp:TextBox ID="TextBox_DelayDays"  runat="server">
                                    
                    </asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
            </tr>
            <tr style="height: 30px">
                <td colspan="6">
                    ������ԭ�������ԭ�����ݣ�<br />
                    <asp:TextBox ID="TextBox_ChangeContent"  TextMode="MultiLine" Width="95%" 
                        runat="server" Height="100px"></asp:TextBox><span style="color:Red; font-weight:bold">*</span><br />
                    ������
                    <asp:FileUpload ID="FileUpload_File" runat="server" />
                    <asp:HyperLink ID="HyperLink_File" runat="server" ForeColor="Blue" Font-Underline="true" Visible="false"></asp:HyperLink>
                </td>
            </tr>
            <tr style="height: 30px">
                <th>
                    ��ע��</th>
                    <td colspan="5">
                    <asp:TextBox ID="TextBox_Remark2" Width="95%" runat="server" 
                           ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="word-break: break-all">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
                        style="border-collapse: collapse; background-color: white">
                        <tr style="height: 30px">
                            <th style="width: 3%;">
                                ���</th>
                                                        <th style="width: 7%;">
                                ���ӻ�����豸
                            </th>
                            <th style="width: 10%;">
                                �豸�ͺ�
                            </th>
                            <th style="width: 10%;">
                                ��������λ��
                            </th>
                            <th style="width: 10%;">
                                ���ۣ�Ԫ��
                            </th>
                            <th style="width: 10%;">
                                ���Ӽ��ٽ�Ԫ��
                            </th>
                            <th style="width: 15%;">
                                ��ע
                            </th>
                            <th style="width: 5%;">
                                �޸�
                            </th>
                            <th  style="width: 5%;">
                                ɾ�� 
                            </th>
                        </tr>
                        <asp:Repeater ID="Repeater_Detail" runat="server" 
                            onitemcommand="Repeater_Detail_ItemCommand" >
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align: center">
                                        <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                    
                                        <input type="hidden" runat="server" value='<%# Eval("IsAdd") %>' id="Hidden_IsAdd" />
                                        <asp:Label ID="Label_IsAdded"  Font-Size="Larger" Text='<%# Eval("IsAddString") %>' runat="server"  ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_DeviceName" Text='<%# Eval("DeviceName") %>' runat="server">
                                    
                                        </asp:Label>
                                        &nbsp;
                                        <asp:Label ID="Label_Model" Text='<%# Eval("Model") %>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_Count" Text='<%# Eval("Count","{0:#,0.#####}") %>' runat="server">
                                    
                                        </asp:Label>
                                        &nbsp;
                                        ��<asp:Label ID="Label_Unit" Text='<%# Eval("Unit") %>' runat="server">
                                    
                                        </asp:Label>��
                                    </td>
                                     <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_UnitPrice" Text='<%# Eval("UnitPrice","{0:#,0.#####}") %>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_IsAdded2" Text='<%# Eval("IsAddString") %>' runat="server"   ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                        </asp:Label><asp:Label ID="Label_Amount" Text='<%# Eval("Amount","{0:#,0.#####}") %>'  ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_Remark" Text='<%# Eval("Remark") %>' runat="server">
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False"
                                                ImageUrl="~/images/ICON/edit.gif" Text="�޸�" OnClientClick="javascript:setModalPopup(this.id,true);" />
                                                
                                             <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" 
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                                             </cc2:ModalPopupExtender>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                       <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="ɾ��" OnClientClick="javascript:return confirm('ȷ��ɾ�����');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <tr style="height: 30px">
                            <th  colspan="5">
                                �ϼ�
</th>
                            <th align="center">
                               <asp:Label ID="Label_TotalAmount" runat="server">
                                    
                                        </asp:Label>
                            </th>
                            <th colspan="3">
                                
                            </th>

                        </tr>
                        
                        <tr style="height: 30px">
                            <td colspan="9" align="right">
                               <input id="Button_SaveItem" type="button" runat="server" value="����" style="display: none"
                                onserverclick="Button_Save_Click" />
                                
                     <input id="Button_Add" type="button" runat="server" class="button_bak" value="����޸���" onclick="javascript:setModalPopup(this.id,false);" />
                      <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Add"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                      </cc2:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
 <tr style="height: 30px">
                <th>
                    ҵ�����������
                </th>
                <td colspan="5">
                    
                    
                    <div id="div_ownerapprovalinfo" runat="server">
                    ���������<asp:Label ID="Label_OwnerApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_OwnerFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">���Ÿ����ˣ�
                    <asp:Label ID="Label_Owner" runat="server"></asp:Label>
                    <br />
                    ���ڣ�<asp:Label ID="Label_OwnerTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <th >
                    ��Լ����ˣ�
                </th>
                <td colspan="5">
                    
                   
                    <div id="div_contractapprovalinfo" runat="server">
                    ���������<asp:Label ID="Label_ContractApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_ContractFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">��Լ�������ˣ�
                    <asp:Label ID="Label_Contract" runat="server"></asp:Label>
                    <br />
                    ���ڣ�<asp:Label ID="Label_ContractTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <th>
                    �쵼������
                </th>
                <td colspan="5">
                    
                   
                    <div id="div_leaderapprovalinfo" runat="server">
                    ���������<asp:Label ID="Label_LeaderApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_LeaderFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">�쵼��
                    <asp:Label ID="Label_Leader" runat="server"></asp:Label>
                    <br />
                    ���ڣ�<asp:Label ID="Label_LeaderTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
           
        </table>
       <center>
                    <asp:Button ID="Button_SaveModify" runat="server" Text="�ύ" 
                        CssClass="button_bak" onclick="Button_SaveModify_Click" />
              </center>
    </div>
</asp:Content>
