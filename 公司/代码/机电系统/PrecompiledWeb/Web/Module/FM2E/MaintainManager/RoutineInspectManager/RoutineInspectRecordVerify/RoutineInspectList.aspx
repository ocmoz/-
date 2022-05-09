<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_MaintainManager_RoutineInspectManager_RoutineInspectRecordVerify_RoutineInspectList, App_Web_skqzczub" title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="例行检测过程信息审核"
        HeadOPTxt="目前操作功能：过程信息审核">
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 900px">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="过程信息列表" ID="TabPanel1">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse; text-align: center">
                                <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                    <td>
                                        选择
                                    </td>
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
                                        审核
                                    </td>
                                    <td>
                                        审核备注
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                    <ItemTemplate>
                                        <tr style="height: 30px">
                                            <td id="<%#Container.ItemIndex %>">
                                                <input id="Checkbox1" runat="server" type="checkbox" onclick="selected(this);" /><asp:Literal
                                                    ID="ltRecordID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false"></asp:Literal>
                                            </td>
                                            <td onclick='javascript:HideRecord("RecordTable_"+<%#Container.ItemIndex %>);'>
                                                <asp:Literal ID="ltEquipmentName" runat="server" Text='<%#Convert.ToDateTime(Eval("RecordDate")).ToString("yyyy-MM-dd") %>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="ltModel" runat="server" Text='<%#Eval("RecordmanName") %>'></asp:Literal>
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Literal ID="ltCount" runat="server" Text='<%#Eval("RecordObject") %>'></asp:Literal>
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Literal ID="ltUnit" runat="server" Text='<%#Eval("RecordResult") %>'></asp:Literal>
                                            </td>
                                            <td id="td5_<%#Container.ItemIndex %>" style="display: none">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                                    <asp:ListItem Selected="True">按计划执行</asp:ListItem>
                                                    <asp:ListItem>未按计划执行</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td id="td6_<%#Container.ItemIndex %>" style="display: none">
                                                <asp:TextBox ID="tbVerifyRemark" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none" id="RecordTable_<%#Container.ItemIndex %>">
                                            <td>
                                                <%#Container.ItemIndex+1 %>
                                            </td>
                                            <td colspan="6">
                                                <table width="100%">
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                                            系统：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                                            <asp:Label ID="lbSystem" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="table_body table_body_NoWidth">
                                                            子系统：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbSubsystem" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth">
                                                            检测项目：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbRecordObject" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                                            检测周期：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                                            <asp:Label ID="lbRecordPeriod" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth">
                                                            检测内容：
                                                        </td>
                                                        <td colspan="3" class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbRecordContent" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth">
                                                            验收标准：
                                                        </td>
                                                        <td colspan="3" class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbCheckStandard" runat="server" Text="Label"></asp:Label>
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
                            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                                runat="server">
                                <tr>
                                    <td align="right" style="height: 38px">
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                        <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="审核历史" ID="TabPanel2">
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
                                <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                    <ItemTemplate>
                                        <tr style="height: 30px">
                                            <td onclick='javascript:HideRecord("HistoryTable_"+<%#Container.ItemIndex %>);'>
                                                <asp:Literal ID="Literal3" runat="server" Text='<%#Eval("RecordID") %>' Visible="false"></asp:Literal><asp:Literal
                                                    ID="ltEquipmentName" runat="server" Text='<%#Convert.ToDateTime(Eval("RecordDate")).ToString("yyyy-MM-dd") %>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="ltModel" runat="server" Text='<%#Eval("RecordmanName") %>'></asp:Literal>
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Literal ID="ltCount" runat="server" Text='<%#Eval("RecordObject") %>'></asp:Literal>
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Literal ID="ltUnit" runat="server" Text='<%#Eval("RecordResult") %>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("VerifiedResultString") %>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="Literal2" runat="server" Text='<%#Eval("VerifyRemark") %>'></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr style="display: none" id="HistoryTable_<%#Container.ItemIndex %>">
                                            <td colspan="6">
                                                <table width="100%">
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                                            系统：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                                            <asp:Label ID="lbSystem" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="table_body table_body_NoWidth">
                                                            子系统：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbSubsystem" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth">
                                                            检测项目：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbRecordObject" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                                            检测周期：
                                                        </td>
                                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                                            <asp:Label ID="lbRecordPeriod" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth">
                                                            检测内容：
                                                        </td>
                                                        <td colspan="3" class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbRecordContent" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="table_body table_body_NoWidth">
                                                            验收标准：
                                                        </td>
                                                        <td colspan="3" class="table_none table_none_NoWidth">
                                                            <asp:Label ID="lbCheckStandard" runat="server" Text="Label"></asp:Label>
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
                                ShowCustomInfoSection="Left" CloneFrom="" Width="880px">
                            </cc1:AspNetPager>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function HideRecord(objId) {
            obj = $get(objId);
            if (obj == null || obj == "undefined")
                return;

            if (obj.style.display != "none")
                obj.style.display = "none";
            else obj.style.display = "inline";
        }
        function selected(obj)
        {
            var id = obj.parentElement.id;
            var td5 = document.all('td5_'+id);
            var td6 = document.all('td6_'+id);
            if(obj.checked)
            {
                td5.style.display = "inline";
                td6.style.display = "inline";
            }
            else
            {
                td5.style.display = "none";
                td6.style.display = "none";
            }
        }
    </script>

</asp:Content>
