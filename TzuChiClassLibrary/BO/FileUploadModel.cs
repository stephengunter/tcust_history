using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    public class FileUploadModel
    {
        public const string FUNCTIONOID_IMAGE = "Image";
        public const string FUNCTIONOID_VIDEO = "Video";
        public const string FUNCTIONOID_COVERIMAGE = "CoverImage";
        public const string FUNCTIONOID_CONTNETIMAGE = "ContentImage";
        public const string FUNCTIONOID_ATTACHMENTIMAGE = "AttachmentImage";    // 附件(單圖)

        public string ItemOID { get; set; }
        public string ContentID { get; set; }
        public string FunctionOID { get; set; }//檔案識別
        public string FunctionType { get; set; }//檔案類型(PDF,ZIP,XLS,JPG,PNG...等)
        public string ItemName { get; set; }//檔案名稱
        public string ItemInfo { get; set; }//檔案資訊
        public string FileName { get; set; }//檔案名稱
        public string Path { get; set; }//目錄路徑
        public string Bit { get; set; }//上傳的資料檔案大小
        public int Sort { get; set; }//排序
        public int ImageWidth { get; set; }//圖片ImageWidth
        public int ImageHeight { get; set; }//圖片ImageHeight
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public UInt64 ClickCount { get; set; }//點擊次數
        public Boolean CoverImage { get; set; }//是否為封面圖
        public string PreviewPath { get; set; }//預覽圖路徑 ( 20140929 )



        public string Type { get; set; }
        public string Index { get; set; }
    }
}
