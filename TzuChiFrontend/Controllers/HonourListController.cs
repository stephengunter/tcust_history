using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;

namespace TzuChiFrontend.Controllers
{
    public class HonourListController : Controller
    {
        //榮譽榜
        IImageShowManagement gImageShowManagement = new ImageShowManagementImpl();
        IHonourListManagement gHonourListManagement = new HonourListManagementImpl();

		// GET: HonourList
		public ActionResult Index()
		{
			//1.DAL(ImageShowManagementImpl)	1.GetByTypeID()

			//輪播圖檔
			List<ImageShowModel> result = gImageShowManagement.GetByTypeID(HonourListModel.TYPEID);

			return View(result);

		}
		public ActionResult Test()
		{
			return View();
		}
		public ActionResult List()
		{
			return View();
		}
		public ActionResult details()
		{
			return View();
		}

		[HttpPost]
        public ActionResult SearchByKeyWord(string textfield, Int32 nextPage)
        {
            // //1.DAL(HonourListManagementImpl)	1.SearchByKeyWord
            //PagenationModel pagenation = (PagenationModel)Session["PagenationModel"];

            ////第一次進入頁面時
            //if (pagenation == null || !pagenation.IsPosted )
            //{
            //    pagenation = new PagenationModel();
            //    Session["PagenationModel"] = pagenation;
            //}
            PagenationModel pagenation = new PagenationModel();

            pagenation.KeyWord = textfield;
            pagenation.Page = nextPage;

            List<HonourListModel> result = gHonourListManagement.SearchByKeyWord(pagenation);
           
            
            if (result.Count > 0)
            {
                int totalCount = result[0].TotalNum ;
                int tempPage = totalCount / pagenation.CntPerPage;
                ViewBag.TotalPage = totalCount % pagenation.CntPerPage == 0 ? tempPage : tempPage + 1;
                ViewBag.TotalCount = totalCount;
                ViewBag.CntPerPage = pagenation.CntPerPage;
            }
            else
            {
                ViewBag.TotalPage = 0;
                ViewBag.TotalCount = 0;
                ViewBag.EmptyResult = "查無結果 關鍵字 : ";
                ViewBag.CntPerPage = pagenation.CntPerPage;

            }
               

            return View(result);
        }


        public ActionResult NextDataAjax(string textfield, Int32 nextPage)
        {

            PagenationModel pagenation = new PagenationModel();

            pagenation.KeyWord = textfield;
            pagenation.Page = nextPage;

            List<HonourListModel> result = gHonourListManagement.SearchByKeyWord(pagenation);


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        


        public ActionResult GetForFrontPage()
        {
            //1.DAL(HonourListManagementImpl)            
            //1.GetForFrontPage
            List<HonourListModel> result = gHonourListManagement.GetForFrontPage();
            List<List<HonourListModel>> everyYear = new List<List<HonourListModel>>();
            int startYear = result[result.Count - 1].ContentTime.Year;
            int endYear = result[0].ContentTime.Year;
            for (var i = startYear; i >= endYear; i--)
            {
                List<HonourListModel>  newList = result.Where(model => model.ContentTime.Year.Equals(i)).ToList();
                everyYear.Add(newList);
            }
            
            return View(everyYear);
        }


        public ActionResult GetByYear(string year, string contentID)
        {
            //1.DAL(HonourListManagementImpl)
            //2.GetByYear

            List<HonourListModel> result = gHonourListManagement.GetByYear(year);
            ViewBag.contentID = contentID;

            return View(result);
        }

        public ActionResult GetByContentID(string contentID)
        {
            //1.DAL(HonourListManagementImpl)
            //3.沿用後台:GetByContentID
            HonourListModel model = gHonourListManagement.GetByContentID(contentID);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}