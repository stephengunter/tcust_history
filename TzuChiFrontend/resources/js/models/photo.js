class  Photo {
  
	static lockWidth(photo){
		if(!photo) return true;

      let widthHeightRatio = 423 / 260;

      try {
         imageRatio = Number(photo.width) / Number(photo.height);

         return widthHeightRatio <= imageRatio;
      }
      catch(err) {
         return true;
      }
   }
   
   
   

   
}


export default Photo;