<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPageNoCheck.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionCheck_CancelMalfunctionSheet, App_Web_js492bxd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="������������"
        HeadHelpTxt="����" HeadOPTxt="Ŀǰ�������ܣ�������������">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="�༭" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="����" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="���ϵ��б�" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="��ӡ" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="upMain" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td colspan="4" rowspan="2" class="table_body_WithoutWidth">
                            <b style="font-family: ����; font-size: medium">
                                <asp:Label ID="lbCompany" runat="server" Text="XX"></asp:Label>
                                ά�޴���</b>
                        </td>
                        <td colspan="2" class="table_body_WithoutWidth">
                            �����
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Label ID="lbSheetNO" runat="server" Text="" Style=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="Table_searchtitle" style="height: 30px">
                            �������
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            ���޵�λ
                        </td>
                        <td style="width: 18%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbDepartment" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            ������
                        </td>
                        <td style="width: 17%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbReporter" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            ����
                        </td>
                        <td style="width: 17%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbReportTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                  <%--  <tr>
                        <td class="table_body_WithoutWidth">
                            �����豸
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5" valign="middle">
                            <br />
                            <asp:Repeater ID="repeatEquipments" runat="server">
                                <ItemTemplate>
                                    <%#Container.ItemIndex+1 %>���豸���ƣ�&nbsp;<%#Eval("EquipmentNAME") %>&nbsp;<%#!string.IsNullOrEmpty(Convert.ToString(Eval("EquipmentNO")))?"("+Eval("EquipmentNO")+")":"" %><br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />
                        </td>
                    </tr>--%>
                    
                    
                    <tr>
                        <td class="table_body_WithoutWidth">�����豸������</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:Label ID="lbEqNo" runat="server" Text=""></asp:Label></td>
                        <td class="table_body_WithoutWidth">�����豸����</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:Label ID="lbEqName" runat="server" Text=""></asp:Label></td>
                        <td class="table_body_WithoutWidth">����ϵͳ</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:Label ID="lbEqSystem" runat="server" Text=""></asp:Label></td>
                    </tr>
                    
                    
                    <tr>
                        <td class="table_body_WithoutWidth">
                            �����豸��ַ
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="table_body_WithoutWidth">
                            ��ַ��ϸ����
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:Label ID="lbAddressDetail" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="table_body_WithoutWidth">
                            ��������
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td class="table_body_WithoutWidth">
                            ����ϵͳ
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbSystem" runat="server" Text=""></asp:Label>
                        </td>--%>
                        
                        <td class="table_body_WithoutWidth">
                                        ����ԭ��
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbMalReason" runat="server"></asp:Label>
                        </td>
                        
                        
                        <td class="table_body_WithoutWidth">
                            ά�޵�λ
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbMaintainTeam" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth" runat="server" id="MaintainPlanRankTitle">
                            ���ϵȼ�
                        </td>
                        <td class="table_none_WithoutWidth" runat="server" id="MaintainPlanRankContent">
                            <asp:Label ID="lbMalfunctionRank" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <%--<tr runat="server" id="MaintainPlanTime">
                        <td class="table_body_WithoutWidth">
                            ��Ӧʱ��
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbResponseTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            �����Իָ�ʱ��
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbFunRestoreTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            �޸�ʱ��
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbRepairTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���ϼ�¼���ţ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbRecordDept" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���ϼ�¼�ˣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbRecorder" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="height: 30px;" colspan="6">
                            ά������Ǽ�
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            ά�޵�λ��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                            <asp:Label ID="lbMaintainTeamx" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            �����ˣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            <asp:Label ID="lbReceiver" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            �������ڣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            <asp:Label ID="lbReceiveDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            ʵ����Ӧʱ�䣺
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                            <asp:Label ID="lbActResponseTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            &nbsp;</td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            ���Ϻ�ʱ��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            <asp:Label ID="lbActRepairTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            ά����Ա�б�
                        </td>
                        <td colspan="5">
                            <asp:Label ID="lbMaintainStaffList" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptMaintainHistory" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td rowspan="2" class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                                    <%#Eval("UpdateTime", "{0:yyyy-MM-dd  HH:mm}")%>
                                </td>
                                <td style="padding-left: 20px; padding-top: 8px;" colspan="5">
                                    ά��ʱ�䣺<%#Eval("UpdateTime", "{0:yyyy-MM-dd}")%>&nbsp;&nbsp;&nbsp;&nbsp; ά�޵�λ��<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;ά�޼�¼�ˣ�<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                    ά���ܷ��ã�<%#Eval("TotalFee","{0:#,0.#}")%>Ԫ&nbsp;&nbsp;&nbsp;&nbsp; �޸������<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;�Ƿ����ޣ�<%#Convert.ToBoolean(Eval("IsDelivered"))?"��":"��"%><br />
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
                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                                style="border-collapse: collapse;">
                                                <tr>
                                                    <td colspan="7" align="center" style="background-color: #EFEFEF; font-weight: bold;">
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
                                                    </td>
                                                    <td align="center" style="width: 6%">
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
                                            <tr>
                                                <td align="center">
                                                    <%#Eval("EquipmentNO") %>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("EquipmentName") %>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("Model") %>
                                                </td>
                                                <td align="center" style="color:Red">
                                                    <%#Eval("SerialNum") %>
                                                </td>
                                                <td align="center">
                                                    <%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %>
                                                </td>
                                                <td align="center" style="color:Red">
                                                    <%# Eval("LastAddress") %>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("Remark")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" align="center" style="background-color: #EFEFEF; font-weight: bold;">�������豸</td>
                                                <td colspan="4">
                                                    <asp:Repeater ID="rptHistoryEquipmentsParts" runat="server" DataSource='<%# Eval("MaintainedEquipmentParts") %>'
                                                        Visible='<%#((IList)Eval("MaintainedEquipmentParts")).Count>0?true:false%>'>
                                                        <HeaderTemplate>
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
                                                                <tr>
                                                                    <td align="center" style="width: 50%">
                                                                        �������
                                                                    </td>
                                                                    <td align="center" style="width: 50%">
                                                                        �������
                                                                    </td>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center">
                                                                    <%#Eval("PartName")%>
                                                                </td>
                                                                <td align="center">
                                                                    <%#Eval("MaintainFee")%>
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
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr >
                            
                            <td class="Table_searchtitle" colspan="10"  >�豸������ͳ��</td>
                        </tr>
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth"  style="text-align:center; width: 3%"  >���</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 10%"  >����</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 10%"  >�ͺ�</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 5%"  >��λ</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 5%"  >����</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 23%"  >��ע</td>
                           
                            
                        </tr>
                        
                        <asp:Repeater ID="rpEquipmentItems"  runat="server" 
                            >                          
                        
                            <ItemTemplate>
                            <tr >
                                <td class="table_body_WithoutWidth"><%# (Container.ItemIndex+1)%></td>
                                <td class="table_none_WithoutWidth"><asp:label runat="server" id="lbEqName"><%#Eval("EqName") %></asp:label></td>
                                <td class="table_none_WithoutWidth"><asp:label runat="server" id="lbEqModel"><%#Eval("EqModel") %></asp:label></td>
                                <td class="table_none_WithoutWidth"><asp:label runat="server" id="lbEqUnit"><%#Eval("EqUnit") %></asp:label></td>
                                <td class="table_none_WithoutWidth"><asp:label runat="server" id="lbEqNum"><%#Eval("EqNum") %></asp:label></td>
                                <td class="table_none_WithoutWidth"><asp:label runat="server" id="lbEqRemark"><%#Eval("EqRemark") %></asp:label></td>
                                
                                
                                </tr></ItemTemplate></asp:Repeater>
                    
                    <tr >
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���ϴ���״̬��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
                            &nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �������ʱ�䣺
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbUpdateTime" runat="server" Text="" ForeColor="Red"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    
                    
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
                    
                    <%-- By L 4-27 ����ȵ�������****************************************************************************************************
                    
                    <tr  style=" display:none;">
                        <td class="Table_searchtitle" colspan="6">
                            ���ϴ�������ȵ���
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ά��ʱ�����ۣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:CheckBox ID="cbIsResponseInTime" runat="server" Text="��Ӧ��ʱ" />
                            <asp:CheckBox ID="cbIsFunRestoreInTime" runat="server" Text="���ָܻ���ʱ" />
                            <asp:CheckBox ID="cbIsRepairInTime" runat="server" Text="�޸���ʱ" />
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ����Ч����
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblEffect" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �������ۣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblTechnicEvaluate" runat="server" RepeatDirection="Horizontal"
                                Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ����̬�ȣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblAttitude" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ������ϵĺ����ԣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblRationality" runat="server" RepeatDirection="Horizontal"
                                Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ʹ�ò��������
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
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
                    
                    
                    <tr id="trCancelReason" runat="server" visible="false">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ����ԭ��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbCancelReason" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �����ˣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbCanceler" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Repeater ID="rptModifyHistory" runat="server" Visible="false">
                    <HeaderTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" style="height: 30px" colspan="2">
                                    ���ϵ��޸���ʷ
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="height: 30px; text-align: center" class="table_none_WithoutWidth">
                                <%#Eval("ModifyDate","{0:yyyy-MM-dd HH:mm}") %><br />
                                <%#Eval("ModifierName") %>
                            </td>
                            <td style="height: 30px; text-align: left" class="table_none_WithoutWidth">
                                <%#Eval("ModifyDescription")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div id="divUndo" runat="server" visible="false">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                �������ϴ���
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ����ԭ��
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                <asp:TextBox ID="tbCancelReason" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine"
                                    Rows="3" title="�����볷��ԭ��~200:!"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btUndo" runat="server" Text="�������ϵ�" CssClass="button_bak2" OnClick="btUndo_Click"
                                    OnClientClick="javascript:return checkForm(document.forms[0],true)&&confirm('ȷ��Ҫ�����˹��ϵ���');" />&nbsp;&nbsp;
                                <asp:Button ID="btClose" runat="server" Text="ȡ��" CssClass="button_bak" OnClick="btClose_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="btUndoMode" runat="server" Text="Button" OnClick="btUndoMode_Click"
                    Style="display: none" />
                    
                
                <div id="CheckMaintainDIV" style="text-align:center; padding:15px;">
                
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
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
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyCancelName" runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                ���볷��˵����
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyCancelReason"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                ��������˵����
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                <asp:TextBox ID="tbCheckInfor" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine"
                                    Rows="3" title="���������˵��~200:!"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                    </table>
                
                    <asp:Button ID="btCheckPass" runat="server" Text="ͬ�⳷��" CssClass="button_bak2" 
                        onclick="btCheckPass_Click" OnClientClick="javascript:return checkForm(document.forms[0],true)&& confirm('ȷ��ͬ�⳷����');" />
                    &nbsp;
                    <asp:Button ID="btCheckFailed" runat="server" Text="��ͬ�⳷��" 
                        CssClass="button_bak2" onclick="btCheckFailed_Click" OnClientClick="javascript:return checkForm(document.forms[0],true)&& confirm('ȷ����ͬ�⳷���������޸���');" />
                </div>
                    
                    
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" language="javascript">
        document.getElementById("<%= tbCancelReason.ClientID %>").focus();
    </script>
</asp:Content>

