<template>
  
   <div class="wbox1" style="width:1920px;">
     
      <div class="leftbox">
        <a @click.prevent="init"  href="#"> 
			  <img :src="title_img_path" width="134" height="750">
        </a>
      </div>
		<diary-list v-show="!selectedPost" ref="diaryList" :posts="getViewList()" 
		   :search_mode="searchMode" :busy="busy" @page-next="pageNext"
			@index-changed="onSlideIndexChanged" @selected="onDetails">
		</diary-list>
		
		<diary-details v-if="selected" v-show="selectedPost" :id="selected" 
			@loaded="onDetailsLoaded" @back="onBackToIndex">
			
		</diary-details>

		
      <!--右邊選單-->
		
      <diary-search ref="diarySearch"  @submit="onSearch" @leave="leave">
         <div slot="title" class="righttitle">
				<a @click.prevent="init" href="#">
            	<img :src="search_img_path" width="210" height="55">
				</a>
         </div>

      </diary-search>

		
		

		
   </div>
</template>

<script>
import DiarySearch from '../../components/diary/search';
import DiaryList from '../../components/diary/list';
import DiarySearchList  from '../../components/diary/search-list';
import DiaryDetails  from '../../components/diary/details';
export default {
   name:'DiaryView',
   components: {
		'diary-list':DiaryList,
      'diary-search':DiarySearch,
		'diary-search-list':DiarySearchList,
		'diary-details':DiaryDetails,
   },
   props: {
      title_img_path: {
         type: String,
         default: ''
      },
      search_img_path: {
         type: String,
         default: ''
      },
   },
   data() {
      return {
			busy:false,

         params: {
            terms: '',
            keyword: '',
            page: 1,
            pageSize: 10
         },
         model: null,

         searchMode:false,


         selected:0,
			selectedPost:null,

			

			
      }
   },
   watch:{
      
   },
   beforeMount() {
      this.fetchData();
   },
   mounted(){
    
    
   },
   methods: {
		init(){
			this.busy=false;

			this.params={
            terms: '',
            keyword: '',
            page: 1,
            pageSize: 10
			};
			
			this.searchMode=false;
			
			this.model=null;

			this.$refs.diarySearch.init();

			this.selected=0;
			this.selectedPost=null;

			this.fetchData();

		},
      getViewList(){
         if(this.model) return this.model.viewList;
         return [];
      },
      fetchData() {
			this.busy=true;

         let getData = Api.getDiaryList(this.params);

         getData.then(model => {
            if(this.model){
               this.model.viewList=this.model.viewList.concat(model.viewList);

               this.onPostsLoaded();
            }else{
              
               this.model = { ...model };
              
               this.onReady();

					this.onPostsLoaded();
            }  

         })
         .catch(error => {
				Helper.BusEmitError(error);
				this.busy=false;

         })
      },
		onPostsLoaded(){
			this.busy=false;

			if(this.searchMode){
				
				
			}else{

				this.$refs.diaryList.reinit();
			} 
		
		},
		
      onSlideIndexChanged(currentSlide){
         let left= this.getViewList().length - currentSlide;
         if(left <=3) this.pageNext();
        
      },
      pageNext() {
         this.params.page += 1;
         this.fetchData(); 
      },
      onSearch(params) {
        
         this.params.keyword = params.keyword;
         this.params.terms = params.terms;

			this.searchMode=true;
			this.model=null;
			
			this.fetchData();
			
			this.onBackToIndex();
      },
      onReady() {
			$('.wbox1').fadeIn();
			this.bindSlide();
		},
      bindSlide() {
			
			$('.slick-slide').hammer().bind("tap press", function (ev) {
				if ($.type($(this).find('.initxt-a').attr('href')) != 'undefined') {
					location.href = $(this).find('.initxt-a').attr('href');
				}
			});
		},
		onDetails(id){
			$('#mainContent').fadeOut();

			$('#slideStyles').html('');
			this.selected=id;

			$('#detailsContent').fadeIn();
			
		},
		onDetailsLoaded(post){
			this.selectedPost=post;
		},
		onBackToIndex(){
			$('#detailsContent').fadeOut();
			$('#slideStyles').html('.slick-track {margin-top: -300px;}');
			
		
			this.selected=0;
			this.selectedPost=null;

			$('#mainContent').fadeIn();

			this.onPostsLoaded();
			
		},
		leave(){
			this.$emit('leave');
		}
      
      
      
   }



}
</script>

<style>
.slick-track {
	width: 1400px;
}

.slick-list.draggable {
	height: 900px !important;
}

</style>








