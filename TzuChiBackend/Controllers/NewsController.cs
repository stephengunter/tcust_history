using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.Security;
using TzuChiBackend.Services;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;
using TzuChiBackend.Context;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        INewsManagement gNewsManagement = new NewsManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();

        private NewsService newsService;
        private CategoryService categoryService;
        public NewsController()
        {
            this.newsService = new NewsService(Context);
            this.categoryService = new CategoryService(Context);
        }

        public ActionResult ReFormat()
        {

            this.newsService.ReFormatAll();
            return Content("done");

        }

        // GET: Backend/News
        public ActionResult List()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted || string.IsNullOrEmpty(Request["ca"]))
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }

            List<CategoryModel> lstSelectData = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_NEWS_SEARCH);
            ViewBag.SearchList = new SelectList(lstSelectData, "CategoryID", "CategoryName", string.Empty).ToList();

            //頁數
            var models = gNewsManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalNews = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;
            return View(pagenation);
        }

        [HttpPost]
        public ActionResult List(PagenationModel pagenation, int totalPage)
        {
            pagenation.IsPosted = true;
            pagenation.Page = pagenation.Page > totalPage ? totalPage : pagenation.Page <= 0 ? 1 : pagenation.Page;
            Session["Pagenation"] = pagenation;
            return RedirectToAction("List", new { ca = 1 });
        }

        [HttpGet]
        public ActionResult Page(string contentID)
        {
            NewsModel result = null;
            if (string.IsNullOrEmpty(contentID))
            {
                ViewBag.Commamd = "add";
            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gNewsManagement.GetByContentID(contentID);
            }

            ViewBag.SelectYearList = this.categoryService.GetSchoolYears(true).ToSelectListItems();

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(NewsModel model, string command)
        {
            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            model.ContentCreator = Deviceidentity.UserID;
            model.ContentUpdater = Deviceidentity.UserID;

            model.Description = model.ContentText;

            if ("add".Equals(command))
            {
                if (gNewsManagement.Add(model))
                    TempData["NewsMessage"] = string.Format(@"News: {0} add successfully.", model.ContentID);
                else
                    TempData["NewsMessage"] = string.Format(@"News: {0} add fail. Please try again.", model.ContentID);
            }
            else if ("edit".Equals(command))
            {
                if (gNewsManagement.Update(model))
                    TempData["NewsMessage"] = string.Format(@"News: {0} Edit successfully.", model.ContentID);
                else
                    TempData["NewsMessage"] = string.Format(@"News: {0} Edit fail. Please try again.", model.ContentID);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(string contentId)
        {
            Boolean result = gNewsManagement.Delete(contentId);
            return RedirectToAction("List");
        }

    }
}