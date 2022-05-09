<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="RoutineMaintainRecord.aspx.cs" Inherits="Module_FM2E_MaintainManager_RoutineMaintainManager_RoutineMaintainRecord_RoutineMaintainRecord"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Import Namespace="FM2E.Model.Equipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" defer="defer">
        //地址选定
        var changed='false';
        function addAddress(val)
        {
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];
            if(addcode!='00'){
            document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
            document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
            document.getElementById('<%= ButtonHidden.ClientID %>').click();
            }
        }
        function selectAll(obj)
        {
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;
        
            for(i=0;i<theTable.rows.length;i++)
            {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if(objCheckBox.checked!=null)objCheckBox.checked = obj.checked;
            }
        }
        function Clear(target)
        {
            switch(target)
            {
                case 'Address':
                {
                    document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '';
                    document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
                    break;
                }
                default:break;
            }
        }
    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="例行保养过程信息维护"
        HeadOPTxt="目前操作功能：实际例行保养过程信息录入">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="实际例行保养信息">
                    <ContentTemplate>
                        <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <table width="880px" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        系统：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="DDLSystem" runat="server" title="请选择系统~">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        子系统：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:DropDownList ID="DDLSubsystem" runat="server" title="请选择子系统~">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        保养项目：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                        <asp:DropDownList ID="DDLRecordObject" runat="server" title="请选择保养项目~" OnSelectedIndexChanged="DDLRecordObjectOnSelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                    <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DDLSystem"
                                        Category="System" PromptText="请选择系统..." LoadingText="系统加载中..." ServicePath="SystemSubsystemMaintainObjectService.asmx"
                                        ServiceMethod="GetSystem" Enabled="True">
                                    </cc2:CascadingDropDown>
                                    <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DDLSubsystem"
                                        Category="Subsystem" PromptText="请选择子系统..." LoadingText="子系统加载中..." ServicePath="SystemSubsystemMaintainObjectService.asmx"
                                        ServiceMethod="GetSubsystem" ParentControlID="DDLSystem" Enabled="True">
                                    </cc2:CascadingDropDown>
                                    <cc2:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="DDLRecordObject"
                                        Category="MaintainObject" PromptText="请选择保养项目..." LoadingText="保养项目加载中..." ServicePath="SystemSubsystemMaintainObjectService.asmx"
                                        ServiceMethod="GetRoutineMaintainObject" ParentControlID="DDLSubsystem" Enabled="True">
                                    </cc2:CascadingDropDown>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        地址信息：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3">
                                        <input id="TextBox_Address" type="text" style="width: 70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);" />
                                        <input type="hidden" id="Hidden_AddressID" runat="server" />
                                        <input class="cbutton" onclick="javascript:Clear('Address');" type="button" value="清除"
                                            id="Button_ClearAddress" />
                                        <asp:Button ID="ButtonHidden" runat="server" Text="刷选" OnClick="ButtonHidden_Click" class="cbutton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                       设备列表：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3">
                                        
                                        <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                            HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                            OnRowCommand="GridView1_RowCommand" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                            OnRowDataBound="GridView1_RowDataBound" OnDataBinding="GridView1_OnDataBinding">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="checkBox1" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <input id="CheckAll" runat="server" type="checkbox" onclick="selectAll(this);"  />本页全选
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="设备名称"></asp:BoundField>
                                                <asp:BoundField DataField="Model" HeaderText="型号 "></asp:BoundField>
                                                <asp:TemplateField HeaderText="保养结果" HeaderStyle-Width="20%">
                                                    <ItemTemplate>
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                                        <asp:ListItem Text="正常" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="故障"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                        <asp:TextBox ID="TBRecordRemark" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            
                                            <EmptyDataTemplate>
                                                没有设备信息
                                            </EmptyDataTemplate>
                                            <RowStyle HorizontalAlign="Center" Height="20px" />
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        </asp:GridView>
                                        <asp:Button ID="Button3" runat="server" class="cbutton" Text="全选" OnClick="SelectAllE" />
                                        <asp:Button ID="Button4" runat="server" class="cbutton" Text="取消全选" OnClick="SelectNoneE" />
                                        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                        </cc1:AspNetPager>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        保养日期：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="TBRecordDate" runat="server" title="请输入保养日期~" class="input_calender"
                                            onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        保养人：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:TextBox ID="TBRecordmanID" runat="server" title="请输入保养人~"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 78px">
                                        实际保养结果：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3" style="height: 78px">
                                        <textarea id="taRecordResult" style="width: 650px; height: 67px" runat="server" title="请输入实际保养结果~200:"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 78px">
                                        备注：
                                    </td>
                                    <td class="table_none table_none_NoWidth" colspan="3" style="height: 78px">
                                        <textarea id="taRemark" style="width: 650px; height: 67px" runat="server" title="请输入备注~200:"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td1" colspan="2" runat="server">
                                        <asp:Label ID="LblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td id="Td2" colspan="2" align="right" style="height: 38px" runat="server">
                                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="添加记录" OnClick="Button1_Click" />
                                        <asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="确定" OnClick="Button2_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="880px" style="text-align: center">
                            <tr align="center">
                                <td class="Table_searchtitle">
                                    已执行例行保养信息
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand"
                                        Width="100%" OnRowDataBound="GridView2_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <%# (this.AspNetPager2.CurrentPageIndex - 1) * this.AspNetPager2.PageSize + Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RecordObject" HeaderText="保养项目"></asp:BoundField>
                                            <asp:BoundField DataField="RecordDate" HeaderText="保养时间"></asp:BoundField>
                                            <asp:BoundField DataField="RecordmanName" HeaderText="保养人"></asp:BoundField>
                                            <asp:BoundField DataField="VerifiedResultString" HeaderText="审核结果"></asp:BoundField>
                                            <asp:TemplateField HeaderText="编辑">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                                        CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false"
                                                        Visible='<%#Convert.ToString(Eval("VerifiedResultString"))=="未审核"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="删除">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                        CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此信息吗？')"
                                                        CausesValidation="false" Visible='<%#Convert.ToString(Eval("VerifiedResultString"))=="未审核"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            没有计划明细信息
                                        </EmptyDataTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:GridView>
                                    <cc1:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager2_PageChanged"
                                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                        NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                        ShowCustomInfoSection="Left" CloneFrom="" Width="800px">
                                    </cc1:AspNetPager>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
    function f(obj)
       {
           obj.select();
       }
    </script>

</asp:Content>
