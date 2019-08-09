using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace TzuChiBackend.Controllers
{
    [Authorize]
    public class ClassBookController : Controller
    {
        // 畢業紀念冊 蓋檔頁面
        // GET: ClassBook
        public ActionResult List()
        {
            return View();
        }

        public ActionResult test()
        {
            string errorMsg = "";
            //string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["FrontRootPath"]
           //    + System.Web.Configuration.WebConfigurationManager.AppSettings["ClassBookPath"];

           
            TzuChiClassLibrary.Utils.ExcelImportDb excelImport = new TzuChiClassLibrary.Utils.ExcelImportDb();
            if (excelImport.OpenExcel())
            {

                if (!excelImport.ClassBookHandler())
                {
                    errorMsg += "匯入畢業紀念冊失敗。";
                }
                if (!excelImport.DirectorsHandler())
                {
                    errorMsg += "匯入歷屆董事失敗。";
                }
            }
            else
            {
                //讀excel發生錯誤
                errorMsg = "讀取Excel時發生錯誤。";
            }
            return Content(errorMsg);
        }

        [HttpGet]
        public ActionResult DownloadClassBook()
        {
            string fullPath = WebConfigurationManager.AppSettings["FrontRootPath"] + WebConfigurationManager.AppSettings["ClassBookPath"];
            return File(fullPath, "application/vnd.ms-excel", "classbook.xls");
        }
    }
}