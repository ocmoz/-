<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_WareHouseCheck_EditCheckForm, App_Web_ajp3ytvr" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="仓库检查表填写"
        HeadOPTxt="目前操作功能：填写仓库检查表">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="仓库检查表列表" ButtonPopedom="List"
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
                    <td colspan="6" style="font-family: 宋体; font-size: medium; text-align: center" class="table_body">
                        <b style="font-family: 宋体; font-size: medium">
                            <asp:Label ID="lbCompanyName" runat="server" Text="Label"></asp:Label>公路有限公司<br />
                            <asp:DropDownList ID="ddlWareHouse" runat="server" title="请选择仓库~!">
                            </asp:DropDownList><span style="color:Red">*</span>
                            仓库检查表</b>
                    </td>
                    <td style="font-family: 宋体; text-align: center" class="table_body">
                        <b style="font-size: medium">表单编号</b>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" class="table_body table_body_NoWidth">
                        检查日期
                    </td>
                    <td class="table_none table_none_NoWidth" colspan="4">
                        &nbsp;
                        <asp:TextBox ID="tbCheckDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入检查时间~date!"></asp:TextBox><span style="color:Red">*</span>
                    </td>
                    <td style="text-align: center" class="table_body table_body_NoWidth">
                        编号
                    </td>
                    <td class="table_none table_none_NoWidth">
                        &nbsp;
                        <asp:Label ID="lbFormNO" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="8" style="text-align: center" class="table_body">
                        检查情况
                    </td>
                    <td colspan="6" style="text-align: left" class="table_none table_none_NoWidth">
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 80px">
                                    材料类型：
                                </td>
                                <td>
<%--                                    <asp:CheckBoxList ID="materialType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">机电</asp:ListItem>
                                        <asp:ListItem Value="2">监控</asp:ListItem>
                                        <asp:ListItem Value="3">消防</asp:ListItem>
                                        <asp:ListItem Value="4">其它</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbMaterialType" value="1" title="请选择材料类型~min:1max:1" onclick="javascript:SelectChange('cbMaterialType',0,'<%= hdMaterialType.ClientID%>');" />机电
                                    <input type="checkbox" name="cbMaterialType" value="2" onclick="javascript:SelectChange('cbMaterialType',1,'<%= hdMaterialType.ClientID%>');"/>监控
                                    <input type="checkbox" name="cbMaterialType" value="3" onclick="javascript:SelectChange('cbMaterialType',2,'<%= hdMaterialType.ClientID%>');"/>消防
                                    <input type="checkbox" name="cbMaterialType" value="4" onclick="javascript:SelectChange('cbMaterialType',3,'<%= hdMaterialType.ClientID%>');"/>其它
                                    <input id="hdMaterialType" type="hidden" runat="server" /><span style="color:Red">*</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left" class="table_none table_none_NoWidth">
                        &nbsp;检查人员：
                        <asp:Label ID="lbChecker" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                        检查项目
                    </td>
                    <td style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                        抽查：
                    </td>
                    <td style="text-align: left" colspan="4">
                        &nbsp;<asp:TextBox ID="tbSpotCheck" runat="server" TextMode="MultiLine" Style="width: 95%;"
                            Rows="3" title="请输入抽查情况~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 317px; text-align: center" class="table_body_WithoutWidth">
                        盘点：
                    </td>
                    <td style="text-align: left" colspan="4">
                        &nbsp;<asp:TextBox ID="tbStockCount" runat="server" TextMode="MultiLine" Style="width: 95%;"
                            Rows="3" title="请输入盘点情况~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="7">
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 90px">
                                    数量情况：
                                </td>
                                <td align="left">
      <%--                              <asp:CheckBoxList ID="cblQuantitySituation" runat="server" RepeatDirection="Horizontal" title="请选择数量情况~!">
                                        <asp:ListItem Value="1">盘盈</asp:ListItem>
                                        <asp:ListItem Value="2">盘亏</asp:ListItem>
                                        <asp:ListItem Value="3">正常</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbQuantitySituation" value="1" title="请选择数量情况~min:1max:1" onclick="javascript:SelectChange('cbQuantitySituation',0, '<%=hdQuantitySituation.ClientID%>');" />盘盈
                                    <input type="checkbox" name="cbQuantitySituation" value="2" onclick="javascript:SelectChange('cbQuantitySituation',1,'<%=hdQuantitySituation.ClientID%>');"/>盘亏
                                    <input type="checkbox" name="cbQuantitySituation" value="3" onclick="javascript:SelectChange('cbQuantitySituation',2,'<%=hdQuantitySituation.ClientID%>');"/>正常
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
                                    质量情况：
                                </td>
                                <td align="left">
                      <%--              <asp:CheckBoxList ID="cblQuality" runat="server" RepeatDirection="Horizontal" title="请选择质量情况~!">
                                        <asp:ListItem Value="1">正常</asp:ListItem>
                                        <asp:ListItem Value="2">有损耗</asp:ListItem>
                                        <asp:ListItem Value="3">已损耗</asp:ListItem>
                                        <asp:ListItem Value="4">其它</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbQualitySituation" value="1" title="请选择质量情况~min:1max:1" onclick="javascript:SelectChange('cbQualitySituation',0,'<%= hdQualitySituation.ClientID%>');" />正常
                                    <input type="checkbox" name="cbQualitySituation" value="2" onclick="javascript:SelectChange('cbQualitySituation',1,'<%= hdQualitySituation.ClientID%>');"/>有损耗
                                    <input type="checkbox" name="cbQualitySituation" value="3" onclick="javascript:SelectChange('cbQualitySituation',2,'<%= hdQualitySituation.ClientID%>');"/>已损耗
                                    <input type="checkbox" name="cbQualitySituation" value="4" onclick="javascript:SelectChange('cbQualitySituation',3,'<%= hdQualitySituation.ClientID%>');"/>其它
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
                                    表单登记情况：
                                </td>
                                <td align="left">
<%--                                    <asp:CheckBoxList ID="cblRegSituation" runat="server" RepeatDirection="Horizontal" title="请选择表单登记情况~!" onclick="javascript:SelectChange(this);">
                                        <asp:ListItem Value="1">规范</asp:ListItem>
                                        <asp:ListItem Value="2">不规范</asp:ListItem>
                                        <asp:ListItem Value="3">需改进</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                    <input type="checkbox" name="cbRegSituation" value="1" title="请选择表单登记情况~min:1max:1" onclick="javascript:SelectChange('cbRegSituation',0,'<%= hdRegSituation.ClientID%>');" />规范
                                    <input type="checkbox" name="cbRegSituation" value="2" onclick="javascript:SelectChange('cbRegSituation',1,'<%= hdRegSituation.ClientID%>');"/>不规范
                                    <input type="checkbox" name="cbRegSituation" value="3" onclick="javascript:SelectChange('cbRegSituation',2,'<%= hdRegSituation.ClientID%>');"/>需改进
                                    <input id="hdRegSituation" type="hidden" runat="server" /><span style="color:Red">*</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 317px; text-align: center" class="table_body_WithoutWidth" colspan="4">
                        有无异常情况
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="tbExceptionSituation" runat="server" TextMode="MultiLine" Style="width: 95%;"
                            Rows="3" title="请输入有无异常情况~50:"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr>
                    <td align="right" style="height: 38px">
                        <asp:Button ID="btSave" runat="server" CssClass="button_bak" Text="保存草稿" OnClick="btSave_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="提交" OnClick="btSubmit_Click" />&nbsp;&nbsp;
                        <input id="Reset1" class="button_bak" type="reset" value="重填" />
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
