<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewDeviceInfo.aspx.cs" Inherits="Module_FM2E_MaintainManager_ViewDeviceInfo_ViewDeviceInfo"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="Server">
   <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/common.js"></script>

    <link href="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/css/subModal.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>inc/FineMessBox/js/subModal.js"></script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="在用设备管理" HeadOPTxt="目前操作功能：查看设备信息">
        <cc1:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonUrlType="javaScript"
            ButtonPopedom="List" ButtonUrl="window.history.go(-1);" />
    </cc1:HeadMenuWebControls>
    <div style="width: 900px; height: 300px;">
        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <cc2:TabPanel runat="server" HeaderText="设备基本信息" ID="TabPanel1">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        &nbsp;
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td class="Table_searchtitle" colspan="3">
                                    设备基本信息
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    条形码编号：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <table width="100%">
                                        <tr align="center">
                                            <td align="center">
                                                <asp:Label ID="equipmentno" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="equipmentpic" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td rowspan="11" style="vertical-align: top;">
                                    <div style="width: 100%; text-align: center; vertical-align: middle;">
                                        <asp:Image ID="Image1" runat="server" Width="220px" AlternateText="No Picture" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    设备名称：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="equipmentname" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    所属公司：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="companyname" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    所属路段：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="sectionname" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    所属位置：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="locationname" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    所属系统：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="systemname" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    设备类型：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="serialnum" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    型号：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="model" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    品牌：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="specification" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    设备状态：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="status" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    净值/原价：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                <asp:Label ID="net" runat="server" Text="" ></asp:Label>/<asp:Label ID="price" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                           
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel runat="server" HeaderText="其它参考信息" ID="TabPanel2">
                <ContentTemplate>
                    <div style="width: 100%; text-align: center; vertical-align: top; padding: 0px 0px 0px 0px;">
                        &nbsp;
                        <table style="width: 100%; border-collapse: collapse; vertical-align: middle; text-align: left;
                            text-indent: 13px; border: solid 1px #a7c5e2;" border="1px">
                            <tr>
                                <td class="Table_searchtitle" colspan="4">
                                    其它参考信息
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    采购单号：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="purchaseorderid" runat="server" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    供应商：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="producername" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    生产商：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="shengchangshang" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    采购人：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="purchasername" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    责任人：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="responsibilityname" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    验收人：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="checkername" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    采购日期：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="purchasedate" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    验收日期：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="examdate" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    启用日期：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="openingdate" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    建档日期：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="filedate" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    保修至：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="warrantydate" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    使用年限：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="servicelife" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    最近更新时间：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="updatetime" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    所属设备种类：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="categoryname" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    折旧方法：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="depreciationmethod" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    折旧年限：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="depreciablelife" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    折旧净残率：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="residualrate" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    维修次数：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="maintenancetimes" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_body table_body_NoWidth">
                                    备注：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="remark" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                                <td class="table_body table_body_NoWidth">
                                    是否已资产注销：
                                </td>
                                <td class="table_none table_none_NoWidth">
                                    <asp:Label ID="iscancel" runat="server" Text="" Width="250px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
