<%@ page title="�༭ά����¼" language="C#" masterpagefile="~/MasterPage/MasterPageNoCheck.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainSheet_EditMaintainSheet, App_Web_ltngjpvs" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="ά����¼�༭"
        HeadOPTxt="Ŀǰ�������ܣ�ά����¼�༭" HeadHelpTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="ά����¼�б�" ButtonUrlType="Href"
            ButtonUrl="MaintainSheetList.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <%--  <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="ά���ƻ�" ID="TabPanel1">
                <ContentTemplate>--%>
        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td colspan="4" class="table_body_WithoutWidth">
                        ά������<asp:Label ID="Label_TemplateSheetName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ϵͳ��
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="Label_SystemName" runat="server"></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ά�����ţ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="Label_DeparmentName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ά���ˣ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="Label_Maintainer" runat="server"></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ά��ʱ�䣺
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:TextBox ID="TextBox_MaintainTime" class="input_calender" onfocus="javascript:HS_setDate(this);"
                            runat="server" title="������ά��ʱ��~B:date!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ���ͣ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="Label_TypeName" runat="server"></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ���ڣ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="Label_Period" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��ַ��
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="Label_AddressName" runat="server"></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        ��ʱ��
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Image ID="Image_IsTemp" runat="server" ImageUrl="~/images/right.gif" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        ����ע��
                    </td>
                    <td class="table_none table_none_NoWidth" colspan="3">
                        <asp:Label ID="Label_Remark" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        ά����ע��
                    </td>
                    <td class="table_none table_none_NoWidth" colspan="3">
                        <asp:TextBox ID="TextBox_MaintainRemark" runat="server" Width="95%" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel_Edit" runat="server">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="table_body_WithoutWidth">
                                ά���豸(����ʽ��
                                <asp:DropDownList ID="ordertype" AutoPostBack="true" runat="server" 
                                    onselectedindexchanged="ordertype_SelectedIndexChanged">
                                    <asp:ListItem Value="address">����ַ</asp:ListItem>
                                    <asp:ListItem Value="name">���豸����</asp:ListItem>
                                </asp:DropDownList>
                                )
                            </td>
                        </tr>
                        <tr>
                            <td class="table_none table_none_NoWidth">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <asp:Repeater ID="Repeater_EquipmentList" runat="server" OnItemDataBound="Repeater_EquipmentList_ItemDataBound"
                                        OnItemCommand="Repeater_EquipmentList_ItemCommand">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex % CountPerRow ==0?"<tr>":""%>
                                            <td>
                                                <a style="color: Blue;" href="javascript:showPopWin('�豸��Ϣ�鿴','../../../DeviceManager/DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/ViewDeviceInfo.aspx?cmd=view&id=<%# Eval("EquipmentID") %>',800, 430, null,true,true);">
                                                    <%# Eval("EquipmentName") %></a><br />
                                                [<%# Eval("AddressName") %><%# Eval("DetailLocation") %>]
                                                <asp:ImageButton ID="ImageButton_Delete" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                    CommandName="DeleteCMD" ImageUrl="~/images/ICON/delete.gif" Visible='<%# Eval("IsExtra") %>' />
                                                <asp:RadioButtonList ID="RadioButtonList_Normal" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="RadioButtonList_Normal_Changed" AutoPostBack="true">
                                                    <asp:ListItem Value="1" Text="����"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="�쳣"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <span id="span_input" runat="server" visible="false">�豸״̬��<asp:DropDownList ID="DropDownList_NewStatus"
                                                    runat="server">
                                                </asp:DropDownList>
                                                    ��ע��<asp:TextBox ID="TextBox_EquipmentRemark" runat="server"></asp:TextBox>
                                                </span>
                                            </td>
                                            <%# Container.ItemIndex % CountPerRow == (CountPerRow-1) ?"</tr>":""%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body_WithoutWidth">
                                <font color="red">�쳣�豸</font>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 100%;">
                                    <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" OnRowDataBound="GridView1_OnRowDataBound"
                                        OnRowCreated="GridView1_OnRowCreated">
                                        <Columns>
                                            <asp:BoundField DataField="EquipmentNO" HeaderText="�豸������">
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EquipmentName" HeaderText="�豸����">
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EquipmentModel" HeaderText="�ͺ�">
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DetailLocation" HeaderText="��װλ��">
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="�쳣���">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DropDownList_NewStatus" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TextBox_EquipmentRemark" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            û���쳣�豸
                                        </EmptyDataTemplate>
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--    </ContentTemplate>
            </cc2:TabPanel>
            
        </cc2:TabContainer>--%>
        <asp:UpdatePanel ID="UpdatePanel_OtherEquipment" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td colspan="4" class="table_body_WithoutWidth">
                            ����ά������
                            <asp:Label ID="Label_ErrMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            �����룺
                            <asp:TextBox ID="TextBox_OtherEquipmentNO" runat="server" title="�������豸����~A20:"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            ���ƣ�<asp:TextBox ID="TextBox_OtherName" title="�������豸����~A20:!" runat="server"></asp:TextBox>
                            <font color="red">*</font>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            �ͺţ�<asp:TextBox ID="TextBox_OtherModel" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            ��װλ�ã�<asp:TextBox ID="TextBox_OtherLocation" runat="server" title="�����밲װλ��~A50:!"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth">
                            �豸״̬��<asp:DropDownList ID="DropDownList_OtherStatus" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="table_body table_body_NoWidth">
                            ��ע��<asp:TextBox ID="TextBox_OtherRemark" runat="server" Width="75%"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth">
                            <asp:Button ID="Button_AddOther" runat="server" class="button_bak" Text="���" OnClick="Button_AddOther_Click"
                                OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'A')" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <center>
            <asp:Button ID="Button_Submit" Text="����" runat="server" OnClick="Button_Submit_Click"
                CssClass="button_bak" OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'B')&&confirm('ȷ�ϱ��棿');" />
            <asp:Button ID="Button_Cancel" OnClick="Button_Cancel_Click" runat="server" class="button_bak"
                Text="ȡ��" OnClientClick="javascript:return confirm('ȷ��ȡ����');" />
        </center>
    </div>
</asp:Content>
