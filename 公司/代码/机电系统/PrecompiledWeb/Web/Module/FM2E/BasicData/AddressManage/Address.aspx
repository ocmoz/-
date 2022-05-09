<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_AddressManage_Address, App_Web_expf-0id" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Import Namespace="FM2E.Model.Basic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="地址信息管理"
                HeadOPTxt="目前操作功能：地址信息管理" HeadHelpTxt="点击地址树查看地址信息">
                <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加下级地址" ButtonPopedom="New"
                    ButtonVisible="true" ButtonUrlType="JavaScript" ButtonUrl="" />
                <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="修改" ButtonPopedom="Edit"
                    ButtonVisible="true" ButtonUrlType="JavaScript" ButtonUrl="" />
                <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete"
                    ButtonVisible="true" ButtonUrlType="JavaScript" ButtonUrl="" />
            </cc1:HeadMenuWebControls>
            <div style='width: 100%; <%=(operatorType=="select")?" overflow-y:auto; overflow-x:hidden; height:340px;": ""%>'>
                <table style='width: 100%; border-collapse: collapse; border: solid 0px #000000;'>
                    <tr>
                        <td style="width: 200px;" align="left" valign="top">
                            <div style="width: 100%; overflow: auto; height: 100%; border: solid 1px #ffffff;">
                                <asp:TreeView ID="addressTree" runat="server" ShowLines="true" OnSelectedNodeChanged="addressTree_SelectedNodeChanged"
                                    OnTreeNodePopulate="addressTree_TreeNodePopulate">
                                    <SelectedNodeStyle ForeColor="#FF5050" />
                                </asp:TreeView>
                            </div>
                        </td>
                        <td style="vertical-align: top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" style="text-align: left" colspan="4">
                                        &nbsp;<asp:Label ID="lbTitle" runat="server" Text="地址详细信息"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        地址名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                        <asp:Label ID="lbAddressName" runat="server" Text="地址信息"></asp:Label>
                                        <asp:TextBox ID="tbAddressName" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        地址全称：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                        <asp:Label ID="Label_FullName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr style='display: <%=addressTree.SelectedValue=="1"&&Mode=="viewmode"?"none":"block"%>'>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        地址类型：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:Label ID="lbAddressType" runat="server" Text="Label"></asp:Label>
                                        <input type="hidden" id="Hidden_AddressType" runat="server" />
                                        <asp:DropDownList ID="ddlAddressType" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        上级地址：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        <asp:Label ID="lbParentAddress" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr style='display: <%=addressTree.SelectedValue=="1"&&Mode=="viewmode"?"none":"block"%>'>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        下级地址数量：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        &nbsp;<asp:Label ID="lbChildCount" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        修改人：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                        &nbsp;<asp:Label ID="lbModifier" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr style='display: <%=addressTree.SelectedValue=="1"&&Mode=="viewmode"?"none":"block"%>'>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        地址描述：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                        <asp:Label ID="lbDescription" runat="server" Text="Label"></asp:Label>
                                        <asp:TextBox ID="tbDescription" runat="server" Width="95%" TextMode="MultiLine" Rows="3"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: <%=(Mode!="viewmode")?"block":"none"%>">
                                    <td align="right" colspan="4">
                                        <asp:Button ID="btSave" runat="server" OnClientClick="return checkInput();" CssClass="button_bak"
                                            Text="保存" OnClick="btSave_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btCancel" CssClass="button_bak" runat="server" Text="取消" OnClick="btCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                        所属维护队：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:DropDownList ID="ddlMaintainTeam" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style='display: <%=(Mode!="addmode")?"block":"none"%>;'>
                                    <td class="Table_searchtitle" colspan="4" style="height: 30px">
                                        下级地址列表
                                    </td>
                                </tr>
                                <tr style='display: <%=(Mode!="addmode")?"block":"none"%>;'>
                                    <td style="height: 30px" colspan="4">
                                        <asp:GridView ID="gvSubAddress" runat="server" AutoGenerateColumns="False" Width="100%"
                                            OnRowCommand="gvSubAddress_RowCommand" OnRowDataBound="gvSubAddress_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="AddressCode" HeaderText="地址编号">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AddressName" HeaderText="地址名称">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        地址类型</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#EnumHelper.GetDescription((Enum)Eval("AddressType")) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ChildCount" HeaderText="子地址数量">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="地址描述">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Modifier" HeaderText="修改人">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                没有下级地址
                                            </EmptyDataTemplate>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                            <RowStyle HorizontalAlign="Center" Height="20px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <input type="hidden" id="SelectedAddress" runat="server" />
            <div style="width: 100%; text-align: center; display: <%=(operatorType=="select")?"block":"none"%>">
                <input type="button" value="确认" class="button_bak" onclick="javascript:OnOK();" />
                <input type="button" value="关闭" class="button_bak" onclick="javascript:window.parent.hidePopWin();" /></div>
            <asp:Button ID="btAddMode" runat="server" Text="Button" OnClick="btAddMode_Click"
                Style="display: none" />
            <asp:Button ID="btEditMode" runat="server" Text="Button" OnClick="btEditMode_Click"
                Style="display: none" />
            <asp:Button ID="btDelete" runat="server" Text="Button" OnClick="btDelete_Click" OnClientClick="javascript:return confirm('确认要删除地址吗？');"
                Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        function checkInput() {
           var obj=document.all.<%=tbAddressName.ClientID %>;
           if(obj.value.trim()=="")
           {
             alert('地址名称不能为空');
             return false;
           }
           else return true;
        }
        function OnOK()
        {
            if(<%= (int)selectType %> != <%=(int) AddressType.Unknown %>
            &&'<%= (int)selectType %>' != $get('<%= Hidden_AddressType.ClientID %>').value)
            {
                alert('必须选择类型为：'+ '<%= EnumHelper.GetDescription(selectType) %>' +' 的地址');
                return false;
            }
            
            window.returnVal = document.all.<%=SelectedAddress.ClientID %>.value;
            window.parent.hidePopWin(true);
        }
    </script>

</asp:Content>
