﻿var canvas, stage, exportRoot;

function init() {

    return false;
    canvas = document.getElementById("canvas");
    images = images || {};

    var manifest = [
        { src: "/Images/TeachPhilosophy/water/_00000.jpg", id: "_00000" },
        { src: "/Images/TeachPhilosophy/water/_00001.jpg", id: "_00001" },
        { src: "/Images/TeachPhilosophy/water/_00002.jpg", id: "_00002" },
        { src: "/Images/TeachPhilosophy/water/_00003.jpg", id: "_00003" },
        { src: "/Images/TeachPhilosophy/water/_00004.jpg", id: "_00004" },
        { src: "/Images/TeachPhilosophy/water/_00005.jpg", id: "_00005" },
        { src: "/Images/TeachPhilosophy/water/_00006.jpg", id: "_00006" },
        { src: "/Images/TeachPhilosophy/water/_00007.jpg", id: "_00007" },
        { src: "/Images/TeachPhilosophy/water/_00008.jpg", id: "_00008" },
        { src: "/Images/TeachPhilosophy/water/_00009.jpg", id: "_00009" },
        { src: "/Images/TeachPhilosophy/water/_00010.jpg", id: "_00010" },
        { src: "/Images/TeachPhilosophy/water/_00011.jpg", id: "_00011" },
        { src: "/Images/TeachPhilosophy/water/_00012.jpg", id: "_00012" },
        { src: "/Images/TeachPhilosophy/water/_00013.jpg", id: "_00013" },
        { src: "/Images/TeachPhilosophy/water/_00014.jpg", id: "_00014" },
        { src: "/Images/TeachPhilosophy/water/_00015.jpg", id: "_00015" },
        { src: "/Images/TeachPhilosophy/water/_00016.jpg", id: "_00016" },
        { src: "/Images/TeachPhilosophy/water/_00017.jpg", id: "_00017" },
        { src: "/Images/TeachPhilosophy/water/_00018.jpg", id: "_00018" },
        { src: "/Images/TeachPhilosophy/water/_00019.jpg", id: "_00019" },
        { src: "/Images/TeachPhilosophy/water/_00020.jpg", id: "_00020" },
        { src: "/Images/TeachPhilosophy/water/_00021.jpg", id: "_00021" },
        { src: "/Images/TeachPhilosophy/water/_00022.jpg", id: "_00022" },
        { src: "/Images/TeachPhilosophy/water/_00023.jpg", id: "_00023" },
        { src: "/Images/TeachPhilosophy/water/_00024.jpg", id: "_00024" },
        { src: "/Images/TeachPhilosophy/water/_00025.jpg", id: "_00025" },
        { src: "/Images/TeachPhilosophy/water/_00026.jpg", id: "_00026" },
        { src: "/Images/TeachPhilosophy/water/_00027.jpg", id: "_00027" },
        { src: "/Images/TeachPhilosophy/water/_00028.jpg", id: "_00028" },
        { src: "/Images/TeachPhilosophy/water/_00029.jpg", id: "_00029" },
        { src: "/Images/TeachPhilosophy/water/_00030.jpg", id: "_00030" },
        { src: "/Images/TeachPhilosophy/water/_00031.jpg", id: "_00031" },
        { src: "/Images/TeachPhilosophy/water/_00032.jpg", id: "_00032" },
        { src: "/Images/TeachPhilosophy/water/_00033.jpg", id: "_00033" },
        { src: "/Images/TeachPhilosophy/water/_00034.jpg", id: "_00034" },
        { src: "/Images/TeachPhilosophy/water/_00035.jpg", id: "_00035" },
        { src: "/Images/TeachPhilosophy/water/_00036.jpg", id: "_00036" },
        { src: "/Images/TeachPhilosophy/water/_00037.jpg", id: "_00037" },
        { src: "/Images/TeachPhilosophy/water/_00038.jpg", id: "_00038" },
        { src: "/Images/TeachPhilosophy/water/_00039.jpg", id: "_00039" },
        { src: "/Images/TeachPhilosophy/water/_00040.jpg", id: "_00040" },
        { src: "/Images/TeachPhilosophy/water/_00041.jpg", id: "_00041" },
        { src: "/Images/TeachPhilosophy/water/_00042.jpg", id: "_00042" },
        { src: "/Images/TeachPhilosophy/water/_00043.jpg", id: "_00043" },
        { src: "/Images/TeachPhilosophy/water/_00044.jpg", id: "_00044" },
        { src: "/Images/TeachPhilosophy/water/_00045.jpg", id: "_00045" },
        { src: "/Images/TeachPhilosophy/water/_00046.jpg", id: "_00046" },
        { src: "/Images/TeachPhilosophy/water/_00047.jpg", id: "_00047" },
        { src: "/Images/TeachPhilosophy/water/_00048.jpg", id: "_00048" },
        { src: "/Images/TeachPhilosophy/water/_00049.jpg", id: "_00049" },
        { src: "/Images/TeachPhilosophy/water/_00050.jpg", id: "_00050" },
        { src: "/Images/TeachPhilosophy/water/_00051.jpg", id: "_00051" },
        { src: "/Images/TeachPhilosophy/water/_00052.jpg", id: "_00052" },
        { src: "/Images/TeachPhilosophy/water/_00053.jpg", id: "_00053" },
        { src: "/Images/TeachPhilosophy/water/_00054.jpg", id: "_00054" },
        { src: "/Images/TeachPhilosophy/water/_00055.jpg", id: "_00055" },
        { src: "/Images/TeachPhilosophy/water/_00056.jpg", id: "_00056" },
        { src: "/Images/TeachPhilosophy/water/_00057.jpg", id: "_00057" },
        { src: "/Images/TeachPhilosophy/water/_00058.jpg", id: "_00058" },
        { src: "/Images/TeachPhilosophy/water/_00059.jpg", id: "_00059" },
        { src: "/Images/TeachPhilosophy/water/_00060.jpg", id: "_00060" },
        { src: "/Images/TeachPhilosophy/water/_00061.jpg", id: "_00061" },
        { src: "/Images/TeachPhilosophy/water/_00062.jpg", id: "_00062" },
        { src: "/Images/TeachPhilosophy/water/_00063.jpg", id: "_00063" },
        { src: "/Images/TeachPhilosophy/water/_00064.jpg", id: "_00064" },
        { src: "/Images/TeachPhilosophy/water/_00065.jpg", id: "_00065" },
        { src: "/Images/TeachPhilosophy/water/_00066.jpg", id: "_00066" },
        { src: "/Images/TeachPhilosophy/water/_00067.jpg", id: "_00067" },
        { src: "/Images/TeachPhilosophy/water/_00068.jpg", id: "_00068" },
        { src: "/Images/TeachPhilosophy/water/_00069.jpg", id: "_00069" },
        { src: "/Images/TeachPhilosophy/water/_00070.jpg", id: "_00070" },
        { src: "/Images/TeachPhilosophy/water/_00071.jpg", id: "_00071" },
        { src: "/Images/TeachPhilosophy/water/_00072.jpg", id: "_00072" },
        { src: "/Images/TeachPhilosophy/water/_00073.jpg", id: "_00073" },
        { src: "/Images/TeachPhilosophy/water/_00074.jpg", id: "_00074" },
        { src: "/Images/TeachPhilosophy/water/_00075.jpg", id: "_00075" },
        { src: "/Images/TeachPhilosophy/water/_00076.jpg", id: "_00076" },
        { src: "/Images/TeachPhilosophy/water/_00077.jpg", id: "_00077" },
        { src: "/Images/TeachPhilosophy/water/_00078.jpg", id: "_00078" },
        { src: "/Images/TeachPhilosophy/water/_00079.jpg", id: "_00079" },
        { src: "/Images/TeachPhilosophy/water/_00080.jpg", id: "_00080" },
        { src: "/Images/TeachPhilosophy/water/_00081.jpg", id: "_00081" },
        { src: "/Images/TeachPhilosophy/water/_00082.jpg", id: "_00082" },
        { src: "/Images/TeachPhilosophy/water/_00083.jpg", id: "_00083" },
        { src: "/Images/TeachPhilosophy/water/_00084.jpg", id: "_00084" },
        { src: "/Images/TeachPhilosophy/water/_00085.jpg", id: "_00085" },
        { src: "/Images/TeachPhilosophy/water/_00086.jpg", id: "_00086" },
        { src: "/Images/TeachPhilosophy/water/_00087.jpg", id: "_00087" },
        { src: "/Images/TeachPhilosophy/water/_00088.jpg", id: "_00088" },
        { src: "/Images/TeachPhilosophy/water/_00089.jpg", id: "_00089" },
        { src: "/Images/TeachPhilosophy/water/_00090.jpg", id: "_00090" },
        { src: "/Images/TeachPhilosophy/water/_00091.jpg", id: "_00091" },
        { src: "/Images/TeachPhilosophy/water/_00092.jpg", id: "_00092" },
        { src: "/Images/TeachPhilosophy/water/_00093.jpg", id: "_00093" },
        { src: "/Images/TeachPhilosophy/water/_00094.jpg", id: "_00094" },
        { src: "/Images/TeachPhilosophy/water/_00095.jpg", id: "_00095" },
        { src: "/Images/TeachPhilosophy/water/_00096.jpg", id: "_00096" },
        { src: "/Images/TeachPhilosophy/water/_00097.jpg", id: "_00097" },
        { src: "/Images/TeachPhilosophy/water/_00098.jpg", id: "_00098" },
        { src: "/Images/TeachPhilosophy/water/_00099.jpg", id: "_00099" },
        { src: "/Images/TeachPhilosophy/water/_00100.jpg", id: "_00100" },
        { src: "/Images/TeachPhilosophy/water/_00101.jpg", id: "_00101" },
        { src: "/Images/TeachPhilosophy/water/_00102.jpg", id: "_00102" },
        { src: "/Images/TeachPhilosophy/water/_00103.jpg", id: "_00103" },
        { src: "/Images/TeachPhilosophy/water/_00104.jpg", id: "_00104" },
        { src: "/Images/TeachPhilosophy/water/_00105.jpg", id: "_00105" },
        { src: "/Images/TeachPhilosophy/water/_00106.jpg", id: "_00106" },
        { src: "/Images/TeachPhilosophy/water/_00107.jpg", id: "_00107" },
        { src: "/Images/TeachPhilosophy/water/_00108.jpg", id: "_00108" },
        { src: "/Images/TeachPhilosophy/water/_00109.jpg", id: "_00109" },
        { src: "/Images/TeachPhilosophy/water/_00110.jpg", id: "_00110" },
        { src: "/Images/TeachPhilosophy/water/_00111.jpg", id: "_00111" },
        { src: "/Images/TeachPhilosophy/water/_00112.jpg", id: "_00112" },
        { src: "/Images/TeachPhilosophy/water/_00113.jpg", id: "_00113" },
        { src: "/Images/TeachPhilosophy/water/_00114.jpg", id: "_00114" },
        { src: "/Images/TeachPhilosophy/water/_00115.jpg", id: "_00115" },
        { src: "/Images/TeachPhilosophy/water/_00116.jpg", id: "_00116" },
        { src: "/Images/TeachPhilosophy/water/_00117.jpg", id: "_00117" },
        { src: "/Images/TeachPhilosophy/water/_00118.jpg", id: "_00118" },
        { src: "/Images/TeachPhilosophy/water/_00119.jpg", id: "_00119" },
        { src: "/Images/TeachPhilosophy/water/_00120.jpg", id: "_00120" },
        { src: "/Images/TeachPhilosophy/water/_00121.jpg", id: "_00121" },
        { src: "/Images/TeachPhilosophy/water/_00122.jpg", id: "_00122" },
        { src: "/Images/TeachPhilosophy/water/_00123.jpg", id: "_00123" },
        { src: "/Images/TeachPhilosophy/water/_00124.jpg", id: "_00124" },
        { src: "/Images/TeachPhilosophy/water/_00125.jpg", id: "_00125" },
        { src: "/Images/TeachPhilosophy/water/_00126.jpg", id: "_00126" },
        { src: "/Images/TeachPhilosophy/water/_00127.jpg", id: "_00127" },
        { src: "/Images/TeachPhilosophy/water/_00128.jpg", id: "_00128" },
        { src: "/Images/TeachPhilosophy/water/_00129.jpg", id: "_00129" },
        { src: "/Images/TeachPhilosophy/water/_00130.jpg", id: "_00130" },
        { src: "/Images/TeachPhilosophy/water/_00131.jpg", id: "_00131" },
        { src: "/Images/TeachPhilosophy/water/_00132.jpg", id: "_00132" },
        { src: "/Images/TeachPhilosophy/water/_00133.jpg", id: "_00133" },
        { src: "/Images/TeachPhilosophy/water/_00134.jpg", id: "_00134" },
        { src: "/Images/TeachPhilosophy/water/_00135.jpg", id: "_00135" },
        { src: "/Images/TeachPhilosophy/water/_00136.jpg", id: "_00136" },
        { src: "/Images/TeachPhilosophy/water/_00137.jpg", id: "_00137" },
        { src: "/Images/TeachPhilosophy/water/_00138.jpg", id: "_00138" },
        { src: "/Images/TeachPhilosophy/water/_00139.jpg", id: "_00139" },
        { src: "/Images/TeachPhilosophy/water/_00140.jpg", id: "_00140" },
        { src: "/Images/TeachPhilosophy/water/_00141.jpg", id: "_00141" },
        { src: "/Images/TeachPhilosophy/water/_00142.jpg", id: "_00142" },
        { src: "/Images/TeachPhilosophy/water/_00143.jpg", id: "_00143" },
        { src: "/Images/TeachPhilosophy/water/_00144.jpg", id: "_00144" },
        { src: "/Images/TeachPhilosophy/water/_00145.jpg", id: "_00145" },
        { src: "/Images/TeachPhilosophy/water/_00146.jpg", id: "_00146" },
        { src: "/Images/TeachPhilosophy/water/_00147.jpg", id: "_00147" },
        { src: "/Images/TeachPhilosophy/water/_00148.jpg", id: "_00148" },
        { src: "/Images/TeachPhilosophy/water/_00149.jpg", id: "_00149" },
        { src: "/Images/TeachPhilosophy/water/_00150.jpg", id: "_00150" },
        { src: "/Images/TeachPhilosophy/water/_00151.jpg", id: "_00151" },
        { src: "/Images/TeachPhilosophy/water/_00152.jpg", id: "_00152" },
        { src: "/Images/TeachPhilosophy/water/_00153.jpg", id: "_00153" },
        { src: "/Images/TeachPhilosophy/water/_00154.jpg", id: "_00154" },
        { src: "/Images/TeachPhilosophy/water/_00155.jpg", id: "_00155" },
        { src: "/Images/TeachPhilosophy/water/_00156.jpg", id: "_00156" },
        { src: "/Images/TeachPhilosophy/water/_00157.jpg", id: "_00157" },
        { src: "/Images/TeachPhilosophy/water/_00158.jpg", id: "_00158" },
        { src: "/Images/TeachPhilosophy/water/_00159.jpg", id: "_00159" },
        { src: "/Images/TeachPhilosophy/water/_00160.jpg", id: "_00160" },
        { src: "/Images/TeachPhilosophy/water/_00161.jpg", id: "_00161" },
        { src: "/Images/TeachPhilosophy/water/_00162.jpg", id: "_00162" },
        { src: "/Images/TeachPhilosophy/water/_00163.jpg", id: "_00163" },
        { src: "/Images/TeachPhilosophy/water/_00164.jpg", id: "_00164" },
        { src: "/Images/TeachPhilosophy/water/_00165.jpg", id: "_00165" },
        { src: "/Images/TeachPhilosophy/water/_00166.jpg", id: "_00166" },
        { src: "/Images/TeachPhilosophy/water/_00167.jpg", id: "_00167" },
        { src: "/Images/TeachPhilosophy/water/_00168.jpg", id: "_00168" },
        { src: "/Images/TeachPhilosophy/water/_00169.jpg", id: "_00169" }
    ];

    var loader = new createjs.LoadQueue(false);
    loader.addEventListener("fileload", handleFileLoad);
    loader.addEventListener("complete", handleComplete);
    loader.loadManifest(manifest);
}

function handleFileLoad(evt) {
    if (evt.item.type == "image") { images[evt.item.id] = evt.result; }
}

function handleComplete() {
    exportRoot = new lib.water();

    stage = new createjs.Stage(canvas);
    stage.addChild(exportRoot);
    stage.update();

    createjs.Ticker.setFPS(24);
    createjs.Ticker.addEventListener("tick", stage);
}

function movieInit() {
    $('video').attr('src', $('.Mvglow').eq(0).find('img').attr('data-video'));
    //文字的click事件
    $(".Mvtxt").click(function () {
        var index = $('.Mvtxt').index($(this));
        $('video').attr('src', $('.Mvglow').eq(index).find('img').attr('data-video'));
        $('video').attr('poster', $('.Mvglow').eq(index).find('img').attr('src'));
    });

    //預覽圖的click事件
    $(".Mvglow").click(function () {
        var index = $('.Mvglow').index($(this));
        $('video').attr('src', $('.Mvglow').eq(index).find('img').attr('data-video'));
        $('video').attr('poster', $('.Mvglow').eq(index).find('img').attr('src'));
    });
}//影片頁面專用

function bookInit() {
    //翻頁特效
    $('.flipbook').turn({
        page: 2,
        width: 1500,
        height: 905,
        elevation: 0,
        gradients: true,
        autoCenter: false,
        when: {
            start: function (event, pageObject, corner) {
                if (pageObject.next == 1)
                    event.preventDefault();
            },
            turning: function (event, page, view) {
                if (page == 1)
                    event.preventDefault();
            }
        }
    });
}//翻頁頁面專用