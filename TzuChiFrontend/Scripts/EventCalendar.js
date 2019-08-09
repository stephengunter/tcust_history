var allowPrevNext = true;
var allowTimer; 
var bkImgtimer; //背景輪播定時timer

function triggerAllowPrevNext() {
    allowPrevNext = true;
}

$(function () {
    var cnt = 0;
    var xDownPosition;      //平行軸
    var yDownPosition;      //垂直軸
    var xChangePosition;
    var yChangePosition;
    var boolMove = false;   // 控制是否要捲動
    var hasDirection;       // null為沒方向 h為平行 v為垂直
    var yearNumber = $('.event-bar').last().index() + 1;    //幾個年度
    var eventWidth = -289.5;                                   //每移動一個年度的長度
    var marginLeft = 0;
    var marginTop = 0;
    var scrollTop = 0;
    var isLockPanrightPanLeft = false;

    $(window).load(function(){
        $('body').show();
    });

    $('.drag').hammer().bind("panstart pandown panup panend panright panleft", function (ev) {
        if (ev.type == 'panstart') {
            var clickDragId = $(this).attr('id');
            marginTop = parseInt($('#' + clickDragId).position().top);
            marginLeft = parseInt($('#drag-x').css('margin-left'));
        } else if (ev.type == 'pandown' && Math.abs(ev.gesture.deltaY) > Math.abs(ev.gesture.deltaX)) {
            isLockPanrightPanLeft = true;
            moveTimeline(ev.gesture.deltaY, 'v', $(this)); // 水平 下
        } else if (ev.type == 'panup' && Math.abs(ev.gesture.deltaY) > Math.abs(ev.gesture.deltaX)) {
            isLockPanrightPanLeft = true;
            moveTimeline(ev.gesture.deltaY, 'v', $(this)); // 水平 上
        } else if (ev.type == 'panend') {
            isLockPanrightPanLeft = false;
        } else if (ev.type == 'panright' && !isLockPanrightPanLeft) {
            if (Math.abs(ev.gesture.deltaX) > 100) {
                moveTimeline(ev.gesture.deltaX + marginLeft, 'h', $(this));
            }
        } else if (ev.type == 'panleft' && !isLockPanrightPanLeft) {
            if (Math.abs(ev.gesture.deltaX) > 100) {
                moveTimeline(ev.gesture.deltaX + marginLeft, 'h', $(this));
            }
        }
    });

    function moveTimeline(distance, direction, $clickDragId) {
        if (direction == 'h') {                   
            //var newLeft = marginLeft + parseInt(distance);
            var newLeft = Math.round($('#drag-x').position().left) + parseInt(distance);
            var maxLeft = ((yearNumber - 12) * eventWidth);

            if (yearNumber < 12) { }
            else if (newLeft < 0 && newLeft > maxLeft) {
                $('#drag-x').css('margin-left', newLeft + 'px');
                monitorLeft();
            }
            if (newLeft >= 0) {
                $('#drag-x').css('margin-left', '0px');
                monitorLeft();
            }
            else if (newLeft < maxLeft) {
                $('#drag-x').css('margin-left', maxLeft + 'px');
                monitorLeft();
            }
            $('.timefooter').css('margin-left', $('#drag-x').css('margin-left')); //讓下方釘子軸跟著捲動
        } else if (direction == 'v') {                    
            var newTop = parseInt(marginTop) + parseInt(distance);
            //console.log('now top :' + marginTop + 'dis:' + distance);
            //var newTop = parseInt($clickDragId.css("top"), 10) + parseInt(distance);
            var maxTop = ($clickDragId.children('ul').length - 2) * -220;
            var isImg = $clickDragId.children('[data-isimg="true"]').length;
            var isNoImg = $clickDragId.children('[data-isimg="false"]').length;
            var maxTop = (isImg * -220 + isNoImg * -61) - 5 - 46;
                    
                    
            if (newTop < -80 && newTop >= maxTop) {
                $clickDragId.css('top', newTop + 'px');
            }
            else if (newTop > -80) {
                $clickDragId.css('top', '-80px');
            }
            else if (newTop <= maxTop) {
                $clickDragId.css('top', maxTop + 'px');
            }

                    
        }
    }
    function getNewCoordinate() {
        xDownPosition = event.pageX;
        yDownPosition = event.pageY;
    }


    /*防止拖動出現禁止符號*/
    window.onload = function (e) {
        var evt = e || window.event, imgs, divs, i;
        if (evt.preventDefault) {
            imgs = document.getElementsByTagName('img');
            divs = document.getElementsByTagName('div');
            for (i = 0; i < imgs.length; i++) {
                imgs[i].onmousedown = disableDragging;
            }
            for (i = 0; i < divs.length; i++) {
                divs[i].onmousedown = disableDragging;
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
});


function monitorLeft() {
    var TopPos = -930;

    var count = 0;
    $('.drag').each(function (index) {
        var offset = $(this).offset();
        if (offset.left >= 0) {
            if (count < 9) {

                $(this).animate({
                    top: TopPos += 85
                }, 0, 'linear');
                count++;
            }
        }
    });
}


//code by Aaron.Young
//company Hy-Tech
//Date 2014/10/19
var leftDivTimer;//左計時器
var rightDivTimer;//右計時器
var LEFT_DIV = $('#LeftDiv');//左彈跳視窗
var LEFT_CONTENTID = null;
var RIGHT_DIV = $('#RightDiv');//右彈跳視窗
var RIGHT_CONTENTID = null;
var TIMEOUT_CONSTANT = 300000;//設定關閉彈跳視窗計時常數
var INTERFACE = '/EventCalendar/GetByContentID';//接口位址
var MOUSE_POSITION_X;
var MOUSE_POSITION_Y;

$(document).ready(function () {
    init();
});
function init() {
    init_win();//初始化彈跳視窗
    eventInit();//註冊彈跳視窗事件&彈跳視窗內的所有點擊事件
}

function init_win() {//初始化彈跳視窗位置
    var width = $(window).width();
    var height = $(window).height();
    LEFT_DIV.offset({ left: 485, top: 190 });
    RIGHT_DIV.offset({ left: 2425, top: 190 });
}

function eventInit() {//事件初始化
    LEFT_DIV.click(function () {
        clearTimeout(leftDivTimer);
        leftDivTimer = setTimeout("win_close(LEFT_DIV)", TIMEOUT_CONSTANT);
    });//左彈跳視窗被點擊，清除計時器並重設

    RIGHT_DIV.click(function () {
        clearTimeout(rightDivTimer);
        rightDivTimer = setTimeout("win_close(RIGHT_DIV)", TIMEOUT_CONSTANT);
    });//右彈跳視窗被點擊，清除計時器並重設

    $('.closewin img').hammer().on('tap', function () {
        win_close($(this).parent().parent());
        var videoId = '';
        if ($(this).parent().parent().attr('id') === "LeftDiv") {
            videoId = 'leftVideo';
        }
        else {
            videoId = 'rightVideo';
        }



       
        if (videojs.getPlayers()[videoId]) {
            videojs(videoId).dispose();
        } 
       
       
       
    });//"X"被點擊，清除計時器並重設

    $('.tnew').hammer().on('tap', function (e) {

        //console.log(e);

        MOUSE_POSITION_X = e.gesture.center.x;
        MOUSE_POSITION_Y = e.gesture.center.y;

        if (IsLeft($(this))) {
            PopupAjax($(this).attr('id'), 'LEFT');
        }
        else {
            PopupAjax($(this).attr('id'), 'RIGHT');
        }
    });

    // 20141020 Allen上下筆資料
    $('.PrevNext').hammer().on('tap', function (e) {
        //console.log(allowPrevNext)
        if (allowPrevNext == true) {
            allowPrevNext = false;
            clearTimeout(allowTimer);
            allowTimer = setTimeout('triggerAllowPrevNext()', 500);
        } else {
            return false;
        }

        var action = $(this).attr('id');
        //console.log(action);
        var parentDivId = $('#' + LEFT_CONTENTID).parent().attr('id');
        //console.log("LEFT_CONTENTID" + LEFT_CONTENTID + "parentDivId" + parentDivId);
        var firstEventIndex = 2;
        var nowEventIndex;
        var lastEventIndex;
        if ($(this).hasClass('LeftPopup')) {
            parentDivId = $('#' + LEFT_CONTENTID).parent().attr('id');
            nowEventIndex = $('#' + LEFT_CONTENTID).index();
            lastEventIndex = $('#' + parentDivId + ' ul').last().index();
            if (action == 'LeftPrev') {
                if (nowEventIndex >= firstEventIndex) {
                    var nextID = $('#' + parentDivId + ' ul').eq(--nowEventIndex).attr('id');
                    if ($.type(nextID) === 'undefined') {
                        return false;
                    }
                    PopupAjax(nextID, 'LEFT', 'LeftPrev');
                }
            }
            else if (action == 'LeftNext') {
                if (nowEventIndex < lastEventIndex) {
                    var nextID = $('#' + parentDivId + ' ul').eq(++nowEventIndex).attr('id');
                    if ($.type(nextID) === 'undefined') {
                        return false;
                    }
                    PopupAjax(nextID, 'LEFT', 'LeftNext');
                }
            }
        } else if ($(this).hasClass('RightPopup')) {
            parentDivId = $('#' + RIGHT_CONTENTID).parent().attr('id');
            nowEventIndex = $('#' + RIGHT_CONTENTID).index();
            lastEventIndex = $('#' + parentDivId + ' ul').last().index();
            if (action == 'RightPrev') {
                if (nowEventIndex >= firstEventIndex) {
                    var nextID = $('#' + parentDivId + ' ul').eq(--nowEventIndex).attr('id');
                    if ($.type(nextID) === 'undefined') {
                        return false;
                    }
                    PopupAjax(nextID, 'RIGHT', 'RightPrev');
                }
            }
            else if (action == 'RightNext') {
                if (nowEventIndex < lastEventIndex) {
                    var nextID = $('#' + parentDivId + ' ul').eq(++nowEventIndex).attr('id');
                    if ($.type(nextID) === 'undefined') {
                        return false;
                    }
                    PopupAjax(nextID, 'RIGHT', 'RightNext');
                }
            }
        }
    });

    // 丟入id與哪一邊跳出
    function PopupAjax(id, side, PrevNext) {
        var ajaxData;
        var actionDiv;
        if (side == 'LEFT') {
            LEFT_CONTENTID = id;
            clearTimeout(leftDivTimer);
            actionDiv = LEFT_DIV;
        } else if (side == 'RIGHT') {
            RIGHT_CONTENTID = id;
            clearTimeout(rightDivTimer);
            actionDiv = RIGHT_DIV;
        }
        $.ajax({
            url: INTERFACE,
            data: 'contentID=' + id,
            type: 'POST',
            datatype: 'json',
            success: function (jData) {
                ajaxData = jData;

                //無片無影音
                if (ajaxData['PicUrlList'].length == 0 && ajaxData['VideoUrlList'].length == 0) {

                    if (PrevNext == 'RightPrev') {
                        allowPrevNext = true;
                        $('#RightPrev').hammer().trigger('tap');
                    } else if (PrevNext == 'RightNext') {
                        allowPrevNext = true;
                        $('#RightNext').hammer().trigger('tap');
                    } else if (PrevNext == 'LeftPrev') {
                        allowPrevNext = true;
                        $('#LeftPrev').hammer().trigger('tap');
                    } else if (PrevNext == 'LeftNext') {
                        allowPrevNext = true;
                        $('#LeftNext').hammer().trigger('tap');
                    }

                    return false;
                }
                showData(actionDiv, ajaxData);//, ajaxData);
                dynamicImgEventInit(actionDiv, ajaxData);
                win_open(actionDiv);
                if (side == 'LEFT') {
                    clearTimeout(leftDivTimer);
                    leftDivTimer = setTimeout("win_close(LEFT_DIV)", TIMEOUT_CONSTANT);

                }

                if (side == 'RIGHT') {
                    clearTimeout(rightDivTimer);
                    rightDivTimer = setTimeout("win_close(RIGHT_DIV)", TIMEOUT_CONSTANT);
                }
            },
            complete: function () { }
        });
    }
}

function IsLeft(targetImg) {//判斷被點擊的圖片
    var imgPositionX = targetImg.offset().left;
    //if (imgPositionX <= ($(window).width() / 2)) {
    if (MOUSE_POSITION_X < 1940) {  // 20141020 Allen 修正點擊位置(抓時間軸長度的一半)
        return true;//左邊
    }
    else {
        return false;//右邊
    }
}



function win_close(targetWin) {//關閉彈跳視窗
    if (targetWin.attr('id') === 'LeftDiv') {
        clearTimeout(leftDivTimer);
    }
    else {
        clearTimeout(rightDivTimer);
    }
    targetWin.fadeOut();
    targetWin.find('.showobject').html('');

    var tID = targetWin.attr('id');
    if (tID == 'LeftDiv') {
        $('#LeftDivLL').fadeOut();
        $('#LeftDivRR').fadeOut();
    } else if (tID == 'RightDiv') {
        $('#RightDivLL').fadeOut();
        $('#RightDivRR').fadeOut();
    }


}

function win_open(targetWin) {
    targetWin.fadeIn();
    var tID = targetWin.attr('id');

    if (tID == 'LeftDiv') {
        $('#LeftDivLL').fadeIn();
        $('#LeftDivRR').fadeIn();
    } else if (tID == 'RightDiv') {
        $('#RightDivLL').fadeIn();
        $('#RightDivRR').fadeIn();
    }
}

function ConvertToDate(srcDate) {
    var ms = parseInt(srcDate.match(/\((\d+)\)/)[1]);
    return new Date(ms);
}

function FormatDateString(srcDate) {
    var dateString = srcDate.getFullYear() + '‧' + (srcDate.getMonth() + 1) + '‧' + srcDate.getDate();
    return dateString;
}

$(function () {
    ifvisible.setIdleDuration(300);
    ifvisible.idle(function () {
        location.reload();
    });
})