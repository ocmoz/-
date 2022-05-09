
//定期获取新消息与待办事项
var newMessageCount = 0;
var lastMsgID = "-1";
var msgBox = null;
function Init() {
    GetNewMessageCount();
    GetNewPendingOrder();
    window.setInterval("GetNewMessageCount();", 10000);
    window.setInterval("GetNewPendingOrder();", 10000);
    
}
function GetNewPendingOrder() {
try{
    WebService_NewPendingOrder.GetNewPendingOrder(InitMsgBox);
    }catch(ex){}
}

function GetNewMessageCount() {
try{
    WebService_NewMessage.GetNewMessageCount(GetNewMessage);
    }
    catch(ex){}
}

function InitMsgBox(response) {

    /**
        modified by zjl 4-23
        解决待办事务删除至0后，页面无法刷新，依然显示 待办事务(1)
    */
    if (response != null && response.Count > 0) {
        if(lastMsgID == response.MsgID)
            return;
        document.all.newPendingOrder.style.display = '';
        document.all.newPendingOrder.innerText = '(' + response.Count + ')';
    }
    else {
        document.all.newPendingOrder.style.display = 'none';
        document.all.newPendingOrder.innerText = '(' + 0 + ')';
    }

    try {
        var toolBarPosition = $get("BarPosition");
        var left = getLeft(toolBarPosition)+5;
        var bottom = document.body.clientHeight - 30;  //getTop(toolBarPosition)-30;

        if (msgBox != null) {
            msgBox.Close();
        }
            
        msgBox = new MessageBox(left, bottom, 409, 130, response.SenderName, response.SendTime, response.Msg, response.MsgType, response.Count, "mainFrame", response.URL);
        msgBox.CreatePopup();
        lastMsgID = response.MsgID;
    }
    catch (e) { }
}

function GetNewMessage(result) {
    //alert(result);
    if (result > 0) {
        document.all.MsgDiv.style.display = '';
        document.all.newMessage.innerText = result + '条新消息';
    }
    else {
        document.all.MsgDiv.style.display = 'none';
        document.all.newMessage.innerText = '0条新消息';
    }
}

//function GetNewPendingOrder(result) {
//    if (result > 0) {
//        document.all.newPendingOrder.style.display = '';
//        document.all.newPendingOrder.innerText = '(' + result + ')';
//    }
//    else {
//        document.all.newPendingOrder.style.display = 'none';
//        document.all.newPendingOrder.innerText = '(' + 0 + ')';
//    }
//}

//菜单列表javascript
var NowClickName = "";
var ItemClickName = "";
var lastItemIndex = "-1";
var nowItemIndex = "-1";

function printdiv(PrintDivID)//打印与预览用函数
{
    var headstr = "<html><head><title>aaa</title></head><body><object id='WebBrowser' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2' ></object><div style='width: 649px;'>";
    var footstr = "</div></body>";
    var newstr = document.all('mainFrame').contentWindow.document.all.item(PrintDivID).innerHTML;
    var childframeurl = window.mainFrame.location.href;
    var oldstr = document.body.innerHTML;
    document.body.innerHTML = headstr + newstr + footstr;
    document.all.WebBrowser.ExecWB(7, 1);
    //            window.print();
    document.body.innerHTML = oldstr;
    window.mainFrame.location.href = childframeurl;
    return false;
}

function NowShow(TopMenuName, nowno) {
    lastItemIndex = nowItemIndex;
    nowItemIndex = nowno;

    for (x = 1; x <= TotalTopMenuCount; x++) {
        document.all[TopMenuName + x].className = "topmenuoff";
        var obj = document.all[TopMenuName + x + "_table"];

        if (obj == null)
            continue;
        obj.style.display = "none";
    }
    document.all[TopMenuName + nowno].className = "topmenuon";
    obj = document.all[TopMenuName + nowno + "_table"];
    if (obj != null)
        document.all[TopMenuName + nowno + "_table"].style.display = "";
    NowClickName = TopMenuName + nowno;

    if (nowItemIndex != lastItemIndex) {
        $get("menuPosition").style.display = "none";
        $get("menu").style.display = "none";
    }
}
function NowClick1(url) {
   var obj = window.parent;
    if (obj != null && url != "")
        window.mainFrame.location.href = url;
        }
function NowClick(TopMenuName, nowno, url) {
    for (x = 1; x <= TotalTopMenuCount; x++) {
        document.all[TopMenuName + x].className = "topmenuoff";
        var obj = document.all[TopMenuName + x + "_table"];
        if (obj == null)
            continue;
        obj.style.display = "none";
    }
    document.all[TopMenuName + nowno].className = "topmenuon";
    obj = document.all[TopMenuName + nowno + "_table"];
    if (obj != null)
        document.all[TopMenuName + nowno + "_table"].style.display = "";
    NowClickName = TopMenuName + nowno;

    if (ItemClickName != "") {
        document.all(ItemClickName).className = "topmenuoff2 topmenuItem";
    }

    var obj = window.parent;
    if (obj != null && url != "")
        window.mainFrame.location.href = url;

    $get("menuPosition").style.display = "none";
    $get("menu").style.display = "none";
}
function ImageOverOROut(iname, ck) {
    if (NowClickName != iname) {
        if (ck == "v") {
            document.all(iname).className = "topmenuon";
        }
        else if (ck == "o") {
            document.all(iname).className = "topmenuoff";
        }
    }
}
function xNowShow(iname, links) {
    if (ItemClickName != "") {
        document.all(ItemClickName).className = "topmenuoff2 topmenuItem";
    }
    ItemClickName = iname;
    document.all(iname).className = "topmenuon2 topmenuItemOn";
    var obj = window.parent;
    if (obj != null & links != "")
        window.mainFrame.location.href = links;

    $get("menuPosition").style.display = "none";
    $get("menu").style.display = "none";
}
function xNowShowWithSubMenu(iname, links, moduleID,userName) {
    if (ItemClickName != "") {
        document.all(ItemClickName).className = "topmenuoff2 topmenuItem";
    }
    ItemClickName = iname;
    document.all(iname).className = "topmenuon2 topmenuItemOn";
    var obj = window.parent;
    if (obj != null & links != "")
        window.mainFrame.location.href = links;

    MenuService.GetSubMenu(userName, moduleID, SuccessCallBackFunction);
}
function xImageOverOROut(iname, ck) {
    //        if (ItemClickName != iname)
    //        {
    //            if (ck=="v")
    //            {
    //                document.all(iname).className = "topmenuon2 topmenuItemOn";
    //            }
    //            else if (ck=="o")
    //            {
    //                document.all(iname).className = "topmenuoff2 topmenuItem";
    //                
    //            }
    //        }
}
//获取元素的纵坐标
function getTop(e) {
    var offset = e.offsetTop;
    if (e.offsetParent != null) offset += getTop(e.offsetParent);
    return offset;
}

//获取元素的横坐标
function getLeft(e) {
    var offset = e.offsetLeft;
    if (e.offsetParent != null) offset += getLeft(e.offsetParent);
    return offset;
}

function menuMouseOver(obj) {
    if (obj != null)
        obj.className = "subMenuItemOn";
}
function menuMouseOut(obj) {
    if (obj != null)
        obj.className = "subMenuItemOff";
}

function ClickMenu(url) {
    $get("menu").style.display = "none";
    if (url != "") {
        window.mainFrame.location.href = url;
    }
}

function SuccessCallBackFunction(responseText) {
    var menu = $get("menu");
    if (menu == null)
        return;

    if (responseText.trim() == "") {
        $get("menuPosition").style.display = "none";
        $get("menu").style.display = "none";
        return;
    }

    menu.innerHTML = responseText;

    var menuPosition = $get("menuPosition");
    menuPosition.style.display = "block";

    var left = getLeft(menuPosition) + menuPosition.offsetWidth;
    var top = getTop(menuPosition);
    menu.style.position = "absolute";
    menu.style.left = left;
    menu.style.top = top;

    menu.style.display = "block";
}

function ShowMenu() {
    var menu = $get("menu");
    if (menu == null)
        return;

    var menuPosition = $get("menuPosition");
    var left = getLeft(menuPosition) + menuPosition.offsetWidth;
    var top = getTop(menuPosition);
    menu.style.position = "absolute";
    menu.style.left = left;
    menu.style.top = top;

    menu.style.display = "block";
}

function HideMenu() {
    $get("menu").style.display = "none";
}

function CallBackHint(msg) {
    alert(msg);
}


