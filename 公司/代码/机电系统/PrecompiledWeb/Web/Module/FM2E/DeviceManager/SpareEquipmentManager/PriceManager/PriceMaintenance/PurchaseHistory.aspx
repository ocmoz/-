<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_PurchaseHistory, App_Web_nuqbk7th" title="无标题页" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="PopupDiv" style="position: absolute; display: block; z-index: 100">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div style="width: 900px; height: 350px;">
            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc2:TabPanel runat="server" HeaderText="历史购买价格" ID="TabPanel1">
                    <ContentTemplate>
                        <table>
                            <tr align="center">
                                <td>
                                    <div style="width: 880px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
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
                                                <asp:BoundField DataField="PurchaseTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false"
                                                    HeaderText="购买时间">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ActualPrice" DataFormatString="{0:#,0.##}" HeaderText="实际购买价格">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Supplier" DataFormatString="{0:#,0.##}" HeaderText="供应商">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                此产品还没有采购历史
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
                        </table>
                    </ContentTemplate>
                </cc2:TabPanel>
                <cc2:TabPanel runat="server" HeaderText="实际价格曲线" ID="TabPanel2">
                <ContentTemplate>
                <div style="width: 900px; height: 700px;">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" DisplayGroupTree="False" runat="server" AutoDataBind="true" DisplayToolbar="False" PrintMode="ActiveX"   />
                </div>
                </ContentTemplate>
                </cc2:TabPanel>
            </cc2:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <iframe id="DivShim" src="javascript:false;" scrolling="no" frameborder="0" style="position: absolute;
        display: block;"></iframe>
        <script type="text/javascript" language="javascript">
        DivSetVisible();
        function DivSetVisible()
　　 {
　
　　 var DivRef = document.getElementById('PopupDiv');
　　 var IfrRef = document.getElementById('DivShim');
　
　　 DivRef.style.display = "block";
　　 IfrRef.style.width = DivRef.offsetWidth;
　　 IfrRef.style.height = DivRef.offsetHeight+10;
　　 IfrRef.style.top = DivRef.style.top;
　　 IfrRef.style.left = DivRef.style.left;
　　 IfrRef.style.zIndex = DivRef.style.zIndex - 1;
　　 IfrRef.style.display = "block";

　　
　　 }

    </script>
</asp:Content>
