using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //資訊查詢 -> 大愛新聞
    public class NewsModel:BasePost
    {
        public const string TYPEID = "5ccee554-0fc6-4752-bc58-3bf2b2f8cc97";
        public string ContentID { get; set; }

        [Required(ErrorMessage = "請輸入編號")]
        [StringLength(30, ErrorMessage = ("編號長度不得大於30字元"))]
        public string SerialNo { get; set; }                         //編號
        public FileUploadModel CoverImage { get; set; }
        public FileUploadModel ContentImage { get; set; }
        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(50, ErrorMessage = ("標題長度不得大於50字元"))]
        public string ContentName { get; set; }                     // 標題 ( for keyword search)

        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string Description { get; set; }                     // 內容 ( for keyword search)
        [Required(ErrorMessage = "請選擇新聞時間")]
        public DateTime ContentTime { get; set; }                   // 新聞日期
        [Required(ErrorMessage = "請選擇學年度")]
        public string AcademicYear { get; set; }                    // 學年
        [Url]
        public string RelatedLink { get; set; }                     // 相關連結 ( for create QR code ) 20140929
        public Boolean ShowDate { get; set; }                       // 20141006 有無活動起迄日
        public DateTime? OpenTime { get; set; }                     // 20141006 起 日期
        public DateTime? CloseTime { get; set; }                    // 20141006 迄 日期
        public string ContentCreator { get; set; }                  // 建立者
        public DateTime ContentCreateTime { get; set; }             // 建立時間
        public string ContentUpdater { get; set; }                  // 更新者
        public DateTime ContentUpdateTime { get; set; }             // 更新時間
        public Int32 TotalNum { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "請填入數字")]
        public int DisplayOrder { get; set; }

        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string ContentText { get; set; }

        [StringLength(256, ErrorMessage = ("作者長度不得大於256字元"))]
        public string Author { get; set; }                  
    }
}
