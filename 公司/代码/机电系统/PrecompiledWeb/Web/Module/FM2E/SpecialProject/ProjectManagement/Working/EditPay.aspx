<%@ page title="编辑专项工程支付计划" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_EditPay, App_Web_fzwntbc0" %>

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
        function setModalPopup(button_id, edit) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            //ITEMID
            if (edit) {
                var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_ItemID')).value;
                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

                //名称
                var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemName')).innerText;
                document.getElementById('<%= TextBox_ItemName.ClientID %>').value = name;

                //支付时间
                var time = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Time')).innerText;
                document.getElementById('<%= TextBox_Time.ClientID %>').value = time;

                //支付方式
                var m = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Method')).innerText;
                document.getElementById('<%= TextBox_Method.ClientID %>').value = m;


                //金额
                var a = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Amount')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = a;


                //备注
                var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
                document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;

                document.getElementById('Button_Save').value = "保存";
            }
            else {

                document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = 0;
                document.getElementById('<%= TextBox_ItemName.ClientID %>').value = "";
                document.getElementById('<%= TextBox_Method.ClientID %>').value = "";
                document.getElementById('<%= TextBox_Time.ClientID %>').value = "";
                document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
                document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";
                document.getElementById('Button_Save').value = "添加";
            }
        }


        //编辑的时候设置模式对话框的值
        function setModalPopupContract(button_id, edit) {
            var regS = new RegExp(",", "gi"); //去掉逗号

            //ITEMID
            if (edit) {
                var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_ItemID')).value;
                document.getElementById('<%= Hidden_ItemIDContract.ClientID %>').value = itemid;

                //名称
                var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_ItemName')).innerText;
                document.getElementById('<%= TextBox_ItemNameContract.ClientID %>').value = name;

                //支付时间
                var days = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DaysAfter')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_DaysAfter.ClientID %>').value = days;

                //支付方式
                var m = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Method')).innerText;
                document.getElementById('<%= TextBox_MethodContract.ClientID %>').value = m;


                //金额
                var a = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Amount')).innerText.replace(regS, "");
                document.getElementById('<%= TextBox_AmountContract.ClientID %>').value = a;


                //备注
                var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
                document.getElementById('<%= TextBox_RemarkContract.ClientID %>').value = remark;

                //工作项
                var s = document.getElementById('<%= DropDownList_JobItem.ClientID %>');
                var job = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_PlanItemID')).value;
                for (i = 0; i < s.options.length; i++) {
                    if (s.options[i].value == job)
                        s.options[i].selected = true;
                    else
                        s.options[i].selected = false;
                }
                
                document.getElementById('Button_SaveContract').value = "保存";
            }
            else {
                document.getElementById('<%= Hidden_ItemIDContract.ClientID %>').value = 0;
                document.getElementById('<%= TextBox_ItemNameContract.ClientID %>').value = "";
                document.getElementById('<%= TextBox_MethodContract.ClientID %>').value = "";
                document.getElementById('<%= TextBox_DaysAfter.ClientID %>').value = "";
                document.getElementById('<%= TextBox_AmountContract.ClientID %>').value = "";
                document.getElementById('<%= TextBox_RemarkContract.ClientID %>').value = "";
                document.getElementById('Button_SaveContract').value = "添加";
                //工作项
                var s = document.getElementById('<%= DropDownList_JobItem.ClientID %>');
                s.selectedIndex = 0;
            }
        }

        //保存编辑的项
        function saveEditItem() {

            //名称
            var name = trim(document.getElementById('<%= TextBox_ItemName.ClientID %>').value);
            if (name.length == 0) {
                alert('请支付项名称');
                return false;
            }

            //名称
            var method = trim(document.getElementById('<%= TextBox_Method.ClientID %>').value);
            if (method.length == 0) {
                alert('请支付方式');
                return false;
            }
            //支付日期
            var time = trim(document.getElementById('<%= TextBox_Time.ClientID %>').value);
            if (!checkDate(time, '支付日期')) {
                return;
            }
            //金额
            var count = trim(document.getElementById('<%= TextBox_Amount.ClientID %>').value);
            if (!checkFloat(count, '金额')) {
                return;
            }
            //备注
            var remark = trim(document.getElementById('<%= TextBox_Remark.ClientID %>').value);

            document.getElementById('Button_OK').click();
            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }

        //保存编辑的项
        function saveEditItemContract() {

            //名称
            var name = trim(document.getElementById('<%= TextBox_ItemNameContract.ClientID %>').value);
            if (name.length == 0) {
                alert('请支付项名称');
                return false;
            }

            //支付方法
            var method = trim(document.getElementById('<%= TextBox_MethodContract.ClientID %>').value);
            if (method.length == 0) {
                alert('请支付方式');
                return false;
            }
            //延迟天数
            var days = trim(document.getElementById('<%= TextBox_DaysAfter.ClientID %>').value);
            if (!checkInt(days, '延迟天数')) {
                return;
            }
            //金额
            var count = trim(document.getElementById('<%= TextBox_AmountContract.ClientID %>').value);
            if (!checkFloat(count, '金额')) {
                return;
            }
            //备注
            var remark = trim(document.getElementById('<%= TextBox_RemarkContract.ClientID %>').value);

            document.getElementById('Button_OKContract').click();
            document.getElementById('<%= Button_SaveContract.ClientID %>').click();
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

        //整数检查
        function checkInt(value, text) {
            var intVal = parseInt(value);
            if (isNaN(intVal) || intVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个整数。");
                return false;
            }
            return true;
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：计量支付计划" HeadHelpTxt="制定预支付以及合同支付工作计划；合同支付根据施工计划进行">
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
                    <asp:TextBox ID="TextBox_ItemName" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    支付时间：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Time" class="input_calender" onfocus="javascript:HS_setDate(this);"
                        runat="server"></asp:TextBox><span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Amount" runat="server"></asp:TextBox>元<span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    支付方式：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Method" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_Save" class="button_bak" type="button" value="保存" onclick="javascript:saveEditItem();" />
            <input id="Button_OK" class="button_bak" style="display: none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" Text="取消" />
        </center>
    </asp:Panel>
    
    <asp:Panel ID="Panel_EditItemContract" runat="server" Style="width: 95%; height: 200px; display: none"
        CssClass="modalPopup">
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
                    <asp:TextBox ID="TextBox_ItemNameContract" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
           </tr>
           <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    对应工作项：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:DropDownList ID="DropDownList_JobItem" runat="server"></asp:DropDownList>
                </td>
            
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    延迟时间：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_DaysAfter"
                        runat="server"></asp:TextBox>天<span style="color: Red; font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    金额：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_AmountContract" runat="server"></asp:TextBox>元<span style="color: Red;
                        font-weight: bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    支付方式：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_MethodContract" runat="server"></asp:TextBox><span style="color: Red;
                        font-weight: bold">*</span>
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;">
                    备注：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_RemarkContract" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_SaveContract" class="button_bak" type="button" value="保存" onclick="javascript:saveEditItemContract();" />
            <input id="Button_OKContract" class="button_bak" style="display: none" value="OK" />
            <asp:Button ID="Button_Cancel_EditContract" runat="server" class="button_bak" Text="取消" />
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
                        OnRowDataBound="gridview_ItemList_RowDataBound" OnRowDeleting="gridview_ItemList_RowDeleted" HeaderStyle-BackColor="#efefef"
                        DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" 
                        RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
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
                            <asp:TemplateField HeaderText="金额(元)">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>
                                    </FooterTemplate>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支付方式">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                        Text="修改" OnClientClick="javascript:setModalPopup(this.id,true);" />
                                    <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                        PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_OK"
                                        CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                                    </cc2:ModalPopupExtender>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                        CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
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
            
        </table>
        <center>
                    <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                        onserverclick="Button_Save_Click" />
                    <input id="Button_Add" type="button" runat="server" class="button_bak2" value="添加预支付项"
                        onclick="javascript:setModalPopup(this.id,false);" />
                    <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Add"
                        PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" OkControlID="Button_OK"
                        CancelControlID="Button_Cancel_Edit" DynamicServicePath="" Enabled="true">
                    </cc2:ModalPopupExtender>
               </center>
        
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
                         HeaderStyle-BackColor="#efefef" OnRowDataBound="gridview_ItemContractList_RowDataBound" OnRowDeleting="gridview_ItemContractList_RowDeleted" 
                        DataKeyNames="ItemID" HeaderStyle-Height="25px" ShowFooter="true" 
                        RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
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
                            
                            <asp:TemplateField HeaderText="金额(元)">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Amount" runat="server" Text='<%# Eval("Amount", "{0:#,0.##}") %>'></asp:Label>
                                    </FooterTemplate>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="Label_TotalAmountContract" runat="server"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支付方式">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Method" runat="server" Text='<%# Eval("Method") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False" ImageUrl="~/images/ICON/edit.gif"
                                        Text="修改" OnClientClick="javascript:setModalPopupContract(this.id,true);" />
                                    <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                        PopupControlID="Panel_EditItemContract" BackgroundCssClass="modalBackground" OkControlID="Button_OKContract"
                                        CancelControlID="Button_Cancel_EditContract" DynamicServicePath="" Enabled="true">
                                    </cc2:ModalPopupExtender>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                        CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
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
            </table>
            <center>
                    <input id="Button_SaveContract" type="button" runat="server" value="保存" style="display: none"
                        onserverclick="Button_SaveContract_Click" />
                    <input id="Button_AddContract" type="button" runat="server" class="button_bak2" value="添加合同支付项"
                        onclick="javascript:setModalPopupContract(this.id,false);" />
                    <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button_AddContract"
                        PopupControlID="Panel_EditItemContract" BackgroundCssClass="modalBackground" OkControlID="Button_OKContract"
                        CancelControlID="Button_Cancel_EditContract" DynamicServicePath="" Enabled="true">
                    </cc2:ModalPopupExtender>
             </center>
    </div>
    </ContentTemplate>
    </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
