<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MalfunctionHandleFinished.aspx.cs" Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionMaintain_MalfunctionHandleFinished" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" OnNavigate="ScriptManager1_Navigate">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="故障处理"
        HeadOPTxt="目前操作功能：故障处理" HeadHelpTxt="帮助">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="故障处理单" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td colspan="4" rowspan="2" class="table_body_WithoutWidth">
                                <b style="font-family: 宋体; font-size: medium">
                                    <asp:Label ID="lbCompany" runat="server" Text="XX"></asp:Label>
                                    高速公路有限公司维修处理单</b>
                            </td>
                            <td colspan="2" class="table_body_WithoutWidth">
                                表单编号
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Label ID="lbSheetNO" runat="server" Style=""></asp:Label>
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
                                <asp:Label ID="lbDepartment" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth" style="width: 16%">
                                报修人
                            </td>
                            <td style="width: 17%" class="table_none_WithoutWidth">
                                <asp:Label ID="lbReporter" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth" style="width: 16%">
                                日期
                            </td>
                            <td style="width: 17%" class="table_none_WithoutWidth">
                                <asp:Label ID="lbReportTime" runat="server"></asp:Label>
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
                        <tr>
                            <td class="table_body_WithoutWidth">
                                故障设备地址
                            </td>
                            <td class="table_none_WithoutWidth" colspan="5">
                                <asp:Label ID="lbAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body_WithoutWidth">
                                地址详细描述
                            </td>
                            <td class="table_none_WithoutWidth" colspan="5">
                                <asp:Label ID="lbAddressDetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body_WithoutWidth">
                                故障描述
                            </td>
                            <td class="table_none_WithoutWidth" colspan="5">
                                <asp:Label ID="lbDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body_WithoutWidth">
                                故障原因
                            </td>
                            <td class="table_none_WithoutWidth">
                                <asp:Label ID="lbSystem" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth">
                                维修单位
                            </td>
                            <td class="table_none_WithoutWidth">
                                <asp:Label ID="lbMaintainTeam" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth">
                                故障等级
                            </td>
                            <td class="table_none_WithoutWidth">
                                <asp:Label ID="lbMalfunctionRank" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body_WithoutWidth " style="height: 30px;">
                                故障记录部门：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                                <asp:Label ID="lbRecordDept" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td class="table_body_WithoutWidth " style="height: 30px;">
                                故障记录人：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                                <asp:Label ID="lbRecorder" runat="server"></asp:Label>
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
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                                受理人：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                                受理日期：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                                实际响应时间：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px; width: 18%">
                                <asp:Label ID="lbActResponseTime" runat="server"></asp:Label>
                            </td>
                            <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                                &nbsp;</td>
                            <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                                &nbsp;</td>
                            <td class="table_body_WithoutWidth " style="height: 30px; width: 16%">
                                实际修复时间：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px; width: 17%">
                                <asp:Label ID="lbActRepairTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                        <asp:Repeater ID="rptMaintainHistory1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td rowspan="2" align="center">
                                        <%#Eval("UpdateTime", "{0:yyyy-MM-dd  HH:mm}")%>
                                    </td>
                                    <td style="padding-left: 20px; padding-top: 8px;" colspan="5">
                                        维修时间：<%#Eval("UpdateTime", "{0:yyyy-MM-dd}")%>&nbsp;&nbsp;&nbsp;&nbsp; 维修单位：<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;维修人：<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                        维修总费用：<%#Eval("TotalFee","{0:#,0.#}")%>元&nbsp;&nbsp;&nbsp;&nbsp; 修复情况：<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%>&nbsp;&nbsp;&nbsp;&nbsp;是否送修：<%#Convert.ToBoolean(Eval("IsDelivered"))?"是":"否"%><br />
                                        处理意见：<%#Eval("MaintenanceDetail")%>
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
                        <tr>
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
                        <tr>
                            <td class="table_body_WithoutWidth " style="height: 30px;">
                                使用部门意见：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                                <asp:Label ID="lbFeeBackOpinion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body_WithoutWidth " style="height: 30px;">
                                故障处理单状态：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                                <asp:Label ID="lbStatus" runat="server" ForeColor="Red"></asp:Label>
                                &nbsp;
                            </td>
                            <td class="table_body_WithoutWidth " style="height: 30px;">
                                最近更新时间：
                            </td>
                            <td class="table_none_WithoutWidth " style="height: 30px;" colspan="2">
                                <asp:Label ID="lbUpdateTime" runat="server" ForeColor="Red"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Repeater ID="rptModifyHistory" runat="server" Visible="False">
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
                    
                    <hr style="width: 95%" />
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;" id="tableRepairRecord" runat="server">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" class="Table_searchtitle" style="height: 30px;" colspan="6" runat="server">
                                维修情况追加
                            </td>
                        </tr>
                        <tr id="Tr2" runat="server">
                            <td id="Td2" class="table_body_WithoutWidth " style="height: 30px; width: 16%" 
                                runat="server">
                                维修单位：
                            </td>
                            <td id="Td3" class="table_none_WithoutWidth " style="height: 30px; width: 18%" 
                                runat="server">
                                <asp:Label ID="lbMaintainTeamx" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td id="Td4" class="table_body_WithoutWidth " style="height: 30px; width: 16%" 
                                runat="server">
                                受理人：
                            </td>
                            <td id="Td5" class="table_none_WithoutWidth " style="height: 30px; width: 17%" 
                                runat="server">
                                <asp:Label ID="lbReceiver" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td id="Td6" class="table_body_WithoutWidth " style="height: 30px; width: 16%" 
                                runat="server">
                                受理日期：
                            </td>
                            <td id="Td7" class="table_none_WithoutWidth " style="height: 30px; width: 17%" 
                                runat="server">
                                <asp:Label ID="lbReceiveDate" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr id="Tr3" runat="server">
                            <td id="Td8" class="table_body_WithoutWidth " style="height: 30px; width: 16%" 
                                runat="server">
                                维修日期：
                            </td>
                            <td id="Td9" class="table_none_WithoutWidth " style="height: 30px; width: 18%" 
                                runat="server">
                                <asp:Label ID="lbMaintainDate" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td id="Td10" class="table_body_WithoutWidth " style="height: 30px; width: 16%" 
                                runat="server">
                                修复程度：
                            </td>
                            <td id="Td11" class="table_none_WithoutWidth " style="height: 30px; width: 18%" runat="server" colspan="3">
                                完全修复
                                <asp:RadioButtonList ID="rblRepairSituation" runat="server" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="Tr4" runat="server">
                            <td id="Td12" class="table_body_WithoutWidth " style="height: 30px;" runat="server">
                                处理意见：
                            </td>
                            <td id="Td13" class="table_none_WithoutWidth " style="height: 30px;" colspan="5" 
                                runat="server">
                                <asp:TextBox ID="tbMaintenanceDetail" runat="server" Width="95%" TextMode="MultiLine"
                                    Rows="4"  MaxLength="200"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr id="Tr5" runat="server">
                            <td id="Td14" class="table_body_WithoutWidth " colspan="2" 
                                style="height: 30px; border-right-width: 0px" runat="server">
                            </td>
                            <td id="Td15" class="table_body_WithoutWidth " colspan="2" style="height: 30px; border-left-width: 0px;
                                border-right-width: 0px" runat="server">
                                已维修的设备
                            </td>
                            <td id="Td16" class="table_body_WithoutWidth " colspan="2" style="height: 30px; text-align: right;
                                border-left-width: 0px; border-right-width: 0px" runat="server">
                                <input id="btAddEquipment" type="button" runat="server" class="button_bak" value="添加设备" />
                                <cc2:ModalPopupExtender ID="ModalPopupExtender_AddEquipment" runat="server" TargetControlID="btAddEquipment"
                                    PopupControlID="plAddEquipment" BackgroundCssClass="modalBackground" OkControlID="Button_OK"
                                    CancelControlID="Button_Cancel" DynamicServicePath="" Enabled="True">
                                </cc2:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr id="Tr6" runat="server">
                            <td id="Td17" class="table_none_WithoutWidth " colspan="6" 
                                style="height: 30px; border-right-width: 0px" runat="server">
                                <asp:UpdatePanel ID="updatePanelEquipments" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvMaintainedEquipments" runat="server" Width="100%" AutoGenerateColumns="False"
                                            OnRowCommand="gvMaintainedEquipments_RowCommand" OnRowDataBound="gvMaintainedEquipments_RowDataBound"
                                            ShowFooter="true">
                                            <EmptyDataTemplate>
                                                暂未添加经过维修的设备
                                            </EmptyDataTemplate>
                                            <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" Font-Bold="True" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        序号</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %></ItemTemplate>
                                                        <ItemStyle Width="4%"/>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码">
                                                <ItemStyle Width="10%"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EquipmentName" HeaderText="设备名称">
                                                <ItemStyle Width="13%"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Model" HeaderText="型号">
                                                <ItemStyle Width="13%"/>
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        维修结果</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%"/>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Remark" HeaderText="维修情况">
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="维修费用（元）">
                                                    <ItemTemplate>
                                                        <%# Eval("MaintainFee", "{0:#,0.##}") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbTotalFee" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="12%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        删除</HeaderTemplate>
                                                    <ItemStyle Width="4%" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此申请明细吗？')"
                                                            CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                            <RowStyle HorizontalAlign="Center" Height="20px" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="plAddEquipment" runat="server" Style="width: 95%; height: 200px; display: none"
                        CssClass="modalPopup">
                        <asp:UpdatePanel ID="updatePanelAddEquipment" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" style="height: 30px" colspan="4">
                                            添加已维修的设备
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            设备条形码：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="tbEquipmentNO" runat="server" AutoPostBack="True" OnTextChanged="tbEquipmentNO_TextChanged"
                                                title="请输入设备条形码~20:noChinese"></asp:TextBox>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            设备名称：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="tbEquipmentName" runat="server" MaxLength="20"></asp:TextBox><span style="color: Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            规格型号：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="tbModel" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            维修次数：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="tbMaintainTimes" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            修复状态：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:DropDownList ID="ddlStatus" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            维修费用：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="tbMaintainFee" runat="server" Text="0" Width="50px"></asp:TextBox>元
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            维修具体情况：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                            <asp:TextBox ID="tbRemark" runat="server" TextMode="MultiLine" Rows="3" Width="95%"
                                                MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <center>
                            <table width="100%">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:UpdatePanel ID="updatePanelMsg" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbMsg" runat="server" ForeColor="Red"></asp:Label></ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="right" style="width: 40%">
                                        <asp:UpdatePanel ID="updatePanelButton" runat="server">
                                            <ContentTemplate>
                                                <input id="btSaveEquipment" class="button_bak2" type="button" runat="server" value="添加到已维修列表"
                                                    onserverclick="btSaveEquipment_Click" /></ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left" style="width: 20%">
                                        &nbsp;&nbsp;
                                        <input id="Button_OK" class="button_bak" style="display: none" value="OK" />
                                        <asp:Button ID="Button_Cancel" runat="server" class="button_bak" Text="取消" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </asp:Panel>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lbErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                            <table><tr><td> <asp:Button ID="btAccept" runat="server" CssClass="button_bak" Text="我要受理" OnClick="btAccept_Click" />&nbsp;&nbsp;</td>
                            <td> <asp:UpdatePanel ID="updatePanelSaveButton" runat="server">
                                            <ContentTemplate> <asp:Button ID="btSave" runat="server" Text="提交" CssClass="button_bak" OnClick="btSave_Click" />&nbsp;&nbsp;</ContentTemplate>
                                            </asp:UpdatePanel></td>
                                            <td>  <asp:Button ID="btCancel" runat="server" Text="取消" CssClass="button_bak" OnClick="btCancel_Click" /> </td></tr></table>
                               
                                
                              
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="故障单处理历史" ID="TabPanel2">
                <ContentTemplate>
                    <asp:Repeater ID="rptMaintainHistory" runat="server">
                        <HeaderTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" style="height: 30px;" colspan="2">
                                        故障单处理历史
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td rowspan="2" align="center"><%#Eval("UpdateTime", "{0:yyyy-MM-dd HH:mm}")%></td>
                                <td style="padding-left: 20px; padding-top:8px;">
                                    维修时间：<%#Eval("UpdateTime", "{0:yyyy-MM-dd}")%>&nbsp;&nbsp;&nbsp;&nbsp; 维修单位：<%#Eval("MaintenanceTeam")%>&nbsp;&nbsp;&nbsp;&nbsp;维修人：<%#Eval("MaintenanceStaffName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                    维修总费用：<%#Eval("TotalFee","{0:#,0.#}")%>元&nbsp;&nbsp;&nbsp;&nbsp; 修复情况：<%#EnumHelper.GetDescription((Enum)Eval("RepairSituation"))%><br />
                                    处理意见：<%#Eval("MaintenanceDetail")%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Repeater ID="rptHistoryEquipments" runat="server" DataSource='<%# Eval("MaintainedEquipments") %>' Visible='<%#((IList)Eval("MaintainedEquipments")).Count>0?true:false%>'>
                                        <HeaderTemplate>
                                         <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                           style="border-collapse: collapse;">
                                           <tr><td colspan="6" align="center" style=" background-color:#EFEFEF; font-weight:bold;">经过维修的设备</td></tr>
                                           <tr>
                                           <td align="center" style="width:10%">设备条形码</td>
                                           <td align="center" style="width:10%">设备名称</td>
                                           <td align="center" style="width:13%">型号</td>
                                           <td align="center" style="width:10%">维修结果</td>
                                           <td align="center" style="width:15%">维修费用（元）</td>
                                           <td align="center">维修情况</td>
                                           </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <tr>
                                        <td align="center"><%#Eval("EquipmentNO") %></td>
                                        <td align="center"><%#Eval("EquipmentName") %></td>
                                        <td align="center"><%#Eval("Model") %></td>
                                        <td align="center"><%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %></td>
                                        <td align="center"><%# Eval("MaintainFee", "{0:#,0.##}") %></td>
                                        <td align="center"><%#Eval("Remark")%></td>
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
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

