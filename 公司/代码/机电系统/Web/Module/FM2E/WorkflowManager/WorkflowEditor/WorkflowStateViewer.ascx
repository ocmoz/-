<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkflowStateViewer.ascx.cs"
    Inherits="WorkflowStateViewer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Panel ID="panel_StateViewer" runat="server">
    <asp:Panel ID="panel_StateDescription" runat="server" CssClass="stateTitlePanel">
        <asp:Label ID="lb_StateDescription" runat="server" Text="" CssClass="stateTitleLabel" />
    </asp:Panel>
    <asp:UpdatePanel ID="upanel_StatePanel" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="panel_Content" runat="server" CssClass="stateContentPanel" />
            <asp:HiddenField ID="field_DisplayMode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc2:DragPanelExtender ID="DragExtender1" runat="server" DragHandleID="panel_StateDescription"
        TargetControlID="panel_StateViewer" />
</asp:Panel>
