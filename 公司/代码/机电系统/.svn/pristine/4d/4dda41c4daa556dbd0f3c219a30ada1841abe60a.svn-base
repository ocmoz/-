<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="EditUser.aspx.cs" Inherits="Module_FM2E_SystemManager_UserManager_EditUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="用户管理"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：用户信息添加">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="用户列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="UserList.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="用户基本信息" ID="TabPanel1">
                <HeaderTemplate>
                    用户基本信息
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    用户基本信息
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    用户名：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbUserName" runat="server" Columns="20" title="请输入用户名~20:noChinese!"
                                        Width="120px"></asp:TextBox><span style="color: Red">*</span> &nbsp;
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    用户姓名：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbPersonName" runat="server" title="请输入用户姓名~20:!"></asp:TextBox><span
                                        style="color: Red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    密码：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbPassword" runat="server" Columns="20" TextMode="Password" title="请输入密码~32:noChinese!"
                                        Width="120px"></asp:TextBox><span style="color: Red">*</span>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    确认密码：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbConfirmPassword" runat="server" Columns="20" TextMode="Password"
                                        title="请输入确认密码~32:noChinese!" Width="120px"></asp:TextBox><span style="color: Red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    分管公司：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <%--<div style='display: <%=(((bool)ViewState["IsShow"])?"inline":"none")%>'>--%>
                                        <asp:DropDownList ID="ddlCompany" runat="server" title="请选择所属公司~!">
                                        </asp:DropDownList>
                                        <%--<span style="color: Red">*</span>
                                    </div>
                                    <%if (!((bool)ViewState["IsShow"]))
                                      { %>
                                    <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label>
                                    <%} %>--%>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    所属部门：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" title="请选择所属部门~!">
                                    </asp:DropDownList>
                                </td>
                               <%-- <cc2:CascadingDropDown ID="cddCompany" runat="server" Category="Company" Enabled="True"
                                    PromptValue="0" LoadingText="公司加载中..." PromptText="请选择公司" ServiceMethod="GetCompany"
                                    ServicePath="CompanyDeptService.asmx" TargetControlID="ddlCompany">
                                </cc2:CascadingDropDown>
                                <cc2:CascadingDropDown ID="cddDepartment" runat="server" Category="Department" Enabled="True"
                                    PromptValue="0" LoadingText="部门加载中..." ParentControlID="ddlCompany" PromptText="请选择部门"
                                    ServiceMethod="GetDepartment" EmptyText="请选择部门" EmptyValue="0" ServicePath="CompanyDeptService.asmx"
                                    TargetControlID="ddlDepartment">
                                </cc2:CascadingDropDown>--%>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    职位：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlPosition" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    性别：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    用户类型：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlUserType" runat="server">
                                        <asp:ListItem Value="1">超级用户</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">普通用户</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    用户状态：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlUserStatus" runat="server">
                                        <asp:ListItem Value="1">正常</asp:ListItem>
                                        <asp:ListItem Value="2">停用</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    用户角色：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                    <cc1:MultiListBox ID="mblRoles" runat="server" DataTextField="RoleName" DataValueField="RoleID"
                                        Heigth="" Rows="4" SelectionMode="Multiple">
                                        <FirstListBox Style="">
                                            <StyleSheet Height="160px" Width="140px" />
                                        </FirstListBox>
                                        <SecondListBox Style="">
                                            <StyleSheet Height="160px" Width="140px" />
                                        </SecondListBox>
                                    </cc1:MultiListBox>
                                    注：超级用户不受角色限制（左边为未拥有的角色，右边为已拥有的角色）
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="附加信息" ID="TabPanel2">
                <HeaderTemplate>
                    附加信息
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    用户附加信息
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    员工工号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbStaffNO" runat="server" Columns="20" title="请输入员工工号~20:noChinese"
                                        Width="120px"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    身份证号码：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbIDCard" runat="server" Columns="20" title="请输入身份证号码~20:noChinese"
                                        Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    出生日期：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbBirthday" runat="server" title="请输入出生日期~date" CssClass="input_calender"
                                        onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    办公电话：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbOfficePhone" runat="server" title="请输入办公电话~20:noChinese"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    手机：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbMobilePhone" runat="server" title="请输入手机号~20:noChinese"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    家庭电话：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbHomePhone" runat="server" title="请输入家庭电话~20:noChinese"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    传真：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbFax" runat="server" title="请输入传真~20:noChinese"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    住址：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbAddress" runat="server" title="请输入住址~50:"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    Email：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbEmail" runat="server" title="请输入Email~50:noChinese"></asp:TextBox>
                                </td>
                                <%--<td class="table_body table_body_NoWidth" style="height: 30px">
                                    IM（例如QQ）：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbIM" runat="server" title="请输入IM号~30:noChinese"></asp:TextBox>
                                </td>--%>
                                
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    所属系统工程师：
                                </td>
                                <%--<td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList runat="server" ID="ddlusersystemid"></asp:DropDownList>
                                </td>--%>
                                 <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                    <cc1:MultiListBox ID="sysNames" runat="server" DataTextField="SystemName" DataValueField="SystemID"
                                        Heigth="" Rows="4" SelectionMode="Multiple">
                                        <FirstListBox Style="">
                                            <StyleSheet Height="160px" Width="140px" />
                                        </FirstListBox>
                                        <SecondListBox Style="">
                                            <StyleSheet Height="160px" Width="140px" />
                                        </SecondListBox>
                                    </cc1:MultiListBox>
                                    
                                </td>
                                
                                
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    主要职责：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                    <asp:TextBox ID="tbResposibility" runat="server" title="请输入主要职责~100:" TextMode="MultiLine"
                                        Rows="3" Width="95%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    照片：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="FileUpload1" runat="server" onpropertychange="document.all('myimg').src=this.value;imgDiv.style.display='block';"
                                                Height="20px" Width="250px" />
                                            <asp:Button ID="ButtonCancel" runat="server" Text="取消修改" OnClientClick="imgDiv.style.display='none'"
                                                Visible="false" OnClick="ButtonCancel_Click" CssClass="button_bak" />
                                            <asp:Image ID="imgPhotoThumb" runat="server" ToolTip="单击查看大图" Width="120px" 
                                                Style="display: inline" />
                                            <asp:Button ID="btModifyPic" Text="修改图片" OnClick="btModifyPic_Click" CssClass="button_bak"
                                                runat="server" />
                                            <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="imgPhotoThumb"
                                                PopupControlID="Panel1" DynamicServicePath="" Enabled="True" ExtenderControlID="">
                                            </cc2:PopupControlExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Panel ID="Panel1" CssClass="popupControl" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Image ID="imgPhoto" Width="200px" OnClientClick="javascript:return false;" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div id="imgDiv" style="display: none">
                                        <img src="" id="myimg" width="200px">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
            <tr id="Tr1" runat="server">
                <td id="Td1" align="right" style="height: 38px" runat="server">
                    <asp:Button ID="btSave" runat="server" CssClass="button_bak" Text="确定" OnClick="btSave_Click" />&nbsp;&nbsp;
                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
