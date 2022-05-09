<%@ page masterpagefile="~/MasterPage/MasterPage.master" language="C#" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_PriceApply, App_Web_nuqbk7th" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="价格管理"
        HeadOPTxt="目前操作功能：查看申请审批的信息" HeadHelpTxt="显示包括已审批的或未审批的请求信息">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="返回申请审批列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="PriceDetail.aspx?tabindex=3" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td class="Table_searchtitle" colspan="8">
                        申请审批的基本信息
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        申请人
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="Applicant" runat="server"></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        申请日期
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="ApplyDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        审批人
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="Approvaler" runat="server"></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        审批日期
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="ApprovalDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        当前状态
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="LbStatus" runat="server"></asp:Label>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        
                    </td>
                    <td class="table_none table_none_NoWidth">
                        
                    </td>
                </tr>
                <td class="Table_searchtitle" colspan="8">
                    申请审批的具体价格信息
                </td>
                <tr>
                    <td align="center" colspan="8">
                        <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="DeleteOldOrNot" HeaderText="操作类型">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="产品名称">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="产品规格型号">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unit" HeaderText="标价单位">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="StartTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                    HeaderText="原启用时间">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:TemplateField>
                            <HeaderTemplate>
                                旧指导价格(元)
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="OldLowerPrice" runat="server" Text='<%#Eval("OldLowerPrice","{0:#,0.##}")%>'></asp:Label>
                                －
                                <asp:Label ID="OldUpperPrice" runat="server" Text='<%#Eval("OldUpperPrice","{0:#,0.##}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                新指导价格(元)
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="NewLowerPrice" runat="server" Text='<%#Eval("NewLowerPrice","{0:#,0.##}")%>'></asp:Label>
                                －
                                <asp:Label ID="NewUpperPrice" runat="server" Text='<%#Eval("NewUpperPrice","{0:#,0.##}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <%--<asp:BoundField DataField="OldUpperPrice" DataFormatString="{0:#,0.##}" HeaderText="旧指导价格上限">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="NewUpperPrice" DataFormatString="{0:#,0.##}" HeaderText="新指导价格上限">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="OldLowerPrice" DataFormatString="{0:#,0.##}" HeaderText="旧指导价格下限">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="NewLowerPrice" DataFormatString="{0:#,0.##}" HeaderText="新指导价格下限">
                                    <HeaderStyle />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Reason" HeaderText="变更理由">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="审批结果">
                                    <HeaderStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="FeeBack" HeaderText="审批反馈意见">
                                    <HeaderStyle />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                没有申请审批的具体价格信息
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
