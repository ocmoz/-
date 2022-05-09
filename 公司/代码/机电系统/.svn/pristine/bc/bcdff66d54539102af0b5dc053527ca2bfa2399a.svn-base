<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ScrapApply.aspx.cs" Inherits="Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapApply_ScrapApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="报废申请"
        HeadHelpTxt="" HeadOPTxt="目前操作功能：报废申请单列表">
        <cc1:HeadMenuButtonItem ButtonName="申请报废" ButtonIcon="new.gif" ButtonUrlType="Href"
            ButtonPopedom="New" ButtonUrl="EditScrapApply.aspx?cmd=add" />    
    </cc1:HeadMenuWebControls>
    <div style="width:900px;height:300px;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <cc2:TabPanel runat="server" HeaderText="我的设备借调申请" ID="TabPanel1">
                        <HeaderTemplate>
                            我的报废借调申请
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                    <EmptyDataTemplate>
                                        没有报废借调申请
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="SheetName" HeaderText="报废单编号" />
                                        <asp:BoundField DataField="DepName" HeaderText="部门" />
                                        <asp:BoundField DataField="StatusString" HeaderText="状态" />
                                        <asp:BoundField DataField="ApplyDate" HeaderText="申请时间" />
                                        <asp:BoundField DataField="ApplicantName" HeaderText="申请人" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                            CommandName="view">
                                            <HeaderStyle Width="60px" />
                                        </asp:ButtonField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="60px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此模块信息吗？')"
                                                    CausesValidation="false" Visible='<%#Convert.ToInt32(Eval("Status"))!=2?false:true%>' />
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
                                            报废单编号：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            申请单状态：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:DropDownList ID="DropDownList2" runat="server">
                                                <asp:ListItem Text="不限" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="草稿" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="等待审批结果" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="审批通过" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="审批不通过" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="报废完成" Value="32"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            部门：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:DropDownList ID="DropDownList3" runat="server">
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
</asp:Content>