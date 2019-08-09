(function (lib, img, cjs) {

var p; // shortcut to reference prototypes

// stage content:
(lib.cloud = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// 圖層 1
	this.instance = new lib.補間動畫1("synched",0);
	this.instance.setTransform(3573.5,307);

	this.timeline.addTween(cjs.Tween.get(this.instance).to({x:-742.3},699).to({_off:true},1).wait(100));

	// cloud2.png
	this.instance_1 = new lib.補間動畫2("synched",0);
	this.instance_1.setTransform(2411,327.5);

	this.timeline.addTween(cjs.Tween.get(this.instance_1).to({x:-2190.8},699,cjs.Ease.get(-0.49)).to({_off:true},1).wait(100));

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(1937,-107.9,2376,830);


// symbols:
(lib.cloud_1 = function() {
	this.initialize(img.cloud_1);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,1479,830);


(lib.cloud2 = function() {
	this.initialize(img.cloud2);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,948,577);


(lib.補間動畫2 = function() {
	this.initialize();

	// 圖層 1
	this.instance = new lib.cloud2();
	this.instance.setTransform(-473.9,-288.4);

	this.addChild(this.instance);
}).prototype = p = new cjs.Container();
p.nominalBounds = new cjs.Rectangle(-473.9,-288.4,948,577);


(lib.補間動畫1 = function() {
	this.initialize();

	// 圖層 1
	this.instance = new lib.cloud_1();
	this.instance.setTransform(-739.4,-414.9);

	this.addChild(this.instance);
}).prototype = p = new cjs.Container();
p.nominalBounds = new cjs.Rectangle(-739.4,-414.9,1479,830);

})(lib = lib||{}, images = images||{}, createjs = createjs||{});
var lib, images, createjs;