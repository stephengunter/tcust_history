<template>
<ul class="imgintxt2">
   
   <li class="arrowl" style="margin-left:-5px;">
      <a href="#" @click.prevent="prev" >
         <img src="/Scripts/inform/images/inform/arrowl.png" width="20" height="27">
      </a>
   </li>
   <li class="slider-nav" style="float:left;margin-left:10px;">
   
      <slick ref="mediaSlick" :options="slickOptions"  @beforeChange="onSlideIndexChanged">
         <div v-for="(item,index) in  items" :key="index" :class="{ newglow:true, hollow:!isActive(index) }">   
            <div  style="display: table-cell; vertical-align: middle; align:center; width:170px ; height:130px" >
               <img width="172" style="max-height:124px; text-align: center; margin-left: auto; margin-right: auto;"
               class="newglow-img" :src="item.previewPath" />
            </div>
         </div>
         
      </slick>
  
   </li>   
   
   
   <div  z-index="10" class="arrowl" style="float:right;margin-right:15px;margin-top:-10px">
      <a href="#" @click.prevent="next" >
         <img src="/Scripts/inform/images/inform/arrowr.png" width="20" height="27">
      </a>
   </div>
</ul> 
</template>

<script>

import Slick from 'vue-slick';

export default {
   name:'Medias',
   components: {
      Slick,
   },
   props: {
      items: {
         type: Array,
         default: null
      },
   },
   data() {
      return {
         
         slickOptions: {
            prevArrow: false,
            nextArrow: false,
            infinite: true,
            slidesToShow: 3,
            slidesToScroll: 1,
            focusOnSelect: true,
         },

         activeIndex:0,
         activeImage:null,
      };
   },
   beforeMount() {
      
   },
   mouted(){
      this.reInit();
   },
   methods: {
      next() {
         this.$refs.mediaSlick.next();
      },
      prev() {
         this.$refs.mediaSlick.prev();
      },
      reInit() {
         this.$nextTick(() => {
            this.$refs.slick.reSlick();
         });
      },
      isActive(index){
         return index==this.activeIndex;
      }, 
      onSlideIndexChanged(event, slick, currentSlide, nextSlide){
         this.activeIndex=nextSlide;
         this.$emit('index-changed',nextSlide);
        
      },
   },
}
</script>


<style scoped>
.imgintxt2 {
    float: left;
    display: block;
    width: 600px;
    height: 130px;
    padding: 0px 0px 0px 0px;
    margin: 20px 0px 0px 10px;
}
.slider-nav {
   width: 510px !important;
   height: 124px;
}

.slick-prev {
   display: none !important;
}

.slick-next {
   display: none !important;
}
.newglow {
    /* float: left; */
    display: block;
    width: 180px;
    height: 130px;
    padding: 3px 0px 0px 0px;
    margin: 0px 0px 0px 0px;
    opacity: 1;
}
.hollow {
    opacity: 0.5;
}
</style>


