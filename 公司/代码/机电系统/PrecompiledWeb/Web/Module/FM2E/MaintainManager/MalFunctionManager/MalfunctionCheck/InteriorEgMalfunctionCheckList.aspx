<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionCheck_InteriorEgMalfunctionCheckList, App_Web_js492bxd" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Maintain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="���ϴ�������" HeadOPTxt="Ŀǰ�������ܣ����ϴ��������б�">
<%--            <cc1:HeadMenuButtonItem ButtonName="���ϵǼ���ʷ" ButtonIcon="list.gif" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="RecordHistory.aspx" />--%>
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="���ϴ��������б�" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%">
                    <tr>
                    <td style="text-align:left;">���ϲ��ţ�<asp:DropDownList ID="ddlFilterDepartment" runat="server">
                        </asp:DropDownList></td>
                    <td style="text-align:left;">����ϵͳ��<asp:DropDownList ID = "ddlSystem" runat="Server"></asp:DropDownList></td>
                    <td style="text-align:left;">ά�޵�λ��<asp:DropDownList ID="ddlFilterMaintainTeam" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:left;">���ϵȼ���<asp:DropDownList ID="ddlFilterRank" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:left; display:none;">��״̬��<asp:DropDownList ID="ddlFilterStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:left;"><asp:Button ID="btFilter" runat="server" Text="GO" OnClick="btFilter_Click" /></td>
                    </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        <EmptyDataTemplate>
                            û���κεĹ��ϴ���
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="SheetNO" HeaderText="���ϵ����" />
                            <asp:BoundField DataField="DepartmentName" HeaderText="���ϲ���" />
                            <asp:BoundField DataField="ReportDate" HeaderText="����ʱ��" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}" />
                            <%--<asp:BoundField DataField="UpdateTime" HeaderText="����ʱ��" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}" />--%>
                            <asp:BoundField DataField="RecorderName" HeaderText="��¼��" />
                            <asp:BoundField DataField="MaintainDeptName" HeaderText="ά�޵�λ" />
                            <asp:BoundField DataField="AddressName" HeaderText="���ϵ�ַ" />
                            <%--<asp:TemplateField>
                            <HeaderTemplate>�����豸</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Repeater ID="rptFaultyEquipments" runat="server" DataSource='<%#Eval("FaultyEquipments") %>'>
                                <ItemTemplate>
                                <%#Eval("EquipmentName") %>
                                <span style='display:<%#!string.IsNullOrEmpty(Convert.ToString(Eval("EquipmentNO")))?"inline":"none"%>'>(<%#Eval("EquipmentNO") %>)</span><br />
                                </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    ����״̬</HeaderTemplate>
                                <ItemTemplate>
                                  <%#EnumHelper.GetDescription((Enum)Eval("Status")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    ����</HeaderTemplate>
                                <ItemTemplate>
                                  <asp:ImageButton  CommandArgument="<%# Container.DataItemIndex %>" AlternateText="����" runat="server" ImageUrl="~/images/ICON/Approval.gif" ID="ibCheck" CommandName="check" Visible='<%#Convert.ToInt32(Eval("Status"))==(int)MalfunctionHandleStatus.Wait4EngineerCheck?true:false  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  
                            <asp:TemplateField>
                                <ItemStyle Width="60px" />
                                <HeaderTemplate>
                                    ɾ��</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                        CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('ȷ��Ҫɾ���˹��������')"
                                        CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            --%>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                        InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString="{0}"
                        PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
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
                                ���ϴ����ţ�
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbSheetNO" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ���ϲ��ţ�
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlDepartment" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                �����豸�����룺
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbEquipmentNO" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                �����豸���ƣ�
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                 <asp:TextBox ID="tbEquipmentName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                �����ˣ�
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbReporter" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ����ʱ�䣺
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbReportTimeFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="�����뱨��ʱ��~date"></asp:TextBox>&nbsp;��&nbsp;<asp:TextBox ID="tbReportTimeTo"
                                        runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="�����뱨��ʱ��~date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ���ϼ�¼�ˣ�
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbRecorderName" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ά�޵�λ��
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlMaintainTeam" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                          <tr style="display:none">
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ������״̬��
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="ȷ��" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="����" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script language="javascript" type="text/javascript">
        //�ػ�س��¼�
        function document.onkeydown() {
            var tagName = event.srcElement.tagName.toUpperCase();
            if (event.keyCode == 13) {
                $get('<%=btFilter.ClientID %>').click();
            }
        }
    </script>
</asp:Content>