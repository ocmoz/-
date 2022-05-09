function MessageBox(x, y, width, height, sender, sendTime, msg, msgType, count, target, url) {
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.sender = sender;
    this.sendTime = sendTime;
    this.msg = msg;
    this.msgType = msgType;
    this.count = count;
    this.target = target;
    this.url = url;
    this.h = 0;
}
function URL(url, target) {
    this.url = url;
    this.target = target;
}
/*创建弹出框*/
MessageBox.prototype.CreatePopup = function() {
    try {

        this.popup = window.createPopup();
        var doc = this.popup.document;
        var body = doc.body;
        body.rightmargin = 0;
        body.leftmargin = 0;
        body.topmargin = 0;
        body.bottommargin = 0;
        body.innerHTML = this.CreateBody();
        //alert(body.innerHTML);
        doc.createStyleSheet().addImport("Css/PopupBox.css")   //加载样式表
        body.style.border = "none 0 #ffffff";
        body.style.cursor = "default";
        body.style.fontFamily = "Verdana", "Arial", "细明体", "sans-serif";
        body.style.fontSize = "12px";

        var me = this;
        this.close = false;
        var closeButton = doc.getElementById("btClose");
        closeButton.onclick = function() {
            me.close = true;
            me.PopupClose();
        }
        closeButton.setAttribute("popup", this.popup);

        var fun = function() {
            var cp = this.getAttribute("popup");
            me.close = true;
            me.PopupClose();
            window.open(cp.url, cp.target);
        }
        var popupMessage = doc.getElementById("PopupMessage");
        var urlObj = new URL(this.url, this.target);
        popupMessage.onclick = fun;
        popupMessage.setAttribute("popup", urlObj);

        var other = doc.getElementById("GotoOther");
        var otherUrl = new URL("Module/FM2E/PendingOrderMessage/ViewPendingOrder.aspx", this.target);
        other.onclick = fun;
        other.setAttribute("popup", otherUrl);

        this.ShowPopup();

    } catch (e) { }
}
MessageBox.prototype.CreateBody = function() {

    var str;
    str = "<div id='HintBox'>";
    str += "<table style='width: 100%; ' cellpadding='0' cellspacing='0'>";
    str += "<tr><td  class='HintBoxtitle'><div class='CloseButton' id='btClose'></div></td></tr>";
    str += " <tr><td class='HintBoxbody'>";
    str += "<div class='SendBy'>" + this.sender + "&nbsp;&nbsp;" + this.sendTime + "：</div>";
    str += " <div class='NewMessage'><span style='color: Red;'>NEW&nbsp;&nbsp;</span>";
    var str1 = this.msg;
    var str2 = "上报";
    var str3 = str1.indexOf(str2);
    if (str3 == -1) {
    str += "<a href='#' id='PopupMessage'>" + this.msg + "<span style='color: Red;'>（" + this.msgType + "）</span></a></div>";
      }
    else {
        str += "<a href='#' id='PopupMessage' style='color: Green;'>" + this.msg + "<span style='color: Red;'>（" + this.msgType + "）</span></a></div>";
            }
    str += "<div class='OtherMessage'><a href='#' id='GotoOther' >您还有" + this.count + "件待办事务</a></div></td></tr></table></div>";
    return str;
}
/*显示弹出框*/
//MessageBox.prototype.ShowPopup = function() {
//    var me = this;
//    var fun = function() {
//        if (me.h >= me.height || me.popup == null || !me.popup.isOpen) {
//            window.clearInterval(me.timerShow);
//            me.timerShow = null;
//            me.popup.show(me.x, me.y - me.height, me.width, me.height, top.document.body);
//            me.KeepShow();
//        } else {
//            me.h += 3;
//            me.popup.show(me.x, me.y - me.h, me.width, me.h, top.document.body);
//        }
//    }
//    this.h = 0;
//    this.popup.show(me.x, me.y, this.width, this.h, top.document.body);
//    this.timerShow = window.setInterval(fun, 5);
//}
//MessageBox.prototype.KeepShow = function() {
//    var me = this;
//    var fun = function() {
//        if (me.close == true) {
//            window.clearInterval(me.timerShow);
//            return;
//        }
//        me.popup.show(me.x, me.y - me.height, me.width, me.height, top.document.body);
//    }
//    this.timerShow = window.setInterval(fun, 5);
//}
///*自动隐藏弹出框*/
//MessageBox.prototype.PopupToLower = function() {
//    var me = this;
//    var fun = function() {
//        if (me.h <= 0 || me.popup == null || !me.popup.isOpen) {
//            window.clearInterval(this.timerShow);
//            this.PopupClose();
//        } else {
//            me.h -= 3; me.y += 3;
//            me.popup.show(me.x, me.y, me.height, me.h);
//        }
//    }
//    if (this.popup != null) this.timerShow = window.setInterval(fun, 5);
//}

/*关闭弹出框*/
MessageBox.prototype.PopupClose = function() {

    //this.close = true;
    if (this.timerShow != null)
        window.clearInterval(this.timerShow);
    if (this.popup != null && this.popup.isOpen) this.popup.hide();
    this.popup = null;
}

MessageBox.prototype.Close= function(){
    this.close = true;
    this.PopupClose();
}