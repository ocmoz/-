<%@ Page Title="编辑维护计划" Language="C#" MasterPageFile="~/MasterPage/MasterPageNoCheck.master"
    AutoEventWireup="true" CodeFile="EditMaintainTemplate.aspx.cs" Inherits="Module_FM2E_MaintainManager_MaintainManager_MaintainTemplate_EditMaintainTemplate" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" defer="defer">
        //地址选定
        function addAddress(val) {
            var arr = new Array;
            arr = val.split('|');
            var addid = arr[0];
            var addcode = arr[1];
            var addname = arr[2];
            if (addcode != '00') {
                document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
                document.getElementById('<%= Hidden_AddressCode.ClientID %>').value = addcode;
                document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;

            }
        }

        function Clear(target) {
            switch (target) {
                case 'Address':
                    {
                        document.getElementById('<%= Hidden_AddressID.ClientID %>').value = '0';
                        document.getElementById('<%= TextBox_Address.ClientID %>').value = '';
                        document.getElementById('<%= Hidden_AddressCode.ClientID %>').value = '00';
                        break;
                    }
                default: break;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="维护计划编辑"
        HeadOPTxt="目前操作功能：维护计划编辑" HeadHelpTxt="">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="维护计划列表" ButtonUrlType="Href"
            ButtonUrl="MaintainTemplateList.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <%--  <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="维护计划" ID="TabPanel1">
                <ContentTemplate>--%>
        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td class="table_body table_body_NoWidth">
                        名称：
                    </td>
                    <td class="table_none table_none_NoWidth" colspan="3">
                        <asp:TextBox ID="TextBox_Name" runat="server" Text="" Width="50%"  title="请输入维护表单名称~B20:!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        系统：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:DropDownList ID="DDLSystem" runat="server" title="请选择系统~B!">
                        </asp:DropDownList>
                        <span style="color: Red">*</span>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        临时：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:CheckBox ID="CheckBox_IsTemp" runat="server" Checked="false" />
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        周期：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:TextBox ID="TextBox_Period" runat="server" Text="1" Width="30px"></asp:TextBox>
                        <asp:DropDownList ID="DropDownList_PeriodUnit" runat="server"  title="请选择周期~B!">
                        </asp:DropDownList>
                        <span style="color: Red">*</span>
                    </td>
                    <td class="table_body table_body_NoWidth" style="height: 30px">
                        维护类型：
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:DropDownList ID="DropDownList_Type" runat="server"  title="请选择维护类型~B!">
                        </asp:DropDownList>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        地址信息：
                    </td>
                    <td class="table_none table_none_NoWidth" colspan="3">
                        <input id="TextBox_Address" type="text" style="width: 70%"  title="请选择地址~B:!" runat="server" onfocus="javascript:showPopWin('选择地址','../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);" />
                        <input type="hidden" id="Hidden_AddressCode" runat="server" value="00" />
                        <input type="hidden" id="Hidden_AddressID" runat="server" value="0" />
                        <input class="button_bak" onclick="javascript:Clear('Address');" type="button" value="清除地址"
                            id="Button_ClearAddress" />
                        <asp:Button ID="Button_AddEquipment" CssClass="button_bak" Text="筛选设备" runat="server"
                            OnClick="Button_AddEquipment_Click" />
                        <asp:Button ID="Button_ShowAdd" CssClass="button_bak" Text="继续添加" runat="server"
                            Style="display: none" />
                        <cc2:ModalPopupExtender ID="ModalPopupExtender_AddItem" runat="server" TargetControlID="Button_ShowAdd"
                            PopupControlID="Panel_AddEquipment" BackgroundCssClass="modalBackground" OkControlID="Button_Update"
                            CancelControlID="Button_CancelAdd" DynamicServicePath="" Enabled="true">
                        </cc2:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        备注：
                    </td>
                    <td class="table_none table_none_NoWidth" colspan="3">
                        <textarea id="taRemark" style="width: 100%; height: 50px" runat="server" title="请输入备注~200:"></textarea>
                    </td>
                </tr>

            </table>
            
            <asp:UpdatePanel ID="UpdatePanel_Edit" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button_Update" />
                            </Triggers>
                            <ContentTemplate>
                             <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                <td colspan="<%= CountPerRow %>"  class="table_body_WithoutWidth">
                        维护对象
                    </td></tr>
                                    <asp:Repeater ID="Repeater_EquipmentList" runat="server" OnItemCommand="Repeater_EquipmentList_ItemCommand"
                                        OnItemDataBound="Repeater_EquipmentList_ItemDataBound">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex % CountPerRow ==0?"<tr>":""%>
                                            <td runat="server" id="td_item" style="text-align:left">
                                                <asp:ImageButton ID="ImageButton_Delete" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                    CommandName="DeleteCMD" ImageUrl="~/images/ICON/delete.gif" />
                                                    <%# Eval("EquipmentName") %>
                                                
                                                <br />
                                                [<%# Eval("AddressName") %><%# Eval("DetailLocation") %>]
                                            </td>
                                            <%# Container.ItemIndex % CountPerRow == (CountPerRow-1) ?"</tr>":""%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
        </div>
        <asp:Panel ID="Panel_AddEquipment" runat="server" Style="width: 95%; display: none"
            Height="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePlan_Add" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_AddEquipment" />
                </Triggers>
                <ContentTemplate>
                系统：<asp:Label ID="Label_FilterSystem" runat="server"></asp:Label>
                地址：<asp:Label ID="Label_FilterAddress" runat="server"></asp:Label>
                    <table width="100%">
                        <tr>
                            <td>
                                名称：<asp:TextBox ID="TextBox_FilterName" runat="server" OnTextChanged="OnFilter" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                型号：<asp:TextBox ID="TextBox_FilterModel" runat="server" OnTextChanged="OnFilter"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                公司：<asp:DropDownList ID="DropDownList_FilterCompany" runat="server" OnSelectedIndexChanged="OnFilter"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                状态：<asp:DropDownList ID="DropDownList_FilterStatus" runat="server" OnSelectedIndexChanged="OnFilter"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div style="width: 100%; height: 250px; overflow-y: auto">
                        <asp:GridView Width="98%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                            HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px" RowStyle-Height="20px"
                            HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        本页全选<br />
                                        <asp:CheckBox ID="CheckBox_SelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox_SelectAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox_Select" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox_Select_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" />
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        设备ID
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_EquipmentID" runat="server" Text='<%# Eval("EquipmentID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        设备条形码
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_EquipmentNO" runat="server" Text='<%# Eval("EquipmentNO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        设备名称
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        型号
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CompanyName" HeaderText="公司" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="SystemName" HeaderText="系统" ItemStyle-Width="7%" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        状态</HeaderTemplate>
                                    <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <%# EnumHelper.GetDescription((FM2E.Model.Equipment.EquipmentStatus)Eval("Status")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        地址ID
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_AddressID" runat="server" Text='<%# Eval("AddressID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        地址
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_AddressName" runat="server" Text='<%# Eval("AddressName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        安装位置
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_DetailLocation" runat="server" Text='<%# Eval("DetailLocation") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <center>
                                    没有找到符合条件的相关设备信息</center>
                            </EmptyDataTemplate>
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                        </asp:GridView>
                    </div>
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                        InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                        PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
                </ContentTemplate>
            </asp:UpdatePanel>
            <center>
                <input id="Button_Update" class="button_bak" type="button" value="添加完毕" onserverclick="Button_Update_Click"
                    runat="server" />
                <asp:Button ID="Button_CancelAdd" runat="server" class="button_bak" Text="取消添加" Style="display: none" />
            </center>
        </asp:Panel>
        <%--    </ContentTemplate>
            </cc2:TabPanel>
            
        </cc2:TabContainer>--%>
        <div>
          <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr><td colspan="4"  class="table_body_WithoutWidth">其他维护对象</td>
            </tr>
            <tr>
        <td class="table_body table_body_NoWidth">名称：<asp:TextBox ID="TextBox_OtherName" runat="server"  title="请输入设备名称~A20:!"></asp:TextBox></td>
            <td class="table_body table_body_NoWidth">型号：<asp:TextBox ID="TextBox_OtherModel" runat="server"  title="请输入设备型号~A20:"></asp:TextBox></td>
            <td class="table_body table_body_NoWidth">安装位置：<asp:TextBox ID="TextBox_OtherLocation" runat="server"  title="请输入安装位置~A50:!"></asp:TextBox></td>
            <td class="table_body table_body_NoWidth">
            
            <asp:Button  OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'A')" ID="Button_AddOther" runat="server" class="button_bak" Text="添加" OnClick="Button_AddOther_Click" /></td>
            </tr></table>
        </div>
        <center>
            <asp:Button ID="Button_Submit" Text="保存" runat="server" OnClick="Button_Submit_Click"
                CssClass="button_bak" OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'B')&&confirm('确认保存？');" />
            <asp:Button ID="Button_Cancel" OnClick="Button_Cancel_Click" runat="server" class="button_bak"
                Text="取消" OnClientClick="javascript:return confirm('确认取消？');" />
        </center>
    </div>
</asp:Content>
