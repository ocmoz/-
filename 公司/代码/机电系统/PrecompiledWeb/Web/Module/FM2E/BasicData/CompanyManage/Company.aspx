<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_CompanyManage_Company, App_Web_xtkqeoa6" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="��˾��Ϣά��"
        HeadOPTxt="Ŀǰ�������ܣ���˾��Ϣά��" HeadHelpTxt="��˾�б�Ĭ����ʾ������˾��Ϣ">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="��ӹ�˾" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditCompany.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView1_RowDataBound" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="CompanyID" HeaderText="���">
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompanyName" HeaderText="��˾����">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="��ַ">
                                    <ItemStyle Width="30%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Contact" HeaderText="��ϵ��">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="�绰">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="�ʼ���ַ">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fax" HeaderText="����">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemStyle Width="5%" />
                                    <HeaderTemplate>
                                        �ܹ�˾</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="aaa" runat="server" Text='<%# Convert.ToBoolean(Eval("IsParentCompany"))?"��":"��" %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                                                 
                               
                                <asp:ButtonField ButtonType="Image" Text="�鿴" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="�鿴" CommandName="view">
                                    <ItemStyle Width="5%" />
                                </asp:ButtonField>
                                <asp:TemplateField Visible="false">
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('ȷ��Ҫɾ���˹�˾��Ϣ��')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                    <HeaderTemplate>ɾ��</HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                               <center> û�й�˾��Ϣ</center>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                            InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString="{0}"
                            PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                
</asp:Content>
