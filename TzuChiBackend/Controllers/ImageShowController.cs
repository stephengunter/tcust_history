using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TzuChiBackend.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class ImageShowController : Controller
    {
        // GET: ImageShow
        IImageShowManagement gImageShowManagement = new ImageShowManagementImpl();

        // GET: Backend/ImageShow
        public ActionResult List()
        {
            PagenationModel pagenation = (PagenationModel)Session["Pagenation"];

            //第一次進入頁面時
            if (pagenation == null || !pagenation.IsPosted)
            {
                pagenation = new PagenationModel();
                Session["Pagenation"] = pagenation;
            }
            //輪播類別
            SelectListItem hlItem = new SelectListItem() { Value = HonourListModel.TYPEID, Text = "榮譽榜" };
            SelectListItem ecItem = new SelectListItem() { Value = EventCalendarModel.TYPEID, Text = "大事記要" };
            SelectListItem slItem = new SelectListItem() { Value = ImageShowModel.SERVICELEARN_TYPEID, Text = "慈濟人文學程 -> 服務學習" };
            List<SelectListItem> typeList = new List<SelectListItem>();
            typeList.Add(hlItem);
            typeList.Add(ecItem);
            typeList.Add(slItem);
            ViewBag.TypeList = typeList;
            //頁數
            var models = gImageShowManagement.GetAll(pagenation);
            ViewBag.DataModel = models;
            ViewBag.TotalImageShow = (models.Count > 0) ? models.First().TotalNum : 0;
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

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageShowModel model)
        {
            var Deviceidentity = User.ToCustomPrincipal().CustomIdentity;
            model.Creator = Deviceidentity.UserID;
            model.Updater = Deviceidentity.UserID;

            if (gImageShowManagement.Add(model))
                TempData["ImageShowMessage"] = string.Format(@"ImageShow: {0} add successfully.", model.ContentID);
            else
                TempData["ImageShowMessage"] = string.Format(@"ImageShow: {0} add fail. Please try again.", model.ContentID);

            return RedirectToAction("List");
            
        }

        [HttpPost]
        public ActionResult Delete(string imageShowID)
        {
            Boolean result = gImageShowManagement.Delete(imageShowID);
            return RedirectToAction("List");
        }
    }
}