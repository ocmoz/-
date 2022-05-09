<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="OnlineUser.aspx.cs" Inherits="Module_FM2E_SystemManager_OnlineUserManager_OnlineUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="在线用户列表"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：查看在线用户">
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="在线用户列表" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有在线用户
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="U_Name" HeaderText="用户名" />
                                <asp:BoundField DataField="U_PersonName" HeaderText="姓名" />
                                <asp:BoundField DataField="U_StartTime" HeaderText="进入时间" />
                                <asp:BoundField DataField="U_LastTime" HeaderText="最后访问时间" />
                                <asp:TemplateField HeaderText="在线时间">
                                    <ItemTemplate>
                                        <span title="最后访问:<%#Eval("U_LastUrl")%>">
                                            <%#string.Format("{0:N2}", (double)Eval("U_OnlineSeconds") / 60)%>(分)</span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IP地址">
                                    <ItemTemplate>
                                        <%#Eval("U_LastIP") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="下线" CommandName="OutOnline"
                                            OnClientClick="return confirm('确认要强制用户下线吗？');" CommandArgument='<%# Eval("U_Guid")%>'>
                                        </asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
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
        </cc2:TabContainer>
    </div>
</asp:Content>
