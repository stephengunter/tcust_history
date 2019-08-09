using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TzuChiClassLibrary.DAL;
using TzuChiClassLibrary.BO;
using TzuChiBackend.Security;
using TzuChiBackend.ViewModels;
using TzuChiBackend.Services;
using TzuChiBackend.Context;
using PagedList;
using TzuChiBackend.Helpers;
using System.Net.Http;
using Newtonsoft.Json;

namespace TzuChiBackend.Controllers
{
    // [Authorize]
    public class SchoolDiaryController : BaseController
    {
        ISchoolDiaryManagement gSchoolDiaryManagement = new SchoolDiaryManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();

        private SchoolDairyService schoolDairyService;
        private CategoryService categoryService;
        private FileUplaodService fileUplaodService;
       
        public SchoolDiaryController()
        {
			

			this.categoryService = new CategoryService(Context);
            this.schoolDairyService = new SchoolDairyService(Context);
            this.fileUplaodService = new FileUplaodService(Context);
        }

		public JsonResult test()
		{
			var posts = schoolDairyService.GetDiaryList().Take(5);
			return Json(posts,JsonRequestBehavior.AllowGet);
		}

		#region  Index
		[HttpGet]
        public ActionResult Index(string type = "", string year = "", string keyword = "", string page = "", string order = "", string desc = "")
        {
			
			//Rootobject rootobject = null;
			//using (var client = new HttpClient())
			//{
			//	client.BaseAddress = new Uri("http://localhost:50001/api/");
			//	//HTTP GET
			//	var responseTask = client.GetAsync("blog/topposts");
			//	responseTask.Wait();

			//	var result = responseTask.Result;
			//	if (result.IsSuccessStatusCode)
			//	{

			//		var readTask = result.Content.ReadAsAsync<Rootobject>();
			//		readTask.Wait();

			//		rootobject = readTask.Result;


			//	}
			//}

			var model = new SchoolDiarySearchForm()
            {
                TypeId = type,
                YearId = year,
                KeyWord = keyword,
                PageNumber = page.ToInt(),
                PageSize = 10,

                OrderOptionId = order.ToInt(),
                OrderByDescending = desc.ToBoolean()
            };

            LoadOptions(model);

            SearchPosts(model);

            return View(model);

        }
        [HttpPost]
        public ActionResult Index(SchoolDiarySearchForm model)
        {

            LoadOptions(model);

            SearchPosts(model);

            return View(model);
        }
        void LoadOptions(SchoolDiarySearchForm model)
        {
            if (model.PageNumber < 1) model.PageNumber = 1;
            if (model.PageSize < 1) model.PageSize = 10;

            model.TypeOptions = this.schoolDairyService.TypeSelectList(model.TypeId);
            model.YearOptions = this.categoryService.GetSchoolYears().ToSelectListItems(model.YearId, true);


            if (String.IsNullOrEmpty(model.TypeId))
            {
                model.TypeId = model.TypeOptions.First().Value;
            }

            model.TypeName = this.schoolDairyService.GetTypeName(model.TypeId);
        }
        void SearchPosts(SchoolDiarySearchForm model)
        {
            IEnumerable<Content> postList = this.schoolDairyService.Search(model.TypeId, model.YearId, model.KeyWord);


            if (postList.IsNullOrEmpty())
            {
                model.DataCount = 0;
                return;
            }

            postList = postList.OrderByDescending(p => p.ContentUpdateTime);

            if (model.OrderOptionId == 1)  //編號
            {
                if (model.OrderByDescending) postList = postList.OrderByDescending(p => p.SerialNo);
                else postList = postList.OrderBy(p => p.SerialNo);
            }
            else if (model.OrderOptionId == 2)  //日期
            {
                if (model.OrderByDescending) postList = postList.OrderByDescending(p => p.ContentTime);
                else postList = postList.OrderBy(p => p.ContentTime);
            }

            int page = model.PageNumber;
            model.DataCount = postList.Count();
            model.PagedRecordResults = postList.ToList().ToPagedList(page, model.PageSize);


            var viewResultList = new List<SchoolDiaryViewModel>();
            foreach (var item in model.PagedRecordResults)
            {
                viewResultList.Add(GetSchoolDiaryViewModel(item));
            }


            model.PagedViewResults = viewResultList;

        }

        SchoolDiaryViewModel GetSchoolDiaryViewModel(Content post)
        {
            var model = new SchoolDiaryViewModel()
            {
                Id = post.ContentID,
                Number = post.SerialNo,
                Author = post.Author,
                Title = post.ContentName,
                ContentText = post.ContentText,
                DairyDate = Convert.ToDateTime(post.ContentTime),

            };

            string[] yearTerm = this.schoolDairyService.GetSchoolYearTerm(post);

            model.SchoolYear = yearTerm[0];
            model.Term = yearTerm[1];

            var files = this.fileUplaodService.GetByContentId(post.ContentID);

            if (files.IsNullOrEmpty())
            {
                model.PicUrlList = null;
                model.AttachPicUrl = null;
            }
            else
            {
                model.PicUrlList = files.Where(f => f.CoverImage).ToList();
                model.AttachPicUrl = files.Where(f => f.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_ATTACHMENTIMAGE)).SingleOrDefault();
            }


            return model;
        }

        #endregion


        // GET: Backend/SchoolDiary
        public ActionResult List()
        {

            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted || string.IsNullOrEmpty(Request["ca"]))
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }

            List<CategoryModel> lstSelectData = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_SCHOOLDIARY_SEARCH);
            ViewBag.SearchList = new SelectList(lstSelectData, "CategoryID", "CategoryName", string.Empty).ToList();

            //頁數
            var models = gSchoolDiaryManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalSchoolDiary = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;
            return View(pagenation);
        }

        [HttpPost]
        public ActionResult List(PagenationModel pagenation, int totalPage)
        {
            pagenation.IsPosted = true;
            pagenation.Page = pagenation.Page > totalPage ? totalPage : pagenation.Page <= 0 ? 1 : pagenation.Page;
            Session["Pagenation"] = pagenation;
            return RedirectToAction("List", new { ca = 1 });
        }

        [HttpGet]
        public ActionResult Page(string contentID, string type = "")
        {
			

			SchoolDiaryModel result = null;
            if (string.IsNullOrEmpty(contentID))
            {
                ViewBag.Commamd = "add";

                string typeName = this.schoolDairyService.GetTypeName(type);
                if (!String.IsNullOrEmpty(typeName))
                {
                    result = new SchoolDiaryModel
                    {
                        ContentType = new List<string>() { type }
                    };
                   
                    if (typeName == "Dairy")
                    {
                        result.ContentTime = DateTime.Today;
                    }
                    else   //Honor
                    {
                        result.ContentTime = new DateTime ( 3000, 1, 1 );

                    }
                }


            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gSchoolDiaryManagement.GetByContentID(contentID);
            }

            ViewBag.SelectYearList = this.categoryService.GetSchoolYears(true).ToSelectListItems();

            List<CategoryModel> termList = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_TERM);
            ViewBag.SelectTermList = new SelectList(termList, "CategoryID", "CategoryName").ToList();




            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(SchoolDiaryModel model, string command)
        {
            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            model.ContentCreator = Deviceidentity.UserID;
            model.ContentUpdater = Deviceidentity.UserID;

            model.Description = model.ContentText;

            if ("add".Equals(command))
            {
                if (gSchoolDiaryManagement.Add(model))
                    TempData["SchoolDiaryMessage"] = string.Format(@"SchoolDiary: {0} add successfully.", model.ContentName);
                else
                    TempData["SchoolDiaryMessage"] = string.Format(@"SchoolDiary: {0} add fail. Please try again.", model.ContentName);
            }
            else if ("edit".Equals(command))
            {
                if (gSchoolDiaryManagement.Update(model))
                    TempData["SchoolDiaryMessage"] = string.Format(@"SchoolDiary: {0} Edit successfully.", model.ContentName);
                else
                    TempData["SchoolDiaryMessage"] = string.Format(@"SchoolDiary: {0} Edit fail. Please try again.", model.ContentName);
            }

            if (string.IsNullOrEmpty(model.ContentID))
            {
                throw new Exception();
            }

			



			return RedirectToAction("Index", new { type = model.ContentType.FirstOrDefault() });
        }

        [HttpPost]
        public ActionResult Delete(string contentId)
        {


            var jsonResult = new Dictionary<string, object>()
                {
                    {"status", "fail"},
                    {"msg", "刪除失敗"}
                };


            Boolean result = gSchoolDiaryManagement.Delete(contentId);

            if (result)
            {
				

                jsonResult = new Dictionary<string, object>()
                {
                    {"status", "success"},
                    {"msg", "刪除成功"}
                };
            }

            return Json(jsonResult, JsonRequestBehavior.AllowGet);



        }

        // POST: SchoolDiary/checkSerialNo
        [HttpPost]
        public JsonResult checkSerialNo(string serialNo)
        {
            Boolean result = gSchoolDiaryManagement.checkSerialNo(serialNo);

            return Json(result);
        }


		









	}
}