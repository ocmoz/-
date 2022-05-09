<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Dept.aspx.cs" Inherits="Module_FM2E_BasicData_DeptManage_Dept" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="部门信息维护"
        HeadOPTxt="目前操作功能：部门信息维护" HeadHelpTxt="部门列表默认显示新增部门">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加部门" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditDept.aspx?cmd=add&id=0" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回公司" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="../CompanyManage/Company.aspx" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;" >
                <tr>
                    <td style="width: 20%;" align="left" valign="top">
                        <div>
                            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                            </asp:TreeView>
                        </div>
                    </td>
                    <td valign="top">
                        <div style="width: 100%;">
                            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                                <cc2:TabPanel runat="server" HeaderText="部门列表" ID="TabPanel1" Width="100%">
                                    <ContentTemplate>
                                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="DepartmentID" HeaderText="部门编号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CompanyName" HeaderText="所属公司">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="部门名称">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                   
                                                   <asp:TemplateField>
                                                    <HeaderTemplate>类型</HeaderTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <ItemTemplate>
                                                    
                                                    <%# EnumHelper.GetDescription((Enum)Eval("DepartmentType")) %>
                                                    
                                                    </ItemTemplate>
                                                   
                                                   </asp:TemplateField>
                                                   
                                                    <asp:BoundField DataField="ParentName" HeaderText="上级部门">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="ChildrenCount" HeaderText="子部门数">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                                        HeaderText="查看" CommandName="view">
                                                        <HeaderStyle Width="60px" />
                                                    </asp:ButtonField>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="60px" />
                                                        <HeaderTemplate>删除</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ImageUrl="~/images/ICON/delete.gif" CommandName="del"
                                                                CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('删除此部门将会同时删除其子部门\r\n\r\n确认要删除这些部门信息吗？')"
                                                                CausesValidation="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    没有部门信息
                                                </EmptyDataTemplate>
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                                <RowStyle HorizontalAlign="Center" Height="20px" />
                                            </asp:GridView>
                                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                                        </div>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" HeaderText="部门查询" ID="TabPanel2" Width="100%">
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
                                                    部门编号：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:TextBox ID="TextBox1" runat="server" title="~:int"></asp:TextBox>
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    所属公司：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    负责人：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    部门名称：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    层次：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:TextBox ID="TextBox5" runat="server" title="~:int"></asp:TextBox>
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    上级部门：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <center>
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                                </center>
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
