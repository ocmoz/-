<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="OutWarehouseRecord.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseRecord_OutWarehouseRecord"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <script type="text/javascript">
        //地址选定
        function addAddress(val) {
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];
            if (addcode != '00') {
                document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
                document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备出库信息登记"
        HeadOPTxt="目前操作功能：出库登记信息登记">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="申请单列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="OutWarehouseApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="出库信息" ID="TabPanel1">
                        <ContentTemplate>
                            <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                                border: solid 1px #a7c5e2;" border="1px">
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 15%">
                                        申请单编号：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 35%">
                                        <asp:Label ID="Label_SheetName" runat="server"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="width: 15%">
                                        仓库：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 35%">
                                        <asp:Label ID="Label_WarehouseName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        申请人：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        申请日期：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_ApplyTime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        申请备注：
                                    </td>
                                    <td colspan="3" class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_ApplyRemark" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        状态：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_Status" runat="server"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        仓管员：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_OperatorName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        经办人：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_ReceiverName" runat="server"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        领用时间：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_OutTime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        出库备注：
                                    </td>
                                    <td colspan="3" class="table_none table_none_NoWidth">
                                        <asp:Label ID="Label_WarehouseRemark" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Table_searchtitle" colspan="4">
                                        出库申请明细
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                           <table style="width: 100%; border-collapse: collapse; vertical-align: middle; 
                                border: solid 1px #a7c5e2;" border="1px">
                                                <tr>
                                                    <th class="table_body table_body_NoWidth" style="width:5%; text-align:center">
                                                        序号
                                                    </th>
                                                    <th  class="table_body table_body_NoWidth" style="width:10%; text-align:center">
                                                        系统
                                                    </th>
                                                    <th class="table_body table_body_NoWidth" style="width:10%; text-align:center">
                                                        产品名称
                                                    </th>
                                                    <th class="table_body table_body_NoWidth" style="width:10%; text-align:center">
                                                        规格型号
                                                    </th>
                                                    <th class="table_body table_body_NoWidth" style="width:10%; text-align:center">
                                                        申请数量
                                                    </th>
                                                    <th class="table_body table_body_NoWidth" style="width:10%; text-align:center">
                                                        已出库数量
                                                    </th>
                                                    <th class="table_body table_body_NoWidth" style="width:5%; text-align:center">
                                                        单位
                                                    </th>
                                                    <th  class="table_body table_body_NoWidth" style="width:15%; text-align:center">
                                                        使用地址
                                                    </th>
                                                    <th class="table_body table_body_NoWidth" style="width:15%; text-align:center">
                                                        备注
                                                    </th>
                                                    <th class="table_body table_body_NoWidth" style="width:5%; text-align:center">
                                                        登记
                                                    </th>
                                                </tr>
                                                <asp:Repeater ID="Repeater_Detail" runat="server" OnItemCommand="Repeater_Detail_RowCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td rowspan="2">
                                                                <%# Container.ItemIndex + 1%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SystemName") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ProductName") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Model") %>
                                                            </td>
                                                            <td>
                                                            <%# Eval("Count","{0:#,0.#####}") %></td>
                                                            <td>
                                                                <%#Eval("OutCount","{0:#,0.#####}") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Unit") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("AddressName") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Remark") %>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/b.gif"
                                                                    CommandName="outEquipments" CommandArgument='<%# Eval("ItemID") %>' CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="9">
                                                            <asp:GridView ID="GridView_OutDetail" runat="server" AutoGenerateColumns="False"
                                                                Width="100%" OnRowDataBound="GridView2_RowDataBound" DataSource='<%# Eval("OutEquipmentList") %>'>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="序号">
                                                                        <ItemTemplate>
                                                                            [<%# Container.DataItemIndex + 1%>]
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="25px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                                                    <asp:BoundField DataField="Name" HeaderText="产品名称"></asp:BoundField>
                                                                    <asp:BoundField DataField="Model" HeaderText="型号"></asp:BoundField>
                                                                    <asp:BoundField DataField="Count" DataFormatString="{0:#,0.#####}" HeaderText="数量">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Unit" HeaderText="单位"></asp:BoundField>
                                                                    <asp:BoundField DataField="OutTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                                                        HeaderText="出库时间"></asp:BoundField>
                                                                    <asp:BoundField DataField="AddressName" HeaderText="使用地址"></asp:BoundField>
                                                                    <asp:BoundField DataField="Remark" HeaderText="备注"></asp:BoundField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <center>
                                                                        <span style="color: Red">没有出库明细信息 </span>
                                                                    </center>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                                                <RowStyle HorizontalAlign="Center" Height="20px" />
                                                            </asp:GridView></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        出库备注：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3">
                                        <textarea id="TextArea1" rows="4" style="width: 85%;" runat="server" title="请输入出库备注~50:"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        领用人签名：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3">
                                        用户名：
                                        <asp:TextBox ID="tbReceiver" runat="server" title="请输入用户名~20:"></asp:TextBox>
                                        &nbsp; 密码：
                                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <asp:Button ID="btSubmit" runat="server" OnClientClick="javascript:return confirm('确认出库？')"
                                    CssClass="button_bak" Text="提交" OnClick="btSubmit_Click" />
                            </center>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="审批记录" ID="TabPanel3">
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
                                        <span style="color: Red">未经审批</span></center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="出库登记" Visible="false">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr align="center">
                                    <td class="Table_searchtitle">
                                        当前出库条目<input type="hidden" runat="server" id="Hidden_EditItemID" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%">
                                            <Columns>
                                            <asp:BoundField DataField="SystemName" HeaderText="系统">
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="ProductName" HeaderText="产品名称"><ItemStyle Width="10%" /></asp:BoundField>
                                                <asp:BoundField DataField="Model" HeaderText="规格型号"><ItemStyle Width="10%" /></asp:BoundField>
                                                <asp:BoundField DataField="Count" DataFormatString="{0:#,0.#####}" HeaderText="申请数量"><ItemStyle Width="10%" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        已出库数量
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("OutCount","{0:#,0.#####}") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Unit" HeaderText="单位"><ItemStyle Width="5%" /></asp:BoundField>
                                                <asp:BoundField DataField="AddressName" HeaderText="使用地址">
                                                <ItemStyle Width="25%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Remark" HeaderText="备注"><ItemStyle Width="20%" /></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                            <RowStyle HorizontalAlign="Center" Height="20px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td class="Table_searchtitle">
                                        已出库设备列表
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand"
                                            Width="100%" OnRowDataBound="GridView2_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="产品名称"></asp:BoundField>
                                                <asp:BoundField DataField="Model" HeaderText="型号"></asp:BoundField>
                                                <asp:BoundField DataField="Count" DataFormatString="{0:#,0.#####}" HeaderText="数量">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit" HeaderText="单位"></asp:BoundField>
                                                <asp:BoundField DataField="OutTime" HeaderText="出库时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                                                    HtmlEncode="false"></asp:BoundField>
                                                <asp:BoundField DataField="AddressName" HeaderText="使用地址"></asp:BoundField>
                                                <asp:BoundField DataField="Remark" HeaderText="备注"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                                            CommandName="editItem" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        编辑</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此入库明细吗？')"
                                                            CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        删除</HeaderTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <center>
                                                    <span style="color: Red">没有出库明细信息 </span>
                                                </center>
                                            </EmptyDataTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                            <RowStyle HorizontalAlign="Center" Height="20px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr align="center">
                                    <td class="Table_searchtitle" colspan="4">
                                        出库详细信息<input id="Hidden_EditEquipmentItemIndex" runat="server" type="hidden" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 25px">
                                        是否固定资产：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 25px">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 25px">
                                        设备条形码：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 25px">
                                        <asp:TextBox ID="TBEquipment" runat="server" title="请输入设备条形码~20:" AutoPostBack="True"
                                            OnTextChanged="TBEquipment_TextChanged"></asp:TextBox><span style="color: Red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 25px">
                                        产品名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 25px">
                                        <asp:TextBox ID="TBProduct" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:DropDownList ID="DDLProduct" runat="server">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 25px">
                                        型号：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 25px">
                                        <asp:TextBox ID="TBModel" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:DropDownList ID="DDLModel" runat="server">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                        <div id="Div1" runat="server" style="display: none">
                                            <asp:DropDownList ID="DDLWarehouse" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DDLWarehouse"
                                            Category="Warehouse" PromptText="请选择仓库..." LoadingText="仓库加载中..." ServicePath="ProductModelService.asmx"
                                            ServiceMethod="GetWarehouse" Enabled="True">
                                        </cc2:CascadingDropDown>
                                        <cc2:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="DDLProduct"
                                            Category="Product" PromptText="请选择产品..." LoadingText="产品加载中..." ServicePath="ProductModelService.asmx"
                                            ServiceMethod="GetProduct" ParentControlID="DDLWarehouse" Enabled="True">
                                        </cc2:CascadingDropDown>
                                        <cc2:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="DDLModel"
                                            Category="Model" PromptText="请选择型号..." LoadingText="型号加载中..." ServicePath="ProductModelService.asmx"
                                            ServiceMethod="GetModel" ParentControlID="DDLProduct" Enabled="True">
                                        </cc2:CascadingDropDown>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 25px">
                                        数量：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 25px">
                                        <asp:TextBox ID="TextBox4" runat="server" title="请输入数量~float"></asp:TextBox><span
                                            style="color: Red">*</span>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 25px">
                                        单位：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 25px">
                                        <asp:DropDownList ID="DDLUnit" runat="server">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        详细地址：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <input id="TextBox_Address" type="text" style="width: 70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);" /><span
                                            style="color: Red">*</span>
                                        <input type="hidden" id="Hidden_AddressID" runat="server" />
                                        <input type="text" id="TextBox_DetailLocation" style="width: 20%;" runat="server"
                                            title="请输入详细地址~100:" />
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        所属系统：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="DDLSystem" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        备注：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                        <textarea id="TextArea3" style="width: 99%; height: 100%" runat="server" title="请输入申请备注~50:"></textarea>
                                    </td>
                                </tr>
                            </table>
                            <center>
                                        <asp:Label ID="LblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                                            <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="添加明细" OnClick="Button3_Click" />
                                    </center>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">
        function f(obj) {
            obj.select();
        }
    </script>

</asp:Content>
