using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //天涯比鄰 -> 境內
    public class PlanInsideModel
    {
        public const string CATEGORYQUTSIDEID_UNION = "85b5e9b1-f134-4d15-81b0-38e8c81f0f36"; //策略聯盟
        public const string CATEGORYQUTSIDEID_INDUSTRYCOOPERATION = "ab004ad5-47c6-4942-baae-f6fe97ca10bc"; //產學合作
        public const string CATEGORYQUTSIDEID_INTERNSHIP = "baed9b98-d212-481c-bbcf-2183e242217b"; //企業實習

        public const string TYPEID = "9f493161-f252-488d-b627-a68f0aac4799";
        public string ContentID { get; set; }
        public string CategoryPrepID { get; set; }                      // 產學類型 - PlanInside
        [Required(ErrorMessage = "請選擇年度")]
        public string CategoryYearID { get; set; }                      // 年度 [1] - PlanInside
        public FileUploadModel AttachPicUrl { get; set; }               // 附件 ( 單圖 ) [1]
        [Required(ErrorMessage = "請選擇分區")]
        public string CategoryPartitionID { get; set; }                 // 分區 - PlanInside
        [Required(ErrorMessage = "請選擇地點")]
        public string CategorySiteID { get; set; }                      // 區域地點 - PlanInside
        [Required(ErrorMessage = "請輸入單位")]
        public string Department { get; set; }                          // 單位 - PlanInside
        public List<FileUploadModel> PicUrlList { get; set; }           // 圖片 (指定封面)
        //[Required(ErrorMessage = "請輸入座標")] 20141017 18:01 Cody取消產學合作必填
        public string ImageXY { get; set; }                             //關連座標
        public CoordinateModel Coordinate { get; set; }                 //座標
        public DateTime? ContentTime { get; set; }                       // 簽約日期
        public DateTime? EndTime { get; set; }                           // 結束日期 - PlanInside
        public string Description { get; set; }                         // 簡介
        public string Agencies { get; set; }                            // 實習地點 - PlanInside
        public string ContentName{ get; set; }                          // 標題        
        public string ContentCreator { get; set; }                      // 建立者
        public DateTime ContentCreateTime { get; set; }                 // 建立時間
        public string ContentUpdater { get; set; }                      // 更新者
        public DateTime ContentUpdateTime { get; set; }                 // 更新時間
        public Int32 TotalNum { get; set; }

    }
}
