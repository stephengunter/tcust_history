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
    public class HonourListController : Controller
    {
        //榮譽榜
        IHonourListManagement gHonourListManagement = new HonourListManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();

       

        // GET: Backend/HonourList
        public ActionResult List()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted || string.IsNullOrEmpty(Request["ca"]))
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }
            List<CategoryModel> lstSelectData = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_HONOURLIST_SEARCH);
            ViewBag.SearchList = new SelectList(lstSelectData, "CategoryID", "CategoryName", string.Empty).ToList();

            //頁數
            var models = gHonourListManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalHonourList = (models.Count > 0) ? models.First().TotalNum : 0;
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
            HonourListModel result = null;
            if (string.IsNullOrEmpty(contentID))
            {
                ViewBag.Commamd = "add";
            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gHonourListManagement.GetByContentID(contentID);
            }

            List<CategoryModel> listType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_YEAR);
            ViewBag.SelectTypeList = new SelectList(listType, "CategoryID", "CategoryName").ToList();

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(HonourListModel model, string command)
        {
            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            model.ContentCreator = Deviceidentity.UserID;
            model.ContentUpdater = Deviceidentity.UserID;

            if ("add".Equals(command))
            {
                if (gHonourListManagement.Add(model))
                    TempData["HonourListMessage"] = string.Format(@"HonourList: {0} add successfully.", model.ContentID);
                else
                    TempData["HonourListMessage"] = string.Format(@"HonourList: {0} add fail. Please try again.", model.ContentID);
            }
            else if ("edit".Equals(command))
            {
                if (gHonourListManagement.Update(model))
                    TempData["HonourListMessage"] = string.Format(@"HonourList: {0} Edit successfully.", model.ContentID);
                else
                    TempData["HonourListMessage"] = string.Format(@"HonourList: {0} Edit fail. Please try again.", model.ContentID);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(string contentId)
        {
            Boolean result = gHonourListManagement.Delete(contentId);
            return RedirectToAction("List");
        }
    }
}