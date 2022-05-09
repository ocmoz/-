<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="WorkflowList.aspx.cs" Inherits="Module_FM2E_WorkflowManager_WorkflowList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="工作流管理"
        HeadHelpTxt="选择工作流进行管理" HeadOPTxt="目前操作功能：工作流列表">
        <cc1:HeadMenuButtonItem ButtonName="添加工作流" ButtonIcon="new.gif" ButtonPopedom="New"
            ButtonUrl="" ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="工作流列表" ID="TabPanel1">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    没有工作流信息
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="工作流名称" HeaderStyle-Width ="32%" ItemStyle-Width ="32%"/>
                                    <asp:BoundField DataField="Description" HeaderText="工作流描述" HeaderStyle-Width ="32%" ItemStyle-Width ="40%"/>
                                    <asp:TemplateField HeaderStyle-Width ="4%" ItemStyle-Width ="4%">
                                        <HeaderTemplate>
                                            包含规则</HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Convert.ToBoolean(Eval("HasRule"))==true?"是":"否"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/edit.gif" HeaderText="编辑工作流"
                                        CommandName="editDef" HeaderStyle-Width ="12%" ItemStyle-Width ="12%">
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/edit.gif" HeaderText="工作流角色"
                                        CommandName="editRole" HeaderStyle-Width ="12%" ItemStyle-Width ="12%">
                                    </asp:ButtonField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
<%--                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
