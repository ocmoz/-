<%@ page title="选择用户" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaserManager_Select, App_Web_j56zncbu" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        //选择用户
        function selectItem(radio_id) {
        
            //先把其他radio设置成为check=false;
            var inputs = document.getElementsByTagName("INPUT");
            for (i = 0; i < inputs.length; i++) {
                if (inputs[i].type == 'radio' && inputs[i].id != radio_id) {
                    inputs[i].checked = false;
                }
            }

            var regS = new RegExp(",", "gi"); //去掉逗号

            //用户名
            var id = document.getElementById(radio_id.replace('Radio_Select', 'Label_UserID')).innerText;
            document.getElementById('Hidden_UserID').value = id;
            //名称
            var name = document.getElementById(radio_id.replace('Radio_Select', 'Label_UserName')).innerText ;
            document.getElementById('Hidden_UserName').value = name;

            
        }


        //清空查询参数
        function clearQueryInput() {
            document.getElementById('<%= TextBox_UserID.ClientID %>').value = "";
            document.getElementById('<%= TextBox_UserName.ClientID %>').value = "";
        }

        //添加材料
        function addItem() {
            //返回的值包括用户名、姓名、描述

            var id = document.getElementById('Hidden_UserID').value;
            if (id == null || id == '') {
                alert('请选择用户');
                return false;
            }
            var name = document.getElementById('Hidden_UserName').value;

            //备注
            var remark = trim(document.getElementById('<%= TextBox_Remark.ClientID %>').value);
            if (remark == '') {
                alert('请输入描述');
                return false;
            }
            window.returnVal = id + "|" + name + "|" + remark ;
            window.parent.hidePopWin(true);
            
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
                            用户名：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_UserID" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px ;text-align:right">
                            姓名：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_UserName" runat="server"></asp:TextBox>
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
                <table width="99%">
                    <tr align="center">
                        <td colspan="2">
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_Result" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <input type="radio" id="Radio_Select" runat="server" onclick="javascript:selectItem(this.id)"/>
                                            </ItemTemplate>
                                            <HeaderTemplate>选择</HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="用户名">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_UserID" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_UserName" runat="server" Text='<%# Eval("PersonName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        查询不到以下用户，请手工添加<br />
                                        用户名包含<font color="red"><%= TextBox_UserID.Text.Trim() %></font><br />
                                        姓名包含<font color="red"><%= TextBox_UserName.Text.Trim()%></font><br />
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
                     <td class="table_body table_body_NoWidth" style="text-align:right; width:50px">
                            描述：
                        </td>
                        
                         <td class="table_none table_none_NoWidth" style="width:400px">
                            <asp:TextBox ID="TextBox_Remark" runat="server"  title="请输入描述~50!:" Width="400px"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                        <input type="hidden" id="Hidden_UserID" />
                           <input type="hidden" id="Hidden_UserName" /></td>
                    </tr>
                    <tr align="center">
                        <td colspan="2">
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
