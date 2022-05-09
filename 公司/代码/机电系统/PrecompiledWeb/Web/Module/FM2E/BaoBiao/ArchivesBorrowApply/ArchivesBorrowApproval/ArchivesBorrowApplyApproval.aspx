<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_ArchivesManager_ArchivesBorrowApply_ArchivesBorrowApproval_ArchivesBorrowApplyApproval, App_Web_vk5gk4re" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="档案借阅申请信息审批"
        HeadOPTxt="目前操作功能：档案借阅申请审批">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="申请列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="ArchivesBorrowApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 900px">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="申请审批" ID="TabPanel1">
                        <ContentTemplate>
                            <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Module" HeaderText="系统模块"></asp:BoundField>
                                        <asp:BoundField DataField="ArchivesType" HeaderText="档案类型"></asp:BoundField>
                                        <asp:BoundField DataField="ArchivesName" HeaderText="档案名称"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        没有申请明细信息
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left" CloneFrom="">
                                </cc1:AspNetPager>
                            </div>
                            <table width="880px" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="4">
                                        档案借阅申请详细信息
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        申请表单号：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        申请借阅时间：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        借阅人：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        借阅人所属部门：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        借阅缘由：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        借阅期限：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr><td class="table_body table_body_NoWidth" style="height: 30px">
                                        申请状态：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        备注：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="880px" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="4">
                                        档案借阅申请审批信息
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        审批：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        审批意见：
                                    </td>
                                    <td colspan="3" class="table_none table_none_NoWidth">
                                        <textarea id="TextArea1" style="width: 650px; height: 67px" runat="server" title="请输入审批意见~50:"></textarea>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td colspan="4" align="right" style="height: 38px" runat="server">
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定审批" OnClick="Button1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
