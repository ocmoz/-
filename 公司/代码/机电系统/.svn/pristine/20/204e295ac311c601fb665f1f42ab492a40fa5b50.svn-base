<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditDept.aspx.cs" Inherits="Module_FM2E_BasicData_DeptManage_EditDept"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="部门信息维护"
        HeadOPTxt="目前操作功能：职位信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="取消修改" ButtonPopedom="List"
            ButtonUrlType="Href" ButtonUrl="ViewDept.aspx?cmd=view&companyid={0}&id={1}" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="width: 100%;">
                <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#cccccc" border="1pt"
                    style="border-collapse: collapse;">
                    <tr>
                        <td class="Table_searchtitle" colspan="4">
                            部门详细信息
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            部门名称：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox8" title="请输入部门名称~20:!" runat="server"></asp:TextBox>
                            <span style="color: Red">*</span>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            分管公司：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:DropDownList ID="ddlCompany" runat="server" title="请选择分管公司~">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            联系电话：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox3" title="请输入联系电话~20:" runat="server"></asp:TextBox><span
                                style="color: Red"></span>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            负责人：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <%--                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>--%>
                            <input type="text" runat="server" id="TextBox4" onclick="javascript:showPopWin('选择部门负责人','../../SystemManager/UserManager/SelectUser.aspx?number=1',900, 400, addResponsiblity,true,true);" />
                            <input class="cbutton" onclick="javascript:Clear1();" type="button" value="清除" id="Button2" />
                            <input id="principalID" runat="server" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            上级部门：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px; color: #FF0000;">
                            <asp:TextBox onfocus="javascript:causeValidate=false;" ID="TextBox5" runat="server"
                                AutoPostBack="True"></asp:TextBox>
                            点击修改部门位置
                            <asp:Panel ID="Panel1" CssClass="popupControl" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="TextBox5"
                                PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                ExtenderControlID="">
                            </cc2:PopupControlExtender>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            部门类型：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:DropDownList ID="DropDownList_DepartmentType" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            备注：
                        </td>
                        <td colspan="3" class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox9" runat="server" title="请输入备注~1000:" TextMode="MultiLine"
                                Rows="4" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                            子部门数：
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                        </td>
                        <td class="table_body table_body_NoWidth" style="height: 30px">
                        </td>
                        <td class="table_none table_none_NoWidth" style="height: 30px">
                        </td>
                    </tr>
                </table>
                <center>
                    <asp:Button ID="Button1" onmousemove="javascript:causeValidate=true;" runat="server"
                        CssClass="button_bak" Text="确定" OnClick="Button1_Click" />
                    &nbsp;&nbsp;
                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input type="hidden" runat="server" id="olderparentcompanyid" />

    <script type="text/javascript" language="javascript">

    function addResponsiblity(addstring)
    {
        var arr = addstring.split(",");
        if(arr.length>=2){
         document.getElementById('<%= TextBox4.ClientID %>').value = arr[1];
         document.getElementById('<%= principalID.ClientID %>').value = arr[0];
         }
        
    }
        
            function Clear1()
    {
        document.all.<%=this.principalID.ClientID %>.value='';
	    document.all.<%=this.TextBox4.ClientID %>.value='';  
    }
        

    </script>

</asp:Content>
