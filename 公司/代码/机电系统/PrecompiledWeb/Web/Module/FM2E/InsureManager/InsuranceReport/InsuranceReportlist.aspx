<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_InsureManager_InsureInfoManager_InsuranceReportlist, App_Web_nlqf5cyp" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Insurance" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="报险信息管理"
        HeadHelpTxt="查看报险信息" HeadOPTxt="目前操作功能：报险信息列表">
        <cc1:HeadMenuButtonItem ButtonName="新增报险信息" ButtonIcon="new.gif" ButtonUrlType="Href" ButtonPopedom="New"
            ButtonUrl="InsuranceReportManager.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel ID="TabPanel1" HeaderText="报险信息" runat="server">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table  width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                             <tr>
                                 <td class="table_body table_body_NoWidth" style="width: 190px">
                                     保单号：<asp:TextBox ID="tb_search_insuranceNo" runat="server"></asp:TextBox>
                                 </td>
                                 <td class="table_body table_body_NoWidth" style="width: 207px">
                                     报案号：<asp:TextBox ID="tb_search_reportNo" runat="server"></asp:TextBox>
                                 </td>
		                        <td class="table_body table_body_NoWidth"style="width: 130px">
		                            出险类型：<asp:DropDownList ID="ddl_search_riskType" runat="server">
			                        </asp:DropDownList>
		                        </td>
                                <td class="table_body table_body_NoWidth"style="width: 130px">
		                            状态：<asp:DropDownList ID="ddl_search_state" runat="server">
			                        </asp:DropDownList>
		                        </td>
		                    </tr>
                            <tr>
		                        <td class="table_body table_body_NoWidth" style="width: 345px" >时间：
		                            <asp:TextBox ID="tb_search_startRiskDate" runat="server" Width="80px"   class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="请输入开始时间~date"></asp:TextBox>-
		                            <asp:TextBox ID="tb_search_endRiskDate"  runat="server"  Width="80px" class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="请输入结束时间~date"></asp:TextBox> 
		                        </td>
                                <td class="table_body table_body_NoWidth" style="width: 345px" >时间：
		                            <asp:TextBox ID="tb_search_startReportDate" runat="server" Width="80px"   class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="请输入开始时间~date"></asp:TextBox>-
		                            <asp:TextBox ID="tb_search_endReportDate"  runat="server"  Width="80px" class="input_calender" onfocus="javascript:HS_setDate(this);"
										                            title="请输入结束时间~date"></asp:TextBox>
		                        </td>
                                <td>
                                    <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="button_bak" OnClick="btSearch_Click"/>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" 
                            AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
                            onrowdatabound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有保单信息
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="系统编码" Visible="false" />
                                <asp:BoundField DataField="InsuranceNo" HeaderText="保单号" />
                                <asp:BoundField DataField="ReportNo" HeaderText="报案号" />
                                <asp:BoundField DataField="RiskDate" HeaderText="出险日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="ReportDate" HeaderText="报险日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        出险类型</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# EnumHelper.GetDescription((RiskType)Eval("RiskType"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        状态</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# EnumHelper.GetDescription((State)Eval("State"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Link" Text="修复"  HeaderText="修复"
                                    CommandName="repair">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Link" Text="复核"  HeaderText="复核"
                                    CommandName="review">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <%--<asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"  OnClientClick="return confirm('确认要删除此模块信息吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </div>
                     <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
