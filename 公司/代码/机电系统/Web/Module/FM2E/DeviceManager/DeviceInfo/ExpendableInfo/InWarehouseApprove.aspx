<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="InWarehouseApprove.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_InWarehouseApprove"
    Title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
 <script type="text/javascript">
     var fileId = 1;
     function addFile() {
         var FilesDiv = document.getElementById('FilesDiv');

         var divId = "div" + fileId;
         var str = '<div id="' + divId + '">';
         str += '<input type="file" size="40" name="File" style="border: solid 1px #0077B2">'
         str += '&nbsp;<img src="../../../../images/ICON/delete.gif" onclick="delFile(\'' + divId + '\')"/>';
         str += "<div>";
         FilesDiv.insertAdjacentHTML("beforeEnd", str)
         fileId++;
     }

     function delFile(obj) {
         var FilesDiv = document.getElementById('FilesDiv');
         var elem = document.getElementById(obj);
         FilesDiv.removeChild(elem);
     }    
        </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品入库申请单审批"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：易耗品入库申请单审批">
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
                入库备注：
            </td>
            <td class="table_none table_none_NoWidth" colspan="3">
                <asp:Label ID="LB_IWRemark" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                入库明细
            </td>
            <td colspan="3">
                <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                    text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
                    <tr>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 500px">
                                        产品名称：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="LB_IWName" runat="server" /><%# Eval("Name")%>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        型号：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="LB_IWModel" runat="server" /><%# Eval("Model")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        数量：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="LB_IWCount" runat="server" /><%# Eval("Count")%>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        单位：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="LB_IWUnit" runat="server" /><%# Eval("Unit")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        种类：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <asp:Label ID="LB_IWCategory" runat="server" /><%# Eval("CategoryName")%>
                                    </td>
                                    <td class="table_body table_body_NoWidth">
                                        单价：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
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
            <td class="table_body table_body_NoWidth" width="10%">
                上传附件：
            </td>
            <td class="table_none table_none_NoWidth" style="height: 30px; width: 90%" colspan="3">                
                <div id="FileUpload_div" runat="server">
                    <div id="FilesDiv">
                        <div id="div0">
                            <input type="file" runat="server" size="40" name="File" id="file0" style="border: solid 1px #0077B2" />
                            <img src="../../../../../images/ICON/delete.gif" onclick="delFile('div0')" />
                        </div>
                    </div>
                    <input type="button" value="添加附件" onclick="addFile();return false" id="btnInput"
                        runat="server" />
                </div>
                <div id="downFileDiv">
                    <asp:GridView ID="gridviewFile" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                        DataKeyNames="Editreason1id" HeaderStyle-Height="25px" RowStyle-Height="20px"
                        Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                        OnRowCommand="GridView_OnRowCommand" OnRowDataBound="GridView_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号" ShowHeader="False">
                                <ItemTemplate>
                                    <%# Eval("Editreason1id") %>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="附件" ShowHeader="False">
                                <ItemTemplate>
                                    <%# Eval("Name")%>
                                </ItemTemplate>
                                <ItemStyle Width="70%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                        CommandArgument='<%# Eval("Editreason1id") %>' CommandName="DELETE_SP" ImageUrl="~/images/ICON/delete.gif"
                                        Text="删除" OnClientClick="javascript:return confirm('确认删除该项？');" />
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            当前无附件
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
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
        <tr runat="server" id="EAndA">
            <td class="table_body table_body_NoWidth">
                审批意见：
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbApprovalRemark" runat="server" Height="70px" MaxLength="200" TextMode="MultiLine"
                    Width="414px" Text="同意"></asp:TextBox>
            </td>
        </tr>
    </table>
    <center>
        <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="提 交" CssClass="button_bak" OnClientClick="javascript:return confirm('确定提交审批？');"
            OnClick="Button1_Click" Height="20px" />
        <input id="Reset1" class="button_bak" runat="server" type="button" value="返回" onclick="javascript:window.history.go(-1);" />
    </center>
</asp:Content>
