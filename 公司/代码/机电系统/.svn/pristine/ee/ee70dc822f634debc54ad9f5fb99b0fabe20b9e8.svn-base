<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkFlowUserSelectControl.ascx.cs" Inherits="control_WorkFlowUserSelectControl" %>
<asp:UpdatePanel ID="UpdatePanelSelectWFUser" runat="server">
<ContentTemplate>
    <asp:RadioButtonList ID="RadioButtonList_Events" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"
        AutoPostBack="true" onclick="javascript:CheckButtonList();"
       OnSelectedIndexChanged="RadioButtonList_Events_SelectedChanged">
    </asp:RadioButtonList>
    <div id="div_selectuser" runat="server" visible="false">
<span id="span_company" runat="server">公司：<asp:DropDownList ID="DropDownList_Company" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Company_SelectedChanged" runat="server"></asp:DropDownList></span>
部门：<asp:DropDownList ID="DropDownList_Department" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Department_SelectedChanged"></asp:DropDownList>
用户：<asp:DropDownList ID="DropDownList_User" runat="server"></asp:DropDownList></div>
<input id="clickyet" type="hidden" runat="server" />
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript" language="javascript">
    function CheckButtonList() {
        document.getElementById("<%=clickyet.ClientID%>").value = "true";
    }
    </script>