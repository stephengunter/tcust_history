$(function () {
    $('#search').click(function () {
        //console.log($('#textfield').val());
        //window.location = '/HonourList/SearchByKeyWord' + '?key=' + 
        //    $('#textfield').val() + '&nextPage=1';

        document.forms['searchFrom'].submit();
    });

    $('#textfield').focus(function () {
        $(this).attr("placeholder", "");
    });
});

var canvas, stage, exportRoot;

function initAnimate() {
    canvas = document.getElementById("canvas");
    images = images || {};

    var manifest = [
		{ src: "../images/flower/flower_posed_00000.png", id: "flower_posed_00000" },
		{ src: "../images/flower/flower_posed_00001.png", id: "flower_posed_00001" },
		{ src: "../images/flower/flower_posed_00002.png", id: "flower_posed_00002" },
		{ src: "../images/flower/flower_posed_00003.png", id: "flower_posed_00003" },
		{ src: "../images/flower/flower_posed_00004.png", id: "flower_posed_00004" },
		{ src: "../images/flower/flower_posed_00005.png", id: "flower_posed_00005" },
		{ src: "../images/flower/flower_posed_00006.png", id: "flower_posed_00006" },
		{ src: "../images/flower/flower_posed_00007.png", id: "flower_posed_00007" },
		{ src: "../images/flower/flower_posed_00008.png", id: "flower_posed_00008" },
		{ src: "../images/flower/flower_posed_00009.png", id: "flower_posed_00009" },
		{ src: "../images/flower/flower_posed_00010.png", id: "flower_posed_00010" },
		{ src: "../images/flower/flower_posed_00011.png", id: "flower_posed_00011" },
		{ src: "../images/flower/flower_posed_00012.png", id: "flower_posed_00012" },
		{ src: "../images/flower/flower_posed_00013.png", id: "flower_posed_00013" },
		{ src: "../images/flower/flower_posed_00014.png", id: "flower_posed_00014" },
		{ src: "../images/flower/flower_posed_00015.png", id: "flower_posed_00015" },
		{ src: "../images/flower/flower_posed_00016.png", id: "flower_posed_00016" },
		{ src: "../images/flower/flower_posed_00017.png", id: "flower_posed_00017" },
		{ src: "../images/flower/flower_posed_00018.png", id: "flower_posed_00018" },
		{ src: "../images/flower/flower_posed_00019.png", id: "flower_posed_00019" },
		{ src: "../images/flower/flower_posed_00020.png", id: "flower_posed_00020" },
		{ src: "../images/flower/flower_posed_00021.png", id: "flower_posed_00021" },
		{ src: "../images/flower/flower_posed_00022.png", id: "flower_posed_00022" },
		{ src: "../images/flower/flower_posed_00023.png", id: "flower_posed_00023" },
		{ src: "../images/flower/flower_posed_00024.png", id: "flower_posed_00024" },
		{ src: "../images/flower/flower_posed_00025.png", id: "flower_posed_00025" },
		{ src: "../images/flower/flower_posed_00026.png", id: "flower_posed_00026" },
		{ src: "../images/flower/flower_posed_00027.png", id: "flower_posed_00027" },
		{ src: "../images/flower/flower_posed_00028.png", id: "flower_posed_00028" },
		{ src: "../images/flower/flower_posed_00029.png", id: "flower_posed_00029" },
		{ src: "../images/flower/flower_posed_00030.png", id: "flower_posed_00030" },
		{ src: "../images/flower/flower_posed_00031.png", id: "flower_posed_00031" },
		{ src: "../images/flower/flower_posed_00032.png", id: "flower_posed_00032" },
		{ src: "../images/flower/flower_posed_00033.png", id: "flower_posed_00033" },
		{ src: "../images/flower/flower_posed_00034.png", id: "flower_posed_00034" },
		{ src: "../images/flower/flower_posed_00035.png", id: "flower_posed_00035" },
		{ src: "../images/flower/flower_posed_00036.png", id: "flower_posed_00036" },
		{ src: "../images/flower/flower_posed_00037.png", id: "flower_posed_00037" },
		{ src: "../images/flower/flower_posed_00038.png", id: "flower_posed_00038" },
		{ src: "../images/flower/flower_posed_00039.png", id: "flower_posed_00039" },
		{ src: "../images/flower/flower_posed_00040.png", id: "flower_posed_00040" },
		{ src: "../images/flower/flower_posed_00041.png", id: "flower_posed_00041" },
		{ src: "../images/flower/flower_posed_00042.png", id: "flower_posed_00042" },
		{ src: "../images/flower/flower_posed_00043.png", id: "flower_posed_00043" },
		{ src: "../images/flower/flower_posed_00044.png", id: "flower_posed_00044" },
		{ src: "../images/flower/flower_posed_00045.png", id: "flower_posed_00045" },
		{ src: "../images/flower/flower_posed_00046.png", id: "flower_posed_00046" },
		{ src: "../images/flower/flower_posed_00047.png", id: "flower_posed_00047" },
		{ src: "../images/flower/flower_posed_00048.png", id: "flower_posed_00048" },
		{ src: "../images/flower/flower_posed_00049.png", id: "flower_posed_00049" },
		{ src: "../images/flower/flower_posed_00050.png", id: "flower_posed_00050" },
		{ src: "../images/flower/flower_posed_00051.png", id: "flower_posed_00051" },
		{ src: "../images/flower/flower_posed_00052.png", id: "flower_posed_00052" },
		{ src: "../images/flower/flower_posed_00053.png", id: "flower_posed_00053" },
		{ src: "../images/flower/flower_posed_00054.png", id: "flower_posed_00054" },
		{ src: "../images/flower/flower_posed_00055.png", id: "flower_posed_00055" },
		{ src: "../images/flower/flower_posed_00056.png", id: "flower_posed_00056" },
		{ src: "../images/flower/flower_posed_00057.png", id: "flower_posed_00057" },
		{ src: "../images/flower/flower_posed_00058.png", id: "flower_posed_00058" },
		{ src: "../images/flower/flower_posed_00059.png", id: "flower_posed_00059" },
		{ src: "../images/flower/flower_posed_00060.png", id: "flower_posed_00060" },
		{ src: "../images/flower/flower_posed_00061.png", id: "flower_posed_00061" },
		{ src: "../images/flower/flower_posed_00062.png", id: "flower_posed_00062" },
		{ src: "../images/flower/flower_posed_00063.png", id: "flower_posed_00063" },
		{ src: "../images/flower/flower_posed_00064.png", id: "flower_posed_00064" },
		{ src: "../images/flower/flower_posed_00065.png", id: "flower_posed_00065" },
		{ src: "../images/flower/flower_posed_00066.png", id: "flower_posed_00066" },
		{ src: "../images/flower/flower_posed_00067.png", id: "flower_posed_00067" },
		{ src: "../images/flower/flower_posed_00068.png", id: "flower_posed_00068" },
		{ src: "../images/flower/flower_posed_00069.png", id: "flower_posed_00069" },
		{ src: "../images/flower/flower_posed_00070.png", id: "flower_posed_00070" },
		{ src: "../images/flower/flower_posed_00071.png", id: "flower_posed_00071" },
		{ src: "../images/flower/flower_posed_00072.png", id: "flower_posed_00072" },
		{ src: "../images/flower/flower_posed_00073.png", id: "flower_posed_00073" },
		{ src: "../images/flower/flower_posed_00074.png", id: "flower_posed_00074" },
		{ src: "../images/flower/flower_posed_00075.png", id: "flower_posed_00075" },
		{ src: "../images/flower/flower_posed_00076.png", id: "flower_posed_00076" },
		{ src: "../images/flower/flower_posed_00077.png", id: "flower_posed_00077" },
		{ src: "../images/flower/flower_posed_00078.png", id: "flower_posed_00078" },
		{ src: "../images/flower/flower_posed_00079.png", id: "flower_posed_00079" },
		{ src: "../images/flower/flower_posed_00080.png", id: "flower_posed_00080" },
		{ src: "../images/flower/flower_posed_00081.png", id: "flower_posed_00081" },
		{ src: "../images/flower/flower_posed_00082.png", id: "flower_posed_00082" },
		{ src: "../images/flower/flower_posed_00083.png", id: "flower_posed_00083" },
		{ src: "../images/flower/flower_posed_00084.png", id: "flower_posed_00084" },
		{ src: "../images/flower/flower_posed_00085.png", id: "flower_posed_00085" },
		{ src: "../images/flower/flower_posed_00086.png", id: "flower_posed_00086" },
		{ src: "../images/flower/flower_posed_00087.png", id: "flower_posed_00087" },
		{ src: "../images/flower/flower_posed_00088.png", id: "flower_posed_00088" },
		{ src: "../images/flower/flower_posed_00089.png", id: "flower_posed_00089" },
		{ src: "../images/flower/Ripple_posed_00000.png", id: "Ripple_posed_00000" },
		{ src: "../images/flower/Ripple_posed_00001.png", id: "Ripple_posed_00001" },
		{ src: "../images/flower/Ripple_posed_00002.png", id: "Ripple_posed_00002" },
		{ src: "../images/flower/Ripple_posed_00003.png", id: "Ripple_posed_00003" },
		{ src: "../images/flower/Ripple_posed_00004.png", id: "Ripple_posed_00004" },
		{ src: "../images/flower/Ripple_posed_00005.png", id: "Ripple_posed_00005" },
		{ src: "../images/flower/Ripple_posed_00006.png", id: "Ripple_posed_00006" },
		{ src: "../images/flower/Ripple_posed_00007.png", id: "Ripple_posed_00007" },
		{ src: "../images/flower/Ripple_posed_00008.png", id: "Ripple_posed_00008" },
		{ src: "../images/flower/Ripple_posed_00009.png", id: "Ripple_posed_00009" },
		{ src: "../images/flower/Ripple_posed_00010.png", id: "Ripple_posed_00010" },
		{ src: "../images/flower/Ripple_posed_00011.png", id: "Ripple_posed_00011" },
		{ src: "../images/flower/Ripple_posed_00012.png", id: "Ripple_posed_00012" },
		{ src: "../images/flower/Ripple_posed_00013.png", id: "Ripple_posed_00013" },
		{ src: "../images/flower/Ripple_posed_00014.png", id: "Ripple_posed_00014" },
		{ src: "../images/flower/Ripple_posed_00015.png", id: "Ripple_posed_00015" },
		{ src: "../images/flower/Ripple_posed_00016.png", id: "Ripple_posed_00016" },
		{ src: "../images/flower/Ripple_posed_00017.png", id: "Ripple_posed_00017" },
		{ src: "../images/flower/Ripple_posed_00018.png", id: "Ripple_posed_00018" },
		{ src: "../images/flower/Ripple_posed_00019.png", id: "Ripple_posed_00019" },
		{ src: "../images/flower/Ripple_posed_00020.png", id: "Ripple_posed_00020" },
		{ src: "../images/flower/Ripple_posed_00021.png", id: "Ripple_posed_00021" },
		{ src: "../images/flower/Ripple_posed_00022.png", id: "Ripple_posed_00022" },
		{ src: "../images/flower/Ripple_posed_00023.png", id: "Ripple_posed_00023" },
		{ src: "../images/flower/Ripple_posed_00024.png", id: "Ripple_posed_00024" },
		{ src: "../images/flower/Ripple_posed_00025.png", id: "Ripple_posed_00025" },
		{ src: "../images/flower/Ripple_posed_00026.png", id: "Ripple_posed_00026" },
		{ src: "../images/flower/Ripple_posed_00027.png", id: "Ripple_posed_00027" },
		{ src: "../images/flower/Ripple_posed_00028.png", id: "Ripple_posed_00028" },
		{ src: "../images/flower/Ripple_posed_00029.png", id: "Ripple_posed_00029" },
		{ src: "../images/flower/Ripple_posed_00030.png", id: "Ripple_posed_00030" },
		{ src: "../images/flower/Ripple_posed_00031.png", id: "Ripple_posed_00031" },
		{ src: "../images/flower/Ripple_posed_00032.png", id: "Ripple_posed_00032" },
		{ src: "../images/flower/Ripple_posed_00033.png", id: "Ripple_posed_00033" },
		{ src: "../images/flower/Ripple_posed_00034.png", id: "Ripple_posed_00034" },
		{ src: "../images/flower/Ripple_posed_00035.png", id: "Ripple_posed_00035" },
		{ src: "../images/flower/Ripple_posed_00036.png", id: "Ripple_posed_00036" },
		{ src: "../images/flower/Ripple_posed_00037.png", id: "Ripple_posed_00037" },
		{ src: "../images/flower/Ripple_posed_00038.png", id: "Ripple_posed_00038" },
		{ src: "../images/flower/Ripple_posed_00039.png", id: "Ripple_posed_00039" },
		{ src: "../images/flower/Ripple_posed_00040.png", id: "Ripple_posed_00040" },
		{ src: "../images/flower/Ripple_posed_00041.png", id: "Ripple_posed_00041" },
		{ src: "../images/flower/Ripple_posed_00042.png", id: "Ripple_posed_00042" },
		{ src: "../images/flower/Ripple_posed_00043.png", id: "Ripple_posed_00043" },
		{ src: "../images/flower/Ripple_posed_00044.png", id: "Ripple_posed_00044" },
		{ src: "../images/flower/Ripple_posed_00045.png", id: "Ripple_posed_00045" },
		{ src: "../images/flower/Ripple_posed_00046.png", id: "Ripple_posed_00046" },
		{ src: "../images/flower/Ripple_posed_00047.png", id: "Ripple_posed_00047" },
		{ src: "../images/flower/Ripple_posed_00048.png", id: "Ripple_posed_00048" },
		{ src: "../images/flower/Ripple_posed_00049.png", id: "Ripple_posed_00049" },
		{ src: "../images/flower/Ripple_posed_00050.png", id: "Ripple_posed_00050" },
		{ src: "../images/flower/Ripple_posed_00051.png", id: "Ripple_posed_00051" },
		{ src: "../images/flower/Ripple_posed_00052.png", id: "Ripple_posed_00052" },
		{ src: "../images/flower/Ripple_posed_00053.png", id: "Ripple_posed_00053" },
		{ src: "../images/flower/Ripple_posed_00054.png", id: "Ripple_posed_00054" },
		{ src: "../images/flower/Ripple_posed_00055.png", id: "Ripple_posed_00055" },
		{ src: "../images/flower/Ripple_posed_00056.png", id: "Ripple_posed_00056" },
		{ src: "../images/flower/Ripple_posed_00057.png", id: "Ripple_posed_00057" },
		{ src: "../images/flower/Ripple_posed_00058.png", id: "Ripple_posed_00058" },
		{ src: "../images/flower/Ripple_posed_00059.png", id: "Ripple_posed_00059" },
		{ src: "../images/flower/Ripple_posed_00060.png", id: "Ripple_posed_00060" },
		{ src: "../images/flower/Ripple_posed_00061.png", id: "Ripple_posed_00061" },
		{ src: "../images/flower/Ripple_posed_00062.png", id: "Ripple_posed_00062" },
		{ src: "../images/flower/Ripple_posed_00063.png", id: "Ripple_posed_00063" },
		{ src: "../images/flower/Ripple_posed_00064.png", id: "Ripple_posed_00064" },
		{ src: "../images/flower/Ripple_posed_00065.png", id: "Ripple_posed_00065" },
		{ src: "../images/flower/Ripple_posed_00066.png", id: "Ripple_posed_00066" },
		{ src: "../images/flower/Ripple_posed_00067.png", id: "Ripple_posed_00067" },
		{ src: "../images/flower/Ripple_posed_00068.png", id: "Ripple_posed_00068" },
		{ src: "../images/flower/Ripple_posed_00069.png", id: "Ripple_posed_00069" },
		{ src: "../images/flower/Ripple_posed_00070.png", id: "Ripple_posed_00070" },
		{ src: "../images/flower/Ripple_posed_00071.png", id: "Ripple_posed_00071" },
		{ src: "../images/flower/Ripple_posed_00072.png", id: "Ripple_posed_00072" },
		{ src: "../images/flower/Ripple_posed_00073.png", id: "Ripple_posed_00073" },
		{ src: "../images/flower/Ripple_posed_00074.png", id: "Ripple_posed_00074" },
		{ src: "../images/flower/Ripple_posed_00075.png", id: "Ripple_posed_00075" },
		{ src: "../images/flower/Ripple_posed_00076.png", id: "Ripple_posed_00076" },
		{ src: "../images/flower/Ripple_posed_00077.png", id: "Ripple_posed_00077" },
		{ src: "../images/flower/Ripple_posed_00078.png", id: "Ripple_posed_00078" },
		{ src: "../images/flower/Ripple_posed_00079.png", id: "Ripple_posed_00079" },
		{ src: "../images/flower/Ripple_posed_00080.png", id: "Ripple_posed_00080" },
		{ src: "../images/flower/Ripple_posed_00081.png", id: "Ripple_posed_00081" },
		{ src: "../images/flower/Ripple_posed_00082.png", id: "Ripple_posed_00082" },
		{ src: "../images/flower/Ripple_posed_00083.png", id: "Ripple_posed_00083" },
		{ src: "../images/flower/Ripple_posed_00084.png", id: "Ripple_posed_00084" },
		{ src: "../images/flower/Ripple_posed_00085.png", id: "Ripple_posed_00085" },
		{ src: "../images/flower/Ripple_posed_00086.png", id: "Ripple_posed_00086" },
		{ src: "../images/flower/Ripple_posed_00087.png", id: "Ripple_posed_00087" },
		{ src: "../images/flower/Ripple_posed_00088.png", id: "Ripple_posed_00088" },
		{ src: "../images/flower/Ripple_posed_00089.png", id: "Ripple_posed_00089" }
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
    exportRoot = new lib.flower();

    stage = new createjs.Stage(canvas);
    stage.addChild(exportRoot);
    stage.update();

    createjs.Ticker.setFPS(24);
    createjs.Ticker.addEventListener("tick", stage);
}