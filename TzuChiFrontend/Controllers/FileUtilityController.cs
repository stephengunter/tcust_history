using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace TzuChiFrontend.Controllers
{
    public class FileUtilityController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string excelSubPath = WebConfigurationManager.AppSettings["ExcelSubPath"];

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase filedata)
        {
            Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (filedata != null && filedata.ContentLength > 0)
            {
                try
                {
                    string filepath = this.Server.MapPath(excelSubPath);
                    filedata.SaveAs(filepath);

                    return Json(true);

                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                    return Json(false);
                }
            } // if (Filedata != null && Filedata.ContentLength > 0)
            return Json(false);

        } // UploadFile()

        public ActionResult DownloadExcel()
        {
            string filepath = this.Server.MapPath(excelSubPath);

            if (!System.IO.File.Exists(filepath))
                return Content(string.Empty);

            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0};", HttpUtility.UrlEncode(excelSubPath.Split('/')[2], Response.HeaderEncoding)));
            Response.WriteFile(filepath);
            return Content(string.Empty);
        } // DownloadFile()

    }
}