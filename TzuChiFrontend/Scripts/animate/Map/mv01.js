(function (lib, img, cjs) {

var p; // shortcut to reference prototypes

// stage content:
(lib.mv01 = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// btn1
	this.instance = new lib.btn1();
	this.instance.setTransform(1525.2,315.2);

	this.timeline.addTween(cjs.Tween.get(this.instance).to({alpha:0},55).to({_off:true},1).wait(65));

	// btn2
	this.instance_1 = new lib.btn2();
	this.instance_1.setTransform(1607.2,469.4);

	this.timeline.addTween(cjs.Tween.get(this.instance_1).to({alpha:0},55).to({_off:true},1).wait(65));

	// 升起的高度
	this.instance_2 = new lib.earth01();
	this.instance_2.setTransform(766.1,1106.8);

	this.timeline.addTween(cjs.Tween.get(this.instance_2).to({y:792.8},89).wait(32));

	// 建築
	this.instance_3 = new lib.bu();

	this.timeline.addTween(cjs.Tween.get(this.instance_3).to({alpha:0},55).to({_off:true},1).wait(65));

	// 圖層 6
	this.instance_4 = new lib.movie2();

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.instance_4}]}).wait(121));

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-402.3,0,2337,1779.8);


// symbols:
(lib.movie1 = function() {
	this.initialize(img.movie1);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,2337,1346);


(lib.movie2 = function() {
	this.initialize(img.movie2);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,1920,1080);


(lib.movie3 = function() {
	this.initialize(img.movie3);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,1280,720);


(lib.movie4 = function() {
	this.initialize(img.movie4);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,341,106);


(lib.movie5 = function() {
	this.initialize(img.movie5);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,341,106);


(lib.earth01 = function() {
	this.initialize();

	// 圖層 1
	this.instance = new lib.movie1();
	this.instance.setTransform(-1168.4,-672.9);

	this.addChild(this.instance);
}).prototype = p = new cjs.Container();
p.nominalBounds = new cjs.Rectangle(-1168.4,-672.9,2337,1346);


(lib.bu = function() {
	this.initialize();

	// 圖層 1
	this.instance = new lib.movie3();
	this.instance.setTransform(0,0,1.5,1.5);

	this.addChild(this.instance);
}).prototype = p = new cjs.Container();
p.nominalBounds = new cjs.Rectangle(0,0,1920,1080);


(lib.btn2 = function() {
	this.initialize();

	// 圖層 1
	this.instance = new lib.movie5();
	this.instance.setTransform(-161.9,-52.9);

	this.shape = new cjs.Shape();
	this.shape.graphics.lf(["rgba(255,255,255,0)","#FFFFFF","rgba(255,255,255,0.796)","rgba(255,255,255,0)"],[0,0.533,0.576,1],-314.5,0,314.7,0).s().p("EgxJAAqIAAhTMBiTAAAIAABTg");
	this.shape.setTransform(0,48.3);

	this.addChild(this.shape,this.instance);
}).prototype = p = new cjs.Container();
p.nominalBounds = new cjs.Rectangle(-314.6,-52.9,629.4,106);


(lib.btn1 = function() {
	this.initialize();

	// 圖層 1
	this.instance = new lib.movie4();
	this.instance.setTransform(-172.5,-52.9);

	this.shape = new cjs.Shape();
	this.shape.graphics.lf(["rgba(255,255,255,0)","#FFFFFF","rgba(255,255,255,0.796)","rgba(255,255,255,0)"],[0,0.533,0.576,1],-314.5,0,314.7,0).s().p("EgxJAAqIAAhTMBiTAAAIAABTg");
	this.shape.setTransform(0,44.7);

	this.addChild(this.shape,this.instance);
}).prototype = p = new cjs.Container();
p.nominalBounds = new cjs.Rectangle(-314.6,-52.9,629.4,106);

})(lib = lib||{}, images = images||{}, createjs = createjs||{});
var lib, images, createjs;