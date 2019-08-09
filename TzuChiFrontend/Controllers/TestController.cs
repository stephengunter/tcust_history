using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.Utils;
using TzuChiBackend.Services;
using TzuChiFrontend.Models;
using Core.Extensions;

namespace TzuChiFrontend.Controllers
{
    public class TestController : TzuChiBackend.Controllers.BaseController 
    {
        private VideoService videoService;       
        public TestController()
        {
            this.videoService = new VideoService(Context);

        }

        //上人與慈濟世界
        // GET: TeachPhilosophy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int category=1,int id=1)
        {
            var model = new EBookSearchModel()
            {
                Category=category,
                Menu=id
            };
           
            if (category == 2)  // 八法
            {
                return View("World", model);
            }
            else  ////教育 
            {
                if (id == 4)
                {
                   
                    GetVideoCategories(model);

                    GetVideos(model);


                }
                return View("Idea", model);
            }
        }

       
        public ActionResult GetVideos(string category)
        {
            if (String.IsNullOrEmpty(category)) return HttpNotFound();

            var model = new EBookSearchModel()
            {
                VideoCategoryId = category,
                Ajax=true                
            };

            GetVideos(model);

            return PartialView("_Video",model);
        }

        void GetVideoCategories(EBookSearchModel model)
        {
            var categoryList = new List<EBookCategory>();

            var videoCatgories = this.videoService.HasVideoCategories();           
            if (!videoCatgories.IsNullOrEmpty())
            {
                foreach (var item in videoCatgories)
                {
                    categoryList.Add(new EBookCategory { Id = item.CategoryID, Text = item.CategoryName });
                }

            }

            model.CategoryList = categoryList;

        }

        void GetVideos(EBookSearchModel model)
        {
           
            if (String.IsNullOrEmpty(model.VideoCategoryId))
            {
                if (!model.CategoryList.IsNullOrEmpty()) model.VideoCategoryId = model.CategoryList.FirstOrDefault().Id;

            }

            if (String.IsNullOrEmpty(model.VideoCategoryId)) return;

            var eBookFileList = new List<EBookFile>();
           
            var videoList = this.videoService.GetByCategory(model.VideoCategoryId);
            if (!videoList.IsNullOrEmpty())
            {

                foreach (var item in videoList)
                {
                    eBookFileList.Add(new EBookFile { Id = item.ContentID, Title = item.ContentName });
                }
            }

            model.EBookFileList = eBookFileList;
        }

        
    }
}