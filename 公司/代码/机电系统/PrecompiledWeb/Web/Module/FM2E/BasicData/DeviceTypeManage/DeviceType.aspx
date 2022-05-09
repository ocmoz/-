<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_BasicData_DeviceTypeManage_DeviceType, App_Web_etjlb33o" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备种类信息维护"
        HeadOPTxt="目前操作功能：设备种类信息维护" HeadHelpTxt="设备种类列表默认显示新增设备种类">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加设备种类" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="EditDeviceType.aspx?cmd=add" />
       <%-- <cc1:HeadMenuButtonItem ButtonIcon="move.gif" ButtonName="导入设备种类信息" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="Import.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="xls.gif" ButtonName="导出结果" ButtonPopedom="Print"
            ButtonVisible="true" ButtonUrlType="Href" ButtonUrl="?cmd=export" />--%>
            
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <iframe style="Z-INDEX:-1;WIDTH:99%;POSITION:absolute;TOP:0px;" frameborder="0"></iframe>
        <div id="PopupDiv" style="position: absolute; display: block; z-index: 100">
            <table style="width: 99%;">
                <tr>
                    <td style="width: 15%" align="left" valign="top">
                        <div style="width:100px; overflow:scroll;overflow:hidden">
                            <asp:TreeView ID="TreeView1" runat="server">
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="width:85%" align="left" valign="top">
                        <div style="width: 100%; height: 100%;">
                            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                                <cc2:TabPanel runat="server" HeaderText="设备种类列表" ID="TabPanel1">
                                    <ContentTemplate>
                                        <div id="PrintDiv" style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#efefef"
                                                HeaderStyle-Height="25px" RowStyle-Height="20px" OnRowCommand="GridView1_RowCommand"
                                                HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="CategoryID" HeaderText="种类编码">
                                                        
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CategoryName" HeaderText="种类名称">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Unit" HeaderText="设备单位">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ParentName" HeaderText="上一级种类">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DepreciableLife" HeaderText="折旧年限">
                                                      
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ResidualRate" DataFormatString="{0:#,0.##}" HeaderText="净残值率(%)">
                                                       
                                                    </asp:BoundField>
                                                    <asp:ButtonField ButtonType="Image" Text="查看" ImageUrl="~/images/ICON/select.gif"
                                                        HeaderText="查看" CommandName="view">
                                                     
                                                    </asp:ButtonField>
                                                    <asp:TemplateField>
                                                     <HeaderTemplate>
                                                        删除
                                                     </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/delete.gif" CommandName="del"
                                                                CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('确认要删除此设备种类信息吗？')"
                                                                CausesValidation="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    没有设备种类信息
                                                </EmptyDataTemplate>
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <cc1:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                AlwaysShow="True" CloneFrom="" CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10"
                                                InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString=""
                                                PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                                            </cc1:AspNetPager>
                                        </div>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" HeaderText="设备种类查询" ID="TabPanel2">
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
                                                    种类编码：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <input id="zhongleibianma" runat="server" type="text" />
                                                </td>
                                                <td class="table_body table_body_NoWidth" style="height: 30px">
                                                    种类名称：
                                                </td>
                                                <td class="table_none table_none_NoWidth" style="height: 30px">
                                                    <input id="zhongleiming" runat="server" type="text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    设备单位：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input id="shebeidanwei" runat="server" type="text" />
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    上一级种类：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input id="shangjizhonglei" runat="server" type="text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    折旧方法：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    
                                                    <asp:DropDownList ID="zhejiufangfa" runat="server">
                                                    <asp:ListItem Value="0" Selected="True" Text="--请选择--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="不折旧"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="直线折旧"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="双倍余额"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                    折旧年限：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input size="5" id="zhejiunianxian1" runat="server" type="text" />年&nbsp;至&nbsp;
                                                    <input size="5" id="zhejiunianxian2" runat="server" type="text" />年
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_body table_body_NoWidth">
                                                    净残值率：
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                    <input size="5" id="jinchangzhilv1" runat="server" type="text" />&nbsp;至&nbsp;
                                                    <input size="5" id="jinchangzhilv2" runat="server" type="text" />
                                                </td>
                                                <td class="table_body table_body_NoWidth">
                                                </td>
                                                <td class="table_none table_none_NoWidth">
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton"
                                            runat="server">
                                            <tr>
                                                <td align="center" style="height: 38px">
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                            </cc2:TabContainer>
                        </div>
                    </td>
                </tr>
            </table>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
