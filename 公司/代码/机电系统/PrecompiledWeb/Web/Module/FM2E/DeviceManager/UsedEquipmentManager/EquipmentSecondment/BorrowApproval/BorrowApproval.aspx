<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApproval_BorrowApproval, App_Web_lxvpklpb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备借调审批"
        HeadHelpTxt="默认显示最近申请" HeadOPTxt="目前操作功能：设备借调申请与审批列表">
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="设备借调申请" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                             OnRowDataBound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有设备借调申请
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="BorrowApplyID" HeaderText="申请单ID" Visible="False" />
                                <asp:BoundField DataField="SheetName" HeaderText="申请单编号" />
                                <asp:BoundField DataField="BorrowCompanyName" HeaderText="申请方" />
                                <asp:BoundField DataField="ApplicantName" HeaderText="申请人" />
                                <asp:BoundField DataField="SubmitTime" HeaderText="申请时间" 
                                    DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False" />
                                <asp:BoundField DataField="StatusString" HeaderText="状态" />
                                <asp:TemplateField HeaderText="审批" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_Approval" runat="server" CausesValidation="false" 
                                          ImageUrl="~/images/ICON/Approval.gif" NavigateUrl='<%# "Approval.aspx?cmd=approval&id=" + Eval("BorrowApplyID") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px" />
                                </asp:TemplateField>
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
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="我的历史审批">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="GridView2_RowDataBound">
                            <EmptyDataTemplate>
                                没有历史审批记录
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="SheetNO" HeaderText="申请单编号" />
                                <asp:BoundField DataField="BorrowCompanyName" HeaderText="申请方" />
                                <asp:BoundField DataField="ApplicantName" HeaderText="申请人" />
                                <asp:BoundField DataField="BorrowCompanyName" HeaderText="申请时间"  DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false" />
                                <asp:BoundField DataField="StatusString" HeaderText="状态" />
                                <asp:BoundField DataField="ApprovalDate" HeaderText="审批时间"  DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false" />
                                   <asp:TemplateField HeaderText="审批结果">
                                    <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("ApprovalResult"))?"通过":"不通过" %>
                                    </ItemTemplate>
                                     <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="查看" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" CausesValidation="false" 
                                          ImageUrl="~/images/ICON/select.gif" NavigateUrl='<%# "Approval.aspx?cmd=view&id=" + Eval("BorrowApplyID") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px" />
                                </asp:TemplateField>
                              
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager2" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="查询审批历史">
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
                            <tr>
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
                                        title="请输入申请时间~date"></asp:TextBox>&nbsp;至&nbsp;
                                    <asp:TextBox ID="tbApplyDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入申请时间~date"></asp:TextBox>
                                </td>
                            </tr>
                            <tr  id="resultTr">
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    审批时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbApprovalDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入审批时间~date"></asp:TextBox>
                                    &nbsp; 至&nbsp;
                                    <asp:TextBox ID="tbApprovalDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入审批时间~date"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">审批结果：</td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                 <asp:DropDownList ID="ddlResult" runat="server">
                                            <asp:ListItem Text="不限" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="审批不通过" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="审批通过" Value="1"></asp:ListItem>
                                    </asp:DropDownList></td>
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

//        function SetVisible(obj) {
//              if(obj.value=="0")
//              {
//               $get("resultTr").style.display = "none";
//              }
//              if(obj.value=="1")
//              {
//               $get("resultTr").style.display = "inline";
//              }
//        }
    </script>
</asp:Content>
