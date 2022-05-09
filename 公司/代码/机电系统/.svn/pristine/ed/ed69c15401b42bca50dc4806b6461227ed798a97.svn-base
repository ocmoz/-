<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditContractInformation.aspx.cs" Inherits="Module_FM2E_Contract_ContractInformation_EditContractInformation"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        var fileId = 1;
        function addFile() {
            var FilesDiv = document.getElementById('FilesDiv');

            var divId = "div" + fileId;
            var str = '<div id="' + divId + '">';
            str += '<input type="file" size="40" name="File" style="border: solid 1px #0077B2">'
            str += '&nbsp;<img src="../../../../images/ICON/delete.gif" onclick="delFile(\'' + divId + '\')"/>';
            str += "<div>";
            FilesDiv.insertAdjacentHTML("beforeEnd", str)
            fileId++;
        }

        function delFile(obj) {
            var FilesDiv = document.getElementById('FilesDiv');
            var elem = document.getElementById(obj);
            FilesDiv.removeChild(elem);
        }    
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="合同基本信息管理"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：合同基本信息管理">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="合同基本信息管理" ID="TabPanel1">
                <HeaderTemplate>
                    合同基本信息管理
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    合同编号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_ContractNo" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_ContractNo" runat="server" title="请输入合同编号~30:!"></asp:TextBox>
                                    <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    合同名称：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_ContractName" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_ContractName" runat="server" title="请输入合同名称~50:!"></asp:TextBox>
                                    <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    签约单位：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_ContractedUnits" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_ContractedUnits" runat="server" title="请输入签约单位~50:!"></asp:TextBox>
                                    <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    联系人：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_ContractPeople" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_ContractPeople" runat="server" title="请输入联系人~50:!"></asp:TextBox>
                                    <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    联系方式：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_ContractTheWay" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_ContractTheWay" runat="server" title="请输入联系方式~50:!"></asp:TextBox>
                                    <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    合同金额：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_ContractAmount" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_ContractAmount" runat="server" title="合同金额输入格式不正确~float"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    结算金额：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_SettlementAmount" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    质保期：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_Period" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_Period" runat="server" title="请输入期数~int"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    质保金：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_Retentions" runat="server"></asp:Label>
                                    <asp:TextBox ID="tb_Retentions" runat="server" title="质保金输入格式不正确~float"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    附件：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <div id="FileUpload_div" runat="server">
                                        <div id="FilesDiv">
                                            <div id="div0">
                                                <input type="file" runat="server" size="40" id="file0" 
                                                    style="border: solid 1px #0077B2" />
                                                <img src="../../../../images/ICON/delete.gif" onclick="delFile('div0')" />
                                            </div>
                                        </div>
                                        <input type="button" value="添加附件" onclick="addFile();return false" id="btnInput"
                                            runat="server" />
                                    </div>
                                    <div id="downFileDiv">
                                        <asp:GridView ID="gridviewFile" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="Editreason1id"
                                            Width="100%"
                                            OnRowCommand="GridView_OnRowCommand" 
                                            OnRowDataBound="GridView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <%# Eval("Editreason1id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="附件" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="ArticleName" runat="server" ForeColor="Blue" Font-Underline="true"
                                                            Text='<%# Eval("Name")%>' NavigateUrl='<%# "~/public/Contract/"+ Eval("Address")%>'> </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="70%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                            CommandArgument='<%# Eval("Editreason1id") %>' CommandName="DELETE_SP" ImageUrl="~/images/ICON/delete.gif"
                                                            Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle Height="20px" HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                当前无附件
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                            runat="server">
                            <tr id="Tr2" runat="server">
                                <td id="Td2" align="right" style="height: 38px" runat="server">
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="支付情况" ID="TabPanel2">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    预付金额：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tb_Prepaid" runat="server" title="预付金额输入格式不正确~float"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    期中支付金额：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="添加" OnClick="Button3_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                        <EmptyDataTemplate>
                                            没有期中支付金额
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="系统编号" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="PaymentAmount" HeaderText="支付金额"></asp:BoundField>
                                            <asp:BoundField DataField="PaymentTime" HeaderText="支付时间"></asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" Text="编辑" ImageUrl="~/images/ICON/edit.gif" HeaderText="编辑"
                                                CommandName="edit"></asp:ButtonField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                        CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此支付信息吗？')"
                                                        CausesValidation="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    竣工支付金额：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tb_CompletedPayment" runat="server" title="竣工支付金额输入格式不正确~float"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    交工支付金额：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tb_HandOverpay" runat="server" title="交工支付金额输入格式不正确~float"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="Table1"
                            runat="server">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" align="right" style="height: 38px" runat="server">
                                    <asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="确定" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>          
        </cc2:TabContainer>
    </div>
</asp:Content>
