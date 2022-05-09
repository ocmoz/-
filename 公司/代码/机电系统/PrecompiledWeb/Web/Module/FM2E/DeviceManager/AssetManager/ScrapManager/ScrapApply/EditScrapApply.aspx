<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapApply_EditScrapApply, App_Web_no8k4bt3" title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备报废申请"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：申请设备报废">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="报废申请列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="ScrapApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
                 <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="添加报废申请" ID="TabPanel1">
                        <ContentTemplate>
                            <div style="width: 850px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" colspan="4">
                                            设备报废申请单
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            表单编号：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                            <asp:Label ID="lbSheetNO" runat="server" Columns="20" Width="120px"></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            公司：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:Label ID="lbCompany" runat="server"></asp:Label>   
                                            <div style="display:none">
                                        <asp:DropDownList ID="DropDownList_Company" runat="server">
                                        </asp:DropDownList></div>                                         
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            部门：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:DropDownList ID="ddlDep" runat="server" title="请选择部门~!">
                                            </asp:DropDownList>
                                            <span style="color:Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            报废设备条形码：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                             <asp:TextBox ID="tbEquipmentNO" runat="server" MaxLength="20" 
                                                title="请输入报废设备条形码~20:" AutoPostBack="True" 
                                                ontextchanged="tbEquipmentNO_TextChanged"></asp:TextBox>
                                            <span style="color:Red">*</span>
                                        </td>
                                        <td  style="height: 30px">
                                            报废设备名称：</td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                           <asp:TextBox ID="tbEquipmentName" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            报废原因：</td>
                                        <td class="table_none table_none_NoWidth" colspan="3">
                                            <asp:DropDownList ID="tbReason" runat="server" title="请选择报废原因!" 
                                                 Width="95%">
                                            </asp:DropDownList>
                                            
                                        </td>
                                    </tr>
                                     <tr>
                        <td class="table_body table_body_NoWidth" style="height: 30px;">
                            附件记录：
                        </td>
                        <td class="table_none_WithoutWidth " style="height: 30px;" colspan="5">
                            <asp:FileUpload ID="FileUpload_ArchivesAttachmentFile" runat="server" /> 
                            <asp:HyperLink ID="HyperLink_File" ForeColor="Blue" Font-Underline="true"
                                runat="server" Visible="false" ></asp:HyperLink>                          
                        </td>
                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            备注：</td>
                                        <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                            <asp:TextBox ID="tbRemark" runat="server" TextMode="MultiLine" MaxLength="100" title="请输入备注~100:" Width="95%" Rows="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                            申请人：</td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:Label ID="lbApplicant" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            申请单状态：</td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:Label ID="lbStatus" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                                    <tr>
                                        <td id="Td1" align="right" style="height: 38px" runat="server">
                                            <asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="保存草稿" OnClick="Button2_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="提交申请" OnClick="Button3_Click" />&nbsp;&nbsp;
                                            <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="Company"
                                        Enabled="True" LoadingText="公司加载中..." PromptText="请选择公司..." ServiceMethod="GetCompany"
                                        ServicePath="~/Module/FM2E/SystemManager/UserManager/CompanyDeptService.asmx" TargetControlID="DropDownList_Company">
                                    </cc2:CascadingDropDown>
                                    <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="Department"
                                        Enabled="True" LoadingText="部门加载中..." ParentControlID="DropDownList_Company" PromptText="请选择部门..."
                                        ServiceMethod="GetDepartment" ServicePath="~/Module/FM2E/SystemManager/UserManager/CompanyDeptService.asmx" TargetControlID="ddlDep">
                                    </cc2:CascadingDropDown>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
    </div>
</asp:Content>

