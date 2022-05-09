<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNoCheck.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="MaintainItem.aspx.cs" Inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainItem_MaintainItem"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="维护项目管理"
        HeadOPTxt="目前操作功能：维护项目列表" HeadHelpTxt="请按照系统、子系统筛选维护项目">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加维护标准项" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditMaintainItem.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    
    <div style="width: 100%;">
        <%--  <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="维护计划" ID="TabPanel1">
                <ContentTemplate>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always">
            <ContentTemplate>
              <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td style="width: 20%; text-align: center" class="table_body table_body_NoWidth">
                            系统：
                            <asp:DropDownList ID="DDLSystem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLSystem_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 30%; text-align: center" class="table_body table_body_NoWidth">
                            子系统：
                            <asp:DropDownList ID="DDLSubsystem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLSubsystem_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: center" class="table_body table_body_NoWidth">
                            对象：
                            <asp:TextBox ID="TextBox_ObjectSearch" runat="server"  AutoPostBack="true"
                                ontextchanged="TextBox_ObjectSearch_TextChanged"></asp:TextBox>
                        </td>
                        <td style="width: 15%; text-align: center" class="table_body table_body_NoWidth">
                            类型：
                            <asp:DropDownList ID="DropDownList_Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Type_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%; text-align: center" class="table_body table_body_NoWidth">
                            周期：
                            <asp:DropDownList ID="DropDownList_Period" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Period_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                    <asp:GridView ID="GridView_PlanList" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView_PlanList_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SystemName" HeaderText="系统" ItemStyle-Width="8%">
                                <ItemStyle Width="8%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SubSystemName" HeaderText="子系统" ItemStyle-Width="10%">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Object" HeaderText="对象" ItemStyle-Width="12%">
                                <ItemStyle Width="12%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="类型">
                                <ItemTemplate>
                                    <%# EnumHelper.GetDescription((Enum)Eval("MaintainType"))%>
                                </ItemTemplate>
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="周期">
                                <ItemTemplate>
                                    <%# Eval("Period") %><%# EnumHelper.GetDescription((Enum)Eval("PeriodUnit")) %>
                                </ItemTemplate>
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="维护内容/验收标准">
                                <ItemTemplate>
                                    维护内容：<%# Eval("Content")%><hr />
                                    验收标准：<%# Eval("Standard")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                        CommandName="EditCMD" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                        CommandArgument='<%# Eval("ID") %>' CommandName="DeleteCMD" ImageUrl="~/images/ICON/delete.gif"
                                        OnClientClick="javascript:return confirm('确认删除该项？');" />
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <center>
                                <font color="red">没有找到符合条件的维护项目</font></center>
                        </EmptyDataTemplate>
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                        NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                        ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--</ContentTemplate>
            </cc2:TabPanel>
            
        </cc2:TabContainer>--%>
    </div>
</asp:Content>
