<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_SectionManage_EditSection, App_Web_dkdufjdh" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="·����Ϣά��"
        HeadOPTxt="Ŀǰ�������ܣ�·����Ϣά��">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="·���б�" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="Section.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="JavaScript" ButtonPopedom="List" 
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
        <asp:Panel ID="Panel1" runat="server" Style="display: none; width: 300px">
        <asp:Image ID="Image1" runat="server" Width="300px" />
    </asp:Panel>
    <div style="width: 900px; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                ·����ϸ��Ϣ
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ·�α�ţ�
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox1" runat="server" title="������·�α��~2:!" MaxLength="2"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px; width: 15%;">
                                ·�����ƣ�
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox2" runat="server" title="������·������~20:!" MaxLength="20"></asp:TextBox><span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ·��������˾��
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="DropDownList1" runat="server" title="��ѡ��������˾~!">
                                </asp:DropDownList><span style="color:Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                                ·�γ��ȣ�
                            </td>
                            <td class="table_none table_none_NoWidth" 
                                style="height: 30px; vertical-align:middle" valign="middle">
                                <asp:TextBox ID="TextBox4" runat="server" title="������·�γ���~float!"></asp:TextBox>ǧ��<span style="color:Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                ·������ʱ�䣺
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TextBox5" runat="server" title="������·������ʱ��~date" class="input_calender"
                                    onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                                ·��ͼƬ��
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                    
                                        <asp:FileUpload ID="FileUpload1" runat="server" style="height: 25px; width: 100px" onpropertychange="show(this)" />
                                        
                                            <div id="t2" style="display:none">
                                        <asp:Button ID="Button2" runat="server" Text="ȡ��" onclick="Button2_Click" Visible="true" OnClientClick="t1.style.display='none'" /></div>
                                        <div id="t3" style="display:none">
                                        <input id="Button3" type="button" value="ȷ��" onclick ="t1.style.display='none';t2.style.display='none';t3.style.display='none';t4.style.display='none'" /></div>
                                        <div id="t4" style="display:inline"><asp:ImageButton ID="ImageButton1" runat="server" ToolTip="������ʾ��ͼ" Width="40px" Height="30px" /></div>
                                        <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ImageButton1"
                                            PopupControlID="Panel1" Enabled="True" DynamicServicePath="" ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr id="tr11" runat="server">
                            <td id="Td1" colspan="4" runat="server">
                            <div id="t1" style="display:none">
                                <img src="" id="myimg" height="200px"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 78px">
                                ��ע��
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3" style="height: 78px">
                                <textarea id="TextArea1" style="width: 723px; height: 67px" runat="server" title="�����뱸ע~100:"></textarea>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr runat="server">
                            <td align="right" style="height: 38px" runat="server">
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="ȷ��" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="����" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
<script type="text/javascript" language="javascript">
    function show(obj)
       {
            if(obj.value!='')
            {
               document.all('myimg').src=obj.value;
               t1.style.display='block';
               t2.style.display='inline';
               t3.style.display='inline'
            }
       }
    </script>
</asp:Content>
