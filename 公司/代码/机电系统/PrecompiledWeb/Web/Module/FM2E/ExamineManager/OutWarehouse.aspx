<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_OutWarehouse, App_Web_wqec-swl" title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
        text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
        <tr>
            <td class="Table_searchtitle" colspan="2">
                出库详细信息 <input id="hidden_totalcount" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="width: 10%">
                出库部门：
            </td>
            <td class="table_none table_none_NoWidth" style="width: 90%">
                <asp:DropDownList ID="DDL_OutDepartment" runat="server" title="请选择出库部门~!">
                </asp:DropDownList>
                <span style="color: Red">*</span>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                出库明细：
            </td>
            <td class="table_none table_none_NoWidth">
                <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                产品名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">              
                                <asp:TextBox ID="TB_OWName" runat="server" title="请输入产品名称~20:"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                型号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">                              
                                <asp:TextBox ID="TB_OWModel" runat="server" title="请输入型号~20:"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                数量：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_OWCount" runat="server" title="请输入数量~float!"></asp:TextBox><span
                                    style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                单位：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_OWUnit" runat="server" title="请选择单位">
                                </asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                种类：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                               <asp:TextBox ID="TB_OWCategory" runat="server" title="请选择种类">
                                </asp:TextBox>
                                <asp:TextBox ID="TB_OWCategoryID" runat="server" title="请选择种类" Visible="false"></asp:TextBox>
                                 <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                单价：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                 <asp:TextBox ID="TB_OWPrice" runat="server" title="请输入单价~float"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                出库时间：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_OWDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                title="请输入出库时间~date!"></asp:TextBox>
                               <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                出库备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:TextBox ID="TB_OWRemark" runat="server" title="请输入出库备注~50:" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                经办人：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                账号<asp:TextBox ID="TB_HandlerID" runat="server" title="请输入经办人ID~20:!"></asp:TextBox> <span style="color: Red">*</span>
                                姓名<asp:TextBox ID="TB_HandlerName" runat="server" title="请输入经办人姓名~20:!"></asp:TextBox> <span style="color: Red">*</span>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="出库" OnClick="Button_SaveOutWarehouse_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="button" value="取消" onclick="javascript:window.parent.hidePopWin();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr> 
    </table>
</asp:Content>

