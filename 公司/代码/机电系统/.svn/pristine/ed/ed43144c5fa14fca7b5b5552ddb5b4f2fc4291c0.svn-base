<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="DailyPatrolTrack.aspx.cs" Inherits="Module_FM2E_MaintainManager_DailyPatrolManager_DailyPatrolView_DailyPatrolTrack"
    Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="实际日常巡查信息">
                    <ContentTemplate>
                        <table width="880px" style="text-align: center">
                            <tr align="center">
                                <td class="Table_searchtitle">
                                    当前日常巡查计划明细
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    子系统：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    巡查周期：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    巡查项目：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    巡查内容：
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    验收标准：
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr align="center">
                                <td class="Table_searchtitle">
                                    计划进度信息
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RecordDateString" HeaderText="计划巡查时间范围"></asp:BoundField>
                                            <asp:BoundField DataField="RecordDateString" HeaderText="实际巡查时间"></asp:BoundField>
                                            <asp:BoundField DataField="RecordResultString" HeaderText="实际巡查结果"></asp:BoundField>
                                            <asp:BoundField DataField="RecordmanName" HeaderText="巡查人"></asp:BoundField>
                                            <asp:BoundField DataField="VerifyName" HeaderText="审核人"></asp:BoundField>
                                            <asp:BoundField DataField="VerifiedResultString" HeaderText="结果"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            没有计划明细信息
                                        </EmptyDataTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:GridView>
                                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                        NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                        ShowCustomInfoSection="Left" CloneFrom="" Width="800px">
                                    </cc1:AspNetPager>
                                </td>
                            </tr>
                            <tr align="right">
                                <td>
                                    <asp:Button ID="btnReport1" runat="server" CssClass="button_bak" Text="分布报表" OnClick="btnReport1_Click" />
                                    <asp:Button ID="btnReport2" runat="server" CssClass="button_bak" Text="跟踪报表" OnClick="btnReport2_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="日常巡查执行情况">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" DisplayGroupTree="False" runat="server"
                                AutoDataBind="true" PrintMode="ActiveX" />
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <input class="button_bak" type="button" value="确定" onclick="javascript:onOK();" />
        <input class="button_bak" type="button" value="关闭" onclick="javascript:window.parent.hidePopWin(false);" />
    </div>

    <script type="text/javascript" language="javascript">
       function onOK() {
       window.returnVal = null;
       window.parent.hidePopWin(true);
        }
    </script>

</asp:Content>
