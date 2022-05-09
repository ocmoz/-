<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPageNoCheck.master" autoeventwireup="true" inherits="Module_FM2E_ExamineManager_ExamineResult_GetExamineResult, App_Web_dtuveobp" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="生成考核结果表"
        HeadOPTxt="目前操作功能：生成考核结果表" HeadHelpTxt="帮助">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="考核结果列表" ButtonUrlType="Href"
            ButtonUrl="ExamineResultList.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%">
        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    生成考核结果
                </td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth">
                    考核对象：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:DropDownList ID="ddlExamineTarget" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="table_body table_body_NoWidth">
                    考核时间：
                </td>
                <td class="table_none table_none_NoWidth">
                    <asp:DropDownList ID="ddlYears" runat="server">
                    </asp:DropDownList>
                    年
                    <asp:DropDownList ID="ddlSeason" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td colspan="6" class="table_none_WithoutWidth">
                    日常考核占总分比例：<input type="text" runat="server" id="DailyExamineRatio_inp" />%&nbsp;&nbsp;&nbsp;&nbsp;
                    季度考核占总分比例：<input type="text" runat="server" id="SeasonExamineRatio_inp" />%&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btGenerate" runat="server" Text="生成考核结果" CssClass="button_bak2" OnClick="btGenerate_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_RowDataBound">
            <HeaderTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                    style="border-collapse: collapse;">
                    <tr>
                        <td class="Table_searchtitle" colspan="6">
                            考核结果
                        </td>
                    </tr>
                    <tr>
                        <td class="Table_searchtitle">
                            考核表编号
                        </td>
                        <td class="Table_searchtitle">
                            考核表名称
                        </td>
                        <td class="Table_searchtitle">
                            考核类型
                        </td>
                        <td class="Table_searchtitle">
                            考核人
                        </td>
                        <td class="Table_searchtitle">
                            考核得分
                        </td>
                        <td class="Table_searchtitle">
                            考核时间
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="table_none_WithoutWidth">
                        <asp:Literal ID="ltSheetNO" runat="server"></asp:Literal>
                    </td>
                    <td class="table_none_WithoutWidth">
                        <%#Eval("ExamSheetName") %>
                    </td>
                    <td class="table_none_WithoutWidth">
                        <%#EnumHelper.GetDescription((Enum)Eval("ExamineType")) %>
                    </td>
                    <td class="table_none_WithoutWidth">
                        <%#Eval("ExaminerName") %>
                    </td>
                    <td class="table_none_WithoutWidth">
                        <%#Eval("Score") %>分
                    </td>
                    <td class="table_none_WithoutWidth">
                        <%#Eval("SaveTime","{0:yyyy-MM-dd}") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td colspan="6" class="table_none_WithoutWidth">
                        日常考核得分：<%=CurrentExamineResult.DailyExamineResult.ToString("0.##") %>&nbsp;&nbsp;&nbsp;&nbsp;
                        季度考核得分：<%=CurrentExamineResult.SeasonExamineResult.ToString("0.##") %>&nbsp;&nbsp;&nbsp;&nbsp;
                        总分：<%=CurrentExamineResult.DailyExamineResult.ToString("0.##") %>×<%=CurrentExamineResult.DailyExamineRatio*100 %>%+
                        <%=CurrentExamineResult.SeasonExamineResult.ToString("0.##") %>×<%=CurrentExamineResult.SeasonExamineRatio*100 %>%＝
                        <%=CurrentExamineResult.Score.ToString("0.##") %>分
                    </td>
                </tr>
                </table></FooterTemplate>
        </asp:Repeater>
        <table runat="server" id="ButtonTable" visible="false">
            <tr>
                <td>
                    输入考核表名称：<asp:TextBox ID="tbSheetName" runat="server" MaxLength="50" title="请输入考核表名称~50:!"></asp:TextBox>
                    <asp:Button ID="btSave" runat="server" OnClick="btSave_Click" Text="保存考核结果" CssClass="button_bak2"
                        OnClientClick="javascript:return checkForm(document.forms[0],true)&&confirm('确定要保存考核结果吗?');" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
