<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_ProducerManage_EditProducer, App_Web_xyozrjue" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="生产商信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="生产商列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="Producer.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript" ButtonPopedom="List" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                生产商详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                生产商名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox1" runat="server" title="请输入供应商名称~30:!" MaxLength="30"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                信用等级：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <cc2:Rating ID="LikeRating" runat="server" CurrentRating="3" MaxRating="5" StarCssClass="ratingStar"
                                            WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar"
                                            OnChanged="LikeRating_Changed" Style="float: left;">
                                        </cc2:Rating>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                地址：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox2" runat="server" title="请输入地址~50:!" MaxLength="50"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                电话：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox3" runat="server" title="请输入电话~20:noChinese!" MaxLength="20"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                传真：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox4" runat="server" title="请输入传真~20:noChinese" MaxLength="20"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                Email：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox5" runat="server" title="请输入Email~30:email" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                主页地址：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox6" runat="server" title="请输入主页~100:noChinese" MaxLength="100"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                负责人：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox7" runat="server" title="请输入负责人~20:!" MaxLength="20"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                负责人电话：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox8" runat="server" title="请输入负责人电话~20:noChinese!" MaxLength="20"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth">
                            </td>
                            <td class="table_none table_none_NoWidth">
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 75px">
                                产品：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 75px">
                                <asp:TextBox ID="TextBox9" runat="server" Height="52px" TextMode="MultiLine" Width="571px"
                                    title="请输入供应产品~500:"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 75px">
                                备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 75px">
                                <asp:TextBox ID="TextBox10" runat="server" Height="53px" TextMode="MultiLine" Width="569px"
                                    title="请输入备注~100:"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr runat="server">
                            <td align="right" style="height: 38px" runat="server">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
</asp:Content>
