<%@ page language="C#" autoeventwireup="true" inherits="Default, App_Web_fublbnad" %>

<%@ Import Namespace="WebUtility" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>高速公路机电管理系统</title>
    <link href="Css/Site_Css.css" rel="stylesheet" type="text/css" />
    <link href="inc/FineMessBox/css/subModal.css" rel="stylesheet" type="text/css" />
    <link href="Css/Menu.css" rel="stylesheet" type="text/css" />
    <link href="Css/menuTree.css" rel="stylesheet" type="text/css" />
    <%--<link href="<%=Page.ResolveUrl("~/") %>Css/modalpopup.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" src="inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="inc/FineMessBox/js/subModal.js"></script>

    <script type="text/javascript" src="js/PopupBox.js"></script>

    <script type="text/javascript" src="js/DefaultPage.js"></script>

    <script src="js/MoveTree.js" type="text/javascript"></script>

    <link href="Css/menuTree.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0 0 0 0;" onload="Init()">
    <!--弹出式菜单浮动层开始-->
    <div class="subMenuArea" style="display: none" id="menu" onmouseout="javascript:HideMenu();"
        onmouseover="javascript:ShowMenu();">
    </div>
    <!--弹出式菜单浮动层结束-->
    <!--弹出式事务提示框-->
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/MenuService.asmx" />
            <asp:ServiceReference Path="~/Module/FM2E/MessageManager/WebService_NewMessage.asmx" />
            <asp:ServiceReference Path="~/Module/FM2E/PendingOrderMessage/WebService_NewPendingOrder.asmx" />
        </Services>
    </asp:ScriptManager>
    </form>
    <!--弹出式事务提示框-->
    <div style=" text-align: center;  height: 100%;width: 100%;">
        <table border="0" cellspacing="0" cellpadding="0" style=" height: 100%;width: 100%;
             margin: 0 auto;">
            <tr>
                <td style="background-repeat: repeat; background-position: center; height: 69px;
                    background: url(images/top_bg.png);" valign="top" colspan="2">
                    <div>
                        <div style="margin: 7px auto auto 0px; height: 69px; float: left;">
                            <img src="images/top-gray.jpg" alt="" />
                        </div>
                        <div id="ToolBar" style="height: 50px; min-width: 200px;">
                            <div class="welcome">
                                当前登录用户：<strong>
                                    <%=currentUser %>
                                    (<%= UserData.CurrentUserData.PersonName %>) </strong>&nbsp;&nbsp;<%= UserData.CurrentUserData.CompanyName %>&nbsp;&nbsp;<%= UserData.CurrentUserData.DepartmentName %>
                                &nbsp;&nbsp;<asp:Label ID="Label_WarehouseName" runat="server"></asp:Label>
                            </div>
                            <div>
                                <div class="quit">
                                    <a href="javascript:if(confirm('确定要退出系统？')){window.location.href='Logout.aspx'}">退出系统</a>
                                </div>
                                <div class="backhome">
                                    <a href="javascript: window.location.href='Default.aspx'">回到首页</a>
                                </div>
                                <div class="book">
                                    <a href="public/Standardization/高速公路机电系统维护标准化管理系统--标准化细则.doc">细则</a>
                                </div>
                                <div class="set">
                                    <a href="javascript:showPopWin('个人设定','UserSet.aspx',550, 200, CallBackHint,true)">个人设定</a>
                                </div>
                                <div class="feedback">
                                    <a href="Module/FM2E/BugReportManager/SendBugreport/SendBugreport.aspx" target="mainFrame">
                                        用户反馈</a>
                                </div>
                                <div class="about">
                                    <a href="javascript:showPopWin('About','about.aspx',550, 220, null,true);">版本信息</a>
                                </div>
                                <div class="message" style="display: none" id="MsgDiv">
                                    <a style="color: Red;" target="mainFrame" id="newMessage" href="Module/FM2E/MessageManager/ViewMessage.aspx">
                                        0条新消息</a>
                                </div>
                                <div class="announce">
                                    <a target="mainFrame" href="Module/FM2E/PendingOrderMessage/ViewPendingOrder.aspx">待办事务</a>
                                    <a style="color: Red; display: none" target="mainFrame" id="newPendingOrder" href="Module/FM2E/PendingOrderMessage/ViewPendingOrder.aspx">
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 1px;" colspan="2">
                    <div style="margin: 0 0 0 0; display: none; height: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr style="text-align: center; height: 28px;" valign="bottom">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" style="text-align: center;">
                                        <tr>
                                            <td style="width: 30px">
                                            </td>
                                            <%=sb_TopHTMLSrc.ToString()%></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="<%=TopMenuCount+1 %>" style="background-image: url(images/Index/1.gif);
                                    background-repeat: repeat-x; background-position: top; height: 4px;" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; height: 14px;" colspan="<%=TopMenuCount+1 %>">
                                    <%=sb_DownHTMLSrc.ToString() %>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <img src="images/openMenu.gif" alt="" id="menuPosition" style="display: none;" onmouseover="javascript:ShowMenu();" /></div>
                </td>
            </tr>
            <tr style="width:1090px">
                <td style="vertical-align: top; text-align:left; width: 175px; min-width:175px">
                    <div id="Treebody" style="width: 175px; height: 100%; min-height: 400px; overflow: scroll;
                        vertical-align: top; background-color: #b7ddf0;">
                        <ul id="Ul1">
                            <%=sb1.ToString()%>
                        </ul>
                    </div>
                </td>
                <td style="vertical-align: top;text-align:left;width:100%;">
                    <div style=" width:100%;min-width: 900px; height: 100%; min-height: 400px">
                        <%--<a href="Module/FM2E/MaintainManager/MalFunctionManager/MalfunctionReport/MalfunctionList.aspx">
                            Module/FM2E/MaintainManager/MalFunctionManager/MalfunctionReport/MalfunctionList.aspx</a>MainPage.aspx--%>
                        <iframe id="Iframe1" name="mainFrame" style="width: 1080px; max-width:100px ; height: 100%; visibility: inherit;
                            margin: 0 0 0 0; overflow: scroll; z-index: 1" scrolling="yes" frameborder="0"
                            src="MainPage.aspx"></iframe>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>

<script type="text/javascript" language="javascript">
        var TotalTopMenuCount = <%=TopMenuCount%>;
        
        var cur_id="";
var flag=0,sflag=0;

//-------- 菜单点击事件 -------
function c(srcelement)
{
  var targetid,srcelement,targetelement;
  var strbuf;

  //-------- 如果点击了展开或收缩按钮---------
  targetid=srcelement.id+"d";
  targetelement=document.getElementById(targetid);

  if (targetelement.style.display=="none")
  {
     srcelement.className="active";
     targetelement.style.display='';

     menu_flag=0;
  }
  else
  {
     srcelement.className="";
     targetelement.style.display="none";

     menu_flag=1;
     var links=document.getElementsByTagName("A");
     for (i=0; i<links.length; i++)
     {
       srcelement=links[i];
       if(srcelement.parentNode.className.toUpperCase()=="l1" && srcelement.className=="active" && srcelement.id.substr(0,1)=="m")
       {
          menu_flag=0;
          break;
       }
     }
  }
}

function iFrameHeight() {   
var ifm= document.getElementById("mainFrame");   
var subWeb = document.frames ? document.frames["mainFrame"].document : ifm.contentDocument;   
if(ifm != null && subWeb != null) {
   ifm.height = subWeb.body.scrollHeight;
   ifm.width = subWeb.body.scrollWidth;
}   
}  
</script>

</html>
