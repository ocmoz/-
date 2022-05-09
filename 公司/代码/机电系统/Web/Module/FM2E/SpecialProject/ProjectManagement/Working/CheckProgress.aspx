<%@ Page Title="专项工程进度检查" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="CheckProgress.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_CheckProgress" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">

        //编辑的时候设置模式对话框的值
        function setModalPopup(button_id) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_ItemID')).value;
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

            //工程项目
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemName')).innerText;
            document.getElementById('<%= Label_ItemName.ClientID %>').innerText = name;

            //前置项目
            var prefixid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_PrefixItemID')).value;
            
            if (prefixid != '0') {
                document.getElementById('<%= span_inputdays.ClientID %>').style.display = 'block';
                document.getElementById('<%= tr_startend.ClientID %>').style.display = 'none';
            }
            else {
                document.getElementById('<%= span_inputdays.ClientID %>').style.display = 'none';
                document.getElementById('<%= tr_startend.ClientID %>').style.display = 'block';
            }

            document.getElementById('<%= Hidden_SelectPrefixItemID.ClientID %>').value = prefixid;
            var prefixitemname = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PrefixItemName')).innerText;
            document.getElementById('<%= Label_PrefixItemName.ClientID %>').innerText = prefixitemname;
            
            //前置N天后
            var daysafter_control = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_DaysAfter'));
            if (daysafter_control != null) {
                daysafter = daysafter_control.value;
                document.getElementById('<%= Label_DaysAfter.ClientID %>').innerText = daysafter;
            }

            //开始
            var start = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_StartTime')).innerText;
            document.getElementById('<%= Label_StartTime.ClientID %>').innerText = start;

            //结束
            var end = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_EndTime')).innerText.replace(regS, "");
            document.getElementById('<%= Label_EndTime.ClientID %>').innerText = end;

            //人力
            var hr = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_HRPlan')).innerText;
            document.getElementById('<%= Label_HRPlan.ClientID %>').innerText = hr;

            //设备
            var device = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DevicePlan')).innerText;
            document.getElementById('<%= Label_DevicePlan.ClientID %>').innerText = device;

            //进度
            document.getElementById('<%= TextBox_Progress.ClientID %>').value = "";

            //日期
            document.getElementById('<%= TextBox_Time.ClientID %>').value = "";

            document.getElementById('<%= TextBox_HR.ClientID %>').value = "";
            document.getElementById('<%= TextBox_Quality.ClientID %>').value = "";
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";
        }


        //保存编辑的项
        function saveEditItem() {

            var progress = trim(document.getElementById('<%= TextBox_Progress.ClientID %>').value);

            if (!checkFloat(progress, '最新进度')) {
                return false;
            }

            if (parseFloat(progress) < 0 || parseFloat(progress) > 100) {
                alert('最新进度百分比必须在0~100之间');
                return false;
            }

            var time = trim(document.getElementById('<%= TextBox_Time.ClientID %>').value);

            if (!checkDate(time, '截至日期')) {
                return false;
            }

            var hr = trim(document.getElementById('<%= TextBox_HR.ClientID %>').value);
            if (hr.length == 0) {
                alert('请输入实际人力安排情况');
                return false;
            }

            var quality = trim(document.getElementById('<%= TextBox_Quality.ClientID %>').value);
            if (quality.length == 0) {
                alert('请输入质量评估');
                return false;
            }

            
            
            var cfm = confirm('确认更新进度？添加更新记录后不能删除。');
            if (cfm == false)
                return false;

            document.getElementById('Button_OK').click();
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }

        //浮点数检查
        function checkFloat(value, text) {
            var floatVal = parseFloat(value);
            if (isNaN(floatVal) || floatVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个数字。");
                return false;
            }
            return true;
        }
        //时期检查
        function checkDate(value, text) {
            //日期
            var found = value.match(/(\d{1,5})-(\d{1,2})-(\d{1,2})/);
            var found2 = value.match(/(\d{1,5})\/(\d{1,2})\/(\d{1,2})/);
            if (found == null || found[0] != value || found[2] > 12 || found[3] > 31) {
                if (found2 == null || found2[0] != value || found2[2] > 12 || found2[3] > 31) {
                    //alert(info+"\n"+name+"的格式不正确:\n\""+value+"\"不是一个日期\n提示：[2000-01-01]");
                    alert(text + "\n其格式不正确:\n\"" + value + "\"不是一个日期\n提示：[2000-01-01]");
                    return false;
                }
            }
            var year = trim0(found[1]);
            var month = trim0(found[2]) - 1;
            var date = trim0(found[3]);
            var d = new Date(year, month, date);
            if (d.getFullYear() != year || d.getMonth() != month || d.getDate() != date) {
                //alert(info+"\n"+name+"的内容不正确:\n\""+value+"\"不是一个正确的日期\n提示：[2000-01-01]");
                alert(text + "\n其内容不正确:\n\"" + value + "\"不是一个正确的日期\n提示：[2000-01-01]");
                return false;
            }
            return true;
        }
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：进度检查" HeadHelpTxt="更新进度情况后，横道图会自动更新">
       <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="ViewProject.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 350px; display:none"
        CssClass="modalPopup">
                <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                    text-indent: 13px; border: solid 1px #a7c5e2;" border="1" id="table_edit" runat="server">
                    <tr>
                        <td class="Table_searchtitle" style="text-align: center" colspan="4">
                            工程项<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            项目：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;"  colspan="3">
                            <asp:Label ID="Label_ItemName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            前置项目：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;"  colspan="3">
                            <input type="hidden" value="" id="Hidden_SelectPrefixItemID" runat="server" />
                             <asp:Label ID="Label_PrefixItemName" runat="server"></asp:Label>
                            <span id="span_inputdays" style="display: none" runat="server">
                                <asp:Label ID="Label_DaysAfter" runat="server" Text="1"></asp:Label>天后开始 共：
                                <asp:Label ID="Label_DaysSpan" runat="server" Text="1"></asp:Label>天 </span>
                        </td>
                    </tr>
                    <tr id="tr_startend" runat="server">
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            起止日期：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                            <asp:Label ID="Label_StartTime" runat="server" ></asp:Label>-<asp:Label
                                ID="Label_EndTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            原定人力安排：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;"  colspan="3">
                            <asp:Label ID="Label_HRPlan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            原定设备安排：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;"  colspan="3">
                            <asp:Label ID="Label_DevicePlan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            最新进度：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;">
                            <asp:TextBox ID="TextBox_Progress" runat="server"></asp:TextBox>%<span style="color: Red; font-weight: bold">*</span>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            截至日期：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;">
                            <asp:TextBox ID="TextBox_Time" runat="server"  class="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox> <span style="color: Red; font-weight: bold">*</span>
                        </td>
                    </tr>
                     <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            实际人力安排：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;"  colspan="3">
                            <asp:TextBox ID="TextBox_HR" runat="server" Width="90%"></asp:TextBox> <span style="color: Red; font-weight: bold">*</span>
                        </td>
                    </tr>
                     <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            质量评价：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;"  colspan="3">
                            <asp:TextBox ID="TextBox_Quality" runat="server" Width="90%"></asp:TextBox> <span style="color: Red; font-weight: bold">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                            备注：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px;"  colspan="3">
                            <asp:TextBox ID="TextBox_Remark" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                </table>

  
        <center>
            <input id="Button_Save" class="button_bak" type="button" value="更新进度" onclick="javascript:saveEditItem();" />
            <input id="Button_OK" class="button_bak" style="display: none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    专项工程&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true"
                        runat="server" ForeColor="Blue"></asp:Label>
                    &nbsp;施工计划
                </td>
            </tr>
            <tr>
                <td>
                
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                <th style="width:3%; text-align:center">
                                    序号
                                </th>
                                <th style="width:10%; text-align:center">
                                    项目
                                </th>
                                <th style="width:10%; text-align:center">
                                    前置项目
                                </th>
                                <th style="width:10%; text-align:center">
                                   时间
                                </th>
                                <th style="width:5%; text-align:center">
                                    时长(天)
                                </th>
                                <th style="width:15%; text-align:left">
                                    人力安排
                                </th>
                                <th style="width:5%; text-align:center">
                                    设备安排
                                </th>
 
                                <th style="width:5%; text-align:center">
                                    当前进度
                                </th>
                                
                                <th style="width:5%; text-align:center">
                                    状态
                                </th>
                                
                                <th style="width:5%; text-align:center">
                                    更新进度
                                </th>
                            </tr>
                              <asp:Repeater ID="Repeater_ProgressCheckItemList" runat="server">
                                <ItemTemplate>
                                    <tr style="height: 30px" id="tr_item" runat="server">
                                        <td style="text-align:center">
                                           <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                        </td>
                                        <td style="text-align:center">
                                              <asp:Label ID="Label_ItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                            <asp:Label ID="Label_PrefixItemName" runat="server" Text='<%# Eval("PrefixItemName") %>'></asp:Label>
                                    <input type="hidden" id="Hidden_PrefixItemID" value='<%# Eval("PrefixItemID") %>'
                                        runat="server" />
                                    <span id="span_daysafter" runat="server" visible='<%# Convert.ToInt64( Eval("PrefixItemID"))>0 %>'>
                                        (<asp:Label ID="Label_DaysAfter" runat="server" Text='<%# Eval("DaysAfter") %> '></asp:Label>天后开始)</span>
                                    <input type="hidden" id="Hidden_DaysAfter" value='<%# Eval("DaysAfter") %>' runat="server" />
                                        </td>
                                        <td style="text-align:center">
                                                <asp:Label ID="Label_StartTime" runat="server" Text='<%# Eval("StartTime", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                    -
                                    <asp:Label ID="Label_EndTime" runat="server" Text='<%# Eval("EndTime", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                              <asp:Label ID="Label_Days" runat="server" Text='<%# Eval("Days") %>'></asp:Label>
                                        </td>
                                        <td>
                                             <asp:Label ID="Label_HRPlan" runat="server" Text='<%# Eval("HRPlan") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                         <asp:Label ID="Label_DevicePlan" runat="server" Text='<%# Eval("DevicePlan") %>'></asp:Label>
                                        </td>
                            <td>
                                           <asp:Label ID="Label_Progress" runat="server" Text='<%# Eval("Progress","{0:P}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                          <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label>
                                        </td>

                                        <td style="text-align:center">
                                          <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                        Text="修改" OnClientClick="javascript:setModalPopup(this.id,true);" />
                                    <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                        PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_OK"
                                        CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                    </cc2:ModalPopupExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="10" style="word-break:break-all">
                                             <table width="100%" cellpadding="0" cellspacing="0" border="0" bordercolor="#000000"
                            style="border-collapse: collapse; background-color:white">
                                               
                                                <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Eval("ProgressCheckRecord") %>' >
                                              
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                [<asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>]
                                                            </td>
                                                            <td style="word-wrap:break-word;word-break:break-all">
                                                                <asp:Label ID="Label_Time" Text='<%# Eval("CheckTime","{0:yyyy-MM-dd}") %>' runat="server">
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="word-wrap:break-word;word-break:break-all">
                                                                <asp:Label ID="Label_Progress" Text='<%# Eval("Progress","{0:P}") %>' runat="server">
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="word-wrap:break-word;word-break:break-all">
                                                                人力：<asp:Label ID="Label_HR" Text='<%# Eval("HR") %>' runat="server">
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="word-wrap:break-word;word-break:break-all">
                                                               质量：<asp:Label ID="Label_Quality" Text='<%# Eval("Quality") %>' runat="server">
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="word-wrap:break-word;word-break:break-all">
                                                               备注：<asp:Label ID="Label_Remark" Text='<%# Eval("Remark") %>' runat="server">
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="word-wrap:break-word;word-break:break-all">
                                                                检查人：<asp:Label ID="Label_Checker" Text='<%# Eval("Checker") %>' runat="server">
                                                                     </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>
                    
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                        onserverclick="Button_Save_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="div_graph">
        <table id="Table_Graph" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    专项工程&nbsp;<asp:Label ID="Label_ProjectName2" Font-Underline="true" Font-Bold="true" runat="server"
                        ForeColor="Blue"></asp:Label>
                    &nbsp;施工计划横道图
                </td>
            </tr>
             <tr>
                <td class="Table_searchtitle">
                  <div id="div_gant" class="fixed" enableviewstate="false" runat="server" style="width:900px; height:400px; overflow:auto"></div></td>
            </tr>
        </table>
    </div>
</asp:Content>
