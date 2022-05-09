
var HKEY_Root, HKEY_Path, HKEY_Key;
HKEY_Root = "HKEY_CURRENT_USER";
HKEY_Path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";

function PageSetup_Null() {
    try {
        var Wsh = new ActiveXObject("WScript.Shell");
        HKEY_Key = "header";
        Wsh.RegWrite(HKEY_Root + HKEY_Path + HKEY_Key, "");
        HKEY_Key = "footer";
        Wsh.RegWrite(HKEY_Root + HKEY_Path + HKEY_Key, "");
    }
    catch (e) { }
}

function PrintPreview(webBrowser) {
    try {
        var browser=document.getElementById(webBrowser);
        PageSetup_Null();
        webBrowser.ExecWB(7, 1);
    }
    catch (e) {
        //alert("生成打印预览时出错，请通过右键菜单中的\"打印\"进行打印");
        Print();
    }
}

function Print() {
    window.print();
}