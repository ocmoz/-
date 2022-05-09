<%@ Page Title="选择材料" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="Select.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_Select" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        //选择材料
        function selectItem(radio_id) {
        
            //先把其他radio设置成为check=false;
            var inputs = document.getElementsByTagName("INPUT");
            for (i = 0; i < inputs.length; i++) {
                if (inputs[i].type == 'radio' && inputs[i].id != radio_id) {
                    inputs[i].checked = false;
                }
            }

            var regS = new RegExp(",", "gi"); //去掉逗号

            //产品名称
            var name = document.getElementById(radio_id.replace('Radio_Select', 'Label_ProductName')).innerText;
            document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value = name;
            //规格型号
            var model = document.getElementById(radio_id.replace('Radio_Select', 'Label_ProductModel')).innerText ;
            document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value = model;

            //单价
            var lower = document.getElementById(radio_id.replace('Radio_Select', 'Label_LowerPrice')).innerText.replace(regS, "");
            var upper = document.getElementById(radio_id.replace('Radio_Select', 'Label_UpperPrice')).innerText.replace(regS, "");
            var price = (parseFloat(lower) + parseFloat(upper)) / 2;//取平均价格
            document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value = price;
            
            //单位
            var unit = document.getElementById(radio_id.replace('Radio_Select', 'Label_Unit')).innerText.replace(regS, "");
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].value == unit) {
                    s.options[i].selected = true;
                }
                else
                    s.options[i].selected = false;
            }

            //数量
            var count = "1";
            document.getElementById('<%= TextBox_Count.ClientID %>').value = count;

            //小计
            var amount = price * count;
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = amount;
        }

        //数量/单价变化的时候，自动更新金额小计
        function onCountChange() {
            var regS = new RegExp(",", "gi"); //去掉逗号
            var control_count  = document.getElementById('<%= TextBox_Count.ClientID %>');
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
            try
            {
                count = parseFloat(count);
            }
            catch(e)
            { 
                alert("数量"+count+"不是数字，请输入数字");
                control_count.focus();
                return;
            }
            try {
                price = parseFloat(price);
            }
            catch (e) {
                alert("单价"+price + "不是数字，请输入数字");
                control_price.focus();
                return;
            }
            document.getElementById('<%= TextBox_Amount.ClientID %>').value = price * count;

        }

        //清空查询参数
        function clearQueryInput() {
            document.getElementById('<%= TextBox_ProductName.ClientID %>').value = "";
            document.getElementById('<%= TextBox_Model.ClientID %>').value = "";
        }

        //添加材料
        function addItem() {
            //返回的值包括名称、型号、单价、单位、数量、备注（金额不在这里传）

            //产品名称
            var name = trim(document.getElementById('<%= TextBox_SelectedProductName.ClientID %>').value);
            if (name.length == 0) {
                alert('请输入产品名称');
                return;
            }
            //规格型号
            var model = trim(document.getElementById('<%= TextBox_SelectedProductModel.ClientID %>').value);
            if (model.length == 0) {
                alert('请输入规格型号');
                return;
            }
            //单价
            var price = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
            if (!checkFloat(price,'单价')) {
                
                return;
            }
            //单位
            var s = document.getElementById('<%= DropDownList_Unit.ClientID %>');
            if (s.selectedIndex == 0) {
                alert('请选择单位');
                return;
            }
            //数量
            var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
            if (!checkFloat(count, '数量')) {
                return;
            }
            //备注
            var remark = trim(document.getElementById('<%= TextBox_Remark.ClientID %>').value);


            //系统
            var ss = document.getElementById('<%= DropDownList_System.ClientID %>');
            var sysid = ss.options[ss.selectedIndex].value;
            if (sysid == '') {
                if (!confirm('未选定系统，继续添加？'))
                    return;
            }

            var sysname = ss.options[ss.selectedIndex].innerText;

            window.returnVal = name + "|" + model + "|" + price + "|" + s.options[s.selectedIndex].value + "|" + count + "|" + remark + "|" + sysid + "|" + sysname;
            window.parent.hidePopWin(true);
            
        }

        //浮点数检查
        function checkFloat(value,text) {
            var floatVal = parseFloat(value);
            if (isNaN(floatVal) || floatVal != value) {
                alert(text + "\n其格式不正确:\n" + value + "不是一个浮点数。");
                return false;
            }
            return true;
        }
    </script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
        <cc2:TabPanel runat="server" HeaderText="查询" ID="TabPanel_Query">
            <HeaderTemplate>
                查询
            </HeaderTemplate>
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            组合查询（支持模糊查询）
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align:right">
                            产品名称：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_ProductName" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px ;text-align:right">
                            规格型号：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                    runat="server">
                    <tr runat="server">
                        <td align="center" style="height: 30px" runat="server">
                            <asp:Button ID="Button_Query" runat="server" CssClass="button_bak" Text="确定" 
                                onclick="Button_Query_Click" />&nbsp;&nbsp;
                            <input class="button_bak" type="button" value="重填" onclick="javascript:clearQueryInput();" />&nbsp;&nbsp;
                            <input class="button_bak" onclick="javascript:window.parent.hidePopWin();" 
                                type="button" value="关闭" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="查询结果" ID="TabPanel_Result">
            <ContentTemplate>
                <table style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; border: solid 1px #a7c5e2;" border="1">
                    <tr align="center">
                        <td colspan="6">
                          
                                <asp:GridView ID="GridView_Result" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <input type="radio" id="Radio_Select" runat="server" onclick="javascript:selectItem(this.id)"/>
                                            </ItemTemplate>
                                            <HeaderTemplate>选择</HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="产品名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格型号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductModel" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单位">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="价格(元)">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_LowerPrice" runat="server" Text='<%# Eval("LowerPrice","{0:#,0.##}") %>'></asp:Label>
                                                &nbsp; 至&nbsp;
                                                <asp:Label ID="Label_UpperPrice" runat="server" Text='<%# Eval("UpperPrice","{0:#,0.##}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        查询不到以下产品，请手工添加<br />
                                        产品名称为<font color="red"><%= TextBox_ProductName.Text.Trim() %></font><br />
                                        规格型号为<font color="red"><%= TextBox_Model.Text.Trim()%></font><br />
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" CloneFrom=""  OnPageChanged="AspNetPager1_PageChanged" 
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left">
                                </cc1:AspNetPager>
                          
                        </td>
                    </tr>
                    <tr>
                     <th  class="Table_searchtitle" style="width:10%">
                            产品名称
                        </th>
                       <td  style="width:25%">
                            <asp:TextBox ID="TextBox_SelectedProductName" runat="server" title="请输入产品名称~"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                        </td>
                        <th class="Table_searchtitle" style="width:10%">
                            规格型号
                        </th>
                         <td  style="width:25%">
                            <asp:TextBox ID="TextBox_SelectedProductModel" runat="server" title="请输入规格型号~"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                        </td>
                        <th class="Table_searchtitle" style="width:10%">系统</th>
                         <td  style="width:20%">
                            <asp:DropDownList ID="DropDownList_System" runat="server"></asp:DropDownList>
                        </td>
                     </tr>
                     
                     <tr>
                         <th  class="Table_searchtitle">
                            单价(元)
                        </th>
                         <td >
                            <asp:TextBox ID="TextBox_UnitPrice" runat="server"  Width="80px"  title="请输入单价~float"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                        </td>
                         <th  class="Table_searchtitle">
                            单位
                        </th>
                         <td >
                            <asp:DropDownList ID="DropDownList_Unit" runat="server"></asp:DropDownList><span style="color:Red; font-weight:bold">*</span>
                        </td>
                         <th class="Table_searchtitle">
                           数量
                        </th>
                      <td>
                            <asp:TextBox ID="TextBox_Count" runat="server"  Width="50px"  title="请输入数量~float"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                        </td>
                      </tr>
                      <tr>
                         <th class="Table_searchtitle">
                           小计(元)
                        </th>
                          <td >
                            <asp:TextBox ID="TextBox_Amount" runat="server"  title="自动计算金额~float"></asp:TextBox>
                        </td>
                         <th class="Table_searchtitle">
                          备注
                        </th>
                         <td  colspan="3">
                            <asp:TextBox ID="TextBox_Remark" runat="server"  title="请输入备注~50:" Width="100%"></asp:TextBox>
                        </td>
                      </tr>
                  
                    <tr align="center">
                        <td colspan="6">
                            <input class="button_bak" type="button" value="添加" 
                            onclick="javascript:addItem();" />
                            <input class="button_bak" type="button" value="关闭" onclick="javascript:window.parent.hidePopWin();" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
    
</asp:Content>
