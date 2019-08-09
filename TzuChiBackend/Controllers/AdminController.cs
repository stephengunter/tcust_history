using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
		
		//管理員
		IAdminManagement gAdminManagement = new AdminManagementImpl();

        // GET: Backend/Admin
        public ActionResult List()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted || string.IsNullOrEmpty(Request["ca"]))
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }
            //頁數
            var models = gAdminManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalAdmin = (models.Count > 0) ? models.First().TotalNum : 0;
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
        public ActionResult Page(string AdminID)
        {
            AdminModel result = null;
            if (string.IsNullOrEmpty(AdminID))
            {
                ViewBag.Commamd = "add";
            }
            else
            {
                ViewBag.Commamd = "edit";
                result = gAdminManagement.GetByAdminID(AdminID);
                result.Password = gAdminManagement.DesDecrypt(result.Password); //將密碼解密
            }

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Page(AdminModel model, string command)
        {
            model.Password = gAdminManagement.DesEncrypt(model.Password);//將密碼加密
            if ("add".Equals(command))
            {
                if (gAdminManagement.Add(model))
                    TempData["AdminMessage"] = string.Format(@"Admin: {0} add successfully.", model.AdminID);
                else
                    TempData["AdminMessage"] = string.Format(@"Admin: {0} add fail. Please try again.", model.AdminID);
            }
            else if ("edit".Equals(command))
            {
                if (gAdminManagement.Update(model))
                    TempData["AdminMessage"] = string.Format(@"Admin: {0} Edit successfully.", model.AdminID);
                else
                    TempData["AdminMessage"] = string.Format(@"Admin: {0} Edit fail. Please try again.", model.AdminID);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(string AdminID)
        {
            Boolean result = gAdminManagement.Delete(AdminID);
            return RedirectToAction("List");
        }

        
    }
}