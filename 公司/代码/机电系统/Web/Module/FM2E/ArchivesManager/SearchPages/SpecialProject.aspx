<%@ Page Title="专项工程列表" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialProject.aspx.cs" Inherits="Module_FM2E_ArchivesManager_SearchPages_SpecialProject" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.SpecialProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
<script type="text/javascript">
    function clearbox() {
       
        document.getElementById('<%= TextBox_ProjectName.ClientID %>').value = "";
        document.getElementById('<%= TextBox_BudgetLower.ClientID %>').value = "";
        document.getElementById('<%= TextBox_BudgetUpper.ClientID %>').value = "";
        document.getElementById('<%= TextBox_TimeLower.ClientID %>').value = "";
        document.getElementById('<%= TextBox_TimeUpper.ClientID %>').value = "";
        document.getElementById('<%= TextBox_DesignCompany.ClientID %>').value = "";
        document.getElementById('<%= TextBox_BidCompany.ClientID %>').value = "";
    }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程档案查阅"
        HeadOPTxt="目前操作功能：查询专项工程档案" HeadHelpTxt="">
       <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回申请表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="" />
    </cc1:HeadMenuWebControls>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <cc2:TabPanel runat="server" HeaderText="专项工程列表" ID="TabPanel0">
            <ContentTemplate>
               
                                <asp:GridView ID="gridview_ProjectList" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-BackColor="#efefef" DataKeyNames="ProjectID" HeaderStyle-Height="25px"  OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
                                    RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                    <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkBox1" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <input id="CheckAll" runat="server" type="checkbox" onclick="selectAll(this);" />本页全选
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="专项工程名称">
                                            <ItemTemplate>
                                                
                                                    <asp:Label Text='<%# Eval("ProjectName") %>' runat="server" ID="Label_ProjectName"></asp:Label>
                                               
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Budget" HeaderText="预算" DataFormatString="{0:#,#.######万元}">
                                            <HeaderStyle />
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StatusString" HeaderText="状态">
                                            <HeaderStyle />
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                            <HeaderStyle />
                                            <ItemStyle Width="25%" />
                                        </asp:BoundField>
                                       
                                       <asp:TemplateField HeaderText="查看">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/select.gif"
                                            CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false"
                                            Visible='<%#IsBorrowed(Convert.ToInt64(Eval("ProjectID")))==true?true:false%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        当前没有专项工程或者没有查找到符合条件的专项工程
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left">
                                </cc1:AspNetPager>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="Table2"
                        runat="server">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Label ID="lbErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:Button ID="BtnBorrow" runat="server" CssClass="button_bak" Text="申请借阅" OnClick="BtnBorrow_Click" />
                                <asp:Button ID="BtnDestroy" runat="server" CssClass="button_bak" Text="申请销毁" OnClick="BtnDestroy_Click" />
                                <input type="hidden" runat="server" id="addstring" />
                            </td>
                        </tr>
                    </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="专项工程查询" ID="TabPanel1">
            <ContentTemplate>
            <table id="Table1" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                专项工程查询
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                工程名称：
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_ProjectName" runat="server"></asp:TextBox>
                            </td>
                             <td class="Table_searchtitle">
                                预算：
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_BudgetLower" runat="server"></asp:TextBox>-<asp:TextBox ID="TextBox_BudgetUpper" runat="server"></asp:TextBox>万元
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                时间：
                            </td>
                            <td  colspan="3">
                                <asp:TextBox ID="TextBox_TimeLower" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>-
                                <asp:TextBox ID="TextBox_TimeUpper" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                            </td>
                        </tr>
                       <tr>
                        <td class="Table_searchtitle">
                                设计单位：
                            </td>
                            <td >
                                <asp:TextBox ID="TextBox_DesignCompany" runat="server"></asp:TextBox>
                            </td>
                            
                             <td class="Table_searchtitle">
                                施工单位：
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_BidCompany" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                <asp:Button ID="Button_Query" runat="server" Text="查询" CssClass="button_bak"  OnClick="Button_Query_Click" />
                                <input type="button" id="button_clear" value="清空" class="button_bak" onclick="javascript:clearbox();" />
                            </td>
                        </tr>
                    </table>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
    <script type="text/javascript" language="javascript">
        function selectAll(obj) {
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;

            for (i = 0; i < theTable.rows.length; i++) {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
            }
        }
    </script>
</asp:Content>
