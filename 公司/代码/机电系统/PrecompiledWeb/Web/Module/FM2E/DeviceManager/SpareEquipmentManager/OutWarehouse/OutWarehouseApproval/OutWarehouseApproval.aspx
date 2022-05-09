<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApproval_OutWarehouseApproval, App_Web_dfxldiuv" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../../../../control/WorkFlowUserSelectControl.ascx" tagname="WorkFlowUserSelectControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备出库申请信息审批"
        HeadOPTxt="目前操作功能：出库申请单审批">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="申请单列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="OutWarehouseApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="出库申请审批" ID="TabPanel1">
                        <ContentTemplate>
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                             border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td  class="table_body table_body_NoWidth" style="width:15%">
                                    申请单编号：
                                </td>
                                <td  class="table_none table_none_NoWidth" style="width:35%">
                                    <asp:Label ID="Label_SheetName" runat="server" Text=""></asp:Label>
                                </td>
                          
                                <td  class="table_body table_body_NoWidth" style="width:15%">
                                    仓库：
                                </td>
                                <td  class="table_none table_none_NoWidth" style="width:35%">
                                    <asp:Label ID="Label_WarehouseName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td  class="table_body table_body_NoWidth">
                                    申请人：
                                </td>
                                <td  class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_ApplicantName" runat="server" Text=""></asp:Label>
                                </td>
                                <td  class="table_body table_body_NoWidth">
                                    申请日期：
                                </td>
                                <td  class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_ApplyTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td  class="table_body table_body_NoWidth">
                                    申请备注：
                                </td>
                                <td colspan="3" class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_ApplyRemark" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                           
                            <tr>
                                <td  class="table_body table_body_NoWidth">
                                    状态：
                                </td>
                                <td  class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_Status" runat="server" Text=""></asp:Label>
                                </td>
                                 <td  class="table_body table_body_NoWidth">
                                    仓管员：
                                </td>
                                <td  class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_OperatorName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                              <td  class="table_body table_body_NoWidth">
                                    经办人：
                                </td>
                                <td  class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_ReceiverName" runat="server" Text=""></asp:Label>
                                </td>
                                <td  class="table_body table_body_NoWidth">
                                    领用时间：
                                </td>
                                <td  class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_OutTime" runat="server" Text=""></asp:Label>
                                </td>
                            
                              
                            </tr>
                         
                            <tr>
                                <td  class="table_body table_body_NoWidth">
                                    出库备注：
                                </td>
                                <td colspan="3"  class="table_none table_none_NoWidth">
                                    <asp:Label ID="Label_WarehouseRemark" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                出库申请明细<input id="Button5" type="button" runat="server" class="button_bak" value="查询库存"
                                            onclick="javascript:showPopWin('查询库存','QueryStorage.aspx', 900, 380, addtolist,true,true);" />
                                </td>
                            </tr>
                            <tr>
                            <td colspan="4">
                                 <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView_ApplyDetail" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                 <asp:BoundField DataField="SystemName" HeaderText="系统"><ItemStyle Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="产品名称"><ItemStyle Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="规格型号"><ItemStyle Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="Count" DataFormatString="{0:#,0.#####}" HeaderText="数量"><ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unit" HeaderText="单位"><ItemStyle Width="5%" /></asp:BoundField>
                                <asp:BoundField DataField="AddressName" HeaderText="使用地点"><ItemStyle Width="20%" /></asp:BoundField>
                                <asp:BoundField DataField="Usage" HeaderText="用途"><ItemStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Remark" HeaderText="备注"><ItemStyle Width="15%" /></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <center>
                                 <span style="color:Red">没有申请明细信息</span></center>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </div></td>
                            </tr>
                        </table>
                    </div>
                   
                    
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="2">
                                        出库申请单审批信息
                                    </td>
                                </tr>
                                <tr>
                                 <td class="table_body table_body_NoWidth">
                                        审批结果：
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                     <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        反馈意见：
                                    </td>
                                    <td  class="table_none table_none_NoWidth">
                                        <textarea id="TextArea1" style="width: 650px; height: 67px" runat="server" title="请输入反馈意见~50:"></textarea>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td colspan="2" align="center" >
                                        
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定审批" OnClick="Button1_Click" OnClientClick="javascript:return confirm('确认提交审批？');" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="审批记录" ID="TabPanel2">
                <ContentTemplate>
                    <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="审批人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批结果">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("Result") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="反馈意见">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" Text='<%# Eval("FeedBack") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="审批时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                     <span style="color:Red">   未经审批</span></center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                </ContentTemplate>
                </cc2:TabPanel>
                </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function addtolist() {
           
        }
    </script>

</asp:Content>
