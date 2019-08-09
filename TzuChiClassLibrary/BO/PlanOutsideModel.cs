using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //天涯比鄰 -> 境外
    public class PlanOutsideModel
    {
        public const string TYPEID = "9f9670da-f644-47f7-bd4f-312ebaf34963";
        public const string CATEGORYQUTSIDEID_SISTER = "1ed63661-7538-40df-a5e8-e58c76bbf3a5";  //姊妹校
        public const string CATEGORYQUTSIDEID_DEGREE = "7ccbdebf-9ac8-4a12-9274-651f299e51aa";  //雙聯學位
        public const string CATEGORYQUTSIDEID_OVERSEA = "be7892c3-56d1-41e3-aa55-d8b9b764c53c"; //海外交流
        public const string CATEGORYQUTSIDEID_INDUSTRYCOOPERATION = "d88c0ee7-d872-465c-a2ec-85b2409a8bb2"; //產學合作
        public const string CATEGORYQUTSIDEID_INTERNSHIP = "609bbb2f-2e6a-4850-97c6-381cb2dfe8f4"; //企業實習

        public string ContentID { get; set; }
        [Required(ErrorMessage = "請選擇境外類型")]
        public string CategoryOutsideID { get; set; }           // 境外類型 - PlanOutside
        [Required(ErrorMessage = "請選擇國別")]
        public string CategoryCountryID { get; set; }           // 國別 - PlanOutside
        public string CategoryDepartmentID { get; set; }        // 系所 ( 簽約單位 ) - PlanOutside
        [Required(ErrorMessage = "請輸入中文校名")]
        public string ContentName { get; set; }                 // 學校名稱 - 中文 - Content
        [Required(ErrorMessage = "請輸入英文校名")]
        public string EnglishName { get; set; }                 // 學校名稱 - 英文 - PlanOutside
        public DateTime? ContentTime { get; set; }               // 締結 ( 交流 ) 開始日期 - Content
        public DateTime? EndTime { get; set; }                   // 締結 ( 交流 ) 結束日期 - PlanOutside
        public string Description { get; set; }                 // 活動名稱 ( 20141003 ) - Content
        public string IntroCh { get; set; }                     // 校園簡介-中文 ( 20140929 )
        public string IntroEn { get; set; }                     // 校園簡介-英文 ( 20140929 )  
        public List<FileUploadModel> PicUrlList { get; set; }   // 相簿
        [Required(ErrorMessage = "請輸入地圖座標")]
        public string ImageXY { get; set; }                     //關連座標
        public CoordinateModel Coordinate { get; set; }         //座標
        public FileUploadModel AttachPicUrl { get; set; }       // 附圖 ( 單圖 ) [4, 5]
        public string ContentCreator { get; set; }              // 建立者
        public DateTime ContentCreateTime { get; set; }         // 建立時間
        public string ContentUpdater { get; set; }              // 更新者
        public DateTime ContentUpdateTime { get; set; }         // 更新時間
        public Int32 TotalNum { get; set; }
    }
}
