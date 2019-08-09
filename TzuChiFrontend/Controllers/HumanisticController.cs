using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TzuChiClassLibrary.Utils;

namespace TzuChiFrontend.Controllers
{
    public class HumanisticController : Controller
    {
        //人文博雅
        // GET: Humanistic
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookIntro()
        {
            return View();
        }

        public ActionResult BookPage(String path, String videoKey, String imgKey)
        {
            if (videoKey == null || imgKey == null)//FolderKey contains Video key and Img key
            {
                ViewBag.Video = null;
            }
            else
            {
                String videoFilePath = System.AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings[videoKey];
                String imgFilePath = System.AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings[imgKey];
                var temp = VideoProcessing.GetVideoList(imgFilePath, videoFilePath);
                ViewBag.Video = VideoProcessing.GetVideoList(imgFilePath, videoFilePath);
            }
            ViewBag.PartialPath = path;
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetByContentId()
        {
            // 1.DAL(ActivitiesManagementImpl) 1.GetByContentId
            

            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            // 2.DAL(IEducationCenterManagement) 2.GetAll

            return View();
        }
    }
}