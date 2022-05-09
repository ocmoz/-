<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApply_EditOutWarehouseApply, App_Web_cz-pz2av" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../../../../control/WorkFlowUserSelectControl.ascx" tagname="WorkFlowUserSelectControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
<script type="text/javascript">
    //地址选定
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
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备出库申请信息维护"
        HeadOPTxt="目前操作功能：出库申请单维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="申请单列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="OutWarehouseApply.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="OutWarehouseApply.aspx" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%; ">
       <%-- <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="添加出库申请" ID="TabPanel1">
                <ContentTemplate>--%>
                    <table id="outwarehouse" width="100%" cellpadding="0" cellspacing="0" border="1"
                        bordercolor="#cccccc" style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                出库申请单详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                申请单号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                仓库：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DropDownList_Warehouse" runat="server" title="请选择仓库~">
                                </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                申请备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <input id="TextArea1" style="width: 99%;" runat="server" title="请输入申请备注~50:" />
                            </td>
                        </tr>
                    </table>
                   
            
                    
                    
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                            style="border-collapse: collapse;">
                             <tr align="center">
                                <td class="Table_searchtitle" colspan="4">
                                    申请设备信息、用途以及地点
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    产品名称：<input id="Hidden_Edit_RowIndex" runat="server" type="hidden" />
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <input type="text" runat="server" id="ProductName" onfocus="javascript:showPopWin('查询库存','SelectStorage.aspx', 900, 400, addProduct,true,true);" onclick="return ProductName_onclick()" />
                                    <input class="cbutton" onclick="javascript:Clear('ProductName');" type="button"
                                        value="清除" id="Button1" />
                                    <input id="ProductNo" runat="server" type="hidden" style="width: 15px" />
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    品牌型号：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="TextBox_Model" runat="server" title="请输入品牌型号~20:"></asp:TextBox><span
                                        style="color: Red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    数量：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:TextBox ID="TextBox_Count" runat="server" title="请输入数量~float"></asp:TextBox><span
                                        style="color: Red">*</span>
                                </td>
                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                    单位：
                                </td>
                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                    <asp:DropDownList ID="DDLUnit" runat="server">
                                    </asp:DropDownList>
                                    <span style="color: Red">*</span>
                                </td>
                            </tr>
                           
                            <tr>
                                <td class="table_body table_body_NoWidth" >
                                    转入地址：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                <input ID="TextBox_Address" type="text" style="width:70%" runat="server" onfocus="javascript:showPopWin('选择地址','../../../../BasicData/AddressManage/Address.aspx?operator=select', 900, 400, addAddress,true,true);"/><span style="color: Red">*</span>
                                <input type="hidden" id="Hidden_AddressID" runat="server" />
                                    <input type="text" id="TextBox_DetailLocation" style="width: 20%;" runat="server" title="请输入详细地址~100:" />
                                </td>
                            
                                <td class="table_body table_body_NoWidth">
                                    所属系统：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:DropDownList ID="DDLSystem" runat="server">
                                    </asp:DropDownList>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    用途：
                                </td>
                                <td class="table_none table_none_NoWidth" >
                                    <input  type="text" id="TextBox_Usage" style="width: 90%;" runat="server" title="请输入用途~50:" />
                                </td>
                           
                                <td class="table_body table_body_NoWidth" >
                                    备注：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <input type="text" id="TextBox_ApplyRemark" style="width: 90%;" runat="server" title="请输入申请备注~50:" />
                                </td>
                            </tr>
                            <tr>
                                <td  colspan="4">
                                <center>
                                    <asp:Label ID="LblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <br />
                                    
                                    <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="添加明细" OnClick="Button3_Click" />
                                    </center>
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
                                        <%#  Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle  Width="5%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SystemName" HeaderText="系统">
                                <ItemStyle  Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="产品名称"><ItemStyle  Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="品牌型号"><ItemStyle  Width="10%" /></asp:BoundField>
                                
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <%# Eval("Count","{0:#,0.#####}") %>&nbsp;&nbsp;<%# Eval("Unit") %>
                                    </ItemTemplate>
                                    <ItemStyle  Width="10%" />
                                    <HeaderTemplate>数量</HeaderTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Usage" HeaderText="用途"><ItemStyle  Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="AddressName" HeaderText="使用地址"><ItemStyle  Width="25%" /></asp:BoundField>
                                <asp:BoundField DataField="Remark" HeaderText="备注"><ItemStyle  Width="10%" /></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" ImageUrl="~/images/ICON/edit.gif"
                                            CommandName="edititem" CommandArgument="<%# Container.DataItemIndex %>"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                    <ItemStyle  Width="5%" />
                                    <HeaderTemplate>修改</HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif"
                                            CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此申请明细吗？')"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                    <ItemStyle  Width="5%" />
                                    <HeaderTemplate>删除</HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                               <center><span style="color:Red"> 没有申请明细信息</span></center>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                    </div>
                    
                    <center>
                
                    <div>                
                    <uc1:WorkFlowUserSelectControl ID="WorkFlowUserSelectControl1" runat="server" />
             </div>
                            <asp:Button ID="btSubmit" runat="server" CssClass="button_bak2" Text="保存或提交申请" OnClientClick="javascript:return confirm('确定提交或保存？');" OnClick="btSubmit_Click" />
                    </center>
             <%--   </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>--%>

        <script type="text/javascript" language="javascript">
        
        function addtolist() {

        }
        
        function addProduct(addstring)
        {
            var arr = addstring.split(",");
            if(arr.length>=4){
                document.all.<%=this.ProductName.ClientID %>.value=arr[0];
	            document.all.<%=this.TextBox_Model.ClientID %>.value=arr[1];
	            document.all.<%=this.TextBox_Count.ClientID %>.value=arr[2];
	            document.all.<%=this.DDLUnit.ClientID %>.value=arr[3];
             }
            
        }
        function Clear(target)
        {
            switch(target)
            {
                case 'ProductName':
                {
                    document.all.<%=this.ProductName.ClientID %>.value='';
	                document.all.<%=this.TextBox_Model.ClientID %>.value=''; 
	                break;
                }
	        default:break;
            }
             
        }
function ProductName_onclick() {

}

        </script>
</asp:Content>
