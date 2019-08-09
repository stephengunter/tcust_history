using System;
using NLog;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using TzuChiClassLibrary.BO;
using System.Collections.Generic;

namespace TzuChiClassLibrary.Utils
{
    public class VideoProcessing
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //截取影片預覽圖
        public static void IntoImage(string imgPath, string videoPath) {

            string ffmpegPath = System.Web.HttpContext.Current.Server.MapPath("~/Referanslar/ffmpeg.exe");

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = ffmpegPath;
            process.StartInfo.Arguments = "-i " + videoPath + " -y -f image2 -t 0.010 -s 480x360 -ss 00:00:05 " + imgPath ;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
            }
            finally 
            {
                process.Close();
            }
      
        }

        //依照給與目標資料夾，取得Video資料夾下的Video清單以及預覽圖片
        public static IEnumerable<FileUploadModel> GetVideoList(String imgFilePath, String videoFilePath)
        {
            if (String.IsNullOrEmpty(imgFilePath) || String.IsNullOrEmpty(videoFilePath))
                return null;

            List<FileUploadModel> fileCollection = new List<FileUploadModel>();
            try
            {
                foreach (String videoPath in Directory.GetFiles(videoFilePath, "*.mp4"))
                {
                    String videoName = Path.GetFileNameWithoutExtension(videoPath);
                    String jpgPath = imgFilePath + "/" + videoName + ".jpg";//應對jpg

                    FileUploadModel fileInfo = new FileUploadModel();

                    if (File.Exists(jpgPath))
                    {
                        fileInfo.PreviewPath = jpgPath;
                    }
                    else
                    {
                        VideoProcessing.IntoImage(jpgPath, videoPath);
                    }

                    fileInfo.FileName = videoName;
                    fileInfo.Path = videoPath.Replace(HttpContext.Current.Server.MapPath("~/"), "/").Replace(@"\", @"/");
                    fileInfo.PreviewPath = jpgPath.Replace(HttpContext.Current.Server.MapPath("~/"), "/");
                    fileCollection.Add(fileInfo);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return fileCollection;
        }

    }
}
