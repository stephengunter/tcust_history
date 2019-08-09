<template>
   <div>
      <search-form @search="onSearch"></search-form>
      <div class="wbox">
         <div class="swiper-container">
            <div class="swiper-wrapper empty">
               <empty />
               <year-item  title="傑出校友" :items="famers" key="3000"
               	@selected="onSelected">
               </year-item>

               

               <year-item v-for="(model,index) in honorsModels" :key="index" 
                   :title="String(model.year)" :items="model.posts"
                   @selected="onSelected" >

               </year-item>
               
               <empty />
               <empty />
            </div>
         </div>

         <div id="flowerDiv1">
            <canvas id="canvas" width="645" height="700"></canvas>
         </div>   
      </div>
   </div>
</template>

<script>
import SearchForm from '../../components/honor/search-form';
import Empty from '../../components/honor/empty';
import YearItem from '../../components/honor/year-item';

export default {
   name: 'HonorView',
   components: {
      'search-form': SearchForm,
      'year-item' : YearItem,
      Empty,
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

         honorsModels:[],

         famerModel:null,

         model: null,
        
         honors:[],
        

         searchMode:false,


         selected:0,
         selectedPost:null,
         
         
			
      }
   },
   computed: {
      famers(){
         if(this.famerModel) return this.famerModel.viewList;
         return [];
      }
   },
   beforeMount() {
      this.fetchData();
   },
   mounted() {
     
   },
   methods: {
      onSearch(){
         this.$emit('search');
      },
      fetchData(){
         let fetchFamers=Api.getFamerList();
         let fetchHonors=Api.getHonorList();
         Promise.all([fetchFamers, fetchHonors]).then(values => { 
            this.famerModel=  { ...values[0] }; 
            this.honorsModels= values[1].slice();

            this.onReady();
         });
      },
      onReady(){
         this.$nextTick(() => {
            this.$emit('ready');
         })
		},
		onSelected(year,id){
			
			if(isNaN(year)){
				this.$emit('selected',3000, id );
			}else{
				this.$emit('selected',parseInt(year), id );

			}
			
		}
   }
}
</script>

