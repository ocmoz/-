<%@ page title="�ɹ�Ա����" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaserManager_PurchaserManager, App_Web_j56zncbu" %>
<%@ Import Namespace="WebUtility" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">

        //�༭��ʱ������ģʽ�Ի����ֵ
        function setModalPopup(button_id) {
            var regS = new RegExp(",", "gi"); //ȥ������

            //ITEMID
            var itemid = document.getElementById(button_id.replace('ImageButton_Edit', 'Hidden_PurchaserID')).value;
            document.getElementById('<%= Hidden_EditItemID.ClientID %>').value = itemid;
            
            //�û���
            var name = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_UserID')).innerText;
            document.getElementById('<%= TextBox_UserID.ClientID %>').value = name;

            //����
            var model = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_PurchaserName')).innerText;
            document.getElementById('<%= TextBox_PersonName.ClientID %>').value = model;


            //��ע
            var remark = document.getElementById(button_id.replace('ImageButton_Edit', 'Label_Remark')).innerText;
            document.getElementById('<%= TextBox_Remark.ClientID %>').value = remark;
        }

        //����༭����
        function saveEditItem() {

            

            document.getElementById('<%= Button_SaveItem.ClientID %>').click();
        }



    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="��Ʒ�����ɹ�"
        HeadOPTxt="Ŀǰ�������ܣ��ɹ�Ա����">
       
    </cc1:HeadMenuWebControls>
    
    
    <asp:Panel ID="Panel_EditPurchaser" runat="server" Style="width: 95%; height: 200px; display:none"
        CssClass="modalPopup">
        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
            text-indent: 13px; border: solid 1px #a7c5e2;" border="1">
            <tr>
                <td class="Table_searchtitle" style="text-align: center" colspan="4">
                    �༭�ɹ�Ա<input type="hidden" value="" id="Hidden_EditItemID" runat="server" />
               &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    �û�����
                </td>
                <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_UserID" runat="server" ></asp:TextBox>
                </td>
                
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ������</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px; ">
                    <asp:TextBox ID="TextBox_PersonName" runat="server"></asp:TextBox>
                </td>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 30px; text-align: right;
                    ">
                    ������</td>
                 <td class="table_none table_none_NoWidth" style="height: 30px;" colspan="3">
                    <asp:TextBox ID="TextBox_Remark" runat="server" title="�����뱸ע~50:" Width="400px"></asp:TextBox>(�ɱ༭)
                </td>
            </tr>
        </table>
        <center>
            <input id="Button_SaveEditPurchaser" class="button_bak"
                type="button" value="����" onclick="javascript:saveEditItem();"/>
            <asp:Button ID="Button_Cancel_EditPurchaser" runat="server" class="button_bak" 
                Text="ȡ��" />
        </center>
    </asp:Panel>
    
    <div>
        <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
            text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" unat="server">
            <tr>
                <td class="Table_searchtitle" >
                    <%= WebUtility.UserData.CurrentUserData.CompanyName%> �Ĳɹ�Ա�б�
                </td>
            </tr>
            <tr>
                <td class="table_none_NoWidth">
                        <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gridview_PurchaserList" runat="server" AutoGenerateColumns="False"
                                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" 
                                        HeaderStyle-Height="25px" RowStyle-Height="20px"
                                        Width="98%" HeaderStyle-HorizontalAlign="center" 
                                        RowStyle-HorizontalAlign="center" 
                                        onrowdeleting="gridview_PurchaserList_RowDeleting">
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="���">
                                                <ItemTemplate>
     
                                                   <asp:Label Text='<%# Container.DataItemIndex + 1%>' runat="server" ID="Label_Index"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="�û���">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="Hidden_PurchaserID" value='<%# Eval("ID") %>' />
                                                   <asp:Label Text='<%# Eval("UserID") %>' runat="server" ID="Label_UserID"></asp:Label>
                                                    
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="����">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_PurchaserName" runat="server" Text='<%# Eval("PurchaserName") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="����">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                </ItemTemplate>
                                      
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="�༭" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CausesValidation="False"
                                                        ImageUrl="~/images/ICON/edit.gif"  OnClientClick="javascript:setModalPopup(this.id);" />
                                                        
                                                     <cc2:ModalPopupExtender ID="ModalPopupExtender_EditPurchaser" runat="server" TargetControlID="ImageButton_Edit"
                                              
                                                                    PopupControlID="Panel_EditPurchaser" BackgroundCssClass="modalBackground"
                                                                    OkControlID="Button_SaveEditPurchaser"  CancelControlID="Button_Cancel_EditPurchaser" DynamicServicePath=""
                                                                    Enabled="true">
                                             </cc2:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField ShowHeader="False" HeaderText="ɾ��">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton_Delete" runat="server" CausesValidation="False" 
                                                        CommandName="Delete"   ImageUrl="~/images/ICON/delete.gif"  OnClientClick="javascript:return confirm('ȷ��ɾ���ɹ�Ա��');"></asp:ImageButton>
                                                </ItemTemplate>
                                                 <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <RowStyle Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            ���޲ɹ�Ա�������
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                </td>
            </tr>
             <tr>
                <td  style="text-align: center">
                    <input id="Button_Select" type="button" runat="server" class="button_bak" value="���"
                        onclick="javascript:showPopWin('ѡ��ɹ�Ա','Select.aspx', 900, 450, addtolist,true,true);" />
                    <input id="Hidden_SelectedUser" value="" runat="server" type="hidden" />
                    <input type="button" style="display:none" id="Button_Save" onserverclick="Button_Save_Click" runat="server"   />
                    <input type="button" style="display:none" id="Button_SaveItem" onserverclick="Button_SaveItem_Click" runat="server"   />
                </td>
            </tr>
        </table>
    </div>
    
    <script type="text/javascript" language="javascript">
        //���һ���ɹ�Ա���ش�����Ϣ�������ɹ�Ա��ID������
        function addtolist(addstring) {
            document.getElementById('<%= Hidden_SelectedUser.ClientID %>').value = addstring;
            document.getElementById('<%= Button_Save.ClientID %>').click();
        }

    </script>
</asp:Content>
