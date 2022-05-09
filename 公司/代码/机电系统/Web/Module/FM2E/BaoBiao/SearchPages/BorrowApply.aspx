<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="BorrowApply.aspx.cs" Inherits="Module_FM2E_ArchivesManager_SearchPages_BorrowApply" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备借调档案信息查询"
        HeadOPTxt="目前操作功能：档案信息查询">
                <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回申请表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; height: 300px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" e>
                    <cc2:TabPanel runat="server" HeaderText="设备借调单" ID="TabPanel1">
                        <HeaderTemplate>
                            设备借调单
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                    <EmptyDataTemplate>
                                        没有设备借调单
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkBox1" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <input id="CheckAll" runat="server" type="checkbox" onclick="selectAll(this);" />本页全选
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BorrowApplyID" HeaderText="申请单ID" />
                                        <asp:BoundField DataField="SheetName" HeaderText="申请单编号" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="借出方" />
                                        <asp:BoundField DataField="StatusString" HeaderText="状态" />
                                        <asp:BoundField DataField="SubmitTime" HeaderText="申请时间" />
                                        <asp:TemplateField HeaderText="查看">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/select.gif"
                                                    CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false"
                                                    Visible='<%#IsBorrowed(Convert.ToInt64(Eval("BorrowApplyID")))==true?true:false%>' />
                                            </ItemTemplate>
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
                            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="Table1"
                                runat="server">
                                <tr>
                                    <td align="right" style="height: 38px">
                                        <asp:Label ID="lbErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:Button ID="BtnBorrow" runat="server" CssClass="button_bak" Text="申请借阅" OnClick="BtnBorrow_Click" />
                                        <asp:Button ID="BtnDestroy" runat="server" CssClass="button_bak" Text="申请销毁" OnClick="BtnDestroy_Click" />
                                        <input type="hidden" runat="server" id="addstring" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="查询" ID="TabPanel2">
                        <HeaderTemplate>
                            查询
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" colspan="4">
                                            组合查询（支持模糊查询）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            申请单名：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            借出方：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            申请单状态：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:DropDownList ID="DropDownList2" runat="server">
                                                <asp:ListItem Text="不限" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="草稿" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="等待审批结果" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="审批通过" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="审批不通过" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="已借出" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            提交时间：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="TextBox2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                                title="请输入提交时间~date"></asp:TextBox>&nbsp;至&nbsp;<asp:TextBox ID="TextBox3" runat="server"
                                                    class="input_calender" onfocus="javascript:HS_setDate(this);" title="请输入提交时间~date"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                                    runat="server">
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">
        function selectAll(obj) {
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;

            for (i = 0; i < theTable.rows.length; i++) {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
            }
        }
    </script>

</asp:Content>
