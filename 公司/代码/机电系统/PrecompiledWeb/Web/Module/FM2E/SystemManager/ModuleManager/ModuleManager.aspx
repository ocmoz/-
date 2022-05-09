<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SystemManager_ModuleManager_ModuleManager, App_Web_ip7hmmuc" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="模块配置"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：模块分类配置">
        <cc1:HeadMenuButtonItem ButtonName="模块分类列表" ButtonIcon="list.gif" ButtonUrlType="Href"
            ButtonUrl="Modulelist.aspx" />
    </cc1:HeadMenuWebControls>

    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="模块列表->分类管理" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" style="text-align: left" colspan="2">
                                    <asp:Label ID="TitleTxt" runat="server"></asp:Label>
                                    <div style="float:right;">
                                    <asp:UpdatePanel
                                            ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel3" runat="server" Style="background-color: White; display: none;
                                                    width: 920px; overflow: auto;" CssClass="modalPopup">
                                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                                                        <tr class="table_body" align="center">
                                                            <td width="25%" align="left">
                                                                页面名称
                                                            </td>
                                                            <td>
                                                                查看&nbsp;
                                                            </td>
                                                            <td>
                                                                新增&nbsp;
                                                            </td>
                                                            <td>
                                                                修改&nbsp;
                                                            </td>
                                                            <td>
                                                                删除&nbsp;
                                                            </td>
                                                            <td>
                                                                打印&nbsp;
                                                            </td>
                                                            <td>
                                                                审批&nbsp;
                                                            </td>
                                                            <td>
                                                                权限A
                                                            </td>
                                                            <td>
                                                                权限B
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater ID="PageRepeater" runat="server" OnItemDataBound="PageRepeater_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr class="table_none" align="center">
                                                                    <td align="left">
                                                                        <input id="SelectButton"  type="button"
                                                                        value="全选" onclick='selectAll(this,"<%#Eval("PageName")%>");' />
                                                                        <%# Eval("PageName")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal ID="Lab2_Txt" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal runat="server" ID="Lab4_Txt"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal runat="server" ID="Lab8_Txt"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal runat="server" ID="Lab16_Txt"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal runat="server" ID="Lab32_Txt"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal runat="server" ID="Lab64_Txt"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal runat="server" ID="Lab128_Txt"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Literal runat="server" ID="Lab256_Txt"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr id="warnMsg" runat="server" ><td colspan="9" align="center">此模块所在目录不存在任何页面，请检查模块URL是否已设置正确</td></tr>
                                                   <tr><td colspan="9" align="right">
                                                   <asp:Button ID="Button5" runat="server" Text="生成模块配置文件" OnClick="Button5_Click" />
                                                   <input id="OKButton" runat="server" type="button" value="取消" />
                                                   </td></tr>
                                                    </table>
                                                    
                                                    </asp:Panel>
                                                <asp:Button ID="Button4" runat="server" Text="页面配置" OnClick="Button4_Click" />
                                                <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
                                                <cc2:ModalPopupExtender ID="Button3_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                                    DropShadow="True" DynamicServicePath="" Enabled="True" OkControlID="OKButton"
                                                    PopupControlID="Panel3" TargetControlID="Button3">
                                                </cc2:ModalPopupExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    模块编号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    模块名称：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    <asp:TextBox ID="TextBox2" runat="server" title="请输入模块名称~20:!"></asp:TextBox>
                                    <asp:Label ID="lbStar" runat="server" ForeColor="Red">*</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    父级模块：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    <input id="ParentID" type="hidden" runat="server" ></input>


                                        

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                </td></tr><tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    模块URL：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">

                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox4"
                                        runat="server" Width="400px" title="请输入模块URL~255:"></asp:TextBox><asp:Button ID="btClearUrl"
                                            runat="server" Text="清空URL" OnClick="btClearUrl_Click" /></td></tr><tr>

                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    是否为系统模块：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">

                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label><asp:DropDownList
                                        ID="DropDownList1" runat="server" Enabled="False">
                                        <asp:ListItem Value="1">是</asp:ListItem><asp:ListItem Selected="True" Value="0">否</asp:ListItem></asp:DropDownList>(注：系统模块无法修改删除)

                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    是否关闭：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">

                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label><asp:DropDownList
                                        ID="DropDownList2" runat="server">
                                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem><asp:ListItem Value="1">是</asp:ListItem></asp:DropDownList></td></tr></table><table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"

                            runat="server">
                            <tr runat="server">
                                <td align="right" style="height: 38px" runat="server">
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" CssClass="popupLayer">
                        <div style="border: 1px outset white;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TreeView ID="pageList" runat="server" OnTreeNodePopulate="PopulateNode" OnSelectedNodeChanged="Select_Change">
                                    </asp:TreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                    <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="TextBox4"
                        PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                        ExtenderControlID="">
                    </cc2:PopupControlExtender>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="模块列表" ID="TabPanel2">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <EmptyDataTemplate>

                                没有模块信息</EmptyDataTemplate><Columns>
                                <asp:BoundField DataField="ModuleID" HeaderText="模块编码" />
                                <asp:BoundField DataField="Name" HeaderText="模块名称" />
                                <asp:BoundField DataField="ChildCount" HeaderText="子模块数" />

                                <asp:TemplateField>
                                    <HeaderTemplate>

                                        模块Url</HeaderTemplate><ItemTemplate>
                                        <%#(Convert.ToString(Eval("Directory")) == null || Convert.ToString(Eval("Directory")).Trim() == string.Empty) ? "无" : Convert.ToString(Eval("Directory"))%></ItemTemplate></asp:TemplateField><asp:TemplateField>

                                    <HeaderTemplate>

                                        系统模块</HeaderTemplate><ItemTemplate>
                                        <%#Convert.ToBoolean(Eval("IsSystem"))?"是":"否"%></ItemTemplate></asp:TemplateField><asp:TemplateField>

                                    <HeaderTemplate>

                                        关闭</HeaderTemplate><ItemTemplate>
                                        <%#Convert.ToBoolean(Eval("IsClose"))?"是":"否"%></ItemTemplate></asp:TemplateField><asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"

                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此模块信息吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="模块排序">
                <ContentTemplate>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                        <tr class="table_none">
                            <td align="center">
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <cc2:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" OnItemReorder="ReorderList1_ItemReorder">
                                                <ItemTemplate>
                                                    <div class="itemArea">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("Name"))) %>' />
                                                    </div>
                                                </ItemTemplate>
                                                <ReorderTemplate>
                                                    <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
                                                </ReorderTemplate>
                                                <DragHandleTemplate>
                                                    <div class="dragHandle">
                                                    </div>
                                                </DragHandleTemplate>
                                                <EmptyListTemplate>
                                                    没有模块数据</EmptyListTemplate></cc2:ReorderList></div></ContentTemplate></asp:UpdatePanel></td></tr><tr>
                            <td class='menubar_readme_text' style="text-align: left">
                                请用鼠标拖动进行排序
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button2" runat="server" CssClass="button_bak" OnClick="Button2_Click"
                                    Text="确定" />
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
