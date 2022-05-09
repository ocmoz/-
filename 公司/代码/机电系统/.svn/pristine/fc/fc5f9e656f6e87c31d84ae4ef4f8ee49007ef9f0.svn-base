<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditCompany.aspx.cs" Inherits="Module_FM2E_BasicData_CompanyManage_EditCompany"
    Title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="公司信息维护"
        HeadOPTxt="目前操作功能：公司信息添加">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="取编辑消" ButtonPopedom="List" ButtonUrlType="Href"
            ButtonUrl="ViewCompany.aspx?cmd=view&id=" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
       
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                公司详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                公司编号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox1" runat="server" title="请输入公司编号~2:!"></asp:TextBox>
                                <span style="color:Red">*</span>
                            </td>
                        
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                公司名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox2" runat="server" title="请输入公司名称~20:!"></asp:TextBox>
                                <span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                公司地址：
                                
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:TextBox ID="TextBox3" runat="server" title="请输入公司地址~100:" Width="98%"></asp:TextBox>
                                <span style="color:Red"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                联系人：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox4" runat="server" title="请输入公司联系人~20:"></asp:TextBox>
                                <span style="color:Red"></span>
                            </td>
                       
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                联系电话：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox5" runat="server" title="请输入公司联系电话~20:"></asp:TextBox>
                                <span style="color:Red"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                公司网址：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox6" runat="server" title="请输入公司网址~50:"></asp:TextBox>
                            </td>
                        
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                电子邮件：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox7" runat="server" title="请输入电子邮件~30:email"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                传真：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox8" runat="server" title="请输入传真~20:"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                            是否总公司：
                        </td>
                        <td class="table_none table_none_NoWidth">
                            <asp:DropDownList ID="DropDownList3" runat="server" title="请选择是否总公司">
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList><span style="color:Red"></span>
                        </td>
                        </tr>
                        <tr>
                           <td class="table_body table_body_NoWidth">
                                备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:TextBox ID="TextBox9" title="请输入备注~1000:" runat="server" Rows="4" TextMode="MultiLine"
                                    Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                上传图片：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" onpropertychange="document.all('myimg').src=this.value;t1.style.display='block';queding.style.display='inline'" Height="20px" Width="250px" />
                                        <asp:Button ID="ButtonCancel" runat="server" Text="取消修改" OnClientClick="t1.style.display='none'" Visible="false" OnClick="ButtonCancel_Click"
                                             CssClass="button_bak"/>
                                        <div id="queding" style="display:none">
  
                                        <input id="sure" type="button" value="确定" onclick="t1.style.display='none';queding.style.display='none'"  class="button_bak"/>
                                        </div>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="单击查看原大小" Width="80px"
                                            Height="60px" />
                                        <asp:Button ID="shoebig" Text="修改图片" OnClick="ImageButton1_Click" runat="server" CssClass="button_bak" />
                                        <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ImageButton1"
                                            PopupControlID="Panel1" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr id="tr11" runat="server">
                            <td runat="server"></td>
                            <td id="Td1" runat="server">
                            <div id="t1" style="display:none">
                                <img src="" id="myimg"></div>
                            </td>
                        </tr>
                       
                    </table>
                    <center>
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </center>
    </div>
    <asp:Panel ID="Panel1" CssClass="popupControl" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ImageButton ID="ImageButton2" OnClientClick="javascript:return false;" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
