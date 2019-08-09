
using ApplicationCore.Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TzuChiBackend.ViewModels;

using TzuChiBackend.Services.Blog;
using Blog.Models;

namespace TzuChiBackend.Controllers
{
	[Authorize]
	public class TopsController : BaseController
    {
        private readonly IPostService postService;
        public TopsController(IPostService postService)
        {

			this.postService = postService;
		}

        // GET: Tops
        public ActionResult Index()
        {
            var model = new PostSearchModel();

            var viewList = new List<PostViewModel>();
            var postList = postService.TopPosts();
            if (postList.IsNullOrEmpty()) return View(model);

            bool coverImageOnly = true;
            foreach (var post in postList)
            {
                viewList.Add(new PostViewModel(post, coverImageOnly));
            }

            model.DataCount = viewList.Count();
            model.PostViewList = viewList;


            return View(model);
        }

        public ActionResult Select(string page = "", string keyword = "")
        {
            var model = new PostSearchModel()
            {
                KeyWord = keyword.Trim(),
                PageNumber = page.ToInt(),
                PageSize = 10,
            };

            SearchPosts(model);
            return PartialView("_Select", model);

           
        }

        void SearchPosts(PostSearchModel model, bool archives = false)
        {
            if (model.PageNumber < 1) model.PageNumber = 1;
            if (model.PageSize < 1) model.PageSize = 10;

            var postList = postService.GetAll().Where(p => !p.Top);
            
            string keyword = model.KeyWord;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                postList = postList.Where(p => p.Title != null && p.Title.Contains(keyword));

            }

            postList = postList.OrderByDescending(p => p.DisplayOrder).ThenByDescending(p => p.CreatedAt);

            int page = model.PageNumber;
            model.DataCount = postList.Count();
            model.PagedRecordResults = postList.ToPagedList(page, model.PageSize);


            if (postList.IsNullOrEmpty()) return;

            var viewList = new List<PostViewModel>();
            foreach (var post in model.PagedRecordResults)
            {
                bool coverOnly = true;
                viewList.Add(new PostViewModel(post, coverOnly));
            }

            model.PostViewList = viewList;

        }

        public ActionResult Edit(int id)
        {
            var post = postService.GetById(id);
           
            if (post == null) return HttpNotFound();

            var model = new PostEditForm(post);

            return PartialView("_Edit",model);


        }

        [HttpPost]
        public ActionResult Update()
        {
            var id= Request["Id"].ToInt();

            string title = Request["Title"];
            string summary = Request["Summary"];
            int coverId = Request["CoverId"].ToInt();

            this.postService.UpdateTopPost(id, title, summary ,coverId);


            var msg = new { Success = true };
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            this.postService.RemoveTopPost(id);


            var msg = new { Success = true };
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateOrder(IList<Post> posts)
        {
			
			foreach (var item in posts)
			{
				this.postService.UpdateOrder(item.Id, item.DisplayOrder);
			}


			var msg = new { Success = true };
			return Json(msg, JsonRequestBehavior.AllowGet);
		}
    }
}