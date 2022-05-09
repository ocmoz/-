<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_SystemManage_ViewSystem, App_Web_me0bbips" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadTitleTxt="系统划分" HeadHelpTxt="点击模块查看进入配置" 
        HeadOPTxt="">
    <cc1:HeadMenuButtonItem ButtonIcon="new.gif"
        ButtonName="添加子系统" ButtonPopedom="New" 
        ButtonUrlType="Href" ButtonUrl="EditSubSystem.aspx?cmd=add"/>
    <cc1:HeadMenuButtonItem ButtonIcon="edit.gif"
        ButtonName="编辑" ButtonPopedom="Edit" />    
    <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" 
        ButtonName="删除" ButtonPopedom="Delete"/>
    <cc1:HeadMenuButtonItem ButtonIcon="back.gif"
        ButtonName="返回" ButtonUrlType="javaScript"
        ButtonPopedom="List"  ButtonUrl="window.history.go(-1);"/>
    </cc1:HeadMenuWebControls>
    <div style="width:900px;height:300px;">    
        <cc2:TabContainer ID="TabContainer1" runat="server">
             <cc2:TabPanel runat="server" HeaderText="系统信息" ID="TabPanel1">
             <ContentTemplate>
             <div align="left"  style="width:800px;text-align:left; vertical-align:top; padding:0px 0px 0px 0px;">
                 <table style="width: 100%; border-collapse:collapse; vertical-align:middle; text-align:left; text-indent:13px; border:solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td style="width: 76px">
                            系统编号：</td>
                        <td style="width: 693px">
                            <asp:Label ID="Label1" runat="server" Text="Label" Width="250px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 76px">
                            系统名称：</td>
                        <td style="width: 693px">
                            <asp:Label ID="Label2" runat="server" Text="Label" Width="250px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 76px; text-align: left;">
                            备注：<br />
                            <br />
                        </td>
                        <td colspan="1" style="width: 693px">
                            <asp:Label ID="Label3" runat="server" Text="Label" Width="250px"></asp:Label></td>
                    </tr>
                </table>               
             </div>  
             </ContentTemplate>
             </cc2:TabPanel>
             <cc2:TabPanel runat="server" HeaderText="子系统列表" ID="TabPanel2">
             <ContentTemplate>
                 <asp:GridView ID="GridView1" runat="server" width="100%"
                    onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound"
                    AutoGenerateColumns="False" >                    
                    <EmptyDataTemplate>
                        <center>没有子系统</center>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="SubSystemID" HeaderText="系统编码" />
                        <asp:BoundField DataField="SubSystemName" HeaderText="系统名称" />
                        <asp:BoundField DataField="Remark" HeaderText="备注" />
                        <asp:ButtonField ButtonType="Image" HeaderText="查看"
                            ImageUrl="~/images/ICON/select.gif" CommandName="view">
                            <HeaderStyle Width="60px" />
                        </asp:ButtonField>
                        <asp:TemplateField>
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/images/ICON/delete.gif" CausesValidation="false" 
                                CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                OnClientClick="return confirm('确认要删除此子系统吗？')"/>
                            </ItemTemplate>
                         </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                    <RowStyle HorizontalAlign="Center" Height="20px" />
                 </asp:GridView>              
             </ContentTemplate>
             </cc2:TabPanel>   
        </cc2:TabContainer>
    </div>
</asp:Content>

