<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionMaintain_MalfunctionList, App_Web_roqfceyc" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
         
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="���ϴ���"
        HeadHelpTxt="Ĭ����ʾ���µĴ�������ϵ�" HeadOPTxt="Ŀǰ�������ܣ����ϴ����б�">
        <cc1:HeadMenuButtonItem ButtonName="������Ĺ��ϵ�" ButtonIcon="list.gif" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="MalfunctionSheets.aspx" />
        <cc1:HeadMenuButtonItem ButtonName="�鿴���еĹ��ϵ�" ButtonIcon="list.gif" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="MyMalfunctionSheets.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:Timer ID="Timer_Refresh" runat="server" OnTick="Timer_Refresh_Tick">
        </asp:Timer>
        <%--         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer_Refresh" EventName="Tick" />
        </Triggers>
        <ContentTemplate>--%>
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="���ϴ����б�" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                ���ϲ��ţ�<asp:DropDownList ID="ddlFilterDepartment" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                ���ϼ�¼�ˣ�<asp:TextBox ID="tbFilterRecorder" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                ά�޵�λ��<asp:DropDownList ID="ddlFilterMaintainTeam" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                ���ϵȼ���<asp:DropDownList ID="ddlFilterRank" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btFilter" runat="server" Text="GO" OnClick="btFilter_Click" />
                            </td>
                            <td style="width: 20%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                        OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SheetNO" HeaderText="���ϵ����">
                                <HeaderStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AddressName" HeaderText="���ϵص�">
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    �豸������
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#((string)Eval("AddressDetail")).Split('@')[0]%>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MalfunctionDescription" HeaderText="��������">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DepartmentName" HeaderText="���ϲ���">
                                <HeaderStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReportDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="����ʱ��"
                                HtmlEncode="False">
                                <HeaderStyle Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MaintainDeptName" HeaderText="ά�޵�λ">
                                <HeaderStyle Width="8%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    ����״̬
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#EnumHelper.GetDescription((Enum)Eval("Status")) %>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                            </asp:TemplateField>
                           
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/Approval.gif" HeaderText="����"
                                CommandName="view">
                                <HeaderStyle Width="60px" />
                            </asp:ButtonField>
                        </Columns>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            û���κεĹ��ϴ���
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                    </asp:GridView>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��&lt;font color='red'&gt;0&lt;/font&gt;  ҳ�룺1/1  ÿҳ��"
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
                                ά�޵�λ��
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:DropDownList ID="ddlMaintainTeam" runat="server">
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
        <%--        </ContentTemplate>
        </asp:UpdatePanel>--%>
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
