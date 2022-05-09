<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BudgetManagement_BudgetAccounts_EditSubjectRelation, App_Web_xnjg_m0h" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
        <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="预算科目信息维护"
        HeadOPTxt="目前操作功能：预算科目信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="预算科目列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="SubjectRelation.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript" ButtonPopedom="List" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 800px; height: 300px;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="811px">
            <cc2:TabPanel HeaderText="编辑预算科目信息" runat="server" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                预算科目详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                科目名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="Name" title="请输入预算科目名称,现在30个字符内~30:!" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="table_body table_body_NoWidth">
                                上级科目名称：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="ParentName" ReadOnly="true" onfocus="javascript:causeValidate = false;"  runat="server"></asp:TextBox>
                                <asp:Panel ID="Panel1" CssClass="popupControl" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                            </asp:TreeView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ParentName"
                                    PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                    ExtenderControlID="">
                                </cc2:PopupControlExtender>
                            </td>
                        </tr>
                        
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" onmousemove="javascript:causeValidate = true;" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

