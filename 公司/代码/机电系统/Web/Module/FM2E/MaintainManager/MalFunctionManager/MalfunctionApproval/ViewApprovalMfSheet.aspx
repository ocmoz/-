<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageNoCheck.master"
    AutoEventWireup="true" CodeFile="ViewApprovalMfSheet.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionApproval_ViewApprovalMfSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="���ϴ����鿴"
        HeadHelpTxt="����" HeadOPTxt="Ŀǰ�������ܣ����ϴ����鿴">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="�༭" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="����" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="���ϵ��б�" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="��ӡ" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="text-align: center;">
        <asp:UpdatePanel ID="upMain" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;
                    position: inherit; z-index: inherit;">
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
                        <td class="table_body_WithoutWidth">
                            �����豸������
                        </td>
                        <td class="table_none_WithoutWidth" colspan="1">
                            <asp:Label ID="lbEqNo" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            �����豸����
                        </td>
                        <td class="table_none_WithoutWidth" colspan="1">
                            <asp:Label ID="lbEqName" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            ����ϵͳ
                        </td>
                        <td class="table_none_WithoutWidth" colspan="1">
                            <asp:Label ID="lbEqSystem" runat="server" Text=""></asp:Label>
                        </td>
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
                    <%--���ϼ�¼�˸�Ϊ���ϵ������¼--%>
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
                            ȷ���޸�ʱ�䣺
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                            <asp:Label ID="lbConTime" runat="server" Text=""></asp:Label>
                        </td>
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
                        <td colspan="5" class="table_none_WithoutWidth ">
                            <asp:Label ID="lbMaintainStaffList" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptMaintainHistory" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td rowspan="2" class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                                    <%#Eval("UpdateTime", "{0:yyyy-MM-dd  HH:mm}")%>
                                </td>
                                <td style="padding-left: 20px; padding-top: 8px;" colspan="5" class="table_none_WithoutWidth ">
                                    ά�޵�λ��<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;ά�޼�¼�ˣ�<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                    �޸������<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;�Ƿ����ޣ�<%#Convert.ToBoolean(Eval("IsDelivered"))?"��":"��"%><br />
                                    ������ϸ������<%#Eval("MaintenanceDescription")%><br />
                                    ���ϴ���취��<%#Eval("MaintenanceMethod")%><br />
                                    ���豸���ϣ�<%#Eval("NoEquipment")%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Repeater ID="rptHistoryEquipments" runat="server" DataSource='<%# Eval("MaintainedEquipments") %>'
                                        OnItemDataBound="rptHistoryEquipments_ItemDataBound" Visible='<%#((IList)Eval("MaintainedEquipments")).Count>0?true:false%>'>
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
                                                    <td align="center">
                                                        ά�����
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center">
                                                    <%-- 
                                                    <%#Eval("EquipmentNO") %>
                                                --%>
                                                    <asp:Literal ID="lbEqNO" runat="Server" Text=""></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("EquipmentName") %>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("Model") %>
                                                </td>
                                                <td align="center" style="color: Red">
                                                    <%#Eval("SerialNum") %>
                                                </td>
                                                <td align="center">
                                                    <%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %>
                                                </td>
                                                <td align="center" style="color: Red">
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
                                            <tr>
                                                <td colspan="7" align="center" style="background-color: #EFEFEF; font-weight: bold;">
                                                    �����б�
                                                </td>
                                                <td colspan="4">
                                                    <asp:Repeater ID="rptHistoryEquipmentsParts" runat="server" DataSource='<%# Eval("MaintainedEquipmentParts") %>'
                                                        Visible='<%#((IList)Eval("MaintainedEquipmentParts")).Count>0?true:false%>'>
                                                        <HeaderTemplate>
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                                                style="border-collapse: collapse;">
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
                                    &nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%>
                                    <br />
                                    &nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("CheckDate")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                        <%--<FooterTemplate></tr></FooterTemplate>--%></asp:Repeater>
                    <tr id="EqCostHeader" runat="server">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���Ϸ���ͳ�Ʊ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; text-align: left;" colspan="5">
                            <span id="CloseSpan" style="cursor: pointer; color: Blue; font-weight: bold; width: 100%;
                                display: block; margin-left: 20px;" onclick="javascript:CollapseOrExpand();">--�۵�</span>
                        </td>
                    </tr>
                    <tr id="EqCostDisplayTR">
                        <td colspan="6">
                            <asp:Panel ID="pnMoneyStatistics" runat="server" HorizontalAlign="Center">
                                <asp:UpdatePanel ID="UpdatePanel_MoneyStatistics" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                            style="border-collapse: collapse;">
                                            <%--<tr >
                            <td id="Td1" class="Table_searchtitle" style="height: 30px;" colspan="9" runat="server">
                                ���Ϸ���ͳ�Ʊ�
                            </td>
                        </tr>--%>
                                            <tr>
                                                <td class="table_body_WithoutWidth" style="font-weight: bold; height: 31px;">
                                                    һ
                                                </td>
                                                <td class="table_body_WithoutWidth" colspan="10" style="height: 31px">
                                                    �豸�Ѽ����Ϸ�
                                                </td>
                                            </tr>
                                            <tr style="font-weight: bold;">
                                                <td class="table_body_WithoutWidth" style="width: 3%">
                                                    ���
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    ����
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    �ͺ�
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 5%">
                                                    ��λ
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 5%">
                                                    ����
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 8%">
                                                    �ۺϵ���
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    �ϼ�
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 8%">
                                                    ����ۺϵ���
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    ��˺ϼ�
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 23%">
                                                    ��ע
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 2%">
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rpEquipmentItems" runat="server" OnItemCommand="rpEquipmentItems_ItemCommand">
                                                <ItemTemplate runat="Server">
                                                    <tr>
                                                        <td class="table_body_WithoutWidth">
                                                            <%# (Container.ItemIndex+1)%>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqName"><%#Eval("EqName") %></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqName" Text='<%#Eval("EqName")%>' Width="80%"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqModel"><%#Eval("EqModel") %></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqModel" Text='<%#Eval("EqModel")%>' Width="80%"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqUnit"><%#Eval("EqUnit") %></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqUnit" Text='<%#Eval("EqUnit")%>' Width="80%"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqNum"><%#Eval("EqNum") %></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqNum" Text='<%#Eval("EqNum")%>' Width="80%"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqSinglePrice"><%#Eval("EqUnitPrice") %></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqSinglePrice" Text='<%#Eval("EqUnitPrice")%>' Width="80%"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqSumPrice"><%#Eval("EqTotalPrice") %></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqSumPrice" Text='<%#Eval("EqTotalPrice")%>' Width="80%"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqApprovalUnitPrice"><%#Eval("EqApprovalUnitPrice")%></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqApprovalUnitPrice" Text='<%#Eval("EqApprovalUnitPrice")%>'
                                                                Width="80%" OnTextChanged="tbMeasureCost_TextChanged2"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqApprovalTotalPrice"><%#Eval("EqApprovalTotalPrice")%></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqApprovalTotalPrice" Text='<%#Eval("EqApprovalTotalPrice")%>'
                                                                Width="80%" OnTextChanged="tbMeasureCost_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:Label runat="server" ID="lbEqRemark"><%#Eval("EqRemark") %></asp:Label><asp:TextBox
                                                                runat="server" ID="tbEqRemark" Text='<%#Eval("EqRemark")%>' Width="97%"></asp:TextBox>
                                                        </td>
                                                        <td class="table_none_WithoutWidth">
                                                            <asp:ImageButton ID="ibDelEqItems" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                                CommandArgument="<%# Container.ItemIndex %>" CommandName="delEq" OnClientClick="return confirm('ȷ��Ҫɾ�����豸������')"
                                                                CausesValidation="false" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="inputEqItemsTR">
                                                <td class="table_body_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:TextBox ID="tbEqName" runat="server" Width="70"></asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:TextBox ID="tbEqModel" runat="server" Width="70"></asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:TextBox ID="tbEqUnit" runat="server" MaxLength="200" Width="70"></asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:TextBox ID="tbEqNum" runat="server" MaxLength="200" Width="70" onBlur="javascript:tpblurtbEqNum(this.id);">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:TextBox ID="tbEqSinglePrice" runat="server" MaxLength="200" Width="70" onBlur="javascript:tpblurtbEqSinglePrice(this.id);">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:TextBox ID="tbEqTotalPrice" runat="server" MaxLength="200" Width="70">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Button runat="server" ID="btAddEquipmentItems" CssClass="button_bak" Text="���"
                                                        OnClick="btAddEquipmentItems_Click1" />
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:TextBox ID="tbEqRemark" runat="server" MaxLength="200" Width="120"></asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                            </tr>
                                            <tr style="font-weight: bold;">
                                                <td class="table_body_WithoutWidth">
                                                    N
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5">
                                                    С��
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbSumTotal" runat="server">0</asp:Label>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalSumTotal" runat="server">0</asp:Label>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body_WithoutWidth" style="font-weight: bold;">
                                                    ��
                                                </td>
                                                <td class="table_body_WithoutWidth" colspan="10">
                                                    ��������
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body_WithoutWidth">
                                                    1
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5" style="font-weight: bold;">
                                                    ��ʩ��
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbMeasureCost" runat="server">0</asp:Label><asp:TextBox ID="tbMeasureCost"
                                                        runat="server" MaxLength="200" title="�۸������ʽ����ȷ~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalMeasureCost" runat="server">0</asp:Label>
                                                    <asp:TextBox runat="server" ID="tbApprovalMeasureCost" Text='<%#Eval("EqApprovalUnitPrice")%>'
                                                        title="�۸������ʽ����ȷ~int!" Width="80px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="2">
                                                    <asp:Label ID="lbMarkOne" runat="server"></asp:Label>
                                                    <asp:TextBox runat="server" ID="tbMarkOne" Text='<%#Eval("EqMarkOne")%>' Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body_WithoutWidth">
                                                    2
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5" style="font-weight: bold;">
                                                    ���
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbGuiCost" runat="server">0</asp:Label><asp:TextBox ID="tbGuiCost"
                                                        runat="server" MaxLength="200" title="�۸������ʽ����ȷ~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalGuiCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalGuiCost" Text='<%#Eval("EqApprovalUnitPrice")%>' title="�۸������ʽ����ȷ~float!"
                                                        Width="80px" OnTextChanged="tbMeasureCost_TextChanged" AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="2">
                                                    <asp:Label ID="lbMarkTwo" runat="server"></asp:Label>
                                                    <asp:TextBox runat="server" ID="tbMarkTwo" Text='<%#Eval("EqMarkTwo")%>' Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body_WithoutWidth">
                                                    3
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5" style="font-weight: bold;">
                                                    ˰��
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbTaxCost" runat="server">0</asp:Label><asp:TextBox ID="tbTaxCost"
                                                        runat="server" MaxLength="200" title="�۸������ʽ����ȷ~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="1">
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="1">
                                                    <asp:Label ID="lbApprovalTaxCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalTaxCost" Text='<%#Eval("EqApprovalUnitPrice")%>' title="�۸������ʽ����ȷ~float!"
                                                        Width="80px" OnTextChanged="tbMeasureCost_TextChanged" AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="2">
                                                    <asp:Label ID="lbMarkThree" runat="server"></asp:Label>
                                                    <asp:TextBox runat="server" ID="tbMarkThree" Text='<%#Eval("EqMarkThree")%>' Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body_WithoutWidth">
                                                    4
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5" style="font-weight: bold;">
                                                    ��ͨ��
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbTrafficCost" runat="server">0</asp:Label><asp:TextBox ID="tbTrafficCost"
                                                        runat="server" MaxLength="200" title="�۸������ʽ����ȷ~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalTrafficeCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalTrafficeCost" Text='<%#Eval("EqApprovalUnitPrice")%>' title="�۸������ʽ����ȷ~float!"
                                                        Width="80px" OnTextChanged="tbMeasureCost_TextChanged" AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="2">
                                                    <asp:Label ID="lbMarkFour" runat="server"></asp:Label>
                                                    <asp:TextBox runat="server" ID="tbMarkFour" Text='<%#Eval("EqMarkFour")%>' Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body_WithoutWidth">
                                                    5
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5" style="font-weight: bold;">
                                                    ��������
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbOtherCost" runat="server">0</asp:Label><asp:TextBox ID="tbOtherCost"
                                                        runat="server" MaxLength="200" title="�۸������ʽ����ȷ~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalOtherCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalOtherCost" Text='<%#Eval("EqApprovalOtherCost")%>' title="�۸������ʽ����ȷ~float!"
                                                        Width="80px" OnTextChanged="tbMeasureCost_TextChanged" AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="2">
                                                    <asp:Label ID="lbMarkFive" runat="server"></asp:Label>
                                                    <asp:TextBox runat="server" ID="tbMarkFive" Text='<%#Eval("EqMarkFive")%>' Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="font-weight: bold;">
                                                <td class="table_body_WithoutWidth">
                                                    N
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5">
                                                    С��
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbSumOther" runat="server">0</asp:Label>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalSumOther" runat="server">0</asp:Label>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="2">
                                                </td>
                                            </tr>
                                            <tr style="font-weight: bold;">
                                                <td class="table_body_WithoutWidth" style="height: 20px">
                                                    ��
                                                </td>
                                                <td class="table_body_WithoutWidth" colspan="5" style="height: 20px">
                                                    �ϼ�
                                                </td>
                                                <td style="font-size: 9pt; background: #efefef; height: 20px; padding-left: 8px;
                                                    padding-right: 5px; font-family: 'Verdana', 'Arial', 'Helvetica', 'sans-serif';
                                                    text-align: left;">
                                                    <asp:Label ID="lbSumAll" runat="server">0</asp:Label>
                                                </td>
                                                <td class="table_body_WithoutWidth" style="height: 20px">
                                                </td>
                                                <td style="font-size: 9pt; background: #efefef; height: 20px; padding-left: 8px;
                                                    padding-right: 5px; font-family: 'Verdana', 'Arial', 'Helvetica', 'sans-serif';
                                                    text-align: left;">
                                                    <asp:Label ID="lbApprovalSumAll" runat="server">0</asp:Label>
                                                </td>
                                                <td class="table_body_WithoutWidth" style="height: 20px" colspan="2">
                                                    <asp:CheckBox ID="checkbox1" runat="Server" Text="" AutoPostBack="true" OnCheckedChanged="Check">
                                                    </asp:CheckBox><asp:Label runat="server" ID="jiliang" ForeColor="Blue">��Ҫ����</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr id="DelayDisplay" runat="server">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �������룺
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbshenqingjiliang" runat="server" Text="" ForeColor="Blue"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �Ƿ������
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="jilianga" GroupName="rb1" runat="server" Text="��" ForeColor="Red" />
                            <asp:RadioButton ID="jiliangb" GroupName="rb1" runat="server" Text="��" ForeColor="Red" />
                            <asp:Label ID="lbjiliang" Text="" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �Ƿ�׹���
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="jiagonga" GroupName="rb2" runat="server" Text="��" ForeColor="Red" />
                            <asp:RadioButton ID="jiagongb" GroupName="rb2" runat="server" Text="��" ForeColor="Red" />
                            <asp:Label ID="lbjiagong" Text="" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���ϴ���״̬��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbStatus" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �������ʱ�䣺
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbUpdateTime" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <%--<tr ID = "DelayDisplay" runat="server">
                        <td class="table_body_WithoutWidth " style="height: 30px;" >
                            ��ʱ���룺
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="DelayTime" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ����ʦ��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="DelayCheck1a" GroupName="rb1" runat="server" Text="ͨ��" ForeColor=Red/>
                            <asp:RadioButton ID="DelayCheck1b" GroupName="rb1" runat="server" Text="��ͨ��" ForeColor=Red/>
                            <asp:Label ID="DelayCheck1pass" Text="ͨ��" ForeColor="Blue" runat="server"></asp:Label>
                            <asp:Label ID="DelayCheck1dispass" Text="��ͨ��" ForeColor="Blue" runat="server"></asp:Label>
                            
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �߼�����
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="DelayCheck2a" GroupName="rb1" runat="server" Text="ͨ��"  ForeColor=Red/>
                            <asp:RadioButton ID="DelayCheck2b" GroupName="rb1" runat="server" Text="��ͨ��"  ForeColor=Red/>
                            <asp:Label ID="DelayCheck2pass" Text="ͨ��" ForeColor="Blue" runat="server"></asp:Label>
                            <asp:Label ID="DelayCheck2dispass" Text="��ͨ��" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                        
                    </tr>--%>
                    <tr runat="server" id="ScrapChoose">
                        <td class="table_body_WithoutWidth">
                            �������豸���飺
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Literal ID="ScrapEquipmentNo" runat="server"></asp:Literal>
                        </td>
                        <td class="table_body_WithoutWidth">
                            �Ƿ񱨷ϣ�
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:RadioButton ID="ScrapButton" GroupName="Status" runat="server" Text="��Ҫ����" ForeColor="Red" />
                            <asp:RadioButton ID="RadioButton1" GroupName="Status" runat="server" Text="��" ForeColor="Red" />
                        </td>
                    </tr>
                    <%--*********************************************���ι��ϴ�������ȵ���4-27--%>
                    <%-- 
                    <tr style=" display:none;">
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
                            <asp:Label ID="lbFeeBackOpinion" runat="server" Text=""></asp:Label></td></tr><tr >
                        <td class="Table_searchtitle" colspan="6">
                            �����������
                        </td>
                    </tr>
                    --%>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ������¼��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:HyperLink ID="HyperLink_File" ForeColor="Blue" Font-Underline="true" runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���ϵ�������¼��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <%-- <HeaderTemplate><table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;"></HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;<%#Eval("UserDeptName")%>���������</td>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;<%#Eval("EvenName")%></td>&nbsp;&nbsp;
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%></td>>
                                    <td colspan="3" bordercolor="#ffffff" style=" border-bottom-color:#cccccc">&nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("ApprovalDate")%>&nbsp;</td></tr>
                                </ItemTemplate>--%>
                                <HeaderTemplate>
                                    <table border="0" width="100%">
                                        <tr>
                                            <th>
                                                ��������
                                            </th>
                                            <th>
                                                �������
                                            </th>
                                            <th>
                                                �������
                                            </th>
                                            <th>
                                                ְλ
                                            </th>
                                            <th>
                                                �û���
                                            </th>
                                            <th>
                                                ��������
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr align="center">
                                        <td>
                                            <%#Eval("UserDeptName")%>
                                        </td>
                                        <td>
                                            <%#Eval("EvenName")%>
                                        </td>
                                        <td>
                                            <%#Eval("Remark")%>
                                        </td>
                                        <td>
                                            <%#Eval("UserPsnName")%>
                                        </td>
                                        <td>
                                            <%#Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ApprovalDate")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table ></FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ���������
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:TextBox ID="tbApprovalRemark" runat="server" Height="70px" MaxLength="200" TextMode="MultiLine"
                                Width="414px"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr id="trCancelReason" runat="server" visible="false">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            ����ԭ��
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbCancelReason" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            �����ˣ�
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbCanceler" runat="server" Text=""></asp:Label>&nbsp;
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
                            <td class="Table_searchtitle" colspan="2">
                                �������ϴ���
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ����ԭ��
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbCancelReason" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine"
                                    Rows="3" title="�����볷��ԭ��~200:!"></asp:TextBox><span style="color: Red">*</span>
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
                <div id="ApprovalDiv" style="text-align: center; padding: 10px 10px 20px 10px;">
                    <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="�� ��" CssClass="button_bak" OnClientClick="javascript:return confirm('ȷ���ύ������');"
                        OnClick="Button1_Click" Height="20px" />
                    <asp:RadioButton ID="rdoWanghai" GroupName="rb1" runat="server" Text="����" ForeColor="Red" />
                    <asp:RadioButton ID="rdoHuangLiang" GroupName="rb1" runat="server" Text="����" ForeColor="Red" />
                </div>
                <asp:Button ID="btUndoMode" runat="server" Text="Button" OnClick="btUndoMode_Click"
                    Style="display: none" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="attachmentId" runat="Server" visible="false">
            <table width="100%">
                <tr>
                    <td class="table_body_WithoutWidth " style="height: 30px;">
                        �ϴ�������
                    </td>
                    <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                        <asp:FileUpload ID="FileUpload_ArchivesAttachmentFile" runat="server"></asp:FileUpload>
                        <asp:Button ID="Button2" runat="server" Text="�ϴ�" CssClass="button_bak" OnClick="uploadfile_Click" />
                        <asp:Label ID="uploadname" runat="server" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        document.getElementById("<%= tbCancelReason.ClientID %>").focus();


        function CollapseOrExpand() {
            var obj = $get("CloseSpan");
            if (obj == null)
                return;

            if (obj.innerText == "--�۵�") {
                $get("EqCostDisplayTR").style.display = "none";
                obj.innerText = "+չ��";
            } else if (obj.innerText == "+չ��") {
                $get("EqCostDisplayTR").style.display = "inline";
                obj.innerText = "--�۵�";
            }
        }

        var inputval = 0;
        var SumTotal = Number(document.getElementById("<%= lbApprovalSumTotal.ClientID %>").innerText);
        var SumOther = Number(document.getElementById("<%= lbApprovalSumOther.ClientID %>").innerText);
        document.getElementById("<%= lbApprovalSumAll.ClientID %>").innerText = SumTotal + SumOther;

        function tpfocuslbApprovalSumTotal(id) {
            inputval = document.getElementById(id).value;
        }
        function tpblurlbApprovalSumTotal(id) {
            var updateval = document.getElementById(id).value;
            updateval = Number(document.getElementById("<%= lbApprovalSumTotal.ClientID %>").innerText) + Number(updateval) - Number(inputval);
            document.getElementById("<%= lbApprovalSumTotal.ClientID %>").innerText = updateval;

            SumTotal = updateval;
            //SumOther = Number(document.getElementById("<%= lbApprovalSumOther.ClientID %>").innerText);

            document.getElementById("<%= lbApprovalSumAll.ClientID %>").innerText = updateval + SumOther;

        }

        function tpfocuslbApprovalSumOther(id) {
            inputval = document.getElementById(id).value;
        }
        function tpblurlbApprovalSumOther(id) {
            var updateval = document.getElementById(id).value;
            updateval = Number(document.getElementById("<%= lbApprovalSumOther.ClientID %>").innerText) + Number(updateval) - Number(inputval);
            document.getElementById("<%= lbApprovalSumOther.ClientID %>").innerText = updateval;

            //SumOther = updateval;
            //SumTotal = Number(document.getElementById("<%= lbApprovalSumTotal.ClientID %>").innerText);

            document.getElementById("<%= lbApprovalSumAll.ClientID %>").innerText = updateval + SumTotal;
        }

        var num_1 = 0, price_1 = 0, sum_1 = 0;
        function tpblurtbEqSinglePrice(id) {
            num_1 = document.getElementById(id).value;

            sum_1 = price_1 * num_1;
            document.getElementById("<%= tbEqTotalPrice.ClientID %>").innerText = sum_1;
        }
        function tpblurtbEqNum(id) {
            price_1 = document.getElementById(id).value;

            sum_1 = price_1 * num_1;
            document.getElementById("<%= tbEqTotalPrice.ClientID %>").innerText = sum_1;
        }

    </script>

</asp:Content>
