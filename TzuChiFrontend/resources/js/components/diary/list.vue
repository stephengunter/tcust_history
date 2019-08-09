<template>
   <div id="mainContent" class="centerbox">
		<search-list v-if="search_mode" :busy="busy" :posts="posts" 
			@next-page="pageNext" @selected="selected">

		</search-list>

		<div v-else style="vertical-align:middle; position:absolute; margin-top:100px;">
			<slick ref="slick" :options="slickOptions" @beforeChange="onSlideNext">
				<diary-item v-for="(post,index) in posts" :key="index" :post="post" 
					@selected="selected">
				</diary-item>
			</slick>
		</div> 
   </div>
	
</template>

<script>

import Slick from 'vue-slick';
import DiaryItem from './item';

import SearchList  from './search-list'; 

export default {
   name:'DiaryList',
   components: {
      Slick,
		'diary-item':DiaryItem,
		'search-list':SearchList
   },
   props: {
      posts: {
         type: Array,
         default: null
		},
		busy: {
         type: Boolean,
         default: false
		},
		search_mode: {
         type: Boolean,
         default: false
		},
   },
   data() {
      return {
         slickOptions: {
            centerMode: true,
            prevArrow: false,
    			nextArrow: false,
				dots: false,
            vertical: true,
            verticalSwiping:true,
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: false,
           
         },
      }
   },
   methods: {
      reinit(){
         let currIndex = this.$refs.slick.currentSlide();

         this.$refs.slick.destroy();
         this.$nextTick(() => {
            this.$refs.slick.create();
            this.$refs.slick.goTo(currIndex, true);
         })
      },
      onSlideNext(event, slick, currentSlide, nextSlide){
         this.$emit('index-changed', currentSlide);
        
		},
		pageNext(){
			this.$emit('page-next');
		},
      selected(id){
			this.$emit('selected',id);
		}
      
   }
}
</script>
