<template>
   <div class="swiper-slide">
      <div >
      
			<div class="yearDiv" style="overflow:hidden;">       
				<div class="yearTitle">
					{{ title }}
				</div>
				
				<honor-item v-if="items && items.length>0" :post="items[0]" @selected="onSelected"></honor-item>
				<honor-item v-if="items && items.length>1" :post="items[1]" @selected="onSelected"></honor-item>

				<div v-if="items && items.length>2" class="yearNew2">
					<ul class="NewImg" style="width:423px;height:260px;display: table-cell; float:none; background-color: transparent;" align="center">
                  <img :src="getYearImgSrc()" height="114" style="max-width:423px; text-align: center; margin-left: auto; margin-right: auto;">
               </ul>
					<ul class="Newbk">
						<li class="Newtxt">
						</li>
					</ul>
				</div>
														

			</div>
      </div>
   </div>
</template>

<script>
import HonorItem from './item';
export default {
	name:'YearItem',
	components: {
		'honor-item':HonorItem
	},
	props: {
		id:{
			type: String,
         default: ''
		},
      title: {
         type: String,
         default: ''
		},
		items: {
         type: Array,
         default: null
      }
	},
	computed:{
		
	},
	methods:{
		getYearImgSrc(){
			let year=parseInt(this.title);
			if(!year) year=3000;
			else if(year < 2011) year=2011;
			return `/Images/HonourList/third/${year}.png`;
			
		},
		onSelected(id){
			this.$emit('selected', this.title , id);
		}
	}
}
</script>

