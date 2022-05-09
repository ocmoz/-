<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="BorrowRecord.aspx.cs" Inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowRecord_BorrowRecord" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="借出设备登记"
        HeadHelpTxt="默认显示最近通过申请" HeadOPTxt="目前操作功能：最近申请列表">
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="借调申请列表" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"
                             OnRowDataBound="GridView1_RowDataBound" Width="100%">
                            <EmptyDataTemplate>
                                没有审批通过的借调申请
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="SheetName" HeaderText="申请单编号" />
                                <asp:BoundField DataField="BorrowCompanyName" HeaderText="申请方" />
                                <asp:BoundField DataField="ApplicantName" HeaderText="申请人" />
                                <asp:BoundField DataField="SubmitTime" HeaderText="申请时间" />
                                <asp:BoundField DataField="StatusString" HeaderText="状态" />
                                <asp:TemplateField HeaderText="借出登记" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_Borrow" runat="server" CausesValidation="false" 
                                          ImageUrl="~/images/ICON/move.gif" NavigateUrl='<%# "RecordOutEquipment.aspx?cmd=add&id=" + Eval("BorrowApplyID") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px" />
                                </asp:TemplateField>
                               
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="我的借出登记" ID="TabPanel3">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                             OnRowDataBound="GridView2_RowDataBound">
                            <EmptyDataTemplate>
                                没有借出登记信息
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码" />
                                <asp:BoundField DataField="BorrowCompanyName" HeaderText="借用方" />
                                <asp:BoundField DataField="BorrowerName" HeaderText="领用人" />
                                <asp:BoundField DataField="Reason" HeaderText="借用原因" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="BorrowTime" HeaderText="借出时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="false"  />
                                <asp:BoundField DataField="ReturnDate" HeaderText="归还期限" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" />
                                 <asp:TemplateField>
                                <HeaderTemplate>
                                    是否已归还</HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("IsReturned")) ? "是" : "否"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="查看详情" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_Borrow" runat="server" CausesValidation="false" 
                                          ImageUrl="~/images/ICON/select.gif" NavigateUrl='<%# "ViewBorrowRecord.aspx?cmd=view&id=" + Eval("BorrowApplyID") +"&equipmentNO="+Eval("EquipmentNO")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px" />
                                </asp:TemplateField>
                                
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                         <cc1:AspNetPager ID="AspNetPager2" runat="server" OnPageChanged="AspNetPager2_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="查询" ID="TabPanel2">
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
                                    查询对象：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:RadioButtonList ID="rblSearchObject" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">申请列表</asp:ListItem>
                                        <asp:ListItem Value="1">最近借出登记</asp:ListItem>
                                    </asp:RadioButtonList>
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
                                    申请方：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlBorrowCompany" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="applySearch" style="display:<%=(bShowRecordSearch?"none":"inline")%>">
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    申请人：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbApplicant" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    申请时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbApplyDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入申请时间~date"></asp:TextBox>
                                    &nbsp;至&nbsp;
                                    <asp:TextBox ID="tbApplyDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入申请时间~date"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="recordSearch1" style="display:<%=(bShowRecordSearch?"inline":"none")%>">
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    领用人：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbBorrower" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    借出时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbBorrowTimeFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入申请时间~date"></asp:TextBox>
                                    &nbsp;至&nbsp;
                                    <asp:TextBox ID="tbBorrowTimeTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入申请时间~date"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="recordSearch2" style="display:<%=(bShowRecordSearch?"inline":"none")%>">
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    归还时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:TextBox ID="tbReturnDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入归还时间~date"></asp:TextBox>
                                    &nbsp;至&nbsp;
                                    <asp:TextBox ID="tbReturnDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入归还时间~date"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                      <center>
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                              </center>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
        <script type="text/javascript" language="javascript">

        function SetVisible(obj) {
              if(obj.value=="0")
              {
                  $get("applySearch").style.display = "inline";
                  $get("recordSearch1").style.display = "none";
                  $get("recordSearch2").style.display = "none";
              }
              if(obj.value=="1")
              {
                  $get("applySearch").style.display = "none";
                  $get("recordSearch1").style.display = "inline";
                  $get("recordSearch2").style.display = "inline";                  
              }
        }
    </script>
</asp:Content>
