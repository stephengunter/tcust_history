using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //資訊查詢 -> 畢業紀念冊
    public class ClassBookModel
    {
        public string ClassBookID { get; set; }
        public string AcademicYear { get; set; }            //學年度
        public string Department { get; set; }              //科系
        public string Class { get; set; }                   //班級
        public string StudentId { get; set; }               //學號
        public string StudentName { get; set; }             //學生名
        public string PictureUrl { get; set; }              //圖片位置
        public string Page { get; set; }                    //頁碼
        public Boolean IsTurnLeft { get; set; }             //翻頁方向 是否往左
    }
}
