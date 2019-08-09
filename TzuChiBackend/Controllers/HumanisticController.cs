using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class HumanisticController : Controller
    {
        // 慈濟人文 蓋檔頁面
        // GET: Humanistic
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DownloadHumanistic(string num)
        {
            string fullPath = WebConfigurationManager.AppSettings["FrontRootPath"] + WebConfigurationManager.AppSettings["HumanisticPath"] + num + ".xml";
            string fileName = "book_culture_book" + num + ".xml";
            return File(fullPath, "text/xml", fileName);
        }
    }
}