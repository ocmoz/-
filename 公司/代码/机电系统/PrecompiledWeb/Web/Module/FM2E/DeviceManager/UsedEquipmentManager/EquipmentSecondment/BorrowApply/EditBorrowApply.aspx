<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApply_EditBorrowApply, App_Web_r3p6tpns" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备借调申请"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：申请设备借调">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="借调申请列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="BorrowApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="JavaScript" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
<%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
              <%--  <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <cc2:TabPanel runat="server" HeaderText="添加借调申请" ID="TabPanel1">
                        <ContentTemplate>--%>

                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" colspan="4">
                                            设备借调申请单
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            表单编号：
                                        </td>
                                        <td class="table_none table_none_NoWidth" colspan="3">
                                            <asp:Label ID="lbSheetNO" runat="server" Columns="20" Width="120px" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            申请方：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:Label ID="lbBorrowCompany" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            借出方：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:DropDownList ID="ddlLendCompany" runat="server">
                                            </asp:DropDownList>
                                            <span style="color:Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            申请人：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:Label ID="lbApplicant" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            申请单状态：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                               
                            </div>
                   <%--     </ContentTemplate>
                    </cc2:TabPanel>
                    <cc2:TabPanel runat="server" HeaderText="编辑借调明细" ID="TabPanel2" Visible="false">
                        <ContentTemplate>--%>
    <%--                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            物品名称：<input type="hidden" id="Hidden_EditItemIndex" runat="server" />
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbEquipmentName" runat="server" title="请输入产品名称~20:"></asp:TextBox>
                                            <span style="color:Red">*</span>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            规格型号：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbModel" runat="server" title="请输入规格型号~20:"></asp:TextBox>
                                            <span style="color:Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            数量：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbCount" runat="server" title="请输入数量~int"></asp:TextBox>
                                            <span style="color:Red">*</span>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            单位：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:DropDownList ID="ddlUnit" runat="server">
                                            </asp:DropDownList>
                                            <span style="color:Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            归还日期：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbReturnDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                                title="请输入提交时间~date"></asp:TextBox>
                                                <span style="color:Red">*</span>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            借用原因：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="tbReason" runat="server" Width="99%"
                                                title="请输入借用原因~50:"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            使用地点：
                                        </td>
                                        <td class="table_none table_none_NoWidth" colspan="3">
                                          <input ID="TextBox_Address" type="text" style="width:70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"/><span style="color: Red">*</span>
                                <input type="hidden" id="Hidden_AddressID" runat="server" />
                                    <input type="text" id="TextBox_DetailLocation" style="width: 20%;" runat="server" title="请输入详细地址~40:" />
                                </td>
                                              
                                        
                                       
                                    </tr>
                                </table>
                                <center>
                                <asp:Label ID="errMsg" ForeColor="Red" runat="server"></asp:Label><br />
                                            <asp:Button ID="Button_AddUpdate" runat="server" CssClass="button_bak" Text="添加明细" OnClick="Button_AddUpdate_Click" />
                                           <%-- <asp:Button ID="Button5" runat="server" CssClass="button_bak" Text="结束编辑" OnClick="Button5_Click" />--%>
                                       </center>
                            </div>
                            <hr style="width: 100%" />
                            <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                <asp:GridView ID="GridView_Detail" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                    OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EquipmentName" HeaderText="物品名称">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="规格型号">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Count" HeaderText="数量">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="单位">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Reason" HeaderText="借用原因">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AddressName" HeaderText="使用地点">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReturnDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="归还日期"
                                            HtmlEncode="False">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                          <asp:TemplateField>
                                          <HeaderTemplate>编辑</HeaderTemplate>
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                                    CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                    CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此申请明细吗？')"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                            <HeaderTemplate>删除</HeaderTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                       <span style="color:Red">未添加借调申请明细信息</span>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" Height="20px" />
                                </asp:GridView>
                               
                            </div>
    <%--                        </ContentTemplate>
                            </asp:UpdatePanel>--%>
                      <%--  </ContentTemplate>
                    </cc2:TabPanel>
                </cc2:TabContainer>--%>
<%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <center>
                                        <%--<asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="编辑明细" OnClick="Button1_Click" />&nbsp;&nbsp;--%>
                                            <asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="保存草稿" OnClick="Button2_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="提交申请" OnClick="Button3_Click" OnClientClick="javascript:return confirm('确认提交？');" />&nbsp;&nbsp;
                                            <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                       </center>
    </div>
</asp:Content>
