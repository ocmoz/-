<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageGroupCheck.master"
    AutoEventWireup="true" CodeFile="MonthlyBudget.aspx.cs" Inherits="Module_FM2E_BudgetManager_BudgetStaticsManager_MonthlyBudget"
    Title="�ޱ���ҳ" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="Ԥ��ͳ�Ƹ���"
        HeadOPTxt="Ŀǰ�������ܣ�Ԥ��ͳ�Ƹ���">
        <%-- <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="����¶�Ԥ��" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="MakeMonthlyBudget.aspx?cmd=add" />--%>
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc2:TabPanel runat="server" HeaderText="ʵ�ʿ�֧¼��" ID="TabPanel1">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                        HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                        OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Year" HeaderText="���"></asp:BoundField>
                                            <asp:BoundField DataField="Month" HeaderText="�·�"></asp:BoundField>
                                            <asp:BoundField DataField="Title" HeaderText="����"></asp:BoundField>
                                            <asp:BoundField DataField="BudgetApply" HeaderText="���Ԥ���ܶ�"></asp:BoundField>
                                            <asp:BoundField DataField="TotalExpenditure" HeaderText="�ۼ��ѿ�֧"></asp:BoundField>
                                            <asp:BoundField DataField="NonPayment" HeaderText="δ����"></asp:BoundField>
                                            <asp:BoundField DataField="BudgetPermonth" HeaderText="Ԥ����"></asp:BoundField>
                                            <asp:BoundField DataField="Total" HeaderText="�ϼ���"></asp:BoundField>
                                            <asp:BoundField DataField="SurplusExpenditure" HeaderText="���ɿ�֧"></asp:BoundField>
                                            <asp:BoundField DataField="MakeTime" HeaderText="����ʱ��"></asp:BoundField>
                                            <asp:BoundField DataField="StatusShow" HeaderText="�Ƿ�����д��֧"></asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" Text="ʵ�ʿ�֧" ImageUrl="~/images/ICON/select.gif"
                                                HeaderText="ʵ�ʿ�֧" CommandName="approval"></asp:ButtonField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            û���¶�Ԥ����Ϣ
                                        </EmptyDataTemplate>
                                        <RowStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                    <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                        AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                                        InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString=""
                                        PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                                    </cc1:AspNetPager>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="ͳ�Ƹ�����ñ���" ID="TabPanel2">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    ��ѡ��ͳ�Ƶ���ʼ���ºͽ�������
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="��������ʼ���~A:int!" id="BeginYear" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="BeginMonth" title="~A!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="�������ֹ���~A:int!" id="EndYear" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="EndMonth" title="~A!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="table_body table_body_NoWidth">
                                    ���ţ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                <input type="text" id="Title1" runat="server" />(Ĭ��Ϊȫ������)
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="ȷ��" class="button_bak" onmouseover="javascript:causeValidate = true;group='A';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Click" id="Sure" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" DisplayGroupTree="False" runat="server"
                                        AutoDataBind="true" DisplayToolbar="False" PrintMode="ActiveX" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="���ٷ���ʹ�ý���" ID="TabPanel3">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    ��ѡ��ͳ�Ƶ���ʼ���ºͽ�������
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="��������ʼ���~B:int!" id="BeginYear2" runat="server" />
                                    ��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="BeginMonth2" title="~B!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="�������ֹ���~B:int!" id="EndYear2" runat="server" />
                                    ��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="EndMonth2" title="~B!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    �������ͣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:TextBox title="��ѡ���������~B:!" onfocus='javascript:document.getElementById("CrystalView").style.display = "none";'
                                        ID="SubIDNametb" runat="server"></asp:TextBox>
                                    <asp:TextBox title="��ѡ���������~B:!" ID="SubIDtb" runat="server" Visible="False"></asp:TextBox>
                                    <asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                                        <div style="border: 1px outset white; width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="TreeView2" runat="server" onclick="javascript:causeValidate = false;"
                                                        OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                                    </asp:TreeView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </asp:Panel>
                                    <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="SubIDNametb"
                                        PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                        ExtenderControlID="">
                                    </cc2:PopupControlExtender>
                                    <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="SubIDtb"
                                        PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                        ExtenderControlID="">
                                    </cc2:PopupControlExtender>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="ȷ��" class="button_bak" onmouseover="javascript:causeValidate = true;group='B';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure2_Click" id="Sure2" />
                                </td>
                            </tr>
                            <tr id="CrystalView">
                                <td colspan="4">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer2" DisplayGroupTree="False" runat="server"
                                        AutoDataBind="True" DisplayToolbar="False" PrintMode="ActiveX" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="ͳ��ʵ�ʿ�֧��ϸ" ID="TabPanel4">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    ��ѡ��ͳ�Ƶ���ʼ���ºͽ�������
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="��������ʼ���~C:int!" id="StartYear4" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="StartMonth4" title="~C!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="�������ֹ���~C:int!" id="EndYear4" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="EndMonth4" title="~C!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ���ţ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input type="text" id="Title4" runat="server" />(Ĭ��Ϊȫ������)
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    �տ��
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input type="text" id="Supplier5" runat="server" />Ĭ��Ϊ�����տ
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="ȷ��" class="button_bak" onmouseover="javascript:causeValidate = true;group='C';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Click4" id="SureButton4" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr style="font-weight: bold" align="center">
                                            <td colspan="50">
                                                ͳ��ʵ�ʿ�֧��ϸ����
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="HeadRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr style="font-weight: bold">
                                                    <td>
                                                        ���
                                                    </td>
                                                    <td>
                                                        ��Ŀ
                                                    </td>
                                                    <td>
                                                        �տ
                                                    </td>
                                                    <td>
                                                        ������
                                                    </td>
                                                    <asp:Repeater ID="AllTotalList" runat="server" DataSource='<%# Eval("Totallist") %>'>
                                                        <ItemTemplate>
                                                            <td>
                                                                <%# Eval("CompanyName")%>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td>
                                                        <%# Eval("ExpenditureName")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="StaticsBudgetDetail" runat="server">
                                            <ItemTemplate>
                                                <tr style='background-color: #efefef; display: <%# Convert.ToDecimal(Eval("RealExpenditure"))==0?"none":"block" %>'>
                                                    <td align="left" colspan="50">
                                                        <%# Container.ItemIndex+1%>��<%# Eval("SubName")%>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="Detail" runat="server" DataSource='<%# Eval("BudgetDetailList") %>'>
                                                    <ItemTemplate>
                                                        <tr style='background-color: #8f8f8f; display: <%# Convert.ToDecimal(Eval("RealExpenditure"))==0?"none":"block" %>''>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <%# Container.ItemIndex+1%>��<%# Eval("ExpenditureName")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Supplier")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Manager")%>
                                                            </td>
                                                            <asp:Repeater ID="DetailCompany" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <%# Eval("RealExpenditure")%>
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <td>
                                                                <%# Eval("RealExpenditure")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr style='background-color: #bfbfbf; display: <%# Convert.ToDecimal(Eval("RealExpenditure"))==0?"none":"block" %>''>
                                                    <td>
                                                        С��
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <asp:Repeater ID="DetailTotal" runat="server" DataSource='<%# Eval("Totallist") %>'>
                                                        <ItemTemplate>
                                                            <td>
                                                                <%# Eval("CompanyExpenditure")%>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td>
                                                        <%# Eval("RealExpenditure") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="TotalRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr style="background-color: Red">
                                                    <td>
                                                        �ܼ�
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <asp:Repeater ID="AllTotalList" runat="server" DataSource='<%# Eval("Totallist") %>'>
                                                        <ItemTemplate>
                                                            <td>
                                                                <%# Eval("CompanyExpenditure")%>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td>
                                                        <%# Eval("RealExpenditure") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr style="font-weight: bold" align="center">
                                            <td colspan="50">
                                                <input type="button" runat="server" value="����" class="button_bak" onclick='javascript:exportbutclick();'
                                                    id="exporttempbutton" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="����Ԥ��ʹ�����" ID="TabPanel5">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    ��ѡ��Ҫ����Ԥ�����ݺ��·�
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="���������~D:int!" id="Year5" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    �·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="Month5" title="~D!" runat="server">
                                        <asp:ListItem Value="12">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ���ţ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input type="text" id="Title5" title="�����벿��~D!" runat="server" />
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="ȷ��" class="button_bak" onmouseover="javascript:causeValidate = true;group='D';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Click5" id="Sure_Button5" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr style="font-weight: bold" align="center">
                                <td colspan="50">
                                    Ԥ�����ʹ�ý��ȱ���
                                </td>
                            </tr>
                            <asp:Repeater ID="FirstHeadRepeater" runat="server">
                                <ItemTemplate>
                                    <tr style="font-weight: bold; background-color: #8f8f8f" align="center">
                                        <td>
                                            <%#Eval("SubName")%>
                                        </td>
                                        <asp:Repeater ID="FirstHeadRepeater2" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                            <ItemTemplate>
                                                <td colspan="3">
                                                    <%#Eval("CompanyName")%>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="SecondHeadRepeater" runat="server">
                                <ItemTemplate>
                                    <tr style="font-weight: bold" align="center">
                                        <td>
                                            <%#Eval("SubName")%>
                                        </td>
                                        <asp:Repeater ID="SecondHeadRepeater2" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                            <ItemTemplate>
                                                <td>
                                                    ���Ԥ��
                                                </td>
                                                <td>
                                                    �ۼƿ�֧
                                                </td>
                                                <td>
                                                    ���ɿ�֧
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr style='background-color: <%# (Container.ItemIndex%2)==0?"#bfbfbf":"#efefef"%>'>
                                        <td>
                                            <%#Eval("SubName")%>
                                        </td>
                                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("CompanyList") %>'>
                                            <ItemTemplate>
                                                <td>
                                                    <%#Eval("BudgetYear")%>
                                                </td>
                                                <td>
                                                    <%#Eval("HavePaid")%>
                                                </td>
                                                <td>
                                                    <%#Eval("LeftMoney")%>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="��ϸ�����鿴" ID="TabPanel6">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    ������Ҫͳ�Ƶ�����
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="���������~E:int!" id="Year6" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="ȷ��" class="button_bak" onmouseover="javascript:causeValidate = true;group='E';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Click6" id="Sure_Button6" />
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="showtable" visible="false" border="1">
                            <tr align="center">
                                <td colspan="3">
                                    Ԥ�����<input type="text" size="4" id="Year" readonly="readonly" runat="server" />
                                    Ԥ�㲿��<input type="text" id="INPTitle" readonly="readonly" runat="server" />
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="center" style="font-weight: bold;">
                                    ������Ŀ
                                </td>
                                <td align="center" style="font-weight: bold;">
                                    �ܿ�֧
                                </td>
                                <td align="center" style="font-weight: bold;">
                                    ��ռ����
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="left" valign="top">
                                    <div>
                                        <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_OnSelectedNodeChanged"
                                            OnTreeNodeCollapsed="TreeView1_OnTreeNodeCollapsed" OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded">
                                            <NodeStyle VerticalPadding="1px" Height="16px" />
                                        </asp:TreeView>
                                    </div>
                                </td>
                                <td style="width: 100px;">
                                    <div id="TotalExpenditurediv" runat="server">
                                    </div>
                                </td>
                                <td style="width: 100px;">
                                    <div id="PercentDiv" runat="server">
                                    </div>
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    �ϼ�:
                                </td>
                                <td align="left">
                                    <input type="text" size="14" id="TotalTotalExpenditure" readonly="readonly" value='<%=(Session["TotalTotalExpenditure"]!=null)?Session["TotalTotalExpenditure"]:"" %>' />
                                </td>
                                <td align="left">
                                    <input type="text" size="14" id="TotalPercent" readonly="readonly" value='<%=(Session["TotalPercent"]!=null)?Session["TotalPercent"]:"" %>%' />
                                </td>
                            </tr>
                            <%--<tr align="center">
                    <td colspan="7">
                    ��ע:<asp:TextBox ID="Remark" runat="server" Height="50px" Width="240px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>--%>
                        </table>
                        <input type="hidden" id="sessionvalue" runat="server" />
                        <input id="companycount" runat="server" type="hidden" />
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="ʵ�ʿ�֧��ϸ�嵥" ID="TabPanel7">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    ��ѡ����ϸ����ʼ���ºͽ�������
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="��������ʼ���~F:int!" id="syearf" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ʼ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="smonthf" title="~F!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ��ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input size="4" type="text" title="�������ֹ���~F:int!" id="eyearf" runat="server" />��
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    ��ֹ�·ݣ�
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="emonthf" title="~F!" runat="server">
                                        <asp:ListItem Value="0">-��ѡ��-</asp:ListItem>
                                        <asp:ListItem Value="1">1��</asp:ListItem>
                                        <asp:ListItem Value="2">2��</asp:ListItem>
                                        <asp:ListItem Value="3">3��</asp:ListItem>
                                        <asp:ListItem Value="4">4��</asp:ListItem>
                                        <asp:ListItem Value="5">5��</asp:ListItem>
                                        <asp:ListItem Value="6">6��</asp:ListItem>
                                        <asp:ListItem Value="7">7��</asp:ListItem>
                                        <asp:ListItem Value="8">8��</asp:ListItem>
                                        <asp:ListItem Value="9">9��</asp:ListItem>
                                        <asp:ListItem Value="10">10��</asp:ListItem>
                                        <asp:ListItem Value="11">11��</asp:ListItem>
                                        <asp:ListItem Value="12">12��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" align="center" colspan="4">
                                    <input type="button" runat="server" value="ȷ��" class="button_bak" onmouseover="javascript:causeValidate = true;group='F';"
                                        onmouseout="javascript:causeValidate = false;" onserverclick="Sure_Clickf" id="surebuttonf" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="40">
                                    <table width="100%">
                                        <tr style="font-weight: bold; width: 100%" align="center">
                                            <td colspan="11">
                                                ʵ�ʿ�֧��ϸ�嵥
                                            </td>
                                        </tr>
                                        <tr style='background-color: #8f8f8f; font-weight: bolder; width: 100%'>
                                            <td style="width: 5%">
                                                Ԥ���Ŀ
                                            </td>
                                            <td style="width: 5%">
                                                ��֧��Ŀ
                                            </td>
                                            <td style="width: 10%">
                                                �տ
                                            </td>
                                            <td style="width: 10%">
                                                ������
                                            </td>
                                            <td style="width: 10%">
                                                ��������
                                            </td>
                                            <asp:Repeater ID="RepeaterHead3" runat="server">
                                                <ItemTemplate>
                                                    <td style="width: 10%">
                                                        <%# Eval("CompanyName")%>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td style="width: 10%">
                                                �ϼ�
                                            </td>
                                            <td style="width: 10%">
                                                ��������
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="detailreportf" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <ItemTemplate>
                                                <tr style='background-color: #E3EEFB;'>
                                                    <td colspan="5" style="font-weight: bolder">
                                                        <%# Container.ItemIndex+1%>��<%# Eval("Key")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="subcom1"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="subcom2"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="subcom3"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="subcom4"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="suball"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="Repeater2" OnItemDataBound="Repeater2_ItemDataBound" DataSource='<%#Eval("Value")%>'
                                                    runat="server">
                                                    <ItemTemplate>
                                                        <tr style='background-color: #EFEFEF;'>
                                                            <td>
                                                            </td>
                                                            <td colspan="4">
                                                                <%# Container.ItemIndex+1%>��<asp:Label runat="server" ID="exname"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="excom1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="excom2"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="excom3"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="excom4"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="exall"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater DataSource='<%#Eval("Value")%>' runat="server">
                                                            <ItemTemplate>
                                                                <asp:Repeater ID="Repeater3" DataSource='<%#Eval("Value")%>' runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Supplier")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Manager")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# DateTime.Compare(Convert.ToDateTime(Eval("PayDate")), DateTime.MinValue) == 0 ? "" : Convert.ToDateTime(Eval("PayDate")).ToString("yyyy-MM-dd")%>
                                                                            </td>
                                                                            <asp:Repeater DataSource='<%#Eval("CompanyList")%>' runat="server">
                                                                                <ItemTemplate>
                                                                                    <td>
                                                                                        <%# Eval("CompanyExpenditure")%>
                                                                                    </td>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                            <td>
                                                                                <%# Eval("BudgetApprove")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Year")%>��<%# Eval("Month")%>��
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
            <center style="display: none">
                <asp:Button ID="btnExport" runat="server" CssClass="button_bak" Text="����" OnClick="btnExport_Click">
                </asp:Button>
            </center>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        causeValidate = false;
        
        var oldcolor = "";
        function setslectrowcolor(str) {
            var list = document.getElementsByTagName("input");
            var oldcolor2 = oldcolor;
            for (var i = 0; i < list.length; i++) {
                if (list.item(i).title == str && list.item(i).title != "") {
                    oldcolor = list.item(i).style.background;
                    list.item(i).style.background = "Yellow";
                }
                else
                    if (list.item(i).style.background == "yellow") {
                    list.item(i).style.background = oldcolor2;
                }
            }
        }

        function exportbutclick() {
            document.getElementById('<%= btnExport.ClientID %>').click();
        }
    </script>

</asp:Content>
