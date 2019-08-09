using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //資訊查詢 -> 校園日誌
    //後台榮譽榜與此功能合併
    public class SchoolDiaryModel:BasePost
    {
        public const string TYPEID = "7f266b34-c03e-41ae-84a1-1e27dccc7039";

        public string ContentID { get; set; }
        
        [Required(ErrorMessage = "請輸入編號")]
        [StringLength(30, ErrorMessage = ("編號長度不得大於30字元"))]
        public string SerialNo { get; set; }                //編號

        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(50, ErrorMessage = ("標題長度不得大於50字元"))]
        public string ContentName { get; set; }             //標題

        [Required(ErrorMessage = "請選擇類別")]
        public List<string> ContentType { get; set; }             //類別
        
        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string Description { get; set; }             //內容

        [Required(ErrorMessage = "請選擇日期")]
        public DateTime ContentTime { get; set; }           //日期

        [Required(ErrorMessage = "請選擇學年度")]
        public string AcademicYear { get; set; }            //學年

        [Required(ErrorMessage = "請選擇學期")]
        public string Semester { get; set; }                //學期
        [Url]
        public string RelatedLink { get; set; }             // 相關連結 ( for create QR code ) 20140929

        public FileUploadModel CoverImage { get; set; }     //封面圖片

        public FileUploadModel ContentImage { get; set; }   //內容圖片

       
        public Boolean ShowDate { get; set; }               // 20141006 有無活動起迄日
        public DateTime? OpenTime { get; set; }              // 20141006 起 日期
        public DateTime? CloseTime { get; set; }             // 20141006 迄 日期
        public string ContentCreator { get; set; }

        public DateTime ContentCreateTime { get; set; }
        
        public string ContentUpdater { get; set; }

        public DateTime ContentUpdateTime { get; set; }

        public Int32 TotalNum { get; set; }

        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string ContentText { get; set; }

      
        [StringLength(256, ErrorMessage = ("作者長度不得大於256字元"))]
        public string Author { get; set; }
    }
}
