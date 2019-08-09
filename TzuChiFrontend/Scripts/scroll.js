
//固定畫面比例不捲動 
var scrollFunc = function (e) {
    e = e || window.event;
    if (e.wheelDelta && event.ctrlKey) {//IE/Opera/Chrome 
        event.returnValue = false;
    } else if (e.detail) {//Firefox 
        event.returnValue = false;
    }
}
if (document.addEventListener) {
    document.addEventListener('DOMMouseScroll', scrollFunc, false);
}//W3C 
window.onmousewheel = document.onmousewheel = scrollFunc;//IE/Opera/Chrome/Safari 