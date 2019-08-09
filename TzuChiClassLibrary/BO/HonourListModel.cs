using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //後台榮譽榜與校園日誌合併
    public class HonourListModel
    {
        public const string TYPEID = "4af854f1-d569-4394-9a89-3ac1383734c0";
        public string ContentID { get; set; }
        [Required(ErrorMessage = "請選擇事件時間")]
        public DateTime ContentTime { get; set; }                 // 榮譽事件日期
        [Required(ErrorMessage = "請輸入編號")]
        [StringLength(30, ErrorMessage = ("編號長度不得大於30字元"))]
        public string ContentName { get; set; }                   // 標題 ( for keyword search AND SHOW)
        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string Description { get; set; }                   // 內容 ( for keyword search)
        public string RelatedLink { get; set; }                   // 相關連結 ( for create QR code ) 20140929
        public FileUploadModel ContentImage { get; set; }         // 活動日誌
        public List<FileUploadModel> PicUrlList { get; set; }     // 照片記錄
        public List<FileUploadModel> VideoUrlList { get; set; }   // 影片記錄
        public string ContentCreator { get; set; }                // 建立者
        public DateTime ContentCreateTime { get; set; }           // 建立時間
        public string ContentUpdater { get; set; }                // 更新者
        public DateTime ContentUpdateTime { get; set; }           // 更新時間
        public Int32 TotalNum { get; set; }

        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string ContentText { get; set; }


        [StringLength(256, ErrorMessage = ("作者長度不得大於256字元"))]
        public string Author { get; set; }
    }
}
