using NLog;
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TzuChiBackend.Helpers;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.Utils;

namespace TzuChiBackend.Controllers
{    
    public class FileUtilityController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string uploadDir = string.Format("~/{0}/", WebConfigurationManager.AppSettings["upload.folder.name"]);

        /// <summary>
        /// 縮放圖片
        /// </summary>
        /// <param name="thumbnailType">圖片縮放方式（crop:截圖  (default): 依比例縮圖）</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <param name="imgPath">圖片路徑</param>
        /// <returns></returns>
        public string ZoomImg(string thumbnailType, string width, string height, string imgPath)
        {

            string uploadDir = string.Format("~/{0}/", WebConfigurationManager.AppSettings["upload.folder.name"]);
            string imgSourcePath = Server.MapPath(uploadDir + imgPath);

            try
            {
                if (!System.IO.File.Exists(imgSourcePath))
                    return string.Format("圖片路徑無效:{0}", imgSourcePath);

                string extension = (System.IO.Path.HasExtension(imgSourcePath)) ?
                                              System.IO.Path.GetExtension(imgSourcePath).Substring(1).ToUpper() :
                                              string.Empty;
                if (!("JPG".Equals(extension) || "GIF".Equals(extension) || "PNG".Equals(extension)))
                    return string.Format("圖片格式錯誤:{0}", imgSourcePath);

                int resizeWidth = 0, resizeHeight = 0;
                if (!string.IsNullOrEmpty(width))
                    int.TryParse(width.Trim(), out resizeWidth);
                if (!string.IsNullOrEmpty(height))
                    int.TryParse(height.Trim(), out resizeHeight);

                // 長寬數值不正確時, 回傳原圖
                if (resizeWidth <= 0 || resizeHeight <= 0)
                {
                    SendOriginalImage(imgSourcePath);
                    return string.Empty;
                }

                Image imgSource = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(imgSourcePath)));
                Image imgResized = null;

                switch (thumbnailType)
                {
                    case "crop":
                        #region 截圖
                        bool isSizeChanged = false;
                        // 計算截圖範圍
                        int cropWidth = imgSource.Width, cropHeight = imgSource.Height;
                        if (resizeWidth < imgSource.Width && resizeHeight < imgSource.Height)
                        {
                            if (1.0 * imgSource.Width / resizeWidth * resizeHeight <= imgSource.Height)
                            {
                                cropHeight = (int)(1.0 * imgSource.Width / resizeWidth * resizeHeight);
                                isSizeChanged = true;
                            }
                            else if (1.0 * imgSource.Height / resizeHeight * resizeWidth <= imgSource.Width)
                            {
                                cropWidth = (int)(1.0 * imgSource.Height / resizeHeight * resizeWidth);
                                isSizeChanged = true;
                            }
                        }
                        else if (resizeWidth < imgSource.Width && resizeHeight >= imgSource.Height)
                        {
                            cropWidth = (int)(1.0 * imgSource.Height / resizeHeight * resizeWidth);
                            isSizeChanged = true;
                        }
                        else if (resizeWidth >= imgSource.Width && resizeHeight < imgSource.Height)
                        {
                            cropHeight = (int)(1.0 * imgSource.Width / resizeWidth * resizeHeight);
                            isSizeChanged = true;
                        }
                        else
                        {
                            // 原圖
                        }

                        // 進行截圖及縮圖
                        if (isSizeChanged)
                        {
                            imgResized = ImageResizer.Crop(imgSource, cropWidth, cropHeight, AnchorPosition.Center);
                            imgResized = ImageResizer.ScaleByFixedSize(imgResized, resizeWidth, resizeHeight);
                        }
                        imgSource.Dispose();
                        #endregion
                        break;
                    default:
                        #region 依比例縮圖
                        // 計算大小
                        if (resizeWidth <= imgSource.Width && resizeHeight >= (1.0 * imgSource.Height * resizeWidth / imgSource.Width))
                        {
                            resizeHeight = (int)(1.0 * imgSource.Height * resizeWidth / imgSource.Width);
                        }
                        else if (resizeHeight <= imgSource.Height && resizeWidth >= (1.0 * imgSource.Width * resizeHeight / imgSource.Height))
                        {
                            resizeWidth = (int)(1.0 * imgSource.Width * resizeHeight / imgSource.Height);
                        }

                        // 如果需要縮圖
                        if (resizeWidth < imgSource.Width || resizeHeight < imgSource.Height)
                        {
                            imgResized = ImageResizer.ScaleByFixedSize(imgSource, resizeWidth, resizeHeight);
                        }
                        imgSource.Dispose();
                        #endregion
                        break;

                }

                // 輸出縮圖
                if (imgResized != null)
                {
                    Response.ContentType = "image/jpeg";
                    imgResized.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Response.CacheControl = "public";
                    Response.Cache.SetExpires(DateTime.Now.AddDays(1));
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                }
                else
                {
                    SendOriginalImage(imgSourcePath);
                    Response.CacheControl = "public";
                    Response.Cache.SetExpires(DateTime.Now.AddDays(1));
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                }
            }
            catch { }

            return string.Empty;
        }

        /// <summary>
        /// 傳回原始圖片
        /// </summary>
        /// <param name="context"></param>
        /// <param name="imgSourcePath"></param>
        private void SendOriginalImage(string imgSourcePath)
        {
            switch (Path.GetExtension(imgSourcePath).ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    Response.ContentType = "image/jpeg";
                    break;
                case ".png":
                    Response.ContentType = "image/png";
                    break;
                case ".gif":
                    Response.ContentType = "image/gif";
                    break;
            }
            Response.WriteFile(imgSourcePath);
        } // SendOriginalImage()

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase filedata)
        {
            if (filedata != null && filedata.ContentLength > 0)
            {
                try
                {
                    //檢查檔案路徑
                    string filepath = this.Server.MapPath(uploadDir + DateTime.Now.ToString("yyyyMMdd"));
                    if (!Directory.Exists(filepath))
                        Directory.CreateDirectory(filepath);

                    FileUploadModel fileUploadModel = this.HttpPostedFileToFileUpload(filedata);

                    var fileMapPath = this.Server.MapPath(uploadDir + fileUploadModel.Path);
                    filedata.SaveAs(fileMapPath);

                    //截取影片預覽圖
                    string functionOID_Video = FileUploadModel.FUNCTIONOID_VIDEO;
                    if (functionOID_Video.Equals(fileUploadModel.FunctionOID))
                    {
                        string imgPath = this.Server.MapPath(uploadDir + Path.ChangeExtension(fileUploadModel.Path, ".jpg"));
                        string videoPath = this.Server.MapPath(uploadDir + fileUploadModel.Path);
                        VideoProcessing.IntoImage(imgPath, videoPath);
                        fileUploadModel.PreviewPath = Path.ChangeExtension(fileUploadModel.Path, ".jpg");
                    }

                    fileUploadModel.Type = Request["Type"];
                    fileUploadModel.Index = DateTime.Now.ToTimeNumber().ToString();

                    return PartialView("_FileUpload", fileUploadModel);

                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                    return Json(string.Empty);
                }
            } // if (Filedata != null && Filedata.ContentLength > 0)
            return Json(string.Empty);

        } // UploadFile()

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase filedata)
        {
            if (filedata != null && filedata.ContentLength > 0)
            {
                try
                {
                    //檢查檔案路徑
                    string filepath = this.Server.MapPath(uploadDir + DateTime.Now.ToString("yyyyMMdd"));
                    if (!Directory.Exists(filepath))
                        Directory.CreateDirectory(filepath);

                    FileUploadModel fileUploadModel = this.HttpPostedFileToFileUpload(filedata);

                    var fileMapPath = this.Server.MapPath(uploadDir + fileUploadModel.Path);
                    filedata.SaveAs(fileMapPath);

                    //截取影片預覽圖
                    string functionOID_Video = FileUploadModel.FUNCTIONOID_VIDEO;
                    if (functionOID_Video.Equals(fileUploadModel.FunctionOID))
                    {
                        string imgPath = this.Server.MapPath(uploadDir + Path.ChangeExtension(fileUploadModel.Path, ".jpg"));
                        string videoPath = this.Server.MapPath(uploadDir + fileUploadModel.Path);
                        VideoProcessing.IntoImage(imgPath, videoPath);
                        fileUploadModel.PreviewPath = Path.ChangeExtension(fileUploadModel.Path, ".jpg");
                    }

                    fileUploadModel.Type= Request["Type"];


                    return Json(fileUploadModel);

                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                    return Json(string.Empty);
                }
            } // if (Filedata != null && Filedata.ContentLength > 0)
            return Json(string.Empty);

        } // UploadFile()

        public ActionResult UploadFileWithZoom(HttpPostedFileBase filedata)
        {
            if (filedata != null && filedata.ContentLength > 0)
            {
                try
                {
                    //檢查檔案路徑
                    string filepath = this.Server.MapPath(uploadDir + DateTime.Now.ToString("yyyyMMdd"));
                    if (!Directory.Exists(filepath))
                        Directory.CreateDirectory(filepath);

                    FileUploadModel fileUploadModel = this.HttpPostedFileToFileUpload(filedata);

                    var fileMapPath = this.Server.MapPath(uploadDir + fileUploadModel.Path);
                    filedata.SaveAs(fileMapPath);

                    //輸出縮圖
                    Image imgSource = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(fileMapPath)));
                    Image imgResized = null;
                    int resizeWidth = 750;
                    int resizeHeight = 516;
                    #region 依比例縮圖
                    // 計算大小
                    if (resizeWidth <= imgSource.Width && resizeHeight >= (1.0 * imgSource.Height * resizeWidth / imgSource.Width))
                    {
                        resizeHeight = (int)(1.0 * imgSource.Height * resizeWidth / imgSource.Width);
                    }
                    else if (resizeHeight <= imgSource.Height && resizeWidth >= (1.0 * imgSource.Width * resizeHeight / imgSource.Height))
                    {
                        resizeWidth = (int)(1.0 * imgSource.Width * resizeHeight / imgSource.Height);
                    }

                    // 如果需要縮圖
                    if (resizeWidth < imgSource.Width || resizeHeight < imgSource.Height)
                    {
                        imgResized = ImageResizer.ScaleByFixedSize(imgSource, resizeWidth, resizeHeight);
                    }
                    imgSource.Dispose();
                    #endregion
                    if (imgResized != null)
                    {
                        imgResized.Save(fileMapPath);
                    }

                    return Json(fileUploadModel);

                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                    return Json(string.Empty);
                }
            } // if (Filedata != null && Filedata.ContentLength > 0)
            return Json(string.Empty);

        } // UploadFileWithZoom()

        private FileUploadModel HttpPostedFileToFileUpload(HttpPostedFileBase filedata)
        {
            FileUploadModel result = new FileUploadModel();

            string itemID = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(filedata.FileName);
            string itemfilename = itemID + extension;

            switch (extension.ToLower())
            {
                case ".png":
                case ".jpg":
                case ".gif":
                    result.FunctionOID = FileUploadModel.FUNCTIONOID_IMAGE;
                    break;
                case ".mp4":
                    result.FunctionOID = FileUploadModel.FUNCTIONOID_VIDEO;
                    break;
                default:
                    result.FunctionOID = string.Empty;
                    break;
            }

            result.ItemOID = itemID;
            result.FunctionType = extension;
            result.FileName = System.IO.Path.GetFileName(filedata.FileName);
            result.Path = string.Format("{0}/{1}",
                                    DateTime.Now.ToString("yyyyMMdd"),
                                    itemfilename);
            // K byte
            result.Bit = (Math.Round((decimal)filedata.ContentLength / (1024))).ToString();

            //截取圖片大小
            string functionOID_Image = FileUploadModel.FUNCTIONOID_IMAGE;
            if (functionOID_Image.Equals(result.FunctionOID))
            {
                System.Drawing.Image image = Image.FromStream(filedata.InputStream, true, true);
                result.ImageHeight = image.Height;
                result.ImageWidth = image.Width;
            }

            return result;
        }//HttpPostedFileToFileUpload

        // GET: FileUtility
        [HttpPost]
        public JsonResult UploadMaintainData(HttpPostedFileBase filedata, string coverPath, string fileName)
        {
            if (filedata != null && filedata.ContentLength > 0)
            {
                try
                {
                    //指到前台路徑
                    coverPath = WebConfigurationManager.AppSettings["FrontRootPath"].ToString() + coverPath;

                    //檢查檔案路徑
                    if (!Directory.Exists(coverPath))
                        Directory.CreateDirectory(coverPath);

                    //原檔案移放處
                    if (!Directory.Exists(coverPath + "oldTemp/"))
                        Directory.CreateDirectory(coverPath + "oldTemp/");

                    //設定覆蓋完整路徑
                    var filePath = coverPath + fileName;

                    //若原有檔案存在則移動至 oldTemp 加上移動時間
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Copy(filePath, coverPath + "oldTemp/" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileName);
                        System.IO.File.Delete(filePath);
                    }

                    filedata.SaveAs(filePath);

                    // 上傳畢業紀念冊時，更新db資料
                    if (fileName.Contains("classbook")) 
                    {
                        string errorMsg = string.Empty;                         
                        TzuChiClassLibrary.Utils.ExcelImportDb excelImport = new ExcelImportDb();                        
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
                        return Json(errorMsg, JsonRequestBehavior.AllowGet);
                    }

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);

                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            } // if (Filedata != null && Filedata.ContentLength > 0)
            return Json(false, JsonRequestBehavior.AllowGet);
        } // 上傳維護資料（檔案上傳下拉式四項）

    }
}