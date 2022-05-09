<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="System.aspx.cs" Inherits="Module_FM2E_BasicData_SystemManage_System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1"  runat="server" HeadTitleTxt="系统划分"
        HeadHelpTxt="点击查看后可查看或增删子系统" HeadOPTxt="目前操作功能：系统分类列表">
        <cc1:HeadMenuButtonItem ButtonName="添加系统" ButtonIcon="new.gif" ButtonUrlType="Href" ButtonPopedom="New"
            ButtonUrl="EditSystem.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel runat="server" HeaderText="系统列表" ID="TabPanel1">
            <ContentTemplate>
             <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                 <asp:GridView ID="GridView1" runat="server" width="100%"
                    AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
                            onrowdatabound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有系统划分信息
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="SystemID" HeaderText="系统编码" />
                                <asp:BoundField DataField="SystemName" HeaderText="系统名称" />
                                <asp:BoundField DataField="Remark" HeaderText="备注" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"  OnClientClick="return confirm('确认要删除此系统吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        
                 </asp:GridView>
             </div>
            </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

