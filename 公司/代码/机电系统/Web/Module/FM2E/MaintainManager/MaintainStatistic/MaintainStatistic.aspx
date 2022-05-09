<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MaintainStatistic.aspx.cs" Inherits="Module_FM2E_MaintainManager_MaintainStatistic_MaintainStatistic"
    EnableEventValidation="false" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备保养记录统计查询"
        HeadOPTxt="目前操作功能：设备保养记录统计查询">
    </cc1:HeadMenuWebControls>
    <div style="width: 900px;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="查询条件" ID="TabPanel1">
                <ContentTemplate>
                    <table id="outwarehouse" width="880px" cellpadding="0" cellspacing="0" border="1"
                        bordercolor="#cccccc" style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                查询条件
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLSystem" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                子系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLSubsystem" runat="server">
                                </asp:DropDownList>
                            </td>
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DDLSystem"
                                Category="System" PromptText="请选择系统..." LoadingText="系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                ServiceMethod="GetSystem" Enabled="True">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DDLSubsystem"
                                Category="Subsystem" PromptText="请选择子系统..." LoadingText="子系统加载中..." ServicePath="SystemSubsystemService.asmx"
                                ServiceMethod="GetSubsystem" ParentControlID="DDLSystem" Enabled="True">
                            </cc2:CascadingDropDown>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                设备条形码：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TBEquipmentNO" runat="server" title="请输入设备条形码~"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth">
                            </td>
                            <td class="table_none table_none_NoWidth">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" align="right" style="height: 38px" runat="server">
                                <asp:Button ID="btnSearch" runat="server" CssClass="button_bak" Text="查询" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="日常巡查记录">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse; text-align: center">
                        <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                            <td>
                                巡查时间
                            </td>
                            <td>
                                巡查人
                            </td>
                            <td>
                                巡查项目
                            </td>
                            <td>
                                实际巡查结果
                            </td>
                            <td>
                                审核结果
                            </td>
                            <td>
                                审核备注
                            </td>
                        </tr>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <ItemTemplate>
                                <tr style="height: 30px">
                                    <td onclick='javascript:HideRecord("PatrolHistoryTable_"+<%#Container.ItemIndex %>);'>
                                        <asp:Literal ID="ltPatrolRecordID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false"></asp:Literal><asp:Literal
                                            ID="ltPatrolEquipmentName" runat="server" Text='<%#Convert.ToDateTime(Eval("RecordDate")).ToString("yyyy-MM-dd") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltPatrolmanName" runat="server" Text='<%#Eval("RecordmanName") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Literal ID="ltPatrolObject" runat="server" Text='<%#Eval("RecordObject") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Literal ID="ltPatrolResult" runat="server" Text='<%#Eval("RecordResult") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltPatrolVerified" runat="server" Text='<%#Eval("VerifiedResultString") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltPatrolRemark" runat="server" Text='<%#Eval("VerifyRemark") %>'></asp:Literal>
                                    </td>
                                </tr>
                                <tr style="display: none" id="PatrolHistoryTable_<%#Container.ItemIndex %>">
                                    <td colspan="6">
                                        <table width="100%">
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="lbPatrolSystem" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    子系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbPatrolSubsystem" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    巡查项目：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbPatrolPlanObject" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    巡查周期：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="lbPatrolPlanPeriod" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    巡查内容：
                                                </td>
                                                <td colspan="3" class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbPatrolPlanContent" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    验收标准：
                                                </td>
                                                <td colspan="3" class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbPatrolCheckStandard" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                        NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                        ShowCustomInfoSection="Left" CloneFrom="" Width="880px">
                    </cc1:AspNetPager>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="例行保养记录">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse; text-align: center">
                        <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                            <td>
                                保养时间
                            </td>
                            <td>
                                保养人
                            </td>
                            <td>
                                保养项目
                            </td>
                            <td>
                                实际保养结果
                            </td>
                            <td>
                                审核结果
                            </td>
                            <td>
                                审核备注
                            </td>
                        </tr>
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <ItemTemplate>
                                <tr style="height: 30px">
                                    <td onclick='javascript:HideRecord("MaintainHistoryTable_"+<%#Container.ItemIndex %>);'>
                                        <asp:Literal ID="ltMaintainRecordID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false"></asp:Literal><asp:Literal
                                            ID="ltMaintainEquipmentName" runat="server" Text='<%#Convert.ToDateTime(Eval("RecordDate")).ToString("yyyy-MM-dd") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltMaintainmanName" runat="server" Text='<%#Eval("RecordmanName") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Literal ID="ltMaintainObject" runat="server" Text='<%#Eval("RecordObject") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Literal ID="ltMaintainResult" runat="server" Text='<%#Eval("RecordResult") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltMaintainVerified" runat="server" Text='<%#Eval("VerifiedResultString") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltMaintainRemark" runat="server" Text='<%#Eval("VerifyRemark") %>'></asp:Literal>
                                    </td>
                                </tr>
                                <tr style="display: none" id="MaintainHistoryTable_<%#Container.ItemIndex %>">
                                    <td colspan="6">
                                        <table width="100%">
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="lbMaintainSystem" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    子系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbMaintainSubsystem" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    保养项目：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbMaintainPlanObject" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    保养周期：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="lbMaintainPlanPeriod" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    保养内容：
                                                </td>
                                                <td colspan="3" class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbMaintainPlanContent" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    验收标准：
                                                </td>
                                                <td colspan="3" class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbMaintainCheckStandard" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager2_PageChanged"
                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                        NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                        ShowCustomInfoSection="Left" CloneFrom="">
                    </cc1:AspNetPager>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel4" runat="server" HeaderText="例行检测记录">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse; text-align: center">
                        <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                            <td>
                                检测时间
                            </td>
                            <td>
                                检测人
                            </td>
                            <td>
                                检测项目
                            </td>
                            <td>
                                实际检测结果
                            </td>
                            <td>
                                审核结果
                            </td>
                            <td>
                                审核备注
                            </td>
                        </tr>
                        <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                            <ItemTemplate>
                                <tr style="height: 30px">
                                    <td onclick='javascript:HideRecord("InspectHistoryTable_"+<%#Container.ItemIndex %>);'>
                                        <asp:Literal ID="ltInspectRecordID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false"></asp:Literal><asp:Literal
                                            ID="ltInspectEquipmentName" runat="server" Text='<%#Convert.ToDateTime(Eval("RecordDate")).ToString("yyyy-MM-dd") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltInspectmanName" runat="server" Text='<%#Eval("RecordmanName") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Literal ID="ltInspectObject" runat="server" Text='<%#Eval("RecordObject") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Literal ID="ltInspectResult" runat="server" Text='<%#Eval("RecordResult") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltInspectVerified" runat="server" Text='<%#Eval("VerifiedResultString") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltInspectRemark" runat="server" Text='<%#Eval("VerifyRemark") %>'></asp:Literal>
                                    </td>
                                </tr>
                                <tr style="display: none" id="InspectHistoryTable_<%#Container.ItemIndex %>">
                                    <td colspan="6">
                                        <table width="100%">
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="lbInspectSystem" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    子系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbInspectSubsystem" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    检测项目：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbInspectPlanObject" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    检测周期：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="lbInspectPlanPeriod" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    检测内容：
                                                </td>
                                                <td colspan="3" class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbInspectPlanContent" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    验收标准：
                                                </td>
                                                <td colspan="3" class="table_none table_none_NoWidth">
                                                    <asp:Label ID="lbInspectCheckStandard" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <cc1:AspNetPager ID="AspNetPager3" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager3_PageChanged"
                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                        NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                        ShowCustomInfoSection="Left" CloneFrom="">
                    </cc1:AspNetPager>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>

        <script type="text/javascript" language="javascript">
        function HideRecord(objId) {
            obj = $get(objId);
            if (obj == null || obj == "undefined")
                return;

            if (obj.style.display != "none")
                obj.style.display = "none";
            else obj.style.display = "inline";
        }
        </script>
</asp:Content>
