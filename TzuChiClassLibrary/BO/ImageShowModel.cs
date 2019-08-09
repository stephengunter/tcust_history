using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    public class ImageShowModel
    {
        public const string SERVICELEARN_TYPEID = "1af844f1-d569-4z94-9a8k-3ac1383734jj";   //慈濟人文學程 -> 服務學習
        
        public string ImageShowID { get; set; }
        public string TypeID { get; set; }
        public string ContentID { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set;}
        public int ImageWidth { get; set;}
        public int ImageHeight { get; set;}
        public string Creator { get; set;}
        public DateTime CreateTime { get; set;}
        public string Updater { get; set;}
        public DateTime UpdateTime { get; set; }
        public Int32 TotalNum { get; set; }
    }
}
