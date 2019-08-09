using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.DAL;
using TzuChiClassLibrary.BO;

namespace TzuChiFrontend.Controllers
{
    public class InfoInquiryController : Controller
    {
        ISchoolDiaryManagement gSchoolDiaryManagement = new SchoolDiaryManagementMock();
        
        //資訊查詢
        // GET: InfoInquiry
        public ActionResult Index()
        {
            return View();
        }

        // Go to 校園日誌
        public ActionResult SchoolDiary() { return RedirectToAction("GetSchoolDiaryList", "SchoolDiary"); }
        // Go to 大愛新聞
        public ActionResult News() { return RedirectToAction("GetNewsList", "News"); }
        
        public ActionResult inform_2()
        {
            return View();
        }

        public ActionResult inform_2_pic()
        {
            return View();
        }

        public ActionResult inform_3()
        {
            return View();
        }

        public ActionResult inform_3_book()
        {
            return View();
        }
    }
}