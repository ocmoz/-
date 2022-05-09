<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ExpendableApproval.aspx.cs" Inherits="Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ExpendableApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="公司信息维护"
        HeadOPTxt="目前操作功能：出入库数据审批">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <script type="text/javascript" language="javascript">
function addtosessionfun(id,value)
{
if(!checkForm(document.forms[0],causeValidate))
{
    document.getElementById(id).value = document.getElementById(id+"oldvalue").value;
    return false;
}
document.getElementById('<%= oldvalue.ClientID %>').value = document.getElementById(id+"oldvalue").value;
document.getElementById('<%= newvalue.ClientID %>').value = value;
document.getElementById('<%= expendableid.ClientID %>').value = document.getElementById(id+"val").value;
document.getElementById('<%= expendablename.ClientID %>').value = document.getElementById(id+"name").value;
document.getElementById('<%= expendabletype.ClientID %>').value = document.getElementById(id+"Type").value;
document.getElementById('<%= expendableclick.ClientID %>').click();
}
function recordoldvalue(id,oldvalue)
{
document.getElementById(id+"oldvalue").value = oldvalue;
}
    </script>

    <input id="oldvalue" runat="server" type="hidden" />
    <input id="newvalue" runat="server" type="hidden" />
    <input id="expendablename" runat="server" type="hidden" />
    <input id="expendabletype" runat="server" type="hidden" />
    <input id="expendableid" runat="server" type="hidden" />
    <div style="display: none">
        <asp:Button ID="expendableclick" runat="server" OnClick="Button1_Click" /></div>
    <table width="100%">
        <tr>
            <td colspan="4">
                <asp:Label ID="sheetname" runat="server" />
                <input id="sheettime" type="hidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView AutoGenerateColumns="False" Width="100%" ID="recordlist" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="物品名称">
                            <ItemTemplate>
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="25%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请时间">
                            <ItemTemplate>
                                <asp:Label ID="InOutTime" runat="server" Text='<%# Eval("InOutTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓管人">
                            <ItemTemplate>
                                <asp:Label ID="WarehousekeeperName" runat="server" Text='<%# Eval("WarehousekeeperName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="经手人">
                            <ItemTemplate>
                                <asp:Label ID="ReceiverName" runat="server" Text='<%# Eval("ReceiverName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出入库类型">
                            <ItemTemplate>
                                <asp:Label ID="Typestr" runat="server" Text='<%# Eval("Typestr") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出入库数量">
                            <ItemTemplate>
                                <input id="Amountstr" runat="server" value='<%# Eval("Amountstr") %>' title="请输入要修正的数额~:int"
                                    onfocus="recordoldvalue(this.id,this.value);" onchange='return addtosessionfun(this.id,this.value);' />
                                <input id="Amountstrval" runat="server" type="hidden" value='<%# Eval("ID") %>' />
                                <input id="Amountstrname" runat="server" type="hidden" value='<%# Eval("Name") %>' />
                                <input id="AmountstrType" runat="server" type="hidden" value='<%# Eval("Typestr") %>' />
                                <input id="Amountstroldvalue" runat="server" type="hidden" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位">
                            <ItemTemplate>
                                <asp:Label ID="Unit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="价格">
                            <ItemTemplate>
                                <asp:Label ID="Price" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                行政业务意见:
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="xinzhenyewu" runat="server" title="请选择意见">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">同意</asp:ListItem>
                    <asp:ListItem Value="2">不同意</asp:ListItem>
                    <asp:ListItem Value="3">其它</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="table_body table_body_NoWidth">
                综合事务意见
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="zongheshiwu" runat="server" title="请选择意见">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">同意</asp:ListItem>
                    <asp:ListItem Value="2">不同意</asp:ListItem>
                    <asp:ListItem Value="3">其它</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                计划财务意见:
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="jihuacaiwu" runat="server" title="请选择意见">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">同意</asp:ListItem>
                    <asp:ListItem Value="2">不同意</asp:ListItem>
                    <asp:ListItem Value="3">其它</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="table_body table_body_NoWidth">
                分管领导意见
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="fenguanlingdao" runat="server" title="请选择意见">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">同意</asp:ListItem>
                    <asp:ListItem Value="2">不同意</asp:ListItem>
                    <asp:ListItem Value="3">其它</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="table_body table_body_NoWidth">
                总经理意见:
            </td>
            <td class="table_none table_none_NoWidth">
                <asp:DropDownList ID="zongjingli" runat="server" title="请选择意见">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">同意</asp:ListItem>
                    <asp:ListItem Value="2">不同意</asp:ListItem>
                    <asp:ListItem Value="3">其它</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="savebutton" runat="server" Text="确定" OnClick="savebutton_Click" />
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
