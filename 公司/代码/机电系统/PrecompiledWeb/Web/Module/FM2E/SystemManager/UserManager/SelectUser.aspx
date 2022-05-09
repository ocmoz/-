<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_SystemManager_UserManager_SelectUser, App_Web_1z8ztek-" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%; ">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="用户查询" ID="TabPanel1">
                        <HeaderTemplate>
                            用户查询
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
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        用户名：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                        <asp:TextBox ID="TextBox_UserName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        姓名：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        性别：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="DropDownList_Sex" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        公司：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="DropDownList_Company" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        部门：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:DropDownList ID="DropDownList_Department" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="Company"
                                        Enabled="True" LoadingText="公司加载中..." PromptText="请选择公司..." ServiceMethod="GetCompany"
                                        ServicePath="CompanyDeptService.asmx" TargetControlID="DropDownList_Company">
                                    </cc2:CascadingDropDown>
                                    <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="Department"
                                        Enabled="True" LoadingText="部门加载中..." ParentControlID="DropDownList_Company" PromptText="请选择部门..."
                                        ServiceMethod="GetDepartment" ServicePath="CompanyDeptService.asmx" TargetControlID="DropDownList_Department">
                                    </cc2:CascadingDropDown>
                                </tr>
                            </table>
                            <center>
                                <asp:Button ID="Button_Query" runat="server" CssClass="button_bak" Text="查询" OnClick="ButtonQuery_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />&nbsp;&nbsp;
                                <input id="Button_Close" value="关闭" type="button" class="button_bak" onclick="javascript:onCancel();" />
                            </center>
                        </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="用户列表" ID="TabPanel2">
                        <ContentTemplate>
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_UserList" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView_UserList_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkBox1" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserName" HeaderText="用户名">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PersonName" HeaderText="姓名">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                                <HeaderTemplate>
                                                    性别</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#EnumHelper.GetDescription((Enum)Eval("Sex"))%>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            
                                        <asp:BoundField DataField="CompanyName" HeaderText="公司">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DepartmentName" HeaderText="部门">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        没有找到对应的用户信息
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left">
                                </cc1:AspNetPager>
                            </div>
                            <input id="SelectedValue" type="hidden" runat="server" />
                            当前选中：<asp:Label runat="server" ID="Label_SelectName"></asp:Label>
                            <center>
                                <input id="Button_OK" type="button" value="确定" class="button_bak" onclick="javascript:onOk();" />
                                <input id="Button_Cancel" value="关闭" type="button" class="button_bak" onclick="javascript:onCancel();" />
                            </center>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function onClientClick(cbid, username, personname) {
            //用隐藏控件记录下选中的行号
            var v = username + "," + personname;
            var orginalv = document.getElementById('<%= SelectedValue.ClientID %>').value;
            var lb = document.getElementById('<%= Label_SelectName.ClientID %>');
            if (orginalv.length > 0) {
                orginalv += ";" + v;
                lb.innerText =  personname;
            }

            else {
                orginalv = v;
                lb.innerText = personname;
            }

            document.getElementById('<%= SelectedValue.ClientID %>').value = v;
            var inputs = document.getElementById("<%=GridView_UserList.ClientID%>").getElementsByTagName("input");
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
