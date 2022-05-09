<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_InOutWarehouseRecord, App_Web_4xxpdmqi" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <center>
    <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
        <tr>
            <td class="Table_searchtitle" style="width:10%">
                品名
            </td>
            <td  style="width:20%">
                <asp:Label ID="Label_Name" runat="server"></asp:Label>
                <input type="hidden" id="Hidden_WarehouseID" runat="server" />
            </td>
            <td class="Table_searchtitle"  style="width:10%">
                型号
            </td>
            <td  style="width:20%">
                <asp:Label ID="Label_Model" runat="server"></asp:Label>
            </td>
            <td class="Table_searchtitle"  style="width:10%">
                部门
            </td>
            <td  style="width:20%">
                <asp:DropDownList ID="DDL_OutDepartment" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="Table_searchtitle">
                时间
            </td>
            <td colspan="5">
                <asp:TextBox ID="TB_TimeLower" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                    title="请输入开始时间~date"></asp:TextBox>-
                <asp:TextBox ID="TB_TimeUpper" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                    title="请输入开始时间~date"></asp:TextBox>
                <asp:Button ID="Button_Search" runat="server" Text="查询" CssClass="button_bak" 
                    onclick="Button_Search_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="时间">
                            <ItemTemplate>
                                <asp:Label ID="Label_Time" runat="server" Text='<%# Eval("InOutTime","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="部门">
                            <ItemTemplate>
                                <asp:Label ID="Label_CompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="12%" />
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
                        <asp:TemplateField>
                            <ItemStyle Width="8%" />
                            <HeaderTemplate>
                               删除
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此出入库信息吗？')"
                                 CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <center>
                            <span style="color: Red">没有出入库记录</span></center>
                    </EmptyDataTemplate>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                    <RowStyle HorizontalAlign="Center" Height="20px" />
                </asp:GridView>
                <asp:Label ID="lbStatisticsMsg" runat="server" ForeColor="Red"></asp:Label>
                <cc1:aspnetpager id="AspNetPager1" runat="server" alwaysshow="True" onpagechanged="AspNetPager1_PageChanged"
                    cssclass="" custominfoclass="" custominfohtml="总记录：0  页码：1/1  每页：10" invalidpageindexerrormessage="页索引不是有效的数值！"
                    navigationtooltiptextformatstring="" pageindexoutofrangeerrormessage="页索引超出范围！"
                    showcustominfoexpendable="Left">
                                            </cc1:aspnetpager>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>
