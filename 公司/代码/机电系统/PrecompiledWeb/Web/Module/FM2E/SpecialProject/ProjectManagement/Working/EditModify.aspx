<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_EditModify, App_Web_fzwntbc0" %>

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
            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Index')).innerText;
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;

            var isadd = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_IsAdd')).value;
            if (isadd == 'True') {
                document.getElementById('<%= RadioButton_Add.ClientID %>').checked = true;
                document.getElementById('<%= RadioButton_Cut.ClientID %>').checked = false;
            }
            else {
                document.getElementById('<%= RadioButton_Add.ClientID %>').checked = false;
                document.getElementById('<%= RadioButton_Cut.ClientID %>').checked = true;
            }
            

            //产品名称
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_DeviceName')).innerText;
            document.getElementById('<%= TextBox_Equipment.ClientID %>').value = name;

            //规格型号
            var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Model')).innerText;
            document.getElementById('<%= TextBox_Model.ClientID %>').value = model;

            var s = s = document.getElementById('<%= DropDownList_JobItems.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].innerText == name + ' ' + model) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }

            //单价
            var price = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_UnitPrice')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = price;

            //单位
            var unit = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Unit')).innerText;
            document.getElementById('<%= TextBox_Unit.ClientID %>').value = unit;

            //数量
            var count = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Count')).innerText.replace(regS, "");
            document.getElementById('<%= TextBox_Count.ClientID %>').value = count;

            //小计
            var amount = parseFloat(price) * parseFloat(count);
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = amount;

            //备注
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;

            document.getElementById('Button_Save').value = "保存";
        }
        else {

            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = "-1";
            
            document.getElementById('<%= RadioButton_Add.ClientID %>').checked = true;
            document.getElementById('<%= RadioButton_Cut.ClientID %>').checked = false;

            document.getElementById('<%= TextBox_Equipment.ClientID %>').value = "";

            var s = document.getElementById('<%= DropDownList_JobItems.ClientID %>');

            s.options[0].selected = true;
            
            document.getElementById('<%= TextBox_Model.ClientID %>').value = "";


            document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Unit.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Count.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";


            document.getElementById('<%= TextBox_Remark.ClientID %>').value = "";

            document.getElementById('Button_Save').value = "添加";
        }
    }

    //保存编辑的项
    function saveEditItem() {

        //产品名称
        var name = trim(document.getElementById('<%= TextBox_Equipment.ClientID %>').value);
        if (name.length == 0) {
            alert('请输入设备');
            return false;
        }
        //规格型号
        var model = trim(document.getElementById('<%= TextBox_Model.ClientID %>').value);
        if (model.length == 0) {
            alert('请输入规格型号');
            return;
        }
        //单价
        var price = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
        if (!checkFloat(price, '单价')) {
            return;
        }
        //单位
        var unit = trim(document.getElementById('<%= TextBox_Unit.ClientID %>').value);
        if (unit.length == 0) {
            alert('请输入单位');
            return;
        }
        //数量
        var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
        if (!checkFloat(count, '数量')) {
            return;
        }
        //备注
        var remark = trim(document.getElementById('<%= TextBox_Remark.ClientID %>').value);

        document.getElementById('Button_OK').click();
        document.getElementById('<%= Button_SaveItem.ClientID %>').click();
    }

    //浮点数检查
    function checkFloat(value, text) {
        var floatVal = parseFloat(value);
        if (isNaN(floatVal) || floatVal != value) {
            alert(text + "\n其格式不正确:\n" + value + "不是一个整数。");
            return false;
        }
        return true;
    }

    //数量/单价变化的时候，自动更新金额小计
    function onCountChange() {
        
        var regS = new RegExp(",", "gi"); //去掉逗号
        var control_count = document.getElementById('<%= TextBox_Count.ClientID %>');
        var control_price = document.getElementById('<%= TextBox_UnitPrice.ClientID %>');
        var count = control_count.value.replace(/(^\s*)|(\s*$)/g, "").replace(regS, "");
        var price = control_price.value.replace(/(^\s*)|(\s*$)/g, "").replace(regS, "");
        if (count.length == 0) {
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
            return;
        }
        if (price.length == 0) {
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = "";
            return;
        }
        try {
            count = parseFloat(count);
        }
        catch (e) {
            alert("数量" + count + "不是数字，请输入数字");
            control_count.focus();
            return;
        }
        try {
            price = parseFloat(price);
        }
        catch (e) {
            alert("单价" + price + "不是数字，请输入数字");
            control_price.focus();
            return;
        }
        document.getElementById('<%= TextBox_Amount.ClientID %>').value = price * count;

    }
    </script>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：工程变更情况编辑" HeadHelpTxt="输入工程变更的具体情况，以便进行审批以及工程项目的改变">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回列表" ButtonUrlType="Href"
            ButtonUrl="ModifyList.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
    
     <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; height: 250px; display:none"
        CssClass="modalPopup">
        
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    变更设备明细<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    增加或减少：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:RadioButton ID="RadioButton_Add" GroupName="Add" Text="增加" runat="server" Checked="true" />
                    <asp:RadioButton ID="RadioButton_Cut" GroupName="Add" Text="减少" runat="server" />
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    设备项：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:DropDownList ID="DropDownList_JobItems" runat="server"  AutoPostBack="true"
                         onselectedindexchanged="DropDownList_JobItems_SelectedIndexChanged">
                    
                    </asp:DropDownList>
                </td>
                </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    设备：
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_Equipment" runat="server" ></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    规格型号：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                </tr>
        <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    单价：</td> <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_UnitPrice" runat="server"></asp:TextBox>元<span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    单位：</td><td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Unit" runat="server" ></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
           </tr>
           <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    数量：</td> <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Count" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                   ">
                    金额：</td>
                   <td class="table_none table_none_NoWidth" style="height: 30px;">
                    <asp:TextBox ID="TextBox_Amount" runat="server"></asp:TextBox>元
                </td>
           </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    备注：</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="请输入备注~50:" Width="400px"></asp:TextBox>
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <center>
            <input id="Button_Save" class="button_bak"
                type="button" value="保存" onclick="javascript:saveEditItem();"/>
            <input id="Button_OK" class="button_bak" style="display:none" value="OK" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" 
                Text="取消" />
        </center>
    </asp:Panel>
    <div id="div_table">
        <center>
        <span style="font-size:large">
            专项工程&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true"
                runat="server" ForeColor="Blue"></asp:Label>
            &nbsp;变更设计申请报告单 [  <asp:Label ID="Label_Status" runat="server"></asp:Label>]</span>
        </center>
        <br />
        项&nbsp;&nbsp;目： <br />
        承包人：（单位签章）
        <table width="100%" cellpadding="0" cellspacing="0" border="1"
            style="border-collapse: collapse;">
            <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                <th style="width: 10%; text-align: center">
                    变更工程名称
                </th>
                <td colspan="2" style="width: 40%;">
                    <asp:Label ID="Label_ProjectName2" runat="server"></asp:Label>
                </td>
                <th style="width: 10%; text-align: center">
                    申请日期
                </th>
                <td colspan="2" style="width: 40%; ">
                    <asp:Label ID="Label_ApplyTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 30px">
                <th style="width: 3%; text-align: center">
                    变更后金额<br />
                    （元）
                </th>
                <td>
                    <asp:TextBox ID="TextBox_BudgetChange"
                        runat="server">
                                    
                    </asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <th style="width: 10%; text-align: center">
                    增减金额<br />
                    （元）
                </th>
                <td>
                    <asp:TextBox ID="TextBox_BudgetIncDesc"
                        runat="server">
                                    
                    </asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
                <th style="width: 10%; text-align: center">
                    估计延长工期<br />
                    （天）
                </th>
                <td>
                    <asp:TextBox ID="TextBox_DelayDays"  runat="server">
                                    
                    </asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                </td>
            </tr>
            <tr style="height: 30px">
                <td colspan="6">
                    变更设计原因变更设计原因及内容：<br />
                    <asp:TextBox ID="TextBox_ChangeContent"  TextMode="MultiLine" Width="95%" 
                        runat="server" Height="100px"></asp:TextBox><span style="color:Red; font-weight:bold">*</span><br />
                    附件：
                    <asp:FileUpload ID="FileUpload_File" runat="server" />
                    <asp:HyperLink ID="HyperLink_File" runat="server" ForeColor="Blue" Font-Underline="true" Visible="false"></asp:HyperLink>
                </td>
            </tr>
            <tr style="height: 30px">
                <th>
                    备注：</th>
                    <td colspan="5">
                    <asp:TextBox ID="TextBox_Remark2" Width="95%" runat="server" 
                           ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="word-break: break-all">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
                        style="border-collapse: collapse; background-color: white">
                        <tr style="height: 30px">
                            <th style="width: 3%;">
                                序号</th>
                                                        <th style="width: 7%;">
                                增加或减少设备
                            </th>
                            <th style="width: 10%;">
                                设备型号
                            </th>
                            <th style="width: 10%;">
                                数量（单位）
                            </th>
                            <th style="width: 10%;">
                                单价（元）
                            </th>
                            <th style="width: 10%;">
                                增加减少金额（元）
                            </th>
                            <th style="width: 15%;">
                                备注
                            </th>
                            <th style="width: 5%;">
                                修改
                            </th>
                            <th  style="width: 5%;">
                                删除 
                            </th>
                        </tr>
                        <asp:Repeater ID="Repeater_Detail" runat="server" 
                            onitemcommand="Repeater_Detail_ItemCommand" >
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align: center">
                                        <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                    
                                        <input type="hidden" runat="server" value='<%# Eval("IsAdd") %>' id="Hidden_IsAdd" />
                                        <asp:Label ID="Label_IsAdded"  Font-Size="Larger" Text='<%# Eval("IsAddString") %>' runat="server"  ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_DeviceName" Text='<%# Eval("DeviceName") %>' runat="server">
                                    
                                        </asp:Label>
                                        &nbsp;
                                        <asp:Label ID="Label_Model" Text='<%# Eval("Model") %>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_Count" Text='<%# Eval("Count","{0:#,0.#####}") %>' runat="server">
                                    
                                        </asp:Label>
                                        &nbsp;
                                        （<asp:Label ID="Label_Unit" Text='<%# Eval("Unit") %>' runat="server">
                                    
                                        </asp:Label>）
                                    </td>
                                     <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_UnitPrice" Text='<%# Eval("UnitPrice","{0:#,0.#####}") %>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_IsAdded2" Text='<%# Eval("IsAddString") %>' runat="server"   ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                        </asp:Label><asp:Label ID="Label_Amount" Text='<%# Eval("Amount","{0:#,0.#####}") %>'  ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_Remark" Text='<%# Eval("Remark") %>' runat="server">
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False"
                                                ImageUrl="~/images/ICON/edit.gif" Text="修改" OnClientClick="javascript:setModalPopup(this.id,true);" />
                                                
                                             <cc2:ModalPopupExtender ID="ModalPopupExtender_EditItem" runat="server" TargetControlID="ImageButton_Edit"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" 
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                                             </cc2:ModalPopupExtender>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                       <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/images/ICON/delete.gif" Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <tr style="height: 30px">
                            <th  colspan="5">
                                合计
</th>
                            <th align="center">
                               <asp:Label ID="Label_TotalAmount" runat="server">
                                    
                                        </asp:Label>
                            </th>
                            <th colspan="3">
                                
                            </th>

                        </tr>
                        
                        <tr style="height: 30px">
                            <td colspan="9" align="right">
                               <input id="Button_SaveItem" type="button" runat="server" value="保存" style="display: none"
                                onserverclick="Button_Save_Click" />
                                
                     <input id="Button_Add" type="button" runat="server" class="button_bak" value="添加修改项" onclick="javascript:setModalPopup(this.id,false);" />
                      <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_Add"
                                              
                                                                    PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_OK"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                      </cc2:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
 <tr style="height: 30px">
                <th>
                    业主代表意见：
                </th>
                <td colspan="5">
                    
                    
                    <div id="div_ownerapprovalinfo" runat="server">
                    审批结果：<asp:Label ID="Label_OwnerApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_OwnerFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">部门负责人：
                    <asp:Label ID="Label_Owner" runat="server"></asp:Label>
                    <br />
                    日期：<asp:Label ID="Label_OwnerTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <th >
                    合约部审核：
                </th>
                <td colspan="5">
                    
                   
                    <div id="div_contractapprovalinfo" runat="server">
                    审批结果：<asp:Label ID="Label_ContractApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_ContractFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">合约部负责人：
                    <asp:Label ID="Label_Contract" runat="server"></asp:Label>
                    <br />
                    日期：<asp:Label ID="Label_ContractTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <th>
                    领导审批：
                </th>
                <td colspan="5">
                    
                   
                    <div id="div_leaderapprovalinfo" runat="server">
                    审批结果：<asp:Label ID="Label_LeaderApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_LeaderFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">领导：
                    <asp:Label ID="Label_Leader" runat="server"></asp:Label>
                    <br />
                    日期：<asp:Label ID="Label_LeaderTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
           
        </table>
       <center>
                    <asp:Button ID="Button_SaveModify" runat="server" Text="提交" 
                        CssClass="button_bak" onclick="Button_SaveModify_Click" />
              </center>
    </div>
</asp:Content>
