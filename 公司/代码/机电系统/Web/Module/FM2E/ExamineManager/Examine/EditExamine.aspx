<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageNoCheck.master" AutoEventWireup="true"
    CodeFile="EditExamine.aspx.cs" Inherits="Module_FM2E_ExamineManager_Examine_EditExamine" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
  <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="考核表"
        HeadOPTxt="目前操作功能：" HeadHelpTxt="帮助">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="考核表列表" ButtonUrlType="Href"
            ButtonUrl="ExamineList.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%">
        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
            style="border-collapse: collapse;">
            <tr><td class="Table_searchtitle" colspan="4"><%=title%>考核表</td></tr>
            <tr>
            <td class="table_body table_body_NoWidth">考核表编号：</td>
            <td class="table_none table_none_NoWidth">
                <asp:Label ID="lbSheetNO" runat="server" Text="_____________________"></asp:Label></td>
            <td class="table_body table_body_NoWidth">考核表名称：</td>
            <td class="table_none table_none_NoWidth">
                <asp:TextBox ID="tbSheetName" runat="server" MaxLength="50" title="请输入考核表名称~C50:!"></asp:TextBox>
                <span style="color:Red">*</span>
            </td>
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
                <asp:DropDownList ID="ddlExamineTarget" runat="server">
                </asp:DropDownList>
            </td>
            <td class="table_body table_body_NoWidth">考核时间：</td>
            <td class="table_none table_none_NoWidth">
                <asp:TextBox ID="tbSaveTime" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入考核时间~Cdate!"></asp:TextBox></td>
            </tr>
        </table>
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
                        class="table_none_WithoutWidth" rowspan='<%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?2:1 %>'>
                        <span style='display: <%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none"%>'>
                        <asp:LinkButton ID="linkButtonEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("ExamItemID") %>' ToolTip="点击编辑" ><span style=" color: #1e5494; "><%#Eval("NO")%><%#Eval("ItemName") %></span></asp:LinkButton>
                        </span>
                        <span style='display: <%#Convert.ToInt32(Eval("ChildCount"))!=0||Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none"%>'>
                        <span style=" color: #1e5494; "><%#Eval("NO")%><%#Eval("ItemName") %></span>
                        </span>
                         <span style='display: <%#Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none"%>'>
                        <asp:ImageButton ID="imgBtAdd" runat="server" CommandName="AddItem" CommandArgument='<%# Eval("ExamItemID") %>'
                            ImageUrl="~/images/ICON/add.gif" ToolTip="输入考核结果" />
                         </span>
                        <span style='display: <%#Convert.ToInt32(Eval("ChildCount"))==0&&!Convert.ToBoolean(Eval("CanAddChild"))?"inline":"none"%>'>
                            <asp:ImageButton ID="imgBtDel" runat="server" ToolTip="删除考核结果" CommandName="DeleteItem"
                                CommandArgument='<%# Eval("ExamItemID") %>' ImageUrl="~/images/ICON/delete.gif"
                                OnClientClick="javascript:return confirm('确定要删除此考核结果吗?');" />
                        </span>
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
                        添加扣分项
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
                <td class="table_body table_body_NoWidth">考核标准：</td>
                <td colspan="3" class="table_none table_none_NoWidth"><asp:Label ID="lbContent" runat="server" Text="Label"></asp:Label></td>
                </tr>
                 <tr>
                <td class="table_body table_body_NoWidth">评分标准：</td>
                <td colspan="3" class="table_none table_none_NoWidth"><asp:Label ID="lbStandard" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        考核项名称：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="tbItemName" runat="server" MaxLength="50" title="请输入考核项名称~A50:!"></asp:TextBox>
                        <span style="color:Red;">*</span>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        扣分：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="tbDeduct" runat="server" MaxLength="50" title="请输入扣分~Afloat!"></asp:TextBox>
                        <span style="color:Red;">*</span>
                        </td></tr><tr>
                <td class="table_body table_body_NoWidth">
                        扣分原因：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbDeductReason" runat="server" MaxLength="30" Width="85%" title="请输入扣分原因~A30:!"></asp:TextBox><span style="color:Red;">*</span>
                    </td>
                     <td class="table_body table_body_NoWidth">
                        考核时间：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbExamineDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入考核时间~date"></asp:TextBox><span style="color:Red;">*</span>
                    </td></tr><tr>
                    <td class="table_body table_body_NoWidth">
                        备注：
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbRemark" runat="server" MaxLength="200" Width="80%" title="请输入备注~A50:"></asp:TextBox> <asp:Button ID="btAdd" runat="server" Text="添加扣分项" CssClass="button_bak2" OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'A');" OnClick="btAdd_Click"/></td></tr>
                       

                    <tr><td style="text-align:left" colspan="4">
                        <asp:Label ID="lbErrorMsg" ForeColor="Red" runat="server" Text=""></asp:Label></td></tr>
                    </table>
                    <div style="overflow-y:auto; height:180px;">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
            <tr><td class="Table_searchtitle">考核扣分列表</td></tr></table><asp:Repeater ID="rptSubExamineItems" runat="server">
            <HeaderTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
            <tr>
            <td style="text-align:center">序号</td>
            <td style="text-align:center">考核项</td>
            <td style="text-align:center">扣分</td>
            <td style="text-align:center">考核人</td>
            <td style="text-align:center">考核日期</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
            <tr>
            <td rowspan="2" style="text-align:center"><%#Container.ItemIndex+1 %></td>
            <td><%#Eval("ItemName") %></td>
            <td><%#Eval("Deduct","{0:0.##}") %></td>
            <td><%#Eval("ExaminerName") %></td>
            <td><%#Eval("ExamineDate","{0:yyyy-MM-dd}") %></td></tr><tr>
            <td colspan="4">
            扣分原因：<%#Eval("DeductReason") %><br />
            备注：<%#Eval("Remark") %>
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
                        修改扣分项
                    </td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        父考核项名称：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="lbEditParentItem" runat="server" Text="Label"></asp:Label>
                    </td>
                      <td class="table_body table_body_NoWidth">
                        父考核项所占分数：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:Label ID="lbEditParentScore" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td class="table_body table_body_NoWidth">考核标准：</td>
                <td colspan="3" class="table_none table_none_NoWidth"><asp:Label ID="lbEditContent" runat="server" Text="Label"></asp:Label></td>
                </tr>
                 <tr>
                <td class="table_body table_body_NoWidth">评分标准：</td>
                <td colspan="3" class="table_none table_none_NoWidth"><asp:Label ID="lbEditStandard" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td class="table_body table_body_NoWidth">
                        考核项名称：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="tbEditItemName" runat="server" MaxLength="50" title="请输入考核项名称~B50:!"></asp:TextBox>
                        <span style="color:Red;">*</span>
                    </td>
                    <td class="table_body table_body_NoWidth">
                        扣分：
                    </td>
                    <td class="table_none table_none_NoWidth">
                        <asp:TextBox ID="tbEditDeduct" runat="server" MaxLength="50" title="请输入扣分~Bfloat!"></asp:TextBox>
                        <span style="color:Red;">*</span>
                        </td></tr><tr>
                <td class="table_body table_body_NoWidth">
                        扣分原因：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEditDeductReason" runat="server" MaxLength="30" Width="85%" title="请输入扣分原因~B30:!"></asp:TextBox><span style="color:Red;">*</span>
                    </td>
                     <td class="table_body table_body_NoWidth">
                        考核时间：
                    </td>
                    <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEditExamineDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入考核时间~Bdate"></asp:TextBox><span style="color:Red;">*</span>
                    </td></tr><tr>
                    <td class="table_body table_body_NoWidth">
                        备注：
                    </td>
                    <td colspan="3" class="table_none table_none_NoWidth">
                    <asp:TextBox ID="tbEditRemark" runat="server" MaxLength="200" Width="80%" title="请输入备注~B50:"></asp:TextBox> </td></tr>
                       

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
        <center>
                    <asp:UpdatePanel ID="UpdatePanel_Button" runat="server">
            <ContentTemplate>
            <asp:Button ID="btSaveDraft" runat="server" Text="保存草稿" OnClick="btSaveDraft_Click" CssClass="button_bak" OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'C');" />
            <asp:Button ID="btCommit" runat="server" Text="提交" OnClick="btCommit_Click" CssClass="button_bak" OnClientClick="javascript:return checkGroupForm(document.forms[0],true,'C');" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
