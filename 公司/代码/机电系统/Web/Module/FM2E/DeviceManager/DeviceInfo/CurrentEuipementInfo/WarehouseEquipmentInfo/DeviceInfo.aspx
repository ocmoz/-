<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="DeviceInfo.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_WarehouseEquipmentInfo_DeviceInfo"
    Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>


<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <input type="hidden" id="Hidden_WarehouseAddressCode" runat="server" />
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库设备管理"
        HeadOPTxt="目前操作功能：仓库设备管理" HeadHelpTxt="设备列表默认显示新添设备">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加设备" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditDevice.aspx?cmd=add" />
        <%--            <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonUrl="window.history.go(-1);" />--%>
        <%--   <cc1:HeadMenuButtonItem ButtonIcon="move.gif" ButtonName="导入设备信息" ButtonPopedom="New" ButtonVisible="true" ButtonUrlType="href" ButtonUrl="Import.aspx"/>--%>
        <%--        <cc1:HeadMenuButtonItem ButtonIcon="xls.gif" ButtonName="导出结果" ButtonPopedom="Print"
            ButtonVisible="true" ButtonUrlType="Href" ButtonUrl="?cmd=export" />--%>
    </cc1:HeadMenuWebControls>
   
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 100%; ">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <cc2:TabPanel runat="server" HeaderText="仓库设备信息列表" ID="TabPanel1">
                        <ContentTemplate>
                        
                        <table width="100%"><tr><td >名称：<asp:TextBox ID="TextBox_FilterName" runat="server" OnTextChanged="OnFilter"  AutoPostBack="true"></asp:TextBox></td>
                                    <td >型号：<asp:TextBox ID="TextBox_FilterModel" runat="server"  OnTextChanged="OnFilter"  AutoPostBack="true"></asp:TextBox></td>
                                   
                                    <td >系统：<asp:DropDownList ID="DropDownList_FilterSystem" runat="server"  OnSelectedIndexChanged="OnFilter" AutoPostBack="true"></asp:DropDownList></td>
                                    <td > 状态：<asp:DropDownList ID="DropDownList_FilterStatus" runat="server"  OnSelectedIndexChanged="OnFilter" AutoPostBack="true"> </asp:DropDownList></td>
                                    <td > 仓库：<asp:DropDownList ID="DropDownList_FilterWareHouse" runat="server"  OnSelectedIndexChanged="OnFilter" AutoPostBack="true"> </asp:DropDownList></td>
                                    <td > 等待确认：<asp:DropDownList ID="DropDownList1"  runat="server"  OnSelectedIndexChanged="OnFilter" AutoPostBack="true"> 
                                                        <asp:ListItem Value="0">不限</asp:ListItem>
                                                        <asp:ListItem Value="9999">是</asp:ListItem>
                                                        <asp:ListItem Value="1">否</asp:ListItem>
                                                    </asp:DropDownList></td>
                                    
                                    </tr></table>
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
                                            <asp:BoundField DataField="Name" HeaderText="设备名称"><ItemStyle Width="10%" /></asp:BoundField>
                                            <asp:BoundField DataField="Model" HeaderText="型号"><ItemStyle Width="10%" /></asp:BoundField>
                                            <asp:BoundField DataField="SystemName" HeaderText="系统" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="PurchaseDate" HeaderText="采购日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                            <ItemStyle Width="10%" /></asp:BoundField>
                                            <asp:BoundField DataField="Price" HeaderText="价格(元)" DataFormatString="{0:#,0.##}"><ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                           <HeaderTemplate>状态</HeaderTemplate>
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                            <%# EnumHelper.GetDescription((EquipmentStatus)Eval("Status")) %>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AddressName" HeaderText="当前位置"><ItemStyle Width="25%" HorizontalAlign="Center" /></asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                                HeaderText="查看" CommandName="view">
                                                <ItemStyle Width="5%" />
                                                </asp:ButtonField>
                                                                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                待确认</HeaderTemplate>
                                            <ItemTemplate>
                                              <asp:ImageButton  CommandArgument="<%# Container.DataItemIndex %>" AlternateText="入库确认" runat="server" ImageUrl="~/images/ICON/Approval.gif" ID="ibWarmingCheck" CommandName="WarmingCheck" Visible='<%# Convert.ToInt32(Eval("Warming"))==9999 ? true : false  %>' OnClientClick="return confirm('是否要确认该设备入库？')" />
                                              <asp:Label runat="server" ID="lbaaaa2" Visible='<%#Convert.ToInt32(Eval("Warming"))==1?true:false %>'>已确认</asp:Label>
                                            </ItemTemplate><ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    删除</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/images/ICON/delete.gif" CommandName="del"
                                                        CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此设备信息吗？')"
                                                        CausesValidation="false" />
                                                </ItemTemplate><ItemStyle Width="5%" />
                                            </asp:TemplateField>
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
                            <%--<table width="100%">
                                <tr align="center">
                                    <td>
                                        <input id="Printcurrentpage" runat="server" type="button" class="button_bak" value="打印本页" onserverclick="PrintPreview" />
                                        <input id="Printallpage" runat="server" type="button" class="button_bak" value="打印"
                                            onserverclick="PrintPreviewAll" />
                                    </td>
                                </tr>
                            </table>--%>
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
                                        采购单号：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3">
                                        <asp:TextBox ID="PurchaseOrderID" runat="server"></asp:TextBox>
                                    </td></tr>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
    
    function printdiv(PrintDivID,param)//打印与预览用函数
            {
            var headstr = "<html><head><title>aaa</title></head><body><object id='WebBrowser' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2' style='display:none'></object><div style='width: 649px;'>";
            var footstr = "</div></body>";
            var newstr = document.all.item(PrintDivID).innerHTML;
            var oldlocation = window.location.href;
            winname = window.open("", "_blank");
            winname.document.write(headstr+newstr+footstr);
            try
            {
            winname.document.title = '';
            winname.document.body.innerHTML = headstr+newstr+footstr;
            winname.document.all.WebBrowser.ExecWB(7,1);
            winname.close();
            }
            catch(e)
            {
            }
            window.location.href = oldlocation;
        }

       
    </script>

</asp:Content>
