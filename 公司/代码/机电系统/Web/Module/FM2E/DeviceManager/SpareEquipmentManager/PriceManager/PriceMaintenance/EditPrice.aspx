<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditPrice.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_EditPrice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="价格管理"
        HeadOPTxt="目前操作功能：查看申请审批的信息" HeadHelpTxt="显示包括已审批的或未审批的请求信息">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="返回当前指导价格列表" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="PriceDetail.aspx?tabindex=1" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="history.go(-1);" />
    </cc1:HeadMenuWebControls>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table width="100%">
                <tr align="center">
                    <td class="Table_searchtitle">
                        所要修改的项
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Label ForeColor="Red" ID="errormessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        产品名称
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="ProductName" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        产品规格型号
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Model" runat="server" Text='<%#Eval("Model")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        标价单位
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Unit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        原启用时间
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="StartTime" runat="server" Text='<%#DateTime.Compare(Convert.ToDateTime(Eval("StartTime")),DateTime.MinValue)==0?"": Eval("StartTime","{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        旧指导价格(元)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LowerPrice" runat="server" Text='<%#Eval("LowerPrice","{0:#,0.##}")%>'></asp:Label>
                                        －
                                        <asp:Label ID="UpperPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UpperPrice","{0:#,0.##}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        新指导价格(元)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="NewLowerPrice" Text='<%#(Eval("NewLowerPrice","{0:#,0.##}")=="0")?"":Eval("NewLowerPrice","{0:#,0.##}")%>'
                                            runat="server" Width="80px"></asp:TextBox>
                                        －
                                        <asp:TextBox ID="NewUpperPrice" Text='<%#(DataBinder.Eval(Container.DataItem,"NewUpperPrice","{0:#,0.##}")=="0")?"":DataBinder.Eval(Container.DataItem,"NewUpperPrice","{0:#,0.##}")%>'
                                            runat="server" Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        变更理由
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="Reason" runat="server" Text='<%#Eval("Reason")%>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        是否删除
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="deleteornot" Checked='<%#Eval("DeleteOrNot")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        
                                        <asp:Button ID="Button1" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                            Text="关闭" CssClass="button_bak" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button runat="server" CssClass="button_bak" Text="对比历史" OnClientClick='<%#Eval("ClientClick")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                暂无要修改的项
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td class="Table_searchtitle">
                        所要添加的项
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Label ForeColor="Red" ID="errormessage2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView Width="100%" ID="GridView2" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        产品名称
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="ProductName" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        产品规格型号
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Model" runat="server" Text='<%#Eval("Model")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        标价单位
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Unit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        指导价格(元)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LowerPrice" runat="server" Text='<%#Convert.ToDecimal(Eval("LowerPrice"))%>'></asp:Label>
                                        －
                                        <asp:Label ID="UpperPrice" runat="server" Text='<%#Convert.ToDecimal(Eval("UpperPrice"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                            Text="关闭" CssClass="button_bak"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button runat="server" CssClass="button_bak" Text="对比历史" OnClientClick='<%#Eval("ClientClick")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                暂无要添加的项
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <input id="Button3" type="button" runat="server" class="button_bak2" value="增加修改项"
                            onclick="javascript:showPopWin('添加修改项','SelectPrice.aspx', 900, 380, addtolist,true,true);" />
<%--                        <input id="Button10" type="button" class="button_bak2" runat="server"
                            value="提交到审批" onclick="javascript:return confirm('确认提交');" />--%>
                            <asp:Button ID="button9" OnClientClick="javascript:return confirm('确认提交?');" CssClass="button_bak2" OnClick="submittoapproval" Text="提交到审批" runat="server" />
                        <input id="addstring" type="hidden" runat="server" />
                        <input id="AddItem" type="button" style="display: none" onserverclick="AddItem_Click"
                            runat="server" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    请在下面填写添加的指导价格信息
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    产品名称
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:TextBox title="请输入产品名称~20:!" ID="TextBox_ProductName" runat="server">
                                    </asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    产品规格型号
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:TextBox title="请输入产品规格型号~20:!" ID="TextBox_Model" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    指导价格(元)
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:TextBox title="请输入价格下限~float!" ID="TextBox_LowerPrice" runat="server">
                                    </asp:TextBox>
                                    －
                                    <asp:TextBox title="请输入价格上限~float!" ID="TextBox_UpperPrice" runat="server">
                                    </asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    标价单位
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList title="请选择标价单位~!" ID="DropDownList_Unit" runat="server">
                                    </asp:DropDownList>
                                </td>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <input id="newitem" runat="server" onmouseover="javascript:causeValidate=true;" onmouseout="javascript:causeValidate=false;"
                            value="添加" type="button" onserverclick="AddPriceDetail" class="button_bak" />
                    </td>
                </tr>
            </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">
        causeValidate = false;
        function addtolist(addstring) {
            document.getElementById("<%=addstring.ClientID %>").value = addstring;
            document.getElementById("<%=AddItem.ClientID %>").click();

        }
    </script>

</asp:Content>
