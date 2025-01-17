﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditInterimPayment.aspx.cs" Inherits="Module_FM2E_Contract_ContractInformation_EditInterimPayment"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="合同期中支付"
        HeadOPTxt="目前操作功能：合同期中支付">       
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="添加期中支付金额" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="2">
                                添加期中支付金额
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                期中支付金额：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="2" style="height: 30px">
                                <asp:TextBox ID="tb_PaymentAmount" runat="server" title="期中支付金额输入格式不正确~float!"></asp:TextBox>
                                     <span style="color: red">*</span>
                            </td>
                        </tr>
                           <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                支付时间：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="2" style="height: 30px">
                                <asp:TextBox ID="tb_PaymentTime" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="支付时间~date"></asp:TextBox><span style="color: red">*</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

