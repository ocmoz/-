<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_WareHouseCheck_EditCheckForm, App_Web_ajp3ytvr" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="�ֿ������д"
        HeadOPTxt="Ŀǰ�������ܣ���д�ֿ����">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="�ֿ�����б�" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="WareHouseCheck.aspx" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel1" runat="server" Style="display: none; width: 300px">
        <asp:Image ID="Image1" runat="server" Width="300px" />
    </asp:Panel>
    <div style="width: 900px; ">
        <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                style="border-collapse: collapse;">
                <tr>
                    <td colspan="6" style="font-family: ����; font-size: medium; text-align: center" class="table_body">
                        <b style="font-family: ����; font-size: medium">
                            <asp:Label ID="lbCompanyName" runat="server" Text="Label"></asp:Label>��·���޹�˾<br />
                            <asp:DropDownList ID="ddlWareHouse" runat="server" title="��ѡ��ֿ�~!">
                            </asp:DropDownList><span style="color:Red">*</span>
                            �ֿ����</b>
                    </td>
                    <td style="font-family: ����; text-align: center" class="table_body">
                        <b style="font-size: medium">�����</b>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" class="table_body table_body_NoWidth">
                        �������
                    </td>
                    <td class="table_none table_none_NoWidth" colspan="4">
                        &nbsp;
                        <asp:TextBox ID="tbCheckDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="��������ʱ��~date!"></asp:TextBox><span style="color:Red">*</span>
                    </td>
                    <td style="text-align: center" class="table_body table_body_NoWidth">
                        ���
                    </td>
                    <td class="table_none table_none_NoWidth">
                        &nbsp;
                        <asp:Label ID="lbFormNO" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="8" style="text-align: center" class="table_body">
                        ������
                    </td>
                    <td colspan="6" style="text-align: left" class="table_none table_none_NoWidth">
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 80px">
                                    �������ͣ�
                                </td>
                                <td>
<%--                                    <asp:CheckBoxList ID="materialType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">����</asp:ListItem>
                                        <asp:ListItem Value="2">���</asp:ListItem>
                                        <asp:ListItem Value="3">����</asp:ListItem>
                                        <asp:ListItem Value="4">����</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbMaterialType" value="1" title="��ѡ���������~min:1max:1" onclick="javascript:SelectChange('cbMaterialType',0,'<%= hdMaterialType.ClientID%>');" />����
                                    <input type="checkbox" name="cbMaterialType" value="2" onclick="javascript:SelectChange('cbMaterialType',1,'<%= hdMaterialType.ClientID%>');"/>���
                                    <input type="checkbox" name="cbMaterialType" value="3" onclick="javascript:SelectChange('cbMaterialType',2,'<%= hdMaterialType.ClientID%>');"/>����
                                    <input type="checkbox" name="cbMaterialType" value="4" onclick="javascript:SelectChange('cbMaterialType',3,'<%= hdMaterialType.ClientID%>');"/>����
                                    <input id="hdMaterialType" type="hidden" runat="server" /><span style="color:Red">*</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left" class="table_none table_none_NoWidth">
                        &nbsp;�����Ա��
                        <asp:Label ID="lbChecker" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                        �����Ŀ
                    </td>
                    <td style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                        ��飺
                    </td>
                    <td style="text-align: left" colspan="4">
                        &nbsp;<asp:TextBox ID="tbSpotCheck" runat="server" TextMode="MultiLine" Style="width: 95%;"
                            Rows="3" title="�����������~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                        �̵㣺
                    </td>
                    <td style="text-align: left" colspan="4">
                        &nbsp;<asp:TextBox ID="tbStockCount" runat="server" TextMode="MultiLine" Style="width: 95%;"
                            Rows="3" title="�������̵����~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="7">
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 90px">
                                    ���������
                                </td>
                                <td align="left">
      <%--                              <asp:CheckBoxList ID="cblQuantitySituation" runat="server" RepeatDirection="Horizontal" title="��ѡ���������~!">
                                        <asp:ListItem Value="1">��ӯ</asp:ListItem>
                                        <asp:ListItem Value="2">�̿�</asp:ListItem>
                                        <asp:ListItem Value="3">����</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbQuantitySituation" value="1" title="��ѡ���������~min:1max:1" onclick="javascript:SelectChange('cbQuantitySituation',0, '<%=hdQuantitySituation.ClientID%>');" />��ӯ
                                    <input type="checkbox" name="cbQuantitySituation" value="2" onclick="javascript:SelectChange('cbQuantitySituation',1,'<%=hdQuantitySituation.ClientID%>');"/>�̿�
                                    <input type="checkbox" name="cbQuantitySituation" value="3" onclick="javascript:SelectChange('cbQuantitySituation',2,'<%=hdQuantitySituation.ClientID%>');"/>����
                                    <input id="hdQuantitySituation" type="hidden" runat="server" /><span style="color:Red">*</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="7">
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 90px">
                                    ���������
                                </td>
                                <td align="left">
                      <%--              <asp:CheckBoxList ID="cblQuality" runat="server" RepeatDirection="Horizontal" title="��ѡ���������~!">
                                        <asp:ListItem Value="1">����</asp:ListItem>
                                        <asp:ListItem Value="2">�����</asp:ListItem>
                                        <asp:ListItem Value="3">�����</asp:ListItem>
                                        <asp:ListItem Value="4">����</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbQualitySituation" value="1" title="��ѡ���������~min:1max:1" onclick="javascript:SelectChange('cbQualitySituation',0,'<%= hdQualitySituation.ClientID%>');" />����
                                    <input type="checkbox" name="cbQualitySituation" value="2" onclick="javascript:SelectChange('cbQualitySituation',1,'<%= hdQualitySituation.ClientID%>');"/>�����
                                    <input type="checkbox" name="cbQualitySituation" value="3" onclick="javascript:SelectChange('cbQualitySituation',2,'<%= hdQualitySituation.ClientID%>');"/>�����
                                    <input type="checkbox" name="cbQualitySituation" value="4" onclick="javascript:SelectChange('cbQualitySituation',3,'<%= hdQualitySituation.ClientID%>');"/>����
                                     <input id="hdQualitySituation" type="hidden" runat="server" /><span style="color:Red">*</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="7">
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 90px">
                                    ���Ǽ������
                                </td>
                                <td align="left">
<%--                                    <asp:CheckBoxList ID="cblRegSituation" runat="server" RepeatDirection="Horizontal" title="��ѡ����Ǽ����~!" onclick="javascript:SelectChange(this);">
                                        <asp:ListItem Value="1">�淶</asp:ListItem>
                                        <asp:ListItem Value="2">���淶</asp:ListItem>
                                        <asp:ListItem Value="3">��Ľ�</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbRegSituation" value="1" title="��ѡ����Ǽ����~min:1max:1" onclick="javascript:SelectChange('cbRegSituation',0,'<%= hdRegSituation.ClientID%>');" />�淶
                                    <input type="checkbox" name="cbRegSituation" value="2" onclick="javascript:SelectChange('cbRegSituation',1,'<%= hdRegSituation.ClientID%>');"/>���淶
                                    <input type="checkbox" name="cbRegSituation" value="3" onclick="javascript:SelectChange('cbRegSituation',2,'<%= hdRegSituation.ClientID%>');"/>��Ľ�
                                    <input id="hdRegSituation" type="hidden" runat="server" /><span style="color:Red">*</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 317px; text-align: center" class="table_body_WithoutWidth" colspan="4">
                        �����쳣���
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="tbExceptionSituation" runat="server" TextMode="MultiLine" Style="width: 95%;"
                            Rows="3" title="�����������쳣���~50:"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr>
                    <td align="right" style="height: 38px">
                        <asp:Button ID="btSave" runat="server" CssClass="button_bak" Text="����ݸ�" OnClick="btSave_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="�ύ" OnClick="btSubmit_Click" />&nbsp;&nbsp;
                        <input id="Reset1" class="button_bak" type="reset" value="����" />
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript" language="javascript">
            function SelectChange(name,index,targetName) {
                var inputs = document.getElementsByName(name);
                var target = $get(targetName);
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type == "checkbox") {
                        if (i != index)
                            inputs[i].checked = false;
                        else {
                            inputs[i].checked = true;
                            if (target != "undefined")
                                target.value = inputs[i].value;
                        }
                    }
                }
            }
            function InitCheckBoxList() {
                SelectCheckBox("cbMaterialType", '<%=hdMaterialType.ClientID %>');
                SelectCheckBox("cbQuantitySituation", '<%=hdQuantitySituation.ClientID %>');
                SelectCheckBox("cbQualitySituation", '<%=hdQualitySituation.ClientID %>');
                SelectCheckBox("cbRegSituation", '<%=hdRegSituation.ClientID %>');
            }

            function SelectCheckBox(checkBoxName,targetName) {
                var inputs = document.getElementsByName(checkBoxName);
                var target = $get(targetName);
                if(target!="undefined")
                {
                    for (var i = 0; i < inputs.length; i++) {
                        if (inputs[i].type == "checkbox") {
                            if (inputs[i].value == target.value)
                                inputs[i].checked = true;
                        }
                    }
                }
            }
        </script>
        
        <%if (cmd == "edit")
          { %>
          <script type="text/javascript" language="javascript">
              InitCheckBoxList();
          </script>
        <%} %> 
</asp:Content>
