<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_InsureManager_InsureInfoManager_InsuranceManager, App_Web_e_3pgr5i" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="保单信息管理"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：保单信息管理">
        <cc1:HeadMenuButtonItem ButtonName="保单信息管理" ButtonIcon="list.gif" ButtonUrlType="Href"
            ButtonUrl="Insurancelist.aspx" />
    </cc1:HeadMenuWebControls>

    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="报险管理->保单信息管理" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    保单号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_insuranceNo" runat="server" Text="Label"></asp:Label>
                                     <asp:TextBox ID="tb_insuranceNo" runat="server" title="请输入保单号~30:!"></asp:TextBox>
                                    <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    保单对象：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_insuranceTarget" runat="server" Text="Label"></asp:Label>
                                    <asp:TextBox ID="tb_insuranceTarget" runat="server" title="请输入保单对象~50:!"></asp:TextBox>
                                     <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    开始时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_startDate" runat="server" Text="Label"></asp:Label>
                                    <asp:TextBox ID="tb_startDate"  CssClass="input_calender" onfocus="javascript:HS_setDate(this);" runat="server" title="请输入保单对象~50:!"></asp:TextBox>
                                     <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    结束时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lb_endDate" runat="server" Text="Label"></asp:Label>
                                    <asp:TextBox ID="tb_endDate"  CssClass="input_calender" onfocus="javascript:HS_setDate(this);" runat="server" title="请输入保单对象~50:!"></asp:TextBox>
                                     <span style="color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    保险分类：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">

                                    <asp:Label ID="lb_insuranceType" runat="server" Text="Label"></asp:Label>
                                    <asp:DropDownList ID="dd_insuranceType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton" runat="server">
                            <tr runat="server">
                                <td align="right" style="height: 38px" runat="server">
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
    
    <script language="javascript" type="text/javascript">

        function selectAll(obj, id) {
            var rid=id.replace(".","+");
            var inputs = document.getElementsByName("Page_" + rid);

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.value == "全选")
                        inputs[i].checked = true;
                    else inputs[i].checked = false;
                }
            }
            if (obj.value == "全选")
                obj.value = "反选";
            else obj.value = "全选";
        }
    </script>
</asp:Content>
