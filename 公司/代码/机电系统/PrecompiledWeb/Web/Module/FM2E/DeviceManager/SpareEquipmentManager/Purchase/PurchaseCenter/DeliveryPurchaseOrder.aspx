<%@ page title="分配采购项" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseCenter_DeliveryPurchaseOrder, App_Web__gf_si04" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="FM2E.Model.Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
 <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    
      <script type="text/javascript">

          //编辑的时候设置模式对话框的值
          function setModalPopup(button_id) {

              
              
              var regS = new RegExp(",", "gi"); //去掉逗号

              //ITEMID
              var itemid = document.getElementById(button_id.replace('LinkButton_EditPurchaser', 'Label_ItemID')).innerText.replace(regS, "");
              document.getElementById('<%= Hidden_ItemID.ClientID %>').value = itemid;

              

              //采购者ID
              var id = document.getElementById(button_id.replace('LinkButton_EditPurchaser', 'Hidden_PurchaserID')).value;
              document.getElementById('<%= Hidden_EditPurchaserID.ClientID %>').value = id;

              //采购者名称
              
              var name = document.getElementById(button_id.replace('LinkButton_EditPurchaser', 'LinkButton_EditPurchaser')).innerText;
              document.getElementById('<%= Label_SelectedPurchaser.ClientID %>').innerText = name;

              //默认选择
              var controls = document.getElementsByTagName("INPUT");
              for (i = 0; i < controls.length; i++) {
                  if (controls[i].type == 'radio' && controls[i].id.indexOf('<%=RadioButtonList_Purchasers.ClientID%>') >= 0 && controls[i].value == id) {
                      controls[i].checked = 'checked';
                      break;
                  }
              }
          }

          function setSelectedValue(id, name) {
              alert(id + "--" + name);
              document.getElementById('<%= Hidden_EditPurchaserID.ClientID %>').value = id;
              document.getElementById('<%= Label_SelectedPurchaser.ClientID %>').innerText = name;
              //alert(id + name);
          }

      </script>
    
     <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="采购中心--分发采购项"
        HeadOPTxt="目前操作功能：分发采购项" HeadHelpTxt="点击采购人进行指派或者修改">
       
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回采购中心" ButtonUrlType="Href" ButtonPopedom="List"
            ButtonUrl="PurchaseCenter.aspx" />

    </cc1:HeadMenuWebControls>
    
    <asp:Panel ID="Panel_EditPurchaser" runat="server" Style="width: 95%; height: 200px; display:none"
        CssClass="modalPopup">
       
                    <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="2">
                    选择采购员<input type="hidden" value="" id="Hidden_EditPurchaserID" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="hidden" value="" id="Hidden_ItemID" runat="server" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

             </td> 
            </tr>
            <tr>
                <td style="
                    " colspan="2">
                    
                    <asp:RadioButtonList ID="RadioButtonList_Purchasers" runat="server" RepeatColumns="5" 
                        RepeatDirection="Horizontal" DataTextField="UserIDPurchaserNameRemark" 
                        DataValueField="UserID" Width="100%" 
                        >
                    </asp:RadioButtonList>
                    
                </td></tr>
        <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right; width:10%
                    ">
                    原来选定：</td> <td class="table_none table_none_NoWidth" style="height: 30px; width:90%">
                    <asp:Label ID="Label_SelectedPurchaser" runat="server"></asp:Label>
                </td>
           </tr>
           </table>


        <center>
            <input id="Button_Save" class="button_bak"
                type="button" value="保存"  runat="server" onserverclick="Button_Save_Click" />
            <asp:Button ID="Button_Cancel_Edit" runat="server" class="button_bak" 
                Text="取消" />
        </center>
    </asp:Panel>
    
    <div id="div_table">
        <table id="RootTable" style="width: 98%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" colspan="4">
                    材料申购表
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="text-align: center" colspan="2" class="Table_searchtitle">
                    <asp:Label ID="Label_CompanyName" runat="server" Text="未知公司"></asp:Label><br />
                     <asp:Label ID="Label_OrderName" runat="server" Font-Underline="true" ForeColor="Blue" ></asp:Label>&nbsp;机电材料申购表
                </td>
                <td style="text-align: center" colspan="2" class="Table_searchtitle">
                    表单编号
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Label ID="Label_OrderID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
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
                                    <asp:TemplateField HeaderText="数量<br/>审批/采购">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_AdjustCount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("FinalCount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualCount" runat="server"
                                                Text='<%# Eval("ActualCount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单价(元)<br/>审批/采购">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_AdjustPrice" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("FinalPrice", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualPrice" runat="server"
                                                Text='<%# Eval("ActualPrice", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金额(元)<br/>审批/采购">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_AdjustAmount" runat="server" ForeColor="Red" 
                                                Text='<%# Eval("FinalAmount", "{0:#,0.##}") %>'></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualAmount" runat="server"
                                                Text='<%# Eval("ActualAmount", "{0:#,0.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label_FinalTotalAmount" runat="server" ForeColor="Red" ></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label_ActualTotalAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请备注">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label><br />
                                            
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("StatusString") %>'></asp:Label><br />
                                            <input type="hidden" id="Hidden_Status" value='<%# Eval("Status") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="采购人">
                                        <ItemTemplate>
                                            <input type="hidden" id="Hidden_PurchaserID" value='<%# Eval("Purchaser") %>' runat="server" />
                                         <asp:LinkButton OnClientClick="javascript:setModalPopup(this.id);" ID="LinkButton_EditPurchaser"
                                          Text='<%# (((string)Eval("Purchaser")==null)||Eval("Purchaser").ToString()=="")? "未选定":Eval("Purchaser").ToString() %>' runat="server" Visible='<%# Eval("CanChangePurchaser") %>' >
                                         </asp:LinkButton>
                                         <asp:Label ID="Label_Purchaser" Text='<%# ((string)Eval("Purchaser"))==null||Eval("Purchaser").ToString()==""? "未选定":Eval("Purchaser").ToString() %>' runat="server" Visible='<%# !Convert.ToBoolean(Eval("CanChangePurchaser")) %>' ></asp:Label>
                                         
                                         <cc2:ModalPopupExtender ID="ModalPopupExtender_EditPurchaser" runat="server" TargetControlID="LinkButton_EditPurchaser"
                                              
                                                                    PopupControlID="Panel_EditPurchaser" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_Save"  CancelControlID="Button_Cancel_Edit" DynamicServicePath=""
                                                                    Enabled="true">
                                             </cc2:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
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
                <td class="Table_searchtitle" style="text-align:right; width:15%">提交时间：</td>
                <td style="width:35%">  <asp:Label ID="Label_SubmitTime" runat="server"></asp:Label></td>
                 <td class="Table_searchtitle" style="text-align:right; width:15%">申请人：</td>
                 <td style="width:35%"> <asp:Label ID="Label_ApplicantName" runat="server"></asp:Label> </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" style="text-align:right; width:15%">最后更新：</td>
                <td style="width:35%">  <asp:Label ID="Label_UpdateTime" runat="server"></asp:Label></td>
                 <td class="Table_searchtitle" style="text-align:right; width:15%">当前状态：</td>
                 <td style="width:35%">  <asp:Label ID="Label_Status" runat="server"></asp:Label>
                 <asp:Label ID="Label_Approvaling" runat="server"></asp:Label>
                 </td>
            </tr>
            <tr>
                <td class="Table_searchtitle" colspan="4">
                   审批历史
                </td>
            </tr>
                        <tr>
                <td colspan="4">
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
                                                Text='<%# Eval("FeeBack") %>'></asp:Label>
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
                <td class="Table_searchtitle" colspan="4">
                   修改历史
                </td>
            </tr>
                        <tr>
                <td colspan="4">
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
        </table>
    </div>
</asp:Content>

