<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_FeeBackFill_SignForm, App_Web_wcybvyp3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库检查表签名"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：处理意见填写">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="仓库检查表列表" ButtonUrl="WareHouseList.aspx" ButtonUrlType="Href" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <table width="95%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr>
                <td colspan="5" style="font-family: 宋体; font-size: medium; text-align: center" class="table_body">
                    <b style="font-family: 宋体; font-size: medium">
                        <asp:Label ID="lbCompanyName" runat="server" Text="Label"></asp:Label>公路有限公司<br />
                        <asp:Label ID="lbWareHouse" runat="server" Text="Label"></asp:Label>仓库检查表</b>
                </td>
                <td style="font-family: 宋体; text-align: center" class="table_body">
                    <b style="font-size: medium">表单编号</b>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" class="table_body table_body_NoWidth">
                    检查日期
                </td>
                <td class="table_none table_none_NoWidth" colspan="3">
                    &nbsp;
                    <asp:Label ID="lbCheckDate" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center" class="table_body table_body_NoWidth">
                    编号
                </td>
                <td class="table_none table_none_NoWidth">
                    &nbsp;
                    <asp:Label ID="lbFormNO" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="8" style="text-align: center" class="table_body">
                    检查情况
                </td>
                <td colspan="5" style="text-align: left" class="table_none table_none_NoWidth">
                    <table border="0" width="100%">
                        <tr>
                            <td style="width: 80px">
                                材料类型：
                            </td>
                            <td>
                                <asp:CheckBoxList ID="cblMaterialType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">机电</asp:ListItem>
                                    <asp:ListItem Value="2">监控</asp:ListItem>
                                    <asp:ListItem Value="3">消防</asp:ListItem>
                                    <asp:ListItem Value="4">其它</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: left" class="table_none table_none_NoWidth">
                    &nbsp;检查人员：
                    <asp:Label ID="lbChecker" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                    检查项目
                </td>
                <td style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                    抽查：
                </td>
                <td style="text-align: left" colspan="3">
                    &nbsp;&nbsp;
                    <asp:Label ID="lbSpotCheck" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                    盘点：
                </td>
                <td style="text-align: left" colspan="3">
                    &nbsp;&nbsp;
                    <asp:Label ID="lbStockCount" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="5">
                    <table border="0" width="100%">
                        <tr>
                            <td style="width: 90px">
                                数量情况：
                            </td>
                            <td align="left">
                                <asp:CheckBoxList ID="cblQuantitySituation" runat="server" RepeatDirection="Horizontal"
                                    title="请选择数量情况~!">
                                    <asp:ListItem Value="1">盘盈</asp:ListItem>
                                    <asp:ListItem Value="2">盘亏</asp:ListItem>
                                    <asp:ListItem Value="3">正常</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="5">
                    <table border="0" width="100%">
                        <tr>
                            <td style="width: 90px">
                                质量情况：
                            </td>
                            <td align="left">
                                <asp:CheckBoxList ID="cblQuality" runat="server" RepeatDirection="Horizontal" title="请选择质量情况~!">
                                    <asp:ListItem Value="1">正常</asp:ListItem>
                                    <asp:ListItem Value="2">有损耗</asp:ListItem>
                                    <asp:ListItem Value="3">已损耗</asp:ListItem>
                                    <asp:ListItem Value="4">其它</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="5">
                    <table border="0" width="100%">
                        <tr>
                            <td style="width: 90px">
                                表单登记情况：
                            </td>
                            <td align="left">
                                <asp:CheckBoxList ID="cblRegSituation" runat="server" RepeatDirection="Horizontal"
                                    title="请选择表单登记情况~!" onclick="javascript:SelectChange(this);">
                                    <asp:ListItem Value="1">规范</asp:ListItem>
                                    <asp:ListItem Value="2">不规范</asp:ListItem>
                                    <asp:ListItem Value="3">需改进</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 317px; text-align: center" class="table_body_WithoutWidth" colspan="3">
                    有无异常情况
                </td>
                <td colspan="2" align="left">
                    &nbsp;&nbsp;
                    <asp:Label ID="lbExceptionSituation" runat="server" Text="Label"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 317px; text-align: center" class="table_body_WithoutWidth" rowspan="3">
                    检查后处理意见
                </td>
                <td align="center" class="table_body_WithoutWidth">
                    数量情况：
                </td>
                <td colspan="4" align="left">
                    <table border="0" width="95%">
                        <tr>
                            <td style="padding-left:5px;">
                                <asp:Label ID="lbQuantityFeeBack" runat="server" Text="Label"></asp:Label>
                                <asp:TextBox ID="tbQuantityFeeBack" runat="server" TextMode="MultiLine" Rows="3"
                                    Style="width: 98%" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top;padding-left:5px;">
                                <asp:Label ID="lbQuantityConfirmer" runat="server" Text="Label"></asp:Label>
                                <div id="divQuantitySign" runat="server" visible="false">
                                    用户名：<asp:TextBox ID="tbQuantityUser" runat="server"></asp:TextBox>
                                    密码：<asp:TextBox ID="tbQuantityPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:Button ID="btSignQuantity" runat="server" Text="签名" class="button_bak" 
                                        onclick="btSignQuantity_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="table_body_WithoutWidth">
                    质量情况：
                </td>
                <td colspan="4" align="left">
                    <table border="0" width="95%">
                        <tr>
                            <td style="padding-left:5px;">
                                <asp:Label ID="lbQualityFeeBack" runat="server" Text="Label"></asp:Label>
                                <asp:TextBox ID="tbQualityFeeBack" runat="server" TextMode="MultiLine" Rows="3" Style="width: 98%"
                                    Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-left:5px;">
                                <asp:Label ID="lbQualityConfirmer" runat="server" Text="Label"></asp:Label>
                                <div id="divQualitySign" runat="server" visible="false">
                                    用户名：<asp:TextBox ID="tbQualityUser" runat="server"></asp:TextBox>
                                    密码：<asp:TextBox ID="tbQualityPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:Button ID="btQualitySign" runat="server" Text="签名" class="button_bak" 
                                        onclick="btQualitySign_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="table_body_WithoutWidth">
                    表单登记情况：
                </td>
                <td colspan="4" align="left">
                    <table border="0" width="95%">
                        <tr>
                            <td style="padding-left:5px;">
                                <asp:Label ID="lbRegFeeBack" runat="server" Text="Label"></asp:Label>
                                <asp:TextBox ID="tbRegFeeBack" runat="server" TextMode="MultiLine" Rows="3" Style="width: 98%"
                                    Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-left:5px;">
                                <asp:Label ID="lbRegConfirmer" runat="server" Text="Label"></asp:Label>
                                <div id="divRegSign" runat="server" visible="false">
                                    用户名：<asp:TextBox ID="tbRegUser" runat="server"></asp:TextBox>
                                    密码：<asp:TextBox ID="tbRegPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:Button ID="btRegSign" runat="server" Text="签名" class="button_bak" 
                                        onclick="btRegSign_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth">
                </td>
                <td align="center" class="table_body_WithoutWidth">
                    其它意见：
                </td>
                <td colspan="4" align="left">
                    <table border="0" width="95%">
                        <tr>
                            <td style="padding-left:5px;">
                                <asp:Label ID="lbOtherFeeBack" runat="server" Text="Label"></asp:Label>
                                <asp:TextBox ID="tbOtherFeeBack" runat="server" TextMode="MultiLine" Rows="3" Style="width: 98%"
                                    Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-left:5px;">
                                <asp:Label ID="lbOtherConfirmer" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="lbOtherConfirmTime" runat="server" Text="Label"></asp:Label>
                                <div id="divOtherSign" runat="server" visible="false">
                                    用户名：<asp:TextBox ID="tbOtherUser" runat="server"></asp:TextBox>
                                    密码：<asp:TextBox ID="tbOtherPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:Button ID="btOtherSign" runat="server" Text="签名" class="button_bak" 
                                        onclick="btOtherSign_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table_body_WithoutWidth">
                    结果确认人：
                </td>
                <td align="center" class="table_body_WithoutWidth">
                    审核人：
                </td>
                <td colspan="4" align="left" style="padding-left:5px;">
                    <asp:Label ID="lbResultConfirmer" runat="server" Text="Label"></asp:Label>
                    <div id="divResultSign" runat="server" visible="false">
                        用户名：<asp:TextBox ID="tbResultUser" runat="server"></asp:TextBox>
                        密码：<asp:TextBox ID="tbResultPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Button ID="btResultSign" runat="server" Text="签名" class="button_bak" 
                            onclick="btResultSign_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
