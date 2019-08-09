using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    public abstract class BasePost
    {
        public List<FileUploadModel> PicUrlList { get; set; }     // 該事件存在的圖片
        public List<FileUploadModel> VideoUrlList { get; set; }   // 該事件存在的影片
    }
}
