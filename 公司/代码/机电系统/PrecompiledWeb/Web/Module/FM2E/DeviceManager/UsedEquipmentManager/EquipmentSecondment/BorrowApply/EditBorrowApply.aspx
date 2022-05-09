<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApply_EditBorrowApply, App_Web_r3p6tpns" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        //��ַѡ��DataFormatString="{0:yyyy-MM-dd}"
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
</script>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�豸�������"
        HeadHelpTxt="����" HeadOPTxt="Ŀǰ�������ܣ������豸���">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="��������б�" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="BorrowApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonPopedom="List"
            ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
<%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
              <%--  <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="��ӽ������" ID="TabPanel1">
                        <ContentTemplate>--%>

                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" colspan="4">
                                            �豸������뵥
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            ����ţ�
                                        </td>
                                        <td class="table_none table_none_NoWidth" colspan="3">
                                            <asp:Label ID="lbSheetNO" runat="server" Columns="20" Width="120px" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            ���뷽��
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:Label ID="lbBorrowCompany" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            �������
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:DropDownList ID="ddlLendCompany" runat="server">
                                            </asp:DropDownList>
                                            <span style="color:Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            �����ˣ�
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:Label ID="lbApplicant" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            ���뵥״̬��
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                               
                            </div>
                   <%--     </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="�༭�����ϸ" ID="TabPanel2" Visible="false">
                        <ContentTemplate>--%>
    <%--                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            ��Ʒ���ƣ�<input type="hidden" id="Hidden_EditItemIndex" runat="server" />
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbEquipmentName" runat="server" title="�������Ʒ����~20:"></asp:TextBox>
                                            <span style="color:Red">*</span>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            ����ͺţ�
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbModel" runat="server" title="���������ͺ�~20:"></asp:TextBox>
                                            <span style="color:Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            ������
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbCount" runat="server" title="����������~int"></asp:TextBox>
                                            <span style="color:Red">*</span>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            ��λ��
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:DropDownList ID="ddlUnit" runat="server">
                                            </asp:DropDownList>
                                            <span style="color:Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            �黹���ڣ�
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbReturnDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                                title="�������ύʱ��~date"></asp:TextBox>
                                                <span style="color:Red">*</span>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            ����ԭ��
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbReason" runat="server" Width="99%"
                                                title="���������ԭ��~50:"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            ʹ�õص㣺
                                        </td>
                                        <td class="table_none table_none_NoWidth" colspan="3">
                                          <input ID="TextBox_Address" type="text" style="width:70%" runat="server" onfocus="javascript:showPopWin('ѡ���ַ','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"/><span style="color: Red">*</span>
                                <input type="hidden" id="Hidden_AddressID" runat="server" />
                                    <input type="text" id="TextBox_DetailLocation" style="width: 20%;" runat="server" title="��������ϸ��ַ~40:" />
                                </td>
                                              
                                        
                                       
                                    </tr>
                                </table>
                                <center>
                                <asp:Label ID="errMsg" ForeColor="Red" runat="server"></asp:Label><br />
                                            <asp:Button ID="Button_AddUpdate" runat="server" CssClass="button_bak" Text="�����ϸ" OnClick="Button_AddUpdate_Click" />
                                           <%-- <asp:Button ID="Button5" runat="server" CssClass="button_bak" Text="�����༭" OnClick="Button5_Click" />--%>
                                       </center>
                            </div>
                            <hr style="width: 100%" />
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_Detail" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                    OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="���">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EquipmentName" HeaderText="��Ʒ����">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="����ͺ�">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Count" HeaderText="����">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="��λ">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Reason" HeaderText="����ԭ��">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AddressName" HeaderText="ʹ�õص�">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReturnDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="�黹����"
                                            HtmlEncode="False">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                          <asp:TemplateField>
                                          <HeaderTemplate>�༭</HeaderTemplate>
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                                    CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('ȷ��Ҫɾ����������ϸ��')"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                            <HeaderTemplate>ɾ��</HeaderTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                       <span style="color:Red">δ��ӽ��������ϸ��Ϣ</span>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                               
                            </div>
    <%--                        </ContentTemplate>
                            </asp:UpdatePanel>--%>
                      <%--  </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>--%>
<%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <center>
                                        <%--<asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="�༭��ϸ" OnClick="Button1_Click" />&nbsp;&nbsp;--%>
                                            <asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="����ݸ�" OnClick="Button2_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="�ύ����" OnClick="Button3_Click" OnClientClick="javascript:return confirm('ȷ���ύ��');" />&nbsp;&nbsp;
                                            <input id="Reset1" class="button_bak" type="reset" value="����" />
                                       </center>
    </div>
</asp:Content>
