<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_PriceDetail, App_Web_nuqbk7th" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

<script type="text/javascript">
    function clearbox() {

        document.getElementById('<%= TextBox_ProductName.ClientID %>').value = "";
        document.getElementById('<%= TextBox_Model.ClientID %>').value = "";
        document.getElementById('<%= TextBox_Approvaler.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApplyTimeLower.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApplyTimeUpper.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApprovalTimeLower.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApprovalTimeUpper.ClientID %>').value = "";
    }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�۸����"
        HeadOPTxt="Ŀǰ�������ܣ�ָ���Լ۸�ά��" HeadHelpTxt="��ʾ������ǰ�ġ���ʷ�ĺ������е�ָ���۸�">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="����ָ���۸���Ϣ" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="Href" ButtonUrl="EditPrice.aspx" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                <cc2:TabPanel runat="server" HeaderText="��ǰָ���۸��б�" ID="TabPanel1">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="��Ʒ����">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Model" HeaderText="��Ʒ����ͺ�">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unit" HeaderText="��۵�λ">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="����ʱ��">
                                        <HeaderStyle />
                                    </asp:BoundField>
             
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            ָ���۸�(Ԫ)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LowerPrice" runat="server" Text='<%#Eval("LowerPrice","{0:#,0.##}")%>'></asp:Label>
                                            &nbsp;��
                                            <asp:Label ID="UpperPrice" runat="server" Text='<%#Eval("UpperPrice","{0:#,0.##}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" Text="�༭" ImageUrl="~/images/ICON/edit.gif" HeaderText="�༭"
                                        CommandName="view">
                                        <HeaderStyle Width="70px" />
                                    </asp:ButtonField>
                                </Columns>
                                <EmptyDataTemplate>
                                    û�е�ǰָ���۸���Ϣ
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                                InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="��ʷָ���۸��б�" ID="TabPanel2">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:GridView Width="100%" ID="GridView2" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="��Ʒ����">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Model" HeaderText="��Ʒ����ͺ�">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unit" HeaderText="��۵�λ">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="����ʱ��">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="ͣ��ʱ��">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            ָ���۸�(Ԫ)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LowerPrice" runat="server" Text='<%#Eval("LowerPrice","{0:#,0.##}")%>' ></asp:Label>
                                            &nbsp;��
                                            <asp:Label ID="UpperPrice" runat="server" Text='<%#Eval("UpperPrice","{0:#,0.##}")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    û����ʷָ���۸���Ϣ
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager2" runat="server" OnPageChanged="AspNetPager2_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                                InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
                
                <cc2:TabPanel runat="server" HeaderText="ָ���۸��ѯ" ID="TabPanel4">
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
                                    ��Ʒ���ƣ�
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ����ͺţ�
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="Model" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ��ǰ������ʷ�۸�
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:DropDownList ID="historyorcurrent" onchange="javascript:return selectchange()"
                                        runat="server">
                                        <asp:ListItem Value="1">��ǰ�۸�</asp:ListItem>
                                        <asp:ListItem Value="2">��ʷ�۸�</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                           </tr>
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ָ���۸�Χ��
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:TextBox ID="LowerPrice" title="������ָ���۸�����~float" runat="server"></asp:TextBox>
                                    ��
                                    <asp:TextBox ID="UpperPrice" title="������ָ���۸�����~float" runat="server"></asp:TextBox> 
                                    
                                </td>
                                
                             </tr>
                             
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ����ʱ�䷶Χ��
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                
                                <asp:TextBox ID="StartTime2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="����������ʱ������~date"></asp:TextBox> ��
                                    <asp:TextBox ID="StartTime1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="����������ʱ������~date"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    <div id="id1" style="display: <%= ShowEndTime %>">
                                        ͣ��ʱ�䷶Χ��
                                    </div>
                                </td>
                            
                                <td class="table_none table_none_NoWidth" colspan="3">
                                    <div id="id2" style="display: <%= ShowEndTime %>">
                                     <asp:TextBox ID="EndTime2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="������ͣ��ʱ������~date"></asp:TextBox>
                                             ��
                                        <asp:TextBox ID="EndTime1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="������ͣ��ʱ������~date"></asp:TextBox>
                                       
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                            runat="server">
                            <tr>
                                <td align="center" style="height: 38px">
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" OnClick="Button1_Click"
                                        Text="ȷ��" />&nbsp;&nbsp;
                                    <input id="Reset1" class="button_bak" type="reset" value="����" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                
                <cc2:TabPanel runat="server" HeaderText="���������б�" ID="TabPanel3">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:GridView Width="100%" ID="GridView3" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="GridView3_RowCommand" OnRowDataBound="GridView3_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ApplicantName" HeaderText="������">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApplyDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="��������">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApprovalerName" HeaderText="������">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            ��������</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovalDate" runat="server" Text='<%#DateTime.Compare(Convert.ToDateTime(Eval("ApprovalDate")),DateTime.MinValue)==0?"": Eval("ApprovalDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StatusName" HeaderText="��ǰ״̬">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" Text="�鿴" ImageUrl="~/images/ICON/select.gif"
                                        HeaderText="�鿴" CommandName="view">
                                        <HeaderStyle Width="60px" />
                                    </asp:ButtonField>
                                </Columns>
                                <EmptyDataTemplate>
                                    û��������������Ϣ
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager3" runat="server" OnPageChanged="AspNetPager3_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="�ܼ�¼��0  ҳ�룺1/1  ÿҳ��10"
                                InvalidPageIndexErrorMessage="ҳ����������Ч����ֵ��" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="ҳ����������Χ��" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
                
                <cc2:TabPanel runat="server" HeaderText="������ѯ" ID="TabPanel5">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    ����������ϲ�ѯ��֧��ģ����ѯ��
                                </td>
                            </tr>
                            <tr>
                             <td class="table_body table_body_NoWidth" style="height: 30px; width:10%">
                                    �豸���ƣ�
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px; width:40%">
                                    <asp:TextBox ID="TextBox_ProductName" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px;width:10%">
                                    �豸�ͺţ�
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px;width:40%">
                                    <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ������������
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:TextBox ID="TextBox_Approvaler" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ����ʱ�䷶Χ��
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                
                                <asp:TextBox ID="TextBox_ApplyTimeLower" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="����������ʱ������~date"></asp:TextBox> ��
                                    <asp:TextBox ID="TextBox_ApplyTimeUpper" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="����������ʱ������~date"></asp:TextBox>
                                    
                                </td>
                                
                             </tr>
                             
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    ����ʱ�䷶Χ��
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                
                                <asp:TextBox ID="TextBox_ApprovalTimeLower" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="����������ʱ������~date"></asp:TextBox> ��
                                    <asp:TextBox ID="TextBox_ApprovalTimeUpper" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="����������ʱ������~date"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            
                            
                       
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="Button_QueryApply" runat="server" CssClass="button_bak" OnClick="Button_QueryApply_Click"
                                        Text="ȷ��" />&nbsp;&nbsp;
                                    <input id="Reset_QueryApply" class="button_bak" type="button" value="����" onclick="javascript:clearbox();" />
                                </td>
                            </tr>
                        </table>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
    function selectchange()
    {
    var temp = document.getElementById('<%=this.historyorcurrent.ClientID%>');
    if(temp.value=="1"){
        document.getElementById("id2").style.display = "none";
        document.getElementById("id1").style.display = "none";
        }
    if(temp.value=="2"){
        document.getElementById("id2").style.display = "block";
        document.getElementById("id1").style.display = "block";
        }
    }
    
    </script>

</asp:Content>
