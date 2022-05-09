<%@ control language="C#" autoeventwireup="true" inherits="EnumItemReorderList, App_Web_lyncxsub" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

        <asp:HiddenField ID="field_EnumName" runat="server" />
        <asp:HiddenField ID="field_ParserGuid" runat="server" />
        <cc1:ReorderList ID="list_EnumItemList" runat="server" AllowReorder="True" PostBackOnReorder="True" SortOrderField="Order" OnItemReorder="list_EnumItemList_ItemReorder" >
            <ItemTemplate >
                   <asp:TextBox ID="Label2" runat="server" Text='<%# Eval("Description") %>'   AutoPostBack ="true" OnTextChanged="ItemDescription_TextChanged" CssClass="workflowEnumItemBox" ItemValue='<%# Eval("Value") %>' OldValue='<%# Eval("Description") %>' onkeydown="return limitInput();"/>
                   &nbsp
                <asp:ImageButton ID="ibtn_DelItem" runat="server"  ImageUrl ="~/images/WorkflowButton/Button_LittleDelete.png" ToolTip="删除"  OnClick="ibtn_DelItem_Click" ItemValue='<%# Eval("Value") %>' />
            </ItemTemplate>
            <DragHandleTemplate>
                <asp:Panel ID="Panel2" runat="server" CssClass="dragHandle" ToolTip ="上下拖动可改变顺序">
                      <asp:Label ID="Label1" runat="server" Text='<%# Eval("Value") %>' Font-Bold="True"/>
                </asp:Panel>
            </DragHandleTemplate>
        </cc1:ReorderList>