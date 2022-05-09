<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageNoCheck.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="RecordMalfunction.aspx.cs"
    Inherits="Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionReport_RecordMalfunction" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="故障上报"
        HeadOPTxt="添加故障处理单" HeadHelpTxt="帮助">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="故障处理单列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="MalfunctionList.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
        
            ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td colspan="4" rowspan="2" class="table_body_WithoutWidth">
                            <b style="font-family: 宋体; font-size: medium">
                                <asp:Label ID="lbCompany" runat="server" Text="XX"></asp:Label>
                                机电设备故障处理表</b>
                        </tdz>
                        <td colspan="2" class="table_body_WithoutWidth">
                            表单编号
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Label ID="lbSheetNO" runat="server" Text="___________________" Style=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="Table_searchtitle" style="height: 30px">
                            报修情况
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body_WithoutWidth" style="width: 10%">
                            报修单位
                        </td>
                        <td style="width: 15%" class="table_none_WithoutWidth">
                           
                                <asp:DropDownList ID="ddlCompany" runat="server">
                                </asp:DropDownList>
                            
                            <asp:DropDownList ID="ddlDepartment" runat="server" title="请选择报修单位~!">
                            </asp:DropDownList>
                            <span style="color: Red">*</span>
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 10%">
                            报修人
                        </td>
                        <td style="width: 14%" class="table_none_WithoutWidth">
                            <asp:TextBox ID="tbReporter" runat="server" MaxLength="10" title="请输入报修人~10:!"></asp:TextBox><span
                                style="color: Red">*</span>
                        </td>
                        <td class="table_body_WithoutWidth" style="width: 10%">
                            日期&nbsp;&nbsp;
                        </td>
                        <td style="width: 23%" class="table_none_WithoutWidth">
                            <asp:TextBox ID="tbReportDate" runat="server" title="请输入报障日期~date!" class="input_calender"
                                onfocus="javascript:HS_setDate(this);" Width="100px" Enabled="False"></asp:TextBox>
                            <asp:TextBox ID="tbReportHour" runat="server" MaxLength="2" Width="10px" 
                                title="请输入报障时间(小时)~int!" Enabled="False"></asp:TextBox>时
                            <asp:TextBox ID="tbReportMinute" runat="server" MaxLength="2" Width="10px" 
                                title="请输入报障时间(小时)~int!" Enabled="False"></asp:TextBox>分
                            <cc2:NumericUpDownExtender ID="numUpDownHour" runat="server" TargetControlID="tbReportHour"
                                Width="50" RefValues="" ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID=""
                                TargetButtonUpID="" Minimum="0" Maximum="23" Enabled="false" />
                            <cc2:NumericUpDownExtender ID="numUpDownMinute" runat="server" TargetControlID="tbReportMinute"
                                Width="50" RefValues="" ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID=""
                                TargetButtonUpID="" Minimum="0" Maximum="59" Enabled="false" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="table_body_WithoutWidth">
                            故障设备
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5" valign="middle">
                            <input id="hdErrorFlag" runat="server" value="00000" type="hidden" />
                            <input id="hdOpenDivNum" type="hidden" runat="server" value="1" />
                            <div id="equipmentDiv1">
                                1、设备条形码
                                <asp:TextBox ID="tbEquipmentNO1" runat="server" MaxLength="20" AutoPostBack="true"
                                    OnTextChanged="tbEquipmentNO_TextChanged"></asp:TextBox>
                                设备名称
                                <asp:TextBox ID="tbEquipmentName1" runat="server" Width="200px" MaxLength="20" title="请输入设备名称~20:!"></asp:TextBox><span
                                    style="color: Red">*</span>
                                <cc2:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tbEquipmentName1"
                                                        ServicePath="EquipmentInfoService.asmx" ServiceMethod="GetEquipmentNameList" 
                                                        MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" Enabled="True">
                                                    </cc2:AutoCompleteExtender>
                                <img id="imgDel1" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_Delete.gif"
                                    alt="删除" onclick="javascript:Del(1);" style="display: none; cursor: pointer;" />
                                <img id="imgAdd1" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_AddItem.png"
                                    alt="增加" onclick="javascript:Add(1,'<%=tbEquipmentName2.ClientID %>');" style="cursor: pointer;" />
                                <span id="span1" runat="server"></span>
                            </div>
                            <div id="equipmentDiv2" style="display: none">
                                2、设备条形码
                                <asp:TextBox ID="tbEquipmentNO2" runat="server" MaxLength="20" AutoPostBack="true"
                                    OnTextChanged="tbEquipmentNO_TextChanged"></asp:TextBox>
                                设备名称
                                <asp:TextBox ID="tbEquipmentName2" runat="server" Width="200px" MaxLength="20"></asp:TextBox><span
                                    style="color: Red">*</span>
                                     <cc2:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="tbEquipmentName2"
                                                        ServicePath="EquipmentInfoService.asmx" ServiceMethod="GetEquipmentNameList" 
                                                        MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" Enabled="True">
                                                    </cc2:AutoCompleteExtender>
                                <img id="imgDel2" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_Delete.gif"
                                    alt="删除" onclick="javascript:Del(2);" style="cursor: pointer;" />
                                <img id="imgAdd2" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_AddItem.png"
                                    alt="增加" onclick="javascript:Add(2,'<%=tbEquipmentName3.ClientID %>');" style="cursor: pointer;" />
                                <span id="span2" runat="server"></span>
                            </div>
                            <div id="equipmentDiv3" style="display: none">
                                3、设备条形码
                                <asp:TextBox ID="tbEquipmentNO3" runat="server" MaxLength="20" AutoPostBack="true"
                                    OnTextChanged="tbEquipmentNO_TextChanged"></asp:TextBox>
                                设备名称
                                <asp:TextBox ID="tbEquipmentName3" runat="server" Width="200px" MaxLength="20"></asp:TextBox><span
                                    style="color: Red">*</span>
                                        <cc2:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tbEquipmentName3"
                                                        ServicePath="EquipmentInfoService.asmx" ServiceMethod="GetEquipmentNameList" 
                                                        MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" Enabled="True">
                                                    </cc2:AutoCompleteExtender> 
                                <img id="imgDel3" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_Delete.gif"
                                    alt="删除" onclick="javascript:Del(3);" style="cursor: pointer;" />
                                <img id="imgAdd3" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_AddItem.png"
                                    alt="增加" onclick="javascript:Add(3,'<%=tbEquipmentName4.ClientID %>');" style="cursor: pointer;" />
                                <span id="span3" runat="server"></span>
                            </div>
                            <div id="equipmentDiv4" style="display: none">
                                4、设备条形码
                                <asp:TextBox ID="tbEquipmentNO4" runat="server" MaxLength="20" AutoPostBack="true"
                                    OnTextChanged="tbEquipmentNO_TextChanged"></asp:TextBox>
                                设备名称
                                <asp:TextBox ID="tbEquipmentName4" runat="server" Width="200px" MaxLength="20"></asp:TextBox><span
                                    style="color: Red">*</span>
                                  <cc2:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="tbEquipmentName4"
                                                        ServicePath="EquipmentInfoService.asmx" ServiceMethod="GetEquipmentNameList" 
                                                        MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" Enabled="True">
                                                    </cc2:AutoCompleteExtender>        
                                <img id="imgDel4" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_Delete.gif"
                                    alt="删除" onclick="javascript:Del(4);" style="cursor: pointer;" />
                                <img id="imgAdd4" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_AddItem.png"
                                    alt="增加" onclick="javascript:Add(4,'<%=tbEquipmentName5.ClientID %>');" style="cursor: pointer;" />
                                <span id="span4" runat="server"></span>
                            </div>
                            <div id="equipmentDiv5" style="display: none">
                                5、设备条形码
                                <asp:TextBox ID="tbEquipmentNO5" runat="server" MaxLength="20" AutoPostBack="true"
                                    OnTextChanged="tbEquipmentNO_TextChanged"></asp:TextBox>
                                设备名称
                                <asp:TextBox ID="tbEquipmentName5" runat="server" Width="200px" MaxLength="20"></asp:TextBox><span
                                    style="color: Red">*</span>
                                 <cc2:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="tbEquipmentName5"
                                                        ServicePath="EquipmentInfoService.asmx" ServiceMethod="GetEquipmentNameList" 
                                                        MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" Enabled="True">
                                                    </cc2:AutoCompleteExtender>          
                                <img id="imgDel5" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_Delete.gif"
                                    alt="删除" onclick="javascript:Del(5);" style="cursor: pointer;" />
                                <span id="span5" runat="server"></span>
                                <img id="imgAdd5" src="<%=Page.ResolveUrl("~/") %>images/ICON/Button_AddItem.png" alt="增加" style="cursor:pointer;"/>
                            </div>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="table_body_WithoutWidth">
                            故障设备地址
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <input id="hdAddressID" runat="server" type="hidden" title="请输入故障设备地址~!" />
                            <input id="hdSystemID" runat="server" type="hidden"  />
                            <%--<asp:TextBox ID="tbAddress" runat="server" Width="80%" onclick="javascript:showPopWin('地址选择','../../../BasicData/AddressManage/Address.aspx?operator=select',700, 400, RecordAddress,false,true);"></asp:TextBox>--%>
                            <asp:TextBox ID="tbAddress" runat="server" Width="80%" onclick="javascript:showPopWin('设备选择','../../../DeviceManager/BarCode/BatchBarCode/SelectDevice.aspx',950, 400, RecordAddress,true,true);"></asp:TextBox>
                            <span style="color: Red">*</span><input type="button" value="清空" class="button_bak"
                                onclick="javascript:ClearAddress();">
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td class="table_body_WithoutWidth">设备条形码</td>
                        <td class="table_none_WithoutWidth" colspan="1">
                            <asp:TextBox ID="tbEqNo" 
                                runat="server" Width="143px" 
                                ontextchanged="tbEqNo_TextChanged" Enabled="False" ></asp:TextBox></td>
                        <td class="table_body_WithoutWidth">设备名称</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:TextBox ID="tbEqName" 
                                runat="server" Width="130px" ReadOnly="true"></asp:TextBox></td>
                        <td class="table_body_WithoutWidth">所属系统</td>
                        <td class="table_none_WithoutWidth" colspan="1"><asp:TextBox ID="tbEqSystem" runat="server"  Width="180px" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                       <td class="table_body_WithoutWidth">设备类型</td>
                       <td class="table_none_WithoutWidth">
                            <asp:DropDownList runat="Server" id="dlMalfunctionType">
                            <asp:ListItem Value=0>硬件故障</asp:ListItem>
                            <asp:ListItem Value=1>软件故障</asp:ListItem>
                            </asp:DropDownList>
                       </td>
                    </tr>
                    
                    <%--<tr>
                        <td class="table_body_WithoutWidth">
                            地址详细描述
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:TextBox ID="tbAddressDetail" runat="server" Width="95%" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="table_body_WithoutWidth">
                            故障描述
                        </td>
                        <td class="table_none_WithoutWidth" colspan="5">
                            <asp:TextBox ID="tbMalfunctionDescription" runat="server" TextMode="MultiLine" MaxLength="200"
                                Width="95%" Rows="4" title="请输入故障描述~200:!"></asp:TextBox><span style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="table_body_WithoutWidth">
                            故障系统
                        </td>
                        <td class="table_none_WithoutWidth">
                            &nbsp;<asp:DropDownList ID="ddlSystem" runat="server">
                            </asp:DropDownList>
                        </td>--%>
                        <td class="table_body_WithoutWidth">
                            维修单位
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:DropDownList ID="ddlMaintainType" runat="server" title="请选择维修单位类型~!"   onchange="MaintainSeletedChange()">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlMaintainTeam" runat="server" title="请选择维修单位~!">
                            </asp:DropDownList>
                            <input id="hdRank" type="hidden" runat="server" value="0" />
                            <%--<asp:DropDownList ID="ddlApproveler" runat="server">
                            </asp:DropDownList>--%>
                        </td>
                        
                        <td class="table_body_WithoutWidth">
                            故障原因
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:DropDownList ID="ddlMalReason" runat="server" >
                            </asp:DropDownList>
                        </td>
                        
                        <td class="table_body_WithoutWidth" id="MaintainPlanRankTitle">
                            故障等级
                        </td>
                        <td class="table_none_WithoutWidth" id="MaintainPlanRankContent">
                            <asp:DropDownList ID="ddlRank" runat="server" Enabled="False">
                            </asp:DropDownList>
                            &nbsp;
                            <input id="btSearchRank" class="button_bak" onclick="javascript:showPopWin('故障等级查询','../MalfunctionClassify/MalfunctionRankSearch.aspx',700, 400, SelectRank,true,true);"
                                type="button" value="等级查询" />
                        </td>
                    </tr>
                    <tr id="MaintainPlanTime">
                        <td class="table_body_WithoutWidth">
                            响应时间
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:TextBox ID="tbResponseTime" runat="server" Width="40px" title="请输入响应时间~int!" Enabled="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlResponseTime" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="table_body_WithoutWidth">
                            功能性恢复时间
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:TextBox ID="tbFunRestoreTime" runat="server" Width="40px" title="请输入功能性恢复时间~int!" Enabled="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlFunRestoreTime" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="table_body_WithoutWidth">
                            修复时间
                        </td>
                        <td class="table_none_WithoutWidth">
                            <asp:TextBox ID="tbRepairTime" runat="server" Width="40px" title="请输入修复时间~int!" Enabled="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlRepairTime" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                    <tr>
                        <td align="center" style="height: 38px">
                            <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="提交" OnClientClick="javascript:return checkForm(document.forms[0],true)&&confirm('是否确认提交？')"
                                OnClick="btSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="back" class="button_bak" type="button" value="取消" onclick="javascript:window.location.href='MalfunctionList.aspx'" />
                        </td>
                    </tr>
                </table>
                <cc2:CascadingDropDown ID="cddCompany" runat="server" Category="Company" Enabled="True"
                    LoadingText="公司加载中..." PromptText="请选择公司..." ServiceMethod="GetCompany" ServicePath="~/Module/FM2E/SystemManager/UserManager/CompanyDeptService.asmx"
                    TargetControlID="ddlCompany">
                </cc2:CascadingDropDown>
                <cc2:CascadingDropDown ID="cddDepartment" runat="server" Category="Department" Enabled="True"
                    LoadingText="部门加载中..." ParentControlID="ddlCompany" PromptText="请选择部门..." ServiceMethod="GetDepartmentByCompany"
                    ServicePath="~/Module/FM2E/SystemManager/UserManager/CompanyDeptService.asmx"
                    TargetControlID="ddlDepartment">
                </cc2:CascadingDropDown>
                
                
                
                <cc2:CascadingDropDown ID="cddMaintainType" runat="server" Category="DeptType" Enabled="True"
                    LoadingText="维护队类型加载中..."  PromptText="请选择维护队类型..." ServiceMethod="GetMaintainDeptTypes" ServicePath="~/Module/FM2E/SystemManager/UserManager/CompanyDeptService.asmx"
                    TargetControlID="ddlMaintainType">
                </cc2:CascadingDropDown>
                <cc2:CascadingDropDown ID="cddMaintainDept" runat="server" Category="MaintainDeptID" Enabled="True"
                    LoadingText="维护队加载中..." ParentControlID="ddlMaintainType" PromptText="请选择维护队..." ServiceMethod="GetMaintainDepts" ServicePath="~/Module/FM2E/SystemManager/UserManager/CompanyDeptService.asmx"
                    TargetControlID="ddlMaintainTeam">
                </cc2:CascadingDropDown>
                <%--<cc2:CascadingDropDown ID="cddApprovel" runat="server" Category="Approvel" Enabled="True"
                    LoadingText="人员加载中..." ParentControlID="ddlMaintainTeam" PromptText="请选择受理人(全体)" ServiceMethod="GetApprovelers"
                    ServicePath="~/Module/FM2E/SystemManager/UserManager/CompanyDeptService.asmx"
                    TargetControlID="ddlApproveler">
                </cc2:CascadingDropDown>--%>
                
                <%--                <script language="javascript" type="text/javascript">
                    InitEquipmentDiv();
                </script>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">
//      //检查小时与分钟的输入是否合法
//        function CheckInput()
//        {
//            //整数
//            var hourObj=$get('<%=tbReportHour.ClientID %>');
//            alert(hourObj.value);
//            if(hourObj.value.trim()=="")
//            {
//                alert("报修时间（小时）不能为空");
//                hourObj.focus();
//				return false;
//            }
//			var intVal=parseInt(hourObj.value.trim());
//			if(isNaN(intVal)||intVal!=value)
//			{
//				//alert(info+"\n"+name+"的格式不正确:\n"+value+"不是一个整数。");
//				alert("报修时间（小时）不是一个整数");
//				hourObj.focus();
//				return false;
//			}
//			if(intVal<0||intVal>23)
//			{
//			    alert("报修时间（小时）必须为0~23的整数");
//				hourObj.focus();
//				return false;
//			}
//			
//			var minuteObj=$get('<%=tbReportMinute.ClientID %>');
//			 if(minuteObj.value.trim()=="")
//            {
//                alert("报修时间（分钟）不能为空");
//                minuteObj.focus();
//				return false;
//            }
//			intVal=parseInt(minuteObj.value.trim());
//			if(isNaN(intVal)||intVal!=value)
//			{
//				//alert(info+"\n"+name+"的格式不正确:\n"+value+"不是一个整数。");
//				alert("报修时间（分钟）不是一个整数");
//				minuteObj.focus();
//				return false;
//			}
//			if(intVal<0||intVal>59)
//			{
//			    alert("报修时间（分钟）必须为0~59的整数");
//				minuteObj.focus();
//				return false;
//			}
//			
//			return true;
//        }
        function ClearAddress()
        {
            $get('<%=tbAddress.ClientID %>').value='';
            $get('<%=hdAddressID.ClientID %>').value='';
            
            $get('<%=tbEqNo.ClientID %>').value='';
            $get('<%=tbEqName.ClientID %>').value='';
            $get('<%=tbEqSystem.ClientID %>').value='';
        }
//        function RecordAddress(val) {
//            //alert(val);
//            var arr = new Array;
//            arr = val.split('|');
//            var addid = arr[0];
//            var addcode = arr[1];
//            var addname = arr[2];
//            if (addcode != '00') {
//                document.getElementById('<%= hdAddressID.ClientID %>').value = addid;
//                document.getElementById('<%= tbAddress.ClientID %>').value = addname;
//            }
//        }
            function RecordAddress(val) {
//            document.getElementById('<%= tbEqNo.ClientID %>').value = val;
//            alert(val);
//             
            if(val==null||val=="")
            {
                alert('你还没有选择设备！');
            }
            else
            {
                var arr = new Array;
                arr = val.split('@');
                var eqmno = arr[0];
                var eqmname = arr[1];
                var eqmsystem = arr[2];
                var addname = arr[3];
                var addid = arr[4];
                var sysid = arr[5];
                document.getElementById('<%= tbAddress.ClientID %>').value = addname;
                document.getElementById('<%= tbEqNo.ClientID %>').value = eqmno;
                document.getElementById('<%= tbEqName.ClientID %>').value = eqmname;
                document.getElementById('<%= tbEqSystem.ClientID %>').value = eqmsystem;
                document.getElementById('<%= hdAddressID.ClientID %>').value = addid;
                document.getElementById('<%= hdSystemID.ClientID %>').value = sysid;
            }
//            if (addcode != '00') {
//                document.getElementById('<%= hdAddressID.ClientID %>').value = addid;
//                document.getElementById('<%= tbAddress.ClientID %>').value = addname;
//            }
        }
        //选择故障等级
        function SelectRank(val) {
          
          if(val==null||val.trim()==""||val=="undefined")
              return;
         
          var arr=new Array;
          arr=val.split('|');
          var rank=arr[0];
          
          if(rank!="undefined")
          {
             var obj = document.all.<%=ddlRank.ClientID %>;
             for(var i=0;i<obj.options.length;i++)
             {
                 if(obj.options[i].value==rank)
                 {
                    obj.options[i].selected=true;
                    break;
                 }
             }
          }
          
          $get('<%=tbResponseTime.ClientID %>').value=arr[1];
          $get('<%=tbFunRestoreTime.ClientID %>').value=arr[3];
          $get('<%=tbRepairTime.ClientID %>').value=arr[5];
          
          var responseUnit = document.all.<%=ddlResponseTime.ClientID %>;
          for(var i=0;i<responseUnit.options.length;i++)
          {
              if(responseUnit.options[i].value==arr[2])
              {
                 responseUnit.options[i].selected=true;
                 break;
              }
          }
          
          var funRestoreUnit = document.all.<%=ddlFunRestoreTime.ClientID %>;
          for(var i=0;i<funRestoreUnit.options.length;i++)
          {
              if(funRestoreUnit.options[i].value==arr[4])
              {
                 funRestoreUnit.options[i].selected=true;
                 break;
              }
          }
          
          var repairUnit = document.all.<%=ddlRepairTime.ClientID %>;
          for(var i=0;i<repairUnit.options.length;i++)
          {
              if(repairUnit.options[i].value==arr[6])
              {
                 repairUnit.options[i].selected=true;
                 break;
              }
          }
        }


    function MaintainSeletedChange()
    {
        var obj = document.all.<%=ddlMaintainType.ClientID %>;
        if(obj.value=="1") // 自维单位
        {
            document.getElementById("btSearchRank").disabled=true;
            
            var objRank = document.all.<%=ddlRank.ClientID %>;
            objRank.options[0].selected=true;
            var responseUnit = document.all.<%=ddlResponseTime.ClientID %>;
            responseUnit.options[2].selected=true;
            var funRestoreUnit = document.all.<%=ddlFunRestoreTime.ClientID %>;
            funRestoreUnit.options[2].selected=true;
            var repairUnit = document.all.<%=ddlRepairTime.ClientID %>;
            repairUnit.options[2].selected=true;
            
            
            $get('<%=tbResponseTime.ClientID %>').value="9999";
            $get('<%=tbFunRestoreTime.ClientID %>').value="9999";
            $get('<%=tbRepairTime.ClientID %>').value="9999";
            
            document.getElementById("MaintainPlanTime").style.visibility="hidden";
            document.getElementById("MaintainPlanRankTitle").style.visibility="hidden";
            document.getElementById("MaintainPlanRankContent").style.visibility="hidden";
        }
        else
        {
            document.getElementById("btSearchRank").disabled=false;
            
            
            $get('<%=tbResponseTime.ClientID %>').value="";
            $get('<%=tbFunRestoreTime.ClientID %>').value="";
            $get('<%=tbRepairTime.ClientID %>').value="";
            
            document.getElementById("MaintainPlanTime").style.visibility="visible";
            document.getElementById("MaintainPlanRankTitle").style.visibility="visible";
            document.getElementById("MaintainPlanRankContent").style.visibility="visible";
        }
    }
    

    </script>

</asp:Content>
