<template>
  
   <div v-if="post" class="yearNew1">
      <ul class="yearUp1">
         <li class="yearUp1txt">
           {{ postDate.format('MM') }}.{{ postDate.format('DD') }}
         </li>
      </ul>
      <ul class="yearUp2">
         <li class="arrowUp">
            <a href="#" @click.prevent="onSelected" >
            </a>
         </li>
      </ul>                               
      
      <ul v-if="photoSrc" class="NewImg" style="width:423px;height:260px;display: table-cell;  vertical-align: middle; float:none;" align="center">
       
         <a href="#" @click.prevent="onSelected">
            <img v-if="lockWidth" :src="photoSrc" width="423" style="max-height:260px; text-align: center;"   >
            <img v-else  :src="photoSrc" height="260" style="max-width:423px; text-align: center; margin-left: auto; margin-right: auto;" >
         </a> 
      </ul>
      <ul v-else class="NewImg" style="background-color:white"></ul>
                                             
      <ul class="Newbk">
         <li class="Newtxt">
            {{ post.title }}
         </li>
      </ul>
   </div>
															
				
														

</template>

<script>
export default {
	name:'HonorItem',
	props: {
		post: {
         type: Object,
         default: null
      }
   },
   computed:{
		postDate(){
			if(this.post.date) return moment(this.post.date,'YYYY-MM-DD');
			return moment(this.post.createdAt);
      },
      photoSrc(){
         if(!this.getPhoto()) return '';
         return this.getPhoto().previewPath;
      },
      lockWidth(){
        
         return Photo.lockWidth(this.getPhoto());
         
      },
   },
   methods:{
		getPhoto(){
         if(!this.post) return null;
         return this.post.cover;
			
      },
      onSelected(){
         this.$emit('selected', this.post.id);
      }
	}
	
}
</script>

