<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionConfirm_Confirm, App_Web_rk0yqht9" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="故障处理满意度调查"
        HeadOPTxt="目前操作功能：故障处理满意度调查" HeadHelpTxt="帮助">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
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
                    <asp:Label ID="lbDepartment" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="width: 16%">
                    报修人
                </td>
                <td style="width: 17%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbReporter" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" style="width: 16%">
                    日期
                </td>
                <td style="width: 17%" class="table_none_WithoutWidth">
                    <asp:Label ID="lbReportTime" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            
            
            
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
                    <asp:Label ID="lbAddress" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td class="table_body_WithoutWidth">
                    地址详细描述
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbAddressDetail" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="table_body_WithoutWidth">
                    故障描述
                </td>
                <td class="table_none_WithoutWidth" colspan="5">
                    <asp:Label ID="lbDescription" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth">
                                        故障原因
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbSystem" runat="server" ></asp:Label>
                </td>
                <td class="table_body_WithoutWidth">
                    维修单位
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbMaintainTeam" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth" runat="server" id="MaintainPlanRankTitle">
                    故障等级
                </td>
                <td class="table_none_WithoutWidth" runat="server" id="MaintainPlanRankContent">
                    <asp:Label ID="lbMalfunctionRank" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
          <%--  <tr runat="server" id="MaintainPlanTime">
                <td class="table_body_WithoutWidth">
                    响应时间
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbResponseTime" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth">
                    功能性恢复时间
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbFunRestoreTime" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth">
                    修复时间
                </td>
                <td class="table_none_WithoutWidth">
                    <asp:Label ID="lbRepairTime" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    故障记录部门：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:Label ID="lbRecordDept" runat="server" Text="Label"></asp:Label>
                    &nbsp;
                </td>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    故障记录人：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:Label ID="lbRecorder" runat="server" Text="Label"></asp:Label>
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
                    <asp:Label ID="lbMaintainTeamx" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                    受理人：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                    <asp:Label ID="lbReceiver" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                    受理日期：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                    <asp:Label ID="lbReceiveDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
             <tr>
                <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                    实际响应时间：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                    <asp:Label ID="lbActResponseTime" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                    &nbsp;</td>
                <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                    &nbsp;</td>
                <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                    排障耗时：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                    <asp:Label ID="lbActRepairTime" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            
            <tr>
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
            </tr>
            
            <asp:Repeater ID="rptMaintainHistory" runat="server">
                <ItemTemplate>
                    <tr>
                        <td rowspan="2" align="center">
                            <%#Eval("UpdateTime", "{0:yyyy-MM-dd  HH:mm}")%>
                        </td>
                        <td style="padding-left: 20px; padding-top:8px;" colspan="5">
                            维修时间：<%#Eval("UpdateTime", "{0:yyyy-MM-dd}")%>&nbsp;&nbsp;&nbsp;&nbsp; 维修单位：<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;维修人：<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                            维修总费用：<%#Eval("TotalFee","{0:#,0.#}")%>元&nbsp;&nbsp;&nbsp;&nbsp; 修复情况：<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;是否送修：<%#Convert.ToBoolean(Eval("IsDelivered"))?"是":"否"%><br />
                            处理意见：<%#Eval("MaintenanceDetail")%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Repeater ID="rptHistoryEquipments" runat="server" DataSource='<%# Eval("MaintainedEquipments") %>'  Visible='<%#((IList)Eval("MaintainedEquipments")).Count>0?true:false%>'>
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                        style="border-collapse: collapse;">
                                        <tr>
                                            <td colspan="6" align="center" style="background-color: #EFEFEF; font-weight: bold;">
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
                                            <td align="center" style="width: 13%">
                                                型号
                                            </td>
                                            <td align="center" style="width: 10%">
                                                维修结果
                                            </td>
                                            <td align="center" style="width: 15%">
                                                维修费用（元）
                                            </td>
                                            <td align="center">
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
                                        <td align="center">
                                            <%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %>
                                        </td>
                                        <td align="center">
                                            <%# Eval("MaintainFee", "{0:#,0.##}") %>
                                        </td>
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
            <tr>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    故障处理单状态：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:Label ID="lbStatus" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                    &nbsp;
                </td>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    最近更新时间：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:Label ID="lbUpdateTime" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="6">
                    故障处理满意度调查
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    维修时间评价：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                    <asp:CheckBox ID="cbIsResponseInTime" runat="server" Text="响应及时" />
                    <asp:CheckBox ID="cbIsFunRestoreInTime" runat="server" Text="功能恢复及时" />
                    <asp:CheckBox ID="cbIsRepairInTime" runat="server" Text="修复及时" />
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    处理效果：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:RadioButtonList ID="cblEffect" runat="server" RepeatDirection="Horizontal" Style="display: inline">
                    </asp:RadioButtonList>
                </td>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    技术评价：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:RadioButtonList ID="cblTechnicEvaluate" runat="server" RepeatDirection="Horizontal"
                        Style="display: inline">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    工作态度：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:RadioButtonList ID="cblAttitude" runat="server" RepeatDirection="Horizontal"
                        Style="display: inline">
                    </asp:RadioButtonList>
                </td>
                 <td class="table_body_WithoutWidth " style="height: 30px;">
                    处理故障的合理性：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                    <asp:RadioButtonList ID="cblRationality" runat="server" RepeatDirection="Horizontal"
                        Style="display: inline">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth " style="height: 30px;">
                    使用部门意见：
                </td>
                <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                    <asp:TextBox ID="tbFeebackOpinion" runat="server" TextMode="MultiLine" Rows="4" Width="95%"
                        title="请输入使用部门意见~200:" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" style=" padding:10px 10px 20px 10px">
            <tr>
                <td align="center">
                    <asp:Button ID="btSave" runat="server" Text="提交" CssClass="button_bak" OnClick="btSave_Click"
                        OnClientClick="javascript:return confirm('确认提交？');" />
                    <br />
                    <br /><br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
