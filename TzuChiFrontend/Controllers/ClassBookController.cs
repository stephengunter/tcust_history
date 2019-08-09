using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;
using TzuChiClassLibrary.Utils;


namespace TzuChiFrontend.Controllers
{
    public class ClassBookController : Controller
    {
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();
        IClassBookManagement gClassBookManagement = new ClassBookManagementImpl();

        public ActionResult GetClassBookList()
        {   
            List<string> yearList = gClassBookManagement.GetAllAcademicYearName();
            ViewBag.SelectYearList = yearList;

            List<string> clsList = gClassBookManagement.GetAllClassName();
            ViewBag.SelectClsList = clsList;

            List<string> depList = gClassBookManagement.GetAllDepartmentName();
            ViewBag.SelectDepList = depList;
            return View();
        }


        public ActionResult GetClassBookByContentID(string Id, string year, string dep, string cls)
        {
            year = HttpUtility.UrlDecode(year);
            dep = HttpUtility.UrlDecode(dep);
            cls = HttpUtility.UrlDecode(cls);
            
            //20150417 ED 發佈正式機無法正常判斷cache機制 導致每次更新資料
            //ExcelCacheProcessing.CacheProcess(ExcelCacheProcessing.ClassBook);

            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || (Id == null))
            {
                pagenation = new PagenationModel();
            }

            List<ClassBookModel> resultList = new List<ClassBookModel>();
            string page = "1";

            if (!string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(dep) && string.IsNullOrEmpty(cls)) //ByID
            {
                ClassBookModel model = gClassBookManagement.GetPageByClassBookID(Id);
                page = model.Page;
                year = model.AcademicYear;
            }
            else if (!string.IsNullOrEmpty(year) && string.IsNullOrEmpty(dep) && string.IsNullOrEmpty(cls) ) //ByYear 
            {   
                page = "1"; //
            }
            else if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(dep) && !string.IsNullOrEmpty(cls)) //ByCls
            {
                pagenation.Year = year;
                pagenation.Department = dep;
                pagenation.Class = cls;

                ClassBookModel model = gClassBookManagement.GetPageByCondition(pagenation);
                page = model.Page;
                year = model.AcademicYear;
            }
            else if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(dep) && string.IsNullOrEmpty(cls)) //ByDep
            {
                List<ClassNumberModel> clsListChk = gClassBookManagement.GetClassNumberByAcademicYear(year);
                foreach (var item in clsListChk)
                {
                    if (item.Department == dep)
                    {
                        pagenation.Class = item.ClassList[0];
                        break;
                    }
                }
                
                
                pagenation.Year = year;
                pagenation.Department = dep;

                ClassBookModel model = gClassBookManagement.GetPageByCondition(pagenation);
                
                
                page = model.Page;
                year = model.AcademicYear;
            }
            

            List<string> yearList = gClassBookManagement.GetAllAcademicYearName();
            ViewBag.SelectYearList = yearList;

            List<string> clsList = gClassBookManagement.GetAllClassName();
            ViewBag.SelectClsList = clsList;

            List<string> depList = gClassBookManagement.GetAllDepartmentName();
            ViewBag.SelectDepList = depList;

            ViewBag.FlipPage = (!string.IsNullOrEmpty(page)) ? page : "1";

            
            ViewBag.DataModel = gClassBookManagement.GetAllByAcademicYear(year);

            if (ViewBag.DataModel.Count > 0)
            {
                pagenation.IsTurnLeft = ViewBag.DataModel[0].IsTurnLeft;

                if (pagenation.IsTurnLeft == false) { 
                    ViewBag.DataModel.Reverse();
                }
            }


            List<ClassNumberModel> clsNumberList = gClassBookManagement.GetClassNumberByAcademicYear(year);
            ViewBag.ClsNumberList = clsNumberList;


            Session["Pagenation"] = pagenation;

            return View(pagenation);
        }

        public ActionResult SearchClassBook(string clssearch, string depsearch, string yearsearch, string stunumbersearch, string stunamesearch)
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null)
            {
                pagenation = new PagenationModel();
            }
            
            pagenation.Class = clssearch;
            pagenation.Department = depsearch;
            pagenation.Year = yearsearch;
            pagenation.StudentID = stunumbersearch;
            pagenation.KeyWord = stunamesearch;
            pagenation.Page = 1;

            Session["Pagenation"] = pagenation;

            var models = gClassBookManagement.GetQueryList(pagenation);
            ViewBag.DataModel = models;

            List<string> yearList = gClassBookManagement.GetAllAcademicYearName();
            ViewBag.SelectYearList = yearList;

            List<string> clsList = gClassBookManagement.GetAllClassName();
            ViewBag.SelectClsList = clsList;

            List<string> depList = gClassBookManagement.GetAllDepartmentName();
            ViewBag.SelectDepList = depList;

            return View(pagenation);
        }

        public ActionResult ScrollSearchClassBook(int page)
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null)
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }

            try
            {
                pagenation.Page = int.Parse(Request.QueryString["page"]);
            }
            catch (Exception ex)
            {
                return View(pagenation);
            }

            var models = gClassBookManagement.GetQueryList(pagenation);
            ViewBag.DataModel = models;

            return View();
        }
    }
}