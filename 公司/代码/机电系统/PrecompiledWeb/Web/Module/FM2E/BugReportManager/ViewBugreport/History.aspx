<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BugReportManager_ViewBugreport_History, App_Web_uggv5sxo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="意见列表"
        HeadOPTxt="目前操作功能：审查用户意见">
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                <cc2:TabPanel HeaderText="待修改用户意见" runat="server" ID="TabPanel1">
                    <ContentTemplate>
                        <asp:GridView ID="gridview2" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                            HeaderStyle-Height="25px" RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center"
                            RowStyle-HorizontalAlign="center" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="SenderID" HeaderText="反馈人">
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Title" HeaderText="标题">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Message" HeaderText="内容">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="ReportTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Image" Text="反馈" ImageUrl="~/images/ICON/back.gif"
                                    HeaderText="反馈" CommandName="view">
                                    <ItemStyle Width="5%" />
                                </asp:ButtonField>
                            </Columns>
                            <RowStyle Height="20px" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager2_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel HeaderText="已修改用户意见" runat="server" ID="TabPanel2">
                    <ContentTemplate>
                        <asp:GridView ID="gridview_MessageList" runat="server" AutoGenerateColumns="False"
                            HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                            Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                            OnRowDataBound="gridview_MessageList_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                               <asp:BoundField DataField="SenderID" HeaderText="反馈人">
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Title" HeaderText="标题">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Message" HeaderText="内容">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="ReportTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                    HeaderText="查看" CommandName="view">
                                    <ItemStyle Width="5%" />
                                </asp:ButtonField>
                            </Columns>
                            <RowStyle Height="20px" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel HeaderText="查询用户意见" runat="server" ID="TabPanel3">
                    <ContentTemplate>
                        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    请输入查询条件(支持模糊查询)
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    意见标题：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:TextBox ID="TB_FTitle" title="请输入标题~50:" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    是否已修改：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="DDL_Status" runat="server">
                                        <asp:ListItem Value="1">--未修改--</asp:ListItem>
                                        <asp:ListItem Value="2">--已修改--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    <asp:Button ID="btn_Sure" runat="server" Text="确定" CssClass="button_bak" OnClick="btn_Sure_Click" />
                                    &nbsp;<input id="btn_Clear2" type="reset" value="清空" class="button_bak" onclick="javascipt:return confirm('确认清空？');"
                                        tabindex="2" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
