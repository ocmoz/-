<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_PriceDetail, App_Web_nuqbk7th" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

<script type="text/javascript">
    function clearbox() {

        document.getElementById('<%= TextBox_ProductName.ClientID %>').value = "";
        document.getElementById('<%= TextBox_Model.ClientID %>').value = "";
        document.getElementById('<%= TextBox_Approvaler.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApplyTimeLower.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApplyTimeUpper.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApprovalTimeLower.ClientID %>').value = "";
        document.getElementById('<%= TextBox_ApprovalTimeUpper.ClientID %>').value = "";
    }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="价格管理"
        HeadOPTxt="目前操作功能：指导性价格维护" HeadHelpTxt="显示包括当前的、历史的和审批中的指导价格">
        <cc1:HeadMenuButtonItem ButtonIcon="edit.gif" ButtonName="管理指导价格信息" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="Href" ButtonUrl="EditPrice.aspx" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                <cc2:TabPanel runat="server" HeaderText="当前指导价格列表" ID="TabPanel1">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="产品名称">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Model" HeaderText="产品规格型号">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unit" HeaderText="标价单位">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="启用时间">
                                        <HeaderStyle />
                                    </asp:BoundField>
             
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            指导价格(元)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LowerPrice" runat="server" Text='<%#Eval("LowerPrice","{0:#,0.##}")%>'></asp:Label>
                                            &nbsp;－
                                            <asp:Label ID="UpperPrice" runat="server" Text='<%#Eval("UpperPrice","{0:#,0.##}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" Text="编辑" ImageUrl="~/images/ICON/edit.gif" HeaderText="编辑"
                                        CommandName="view">
                                        <HeaderStyle Width="70px" />
                                    </asp:ButtonField>
                                </Columns>
                                <EmptyDataTemplate>
                                    没有当前指导价格信息
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="历史指导价格列表" ID="TabPanel2">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:GridView Width="100%" ID="GridView2" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="产品名称">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Model" HeaderText="产品规格型号">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unit" HeaderText="标价单位">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="启用时间">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="停用时间">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            指导价格(元)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LowerPrice" runat="server" Text='<%#Eval("LowerPrice","{0:#,0.##}")%>' ></asp:Label>
                                            &nbsp;－
                                            <asp:Label ID="UpperPrice" runat="server" Text='<%#Eval("UpperPrice","{0:#,0.##}")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    没有历史指导价格信息
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager2" runat="server" OnPageChanged="AspNetPager2_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
                
                <cc2:TabPanel runat="server" HeaderText="指导价格查询" ID="TabPanel4">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    组合查询（支持模糊查询）
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    产品名称：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    规格及型号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="Model" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    当前还是历史价格：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:DropDownList ID="historyorcurrent" onchange="javascript:return selectchange()"
                                        runat="server">
                                        <asp:ListItem Value="1">当前价格</asp:ListItem>
                                        <asp:ListItem Value="2">历史价格</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                           </tr>
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    指导价格范围：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:TextBox ID="LowerPrice" title="请输入指导价格下限~float" runat="server"></asp:TextBox>
                                    －
                                    <asp:TextBox ID="UpperPrice" title="请输入指导价格上限~float" runat="server"></asp:TextBox> 
                                    
                                </td>
                                
                             </tr>
                             
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    启用时间范围：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                
                                <asp:TextBox ID="StartTime2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入启用时间下限~date"></asp:TextBox> －
                                    <asp:TextBox ID="StartTime1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入启用时间上限~date"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    <div id="id1" style="display: <%= ShowEndTime %>">
                                        停用时间范围：
                                    </div>
                                </td>
                            
                                <td class="table_none table_none_NoWidth" colspan="3">
                                    <div id="id2" style="display: <%= ShowEndTime %>">
                                     <asp:TextBox ID="EndTime2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="请输入停用时间下限~date"></asp:TextBox>
                                             －
                                        <asp:TextBox ID="EndTime1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                            title="请输入停用时间上限~date"></asp:TextBox>
                                       
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                            runat="server">
                            <tr>
                                <td align="center" style="height: 38px">
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" OnClick="Button1_Click"
                                        Text="确定" />&nbsp;&nbsp;
                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                
                <cc2:TabPanel runat="server" HeaderText="申请审批列表" ID="TabPanel3">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:GridView Width="100%" ID="GridView3" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="GridView3_RowCommand" OnRowDataBound="GridView3_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ApplicantName" HeaderText="申请人">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApplyDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                        HeaderText="申请日期">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApprovalerName" HeaderText="审批人">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            审批日期</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovalDate" runat="server" Text='<%#DateTime.Compare(Convert.ToDateTime(Eval("ApprovalDate")),DateTime.MinValue)==0?"": Eval("ApprovalDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StatusName" HeaderText="当前状态">
                                        <HeaderStyle />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                        HeaderText="查看" CommandName="view">
                                        <HeaderStyle Width="60px" />
                                    </asp:ButtonField>
                                </Columns>
                                <EmptyDataTemplate>
                                    没有申请审批的信息
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                <RowStyle HorizontalAlign="Center" Height="20px" />
                            </asp:GridView>
                            <cc1:AspNetPager ID="AspNetPager3" runat="server" OnPageChanged="AspNetPager3_PageChanged"
                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                            </cc1:AspNetPager>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
                
                <cc2:TabPanel runat="server" HeaderText="审批查询" ID="TabPanel5">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    申请审批组合查询（支持模糊查询）
                                </td>
                            </tr>
                            <tr>
                             <td class="table_body table_body_NoWidth" style="height: 30px; width:10%">
                                    设备名称：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px; width:40%">
                                    <asp:TextBox ID="TextBox_ProductName" runat="server"></asp:TextBox>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px;width:10%">
                                    设备型号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px;width:40%">
                                    <asp:TextBox ID="TextBox_Model" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    审批人姓名：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                    <asp:TextBox ID="TextBox_Approvaler" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    申请时间范围：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                
                                <asp:TextBox ID="TextBox_ApplyTimeLower" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入申请时间下限~date"></asp:TextBox> －
                                    <asp:TextBox ID="TextBox_ApplyTimeUpper" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入申请时间上限~date"></asp:TextBox>
                                    
                                </td>
                                
                             </tr>
                             
                           <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    审批时间范围：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                
                                <asp:TextBox ID="TextBox_ApprovalTimeLower" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入审批时间下限~date"></asp:TextBox> －
                                    <asp:TextBox ID="TextBox_ApprovalTimeUpper" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入审批时间上限~date"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            
                            
                       
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="Button_QueryApply" runat="server" CssClass="button_bak" OnClick="Button_QueryApply_Click"
                                        Text="确定" />&nbsp;&nbsp;
                                    <input id="Reset_QueryApply" class="button_bak" type="button" value="重填" onclick="javascript:clearbox();" />
                                </td>
                            </tr>
                        </table>
                        </div>
                    </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
    function selectchange()
    {
    var temp = document.getElementById('<%=this.historyorcurrent.ClientID%>');
    if(temp.value=="1"){
        document.getElementById("id2").style.display = "none";
        document.getElementById("id1").style.display = "none";
        }
    if(temp.value=="2"){
        document.getElementById("id2").style.display = "block";
        document.getElementById("id1").style.display = "block";
        }
    }
    
    </script>

</asp:Content>
