<%@ page title="�鿴���ɹ���" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_InWarehouseHistoryPurchaseOrder, App_Web_e2vo_fnn" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        function HideShow(objid) {

            var obj = document.getElementById(objid);

            if (obj != null) {
                obj.style.display = (obj.style.display == 'none' ? 'block' : 'none');
            }
        }
    </script>
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="��Ʒ������ⵥ�鿴"
        HeadOPTxt="Ŀǰ�������ܣ���ⵥ�鿴" HeadHelpTxt="ǳ��ɫ����Ϊ���ֿ�������">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="������ⵥ��ʷ�б�" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="InWarehouseHistory.aspx" />
    </cc1:HeadMenuWebControls>

    <input type="hidden" id="Hidden_WarehouseID" runat="server" />
    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    �����깺��
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="2" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="δ֪��˾"></asp:Label><br />
                    <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue" ></asp:Label>&nbsp;��������깺��
                </td>
                <td style="text-align: center" colspan="2" class="Table_searchtitle">
                    �����
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="Label_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                        
                        <table id="Table_detaillist" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                                border: solid 1px #a7c5e2;" border="1">
                                <tr>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ���
                                    </th>
                                     <th  style="width: 8%" class="Table_searchtitle"
                                    
                                    >ϵͳ</th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ��Ʒ����
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ����ͺ�
                                    </th>
                                    <th style="width: 15%" class="Table_searchtitle">
                                        ����<br />
                                        ����/�ɹ�/����
                                    </th>
                                    <th style="width: 5%" class="Table_searchtitle">
                                        ��λ
                                    </th>
                                    <th class="Table_searchtitle">
                                        ��ע
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        ״̬
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        �ɹ���
                                    </th>
                                    
                                </tr>
                                <asp:Repeater ID="Repeater_ItemList" runat="server">
                                    <ItemTemplate>
                                        <tr id="tr_item" runat="server">
                                            <td style="text-align: center">
                                                <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                              <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="Label_AdjustCount" runat="server" ForeColor="Red" Text='<%# Eval("FinalCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                    ID="Label_ActualCount" runat="server" Text='<%# Eval("ActualCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                        ID="Label_AcceptedCount" runat="server" Text='<%# Eval("AcceptedCount", "{0:#,0.##}") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="Label_Purchaser" Text='<%# Eval("PurchaserName") %>' runat="server">
                                                </asp:Label>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td valign="top" style="text-align: center">
                                                <a href='<%# "javascript:HideShow(\"" + Container.FindControl("div_recordlist").ClientID + "\");" %>'>
                                                    <span style="color: Blue">�ɹ���¼</span></a>
                                            </td>
                                            <td colspan="8">
                                                <div runat="server" id="div_recordlist">
                                                <table id="table_PurchaseRecordList" style="width: 100%; border-collapse: collapse;
                                                    vertical-align: middle; border: solid 1pt #000000;" border="1">
                                                    <tr>
                                                        <th style="width: 3%" class="Table_searchtitle">
                                                            ���
                                                        </th>
                                                        <th style="width: 7%" class="Table_searchtitle">
                                                            ��Ʒ����
                                                        </th>
                                                        <th style="width: 8%" class="Table_searchtitle">
                                                            ����ͺ�
                                                        </th>
                                                        <th style="width: 7%" class="Table_searchtitle">
                                                            ����<br />
                                                            �ɹ�/����
                                                        </th>
                                                        <th style="width: 5%" class="Table_searchtitle">
                                                            ��λ
                                                        </th>
                                                        <th style="width: 5%" class="Table_searchtitle">
                                                            ����(Ԫ)
                                                        </th>
                                                        <th style="width: 5%" class="Table_searchtitle">
                                                            ���(Ԫ)
                                                        </th>
                                                        
                                                        <th style="width: 10%" class="Table_searchtitle">
                                                            ������Ӧ
                                                        </th>
                                                        <th style="width: 10%" class="Table_searchtitle">
                                                            ������
                                                        </th>
                                                        <th style="width: 8%" class="Table_searchtitle">
                                                            ״̬
                                                        </th>
                                                        <th style="width: 12%" class="Table_searchtitle">
                                                            ��ע
                                                        </th>
                                                        <th style="width: 5%" class="Table_searchtitle">
                                                            ���
                                                        </th>
                                                    </tr>
                                                    <asp:Repeater ID="Repeater_PurchaseRecordList" runat="server" DataSource='<%# Eval("PurchaseRecordList") %>' OnItemDataBound="Repeater_ItemList_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="tr_item" runat="server">
                                                                 <td style="text-align: center">
                                                                    [<asp:Label ID="Label_ID" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>]
                                                                    <input id="Hidden_Record_OrderID" runat="server" value='<%# Eval("OrderID") %>' type="hidden" />
                                                                    <input id="Hidden_Record_ItemID" runat="server" value='<%# Eval("PlanDetailItemID") %>' type="hidden" />
                                                                    <input id="Hidden_PurchaseRecordID" runat="server" value='<%# Eval("ID") %>' type="hidden"/>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="Label_ProductName2" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                                </td>
                                                                 <td style="text-align: center">
                                                                    <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="Label_PurchaseCount" runat="server" Text='<%# Eval("PurchaseCount", "{0:#,0.#####}") %>'></asp:Label>
                                                                    &nbsp;/&nbsp;
                                                                    <asp:Label ID="Label_AcceptanceCount" runat="server" Text='<%# Eval("AcceptanceCount", "{0:#,0.#####}") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                                                </td>
                                                               <td style="text-align: center">
                                                                    <asp:Label ID="Label_PurchaseUnitPrice" runat="server" Text='<%# Eval("PurchaseUnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="Label_PurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount", "{0:#,0.##}") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    �����̣�<asp:Label ID="Label_Producer" runat="server" Text='<%# Eval("Producer") %>'></asp:Label><br />
                                                                    ��Ӧ�̣�<asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    �ɹ�Ա��<asp:Label ID="Label_Purchaser" runat="server" Text='<%# Eval("PurchaserName") %>'></asp:Label><br />
                                                                    �ֹ�Ա��<asp:Label ID="Label_Checker_Warehouse" runat="server" Text='<%# Eval("WarehouseKeeperName") %>'></asp:Label><br />
                                                                    ����Ա��<asp:Label ID="Label_Checker_Technician" runat="server" Text='<%# Eval("TechnicianName") %>'></asp:Label>
                                                                 <br />
                                                                �ֿ⣺<asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                                                </td>
                                                                 <td style="text-align: center">
                                                                    <asp:Label ID="Label_StatusString" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    �ɹ���<asp:Label ID="Label_PurchaseRemark" runat="server" Text='<%# Eval("PurchaseRemark") %>'></asp:Label><br />
                                                                    ���գ�<asp:Label ID="Label_AcceptanceRemark" runat="server" Text='<%# Eval("AcceptanceRemark") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                                                                                        <asp:Button ID="Button_Divide" runat="server" Text="���" CssClass="button_bak" OnClientClick='javascript:showDivideWin(this.id);' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="12" style="word-break: break-all">
                                                                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000" 
                                                                        style="border-collapse: collapse;">
                                                                        <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Eval("BarcodeStrList") %>' OnItemCommand="Repeater_BarcodeList_OnCommand">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td style="width: 5%; text-align: center">
                                                                                        <asp:Label ID="Label_DivideBatch" Text='<%# Container.ItemIndex + 1%>' runat="server">
                                                                                
                                                                                        </asp:Label><br />
                                                                                        <asp:Label ID="Label_Name" Text='<%# Eval("Name") %>' runat="server"></asp:Label><br />
                                                                                        [<asp:Label ID="Label2" Text='<%# Eval("Model") %>' runat="server"></asp:Label>]
                                                                                    </td>
                                                                                    <td style="width: 85%;">
                                                                                        <asp:Label ID="Label1" Text='<%# Eval("BarcodeRecordString") %>' runat="server">
                                                                                        </asp:Label>
                                                                                    </td>
                                                                                     <td style="width: 10%;">
                                                                    <asp:UpdatePanel ID="UpdatePanel_Print" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                         <asp:Button ID="Button_Print" runat="server" CommandArgument='<%# Eval("ItemID").ToString()+"|"+Eval("PurchaseRecordID").ToString() %>' Text= "��ӡ������" CssClass="button_bak2" 
                                                                         CommandName="PRINT" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                       
                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                          
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    �ύʱ�䣺
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    �����ˣ�
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    �����£�
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_UpdateTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    ��ǰ״̬��
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_Status" runat="server"></asp:Label><asp:Label ID="Label_Approvaling"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    ������ʷ
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_ApprovalRecord">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="������ ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" Text='<%# Eval("FeeBack") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ϸ����">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Eval("ApprovalLog") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����ʱ��">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
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
                <td class="Table_searchtitle" colspan="4">
                    �޸���ʷ
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ModifyRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="�޸��� ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Eval("ModifierName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�޸Ĳ���">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Eval("ModifyTypeString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��������">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" Text='<%# Eval("Content") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�޸�ʱ��">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Eval("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
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
        </table>
    </div>
    <script type="text/javascript" language="javascript">
        //���һ������ˢ��
        function doconfirm(returnstring) {
            //ˢ�¼���
            window.location.reload();
        }

        //���
        function showDivideWin(id) {
            var orderid = document.getElementById(id.replace("Button_Divide", "Hidden_Record_OrderID")).value;
            var itemid = document.getElementById(id.replace("Button_Divide", "Hidden_Record_ItemID")).value;
            var recordid = document.getElementById(id.replace("Button_Divide", "Hidden_PurchaseRecordID")).value;
            var warehouse = document.getElementById('<% =Hidden_WarehouseID.ClientID %>').value;
            showPopWin("����豸", "ComponentInWarehouse.aspx?type=p&id=" + orderid + "&itemid=" + itemid + "&recordid=" + recordid + "&warehouse=" + warehouse, 900, 430, doconfirm, true, false);
        }
    </script>
</asp:Content>
