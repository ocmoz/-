<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_Plan_MonthFundsUsePlanList, App_Web_rpkbay2n" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script src="<%=Page.ResolveUrl("~/") %>js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="月度资金使用计划列表"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：月度资金使用计划列表">        
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
        <table width="100%">
            <tr>
                <td>
                    时间：<input type="text" id="PlanTime" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月'})"
                        class="Wdate" runat="server" />
                </td>
                <td>
                   <asp:Button ID="Button1" runat="server" Text="添加" onclick="Button1_Click"></asp:Button>
                    <asp:Button ID="Button2" runat="server" Text="查询" onclick="Button2_Click"></asp:Button>
                </td>
            </tr>            
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                        Width="100%" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>                              
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    计划时间
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#((string)Eval("year"))%>年<%#((string)Eval("Month"))%>月
                                </ItemTemplate>
                                <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                                                      
                            <asp:TemplateField HeaderText="查看">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/ICON/select.gif" runat="server"
                                        CommandName="view" CommandArgument='<%# Eval("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="计划用款明细">
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/ICON/select.gif" runat="server"
                                        CommandName="viewUsePlan" CommandArgument='<%# Eval("Id") %>' />                                    
                                  </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="实际用款明细">
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/ICON/select.gif" runat="server"
                                        CommandName="viewUseActual" CommandArgument='<%# Eval("Id") %>' />
                                  </ItemTemplate>
                            </asp:TemplateField> 
                               <asp:TemplateField HeaderText="计划收入明细">
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/ICON/select.gif" runat="server"
                                        CommandName="viewIncomePlan" CommandArgument='<%# Eval("Id") %>' />                                    
                                  </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="实际收入明细">
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/ICON/select.gif" runat="server"
                                        CommandName="viewIncomeActual" CommandArgument='<%# Eval("Id") %>' />
                                  </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="审批">
                                <ItemTemplate> <asp:LinkButton ID="LinkButton4" runat="server" CommandName="sp" CommandArgument='<%# Eval("Id") %>'>审批</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            没有信息
                        </EmptyDataTemplate>
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                        NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                        ShowCustomInfoSection="Left" CloneFrom="">
                    </cc1:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>