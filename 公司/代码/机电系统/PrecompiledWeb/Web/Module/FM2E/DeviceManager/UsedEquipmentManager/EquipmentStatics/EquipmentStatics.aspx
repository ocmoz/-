<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentStatics_EquipmentStatics, App_Web_tyyvxctl" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="现场设备统计查询"
        HeadOPTxt="目前操作功能：现场设备统计查询">
        <%--<cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加申请" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditOutWarehouseApply.aspx?cmd=add" />--%>
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    查询条件
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth">
                    位置(默认全部位置)：
                </td>
                <td class="table_none table_none_NoWidth">
                    <input id="hdAddressID" runat="server" type="hidden" />
                    <asp:TextBox ID="tbAddress" runat="server" Width="75%"></asp:TextBox>
                    <input class="button_bak" onclick="javascript:ClearAddress();" type="button" value="清空" />
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    系统：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="ddlSystem" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth">
                    所属维护队：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:DropDownList ID="ddlMaintainTeam" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    所属公司：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px">
                    <asp:DropDownList ID="DDLCompany" runat="server">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <cc2:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="CompanyInfo"
                        Enabled="True" LoadingText="公司信息加载中..." PromptText="请选择所属的公司..." ServiceMethod="GetCompanyInfo"
                        ServicePath="LocationService.asmx" TargetControlID="DDLCompany">
                    </cc2:CascadingDropDown>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px">
                    时间范围：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                    <div style="width: 80%; float: left">
                        <asp:TextBox ID="tbDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                            title="请输入结束时间~date"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button_bak" Text="查询" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" CssClass="button_bak" Text="导出" OnClick="btnExport_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
            OnRowDataBound="GridView1_RowDataBound" ShowFooter="True">
            <EmptyDataTemplate>
                没有任何的故障设备,设备完好率为100%
            </EmptyDataTemplate>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码">
                    <ItemStyle Width="12%" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        设备名称</HeaderTemplate>
                    <ItemTemplate>
                        <%#Eval("EquipmentName") %></ItemTemplate>
                    <ItemStyle Width="20%" />
                    <FooterTemplate>
                        <asp:Label ID="lbTotalCount" runat="server" Text=""></asp:Label>
                    </FooterTemplate>
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        型号</HeaderTemplate>
                    <ItemTemplate>
                        <%#Eval("Model") %></ItemTemplate>
                    <ItemStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        修复状态</HeaderTemplate>
                    <ItemTemplate>
                        <%#EnumHelper.GetDescription((Enum)Eval("MaintainResult")) %></ItemTemplate>
                    <ItemStyle Width="43%" />
                    <FooterTemplate>
                        <asp:Label ID="lbRate" runat="server" Text=""></asp:Label>
                    </FooterTemplate>
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
            <RowStyle HorizontalAlign="Center" Height="20px" />
        </asp:GridView>
    </div>

    <script type="text/javascript" language="javascript">
        function ClearAddress() {
            $get('<%=tbAddress.ClientID %>').value = '';
            $get('<%=hdAddressID.ClientID %>').value = '';
        }
        function RecordAddress(val) {
            //alert(val);
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];

            document.getElementById('<%= hdAddressID.ClientID %>').value = addcode;
            if (addcode != '00') {
                document.getElementById('<%=tbAddress.ClientID %>').value = addname;
            } else document.getElementById('<%=tbAddress.ClientID %>').value = "";
        }
    </script>

</asp:Content>
