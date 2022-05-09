<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="WorkflowEditor.aspx.cs" Inherits="Module_FM2E_WorkflowManager_WorkflowEditor_WorkflowEditor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Src="WorkflowStateViewer.ascx" TagName="WorkflowStateViewer" TagPrefix="uc1" %>
<%@ Register Src="EnumItemReorderList.ascx" TagName="EnumItemReorderList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <script type='text/javascript' src='WorkflowStateViewerJS.js'></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="工作流编辑"
        HeadHelpTxt="帮助" HeadOPTxt="编辑工作流的基本信息、流程和规则">
        <cc1:HeadMenuButtonItem ButtonIcon="list.gif" ButtonName="工作流列表" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="../WorkflowList.aspx" />
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrlType="href" ButtonUrl="../WorkflowList.aspx" />
    </cc1:HeadMenuWebControls>
    <div>
        <asp:Panel runat="server" ID="panel_WorkflowInfo">
            <asp:Table ID="Table2" runat="server" Width="250px" CellPadding="0" CellSpacing="0"
                border="1" BorderColor="#cccccc" Style="border-collapse: collapse;">
                <asp:TableRow>
                    <asp:TableCell CssClass="workflowTableTitle" colspan="4">
                        <asp:Button ID="btn_SaveAll" runat="server" Text="保存修改" CssClass="button_bak" OnClick="btn_SaveAll_Click" />&nbsp&nbsp
                        <asp:Button ID="btn_Withdraw" runat="server" Text="还原" CssClass="button_bak" OnClick="btn_Withdraw_Click"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="workflowTableTitle" colspan="4">
                        <asp:Label ID="lb_WorkflowName" runat="server" Text="WorkflowName"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell runat="server" CssClass="workflowTableBody">
                        工作流描述：
                    </asp:TableCell>
                    <asp:TableCell CssClass="workflowTableNone" Style="height: 30px">
                        <asp:TextBox ID="tb_WorkflowDescription" CssClass="workflowTableTextbox" runat="server"
                            OnTextChanged="tb_WorkflowDescription_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Table ID="table_StateList" runat="server" Width="250px" CellPadding="0" CellSpacing="0"
                        border="1" BorderColor="#cccccc" Style="border-collapse: collapse;">
                        <asp:TableRow>
                            <asp:TableCell CssClass="workflowTableTitle" colspan="4">
                        状态列表：            
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Table ID="table_EventList" runat="server" Width="250px" CellPadding="0" CellSpacing="0"
                        border="1" BorderColor="#cccccc" Style="border-collapse: collapse;">
                        <asp:TableRow>
                            <asp:TableCell CssClass="workflowTableTitle" colspan="4">
                        事件列表：            
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Table ID="table_DecimalList" runat="server" Width="250px" CellPadding="0" CellSpacing="0"
                        border="1" BorderColor="#cccccc" Style="border-collapse: collapse;">
                        <asp:TableRow>
                            <asp:TableCell CssClass="workflowTableTitle" colspan="4">
                        数值属性列表：            
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Table ID="table_EnumList" runat="server" Width="250px" CellPadding="0" CellSpacing="0"
                        border="1" BorderColor="#cccccc" Style="border-collapse: collapse;">
                        <asp:TableRow>
                            <asp:TableCell CssClass="workflowTableTitle" colspan="4">
                        枚举属性列表：            
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel runat="server" ID="panel_WorkflowContent" />
    </div>
    <style type="text/css">
        .workflowTableTitle
        {
            font-family: "Verdana, Arial, Helvetica, sans-serif";
            font-size: 12px;
            background: #efefef;
            height: 25px;
            color: #1e5494;
            font-weight: bolder;
            text-align: center;
            padding-left: 4px;
        }
        .workflowTableBody
        {
            font-size: 9pt;
            background: #efefef;
            height: 30px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            width: 40%;
            font-family: "Verdana" , "Arial" , "Helvetica" , "sans-serif";
            word-break: break-all;
        }
        .workflowTableNone
        {
            font-size: 9pt;
            height: 30px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            width: 60%;
            font-family: "Verdana" , "Arial" , "Helvetica" , "sans-serif";
        }
        .workflowTableTextbox
        {
            background: #ffffff;
            border: 1px solid #b7b7b7;
            color: #003366;
            cursor: text;
            font-family: "arial";
            font-size: 12px;
            height: 18px;
            padding: 1px;
            width: 130px;
        }
        .stateTitlePanel
        {
            font-family: "Verdana, Arial, Helvetica, sans-serif";
            font-size: 14px;
            background: #C4CAFF;
            height: 20px;
            text-align: left;
            padding-left: 4px;
            padding-top: 6px;
            color: #1e5494;
            font-weight: bolder;
            vertical-align: middle;
            width: 480px;
        }
        .stateTitleLabel
        {
            vertical-align: middle;
            font-weight: bold;
        }
        .stateContentPanel
        {
            font-family: "Verdana, Arial, Helvetica, sans-serif";
            font-size: 12px;
            font-weight: bolder;
            background: white;
            border-style: solid;
            border-color: #C4CAFF;
            border-width: 2px;
            width: 480px;
        }
        .setStateList
        {
            border-right: #000000 1px solid;
            border-top: #ffffff 1px solid;
            font-size: 12px;
            border-left: #ffffff 1px solid;
            color: #003366;
            border-bottom: #000000 1px solid;
            background-color: #f4f4f4;
        }
        .ruleBox
        {
            background: #ffffff;
            border: 1px solid #b7b7b7;
            color: #003366;
            cursor: text;
            font-family: "arial";
            font-size: 12px;
            height: 18px;
            padding: 1px;
            width: 200px;
        }
        .ruleEditorPanel
        {
            font-family: "Verdana, Arial, Helvetica, sans-serif";
            font-size: 14px;
            font-weight: bolder;
            background: white;
            border-style: solid;
            border-color: #C4CAFF;
            border-width: 2px;
            padding: 3px;
            text-align: right;
            width: 280px;
            height: 50px;
            line-height: 230%;
        }
        .ruleEditorBox
        {
            background: #ffffff;
            border: 1px solid #b7b7b7;
            color: #003366;
            cursor: text;
            font-family: "arial";
            font-size: 12px;
            height: 18px;
            padding: 1px;
            width: 274px;
        }
        .eventDrivenPanel
        {
            border-bottom-style: dashed;
            border-width: 2px;
            border-color: #C4CAFF;
        }
        .firstCell
        {
            width: 150px;
            text-align: center;
        }
        .secondCell
        {
            width: 50px;
            text-align: center;
        }
        .thirdCell
        {
            width: 240px;
            text-align: left;
        }
        .eventBlock
        {
            display: block;
        }
        .dragHandle
        {
            width: 22px;
            height: 22px;
            background: #6666FF;
            font-family: Arial ,宋体;
            font-weight: bold;
            font-size: 10pt;
            color: White;
            text-align: center;
            cursor: hand;
            border: none;
        }
        .workflowEnumItemBox
        {
            background: #ffffff;
            border: 1px solid #b7b7b7;
            color: #003366;
            cursor: text;
            font-family: "arial";
            font-size: 12px;
            height: 18px;
            padding: 1px;
            width: 80px;
        }
        ul
        {
            margin: 0;
            padding: 0;
            list-style: none;
        }
        li
        {
            margin: 0;
            padding: 0;
        }
    </style>
</asp:Content>
