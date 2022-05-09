<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="InWarehouse.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_InWarehouse"
    Title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品入库申请信息"
        HeadOPTxt="目前操作功能：易耗品入库申请单">
    </cc1:HeadMenuWebControls>
    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
        text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                入库详细信息
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" style="width: 10%">
                入库部门：
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="DDL_Department" runat="server" title="请选择入库部门~!">
                </asp:DropDownList>
                <span style="color: Red">*</span>
            </td>
              <td class="table_body table_body_NoWidth" style="width:20%">
                易耗品入库申请单号：
            </td>
            <td class="table_none table_none_NoWidth">
               <asp:Label ID="InWarehouseText" runat="server" />
            </td>
        </tr>               
        <tr>
            <td class="table_body table_body_NoWidth">
                入库明细：
            </td>
            <td class="table_none table_none_NoWidth" colspan="3">
                <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                产品名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_IWName" runat="server" title="请输入产品名称~20:"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                型号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_IWModel" runat="server" title="请输入型号~20:"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                数量：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_IWCount" runat="server" title="请输入数量~float!"></asp:TextBox><span
                                    style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                单位：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_IWUnit" runat="server" title="请选择单位">
                                </asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                种类：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_IWCategory" runat="server" title="请选择种类">
                                </asp:TextBox>
                                <asp:TextBox ID="TB_IWCategoryID" runat="server" title="请选择种类" Visible="false"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                单价：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TB_IWPrice" runat="server" title="请输入单价~float"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                入库备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:TextBox ID="TB_IWRemark" runat="server" title="请输入入库备注~50:" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
       
    </table>
    <center>
        <asp:Button ID="btSubmit" runat="server" CssClass="button_bak2" Text="保存或提交申请" OnClientClick="javascript:return confirm('确定提交或保存？');"
            OnClick="Button_SaveInWarehouse_Click" />&nbsp;&nbsp;
        <input id="Reset1" class="button_bak" runat="server" type="button" value="取消" onclick="javascript:window.parent.hidePopWin();" />
         
        <input id="Reset2" class="button_bak" runat="server" type="button" value="返回" onclick="javascript:window.history.go(-1);"  stytle="display:none"/>
   
    </center>
</asp:Content>
