<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManager_SubjectRelationManager_SubjectRelation, App_Web_2ijhrgq6" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="预算科目关系管理"
        HeadOPTxt="目前操作功能：预算科目关系管理" HeadHelpTxt="预算科目列表默认显示最近新增科目">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加预算科目" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditSubjectRelation.aspx?cmd=add" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 900px;" border="1">
                <tr>
                    <td style="width: 200px;" align="left" valign="top">
                        <div style="width: 200px; overflow: scroll; overflow-y: hidden">
                            <asp:TreeView ID="TreeView1" runat="server">
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="width: 700px;" align="left" valign="top">
                        <div style="width: 100%; height: 100%;">
                            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                                <cc2:TabPanel runat="server" HeaderText="预算科目列表" ID="TabPanel1">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                            HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                            OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                            OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="Name" HeaderText="科目名称"></asp:BoundField>
                                                <asp:BoundField DataField="ParentName" HeaderText="上级科目名称"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        是否最底层科目
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#(Convert.ToInt32(Eval("IsLeaf"))==1)?"是":"否"%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                                    HeaderText="查看" CommandName="view"></asp:ButtonField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        删除
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此预算科目信息吗？')"
                                                            CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                没有预算科目信息
                                            </EmptyDataTemplate>
                                            <RowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                        </cc1:AspNetPager>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" HeaderText="预算科目查询" ID="TabPanel2">
                                    <ContentTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                            style="border-collapse: collapse;">
                                            <tr>
                                                <td class="Table_searchtitle" colspan="4">
                                                    组合查询（支持模糊查询）
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    科目名称：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <input id="Name" runat="server" type="text" />
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    上级科目名称：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <input id="ParentName" runat="server" type="text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    是否最底层科目：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:DropDownList ID="IsLeaf" runat="server">
                                                        <asp:ListItem Value="0" Selected="True" Text="--请选择--"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="否"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                                            runat="server">
                                            <tr>
                                                <td align="center" style="height: 38px">
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                            </cc2:TabContainer>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
