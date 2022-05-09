<%@ Page Title="查看专项工程" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="ViewProject.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_ViewProject" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        function HideShowForm(a) {
            var fm = document.getElementById(a.id.replace('a_hideshow', 'tr_form'));
            if (fm.style.display == 'block') {
                a.innerHTML = a.innerHTML.replace('收缩', '展开');
                fm.style.display = 'none';
            }
            else {
                a.innerHTML = a.innerHTML.replace('展开', '收缩');
                fm.style.display = 'block';
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：施工信息查看" HeadHelpTxt="点击各项进入对应工作信息的维护">
            <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="施工计划" ButtonUrlType="Href"
            ButtonUrl="EditPlan.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
            <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="设备计划" ButtonUrlType="Href"
            ButtonUrl="EditDevice.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
             <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="支付计划" ButtonUrlType="Href"
            ButtonUrl="EditPay.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
             <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="进度检查" ButtonUrlType="Href"
            ButtonUrl="CheckProgress.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
            <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="设备进场" ButtonUrlType="Href"
            ButtonUrl="DeviceIn.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
            <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="支付" ButtonUrlType="Href"
            ButtonUrl="Pay.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
            <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="工程变更" ButtonUrlType="Href"
            ButtonUrl="ModifyList.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
             <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回工程列表" ButtonUrlType="Href"
            ButtonUrl="SpecialProjectList.aspx?cmd=list&projectid=" ButtonPopedom="List" />
    </cc1:HeadMenuWebControls>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="5" 
        Width="100%">

                 
                
 
        <cc2:TabPanel runat="server" HeaderText="工程基本信息" ID="TabPanel1">
            <ContentTemplate>
                <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            专项工程可行性报告
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            项目名称
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_ProjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            提交人
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="Label_Submitter" runat="server"></asp:Label>
                        </td>
                        <td class="Table_searchtitle" style="width: 15%">
                            提交时间
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            项目状态
                        </td>
                        <td>
                            <asp:Label ID="Label_Status" runat="server"></asp:Label>
                        </td>
                        <td class="Table_searchtitle" style="width: 15%">
                            最后更新
                        </td>
                        <td>
                            <asp:Label ID="Label_UpdateTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            预算来源
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_BudgetName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            大概预算
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_Budget" runat="server"></asp:Label>万元
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%" rowspan="2">
                            系统现状
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_CurrentStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            附件：
                            <asp:HyperLink ID="HyperLink_CurrentStatusFile" Font-Underline="true" ForeColor="Blue"
                                runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%" rowspan="2">
                            存在的问题
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_Problem" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            附件：
                            <asp:HyperLink ID="HyperLink_ProblemFile" Font-Underline="true" ForeColor="Blue"
                                runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%" rowspan="2">
                            拟解决的技术方案
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_Solution" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            附件：
                            <asp:HyperLink ID="HyperLink_SolutionFile" Font-Underline="true" ForeColor="Blue"
                                runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <th class="Table_searchtitle">
                            完工
                        </th>
                        <td colspan="3">
                            <div id="div_completeinfo" runat="server">
                                <asp:Label ID="Label_Complete" runat="server"></asp:Label>
                                <br />
                                备注：<asp:Label ID="Label_CompleteRemark" runat="server"></asp:Label>
                                <br />
                                <span style="float: right;">负责人：
                                    <asp:Label ID="Label_CompleteApprovaler" runat="server"></asp:Label>
                                    <br />
                                    日期：<asp:Label ID="Label_CompleteDate" runat="server"></asp:Label>
                                </span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th class="Table_searchtitle">
                            交工
                        </th>
                        <td colspan="3">
                            <div id="div_passinfo" runat="server">
                                <asp:Label ID="Label_Pass" runat="server"></asp:Label>
                                <br />
                                备注：<asp:Label ID="Label_PassRemark" runat="server"></asp:Label>
                                <br />
                                <span style="float: right;">负责人：
                                    <asp:Label ID="Label_PassApprovaler" runat="server"></asp:Label>
                                    <br />
                                    日期：<asp:Label ID="Label_PassDate" runat="server"></asp:Label>
                                </span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th class="Table_searchtitle">
                            竣工
                        </th>
                        <td colspan="3">
                            <div id="div_finishinfo" runat="server">
                                <asp:Label ID="Label_Finish" runat="server"></asp:Label>
                                <br />
                                备注：<asp:Label ID="Label_FinishRemark" runat="server"></asp:Label>
                                <br />
                                <span style="float: right;">负责人：
                                    <asp:Label ID="Label_FinishApprovaler" runat="server"></asp:Label>
                                    <br />
                                    日期：<asp:Label ID="Label_FinishDate" runat="server"></asp:Label>
                                </span>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                
                <table id="Table6" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr id="tr1" runat="server">
                        <td id="Td1" class="Table_searchtitle" runat="server">
                            审批历史记录
                        </td>
                    </tr>
                    <tr id="tr2" runat="server">
                        <td id="Td2" runat="server">
                            <asp:GridView ID="gridview_ApprovalList" runat="server" 
                                AutoGenerateColumns="False"  EnableViewState="False"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批项">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalName" runat="server" Text='<%# Eval("ApprovalName") %>'></asp:Label>
                                        </ItemTemplate>
                                   
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批人">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("Approvaler") %>'></asp:Label>
                                        </ItemTemplate>
                                  
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Count" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                   
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="结果">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ResultString" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="具体反馈">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" Text='<%# Eval("FeeBack") %>'></asp:Label>
                                        </ItemTemplate>
                                 
                                    </asp:TemplateField>
                                    
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未经审批</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="工程量清单" ID="TabPanel2">
            <ContentTemplate>
                <table id="Table4" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr id="tr_aftermodifyheader" runat="server">
                        <td class="Table_searchtitle">
                            专项工程变更后工作量清单
                        </td>
                    </tr>
                    <tr id="tr_aftermodifydetail" runat="server">
                        <td>
                            <asp:GridView ID="gridview_JobItemList_AfterModify" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gridview_JobItemList_AfterModify_RowDataBound" HeaderStyle-BackColor="#efefef"
                                HeaderStyle-Height="25px" ShowFooter="true" EnableViewState="false" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="设备 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Equipment" runat="server" Text='<%# Eval("Equipment") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格型号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Count" runat="server" Text='<%# Eval("Count", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单价(元)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_UnitPrice" runat="server" Text='<%# Eval("UnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>
                                            </FooterTemplate>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未添工作项</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle">
                            专项工程原计划工作量清单
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridview_JobItemList" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gridview_JobItemList_RowDataBound" HeaderStyle-BackColor="#efefef"
                                DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" EnableViewState="false"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="设备 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Equipment" runat="server" Text='<%# Eval("Equipment") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格型号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Count" runat="server" Text='<%# Eval("Count", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单价(元)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_UnitPrice" runat="server" Text='<%# Eval("UnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>
                                            </FooterTemplate>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未添工作项</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="预算清单" ID="TabPanel3">
            <ContentTemplate>
                <asp:GridView ID="gridview_BudgetItemList" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="gridview_BudgetItemList_RowDataBound" HeaderStyle-BackColor="#efefef"
                    DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" EnableViewState="false"
                    RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="Label_Index" runat="server" Text='<%# (char)(System.Text.Encoding.ASCII.GetBytes("A")[0] + Container.DataItemIndex) %>'></asp:Label>
                                <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label ID="Label_BudgetItemName" runat="server" Text='<%# Eval("BudgetItemName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否与直接费相关">
                            <ItemTemplate>
                                <asp:Label ID="Label_Related" runat="server" Text='<%# Convert.ToBoolean(Eval("IsRelated2Direct"))== true ? "是":"否" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="与直接费比例(%)">
                            <ItemTemplate>
                                <asp:Label ID="Label_TrueMultiple" runat="server" Text='<%# (Convert.ToDecimal(Eval("TrueMultiple"))*100).ToString("#.##") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_TotalMultiple" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="金额(元)">
                            <ItemTemplate>
                                <asp:Label ID="Label_TrueAmount" runat="server" Text='<%# Eval("TrueAmount", "{0:#,0.##}") %>'></asp:Label>
                                </FooterTemplate>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle Height="20px" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <center>
                            未添预算项</center>
                    </EmptyDataTemplate>
                </asp:GridView>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="设计方案" ID="TabPanel4">
            <ContentTemplate>
                <table id="Table_Design" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            专项工程设计方案
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            方案名称
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_DesignName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            设计成本
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="Label_DesignCost" runat="server"></asp:Label>元
                        </td>
                        <td class="Table_searchtitle" style="width: 15%">
                            工程费用
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="Label_ProjectCost" runat="server"></asp:Label>元
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            设计单位
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_Designer" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            设计单位信息
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_DesignerInfo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%" rowspan="2">
                            设计方案
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_Design" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            附件：
                            <asp:HyperLink Font-Underline="True" ForeColor="Blue" ID="HyperLink_Design" runat="server"
                                Visible="False"></asp:HyperLink>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="4" style="text-align:center">
                            &nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="招标信息" ID="TabPanel5">
            <ContentTemplate>
                <table id="Table_Bid" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            专项工程招标信息
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%">
                            中标单位
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_BiddenCompany" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle" style="width: 15%" rowspan="2">
                            中标单位简介
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label_BiddenCompanyInfo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            相关附件：
                            <asp:HyperLink Font-Underline="true" ForeColor="Blue" ID="HyperLink_File" runat="server"
                                Visible="false"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:center">

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="施工计划" ID="TabPanel6">
            <ContentTemplate>
                <div id="div1">
                    <table id="Table2" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                专项工程施工计划以及进度情况
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                        <th style="width: 3%; text-align: center">
                                            序号
                                        </th>
                                        <th style="width: 10%; text-align: center">
                                            项目
                                        </th>
                                        <th style="width: 10%; text-align: center">
                                            前置项目
                                        </th>
                                        <th style="width: 10%; text-align: center">
                                            时间
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            时长(天)
                                        </th>
                                        <th style="width: 15%; text-align: left">
                                            人力安排
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            设备安排
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            当前进度
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            状态
                                        </th>
                                    </tr>
                                    <asp:Repeater ID="Repeater_PlanItemList" runat="server">
                                        <ItemTemplate>
                                            <tr style="height: 30px" id="tr_item" runat="server">
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                    <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label_ItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label_PrefixItemName" runat="server" Text='<%# Eval("PrefixItemName") %>'></asp:Label>
                                                    <input type="hidden" id="Hidden_PrefixItemID" value='<%# Eval("PrefixItemID") %>'
                                                        runat="server" />
                                                    <span id="span_daysafter" runat="server" visible='<%# Convert.ToInt64( Eval("PrefixItemID"))>0 %>'>
                                                        (<asp:Label ID="Label_DaysAfter" runat="server" Text='<%# Eval("DaysAfter") %> '></asp:Label>天后开始)</span>
                                                    <input type="hidden" id="Hidden_DaysAfter" value='<%# Eval("DaysAfter") %>' runat="server" />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label_StartTime" runat="server" Text='<%# Eval("StartTime", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                                    -
                                                    <asp:Label ID="Label_EndTime" runat="server" Text='<%# Eval("EndTime", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label_Days" runat="server" Text='<%# Eval("Days") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label_HRPlan" runat="server" Text='<%# Eval("HRPlan") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label_DevicePlan" runat="server" Text='<%# Eval("DevicePlan") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label_Progress" runat="server" Text='<%# Eval("Progress","{0:P}") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10" style="word-break: break-all">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" bordercolor="#000000"
                                                        style="border-collapse: collapse; background-color: white">
                                                        <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Eval("ProgressCheckRecord") %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        [<asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>]
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all">
                                                                        <asp:Label ID="Label_Time" Text='<%# Eval("CheckTime","{0:yyyy-MM-dd}") %>' runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all">
                                                                        <asp:Label ID="Label_Progress" Text='<%# Eval("Progress","{0:P}") %>' runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all">
                                                                        人力：<asp:Label ID="Label_HR" Text='<%# Eval("HR") %>' runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all">
                                                                        质量：<asp:Label ID="Label_Quality" Text='<%# Eval("Quality") %>' runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all">
                                                                        备注：<asp:Label ID="Label_Remark" Text='<%# Eval("Remark") %>' runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all">
                                                                        检查人：<asp:Label ID="Label_Checker" Text='<%# Eval("Checker") %>' runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="Table_Graph" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                    text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td class="Table_searchtitle">
                            施工计划横道图
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle">
                            <div id="div_gant" class="fixed" enableviewstate="false" runat="server" style="width: 900px;
                                height: 400px; overflow: auto">
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="进场设备" ID="TabPanel7">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                        <th style="width: 3%; text-align: center">
                            序号
                        </th>
                        <th style="width: 10%; text-align: center">
                            设备
                        </th>
                        <th style="width: 10%; text-align: center">
                            规格型号
                        </th>
                        <th style="width: 10%; text-align: center">
                            尺寸以及功能
                        </th>
                        <th style="width: 5%; text-align: center">
                            状况
                        </th>
                        <th style="width: 15%; text-align: left">
                            标书数量
                        </th>
                        <th style="width: 5%; text-align: center">
                            已进数量
                        </th>
                    </tr>
                    <asp:Repeater ID="Repeater_DeviceItemList" runat="server">
                        <ItemTemplate>
                            <tr style="height: 30px" id="tr_item" runat="server">
                                <td style="text-align: center">
                                    <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="Label_DeviceName" runat="server" Text='<%# Eval("DeviceName") %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="Label_Size" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                    <asp:Label ID="Label_Usage" runat="server" Text='<%# Eval("Usage") %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label_PlanCount" runat="server" Text='<%# Eval("PlanCount", "{0:#,0.##}") %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="Label_ActualCount" runat="server" Text='<%# Eval("ActualCount", "{0:#,0.##}") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10" style="word-break: break-all">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
                                        style="border-collapse: collapse; background-color: LightBlue">
                                        <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                            <th style="width: 5%; text-align: center">
                                                序号
                                            </th>
                                            <th style="width: 45%; text-align: center">
                                                时间
                                            </th>
                                            <th style="width: 45%; text-align: center">
                                                数量
                                            </th>
                                        </tr>
                                        <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Eval("DeviceInRecordList") %>'>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 5%; text-align: center">
                                                        <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; word-wrap: break-word; word-break: break-all">
                                                        <asp:Label ID="Label_Time" Text='<%# Eval("Time","{0:yyyy-MM-dd}") %>' runat="server">
                                                        </asp:Label>
                                                    </td>
                                                    <td style="width: 45%; word-wrap: break-word; word-break: break-all">
                                                        <asp:Label ID="Label_Count" Text='<%# Eval("Count","{0:#,0.#####}") %>' runat="server">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="计量支付" ID="TabPanel8">
            <ContentTemplate>
                <div id="div_table">
                    <table id="Table1" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                预支付项清单
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridview_PrePayItemList" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-BackColor="#efefef" EnableViewState="false" OnRowDataBound="gridview_PrePayItemList_RowDataBound"
                                    DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" RowStyle-Height="20px"
                                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="预定支付日期">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Time" runat="server" Text='<%#  Eval("Time","{0:yyyy-MM-dd}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额(元)">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>
                                                </FooterTemplate>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付方式">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <center>
                                            未添预支付项</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <div id="div_tableContract">
                    <table id="TableContract" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                合同支付项清单
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridview_ContractPayItemList" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-BackColor="#efefef" EnableViewState="false" OnRowDataBound="gridview_ContractPayItemList_RowDataBound"
                                    DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" RowStyle-Height="20px"
                                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工作项">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_PlanItemName" runat="server" Text='<%#  Eval("PlanItemName") %>'></asp:Label>
                                                <input type="hidden" id="Hidden_PlanItemID" value='<%# Eval("PlanItemID") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="延迟时间(天)">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_DaysAfter" runat="server" Text='<%# Eval("DaysAfter") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额(元)">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>
                                                </FooterTemplate>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label_TotalAmountContract" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付方式">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <center>
                                            未添合同支付项</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <div id="div2">
                    <table id="Table3" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                专项工程月度支付清单
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridview_MonthlyPayList" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-BackColor="#efefef" OnRowDataBound="gridview_ItemMonthlyList_RowDataBound"
                                    HeaderStyle-Height="25px" ShowFooter="true" RowStyle-Height="20px" Width="100%"
                                    HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="年月款项">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Year" runat="server" Text='<%# Eval("Year") %>'></asp:Label>年<asp:Label
                                                    ID="Label_Month" runat="server" Text='<%# Eval("Month") %>'></asp:Label>月
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付时间">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_PayTime" runat="server" Text='<%#  Eval("PayTime","{0:yyyy-MM-dd}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工程进度">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Progress" runat="server" Text='<%# Eval("Progress","{0:P}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额(元)<br/>计划支付/已经支付">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_Paid" runat="server" Text='<%# Eval("Paid", "{0:#,0.##}") %>'></asp:Label>
                                                </FooterTemplate>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label_TotalAmountMonthly" runat="server"></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_TotalPaidMonthly" runat="server" Text='<%# Eval("Paid", "{0:#,0.##}") %>'></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付方式">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付者">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Payee" runat="server" Text='<%# Eval("Payee") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <center>
                                            未添加月度支付项</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <div id="div_Total">
                    <table id="Table_Total" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle" colspan="5">
                                专项工程支付简表
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                单位：元
                            </td>
                            <td class="Table_searchtitle">
                                预支付
                            </td>
                            <td class="Table_searchtitle">
                                合同支付
                            </td>
                            <td class="Table_searchtitle">
                                月进度支付
                            </td>
                            <td class="Table_searchtitle">
                                合计
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                计划支付
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanPre" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanContract" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanMonthly" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                实际已支付
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidPre" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidContract" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidMonthly" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                剩余未支付（计划-实际）
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffPre" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffContract" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffMonthly" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="变更记录" ID="TabPanel9">
            <ContentTemplate>
                <div id="div3">
                    <center>
                        专项工程变更设计申请报告单
                    </center>
                    <asp:Repeater ID="Repeater_ModifyList" runat="server">
                        <ItemTemplate>
                            <br />
                            <table border="1pt" style="border-collapse: collapse" style="width: 100%">
                                <tr>
                                    <th align="left">
                                        <span><a href="javascript:void(0)" onclick="javascript:HideShowForm(this);" runat="server"
                                            id="a_hideshow">
                                            <asp:Label ID="Label_ApplyDateTitle" Text='<%# Eval("ApplyTime","{0:yyyy-MM-dd}") %>'
                                                Font-Underline="true" ForeColor="Blue" runat="server"></asp:Label><asp:Label ID="Label_HideShow"
                                                    Text='<%# Container.ItemIndex==0? "[收缩申报单]" : "[展开申报单]" %>' Font-Underline="true"
                                                    ForeColor="Blue" runat="server"></asp:Label><asp:Label ID="Label_StatusString" Text='<%# Eval("StatusString") %>'
                                                        Font-Underline="true" ForeColor="Blue" runat="server"></asp:Label></a></span>
                                    </th>
                                </tr>
                                <tr id="tr_form" runat="server" style='<%# Container.ItemIndex==0? "display:block": "display:none" %>'>
                                    <td>
                                        <center style="font-size: medium; font-weight: bold">
                                            工程变更设计申请报告单</center>
                                        <br />
                                        项&nbsp;&nbsp;目：
                                        <br />
                                        承包人：（单位签章）
                                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                            style="border-collapse: collapse;">
                                            <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                                <th style="width: 10%; text-align: center">
                                                    变更工程名称
                                                </th>
                                                <td colspan="2">
                                                    <asp:Label ID="Label_ProjectName" runat="server"></asp:Label>
                                                </td>
                                                <th style="width: 10%; text-align: center">
                                                    申请日期
                                                </th>
                                                <td colspan="2">
                                                    <asp:Label ID="Label_ApplyTime" Text='<%# Eval("ApplyTime","{0:yyyy-MM-dd}") %>'
                                                        runat="server">
                                    
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <th style="width: 3%; text-align: center">
                                                    变更后金额<br />
                                                    （元）
                                                </th>
                                                <td>
                                                    <asp:Label ID="Label_BudgetChange" Text='<%# Eval("BudgetChange","{0:#,0.###}") %>'
                                                        runat="server">
                                    
                                                    </asp:Label>
                                                </td>
                                                <th style="width: 10%; text-align: center">
                                                    增减金额<br />
                                                    （元）
                                                </th>
                                                <td>
                                                    <asp:Label ID="Label_BudgetIncDesc" Text='<%# Eval("BudgetIncDesc","{0:#,0.###}") %>'
                                                        runat="server">
                                    
                                                    </asp:Label>
                                                </td>
                                                <th style="width: 10%; text-align: center">
                                                    估计延长工期<br />
                                                    （天）
                                                </th>
                                                <td>
                                                    <asp:Label ID="Label_DelayDays" Text='<%# Eval("DelayDays") %>' runat="server">
                                    
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <td colspan="6">
                                                    变更设计原因及内容：<br />
                                                    <asp:Label ID="Label_ChangeContent" Text='<%# Eval("ChangeContent") %>' runat="server">
                                    
                                                    </asp:Label><br />
                                                    附件：
                                                    <asp:HyperLink ID="HyperLink_File" runat="server" ForeColor="Blue" Font-Underline="true"
                                                        Text='<%# Eval("ContentAttechment") %>' NavigateUrl='<%# WebUtility.SystemConfig.Instance.UploadPath + UPLOADFOLDER + Eval("ContentAttechment").ToString() %>'></asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <th>
                                                    备注：
                                                </th>
                                                <td colspan="5">
                                                    <asp:Label ID="Label_Remark2" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10" style="word-break: break-all">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
                                                        style="border-collapse: collapse; background-color: white">
                                                        <tr style="height: 30px">
                                                            <th style="width: 3%;">
                                                                序号
                                                            </th>
                                                            <th style="width: 7%;">
                                                                增加或减少设备
                                                            </th>
                                                            <th style="width: 10%;">
                                                                设备型号
                                                            </th>
                                                            <th style="width: 10%;">
                                                                数量（单位）
                                                            </th>
                                                            <th style="width: 10%;">
                                                                单价（元）
                                                            </th>
                                                            <th style="width: 10%;">
                                                                增加减少金额（元）
                                                            </th>
                                                            <th style="width: 15%;">
                                                                备注
                                                            </th>
                                                        </tr>
                                                        <asp:Repeater ID="Repeater_Detail" runat="server" DataSource='<%# Eval("DetailList") %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                                        <input type="hidden" runat="server" value='<%# Eval("IsAdd") %>' id="Hidden_IsAdd" />
                                                                        <asp:Label ID="Label_IsAdded" Font-Size="Larger" Text='<%# Eval("IsAddString") %>'
                                                                            runat="server" ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                                        <asp:Label ID="Label_DeviceName" Text='<%# Eval("DeviceName") %>' runat="server">
                                    
                                                                        </asp:Label>
                                                                        &nbsp;
                                                                        <asp:Label ID="Label_Model" Text='<%# Eval("Model") %>' runat="server">
                                    
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                                        <asp:Label ID="Label_Count" Text='<%# Eval("Count","{0:#,0.#####}") %>' runat="server">
                                    
                                                                        </asp:Label>
                                                                        &nbsp; （<asp:Label ID="Label_Unit" Text='<%# Eval("Unit") %>' runat="server">
                                    
                                                                        </asp:Label>）
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                                        <asp:Label ID="Label_UnitPrice" Text='<%# Eval("UnitPrice","{0:#,0.#####}") %>' runat="server">
                                    
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                                        <asp:Label ID="Label_IsAdded2" Text='<%# Eval("IsAddString") %>' runat="server" ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                                                        </asp:Label><asp:Label ID="Label_Amount" Text='<%# Eval("Amount","{0:#,0.#####}") %>'
                                                                            ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'
                                                                            runat="server">
                                    
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                                        <asp:Label ID="Label_Remark" Text='<%# Eval("Remark") %>' runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr style="height: 30px">
                                                            <th colspan="5">
                                                                合计
                                                            </th>
                                                            <th align="center">
                                                                <asp:Label ID="Label_TotalAmount" runat="server" Text='<%# (Convert.ToDecimal(Eval("TotalAmountFromDetail"))>=0? "+":"-")+Eval("TotalAmountFromDetail","{0:#,0.##}").ToString() %>'
                                                                    ForeColor='<%# Convert.ToDecimal(Eval("TotalAmountFromDetail"))>0?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                    
                                                                </asp:Label>
                                                            </th>
                                                            <th>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <th>
                                                    业主代表意见：
                                                </th>
                                                <td colspan="5">
                                                    <div id="div_ownerapprovalinfo" runat="server">
                                                        审批结果：<asp:Label ID="Label_OwnerApproval" runat="server" Text='<%# Eval("OwnerResultString") %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="Label_OwnerFeeBack" runat="server" Text='<%# Eval("OwnerFeeBack") %>'></asp:Label>
                                                        <br />
                                                        <span style="float: right;">部门负责人：
                                                            <asp:Label ID="Label_Owner" runat="server" Text='<%# Eval("OwnerApprovaler") %>'></asp:Label>
                                                            <br />
                                                            日期：<asp:Label ID="Label_OwnerTime" runat="server" Text='<%# ((int)Eval("OwnerResult")) == 0? "": ((DateTime)Eval("OwnerApprovalDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <th>
                                                    合约部审核：
                                                </th>
                                                <td colspan="5">
                                                    <div id="div_contractapprovalinfo" runat="server">
                                                        审批结果：<asp:Label ID="Label_ContractApproval" runat="server" Text='<%# Eval("ContractResultString") %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="Label_ContractFeeBack" runat="server" Text='<%# Eval("ContractFeeBack") %>'></asp:Label>
                                                        <br />
                                                        <span style="float: right;">合约部负责人：
                                                            <asp:Label ID="Label_Contract" runat="server" Text='<%# Eval("ContractApprovaler") %>'></asp:Label>
                                                            <br />
                                                            日期：<asp:Label ID="Label_ContractTime" runat="server" Text='<%# ((int)Eval("ContractResult")) == 0? "": ((DateTime)Eval("ContractApprovalDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <th>
                                                    领导审批：
                                                </th>
                                                <td colspan="5">
                                                    <div id="div_leaderapprovalinfo" runat="server">
                                                        审批结果：<asp:Label ID="Label_LeaderApproval" runat="server" Text='<%# Eval("LeaderResultString") %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="Label_LeaderFeeBack" runat="server" Text='<%# Eval("LeaderFeeBack") %>'></asp:Label>
                                                        <br />
                                                        <span style="float: right;">领导：
                                                            <asp:Label ID="Label_Leader" runat="server" Text='<%# Eval("LeaderApprovaler") %>'></asp:Label>
                                                            <br />
                                                            日期：<asp:Label ID="Label_LeaderTime" runat="server" Text='<%# ((int)Eval("LeaderResult")) == 0? "": ((DateTime)Eval("LeaderApprovalDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
