<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPageNoCheck.master" autoeventwireup="true" inherits="Module_FM2E_ExamineManager_Examine_ViewExamine, App_Web_y0z1ertm" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="查看考核表"
        HeadOPTxt="目前操作功能：查看考核表" HeadHelpTxt="帮助">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="编辑" ButtonPopedom="Edit" ButtonUrlType="Href" ButtonUrl="EditExamine.aspx?cmd=edit" />
        <cc1:HeadMenuButtonItem ButtonIcon="delete.gif" ButtonName="删除" ButtonPopedom="Delete" ButtonUrlType="JavaScript" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%">
    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr><td class="Table_searchtitle" colspan="4"><%=title%>表</td></tr>
            <tr>
            <td class="table_body table_body_NoWidth">考核表编号：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbSheetNO" runat="server" Text=""></asp:Label></td>
            <td class="table_body table_body_NoWidth">考核表名称：</td>
            <td class="table_none table_none_NoWidth">
                 <asp:Label ID="lbSheetName" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
            <td class="table_body table_body_NoWidth">公司：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbCompany" runat="server" Text="Label"></asp:Label></td>
            <td class="table_body table_body_NoWidth">考核人：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbExaminer" runat="server" Text="Label"></asp:Label>
            </td>
            </tr>
            <tr>
            <td class="table_body table_body_NoWidth">考核对象：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbExamineTarget" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="table_body table_body_NoWidth">考核时间：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbExamineDate" runat="server" Text="Label"></asp:Label></td>
            </tr>
                 <tr>
            <td class="table_body table_body_NoWidth">考核结果确认人：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbConfirmer" runat="server" Text="Label"></asp:Label></td>
            <td class="table_body table_body_NoWidth">确认结果：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbConfirmResult" runat="server" Text="Label"></asp:Label>
            </td>
            </tr>
            <tr>
            <td class="table_body table_body_NoWidth">确认备注：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbConfirmRemark" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="table_body table_body_NoWidth">确认时间：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbConfirmDate" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>
         <asp:UpdatePanel ID="UpdatePanel_List" runat="server">
        <ContentTemplate>
        <asp:Repeater ID="rptExamineItems" runat="server" OnItemDataBound="rptExamineItems_DataBound">
            <HeaderTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td colspan="4" class="Table_searchtitle">
                            考核项
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr runat="server" id="trItem">
                    <td style="width: 20%;border-right-width: 0px;"
                        class="table_none_WithoutWidth" rowspan='<%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?2:1 %>'>
                        <span style=" color: #1e5494; "><%#Eval("NO")%><%#Eval("ItemName") %></span>
                         <span style='display: <%#Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none"%>'>
                    </td>
                    <td style="width: 17%; border-left-width: 0px; border-right-width: 0px;" class="table_none_WithoutWidth">
                    <span style='display:<%#Convert.ToInt32(Eval("ChildCount"))!=0||Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none" %>'>
                    <span style='display:<%#(type==FM2E.Model.Examine.ExamineType.DailyExamine)&&(Convert.ToInt32(Eval("Level"))==1)?"none":"inline"%>'>
                       <%= (type==FM2E.Model.Examine.ExamineType.SeasonExamine)?"总分：":"占分（%）：" %>
                        <%#Eval("Score")%><br /></span></span>
                         <span style='display:<%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none" %>'>
                         扣分：<%#Eval("Deduct","{0:0.##}") %>
                    </span>
                      <span style='display: <%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?"none":"inline"%>'>
                         得分：<span style="color:Red"><%#Eval("ExamScore") %></span>
                         </span>
                    </td>
                    <td class="table_none_WithoutWidth" style="border-left-width: 0px; border-right-width: 0px;">
                    <span style='display:<%#Convert.ToInt32(Eval("ChildCount"))!=0||Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none" %>'>
                        <%#Convert.ToBoolean(Eval("CanAddChild")) ? "考核标准：" + (!string.IsNullOrEmpty((string)Eval("Content")) ? Eval("Content") : "暂无") : ""%>
                        </span>
                        <span style='display:<%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none" %>'>
                         考核人：<%#Eval("ExaminerName") %>
                         </span>
                    </td>
                    <td class="table_none_WithoutWidth" style="border-left-width: 0px; border-right-width: 0px;">
                        <span style='display:<%#Convert.ToInt32(Eval("ChildCount"))!=0||Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none" %>'>
                        <%#Convert.ToBoolean(Eval("CanAddChild")) ? "评分标准：" + (!string.IsNullOrEmpty((string)Eval("Standard")) ? Eval("Content") : "暂无") : ""%>
                    </span>
                    <span style='display:<%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none" %>'>
                         考核时间：<%#Eval("ExamineDate","{0:yyyy-MM-dd}") %>
                         </span>
                    </td>
                </tr>
                <tr>
                <td colspan="3" class="table_none_WithoutWidth" style='display:<%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none" %>'>
          
                扣分原因：<%#Eval("DeductReason") %><br />
                备注：<%#Eval("Remark") %>
                </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>   
        </ContentTemplate>   
        </asp:UpdatePanel>
    </div>
</asp:Content>

