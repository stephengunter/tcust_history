class Api {
   static source() {
		return 'http://localhost:50001/api';
   }
   
   static getTermYears(){
      let url =`${this.source()}/terms`;
    
		return new Promise((resolve, reject) => {
			axios.get(url)
				  .then(response => {
						resolve(response.data);
				  })
				  .catch(error => {
						reject(error);
				  })

		})
	}
	

	static getDiaryList(params){
		let url =`${this.source()}/posts/GetDiaryList`;

		url=Helper.buildQuery(url, params);

		return new Promise((resolve, reject) => {
			axios.get(url)
				  .then(response => {
						resolve(response.data);
				  })
				  .catch(error => {
						reject(error);
				  })

		})

	}
	static getHonorList(params){
		let url =`${this.source()}/posts/GetHonorList`;

		url=Helper.buildQuery(url, params);

		return new Promise((resolve, reject) => {
			axios.get(url)
				  .then(response => {
						resolve(response.data);
				  })
				  .catch(error => {
						reject(error);
				  })

		})

	}
	static getFamerList(params){
		let url =`${this.source()}/posts/GetFamerList`;

		url=Helper.buildQuery(url, params);

		return new Promise((resolve, reject) => {
			axios.get(url)
				  .then(response => {
						resolve(response.data);
				  })
				  .catch(error => {
						reject(error);
				  })

		})

	}

	static postDetails(id){
		let url =`${this.source()}/posts/${id}`;

		return new Promise((resolve, reject) => {
			axios.get(url)
				  .then(response => {
						resolve(response.data);
				  })
				  .catch(error => {
						reject(error);
				  })

		})
	}
   
   

   
}


export default Api;