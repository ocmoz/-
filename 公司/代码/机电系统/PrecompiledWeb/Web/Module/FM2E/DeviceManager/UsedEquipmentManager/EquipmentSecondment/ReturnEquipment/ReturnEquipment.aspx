<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_ReturnEquipment_ReturnEquipment, App_Web_nri2mwfp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备归还验收"
        HeadHelpTxt="默认显示最近归还设备" HeadOPTxt="目前操作功能：设备归还验收列表">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="归还验收设备" ButtonPopedom="New" ButtonUrl="ReturnAcceptance.aspx?cmd=add" ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="最近验收记录" ID="TabPanel1">
            <ContentTemplate>
             <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有归还设备的验收记录
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码" />
                                <asp:BoundField DataField="EquipmentName" HeaderText="设备名称" />
                                <asp:BoundField DataField="Model" HeaderText="规格型号" />
                                 <asp:TemplateField>
                                <HeaderTemplate>
                                    验收结果</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("Result")) ? "<span style='color:#00cc00'>验收通过</span>" : "<span style='color:#ff0000'>验收不通过</span>"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="ReturnDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" HeaderText="验收日期" />
                                <asp:BoundField DataField="ReturnerName" HeaderText="归还人" />
                                <asp:BoundField DataField="ReturnCompanyName" HeaderText="归还公司" />
                                 <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="{0}"
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
            </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="查询">
            <ContentTemplate>
             <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
              <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    组合查询（支持模糊查询）
                                </td>
                            </tr>
                             <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    设备条形码：</td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbEquipmentNO" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    设备名称：</td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbEquipmentName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                    申请单编号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbSheetNO" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    归还公司： 
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlReturnCompany" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    验收结果：</td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlResult" runat="server">
                                    <asp:ListItem Value="3" Text="不限"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="验收不通过"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="验收通过"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    验收日期：</td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbCheckDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入验收日期~date"></asp:TextBox>
                                    &nbsp;至 
                                    <asp:TextBox ID="tbCheckDateTo" runat="server"  class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入验收日期~date"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                              <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                                    <tr>
                                        <td align="right" style="height: 38px">
                                            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                            <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                        </td>
                                    </tr>
                                </table>
             </div>
            </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

