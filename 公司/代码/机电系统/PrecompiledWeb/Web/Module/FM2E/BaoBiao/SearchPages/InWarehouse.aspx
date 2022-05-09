<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" enableeventvalidation="false" inherits="Module_FM2E_ArchivesManager_SearchPages_InWarehouse, App_Web_a05llto4" title="Untitled Page" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="设备入库信息档案信息查询"
        HeadOPTxt="目前操作功能：档案信息查询">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回申请表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="" />
    </cc1:HeadMenuWebControls>
    <div style="width: 100%;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="入库列表" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                            Width="100%" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkBox1" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <input id="CheckAll" runat="server" type="checkbox" onclick="selectAll(this);" />本页全选
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SheetName" HeaderText="表单号"></asp:BoundField>
                                <asp:BoundField DataField="WarehouseName" HeaderText="仓库"></asp:BoundField>
                                <asp:BoundField DataField="CompanyName" HeaderText="公司"></asp:BoundField>
                                <asp:BoundField DataField="DepartmentName" HeaderText="入库部门"></asp:BoundField>
                                
                                <asp:BoundField DataField="ApplicantName" HeaderText="经办人"></asp:BoundField>
                                <asp:BoundField DataField="OperatorName" HeaderText="仓管员"></asp:BoundField>
                                <asp:BoundField DataField="SubmitTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false" HeaderText="入库时间"></asp:BoundField>
                                <asp:TemplateField HeaderText="查看">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ICON/select.gif"
                                            CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false"
                                            Visible='<%#IsBorrowed(Convert.ToInt64(Eval("ID")))==true?true:false%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                没有入库信息
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" BackColor="#EFEFEF" Height="25px" />
                            <RowStyle HorizontalAlign="Center" Height="20px" />
                        </asp:GridView>
                        <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" InvalidPageIndexErrorMessage="页索引不是有效的数值！"
                            NavigationToolTipTextFormatString="{0}" PageIndexOutOfRangeErrorMessage="页索引超出范围！"
                            ShowCustomInfoSection="Left" CloneFrom="">
                        </cc1:AspNetPager>
                    </div>
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="Table1"
                        runat="server">
                        <tr>
                            <td align="right" style="height: 38px">
                                <asp:Label ID="lbErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:Button ID="BtnBorrow" runat="server" CssClass="button_bak" Text="申请借阅" OnClick="BtnBorrow_Click" />
                                <asp:Button ID="BtnDestroy" runat="server" CssClass="button_bak" Text="申请销毁" OnClick="BtnDestroy_Click" />
                                <input type="hidden" runat="server" id="addstring" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="入库查询" ID="TabPanel2">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                        style="border-collapse: collapse;">
                        <tr>
                            <td class="Table_searchtitle" colspan="4">
                                组合查询（支持模糊查询）
                            </td>
                        </tr>
                        <tr>
                         <td class="table_body table_body_NoWidth">
                                表单编号：
                            </td>
                            <td class="table_none table_none_NoWidth" colspan="3">
                                <asp:TextBox ID="TextBox_SheetID" runat="server"></asp:TextBox>
                            </td>  </tr>
                        <tr>
                            <td class="table_body table_body_NoWidth">
                                入库部门：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DropDownList_Department" runat="server"></asp:DropDownList>
                            </td>
                      
                        
                         <td class="table_body table_body_NoWidth">
                                仓库：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:DropDownList ID="DropDownList_Warehouse" runat="server"></asp:DropDownList>
                            </td>
                           
                       
                        </tr>
                         <tr>
                         <td class="table_body table_body_NoWidth">
                                经办人：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_ApplicantName" runat="server"></asp:TextBox>
                            </td>
                            <td class="table_body table_body_NoWidth">
                                仓管员：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                 <asp:TextBox ID="TextBox_WarehouseKeeperName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                         <td class="table_body table_body_NoWidth">
                                入库时间：
                            </td>
                            <td class="table_none table_none_NoWidth">
                                <asp:TextBox ID="TextBox_TimeLower" runat="server" CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>至
                                <asp:TextBox ID="TextBox_TimeUpper" runat="server"  CssClass="input_calender" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                            </td>
                           
                        </tr>
                      
                    </table>
                    <center>
                                <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />&nbsp;&nbsp;
                                <input id="Reset1" class="button_bak" type="reset" value="重填" />
                            </center>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>

    <script type="text/javascript" language="javascript">
    function selectAll(obj)
       {
           var theTable = obj.parentElement.parentElement.parentElement;
           var i;
           var j = obj.parentElement.cellIndex;
        
           for(i=0;i<theTable.rows.length;i++)
           {
               var objCheckBox = theTable.rows[i].cells[j].firstChild;
               if(objCheckBox.checked!=null)objCheckBox.checked = obj.checked;
           }
       }
    </script>

</asp:Content>
