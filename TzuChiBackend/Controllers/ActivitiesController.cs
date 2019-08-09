using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        //多元活動
        IActivitiesManagement gActivitiesManagement = new ActivitiesManagementImpl();

        // GET: Backend/Activities
        public ActionResult List()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted)
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }
            //檢索搜尋 todo
            //List<CategoryModel> lstSelectData = gCategoryManagement.GetByContentCategory(CategoryModel.CATEGORY_TYPE_LASTNEWS_SEARCH);
            //ViewBag.SearchList = new SelectList(lstSelectData, "CategoryID", "Name", string.Empty).ToList();
            //頁數
            var models = gActivitiesManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalLastNews = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;
            return View(pagenation);
        }

        [HttpPost]
        public ActionResult List(PagenationModel pagenation, int totalPage)
        {
            pagenation.IsPosted = true;
            pagenation.Page = pagenation.Page > totalPage ? totalPage : pagenation.Page <= 0 ? 1 : pagenation.Page;
            Session["Pagenation"] = pagenation;
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Page(string contentID)
        {
            ActivitiesModel result = null;
            if (string.IsNullOrEmpty(contentID))
            {
                ViewBag.Commamd = "add";
            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gActivitiesManagement.GetByContentId(contentID);
            }

            //todo
            //List<CategoryModel> listType = gCategoryManagement.GetByCategoryTypeId(CategoryModel.CATEGORY_TYPE_NEWS);
            //ViewBag.SelectTypeList = new SelectList(listType, "CategoryID", "Name").ToList();

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(ActivitiesModel model, string command)
        {
            //var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            //model.ContentCreator = Deviceidentity.UserID;
            //model.ContentUpdater = Deviceidentity.UserID;

            if ("add".Equals(command))
            {
                if (gActivitiesManagement.Add(model))
                    TempData["LastNewsMessage"] = string.Format(@"LastNews: {0} add successfully.", model.ContentID);
                else
                    TempData["LastNewsMessage"] = string.Format(@"LastNews: {0} add fail. Please try again.", model.ContentID);
            }
            else if ("edit".Equals(command))
            {
                if (gActivitiesManagement.Update(model))
                    TempData["LastNewsMessage"] = string.Format(@"LastNews: {0} Edit successfully.", model.ContentID);
                else
                    TempData["LastNewsMessage"] = string.Format(@"LastNews: {0} Edit fail. Please try again.", model.ContentID);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(string contentId)
        {
            Boolean result = gActivitiesManagement.Delete(contentId);
            return RedirectToAction("List");
        }
    }
}