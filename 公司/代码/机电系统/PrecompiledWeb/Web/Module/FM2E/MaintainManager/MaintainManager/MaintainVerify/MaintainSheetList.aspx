<%@ page title="维护记录核实" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainVerify_MaintainSheetList, App_Web_drzodbbu" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Maintain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="维护记录核实"
        HeadOPTxt="目前操作功能：维护记录列表" HeadHelpTxt="请按照条件筛选记录">
        
    </cc1:HeadMenuWebControls>
<script type="text/javascript" language="javascript" defer="defer">
    //地址选定
    var changed = 'false';
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

    function Clear(target) {
        switch (target) {
            case 'Address':
                {
                    document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '0';
                    document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
                    break;
                }
            default: break;
        }
    }
    </script>

<div style="width: 100%; ">
      <%--  <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="维护计划" ID="TabPanel1">
                <ContentTemplate>--%>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
                 
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                        
                        
                        <tr>
                            <td   style="width: 25%; " class="table_body table_body_NoWidth">
                                系统：
                           
                                <asp:DropDownList ID="DDLSystem" runat="server" >
                                </asp:DropDownList>
                            </td>
                           <td  style="width: 25%; " class="table_body table_body_NoWidth">部门：<asp:DropDownList ID="DropDownList_Department" runat="server"></asp:DropDownList></td>
                            <td  style="width: 15%; " class="table_body table_body_NoWidth">
                                类型：<asp:DropDownList ID="DropDownList_Type" runat="server"
                                    >
                                </asp:DropDownList>
                            </td>
                            <td  style="width: 15%; " class="table_body table_body_NoWidth">
                                周期：<asp:DropDownList ID="DropDownList_Period" runat="server"
                                    >
                                </asp:DropDownList>
                            </td>
                            
                            <td   style="width: 20%; " class="table_body table_body_NoWidth">
                                临时： <asp:DropDownList ID="DropDownList_IsTemp" runat="server">
                                <asp:ListItem Text="不限" Value="0"></asp:ListItem>
                                <asp:ListItem  Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Value="2"></asp:ListItem>
                                </asp:DropDownList></td>
                            
                        </tr>
                        <tr>
                         <td  class="table_body table_body_NoWidth">
                                维护人姓名： <asp:TextBox ID="TextBox_MaintainerName" runat="server"></asp:TextBox>
                            </td>
                            
                        <td colspan="3"  class="table_body table_body_NoWidth">
                                维护时间： <asp:TextBox  class="input_calender"
                                onfocus="javascript:HS_setDate(this);" ID="TextBox_TimeLower" runat="server"></asp:TextBox>——<asp:TextBox 
                                    ID="TextBox_TimeUpper"  class="input_calender"
                                onfocus="javascript:HS_setDate(this);" runat="server" ></asp:TextBox>
                            </td>
                             
                           
                               <td  class="table_body table_body_NoWidth">
                                异常：<asp:DropDownList ID="DropDownList_HasAbnormal" runat="server">
                                </asp:DropDownList>
                            </td>
                             
                        </tr>
                        <tr>
                         <td  class="table_body table_body_NoWidth">
                                核实人姓名： <asp:TextBox ID="TextBox_ConfirmerName" runat="server"></asp:TextBox>
                            </td>
                            
                        <td colspan="3"  class="table_body table_body_NoWidth">
                                核实时间： <asp:TextBox ID="TextBox_ConfirmTimeLower" runat="server"  class="input_calender"
                                onfocus="javascript:HS_setDate(this);"></asp:TextBox>——<asp:TextBox 
                                    ID="TextBox_ConfirmTimeUpper" runat="server"  class="input_calender"
                                onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                            </td>
                        
                          <td  class="table_body table_body_NoWidth">
                                核实结果： <asp:DropDownList ID="DropDownList_ConfirmResult" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                        
                            <td colspan="2"  class="table_body table_body_NoWidth">地址： <asp:TextBox ID="TextBox_Address" runat="server" Width="75%" onfocus="javascript:showPopWin('选择地址','../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"></asp:TextBox> <input class="button_bak" onclick="javascript:Clear('Address');" type="button" value="清除地址"
                            id="Button_ClearAddress" /><input type="hidden" id="Hidden_AddressID" runat="server" value="0" /><input type="hidden" id="Hidden_AddressCode" runat="server" value="00" />
                            </td>
                            <td  colspan="2" class="table_body table_body_NoWidth">
                                维护单：
                                <asp:TextBox ID="TextBox_SheetName" runat="server" Width="75%"></asp:TextBox>
                            </td>
                            <td  class="table_body table_body_NoWidth"><asp:Button ID="Button_Filter" runat="server" Text="筛选"  CssClass="button_bak" 
                                    onclick="Button_Filter_Click" /></td>
                        </tr>
                    </table>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                      
                       <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                      <tr>
                      <td  style="width: 5%; " class="table_body_WithoutWidth">序号</td>
                      
                      <td  style="width: 10%; " class="table_body_WithoutWidth">系统</td>
                      <td  style="width: 10%; " class="table_body_WithoutWidth">部门</td>
                      <td   class="table_body_WithoutWidth">维护单</td>
                      <td  style="width: 7%; " class="table_body_WithoutWidth">类型</td>
                      <td  style="width: 5%; " class="table_body_WithoutWidth">异常</td>
                      
                      <td  style="width: 8%; " class="table_body_WithoutWidth">维护人</td>
                      
                      <td  style="width: 8%; " class="table_body_WithoutWidth">时间</td>
                      
                      <td  style="width: 25%; "  class="table_body_WithoutWidth">核实</td>
                      
                      <td  style="width: 5%; " class="table_body_WithoutWidth">查看</td>
           
                      
                      
                      </tr>
                      
                      <asp:Repeater ID="Repeater_MaintainSheet" runat="server" 
                              onitemcommand="Repeater_MaintainSheet_ItemCommand" 
                              onitemdatabound="Repeater_MaintainSheet_ItemDataBound">
                      <ItemTemplate>
                      
                      <tr runat="server" id="tr_mainrow">
                      <td  rowspan="2"><%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.ItemIndex + 1%></td>
                       <td><%# Eval("SystemName")%></td>
                      <td><%# Eval("DepartmentName")%></td>
                     
                      <td style="text-align:left" class="table_none_WithoutWidth">
                      <%# Eval("SheetNO")%>&nbsp;&nbsp;
                      <%# Eval("SheetName")%></td>
                      <td><%# EnumHelper.GetDescription((Enum)Eval("MaintainType"))%></td>
                      <td>
                      <font color="red">
                      <%# (bool)Eval("HasAbnormal") ? "■":"" %>
                      
                      </font></td>
                                     
                      <td><%#  Eval("MaintainerName") %></td>
                      <td><%#  Eval("MaintainTime","{0:yyyy-MM-dd}") %></td>
                      <td>
                            <%# string.Format("<span style='display:{0}'>",(FM2E.Model.Maintain.MaintainConfirmResult)Eval("ConfirmResult") == FM2E.Model.Maintain.MaintainConfirmResult.NotConfirm? "none":"block")  %>
                            <%#  EnumHelper.GetDescription((Enum)Eval("ConfirmResult")) %>
                            </span>
                            
                            <%# string.Format("<span style='display:{0}'>", (FM2E.Model.Maintain.MaintainConfirmResult)Eval("ConfirmResult") != FM2E.Model.Maintain.MaintainConfirmResult.NotConfirm ? "none" : "block")%>
                            
                            <asp:RadioButtonList ID="RadioButtonList_Confirm" runat="server" RepeatDirection="Horizontal">
                           
                            </asp:RadioButtonList>
                            <asp:TextBox ID="TextBox_Remark" runat="server"></asp:TextBox>
                            <asp:Button ID="Button_Confirm" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/approval.gif"
                                   CommandName="ConfirmCMD" CommandArgument='<%# Eval("SheetID") %>'  Text="核实" CssClass="button_bak" OnClientClick="return confirm('确认核实？')"/>
                            
                            </span>
                            
                      </td>
                       <td><asp:ImageButton ID="ImageButton_View" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/select.gif"
                                                CommandName="ViewCMD" CommandArgument='<%# Eval("SheetID") %>' /></td>

                      
                      </tr>
                      <!--该项对应的上次执行-->
                      <tr>
                        <td colspan="9" style="text-align:left" class="table_none_WithoutWidth">
                        维护地点：<%# Eval("AddressName")%><br />
                            执行周期：<%# Eval("Period") %><%# EnumHelper.GetDescription((Enum)Eval("PeriodUnit")) %>
                            例行/临时：<%# (bool)Eval("IsTemp") ? "临时" : "例行"%>
                            <br />
                            <%# (DateTime)Eval("LastExecuteTime") == DateTime.MinValue ? "制定时间：" + Eval("SaveTime", "{0:yyyy-MM-dd}")
                                
                                : "上次时间："+ Eval("LastExecuteTime","{0:yyyy-MM-dd}").ToString()%> 
                            
                            距离今天：
                            <%# (DateTime)Eval("LastExecuteTime") == DateTime.MinValue 
                                ?
                                                                ((DateTime.Now - (DateTime)Eval("SaveTime")).TotalDays.ToString("0") + "天")
                                : ((DateTime.Now -(DateTime)Eval("LastExecuteTime")).TotalDays.ToString("0") +"天")  %>
                            
                            
                            <%# 
                                
                                (int)Eval("ExpiredDays") > 0 ? ("<font color='red'>超时：" + Eval("ExpiredDays").ToString() + "天</font>")
                                                                : "<font color='green'>未超时</font>"
                                %>
                            
                            
                            <span style='display:<%# Convert.ToInt32(Eval("ConfirmResult"))!=(int)MaintainConfirmResult.NotConfirm?"inline":"none"%>'>
                            <br />核实人：<%#Eval("ConfirmerName") %>&nbsp;&nbsp;
                            核实时间：<%#Convert.ToDateTime(Eval("ConfirmTime")) == DateTime.MinValue ? "" : Convert.ToDateTime(Eval("ConfirmTime")).ToString("yyyy-MM-dd HH:mm")%>&nbsp;&nbsp;
                            核实备注：<%#Eval("ConfirmRemark") %></span>
                        </td>
                      </tr>
                      
                      </ItemTemplate>
                      
                      </asp:Repeater>
                      </table>
                      
                      
                       
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left">
                        </cc1:AspNetPager>
                    </div>
                </ContentTemplate>
                
                </asp:UpdatePanel>
                   
            <%--    </ContentTemplate>
            </cc2:TabPanel>
            
        </cc2:TabContainer>--%>
    </div>
</asp:Content>

