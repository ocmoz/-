<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeFile="RecordOutEquipment.aspx.cs" Inherits="Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowRecord_RecordOutEquipment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
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
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备借出登记"
        HeadHelpTxt="帮助" HeadOPTxt="目前操作功能：设备借出登记">
        <cc1:HeadMenuButtonItem ButtonName="申请单列表" ButtonIcon="list.gif" ButtonPopedom="List"
            ButtonUrl="BorrowRecord.aspx" ButtonUrlType="Href" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="借调申请单信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    申请单编号：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="lbSheetName" runat="server" Text=""></asp:Label>
                               
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    借出方：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="lbLendCompany" runat="server" Text=""></asp:Label>
                                    
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
                                    申请人：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="lbApplicant" runat="server" Text=""></asp:Label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    申请单状态：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                                  
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    申请时间：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="lbSubmitTime" runat="server" Text=""></asp:Label>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    领用人签名：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3">
                                    用户名：
                                    <asp:TextBox ID="tbBorrower" runat="server" title="请输入用户名~20:"></asp:TextBox>
                                    &nbsp; 密码：
                                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btSubmit" runat="server" CssClass="button_bak" Text="提交" OnClick="btSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="9">
                                    申请明细
                                </td>
                            </tr>
                            <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    物品名称
                                </td>
                                <td>
                                    规格型号
                                </td>
                                <td>
                                    数量
                                </td>
                                <td>
                                    单位
                                </td>
                                <td>
                                    借用原因
                                </td>
                                 <td>
                                    借用地点
                                </td>
                                <td>
                                    归还日期
                                </td>
                                <td>
                                    借出
                                </td>
                            </tr>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound"
                                OnItemCommand="Repeater1_ItemCommand">
                                <ItemTemplate>
                                    <tr style="height: 30px">
                                        <td onclick='javascript:HideRecord("RecordTable_"+<%#Container.ItemIndex %>);'>
                                            <asp:Literal ID="ltItemID" runat="server" Text='<%#Eval("ItemID") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="ltEquipmentName" runat="server" Text='<%#Eval("EquipmentName") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="ltModel" runat="server" Text='<%#Eval("Model") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="ltCount" runat="server" Text='<%#Eval("Count") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="ltUnit" runat="server" Text='<%#Eval("Unit") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="ltReason" runat="server" Text='<%#Eval("Reason") %>'></asp:Literal>
                                        </td>
                                         <td>
                                            <asp:Literal ID="ltAddressName" runat="server" Text='<%#Eval("AddressName") %>'></asp:Literal>
                                             <asp:Literal ID="ltDetailLocation" runat="server" Text='<%#Eval("DetailLocation") %>'></asp:Literal>
                                             <input type="hidden" ID="Hidden_AddressID" runat="server" value='<%#Eval("AddressID") %>' />
                                        </td>
                                        <td>
                                            <asp:Literal ID="ltReturnDate" runat="server" Text='<%#Convert.ToDateTime(Eval("ReturnDate")).ToString("yyyy-MM-dd") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="RecordImgButton" runat="server" ImageUrl="~/images/ICON/move.gif"
                                                CommandArgument='<%#Container.ItemIndex %>' CommandName="Record" CausesValidation="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="9">
                                            <table width="100%" style="background-color: #E7E745;" id="RecordTable_<%#Container.ItemIndex %>">
                                                <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: right">
                                                                <img src="../../../../../../images/right.gif" alt="" />
                                                            </td>
                                                            <td align="right" style="width: 70px">
                                                                条形码：
                                                            </td>
                                                            <td align="left" style="width: 70px">
                                                                <%#Eval("EquipmentNO") %>
                                                            </td>
                                                            <td align="right" style="width: 70px">
                                                                物品名称：
                                                            </td>
                                                            <td align="left" style="width: 70px">
                                                                <%#Eval("EquipmentName") %>
                                                            </td>
                                                            <td align="right" style="width: 70px">
                                                                规格型号：
                                                            </td>
                                                            <td align="left" style="width: 70px">
                                                                <%#Eval("Model") %>
                                                            </td>
                                                            <td align="right" style="width: 70px">
                                                                借用原因：
                                                            </td>
                                                            <td align="left" style="width: 100px">
                                                                <%#Eval("Reason") %>
                                                            </td>
                                                            <td align="right" style="width: 70px">
                                                                使用地点：
                                                            </td>
                                                            <td align="left" style="width: 100px">
                                                                <%#Eval("AddressName") %>
                                                            </td>
                                                            <td align="right" style="width: 70px">
                                                                归还日期：
                                                            </td>
                                                            <td align="left" style="width: 70px">
                                                                <%#Convert.ToDateTime(Eval("ReturnDate")).ToString("yyyy-MM-dd") %>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="EditButton" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                                                    CommandArgument='<%#Eval("EquipmentNO") %>' CommandName="Edit" CausesValidation="false" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="DelButton" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                                                    CommandArgument='<%#Eval("EquipmentNO") %>' CommandName="Delete" CausesValidation="false"
                                                                    OnClientClick="return confirm('确认要删除此借出记录吗？')" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="借出设备登记" Visible="false">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    设备借出登记
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    设备条形码：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbEquipmentNO" runat="server" title="请输入设备条形码~20:" OnTextChanged="TBEquipmentNO_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                        <span style="color:Red">*</span>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    设备名称：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbEquipmentName" runat="server" title="请输入设备名称~20:"></asp:TextBox>
                                     <span style="color:Red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    规格型号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbModel" runat="server" title="请输入规格型号~20:"></asp:TextBox>
                                    <span style="color:Red">*</span>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    归还日期：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="tbReturnDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                        title="请输入提交时间~date"></asp:TextBox>
                                        <span style="color:Red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    借用原因：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                    <asp:TextBox ID="tbReason" runat="server" Width="99%"
                                        title="请输入借用原因~50:"></asp:TextBox>
                                </td>
                            </tr>
                        <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    使用地点：
                                </td>
                                <td class="table_none table_none_NoWidth" colspan="3" style="height: 30px">
                                    <input ID="TextBox_Address" type="text" style="width:70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"/><span style="color: Red">*</span>
                                <input type="hidden" id="Hidden_AddressID" runat="server" />
                                    <input type="text" id="TextBox_DetailLocation" style="width: 20%;" runat="server" title="请输入详细地址~40:" />
                                </td>
                            </tr>
                        </table>
                        <center>
                                    <asp:Button ID="btRecord" runat="server" CssClass="button_bak" Text="确定" OnClick="btRecord_Click" />
                                    <asp:Button ID="btCancel" runat="server" CssClass="button_bak" Text="取消" OnClick="btCancel_Click" />
                            </center>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>

    <script type="text/javascript" language="javascript">
        function HideRecord(objId) {
            obj = $get(objId);
            if (obj == null || obj == "undefined")
                return;

            if (obj.style.display != "none")
                obj.style.display = "none";
            else obj.style.display = "inline";
        }

    </script>

</asp:Content>
