<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_InsureManager_InsureInfoManager_InsuranceManager, App_Web_nlqf5cyp" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/control/WorkFlowUserSelectControl.ascx" TagName="WorkFlowUserSelectControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        var fileId = 1;
        function addFile() {
            var FilesDiv = document.getElementById('FilesDiv');
            var divId = "div" + fileId;
            var str = '<div id="' + divId + '">';
            str += '<input type="file" size="40" name="File" id="file'+fileId+'" style="border: solid 1px #0077B2">'
            str += '&nbsp;<img src="../../../../images/ICON/delete.gif" onclick="delFile(\'' + divId + '\')"/>';
            str += "</div>";
            FilesDiv.insertAdjacentHTML("beforeEnd", str)
            fileId++;
        }

        function delFile(child) {
            var FilesDiv = document.getElementById('FilesDiv');
            var elem = document.getElementById(child);
            FilesDiv.removeChild(elem);
        }
        function delFile1(child) {
            var FilesDiv = document.getElementById('FilesDiv1');
            var elem = document.getElementById(child);
            FilesDiv.removeChild(elem);
        }
        var ChildId = 1;
        function addFile1() {
            var FilesDiv1 = document.getElementById('FilesDiv1');

            var divId = "Childdiv" + ChildId;
            var str = '<div id="' + divId + '">';
            str += '<input type="file" size="40" id="child'+ChildId+'" name="File" style="border: solid 1px #0077B2">'
            str += '&nbsp;<img src="../../../../images/ICON/delete.gif" onclick="delFile1(\'' + divId + '\')"/>';
            str += "</div>";
            FilesDiv1.insertAdjacentHTML("beforeEnd", str)
            fileId++;
        }
        function Sub() {
            var len = document.getElementById('FilesDiv').getElementsByTagName('input');
            if (len.length != 0) {
                for (var i = 0; i < len.length; i++) {
                    if (len[i].type == "file") {
                        if (len[i].value == "") {
                            alert('��ѡ��Ҫ�ϴ��ĸ�����');
                            return false;
                        }
                    }
                }
            }
            else {
                alert('��ѡ��Ҫ�ϴ��ĸ�����');
                return false;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="������Ϣ����"
        HeadHelpTxt="����" HeadOPTxt="Ŀǰ�������ܣ�������Ϣ����">
        <cc1:HeadMenuButtonItem ButtonName="������Ϣ����" ButtonIcon="list.gif" ButtonUrlType="Href"
            ButtonUrl="InsuranceReportlist.aspx" />
             <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="����" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px;">
        <div style="width: 880px; text-align: left; vertical-align: top; padding: 0px 0px 0px 0px;">
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td colspan="4" class="table_body_WithoutWidth">
                        <b style="font-family: ����; font-size: medium">������Ϣ���ٱ�</b>
                    </td>
                </tr>
                <tr>
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <asp:Label ID="lb_insuranceNo" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_insuranceNo" runat="server" Width="90%" title="�����뱣����~30:!"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <asp:Label ID="lb_reportNo" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_reportNo" Width="90%" runat="server" title="�����뱨������~50:!"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ���ջ�ִ�ţ�
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <asp:Label ID="lb_ReceiptNo" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_ReceiptNo" Width="90%" runat="server" title="�����뱣�ջ�ִ��~50:!"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ��������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lb_riskType" runat="server" Text="Label"></asp:Label>
                                    <asp:RadioButtonList ID="rb_riskType" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem Value="1" Text="����"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="�׻�"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="��ٻ����"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="�����ҳ�"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="����"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <input type="text" id="tb_riskTypeName" style="float: left" runat="server" />
                                </td>
                            </tr>
                        </table>                    
                    </td>
                </tr>
                <tr>
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ��������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lb_riskDate" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_riskDate" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"
                            runat="server" title="�����뱣������~50:!"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ��������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lb_reportDate" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_reportDate" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"
                            runat="server" title="�����뱣������~50:!"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>                   
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ������
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lb_Estimate" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_Estimate" runat="server"></asp:TextBox>
                    </td>
                     <td class="table_body_WithoutWidth" style="height: 30px">
                        �����
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px">
                        <asp:Label ID="lb_Claim" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_Claim" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>                   
                    <td class="table_body_WithoutWidth" style="height: 30px">
                        ���յ�ַ
                    </td>
                    <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                        <asp:Label ID="lb_Address" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="tb_Address" runat="server" title="�������豸��ַ~50:!" Width="90%"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="table_body_WithoutWidth" rowspan="2">
                        ��������
                    </td>
                    <td class="table_none_WithoutWidth" colspan="3">
                        <asp:Label ID="lb_riskContent" runat="server" ></asp:Label>
                        <asp:TextBox ID="tb_riskContent" runat="server" TextMode="MultiLine" MaxLength="200"
                            Width="95%" Rows="4" title="�������������~200:!"></asp:TextBox><span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    <div id="add1" runat="server">
                        <div id="FilesDiv">
                            <div id="div0">
                                <input type="file" runat="server" size="40" name="File" id="file0" style="border: solid 1px #0077B2" />
                                <img src="../../../../images/ICON/delete.gif" onclick="delFile('div0')" />
                            </div>
                        </div>
                        <input type="button" value="���Ӹ���" onclick="addFile();return false" id="btnInput"
                            runat="server" />
                            </div>
                        <div id="downFileDiv" runat="server">
                            <asp:GridView ID="gridviewFile" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                                DataKeyNames="Editreason1id" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowCommand="GridView1_OnRowCommand" 
                                OnRowDataBound="GridView1_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderText="���" ShowHeader="False">
                                        <ItemTemplate>
                                            <%# Eval("Editreason1id") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="ArticleName" runat="server" ForeColor="Blue" Font-Underline="true"
                                                Text='<%# Eval("Name")%>' NavigateUrl='<%# "~/public/InsuranceReport/"+ Eval("Address")%>'> </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="70%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ɾ��" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandArgument='<%# Eval("Editreason1id") %>' CommandName="del1" ImageUrl="~/images/ICON/delete.gif"
                                                Text="ɾ��" OnClientClick="javascript:return confirm('ȷ��ɾ�����');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    ��ǰ�޸���
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
                    
            <asp:Table Width="100%" ID="table_repair" CellPadding="0" CellSpacing="0" border="1"
                BorderColor="
                #cccccc" Style="border-collapse: collapse;" runat="server">
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell class="table_body_WithoutWidth" RowSpan="3" Style="width: 118px" runat="server">
                            �����޸����
                    </asp:TableCell>
                    <asp:TableCell class="table_none_WithoutWidth" colspan="3">
                          <asp:Label ID="lb_repairContent" runat="server" ></asp:Label>
                        <asp:TextBox ID="tb_repairContent" runat="server" TextMode="MultiLine" MaxLength="200"
                            Width="95%" Rows="4" title="�������������~200:!"></asp:TextBox><span style="color: Red">*</span><br></br>
                        <label id="lb_StationManager" runat="server">
                        </label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell colspan="3">   
                     <div id="add2" runat="server">                   
                        <div id="FilesDiv1">
                            <div id="Childdiv0">
                                <input type="file" runat="server" size="40" name="File" id="child0" style="border: solid 1px #0077B2" />
                                <img src="../../../../images/ICON/delete.gif" onclick="delFile('Childdiv0')" />
                            </div>
                        </div>
                        <input type="button" value="���Ӹ���" onclick="addFile1();return false" id="Button2"
                            runat="server" />
                            </div>
                        <div id="Div3" runat="server">
                            <asp:GridView ID="gridview_repairAttachment" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                                DataKeyNames="Editreason1id" HeaderStyle-Height="25px" RowStyle-Height="20px"
                                Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowCommand="GridView2_OnRowCommand" OnRowDataBound="GridView2_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="���" ShowHeader="False">
                                        <ItemTemplate>
                                            <%# Eval("Editreason1id") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="ArticleName" runat="server" ForeColor="Blue" Font-Underline="true"
                                                Text='<%# Eval("Name")%>' NavigateUrl='<%# "~/public/InsuranceReport/"+ Eval("Address")%>'> </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="70%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ɾ��" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False"
                                                CommandArgument='<%# Eval("Editreason1id") %>' CommandName="del" ImageUrl="~/images/ICON/delete.gif"
                                                Text="ɾ��" OnClientClick="javascript:return confirm('ȷ��ɾ�����');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    ��ǰ�޸���
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>                  
                    </asp:TableCell>
                </asp:TableRow>               
            </asp:Table>           
            <asp:Table ID="tableRow_review" border="0" cellspacing="0" cellpadding="0" width="100%" BorderColor="#cccccc" style="border-collapse: collapse;" runat="server">
            	 <asp:TableRow runat="server" ID="TableRow3">
                    <asp:TableCell class="table_body_WithoutWidth" Style="width: 118px">
                            ���ϸ������
                    </asp:TableCell>
                    <asp:TableCell class="table_none_WithoutWidth" colspan="3">
                        <asp:Label ID="lb_reviewContent" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_reviewContent" runat="server" TextMode="MultiLine" MaxLength="200"
                            Width="95%" Rows="4" title="�������������~200:!"></asp:TextBox><span style="color: Red">*</span><br></br>
                        <label id="lb_insutanceManager" runat="server" />
                    </asp:TableCell></asp:TableRow></asp:Table><table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                runat="server">
                <tr runat="server">
                    <td style="height: 38px; text-align: center" runat="server">
                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="ȷ��" OnClick="Button1_Click" />&nbsp;&nbsp; <input id="Reset1" class="button_bak" type="reset" value="����" /> </td></tr></table></div></div><script language="javascript" type="text/javascript">

        function selectAll(obj, id) {
            var rid = id.replace(".", "+");
            var inputs = document.getElementsByName("Page_" + rid);

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.value == "ȫѡ")
                        inputs[i].checked = true;
                    else inputs[i].checked = false;
                }
            }
            if (obj.value == "ȫѡ")
                obj.value = "��ѡ";
            else obj.value = "ȫѡ";
        }
    </script></asp:Content>