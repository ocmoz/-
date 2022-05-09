<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SystemManager_ModuleManager_Modulelist, App_Web_ip7hmmuc" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="ģ������"
        HeadHelpTxt="���ģ��鿴��������" HeadOPTxt="Ŀǰ�������ܣ�ģ������б�">
        <cc1:HeadMenuButtonItem ButtonName="����ģ�����" ButtonIcon="new.gif" ButtonUrlType="Href" ButtonPopedom="New"
            ButtonUrl="ModuleManager.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel ID="TabPanel1" HeaderText="ģ�����" runat="server">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" 
                            AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
                            onrowdatabound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                û��ģ����Ϣ
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="ModuleID" HeaderText="ģ�����" />
                                <asp:BoundField DataField="Name" HeaderText="ģ������" />
                                <asp:BoundField DataField="ChildCount" HeaderText="��ģ����" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        ϵͳģ��</HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Convert.ToBoolean(Eval("IsSystem"))?"��":"��"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        �ر�</HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Convert.ToBoolean(Eval("IsClose"))?"��":"��"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="�鿴"
                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"  OnClientClick="return confirm('ȷ��Ҫɾ����ģ����Ϣ��')"
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
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="ģ������">
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
                                                    û��ģ������</EmptyListTemplate>
                                            </cc2:ReorderList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr><td class='menubar_readme_text' style="text-align:left">��������϶���������</td></tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" OnClick="Button1_Click"
                                    Text="ȷ��" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
