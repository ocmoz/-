<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ListFile.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ListFile"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="审批文件名称">
                    <ItemTemplate>
                        <asp:Label ID="Label_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="70%" />
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Image" Text="编辑" ImageUrl="~/images/ICON/xls.gif"
                    HeaderText="编辑" CommandName="view">
                    <ItemStyle Width="30%" />
                </asp:ButtonField>
            </Columns>
            <EmptyDataTemplate>
                <center>
                    <span style="color: Red">还没有审批文件</span></center>
            </EmptyDataTemplate>
            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
            <RowStyle HorizontalAlign="Center" Height="20px" />
        </asp:GridView>
    </div>
</asp:Content>
