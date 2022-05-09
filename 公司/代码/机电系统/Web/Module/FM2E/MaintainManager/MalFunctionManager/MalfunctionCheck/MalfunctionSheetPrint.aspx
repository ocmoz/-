<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageNoCheck.master"
    AutoEventWireup="true" CodeFile="MalfunctionSheetPrint.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionReport_MalfunctionSheetPrint" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>js/Common.js"></script>

    <div style="width: 100%; text-align: center;">
        <table width="649px" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
            style="height: 960px; border-collapse: collapse;">
            <tr>
                <td colspan="4" rowspan="2" class="table_body_WithoutWidth" style="color: Black;
                    background-color: White;">
                    <b style="font-family: ����; font-size: medium">
                        <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label>
                         ά�޴���</b>
                </td>
                <td colspan="2" class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    �����
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="lbSheetNO" runat="server" Text="" Style=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="Table_searchtitle" style="color: Black; background-color: White;">
                    �������
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ���޵�λ
                </td>
                <td style="width: 18%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbDepartment" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ������
                </td>
                <td style="width: 17%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbReporter" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ����
                </td>
                <td style="width: 17%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbReportTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    �����豸(�ϱ�)
                </td>
                <td class="table_none_WithoutWidth" colspan="2" valign="middle">
                   <%-- <br />
                    <asp:Repeater ID="repeatEquipments" runat="server">
                        <ItemTemplate>
                            <%#Container.ItemIndex+1 %>���豸���ƣ�&nbsp;<%#Eval("EquipmentNAME") %>&nbsp;<%#!string.IsNullOrEmpty(Convert.ToString(Eval("EquipmentNO")))?"("+Eval("EquipmentNO")+")":"" %><br />
                        </ItemTemplate>
                    </asp:Repeater>
                    <br />--%>
                    <asp:Label  ID = "lbEquipmentNO" runat="server" Text=""></asp:Label>
                    
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    �����豸����
                </td>
                <td class="table_none_WithoutWidth" colspan="2" valign="middle">
                <asp:Label  ID = "lbEquipmentName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    �����豸��ַ
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
           <%-- <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ��ַ��ϸ����
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbAddressDetail" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ��������
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ����ԭ��
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbSystem" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ά�޵�λ
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbMaintainTeam" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ���ϵȼ�
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbMalfunctionRank" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    ��Ӧʱ��
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbResponseTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    �����Իָ�ʱ��
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbFunRestoreTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    �޸�ʱ��
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbRepairTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;">
                    ���ϼ�¼���ţ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbRecordDept" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;">
                    ���ϼ�¼�ˣ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbRecorder" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="color: Black; background-color: White;" colspan="6">
                    ά������Ǽ�
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;
                    width: 16%">
                    ά�޵�λ��
                </td>
                <td class="table_none_WithoutWidth " style="color: Black; background-color: White;
                    width: 18%">
                    <asp:Label ID="lbMaintainTeamx" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;
                    width: 16%">
                    �����ˣ�
                </td>
                <td class="table_none_WithoutWidth " style="color: Black; background-color: White;
                    width: 17%">
                    <asp:Label ID="lbReceiver" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;
                    width: 16%">
                    �������ڣ�
                </td>
                <td class="table_none_WithoutWidth " style="width: 17%">
                    <asp:Label ID="lbReceiveDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ʵ����Ӧʱ�䣺
                </td>
                <td class="table_none_WithoutWidth " style="width: 18%">
                    <asp:Label ID="lbActResponseTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ȷ���޸�ʱ�䣺
                </td>
                <td class="table_none_WithoutWidth " style="width: 17%">
                    <asp:Label ID="lbConTime" runat="server" Text=""></asp:Label></td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ���Ϻ�ʱ��
                </td>
                <td class="table_none_WithoutWidth " style="width: 17%">
                    <asp:Label ID="lbActRepairTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="table_none_WithoutWidth ">
                    ά����Ա�б�
                </td>
                <td colspan="3"  class="table_none_WithoutWidth ">
                    <asp:Label ID="lbMaintainStaffList" runat="server"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    �Ƿ������
                </td>
                <td colspan="1"  class="table_none_WithoutWidth " >
                    <asp:Label ID="jiliang" runat="server"></asp:Label>
                </td>
            </tr>
            <asp:Repeater ID="rptMaintainHistory" runat="server">
                <ItemTemplate>
                    <tr>
                        <td rowspan="2" align="center">
                            <%#Eval("UpdateTime", "{0:yyyy-MM-dd  HH:mm}")%>
                        </td>
                        <td style="padding-left: 20px; padding-top: 8px; text-align: left;" colspan="5" class="table_none_WithoutWidth ">
                            �Ǽ�ʱ�䣺<%#Eval("UpdateTime", "{0:yyyy-MM-dd HH:MM}")%>&nbsp;&nbsp;&nbsp;&nbsp;<%--ά�޵�λ��<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;ά�޼�¼�ˣ�<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;--%>
                            �޸������<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;�Ƿ����ޣ�<%#Convert.ToBoolean(Eval("IsDelivered"))?"��":"��"%><br />
                            ������ϸ������<%#Eval("MaintenanceDescription")%><br />
                            ���ϴ���취��<%#Eval("MaintenanceMethod")%><br />
                            ���豸���ϣ�<%#Eval("NoEquipment")%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Repeater ID="rptHistoryEquipments" runat="server" DataSource='<%# Eval("MaintainedEquipments") %>'
                                Visible='<%#((IList)Eval("MaintainedEquipments")).Count>0?true:false%>'>
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="1" 
                                        style="border-collapse: collapse;font-family: ����; font-size: medium">
                                        <tr class="table_none_WithoutWidth ">
                                                    <td colspan="7" align="center" style=" font-weight: bold;">
                                                        ����ά�޵��豸
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        �豸������
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        �豸����
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        �ͺ�
                                                    </td><td align="center" style="width: 6%">
                                                        �豸����
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        ά�޽��
                                                    </td>
                                                    <td align="center" style="width: 23%">
                                                        �豸����
                                                    </td>
                                                    <td align="center" >
                                                        ά�����
                                                    </td>
                                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="table_none_WithoutWidth ">
                                                <td align="center">
                                                 
                                                    <%#Eval("EquipmentNO") %>
                                                
                                                <%--<asp:Literal ID="lbEqNO" Text="" runat="Server"></asp:Literal>--%>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("EquipmentName") %>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("Model") %>
                                                </td>
                                                <td align="center"  >
                                                    <%#Eval("SerialNum") %>
                                                </td>
                                                <td align="center">
                                                    <%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %>
                                                </td>
                                                <td align="center" >
                                                    <%#Eval("LastAddress") %>
                                                </td>
                                                <%-- 
                                                <td align="center">
                                                    <%# Eval("MaintainFee", "{0:#,0.##}") %>
                                                </td>
                                                --%>
                                                <td align="center">
                                                    <%#Eval("Remark")%>
                                                </td>
                                            </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    
                </ItemTemplate>
            </asp:Repeater>
            
            
                    
                    <tr id="EqCostDisplayTR"><td colspan="6">
                    <asp:Panel ID="pnMoneyStatistics" runat="server" HorizontalAlign="Center">
                    <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;" >
                        <%--<tr >
                            <td id="Td1" class="Table_searchtitle" style="height: 30px;" colspan="9" runat="server">
                                ���Ϸ���ͳ�Ʊ�
                            </td>
                        </tr>--%>
                        
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth"  >һ</td>
                            <td class="table_body_WithoutWidth" colspan="9" >�豸�Ѽ����Ϸ�</td>
                        </tr>
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth" style="width: 5%"  >���</td>
                            <td class="table_none_WithoutWidth" style="width: 13%"  >����</td>
                            <td class="table_none_WithoutWidth" style="width: 13%"  >�ͺ�</td>
                            <td class="table_none_WithoutWidth" style="width: 5%"  >��λ</td>
                            <td class="table_none_WithoutWidth" style="width: 5%"  >����</td>
                            <td class="table_none_WithoutWidth" style="width: 10%"  >�ۺϵ���</td>
                            <td class="table_none_WithoutWidth" style="width: 12%"  >�ϼ�</td>
                            
                            <td class="table_none_WithoutWidth" style="width: 10%"  >����ۺϵ���</td>
                            <td class="table_none_WithoutWidth" style="width: 12%"  >��˺ϼ�</td>
                            
                            <td class="table_none_WithoutWidth" style="width: 15%"  >��ע</td>
                        </tr>
                        
                        <asp:Repeater ID="rpEquipmentItems"  runat="server">
                            <ItemTemplate>
                            <tr >
                                <td class="table_body_WithoutWidth"><%# (Container.ItemIndex+1)%></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqName"><%#Eval("EqName") %></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqModel"><%#Eval("EqModel") %></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqUnit"><%#Eval("EqUnit") %></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqNum"><%#Eval("EqNum") %></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqSinglePrice"><%#Eval("EqUnitPrice") %></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqSumPrice"><%#Eval("EqTotalPrice") %></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqApprovalUnitPrice"><%#Eval("EqApprovalUnitPrice")%></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqApprovalTotalPrice"><%#Eval("EqApprovalTotalPrice")%></label></td>
                                <td class="table_none_WithoutWidth"><label runat="server" id="lbEqRemark"><%#Eval("EqRemark") %></label></td>
                            </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth" >N</td>
                            <td class="table_none_WithoutWidth" colspan="5" >С��</td>
                            <td  class="table_none_WithoutWidth">
                                <asp:Label ID="lbSumTotal" runat="server">0</asp:Label>
                            </td>
                            <td  class="table_none_WithoutWidth">
                            </td>
                            <td  class="table_none_WithoutWidth">
                                <asp:Label ID="lbApprovalSumTotal" runat="server">0</asp:Label>
                            </td>
                            <td  class="table_none_WithoutWidth">
                            </td>
                        </tr>
                        
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth"  >��</td>
                            <td class="table_body_WithoutWidth"  colspan="9"  >��������</td>
                        </tr>
                        
                        <tr>
                            <td class="table_body_WithoutWidth"  >1</td>
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">��ʩ��</td>
                            <td class="table_none_WithoutWidth">
                                <asp:Label ID="lbMeasureCost" runat="server">0</asp:Label></td>
                            <td class="table_none_WithoutWidth">
                            </td>
                            <td class="table_none_WithoutWidth">
                                <asp:Label ID="lbApprovalMeasureCost" runat="server">0</asp:Label></td>
                            <td class="table_none_WithoutWidth"><asp:Label ID="lbMarkOne" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="table_body_WithoutWidth"  >2</td>
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">���</td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbGuiCost" runat="server">0</asp:Label>
                            </td>
                            <td class="table_none_WithoutWidth"  ></td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbApprovalGuiCost" runat="server">0</asp:Label>
                            </td>    
                            <td class="table_none_WithoutWidth"><asp:Label ID="lbMarkTwo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="table_body_WithoutWidth"  >3</td>
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">˰��</td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbTaxCost" runat="server">0</asp:Label>
                            </td>
                            <td class="table_none_WithoutWidth"  ></td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbApprovalTaxCost" runat="server">0</asp:Label>
                            </td>
                            <td class="table_none_WithoutWidth"><asp:Label ID="lbMarkThree" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="table_body_WithoutWidth"  >4</td>
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">��ͨ��</td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbTrafficCost" runat="server">0</asp:Label>
                            </td>
                            <td class="table_none_WithoutWidth"  ></td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbApprovalTrafficeCost" runat="server">0</asp:Label>
                            </td>
                            <td class="table_none_WithoutWidth"><asp:Label ID="lbMarkFour" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="table_body_WithoutWidth"  >5</td>
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">��������</td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbOtherCost" runat="server">0</asp:Label></td>
                                <td class="table_none_WithoutWidth"  ></td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbApprovalOtherCost" runat="server">0</asp:Label></td>
                            <td class="table_none_WithoutWidth" colspan="2">
                                <asp:Label ID="lbMarkFive" runat="server"> </asp:Label>
                                
                            </td>
                        </tr>
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth"  >N</td>
                            <td class="table_none_WithoutWidth" colspan="5" >С��</td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbSumOther" runat="server" ></asp:Label>
                            </td>
                            <td class="table_none_WithoutWidth"  ></td>
                            <td class="table_none_WithoutWidth"  >
                                <asp:Label ID="lbApprovalSumOther" runat="server" ></asp:Label>
                            </td>
                            <td class="table_none_WithoutWidth">
                            </td>
                        </tr>
                        
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth"  >��</td>
                            <td class="table_body_WithoutWidth"  colspan="5" >�ϼ�</td>
                            <td style="FONT-SIZE: 9pt;background:#efefef;height:30px;PADDING-LEFT: 8px;PADDING-RIGHT:5px;font-family: 'Verdana', 'Arial', 'Helvetica', 'sans-serif'; text-align:left;">
                                <asp:Label ID="lbSumAll" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth"  ></td>
                            <td style="FONT-SIZE: 9pt;background:#efefef;height:30px;PADDING-LEFT: 8px;PADDING-RIGHT:5px;font-family: 'Verdana', 'Arial', 'Helvetica', 'sans-serif'; text-align:left;">
                                <asp:Label ID="lbApprovalSumAll" runat="server"></asp:Label>
                            </td>
                            
                            </td>
                        </tr>
                        
                    </table>
                    </ContentTemplate>
                    </asp:Panel>
                    </td></tr>
            
            
            <tr>
                        <td class="Table_searchtitle" colspan="6">
                            �����������
                        </td>
                    </tr>
                   <asp:Repeater ID="Repeater4Check" runat="server">
                        <%--<HeaderTemplate><tr></HeaderTemplate>--%>
                        <ItemTemplate>
                        <tr>
                            <td class="table_body_WithoutWidth " style="height: 30px;">
                                ȷ��ʱ�䣺<%#Eval("CheckDate")%>
                            </td>
                            
                            <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                                &nbsp;&nbsp;<%#Eval("UserDeptName")%>�������˵����<br />
                            
                                &nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%> <br />
                                &nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("CheckDate")%>&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>    
                        </ItemTemplate>
                        <%--<FooterTemplate></tr></FooterTemplate>--%>
                    </asp:Repeater> 
            
            <tr runat="server" id="trHandleOpinion" visible="false">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ���������
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                </td>
            </tr>
            <%-- 
            <tr >
                <td class="Table_searchtitle" colspan="6" style="width: 16%; color: Black; background-color: White;">
                    ���ϴ�������ȵ���
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ά��ʱ�����ۣ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <asp:CheckBox ID="cbIsResponseInTime" runat="server" Text="��Ӧ��ʱ" />
                    <asp:CheckBox ID="cbIsFunRestoreInTime" runat="server" Text="���ָܻ���ʱ" />
                    <asp:CheckBox ID="cbIsRepairInTime" runat="server" Text="�޸���ʱ" />
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ����Ч����
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblEffect" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                    </asp:CheckBoxList>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    �������ۣ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblTechnicEvaluate" runat="server" RepeatDirection="Horizontal"
                        Style="display: inline">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ����̬�ȣ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblAttitude" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                    </asp:CheckBoxList>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ������ϵĺ����ԣ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblRationality" runat="server" RepeatDirection="Horizontal"
                        Style="display: inline">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ʹ�ò��������
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <asp:Label ID="lbFeeBackOpinion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            --%>
            
         <tr >
                        <td class="Table_searchtitle" colspan="6">
                            �����������
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���ϵ�������¼��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <%--<HeaderTemplate><table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;"></HeaderTemplate>
                                <ItemTemplate>
                                <tr>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;<%#Eval("UserDeptName")%>���������</td>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;<%#Eval("EvenName")%></td>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%></td>
                                    <td colspan="3" bordercolor="#ffffff" style=" border-bottom-color:#cccccc">&nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("ApprovalDate")%>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    </tr>
                                </ItemTemplate>--%>
                                <HeaderTemplate>
                                    <table border="0" width="100%">
                                   <tr>
                                    <th>��������</th>
                                    <th>�������</th>
                                    <th>�������</th>

                                    <th>ְλ</th>
                                    <th>�û���</th>
                                    <th>��������</th>

                                </tr>
                </HeaderTemplate>

                                <ItemTemplate>
                                <tr>
                                    <td><%#Eval("UserDeptName")%></td>
                                    <td><%#Eval("EvenName")%> </td>
                                    <td><%#Eval("Remark")%> </td>

                                    <td><%#Eval("UserPsnName")%> </td>
                                    <td><%#Eval("UserName")%></td>
                                     <td><%#Eval("ApprovalDate")%></td>

                                </tr>
                                </ItemTemplate>

                                <FooterTemplate></table ></FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
            
             
                    <tr ID = "DelayDisplay" runat="server">
                        <td class="table_body_WithoutWidth " style="height: 30px;" >
                            �������룺
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbshenqingjiliang" runat="server" Text="" ForeColor="Blue"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �Ƿ������
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbjiliang" Text="" ForeColor="Blue" runat="server"></asp:Label>                                                      
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �Ƿ�׹���
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbjiagong" Text="" ForeColor="Blue" runat="server"></asp:Label>                            
                        </td>
                        
                    </tr>
                  <table width="649px" id="divCancelDetail" runat="Server" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                �������
                            </td>
                        </tr>
                        <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                ���������ˣ�
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbApplyCancelName" runat="Server" Text=""></asp:Label>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                ��������ʱ�䣺
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbCancelApplyTime" runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                ���볷��ԭ��
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyCancelReason"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                �������������
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbCancelApprove"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                  
                    <table width="649px" id="divDelayApply" runat="Server" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                             ��ʱ�������
                            </td>
                        </tr>
                        <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                �ύ��ʱ����ʱ�䣺
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbDelayApplyTime" runat="Server" Text=""></asp:Label>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                �����ϵ������ʱЧʱ��Ϊ��
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbDelayForTime" runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                ��ʱԭ��
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyDelayReason"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                ��ʱ���������
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3"  rowspan="3" style="height: 30px">
                               <asp:Label ID="lbDelayApprove"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                  
                  
            
            <tr id="trCancelStatus" runat="server" visible="false" style="display:none;">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ���ϵ�״̬��
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    �����ˣ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbCanceler" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr id="trCancelReason" runat="server" visible="false" style="display:none;">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ����ԭ��
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <asp:Label ID="lbCancelReason" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <%--<tr id="tr1" runat="server" style="display:none;">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    ά����Աȷ�ϣ�
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <br />
                    <span style="float: right">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>
                </td>
            </tr>--%>
        </table>
    </div>
    <object id='WebBrowser' width="0" height="0" classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'>
    </object>

    <script language="javascript" type="text/javascript">
        PrintPreview('WebBrowser');
    </script>

</asp:Content>
