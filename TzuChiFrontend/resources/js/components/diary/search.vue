<template>
   <div  class="rightbox">
		<div class="div2">
			<section class="main">
				
				<div class="wrapper-demo">
					<year-term-drop v-if="loaded" :options="yearOptions" :selected="yearValue" custom_style="z-index:3"
					   @selected="onYearSelected">

					</year-term-drop>
				</div>
			</section>
			<section class="main">
				<div class="wrapper-demo">
					<year-term-drop v-if="loaded" :options="termOptions" :selected="termValue" custom_style="z-index:2"
					   @selected="onTermSelected">
					</year-term-drop>

					
				</div>
			</section>
		</div>
		<div>
			<div class="menu5">
				<input name="textfield" v-model="params.keyword"  type="text" class="textbox" id="keyWordInput" placeholder="輸入" size="10" maxlength="5">
			</div>
			
			<slot name="title"> 
     
         </slot> 
			
			<div class="btnright">
				<a @click.prevent="onSubmit" href="#">搜尋</a>
			</div>
			<div class="btnIcon">
				<a @click.prevent="leave" href="#"></a>
			</div>
		</div>
		

	</div>
</template>

<script>
import YearTermDrop from './year-term-drop';
export default {
	name:'DiarySearch',
	components: {
		'year-term-drop':YearTermDrop,
	},
   props: {
      model: {
         type: Object,
         default: null
      }
   },
   data(){
      return {
         loaded:false,

         yearTerms:[],
         yearOptions:[{
            value:0,
            text:'學年度'
         }],
         termOptions:[{
            value:0,
            text:'學期'
         }],
         yearTerm:null,
			termId:0,
			

			params:{
				terms:'',
				keyword:'',
			},
         
      }
   },
	computed:{
      yearValue(){
         if(this.yearTerm) return this.yearTerm.id;
         return 0;
      },
      termValue(){
         if(this.termId) return this.termId;

         if(!this.yearValue) return 0;
      
         if(!this.termOptions || !this.termOptions.length)   return 0;

         return this.termOptions[0].value;
      }
   }, 
	beforeMount() {
      this.fetchData();
	},
   methods:{
		init(){
			
			this.loaded=false;

         this.yearTerms=[];
         this.yearOptions=[{
            value:0,
            text:'學年度'
         }];
         this.termOptions=[{
            value:0,
            text:'學期'
         }];
         this.yearTerm=null;
			this.termId=0;
			

			this.params={
				terms:'',
				keyword:'',
			};

			this.fetchData();

		},
		fetchData(){
			let terms=Api.getTermYears();
			terms.then(data => {
				this.initYearTerms(data);
			})
			.catch(error => {
				Helper.BusEmitError(error);
				
			})
		},
      initYearTerms(yearTerms){
         this.yearTerms=yearTerms;
         
         yearTerms.forEach((item)=>{
            this.yearOptions.push( {
               value:item.id,
               text:item.year
            });
         });

         this.loaded=true;
         
		},
      onYearSelected(year){
			
         this.termId=0;
         if(!year.value){
				this.yearTerm=null;
				this.termOptions=[{
               value:0,
               text:'學期'
            }];


            this.$emit('changed',this.getQuery());
            return;
         } 
        

         let getYear = new Promise((resolve, reject)=> {
            let yearTerm=this.yearTerms.find((item)=>{
               return item.id==year.value;
            });
            resolve(yearTerm);
         });

         getYear.then((yearTerm) => {
            if(yearTerm){
               this.yearTerm=yearTerm;
               this.$emit('changed',this.getQuery());
               this.loadTermOptions();
            }else{
               this.yearTerm=null;
               this.$emit('changed',this.getQuery());
            }  
            
         });
      },
      loadTermOptions(){
         let termOptions=[{
               value:0,
               text:'學期'
            }];
         let getOptions = new Promise((resolve, reject)=> {
            
            this.yearTerm.terms.forEach((item)=>{
               termOptions.push({
                  value:item.id,
                  text:item.title
               });
            });
            resolve(termOptions);
         });

         getOptions.then((options) => {
            this.termOptions=options;
         });
         
      },
      onTermSelected(term){
         this.termId=term.value;
         this.$emit('changed',this.getQuery());
      },
      getQuery(){
         if(!this.yearTerm) return '';

         if(this.termId){
           
            let term=this.yearTerm.terms.find((item)=>{
               return item.id==this.termId;
            });

            return term.number;

         }else{
          
            let termNumbers = this.yearTerm.terms.map((item)=>{
               return item.number;
            });

            return termNumbers.join();
         } 

		},
		onSubmit(){
			let getQuery = new Promise((resolve, reject)=> {
				let query=this.getQuery();
            resolve(query);
			});
			
			getQuery.then((terms) => {
				
				this.params.terms=terms;
				this.$emit('submit',this.params);
         });
			
			
		},
      back(){
         this.$emit('back');
		},
		leave(){
         this.$emit('leave');
		},
   }
}
</script>


<style scoped>
   .menu5 {
      margin-top: 20px !important;
   } 
</style>


