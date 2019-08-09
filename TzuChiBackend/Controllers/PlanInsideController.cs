using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TzuChiBackend.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;
using PagedList;

using TzuChiBackend.ViewModels;
using TzuChiBackend.Services;
using TzuChiBackend.Context;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class PlanInsideController : BaseController
    {
        private CategoryService categoryService;
        private PlanInsideService planInsideService;
        private FileUplaodService fileUplaodService;

		public PlanInsideController()
        {
            this.categoryService = new CategoryService(Context);
            this.planInsideService = new PlanInsideService(Context);
            this.fileUplaodService = new FileUplaodService(Context);
        }

        //天涯比鄰 -> 境內
        IPlanInsideManagement gPlanInsideManagement = new PlanInsideManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();
        ICoordinateManagement gCoordinateManagement = new CoordinateManagementImpl();

		

		#region  Index
		[HttpGet]
        public ActionResult Index(string type = "", string area = "", string keyword = "", string page = "",string order="", string desc="")
        {

            try
            {
                var model = new PlanInsideSearchForm()
                {
                    TypeId = type,
                    AreaId = area,
                    KeyWord = keyword,
                    PageNumber = page.ToInt(),
                    PageSize = 10,

                    OrderOptionId= order.ToInt(),
                    OrderByDescending= desc.ToBoolean()
                };

                LoadOptions(model);

                SearchPlans(model);

                return View(model);


            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                
                return View("Error");

            }

        }
        [HttpPost]
        public ActionResult Index(PlanInsideSearchForm model)
        {

            try
            {
                LoadOptions(model);

                SearchPlans(model);

                return View(model);


            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
              
                return View("Error");

            }

        }
        void LoadOptions(PlanInsideSearchForm model)
        {
            if (model.PageNumber < 1) model.PageNumber = 1;
            if (model.PageSize < 1) model.PageSize = 10;

            bool withCooperation = true;
            model.TypeOptions = this.planInsideService.TypeSelectList(withCooperation);
            model.AreaOptions = this.categoryService.GetAreas().ToSelectListItems(model.AreaId, true);

            if (String.IsNullOrEmpty(model.TypeId))
            {
                model.TypeId = model.TypeOptions.First().Value;
            }

            model.TypeName = this.planInsideService.GetTypeName(model.TypeId);
        }
        void SearchPlans(PlanInsideSearchForm model)
        {
            IEnumerable<PlanInside> planList = this.planInsideService.Search(model.TypeId, model.AreaId, model.KeyWord);


            if (planList.IsNullOrEmpty())
            {
                model.DataCount = 0;
                return;
            }

            planList = planList.OrderByDescending(p => p.Content.ContentUpdateTime);

            if (model.OrderOptionId == 1)  //科系
            {
                if (model.OrderByDescending) planList = planList.OrderByDescending(p => p.Department);
                else planList = planList.OrderBy(p => p.Department);
            }
            else if (model.OrderOptionId == 2)  //城市/所在地
            {
                if (model.OrderByDescending) planList = planList.OrderByDescending(p => p.CategorySiteID);
                else planList = planList.OrderBy(p => p.CategorySiteID);
            }
            else if (model.OrderOptionId == 3)  //企業名稱
            {
                if (model.OrderByDescending) planList = planList.OrderByDescending(p => p.Agencies);
                else planList = planList.OrderBy(p => p.Agencies);
            }


           


            int page = model.PageNumber;
            model.DataCount = planList.Count();
            model.PagedRecordResults = planList.ToList().ToPagedList(page, model.PageSize);


            var viewResultList = new List<PlanInsideViewModel>();
            foreach (var item in model.PagedRecordResults)
            {
                viewResultList.Add(GetPlanInsideViewModel(item));
            }

           
            model.PagedViewResults = viewResultList;

        }

        PlanInsideViewModel GetPlanInsideViewModel(PlanInside plan)
        {
            var model = new PlanInsideViewModel()
            {
                Id = plan.ContentID,
                AreaName = this.categoryService.GetById(plan.CategoryPartitionID).CategoryName,
                CityName = this.categoryService.GetById(plan.CategorySiteID).CategoryName,
                TypeName = this.categoryService.GetById(plan.CategoryPrepID).CategoryName,
                CompanyName = plan.Agencies,
                DepartmentName = plan.Department

            };

            var files = this.fileUplaodService.GetByContentId(plan.ContentID);

            if (files.IsNullOrEmpty())
            {
                model.PicUrlList = null;
                model.AttachPicUrl = null;
            }
            else
            {
                model.PicUrlList = files.Where(f=>f.CoverImage).ToList();
                model.AttachPicUrl = files.Where(f => f.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_ATTACHMENTIMAGE)).SingleOrDefault();
            }


            return model;
        }

        #endregion


        // GET: Backend/PlanInside
        public ActionResult List()
        {

            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            var typeList = this.planInsideService.TypeSelectList();
            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted)
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;

                pagenation.Type = typeList.First().Value;
            }
            List<CategoryModel> lstSelectData = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_PLANINSIDE_SEARCH);
            ViewBag.SearchList = new SelectList(lstSelectData, "CategoryID", "CategoryName", string.Empty).ToList();
            //頁數
            var models = gPlanInsideManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalPlanInside = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;

            ViewBag.TypeSelectList = this.planInsideService.TypeSelectList();

            return View(pagenation);
        }

        [HttpPost]
        public ActionResult List(PagenationModel pagenation, int totalPage)
        {
            pagenation.IsPosted = true;
            pagenation.Page = pagenation.Page > totalPage ? totalPage : pagenation.Page <= 0 ? 1 : pagenation.Page;
            Session["Pagenation"] = pagenation;
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Page(string contentID, string categoryInsideID)
        {

            PlanInsideModel result = null;
            if (string.IsNullOrEmpty(contentID))
            {
                ViewBag.Commamd = "add";
            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gPlanInsideManagement.GetByContentID(contentID);
                categoryInsideID = result.CategoryPrepID;
            }

            List<CategoryModel> listType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_INSIDE);
            ViewBag.SelectTypeList = new SelectList(listType, "CategoryID", "CategoryName", string.Empty).ToList();
            List<CategoryModel> yearType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_YEAR);
            ViewBag.SelectYearList = new SelectList(yearType, "CategoryID", "CategoryName", string.Empty).ToList();
            List<CategoryModel> partitionType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_PARTITION);
            ViewBag.SelectPartitionList = new SelectList(partitionType, "CategoryID", "CategoryName", string.Empty).ToList();
            List<CategoryModel> siteType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_SITE);
            ViewBag.SelectSiteList = new SelectList(siteType, "CategoryID", "CategoryName", string.Empty).ToList();
            List<CoordinateModel> coordinateType = gCoordinateManagement.GetAll(PlanInsideModel.TYPEID);
            ViewBag.SelectCoordinateList = new SelectList(coordinateType, "PointID", "ContentName", string.Empty).ToList();

            var departments = this.categoryService.GetDepartments().ToList();
            ViewBag.SelectDepartmentsList = new SelectList(departments, "CategoryName", "CategoryName", string.Empty).ToList();


            ViewBag.CategoryPrepID = categoryInsideID;

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(PlanInsideModel model, string command)
        {
            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            model.ContentCreator = Deviceidentity.UserID;
            model.ContentUpdater = Deviceidentity.UserID;

           

            if ("add".Equals(command))
            {
                if (gPlanInsideManagement.Add(model))
                    TempData["PlanInsideMessage"] = string.Format(@"PlanInside: {0} add successfully.", model.ContentID);
                else
                    TempData["PlanInsideMessage"] = string.Format(@"PlanInside: {0} add fail. Please try again.", model.ContentID);
            }
            else if ("edit".Equals(command))
            {
                if (gPlanInsideManagement.Update(model))
                    TempData["PlanInsideMessage"] = string.Format(@"PlanInside: {0} Edit successfully.", model.ContentID);
                else
                    TempData["PlanInsideMessage"] = string.Format(@"PlanInside: {0} Edit fail. Please try again.", model.ContentID);
            }

            return RedirectToAction("Index",new { type= model.CategoryPrepID });
        }

        [HttpPost]
        public ActionResult Delete(string contentId)
        {
            var jsonResult = new Dictionary<string, object>()
                {
                    {"status", "fail"},
                    {"msg", "刪除失敗"}
                };


            Boolean result = gPlanInsideManagement.Delete(contentId);

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
    }
}