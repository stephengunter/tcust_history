using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;

namespace TzuChiFrontend.Controllers
{
    public class EventCalendarController : Controller
    {
        IImageShowManagement gImageShowManagement = new ImageShowManagementImpl();
        IEventCalendarManagement gEventCalendarManagement = new EventCalendarManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();
        
        //大事紀要
        // GET: EventCalendar
        /// <summary>
        /// ViewBag輪播畫面 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //1.DAL(ImageShowManagementImpl)	1.GetByTypeID()
            
            // 輪播資料
            List<ImageShowModel> result = gImageShowManagement.GetByTypeID(EventCalendarModel.TYPEID);
            
            // 大事紀要List
            List<EventCalendarModel> models = gEventCalendarManagement.GetForFrontPage();
            

            List<CategoryModel> categorys = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_YEAR);
            ViewBag.TotalYear = categorys.Count;
            int cnt = 0;
            foreach (CategoryModel category in categorys)
            {
                ViewData["Year-" + cnt] = models.Where(item => item.AcademicYear.Equals(category.CategoryName)).ToList();
                cnt++;
            }

            return View(result);
        }

        public JsonResult GetByContentID(string contentID)
        {
			//1.DAL(EventCalendarManagementImpl)	沿用後台:GetByContentId()
			bool includeAdmin = false;

			EventCalendarModel jsonResult = new EventCalendarModel();
            jsonResult = gEventCalendarManagement.GetByContentID(contentID, includeAdmin);

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TinyScrollbar()
        {
            return View();
        }


    }
}