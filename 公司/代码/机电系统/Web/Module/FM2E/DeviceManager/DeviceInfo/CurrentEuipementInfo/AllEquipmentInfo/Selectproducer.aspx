<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Selectproducer.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_AllEquipmentInfo_Selectproducer" %>

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
                <div style="width: 900px; height: 340px;">
                    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc2:TabPanel runat="server" HeaderText="生产商列表" ID="TabPanel1">
                            <ContentTemplate>
                                <div style="width: 850px; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="checkBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProducerID" HeaderText="生产商号">
                                                <HeaderStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="名称">
                                                <HeaderStyle Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Credit" HeaderText="信用等级">
                                                <HeaderStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Phone" HeaderText="电话">
                                                <HeaderStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ResName" HeaderText="联系人">
                                                <HeaderStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ResPhone" HeaderText="联系人电话">
                                                <HeaderStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Address" HeaderText="地址">
                                                <HeaderStyle Width="80px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            没有生产商信息
                                        </EmptyDataTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                                        <RowStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:GridView>
                                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                        CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                                        NavigationToolTipTextFormatString="" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                                        ShowCustomInfoSection="Left">
                                    </cc1:AspNetPager>
                                </div>
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <input id="button2temp" class="button_bak" type="button" value="确定" onclick="javascript:onOk();" />
                                            <input id="Button3" class="button_bak" type="button" value="关闭" onclick="javascript:window.parent.hidePopWin();" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc2:TabPanel>
                        <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="生产商查询">
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
                                            生产商名称：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="table_body table_body_NoWidth" style="height: 30px">
                                            产品：
                                        </td>
                                        <td class="table_none table_none_NoWidth" style="height: 30px">
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table_body table_body_NoWidth">
                                            地址：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="table_body table_body_NoWidth">
                                            负责人：
                                        </td>
                                        <td class="table_none table_none_NoWidth">
                                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <tr>
                                            <td class="table_body table_body_NoWidth">
                                                信用等级：
                                            </td>
                                            <td class="table_none table_none_NoWidth">
                                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="table_body table_body_NoWidth">
                                            </td>
                                            <td class="table_none table_none_NoWidth">
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                                    runat="server">
                                    <tr id="Tr1" runat="server">
                                        <td id="Td1" align="right" style="height: 38px" runat="server">
                                            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                            <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                        </td>
                                    </tr>
                                </table>
                                <input id="SelectedID" runat="server" type="hidden" />
                                <input id="SelectedName" runat="server" type="hidden" />
                            </ContentTemplate>
                        </cc2:TabPanel>
                    </cc2:TabContainer>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="确定" OnClick="Button2_Click" />--%>
        <input type="button" style="display:none" runat="server" id="Button2" onserverclick="Button2_Click" />
    </div>
    <iframe id="DivShim" src="javascript:false;" scrolling="no" frameborder="0" style="position: absolute;
        display: block;"></iframe>

    <script type="text/javascript" language="javascript">
        DivSetVisible();
        function onClientClick(selectedId, name, id) {
            //用隐藏控件记录下选中的行号
            document.getElementById("<%=SelectedID.ClientID%>").value = id;
            document.getElementById("<%=SelectedName.ClientID%>").value = name;

            var inputs = document.getElementById("<%=GridView1.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (inputs[i].id == selectedId)
                        inputs[i].checked = true;
                    else
                        inputs[i].checked = false;

                }
            }
            DivSetVisible();
            
        }
        function selectOK() {

        }
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
　　  function onOk()
    {
        document.getElementById("<%=Button2.ClientID%>").click();
    }

    </script>

</asp:Content>
