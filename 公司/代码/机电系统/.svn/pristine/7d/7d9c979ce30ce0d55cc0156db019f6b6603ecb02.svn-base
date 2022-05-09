<%@ Page Title="专项工程变更" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="ModifyList.aspx.cs" Inherits="Module_FM2E_SpecialProject_ProjectManagement_Working_ModifyList" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
    <link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript">
        function HideShowForm(a) {
            var fm = document.getElementById(a.id.replace('a_hideshow', 'tr_form'));
            if (fm.style.display == 'block') {
                a.innerHTML = a.innerHTML.replace('收缩', '展开');
                fm.style.display = 'none';
            }
            else {
                a.innerHTML = a.innerHTML.replace('展开', '收缩');
                fm.style.display = 'block';
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="专项工程管理--施工管理"
        HeadOPTxt="目前操作功能：工程变更" HeadHelpTxt="工程变更列表，点击可以进行展开收缩操作，也可以添加新的变更情况">
        <cc1:HeadMenuButtonItem ButtonIcon="new.gif" ButtonName="添加变更" ButtonUrlType="Href"
            ButtonUrl="ModifyEdit.aspx?cmd=new&projectid=" ButtonPopedom="New" />
            <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="Href"
            ButtonUrl="ViewProject.aspx?cmd=edit&projectid=" ButtonPopedom="Edit" />
    </cc1:HeadMenuWebControls>
    <div id="div_table">
        <center>
            专项工程&nbsp;<asp:Label ID="Label_ProjectName" Font-Underline="true" Font-Bold="true"
                runat="server" ForeColor="Blue"></asp:Label>
            &nbsp;变更设计申请报告单
        </center>
        <asp:Repeater ID="Repeater_ModifyList" runat="server">
            <ItemTemplate>
                <br />
                <table border="1pt" style="border-collapse: collapse" style="width: 100%">
                    <tr>
                        <th align="left">
                            <span><a href="javascript:void(0)" onclick="javascript:HideShowForm(this);" runat="server"
                                id="a_hideshow">
                                <asp:Label ID="Label_ApplyDateTitle" Text='<%# Eval("ApplyTime","{0:yyyy-MM-dd}") %>'
                                    Font-Underline="true" ForeColor="Blue" runat="server"></asp:Label><asp:Label ID="Label_HideShow"
                                        Text='<%# Container.ItemIndex==0? "[收缩申报单]" : "[展开申报单]" %>' Font-Underline="true"
                                        ForeColor="Blue" runat="server"></asp:Label><asp:Label ID="Label_StatusString" Text='<%# Eval("StatusString") %>'
                                            Font-Underline="true" ForeColor="Blue" runat="server"></asp:Label></a></span>
                            <span style="float: right">
                                <asp:HyperLink ID="HyperLinkEdit" Font-Underline="true" ForeColor="Blue" runat="server"
                                    Text="修改" NavigateUrl='<%# "EditModify.aspx?cmd=edit&projectid="+Eval("ProjectID").ToString()+"&modifyid="+ Eval("ModifyID").ToString()%>'></asp:HyperLink>
                                <asp:HyperLink ID="HyperLinkApproval" Font-Underline="true" ForeColor="Blue" runat="server"
                                    Text="审批" NavigateUrl='<%# "~/Module/FM2E/SpecialProject/ProjectApproval/ModifyApproval/ModifyApproval.aspx?cmd=approval&projectid="+Eval("ProjectID").ToString()+"&modifyid="+ Eval("ModifyID").ToString()%>'></asp:HyperLink></span>
                        </th>
                    </tr>
                    <tr id="tr_form" runat="server" style='<%# Container.ItemIndex==0? "display:block": "display:none" %>'>
                        <td>
                            <center style="font-size: medium; font-weight: bold">
                                工程变更设计申请报告单</center>
                            <br />
                            项&nbsp;&nbsp;目：
                            <br />
                            承包人：（单位签章）
                            <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc"
                                style="border-collapse: collapse;">
                                <tr style="background-color: #EFEFEF; font-weight: bold; height: 30px;">
                                    <th style="width: 10%; text-align: center">
                                        变更工程名称
                                    </th>
                                    <td colspan="2">
                                        <asp:Label ID="Label_ProjectName" runat="server"></asp:Label>
                                    </td>
                                    <th style="width: 10%; text-align: center">
                                        申请日期
                                    </th>
                                    <td colspan="2">
                                        <asp:Label ID="Label_ApplyTime" Text='<%# Eval("ApplyTime","{0:yyyy-MM-dd}") %>'
                                            runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <th style="width: 3%; text-align: center">
                                        变更后金额<br />
                                        （元）
                                    </th>
                                    <td>
                                        <asp:Label ID="Label_BudgetChange" Text='<%# Eval("BudgetChange","{0:#,0.###}") %>'
                                            runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <th style="width: 10%; text-align: center">
                                        增减金额<br />
                                        （元）
                                    </th>
                                    <td>
                                        <asp:Label ID="Label_BudgetIncDesc" Text='<%# Eval("BudgetIncDesc","{0:#,0.###}") %>'
                                            runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                    <th style="width: 10%; text-align: center">
                                        估计延长工期<br />
                                        （天）
                                    </th>
                                    <td>
                                        <asp:Label ID="Label_DelayDays" Text='<%# Eval("DelayDays") %>' runat="server">
                                    
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td colspan="6">
                                        变更设计原因及内容：<br />
                                        <asp:Label ID="Label_ChangeContent" Text='<%# Eval("ChangeContent") %>' runat="server">
                                    
                                        </asp:Label><br />
                                        附件：
                                        <asp:HyperLink ID="HyperLink_File" runat="server" ForeColor="Blue" Font-Underline="true"
                                            Text='<%# Eval("ContentAttechment") %>' NavigateUrl='<%# WebUtility.SystemConfig.Instance.UploadPath + UPLOADFOLDER + Eval("ContentAttechment").ToString() %>'></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                <th>
                    备注：</th>
                    <td colspan="5">
                    <asp:Label ID="Label_Remark2"  runat="server"  Text='<%# Eval("Remark") %>'
                           ></asp:Label>
                </td>
            </tr>
                                <tr>
                                    <td colspan="10" style="word-break: break-all">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="#000000"
                                            style="border-collapse: collapse; background-color: white">
                                            <tr style="height: 30px">
                                                <th style="width: 3%;">
                                                    序号
                                                </th>
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
                                            <asp:Repeater ID="Repeater_Detail" runat="server" DataSource='<%# Eval("DetailList") %>'>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label_Index" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                        </td>
                                                        <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                            <input type="hidden" runat="server" value='<%# Eval("IsAdd") %>' id="Hidden_IsAdd" />
                                                            <asp:Label ID="Label_IsAdded" Font-Size="Larger" Text='<%# Eval("IsAddString") %>'
                                                                runat="server" ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                                            </asp:Label>
                                                        </td>
                                                        <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                            <asp:Label ID="Label_DeviceName" Text='<%# Eval("DeviceName") %>' runat="server">
                                    
                                                            </asp:Label>
                                                            &nbsp;
                                                            <asp:Label ID="Label_Model" Text='<%# Eval("Model") %>' runat="server">
                                    
                                                            </asp:Label>
                                                        </td>
                                                        <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                            <asp:Label ID="Label_Count" Text='<%# Eval("Count","{0:#,0.#####}") %>' runat="server">
                                    
                                                            </asp:Label>
                                                            &nbsp; （<asp:Label ID="Label_Unit" Text='<%# Eval("Unit") %>' runat="server">
                                    
                                                            </asp:Label>）
                                                        </td>
                                                        <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                            <asp:Label ID="Label_UnitPrice" Text='<%# Eval("UnitPrice","{0:#,0.#####}") %>' runat="server">
                                    
                                                            </asp:Label>
                                                        </td>
                                                        <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                            <asp:Label ID="Label_IsAdded2" Text='<%# Eval("IsAddString") %>' runat="server" ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                                            </asp:Label><asp:Label ID="Label_Amount" Text='<%# Eval("Amount","{0:#,0.#####}") %>'
                                                                ForeColor='<%# Convert.ToBoolean(Eval("IsAdd"))?System.Drawing.Color.Red:System.Drawing.Color.Green%>'
                                                                runat="server">
                                    
                                                            </asp:Label>
                                                        </td>
                                                        <td style="word-wrap: break-word; word-break: break-all; text-align: center">
                                                            <asp:Label ID="Label_Remark" Text='<%# Eval("Remark") %>' runat="server">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr style="height: 30px">
                                                <th colspan="5">
                                                    合计
                                                </th>
                                                <th align="center">
                                                    <asp:Label ID="Label_TotalAmount" runat="server" Text='<%# (Convert.ToDecimal(Eval("TotalAmountFromDetail"))>=0? "+":"-")+Eval("TotalAmountFromDetail","{0:#,0.##}").ToString() %>'
                                                        ForeColor='<%# Convert.ToDecimal(Eval("TotalAmountFromDetail"))>0?System.Drawing.Color.Red:System.Drawing.Color.Green%>'>
                                    
                                                    </asp:Label>
                                                </th>
                                                <th>
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
                                        <div id="div_ownerapprovalinfo" runat="server">
                                            审批结果：<asp:Label ID="Label_OwnerApproval" runat="server" Text='<%# Eval("OwnerResultString") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="Label_OwnerFeeBack" runat="server" Text='<%# Eval("OwnerFeeBack") %>'></asp:Label>
                                            <br />
                                            <span style="float: right;">部门负责人：
                                                <asp:Label ID="Label_Owner" runat="server" Text='<%# Eval("OwnerApprovaler") %>'></asp:Label>
                                                <br />
                                                日期：<asp:Label ID="Label_OwnerTime" runat="server" Text='<%# ((int)Eval("OwnerResult")) == 0? "": ((DateTime)Eval("OwnerApprovalDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <th>
                                        合约部审核：
                                    </th>
                                    <td colspan="5">
                                        <div id="div_contractapprovalinfo" runat="server">
                                            审批结果：<asp:Label ID="Label_ContractApproval" runat="server" Text='<%# Eval("ContractResultString") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="Label_ContractFeeBack" runat="server" Text='<%# Eval("ContractFeeBack") %>'></asp:Label>
                                            <br />
                                            <span style="float: right;">合约部负责人：
                                                <asp:Label ID="Label_Contract" runat="server" Text='<%# Eval("ContractApprovaler") %>'></asp:Label>
                                                <br />
                                                日期：<asp:Label ID="Label_ContractTime" runat="server" Text='<%# ((int)Eval("ContractResult")) == 0? "": ((DateTime)Eval("ContractApprovalDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <th>
                                        领导审批：
                                    </th>
                                    <td colspan="5">
                                        <div id="div_leaderapprovalinfo" runat="server">
                                            审批结果：<asp:Label ID="Label_LeaderApproval" runat="server" Text='<%# Eval("LeaderResultString") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="Label_LeaderFeeBack" runat="server" Text='<%# Eval("LeaderFeeBack") %>'></asp:Label>
                                            <br />
                                            <span style="float: right;">领导：
                                                <asp:Label ID="Label_Leader" runat="server" Text='<%# Eval("LeaderApprovaler") %>'></asp:Label>
                                                <br />
                                                日期：<asp:Label ID="Label_LeaderTime" runat="server" Text='<%# ((int)Eval("LeaderResult")) == 0? "": ((DateTime)Eval("LeaderApprovalDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
