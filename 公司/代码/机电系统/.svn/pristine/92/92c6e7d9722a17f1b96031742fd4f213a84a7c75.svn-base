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
                    <b style="font-family: 宋体; font-size: medium">
                        <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label>
                         维修处理单</b>
                </td>
                <td colspan="2" class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    表单编号
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="lbSheetNO" runat="server" Text="" Style=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="Table_searchtitle" style="color: Black; background-color: White;">
                    报修情况
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    报修单位
                </td>
                <td style="width: 18%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbDepartment" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    报修人
                </td>
                <td style="width: 17%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbReporter" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    日期
                </td>
                <td style="width: 17%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbReportTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    故障设备(上报)
                </td>
                <td class="table_none_WithoutWidth" colspan="2" valign="middle">
                   <%-- <br />
                    <asp:Repeater ID="repeatEquipments" runat="server">
                        <ItemTemplate>
                            <%#Container.ItemIndex+1 %>、设备名称：&nbsp;<%#Eval("EquipmentNAME") %>&nbsp;<%#!string.IsNullOrEmpty(Convert.ToString(Eval("EquipmentNO")))?"("+Eval("EquipmentNO")+")":"" %><br />
                        </ItemTemplate>
                    </asp:Repeater>
                    <br />--%>
                    <asp:Label  ID = "lbEquipmentNO" runat="server" Text=""></asp:Label>
                    
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    故障设备名称
                </td>
                <td class="table_none_WithoutWidth" colspan="2" valign="middle">
                <asp:Label  ID = "lbEquipmentName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    故障设备地址
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
           <%-- <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    地址详细描述
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbAddressDetail" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    故障描述
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    故障原因
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbSystem" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    维修单位
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbMaintainTeam" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    故障等级
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbMalfunctionRank" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    响应时间
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbResponseTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    功能性恢复时间
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbFunRestoreTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="color: Black; background-color: White;">
                    修复时间
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbRepairTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;">
                    故障记录部门：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbRecordDept" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;">
                    故障记录人：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbRecorder" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="color: Black; background-color: White;" colspan="6">
                    维修情况登记
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;
                    width: 16%">
                    维修单位：
                </td>
                <td class="table_none_WithoutWidth " style="color: Black; background-color: White;
                    width: 18%">
                    <asp:Label ID="lbMaintainTeamx" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;
                    width: 16%">
                    受理人：
                </td>
                <td class="table_none_WithoutWidth " style="color: Black; background-color: White;
                    width: 17%">
                    <asp:Label ID="lbReceiver" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="color: Black; background-color: White;
                    width: 16%">
                    受理日期：
                </td>
                <td class="table_none_WithoutWidth " style="width: 17%">
                    <asp:Label ID="lbReceiveDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    实际响应时间：
                </td>
                <td class="table_none_WithoutWidth " style="width: 18%">
                    <asp:Label ID="lbActResponseTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    确认修复时间：
                </td>
                <td class="table_none_WithoutWidth " style="width: 17%">
                    <asp:Label ID="lbConTime" runat="server" Text=""></asp:Label></td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    排障耗时：
                </td>
                <td class="table_none_WithoutWidth " style="width: 17%">
                    <asp:Label ID="lbActRepairTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="table_none_WithoutWidth ">
                    维修人员列表：
                </td>
                <td colspan="3"  class="table_none_WithoutWidth ">
                    <asp:Label ID="lbMaintainStaffList" runat="server"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    是否计量：
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
                            登记时间：<%#Eval("UpdateTime", "{0:yyyy-MM-dd HH:MM}")%>&nbsp;&nbsp;&nbsp;&nbsp;<%--维修单位：<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;维修记录人：<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;--%>
                            修复情况：<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;是否送修：<%#Convert.ToBoolean(Eval("IsDelivered"))?"是":"否"%><br />
                            故障详细描述：<%#Eval("MaintenanceDescription")%><br />
                            故障处理办法：<%#Eval("MaintenanceMethod")%><br />
                            非设备故障：<%#Eval("NoEquipment")%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Repeater ID="rptHistoryEquipments" runat="server" DataSource='<%# Eval("MaintainedEquipments") %>'
                                Visible='<%#((IList)Eval("MaintainedEquipments")).Count>0?true:false%>'>
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="1" 
                                        style="border-collapse: collapse;font-family: 宋体; font-size: medium">
                                        <tr class="table_none_WithoutWidth ">
                                                    <td colspan="7" align="center" style=" font-weight: bold;">
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
                                                    </td><td align="center" style="width: 6%">
                                                        设备类型
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        维修结果
                                                    </td>
                                                    <td align="center" style="width: 23%">
                                                        设备跟踪
                                                    </td>
                                                    <td align="center" >
                                                        维修情况
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
                                故障费用统计表
                            </td>
                        </tr>--%>
                        
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth"  >一</td>
                            <td class="table_body_WithoutWidth" colspan="9" >设备费及材料费</td>
                        </tr>
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth" style="width: 5%"  >序号</td>
                            <td class="table_none_WithoutWidth" style="width: 13%"  >名称</td>
                            <td class="table_none_WithoutWidth" style="width: 13%"  >型号</td>
                            <td class="table_none_WithoutWidth" style="width: 5%"  >单位</td>
                            <td class="table_none_WithoutWidth" style="width: 5%"  >数量</td>
                            <td class="table_none_WithoutWidth" style="width: 10%"  >综合单价</td>
                            <td class="table_none_WithoutWidth" style="width: 12%"  >合价</td>
                            
                            <td class="table_none_WithoutWidth" style="width: 10%"  >审核综合单价</td>
                            <td class="table_none_WithoutWidth" style="width: 12%"  >审核合价</td>
                            
                            <td class="table_none_WithoutWidth" style="width: 15%"  >备注</td>
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
                            <td class="table_none_WithoutWidth" colspan="5" >小计</td>
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
                            <td class="table_body_WithoutWidth"  >二</td>
                            <td class="table_body_WithoutWidth"  colspan="9"  >其他费用</td>
                        </tr>
                        
                        <tr>
                            <td class="table_body_WithoutWidth"  >1</td>
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">措施费</td>
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
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">规费</td>
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
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">税金</td>
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
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">交通费</td>
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
                            <td class="table_none_WithoutWidth" colspan="5" style="font-weight:bold;">其它费用</td>
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
                            <td class="table_none_WithoutWidth" colspan="5" >小计</td>
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
                            <td class="table_body_WithoutWidth"  >三</td>
                            <td class="table_body_WithoutWidth"  colspan="5" >合计</td>
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
                            
                                &nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%> <br />
                                &nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("CheckDate")%>&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>    
                        </ItemTemplate>
                        <%--<FooterTemplate></tr></FooterTemplate>--%>
                    </asp:Repeater> 
            
            <tr runat="server" id="trHandleOpinion" visible="false">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    处理意见：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                </td>
            </tr>
            <%-- 
            <tr >
                <td class="Table_searchtitle" colspan="6" style="width: 16%; color: Black; background-color: White;">
                    故障处理满意度调查
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    维修时间评价：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <asp:CheckBox ID="cbIsResponseInTime" runat="server" Text="响应及时" />
                    <asp:CheckBox ID="cbIsFunRestoreInTime" runat="server" Text="功能恢复及时" />
                    <asp:CheckBox ID="cbIsRepairInTime" runat="server" Text="修复及时" />
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    处理效果：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblEffect" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                    </asp:CheckBoxList>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    技术评价：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblTechnicEvaluate" runat="server" RepeatDirection="Horizontal"
                        Style="display: inline">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    工作态度：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblAttitude" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                    </asp:CheckBoxList>
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    处理故障的合理性：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:CheckBoxList ID="cblRationality" runat="server" RepeatDirection="Horizontal"
                        Style="display: inline">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr >
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    使用部门意见：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <asp:Label ID="lbFeeBackOpinion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            --%>
            
         <tr >
                        <td class="Table_searchtitle" colspan="6">
                            故障审批情况
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            故障单审批记录：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <%--<HeaderTemplate><table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;"></HeaderTemplate>
                                <ItemTemplate>
                                <tr>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;<%#Eval("UserDeptName")%>审批意见：</td>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;<%#Eval("EvenName")%></td>
                                    <td colspan="5" bordercolor="#ffffff">&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("Remark")%></td>
                                    <td colspan="3" bordercolor="#ffffff" style=" border-bottom-color:#cccccc">&nbsp;&nbsp;<%#Eval("UserDeptName")%>&nbsp;<%#Eval("UserPsnName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("UserName")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("ApprovalDate")%>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    </tr>
                                </ItemTemplate>--%>
                                <HeaderTemplate>
                                    <table border="0" width="100%">
                                   <tr>
                                    <th>审批部门</th>
                                    <th>审批结果</th>
                                    <th>审批意见</th>

                                    <th>职位</th>
                                    <th>用户名</th>
                                    <th>审批日期</th>

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
                            计量申请：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbshenqingjiliang" runat="server" Text="" ForeColor="Blue"></asp:Label>&nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            是否计量：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbjiliang" Text="" ForeColor="Blue" runat="server"></asp:Label>                                                      
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            是否甲供：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="1">
                            <asp:Label ID="lbjiagong" Text="" ForeColor="Blue" runat="server"></asp:Label>                            
                        </td>
                        
                    </tr>
                  <table width="649px" id="divCancelDetail" runat="Server" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                撤单情况
                            </td>
                        </tr>
                        <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                撤单申请人：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbApplyCancelName" runat="Server" Text=""></asp:Label>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                撤单申请时间：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbCancelApplyTime" runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                申请撤单原因：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyCancelReason"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                撤单审批情况：
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
                             延时审批情况
                            </td>
                        </tr>
                        <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                提交延时申请时间：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbDelayApplyTime" runat="Server" Text=""></asp:Label>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                本故障单申请的时效时间为：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="1" style="height: 30px">
                               <asp:Label ID="lbDelayForTime" runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                延时原因：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyDelayReason"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                延时审批情况：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3"  rowspan="3" style="height: 30px">
                               <asp:Label ID="lbDelayApprove"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                  
                  
            
            <tr id="trCancelStatus" runat="server" visible="false" style="display:none;">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    故障单状态：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    撤单人：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="2">
                    <asp:Label ID="lbCanceler" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr id="trCancelReason" runat="server" visible="false" style="display:none;">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    撤单原因：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <asp:Label ID="lbCancelReason" runat="server" Text=""></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <%--<tr id="tr1" runat="server" style="display:none;">
                <td class="table_body_WithoutWidth " style="width: 16%; color: Black; background-color: White;">
                    维护人员确认：
                </td>
                <td class="table_none_WithoutWidth " style="" colspan="5">
                    <br />
                    <span style="float: right">年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>
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
