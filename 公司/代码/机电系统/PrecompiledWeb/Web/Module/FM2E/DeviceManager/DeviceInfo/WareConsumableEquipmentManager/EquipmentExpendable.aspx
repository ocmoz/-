<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_EquipmentExpendable, App_Web_7bfrofdf" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

        //触发返回后重新加载数据事件
        function BackFillData() {
            document.getElementById('<%= Button_FillData.ClientID %>').click();
        }

        //弹出修改窗口
        function showChangeWin(id) {
            var consumableEquipmentID = document.getElementById(id.replace("ImageButton_Change", "Hidden_ConsumableEquipmentID")).value;
            window.location.href = "EquipmentExpendableRecord.aspx?cmd=edit&id=" + consumableEquipmentID;
//            showPopWin("修改仓库设备易耗品", "EquipmentExpendableRecord.aspx?cmd=edit&id=" + consumableEquipmentID, 900, 380, BackFillData, false, true);
        }
        //弹出查看窗口
        function showViewExpense(id) {
            var consumableEquipmentID = document.getElementById(id.replace("ImageButton_Log", "Hidden_ConsumableEquipmentID")).value;
            window.location.href = "ViewEquipmentExpendable.aspx?cmd=edit&id=" + consumableEquipmentID;
        }

        //弹出入库窗口
        function showInWarehouseWin(id) {
            var consumableEquipmentID = document.getElementById(id.replace("ImageButton_InWarehouse", "Hidden_ConsumableEquipmentID")).value;
            window.location.href = "InWarehouse.aspx?id=" + consumableEquipmentID;
        }

        //弹出出库窗口
        function showOutWarehouseWin(id) {
            var consumableEquipmentID = document.getElementById(id.replace("ImageButton_OutWarehouse", "Hidden_ConsumableEquipmentID")).value;
            window.location.href = "OutWarehouse.aspx?id=" + consumableEquipmentID;
        }

        //整套入库
        function showWin(id) {
            var consumableEquipmentID = document.getElementById(id.replace("ImageButton_Log", "Hidden_ConsumableEquipmentID")).value;
            showPopWin("出入库记录", "InOutWarehouseRecord.aspx?id=" + consumableEquipmentID, 900, 410, null, true, true);
        }
        
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="Hidden_WarehouseAddressCode" runat="server" />
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库设备易耗品信息维护"
        HeadOPTxt="目前操作功能：仓库设备易耗品信息维护" HeadHelpTxt="仓库设备易耗品列表">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加仓库设备易耗品" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditEquipmentExpendable.aspx?cmd=add" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 95%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="设备易耗品信息列表" ID="TabPanel1">
                
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                仓库：<asp:DropDownList ID="DropDownList_FilterWareHouse" runat="server" OnSelectedIndexChanged="OnFilter"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            没有设备易耗品信息信息
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="名称">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    <input type="hidden" id="Hidden_SystemID" value='<%# Eval("SystemID") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="12%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="条形码">
                                <ItemTemplate>
                                    <input type="hidden" id="Hidden_ConsumableEquipmentID" value='<%# Eval("ConsumableEquipmentID") %>'
                                        runat="server" />
                                    <asp:Label ID="Label_ConsumableEquipmentNO" runat="server" Text='<%# Eval("ConsumableEquipmentNO") %>'></asp:Label>
                                    <input type="hidden" id="Hidden_AddressID" value='<%# Eval("AddressID") %>' runat="server" />
                                    <%--<asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>--%>
                                    <input type="hidden" id="Hidden_WarehouseID" value='<%# Eval("WarehouseID") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="型号规格">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Label_Specification" runat="server" Text='<%# Eval("Specification") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="库存">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Count" runat="server" Text='<%# Eval("Count","{0:0.##}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="保险库存">
                                <ItemTemplate>
                                    <asp:Label ID="Label_ProducerID" runat="server" Text='<%# Eval("ProducerID","{0:0.##}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价（元）">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Price" runat="server" Text='<%# Eval("Price","{0:0.##}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="12%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Image ID="ImageButton_Change" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                        onclick='javascript:showChangeWin(this.id);' />
                                </ItemTemplate>
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="入库" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Image ID="ImageButton_InWarehouse" runat="server" ImageUrl="~/images/ICON/approval.gif"
                                        onclick='javascript:showInWarehouseWin(this.id);' />
                                </ItemTemplate>
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="出库" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Image ID="ImageButton_OutWarehouse" runat="server" ImageUrl="~/images/ICON/move.gif"
                                        onclick='javascript:showOutWarehouseWin(this.id);' />
                                </ItemTemplate>
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="4%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                        CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此消耗品信息吗？')"
                                        CausesValidation="false" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    删除</HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:Image ID="ImageButton_Log" runat="server" ImageUrl="~/images/ICON/select.gif"
                                        onclick='javascript:showViewExpense(this.id)' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    记录</HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        <RowStyle HorizontalAlign="Center" Height="20px" />
                    </asp:GridView>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：&lt;font color='red'&gt;0&lt;/font&gt;  页码：1/1  每页："
                        InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="{0}"
                        PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
                    <input id="Button_FillData" type="button" runat="server" value="重新加载数据" style="display: none"
                        onserverclick="Button_FillData_Click" />
                    <div style="text-align: left;">
                        当前设备总量为：<asp:Label ID="lbCurrentDeviceCount" runat="server"></asp:Label></div>
                    
                </ContentTemplate>
                
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="查询">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                组合查询（支持模糊查询）
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                仓库设备易耗品条形码：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbConsumableEquipmentNO" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                仓库设备易耗品名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                所属系统：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbSystemID" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                设备类型：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbSerialNum" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                型号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbModel" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                品牌：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbSpecification" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                资产编号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbAssertNumber" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                单位：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbUnit" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                数量：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbCount" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                价格：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbPrice" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                仓库：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbWareHouseName" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                备注：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbRemark" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                位置：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbAddressName" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                所属公司：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLCompany" runat="server">
                                    <asp:ListItem Value="" Text="不限"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="BtnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
    <asp:UpdatePanel ID="UpdatePanel_1" runat="server">
                <ContentTemplate>
                        <asp:Button ID="btnExport" runat="server" CssClass="button_bak" Text="导出" OnClick="btnExport_Click">
                        </asp:Button>
                    </ContentTemplate>
                <triggers>
            <asp:PostBackTrigger ControlID="btnExport"/>
        </triggers>
            </asp:UpdatePanel>
</asp:Content>
