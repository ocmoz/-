<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewBorrowApply.aspx.cs" Inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApply_ViewBorrowApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�豸�������"
        HeadHelpTxt="����" HeadOPTxt="Ŀǰ�������ܣ��豸������鿴">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="�༭" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="ɾ��" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="������뵥����" ID="TabPanel1">
                <ContentTemplate>
                    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                        text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                        <tr>
                            <td style="width: 20%">
                                ���뵥��ţ�
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbSheetName" runat="server" Text=""></asp:Label>
                                &nbsp;
                            </td>
                            <td style="width: 20%">
                                �������
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbLendCompany" runat="server" Text=""></asp:Label>  
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                ���뷽��
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbBorrowCompany" runat="server" Text=""></asp:Label>
                                &nbsp;
                            </td>
                            <td style="width: 20%">
                                �����ˣ�
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbApplicant" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                ���뵥״̬��
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 20%">
                                ����ʱ�䣺
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lbSubmitTime" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EquipmentName" HeaderText="��Ʒ����">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="����ͺ�">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Count" HeaderText="����">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit" HeaderText="��λ">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Reason" HeaderText="����ԭ��">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("AddressName") %><%# Eval("DetailLocation") %>
                                </ItemTemplate>
                                <HeaderTemplate>ʹ�õص�</HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReturnDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="�黹����"
                                HtmlEncode="False">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            û�н��������ϸ��Ϣ
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="������ʷ" ID="TabPanel2">
                <ContentTemplate>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApprovalerName" HeaderText="������">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="�������">
                                <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("Result"))?"ͨ��":"��ͨ��" %>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApprovalDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FeeBack" HeaderText="������ע"></asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            ��û������¼
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="���õ��豸" ID="TabPanel3">
                <ContentTemplate>
                     <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="EquipmentNO" HeaderText="�豸������">
                            </asp:BoundField>
                            <asp:BoundField DataField="EquipmentName" HeaderText="�豸����">
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="����ͺ�"></asp:BoundField>
                             <asp:BoundField DataField="BorrowerName" HeaderText="������"></asp:BoundField>
                            <asp:BoundField DataField="BorrowTime" HeaderText="����ʱ��" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}"></asp:BoundField>
                            <asp:BoundField DataField="ReturnDate" HeaderText="Ӧ�黹����" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false"></asp:BoundField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    �Ƿ��ѹ黹</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("IsReturned")) ? "��" : "��"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            ��û���õ����豸
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="�黹���ռ�¼" ID="TabPanel4">
                <ContentTemplate>
                     <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="EquipmentNO" HeaderText="�豸������">
                            </asp:BoundField>
                            <asp:BoundField DataField="EquipmentName" HeaderText="�豸����">
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="����ͺ�"></asp:BoundField>
                             <asp:BoundField DataField="ReturnDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"></asp:BoundField>
                              <asp:TemplateField>
                                <HeaderTemplate>
                                    ���ս��</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("Result")) ? "����ͨ��" : "���ղ�ͨ��"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FeeBack" HeaderText="���ձ�ע"></asp:BoundField>
                            <asp:BoundField DataField="CheckerName" HeaderText="������"></asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            ��û�黹���ռ�¼
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
