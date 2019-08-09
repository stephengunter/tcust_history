<template>
   <div @click.prevent="active=!active"  v-if="selectedItem" :class="{ 'dd wrapper-dropdown-5': true ,active: active,  }"
          tabindex="1" :style="custom_style">
      <div  class="dd-text" id="yearDropList" >
         {{ selectedItem.text }}
      </div>
      <ul class="dropdown">
         <li @click.prevent="onSelectedChanged(item)" v-for="(item,index) in otherItems" :key="index" >
            <a href="#">{{ item.text }}</a>
         </li>
      </ul>
   </div>
</template>

<script>
export default {
   name:'YearTermDrop',
   props: {
      custom_style:{
         type:String,
         default:''
      },
      options: {
         type: Array,
         default: null
      },
      selected:{
         type:Number,
         default:0
      }
   },
   data(){
      return {
         active:false,
         selectedItem:null,
			otherItems:[],
      }
   },
   watch: {
      'selected':'init',
      'options':'init'
   },
   beforeMount() {
		this.init();
	},
   methods:{
      
      init(){
			this.selectedItem=this.options.find((item)=>{
				return item.value==this.selected;
			});
			this.otherItems=this.options.filter((item)=>{
				return item.value!=this.selected;
			})
		},
      onSelectedChanged(item){
         
         this.$emit('selected',item);
         
      },
      
   }
}
</script>

