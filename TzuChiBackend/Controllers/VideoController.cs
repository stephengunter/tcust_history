using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TzuChiBackend.ViewModels;
using TzuChiBackend.Services;
using TzuChiBackend.Context;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class VideoController : BaseController
    {
        private readonly string frontRootPath = System.Web.Configuration.WebConfigurationManager.AppSettings["FrontRootPath"];
        private readonly string folder = System.Web.Configuration.WebConfigurationManager.AppSettings["ebook_folder"];

        private VideoService videoService;
        private FileUplaodService fileUplaodService;

		string GetImageUrl(FileUplaod image)
		{
			if (image == null) return "";

			string url = System.Web.Configuration.WebConfigurationManager.AppSettings["website.frontend"];

			string imageFolderUrl = url + "Video/img";

			return String.Format("{0}/{1}", imageFolderUrl, image.FileName);
		}

       
        public VideoController()
        {
            this.videoService = new VideoService(Context, this.frontRootPath);
            this.fileUplaodService = new FileUplaodService(Context);

        }
        #region  Index
        [HttpGet]
        public ActionResult Index(string category = "",  string keyword = "")
        {

            var model = new VideoSearchForm()
            {
                CategoryId = category,
                KeyWord = keyword

            };



            LoadOptions(model);

            SearchPosts(model);

            return View(model);

        }

        [HttpPost]
        public ActionResult Index(VideoSearchForm model)
        {

            LoadOptions(model);

            SearchPosts(model);

            return View(model);
        }
        void LoadOptions(VideoSearchForm model)
        {
           
            model.CategoryOptions = this.videoService.CategorySelectList(model.CategoryId);
            
        }
        void SearchPosts(VideoSearchForm model)
        {
            IEnumerable<Content> videoList = this.videoService.Search(model.CategoryId, model.KeyWord);


            if (videoList.IsNullOrEmpty())
            {
                model.DataCount = 0;
                return;
            }

            videoList = videoList.OrderByDescending(p => p.DisplayOrder).ThenByDescending(p=>p.ContentUpdateTime);          

           
            model.DataCount = videoList.Count();


            var viewResultList = new List<VideoViewModel>();
            foreach (var item in videoList)
            {
				var viewModel = new VideoViewModel(item);

				var image = this.videoService.GetCoverImage(item);
				viewModel.ImageUrl = GetImageUrl(image);

				viewResultList.Add(viewModel);
            }

            model.PagedViewResults = viewResultList;

        }
		[HttpGet]
		public ActionResult CategoryOptions(string selected="")
		{
			var options = this.videoService.CategorySelectList(selected);
			
			return Json(new  { options= options }, JsonRequestBehavior.AllowGet);
		}
		#endregion

		public ActionResult Create()
        {
            var model = new VideoViewModel();
            model.CategoryOptions = this.videoService.CategorySelectList();
            return View("Edit",model);
        }
        public ActionResult Edit(string id)
        {
            var video = this.videoService.GetById(id);
            if (video == null) return HttpNotFound();

            var model = new VideoViewModel(video);

			var image = this.videoService.GetCoverImage(video);
			model.ImageUrl = GetImageUrl(image);

			model.CategoryOptions = this.videoService.CategorySelectList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Store(VideoViewModel model)
        {
            bool isCreate = String.IsNullOrEmpty(model.Id);
            if (isCreate)
            {

                var validVideoTypes = new string[] { "video/mp4" };


                if (model.VideoUpload == null || model.VideoUpload.ContentLength == 0)
                {
                    ModelState.AddModelError("VideoUpload", "必須上傳影片檔案");
                }
                else if (!validVideoTypes.Contains(model.VideoUpload.ContentType))
                {
                    ModelState.AddModelError("VideoUpload", "只接受mp4檔案.");
                }

				
			}

			bool editImage = String.IsNullOrEmpty(model.ImageUrl);
			if (editImage)
			{

				if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
				{
					ModelState.AddModelError("ImageUpload", "必須上傳封面圖檔案");
				}
			}


			if (!ModelState.IsValid)
            {
                model.CategoryOptions = this.videoService.CategorySelectList();
                return View("Edit", model);
            }

            var file = model.VideoUpload;
            string title = model.Title.Trim();
            string categoryId = model.CategoryId;
            int order = model.DisplayOrder;
			var image = model.ImageUpload;



			if (String.IsNullOrEmpty(model.Id))
            {
                this.videoService.Create(file, title, categoryId, order, image);
            }
            else
            {
                this.videoService.Update(model.Id, title, categoryId, order, image);
            }

            return RedirectToAction("Index",new { category= categoryId });

        }
        [HttpPost]
        public ActionResult Remove(string id)
        {
           
            
            this.videoService.Delete(id);


            var msg = new { Success = true };
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Categories(int active=0 , int json=0)
        {
			
            var model = new VideoCategorySearchForm() {  Active = active};
           
            var categoryViewList=new List <VideoCategoryViewModel>();

            var categories = this.videoService.GetVideoCategories(active);
            if (!categories.IsNullOrEmpty())
            {
                model.DataCount = categories.Count();
                foreach (var item in categories)
                {
                    categoryViewList.Add(new VideoCategoryViewModel(item));
                }
            }

            model.ViewResultList = categoryViewList;
            return PartialView("_Categories", model);
        }

        public ActionResult AddCategory()
        {
            var model = new VideoCategoryViewModel();
            return PartialView("_EditCategory", model);
        }
        public ActionResult EditCategory(string id)
        {
            var category = this.videoService.GetCategoryById(id);
            if (category == null) return HttpNotFound();

            var model = new VideoCategoryViewModel(category);

          
            return PartialView("_EditCategory", model);
        }
        [HttpPost]
        public ActionResult StoreCategory(VideoCategoryViewModel model)
        {
            

            if (String.IsNullOrEmpty(model.Id))
            {
                this.videoService.CreateCategory(model.Name);
            }
            else
            {
                
                this.videoService.UpdateCategory(model.Id, model.Name, model.Sort);
            }
            var msg = new { Success = true };
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult UpdateOrder(IList<Content> posts)
		{

			foreach (var item in posts)
			{
				this.videoService.UpdateOrder(item.ContentID, item.DisplayOrder);
			}


			var msg = new { Success = true };
			return Json(msg, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult UpdateCategoryOrder(IList<VideoCategoryViewModel> categories)
		{

			foreach (var item in categories)
			{
				this.videoService.UpdateCategoryOrder(item.Id, item.Sort);
			}


			var msg = new { Success = true };
			return Json(msg, JsonRequestBehavior.AllowGet);
		}

	}
}