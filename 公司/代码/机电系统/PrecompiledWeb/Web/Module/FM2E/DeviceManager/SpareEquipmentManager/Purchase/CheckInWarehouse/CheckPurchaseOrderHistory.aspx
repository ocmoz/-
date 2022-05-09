<%@ page title="查看采购单" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckPurchaseOrderHistory, App_Web_bsrfgeas" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
<script type="text/javascript">


    function HideShow(objid) {

        var obj = document.getElementById(objid);

        if (obj != null) {
            obj.style.display = (obj.style.display == 'none' ? 'block' : 'none');
        }
    }
    </script>
    

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件采购"
        HeadOPTxt="目前操作功能：验收查看" HeadHelpTxt="浅黄色部分为本仓库验收过的项">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回验收历史" ButtonUrlType="Href"
            ButtonPopedom="List" ButtonUrl="CheckHistory.aspx" />
    </cc1:HeadMenuWebControls>

  
    <input type="hidden" id="Hidden_WarehouseID" runat="server" />
    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    材料申购表
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="2" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="未知公司"></asp:Label><br />
                    <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue"></asp:Label>&nbsp;机电材料申购表
                </td>
                <td style="text-align: center" colspan="2" class="Table_searchtitle">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="Label_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <table id="Table_detaillist" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                                border: solid 1px #a7c5e2;" border="1">
                                <tr>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        序号
                                    </th>
                                      <th  style="width: 8%" class="Table_searchtitle"
                                    
                                    >系统</th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        产品名称
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        规格型号
                                    </th>
                                    <th style="width: 15%" class="Table_searchtitle">
                                        数量<br />
                                        审批/采购/验收
                                    </th>
                                    <th style="width: 5%" class="Table_searchtitle">
                                        单位
                                    </th>
                                    <th class="Table_searchtitle">
                                        备注
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        状态
                                    </th>
                                    <th style="width: 10%" class="Table_searchtitle">
                                        采购人
                                    </th>
                                    
                                </tr>
                                <asp:Repeater ID="Repeater_ItemList" runat="server" OnItemDataBound="Repeater_ItemList_RowDataBound">
                                    <ItemTemplate>
                                        <tr id="tr_item" runat="server">
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                            </td>
                                             <td style="text-align:center">
                                              <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_AdjustCount" runat="server" ForeColor="Red" Text='<%# Eval("FinalCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                    ID="Label_ActualCount" runat="server" Text='<%# Eval("ActualCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label
                                                        ID="Label_AcceptedCount" runat="server" Text='<%# Eval("AcceptedCount", "{0:#,0.##}") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:Label ID="Label_Purchaser" Text='<%# Eval("PurchaserName") %>' runat="server">
                                                </asp:Label>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td valign="top"  style="text-align:center">
                                                <a href='<%# "javascript:HideShow(\"" + Container.FindControl("gridview_ItemList").ClientID + "\");" %>'>
                                                    <span style="color: Blue">采购记录</span></a>
                                            </td>
                                            <td colspan="8">
                                                <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" DataSource='<%# Eval("PurchaseRecordList") %>'
                                                    RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="序号">
                                                            <ItemTemplate>
                                                                [<asp:Label ID="Label_ID" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>]
                                                                <input type="hidden" id="Hidden_ItemID" runat="server" value='<%# Eval("PlanDetailItemID") %>' />
                                                                <input type="hidden" id="Hidden_RecordID" runat="server" value='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="产品名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="规格型号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量<br/>采购/验收">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseCount" runat="server" Text='<%# Eval("PurchaseCount", "{0:#,0.#####}") %>'></asp:Label>
                                                                &nbsp;/&nbsp;
                                                                <asp:Label ID="Label_AcceptanceCount" runat="server" Text='<%# Eval("AcceptanceCount", "{0:#,0.#####}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单价(元)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseUnitPrice" runat="server" Text='<%# Eval("PurchaseUnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="金额(元)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_PurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount", "{0:#,0.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="生产供应">
                                                            <ItemTemplate>
                                                                生产商：<asp:Label ID="Label_Producer" runat="server" Text='<%# Eval("Producer") %>'></asp:Label><br />
                                                                供应商：<asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%"  HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="责任人">
                                                            <ItemTemplate>
                                                                采购员：<asp:Label ID="Label_Purchaser" runat="server" Text='<%# Eval("PurchaserName") %>'></asp:Label><br />
                                                                仓管员：<asp:Label ID="Label_Checker_Warehouse" runat="server" Text='<%# Eval("WarehouseKeeperName") %>'></asp:Label><br />
                                                                技术员：<asp:Label ID="Label_Checker_Technician" runat="server" Text='<%# Eval("TechnicianName") %>'></asp:Label>
                                                                <br />
                                                                仓库：<asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%"  HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="状态">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label_StatusString" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="备注">
                                                            <ItemTemplate>
                                                                采购：<asp:Label ID="Label_PurchaseRemark" runat="server" Text='<%# Eval("PurchaseRemark") %>'></asp:Label><br />
                                                                验收：<asp:Label ID="Label_AcceptanceRemark" runat="server" Text='<%# Eval("AcceptanceRemark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                         
                                                       
                                                    </Columns>
                                                    <RowStyle Height="20px" HorizontalAlign="Center"  />
                                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <center>
                                                            尚未有采购记录</center>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    提交时间：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    申请人：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    最后更新：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_UpdateTime" runat="server"></asp:Label>
                </td>
                <td class="Table_searchtitle" style="text-align: right; width: 15%">
                    当前状态：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="Label_Status" runat="server"></asp:Label><asp:Label ID="Label_Approvaling"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    审批历史
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_ApprovalRecord">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="审批人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批结果">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="反馈意见">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" Text='<%# Eval("FeeBack") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细建议">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Eval("ApprovalLog") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未经审批</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    修改历史
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ModifyRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                <Columns>
                                    <asp:TemplateField HeaderText="修改人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Eval("ModifierName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改操作">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Eval("ModifyTypeString") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="保存内容">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" Text='<%# Eval("Content") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Eval("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未作修改</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
