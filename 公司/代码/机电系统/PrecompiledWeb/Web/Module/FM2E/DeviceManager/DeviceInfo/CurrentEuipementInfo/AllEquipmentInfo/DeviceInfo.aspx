<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_AllEquipmentInfo_DeviceInfo, App_Web_kt0jmzjw" title="Untitled Page" enableeventvalidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" OnNavigate="ScriptManager1_Navigate">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�����豸����"
        HeadOPTxt="Ŀǰ�������ܣ������豸����" HeadHelpTxt="�豸�б�Ĭ����ʾ�����豸">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="����豸" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditDevice.aspx?cmd=add" />
        <%--            <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />--%>
        <%--   <cc1:HeadMenuButtonItem ButtonIcon="move.gif" ButtonName="�����豸��Ϣ" ButtonPopedom="New" ButtonVisible="true" ButtonUrlType="href" ButtonUrl="Import.aspx"/>--%>
        <%--        <cc1:HeadMenuButtonItem ButtonIcon="xls.gif" ButtonName="�������" ButtonPopedom="Print"
            ButtonVisible="true" ButtonUrlType="Href" ButtonUrl="?cmd=export" />--%>
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="width: 100%;">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <cc2:TabPanel runat="server" HeaderText="�豸��Ϣ�б�" ID="TabPanel1">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td valign="top" style="width: 20%">
                                        <div style="width: 100%; overflow: auto; height: 100%; border: solid 1px #ffffff;">
                                            <asp:TreeView ID="addressTree" runat="server" ShowLines="true" OnSelectedNodeChanged="addressTree_SelectedNodeChanged"
                                                OnTreeNodePopulate="addressTree_TreeNodePopulate">
                                                <SelectedNodeStyle ForeColor="#FF5050" />
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                    <td valign="top" style="width: 80%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    ���ƣ�<asp:TextBox ID="TextBox_FilterName" runat="server" OnTextChanged="OnFilter" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    �ͺţ�<asp:TextBox ID="TextBox_FilterModel" runat="server" OnTextChanged="OnFilter"
                                                        AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    ��˾��<asp:DropDownList ID="DropDownList_FilterCompany" runat="server" OnSelectedIndexChanged="OnFilter"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    ϵͳ��<asp:DropDownList ID="DropDownList_FilterSystem" runat="server" OnSelectedIndexChanged="OnFilter"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    ״̬��<asp:DropDownList ID="DropDownList_FilterStatus" runat="server" OnSelectedIndexChanged="OnFilter"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <div id="PrintDiv" style="width: 100%;">
                                                <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                                    OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                                    OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="EquipmentNO" HeaderText="�豸������">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Name" HeaderText="�豸����">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Model" HeaderText="�ͺ�">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="��˾" ItemStyle-Width="7%" />
                                                        <asp:BoundField DataField="SystemName" HeaderText="ϵͳ" ItemStyle-Width="7%" />
                                                        <asp:BoundField DataField="PurchaseDate" HeaderText="�ɹ�����" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Price" HeaderText="�۸�(Ԫ)" DataFormatString="{0:#,0.##}">
                                                            <ItemStyle Width="6%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Count" HeaderText="����">
                                                            <ItemStyle Width="3%" />
                                                        </asp:BoundField>
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
                                                        <asp:ButtonField ButtonType="Image" Text="�鿴" ImageUrl="~/images/ICON/select.gif"
                                                            HeaderText="�鿴" CommandName="view">
                                                            <ItemStyle Width="5%" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                ɾ��</HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('ȷ��Ҫɾ�����豸��Ϣ��')"
                                                                    CausesValidation="false" Visible='<%# (Eval("Visible").ToString()=="True")?true:false %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
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
                                            <div style="text-align: left;">
                                                ��ǰ�豸����Ϊ��<asp:Label ID="lbCurrentDeviceCount" runat="server"></asp:Label></div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <%--<table width="100%">
                                <tr align="center">
                                    <td>
                                        <input id="Printcurrentpage" runat="server" type="button" class="button_bak" value="��ӡ��ҳ" onserverclick="PrintPreview" />
                                        <input id="Printallpage" runat="server" type="button" class="button_bak" value="��ӡ"
                                            onserverclick="PrintPreviewAll" />
                                    </td>
                                </tr>
                            </table>--%>
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
                                </asp:DropDownList>
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
                                        <input class="cbutton" onclick="javascript:showPopWin('ѡ���ַ','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"
                                            type="button" value="ѡ��" id="Button_SelectAddress" />
                                        <input class="cbutton" onclick="javascript:clearAddress();" type="button" value="���"
                                            id="Button_ClearAddress" />
                                        <asp:TextBox ID="TextBox_DetailLocation" Width="20%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display:none;">
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
                                        ά�޵�λ��
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="ddlMaintainTeam" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ά�޴�����
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="minRepairedTimes" runat="Server"></asp:TextBox>
                                        &nbsp;�������������ֵ�� &nbsp;
                                        <asp:TextBox ID="maxRepairedTimes" runat="Server"></asp:TextBox>
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
    </script>

</asp:Content>
