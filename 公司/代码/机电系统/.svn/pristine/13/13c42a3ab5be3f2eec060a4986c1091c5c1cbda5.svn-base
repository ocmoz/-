<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ExamineList.aspx.cs" Inherits="Module_FM2E_ExamineManager_Examine_ExamineList" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="考核表"
        HeadOPTxt="目前操作功能：考核表列表" HeadHelpTxt="帮助">
         <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加日常考核表" ButtonUrlType="Href"
            ButtonUrl="EditExamine.aspx?cmd=add&type=daily" />
                  <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加季度考核表" ButtonUrlType="Href"
            ButtonUrl="EditExamine.aspx?cmd=add&type=season" />
            <cc1:HeadMenuButtonItem ButtonIcon="select.gif" ButtonName="考核流程" ButtonUrlType="JavaScript"
             ButtonUrl="showPopWin('考核流程', '../ExamineFlow/ExamFlow.aspx', 900,350,null, true, true);" />
    </cc1:HeadMenuWebControls>
    <div style="width:100%">
    <table  width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
    <tr>
    <td class="table_body table_body_NoWidth">考核单编号：<asp:TextBox ID="tbSheetNO" runat="server"></asp:TextBox></td>
    <td class="table_body table_body_NoWidth">公司：<asp:DropDownList ID="ddlCompany" runat="server">
        </asp:DropDownList>
    </td>
    <td class="table_body table_body_NoWidth">考核对象：<asp:DropDownList ID="ddlExamineTarget" runat="server">
        </asp:DropDownList>
    </td>
    <td class="table_body table_body_NoWidth">考核人：<asp:TextBox ID="tbExaminer" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
    <td class="table_body table_body_NoWidth">考核表类型：<asp:DropDownList ID="ddlExamineType" runat="server">
        </asp:DropDownList>
    </td>
    <td class="table_body table_body_NoWidth">考核表状态：<asp:DropDownList ID="ddlStatus" runat="server">
        </asp:DropDownList>
    </td>
    <td class="table_body table_body_NoWidth" colspan="2">考核时间：
    <asp:TextBox ID="tbSaveTimeFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入考核时间~date"></asp:TextBox>-
    <asp:TextBox ID="tbSaveTimeTo"  runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入考核时间~date"></asp:TextBox> <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="button_bak" OnClick="btSearch_Click"/>
    </td>
    </tr>
    </table>
        <asp:GridView ID="gvExamineSheets" runat="server" Width="100%" AutoGenerateColumns="False"
            OnRowCommand="gvExamineSheets_RowCommand" OnRowDataBound="gvExamineSheets_RowDataBound">
            <EmptyDataTemplate>
                没有任何的考核表
            </EmptyDataTemplate>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="ExamSheetNO" HeaderText="考核表编号" />
                <asp:BoundField DataField="ExamSheetName" HeaderText="考核表名" />
                <asp:TemplateField>
                <HeaderTemplate>考核表类型</HeaderTemplate>
                <ItemTemplate>
                <%#EnumHelper.GetDescription((Enum)Eval("ExamineType")) %>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ExamineTargetName" HeaderText="考核对象"/>
                <asp:BoundField DataField="ExaminerName" HeaderText="考核人" />
                <asp:BoundField DataField="SaveTime" HeaderText="考核时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="Score" HeaderText="考核得分" DataFormatString="{0:0.#}" />
                <asp:TemplateField>
                <HeaderTemplate>表单状态</HeaderTemplate>
                <ItemTemplate>
                <%#EnumHelper.GetDescription((Enum)Eval("Status")) %>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField>
                 <HeaderTemplate>查看</HeaderTemplate>
                <ItemStyle Width="60px" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/ICON/select.gif"
                        CommandName="view" CommandArgument='<%#Eval("ExamSheetID")%>' 
                        CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
             <HeaderTemplate>删除</HeaderTemplate>
                <ItemStyle Width="60px" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                        CommandName="del" CommandArgument='<%#Eval("ExamSheetID")%>' OnClientClick="return confirm('确认要删除此考核表吗？')"
                        CausesValidation="false" Visible='<%#(Convert.ToInt32(Eval("Status"))==(int)FM2E.Model.Examine.ExamineSheetStatus.Draft)||(Convert.ToInt32(Eval("Status"))==(int)FM2E.Model.Examine.ExamineSheetStatus.Waiting4ExamineConfirm)||(Convert.ToInt32(Eval("Status"))==(int)FM2E.Model.Examine.ExamineSheetStatus.ExamineConfirmNotPassed)?true:false %>'  />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
            <RowStyle HorizontalAlign="Center" Height="20px" />
        </asp:GridView>
        <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
            AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="{0}"
            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
        </cc1:AspNetPager>
    </div>
</asp:Content>

