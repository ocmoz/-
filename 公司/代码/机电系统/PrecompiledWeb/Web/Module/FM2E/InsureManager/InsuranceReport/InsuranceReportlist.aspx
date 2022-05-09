<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_InsureManager_InsureInfoManager_InsuranceReportlist, App_Web_nlqf5cyp" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Insurance" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="������Ϣ����"
        HeadHelpTxt="�鿴������Ϣ" HeadOPTxt="Ŀǰ�������ܣ�������Ϣ�б�">
        <cc1:HeadMenuButtonItem ButtonName="����������Ϣ" ButtonIcon="new.gif" ButtonUrlType="Href" ButtonPopedom="New"
            ButtonUrl="InsuranceReportManager.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel ID="TabPanel1" HeaderText="������Ϣ" runat="server">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table  width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                             <tr>
                                 <td class="table_body table_body_NoWidth" style="width: 190px">
                                     �����ţ�<asp:TextBox ID="tb_search_insuranceNo" runat="server"></asp:TextBox>
                                 </td>
                                 <td class="table_body table_body_NoWidth" style="width: 207px">
                                     �����ţ�<asp:TextBox ID="tb_search_reportNo" runat="server"></asp:TextBox>
                                 </td>
		                        <td class="table_body table_body_NoWidth"style="width: 130px">
		                            �������ͣ�<asp:DropDownList ID="ddl_search_riskType" runat="server">
			                        </asp:DropDownList>
		                        </td>
                                <td class="table_body table_body_NoWidth"style="width: 130px">
		                            ״̬��<asp:DropDownList ID="ddl_search_state" runat="server">
			                        </asp:DropDownList>
		                        </td>
		                    </tr>
                            <tr>
		                        <td class="table_body table_body_NoWidth" style="width: 345px" >ʱ�䣺
		                            <asp:TextBox ID="tb_search_startRiskDate" runat="server" Width="80px"   class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="�����뿪ʼʱ��~date"></asp:TextBox>-
		                            <asp:TextBox ID="tb_search_endRiskDate"  runat="server"  Width="80px" class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="���������ʱ��~date"></asp:TextBox> 
		                        </td>
                                <td class="table_body table_body_NoWidth" style="width: 345px" >ʱ�䣺
		                            <asp:TextBox ID="tb_search_startReportDate" runat="server" Width="80px"   class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="�����뿪ʼʱ��~date"></asp:TextBox>-
		                            <asp:TextBox ID="tb_search_endReportDate"  runat="server"  Width="80px" class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="���������ʱ��~date"></asp:TextBox>
		                        </td>
                                <td>
                                    <asp:Button ID="btSearch" runat="server" Text="��ѯ" CssClass="button_bak" OnClick="btSearch_Click"/>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" 
                            AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
                            onrowdatabound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                û�б�����Ϣ
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ϵͳ����" Visible="false" />
                                <asp:BoundField DataField="InsuranceNo" HeaderText="������" />
                                <asp:BoundField DataField="ReportNo" HeaderText="������" />
                                <asp:BoundField DataField="RiskDate" HeaderText="��������" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="ReportDate" HeaderText="��������" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        ��������</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# EnumHelper.GetDescription((RiskType)Eval("RiskType"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        ״̬</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# EnumHelper.GetDescription((State)Eval("State"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="�鿴"
                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Link" Text="�޸�"  HeaderText="�޸�"
                                    CommandName="repair">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Link" Text="����"  HeaderText="����"
                                    CommandName="review">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <%--<asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"  OnClientClick="return confirm('ȷ��Ҫɾ����ģ����Ϣ��')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </div>
                     <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                                                InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
