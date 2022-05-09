<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_EditExpendable, App_Web_4xxpdmqi" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript" language="javascript">
          function Clear(target)
          {
              switch(target)
              {
                  case 'CategoryName':
                  {
                      document.all.<%=this.CategoryName.ClientID %>.value='';
	                  //document.all.<%=this.CategoryID.ClientID %>.value='';
	                  break;
                  }
                  default:break;
              }
          }
           function WriteSelect(file_name)
            {
                if (file_name!=undefined)
                {
                    var ShValues = file_name.split('||');
                    switch(ShValues[2])
                    {
                        default:break;
    	            
                    }
	            }
            }
     </script>
     


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品信息维护"
        HeadOPTxt="目前操作功能：易耗品信息维护">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="易耗品列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="Expendable.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel1" runat="server" Style="display: none; width: 300px">
        <asp:Image ID="Image1" runat="server" Width="300px" />
    </asp:Panel>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" ID="TabPanel1">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                易耗品详细信息
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                公司：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px;">
                                <div style="display: <%= IsShow %>">
                                    <asp:DropDownList ID="DDLCompany" runat="server" >
                                    </asp:DropDownList>
                                    <span style="color: Red">*</span>
                                </div>
                                <div runat="server" id="divcompany">
                                </div>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px;">
                                仓库：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px;">
                                <div style="display: <%= IsShow2 %>">
                                    <asp:DropDownList ID="DDLWarehouse" runat="server" >
                                    </asp:DropDownList>
                                    <span style="color: Red">*</span>
                                </div>
                                <div runat="server" id="divwarehouse">
                                </div>
                            </td>
                            <cc2:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DDLCompany"
                                Category="Company" PromptText="请选择公司..." LoadingText="公司加载中..." ServicePath="CompanyWarehouseService.asmx"
                                ServiceMethod="GetCompany" Enabled="True">
                            </cc2:CascadingDropDown>
                            <cc2:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DDLWarehouse"
                                Category="Warehouse" PromptText="请选择仓库..." LoadingText="仓库加载中..." ServicePath="CompanyWarehouseService.asmx"
                                ServiceMethod="GetWarehouse" ParentControlID="DDLCompany" Enabled="True">
                            </cc2:CascadingDropDown>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px; width: 15%;">
                                名称：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="TBName" runat="server" title="请输入易耗品名称~20:!"></asp:TextBox><span
                                    style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                                型号：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 17px">
                                <asp:TextBox ID="TBModel" runat="server" title="请输入易耗品型号~20:"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                                品牌：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 17px">
                                <asp:TextBox ID="TBSpecification" runat="server" title="请输入易耗品规格~60:"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                                库存量：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 17px">
                                <asp:TextBox ID="TBCount" runat="server" title="请输入易耗品库存量~float!"></asp:TextBox><span
                                    style="color: Red">*</span><asp:DropDownList ID="DDLUnit" runat="server" title="请选择易耗品单位~!">
                                    </asp:DropDownList>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                种类：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:UpdatePanel ID="selectcategory" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="CategoryName" runat="server" title="请选择所属设备种类~!"></asp:TextBox>
                                        <input class="cbutton" onclick="javascript:Clear('CategoryName');" type="button"
                                            value="清除" id="Button6" />
                                        <input class="cbutton" onclick="javascript:showPopWin('查询种类信息','<%=Page.ResolveUrl("~/")%>Module/FM2E/BasicData/DeviceTypeManage/DeviceType.aspx?showheader=false', 900, 420, WriteSelect,true,true);"
                                            type="button" value="种类信息" id="Button7" />
                                        <asp:TextBox ID="CategoryID" runat="server" title="请选择所属设备种类~!" Visible="false"></asp:TextBox>
                                        <span style="color: Red">*</span>
                                        <asp:Panel ID="Panel2" CssClass="popupLayer" runat="server">
                                            <div style="border: 1px outset white; ">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TreeView ID="TreeView1" OnTreeNodeExpanded="TreeView1_OnTreeNodeExpanded" runat="server"
                                                            onclick="javascript:causeValidate = false;" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                                        </asp:TreeView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>
                                        <cc2:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="CategoryName"
                                            PopupControlID="Panel2" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                        <cc2:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="CategoryID"
                                            PopupControlID="Panel2" Position="Bottom" DynamicServicePath="" Enabled="True"
                                            ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px;width: 15%;">
                                购置价格(元)：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px;width: 15%;">
                                <asp:TextBox ID="TBPrice" title="价格输入格式不正确~float" runat="server" Text=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                                日期：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 17px">
                               <asp:TextBox ID="TBUpdateDate" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入日期~date!"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 17px">
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="width: 15%; height: 30px;">
                                图片：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" onpropertychange="document.all('myimg').src=this.value;t1.style.display='block';t2.style.display='inline';t3.style.display='inline'" />
                                        <div id="t2" style="display: none">
                                            <asp:Button ID="Button2" runat="server" Text="取消" OnClick="Button2_Click" Visible="true"
                                                CssClass="button_bak" OnClientClick="t1.style.display='none'" /></div>
                                        <div id="t3" style="display: none">
                                            <input id="Button3" type="button" value="确定" class="button_bak" onclick="t1.style.display='none';t2.style.display='none';t3.style.display='none';t4.style.display='none'" /></div>
                                        <div id="t4" style="display: inline">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="单击显示大图" Width="40px" Height="30px" /></div>
                                        <cc2:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ImageButton1"
                                            PopupControlID="Panel1" Enabled="True" DynamicServicePath="" ExtenderControlID="">
                                        </cc2:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr id="tr11" runat="server">
                            <td id="Td2" colspan="4" runat="server">
                                <div id="t1" style="display: none">
                                    <img src="" id="myimg" height="200px"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 78px">
                                备注：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <textarea id="TARemark" style="width: 99%; height: 50px" runat="server" title="请输入备注~100:"></textarea>
                            </td>
                        </tr>
                    </table>
                    <center>
                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                        <input id="Reset1" class="button_bak" type="reset" value="重填" />
                    </center>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanelImport" runat="server">
            <HeaderTemplate>批量导入</HeaderTemplate>
            <ContentTemplate>
                 <asp:FileUpload ID="FileUpload_ImportDevice" runat="server"  />
                <asp:Button ID="Button_Import" runat="server" Text="导入" OnClick="Button_Import_Click" CssClass="button_bak" />
            </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
