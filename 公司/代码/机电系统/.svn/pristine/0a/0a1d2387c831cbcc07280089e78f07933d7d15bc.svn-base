function $(i) {
    return document.getElementById(i);
}

function $$(i) {
    return document.getElementsByTagName(i);
}

function popWindow(panelId, curWin) {
    var array = $(panelId).childNodes;
    var max = 0;
    var curZIndex = curWin.style.zIndex;
    for (var i = 0; i < array.length; ++i) {
        if (max < array[i].style.zIndex)
            max = array[i].style.zIndex;
        if (array[i].style.zIndex > curZIndex)
            --array[i].style.zIndex;
    }
    if (curWin.style.zIndex < max)
        curWin.style.zIndex = max;
}

function clickOnWindow(panelId, id) {
    var curWin = $(id);
    var content = curWin.getElementsByTagName('div')[1];
    if (content.style.display != 'none') {
        popWindow(panelId, curWin);
    }
}

function togglePanelVisible(panelId, id) {
    var panel = $(id);
    if (panel.style.display != 'none') {
        panel.style.display = 'none';
    }
    else {
        panel.style.display = 'block';
        popWindow(panelId, panel.parentNode.parentNode);
    }
}

function recordDisplayMode(fieldId, contentId) {
    $(fieldId).value = $(contentId).style.display;
}

function changeStateDescriptionTitle(containerId, textboxId) {
    var tb = $(textboxId);
    var newDp = tb.value;
    var stateName = tb.StateName;
    var viewers = $(containerId).childNodes;
    for (var i = 0; i < viewers.length; ++i) {
        var title = viewers[i].childNodes[0].childNodes[0];
        if (title.StateName == stateName) {
            title.innerText = newDp;
            return true;
        }
    }
}

function limitInput() {
    var key = window.event.keyCode;
    //alert(key);
    if (key==32) {
        return false;
    }
}

function checkTextEmpty(textboxId) {
    var tb = $(textboxId);
    if (tb.value == "") {
        tb.value = tb.OldValue;
        return false;
    }
    else
        tb.OldValue = tb.value;
}
