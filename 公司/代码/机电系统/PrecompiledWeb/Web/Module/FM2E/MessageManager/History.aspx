<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_MessageManager_History, App_Web_qefdawc4" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="消息发送历史查看"
        HeadOPTxt="目前操作功能：消息发送历史">
        
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="接收消息" ButtonUrlType="Href"
            ButtonUrl="ViewMessage.aspx" />
        
        
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="发送消息" ButtonUrlType="Href"
            ButtonUrl="SendMessage.aspx" />
    </cc1:HeadMenuWebControls>
    <table id="RootTable" style="width: 100%; border-collapse: collapse; vertical-align: middle;
        text-align: left; text-indent: 13px; border: solid 1px #a7c5e2;" border="1" runat="server">
        <tr>
            <td class="Table_searchtitle" colspan="4">
                站内消息发送历史列表
            </td>
        </tr>
        <tr>
            <td class="table_none_NoWidth">
                <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                     <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>

                   <asp:GridView ID="gridview_MessageList" runat="server" AutoGenerateColumns="False" 
                        HeaderStyle-BackColor="#efefef" DataKeyNames="ID" HeaderStyle-Height="25px" RowStyle-Height="20px" Width="100%"
                        HeaderStyle-HorizontalAlign="center" RowStyle-HorizontalAlign="center" OnRowDataBound="gridview_MessageList_RowDataBound">
                        <Columns>
                            
                            
                            <asp:TemplateField HeaderText="标题">
                                <ItemTemplate>
                                    &nbsp;&nbsp;<a  style="color:Blue" href='ViewMessageContent.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ID") %>&cmd=history'><asp:Label Text='<%# Bind("Title") %>' runat="server" ID="Label_Title"></asp:Label></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="45%"  />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TypeString" HeaderText="类型">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SendWayString" HeaderText="发送方式">
                                <HeaderStyle />
                                <ItemStyle  Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MessageTime" HeaderText="发布时间">
                                <HeaderStyle />
                                <ItemStyle  Width="25%" />
                            </asp:BoundField>
                            
                            
                           
                        </Columns>
                        <RowStyle Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EFEFEF" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>
                    
                    <cc1:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged" 
                            CssClass="" CustomInfoClass="" CustomInfoHTML="总记录：0  页码：1/1  每页：10" 
                            InvalidPageIndexErrorMessage="页索引不是有效的数值！" NavigationToolTipTextFormatString="" 
                            PageIndexOutOfRangeErrorMessage="页索引超出范围！" ShowCustomInfoSection="Left">
                    </cc1:AspNetPager>
                                             
                         </ContentTemplate>
                         </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
