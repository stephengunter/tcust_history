using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class TeachPhilosophyController : Controller
    {
        // 上人教育理念 蓋檔頁面
        // GET: TeachPhilosophy
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DownloadTeachPhilosophy(string num)
        {
            string fullPath = WebConfigurationManager.AppSettings["FrontRootPath"] + WebConfigurationManager.AppSettings["TeachPhilosophyPath"] + num + ".xml";
            string fileName = "book_world_book" + num + ".xml";
            return File(fullPath, "text/xml", fileName);
        }
    }
}