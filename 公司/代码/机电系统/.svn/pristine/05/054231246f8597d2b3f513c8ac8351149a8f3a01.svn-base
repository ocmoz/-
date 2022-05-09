<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Statistic.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_Statistic" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    
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
     </script>
    

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品出入库统计"
        HeadOPTxt="目前操作功能：易耗品出入库统计" HeadHelpTxt="默认统计本月的易耗品出入库情况">
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="查询条件" ID="TabPanel1">
                <HeaderTemplate>
                    查询条件
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                    查询条件
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                时间范围：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:TextBox ID="tbDateFrom" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入开始时间~date"></asp:TextBox>至
                                <asp:TextBox ID="tbDateTo" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                    title="请输入结束时间~date"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth" style="height: 30px">
                                故障部门：
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px">
                                <asp:DropDownList ID="ddlDepartment" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                种类：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:UpdatePanel ID="selectcategory" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="CategoryName" runat="server" title="请选择所属设备种类~"></asp:TextBox>
                                        <input class="cbutton" onclick="javascript:Clear('CategoryName');" type="button"
                                            value="清除" id="Button6" />
                                        <asp:TextBox ID="CategoryID" runat="server" title="请选择所属设备种类~" Visible="false"></asp:TextBox>
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
                                年份
                            </td>
                            <td class="table_none table_none_NoWidth" style="height: 30px;width: 15%;">
                                <asp:DropDownList ID="DDLYear" runat="server"></asp:DropDownList>    <asp:Button ID="btn_year" CssClass="button_bak" runat="server" OnClick="yearstatic_Click" Text="年份统计"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                        runat="server">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" align="right" style="height: 38px" runat="server">
                                <asp:Button ID="btnSearch" runat="server" CssClass="button_bak" Text="查询" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="易耗品出入库统计结果">
                <ContentTemplate>
                    <asp:Literal ID="ltStatisticResult" runat="server"></asp:Literal>
                    <br />
                    <div id="divSheets" runat="server">
                        <asp:Repeater ID="rptRxpendableSheets" runat="server" OnItemDataBound="rptRxpendableSheets_ItemDataBound">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            易耗品名称
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            类型
                                        </td>
                                        <td class="Table_searchtitle" style="height:30px">
                                            数量
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            单价
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            设备类型
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            时间
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Name")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Type")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Amount", "{0:0.##}")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Price", "{0:0.##}")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("CategoryID")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("InOutTime", "{0:yyyy-MM-dd}")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            <tr>
                            <td  colspan="6" class="table_body table_body_NoWidth" style="height:30px; text-align:right">
                                <asp:Label ID="lbSheetCount" runat="server" Text="Label"></asp:Label>
                            </td>
                            </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:Label ID="lbErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                </ContentTemplate>
            </cc2:TabPanel>
            
            
            <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="年份统计信息">
                <ContentTemplate>
                    <div runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle"><asp:Label ID="lbYeartitle" runat="server"></asp:Label></td>
                        </tr>   
                        </table>
                        <asp:Repeater ID="rpExpendableYear" runat="server" OnItemDataBound="rpExpendableYear_ItemDataBound">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                    style="border-collapse: collapse;">
                                    <tr>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            易耗品名称
                                        </td>
                                        <td class="Table_searchtitle" style="height:30px">
                                            数量
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            单价
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            设备类型
                                        </td>
                                        <td class="Table_searchtitle" style="height: 30px">
                                            单位
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Name")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Amount", "{0:0.##}")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Price", "{0:0.##}")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("CategoryID")%>
                                    </td>
                                    <td style="height: 30px; text-align: center">
                                        <%#Eval("Unit")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            <tr>
                            <td  colspan="6" class="table_body table_body_NoWidth" style="height:30px; text-align:right">
                                <asp:Label ID="lbYearSheetCount" runat="server" Text="Label"></asp:Label>
                            </td>
                            </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            
        </cc2:TabContainer>
</asp:Content>

