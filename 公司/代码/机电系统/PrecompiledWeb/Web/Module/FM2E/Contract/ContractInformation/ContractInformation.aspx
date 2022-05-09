<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_Contract_ContractInformation_ContractInformation, App_Web_nscfl5lv" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Contract" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="合同基本信息管理"
        HeadHelpTxt="查看合同基本信息" HeadOPTxt="目前操作功能：合同基本信息列表">
        <cc1:HeadMenuButtonItem ButtonName="新增合同基本信息" ButtonIcon="new.gif" ButtonUrlType="Href" ButtonPopedom="New"
            ButtonUrl="EditContractInformation.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel ID="TabPanel1" HeaderText="合同基本信息" runat="server">
                <ContentTemplate>
                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table  width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                             <tr>
                                 <td class="table_body table_body_NoWidth" style="width: 265px">
                                     合同编号：<asp:TextBox ID="tb_search_ContractNo" runat="server"></asp:TextBox>
                                 </td>
                                 <td class="table_body table_body_NoWidth" style="width: 265px">
                                     合同名称：<asp:TextBox ID="tb_search_ContractName" runat="server"></asp:TextBox>
                                 </td>
		                        <td class="table_body table_body_NoWidth" style="width: 350px">
		                            期数：<asp:TextBox ID="tb_search_Period" runat="server"></asp:TextBox>
		                        <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="button_bak" OnClick="btSearch_Click"/>
		                        </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" 
                            AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
                            onrowdatabound="GridView1_RowDataBound">
                            <EmptyDataTemplate>
                                没有合同基本信息
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="系统编码"  Visible="false"/>
                                <asp:BoundField DataField="ContractNo" HeaderText="合同编号" />
                                <asp:BoundField DataField="ContractName" HeaderText="合同名称" />
                                <asp:BoundField DataField="ContractedUnits" HeaderText="签约单位" />
                                 <asp:BoundField DataField="Retentions" HeaderText="质保金额" />
                                <asp:BoundField DataField="SettlementAmount" HeaderText="结算金额" />
                                <asp:BoundField DataField="ContractPeople" HeaderText="联系人" />
                                <asp:BoundField DataField="ContractTheWay" HeaderText="联系方式" />
                               
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/ICON/select.gif" HeaderText="查看"
                                    CommandName="view">
                                    <HeaderStyle Width="60px" />
                                </asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"  OnClientClick="return confirm('确认要删除此模块信息吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </div>
                     <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>

