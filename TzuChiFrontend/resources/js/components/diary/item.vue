<template>			
	<div v-if="post" class="tztable">
		<ul class="title_01">
			<li class="title_02">{{ postDate.year() }} </li>
			<li class="title_03">____________</li>
			<li class="title_04">{{ postDate.format('MMDD') }}</li>
		</ul>
		<ul class="title_05">
			<li style="width:510px;overflow:hidden">
				{{  post.title }}
			</li>
		</ul>
		<ul>
			<li class="intxt">
				<p>{{  post.author }}</p>

				<p v-html="post.content">
					
				</p>
			</li>
			<li class="imgintxt">

				<div style="width:443px;height:325px;display: table-cell;  vertical-align: middle;" align="center">
					<img :src="post.cover.previewPath" height="325" style="max-width:443px; text-align: center; margin-left: auto; margin-right: auto;" >
				</div>

			</li>
			<li class="intxt1">
				<a @click.prevent="selected(post.id)" href="#" class="initxt-a" style="z-index:99999" >more....</a>
			</li>


		</ul>
	</div>
</template>

<script>
export default {
   name:'DiaryItem',
   props: {
      post: {
         type: Object,
         default: null
      },
	},
	mounted(){
      this.$emit('loaded');
   },
	computed:{
		postDate(){
			if(this.post.date) return moment(this.post.date,'YYYY-MM-DD');
			return moment(this.post.createdAt);
		}
	},
   methods:{
		selected(id){
			this.$emit('selected',id);
		}
   }
}
</script>


<style scoped>

   .tztable {
      width: 1240px !important;
   }

   

   .title_05 {
      overflow: hidden;
      width: 600px;
      height: 90px;
      word-break: break-all;
   }

   .imgintxt {
      overflow: hidden;
   }

   

   

   .intxt {
      word-break: break-all;
      width: 620px;
      height: 200px;
      overflow: hidden;
   }


	
   

</style>

