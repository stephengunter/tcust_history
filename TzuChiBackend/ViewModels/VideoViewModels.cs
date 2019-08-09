using TzuChiBackend.Context;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TzuChiBackend.ViewModels
{
    public class VideoSearchForm : BaseSearchForm
    {
        public string CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryOptions { get; set; }

        public string CategoryName { get; set; }
        public IEnumerable<VideoViewModel> PagedViewResults { get; set; }
    }

    public class VideoViewModel
    {
        public VideoViewModel()
        {

        }

        public VideoViewModel(Content content)
        {
            Id = content.ContentID;
            CategoryId = content.SerialNo;
            Title = content.ContentName;
            DisplayOrder = content.DisplayOrder;
            LastUpdate =Convert.ToDateTime(content.ContentUpdateTime);

		

		}

        public string Id { get; set; }
        public string CategoryId { get; set; }

        [Required]
        public string Title { get; set; }
		public string ImageUrl { get; set; }
		public int DisplayOrder { get; set; }
        public DateTime LastUpdate { get; set; }
        public HttpPostedFileBase VideoUpload { get; set; }
		public HttpPostedFileBase ImageUpload { get; set; }
		public IEnumerable<SelectListItem> CategoryOptions { get; set; }


        


    }

    public class VideoCategorySearchForm : BaseSearchForm
    {
        public int Active { get; set; }
        public IEnumerable<VideoCategoryViewModel> ViewResultList { get; set; }
    }

    public class VideoCategoryViewModel 
    {
        public VideoCategoryViewModel()
        {

        }

        public VideoCategoryViewModel(Context.Category category)
        {

            Id = category.CategoryID;
            Name = category.CategoryName;
            LastUpdated = Convert.ToDateTime(category.UpdateTime);
            Sort = Convert.ToInt32(category.Sort);
           
        }



        public string Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
     
        public DateTime LastUpdated { get; set; }

        public string ActiveText
        {
            get
            {
                return this.Sort>=0 ? "公開" : "隱藏";
            }
        }
    }

}