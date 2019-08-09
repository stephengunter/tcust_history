using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //大事記要
    public class EventCalendarModel : BasePost
    {
        public const string TYPEID = "d20f2547-6197-42b4-8005-cca7e8c29caa";
        public string ContentID { get; set; }
        [Required(ErrorMessage = "請選擇學年度")]
        public string AcademicYear { get; set; }                  // 學年度
        [Required(ErrorMessage = "請選擇事件日期")]
        public DateTime ContentTime { get; set; }                 // 事件日期        
        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(50, ErrorMessage = ("標題長度不得大於50字元"))]
        public string ContentName { get; set; }                   // 標題
        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string Description { get; set; }                   // 內容描述


     



        public string ContentCreator { get; set; }                // 建立者
        public DateTime ContentCreateTime { get; set; }           // 建立時間
        public string ContentUpdater { get; set; }                // 更新者
        public DateTime ContentUpdateTime { get; set; }           // 更新時間
        public Int32 TotalNum { get; set; }


        [StringLength(4000, ErrorMessage = ("內容長度過長"))]
        public string ContentText { get; set; }


    }
}
