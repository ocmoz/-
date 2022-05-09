<%@ page title="查看报验单" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_ViewCAForm, App_Web_bsrfgeas" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件采购"
        HeadOPTxt="目前操作功能：验收" HeadHelpTxt="浅黄色部分为您核实过的项">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonPopedom="List" ButtonUrl="javascript:history.go(-1);" />
    </cc1:HeadMenuWebControls>

    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    材料报验表
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="2" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="未知公司"></asp:Label><br />
                    <asp:Label ID="Label_SheetName" runat="server" Font-Underline="true" ForeColor="Blue"></asp:Label>&nbsp;机电材料报验表
                </td>
                <td style="text-align: center" colspan="2" class="Table_searchtitle">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="Label_SheetID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <div runat="server" id="div_recordlist">
                        <table id="table_PurchaseRecordList" style="width: 100%; border-collapse: collapse;
                            vertical-align: middle; border: solid 1pt #000000;" border="1">
                            <tr>
                                <th style="width: 4%" class="Table_searchtitle">
                                    序号
                                </th>
                                 <th style="width: 7%" class="Table_searchtitle">
                                    系统
                                </th>
                                <th style="width: 7%" class="Table_searchtitle">
                                    产品名称
                                </th>
                                <th style="width: 8%" class="Table_searchtitle">
                                    规格型号
                                </th>
                                <th style="width: 7%" class="Table_searchtitle">
                                    数量<br />
                                    采购/验收
                                </th>
                                <th style="width: 5%" class="Table_searchtitle">
                                    单位
                                </th>
                                <th style="width: 5%" class="Table_searchtitle">
                                    单价(元)
                                </th>
                                <th style="width: 5%" class="Table_searchtitle">
                                    金额(元)
                                </th>
                                <th style="width: 10%" class="Table_searchtitle">
                                    生产供应
                                </th>
                                <th style="width: 10%" class="Table_searchtitle">
                                    责任人
                                </th>
                                <th style="width: 7%" class="Table_searchtitle">
                                    状态
                                </th>
                                <th style="width: 12%" class="Table_searchtitle">
                                    备注
                                </th>
                                <th style="width: 6%" class="Table_searchtitle">
                                    核实
                                </th>
                            </tr>
                            <asp:Repeater ID="Repeater_PurchaseRecordList" runat="server" OnItemDataBound="Repeater_ItemList_RowDataBound">
                                <ItemTemplate>
                                    <tr id="tr_item" runat="server">
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                            <input id="Hidden_FormID" runat="server" value='<%# Eval("ID") %>' type="hidden" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_PurchaseCount" runat="server" Text='<%# Eval("PurchaseCount", "{0:#,0.#####}") %>'></asp:Label>
                                            &nbsp;/&nbsp;
                                            <asp:Label ID="Label_AcceptanceCount" runat="server" Text='<%# Eval("AcceptanceCount", "{0:#,0.#####}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_PurchaseUnitPrice" runat="server" Text='<%# Eval("PurchaseUnitPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_PurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount", "{0:#,0.##}") %>'></asp:Label>
                                        </td>
                                        <td>
                                            生产商：<asp:Label ID="Label_Producer" runat="server" Text='<%# Eval("Producer") %>'></asp:Label><br />
                                            供应商：<asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                                        </td>
                                        <td>
                                            采购员：<asp:Label ID="Label_Purchaser" runat="server" Text='<%# Eval("PurchaserName") %>'></asp:Label><br />
                                            仓管员：<asp:Label ID="Label_Checker_Warehouse" runat="server" Text='<%# Eval("WarehouseKeeperName") %>'></asp:Label><br />
                                            技术员：<asp:Label ID="Label_Checker_Technician" runat="server" Text='<%# Eval("TechnicianName") %>'></asp:Label><br />
                                            仓库：<asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                            <input type="hidden" id="Hidden_WarehouseID" runat="server" value='<%# Eval("WarehouseID") %>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_StatusString" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                        </td>
                                        <td>
                                            采购：<asp:Label ID="Label_PurchaseRemark" runat="server" Text='<%# Eval("PurchaseRemark") %>'></asp:Label><br />
                                            验收：<asp:Label ID="Label_AcceptanceRemark" runat="server" Text='<%# Eval("AcceptanceRemark") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label_Confirm" runat="server" Text='<%# (bool)Eval("PurchaserConfirm")?"已核实":"未核实" %>'></asp:Label>
                                               
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                   
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
                    报验人：
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
                    <asp:Label ID="Label_Status" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


