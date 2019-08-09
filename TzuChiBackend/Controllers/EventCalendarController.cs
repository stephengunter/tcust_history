using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;
using TzuChiBackend.Services;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class EventCalendarController : BaseController
    {
        //大事記要
        // GET: EventCalendar
        IEventCalendarManagement gEventCalendarManagement = new EventCalendarManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();

        private EventService eventService;
        private CategoryService categoryService;

        public EventCalendarController()
        {
            this.eventService = new EventService(Context);
            this.categoryService = new CategoryService(Context);

        }
        public ActionResult ReFormat()
        {

            this.eventService.ReFormatAll();
            return Content("done");

        }

        // GET: Backend/EventCalendar
        public ActionResult List()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted || string.IsNullOrEmpty(Request["ca"]))
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }

            List<CategoryModel> lstSelectData = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_EVENTCALENDAR_SEARCH);
            ViewBag.SearchList = new SelectList(lstSelectData, "CategoryID", "CategoryName", string.Empty).ToList();
            
            //頁數
            var models = gEventCalendarManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalEventCalendar = (models.Count > 0) ? models.First().TotalNum : 0;
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
            EventCalendarModel result = null;
            if (string.IsNullOrEmpty(contentID))
            {
                ViewBag.Commamd = "add";
            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gEventCalendarManagement.GetByContentID(contentID);
            }

            ViewBag.SelectYearList = this.categoryService.GetSchoolYears(true).ToSelectListItems();

          

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(EventCalendarModel model, string command)
        {
            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            model.ContentCreator = Deviceidentity.UserID;
            model.ContentUpdater = Deviceidentity.UserID;

            model.Description = model.ContentText;

            if ("add".Equals(command))
            {
                if (gEventCalendarManagement.Add(model))
                    TempData["EventCalendarMessage"] = string.Format(@"EventCalendar: {0} add successfully.", model.ContentID);
                else
                    TempData["EventCalendarMessage"] = string.Format(@"EventCalendar: {0} add fail. Please try again.", model.ContentID);
            }
            else if ("edit".Equals(command))
            {
                if (gEventCalendarManagement.Update(model))
                    TempData["EventCalendarMessage"] = string.Format(@"EventCalendar: {0} Edit successfully.", model.ContentID);
                else
                    TempData["EventCalendarMessage"] = string.Format(@"EventCalendar: {0} Edit fail. Please try again.", model.ContentID);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(string contentID)
        {
            Boolean result = gEventCalendarManagement.Delete(contentID);
            return RedirectToAction("List");
        }
    }
}