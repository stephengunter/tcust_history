using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class FoundingController : Controller
    {
        // 創校緣起 蓋檔頁面
        // GET: Founding
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DownloadFounding(string name)
        {
            string fullPath = WebConfigurationManager.AppSettings["FrontRootPath"] + WebConfigurationManager.AppSettings["FoundingPath"] + name + ".cshtml";
            return File(fullPath, "text/html", name + ".cshtml");
        }
    }
}