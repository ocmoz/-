<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditDeviceType.aspx.cs" Inherits="Module_FM2E_BasicData_DeviceTypeManage_EditDeviceType"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
        <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备种类信息维护"
        HeadOPTxt="目前操作功能：设备种类信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="设备种类列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="DeviceType.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript" ButtonPopedom="List" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <iframe style="Z-INDEX:-1;WIDTH:99%;POSITION:absolute;TOP:0px;" frameborder="0"></iframe>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
            <cc2:TabPanel HeaderText="编辑设备种类信息" runat="server" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                设备种类详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                种类名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="categoryname" title="请输入设备种类名称~20:!" runat="server"></asp:TextBox><span style="color:Red; font-weight:bold">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                设备单位：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList title="请选择单位~!" ID="Unit" runat="server">
                                    </asp:DropDownList><span style="color:Red; font-weight:bold">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                上级种类名称：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="parentcategoryname" onfocus="javascript:causeValidate = false;"  runat="server"></asp:TextBox>
                                <asp:Panel ID="Panel1" CssClass="popupControl" runat="server" BackColor="White">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                            </asp:TreeView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="parentcategoryname"
                                    PopupControlID="Panel1" Position="Bottom" DynamicServicePath="" Enabled="True"
                                    ExtenderControlID="">
                                </cc2:PopupControlExtender>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                折旧方法：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="depreciationmethod" runat="server">
                                    <asp:ListItem Value="0" Text="暂无方法"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="不折旧"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="直线折旧"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="双倍余额"></asp:ListItem>
                                </asp:DropDownList><span style="color:Red; font-weight:bold">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                折旧年限：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="depreciablelife"  title="~:int" runat="server"></asp:TextBox>年<span style="color:Red; font-weight:bold">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                净残值率：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="residualrate" title="~:float" runat="server"></asp:TextBox>%<span style="color:Red; font-weight:bold">*</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr runat="server">
                            <td align="right" style="height: 38px" runat="server">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" onmousemove="javascript:causeValidate = true;" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
