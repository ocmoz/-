<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Module_FM2E_ConsumablesManager_ConsumablesTypeManager_ConsumablesTypeList, App_Web_hf-63gyd" title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="易耗品种类管理"
        HeadOPTxt="目前操作功能：易耗品种类管理" HeadHelpTxt="种类列表按最新添加种类顺序排列">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加易耗品种类" ButtonPopedom="New"
            ButtonVisible="true" ButtonUrlType="href" ButtonUrl="ConsumablesTypeEdit.aspx?cmd=add" />
    </cc1:HeadMenuWebControls>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table style="width: 99%;">
        <tr>
            <td style="width: 15%" align="left" valign="top">
                <div style="width: 100px; overflow: scroll; overflow: hidden">
                    <asp:TreeView ID="TreeView1" runat="server">
                    </asp:TreeView>
                </div>
            </td>
            <td style="width: 85%" align="left" valign="top">
                <div style="width: 100%; height: 100%;">
                    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc2:TabPanel runat="server" HeaderText="易耗品种类列表" ID="TabPanel1">
                            <ContentTemplate>
                            </ContentTemplate>
                        </cc2:TabPanel>
                    </cc2:TabContainer>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
