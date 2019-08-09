<template>
  
   <!--關鍵字-->
   <div id="keyDiv1">
      <form @submit.prevent="onSubmit" name="searchFrom">
         <ul class="menu5">
            <input name="textfield" v-model="keyword" type="text" class="menu5" id="textfield"  placeholder="關鍵字" size="10">
            <input name="nextPage" type="hidden" value="1">
         </ul>
         <ul class="btnright">
            <a href="#"  @click.prevent="onSubmit" >
               搜尋
            </a>
         </ul>
      </form>    
   </div>
  
</template>

<script>
export default {
   name: 'SearchForm',
   data() {
      return {
			keyword:'',
			
      }
   },
   methods: {
      onSubmit(){
         this.$emit('search',this.keyword);
      },
      
   }
}
</script>

