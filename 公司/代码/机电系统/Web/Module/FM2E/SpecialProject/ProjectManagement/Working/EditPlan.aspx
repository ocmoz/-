<%@ Page Title="ʩ���ƻ��༭" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="EditPlan.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_EditPlan" %>

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
                var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_ItemID')).value;
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

                //������Ŀ
                var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemName')).innerText;
                document.getElementById('<%= TextBox_ItemName.ClientID %>').value = name;

                //ǰ����Ŀ
                var prefixid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_PrefixItemID')).value;
                var s = document.getElementById('<%= DropDownList_PrefixItem.ClientID %>');
                for (i = 0; i < s.options.length; i++) {
                    if (s.options[i].value == prefixid) {
                        s.options[i].selected = true;

                    }
                    else {
                        s.options[i].selected = false;

                    }
                }
                if (prefixid != '0') {
                    document.getElementById('<%= span_inputdays.ClientID %>').style.display = 'block';
                    document.getElementById('<%= tr_startend.ClientID %>').style.display = 'none';
                }
                else {
                    document.getElementById('<%= span_inputdays.ClientID %>').style.display = 'none';
                    document.getElementById('<%= tr_startend.ClientID %>').style.display = 'block';
                }

                document.getElementById('<%= Hidden_SelectPrefixItemID.ClientID %>').value = prefixid;
                //ǰ��N���
                var daysafter_control = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_DaysAfter'));
                if (daysafter_control != null) {
                    daysafter = daysafter_control.value;
                    document.getElementById('<%= TextBox_DaysAfter.ClientID %>').value = daysafter;
                }

                //��ʼ
                var start = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_StartTime')).innerText;
                document.getElementById('<%= TextBox_StartTime.ClientID %>').value = start;

                //����
                var end = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_EndTime')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_EndTime.ClientID %>').value = end;

                //����
                var hr = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_HRPlan')).innerText;
                document.getElementById('<%= TextBox_HRPlan.ClientID %>').value = hr;

                //�豸
                var device = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DevicePlan')).innerText;
                document.getElementById('<%= TextBox_DevicePlan.ClientID %>').value = device;

                document.getElementById('Button_Save').value = "����";
            }
            else {

                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = 0;


                document.getElementById('<%= TextBox_ItemName.ClientID %>').value = "";



                document.getElementById('<%= DropDownList_PrefixItem.ClientID %>').options[0].selected = true;

                document.getElementById('<%= span_inputdays.ClientID %>').style.display = 'none';
                
                document.getElementById('<%= tr_startend.ClientID %>').style.display = 'block';

                document.getElementById('<%= Hidden_SelectPrefixItemID.ClientID %>').value = 0;

                document.getElementById('<%= TextBox_DaysAfter.ClientID %>').value = 0;


                document.getElementById('<%= TextBox_StartTime.ClientID %>').value = "";


                document.getElementById('<%= TextBox_EndTime.ClientID %>').value = "";


                document.getElementById('<%= TextBox_HRPlan.ClientID %>').value = "";


                document.getElementById('<%= TextBox_DevicePlan.ClientID %>').value = "";

                document.getElementById('Button_Save').value = "���";
            }
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

        //����༭����
        function saveEditItem() {

            //��Ŀ����
            var name = trim(document.getElementById('<%= TextBox_ItemName.ClientID %>').value);
            if (name.length == 0) {
                alert('�����빤����Ŀ����');
                return false;
            }

            var daysafter = trim(document.getElementById('<%= TextBox_DaysAfter.ClientID %>').value);
            if (document.getElementById('<%= DropDownList_PrefixItem.ClientID %>').selectedIndex > 0) {
                if (!checkInt(daysafter, '�ӳ�����')) {
                    return false;
                }
            }

            var s = document.getElementById('<%= DropDownList_PrefixItem.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('������ǰ������Ϣ');
                return;
            }

            //��ʼʱ��
            if (s.selectedIndex == 1) {
                
                var start = trim(document.getElementById('<%= TextBox_StartTime.ClientID %>').value);
                if (!checkDate(start, '�����뿪ʼʱ��')) {
                    return false;
                }

                //����ʱ�� 
               
                var enddate = trim(document.getElementById('<%= TextBox_EndTime.ClientID %>').value);


                if (!checkDate(enddate, '���������ʱ��')) {
                    return false;
                }

                if (!compareTimeLtEqual(start, enddate)) {
                    alert('����ʱ�䲻�����ڿ�ʼʱ��');
                    return false;
                }
            }
            //����
            var hr = trim(document.getElementById('<%= TextBox_HRPlan.ClientID %>').value);

            //��ע
            var device = trim(document.getElementById('<%= TextBox_DevicePlan.ClientID %>').value);

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

        //ʱ�ڼ��
        function checkDate(value, text) {
            //����
            var found = value.match(/(\d{1,5})-(\d{1,2})-(\d{1,2})/);
            var found2 = value.match(/(\d{1,5})\/(\d{1,2})\/(\d{1,2})/);
            if (found == null || found[0] != value || found[2] > 12 || found[3] > 31) {
                if (found2 == null || found2[0] != value || found2[2] > 12 || found2[3] > 31) {
                    //alert(info+"\n"+name+"�ĸ�ʽ����ȷ:\n\""+value+"\"����һ������\n��ʾ��[2000-01-01]");
                    alert(text + "\n���ʽ����ȷ:\n\"" + value + "\"����һ������\n��ʾ��[2000-01-01]");
                    return false;
                }
            }
            var year = trim0(found[1]);
            var month = trim0(found[2]) - 1;
            var date = trim0(found[3]);
            var d = new Date(year, month, date);
            if (d.getFullYear() != year || d.getMonth() != month || d.getDate() != date) {
                //alert(info+"\n"+name+"�����ݲ���ȷ:\n\""+value+"\"����һ����ȷ������\n��ʾ��[2000-01-01]");
                alert(text + "\n�����ݲ���ȷ:\n\"" + value + "\"����һ����ȷ������\n��ʾ��[2000-01-01]");
                return false;
            }
            return true;
        }


        //�Ƚ�ʱ��,�ж�v1�Ƿ�С�ڵ���v2
        function compareTimeLtEqual(v1, v2) {
            var found_v1 = v1.match(/(\d{1,5})-(\d{1,2})-(\d{1,2})/);
            var year_v1 = trim0(found_v1[1]);
            var month_v1 = trim0(found_v1[2]) - 1;
            var date_v1 = trim0(found_v1[3]);
            var d_v1 = new Date(year_v1, month_v1, date_v1);

            var found_v2 = v2.match(/(\d{1,5})-(\d{1,2})-(\d{1,2})/);
            var year_v2 = trim0(found_v2[1]);
            var month_v2 = trim0(found_v2[2]) - 1;
            var date_v2 = trim0(found_v2[3]);
            var d_v2 = new Date(year_v2, month_v2, date_v2);

            return d_v1 <= d_v2;
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="ר��̹���--ʩ������"
        HeadOPTxt="Ŀǰ�������ܣ�ʩ���ƻ�" HeadHelpTxt="ʩ���ƻ���ʩ�����ݣ��Զ����ɺ��ͼ�Ա�ο�">
       <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="Href"
            ButtonUrl="ViewProject.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 250px; display:none"
        CssClass="modalPopup">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                    text-indent: 13px; border: solid 1px #a7c5e2;" border="1" id="table_edit" runat="server">
                    <tr>
                        <td class="Table_searchtitle" style="text-align: center" colspan="2">
                            ������<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            ��Ŀ��
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;">
                            <asp:TextBox ID="TextBox_ItemName" runat="server"></asp:TextBox><span style="color: Red;
                                font-weight: bold">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            ǰ����Ŀ��
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;">
                            <asp:DropDownList ID="DropDownList_PrefixItem" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="DropDownList_PrefixItem_SelectedIndexChanged">
                            </asp:DropDownList>
                            <span style="color: Red; font-weight: bold">*</span>
                            <input type="hidden" value="" id="Hidden_SelectPrefixItemID" runat="server" />
                            <input type="hidden" value="" id="Hidden_SelectPrefixEndTime" runat="server" />
                            <span id="span_inputdays" style="display: none" runat="server">
                                <asp:TextBox ID="TextBox_DaysAfter" runat="server" Text="1"></asp:TextBox>���ʼ ����
                                <asp:TextBox ID="TextBox_DaysSpan" runat="server" Text="1"></asp:TextBox>�� </span>
                        </td>
                    </tr>
                    <tr id="tr_startend" runat="server">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            ��ֹ���ڣ�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;">
                            <asp:TextBox ID="TextBox_StartTime" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>-<asp:TextBox
                                ID="TextBox_EndTime" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            �������ţ�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;">
                            <asp:TextBox ID="TextBox_HRPlan" runat="server" title="��������������~50:" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            �豸���ţ�
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;">
                            <asp:TextBox ID="TextBox_DevicePlan" runat="server" title="�������豸����~50:" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <center>
            <input id="Button_Save" class="button_bak" type="button" value="����" onclick="javascript:saveEditItem();" />
            <input id="Button_OK" class="button_bak" style="display: none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="ȡ��" />
        </center>
    </asp:Panel>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    ר���&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true"
                        runat="server" ForeColor="Blue"></asp:Label>
                    &nbsp;ʩ���ƻ�
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridview_PlanItemList" runat="server" AutoGenerateColumns="False"
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px"
                        OnRowDeleting="gridview_ItemList_RowDeleted" RowStyle-Height="20px" Width="100%"
                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                        <Columns>
                            <asp:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��Ŀ">
                                <ItemTemplate>
                                    <asp:Label ID="Label_ItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ǰ����Ŀ">
                                <ItemTemplate>
                                    <asp:Label ID="Label_PrefixItemName" runat="server" Text='<%# Eval("PrefixItemName") %>'></asp:Label>
                                    <input type="hidden" id="Hidden_PrefixItemID" value='<%# Eval("PrefixItemID") %>'
                                        runat="server" />
                                    <span id="span_daysafter" runat="server" visible='<%# Convert.ToInt64( Eval("PrefixItemID"))>0 %>'>
                                        (<asp:Label ID="Label_DaysAfter" runat="server" Text='<%# Eval("DaysAfter") %> '></asp:Label>���ʼ)</span>
                                    <input type="hidden" id="Hidden_DaysAfter" value='<%# Eval("DaysAfter") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ʱ��">
                                <ItemTemplate>
                                    <asp:Label ID="Label_StartTime" runat="server" Text='<%# Eval("StartTime", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                    -
                                    <asp:Label ID="Label_EndTime" runat="server" Text='<%# Eval("EndTime", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ʱ��(��)">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Days" runat="server" Text='<%# Eval("Days") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��������">
                                <ItemTemplate>
                                    <asp:Label ID="Label_HRPlan" runat="server" Text='<%# Eval("HRPlan") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="�豸����">
                                <ItemTemplate>
                                    <asp:Label ID="Label_DevicePlan" runat="server" Text='<%# Eval("DevicePlan") %>'></asp:Label>
                                    </FooterTemplate>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��ǰ����">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Progress" runat="server" Text='<%# Eval("Progress","{0:P}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="״̬">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="�޸�" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                        Text="�޸�" OnClientClick="javascript:setModalPopup(this.id,true);" />
                                    <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                        PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_OK"
                                        CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                    </cc2:ModalPopupExtender>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ɾ��" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                        CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="ɾ��" OnClientClick="javascript:return confirm('ȷ��ɾ�����');" />
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <center>
                                δ��ʩ���ƻ�</center>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
             </table>
             <center>
                    <input id="Button_SaveItem" type="button" runat="server" value="����" style="display: none"
                        onserverclick="Button_Save_Click" />
                    <input id="Button_Add" type="button" runat="server" class="button_bak2" value="��ӹ�����"
                        onclick="javascript:setModalPopup(this.id,false);" />
                    <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Add"
                        PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_OK"
                        CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                    </cc2:ModalPopupExtender>
             </center>
       
    </div>
    <div id="div_graph">
        <table id="Table_Graph" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    ר���&nbsp;<asp:Label ID="Label_ProjectName2" Font-Underline="true" Font-Bold="true" runat="server"
                        ForeColor="Blue"></asp:Label>
                    &nbsp;ʩ���ƻ����ͼ
                </td>
            </tr>
             <tr>
                <td class="Table_searchtitle">
                  <div id="div_gant" class="fixed" enableviewstate="false" runat="server" style="width:900px; height:400px; overflow:auto"></div></td>
            </tr>
        </table>
    </div>
</asp:Content>
