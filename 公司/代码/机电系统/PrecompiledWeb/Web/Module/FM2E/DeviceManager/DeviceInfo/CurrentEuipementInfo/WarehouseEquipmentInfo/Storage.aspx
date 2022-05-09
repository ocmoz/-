<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_WarehouseEquipmentInfo_Storage, App_Web_komcswzl" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
<iframe style="Z-INDEX:-1;WIDTH:99%;POSITION:absolute;TOP:0px;HEIGHT:435px" frameborder="0"></iframe>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        //清空查询参数
        function clearQueryInput() {
            document.getElementById('<%= TextBox_ProductName.ClientID %>').value = "";
            document.getElementById('<%= TextBox_Model.ClientID %>').value = "";
        }
    </script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
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
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            产品名称：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_ProductName" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                            规格型号：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right">
                           仓库：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:DropDownList ID="DDLWarehouse" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                    runat="server">
                    <tr id="Tr1" runat="server">
                        <td id="Td1" align="center" style="height: 30px" runat="server">
                            <asp:Button ID="Button_Query" runat="server" CssClass="button_bak" Text="确定" OnClick="Button_Query_Click" />&nbsp;&nbsp;
                            <input class="button_bak" type="button" value="重填" onclick="javascript:clearQueryInput();" />&nbsp;&nbsp;
                            <input class="button_bak" onclick="javascript:window.parent.hidePopWin();" type="button"
                                value="关闭" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="仓库统计--预警值" ID="TabPanel_ResultDevice">
            <ContentTemplate>

                            <div style="width: 100%; overflow:auto; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_ResultDevice" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView_StorageList_RowDataBound"
                                    Width="100%">
                                    <Columns>
                                          <asp:TemplateField HeaderText="仓库">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_WarehouseName" runat="server" Text='<%# Bind("WareHouseName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="产品名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductName" runat="server" Text='<%# Bind("EquipmentName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格型号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_ProductModel" runat="server" Text='<%# Bind("EquipmentModel") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="库存量">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_LowerPrice" runat="server" Text='<%# Bind("Storage","{0:#,0.##}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单位">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Unit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="预警值">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Warming" runat="server" Text='<%# Bind("Warming") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                       
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
                             <input id="SelectedValue" type="hidden" runat="server" />
                            <asp:Label runat="server" ID="Label_SelectName"></asp:Label>
                            <center runat="server" id = "hidden01">
                                <input id="Button_OK" type="button" value="确定" class="button_bak" onclick="javascript:onOk();" />
                                <input id="Button_Cancel" value="关闭" type="button" class="button_bak" onclick="javascript:onCancel();" />
                            </center>

            </ContentTemplate>
        </cc2:TabPanel>
        
    </cc2:TabContainer>
    <script type="text/javascript" language="javascript">
        function onClientClick(cbid, equipmentname, equipmentmodel, equipmentnum, equipmentunit) {
            //用隐藏控件记录下选中的行号
            var v = equipmentname + "," + equipmentmodel + "," + equipmentnum + "," + equipmentunit;
            var orginalv = document.getElementById('<%= SelectedValue.ClientID %>').value;
            var lb = document.getElementById('<%= Label_SelectName.ClientID %>');
            if (orginalv.length > 0) {
                orginalv += ";" + v;
                lb.innerText = equipmentname;
            }

            else {
                orginalv = v;
                lb.innerText = equipmentname;
            }

            document.getElementById('<%= SelectedValue.ClientID %>').value = v;  //显示名称
            var inputs = document.getElementById("<%=GridView_ResultDevice.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i] == cbid) {
                    inputs[i].checked = true;
                }
                else
                    inputs[i].checked = false;

            }
        }
        function onOk() {
            window.returnVal = document.getElementById('<%= SelectedValue.ClientID %>').value;
            window.parent.hidePopWin(true);
        }
        function onCancel() {
            window.parent.hidePopWin();
        }
                          

    </script>
</asp:Content>

