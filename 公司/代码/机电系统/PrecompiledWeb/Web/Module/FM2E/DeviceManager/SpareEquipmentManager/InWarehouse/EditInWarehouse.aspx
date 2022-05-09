<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_InWarehouse_EditInWarehouse, App_Web_7i-4rppn" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备入库信息维护"
        HeadOPTxt="目前操作功能：入库维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="入库列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="InWarehouse.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%; ">
                <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="添加入库" ID="TabPanel1">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr>
                                    <td class="Table_searchtitle" colspan="2">
                                        入库详细信息
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth" style="width: 10%">
                                        入库部门：
                                    </td>
                                    <td class="table_none table_none_NoWidth" style="width: 90%">
                                        <asp:DropDownList ID="DropDownList_Department" runat="server" title="请选择入库部门~!">
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        入库明细：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                                style="border-collapse: collapse;">
                                                <tr>
                                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                                        是否固定资产：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                    </td>
                                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                                        设备条形码：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                                        <asp:TextBox ID="TBEquipment" runat="server" title="请输入设备条形码~20:" AutoPostBack="True"
                                                            OnTextChanged="TBEquipment_TextChanged"></asp:TextBox><span style="color: Red">*</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                                        产品名称：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                                        
                                                        <asp:TextBox ID="TBProduct" runat="server" title="请输入产品名称~20:"></asp:TextBox>
                                                        <cc2:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TBProduct"
                                                         ServicePath="ProductModelServiceforIn.asmx" ServiceMethod="GetProductNameList"
                                                          MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" Enabled="True"
                                                         >
                                                        </cc2:AutoCompleteExtender>
                                                        <span style="color: Red">*</span>
                                                    </td>
                                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                                        型号：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    
                                                    
                                                        <asp:TextBox ID="TBModel" runat="server" title="请输入型号~20:"></asp:TextBox>
                                                         <cc2:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TBModel"
                                                         ServicePath="ProductModelServiceforIn.asmx" ServiceMethod="GetProductModelList"
                                                          MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" Enabled="True"
                                                         >
                                                        </cc2:AutoCompleteExtender>
                                                        <span style="color: Red">*</span>
                                                       <asp:Button ID="Button_UpdateExpendable" runat="server" CssClass="button_bak2" 
                                                            onclick="Button_UpdateExpendable_Click" Text="获取易耗品信息" Visible="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                                        数量：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                                        <asp:TextBox ID="TBCount" runat="server" title="请输入数量~float"></asp:TextBox><span
                                                            style="color: Red">*</span>
                                                    </td>
                                                    <td class="table_body table_body_NoWidth" style="height: 30px">
                                                        单位：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px">
                                                        <asp:DropDownList ID="DDLUnit" runat="server" title="请选择单位">
                                                        </asp:DropDownList>
                                                        <span style="color: Red">*</span>
                                                    </td>
                                                </tr>
                                                
                                                <tr runat="server" id="tr_expandable" visible="False">
                                                    <td class="table_body table_body_NoWidth" style="height: 30px" runat="server">
                                                        种类：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px" runat="server">
                                                       <asp:DropDownList ID="DropDownList_ExpendableType" runat="server" title="请选择种类">
                                                        </asp:DropDownList>
                                                       
                                                       <span
                                                            style="color: Red">*</span>
                                                        <asp:Label ID="Label_NoItem" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td class="table_body table_body_NoWidth" style="height: 30px" runat="server">
                                                        单价：
                                                    </td>
                                                    <td class="table_none table_none_NoWidth" style="height: 30px" runat="server">
                                                         <asp:TextBox ID="TextBox_UnitPrice" runat="server" title="请输入单价~float"></asp:TextBox>
                                                        <span style="color: Red">*</span>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td id="Td1" colspan="4" runat="server">
                                                        <asp:Label ID="LblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr  id="tr_button_add"  runat="server" visible="False">
                                                    <td colspan="4" runat="server">
                                                        <asp:Button ID="Button_Add" OnClick="Button3_Click" runat="server" Text="添加" CssClass="button_bak" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    
                                    
                                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="产品名称"></asp:BoundField>
                                                    <asp:BoundField DataField="Model" HeaderText="型号"></asp:BoundField>
                                                    <asp:BoundField DataField="Count" HeaderText="数量" DataFormatString="{0:#,0.#####}"></asp:BoundField>
                                                    <asp:BoundField DataField="Unit" HeaderText="单位"></asp:BoundField>
                                                    <asp:BoundField DataField="InTime" HeaderText="入库时间" 
                                                        DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False"></asp:BoundField>
                                                   
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                                CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此入库明细吗？')"
                                                                CausesValidation="false" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>删除</HeaderTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <center>
                                                        <span style="color: Red">未添加入库明细信息</span></center>
                                                </EmptyDataTemplate>
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                                <RowStyle HorizontalAlign="Center" Height="20px" />
                                            </asp:GridView>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        入库备注：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        <input type="text" id="TextArea1" style="width: 99%;" runat="server" title="请输入入库备注~50:"></input>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_body table_body_NoWidth">
                                        经办人签名：
                                    </td>
                                    <td class="table_none table_none_NoWidth">
                                        用户名：
                                        <asp:TextBox ID="tbApplicant" runat="server" title="请输入用户名~20:"></asp:TextBox><span
                                            style="color: Red">*</span> &nbsp; 密码：
                                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox><span
                                            style="color: Red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="提交" OnClick="btSubmit_Click"
                                            OnClientClick="javascript:return confirm('确定提交？');" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>
       </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function f(obj) {
            obj.select();
        }
    </script>

</asp:Content>
