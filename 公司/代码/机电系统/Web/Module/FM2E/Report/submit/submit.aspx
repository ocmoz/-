<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="submit.aspx.cs" Inherits="Module_FM2E_Report_Import_EmployeeImport" %>

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script><script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script><link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" /><link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" /><script type="text/javascript" language="javascript">
     </script><asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="业务线提交"
        HeadOPTxt="目前操作功能：业务线管理">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="JavaScript"
            ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <asp:Panel ID="Panel1" runat="server" Style="display: none; width: 300px">
        <asp:Image ID="Image1" runat="server" Width="300px" />
    </asp:Panel>

    
    <div style="width: 100%; height: 229px;">
            
            
                 	
                 	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="yingxiao" runat="server" Text="营销月报提交"  Font-Size="Medium"   OnClientClick="return confirm('提示：是否要提交营销业务线！')"
                    OnClick="yingxiao_Click" Height="30px" 
                    Width="113px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button10" runat="server" Text="营销月报撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交营销业务线！')"
                    OnClick="Button10_Click" Height="30px" 
                    Width="113px" />
                  <br />
                  <br />
                  
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="yuangong" runat="server" Text="员工关系提交" Font-Size="Medium"   OnClientClick="return confirm('提示：是否要提交员工关系业务线！')"
                    OnClick="yuangong_Click" Height="30px" 
                    Width="113px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button1" runat="server" Text="员工关系撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交员工关系业务线！')"
                    OnClick="Button1_Click" Height="30px" 
                    Width="113px" />
                  <br />
                  <br />

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="dudao" runat="server" Text="督导信息提交" Font-Size="Medium"   OnClientClick="return confirm('提示：是否要提交督导信息业务线！')"
                    OnClick="dudao_Click" Height="30px" 
                    Width="113px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button2" runat="server" Text="督导信息撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交督导信息业务线！')"
                    OnClick="Button2_Click" Height="30px" 
                    Width="113px" />
                  <br />
                  <br />

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="houqin" runat="server" Text="后勤月报提交" Font-Size="Medium"   OnClientClick="return confirm('提示：是否要提交后勤业务线！')"
                    OnClick="houqin_Click" Height="30px" 
                    Width="113px" />    
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button3" runat="server" Text="后勤月报撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交后勤月报业务线！')"
                    OnClick="Button3_Click" Height="30px" 
                    Width="113px" />              
                  <br />
                  <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="jingying" runat="server" Text="经营环境提交"  OnClientClick="return confirm('提示：是否要提交经营环境业务线！')"
                    OnClick="jingying_Click" Height="30px" 
                    Width="113px" Font-Size="Medium" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button4" runat="server" Text="经营环境撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交经营环境业务线！')"
                    OnClick="Button4_Click" Height="30px" 
                    Width="113px" />
                  <br />
                  <br />

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="jidian" runat="server" Text="机电月报提交" Font-Size="Medium"  OnClientClick="return confirm('提示：是否要提交机电业务线！')"
                    OnClick="jidian_Click" Height="30px" 
                    Width="113px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button5" runat="server" Text="机电月报撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交机电月报业务线！')"
                    OnClick="Button5_Click" Height="30px" 
                    Width="113px" />
                    <br />
                  <br />
                  
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ludeng" runat="server" Text="路灯隧道提交" Font-Size="Medium" OnClientClick="return confirm('提示：是否要提交路灯隧道业务线！')"
                    OnClick="ludeng_Click" Height="30px" 
                    Width="113px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button6" runat="server" Text="路灯隧道撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交路灯隧道业务线！')"
                    OnClick="Button6_Click" Height="30px" 
                    Width="113px" />
                    <br />
                  <br />

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="kefu" runat="server" Text="客服中心提交" Font-Size="Medium"  OnClientClick="return confirm('提示：是否要提交客服中心业务线！')"
                    OnClick="kefu_Click" Height="30px" 
                    Width="113px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button7" runat="server" Text="客服中心撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交客服中心业务线！')"
                    OnClick="Button7_Click" Height="30px" 
                    Width="113px" />

                    <br />
                  <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="teshu" runat="server" Font-Size="Medium" Height="30px" 
                        OnClick="teshu_Click" OnClientClick="return confirm('提示：是否要提交特殊运营状态信息！')" 
                        Text="特殊运营状态" Width="113px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button8" runat="server" Text="特殊运营撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交特殊运营业务线！')"
                    OnClick="Button8_Click" Height="30px" 
                    Width="113px" />
                        <br />
                  <br />

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="huizong" runat="server" Font-Size="Medium" Height="30px" 
                        OnClick="huizong_Click" OnClientClick="return confirm('提示：是否要提交汇总信息！')" 
                        Text="汇总" Width="113px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="Button9" runat="server" Text="汇总撤销"  
                Font-Size="Medium"   OnClientClick="return confirm('提示：是否要撤销提交汇总业务线！')"
                    OnClick="Button9_Click" Height="30px" 
                    Width="113px" />
                        <br />
                  <br />

      
                 
    </div>

</asp:Content>
