<%@ Page Title="专项工程款项支付" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="Pay.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_Pay" %>

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

            //名称
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemName')).innerText;
            document.getElementById('<%= Label_ItemName.ClientID %>').innerText = name;

            //支付时间
            var time = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Time')).innerText;
            document.getElementById('<%= Label_Time.ClientID %>').innerText = time;

            //支付方式
            var m = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Method')).innerText;
            document.getElementById('<%= Label_Method.ClientID %>').innerText = m;


            //金额
            var a = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Amount')).innerText.replace(regS, "");
            document.getElementById('<%= Label_Amount.ClientID %>').innerText = a;


            //备注
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
            document.getElementById('<%= Label_Remark.ClientID %>').innerText = remark;

            document.getElementById('<%= TextBox_Paid.ClientID %>').value = "";

        }


        //编辑的时候设置模式对话框的值
        function setModalPopupContract(button_id) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_ItemID')).value;
            document.getElementById('<%= Hidden_ItemIDContract.ClientID %>').value = itemid;

            //名称
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemName')).innerText;
            document.getElementById('<%= Label_ItemNameContract.ClientID %>').innerText = name;

            //支付时间
            var days = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DaysAfter')).innerText.replace(regS, "");
            document.getElementById('<%= Label_DaysAfter.ClientID %>').innerText = days;

            //支付方式
            var m = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Method')).innerText;
            document.getElementById('<%= Label_MethodContract.ClientID %>').innerText = m;


            //金额
            var a = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Amount')).innerText.replace(regS, "");
            document.getElementById('<%= Label_AmountContract.ClientID %>').innerText = a;


            //备注
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
            document.getElementById('<%= Label_RemarkContract.ClientID %>').innerText = remark;

            //工作项
            var s = document.getElementById('<%= Label_PlanItemName.ClientID %>');
            var job = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PlanItemName')).innerText;
            s.innerText = job;

            document.getElementById('<%= TextBox_PaidContract.ClientID %>').value = "";

        }


        //编辑的时候设置模式对话框的值
        function setModalPopupMonthly(button_id) {
            document.getElementById('<%= TextBox_PayTimeMonthly.ClientID %>').value = "";
            document.getElementById('<%= TextBox_AmountMonthly.ClientID %>').value = "";
            document.getElementById('<%= TextBox_PaidMonthly.ClientID %>').value = "";
            document.getElementById('<%= TextBox_MethodMonthly.ClientID %>').value = "";
            document.getElementById('<%= TextBox_PaidMonthly.ClientID %>').value = "";
            document.getElementById('<%= TextBox_RemarkMonthly.ClientID %>').value = "";

            document.getElementById('<%= TextBox_ProgressMonthly.ClientID %>').value = "";

        }

        //保存编辑的项
        function saveEditItem() {

            //金额
            var count = trim(document.getElementById('<%= TextBox_Paid.ClientID %>').value);
            if (!checkFloat(count, '实际支付金额')) {
                return false;
            }

            var cfm = confirm('确认支付？支付后不能再修改');
            if (cfm == false)
                return false;

            document.getElementById('Button_OK').click();
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }

        //保存编辑的项
        function saveEditItemContract() {
            //金额
            var count = trim(document.getElementById('<%= TextBox_PaidContract.ClientID %>').value);
            if (!checkFloat(count, '实际支付金额')) {
                return;
            }
            var cfm = confirm('确认支付？支付后不能再修改');
            if (cfm == false)
                return false;
            document.getElementById('Button_OKContract').click();
            document.getElementById('<%= Button_SaveContract.ClientID %>').click();
        }

        //保存编辑的项
        function saveEditItemMonthly() {

            var exist = document.getElementById('<%= Hidden_ExsitMonths.ClientID %>').value;
            var sy = document.getElementById('<%= DropDownList_Year.ClientID %>');
            var sm = document.getElementById('<%= DropDownList_Month.ClientID %>');

            var year = parseInt(sy.options[sy.selectedIndex].value);
            
            var month = parseInt(sm.options[sm.selectedIndex].value);
            
            var timevalue = year * 12 + month;

            var timestr = timevalue.toString() + ',';
            
            if (exist.indexOf(timestr) >= 0) {
                alert(year + '年' + month + '月的支付项已经存在，请选择其他月份');
                return false;
            }
            
            //金额
            var count = trim(document.getElementById('<%= TextBox_AmountMonthly.ClientID %>').value);
            if (!checkFloat(count, '计划支付金额')) {
                return false;
            }
            //金额
            count = trim(document.getElementById('<%= TextBox_PaidMonthly.ClientID %>').value);
            if (!checkFloat(count, '实际支付金额')) {
                return false;
            }

            //金额
            count = trim(document.getElementById('<%= TextBox_PaidMonthly.ClientID %>').value);
            if (!checkFloat(count, '实际支付金额')) {
                return false;
            }

            //进度
            var progress = trim(document.getElementById('<%= TextBox_ProgressMonthly.ClientID %>').value);
            if (!checkFloat(progress, '项目进度')) {
                return false;
            }

            if (parseFloat(progress) < 0 || parseFloat(progress) > 100) {
                alert('项目进度百分比必须在0~100之间');
                return false;
            }
            
            //支付方式
            var date = trim(document.getElementById('<%= TextBox_PayTimeMonthly.ClientID %>').value);
            if (!checkDate(date, '实际支付时间')) {
                return false;
            }
            
            
            var cfm = confirm('确认支付？支付后不能再修改');
            if (cfm == false)
                return false;
            document.getElementById('Button_OKMonthly').click();
            document.getElementById('<%= Button_SaveMonthly.ClientID %>').click();
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

        //浮点数检查
        function checkFloat(value, text) {
            var floatVal = parseFloat(value);
            if (isNaN(floatVal) || floatVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个数字。");
                return false;
            }
            return true;
        }



    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：支付" HeadHelpTxt="预支付登记、月进度支付登记、合同支付登记">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="ViewProject.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 200px; display: none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    预支付项<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    项名称：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_ItemName" runat="server"></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    预计支付时间：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_Time" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    计划支付金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_Amount" runat="server"></asp:Label>元
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    支付方式：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_Method" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:Label ID="Label_Remark" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    实际支付金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Paid" runat="server"></asp:TextBox>元<span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save" class="button_bak" type="button" value="支付" onclick="javascript:saveEditItem();" />
            <input id="Button_OK" class="button_bak" style="display: none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    <asp:Panel ID="Panel_EditItemContract" runat="server" Style="width: 95%; height: 250px;
        display: none" CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    合同支付项<input type="hidden" value="" id="Hidden_ItemIDContract" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    项名称：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:Label ID="Label_ItemNameContract" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    对应工作项：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_PlanItemName" runat="server"></asp:Label>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    延迟时间：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_DaysAfter" runat="server"></asp:Label>天
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    原定金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_AmountContract" runat="server"></asp:Label>元
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    支付方式：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:Label ID="Label_MethodContract" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:Label ID="Label_RemarkContract" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    实际支付金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_PaidContract" runat="server"></asp:TextBox>元<span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_SaveContract2" class="button_bak" type="button" value="支付" onclick="javascript:saveEditItemContract();" />
            <input id="Button_OKContract" class="button_bak" style="display: none" value="OK" />
            <asp:Button ID="Button_Cancel_EditContract" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    
    <asp:Panel ID="Panel_EditItemMonthly" runat="server" Style="width: 95%; height: 250px; display: none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    进度支付项<input type="hidden" value="0" id="Hidden_EditItemIDMonthly" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="hidden" id="Hidden_ExsitMonths" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    款项月份：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:DropDownList ID="DropDownList_Year" runat="server"></asp:DropDownList>年
                    <asp:DropDownList ID="DropDownList_Month" runat="server"></asp:DropDownList>月
                    <span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    实际支付时间：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_PayTimeMonthly" runat="server"  class="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    计划支付金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_AmountMonthly" runat="server"></asp:TextBox>元<span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    实际支付金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_PaidMonthly" runat="server"></asp:TextBox>元<span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
             <tr>
                 <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    项目进度：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_ProgressMonthly" runat="server"></asp:TextBox>%<span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    支付方式：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_MethodMonthly" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_RemarkMonthly" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_SaveMonthly2" class="button_bak" type="button" value="支付" onclick="javascript:saveEditItemMonthly();" />
            <input type="button" id="Button_OKMonthly" class="button_bak" style="display: none" value="OK" />
            <asp:Button ID="Button_Cancel_EditMonthly" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <cc2:TabPanel runat="server" HeaderText="预支付项" ID="TabPanel1">
            <ContentTemplate>
                <div id="div_table">
                    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                专项工程&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true"
                                    runat="server" ForeColor="Blue"></asp:Label>
                                &nbsp;预支付项清单
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridview_PrePayItemList" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="gridview_ItemList_RowDataBound" HeaderStyle-BackColor="#efefef"
                                    DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" RowStyle-Height="20px"
                                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="预定支付日期">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Time" runat="server" Text='<%#  Eval("Time","{0:yyyy-MM-dd}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额(元)<br/>计划支付/已经支付">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_Paid" runat="server" Text='<%# Eval("Paid", "{0:#,0.##}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_TotalPaid" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付方式">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付者">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Payee" runat="server" Text='<%# Eval("Payee") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                                    Text="修改" OnClientClick="javascript:setModalPopup(this.id);" />
                                                <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_OK"
                                                    CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                                </cc2:ModalPopupExtender>
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <center>
                                            未添预支付项</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
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
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="合同支付项" ID="TabPanel2">
            <ContentTemplate>
                <div id="div_tableContract">
                    <table id="TableContract" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                专项工程&nbsp;<asp:Label ID="Label_ProjectNameContract" Font-Underline="true" Font-Bold="true"
                                    runat="server" ForeColor="Blue"></asp:Label>
                                &nbsp;合同支付项清单
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridview_ContractPayItemList" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-BackColor="#efefef" OnRowDataBound="gridview_ItemContractList_RowDataBound"
                                    DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" RowStyle-Height="20px"
                                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                <input type="hidden" id="Hidden_ItemID" value='<%# Eval("ItemID") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工作项">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_PlanItemName" runat="server" Text='<%#  Eval("PlanItemName") %>'></asp:Label>
                                                <input type="hidden" id="Hidden_PlanItemID" value='<%# Eval("PlanItemID") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="延迟时间(天)">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_DaysAfter" runat="server" Text='<%# Eval("DaysAfter") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额(元)<br/>计划支付/已经支付">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_Paid" runat="server" Text='<%# Eval("Paid", "{0:#,0.##}") %>'></asp:Label>
                                                </FooterTemplate>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label_TotalAmountContract" runat="server"></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_TotalPaidContract" runat="server" Text='<%# Eval("Paid", "{0:#,0.##}") %>'></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付方式">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付者">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Payee" runat="server" Text='<%# Eval("Payee") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                                    Text="修改" OnClientClick="javascript:setModalPopupContract(this.id);" />
                                                <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                                    PopupControlID="Panel_EditItemContract" BackgroundCssClass="modalBackground"
                                                    OkControlID="Button_OKContract" CancelControlID="Button_Cancel_EditContract"
                                                    DynamicServicePath="" Enabled="true">
                                                </cc2:ModalPopupExtender>
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <center>
                                            未添合同支付项</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <input id="Button_SaveContract" type="button" runat="server" value="保存" style="display: none"
                                    onserverclick="Button_SaveContract_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="月度支付" ID="TabPanel4">
            <ContentTemplate>
                <div id="div1">
                    <table id="Table1" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle">
                                专项工程&nbsp;<asp:Label ID="Label_ProjectNameMonthly" Font-Underline="true" Font-Bold="true"
                                    runat="server" ForeColor="Blue"></asp:Label>
                                &nbsp;月度支付清单
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridview_MonthlyPayList" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-BackColor="#efefef" OnRowDataBound="gridview_ItemMonthlyList_RowDataBound"
                                    HeaderStyle-Height="25px" ShowFooter="true" RowStyle-Height="20px"
                                    Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Index" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="年月款项">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Year" runat="server" Text='<%# Eval("Year") %>'></asp:Label>年<asp:Label ID="Label_Month" runat="server" Text='<%# Eval("Month") %>'></asp:Label>月
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付时间">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_PayTime" runat="server" Text='<%#  Eval("PayTime","{0:yyyy-MM-dd}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工程进度">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Progress" runat="server" Text='<%# Eval("Progress","{0:P}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额(元)<br/>计划支付/已经支付">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_Paid" runat="server" Text='<%# Eval("Paid", "{0:#,0.##}") %>'></asp:Label>
                                                </FooterTemplate>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label_TotalAmountMonthly" runat="server"></asp:Label>&nbsp;/&nbsp;
                                                <asp:Label ID="Label_TotalPaidMonthly" runat="server" Text='<%# Eval("Paid", "{0:#,0.##}") %>'></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付方式">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="支付者">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Payee" runat="server" Text='<%# Eval("Payee") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                    <RowStyle Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <center>
                                            未添加月度支付项</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <input id="Button_SaveMonthly" type="button" runat="server" value="保存" style="display: none"
                                    onserverclick="Button_SaveMonthly_Click" />
                                    <input id="Button_AddMonthly" type="button" runat="server" class="button_bak" value="添加月度支付"
                        onclick="javascript:setModalPopupMonthly(this.id);" />
                    <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItemMonthly" runat="server" TargetControlID="Button_AddMonthly"
                        PopupControlID="Panel_EditItemMonthly" BackgroundCssClass="modalBackground" OkControlID="Button_OKMonthly"
                        CancelControlID="Button_Cancel_EditMonthly" DynamicServicePath="" Enabled="true">
                    </cc2:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="支付简表" ID="TabPanel3">
            <ContentTemplate>
                <div id="div_Total">
                    <table id="Table_Total" style="width: 100%; border-collapse: collapse; vertical-align: middle;
                        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                        <tr>
                            <td class="Table_searchtitle" colspan="5">
                                专项工程支付简表
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                单位：元
                            </td>
                            <td class="Table_searchtitle">
                                预支付
                            </td>
                            <td class="Table_searchtitle">
                                合同支付
                            </td>
                            <td class="Table_searchtitle">
                                月进度支付
                            </td>
                            <td class="Table_searchtitle">
                                合计
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                计划支付
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanPre" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanContract" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanMonthly" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PlanTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                实际已支付
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidPre" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidContract" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidMonthly" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_PaidTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Table_searchtitle">
                                剩余未支付（计划-实际）
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffPre" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffContract" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffMonthly" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label_DiffTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
