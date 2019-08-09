using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TzuChiBackend.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;
using PagedList;
using TzuChiBackend.Services;
using TzuChiBackend.ViewModels;
using TzuChiBackend.Context;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class PlanOutsideController : BaseController
    {
        //天涯比鄰 -> 境外
        IPlanOutsideManagement gPlanOutsideManagement = new PlanOutsideManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();
        ICoordinateManagement gCoordinateManagement = new CoordinateManagementImpl();

        private CategoryService categoryService;
        private PlanOutsideService planOutsideService;
        private FileUplaodService fileUplaodService;

        public PlanOutsideController()
        {
            this.categoryService = new CategoryService(Context);
            this.planOutsideService = new PlanOutsideService(Context);
            this.fileUplaodService = new FileUplaodService(Context);
        }

        #region  Index
        [HttpGet]
        public ActionResult Index(string type = "", string area = "", string keyword = "", string page = "", string order = "", string desc = "")
        {

            try
            {
                var model = new PlanOutsideSearchForm()
                {
                    TypeId = type,
                    AreaId = area,
                    KeyWord = keyword,
                    PageNumber = page.ToInt(),
                    PageSize = 10,

                    OrderOptionId = order.ToInt(),
                    OrderByDescending = desc.ToBoolean()
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
        public ActionResult Index(PlanOutsideSearchForm model)
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
        void LoadOptions(PlanOutsideSearchForm model)
        {
            if (model.PageNumber < 1) model.PageNumber = 1;
            if (model.PageSize < 1) model.PageSize = 10;


            model.TypeOptions = this.planOutsideService.TypeSelectList();
            model.AreaOptions = this.categoryService.GetCountries().ToSelectListItems(model.AreaId, true);

            if (String.IsNullOrEmpty(model.TypeId))
            {
                model.TypeId = model.TypeOptions.First().Value;
            }

            model.TypeName = this.planOutsideService.GetTypeName(model.TypeId);
        }
        void SearchPlans(PlanOutsideSearchForm model)
        {
            IEnumerable<PlanOutside> planList = this.planOutsideService.Search(model.TypeId, model.AreaId, model.KeyWord);


            if (planList.IsNullOrEmpty())
            {
                model.DataCount = 0;
                return;
            }

            planList = planList.OrderByDescending(p => p.Content.ContentUpdateTime);

            if (model.OrderOptionId == 1)  //校名//活動名稱
            {
                if (model.OrderByDescending) planList = planList.OrderByDescending(p => p.Content.ContentName);
                else planList = planList.OrderBy(p => p.Content.ContentName);
            }
            else if (model.OrderOptionId == 2)  //國家/地區
            {
                if (model.OrderByDescending) planList = planList.OrderByDescending(p => p.CategoryCountryID);
                else planList = planList.OrderBy(p => p.CategoryCountryID);
            }
           





            int page = model.PageNumber;
            model.DataCount = planList.Count();
            model.PagedRecordResults = planList.ToList().ToPagedList(page, model.PageSize);


            var viewResultList = new List<PlanOutsideViewModel>();
            foreach (var item in model.PagedRecordResults)
            {
                viewResultList.Add(GetPlanInsideViewModel(item));
            }


            model.PagedViewResults = viewResultList;

        }

        PlanOutsideViewModel GetPlanInsideViewModel(PlanOutside plan)
        {
            var model = new PlanOutsideViewModel()
            {
                Id = plan.ContentID,
                TypeName = this.categoryService.GetById(plan.CategoryOutsideID).CategoryName,
                AreaName = this.categoryService.GetById(plan.CategoryCountryID).CategoryName,
                CompanyName = plan.Content.ContentName,
              

            };

            var department = this.categoryService.GetById(plan.CategoryDepartmentID);
            if (department!=null) model.DepartmentName = department.CategoryName;

            var files = this.fileUplaodService.GetByContentId(plan.ContentID);

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

        // GET: Backend/PlanOutside
        public ActionResult List()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted)
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }
            List<CategoryModel> lstSelectData = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_PLANOUTSIDE_SEARCH);
            ViewBag.SearchList = new SelectList(lstSelectData, "CategoryID", "CategoryName", string.Empty).ToList();
            //頁數
            var models = gPlanOutsideManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalPlanOutside = (models.Count > 0) ? models.First().TotalNum : 0;
            ViewBag.TotalPage = (models.Count > 0) ? ((int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage) <= 0 ? 1 : (int)Math.Ceiling((double)models.First().TotalNum / pagenation.CntPerPage)) : 1;
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
        public ActionResult Page(string contentID, string categoryOutsideID)
        {
            PlanOutsideModel result = null;
            if (string.IsNullOrEmpty(contentID))
            {
                ViewBag.Commamd = "add";
            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gPlanOutsideManagement.GetByContentID(contentID);
                categoryOutsideID = result.CategoryOutsideID;
            }

            List<CategoryModel> listType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_OUTSIDE);
            ViewBag.SelectTypeList = new SelectList(listType, "CategoryID", "CategoryName", string.Empty).ToList();
            List<CategoryModel> countryType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_COUNTRY);
            ViewBag.SelectCountryList = new SelectList(countryType, "CategoryID", "CategoryName", string.Empty).ToList();
            List<CategoryModel> contractdeType = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_CONTRACTDE);
            ViewBag.SelectContractdeList = new SelectList(contractdeType, "CategoryID", "CategoryName", string.Empty).ToList();
            List<CoordinateModel> coordinateType = gCoordinateManagement.GetAll(PlanOutsideModel.TYPEID);
            ViewBag.SelectCoordinateList = new SelectList(coordinateType, "PointID", "ContentName", string.Empty).ToList();
            ViewBag.CategoryOutsideID = categoryOutsideID;

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(PlanOutsideModel model, string command)
        {
            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            model.ContentCreator = Deviceidentity.UserID;
            model.ContentUpdater = Deviceidentity.UserID;
            
            if ("add".Equals(command))
            {
                if (gPlanOutsideManagement.Add(model))
                    TempData["PlanOutsideMessage"] = string.Format(@"PlanOutside: {0} add successfully.", model.ContentID);
                else
                    TempData["PlanOutsideMessage"] = string.Format(@"PlanOutside: {0} add fail. Please try again.", model.ContentID);
            }
            else if ("edit".Equals(command))
            {
                if (gPlanOutsideManagement.Update(model))
                    TempData["PlanOutsideMessage"] = string.Format(@"PlanOutside: {0} Edit successfully.", model.ContentID);
                else
                    TempData["PlanOutsideMessage"] = string.Format(@"PlanOutside: {0} Edit fail. Please try again.", model.ContentID);
            }

            return RedirectToAction("Index", new { type = model.CategoryOutsideID });
        }

        [HttpPost]
        public ActionResult Delete(string contentID)
        {
          
            var jsonResult = new Dictionary<string, object>()
                {
                    {"status", "fail"},
                    {"msg", "刪除失敗"}
                };


            Boolean result = gPlanOutsideManagement.Delete(contentID);

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