<%@ page title="专项工程列表" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectApply_SpecialProjectList, App_Web_i4s4q9u5" %>

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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--工程立项"
        HeadOPTxt="目前操作功能：工程列表" HeadHelpTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="新建专项工程" ButtonUrlType="Href"
            ButtonUrl="Apply.aspx" ButtonPopedom="New" />
    </cc1:HeadMenuWebControls>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <cc2:TabPanel runat="server" HeaderText="专项工程列表" ID="TabPanel0">
            <ContentTemplate>
                <div id="div_table">
                    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                当前运作专项工程列表
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridview_ProjectList" runat="server" AutoGenerateColumns="False"  OnRowCommand="GridView_OnRowCommand"
                                    HeaderStyle-BackColor="#efefef" DataKeyNames="ProjectID" HeaderStyle-Height="25px"
                                    RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="专项工程名称">
                                            <ItemTemplate>
                                                &nbsp;&nbsp;<a style="color: Blue" href='../ViewProject.aspx?projectid=<%# DataBinder.Eval(Container.DataItem,"ProjectID") %>&cmd=view'>
                                                    <asp:Label Text='<%# Eval("ProjectName") %>' runat="server" ID="Label_ProjectName"></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="21%" />
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
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="立项">
                                            <ItemTemplate>
                                                <a 
                                                    href='ViewProject.aspx?projectid=<%# DataBinder.Eval(Container.DataItem,"ProjectID") %>&cmd=submit'>
                                                    <img src="../../../../images/ICON/select.gif" style="border: 0" />
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="开工">
                                            <ItemTemplate>
                                                <a 
                                                    href='Start.aspx?projectid=<%# DataBinder.Eval(Container.DataItem,"ProjectID") %>&cmd=edit'>
                                                    <img src="../../../../images/ICON/a.gif" style="border: 0" />
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="修改">
                                            <ItemTemplate>
                                                <a 
                                                    href='<%# "Apply.aspx?projectid="+DataBinder.Eval(Container.DataItem,"ProjectID").ToString()+"&cmd=edit" %>'
                                                    runat="server">
                                                    <img src="../../../../images/ICON/edit.gif" style="border: 0" />
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ProjectID") %>'
                                                    
                                                    CommandName="DELETE_SP" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
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
                            </td>
                        </tr>
                    </table>
                </div>
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
</asp:Content>
