<template>
  

	<div class="intxt5 scrollFrame" v-infinite-scroll="loadMore" infinite-scroll-disabled="busy" infinite-scroll-distance="10">
		<ul class="Search" v-for="(post,index) in posts" :key="index">
			<li class="Seartxt">
				<a href="#" @click.prevent="selected(post.id)">{{ post.title }}</a> 
				<span class="pull-right" > {{ getDate(post) }} </span> 
			</li>
			
		</ul>
	</div>
</template>

<script>
export default {
   name:'DiarySearchList',
	props: {
      posts: {
         type: Array,
         default: null
		},
		busy: {
         type: Boolean,
         default: false
      },
	},
	data() {
		return{
			
		}
		
   },
	methods: {
		getDate(post){
			if(post.date) return post.date;

			let date = moment(post.createdAt);
			return date.format("YYYY-MM-DD") ;
		},
		loadMore(){
			if(!this.posts) return;
			if(!this.posts.length) return;
			if(this.busy) return;
			this.$emit('next-page');
		},
		selected(id){
			this.$emit('selected',id);
		}
	}

}
</script>


<style>
   .menu5 {
		margin-top: 20px;
	}


	.Search {
		text-align: left !important;
		padding-left: 50px;
		overflow: hidden;
	}
</style>