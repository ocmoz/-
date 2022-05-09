<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_ArchivesManager_ArchivesBorrowApply_ArchivesBorrowApply_EditArchivesBorrowApply, App_Web_cw5gfixl" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="档案借阅申请信息维护"
        HeadOPTxt="目前操作功能：档案借阅申请维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="申请列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="ArchivesBorrowApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; height: 300px;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="添加申请" ID="TabPanel1">
                <ContentTemplate>
                    <table id="outwarehouse" width="880px" cellpadding="0" cellspacing="0" border="1"
                        bordercolor="#cccccc" style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                申请详细信息
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
                                借阅期限：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TBBorrowTime" runat="server" Width="80px" title="请输入借阅期限~int"></asp:TextBox>天<span
                                    style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                借阅缘由：
                            </td>
                            <td colspan="3" class="table_none table_none_NoWidth">
                                <textarea id="TextArea1" style="width: 650px; height: 50px" runat="server" title="请输入借阅缘由~100:"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                备注：
                            </td>
                            <td colspan="3" class="table_none table_none_NoWidth">
                                <textarea id="TextArea2" style="width: 650px; height: 50px" runat="server" title="请输入备注~100:"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                申请明细信息
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                    Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Module" HeaderText="系统模块"></asp:BoundField>
                                        <asp:BoundField DataField="ArchivesType" HeaderText="档案类型"></asp:BoundField>
                                        <asp:BoundField DataField="ArchivesName" HeaderText="档案名称"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此申请明细吗？')"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr runat="server">
                            <td align="right" style="height: 38px" runat="server">
                                <asp:Label ID="lbErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:Button ID="btnAdd" runat="server" CssClass="button_bak" Text="继续添加" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnDraft" runat="server" CssClass="button_bak" Text="保存草稿" OnClick="btnDraft_Click" />
                                <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="提交申请" OnClick="btSubmit_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
</asp:Content>
