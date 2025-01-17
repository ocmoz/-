﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ViewInWarehouse.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ViewInWarehouse"
    Title="易耗品入库单查看" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品入库单查看"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：易耗品入库单查看">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" />
        <%--<cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="撤单" ButtonPopedom="Delete" />--%>
        <%--<cc1:HeadMenuButtonItem ButtonIcon="print.gif" ButtonName="打印" ButtonPopedom="NotControl" />--%>
    </cc1:HeadMenuWebControls>

    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
        text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                入库详细信息
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                入库部门：
            </td>
            <td>
                <asp:Label ID="LB_Department" runat="server"></asp:Label>
            </td>
            <td class="table_body table_body_NoWidth">
                易耗品入库单号:
            </td>
            <td>
                <asp:Label ID="LB_sheetName" runat="server" />
            </td>
        </tr>
        <tr>           
            <td class="table_body table_body_NoWidth" style="height: 30px">
                申请时间：
            </td>
            <td class="table_none table_none_NoWidth" style="height: 30px">
                <asp:Label ID="Lb_SubmitTime" runat="server" />
            </td>
        </tr>      
        <tr>
            <td class="table_body table_body_NoWidth">
                入库明细
            </td>
            <td colspan="3">
                <table  style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
        text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td colspan="3">
                           <table>
                                        <tr>
                                            <td class="table_body table_body_NoWidth" style="width: 500px" >
                                                产品名称：
                                            </td>
                                            <td class="table_none table_none_NoWidth" >
                                                <asp:Label ID="LB_IWName" runat="server" /><%# Eval("Name")%>
                                            </td>
                                            <td class="table_body table_body_NoWidth">
                                                型号：
                                            </td>
                                            <td class="table_none table_none_NoWidth" >
                                                <asp:Label ID="LB_IWModel" runat="server" /><%# Eval("Model")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="table_body table_body_NoWidth">
                                                数量：
                                            </td>
                                            <td class="table_none table_none_NoWidth" >
                                                <asp:Label ID="LB_IWCount" runat="server" /><%# Eval("Count")%>
                                            </td>
                                            <td class="table_body table_body_NoWidth" >
                                                单位：
                                            </td>
                                            <td class="table_none table_none_NoWidth" >
                                                <asp:Label ID="LB_IWUnit" runat="server" /><%# Eval("Unit")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="table_body table_body_NoWidth" >
                                                种类：
                                            </td>
                                            <td class="table_none table_none_NoWidth" >
                                                <asp:Label ID="LB_IWCategory" runat="server" /><%# Eval("CategoryName")%>
                                            </td>
                                            <td class="table_body table_body_NoWidth" >
                                                单价：
                                            </td>
                                            <td class="table_none table_none_NoWidth" >
                                                <asp:Label ID="LB_IWPrice" runat="server" /><%# Eval("ExpendablePrice")%>
                                            </td>
                                        </tr>
                                    </table>
                              
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
          <tr>
            <td class="table_body table_body_NoWidth">
                入库备注：
            </td>
            <td class="table_none table_none_NoWidth" colspan="3">
                <asp:Label ID="LB_IWRemark" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth" width="10%">
                上传附件：
            </td>
            <td class="table_none table_none_NoWidth" style="height: 30px; width: 90%" colspan="3">
           <asp:HyperLink ID="HyperLink_File" ForeColor="Blue" Font-Underline="true"
                                runat="server" ></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="Table_searchtitle" colspan="4">
                审批记录
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table border="0" width="100%">
                            <tr>
                                <th>
                                    审批部门
                                </th>
                                <th>
                                    审批结果
                                </th>
                                <th>
                                    审批意见
                                </th>
                                <th>
                                    职位
                                </th>
                                <th>
                                    用户名
                                </th>
                                <th>
                                    审批日期
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr align="center">
                            <td>
                                <%#Eval("UserDeptName")%>
                            </td>
                            <td>
                                <%#Eval("EvenName")%>
                            </td>
                            <td>
                                <%#Eval("Remark")%>
                            </td>
                            <td>
                                <%#Eval("UserPsnName")%>
                            </td>
                            <td>
                                <%#Eval("UserName")%>
                            </td>
                            <td>
                                <%#Eval("ApprovalDate")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table ></FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>        
    </table>       
    <center>
        <input id="Reset1" class="button_bak" runat="server" type="button" value="返回" onclick="javascript:window.history.go(-1);" />
    </center>
</asp:Content>
