<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_DeviceTypeManage_DeviceType, App_Web_etjlb33o" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�豸������Ϣά��"
        HeadOPTxt="Ŀǰ�������ܣ��豸������Ϣά��" HeadHelpTxt="�豸�����б�Ĭ����ʾ�����豸����">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="����豸����" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditDeviceType.aspx?cmd=add" />
       <%-- <cc1:HeadMenuButtonItem ButtonIcon="move.gif" ButtonName="�����豸������Ϣ" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="Import.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="xls.gif" ButtonName="�������" ButtonPopedom="Print"
            ButtonVisible="true" ButtonUrlType="Href" ButtonUrl="?cmd=export" />--%>
            
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <iframe style="Z-INDEX:-1;WIDTH:99%;POSITION:absolute;TOP:0px;" frameborder="0"></iframe>
        <div id="PopupDiv" style="position: absolute; display: block; z-index: 100">
            <table style="width: 99%;">
                <tr>
                    <td style="width: 15%" align="left" valign="top">
                        <div style="width:100px; overflow:scroll;overflow:hidden">
                            <asp:TreeView ID="TreeView1" runat="server">
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="width:85%" align="left" valign="top">
                        <div style="width: 100%; height: 100%;">
                            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                                <cc2:TabPanel runat="server" HeaderText="�豸�����б�" ID="TabPanel1">
                                    <ContentTemplate>
                                        <div id="PrintDiv" style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                                                HeaderStyle-Height="25px" RowStyle-Height="20px" OnRowCommand="GridView1_RowCommand"
                                                HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="CategoryID" HeaderText="�������">
                                                        
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CategoryName" HeaderText="��������">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Unit" HeaderText="�豸��λ">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ParentName" HeaderText="��һ������">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DepreciableLife" HeaderText="�۾�����">
                                                      
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ResidualRate" DataFormatString="{0:#,0.##}" HeaderText="����ֵ��(%)">
                                                       
                                                    </asp:BoundField>
                                                    <asp:ButtonField ButtonType="Image" Text="�鿴" ImageUrl="~/images/ICON/select.gif"
                                                        HeaderText="�鿴" CommandName="view">
                                                     
                                                    </asp:ButtonField>
                                                    <asp:TemplateField>
                                                     <HeaderTemplate>
                                                        ɾ��
                                                     </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif" CommandName="del"
                                                                CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('ȷ��Ҫɾ�����豸������Ϣ��')"
                                                                CausesValidation="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    û���豸������Ϣ
                                                </EmptyDataTemplate>
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                                                InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                                        </div>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" HeaderText="�豸�����ѯ" ID="TabPanel2">
                                    <ContentTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                            style="border-collapse: collapse;">
                                            <tr>
                                                <td class="Table_searchtitle" colspan="4">
                                                    ��ϲ�ѯ��֧��ģ����ѯ��
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    ������룺
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <input id="zhongleibianma" runat="server" type="text" />
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    �������ƣ�
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <input id="zhongleiming" runat="server" type="text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    �豸��λ��
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input id="shebeidanwei" runat="server" type="text" />
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    ��һ�����ࣺ
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input id="shangjizhonglei" runat="server" type="text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    �۾ɷ�����
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    
                                                    <asp:DropDownList ID="zhejiufangfa" runat="server">
                                                    <asp:ListItem Value="0" Selected="True" Text="--��ѡ��--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="���۾�"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="ֱ���۾�"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="˫�����"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    �۾����ޣ�
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input size="5" id="zhejiunianxian1" runat="server" type="text" />��&nbsp;��&nbsp;
                                                    <input size="5" id="zhejiunianxian2" runat="server" type="text" />��
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    ����ֵ�ʣ�
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input size="5" id="jinchangzhilv1" runat="server" type="text" />&nbsp;��&nbsp;
                                                    <input size="5" id="jinchangzhilv2" runat="server" type="text" />
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                                            runat="server">
                                            <tr>
                                                <td align="center" style="height: 38px">
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="ȷ��" OnClick="Button1_Click" />&nbsp;&nbsp;
                                                    <input id="Reset1" class="button_bak" type="reset" value="����" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                            </cc2:TabContainer>
                        </div>
                    </td>
                </tr>
            </table>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
