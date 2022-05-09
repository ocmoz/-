<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageNoCheck.master" AutoEventWireup="true"
    CodeFile="ExamineItemConfig.aspx.cs" Inherits="Module_FM2E_ExamineManager_ExamineItemConfig_ExamineItemConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="考核项配置"
        HeadOPTxt="目前操作功能：考核项配置" HeadHelpTxt="帮助">
    </cc1:HeadMenuWebControls>
    <div style="width: 100%">
        <asp:UpdatePanel ID="UpdatePanel_List" runat="server">
        <ContentTemplate>
        <asp:Repeater ID="rptExamineItems" runat="server" OnItemDataBound="rptExamineItems_DataBound"
            OnItemCommand="rptExamineItems_ItemCommand">
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
                        class="table_none_WithoutWidth">
                        <asp:LinkButton ID="linkButtonEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("ExamItemID") %>' ToolTip="点击编辑" ><span style=" color: #1e5494; "><%#Eval("ItemName") %></span></asp:LinkButton>
                        <asp:ImageButton ID="imgBtAdd" runat="server" CommandName="AddItem" CommandArgument='<%# Eval("ExamItemID") %>'
                            ImageUrl="~/images/ICON/add.gif" ToolTip="增加子考核项" />
     <%--            <asp:ImageButton ID="imgBtEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("ExamItemID") %>'
                            ImageUrl="~/images/ICON/edit.gif" ToolTip="编辑考核项" />--%>
                        <span style='display: <%#Convert.ToInt32(Eval("Level"))!=1?"inline":"none"%>'>
                            <asp:ImageButton ID="imgBtDel" runat="server" ToolTip="删除考核项" CommandName="DeleteItem"
                                CommandArgument='<%# Eval("ExamItemID") %>' ImageUrl="~/images/ICON/delete.gif"
                                OnClientClick="javascript:return confirm('确定要删除此考核项吗?');" />
                        </span>
                    </td>
                    <td style="width: 9%; border-left-width: 0px; border-right-width: 0px;" class="table_none_WithoutWidth">
                    <span style='display:<%#(Convert.ToInt32(Eval("ExamineType"))==(int)FM2E.Model.Examine.ExamineType.DailyExamine)&&(Convert.ToInt32(Eval("Level"))==1)?"none":"inline"%>'>
                       <%#Convert.ToInt32(Eval("ExamineType"))==(int)FM2E.Model.Examine.ExamineType.SeasonExamine?"总分：":"占分：" %>
                        <%#Eval("Score")%><br /></span>
                        <span style='display:<%#(Convert.ToInt32(Eval("ChildCount"))==0)||(Convert.ToInt32(Eval("ExamineType"))==(int)FM2E.Model.Examine.ExamineType.DailyExamine)?"none":"inline" %>'>
                        <span style='color:red;display:<%#(Convert.ToInt32(Eval("ScoreOfChild"))-Convert.ToInt32(Eval("Score")))>0?"inline":"none" %>'>
                        已超出&nbsp;<%#(Convert.ToInt32(Eval("ScoreOfChild"))-Convert.ToInt32(Eval("Score")))%>分
                        </span>
                        <span style='color:orange;display:<%#(Convert.ToInt32(Eval("ScoreOfChild"))-Convert.ToInt32(Eval("Score")))<0?"inline":"none" %>'>
                        还剩下&nbsp;<%#(Convert.ToInt32(Eval("Score")) - Convert.ToInt32(Eval("ScoreOfChild")))%>分
                        </span>
                        </span>
                    </td>
                    <td class="table_none_WithoutWidth" style="border-left-width: 0px; border-right-width: 0px;">
                        <%#Convert.ToInt32(Eval("ChildCount")) == 0 ? "考核标准：" + (!string.IsNullOrEmpty((string)Eval("Content")) ? Eval("Content") : "暂无") : ""%>
                    </td>
                    <td class="table_none_WithoutWidth" style="border-left-width: 0px; border-right-width: 0px;">
                        <%#Convert.ToInt32(Eval("ChildCount")) == 0 ? "评分标准：" + (!string.IsNullOrEmpty((string)Eval("Standard")) ? Eval("Standard") : "暂无") : ""%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>   
        </ContentTemplate>   
     
        </asp:UpdatePanel>
            <cc2:ModalPopupExtender ID="popupAddExamItem" runat="server" TargetControlID="btShowModalPopup"
                            PopupControlID="Panel_AddExamItem" BackgroundCssClass="modalBackground" DynamicServicePath=""
                            Enabled="true" OkControlID="btOK" CancelControlID="btCancel">
                        </cc2:ModalPopupExtender>
                        <cc2:ModalPopupExtender ID="popupEditExamItem" runat="server" TargetControlID="btShowEditPopup"
                            PopupControlID="Panel_EditItem" BackgroundCssClass="modalBackground" DynamicServicePath=""
                            Enabled="true" OkControlID="btEditOK" CancelControlID="btEditCancel">
                        </cc2:ModalPopupExtender>
        <asp:Panel ID="Panel_AddExamItem" runat="server" Style="width: 95%; display: none"
            Height="420px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel_AddExamineItem" runat="server">
            <ContentTemplate>
            <div runat="server" id="divAdd">
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td class="Table_searchtitle" colspan="4">
                        添加子考核项
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        父考核项名称：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="lbParentItem" runat="server" Text="Label"></asp:Label>
                    </td>
                      <td class="table_body table_body_NoWidth">
                        父考核项所占分数：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="lbParentScore" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        子考核项名称：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="tbItemName" runat="server" MaxLength="50" title="请输入子考核项名称~A50:!"></asp:TextBox>
                        <span style="color:Red;">*</span>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        考核类型：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="lbExamType" runat="server" Text="Label"></asp:Label></td></tr><tr>
                <td class="table_body table_body_NoWidth">
                        所占分数：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbScore" runat="server" title="请输入所占分数~Afloat!"></asp:TextBox><span style="color:Red;">*</span>
                    </td>
                     <td class="table_body table_body_NoWidth">
                        阈值：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbThreshold" runat="server" Text="0" Width="20px"   title="请输入阈值~Afloat"></asp:TextBox>
                    （注：当考核项得分低于此值时，此考核项得0分）
                    </td></tr><tr>
                    <td class="table_body table_body_NoWidth">
                        考核标准：
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbContent" runat="server" MaxLength="200" Width="95%" title="请输入考核标准~A200:"></asp:TextBox></td></tr><tr>
                    <td class="table_body table_body_NoWidth">
                        评分标准：
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbStandard" runat="server" MaxLength="400" Width="80%" title="请输入评分标准~A400:"></asp:TextBox>
                        <asp:Button ID="btAdd" runat="server" Text="添加考核项" CssClass="button_bak2" OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'A');" OnClick="btAdd_Click"/>
                    </td>
                    </tr>
                    <tr><td style="text-align:left" colspan="4">
                        <asp:Label ID="lbErrorMsg" ForeColor="Red" runat="server" Text=""></asp:Label></td></tr>
                    </table>
                    <div style="overflow-y:auto; height:210px;">
                    <table width="99%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
            <tr><td class="Table_searchtitle">子考核项列表</td></tr></table><asp:Repeater ID="rptSubExamineItems" runat="server">
            <HeaderTemplate>
            <table width="99%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
            <tr>
            <td style="text-align:center">序号</td>
            <td style="text-align:center">考核项</td>
            <td style="text-align:center">考核类型</td>
            <td style="text-align:center">所占分数</td>
            <td style="text-align:center">阈值</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
            <tr>
            <td rowspan="2" style="text-align:center"><%#Container.ItemIndex+1 %></td><td><%#Eval("ItemName") %></td><td><%#EnumHelper.GetDescription((Enum)Eval("ExamineType")) %></td><td><%#Eval("Score") %></td><td><%#Eval("Threshold") %></td></tr><tr>
            <td colspan="4">
            考核标准：<%#Eval("Content") %><br />
            评分标准：<%#Eval("Standard") %>
            </td>
            </tr>
            </ItemTemplate>
            <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
            </div>
            <div runat="server" id="divEmpty" style="text-align:center; height:90px; vertical-align:middle;" visible="false">
            <span style="color :Red; font-weight:bold">查询考核项信息时失败</span>
            </div>
            </div>
            <center>
                <asp:Button ID="btOK" runat="server" style="display:none"/>
                <asp:Button ID="btCancel" runat="server" class="button_bak" Text="" style="display:none" />
                <asp:Button ID="btClose" runat="server" class="button_bak" OnClick="btClose_Click" Text="关闭" />
            </center>
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        
         <asp:Panel ID="Panel_EditItem" runat="server" Style="width: 95%; display: none"
            Height="250px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel_Edit" runat="server">
            <ContentTemplate>
            <div style="width:100%; height:400px;">
                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td class="Table_searchtitle" colspan="4">
                        修改考核项
                    </td>
                </tr>
               
                <tr>
                    <td class="table_body table_body_NoWidth">
                        子考核项名称：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="tbEditItemName" runat="server" MaxLength="50" title="请输入子考核项名称~B50:!"></asp:TextBox>
                        <span style="color:Red;">*</span>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        考核类型：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="lbEditExamType" runat="server" Text="Label"></asp:Label></td></tr><tr>
                <td class="table_body table_body_NoWidth">
                        所占分数：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEditScore" runat="server" title="请输入所占分数~Bfloat!"></asp:TextBox><span style="color:Red;">*</span>
                    </td>
                     <td class="table_body table_body_NoWidth">
                        阈值：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEditThreshold" runat="server" Text="0" Width="20px"  title="请输入阈值~Bfloat"></asp:TextBox>
                    （注：当考核项得分低于此值时，此考核项得0分）
                    </td></tr><tr>
                    <td class="table_body table_body_NoWidth">
                        考核标准：
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEditContent" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine" Rows="3" title="请输入考核标准~B200:"></asp:TextBox></td></tr><tr>
                    <td class="table_body table_body_NoWidth">
                        评分标准：
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEditStandard" runat="server" MaxLength="400" Width="95%" TextMode="MultiLine" Rows="3" title="请输入评分标准~B400:"></asp:TextBox>
                      
                    </td>
                    </tr>
                    <tr><td style="text-align:left" colspan="4">
                        <asp:Label ID="lbEditError" ForeColor="Red" runat="server" Text=""></asp:Label></td></tr>
                    </table>
                    <center>
                        <asp:Button ID="btEditOK" runat="server" Text="Button" style="display:none" />
                        <asp:Button ID="btEditCancel"
                            runat="server" Text="" style="display:none;" />
                            <asp:Button ID="btEdit" runat="server" Text="修改" OnClick="btEdit_Click" CssClass="button_bak" OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'B');"  />
<asp:Button ID="btEditClose" runat="server" Text="关闭" OnClick="btEditClose_Click" CssClass="button_bak" />
                    </center>
                    </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
        <asp:Button ID="btShowModalPopup" runat="server" style="display:none"/>
        <asp:Button ID="btShowEditPopup" runat="server" style="display:none"/>
    </div>
</asp:Content>
