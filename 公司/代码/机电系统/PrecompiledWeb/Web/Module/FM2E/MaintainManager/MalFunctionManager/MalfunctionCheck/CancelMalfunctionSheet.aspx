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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="处理单撤单处理"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：审批撤单申请">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="撤单" ButtonPopedom="Delete" />
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="故障单列表" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="打印" ButtonPopedom="NotControl" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="upMain" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
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
                        <td class="table_body_WithoutWidth">故障设备条形码</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:Label ID="lbEqNo" runat="server" Text=""></asp:Label></td>
                        <td class="table_body_WithoutWidth">故障设备名称</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:Label ID="lbEqName" runat="server" Text=""></asp:Label></td>
                        <td class="table_body_WithoutWidth">所属系统</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:Label ID="lbEqSystem" runat="server" Text=""></asp:Label></td>
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
                            
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                            &nbsp;</td>
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
                                    维修时间：<%#Eval("UpdateTime", "{0:yyyy-MM-dd}")%>&nbsp;&nbsp;&nbsp;&nbsp; 维修单位：<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;维修记录人：<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                    维修总费用：<%#Eval("TotalFee","{0:#,0.#}")%>元&nbsp;&nbsp;&nbsp;&nbsp; 修复情况：<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;是否送修：<%#Convert.ToBoolean(Eval("IsDelivered"))?"是":"否"%><br />
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
                                                    <td align="center" >
                                                        维修情况
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
                                                <td colspan="7" align="center" style="background-color: #EFEFEF; font-weight: bold;">更换的设备</td>
                                                <td colspan="4">
                                                    <asp:Repeater ID="rptHistoryEquipmentsParts" runat="server" DataSource='<%# Eval("MaintainedEquipmentParts") %>'
                                                        Visible='<%#((IList)Eval("MaintainedEquipmentParts")).Count>0?true:false%>'>
                                                        <HeaderTemplate>
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
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
                    <tr >
                            
                            <td class="Table_searchtitle" colspan="10"  >设备及材料统计</td>
                        </tr>
                        <tr style="font-weight:bold;">
                            <td class="table_body_WithoutWidth"  style="text-align:center; width: 3%"  >序号</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 10%"  >名称</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 10%"  >型号</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 5%"  >单位</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 5%"  >数量</td>
                            <td class="table_none_WithoutWidth" style="text-align:center; width: 23%"  >备注</td>
                           
                            
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
                            故障处理单状态：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
                            &nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            最近更新时间：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbUpdateTime" runat="server" Text="" ForeColor="Red"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    
                    
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
                    
                    <%-- By L 4-27 满意度调查屏蔽****************************************************************************************************
                    
                    <tr  style=" display:none;">
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
                    
                    
                    <tr id="trCancelReason" runat="server" visible="false">
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            撤单原因：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                            <asp:Label ID="lbCancelReason" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td class="table_body_WithoutWidth " style="height: 30px;">
                            撤单人：
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
                            <td class="Table_searchtitle" colspan="4">
                                撤消故障处理单
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                撤单原因：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                <asp:TextBox ID="tbCancelReason" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine"
                                    Rows="3" title="请输入撤单原因~200:!"></asp:TextBox>
                                <span style="color: Red">*</span>
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
                <asp:Button ID="btUndoMode" runat="server" Text="Button" OnClick="btUndoMode_Click"
                    Style="display: none" />
                    
                
                <div id="CheckMaintainDIV" style="text-align:center; padding:15px;">
                
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
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
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyCancelName" runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                申请撤单说明：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                               <asp:Label ID="lbApplyCancelReason"  runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center;">
                                撤单审批说明：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                <asp:TextBox ID="tbCheckInfor" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine"
                                    Rows="3" title="请输入相关说明~200:!"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                    </table>
                
                    <asp:Button ID="btCheckPass" runat="server" Text="同意撤单" CssClass="button_bak2" 
                        onclick="btCheckPass_Click" OnClientClick="javascript:return checkForm(document.forms[0],true)&& confirm('确定同意撤单？');" />
                    &nbsp;
                    <asp:Button ID="btCheckFailed" runat="server" Text="不同意撤单" 
                        CssClass="button_bak2" onclick="btCheckFailed_Click" OnClientClick="javascript:return checkForm(document.forms[0],true)&& confirm('确定不同意撤单并返回修复？');" />
                </div>
                    
                    
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" language="javascript">
        document.getElementById("<%= tbCancelReason.ClientID %>").focus();
    </script>
</asp:Content>

