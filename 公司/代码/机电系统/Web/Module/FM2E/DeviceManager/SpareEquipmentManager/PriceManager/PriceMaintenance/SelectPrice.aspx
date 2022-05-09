<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="SelectPrice.aspx.cs" Inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_SelectPrice"
    Title="无标题页" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <iframe style="Z-INDEX:-1;WIDTH:900px;POSITION:absolute;TOP:0px;HEIGHT:380px" frameborder="0"></iframe>
    
    <div id="PopupDiv" style="position: absolute; display: block; z-index: 100">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 900px; height: 350px;">
                    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc2:TabPanel runat="server" HeaderText="当前指导价格列表" ID="TabPanel1">
                            <ContentTemplate>
                                <table>
                                    <tr align="center">
                                        <td>
                                            <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                                <asp:GridView Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="checkBox1" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                                                        <asp:BoundField DataField="UpperPrice" DataFormatString="{0:#,0.##}" HeaderText="指导价格上限">
                                                            <HeaderStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="LowerPrice" DataFormatString="{0:#,0.##}" HeaderText="指导价格下限">
                                                            <HeaderStyle />
                                                        </asp:BoundField>
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
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <input class="button_bak" type="button" value="确定" onclick="javascript:onOK();" />
                                            <input class="button_bak" type="button" value="关闭" onclick="javascript:window.parent.hidePopWin(false);" />
                                            <input type="hidden" runat="server" id="addstring" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc2:TabPanel>
                        <cc2:TabPanel runat="server" HeaderText="指导价格查询" ID="TabPanel2">
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
                                            启用时间范围：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="StartTime1" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                                title="请输入启用时间上限~date"></asp:TextBox>(上限)<br />
                                            <asp:TextBox ID="StartTime2" runat="server" class="input_calender" onfocus="javascript:HS_setDate(this);"
                                                title="请输入启用时间下限~date"></asp:TextBox>(下限)
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            指导价格范围：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="UpperPrice" runat="server"></asp:TextBox>(上限)<br />
                                            <asp:TextBox ID="LowerPrice" runat="server"></asp:TextBox>(下限)
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
                    </cc2:TabContainer>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    

    <script type="text/javascript" language="javascript">
//    DivSetVisible();
//        function DivSetVisible()
//　　 {
//　
//　　 var DivRef = document.getElementById('PopupDiv');
//　　 var IfrRef = document.getElementById('DivShim');
//　
//　　 DivRef.style.display = "block";
//　　 IfrRef.style.width = DivRef.offsetWidth;
//　　 IfrRef.style.height = DivRef.offsetHeight+10;
//　　 IfrRef.style.top = DivRef.style.top;
//　　 IfrRef.style.left = DivRef.style.left;
//　　 IfrRef.style.zIndex = DivRef.style.zIndex - 1;
//　　 IfrRef.style.display = "block";

//　　
//　　 }
    function onClientClick(checkid,companyid,productname,model)
    {
       var checkboxitem =  document.getElementById(checkid);
       var hiddenitem = document.getElementById("<%=addstring.ClientID%>");
       if(checkboxitem.checked==true)
       {
       hiddenitem.value += "|"+companyid+","+productname+","+model;
       }
       else
       {
       hiddenitem.value = hiddenitem.value.replace("|"+companyid+","+productname+","+model,"");
       
       }
   }

   function onOK() {
       window.returnVal = document.getElementById("<%=addstring.ClientID%>").value;
       window.parent.hidePopWin(true);
   }
    </script>

</asp:Content>
