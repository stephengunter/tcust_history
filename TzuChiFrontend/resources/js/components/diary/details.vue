<template>			
	<div id="detailsContent" v-if="post" class="centerbox">
      <!--內文1-->
      <div class="tztable1">
         <ul class="title_01">
            <li class="title_02">{{ postDate.year() }} </li>
            <li class="title_03">____________</li>
            <li class="title_04">{{ postDate.format('MMDD') }}</li>
         </ul>
         <ul class="title_05dalay">
              {{ post.title }} 
         </ul>   
         <ul>
            <li class="intxt2">
               <p>{{ post.author }} </p>
               <p v-html="post.content">
            
               </p>
            </li>
         </ul>
         <div v-if="activeImage" id="picDiv1" >
           {{ activeImage.title }}
         </div>
         <div class="imgintxt1">
            <div style="height:437px" class="image">
               <img v-show="activeImage"  width="561" :src="activeImage.previewPath">
            </div>
         </div>
         <medias ref="medias"  :items="post.medias" @index-changed="onMediaIndexChanged">

			</medias>
       
         <ul class="qrcode">
				<vue-qrcode :value="qrCode.value" :size="qrCode.size" level="H"></vue-qrcode>
         </ul>
      </div>
      <div id="getDiv" class="gettxt2">
         <a href="#" @click.prevent="back">返回</a>
      </div> 
   </div>
</template>

<script>

import Medias from './medias';
import QrcodeVue from 'qrcode.vue';
export default {
   name:'DiaryDetails',
   props: {
      id: {
         type: Number,
         default: 0
      },
	},
   components: {
		'vue-qrcode':QrcodeVue,
		Medias
   },
   data() {
      return {
         post: null,
         img:null,

			qrCode:{
				value:'https://example.com',
				size:120
			},
			
			activeImageIndex:0
      }
   },
   beforeMount() {
      this.fetchData();
   },
	mounted(){
      
   },
	computed:{
		postDate(){
         if(!this.post) return null;
			if(this.post.date) return moment(this.post.date,'YYYY-MM-DD');
			return moment(this.post.createdAt);
      },
      hasMedia(){
         if(!this.post) return false;
         if(!this.post.medias) return false;

         return this.post.medias.length > 0;
		},
		activeImage(){
			if(!this.post) return null;
			if(!this.post.medias) return null;
			if(!this.post.medias.length) return null;

			return this.post.medias[this.activeImageIndex];//.previewPath;
		}
	},
   methods:{
		fetchData() {
			this.busy=true;

         let getData = Api.postDetails(this.id);

         getData.then(post => {
				this.post = { ...post }; 
				
				this.qrCode.value=post.url;
            this.img=post.medias[0];
           
				this.$emit('loaded',post);
				
            
         })
         .catch(error => {
				Helper.BusEmitError(error);
         })
      },
		onMediaIndexChanged(index){
			this.activeImageIndex=index;
		},
      back(){
         this.$emit('back');
      }
   }
}
</script>


<style scoped>
   .shadow {
      box-shadow: 2px 2px 16px rgba(0, 0, 0, 0.6);
   }
   .slider-for {
      margin-top:54px;
      width: 582px;
      height: 437px;
   }

   .title_05dalay {
      height: 87px;
      overflow: hidden;
   }

   

   .news6 {
      overflow: hidden;
      word-break: break-all;

      padding: 5px 5px 5px 5px;
   }

   .menu5 {
      margin-top: 20px;
   }

   .qrcode {
      margin-top: -5px;
      margin-left: 900px;

   }
   
   #picDiv1 {
      position: absolute;
      font-family: "微軟正黑體", Arial;
      font-weight: bold;
      font-size: 24px;
      text-align: left;
      line-height: 36px;
      background-color: #ffffff;
      opacity: 0.8;
      padding: 5px 5px 5px 5px;
      margin: 56px 2px 2px 2px;
      width: 579px;
      height: 80px;
      z-index: 4;
      left: 918px;
      top: 538px;
      overflow: hidden;
   }

   #getDiv {
      position: absolute;
      width: 140px;
      height: 60px;
      top: 997px;
      left: 1392px;
      z-index: 7;
   }

   .intxt2 p {
   
      padding-top:15px;
   }
</style>


