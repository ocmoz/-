<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_WarehouseEquipmentInfo_EditDevice, App_Web_komcswzl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
    function addResponsiblity(addstring)
    {
        var arr = addstring.split(",");
        if(arr.length>=2){
         document.all.<%=this.Responsibility.ClientID %>.value = arr[0];
         document.all.<%=this.ResponsibilityName.ClientID %>.value = arr[1];
         }
        
    }
     function addPurchaser(addstring)
    {
        var arr = addstring.split(",");
        if(arr.length>=2){
         document.all.<%=this.Purchaser.ClientID %>.value=arr[0];
	                document.all.<%=this.PurchaserName.ClientID %>.value=arr[1];
         }
        
    }
     function addChecker(addstring)
    {
        var arr = addstring.split(",");
        if(arr.length>=2){
         document.all.<%=this.Checker.ClientID %>.value=arr[0];
	                document.all.<%=this.CheckerName.ClientID %>.value=arr[1];
         }
        
    }
    
    
        function WriteSelect(file_name)
    {
        if (file_name!=undefined)
        {
	            var ShValues = file_name.split('||');
	            switch(ShValues[2])
	            {
	                            case 'SupplierName':
                        {
                        if (ShValues[1]!=0)
	            {
	                document.all.<%=this.SupplierID.ClientID %>.value=ShValues[0];
	                document.all.<%=this.SupplierName.ClientID %>.value=ShValues[1];
	            } 
	                break;
                        }
                        case 'ProducerName':
                        {
                        if (ShValues[1]!=0)
	            {
	                document.all.<%=this.ProducerID.ClientID %>.value=ShValues[0];
	                document.all.<%=this.ProducerName.ClientID %>.value=ShValues[1];
	            } 
	                break;
                        }
                        case 'PurchaserName':
                        {
                        if (ShValues[1]!=0)
	            {
	                document.all.<%=this.Purchaser.ClientID %>.value=ShValues[0];
	                document.all.<%=this.PurchaserName.ClientID %>.value=ShValues[1];
	            } 
	                break;
                        }
                        case 'ResponsibilityName':
                        {
                        if (ShValues[1]!=0)
	            {
	                document.all.<%=this.Responsibility.ClientID %>.value=ShValues[0];
	                document.all.<%=this.ResponsibilityName.ClientID %>.value=ShValues[1];
	            }  
	                break;
                        }
                        case 'CheckerName':
                        {
                        if (ShValues[1]!=0)
	            {
	                document.all.<%=this.Checker.ClientID %>.value=ShValues[0];
	                document.all.<%=this.CheckerName.ClientID %>.value=ShValues[1];
	            } 
	                break;
                        }
                        default:break;
	            
	            }
	            
	    }
    }
    
       
            function Clear(target)
    {
        switch(target)
        {
            case 'SupplierName':
            {
            document.all.<%=this.SupplierName.ClientID %>.value='';
	    document.all.<%=this.SupplierID.ClientID %>.value=''; 
	    break;
            }
            case 'ProducerName':
            {
            document.all.<%=this.ProducerName.ClientID %>.value='';
	    document.all.<%=this.ProducerID.ClientID %>.value=''; 
	    break;
            }
            case 'PurchaserName':
            {
            document.all.<%=this.PurchaserName.ClientID %>.value='';
	    document.all.<%=this.Purchaser.ClientID %>.value=''; 
	    break;
            }
            case 'ResponsibilityName':
            {
            document.all.<%=this.ResponsibilityName.ClientID %>.value='';
	    document.all.<%=this.Responsibility.ClientID %>.value=''; 
	    break;
            }
            case 'CheckerName':
            {
            document.all.<%=this.CheckerName.ClientID %>.value='';
	    document.all.<%=this.Checker.ClientID %>.value=''; 
	    break;
            }
            case 'CategoryName':
            {
            document.all.<%=this.CategoryName.ClientID %>.value='';
	    //document.all.<%=this.CategoryID.ClientID %>.value='';
	    break;
            }
             case 'Address':
            {
                document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '';
        document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
                break;
            }
            default:break;
        }
         
    }
    //生成条形码时，用于检查采购日期是否已输入
    function CheckPurchaseDate()
    {
        var obj=document.all.<%=this.PurchaseDate.ClientID %>;
        if(obj.value==""||obj.value=="undefined")
        {
           alert("请先输入采购日期");
           return false;
         }
        causeValidate=false;
        return true;
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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库设备信息维护"
        HeadOPTxt="目前操作功能：仓库设备信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="xls.gif" ButtonName="批量导入" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="../EquipmentImport/EquipmentImprot.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="仓库设备信息列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="DeviceInfo.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <%-- <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
     <ContentTemplate>--%>
      <input type="hidden" id="Hidden_WarehouseAddressCode" runat="server" />
    <div style="width: 100%;">
        <%-- <asp:UpdatePanel ID="totalpanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="设备基本信息" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                设备基本信息编辑
                            </td>
                        </tr>
                        <tr>
                          <td class="table_body table_body_NoWidth">
                                所属公司：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <div style="display: ">
                                    <asp:DropDownList ID="DDLCompany" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div id="div_company" runat="server">
                                </div>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                资产编号：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_AssertNumber" title="请输入资产编号~20:" runat="server"></asp:TextBox>
                            </td><cc2:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="CompanyInfo"
                                Enabled="True" LoadingText="公司信息加载中..." PromptText="请选择所属的公司..." ServiceMethod="GetCompanyInfo"
                                ServicePath="LocationService.asmx" TargetControlID="DDLCompany">
                            </cc2:CascadingDropDown>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                设备条形码：
                            </td>
                            <td class="table_none table_none_NoWidth" title="" style="height: 30px">
                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="EquipmentNO" runat="server" title="请输入设备条形码~20:!"></asp:TextBox>
                                        <%if (cmd == "add" && action != "split")
                                          { %>
                                        <asp:CheckBox ID="cbComponent" Checked="false" runat="server" Text="是否零配件" />
                                        <%--  <asp:Button ID="btGenerateBarCode" runat="server" Text="生成条码" CssClass="button_bak"
                                OnClick="btGenerateBarCode_Click" OnClientClick="javascript:return CheckPurchaseDate();" />--%>
                                        <%} %>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                采购日期：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="PurchaseDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入采购日期~date!"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                设备名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="Name" title="请输入设备名称~20:!" runat="server"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                型号：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="Model" title="请输入型号~20:" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                品牌：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="Specification" title="请输入品牌~60:" runat="server"></asp:TextBox>
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
                                种类：
                            </td>
                            <td class="table_none table_none_NoWidth" >
                                <asp:UpdatePanel ID="selectcategory" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="CategoryName" runat="server" title="请选择所属设备种类~!"></asp:TextBox>
                                        <input class="cbutton" onclick="javascript:Clear('CategoryName');" type="button"
                                            value="清除" id="Button6" />
                                        <input class="cbutton" onclick="javascript:showPopWin('查询种类信息','<%=Page.ResolveUrl("~/")%>Module/FM2E/BasicData/DeviceTypeManage/DeviceType.aspx?showheader=false', 900, 420, WriteSelect,true,true);"
                                            type="button" value="种类信息" id="Button7" />
                                        <asp:TextBox ID="CategoryID" runat="server" title="请选择所属设备种类~!" Visible="false"></asp:TextBox>
                                        <span style="color: Red">*</span>
                                        <asp:Panel ID="Panel1" CssClass="popupLayer" runat="server">
                                            <div style="border: 1px outset white; ">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TreeView ID="TreeView1" OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded" runat="server"
                                                            onclick="javascript:causeValidate = false;" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                                        </asp:TreeView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>
                                        <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="CategoryName"
                                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                        <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="CategoryID"
                                            PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                              <td class="table_body table_body_NoWidth">
                                所属系统：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DropDownList_System" runat="server">
                                    <asp:ListItem>--请选择--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="table_body table_body_NoWidth">
                                路段：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="SectionName" runat="server">
                                    <asp:ListItem>--请选择--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                          
                        </tr>
                        <tr>
                          <td class="table_body table_body_NoWidth">
                                地址信息：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <input ID="TextBox_Address" type="text" runat="server" onfocus="javascript:showPopWin('选择地址','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);" style="width:70%"/>
                                <input type="hidden" id="Hidden_AddressID" runat="server" ></input>



                                    

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="Button_ClearAddress" class="cbutton" 
                                        onclick="javascript:Clear('Address');" type="button" value="清除" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox_DetailLocation" runat="server" Width="20%"></asp:TextBox>



                                    

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                
                            </td></tr><tr style="display:none">
                            <td class="table_body table_body_NoWidth">
                                位置类型：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <div style="display: ">
                                    <asp:DropDownList ID="LocationTag" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div id="div_LocationTag" runat="server">
                                </div>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                位置：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <div style="display: ">
                                    <asp:DropDownList ID="LocationID" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div id="div_LocationName" runat="server">
                                </div>
                            </td>
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="LocationType"
                                Enabled="True" LoadingText="位置类型加载中..." ParentControlID="DDLCompany" PromptText="请选择位置类型..."
                                ServiceMethod="GetLocationType" ServicePath="LocationService.asmx" TargetControlID="LocationTag">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" Enabled="True"
                                LoadingText="位置加载中..." ParentControlID="LocationTag" PromptText="请选择所属的位置..."
                                ServiceMethod="GetLocationName" ServicePath="LocationService.asmx" 
                                TargetControlID="LocationID">
                            </cc2:CascadingDropDown>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                购置价格(元)：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="Price" title="价格输入格式不正确~float" runat="server"></asp:TextBox></td><td class="table_body table_body_NoWidth">
                                维修次数：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="MaintenanceTimes" title="维修次数输入格式不正确~int!" runat="server" Text="0"></asp:TextBox></td></tr></table></ContentTemplate></cc2:TabPanel><cc2:TabPanel runat="server" HeaderText="其他参考信息" ID="TabPanel2">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                采购单号：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="PurchaseOrderID" runat="server"></asp:TextBox></td><td class="table_body table_body_NoWidth">
                                采购人：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <input type="text" runat="server" id="PurchaserName" onfocus="javascript:showPopWin('选择采购人','../../../../SystemManager/UserManager/SelectUser.aspx?selecttype=PurchaserName', 900, 400, addPurchaser,true,true);" />
                                <input class="cbutton" onclick="javascript:Clear('PurchaserName');" type="button"
                                    value="清除" id="Button3" />
                                <input id="Purchaser" runat="server" type="hidden" style="width: 15px" />
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="table_body table_body_NoWidth">
                                供应商：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <input type="text" runat="server" id="SupplierName" onfocus="javascript:showPopWin('选择供应商','Selectsupplier.aspx?selecttype=SupplierName', 900, 400, WriteSelect,true,true);" />
                                <input class="cbutton" onclick="javascript:Clear('SupplierName');" type="button"
                                    value="清除" id="clear1" />
                                <input id="SupplierID" runat="server" type="hidden" style="width: 15px" />
                            </td>
                            <td class="table_body table_body_NoWidth">
                                生产商：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <input type="text" runat="server" id="ProducerName" onfocus="javascript:showPopWin('选择生产商','Selectproducer.aspx?selecttype=ProducerName', 900, 400, WriteSelect,true,true);" />
                                <input class="cbutton" onclick="javascript:Clear('ProducerName');" type="button"
                                    value="清除" id="Button2" />
                                <input id="ProducerID" runat="server" type="hidden" style="width: 15px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                责任人 ：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <input type="text" runat="server" id="ResponsibilityName" onfocus="javascript:showPopWin('选择责任人','../../../../SystemManager/UserManager/SelectUser.aspx?selecttype=ResponsibilityName', 900, 400, addResponsiblity,true,true);" />
                                <input class="cbutton" onclick="javascript:Clear('ResponsibilityName');" type="button"
                                    value="清除" id="Button4" />
                                <input id="Responsibility" runat="server" type="hidden" style="width: 15px" />
                            </td>
                            <td class="table_body table_body_NoWidth">
                                验收人：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <input type="text" runat="server" id="CheckerName" onfocus="javascript:showPopWin('选择验收人','../../../../SystemManager/UserManager/SelectUser.aspx?selecttype=CheckerName', 900, 400, addChecker,true,true);" />
                                <input class="cbutton" onclick="javascript:Clear('CheckerName');" type="button" value="清除"
                                    id="Button5" />
                                <input id="Checker" runat="server" type="hidden" style="width: 15px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                验收日期：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:TextBox ID="ExamDate" runat="server" title="请启用验收日期~date" class="input_calender"
                                    onfocus="javascript:HS_setDate(this);"></asp:TextBox></td></tr><tr>
                            <td class="table_body table_body_NoWidth">
                                设备状态：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList title="请选择设备状态~" ID="Status" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                是否已注销资产：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList title="请确认是否已注销资产~" ID="IsCancel" runat="server">
                                    <asp:ListItem Value="1" Selected="True">否</asp:ListItem><asp:ListItem Value="2">是</asp:ListItem></asp:DropDownList></td></tr><tr>
                            <td class="table_body table_body_NoWidth">
                                启用日期：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="OpeningDate" runat="server" title="请输入启用日期~date" class="input_calender"
                                    onfocus="javascript:HS_setDate(this);"></asp:TextBox></td><td class="table_body table_body_NoWidth">
                                建档日期：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="FileDate" runat="server" title="请输入建档日期~date" class="input_calender"
                                    onfocus="javascript:HS_setDate(this);"></asp:TextBox></td></tr><tr>
                            <td class="table_body table_body_NoWidth">
                                保修期：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="baoxiuqi" title="请输入保修期~int" runat="server" Text="0"></asp:TextBox>个月
                            </td>
                            <td class="table_body table_body_NoWidth">
                                使用年限：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox title="请输入使用年限~float" ID="ServiceLife" runat="server"></asp:TextBox>年
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:TextBox ID="Remark" runat="server" Width="95%" title="请输入备注~100:"></asp:TextBox></td></tr><tr>
                            <td class="table_body table_body_NoWidth">
                                图片：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" onpropertychange="document.all('myimg').src=this.value;t1.style.display='block';queding.style.display='inline'"
                                            Height="20px" Width="250px" />
                                        <asp:Button ID="ButtonCancel" runat="server" Text="取消修改" OnClientClick="t1.style.display='none'"
                                            Visible="false" OnClick="ButtonCancel_Click" CssClass="button_bak" />
                                        <div id="queding" style="display: none">
                                            <input id="sure" type="button" value="确定" class="button_bak" onclick="t1.style.display='none';queding.style.display='none'" />
                                        </div>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="单击查看原大小" Width="80px"
                                            Style="display: inline" Height="60px" />
                                        <asp:Button ID="shoebig" Text="修改图片" OnClick="ImageButton1_Click" CssClass="button_bak"
                                            runat="server" />
                                        <cc2:PopupControlExtender ID="PopupControlExtender4" runat="server" TargetControlID="ImageButton1"
                                            PopupControlID="Panel5" DynamicServicePath="" Enabled="True" ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr id="tr11" runat="server">
                            <td>
                            </td>
                            <td id="Td1" runat="server" colspan="3">
                                <div id="t1" style="display: none">
                                    <img src="" id="myimg"></div></td></tr></table></ContentTemplate></cc2:TabPanel></cc2:TabContainer><table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
            runat="server">
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                    <input id="Reset1" class="button_bak" type="reset" value="重填" />&nbsp;&nbsp;
                    <input type="button" id="addthistype" runat="server" value="保存并继续添加" class="button_bak2"
                        onserverclick="AddThisType" />
                </td>
            </tr>
        </table>
        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
        <asp:Panel ID="Panel5" CssClass="popupControl" runat="server" Style="display: none">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ImageButton ID="ImageButton2" OnClientClick="javascript:return false;" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
