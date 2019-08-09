using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.DAL;
using TzuChiClassLibrary.BO;

namespace TzuChiFrontend.Controllers
{
    public class MapController : Controller
    {
        IPlanOutsideManagement gPlanOutsideManagement = new PlanOutsideManagementImpl();
        IPlanInsideManagement gPlanInsideManagement = new PlanInsideManagementImpl();
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();
        
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlanOutside
        public ActionResult PlanOutside(string categoryOutsideID, string categoryCountryID, string year)
        {
            Dictionary<string, string> countryList = gPlanOutsideManagement.GetCountryList(categoryOutsideID);
            ViewBag.SearchList = countryList;
            List<string> yearList = gPlanOutsideManagement.GetYearList(categoryOutsideID);
            ViewBag.YearList = yearList;
            string categoryName = gPlanOutsideManagement.GetAllCategoryID();
            ViewBag.CategoryName = categoryName;

            switch(categoryOutsideID)
            {
                case PlanOutsideModel.CATEGORYQUTSIDEID_SISTER :
                    ViewBag.PartialPath = "PlanOutsidePartial/_SisterPartial";
                    break;
                case PlanOutsideModel.CATEGORYQUTSIDEID_DEGREE :
                    ViewBag.PartialPath = "PlanOutsidePartial/_DegreePartial";
                    break;
                case PlanOutsideModel.CATEGORYQUTSIDEID_OVERSEA:
                    ViewBag.PartialPath = "PlanOutsidePartial/_OverseaPartial";
                    break;
                case PlanOutsideModel.CATEGORYQUTSIDEID_INDUSTRYCOOPERATION:
                    ViewBag.PartialPath = "PlanOutsidePartial/_IndustrycooperationPartial";
                    break;
                case PlanOutsideModel.CATEGORYQUTSIDEID_INTERNSHIP:
                    ViewBag.PartialPath = "PlanOutsidePartial/_InternshipPartial";
                    break;
            }

            ViewBag.Data = gPlanOutsideManagement.GetForFrontPage(categoryOutsideID, categoryCountryID, year);

            return View();
        }

        // GET: PlanInside
        public ActionResult PlanInside(string categoryInsideID, string categoryPartitionID, string categorySiteID, string department)
        {   
            List<string> departList = gPlanInsideManagement.GetDepartList(categoryInsideID);
            ViewBag.DepartList = departList;
            List<CategoryModel> partitionList = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_PARTITION);
            ViewBag.PartitionList = partitionList;
            List<CategoryModel> siteList = gCategoryManagement.GetByCategoryTypeID(CategoryModel.CATEGORY_CATEGORYTYPEID_SITE);
            ViewBag.SiteList = siteList;

            switch (categoryInsideID)
            {
                case PlanInsideModel.CATEGORYQUTSIDEID_INDUSTRYCOOPERATION:
                    ViewBag.PartialPath = "PlanInsidePartial/_IndustryCooperationPartial";
                    break;
                case PlanInsideModel.CATEGORYQUTSIDEID_INTERNSHIP:
                    ViewBag.PartialPath = "PlanInsidePartial/_InternshipPartial";
                    break;
                case PlanInsideModel.CATEGORYQUTSIDEID_UNION:
                    ViewBag.PartialPath = "PlanInsidePartial/_UnionPartial";
                    break;
                default:
                    ViewBag.PartialPath = "PlanInsidePartial/_IndexPartial";
                    break;
            }
            ViewBag.CategoryInsideID = categoryInsideID;
            ViewBag.Data = gPlanInsideManagement.GetForFrontPage(categoryInsideID, categoryPartitionID, categorySiteID, department);
            return View();
        }

        // GET: DepartIntro 系所介紹首頁
        public ActionResult DepartIntro()
        {
            return View();
        }

        // GET: DepartPage 各系所介紹頁
        public ActionResult DepartPage(string path)
        {
            ViewBag.PartialPath = path;
            return View();
        }

    }
}