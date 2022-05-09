<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_AssetManager_AssetAndDepreciation_AssetAndDepreciation, App_Web_j1md7fmu" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
    function Clear(target)
    {
        switch(target)
        {
            case 'Address':
            {
                document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '';
                document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
                break;
            }
            case 'Category':
            {
                document.getElementById('<%=CategoryName.ClientID %>').value = '';
            }
            default:break;
        }
    }
    //地址选定
    function addAddress(val)
    {
        var arr = new Array;
        arr = val.split('|');
        var addid = arr[0];
        var addcode = arr[1];
        var addname = arr[2];
        if(addcode!='00'){
        document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
        document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
        }
    }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="资产信息管理"
        HeadOPTxt="目前操作功能：资产信息管理" HeadHelpTxt="统计设定范围总资产、计算折旧">
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc2:TabPanel runat="server" HeaderText="统计条件" ID="TabPanel1">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    输入统计所需的条件，系统将对满足条件内的结果进行统计，不输的条件默认为选择所有
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    汇总条件
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList runat="server" ID="scope">
                                    </asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="scope2">
                                    </asp:DropDownList>
                                    <cc2:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="scopetype"
                                        Enabled="True" LoadingText="第一级汇总条件加载中..." PromptText="第一级汇总条件" ServiceMethod="getscope"
                                        ServicePath="ScopeService.asmx" TargetControlID="scope">
                                    </cc2:CascadingDropDown>
                                    <cc2:CascadingDropDown ID="CascadingDropDown4" runat="server" Category="scopetype2"
                                        Enabled="True" LoadingText="第二级汇总条件加载中..." ParentControlID="scope" PromptText="第二级汇总条件"
                                        ServiceMethod="getscope2" ServicePath="ScopeService.asmx" TargetControlID="scope2">
                                    </cc2:CascadingDropDown>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                </td>
                                <td class="table_none table_none_NoWidth">
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="table_body table_body_NoWidth">
                                    所属位置类型：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="LocationTag" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    所属位置名字：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="LocationID" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="LocationType"
                                    Enabled="True" LoadingText="单位类型加载中..." PromptText="请选择所属的单位类型..." ServiceMethod="GetLocationType"
                                    ServicePath="LocationService.asmx" TargetControlID="LocationTag">
                                </cc2:CascadingDropDown>
                                <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" Enabled="True"
                                    LoadingText="单位名字加载中..." ParentControlID="LocationTag" PromptText="请选择所属的单位名字..."
                                    ServiceMethod="GetLocationName" ServicePath="LocationService.asmx" 
                                    TargetControlID="LocationID">
                                </cc2:CascadingDropDown>
                            </tr>--%>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    地址信息：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3">
                                    <input id="TextBox_Address" type="text" style="width: 70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);" />
                                    <input type="hidden" id="Hidden_AddressID" runat="server" />
                                    <input class="cbutton" onclick="javascript:Clear('Address');" type="button" value="清除"
                                        id="Button_ClearAddress" />
                                    <asp:TextBox ID="TextBox_DetailLocation" Width="20%" runat="server"></asp:TextBox>
                                </td>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        所属系统：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="SystemName" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="SystemID" runat="server" Visible="False"></asp:TextBox>
                                        <asp:Panel ID="Panel2" CssClass="popupControl" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="TreeView2" runat="server" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                                    </asp:TreeView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                        <cc2:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="SystemName"
                                            PopupControlID="Panel2" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                        <cc2:PopupControlExtender ID="PopupControlExtender4" runat="server" TargetControlID="SystemID"
                                            PopupControlID="Panel2" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        种类名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="CategoryName" runat="server"></asp:TextBox>
                                        <input class="cbutton" onclick="javascript:Clear('Category');" type="button" value="清除"
                                            id="ClearCategory" />
                                        <asp:TextBox ID="CategoryID" runat="server" Visible="False"></asp:TextBox>
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
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        设备条形码：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="EquipmentNO" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        设备名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="Name" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        设备类型：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="ddlEqType" runat="server" Height="30px">
                                            <asp:ListItem Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        型号：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:TextBox ID="Model" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                        <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                    </td>
                                </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="统计结果" ID="TabPanel2">
                    <ContentTemplate>
                        <div id="gridviewdiv" runat="server" style="width: 100%; text-align: center; vertical-align: top;
                            padding: 0px 0px 0px 0px;">
                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="CompanyName" HeaderText="公司名称"></asp:BoundField>
                                    <asp:BoundField DataField="SystemName" HeaderText="所属系统"></asp:BoundField>
                                    <asp:BoundField DataField="CategoryName" HeaderText="所属种类">
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AddressName" HeaderText="所属位置名"></asp:BoundField>
                                    <asp:BoundField DataField="SerialNum" HeaderText="设备类型"></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="设备名 "></asp:BoundField>
                                    <asp:BoundField DataField="EquipmentNO" HeaderText="条形码 "></asp:BoundField>
                                    <asp:BoundField DataField="Model" HeaderText="型号 "></asp:BoundField>
                                    <asp:BoundField DataField="Price" DataFormatString="{0:#,0.##}" HeaderText="购买价格 ">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DepreciationPrice" DataFormatString="{0:#,0.##}" HeaderText="折旧价格 ">
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            具体设备</HeaderTemplate>
                                        <ItemTemplate>
                                            <a href='<%# "javascript:showPopWin(\"查看该统计项的具体设备信息\",\"../../DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/DeviceInfo.aspx?index=" + Container.DataItemIndex + "\", 900, 400, null,true,true);" %>'>
                                                具体设备 </a>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField>
                                        <HeaderTemplate>
                                            具体信息</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/select.gif"
                                                CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除 ">
                                        <ItemTemplate>
                                            <span>
                                                <asp:LinkButton ID="LinkButton1" CommandName="view" CommandArgument="<%# Container.DataItemIndex %>"
                                                    runat="server" Text="删除 "> </asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    没有满足条件的统计信息
                                </EmptyDataTemplate>
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="统计报表" ID="TabPanel3">
                    <ContentTemplate>
                        <div id="PrintDiv" style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" DisplayGroupTree="False" runat="server"
                                AutoDataBind="true" DisplayToolbar="False" PrintMode="ActiveX" />
                        </div>
                        <input type="button" class="button_bak" value="打印" onclick="javascript:printdiv('PrintDiv');" />
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
    
    function printdiv(PrintDivID,param)//打印与预览用函数
            {
            var headstr = "<html><head><title>aaa</title></head><body><object id='WebBrowser' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2' style='display:none'></object><div style='width: 649px;'>";
            var footstr = "</div></body>";
            var newstr = document.all.item(PrintDivID).innerHTML;
//            var oldlocation = window.location.href;
            try
            {
            winname = window.open("/PrintPreview.aspx", "_blank",''); 
            winname.document.title = '';
            winname.document.body.innerHTML = headstr+newstr+footstr;
            winname.document.all.WebBrowser.ExecWB(7,1);
            winname.close();
            }
            catch(e)
            {
            }
//            window.location.href = oldlocation;
            }
            
       function showgridviewitem(index)
       {
        alert('aa');
        javascript:showPopWin("查看该统计项的具体信息","../../DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/DeviceInfo.aspx.aspx?index=" + index , 900, 600, null,true,true);
       }
    </script>

</asp:Content>
