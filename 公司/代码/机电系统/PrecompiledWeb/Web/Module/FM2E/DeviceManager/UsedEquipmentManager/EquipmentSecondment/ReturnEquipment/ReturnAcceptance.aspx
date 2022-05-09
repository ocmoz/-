<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_ReturnEquipment_ReturnAcceptance, App_Web_nri2mwfp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
     <script type="text/javascript">
         //地址选定DataFormatString="{0:yyyy-MM-dd}"
         function addAddress(val) {
             var arr = new Array;
             arr = val.split('|');
             var addid = arr[0];
             var addcode = arr[1];
             var addname = arr[2];
             if (addcode != '00') {
                 document.getElementById('<%= Hidden_AddressID.ClientID %>').value = addid;
                 document.getElementById('<%= TextBox_Address.ClientID %>').value = addname;
             }
         }
</script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    
    
    <Services>
      <asp:ServiceReference Path="LocationService.asmx" />
      </Services>
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备归还验收"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：设备归还验收">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="归还验收设备列表" ButtonPopedom="List"
            ButtonUrl="ReturnEquipment.aspx" ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="归还设备" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    设备条形码：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbEquipmentNO" runat="server" title="请输入设备条形码~20:noChinese" 
                                        ontextchanged="tbEquipmentNO_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <span style="color:Red">*</span>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    设备名称：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lbEquipmentName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    规格型号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:Label ID="lbModel" runat="server"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    借出时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    
                                    <asp:Label ID="lbBorrowTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                           
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    应归还时间：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                   <asp:Label ID="lbReturnDate" runat="server"></asp:Label>
                                </td><td class="table_body table_body_NoWidth" style="height: 30px">
                                    验收结果：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="ddlResult" runat="server" onchange="javascript:ResultChanged(this);">
                                        <asp:ListItem Value="1" Text="验收通过"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="验收不通过"></asp:ListItem>
                                    </asp:DropDownList>
                                </td> 
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    验收备注：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:TextBox ID="tbFeeBack" runat="server" TextMode="MultiLine" Height="51px" Width="714px"
                                        title="请输入验收备注~50:"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center">
                                <td class="Table_searchtitle">
                                    归还放置地点
                                </td>
                                 <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                      <input ID="TextBox_Address" type="text" style="width:70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"/><span style="color: Red">*</span>
                                <input type="hidden" id="Hidden_AddressID" runat="server" />
                                    <input type="text" id="TextBox_DetailLocation" style="width: 20%;" runat="server" title="请输入详细地址~40:" />
                                </td>
                            </tr>
                            
                        </table>
                    
                        <center>
                                    <asp:Label ID="errMsg" ForeColor="Red" runat="server"></asp:Label>
                                
                                    <asp:Button ID="btAddDetail" runat="server" CssClass="button_bak" Text="添加明细" OnClick="btAddDetail_Click" />
                                </center>
                        <hr />
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td colspan="2" class="Table_searchtitle">
                                    需要归还的设备列表
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    归还人签名：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    用户名：
                                    <asp:TextBox ID="tbReturner" runat="server" title="请输入用户名~20:"></asp:TextBox>
                                    &nbsp; 密码：
                                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="提交" OnClick="btSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView1_RowDataBound" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="EquipmentNO" HeaderText="设备条形码"></asp:BoundField>
                                <asp:BoundField DataField="EquipmentName" HeaderText="设备名称"></asp:BoundField>
                                <asp:TemplateField HeaderText="验收结果">
                                    <ItemTemplate>
                                    <%#Convert.ToBoolean(Eval("Result")) ? "验收通过" : "验收不通过"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="归还地址">
                                    <ItemTemplate>
                                    <%#Eval("AddressName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FeeBack" HeaderText="验收备注"></asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        编辑</HeaderTemplate>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                            CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此归还项吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        删除</HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                没有要归还的设备
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
    <script type="text/javascript" language="javascript">
       
    
    </script>
</asp:Content>
