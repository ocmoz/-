<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_PositionManage_Position, App_Web_4fzxnxyi" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js">        function Reset1_onclick() {

        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="ְλ��Ϣά��"
        HeadOPTxt="Ŀǰ�������ܣ�ְλ��Ϣά��" HeadHelpTxt="ְλ�б�Ĭ����ʾ����ְλ">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="���ְλ" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditPosition.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="ְλ�б�" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                û��ְλ��Ϣ
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="PositionID" HeaderText="ְλ���">
                                </asp:BoundField>
                                <asp:BoundField DataField="PositionName" HeaderText="ְλ����">
                                </asp:BoundField>
                                <asp:BoundField DataField="Remark" HeaderText="ְλ����">
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Image" Text="�鿴" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="�鿴" CommandName="view">
                                </asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('ȷ��Ҫɾ����ְλ��Ϣ��')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                            InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString="{0}"
                            PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
