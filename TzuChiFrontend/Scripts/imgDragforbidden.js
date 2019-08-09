/*防止拖動出現禁止符號*/
window.onload = function (e) {
    var evt = e || window.event, imgs, divs, i;
    if (evt.preventDefault) {
        imgs = document.getElementsByTagName('img');
        for (i = 0; i < imgs.length; i++) {
            imgs[i].onmousedown = disableDragging;
        }        
    }
    else {
        document.ondragstart = function () {
            return false;
        };
    }
};
function disableDragging(e) {
    e.preventDefault();
    return false;
}