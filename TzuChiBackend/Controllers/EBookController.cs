using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.ViewModels;
using TzuChiBackend.Services;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class EBookController : BaseController
    {
        private readonly string frontRootPath = System.Web.Configuration.WebConfigurationManager.AppSettings["FrontRootPath"];
        private readonly string folder = System.Web.Configuration.WebConfigurationManager.AppSettings["ebook_folder"];

        private EBookService eBookService;
        public EBookController()
        {
            this.eBookService = new EBookService(Context, this.frontRootPath, this.folder);

        }

        public ActionResult Index()
        {
            

            var viewList = this.eBookService.GetAll();
            var model = new EBookSearchModel();
            model.DataCount = viewList.Count();
            model.EBookViewList = viewList;
            return View(model);
        }
        public ActionResult Create()
        {
            return View(new EBookViewModel());
        }
        public ActionResult Edit(string id)
        {
            var model = this.eBookService.Edit(id);
            if (model == null) return HttpNotFound();

            model.Ajax = Request.IsAjaxRequest();

            if (model.Ajax) return PartialView("_Pages", model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Store(EBookViewModel model)
        {
            this.eBookService.Create(model.Title.Trim());

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update(EBookViewModel model)
        {
            this.eBookService.Update(model.Id, model.Title.Trim());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Upload()
        {
            string id= Request["book_id"];
            // Checking no of files injected in Request object  
            if (Request.Files.Count == 0) throw new Exception("no file");
           
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var  file = Request.Files[i];
                this.eBookService.Upload(id, file);  
                         
            }



            return Json( new { success= true } , JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult RemovePage(string id)
        {
            this.eBookService.RemovePage(id);


            var msg = new { Success = true };
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePageOrder(IList<EBookPageModel> posts)
        {

			foreach (var item in posts)
			{
				this.eBookService.UpdatePageOrder(item.Id, item.Sort);
			}


			var msg = new { Success = true };
			return Json(msg, JsonRequestBehavior.AllowGet);
		}

		
	}
}