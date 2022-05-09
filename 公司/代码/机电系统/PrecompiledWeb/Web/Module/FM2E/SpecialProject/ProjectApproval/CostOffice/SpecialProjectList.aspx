<%@ page title="专项工程列表" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectApproval_CostOffice_SpecialProjectList, App_Web_4kfxef5l" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--工程审批"
        HeadOPTxt="目前操作功能：造价厅管理站设计预算审查" HeadHelpTxt="当前需要进行设计预算审查工作的工程列表">
    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    需要进行 <%= APPROVAL_NAME %> 的专项工程列表
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridview_ProjectList" runat="server" AutoGenerateColumns="False"
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ProjectID" HeaderStyle-Height="25px"
                        RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                        <Columns>
                            <asp:TemplateField HeaderText="专项工程名称">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("ProjectName") %>' runat="server" ID="Label_ProjectName"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Budget" HeaderText="预算" DataFormatString="{0:#,#.######万元}">
                                <HeaderStyle />
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusString" HeaderText="状态">
                                <HeaderStyle />
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                <HeaderStyle />
                                <ItemStyle Width="25%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="审批" ShowHeader="False">
                                <ItemTemplate>
                                    <a href='<%# "ViewProject.aspx?cmd=approval&projectid=" + Eval("ProjectID") %>'>
                                        <img src="~/images/ICON/select.gif" runat="server" style="border:0" />
                                    </a>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <center>
                                当前没有需要审批的工程
                            </center>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                        NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                        ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
