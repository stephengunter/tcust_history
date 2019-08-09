using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.Utils;

namespace TzuChiFrontend.Controllers
{
    public class TeachPhilosophyController : Controller
    {
        //上人與慈濟世界
        // GET: TeachPhilosophy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeachPhilosophy(string partial)
        {
            ViewBag.Partial = partial;
            return View();
        }

        public ActionResult TzuChiWorld(string partial)
        {
            ViewBag.Partial = partial;
            String videoFilePath = System.AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["video.folder.TzuChiWorld.Video"];
            String imgFilePath = System.AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["video.folder.TzuChiWorld.Img"];
            return View(VideoProcessing.GetVideoList(imgFilePath, videoFilePath));
        }

        public ActionResult GetPartialView(String menu)
        {
            if(menu == "_BodhiPartialView")//菩提頁面先給予model套元素
            {
                String videoFilePath = System.AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["video.folder.TeachPhilosophy.Video"];
                String imgFilePath = System.AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["video.folder.TeachPhilosophy.Img"];
                return PartialView(menu, VideoProcessing.GetVideoList(imgFilePath, videoFilePath));
            }
            return PartialView(menu);
        }
    }
}