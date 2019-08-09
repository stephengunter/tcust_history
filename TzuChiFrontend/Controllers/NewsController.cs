using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;
using TzuChiClassLibrary.Utils;

namespace TzuChiFrontend.Controllers
{
    public class NewsController : Controller
    {
        INewsManagement gNewsManagement = new NewsManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();



        //public ActionResult GetNewsPartial(string contentID)
        //{
        //    //單筆新聞 ViewBag.NewsModel            
        //    if (string.IsNullOrEmpty(contentID))
        //    {
        //        contentID = gNewsManagement.GetLatestNews().ContentID;    //抓到最新一筆且在展期的新聞ContentID
        //    }
        //    NewsModel newsModel = gNewsManagement.GetByContentID(contentID);
        //    ViewBag.NewsModel = newsModel;

        //    return PartialView();
        //}


        // GET: News
        public ActionResult GetNews(string contentID)
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || string.IsNullOrEmpty(contentID))
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation; //Session clear 
            }

            List<CategoryModel> yearList = gCategoryManagement.NewsGetYearCategory();
            ViewBag.SelectYearList = yearList;

            //頁數
            var models = gNewsManagement.GetAllForFrontPage(pagenation);

            NewsModel newsModel = null;

            //單筆新聞 ViewBag.NewsModel            
            if (string.IsNullOrEmpty(contentID))
            {
                newsModel = models[0];
                //contentID = gNewsManagement.GetLatestNews().ContentID;    //抓到最新一筆且在展期的新聞ContentID
            }
            else {
                newsModel = gNewsManagement.GetByContentID(contentID);
            }
            
            ViewBag.NewsModel = newsModel;

            //轉換 link -> QRCode
            if (!string.IsNullOrEmpty(newsModel.RelatedLink))
            {
                ViewBag.NewsQrCode = QrCodeHelper.GetQRCodeBase64String(newsModel.RelatedLink);
            }


            ViewBag.DataModel = models;
            ViewBag.TotalNews = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            return View(pagenation);
        }



        [HttpPost]
        public JsonResult AjaxGetNews()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];
            
            pagenation.Page += 1;

            //更新SESSION
            Session["Pagenation"] = pagenation;

            //頁數
            var models = gNewsManagement.GetAllForFrontPage(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            if (models.Count == 0)
            {
                var failResult = new Dictionary<string, object>()
                {
                    {"status", "Fail"},
                    {"msg", "modelisEmpty"}
                };

                return Json(failResult, JsonRequestBehavior.AllowGet);
            }

            return Json(models, JsonRequestBehavior.AllowGet);
        }




        public ActionResult ScrollNews()
        {
            
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            try
            {
                pagenation.Page = int.Parse(Request.QueryString["page"]);
            }
            catch (Exception ex)
            {
                return View();
            }

            ////頁數
            var models = gNewsManagement.GetAllForFrontPage(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            return View();
        }

        public ActionResult SearchNews(string yearSearch, string monthSearch, string keywordSearch)
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null)
            {
                pagenation = new PagenationModel();
                
            }
            else {
                pagenation.Month = monthSearch;
                pagenation.Year = yearSearch;
                pagenation.KeyWord = keywordSearch;
                pagenation.Page = 1;
            }

            Session["Pagenation"] = pagenation;

            List<CategoryModel> yearList = gCategoryManagement.NewsGetYearCategory();
            ViewBag.SelectYearList = yearList;

            ////頁數
            var models = gNewsManagement.GetAllForFrontPage(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            return View(pagenation);
        }

        public ActionResult ScrollSearchNews()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            try
            {
                pagenation.Page = int.Parse(Request.QueryString["page"]);
            }
            catch (Exception ex)
            {
                return View();
            }

            ////頁數
            var models = gNewsManagement.GetAllForFrontPage(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            return View();
        }

        public JsonResult AjaxGetNewsByContentID(string contentID)
        {
            //1.DAL(SchoolDiaryManagementImpl)	1.GetAllForFrontPage

            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];
            NewsModel result = null;
            //SchoolDiaryModel result = null;
            result = gNewsManagement.GetByContentID(contentID);
            ViewBag.DataModel = result;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}