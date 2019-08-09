<template>
   <div>
      <div id="UpDiv">
         <ul class="arrowU">
            <a href="#" @click.prevent="changeIndex(false)"></a>
         </ul>
      </div>
      <div id="DownDiv">
         <ul class="arrowD" @click.prevent="changeIndex(true)">
            <a href="#"></a>
         </ul>
      </div>
      <div class="wbox">
         <div id="SliderDiv3">
            <div class="DailyDiv" style="overflow:hidden;">
               <div :class="{ DailyTitle : true , conversionYear: year==3000  }">
                 {{ year }} 
               </div>
              
               <div class="verticalPic" id="container" > 
								<div  class="DailyNew1" style="color:#000000; opacity:0.6;">
									<ul class="yearUp1">
										<li class="yearUp1txt">
											12.11
										</li>
									</ul>
									<ul class="yearUp2">
										<li class="arrowUp">
											<a href="#"></a>
										</li>
									</ul>
									<ul class="NewImg" style="display:table-cell;  vertical-align: middle; float:none; " >
											
										
										<img src="http://203.64.34.2/Uploads//20150726/2fca6c54-a2bf-44a3-b05d-776390b0358c.JPG" width="423" style="max-height:260px; text-align: center;" >
									
									</ul>
									<ul class="Newbk">
										<li class="Newtxt">
											title
										</li>
									</ul>
								</div>
								<div  class="DailyNew1" style="color:#000000; opacity:0.6;">
									<ul class="yearUp1">
										<li class="yearUp1txt">
											12.11
										</li>
									</ul>
									<ul class="yearUp2">
										<li class="arrowUp">
											<a href="#"></a>
										</li>
									</ul>
									<ul class="NewImg" style="display:table-cell;  vertical-align: middle; float:none; " >
											
										
										<img src="http://203.64.34.2/Uploads//20150726/2fca6c54-a2bf-44a3-b05d-776390b0358c.JPG" width="423" style="max-height:260px; text-align: center;" >
									
									</ul>
									<ul class="Newbk">
										<li class="Newtxt">
											title
										</li>
									</ul>
								</div>
								<div  class="DailyNew1" style="color:#000000; opacity:0.6;">
									<ul class="yearUp1">
										<li class="yearUp1txt">
											12.11
										</li>
									</ul>
									<ul class="yearUp2">
										<li class="arrowUp">
											<a href="#"></a>
										</li>
									</ul>
									<ul class="NewImg" style="display:table-cell;  vertical-align: middle; float:none; " >
											
										
										<img src="http://203.64.34.2/Uploads//20150726/2fca6c54-a2bf-44a3-b05d-776390b0358c.JPG" width="423" style="max-height:260px; text-align: center;" >
									
									</ul>
									<ul class="Newbk">
										<li class="Newtxt">
											title
										</li>
									</ul>
								</div>
              
               </div>
            </div>
         </div>
         <div id="DailyDiv" class="WINglow">
            <div>
               <ul class="WINTitle">活動日誌</ul>
               <ul class="WINTitle2">照片紀錄</ul>
               <ul class="WINTitle3">
                  <li class="WINclose">
                     <a href="#"></a>
                     
                  </li>
               </ul>
            </div>
            <div class="WINin1">
               <ul class="inTxtL">
                  <li class="inTxt1">

                  </li>
                  <li class="inTxt2">
                  </li>
                  <li>

                  </li>
               </ul>
               
            </div>
            <div class="WINin2">
               <ul class="inTxtR">
                  


                  
               </ul>
            </div>
         </div>
      </div>         
   </div>
</template>

<script>
import Slick from 'vue-slick';
import HonorSlick from '../../components/honor/slick';

export default {
   name:'HonorDetailsView',
   components: {
      Slick,
      'honor-slick':HonorSlick
   },
   props: {
      year: {
         type: Number,
         default: 0
      },
      id:{
         type: Number,
         default: 0
      },
   },
   data(){
      return {
         
         years:[1,2,3,4,5],
         honorsModels:[],
      }
   },
   beforeMount() {
      // let honors=Api.getHonorList();
      // honors.then(model=>{
      //    this.honorsModels= model.slice();
      //    this.reinit();
      // })
   },
   mounted(){
		//this.reinit();
      this.onReady();
   },
   computed:{
      lockWidth(){
         return Photo.lockWidth(this.getPhoto());
         
      },
   },
   methods:{
		getSlick(){
			return $('.verticalPic');
		},
      getPosts(){
         if(!this.honorsModels.length) return [];
         return this.honorsModels[0].posts;
      },
		getPhoto(){
         return null;
			
      },
      onSelected(){
         this.$emit('selected', this.post.id);
      },
      onSlickChanged(){

		},
		onReady(){
			this.getSlick().slick({
				dots: false,
				slidesToScroll: 1,
				arrows: false,
				centerMode: false,
				dots: false,
				vertical: true,
				slidesToShow: 2,
				infinite: true,
				draggable: true,
				onAfterChange:this.onSlickIndexChanged,
				swipe: true,
				swipeToSlide: true,
				touchMove: true,
				variableWidth: true
			});
		},
      reinit(){
         let currIndex = this.$refs.slick.currentSlide();

         this.$refs.slick.destroy();
         this.$nextTick(() => {
            this.$refs.slick.create();
            this.$refs.slick.goTo(currIndex, true);
         })
		},
		changeIndex(plus){
			let slick=this.getSlick();
			let currentIndex=slick.slickCurrentSlide();

			if(plus) slick.slickGoTo(currentIndex+1);
			else slick.slickGoTo(currentIndex-1);
			
		},
		onSlickIndexChanged(slider,index){
			alert(index);

			
			
			// index = parseInt(index, 10);
			// focusPic(index);
			// var className = "DailyNew" + index;
			// var contentID = $("." + className).not('div[class*=cloned]').attr("id");
			// getByContentID('@Url.Action("GetByContentID", "HonourList")', contentID);
		}
	}
}
</script>


<style scoped>

#keyDiv1 {
	position: absolute;
	width: 308px;
	height: 52px;
	z-index: 99;
	left: 1565px;
	top: 25px;
}

#SliderDiv3 {
	position: absolute;
	width: 505px;
	height: 1080px;
	z-index: 40;
	left: 0px;
	visibility: visible;
}

#UpDiv {
	position: absolute;
	width: 70px;
	height: 36px;
	z-index: 51;
	left: 168px;
	top: 101px;
}

#DownDiv {
	position: absolute;
	width: 423px;
	height: 56px;
	z-index: 51;
	left: 0px;
	bottom: 0px;
	background-image: url(/images/HonourList/Downdivbk.png);
}

#DailyDiv {
	position: absolute;
	width: 1450px;
	height: 925px;
	z-index: 30;
	left: 318px;
	top: 109px;
}

.yearUp1 {
	width: 365px !important;
}

video {
	width: 1210px;
	-webkit-background-size: cover;
	-moz-background-size: cover;
	-o-background-size: cover;
	background-size: cover;
}

h1, h2, h3, h4, h5, h6, .h1, .h2, .h3, .h4, .h5, .h6 {
	font-family: "華康黑體","微軟正黑體";
}

.conversionYear {
	font-size: 65px;
	margin: -30px 0px 30px 0px;
	padding: 50px 0px 0px 39px;
}

.inTxt2 {
	font-size: 22px;
	line-height: 35px;
}

</style>


