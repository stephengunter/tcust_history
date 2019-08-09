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
    public class SchoolDiaryController : Controller
    {
        ISchoolDiaryManagement gSchoolDiaryManagement = new SchoolDiaryManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();

        public ActionResult Index()
        {
          
            return View();
        }

		public ActionResult List()
		{
			return View();
		}

		// GET: SchoolDiary
		public ActionResult GetSchoolDiaryList()
        {
			return View("List");
			////1.DAL(SchoolDiaryManagementImpl)	1.GetAllForFrontPage

			////PagenationModel pagenation = (PagenationModel)Session["Pagenation"];
			//PagenationModel pagenation = null;

   //         //第一次進入頁面時
   //         if (pagenation == null)
   //         {
   //             pagenation = new PagenationModel();
   //             Session["Pagenation"] = pagenation;
   //         }

   //         //頁數
   //         var models = gSchoolDiaryManagement.GetAllForFrontPage(pagenation);

   //         ViewBag.DataModel = models;
   //         ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
   //         ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

   //         List<CategoryModel> yearList = gCategoryManagement.SchoolDiaryGetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_YEAR);
   //         //ViewBag.SelectYearList = new SelectList(yearList, "CategoryID", "CategoryName").ToList();
   //         ViewBag.SelectYearList = yearList;

   //         List<CategoryModel> termList = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_TERM);
   //         //ViewBag.SelectTermList = new SelectList(termList, "CategoryID", "CategoryName").ToList();
   //         ViewBag.SelectTermList = termList;

   //         return View(pagenation);
        }

        [HttpPost]
        public JsonResult AjaxGetSchoolDiaryList()
        {
            //1.DAL(SchoolDiaryManagementImpl)	1.GetAllForFrontPage

            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //搜尋條件 在search 已經SESSION
            pagenation.Page += 1;

            //更新SESSION
            Session["Pagenation"] = pagenation;

            //頁數
            var models = gSchoolDiaryManagement.GetAllForFrontPage(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            if (models.Count == 0) {
                var failResult = new Dictionary<string, object>()
                {
                    {"status", "Fail"},
                    {"msg", "modelisEmpty"}
                };

                return Json(failResult, JsonRequestBehavior.AllowGet);
            }

            return Json(models, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSchoolDiaryByContentID(string Id)
        {
           
            SchoolDiaryModel result = null;

            if (string.IsNullOrEmpty(Id))
            {
                return View();
            }

            result = gSchoolDiaryManagement.GetByContentID(Id);

            List<CategoryModel> yearList = gCategoryManagement.SchoolDiaryGetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_YEAR);
            ViewBag.SelectYearList = yearList;
            List<CategoryModel> termList = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_TERM);
            ViewBag.SelectTermList = termList;
            ViewBag.DataModel = result;

            //轉換 link -> QRCode
            //if (!string.IsNullOrEmpty(result.RelatedLink))
            //{
            //    ViewBag.SchoolDiaryQrCode = QrCodeHelper.GetQRCodeBase64String(result.RelatedLink);
            //}

            return View();
        }

        public ActionResult SearchSchoolDiary(string yearSearch, string semSearch, string keywordSearch)
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            ////第一次進入頁面時
            if (pagenation == null)
            {
                pagenation = new PagenationModel();
                pagenation.CntPerPage = 200000000;
            }
            else {
                pagenation.Year = yearSearch;
                pagenation.Term = semSearch;
                pagenation.KeyWord = keywordSearch;
                pagenation.Page = 1;
                pagenation.CntPerPage = 200000000;
            }

            Session["Pagenation"] = pagenation;

            

            //頁數
            var models = gSchoolDiaryManagement.GetAllForFrontPage(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            List<CategoryModel> yearList = gCategoryManagement.SchoolDiaryGetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_YEAR);
            ViewBag.SelectYearList = yearList;
            List<CategoryModel> termList = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_TERM);
            ViewBag.SelectTermList = termList;

            return View(pagenation);
        }

        public ActionResult ScrollSearchSchoolDiary()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];
            try
            {
                pagenation.Page = int.Parse(Request.QueryString["page"]);
            }
            catch (Exception ex)
            {
                List<SchoolDiaryModel> SchoolDiarychoolDiaryModelList = new List<SchoolDiaryModel>();
                ViewBag.DataModel = SchoolDiarychoolDiaryModelList;
                return View(pagenation);
            }

            //頁數
            var models = gSchoolDiaryManagement.GetAllForFrontPage(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;

            return View(pagenation);
        }
    }
}
