using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.DAL;
using TzuChiClassLibrary.Utils;

namespace TzuChiFrontend.Controllers
{
    public class FoundingController : Controller
    {
        IDirectorsManagement gDirectorsManagement = new DirectorsManagementImpl();
        //創校緣起
        // GET: Founding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllForFrontPage()
        {
            //1.DAL(NewsManagementImpl)	1.GetAllForFrontPage
            
            return View();
        }

        public ActionResult GetPartialView(String menu)
        {
            //取出彈跳視窗所需的partial
            return PartialView("_" + menu + "PartialView");
        }

        public JsonResult GetAllDirectors() 
        {
            ExcelCacheProcessing.CacheProcess(ExcelCacheProcessing.Directors);
            return Json(gDirectorsManagement.GetAll(),JsonRequestBehavior.AllowGet);
        }
        
    }
}