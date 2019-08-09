using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;

using TzuChiBackend.Services;
using TzuChiBackend.ViewModels;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        //類別管理
        ICategoryManagement gCategoryManagement = new CategoryManagementImpl();
        private CategoryService categoryService;
        private PlanInsideService planInsideService;

        public CategoryController()
        {
            this.categoryService = new CategoryService(Context);
            this.planInsideService = new PlanInsideService(Context);
        }


        // GET: Backend/Category
        public ActionResult Index(string categoryTypeID)
        {
           
            var categories = this.categoryService.GetByTypeId(categoryTypeID).ToList();
          
           
            var model = new CategoryViewModel();
            model.Categories = categories;
            model.CategoryTypeID = categoryTypeID;

            return View(model);
        }

        public ActionResult List(string categoryTypeID)
        {
            var categories = this.categoryService.GetByTypeId(categoryTypeID).ToList();

            var model = new CategoryViewModel();
            model.Categories = categories;
            model.CategoryTypeID = categoryTypeID;

            return PartialView("_List", model);
        }

        // POST: Category/Create
        [HttpPost]
        public JsonResult Create(string categoryTypeID, string categoryName, Int16 sort)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "Fail"},
                        {"msg", "建立失敗"}
                    };

            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;

            try
            {
                //Creator，Updater 為當前登入者
                CategoryModel cate = new CategoryModel();
                var now = DateTime.Now;
                cate.CategoryID = Guid.NewGuid().ToString();
                cate.CategoryTypeID = categoryTypeID;
                cate.CategoryName = categoryName;
                cate.Sort = sort;
                cate.Updater = Deviceidentity.UserID;//identity.Name
                cate.Creator = Deviceidentity.UserID;//identity.Name
                bool rs = gCategoryManagement.Add(cate);
                if (rs)
                {
                    jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "OK"},
                        {"data", cate}
                    };
                }
            }
            catch (Exception e)
            {
                jsonResult = new Dictionary<string, object>()
                {
                    {"status", "Fail"},
                    {"msg", e.Message}
                };
            }

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        // POST: Category/Delete
        [HttpPost]
        public JsonResult Delete(string categoryID)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "fail"},
                        {"msg", "unknow"}
                    };

            try
            {
                bool rs = gCategoryManagement.Delete(categoryID);
                if (rs)
                {
                    jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "OK"}
                    };
                }
            }
            catch (Exception e)
            {
                Dictionary<string, string> model = new Dictionary<string, string>()
                {
                    {"status", "Fail"},
                    {"msg", e.Message}
                };
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateName()
        {
            string id= Request["id"];
            string name = Request["name"];
            Dictionary<string, object> jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "fail"},
                        {"msg", "名稱修改失敗"}
                    };
            try
            {
                var category = this.categoryService.GetById(id);
                string oldName = category.CategoryName;

                this.categoryService.UpdateName(id, name);

                this.planInsideService.UpdateDepartmentName(oldName, name);
                jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "success"},
                        {"msg", "順序修改成功"}
                    };
            }
            catch (Exception e)
            {
                jsonResult = new Dictionary<string, object>()
                {
                    {"status", "fail"},
                    {"msg", e.Message}
                };
            }

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        // POST: Category/UpdateOrder
        [HttpPost]
        public JsonResult UpdateOrder(List<CategoryModel> CateList)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "fail"},
                        {"msg", "順序修改失敗"}
                    };
            try
            {
                bool rs = gCategoryManagement.UpdateOrder(CateList);
                if (rs)
                {
                    jsonResult = new Dictionary<string, object>()
                    {
                        {"status", "success"},
                        {"msg", "順序修改成功"}
                    };
                }
            }
            catch (Exception e)
            {
                jsonResult = new Dictionary<string, object>()
                {
                    {"status", "fail"},
                    {"msg", e.Message}
                };
            }

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}