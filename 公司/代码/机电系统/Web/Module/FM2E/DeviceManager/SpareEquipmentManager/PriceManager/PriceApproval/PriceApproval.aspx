<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="PriceApproval.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceApproval_PriceApproval"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="价格管理"
        HeadOPTxt="目前操作功能：审批请求" HeadHelpTxt="审批提交的添加、修改删除价格请求">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="返回申请审批列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="PriceDetail.aspx?tabindex=3" />
    </cc1:HeadMenuWebControls>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
        style="border-collapse: collapse;">
        <tr align="center" style="height: 30px">
            <td class="Table_searchtitle" colspan="8">
                申请审批的具体价格信息
            </td>
        </tr>
        <tr align="center">
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
                                <asp:Label ID="NewUpperPrice" runat="server" Text='<%#Eval("NewUpperPrice","{0:#,0.##}")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Reason" HeaderText="变更理由">
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                审批结果
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="Result" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                审批反馈意见
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="FeeBack" Text='<%#Eval("FeeBack")%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField>
                            <HeaderTemplate>
                                查看历史
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button Text="查看历史" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <EmptyDataTemplate>
                        没有申请审批的具体价格信息
                    </EmptyDataTemplate>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                    <RowStyle HorizontalAlign="Center" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr align="center">
            <td class="Table_searchtitle" colspan="4">
                申请审批的基本信息
            </td>
        </tr>
        <tr align="center">
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
        <tr align="center">
            <td class="table_body table_body_NoWidth">
                审批
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="DDLWFEvent" runat="server">
                </asp:DropDownList>
            </td>
            <td class="table_body table_body_NoWidth">
            </td>
            <td class="table_none table_none_NoWidth">
            </td>
        </tr>
        <tr align="center">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                runat="server">
                <tr>
                    <td align="center" style="height: 38px">
                        <asp:Button ID="SubmitItem" runat="server" OnClick="SubmitItem_click" CssClass="button_bak"
                            Text="提交" />
                        <input id="Reset1" class="button_bak" type="reset" value="重填" />
                    </td>
                </tr>
            </table>
        </tr>
    </table>
    <%--       </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
