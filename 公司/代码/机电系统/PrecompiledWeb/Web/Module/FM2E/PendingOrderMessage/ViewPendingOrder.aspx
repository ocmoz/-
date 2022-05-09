<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_PendingOrderMessage_ViewPendingOrder, App_Web_mp1r1vfu" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" OnNavigate="ScriptManager1_Navigate">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�����������"
        HeadOPTxt="Ŀǰ�������ܣ��鿴δ�Ķ�����">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="���Ķ�" ButtonUrlType="Href"
            ButtonUrl="History.aspx" />
    </cc1:HeadMenuWebControls>
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle">
                ���������б�
            </td>
        </tr>
        <tr>
            <td class="table_none_NoWidth">
                <div  runat="server" style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_PendingOrderList" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="gridview_PendingOrderList_RowDataBound" OnRowCommand="gridview_PendingOrderList_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="�´�������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_New" runat="server" Text='<%# Bind("HasReadString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Red" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBtitle" runat="server" Text='<%# Bind("Title") %>' CommandName="link"
                                                CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false"
                                                Font-Underline="true"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="45%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TypeString" HeaderText="����">
                                        <HeaderStyle />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="������">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("SenderPersonName").ToString()+"("+Eval("SendFrom").ToString()+")" %>'
                                                runat="server" ID="Label_Sender"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SendTime" HeaderText="����ʱ��">
                                        <HeaderStyle />
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibHadRead" runat="server" 
                                                ImageUrl="~/images/right.gif" CommandArgument="<%# Container.DataItemIndex %>" CommandName="read" />
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ɾ��" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="del" ImageUrl="~/images/ICON/delete.gif"
                                                Text="ɾ��" OnClientClick="javascript:return confirm('ȷ��ɾ������Ϣ��');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="7%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10" InvalidPageIndexErrorPendingOrder="ҳ����������Ч����ֵ��"
                                NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorPendingOrder="ҳ����������Χ��"
                                ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
