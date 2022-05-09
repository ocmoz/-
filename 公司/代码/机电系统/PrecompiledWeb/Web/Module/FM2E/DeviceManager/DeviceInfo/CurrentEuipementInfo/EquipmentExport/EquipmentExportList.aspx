<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_EquipmentExport_EquipmentExportList, App_Web_pdymhtgh" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" OnNavigate="ScriptManager1_Navigate">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备批量导出"
        HeadOPTxt="目前操作功能：设备批量导出" HeadHelpTxt="设备列表默认显示设备">
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
        <ContentTemplate>
            <div style="width: 100%;">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <cc2:TabPanel runat="server" HeaderText="设备信息列表" ID="TabPanel1">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td valign="top"  style="width:20%">
                                        <div style="width: 100%; overflow: auto; height: 100%; border: solid 1px #ffffff;">
                                            
                                            <asp:TreeView ID="addressTree" runat="server" ShowLines="true" OnSelectedNodeChanged="addressTree_SelectedNodeChanged"
                                                OnTreeNodePopulate="addressTree_TreeNodePopulate">
                                                <SelectedNodeStyle ForeColor="#FF5050" />
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                    <td  valign="top" style="width:80%">
                                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <div id="PrintDiv" style="width: 100%;">
                                                <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                                    OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                                    OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Name" HeaderText="设备名称">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Model" HeaderText="型号">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="公司" ItemStyle-Width="7%" />
                                                        <asp:BoundField DataField="SystemName" HeaderText="系统" ItemStyle-Width="7%" />
                                                        <asp:BoundField DataField="PurchaseDate" HeaderText="采购日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Price" HeaderText="价格(元)" DataFormatString="{0:#,0.##}">
                                                            <ItemStyle Width="6%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                状态</HeaderTemplate>
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <%# EnumHelper.GetDescription((EquipmentStatus)Eval("Status")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AddressName" HeaderText="当前位置">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        没有设备信息
                                                    </EmptyDataTemplate>
                                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                                </asp:GridView>
                                            </div>
                                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="高级查询" ID="TabPanel2">
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
                                        设备条形码：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="EquipmentNO" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        资产编号：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="TextBox_AssertNumber" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
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
                                    <td class="table_body table_body_NoWidth">
                                        采购单号：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="PurchaseOrderID" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        设备名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="Name" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        型号：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="Model" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        品牌：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="Specification" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        设备类型：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="ddlEqType" runat="server" Height="16px"></asp:DropDownList>
                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        种类名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="CategoryName" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="CategoryID" runat="server" Visible="false"></asp:TextBox>
                                        <asp:Panel ID="Panel1" CssClass="popupControl" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="TreeView1" OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded" runat="server"
                                                        OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                                    </asp:TreeView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                        <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="CategoryName"
                                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                        <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="CategoryID"
                                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        所属系统：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="DropDownList_System" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        地址信息：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3">
                                        <input id="TextBox_Address" type="text" style="width: 50%" runat="server" />
                                        <input type="hidden" id="Hidden_AddressCode" runat="server" />
                                        <input class="cbutton" onclick="javascript:showPopWin('选择地址','../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"
                                            type="button" value="选择" id="Button_SelectAddress" />
                                        <input class="cbutton" onclick="javascript:clearAddress();" type="button" value="清除"
                                            id="Button_ClearAddress" />
                                        <asp:TextBox ID="TextBox_DetailLocation" Width="20%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td class="table_body table_body_NoWidth">
                                        供应商：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="SupplierName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        生产商：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="ProducerName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        采购人：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="PurchaserName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        采购日期：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="PurchaseDate1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="请输入采购日期~date"></asp:TextBox>&nbsp; 至&nbsp;
                                        <asp:TextBox ID="PurchaseDate2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="请输入采购日期~date"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        验收人：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="CheckerName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        责任人 ：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="ResponsibilityName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        验收日期：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="ExamDate1" runat="server" title="请启用验收日期~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>&nbsp; 至&nbsp;
                                        <asp:TextBox ID="ExamDate2" runat="server" title="请启用验收日期~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        启用日期：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="OpeningDate1" runat="server" title="请输入启用日期~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>&nbsp; 至&nbsp;
                                        <asp:TextBox ID="OpeningDate2" runat="server" title="请输入启用日期~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        建档日期：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="FileDate1" runat="server" title="请输入建档日期~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>&nbsp; 至&nbsp;
                                        <asp:TextBox ID="FileDate2" runat="server" title="请输入建档日期~date" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        最近更新时间：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="UpdateTime1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="请输入最近更新时间~date"></asp:TextBox>&nbsp; 至&nbsp;
                                        <asp:TextBox ID="UpdateTime2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="请输入最近更新时间~date"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        设备状态：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="Status" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        是否已注销资产：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="IsCancel" runat="server">
                                            <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="1">否</asp:ListItem>
                                            <asp:ListItem Value="2">是</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                              <center>
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </center>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
            </div>
            <center>
                <asp:Button ID="btnExport" runat="server" CssClass="button_bak" Text="导出" OnClick="btnExport_Click" ></asp:Button>
            </center>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport"/>
        </Triggers>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

        //地址选定
        function addAddress(val) {
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];
            if (addcode != '00') {
                document.getElementById('<%= Hidden_AddressCode.ClientID %>').value = addcode;
                document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
            }
        }
        function clearAddress() {
            document.getElementById('<%= Hidden_AddressCode.ClientID %>').value = '';
            document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
        }
    </script>

</asp:Content>

