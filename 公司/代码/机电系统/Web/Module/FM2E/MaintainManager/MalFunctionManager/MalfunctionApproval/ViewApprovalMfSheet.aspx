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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="故障处理单查看"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：故障处理单查看">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="撤单" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="故障单列表" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="打印" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="text-align: center;">
        <asp:UpdatePanel ID="upMain" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;
                    position: inherit; z-index: inherit;">
                    <tr>
                        <td colspan="4" rowspan="2" class="table_body_WithoutWidth">
                            <b style="font-family: 宋体; font-size: medium">
                                <asp:Label ID="lbCompany" runat="server" Text="XX"></asp:Label>
                                维修处理单</b>
                        </td>
                        <td colspan="2" class="table_body_WithoutWidth">
                            表单编号
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Label ID="lbSheetNO" runat="server" Text="" Style=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="Table_searchtitle" style="height: 30px">
                            报修情况
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            报修单位
                        </td>
                        <td style="width: 18%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbDepartment" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            报修人
                        </td>
                        <td style="width: 17%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbReporter" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 16%">
                            日期
                        </td>
                        <td style="width: 17%" class="table_none_WithoutWidth">
                            <asp:Label ID="lbReportTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td class="table_body_WithoutWidth">
                            故障设备
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5" valign="middle">
                            <br />
                            <asp:Repeater ID="repeatEquipments" runat="server">
                                <ItemTemplate>
                                    <%#Container.ItemIndex+1 %>、设备名称：&nbsp;<%#Eval("EquipmentNAME") %>&nbsp;<%#!string.IsNullOrEmpty(Convert.ToString(Eval("EquipmentNO")))?"("+Eval("EquipmentNO")+")":"" %><br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="table_body_WithoutWidth">
                            故障设备条形码
                        </td>
                        <td class="table_none_WithoutWidth" colspan="1">
                            <asp:Label ID="lbEqNo" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            故障设备名称
                        </td>
                        <td class="table_none_WithoutWidth" colspan="1">
                            <asp:Label ID="lbEqName" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            所属系统
                        </td>
                        <td class="table_none_WithoutWidth" colspan="1">
                            <asp:Label ID="lbEqSystem" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth">
                            故障设备地址
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="table_body_WithoutWidth">
                            地址详细描述
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:Label ID="lbAddressDetail" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="table_body_WithoutWidth">
                            故障描述
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td class="table_body_WithoutWidth">
                            故障系统
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbSystem" runat="server" Text=""></asp:Label>
                        </td>--%>
                        <td class="table_body_WithoutWidth">
                            故障原因
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbMalReason" runat="server"></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            维修单位
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbMaintainTeam" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth" runat="server" id="MaintainPlanRankTitle">
                            故障等级
                        </td>
                        <td class="table_none_WithoutWidth" runat="server" id="MaintainPlanRankContent">
                            <asp:Label ID="lbMalfunctionRank" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <%--<tr runat="server" id="MaintainPlanTime">
                        <td class="table_body_WithoutWidth">
                            响应时间
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbResponseTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            功能性恢复时间
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbFunRestoreTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth">
                            修复时间
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Label ID="lbRepairTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            故障记录部门：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbRecordDept" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            故障记录人：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbRecorder" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <%--故障记录人改为故障单处理记录--%>
                    <tr>
                        <td class="Table_searchtitle" style="height: 30px;" colspan="6">
                            维修情况登记
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            维修单位：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                            <asp:Label ID="lbMaintainTeamx" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            受理人：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            <asp:Label ID="lbReceiver" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            受理日期：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            <asp:Label ID="lbReceiveDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            实际响应时间：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                            <asp:Label ID="lbActResponseTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            确认修复时间：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                            <asp:Label ID="lbConTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            排障耗时：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            <asp:Label ID="lbActRepairTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                            维修人员列表
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
                                    维修单位：<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;维修记录人：<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                    修复情况：<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;是否送修：<%#Convert.ToBoolean(Eval("IsDelivered"))?"是":"否"%><br />
                                    故障详细描述：<%#Eval("MaintenanceDescription")%><br />
                                    故障处理办法：<%#Eval("MaintenanceMethod")%><br />
                                    非设备故障：<%#Eval("NoEquipment")%>
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
                                                        经过维修的设备
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        设备条形码
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        设备名称
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        型号
                                                    </td>
                                                    <td align="center" style="width: 6%">
                                                        设备类型
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        维修结果
                                                    </td>
                                                    <td align="center" style="width: 23%">
                                                        设备跟踪
                                                    </td>
                                                    <td align="center">
                                                        维修情况
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
                                                    更换列表
                                                </td>
                                                <td colspan="4">
                                                    <asp:Repeater ID="rptHistoryEquipmentsParts" runat="server" DataSource='<%# Eval("MaintainedEquipmentParts") %>'
                                                        Visible='<%#((IList)Eval("MaintainedEquipmentParts")).Count>0?true:false%>'>
                                                        <HeaderTemplate>
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                                                style="border-collapse: collapse;">
                                                                <tr>
                                                                    <td align="center" style="width: 50%">
                                                                        零件名称
                                                                    </td>
                                                                    <td align="center" style="width: 50%">
                                                                        零件费用
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
                            故障验收情况
                        </td>
                    </tr>
                    <asp:Repeater ID="Repeater4Check" runat="server">
                        <%--<HeaderTemplate><tr></HeaderTemplate>--%>
                        <ItemTemplate>
                            <tr>
                                <td class="table_body_WithoutWidth " style="height: 30px;">
                                    确认时间：<%#Eval("CheckDate")%>
                                </td>
                                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                                    &nbsp;&nbsp;<%#Eval("UserDeptName")%>验收情况说明：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%>
                                    <br />
                                    &nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("CheckDate")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                        <%--<FooterTemplate></tr></FooterTemplate>--%></asp:Repeater>
                    <tr id="EqCostHeader" runat="server">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            故障费用统计表
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; text-align: left;" colspan="5">
                            <span id="CloseSpan" style="cursor: pointer; color: Blue; font-weight: bold; width: 100%;
                                display: block; margin-left: 20px;" onclick="javascript:CollapseOrExpand();">--折叠</span>
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
                                故障费用统计表
                            </td>
                        </tr>--%>
                                            <tr>
                                                <td class="table_body_WithoutWidth" style="font-weight: bold; height: 31px;">
                                                    一
                                                </td>
                                                <td class="table_body_WithoutWidth" colspan="10" style="height: 31px">
                                                    设备费及材料费
                                                </td>
                                            </tr>
                                            <tr style="font-weight: bold;">
                                                <td class="table_body_WithoutWidth" style="width: 3%">
                                                    序号
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    名称
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    型号
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 5%">
                                                    单位
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 5%">
                                                    数量
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 8%">
                                                    综合单价
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    合价
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 8%">
                                                    审核综合单价
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 10%">
                                                    审核合价
                                                </td>
                                                <td class="table_none_WithoutWidth" style="width: 23%">
                                                    备注
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
                                                                CommandArgument="<%# Container.ItemIndex %>" CommandName="delEq" OnClientClick="return confirm('确认要删除此设备费用吗？')"
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
                                                    <asp:Button runat="server" ID="btAddEquipmentItems" CssClass="button_bak" Text="添加"
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
                                                    小计
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
                                                    二
                                                </td>
                                                <td class="table_body_WithoutWidth" colspan="10">
                                                    其他费用
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body_WithoutWidth">
                                                    1
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="5" style="font-weight: bold;">
                                                    措施费
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbMeasureCost" runat="server">0</asp:Label><asp:TextBox ID="tbMeasureCost"
                                                        runat="server" MaxLength="200" title="价格输入格式不正确~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalMeasureCost" runat="server">0</asp:Label>
                                                    <asp:TextBox runat="server" ID="tbApprovalMeasureCost" Text='<%#Eval("EqApprovalUnitPrice")%>'
                                                        title="价格输入格式不正确~int!" Width="80px" OnTextChanged="tbMeasureCost_TextChanged"
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
                                                    规费
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbGuiCost" runat="server">0</asp:Label><asp:TextBox ID="tbGuiCost"
                                                        runat="server" MaxLength="200" title="价格输入格式不正确~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalGuiCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalGuiCost" Text='<%#Eval("EqApprovalUnitPrice")%>' title="价格输入格式不正确~float!"
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
                                                    税金
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbTaxCost" runat="server">0</asp:Label><asp:TextBox ID="tbTaxCost"
                                                        runat="server" MaxLength="200" title="价格输入格式不正确~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="1">
                                                </td>
                                                <td class="table_none_WithoutWidth" colspan="1">
                                                    <asp:Label ID="lbApprovalTaxCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalTaxCost" Text='<%#Eval("EqApprovalUnitPrice")%>' title="价格输入格式不正确~float!"
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
                                                    交通费
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbTrafficCost" runat="server">0</asp:Label><asp:TextBox ID="tbTrafficCost"
                                                        runat="server" MaxLength="200" title="价格输入格式不正确~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalTrafficeCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalTrafficeCost" Text='<%#Eval("EqApprovalUnitPrice")%>' title="价格输入格式不正确~float!"
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
                                                    其它费用
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbOtherCost" runat="server">0</asp:Label><asp:TextBox ID="tbOtherCost"
                                                        runat="server" MaxLength="200" title="价格输入格式不正确~float!" Width="60px" OnTextChanged="tbMeasureCost_TextChanged"
                                                        AutoPostBack="True">0</asp:TextBox>
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                </td>
                                                <td class="table_none_WithoutWidth">
                                                    <asp:Label ID="lbApprovalOtherCost" runat="server">0</asp:Label><asp:TextBox runat="server"
                                                        ID="tbApprovalOtherCost" Text='<%#Eval("EqApprovalOtherCost")%>' title="价格输入格式不正确~float!"
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
                                                    小计
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
                                                    三
                                                </td>
                                                <td class="table_body_WithoutWidth" colspan="5" style="height: 20px">
                                                    合计
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
                                                    </asp:CheckBox><asp:Label runat="server" ID="jiliang" ForeColor="Blue">需要计量</asp:Label>
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
                            计量申请：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbshenqingjiliang" runat="server" Text="" ForeColor="Blue"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            是否计量：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="jilianga" GroupName="rb1" runat="server" Text="是" ForeColor="Red" />
                            <asp:RadioButton ID="jiliangb" GroupName="rb1" runat="server" Text="否" ForeColor="Red" />
                            <asp:Label ID="lbjiliang" Text="" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            是否甲供：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="jiagonga" GroupName="rb2" runat="server" Text="是" ForeColor="Red" />
                            <asp:RadioButton ID="jiagongb" GroupName="rb2" runat="server" Text="否" ForeColor="Red" />
                            <asp:Label ID="lbjiagong" Text="" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            故障处理单状态：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbStatus" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            最近更新时间：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbUpdateTime" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <%--<tr ID = "DelayDisplay" runat="server">
                        <td class="table_body_WithoutWidth " style="height: 30px;" >
                            延时申请：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="DelayTime" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            工程师：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="DelayCheck1a" GroupName="rb1" runat="server" Text="通过" ForeColor=Red/>
                            <asp:RadioButton ID="DelayCheck1b" GroupName="rb1" runat="server" Text="不通过" ForeColor=Red/>
                            <asp:Label ID="DelayCheck1pass" Text="通过" ForeColor="Blue" runat="server"></asp:Label>
                            <asp:Label ID="DelayCheck1dispass" Text="不通过" ForeColor="Blue" runat="server"></asp:Label>
                            
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            高级经理：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:RadioButton ID="DelayCheck2a" GroupName="rb1" runat="server" Text="通过"  ForeColor=Red/>
                            <asp:RadioButton ID="DelayCheck2b" GroupName="rb1" runat="server" Text="不通过"  ForeColor=Red/>
                            <asp:Label ID="DelayCheck2pass" Text="通过" ForeColor="Blue" runat="server"></asp:Label>
                            <asp:Label ID="DelayCheck2dispass" Text="不通过" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                        
                    </tr>--%>
                    <tr runat="server" id="ScrapChoose">
                        <td class="table_body_WithoutWidth">
                            经处理设备详情：
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:Literal ID="ScrapEquipmentNo" runat="server"></asp:Literal>
                        </td>
                        <td class="table_body_WithoutWidth">
                            是否报废：
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:RadioButton ID="ScrapButton" GroupName="Status" runat="server" Text="需要报废" ForeColor="Red" />
                            <asp:RadioButton ID="RadioButton1" GroupName="Status" runat="server" Text="否" ForeColor="Red" />
                        </td>
                    </tr>
                    <%--*********************************************屏蔽故障处理满意度调查4-27--%>
                    <%-- 
                    <tr style=" display:none;">
                        <td class="Table_searchtitle" colspan="6">
                            故障处理满意度调查
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            维修时间评价：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:CheckBox ID="cbIsResponseInTime" runat="server" Text="响应及时" />
                            <asp:CheckBox ID="cbIsFunRestoreInTime" runat="server" Text="功能恢复及时" />
                            <asp:CheckBox ID="cbIsRepairInTime" runat="server" Text="修复及时" />
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            处理效果：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblEffect" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            技术评价：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblTechnicEvaluate" runat="server" RepeatDirection="Horizontal"
                                Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            工作态度：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblAttitude" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            处理故障的合理性：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:CheckBoxList ID="cblRationality" runat="server" RepeatDirection="Horizontal"
                                Style="display: inline">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            使用部门意见：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:Label ID="lbFeeBackOpinion" runat="server" Text=""></asp:Label></td></tr><tr >
                        <td class="Table_searchtitle" colspan="6">
                            故障审批情况
                        </td>
                    </tr>
                    --%>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            附件记录：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:HyperLink ID="HyperLink_File" ForeColor="Blue" Font-Underline="true" runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            故障单审批记录：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <%-- <HeaderTemplate><table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;"></HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;<%#Eval("UserDeptName")%>审批意见：</td>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;<%#Eval("EvenName")%></td>&nbsp;&nbsp;
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%></td>>
                                    <td colspan="3" bordercolor="#ffffff" style=" border-bottom-color:#cccccc">&nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("ApprovalDate")%>&nbsp;</td></tr>
                                </ItemTemplate>--%>
                                <HeaderTemplate>
                                    <table border="0" width="100%">
                                        <tr>
                                            <th>
                                                审批部门
                                            </th>
                                            <th>
                                                审批结果
                                            </th>
                                            <th>
                                                审批意见
                                            </th>
                                            <th>
                                                职位
                                            </th>
                                            <th>
                                                用户名
                                            </th>
                                            <th>
                                                审批日期
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
                            审批意见：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:TextBox ID="tbApprovalRemark" runat="server" Height="70px" MaxLength="200" TextMode="MultiLine"
                                Width="414px"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr id="trCancelReason" runat="server" visible="false">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            撤单原因：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbCancelReason" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            撤单人：
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
                                    故障单修改历史
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
                                撤消故障处理单
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                撤单原因：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbCancelReason" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine"
                                    Rows="3" title="请输入撤单原因~200:!"></asp:TextBox><span style="color: Red">*</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btUndo" runat="server" Text="撤消故障单" CssClass="button_bak2" OnClick="btUndo_Click"
                                    OnClientClick="javascript:return checkForm(document.forms[0],true)&&confirm('确认要撤消此故障单？');" />&nbsp;&nbsp;
                                <asp:Button ID="btClose" runat="server" Text="取消" CssClass="button_bak" OnClick="btClose_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="ApprovalDiv" style="text-align: center; padding: 10px 10px 20px 10px;">
                    <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="提 交" CssClass="button_bak" OnClientClick="javascript:return confirm('确定提交审批？');"
                        OnClick="Button1_Click" Height="20px" />
                    <asp:RadioButton ID="rdoWanghai" GroupName="rb1" runat="server" Text="王海" ForeColor="Red" />
                    <asp:RadioButton ID="rdoHuangLiang" GroupName="rb1" runat="server" Text="黄亮" ForeColor="Red" />
                </div>
                <asp:Button ID="btUndoMode" runat="server" Text="Button" OnClick="btUndoMode_Click"
                    Style="display: none" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="attachmentId" runat="Server" visible="false">
            <table width="100%">
                <tr>
                    <td class="table_body_WithoutWidth " style="height: 30px;">
                        上传附件：
                    </td>
                    <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                        <asp:FileUpload ID="FileUpload_ArchivesAttachmentFile" runat="server"></asp:FileUpload>
                        <asp:Button ID="Button2" runat="server" Text="上传" CssClass="button_bak" OnClick="uploadfile_Click" />
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

            if (obj.innerText == "--折叠") {
                $get("EqCostDisplayTR").style.display = "none";
                obj.innerText = "+展开";
            } else if (obj.innerText == "+展开") {
                $get("EqCostDisplayTR").style.display = "inline";
                obj.innerText = "--折叠";
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
