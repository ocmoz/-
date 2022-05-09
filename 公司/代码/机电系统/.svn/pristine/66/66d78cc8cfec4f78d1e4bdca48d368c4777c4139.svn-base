<%@ Page Title="专项工程变更审批" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ModifyApproval.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectApproval_ModifyApproval_ModifyApproval" %>


<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>



    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--工程变更审批"
        HeadOPTxt="目前操作功能：专项工程变更审批" HeadHelpTxt="请按照顺序进行变更审批工作">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加变更" ButtonUrlType="Href"
            ButtonUrl="~/Module/FM2E/SpecialProject/ProjectManagement/Working/EditModify.aspx?cmd=new&projectid=" ButtonPopedom="New" />
    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <center>
        <span style="font-size:large">
            专项工程&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true"
                runat="server" ForeColor="Blue"></asp:Label>
            &nbsp;变更设计申请报告单 [  <asp:Label ID="Label_Status" runat="server"></asp:Label>]</span>
        </center>
        <br />
        项&nbsp;&nbsp;目： <br />
        承包人：（单位签章）
        <table width="100%" cellpadding="0" cellspacing="0" border="1"
            style="border-collapse: collapse;">
            <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                <th style="width: 10%; text-align: center">
                    变更工程名称
                </th>
                <td colspan="2" style="width: 40%;">
                    <asp:Label ID="Label_ProjectName2" runat="server"></asp:Label>
                </td>
                <th style="width: 10%; text-align: center">
                    申请日期
                </th>
                <td colspan="2" style="width: 40%; ">
                    <asp:Label ID="Label_ApplyTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 30px">
                <th style="width: 3%; text-align: center">
                    变更后金额<br />
                    （元）
                </th>
                <td>
                    <asp:Label ID="Label_BudgetChange"
                        runat="server">
                                    
                    </asp:Label>
                </td>
                <th style="width: 10%; text-align: center">
                    增减金额<br />
                    （元）
                </th>
                <td>
                    <asp:Label ID="Label_BudgetIncDesc"
                        runat="server">
                                    
                    </asp:Label>
                </td>
                <th style="width: 10%; text-align: center">
                    估计延长工期<br />
                    （天）
                </th>
                <td>
                    <asp:Label ID="Label_DelayDays"  runat="server">
                                    
                    </asp:Label>
                </td>
            </tr>
            <tr style="height: 30px">
                <td colspan="6">
                    变更设计原因变更设计原因及内容：<br />
                    <asp:Label ID="Label_ChangeContent"
                        runat="server" ></asp:Label><br />
                    附件：
                  
                    <asp:HyperLink ID="HyperLink_File" runat="server" ForeColor="Blue" Font-Underline="true" Visible="false"></asp:HyperLink>
                </td>
            </tr>
            <tr style="height: 30px">
                <th>
                    备注：</th>
                    <td colspan="5">
                    <asp:Label ID="Label_Remark2"  runat="server" 
                           ></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="word-break: break-all">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
                        style="border-collapse: collapse; background-color: white">
                        <tr style="height: 30px">
                            <th style="width: 3%;">
                                序号</th>
                                                        <th style="width: 7%;">
                                增加或减少设备
                            </th>
                            <th style="width: 10%;">
                                设备型号
                            </th>
                            <th style="width: 10%;">
                                数量（单位）
                            </th>
                            <th style="width: 10%;">
                                单价（元）
                            </th>
                            <th style="width: 10%;">
                                增加减少金额（元）
                            </th>
                            <th style="width: 15%;">
                                备注
                            </th>
 
                        </tr>
                        <asp:Repeater ID="Repeater_Detail" runat="server" >
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align: center">
                                        <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                    
                                        <input type="hidden" runat="server" value='<%# Eval("IsAdd") %>' id="Hidden_IsAdd" />
                                        <asp:Label ID="Label_IsAdded"  Font-Size="Larger" Text='<%# Eval("IsAddString") %>' runat="server"  ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_DeviceName" Text='<%# Eval("DeviceName") %>' runat="server">
                                    
                                        </asp:Label>
                                        &nbsp;
                                        <asp:Label ID="Label_Model" Text='<%# Eval("Model") %>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_Count" Text='<%# Eval("Count","{0:#,0.#####}") %>' runat="server">
                                    
                                        </asp:Label>
                                        &nbsp;
                                        （<asp:Label ID="Label_Unit" Text='<%# Eval("Unit") %>' runat="server">
                                    
                                        </asp:Label>）
                                    </td>
                                     <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_UnitPrice" Text='<%# Eval("UnitPrice","{0:#,0.#####}") %>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_IsAdded2" Text='<%# Eval("IsAddString") %>' runat="server"   ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                        </asp:Label><asp:Label ID="Label_Amount" Text='<%# Eval("Amount","{0:#,0.#####}") %>'  ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <td style="word-wrap: break-word; word-break: break-all;text-align: center">
                                        <asp:Label ID="Label_Remark" Text='<%# Eval("Remark") %>' runat="server">
                                        </asp:Label>
                                    </td>
                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <tr style="height: 30px">
                            <th  colspan="5">
                                合计
</th>
                            <th align="center">
                               <asp:Label ID="Label_TotalAmount" runat="server">
                                    
                                        </asp:Label>
                            </th>
                            <th colspan="3">
                                
                            </th>

                        </tr>
                        
                        
                    </table>
                </td>
            </tr>
            <tr style="height: 30px">
                <th>
                    业主代表意见：
                </th>
                <td colspan="5">
                    
                    <div id="div_ownerapproval" runat="server">
                    <asp:DropDownList ID="DropDownList_Owner" runat="server">
                    
                    </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="TextBox_OwnerFeeBack" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox>
                    <br />
                    <span style="float:right;">部门负责人：<asp:TextBox ID="TextBox_Owner" runat="server"></asp:TextBox></span>
                    </div>
                    <div id="div_ownerapprovalinfo" runat="server">
                    审批结果：<asp:Label ID="Label_OwnerApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_OwnerFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">部门负责人：
                    <asp:Label ID="Label_Owner" runat="server"></asp:Label>
                    <br />
                    日期：<asp:Label ID="Label_OwnerTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <th >
                    合约部审核：
                </th>
                <td colspan="5">
                    
                    <div id="div_contractapproval" runat="server">
                    <asp:DropDownList ID="DropDownList_Contract" runat="server">
                    
                    </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="TextBox_ContractFeeBack" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox>
                    <br />
                    <span style="float:right;">合约部负责人：<asp:TextBox ID="TextBox_Contract" runat="server"></asp:TextBox></span>
                    </div>
                    <div id="div_contractapprovalinfo" runat="server">
                    审批结果：<asp:Label ID="Label_ContractApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_ContractFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">合约部负责人：
                    <asp:Label ID="Label_Contract" runat="server"></asp:Label>
                    <br />
                    日期：<asp:Label ID="Label_ContractTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <th>
                    领导审批：
                </th>
                <td colspan="5">
                    
                    <div id="div_leaderapproval" runat="server">
                    <asp:DropDownList ID="DropDownList_Leader" runat="server">
                    
                    </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="TextBox_LeaderFeeBack" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox>
                    <br />
                    <span style="float:right;">领导：<asp:TextBox ID="TextBox_Leader" runat="server"></asp:TextBox></span>
                    </div>
                    <div id="div_leaderapprovalinfo" runat="server">
                    审批结果：<asp:Label ID="Label_LeaderApproval" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label_LeaderFeeBack" runat="server"></asp:Label>
                    <br />
                    <span style="float:right;">领导：
                    <asp:Label ID="Label_Leader" runat="server"></asp:Label>
                    <br />
                    日期：<asp:Label ID="Label_LeaderTime" runat="server"></asp:Label>
                    </span>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <td colspan="6" align="right">
                    <asp:Button ID="Button_SaveModify" runat="server" Text="提交" 
                        CssClass="button_bak" onclick="Button_SaveModify_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
