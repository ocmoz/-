<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_BasicData_DepotManage_EditDepot, App_Web_yu00z5hw" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Basic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">


    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>



     <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <script language="javascript">

        //地址选定
        function addAddress(val) {
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];
            if (addcode != '00') {
                document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
                document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
            }
        }

        //清空
        function ClearAddress() {
           
                document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '';
                document.getElementById('<%= TextBox_Address.ClientID %>').value = '';

            }
//显示地址窗口
            function showAddressWin() {
                showPopWin('选择地址', '../AddressManage/Address.aspx?operator=select&addresstype='+'<%= (int)AddressType.Warehouse %>', 900, 400, addAddress, true, true);
            }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库信息维护"
        HeadOPTxt="目前操作功能：仓库信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="仓库列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="Depot.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>

    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                仓库详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" >
                                仓库编号：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox1" runat="server" title="请输入仓库编号~2:noChinese!" MaxLength="2"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                仓库名称：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox2" runat="server" title="请输入仓库名称~30:!" MaxLength="30"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                        <td class="table_body table_body_NoWidth" >
                                仓库所属公司：
                            </td>
                            <td class="table_none table_none_NoWidth"  colspan="3">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" >
                                仓库地点：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                               
                                <input ID="TextBox_Address" type="text" style="width:70%" title="请输入仓库地点~200:!" 
                                runat="server"
                                onfocus="javascript:showAddressWin();"/>
                                
                                <input type="hidden" id="Hidden_AddressID" runat="server" />
                                <input class="cbutton" onclick="javascript:ClearAddress();" type="button"
                                            value="清除" id="Button_ClearAddress" />
                                 <span style="color:Red">*</span>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="width: 15%;">
                                仓库负责人：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <div id="DIV1" runat="server" style="width: 210px;">
                                    <input id="principal" title="请输入仓库负责人~20:" runat="server" style="width: 120px" type="text" 
                                    onclick="javascript:showPopWin('选择仓库负责人','../../SystemManager/UserManager/SelectUser.aspx?number=1',900, 400, addResponsiblity,true,true);" />
                                    <input class="cbutton" onclick="javascript:Clear1();" type="button" value="清除" id="Button2" />
                                    <input id="principalID" runat="server" type="hidden"  />
                                </div>
                               
                            </td>
                            <td class="table_body table_body_NoWidth" style="width: 15%; ">
                                仓库联系人：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <div id="DIV3" runat="server" style="width: 210px; ">
                                    <input id="contactor" title="请输入仓库联系人~20:" runat="server" style="width: 120px" type="text" 
                                    onclick="javascript:showPopWin('选择仓库联系人','../../SystemManager/UserManager/SelectUser.aspx?number=1',900, 400, addContactor,true,true);" /> 
                                    <input class="cbutton" onclick="javascript:Clear2();" type="button" value="清除" id="Button3" />
                                    <input id="contactorID" runat="server" type="hidden"  />
                                </div>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" >
                                仓库电话：
                            </td>
                            <td class="table_none table_none_NoWidth"  colspan="3">
                                <asp:TextBox ID="TextBox6" runat="server" title="请输入电话~20:" MaxLength="20"></asp:TextBox><span style="color:Red"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 78px">
                                备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <textarea id="TextArea1" style="width: 99%; height: 67px" runat="server" title="请输入备注~100:"></textarea>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" align="right" style="height: 38px" runat="server">
                                <asp:Button ID="Button8" runat="server" CssClass="button_bak2" Text="设定仓管员" OnClick="Button8_Click" />&nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="编辑仓管员" ID="TabPanel2">
                <ContentTemplate>
                    <div runat="server" style="text-align: center">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    仓管员列表
                                </td>
                            </tr>
                            <tr>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                    OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                    <Columns>
                                     <asp:BoundField DataField="UserName" HeaderText="用户名">
                                            <HeaderStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StaffNo" HeaderText="工号">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PersonName" HeaderText="姓名">
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                       
                                        <asp:BoundField DataField="CompanyName" HeaderText="公司">
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DepartmentName" HeaderText="部门">
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PositionName" HeaderText="职位">
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="10%" />
                                            <HeaderTemplate>删除</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此仓管员吗？')"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        没有仓管员信息
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                                <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                    CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                    NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                    ShowCustomInfoSection="Left" CloneFrom="">
                                </cc1:AspNetPager>
                            </tr>
                            <tr>
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
                                    
                                    <div>
                                        <asp:DropDownList ID="DropDownList_Company" runat="server">
                                        </asp:DropDownList></div>
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
                                        ServicePath="../../SystemManager/UserManager/CompanyDeptService.asmx" TargetControlID="DropDownList_Company">
                                    </cc2:CascadingDropDown>
                                    <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="Department"
                                        Enabled="True" LoadingText="部门加载中..." ParentControlID="DropDownList_Company" PromptText="请选择部门..."
                                        ServiceMethod="GetDepartment" ServicePath="../../SystemManager/UserManager/CompanyDeptService.asmx" TargetControlID="DropDownList_Department">
                                    </cc2:CascadingDropDown>
                                </tr>
                            </table>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td align="right" style="height: 38px">
                                    <asp:Button ID="Button9" runat="server" CssClass="button_bak" Text="查询" OnClick="Button9_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_searchtitle">
                                    查询结果
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand"
                                        OnRowDataBound="GridView2_RowDataBound" Width="100%">
                                        <Columns>
                                        <asp:BoundField DataField="UserName" HeaderText="用户名">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StaffNo" HeaderText="工号">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PersonName" HeaderText="姓名">
                                                <HeaderStyle Width="15%" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="CompanyName" HeaderText="公司">
                                                <HeaderStyle Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DepartmentName" HeaderText="部门">
                                                <HeaderStyle Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PositionName" HeaderText="职位">
                                                <HeaderStyle Width="15%" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField>
                                                <ItemStyle Width="10%" />
                                                <HeaderTemplate>选择</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/right.gif"
                                                        CommandName="select" CommandArgument='<%# Eval("UserName") %>' CausesValidation="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                             无法找到符合条件的用户信息
                                        </EmptyDataTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:GridView>
                                    <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager2_PageChanged"
                                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                        NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                        ShowCustomInfoSection="Left" CloneFrom="">
                                    </cc1:AspNetPager>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>

    <script type="text/javascript" language="javascript">
    
    function addResponsiblity(addstring)
    {
        var arr = addstring.split(",");
        if(arr.length>=2){
         document.getElementById('<%= principal.ClientID %>').value = arr[1];
         document.getElementById('<%= principalID.ClientID %>').value = arr[0];
         }
        
    }
    
    function addContactor(addstring)
    {
        var arr = addstring.split(",");
        if(arr.length>=2){
         document.getElementById('<%= contactor.ClientID %>').value = arr[1];
         document.getElementById('<%= contactorID.ClientID %>').value = arr[0];
         }
        
    }
    function Clear1()
    {
        document.all.<%=this.principalID.ClientID %>.value='';
	    document.all.<%=this.principal.ClientID %>.value='';  
    }
    function Clear2()
    {
        document.all.<%=this.contactorID.ClientID %>.value='';
	    document.all.<%=this.contactor.ClientID %>.value='';  
    }

    </script>

</asp:Content>
