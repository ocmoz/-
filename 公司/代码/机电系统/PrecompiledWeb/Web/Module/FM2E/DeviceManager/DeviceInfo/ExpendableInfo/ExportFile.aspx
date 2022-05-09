<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ExportFile, App_Web_4xxpdmqi" title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品信息导出"
        HeadOPTxt="目前操作功能：易耗品信息导出" HeadHelpTxt="易耗品信息导出">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="导出的文件名字（开始行数~结束行数）">
                    <ItemTemplate>
                        <img src="../../../../../images/ICON/xls.gif"/>
                        <a href="../../../../../tempfile/<%# Eval("Description") %>"><%# Eval("RoleName")%></a>
                    </ItemTemplate>
                    <ItemStyle Width="100%" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <center>
                    <span style="color: Red">没有导出文件</span></center>
            </EmptyDataTemplate>
            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
            <RowStyle HorizontalAlign="Center" Height="20px" />
        </asp:GridView>
    </div>
</asp:Content>
