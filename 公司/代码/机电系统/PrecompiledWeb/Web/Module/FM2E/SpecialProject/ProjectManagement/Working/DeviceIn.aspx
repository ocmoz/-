<%@ page title="设备进场" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_DeviceIn, App_Web_fzwntbc0" %>
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

            //产品名称
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DeviceName')).innerText;
            document.getElementById('<%= Label_Equipment.ClientID %>').innerText = name;

            //规格型号
            var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Model')).innerText;
            document.getElementById('<%= Label_Model.ClientID %>').innerText = model;

            //尺寸
            var price = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Size')).innerText;
            document.getElementById('<%= Label_Size.ClientID %>').innerText = price;

            //功能
            var unit = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Usage')).innerText;
            document.getElementById('<%= Label_Usage.ClientID %>').innerText = unit;

            //状况
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Status')).innerText;
            document.getElementById('<%= Label_Status.ClientID %>').innerText = remark;


            //数量
            var count = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PlanCount')).innerText;
            document.getElementById('<%= Label_PlanCount.ClientID %>').innerText = count;

            document.getElementById('<%= TextBox_Count.ClientID %>').value = "";
            document.getElementById('<%= TextBox_Time.ClientID %>').value = "";
        }

        //保存编辑的项
        function saveEditItem() {

            var cfm = confirm('确认进场？确认后不能再修改');
            if (cfm == false) {
                return;
            }
           
            //数量
            var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
            if (!checkFloat(count, '标书数量')) {
                return;
            }

            //日期
            var time = trim(document.getElementById('<%= TextBox_Time.ClientID %>').value);
            if (!checkDate(time, '进场日期')) {
                return;
            }
            
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
        HeadOPTxt="目前操作功能：设备进场" HeadHelpTxt="根据进场设备计划，进行设备进场情况登记">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="ViewProject.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    
    
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 200px;display:none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    设备进场<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    设备：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:Label ID="Label_Equipment" runat="server" ></asp:Label>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    规格型号：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:Label ID="Label_Model" runat="server"></asp:Label>
                </td>
        <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    尺寸：</td> <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:Label ID="Label_Size" runat="server"></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    功能：</td><td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_Usage" runat="server" ></asp:Label>
                </td>
           </tr>
           <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    状况：</td> <td class="table_none table_none_NoWidth" style="height: 30px;" >
                    <asp:Label ID="Label_Status" runat="server"></asp:Label>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    标书数量：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_PlanCount" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    本次进场数量：</td> <td class="table_none table_none_NoWidth" style="height: 30px;" >
                    <asp:TextBox ID="TextBox_Count" runat="server"></asp:TextBox>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    进场日期：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Time" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save" class="button_bak"
                type="button" value="进场" onclick="javascript:saveEditItem();"/>
            <input id="Button_OK" class="button_bak" style="display:none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" 
                Text="取消" />
        </center>
    </asp:Panel>
    
    
    <div id="div_table">
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle">
                    专项工程&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true" runat="server" ForeColor="Blue"></asp:Label>
                    &nbsp;进场设备清单
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
                                    设备
                                </th>
                                <th style="width:10%; text-align:center">
                                    规格型号
                                </th>
                                <th style="width:10%; text-align:center">
                                   尺寸以及功能
                                </th>
                                <th style="width:5%; text-align:center">
                                    状况
                                </th>
                                <th style="width:15%; text-align:left">
                                    标书数量
                                </th>
                                <th style="width:5%; text-align:center">
                                    已进数量
                                </th>
 
                                <th style="width:5%; text-align:center">
                                    添加进场
                                </th>
                            </tr>
                            <asp:Repeater ID="Repeater_ItemList" runat="server">
                                <ItemTemplate>
                                    <tr style="height: 30px" id="tr_item" runat="server">
                                        <td style="text-align:center">
                                            <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                            <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server"/>
                                        </td>
                                        <td style="text-align:center">
                                             <asp:Label ID="Label_DeviceName" runat="server" Text='<%# Eval("DeviceName") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                           <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                             <asp:Label ID="Label_Size" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                            <asp:Label ID="Label_Usage" runat="server" Text='<%# Eval("Usage") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                           <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </td>
                                        <td>
                                             <asp:Label ID="Label_PlanCount" runat="server" 
                                                Text='<%# Eval("PlanCount", "{0:#,0.##}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align:center">
                                           <asp:Label ID="Label_ActualCount" runat="server" 
                                                Text='<%# Eval("ActualCount", "{0:#,0.##}") %>'></asp:Label>
                                        </td>
                          

                                        <td style="text-align:center">
                                          <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False"
                                                ImageUrl="~/images/ICON/edit.gif"  OnClientClick="javascript:setModalPopup(this.id,true);" />
                                                
                                             <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" 
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                                             </cc2:ModalPopupExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="10" style="word-break:break-all">
                                             <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
                            style="border-collapse: collapse; background-color:LightBlue">
                                                <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                <th style="width:5%; text-align:center">
                                    序号
                                </th>
                                <th style="width:45%; text-align:center">
                                    时间
                                </th>
                                <th style="width:45%; text-align:center">
                                    数量
                                </th>
                                </tr>
                                                <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Eval("DeviceInRecordList") %>'>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width:5%; text-align: center">
                                                                <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                            </td>
                                                            <td style="width:45%; word-wrap:break-word;word-break:break-all">
                                                                <asp:Label ID="Label_Time" Text='<%# Eval("Time","{0:yyyy-MM-dd}") %>' runat="server">
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:45%; word-wrap:break-word;word-break:break-all">
                                                                <asp:Label ID="Label_Count" Text='<%# Eval("Count","{0:#,0.#####}") %>' runat="server">
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
                <td style="text-align:right">
                    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                                onserverclick="Button_Save_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
