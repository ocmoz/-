<%@ Page Title="�鿴�ɹ���" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovalHistoryViewPurchaseOrder.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_ApprovalHistoryViewPurchaseOrder" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <script type="text/javascript">
     //����һ������
        function addtolist(addstring) {
            document.getElementById('<%= Hidden_ApprovalItem.ClientID %>').value = addstring;
            document.getElementById('<%= Button_ApprovalItem.ClientID %>').click();
        }

        //��˽����ʾ���棺����޸Ĺ�����ѡ�����ͨ��������ʾ
        //���û���޸Ĺ�����ѡ�񷵻��޸ģ�����ʾ
        //�����Ƿ��޸Ĺ���ѡ��ͨ��������ʾ
        function approvalAlert() {

            if(trim(document.getElementById('<%= TextBox_ApprovalRemark.ClientID %>').value).length==0)
            {
                alert('�������������');
                return false;
            }
            var select = document.getElementById('<%= DropDownList_ApprovalResult.ClientID %>');
           
            var hasModify = (document.getElementById('<%= Span_Adjust.ClientID %>').innerHTML.length > 0);
            
            switch (select.options[select.selectedIndex].value ) {
                case '<%= (int)PurchaseOrderApprovalResult.PASS  %>':
                    if (hasModify)
                        return confirm('�����뵥����������ȷ��ͨ��������');
                    else
                        return confirm('ȷ��ͨ��������');
                    break;
            case '<%= (int)PurchaseOrderApprovalResult.RETURNANDMODIFY %>':
                if (!hasModify)
                    return confirm('�����뵥û�е�������ȷ�������뵥��Ҫ�����޸ģ�');
                else
                    return confirm('ȷ���������������');
                break;
            case '<%= (int)PurchaseOrderApprovalResult.NOTPASS %>':
                return confirm('ȷ��ֱ�ӷ�������뵥��');
                break;
                default:
                return false;
                break;
            }
        }
    </script>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="��Ʒ�����ɹ�"
        HeadOPTxt="Ŀǰ�������ܣ��鿴�깺��" HeadHelpTxt="���������ѯ��桢������۲�ѯָ����">

        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="������ʷ�б�" ButtonUrlType="Href" ButtonPopedom="List"
            ButtonUrl="PurchaseApprovalHistory.aspx" />

    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="6">
                    �����깺��
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="3" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="δ֪��˾"></asp:Label><br />
                    <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue" ></asp:Label>&nbsp;��������깺��
                </td>
                <td style="text-align: center" colspan="3" class="Table_searchtitle">
                    �����
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="3">
                    <asp:Label ID="Label_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="gridview_ItemList_RowDataBound" ShowFooter="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ϵͳ ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��Ʒ���� ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����ͺ�">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����<br/>����/����">
                                        <ItemTemplate>
                                         
                                            <asp:Label ID="Label_PlanCount" runat="server" 
                                                Text='<%# Eval("PlanCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustCount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustCount", "{0:#,0.##}") %>'></asp:Label><a id="querystorage" runat="server" 
                                           href='<%# string.Format(Eval("ProductQueryStorageScript").ToString(),CanAdjust)  %>'><span style="color:Blue">��ѯ���</span></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��λ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����(Ԫ)<br/>����/����">
                                        <ItemTemplate>
                                           
                                           <asp:Label ID="Label_UnitPrice" runat="server" Text='<%# Eval("Price", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustPrice" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustPrice", "{0:#,0.##}") %>'></asp:Label>
                                           
                                          <a runat="server"  id="queryprice"
                                           href='<%# string.Format(Eval("ProductQueryPriceScript").ToString(),CanAdjust) %>'><span style="color:Blue">��ѯָ����</span> </a>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���(Ԫ)<br/>����/����">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_PlanAmount" runat="server" Text='<%# Eval("PlanAmount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustAmount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustAmount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustTotalAmount" runat="server" ForeColor="Red" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ע">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        δ����깺����</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align:right; width:10%">�����ˣ�</td>
                <td style="width:20%">  <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label></td>
                <td class="Table_searchtitle" style="text-align:right; width:10%">�ύʱ�䣺</td>
                <td style="width:20%">  <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label></td>
                 <td class="Table_searchtitle" style="text-align:right; width:10%">��ǰ״̬��</td>
                 <td style="width:20%">  
                 <asp:Label ID="Label_Status" runat="server"></asp:Label>
                 <asp:Label ID="Label_Approvaling" runat="server" Visible="false"></asp:Label>
                 </td>
            </tr>
             <tr>
                
                 <td class="Table_searchtitle" style="text-align:right; width:15%">��ע��</td>
                 <td style="width:85%"  colspan="5">  <asp:Label ID="Label_Remark" runat="server"></asp:Label></td>
            </tr>
             <tr>
                <td class="Table_searchtitle" colspan="6">
                   ������ʷ
                </td>
            </tr>
                        <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_ApprovalRecord">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                 >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="������ ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" 
                                                Text='<%# Bind("FeeBack") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ϸ����">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Eval("ApprovalLog") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����ʱ��">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        δ������</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                 <td  colspan="6" style="text-align:right">
                 <asp:Button ID="Button_GetApproval" runat="server" Text="��Ҫ����" Visible="false" 
                        CssClass="button_bak" onclick="Button_GetApproval_Click" />
                 <asp:Button ID="Button_ReleaseApproval" runat="server" Text="��������"  OnClientClick="javascript:return confirm('ȷ�Ϸ���������');" Visible="false"
                        CssClass="button_bak" onclick="Button_ReleaseApproval_Click"/>
                 <input id="Hidden_ApprovalItem" value="" runat="server" type="hidden" />
                 <input id="Button_ApprovalItem" type="button" runat="server" value="���" style="display: none"
                                onserverclick="Button_ApprovalItem_Click" />
                 </td>
            </tr></table>
          <table runat="server" id="Table_Approval" visible="false" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
              <tr>
                <td class="Table_searchtitle"  colspan="2">
                   ����
                </td>
            </tr>
            <tr>
                
                 <td class="Table_searchtitle" style="text-align:right; width:15%">���������</td>
                 <td style="width:85%" > 
                     <asp:DropDownList ID="DropDownList_ApprovalResult" runat="server">
                     </asp:DropDownList>
                     
                 </td>
            </tr>
            <tr>
             <td class="Table_searchtitle" style="text-align:right; width:15%">���������</td>
                 <td style="width:85%; text-align:left" > 
                    <font color="red"><span id="Span_Adjust" runat="server"></span></font> 
                 </td>
            </tr>
            <tr>
                
                 <td class="Table_searchtitle" style="text-align:right; width:15%">���������</td>
                 <td style="width:85%"> 
                     <asp:TextBox ID="TextBox_ApprovalRemark" runat="server" TextMode="MultiLine" 
                         Width="600px" Height="200px"></asp:TextBox> </asp:Label></td>
            </tr>
             <tr>
                <td class="Table_searchtitle"  colspan="2">
                   <asp:Button ID="Button_DoApproval" runat="server" Text="����" 
                        CssClass="button_bak" onclick="Button_DoApproval_Click"
                         OnClientClick="javascript:return approvalAlert();"
                         />
                </td>
            </tr>
              
            </table>
            
         <table id="Table_ModifyRecord" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
            <tr>
                <td class="Table_searchtitle" colspan="6">
                   �޸���ʷ
                </td>
            </tr>
                        <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ModifyRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                 >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="�޸��� ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Eval("ModifierName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�޸Ĳ���">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Eval("ModifyTypeString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" 
                                                Text='<%# Eval("Content") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="�޸�ʱ��">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Eval("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        δ���޸�</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
             <tr>
                <td class="Table_searchtitle" colspan="6">
                   ����깺��
                </td>
             </tr>
                        <tr>
                <td colspan="6">

                            <asp:GridView ID="gridview_RelatedOrders" runat="server" AutoGenerateColumns="False" 
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" 
                                 RowStyle-Height="20px" Width="100%"
                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" 
                                >
                        <Columns>
                            <asp:TemplateField HeaderText="�깺��">
                                <ItemTemplate>
                                    <a style="color:Blue" href='../PurchaseApply/ViewPurchaseOrder.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history&return=1'>
                                    <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label Text='<%# Bind("SubOrderIndex")%>' runat="server"
                                    ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                     <asp:Label Text='<%# Eval("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName" Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;��������깺��
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%"  />
                            </asp:TemplateField>
                            <asp:BoundField DataField="WorkFlowStateDescription" HeaderText="״̬">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="UpdateTime" HeaderText="������ʱ��" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                <HeaderStyle />
                                <ItemStyle  Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="��ע">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                           
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>

                </td>
            </tr>
            </table>
    </div>
</asp:Content>

