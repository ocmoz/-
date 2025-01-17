﻿<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_WarehouseInventory_WarehouseInventory, App_Web_kuugqg6b" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库盘点信息管理"
        HeadOPTxt="目前操作功能：仓库盘点信息">
        <%--<cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加申请" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditOutWarehouseApply.aspx?cmd=add" />--%>
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel runat="server" HeaderText="盘点信息查询" ID="TabPanel1">
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
                                所属公司：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLCompany" runat="server" title="请选择所属公司~!">
                                </asp:DropDownList><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                仓库：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DDLWarehouse" runat="server" title="请选择所属仓库~!">
                                </asp:DropDownList><span style="color:Red">*</span>
                            </td>
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DDLCompany"
                                Category="Company" PromptText="请选择公司..." LoadingText="公司加载中..." ServicePath="CompanyWarehouseService.asmx"
                                ServiceMethod="GetCompany" Enabled="True">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DDLWarehouse"
                                Category="Warehouse" PromptText="请选择仓库..." LoadingText="仓库加载中..." ServicePath="CompanyWarehouseService.asmx"
                                ServiceMethod="GetWarehouse" ParentControlID="DDLCompany" Enabled="True">
                            </cc2:CascadingDropDown>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                开始时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TBMinTime" runat="server" class="input_calender" title="请选择开始时间~!"
                                    onfocus="javascript:HS_setDate(this);"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                结束时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TBMaxTime" runat="server" class="input_calender" title="请选择结束时间~!"
                                    onfocus="javascript:HS_setDate(this);"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                盘点类型：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="checktypeDDL" runat="server">
                                        <asp:ListItem Value="1">易耗品入库</asp:ListItem>
                                        <asp:ListItem Value="4">易耗品出库</asp:ListItem>
                                        <asp:ListItem Value="2">在用设备入库</asp:ListItem>
                                        <asp:ListItem Value="3">在用设备出库</asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                               
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                            </td>
                        </tr>
                    </table>
                    <center>
                                <asp:Label ID="LblError" runat="server" Text="" ForeColor="Red"></asp:Label>     
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="盘点" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </center>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="易耗品出入库盘点" ID="TabPanel2">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="时间">
                            <ItemTemplate>
                                <asp:Label ID="Label_Time" runat="server" Text='<%# Eval("InOutTime","{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="品名">
                            <ItemTemplate>
                                <asp:Label ID="Label_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="型号">
                            <ItemTemplate>
                                <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="Label_Count" runat="server" Text='<%# Eval("Amount","{0:0.##}") %>'></asp:Label>
                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出入库">
                            <ItemTemplate>
                                <asp:Label ID="Label_InOut" Text='<%# EnumHelper.GetDescription((Enum)Eval("Type")) %>'
                                    runat="server" ForeColor='<%# ((FM2E.Model.Equipment.ExpendableInOutRecordType)Eval("Type"))== FM2E.Model.Equipment.ExpendableInOutRecordType.In?
    System.Drawing.Color.Green:System.Drawing.Color.Red %>'>
                              
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="12%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <center>
                            <span style="color: Red">没有出入库记录</span></center>
                    </EmptyDataTemplate>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                    <RowStyle HorizontalAlign="Center" Height="20px" />
                </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left" CloneFrom="">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="在用设备入库盘点" ID="TabPanel3">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="产品名称"></asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="型号"></asp:BoundField>
                                        <asp:BoundField DataField="Count" HeaderText="数量" DataFormatString="{0:#,0.####}"></asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="单位"></asp:BoundField>
                                        <asp:BoundField DataField="InTime" HeaderText="入库时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <center>没有入库明细信息</center>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="在用设备出库盘点" ID="TabPanel4">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="产品名称"></asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="型号"></asp:BoundField>
                                        <asp:BoundField DataField="Count" HeaderText="数量" DataFormatString="{0:#,0.####}"></asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="单位"></asp:BoundField>
                                        <asp:BoundField DataField="OutTime" HeaderText="出库时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <center>没有出库明细信息</center>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
