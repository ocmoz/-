<%@ page title="查询指导价" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_QueryPrice, App_Web_4inv-6fo" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
<iframe style="Z-INDEX:-1;WIDTH:99%;POSITION:absolute;TOP:0px;HEIGHT:435px" frameborder="0"></iframe>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
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
        function adjustItem() {
            //返回的值包括项序号单价、数量

            var itemid = document.getElementById('<%= Hidden_ItemID.ClientID %>').value;
           
            //单价
            var price = trim(document.getElementById('<%= TextBox_UnitPrice.ClientID %>').value);
            if (!checkFloat(price,'单价')) {
                
                return;
            }
            
            //数量
            var count = trim(document.getElementById('<%= TextBox_Count.ClientID %>').value);
            if (!checkFloat(count, '数量')) {
                return;
            }

            window.returnVal = itemid + "|" + count + "|" + price;
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
        Width="99%">
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
                <table width="100%">
                    <tr align="center">
                        <td colspan="7">
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_Result" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="产品名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格型号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductModel" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="价格(元)">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_LowerPrice" runat="server" Text='<%# Bind("LowerPrice","{0:#,0.##}") %>'></asp:Label>
                                                &nbsp; 至&nbsp;
                                                <asp:Label ID="Label_UpperPrice" runat="server" Text='<%# Bind("UpperPrice","{0:#,0.##}") %>'></asp:Label>
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
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" CloneFrom="" OnPageChanged="AspNetPager1_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left">
                                </cc1:AspNetPager>
                            </div>
                        </td>
                    </tr>
                    <tr>
                     <td class="table_body table_body_NoWidth" style="height: 30px; text-align:center; width:50px">
                            产品名称
                        </td>
                       
                        <td class="table_body table_body_NoWidth" style="height: 30px ;text-align:center; width:50px">
                            规格型号
                        </td>
                        
                         <td class="table_body table_body_NoWidth" style="height: 30px ;text-align:center; width:80px">
                            单价(元)
                        </td>
                        
                         <td class="table_body table_body_NoWidth" style="height: 30px ;text-align:center; width:50px">
                            单位
                        </td>
                       
                         <td class="table_body table_body_NoWidth" style="height: 30px ;text-align:center; width:50px">
                           数量
                        </td>
                         <td class="table_body table_body_NoWidth" style="height: 30px ;text-align:center; width:80px">
                           小计(元)
                        </td>
                      </tr>
                      <tr>
                        <td class="table_none table_none_NoWidth" style="height: 30px; width:50px">
                      <input id="Hidden_ItemID" type="hidden" runat="server" />
                            <asp:TextBox ID="TextBox_SelectedProductName" runat="server" title="请输入产品名称~!"></asp:TextBox>
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px; width:50px">
                            <asp:TextBox ID="TextBox_SelectedProductModel" runat="server" title="请输入规格型号~!"></asp:TextBox>
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px; width:50px">
                            <asp:TextBox ID="TextBox_UnitPrice" runat="server"  Width="80px"  title="请输入单价~float!"></asp:TextBox>
                        </td>
                         <td class="table_none table_none_NoWidth" style="height: 30px; width:50px">
                            <asp:TextBox ID="TextBox_Unit" runat="server" Width="50px"  title="请输入单位~!"></asp:TextBox>
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px; width:50px">
                            <asp:TextBox ID="TextBox_Count" runat="server"  Width="50px"  title="请输入数量~float!"></asp:TextBox>
                        </td>
                         <td class="table_none table_none_NoWidth" style="height: 30px; width:50px">
                            <asp:TextBox ID="TextBox_Amount" runat="server"  title="自动计算金额~float!"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="7">
                            <input class="button_bak" type="button" value="调整" runat="server" id="Button_Adjust"
                            onclick="javascript:adjustItem();" />
                            <input class="button_bak" type="button" value="关闭" onclick="javascript:window.parent.hidePopWin();" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>

</asp:Content>
