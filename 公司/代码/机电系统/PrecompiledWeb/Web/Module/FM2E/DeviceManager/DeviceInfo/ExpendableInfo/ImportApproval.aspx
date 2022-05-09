<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ImportApproval, App_Web_4xxpdmqi" title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品审批导入"
        HeadOPTxt="目前操作功能：易耗品审批导入">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="易耗品列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="Expendable.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel1" runat="server" Style="display: none; width: 300px">
        <asp:Image ID="Image1" runat="server" Width="300px" />
    </asp:Panel>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel ID="TabPanellist" runat="server">
                <HeaderTemplate>
                    审批列表单</HeaderTemplate>
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView AutoGenerateColumns="False" Width="100%"  ID="sheetlist" runat="server" OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="编号">
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="名称">
                                    <ItemTemplate>
                                        <asp:Label ID="name" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="申请时间">
                                    <ItemTemplate>
                                        <asp:Label ID="time" runat="server" Text='<%# Eval("time") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="行政业务意见">
                                    <ItemTemplate>
                                        <asp:Label ID="xinzhengyewustr" runat="server" Text='<%# Eval("xinzhengyewustr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="综合事务意见">
                                    <ItemTemplate>
                                        <asp:Label ID="zongheshiwustr" runat="server" Text='<%# Eval("zongheshiwustr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="计划财务意见">
                                    <ItemTemplate>
                                        <asp:Label ID="jihuacaiwustr" runat="server" Text='<%# Eval("jihuacaiwustr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="分管领导意见">
                                    <ItemTemplate>
                                        <asp:Label ID="fenguanlingdaostr" runat="server" Text='<%# Eval("fenguanlingdaostr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="总经理意见">
                                    <ItemTemplate>
                                        <asp:Label ID="zongjinlistr" runat="server" Text='<%# Eval("zongjinlistr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Image" Text="审批" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="审批" CommandName="edit">
                                    <ItemStyle Width="5%" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="查看" CommandName="view">
                                    <ItemStyle Width="5%" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="{0}"
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanelImport" runat="server">
                <HeaderTemplate>
                    批量导入</HeaderTemplate>
                <ContentTemplate>
                    <asp:FileUpload ID="FileUpload_ImportDevice" runat="server" />
                    <asp:Button ID="Button_Import" runat="server" Text="导入" OnClick="Button_Import_Click"
                        CssClass="button_bak" />
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
