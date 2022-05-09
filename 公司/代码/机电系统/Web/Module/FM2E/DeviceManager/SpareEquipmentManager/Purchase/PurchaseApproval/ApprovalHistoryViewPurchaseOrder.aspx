<%@ Page Title="查看采购单" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovalHistoryViewPurchaseOrder.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_ApprovalHistoryViewPurchaseOrder" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    <script type="text/javascript">
     //审批一个材料
        function addtolist(addstring) {
            document.getElementById('<%= Hidden_ApprovalItem.ClientID %>').value = addstring;
            document.getElementById('<%= Button_ApprovalItem.ClientID %>').click();
        }

        //审核结果提示警告：如果修改过，又选择审核通过，则提示
        //如果没有修改过，又选择返回修改，则提示
        //无论是否修改过，选择不通过，则提示
        function approvalAlert() {

            if(trim(document.getElementById('<%= TextBox_ApprovalRemark.ClientID %>').value).length==0)
            {
                alert('请输入审批意见');
                return false;
            }
            var select = document.getElementById('<%= DropDownList_ApprovalResult.ClientID %>');
           
            var hasModify = (document.getElementById('<%= Span_Adjust.ClientID %>').innerHTML.length > 0);
            
            switch (select.options[select.selectedIndex].value ) {
                case '<%= (int)PurchaseOrderApprovalResult.PASS  %>':
                    if (hasModify)
                        return confirm('该申请单经调整过，确定通过审批？');
                    else
                        return confirm('确定通过审批？');
                    break;
            case '<%= (int)PurchaseOrderApprovalResult.RETURNANDMODIFY %>':
                if (!hasModify)
                    return confirm('该申请单没有调整过，确定本申请单需要返回修改？');
                else
                    return confirm('确定返回审批结果？');
                break;
            case '<%= (int)PurchaseOrderApprovalResult.NOTPASS %>':
                return confirm('确定直接否决该申请单？');
                break;
                default:
                return false;
                break;
            }
        }
    </script>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="备品备件采购"
        HeadOPTxt="目前操作功能：查看申购单" HeadHelpTxt="点击数量查询库存、点击单价查询指导价">

        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回历史列表" ButtonUrlType="Href" ButtonPopedom="List"
            ButtonUrl="PurchaseApprovalHistory.aspx" />

    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="6">
                    材料申购表
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="3" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="未知公司"></asp:Label><br />
                    <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue" ></asp:Label>&nbsp;机电材料申购表
                </td>
                <td style="text-align: center" colspan="3" class="Table_searchtitle">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="3">
                    <asp:Label ID="Label_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdataPanel_GridView">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ItemList" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" DataKeyNames="ItemID" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                OnRowDataBound="gridview_ItemList_RowDataBound" ShowFooter="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="系统 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_SystemName" runat="server" Text='<%# Eval("SystemName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="产品名称 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格型号">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Model" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量<br/>申请/审批">
                                        <ItemTemplate>
                                         
                                            <asp:Label ID="Label_PlanCount" runat="server" 
                                                Text='<%# Eval("PlanCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustCount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustCount", "{0:#,0.##}") %>'></asp:Label><a id="querystorage" runat="server" 
                                           href='<%# string.Format(Eval("ProductQueryStorageScript").ToString(),CanAdjust)  %>'><span style="color:Blue">查询库存</span></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单价(元)<br/>申请/审批">
                                        <ItemTemplate>
                                           
                                           <asp:Label ID="Label_UnitPrice" runat="server" Text='<%# Eval("Price", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustPrice" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustPrice", "{0:#,0.##}") %>'></asp:Label>
                                           
                                          <a runat="server"  id="queryprice"
                                           href='<%# string.Format(Eval("ProductQueryPriceScript").ToString(),CanAdjust) %>'><span style="color:Blue">查询指导价</span> </a>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)<br/>申请/审批">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_PlanAmount" runat="server" Text='<%# Eval("PlanAmount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustAmount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("AdjustAmount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_TotalAmount" runat="server"></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_AdjustTotalAmount" runat="server" ForeColor="Red" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未添加申购材料</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align:right; width:10%">申请人：</td>
                <td style="width:20%">  <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label></td>
                <td class="Table_searchtitle" style="text-align:right; width:10%">提交时间：</td>
                <td style="width:20%">  <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label></td>
                 <td class="Table_searchtitle" style="text-align:right; width:10%">当前状态：</td>
                 <td style="width:20%">  
                 <asp:Label ID="Label_Status" runat="server"></asp:Label>
                 <asp:Label ID="Label_Approvaling" runat="server" Visible="false"></asp:Label>
                 </td>
            </tr>
             <tr>
                
                 <td class="Table_searchtitle" style="text-align:right; width:15%">备注：</td>
                 <td style="width:85%"  colspan="5">  <asp:Label ID="Label_Remark" runat="server"></asp:Label></td>
            </tr>
             <tr>
                <td class="Table_searchtitle" colspan="6">
                   审批历史
                </td>
            </tr>
                        <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_ApprovalRecord">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ApprovalRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                 >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="审批人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Approvaler" runat="server" Text='<%# Eval("ApprovalerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批结果">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalResult" runat="server" Text='<%# Eval("ResultString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="反馈意见">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_FeeBack" runat="server" 
                                                Text='<%# Bind("FeeBack") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细建议">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalLog" runat="server" Text='<%# Eval("ApprovalLog") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ApprovalDate" runat="server" Text='<%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未经审批</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                 <td  colspan="6" style="text-align:right">
                 <asp:Button ID="Button_GetApproval" runat="server" Text="我要审批" Visible="false" 
                        CssClass="button_bak" onclick="Button_GetApproval_Click" />
                 <asp:Button ID="Button_ReleaseApproval" runat="server" Text="放弃审批"  OnClientClick="javascript:return confirm('确认放弃审批？');" Visible="false"
                        CssClass="button_bak" onclick="Button_ReleaseApproval_Click"/>
                 <input id="Hidden_ApprovalItem" value="" runat="server" type="hidden" />
                 <input id="Button_ApprovalItem" type="button" runat="server" value="添加" style="display: none"
                                onserverclick="Button_ApprovalItem_Click" />
                 </td>
            </tr></table>
          <table runat="server" id="Table_Approval" visible="false" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
              <tr>
                <td class="Table_searchtitle"  colspan="2">
                   审批
                </td>
            </tr>
            <tr>
                
                 <td class="Table_searchtitle" style="text-align:right; width:15%">审批结果：</td>
                 <td style="width:85%" > 
                     <asp:DropDownList ID="DropDownList_ApprovalResult" runat="server">
                     </asp:DropDownList>
                     
                 </td>
            </tr>
            <tr>
             <td class="Table_searchtitle" style="text-align:right; width:15%">调整意见：</td>
                 <td style="width:85%; text-align:left" > 
                    <font color="red"><span id="Span_Adjust" runat="server"></span></font> 
                 </td>
            </tr>
            <tr>
                
                 <td class="Table_searchtitle" style="text-align:right; width:15%">审批意见：</td>
                 <td style="width:85%"> 
                     <asp:TextBox ID="TextBox_ApprovalRemark" runat="server" TextMode="MultiLine" 
                         Width="600px" Height="200px"></asp:TextBox> </asp:Label></td>
            </tr>
             <tr>
                <td class="Table_searchtitle"  colspan="2">
                   <asp:Button ID="Button_DoApproval" runat="server" Text="审批" 
                        CssClass="button_bak" onclick="Button_DoApproval_Click"
                         OnClientClick="javascript:return approvalAlert();"
                         />
                </td>
            </tr>
              
            </table>
            
         <table id="Table_ModifyRecord" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
            <tr>
                <td class="Table_searchtitle" colspan="6">
                   修改历史
                </td>
            </tr>
                        <tr>
                <td colspan="6">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:GridView ID="gridview_ModifyRecord" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-BackColor="#efefef" HeaderStyle-Height="25px"
                                RowStyle-Height="20px" Width="100%" HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center"
                                 >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="修改人 ">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Modifier" runat="server" Text='<%# Eval("ModifierName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改操作">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTypeString" runat="server" Text='<%# Eval("ModifyTypeString") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="保存内容">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Content" runat="server" 
                                                Text='<%# Eval("Content") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="修改时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ModifyTime" runat="server" Text='<%# Eval("ModifyTime", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <center>
                                        未作修改</center>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
             <tr>
                <td class="Table_searchtitle" colspan="6">
                   相关申购单
                </td>
             </tr>
                        <tr>
                <td colspan="6">

                            <asp:GridView ID="gridview_RelatedOrders" runat="server" AutoGenerateColumns="False" 
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" 
                                 RowStyle-Height="20px" Width="100%"
                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" 
                                >
                        <Columns>
                            <asp:TemplateField HeaderText="申购单">
                                <ItemTemplate>
                                    <a style="color:Blue" href='../PurchaseApply/ViewPurchaseOrder.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history&return=1'>
                                    <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="Label_OrderID"></asp:Label>-<asp:Label Text='<%# Bind("SubOrderIndex")%>' runat="server"
                                    ID="Label_SubOrderIndex"></asp:Label>&nbsp;
                                     <asp:Label Text='<%# Eval("PurchaseOrderName") %>' runat="server" ID="Label_PurchaseOrderName" Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;机电材料申购单
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%"  />
                            </asp:TemplateField>
                            <asp:BoundField DataField="WorkFlowStateDescription" HeaderText="状态">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="UpdateTime" HeaderText="最后更新时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                <HeaderStyle />
                                <ItemStyle  Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                           
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>

                </td>
            </tr>
            </table>
    </div>
</asp:Content>

